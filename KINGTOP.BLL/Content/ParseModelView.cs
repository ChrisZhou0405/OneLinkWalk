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
// 功能描述：解析模型字段 -- 浏览页面
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region 创建浏览页面
        private void CreateView()
        {
            string pageContent;          // 页面代码
            string templatePath;         // 列表页模板地址
            string saveFilePath;         // 生成的列表页存储地址
            StringBuilder serverControl; // 用于保存参数的服务器控件

            serverControl = new StringBuilder();

            templatePath = "ControlManageViewTemplate.aspx";
            saveFilePath = HttpContext.Current.Server.MapPath(PageName + "view.aspx");
            pageContent = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath), Encoding.Default);                            // 模板内容
            pageContent = pageContent.Replace("<!--{$ParseModel.Fields$}-->", ParseView());                                                // 字段解析
            serverControl.Append("<asp:HiddenField Value=\"" + this.hsModel["TableName"] + "\" ID=\"hdnTableName\" runat=\"server\" />");  // 表名
            serverControl.Append("<asp:HiddenField ID=\"hdnModelID\" Value=\"" + this.modelID + "\" runat=\"server\" />");
            pageContent = pageContent.Replace("<!--$ParseModel.ServerControl$-->", serverControl.ToString());
            pageContent = pageContent.Replace("{#EditLink#}", PageName + "edit.aspx");  // 预定义标签替换
            pageContent = pageContent.Replace("{#ListLink#}", PageName + "list.aspx");
            pageContent = pageContent.Replace("{#ViewLink#}", PageName + "view.aspx");
            File.Delete(saveFilePath);                                                                                                     // 如果列表页存在则删除
            File.WriteAllText(saveFilePath, HttpContext.Current.Server.HtmlDecode(pageContent), Encoding.UTF8);                         // 保存浏览页
        }
        #endregion

        #region 解析浏览页面
        /// <summary>
        /// 解析浏览页面
        /// </summary>
        private string ParseView()
        {
            DataView dvModelField;                      // 在编辑页面上显示的字段,包括基本字段与系统自定义字段
            string[] fieldGroupName;                    // 字段组名
            StringBuilder sbField;                      // 字段解析代码
            StringBuilder sbFieldGroup;                 // 字段组显示HTML代码
            bool isDisplay;                             // 当前组是否显示            string editFieldFilter;                     // 查找在编辑页中显示的字段的条件
            sbField = new StringBuilder();
            sbFieldGroup = new StringBuilder();

            isDisplay = true;
            editFieldFilter = "IsInputValue=1 or (IsSystemFiierd=1 and SystemFirerdHtml is not null and SystemFirerdHtml<>'')";
            dvModelField = this.dtField.DefaultView;
            dvModelField.RowFilter = editFieldFilter;
            dvModelField.Sort = "Orders asc";                   // 字段显示顺序
            fieldGroupName = GetFieldGroupName(dvModelField);   // 获取当前模型的字段分组
            sbField.Append("<div id=\"panel\">");
            if (fieldGroupName != null)
            {
                sbFieldGroup.Append("<ul id=\"tags\">");

                foreach (string groupName in fieldGroupName)   // 遍历所有分组，字段按分组显示
                {
                    if (string.IsNullOrEmpty(groupName))       // 组名为空则忽略
                    {
                        continue;
                    }

                    if (isDisplay)   // 显示第一个分组字段                    {
                        sbFieldGroup.Append("<li class=\"selectTag\"><a href=\"javascript:;\">" + groupName + "</a></li>");
                        sbField.Append("<fieldset>");
                        dvModelField.RowFilter = "(" + editFieldFilter + ") and (ModelGroupName='" + groupName + "' or ModelGroupName is null)";  // 查找当前分组字段
                        isDisplay = false;
                    }
                    else             // 隐藏当前(非第一)分组                                           
                    {
                        sbFieldGroup.Append("<li><a href=\"javascript:;\">" + groupName + "</a></li>");
                        sbField.Append("<fieldset style=\"display:none;\">");
                        dvModelField.RowFilter = "(" + editFieldFilter + ") and (ModelGroupName='" + groupName + "')";// 查找当前分组字段
                    }

                    dvModelField.Sort = "Orders asc";

                    foreach (DataRowView dr in dvModelField)                                // 遍历分组中所有字段                    {
                        string itemHeader;                                                  // 字段别名（标题）
                        string itemValue;                                                  
                        string fieldValue;                                                  // 字段值
                        if (string.Equals(dr["DropDownDataType"].ToString().Trim(), "1"))   // 基本字段中的单选、多选、列表选项由文本框输入
                        {
                            fieldValue = "<%=ctrManageView.ParseFieldValueToText(\"" + dr["ID"].ToString() + "\",hsFieldValue[\"" + dr["Name"].ToString() + "\"])%>";  
                        }
                        else
                        {
                            fieldValue = "<%=hsFieldValue[\"" + dr["name"] + "\"]%>"; 
                        }

                        itemHeader = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString() + "：");
                        itemValue = containerItemTemplate.Replace(containerContent, fieldValue);
                        itemValue = itemValue.Replace("<dd", "<dd id=\""+ this.hsModel["TableName"].ToString() + "_" + dr["Name"].ToString() + "\"");
                        sbField.Append(containerTemplate.Replace(containerContent, itemHeader + itemValue));
                    }
                    sbField.Append("<div style=\"clear:left\"></div></fieldset>");
                }

                sbFieldGroup.Append("</ul>");
            }
            else
            {
                foreach (DataRowView dr in dvModelField)
                {
                    string itemHeader;                                                  // 字段别名（标题）
                    string itemValue;
                    string fieldValue;                                                  // 字段值
                    if (string.Equals(dr["DropDownDataType"].ToString().Trim(), "1"))   // 基本字段中的单选、多选、列表选项由文本框输入
                    {
                        fieldValue = "<%=ctrManageView.ParseFieldValueToText(\"" + dr["ID"].ToString() + "\",hsFieldValue[\"" + dr["Name"].ToString() + "\"])%>";
                    }
                    else
                    {
                        fieldValue = "<%=hsFieldValue[\"" + dr["name"] + "\"]%>";
                    }

                    itemHeader = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString() + "：");
                    itemValue = containerItemTemplate.Replace(containerContent, fieldValue);
                    itemValue = itemValue.Replace("<dd", "<dd id=\"" + this.hsModel["TableName"].ToString() + "_" + dr["Name"].ToString() + "\"");
                    sbField.Append(containerTemplate.Replace(containerContent, itemHeader + itemValue));
                }
            }

            sbField.Append("</div>");
            return sbFieldGroup.ToString() + sbField.ToString() ;
        }
        #endregion
    }
}
