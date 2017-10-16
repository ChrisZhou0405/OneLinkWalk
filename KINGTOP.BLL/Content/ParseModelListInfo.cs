#region 程序集引用
using System;
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

using KingTop.Model;
using KingTop.IDAL;
using KingTop.Common;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-18
// 功能描述：模型列表解析类中的解析显示列表
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region 显示列表解析  UL LI 方式
        // 显示的列及顺序:复选框列(有链接按钮时显示)、 字段表中IsListEnable=1 、模型配置中自定义列表列、预定义操作列
        // 编辑页传入参数列： 添加 modelid&action=add  修改 modelid&id&action=edit
        // string[0] 列表标题  string[1]  列表项
        /// <summary>
        /// 显示列表解析
        /// </summary>
        /// <returns></returns>
        public string[] ListInfoForUL()
        {
            StringBuilder sbHeaderHtml;       // 列表标题
            StringBuilder sbItemHtml;         // 列表项
            string[] listInfo;
            bool isCheckColumn;               // 是否加复选框列
            DataView dvModelField;            // 在页面上显示的字段
            string sysFieldIDMarker;          // 标识系统配置ID是否已调用

            sbHeaderHtml = new StringBuilder();
            sbItemHtml = new StringBuilder();
            listInfo = new string[2];
            dvModelField = this.dtField.DefaultView;
            dvModelField.RowFilter = "IsListEnable=1";
            dvModelField.Sort = "ListOrders asc,Orders asc";
            isCheckColumn = true;
            sysFieldIDMarker = ",";

            if (this.hsModel["ListButton"].ToString().Trim() == "none$")   // 无连接栏
            {
                isCheckColumn = false;
            }

            if (this.listHeight.Trim() != "")
            {
                sbItemHtml.Append(" <ul style=\""+ this.listHeight +"\" class=\"ulheader ulbody\">");
            }
            else
            {
                sbItemHtml.Append(" <ul class=\"ulheader ulbody\">");
            }

            if (isCheckColumn)      // 解析复选框列
            {
                sbHeaderHtml.Append("<li  style=\"width:45px;\"><input type=\"checkbox\" name=\"SlectAll\" id=\"SlectAll\" /></li>");
                sbItemHtml.Append("<li style=\"width:45px;" + this.listHeight + "\"><input type=\"checkbox\" name=\"_chkID\" value=\"<%#Eval(\"ID\") %>\" /></li>");
            }

            foreach (DataRowView dr in dvModelField)            //  遍历当前模型字段
            {
                if (Utils.ParseBool(dr["IsSystemFiierd"]) && !string.IsNullOrEmpty(dr["SystemFirerdHtml"].ToString()))            // 自定义系统字段
                {
                    string sysFieldID;
                    sysFieldID = dr["SystemFirerdHtml"].ToString().Trim();           // 自定义系统字段配置节点ID

                    switch (dr["SysFieldType"].ToString())
                    {
                        case "1":          // 预定义或自定义系统字段
                            if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))          // 自定义系统字段配置未调用
                            {
                                bool result;        // 解析系统字段是否成功
                                result = ListSysField(ref sbHeaderHtml, ref sbItemHtml, sysFieldID);

                                if (!result)       // 解析系统字段失败时尝试调用基本字段解析
                                {
                                    ListBasicField(dr, ref sbHeaderHtml, ref sbItemHtml);
                                }

                                sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                            }
                            else    // 已调用
                            {
                                continue;
                            }

                            break;
                        case "2":           // 推荐区域
                            break;
                        default:
                            if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))          // 自定义系统字段配置未调用
                            {
                                bool result;        // 解析系统字段是否成功
                                result = ListSysField(ref sbHeaderHtml, ref sbItemHtml, sysFieldID);

                                if (!result)        // 解析系统字段失败时尝试调用基本字段解析
                                {
                                    ListBasicField(dr, ref sbHeaderHtml, ref sbItemHtml);
                                }

                                sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                            }
                            else    // 已调用
                            {
                                continue;
                            }

                            break;
                    }
                }
                else // 基本字段
                {
                    ListBasicField(dr, ref sbHeaderHtml, ref sbItemHtml);
                }
            }

            CustomColumn(ref sbHeaderHtml, ref sbItemHtml);       // 自定义列表列

            if (Utils.ParseBool(this.hsModel["IsOrderEdit"]))     // 排序编辑列
            {
                string orderListWidth;

                // 列表表头
                if (this.dtField.Select("name='orders'").Length > 0 && this.dtField.Select("name='orders'")[0]["ListWidth"] != null && !string.IsNullOrEmpty(this.dtField.Select("name='orders'")[0]["ListWidth"].ToString()))
                {
                    orderListWidth = this.dtField.Select("name='orders'")[0]["ListWidth"].ToString();

                    if (!orderListWidth.Contains("%"))
                    {
                        orderListWidth = orderListWidth + "px";
                    }
                }
                else
                {
                    orderListWidth = "80px";
                }

                sbHeaderHtml.Append("<li  style=\"width:" + orderListWidth + ";text-align:center\" >");
                sbHeaderHtml.Append("<a href=\"javascript:sort('" + this.hsModel["TableName"] + ".Orders','3')\">");
                sbHeaderHtml.Append("排序");
                sbHeaderHtml.Append("</a>");
                sbHeaderHtml.Append("</li>");
                sbItemHtml.Append("<li  id=\"HQB_Orders_<%#Eval(\"ID\") %>\" class=\"dragOrders\" style=\"width:" + orderListWidth + ";" + this.listHeight + ";text-align:center\">");
                sbItemHtml.Append (@"<div style=""width:110px""><div style=""float:left;border-right:1px solid #CCCCCC;height:22px;"" title=""拖动排序""><img src=""../images/move.png"" style=""padding:0 8px;cursor: pointer;""/></div>
                        <div style=""float:left;padding-left:8px""><div style="" display:none;"" ><img src=""../images/loading.gif""/></div><span>");    // 列表项
                sbItemHtml.Append("<input style=\"width:50px;height:14px;text-align:center\" type=\"text\" value=\"<%#Eval(\"Orders\") %>\" onblur=\"setOrders('" + this.hsModel["TableName"].ToString() + "','<%#Eval(\"ID\") %>',this.value)\"/>");
                sbItemHtml.Append("</span></div></div></li>");
                AddShowColumn(this.hsModel["TableName"].ToString() + ".Orders");
            }

            OperationColumn(ref sbHeaderHtml, ref sbItemHtml);    // 预定义操作列
            sbItemHtml.Append("</ul>");
            listInfo[0] = sbHeaderHtml.ToString();
            listInfo[1] = sbItemHtml.ToString();
            return listInfo;
        }
        #endregion

        #region 显示列表解析  Table排版方式
        // 显示的列及顺序:复选框列(有链接按钮时显示)、 字段表中IsListEnable=1 、模型配置中自定义列表列、预定义操作列
        // 编辑页传入参数列： 添加 modelid&action=add  修改 modelid&id&action=edit
        // string[0] 列表标题  string[1]  列表项
        /// <summary>
        /// 显示列表解析
        /// </summary>
        /// <returns></returns>
        public string[] ListInfo()
        {
            StringBuilder sbHeaderHtml;       // 列表标题
            StringBuilder sbItemHtml;         // 列表项
            string[] listInfo;
            bool isCheckColumn;               // 是否加复选框列
            DataView dvModelField;            // 在页面上显示的字段
            string sysFieldIDMarker;          // 标识系统配置ID是否已调用

            sbHeaderHtml = new StringBuilder();
            sbItemHtml = new StringBuilder();
            listInfo = new string[2];
            dvModelField = this.dtField.DefaultView;
            dvModelField.RowFilter = "IsListEnable=1";
            dvModelField.Sort = "ListOrders asc,Orders asc";
            isCheckColumn = true;
            sysFieldIDMarker = ",";

            if (this.hsModel["ListButton"].ToString().Trim() == "none$")   // 无连接栏
            {
                isCheckColumn = false;
            }

            if (this.listHeight.Trim() != "")
            {
                sbItemHtml.Append(" <tr style=\"" + this.listHeight + "\" class=\"listInfotr\">");
            }
            else 
            {
                sbItemHtml.Append(" <tr class=\"listInfotr\">");
            }
            sbItemHtml.Append("<span id='Title_<%#Eval(\"ID\") %>' style='display:none'><%#Eval(\"Title\") %></span>");  //日志记录（标题）
            if (isCheckColumn)      // 解析复选框列
            {
                sbHeaderHtml.Append("<td  style=\"width:45px; text-align:center;\"><input type=\"checkbox\" name=\"SlectAll\" id=\"SlectAll\" /></td>");
                sbItemHtml.Append("<td style=\"width:45px; text-align:center;" + this.listHeight + "\"><input type=\"checkbox\" name=\"_chkID\" value=\"<%#Eval(\"ID\") %>\" /></td>");
            }

            foreach (DataRowView dr in dvModelField)            //  遍历当前模型字段
            {
                if (Utils.ParseBool(dr["IsSystemFiierd"]) && !string.IsNullOrEmpty(dr["SystemFirerdHtml"].ToString()))            // 自定义系统字段
                {
                    string sysFieldID;
                    sysFieldID = dr["SystemFirerdHtml"].ToString().Trim();           // 自定义系统字段配置节点ID

                    switch (dr["SysFieldType"].ToString())
                    {
                        case "1":          // 预定义或自定义系统字段
                            if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))          // 自定义系统字段配置未调用
                            {
                                bool result;        // 解析系统字段是否成功
                                result = ListSysField(ref sbHeaderHtml, ref sbItemHtml, sysFieldID);

                                if (!result)       // 解析系统字段失败时尝试调用基本字段解析
                                {
                                    ListBasicField(dr, ref sbHeaderHtml, ref sbItemHtml);
                                }

                                sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                            }
                            else    // 已调用
                            {
                                continue;
                            }

                            break;
                        case "2":           // 推荐区域
                            break;
                        default:
                            if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))          // 自定义系统字段配置未调用
                            {
                                bool result;        // 解析系统字段是否成功
                                result = ListSysField(ref sbHeaderHtml, ref sbItemHtml, sysFieldID);

                                if (!result)        // 解析系统字段失败时尝试调用基本字段解析
                                {
                                    ListBasicField(dr, ref sbHeaderHtml, ref sbItemHtml);
                                }

                                sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                            }
                            else    // 已调用
                            {
                                continue;
                            }

                            break;
                    }
                }
                else // 基本字段
                {
                    ListBasicField(dr, ref sbHeaderHtml, ref sbItemHtml);
                }
            }

            CustomColumn(ref sbHeaderHtml, ref sbItemHtml);       // 自定义列表列

            if (Utils.ParseBool(this.hsModel["IsOrderEdit"]))     // 排序编辑列
            {
                string orderListWidth;

                // 列表表头
                if (this.dtField.Select("name='orders'").Length > 0 && this.dtField.Select("name='orders'")[0]["ListWidth"] != null && !string.IsNullOrEmpty(this.dtField.Select("name='orders'")[0]["ListWidth"].ToString()))
                {
                    orderListWidth = this.dtField.Select("name='orders'")[0]["ListWidth"].ToString();

                    if (!orderListWidth.Contains("%"))
                    {
                        orderListWidth = orderListWidth + "px";
                    }
                }
                else
                {
                    orderListWidth = "80px";
                }

                sbHeaderHtml.Append("<td  style=\"width:" + orderListWidth + ";\" align=center>");
                sbHeaderHtml.Append("<a href=\"javascript:sort('" + this.hsModel["TableName"] + ".Orders','3')\">");
                sbHeaderHtml.Append("排序");
                sbHeaderHtml.Append("</a>");
                sbHeaderHtml.Append("</td>");
                sbItemHtml.Append("<td  id=\"HQB_Orders_<%#Eval(\"ID\") %>\" class=\"dragOrders\" style=\"width:" + orderListWidth + ";" + this.listHeight + "\" align=center>");
                sbItemHtml.Append (@"
                    <div style=""width:110px""><div style=""float:left;border-right:1px solid #CCCCCC;height:22px;"" title=""拖动排序""><img src=""../images/move.png"" style=""padding:0 8px;cursor: pointer;""/></div>
                        <div style=""float:left;padding-left:8px""><div style="" display:none;"" ><img src=""../images/loading.gif""/></div><span>");    // 列表项
                sbItemHtml.Append("<input style=\"width:50px;text-align:center;height:14px\" type=\"text\" value=\"<%#Eval(\"Orders\") %>\" onblur=\"setOrders('" + this.hsModel["TableName"].ToString() + "','<%#Eval(\"ID\") %>',this.value)\"/>");
                sbItemHtml.Append("</span></div></div></td>");
                AddShowColumn(this.hsModel["TableName"].ToString() + ".Orders");
            }

            OperationColumn(ref sbHeaderHtml, ref sbItemHtml);    // 预定义操作列
            sbItemHtml.Append("</tr>");
            listInfo[0] = sbHeaderHtml.ToString();
            listInfo[1] = sbItemHtml.ToString();
            return listInfo;
        }
        #endregion

        #region 解析基本字段
        private void ListBasicField(DataRowView dr, ref StringBuilder sbHeaderHtml, ref StringBuilder sbItemHtml)
        {
            bool isOrderLink;       // 是否在列表标题上加排序链接
            bool isLink;            // 是否在显示列表项中加入链接
            string listWidth;       // 显示列宽度
            bool isClip;            // 在列表中显示的内容是否需要截取

            if (string.Equals(dr["Name"].ToString().ToLower(), "orders")) // 屏蔽排序字段
            {
                return;
            }

            isOrderLink = KingTop.Common.Utils.ParseBool(dr["ListIsOrder"]);
            isLink = KingTop.Common.Utils.ParseBool(dr["ListIsLink"]);

            switch (dr["BasicField"].ToString().Trim())  // 只对字符串类型进行剪切
            {
                case "1":
                case "2":
                case "3":
                    isClip = BaseFieldIsClip(dr);
                    break;
                default:
                    isClip = false;
                    break;

            }

            AddShowColumn(this.hsModel["TableName"].ToString() + "." + dr["name"].ToString());  // 列表显示列字段

            if (dr["ListWidth"].ToString().Contains("%"))
            {
                listWidth = dr["ListWidth"].ToString();
            }
            else
            {
                listWidth = dr["ListWidth"].ToString() + "px";
            }


            if (isOrderLink)    // 列表标题
            {
                sbHeaderHtml.Append("<td  style=\"width:" + listWidth + ";\" align=center>");
                sbHeaderHtml.Append("<a href=\"javascript:sort('" + this.hsModel["TableName"] + "." + dr["Name"].ToString() + "','" + dr["ListOrderOption"].ToString() + "')\">");
                sbHeaderHtml.Append(dr["FieldAlias"]);
                sbHeaderHtml.Append("</a>");
                sbHeaderHtml.Append("</td>");
            }
            else
            {
                sbHeaderHtml.Append("<td  style=\"width:" + listWidth + ";text-align:" + GetListAlignment(dr["ListAlignment"]) + ";\">");
                sbHeaderHtml.Append(dr["FieldAlias"]);
                sbHeaderHtml.Append("</td>");
            }


            if (isLink)     // 列表项
            {
                string link;       // 字段链接及参数
                Regex reg;         // 配置[FiledName]或[TableName.FieldName]
                MatchCollection linkMatch;

                link = dr["ListLinkUrl"].ToString();
                link = link.Replace("{WebSite}", this.rootUrl);
                reg = new Regex(@"(?<1>\[([0-9a-zA-Z_]+|([0-9a-zA-Z_]+\.[0-9a-zA-Z_]+))\])");
                linkMatch = reg.Matches(link);

                if (linkMatch.Count > 0)
                {
                    foreach (Match m in linkMatch)
                    {
                        link = link.Replace(m.Groups[1].Value, "<%#Eval(\"" + m.Groups[1].Value.Replace(".", "_") + "\") %>");
                        if (m.Groups[1].Value.Trim() != "")
                        {
                            this.tableReferenceInfo += "|$" + m.Groups[1].Value;
                        }
                    }
                }

                if (isClip)
                {
                    sbItemHtml.Append("<td isClip=\"1\" style=\"text-align:" + GetListAlignment(dr["ListAlignment"]) + "; width:" + listWidth + ";" + this.listHeight + "\">");
                }
                else
                {
                    sbItemHtml.Append("<td style=\"text-align:" + GetListAlignment(dr["ListAlignment"]) + "; width:" + listWidth + ";" + this.listHeight + "\">");
                }

                switch (dr["BasicField"].ToString().Trim())   // 绑定内容
                {
                    case "1":    // 单行文本
                    case "2":    // 多行文本（不支持HTML）
                    case "3":    // 多行文本（支持HTML）
                        sbItemHtml.Append("<a href=\"" + link + "\" >");
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        sbItemHtml.Append("</a>");
                        break;
                    case "4":    // 单选
                    case "5":    // 多选  
                    case "6":    // 下拉列表
                    case "7":    // 列表（可多选）
                        sbItemHtml.Append("<a href=\"" + link + "\" >");
                        // 基本字段中的单选、多选、列表选项由文本框输入
                        if (string.Equals(dr["DropDownDataType"].ToString().Trim(), "1"))
                        {
                            sbItemHtml.Append("<%#ctrManageList.ParseFieldValueToText(\"" + dr["ID"].ToString() + "\",Eval(\"" + dr["Name"].ToString() + "\") )%>");
                        }
                        else
                        {
                            sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        }
                        sbItemHtml.Append("</a>");
                        break;
                    case "8":    // 数字
                        sbItemHtml.Append("<a href=\"" + link + "\" >");
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        sbItemHtml.Append("</a>");
                        break;
                    case "9":    //货币
                        sbItemHtml.Append("<a href=\"" + link + "\" >");
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        sbItemHtml.Append("</a>");
                        break;
                    case "10":   // 日期
                        string dateFormat;   // 日期格式

                        sbItemHtml.Append("<a href=\"" + link + "\" >");
                        dateFormat = GetDateFormat(dr["DateFormatOption"].ToString());
                        sbItemHtml.Append("<%#string.Format(\"{0:" + dateFormat + "}\",Eval(\"" + dr["Name"].ToString() + "\").ToString()) %>");
                        sbItemHtml.Append("</a>");
                        break;
                    case "11":   // 图片
                        sbItemHtml.Append("<a href=\"" + link + "\" >");

                        if (Utils.ParseInt(dr["ThumbDisplayType"].ToString(), -1) == 1)
                        {
                            sbItemHtml.Append("<img src='<%= GetUploadImgUrl()%>" + "<%#Eval(\"" + dr["Name"].ToString() + "\") %>.gif' onerror=\"this.src='/sysadmin/images/NoPic.jpg'\" height=60/>");
                        }
                        else
                        {
                            sbItemHtml.Append("<img src='<%=GetUploadImgUrl()%>" + "<%#Eval(\"" + dr["Name"].ToString() + "\") %>' onerror=\"this.src='/sysadmin/images/NoPic.jpg'\" height=60/>");
                        }

                        sbItemHtml.Append("</a>");
                        break;
                    case "12":   // 文件
                        sbItemHtml.Append("<a href='<%#Eval(\"" + dr["Name"].ToString() + "\") %>'><%#Eval(\"" + dr["Name"].ToString() + "\") %></a>");
                        break;
                    case "13":  // 隐藏域
                        sbItemHtml.Append("<a href=\"<%Eval(\"" + dr["Name"].ToString() + "\")%>" + dtField + "\" >");
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        sbItemHtml.Append("</a>");
                        break;
                    case "14":  // 子模型
                        if (dr["RelatedType"].ToString().Trim() == "1")
                        {
                            sbItemHtml.Append("<a href=\"<%#\"" + dr["SubModelName"].ToString().Replace("K_U_","").Replace("K_F_","").Replace("K_G_","").Replace ("K_M_","") + "list.aspx?ParentID=\" + Eval(\"ID\").ToString() + \"&OriginalUrl={page:" + hsModel["TableName"].ToString().Replace("K_U_", "").Replace("K_F_", "").Replace("K_G_", "").Replace("K_M_","") + ",nodeCode:\"%><%= Request.QueryString[\"NodeCode\"] + \"}\"%>\">");
                        }
                        else // 子模型分组
                        { 
                            sbItemHtml.Append("<a href=\"<%#Eval(\"" + dr["Name"].ToString() + "\").ToString().Replace(\"K_U_\",\"\").Replace(\"K_F_\",\"\").Replace(\"K_G_\",\"\").Replace (\"K_M_\",\"\") + \"list.aspx?ParentID=\" + Eval(\"ID\").ToString() + \"&OriginalUrl={page:" + hsModel["TableName"].ToString().Replace("K_U_", "").Replace("K_F_", "").Replace("K_G_", "").Replace ("K_M_","") + ",nodeCode:\"%><%= Request.QueryString[\"NodeCode\"] + \"}\"%>\">");
                        }

                        sbItemHtml.Append(dr["FieldAlias"].ToString());
                        sbItemHtml.Append("</a>");
                        break;
                    default:
                        sbItemHtml.Append("<a href=\"" + link + "\" >");
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        sbItemHtml.Append("</a>");
                        break;
                }

                sbItemHtml.Append("</td>");
            }
            else
            {
                if (isClip)
                {
                    sbItemHtml.Append("<td isClip=\"1\"  style=\"text-align:" + GetListAlignment(dr["ListAlignment"]) + ";width:" + listWidth + ";" + this.listHeight + "\">");
                }
                else
                {
                    sbItemHtml.Append("<td  style=\"text-align:" + GetListAlignment(dr["ListAlignment"]) + ";width:" + listWidth + ";" + this.listHeight + "\">");
                }

                switch (dr["BasicField"].ToString().Trim())  // 绑定内容
                {
                    case "1":    // 单行文本
                    case "2":    // 多行文本（不支持HTML）
                    case "3":    // 多行文本（支持HTML）
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        break;
                    case "4":    // 单选
                    case "5":    // 多选  
                    case "6":    // 下拉列表
                    case "7":    // 列表（可多选）
                        if (string.Equals(dr["DropDownDataType"].ToString().Trim(), "1"))   // 基本字段中的单选、多选、列表选项由文本框输入
                        {
                            sbItemHtml.Append("<%#ctrManageList.ParseFieldValueToText(\"" + dr["ID"].ToString() + "\",Eval(\"" + dr["Name"].ToString() + "\") )%>");
                        }
                        else
                        {
                            sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        }
                        break;
                    case "8":    // 数字
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        break;
                    case "9":    //货币
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        break;
                    case "10":   // 日期
                        string dateFormat;   // 日期格式

                        dateFormat = GetDateFormat(dr["DateFormatOption"].ToString());
                        sbItemHtml.Append("<%#string.Format(\"{0:" + dateFormat + "}\",Eval(\"" + dr["Name"].ToString() + "\").ToString()) %>");
                        break;
                    case "11":   // 图片
                        if (Utils.ParseInt(dr["ThumbDisplayType"].ToString(), -1) == 1)
                        {
                            sbItemHtml.Append("<img src='<%= GetUploadImgUrl()%>" + "<%#Eval(\"" + dr["Name"].ToString() + "\") %>.gif' onerror=\"this.src='/sysadmin/images/NoPic.jpg'\" height=60/>");
                        }
                        else
                        {
                            sbItemHtml.Append("<img src='<%= GetUploadImgUrl()%>" + "<%#Eval(\"" + dr["Name"].ToString() + "\") %>' onerror=\"this.src='/sysadmin/images/NoPic.jpg'\" height=60 />");
                        }

                        break;
                    case "12":   // 文件
                        sbItemHtml.Append("<a href='<%#Eval(\"" + dr["Name"].ToString() + "\") %>'><%#Eval(\"" + dr["Name"].ToString() + "\") %></a>");
                        break;
                    case "13":
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        break;
                    case "14":  // 子模型
                        if (dr["RelatedType"].ToString().Trim() == "1")
                        {
                            sbItemHtml.Append("<a href=\"<%#\"" + dr["SubModelName"].ToString().Replace("K_U_", "").Replace("K_F_", "").Replace("K_G_", "").Replace("K_M_", "") + "list.aspx?ParentID=\" + Eval(\"ID\").ToString() + \"&OriginalUrl={page:" + hsModel["TableName"].ToString().Replace("K_U_", "").Replace("K_F_", "").Replace("K_G_", "").Replace("K_M_", "") + ",nodeCode:\"%><%= Request.QueryString[\"NodeCode\"] + \"}\"%>\">");
                        }
                        else // 子模型分组
                        {
                            sbItemHtml.Append("<a href=\"<%#Eval(\"" + dr["Name"].ToString() + "\").ToString().Replace(\"K_U_\",\"\").Replace(\"K_F_\",\"\").Replace(\"K_G_\",\"\").Replace (\"K_M_\",\"\") + \"list.aspx?ParentID=\" + Eval(\"ID\").ToString() + \"&OriginalUrl={page:" + hsModel["TableName"].ToString().Replace("K_U_", "").Replace("K_F_", "").Replace("K_G_", "").Replace("K_M_", "") + ",nodeCode:\"%><%= Request.QueryString[\"NodeCode\"] + \"}\"%>\">");
                        }

                        sbItemHtml.Append(dr["FieldAlias"].ToString());
                        sbItemHtml.Append("</a>");
                        break;
                    default:
                        sbItemHtml.Append("<%#Eval(\"" + dr["Name"].ToString() + "\") %>");
                        break;
                }

                sbItemHtml.Append("</td>");
            }

            // 基本字段中的单选、多选、列表选项由数据库读取
            if (string.Equals(dr["DropDownDataType"].ToString().Trim(), "2"))
            {
                if (!string.IsNullOrEmpty(this.listForignCol))
                {
                    this.listForignCol = this.listForignCol + ",";
                }

                this.listForignCol = this.listForignCol + "[" + dr["DropDownTable"].ToString() + "]|" + this.hsModel["TableName"].ToString() + "." + dr["name"].ToString() + "|" + dr["DropDownTextColumn"].ToString() + "|" + dr["DropDownValueColumn"].ToString();
            }
        }
        #endregion

        #region 解析自定义系统字段
        /// <summary>
        /// sysFieldID为模型表ModelManage
        // 中的系统字段SysField的值
        /// </summary>
        /// <returns>解析是否成功</returns>
        private bool ListSysField(ref StringBuilder sbHeaderHtml, ref StringBuilder sbItemHtml, string sysFieldID)
        {
            string configPath;                  // 配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;     // 字段配置根节点指针
            XPathNodeIterator currentNav;       // 临时指针
            Regex reg;                          // 用于配置字段标签如{FieldName},{TableName.FiledName}
            MatchCollection matchCollection;    // 配置的字段标签集合
            StringBuilder sbCurrentHeaderHtml;  // 当前字段解析列表标题
            StringBuilder sbCurrentItemHtml;    // 当前字段解析列表项 
            bool isClip;                        // 在列表列中显示是否需要截取
            bool result;                        // 处理结果

            sbCurrentHeaderHtml = new StringBuilder();
            sbCurrentItemHtml = new StringBuilder();
            reg = new Regex(@"(?<1>\{\$(?<2>([0-9a-zA-Z_]+|([0-9a-zA-Z_]+\.[0-9a-zA-Z_]+)))\$\})");

            result = true;
            isClip = false;
            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config";
            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);
            navNodeXPath.MoveNext();
            currentNav = navNodeXPath.Current.Select("field[@id=" + sysFieldID + "]");

            if (currentNav != null && currentNav.Count > 0)     // 节点存在
            {
                string isSort = string.Empty;      // 列表标题是否排序
                string sortType = string.Empty;    // 列表标题排序类型  1 只要升序 2 只要降序  3 两者皆要
                string itemHeader = string.Empty;  // 列表标题
                string width = string.Empty;       // 列显示宽
                string title = string.Empty;       // 列显示标题
                string tdContent = string.Empty;   // 列绑定内容
                string enable = string.Empty;      // 是否显示
                string tdID = string.Empty;        // 列表项TD单元格ID，用于显示JS处理后的内容
                string[] fieldName = null;         // 字段名

                currentNav.MoveNext();

                try
                {
                    title = currentNav.Current.GetAttribute("title", "");
                    enable = currentNav.Current.SelectSingleNode("list").GetAttribute("enable", "");
                    width = currentNav.Current.SelectSingleNode("list").GetAttribute("width", "");
                    isSort = currentNav.Current.SelectSingleNode("list").GetAttribute("issort", "");
                    sortType = currentNav.Current.SelectSingleNode("list").GetAttribute("sorttype", "");
                    tdContent = currentNav.Current.SelectSingleNode("list").InnerXml;
                    fieldName = currentNav.Current.SelectSingleNode("name").Value.Split(new char[] { '|' });
                    tdID = fieldName[0];
                    DataRow[] currentDR = dtField.Select("Name='" + fieldName[0] + "'");


                    if (fieldName.Length < 2)       // 配置节点只有一个字段
                    {
                        if (currentDR.Length > 0 && !string.IsNullOrEmpty(currentDR[0]["FieldAlias"].ToString()))  // 列表表题引用字段表中的标题
                        {
                            title = currentDR[0]["FieldAlias"].ToString();
                        }

                        switch (currentDR[0]["BasicField"].ToString().Trim())  // 只对字符串类型进行剪切
                        {
                            case "1":
                            case "2":
                            case "3":
                                isClip = BaseFieldIsClip(currentDR[0]);
                                break;
                            default:
                                isClip = false;
                                break;
                        }
                    }
                    else  // 多个字段
                    {
                        if (string.Equals(currentNav.Current.SelectSingleNode("list").GetAttribute("isClip", ""), "1") && Utils.ParseBool(this.hsModel["IsListContentClip"].ToString()))  // 是否需调用JS截取字符串
                        {
                            isClip = true;
                        }
                        else
                        {
                            isClip = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(currentDR[0]["ListWidth"].ToString()))  // 优选采用页面配置的值
                    {
                        width = currentDR[0]["ListWidth"].ToString();
                    }
                }
                catch
                {
                    result = false;
                }

                matchCollection = reg.Matches(tdContent);

                foreach (Match match in matchCollection)    // 字段标签替换成绑定标签
                {
                    if (match.Groups[2].Value.Contains("."))
                    {
                        AddShowColumn(match.Groups[2].Value);
                        tdContent = tdContent.Replace(match.Groups[1].Value, "<%#Eval(\"" + match.Groups[2].Value.Replace(".", "_") + "\") %>");
                    }
                    else
                    {
                        AddShowColumn(this.hsModel["TableName"].ToString() + "." + match.Groups[2].Value);
                        tdContent = tdContent.Replace(match.Groups[1].Value, "<%#Eval(\"" + match.Groups[2].Value + "\") %>");
                    }
                }

                if (width.Contains("%"))    // 添加列标题
                {
                    itemHeader = "<td  id=\"Header_" + currentNav.Current.SelectSingleNode("name").Value + "\" style=\"width:" + width + ";\" >";
                }
                else
                {
                    itemHeader = "<td  id=\"Header_" + currentNav.Current.SelectSingleNode("name").Value + "\"  style=\"width:" + width + "px;\" >";
                }

                sbCurrentHeaderHtml.Append(itemHeader);

                if (string.Equals(isSort.Trim(), "1"))
                {
                    sbCurrentHeaderHtml.Append("<a href=\"javascript:sort('" + this.hsModel["TableName"] + "." + fieldName[0] + "','" + sortType + "')\">");
                    sbCurrentHeaderHtml.Append(title);
                    sbCurrentHeaderHtml.Append("</a>");
                }
                else
                {
                    sbCurrentHeaderHtml.Append(title);
                }

                sbCurrentHeaderHtml.Append("</td>");

                if (string.IsNullOrEmpty(tdID.Trim()))
                {
                    tdID = "SysField_";
                }
                else
                {
                    tdID = tdID + "_";
                }

                tdID = tdID + "<%#Eval(\"ID\")%>";
                tdContent = tdContent.Replace("{#ContainerID#}", tdID);     // {#ContainerID#}是预定义标签为引用显示内容的标签容器ID

                if (width.Contains("%"))    // 添加列项绑定
                {
                    sbCurrentItemHtml.Append("<td id=\"" + tdID + "\"   style=\"width:" + width + ";" + this.listHeight + "\" ");
                }
                else
                {
                    sbCurrentItemHtml.Append("<td id=\"" + tdID + "\"   style=\"width:" + width + "px;" + this.listHeight + "\" ");
                }

                if (currentNav.Current.SelectSingleNode("name").Value == "FlowState")       // 审核流程显示
                {
                    sbCurrentItemHtml.Append(" class=\"ListItem_FlowState\" ");
                }

                if (isClip)    // 配置节点设置了 isClip="1" 属性
                {
                    sbCurrentItemHtml.Append(" isClip=\"1\"");
                }

                if (!Utils.ParseBool(this.hsModel["IsListContentClip"].ToString()))  // 当前模型在列表项显示时不许截取
                {
                    tdContent = tdContent.Replace("isClip=\"1\"", "");
                }

                sbCurrentItemHtml.Append(">");
                sbCurrentItemHtml.Append(tdContent);
                sbCurrentItemHtml.Append("</td>");
            }
            else
            {
                result = false;
            }

            if (result)    // 解析成功
            {
                sbHeaderHtml.Append(sbCurrentHeaderHtml);
                sbItemHtml.Append(sbCurrentItemHtml);
            }

            return result;
        }
        #endregion

        #region 自定义列表列
        private void CustomColumn(ref StringBuilder sbHeaderHtml, ref StringBuilder sbItemHtml)
        {
            string[] customCol;               // 自定义列表列
            string fieldName;                 // 临时变量，保存字段全名
            string anchorHtml;                // 链接显示模板

            anchorHtml = "<a href=\"{#Url#}\">{#Text#}</a>";
            customCol = this.hsModel["CustomCol"].ToString().Split(new[] { ',' });

            foreach (string strItem in customCol)   // 解析自定义列表列
            {
                string[] col;                                              // col[0] 列表表头配置 col[1]  列表项配置

                col = strItem.Split(new char[] { '|' });

                if (col.Length > 1 && col[0].Trim() != "")
                {
                    string[] colTitle;                                     // colTitle[0] 列表表头列名  colTitle 列宽
                    string[] link;                                         // link[0]  链接文本(也可能是字段名) link[1] 链接内容 link[2] 链接条件
                    Regex reg;                                             // 配置[FiledName]或[TableName.FieldName]

                    colTitle = col[0].Split(new char[] { '$' });
                    reg = new Regex(@"(?<1>\[([0-9a-zA-Z_]+|([0-9a-zA-Z_]+\.[0-9a-zA-Z_]+))\])");
                    link = col[1].Split(new char[] { '$' });

                    if (link.Length > 1 && link[1].Trim() != "")    // 链接列类型自定义列表列
                    {
                        if (link.Length > 1)
                        {
                            MatchCollection matchText;
                            MatchCollection matchLink;

                            link[1] = link[1].Replace("{WebSite}", this.rootUrl);
                            matchText = reg.Matches(link[0]);
                            matchLink = reg.Matches(link[1]);

                            if (colTitle.Length > 1)
                            {
                                if (colTitle[1].Contains("%"))
                                {
                                    sbHeaderHtml.Append("<td style=\"width:" + colTitle[1] + ";\">");
                                }
                                else
                                {
                                    sbHeaderHtml.Append("<td style=\"width:" + colTitle[1] + "px;\">");
                                }
                            }
                            else
                            {
                                sbHeaderHtml.Append("<td>");
                            }

                            sbHeaderHtml.Append(colTitle[0]);
                            sbHeaderHtml.Append("</td>");

                            if (matchText.Count > 0)
                            {
                                foreach (Match m in matchText)
                                {
                                    fieldName = m.Groups[1].Value.Replace(".", "_").Replace("[", "").Replace("]", "");
                                    link[0] = link[0].Replace(m.Groups[1].Value, "<%#Eval(\"" + fieldName + "\") %>");

                                    if (!fieldName.Contains("."))   // 全名格式化字段
                                    {
                                        fieldName = this.hsModel["TableName"].ToString() + "." + fieldName;
                                    }

                                    AddShowColumn(fieldName);
                                }
                            }

                            if (matchLink.Count > 0)
                            {
                                foreach (Match m in matchLink)
                                {
                                    fieldName = m.Groups[1].Value.Replace(".", "_").Replace("[", "").Replace("]", "");
                                    link[1] = link[1].Replace(m.Groups[1].Value, "<%#Eval(\"" + fieldName + "\") %>");

                                    if (!fieldName.Contains("."))   // 全名格式化字段
                                    {
                                        fieldName = this.hsModel["TableName"].ToString() + "." + fieldName;
                                    }

                                    AddShowColumn(fieldName);
                                }
                            }

                            if (colTitle.Length > 1)
                            {
                                if (colTitle[1].Contains("%"))
                                {
                                    sbItemHtml.Append("<td style=\"width:" + colTitle[1] + ";" + this.listHeight + "\">");
                                }
                                else
                                {
                                    sbItemHtml.Append("<td style=\"width:" + colTitle[1] + "px;" + this.listHeight + "\">");
                                }
                            }
                            else
                            {
                                sbItemHtml.Append("<td>");
                            }

                            sbItemHtml.Append(anchorHtml.Replace("{#Url#}", link[1]).Replace("{#Text#}", link[0]));
                            sbItemHtml.Append("</td>");
                        }
                    }
                    else  // 不是链接列
                    {
                        if (colTitle.Length > 1)
                        {
                            if (colTitle[1].Contains("%"))
                            {
                                sbHeaderHtml.Append("<td style=\"width:" + colTitle[1] + ";\">");
                            }
                            else
                            {
                                sbHeaderHtml.Append("<td style=\"width:" + colTitle[1] + "px;\">");
                            }
                        }
                        else
                        {
                            sbHeaderHtml.Append("<td>");
                        }

                        sbHeaderHtml.Append(colTitle[0]);
                        sbHeaderHtml.Append("</td>");
                        Match match = reg.Match(col[1]);

                        if (match.Success)  // 链接文本引用字段名
                        {
                            if (colTitle.Length > 1)
                            {
                                if (colTitle[1].Contains("%"))
                                {
                                    sbItemHtml.Append("<td style=\"width:" + colTitle[1] + ";" + this.listHeight + "\">");
                                }
                                else
                                {
                                    sbItemHtml.Append("<td style=\"width:" + colTitle[1] + "px;" + this.listHeight + "\">");
                                }
                            }
                            else
                            {
                                sbItemHtml.Append("<td>");
                            }

                            sbItemHtml.Append("<%#Eval(\"" + match.Groups[1].Value.Replace(".", "_") + "\") %>");
                            sbItemHtml.Append("</td>");
                        }
                        else
                        {
                            if (colTitle.Length > 1)
                            {
                                if (colTitle[1].Contains("%"))
                                {
                                    sbItemHtml.Append("<td style=\"width:" + colTitle[1] + ";" + this.listHeight + "\">");
                                }
                                else
                                {
                                    sbItemHtml.Append("<td style=\"width:" + colTitle[1] + "px;" + this.listHeight + "\">");
                                }
                            }
                            else
                            {
                                sbItemHtml.Append("<td>");
                            }

                            sbItemHtml.Append(col[1] + "</td>");
                        }
                    }
                }
            }
        }
        #endregion

        #region Repeater中的预定义操作列
        #region 预定义操作列
        private void OperationColumn(ref StringBuilder sbHeaderHtml, ref StringBuilder sbItemHtml)
        {
            StringBuilder sbOperationColumn;    // 解析后的操作作列的HTML代码
            string columnWidth;                 // 列宽

            sbOperationColumn = new StringBuilder();
            columnWidth = string.Empty;
            GetOperationColumn(ref sbOperationColumn, ref columnWidth);

            if (!string.IsNullOrEmpty(this.hsModel["OperationColumnWidth"].ToString()))  // 模型配置的列表列操作宽度
            {
                columnWidth = this.hsModel["OperationColumnWidth"].ToString();
            }

            if (columnWidth.Contains("%"))
            {
                sbHeaderHtml.Append("<td style=\"width:" + columnWidth + ";\" >");
            }
            else
            {
                sbHeaderHtml.Append("<td style=\"width:" + columnWidth + "px;\" >");
            }

            sbHeaderHtml.Append("操作");
            sbHeaderHtml.Append("</td>");

            if (columnWidth.Contains("%"))
            {
                sbItemHtml.Append("<td style=\"width:" + columnWidth + ";" + this.listHeight + ";\" >");
            }
            else
            {
                sbItemHtml.Append("<td style=\"width:" + columnWidth + "px;" + this.listHeight + ";\" >");
            }

            sbItemHtml.Append(sbOperationColumn.ToString());
            sbItemHtml.Append("</td>");
        }
        #endregion

        #region 从配置文件中获取操作列配置
        private void GetOperationColumn(ref StringBuilder sbOperationColumn, ref string columnWidth)
        {
            string operationColumn;             // 当前模型中的操作
            string[] arrOperationColumn;
            string configPath;                  // 配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;     // 字段配置根节点指针
            XPathNodeIterator currentNav;       // 临时指针
            string link;                        // 临时变量
            Regex reg;                          // 用于配置字段标签如{FieldName},{TableName.FiledName}
            MatchCollection matchCollection;    // 配置的字段标签集合

            reg = new Regex(@"(?<1>\{\$(?<2>([0-9a-zA-Z_]+|([0-9a-zA-Z_]+\.[0-9a-zA-Z_]+)))\$\})");
            operationColumn = this.hsModel["OperationColumn"].ToString();
            arrOperationColumn = operationColumn.Split(new char[] { ',' });
            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "ListPath");
            xpath = "/config/rptbutton";
            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);
            navNodeXPath.MoveNext();
            columnWidth = navNodeXPath.Current.GetAttribute("width", "");

            foreach (string nodeID in arrOperationColumn)   // 遍历所有当前模型中的配置节点
            {
                if (!string.IsNullOrEmpty(nodeID))
                {
                    currentNav = navNodeXPath.Current.Select("link[@id=" + nodeID + "]");

                    if (currentNav != null && currentNav.Count > 0)
                    {
                        currentNav.MoveNext();
                        link = currentNav.Current.InnerXml;
                        matchCollection = reg.Matches(link);

                        foreach (Match match in matchCollection)    // 字段标签替换成绑定标签
                        {
                            if (match.Groups[2].Value.Contains("."))
                            {
                                AddShowColumn(match.Groups[2].Value);
                                link = link.Replace(match.Groups[1].Value, "<%#Eval(\"" + match.Groups[2].Value.Replace(".", "_") + "\") %>");
                            }
                            else
                            {
                                AddShowColumn(this.hsModel["TableName"].ToString() + "." + match.Groups[2].Value);
                                link = link.Replace(match.Groups[1].Value, "<%#Eval(\"" + match.Groups[2].Value + "\") %>");
                            }
                        }

                        link = link.Replace("{#EditUrlParam#}", this.keepUrlParam);
                        sbOperationColumn.Append(link);
                    }
                }
            }
        }
        #endregion
        #endregion

        #region 获取系统预定义（自定义）配置文件中的引用文件
        private string GetInclude(bool isEditPage)
        {
            StringBuilder sbInclude;            // 保存JS引用文件名
            string configPath;                  // 配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;     // JS引用文件配置根节点指针
            string sysField;                    // 记录模型系统字段

            sbInclude = new StringBuilder();
            sysField = this.hsModel["SysField"].ToString() + ",";
            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config/include/file";
            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);

            if (navNodeXPath != null && navNodeXPath.Count > 0)     // 存在include配置节点
            {
                while (navNodeXPath.MoveNext())
                {
                    string nodeID;
                    string file;
                    string includePage;

                    try
                    {
                        nodeID = navNodeXPath.Current.GetAttribute("nodeid", "");
                        includePage = navNodeXPath.Current.GetAttribute("page", "").ToLower();
                        file = navNodeXPath.Current.InnerXml;
                    }
                    catch
                    {
                        continue;
                    }

                    if (sysField.Contains(nodeID + ","))    // 引用当前配置的文件
                    {
                        if (isEditPage)   // 编辑页
                        {
                            if (string.Equals(includePage, "edit") || string.Equals(includePage, "both"))
                            {
                                if (!sbInclude.ToString().Contains(file))
                                {
                                    sbInclude.Append(file);
                                }
                            }
                        }
                        else   // 列表页
                        {
                            if (string.Equals(includePage, "list") || string.Equals(includePage, "both"))
                            {
                                if (!sbInclude.ToString().Contains(file))
                                {
                                    sbInclude.Append(file);
                                }
                            }
                        }
                    }
                }
            }

            return sbInclude.ToString();
        }
        #endregion

        #region  判断基本字段是否在列表列中显示时是否需要剪切
        private bool BaseFieldIsClip(DataRowView dr)
        {
            bool isClip;       // 当前字段是否需要剪切
            double maxLength;  // 当前字段预测的最大长度(px)
            double width;      // 设置的宽度(px)

            if (!string.IsNullOrEmpty(dr["DataColumnLength"].ToString()) && Utils.ParseBool(this.hsModel["IsListContentClip"].ToString()))
            {
                maxLength = Utils.ParseInt(dr["DataColumnLength"].ToString(), 0) * 12;

                if (dr["ListWidth"].ToString().Contains("%"))
                {
                    width = Utils.ParseInt(dr["ListWidth"].ToString().Replace("%", ""), 0) * 10.24;
                }
                else
                {
                    width = Utils.ParseInt(dr["ListWidth"].ToString(), 0);
                }

                if (width >= maxLength)
                {
                    isClip = false;
                }
                else
                {
                    isClip = true;
                }
            }
            else
            {
                isClip = false;
            }

            return isClip;
        }

        private bool BaseFieldIsClip(DataRow dr)
        {
            bool isClip;       // 当前字段是否需要剪切
            double maxLength;  // 当前字段预测的最大长度(px)
            double width;      // 设置的宽度(px)

            if (!string.IsNullOrEmpty(dr["DataColumnLength"].ToString()) && Utils.ParseBool(this.hsModel["IsListContentClip"].ToString()))
            {
                maxLength = Utils.ParseInt(dr["DataColumnLength"].ToString(), 0) * 12;

                if (dr["ListWidth"].ToString().Contains("%"))
                {
                    width = Utils.ParseInt(dr["ListWidth"].ToString().Replace("%", ""), 0) * 10.24;
                }
                else
                {
                    width = Utils.ParseInt(dr["ListWidth"].ToString(), 0);
                }

                if (width >= maxLength)
                {
                    isClip = false;
                }
                else
                {
                    isClip = true;
                }
            }
            else
            {
                isClip = false;
            }

            return isClip;
        }
        #endregion

        #region 获取当前站点的文件夹名称
        /// <summary>
        /// 当前站点ID
        /// </summary>
        private int SiteID
        {
            get
            {
                if (HttpContext.Current.Session["SiteID"] != null)
                {
                    return Utils.ParseInt(HttpContext.Current.Session["SiteID"].ToString(), 0);
                }
                else
                {
                    HttpContext.Current.Session.Abandon();
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["SiteID"] = value;
            }
        }

        private string _siteDir = null;

        private string SiteDir
        {
            get
            {
                if (string.IsNullOrEmpty(_siteDir))
                {
                    this._siteDir = GetSiteDir();
                }
                return this._siteDir;
            }

        }
        /// <summary>
        /// 获取图片根目录
        /// </summary>
        /// <returns></returns>
        private string GetSiteDir()
        {
            string strSiteDir = "";
            KingTop.BLL.SysManage.SysWebSite bllWebSite = new KingTop.BLL.SysManage.SysWebSite();
            DataTable dt = bllWebSite.GetList("ONE", Utils.getOneParams(SiteID.ToString()));
            strSiteDir = dt.Rows[0]["Directory"].ToString();
            return strSiteDir;
        }
        #endregion
    }
}
