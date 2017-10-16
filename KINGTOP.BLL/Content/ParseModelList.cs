#region 引用程序集using System;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using KingTop.Common;
using KingTop.Model;
using KingTop.IDAL;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-04-16
// 功能描述：解析模型字段 -- 模型编辑页// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region 创建模型列表页面
        /// <summary>
        /// 创建模型列表页面
        /// </summary>
        private void CreateList()
        {
            string pageContent;          // 页面代码
            string templatePath;         // 列表页模板地址
            string saveFilePath;         // 生成的列表页存储地址
            StringBuilder serverControl; // 用于保存参数的服务器控件
            string[] listInfo;           // [0] 记录列表标题  [1] 记录列表项

            serverControl = new StringBuilder();

            templatePath = "ControlManageListTemplate.aspx";
            saveFilePath = HttpContext.Current.Server.MapPath(PageName + "list.aspx");

            if (this.dtField.Rows.Count < 1 || this.hsModel.Count < 1)
            {
                return;
            }

            if (!string.IsNullOrEmpty(this.hsModel["ListHeight"].ToString().Trim()) && Utils.ParseInt(this.hsModel["ListHeight"].ToString().Trim(),0) > 0) // 设置列表列显示高
            {
                this.listHeight = "height:" + this.hsModel["ListHeight"].ToString().Trim() + "px;";
            }
            else
            {
                this.listHeight = "";
            }

            pageContent = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath), Encoding.Default);
            listInfo = ListInfo();
            pageContent = pageContent.Replace("<!--{$ParseModel.Include$}-->", GetInclude(false));                                                                                    // 加入引用的文件            pageContent = pageContent.Replace("<!--{$ParseModel.ListLink$}-->", ListLink(false));                                                                                     // 文本链接
            pageContent = pageContent.Replace("<!--{$ParseModel.ListButton$}-->", ListLink(true));                                                                                    // 按钮链接
            pageContent = pageContent.Replace("<!--{$ParseModel.ListInfo.Header$}-->", listInfo[0]);                                                                                 // 列表标题
            pageContent = pageContent.Replace("<!--{$ParseModel.ListInfo.Item$}-->", listInfo[1]);                                                                                    // 列表
            pageContent = pageContent.Replace("<!--{$ParseModel.Search$}-->", Search());                                                                                              // 搜索
            pageContent = pageContent.Replace("{#EditLink#}", PageName+ "edit.aspx");                                                // 预定义标签替换            pageContent = pageContent.Replace("{#ListLink#}", PageName + "list.aspx");
            pageContent = pageContent.Replace("{#ViewLink#}", PageName + "view.aspx");
            serverControl.Append("<asp:HiddenField ID=\"hdnNotSearchField\" runat=\"server\" value=\"" + this.hsModel["NotSearchField"] + "\" />");                                   // 不许参与搜索的字段            serverControl.Append("<asp:HiddenField ID=\"hdnBackDeliverUrlParam\" runat=\"server\" value=\"" + this.hsModel["BackDeliverUrlParam"] + "\"  /> ");                       // 返回时传递的参数
            serverControl.Append("<asp:HiddenField ID=\"hdnModelID\" Value=\"" + this.hsModel["ID"] + "\" runat=\"server\" />");
            serverControl.Append("<asp:HiddenField ID=\"hdnTableName\" Value=\"" + this.hsModel["TableName"] + "\" runat=\"server\" />");
            serverControl.Append("  <asp:HiddenField ID=\"hdnDeliverAndSearchUrlParam\" runat=\"server\"  Value=\"" + this.hsModel["DeliverAndSearchUrlParam"].ToString() + "\"/>");
            serverControl.Append(" <input type=\"hidden\" value=\""+ this.keepUrlParam +"\" id=\"HQB_Model_DeliverUrlParam\" />");
            serverControl.Append("<asp:HiddenField ID=\"hdnCustomCol\" Value=\"" + this.tableReferenceInfo + "\" runat=\"server\" />"); // 自定义列相关参数
            // 自定义列的基本字段中，数据来自数据库的相关参数 
            // 格式：引用表名|表名.字段名称|Text引用列名|value引用列名 多个用 ","隔开
            serverControl.Append("   <asp:HiddenField ID=\"hdnForignTableCol\" value=\"" + this.listForignCol + "\" runat=\"server\" />");
            this.showColumn = this.showColumn.Remove(0, 1);                                                                             // 移去起始位置的逗号
            this.showColumn = this.showColumn.Remove(this.showColumn.Length - 1, 1);                                                    // 移去最后一个多余的逗号
            serverControl.Append("<asp:HiddenField ID=\"hdnShowCol\" value=\"" + this.showColumn + "\" runat=\"server\" />");
            pageContent = pageContent.Replace("<!--$ParseModel.ServerControl$-->", serverControl.ToString());                           // 预设服务器控件

            if (pageContent.IndexOf("WdatePicker({lang:") == -1)
            {
                pageContent = pageContent.Replace("<script src=\"../Calendar/WdatePicker.js\" type=\"text/javascript\"></script>", "");
            }            File.Delete(saveFilePath);                                                                                                   // 如果列表页存在则删除
            File.WriteAllText(saveFilePath, HttpContext.Current.Server.HtmlDecode(pageContent), Encoding.UTF8);                       // 保存列表页        }
        #endregion

        #region 对齐类型
        private string GetListAlignment(object objValue)
        {
            string align = "";

            switch (objValue.ToString().Trim())
            {
                case "1":
                    align = "left";
                    break;
                case "2":
                    align = "center";
                    break;
                case "3":
                    align = "right";
                    break;
                default:
                    align = "left";
                    break;
            }

            return align;
        }
        #endregion

        #region 站点根目录URL
        private string GetRootUrl()
        {
            string rootURL;     // 站点URL
            string port;        // 站点端口号            string rootDir;     // 根目录
            rootURL = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            port = ":" + Convert.ToString(HttpContext.Current.Request.ServerVariables["SERVER_PORT"]);
            rootDir = HttpContext.Current.Request.ApplicationPath + "/";

            if (port.Trim() == ":80")
            {
                port = "";
            }

            rootURL = rootURL + port + rootDir;
            return rootURL;
        }
        #endregion

        #region 添加显示列（字段）名
        private void AddShowColumn(string columnName)
        {
            if (!this.showColumn.Contains("," + columnName + ","))
            {
                this.showColumn = this.showColumn + columnName + ",";
            }
        }
        #endregion
    }
}
