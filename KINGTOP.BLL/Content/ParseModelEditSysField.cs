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
// 作者：吴岸标// 创建日期：2010-04-14
// 功能描述：解析模型字段 -- 模型编辑页 -- 系统字段解析
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region 编辑页解析 -- 系统自定义字段        private bool EditSysField(ref StringBuilder sbSysFields, string sysFieldID)
        {
            string controlHtml;                  // 当前节点的HTML代码
            string itemHtml;                     // 临时变量
            string configPath;                   // 链接配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;      // 链接/按钮配置根节点指针            XPathNodeIterator currentNav;        // 临时指针
            Regex reg;                           // 用于配置字段标签如{FieldName}
            MatchCollection matchCollection;     // 配置的字段标签集合            bool isSuccess;                      // 解析是否成功

            isSuccess = true;
            reg = new Regex(@"(?<1>\{\$(?<2>([0-9a-zA-Z_]+))\$\})");
            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config";
            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);
            navNodeXPath.MoveNext();
            currentNav = navNodeXPath.Current.Select("field[@id=" + sysFieldID + "]/edit");

            if (currentNav != null && currentNav.Count > 0)
            {
                currentNav.MoveNext();
                controlHtml = currentNav.Current.InnerXml;
                controlHtml = controlHtml.Replace("{#TableName#}", this.hsModel["TableName"].ToString());   // 解析预定义标签                matchCollection = reg.Matches(controlHtml);

                foreach (Match match in matchCollection)                                                    // 字段标签替换成绑定标签                {
                    controlHtml = controlHtml.Replace(match.Groups[1].Value, "<%=hsFieldValue[\"" + match.Groups[2].Value + "\"] %>");
                }

                controlHtml = containerItemTemplate.Replace(containerContent, controlHtml);                      // 加标签如 <li>
                currentNav = navNodeXPath.Current.Select("field[@id=" + sysFieldID + "]");

                if (currentNav != null && currentNav.Count > 0)                                                  // 配置节点在编辑页显示
                {
                    itemHtml = SetFieldParam(currentNav);                                                        // 设置缺省值并获取显示标题
                    itemHtml = containerItemHeaderTemplate.Replace(containerContent, itemHtml);                   // 标题
                    controlHtml = containerTemplate.Replace(containerContent, itemHtml + controlHtml);
                    sbSysFields.Append(controlHtml);
                }
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;
       }
        #endregion

        #region 设置系统自定义字段参数，用于编辑页中操作
        // 返回值用于显示标题        private string SetFieldParam(XPathNodeIterator fieldNav)
        {
            string[] arrFieldName;              // 配置节点配置的系统字段集合            string[] arrFieldDefaultValue;      // 字段对应的缺省值            string[] arrFieldType;              // 字段类型
            string   fieldName;                 // 从配置文件读取的字段名            string   fieldDefaultValue;         // 从配置文件牟字段缺省值            string fieldType;                   // 从配置文件牟字段类型
            string title;                       // 显示标题
            title = null;

            if (fieldNav != null)
            {
                fieldNav.MoveNext();

                if (fieldNav.Count > 0)
                {
                    fieldNav.MoveNext();
                    title = fieldNav.Current.GetAttribute("title", "");

                    if (fieldNav.Current.SelectSingleNode("sql") != null)
                    {
                        try
                        {
                            fieldName = fieldNav.Current.SelectSingleNode("name").Value;
                            fieldDefaultValue = fieldNav.Current.SelectSingleNode("sql").GetAttribute("default", "");
                            fieldType = fieldNav.Current.SelectSingleNode("sql").GetAttribute("type", "");
                        }
                        catch
                        {
                            fieldName = null;
                            fieldDefaultValue = null;
                            fieldType = null;
                        }

                        if (fieldName != null && fieldDefaultValue != null)
                        {
                            arrFieldName = fieldName.Split(new char[] { '|' });
                            arrFieldDefaultValue = fieldDefaultValue.Split(new char[] { '|' });
                            arrFieldType = fieldType.Split(new char[] { '|' });

                            if (arrFieldName.Length < 2)  // 配置节点只有一个字段,时列表表题引用字段表中的标题
                            {
                                DataRow[] currentDR = dtField.Select("Name='" + arrFieldName[0] + "'");
                                if (currentDR.Length > 0 && !string.IsNullOrEmpty(currentDR[0]["FieldAlias"].ToString()))
                                {
                                    title = currentDR[0]["FieldAlias"].ToString();
                                }
                            }

                            for (int i = 0; i < arrFieldName.Length; i++)
                            {
                                if (string.IsNullOrEmpty(arrFieldName[i]))
                                {
                                    continue;
                                }

                                this.sbFieldParam.Append(arrFieldName[i] + "|");                                                                         // 字段名称
                                this.sbFieldParam.Append(FormatSysFieldDefaultValue(arrFieldDefaultValue[i], arrFieldName[i], arrFieldType[i]) + "|");   // 字段缺省值                                this.sbFieldParam.Append("0,");                                                                                          // 字段类型
                            }
                        }
                    }
                }
            }

            return title;
        }

        #endregion

        #region 按自定义系统字段类型格式化缺省值        private string FormatSysFieldDefaultValue(string originalValue,string fieldName,string fieldType)
        {
            string defaultValue;

            defaultValue = originalValue;

            switch(fieldType.ToLower())
            {
                case "date":
                case "time":
                case "datetime":
                    if(string.Equals(originalValue.Trim(),"getdate()"))
                    {
                        defaultValue = "";
                    }
                    break;
                default:
                    defaultValue = originalValue;
                    break;
            }
  
            return defaultValue;
        }
        #endregion

        #region 推荐区域解析
        private void RecommendArea(ref StringBuilder sbSysFields, DataRowView dr)
        {
            DataTable dtAreaPosition;       // 当前推荐区域的所有位置记录            string chkBoxTemplate;          // 每个位置的复选框代码内容
            StringBuilder chkBoxItemsHtml;
            string  itemHtml;
            string fieldValue;              // 字段的值            string fieldName;

            chkBoxItemsHtml = new StringBuilder();

            fieldName = dr["Name"].ToString();
            fieldValue = "<%=hsFieldValue[\"" + dr["name"] + "\"]%>";           // hsFieldValue为HashTable类型
            chkBoxTemplate = "<input type=\"checkbox\" value=\"{PositionTag}\" name=\"" + fieldName + "\" /><span style=\"color:{Color};{FontStyle}\">{PositionName}</span>&nbsp";
            dtAreaPosition = dal.GetRecommendAreaPosition(dr["SystemFirerdHtml"].ToString().Trim());
            chkBoxItemsHtml.Append("<span id=\"HQB_"+ fieldName +"\">");

            foreach(DataRow position in dtAreaPosition.Rows)    // 遍历所有推荐位置            {
                if (!string.IsNullOrEmpty(position["FontColor"].ToString().Trim()))
                {
                    chkBoxItemsHtml.Append(chkBoxTemplate.Replace("{PositionTag}", position["Tags"].ToString()).Replace("{PositionName}", position["Name"].ToString()).Replace("{FontStyle}", position["FontSylte"].ToString()).Replace("{Color}", position["FontColor"].ToString()));
                }
                else
                {
                    chkBoxItemsHtml.Append(chkBoxTemplate.Replace("{PositionTag}", position["Tags"].ToString()).Replace("{PositionName}", position["Name"].ToString()).Replace("{FontStyle}", position["FontSylte"].ToString()).Replace("{Color}","#000000"));
                }
            }

            chkBoxItemsHtml.Append("</span>");
            chkBoxItemsHtml.Append("<script type=\"text/javascript\"> var varHQB_"+ fieldName +"=\""+ fieldValue +",\"; var arrVarHQB_"+ fieldName +"=varHQB_"+ fieldName +".split(\",\"); $(\"#HQB_"+ fieldName +"\").find(\"input[type='checkbox']\").each(function (){for(var i=0;i<arrVarHQB_"+ fieldName +".length;i++){if($(this).val() == arrVarHQB_"+ fieldName +"[i]){$(this).attr(\"checked\",\"checked\");break;}}});</script>");
            itemHtml = containerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString());          // 添加标题
            sbSysFields.Append(containerTemplate.Replace(containerContent, itemHtml + containerItemTemplate.Replace(containerContent, chkBoxItemsHtml.ToString()))); // 添加容器标签如<ul>
            SetFieldParam(fieldName, "", "0");  // 记录字段，用于编辑页中        }
        #endregion
    }
}