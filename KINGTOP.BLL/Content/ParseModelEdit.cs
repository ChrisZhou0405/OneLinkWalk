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
        #region 创建编辑页面
        private void CreateEdit()
        {
            string pageContent;          // 页面代码
            string templatePath;         // 列表页模板地址
            string saveFilePath;         // 生成的列表页存储地址
            StringBuilder serverControl; // 用于保存参数的服务器控件

            serverControl = new StringBuilder();

            templatePath = "ControlManageEditTemplate.aspx";
            saveFilePath = HttpContext.Current.Server.MapPath(PageName + "edit.aspx");
            pageContent = File.ReadAllText(HttpContext.Current.Server.MapPath(templatePath), Encoding.Default);                            // 模板内容
            pageContent = pageContent.Replace("<!--{$ParseModel.Include$}-->", GetInclude(true));                                          // 加入引用的文件
            pageContent = pageContent.Replace("<!--{$ParseModel.Fields$}-->", ParseEdit("IsInputValue=1"));                                // 字段解析
            serverControl.Append("<asp:HiddenField ID=\"hdnFieldFromUrlParamValue\" runat=\"server\" Value=\"" + this.hsModel["FieldFromUrlParamValue"].ToString() + "\" />");
            serverControl.Append("<asp:hiddenfield ID=\"hdnNodeCode\" runat=\"server\"/>");
            serverControl.Append("<asp:HiddenField Value=\"" + this.hsModel["TableName"] + "\" ID=\"hdnTableName\" runat=\"server\" />");  // 表名
            serverControl.Append("<asp:HiddenField Value=\"" + sbFieldParam.ToString() + "\" ID=\"hdnFieldValue\" runat=\"server\" />");   // 字段参数
            serverControl.Append("<asp:HiddenField ID=\"hdnModelID\" Value=\"" + this.modelID + "\" runat=\"server\" />");
            serverControl.Append("<input type=\"hidden\" id=\"hdnModelAlias\" value=\"" + this.hsModel["Title"] + "\" name=\"hdnModelAlias\"/>");
            pageContent = pageContent.Replace("<!--$ParseModel.ServerControl$-->", serverControl.ToString());
            pageContent = pageContent.Replace("{#EditLink#}", PageName + "edit.aspx");     // 预定义标签替换            pageContent = pageContent.Replace("{#ListLink#}", PageName + "list.aspx");
            pageContent = pageContent.Replace("{#ViewLink#}", PageName + "view.aspx");
            pageContent = pageContent.Replace("{#HtmlField#}", htmlFieldParam);
            if (pageContent.IndexOf("WdatePicker({lang:") == -1)  //日期控件
            {
                pageContent = pageContent.Replace("<script src=\"../Calendar/WdatePicker.js\" type=\"text/javascript\"></script>", "");
            }
            if (pageContent.IndexOf("KingTop.Config.Upload.GetConfig(GetUploadImgPath)") == -1)  //Ckeditor
            {
                pageContent = pageContent.Replace("<script type=\"text/javascript\" src=\"../Editor/ckeditor/ckeditor.js\"></script>", "");
                pageContent = pageContent.Replace("<link href=\"../Editor/ckeditor/content.css\" rel=\"stylesheet\" type=\"text/css\" />", "");
            }
            if (pageContent.IndexOf("AlbumsContainer').sortable") == -1)    //相册
            {
                pageContent = pageContent.Replace("<script src=\"../JS/ModelFieldAlbums.js\" type=\"text/javascript\"></script>", "");
                pageContent = pageContent.Replace("<script src=\"../js/jquery-ui-1.8.14.custom.min.js\" type=\"text/javascript\"></script>", "");
            }

            if (pageContent.IndexOf("$(function initColorPicker()") == -1)    //颜色代码
            {
                pageContent = pageContent.Replace("<script src=\"../ColorPicker/dhtmlxcommon.js\" type=\"text/javascript\"></script>", "");
                pageContent = pageContent.Replace("<script src=\"../ColorPicker/colorpicker.js\" type=\"text/javascript\"></script>", "");
                pageContent = pageContent.Replace("<link href=\"../ColorPicker/colorpicker.css\" rel=\"stylesheet\" type=\"text/css\" />", "");
            }
            
    
            File.Delete(saveFilePath);                                                                                                     // 如果列表页存在则删除
            File.WriteAllText(saveFilePath, HttpContext.Current.Server.HtmlDecode(pageContent), Encoding.UTF8);                         // 保存编辑页        }
        #endregion

        #region 编辑页解析
        public string ParseEdit(string fieldFilter)
        {
            DataView dvModelField;                      // 在编辑页面上显示的字段,包括基本字段与系统自定义字段
            string[] fieldGroupName;                    // 字段组名
            StringBuilder sbField;                      // 字段解析代码
            StringBuilder sbFieldGroup;                 // 字段组显示HTML代码
            string containerID;                         // 显示字段组容器ID
            bool isDisplay;                             // 当前组是否显示            string sysFieldIDMarker;                    // 标识系统配置ID是否已调用            string editFieldFilter;                     // 查找在编辑页中显示的字段的条件
            sbField = new StringBuilder();
            sbFieldGroup = new StringBuilder();

            isDisplay = true;
            editFieldFilter = fieldFilter;
            dvModelField = this.dtField.DefaultView;
            dvModelField.RowFilter = editFieldFilter;
            dvModelField.Sort = "Orders asc";                   // 字段显示顺序
            fieldGroupName = GetFieldGroupName(dvModelField);   // 获取当前模型的字段分组            containerID = Guid.NewGuid().ToString();            // 初始化分组HTML标签容器ID
            sysFieldIDMarker = ",";

            if (fieldGroupName != null && this.modelType == ParserType.Content)
            {
                sbFieldGroup.Append("<ul id=\"tags\">");
                sbField.Append("<div id=\"panel\">");

                foreach (string groupName in fieldGroupName)   // 遍历所有分组，字段按分组显示                {
                    if (string.IsNullOrEmpty(groupName))       // 组名为空则忽略                    {
                        continue;
                    }

                    if (isDisplay)   // 显示第一个分组字段                    {
                        sbFieldGroup.Append("<li class=\"selectTag\"><a href=\"javascript:;\">" + groupName + "</a> </li>");
                        sbField.Append("<fieldset>");
                        dvModelField.RowFilter = "("+ editFieldFilter +") and (ModelGroupName='" + groupName + "' or ModelGroupName is null)";  // 查找当前分组字段
                        isDisplay = false;
                    }
                    else             // 隐藏当前(非第一)分组                                           
                    {
                        sbFieldGroup.Append("<li><a href=\"javascript:;\">" + groupName + "</a> </li>");
                        sbField.Append("<fieldset style=\"display:none;\">");
                        dvModelField.RowFilter = "(" + editFieldFilter + ") and (ModelGroupName='" + groupName + "')";        // 查找当前分组字段
                    }
                    dvModelField.Sort = "Orders asc";

                    foreach (DataRowView dr in dvModelField)    // 遍历分组中所有字段                    {
                        if (Utils.ParseBool(dr["IsSystemFiierd"]) && !string.IsNullOrEmpty(dr["SystemFirerdHtml"].ToString()))  // 自定义系统字段                        {
                            string sysFieldID;
                            bool isSuccess;     // 当前字段解析是否成功

                            sysFieldID = dr["SystemFirerdHtml"].ToString().Trim();    // 自定义系统字段配置节点ID
                            switch (dr["SysFieldType"].ToString().Trim())
                            {
                                case "1":       // 自定义                                    if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))   // 自定义系统字段配置未调用
                                    {
                                        isSuccess = EditSysField(ref sbField, sysFieldID);
                                        if (!isSuccess) // 如果解析失败调用基本字段解析方法
                                        {
                                            EditBasicField(ref sbField, dr); 
                                        }

                                        sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                                    }
                                    else    // 已调用                                    {
                                        continue;
                                    }
                                    break;
                                case "2":       // 推荐区域
                                    RecommendArea(ref sbField, dr);
                                    break;
                                default:        // 自定义                                    isSuccess = EditSysField(ref sbField, sysFieldID);
                                    if (!isSuccess) // 如果解析失败调用基本字段解析方法
                                    {
                                        EditBasicField(ref sbField, dr);
                                    }

                                    if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))   // 自定义系统字段配置未调用
                                    {
                                        EditSysField(ref sbField, sysFieldID);
                                        sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                                    }
                                    else    // 已调用                                    {
                                        continue;
                                    }
                                    break;
                            }
                        }
                        else // 基本字段
                        {
                            EditBasicField(ref sbField, dr); 
                        }
                    }
                    sbField.Append("<div style=\"clear:left\"></div></fieldset>");
                    containerID = Guid.NewGuid().ToString();
                }

                sbFieldGroup.Append("</ul>");
            }
            else
            {

                sbField.Append("<div id=\"panel\">");
                sbField.Append("");

                foreach (DataRowView dr in dvModelField)
                {
                    if (Utils.ParseBool(dr["IsSystemFiierd"]) && !string.IsNullOrEmpty(dr["SystemFirerdHtml"].ToString()))  // 自定义系统字段                    {
                        string sysFieldID;
                        bool isSuccess;     // 当前字段解析是否成功

                        sysFieldID = dr["SystemFirerdHtml"].ToString().Trim();    // 自定义系统字段配置节点ID
                        switch (dr["SysFieldType"].ToString().Trim())
                        {
                            case "1":       // 自定义                                if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))   // 自定义系统字段配置未调用
                                {
                                    isSuccess = EditSysField(ref sbField, sysFieldID);
                                    if (!isSuccess) // 如果解析失败调用基本字段解析方法
                                    {
                                        EditBasicField(ref sbField, dr);
                                    }

                                    sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                                }
                                else    // 已调用                                {
                                    continue;
                                }
                                break;
                            case "2":       // 推荐区域
                                RecommendArea(ref sbField, dr);
                                break;
                            default:        // 自定义                                if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))   // 自定义系统字段配置未调用
                                {
                                    isSuccess = EditSysField(ref sbField, sysFieldID);
                                    if (!isSuccess) // 如果解析失败调用基本字段解析方法
                                    {
                                        EditBasicField(ref sbField, dr);
                                    }

                                    sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                                }
                                else    // 已调用                                {
                                    continue;
                                }
                                break;
                        }
                    }
                    else // 基本字段
                    {
                        EditBasicField(ref sbField, dr);
                    }
                }
            }

            sbField.Append("</div>");
            return sbFieldGroup.ToString() + sbField.ToString();
        }
        #endregion

        #region 获取字段组        private string[] GetFieldGroupName(DataView dvModelField)
        {
            string groupName;   // 保存字段组名，无重复

            groupName = null;

            foreach (DataRowView dr in dvModelField)    // 遍历所有字段筛选出组名
            {
                if (!string.IsNullOrEmpty(dr["ModelGroupName"].ToString()))
                {
                    if (string.IsNullOrEmpty(groupName)) 
                    {
                        groupName = dr["ModelGroupName"].ToString();
                    }
                    else 
                    {
                        if(!groupName.Contains(dr["ModelGroupName"].ToString())) // 没有记录当前字段组名
                        {
                            groupName = groupName + "," + dr["ModelGroupName"].ToString();
                        }
                    }
                }
            }

            if(!string.IsNullOrEmpty(groupName))
            {
                return groupName.Split(new char[]{','});
            }
            else
            {
               return  null;
            }
        }
        #endregion
    }
}
