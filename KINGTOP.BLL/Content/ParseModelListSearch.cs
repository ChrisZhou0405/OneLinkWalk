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
// 作者：吴岸标// 创建日期：2010-03-18
// 功能描述：模型列表解析类中的共用方法
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ParseModel
    {
        #region 搜索解析
        public string Search()
        {
            StringBuilder sbSearchHtml;           // 保存解析后的搜索代码
            DataView dvSearch;                    // 在搜索栏中显示的字段集            string sysFieldIDMarker;              // 标识系统配置ID是否已调用
            sbSearchHtml = new StringBuilder();
            dvSearch = this.dtField.DefaultView;
            dvSearch.RowFilter = "IsSearch=1";
            dvSearch.Sort = "SearchOrders asc,Orders asc";
            sysFieldIDMarker = ",";

            foreach (DataRowView dr in dvSearch)   // 遍历所有需搜索的字段            {
                if (Utils.ParseBool(dr["IsSystemFiierd"]) && !string.IsNullOrEmpty(dr["SystemFirerdHtml"].ToString()))            // 自定义系统字段                {
                    string sysFieldID;

                    sysFieldID = dr["SystemFirerdHtml"].ToString().Trim();           // 自定义系统字段配置节点ID

                    switch (dr["SysFieldType"].ToString())
                    {
                        case "1":   // 预定义或自定义                            if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))          // 自定义系统字段配置未调用
                            {
                                bool result;

                                result = SysFieldSearch(dr, ref sbSearchHtml);

                                if (!result)    // 解析失败尝试调用基本字段进行解析
                                {
                                    FieldSearch(dr, ref sbSearchHtml);
                                }

                                sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                            }
                            else         // 已调用                            {
                                continue;
                            }

                            break;
                        case "2":   // 推荐区域
                            break;
                        default:
                            if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))          // 自定义系统字段配置未调用
                            {
                                bool result;

                                result = SysFieldSearch(dr, ref sbSearchHtml);

                                if (!result)    // 解析失败尝试调用基本字段进行解析
                                {
                                    FieldSearch(dr, ref sbSearchHtml);
                                }

                                sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                            }
                            else         // 已调用                            {
                                continue;
                            }

                            break;
                    }
                }
                else     // 基本字段
                {
                    FieldSearch(dr, ref sbSearchHtml); 
                }
            }

            return sbSearchHtml.ToString();
        }
        #endregion
        
        #region 搜索解析 -- 基本字段解析
        private void FieldSearch(DataRowView dr,ref StringBuilder sbSearchHtml)
        {
            StringBuilder sbFieldSearchHtml;     // 保存解析后的自定义字段解析搜索代码            string fieldType;                    // 字段类型

            sbFieldSearchHtml = new StringBuilder();
            fieldType = dr["BasicField"].ToString().Trim();

            switch (fieldType)  // 基本字段类型
            {
                case "4":   // 单选                    GetInputHtml(dr, ref sbFieldSearchHtml, null, controlRadio, fieldType);
                    break;
                case "5":   // 多选                    GetInputHtml(dr, ref sbFieldSearchHtml, null, controlCheckBox, fieldType);
                    break;
                case "6":   // 下拉列表
                    GetInputHtml(dr, ref sbFieldSearchHtml, controlSelect, controlSelectItem, fieldType);
                    break;
                case "7":   // 列表（可选择多列）                    GetInputHtml(dr, ref sbFieldSearchHtml, controlMutiSelect, controlSelectItem, fieldType);
                    break;
                case "10":  // 日期
                    string controlTemplate;

                    controlTemplate = controlDate.Replace(controlDateFormat, GetDateFormat(dr["DateFormatOption"].ToString()));
                    GetInputTextHtml(dr, controlTemplate, ref sbFieldSearchHtml);
                    break;
                default:
                    GetInputTextHtml(dr, controlTextBox,ref sbFieldSearchHtml);
                    break;
            }

            sbSearchHtml.Append(sbFieldSearchHtml);
        }
        #endregion

        #region 自定义字段解析 -- 单选、多选、列表        /// <summary>
        /// 对于手工输入的：一律以解析显示，进行选择
        /// 字段值是由其它数据库读取：        ///     1、数据量比较少的那种,如单选、复选按钮以按钮列表或复选框的形式弄出        ///     2  对于数据量比较大的，如下拉列表和多行下拉，则用以文本框输入的形式进行搜索,并获取数据的参数
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="sbFieldSearchHtml"></param>
        /// <param name="template">控件代码模板</param>
        /// <param name="itemTemplate">选项代码模板</param>
        /// <param name="controlType">控件类型</param>
        private void GetInputHtml(DataRowView dr, ref StringBuilder sbFieldSearchHtml, string template, string itemTemplate, string controlType)
        {
            string itemHtml;        // 临时变量
            string controlHtml;     // 保存当前字段搜索显示的所有HTML代码

            template = string.IsNullOrEmpty(template)==true?template: template.Replace(controlID, dr["Name"].ToString());
            
            if (string.Equals(dr["DropDownDataType"], "1")) // 手工输入
            {
                string[] arrItemList;   // 值对字符串数组                string[] arrItem;       // 保存按钮的值与文本

                controlHtml = null;
                arrItemList = dr["OptionsValue"].ToString().Split(new char[] { ',' });

                foreach (string strItem in arrItemList)
                {
                    arrItem = strItem.Split(new char[] { '|' });

                    if (arrItem.Length > 0)
                    {
                        itemHtml = itemTemplate;
                        itemHtml = itemHtml.Replace(controlText, arrItem[0]);
                        itemHtml = itemHtml.Replace(controlValue, arrItem[1]);
                        itemHtml = itemHtml.Replace(controlName, dr["name"].ToString());
                        itemHtml = itemHtml.Replace(controlSelected, "");
                        controlHtml = controlHtml + itemHtml;
                    }
                }

                itemHtml = controlHidden.Replace(controlName, dr["name"].ToString() + "{Type}");    // 标识控件类型
                itemHtml = itemHtml.Replace(controlValue, controlType);
                controlHtml = controlHtml + itemHtml;

                if (!string.IsNullOrEmpty(controlHtml))  // 搜索项不为空
                {
                    if (!string.IsNullOrEmpty(template)) // 有父标签模板如<select>则加容器标签
                    {
                        controlHtml = template.Replace(containerContent, controlHtml);
                        controlHtml = controlHtml.Replace(controlName, dr["Name"].ToString());
                    }

                    controlHtml = searchContainerItemTemplate.Replace(containerContent, controlHtml);                       // 添加父容器如<li>
                    itemHtml = searchContainerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString());    // 添加搜索项标题
                    controlHtml = itemHtml + controlHtml;
                }

                controlHtml = searchContainerTemplate.Replace(containerContent, controlHtml);
                sbFieldSearchHtml.Append(controlHtml);
            }
            else // 来源于数据库
            {
                if (string.IsNullOrEmpty(template))
                {
                    string hsObj;                                  //  控件数据集      
                    StringBuilder sbHdnValue;                      // 引用外表数据时的参数列
                    sbHdnValue = new StringBuilder();

                    sbHdnValue.Append(dr["BasicField"]);
                    sbHdnValue.Append(",");                        // 控件类型，如文本框                    sbHdnValue.Append(dr["DropDownTable"]);        // 引用表名
                    sbHdnValue.Append(",");                        // 参数分隔符                    sbHdnValue.Append(dr["name"]);                 // 当前表搜索字段                    sbHdnValue.Append(",");
                    sbHdnValue.Append(dr["DropDownTextColumn"]);   // Text引用列名
                    sbHdnValue.Append(",");
                    sbHdnValue.Append(dr["DropDownValueColumn"]);  // Value引用列名

                    if (!string.IsNullOrEmpty(dr["DropDownSqlWhere"].ToString().Trim()))
                    {
                        sbHdnValue.Append(",");
                        sbHdnValue.Append(dr["DropDownSqlWhere"]);
                    }

                    hsObj = "ctrManageList.InitSearchField(\""
                                + dr["DropDownTable"].ToString() + "\", \""                // 引用表名
                                + dr["DropDownTextColumn"].ToString() + "\", \""           // 控件text绑定字段名
                                + dr["DropDownValueColumn"].ToString() + "\", \""          // 控件value绑定字段名
                                + dr["DropDownSqlWhere"].ToString() + "\")";               // 选择条件  
                    // 搜索单项如select 中的 option
                    itemHtml = itemTemplate.Replace(controlName, dr["name"].ToString());   // 加载搜索项显示模板                    itemHtml = itemHtml.Replace(controlValue , "<%=key.ToString()%>");     // 加载值 
                    itemHtml = itemHtml.Replace(controlText, "<%=" + hsObj + "[key].ToString()%>");   // 加载显示文本   
                    itemHtml = itemHtml.Replace(controlSelected, "");                      // 默认不选中
                    controlHtml = "<%foreach (object key in " + hsObj + ".Keys){%>";
                    controlHtml = controlHtml + itemHtml + " ";                            // 输出至界面                    controlHtml = controlHtml + "<%}%>";
                    // 将引用外表数据的参数保存至HiddenField控件中                    itemHtml = controlHidden.Replace(controlName, "{hdn}" + dr["name"].ToString());
                    itemHtml = itemHtml.Replace(controlValue, sbHdnValue.ToString());
                    controlHtml = controlHtml + itemHtml;
                    controlHtml = searchContainerItemTemplate.Replace(containerContent, controlHtml);                    // 添加容器标签如<li>
                    itemHtml = searchContainerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString()); // 添加搜索项标题
                    controlHtml = itemHtml + controlHtml;
                    controlHtml = searchContainerTemplate.Replace(containerContent, controlHtml);                         //添加容器标签如<ul>

                    sbFieldSearchHtml.Append(controlHtml);
                }
                else
                {
                    GetSelectFromDBHtml(dr, ref sbFieldSearchHtml);     // 以文本框输入的形式进行搜索
                }
            }
        }
        #endregion

        #region 解析列表以文本框输入的形式进行搜索        private void GetSelectFromDBHtml(DataRowView dr, ref StringBuilder sbFieldSearchHtml)
        {
            string itemHtml;        // 临时变量
            string controlHtml;     // 保存当前字段搜索显示的所有HTML代码
            StringBuilder sbHdnValue;// 引用外表数据时的参数列
            sbHdnValue = new StringBuilder();

            sbHdnValue.Append(dr["BasicField"]);
            sbHdnValue.Append(",");                        // 控件类型，如文本框            sbHdnValue.Append(dr["DropDownTable"]);        // 引用表名
            sbHdnValue.Append(",");                        // 参数分隔符            sbHdnValue.Append(dr["name"]);                 // 当前表搜索字段            sbHdnValue.Append(",");
            sbHdnValue.Append(dr["DropDownTextColumn"]);   // Text引用列名
            sbHdnValue.Append(",");
            sbHdnValue.Append(dr["DropDownValueColumn"]);  // Value引用列名

            if (!string.IsNullOrEmpty(dr["DropDownSqlWhere"].ToString().Trim()))
            {
                sbHdnValue.Append(",");
                sbHdnValue.Append(dr["DropDownSqlWhere"]);
            }

            itemHtml = controlHidden.Replace(controlName, "{hdn}" + dr["name"].ToString());                     // 将引用外表数据的参数保存至HiddenField控件中
            itemHtml = itemHtml.Replace(controlValue, sbHdnValue.ToString());
            controlHtml = itemHtml;
            itemHtml = controlTextBox.Replace(controlName, dr["name"].ToString());                               // 搜索文本框
            itemHtml = itemHtml.Replace(controlID, dr["name"].ToString());
            itemHtml = itemHtml.Replace(controlWidth, dr["SearchWidth"].ToString());
            itemHtml = itemHtml.Replace(controlMaxLength, dr["TextBoxWidth"].ToString());
            itemHtml = itemHtml.Replace(controlValue, "");
            controlHtml = itemHtml + controlHtml;
            controlHtml = searchContainerItemTemplate.Replace(containerContent, controlHtml);// 添加容器标签如<li>
            itemHtml = searchContainerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString()); // 添加搜索项标题
            controlHtml = itemHtml + controlHtml;
            controlHtml = searchContainerTemplate.Replace(containerContent, controlHtml);                        //添加容器标签如<ul>
            sbFieldSearchHtml.Append(controlHtml);
        }
        #endregion

        #region 自定义字段解析 -- 数字、货币、日期时间、图片、文件、文本都是以文本框的形式显示
        private void GetInputTextHtml(DataRowView dr,string controlTemplate, ref StringBuilder sbFieldSearchHtml)
        {
            int displayType;         // 界面显示类型
            string firstHtml;        // 第一个控件代码            string secondHtml;       // 第二个控件代码            string tempHtml;         // 临时变量
            string controlHtml;      // 保存当前字段搜索显示的所有HTML代码

            displayType =  Common.Utils.ParseInt(dr["SearchUIType"],-100);

            switch(displayType)
            {
                case 1: // 分范围方式                    controlHtml = searchContainerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString());     // 添加搜索项标题
                    firstHtml = controlTemplate.Replace(controlValidate, "");                                                   // 第一个文本框
                    firstHtml = firstHtml.Replace(controlName, dr["name"].ToString());
                    firstHtml = firstHtml.Replace(controlID, dr["name"].ToString());
                    firstHtml = firstHtml.Replace(controlWidth, dr["SearchWidth"].ToString());
                    firstHtml = firstHtml.Replace(controlMaxLength, dr["TextBoxWidth"].ToString());
                    firstHtml = firstHtml.Replace(controlValue, "");
                    firstHtml = searchContainerItemTemplate.Replace(containerContent, firstHtml);
                    firstHtml = searchContainerItemTemplate.Replace(containerContent, firstHtml);
                    secondHtml = controlTemplate.Replace(controlValidate,"");                                                   // 第二个文本框
                    secondHtml = secondHtml.Replace(controlName, dr["name"].ToString() + "{0}");
                    secondHtml = secondHtml.Replace(controlID, dr["name"].ToString() + "{0}");
                    secondHtml = secondHtml.Replace(controlWidth, dr["SearchWidth"].ToString());
                    secondHtml = secondHtml.Replace(controlMaxLength, dr["TextBoxWidth"].ToString());
                    secondHtml = secondHtml.Replace(controlValue, "");
                    secondHtml = secondHtml.Replace(containerContent, secondHtml);
                    secondHtml = searchContainerItemTemplate.Replace(containerContent, secondHtml);
                    controlHtml = controlHtml + searchContainerItemTemplate.Replace(containerContent,"从：");                   // 组装当前字段所有HTML代码
                    controlHtml = controlHtml + firstHtml;
                    controlHtml = controlHtml + searchContainerItemTemplate.Replace(containerContent, "到：");
                    controlHtml = controlHtml + secondHtml;
                    controlHtml = searchContainerTemplate.Replace(containerContent, controlHtml);
                    sbFieldSearchHtml.Append(controlHtml);
                    break;
                case 2: // 列表加文本框
                    firstHtml = controlSelectItem.Replace(controlText,"大于");              // 大于选择项                    firstHtml = firstHtml.Replace(controlValue,">");
                    tempHtml = firstHtml;
                    firstHtml = controlSelectItem.Replace(controlText, "大于等于");         // 大于等于选择项                    firstHtml = firstHtml.Replace(controlValue, ">=");
                    tempHtml = tempHtml + firstHtml;
                    firstHtml = controlSelectItem.Replace(controlText, "小于");             // 小于选择项                    firstHtml = firstHtml.Replace(controlValue, "<");
                    tempHtml = tempHtml + firstHtml;
                    firstHtml = controlSelectItem.Replace(controlText, "小于等于");         // 小于等于选择项                    firstHtml = firstHtml.Replace(controlValue, "<=");
                    tempHtml = tempHtml + firstHtml;
                    firstHtml = controlSelectItem.Replace(controlText, "等于");             // 等于选择项                    firstHtml = firstHtml.Replace(controlValue, "=");
                    tempHtml = tempHtml + firstHtml;
                    firstHtml = controlSelectItem.Replace(controlText, "不等于");           // 不等于选择项                    firstHtml = firstHtml.Replace(controlValue, "<>");
                    firstHtml = tempHtml + firstHtml;                                       // 下拉列表控件 <input Select>
                    firstHtml = controlSelect.Replace(containerContent, firstHtml);
                    firstHtml = firstHtml.Replace(controlName, dr["name"].ToString() + "{Compare}");
                    firstHtml = searchContainerItemTemplate.Replace(containerContent, firstHtml);  // 加父容器如<Li>
                    secondHtml = controlTemplate.Replace(controlValidate,"");                                               // 文本框控件<input Text>
                    secondHtml = secondHtml.Replace(controlName, dr["name"].ToString());
                    secondHtml = secondHtml.Replace(controlID, dr["name"].ToString());
                    secondHtml = secondHtml.Replace(controlWidth, dr["SearchWidth"].ToString());
                    secondHtml = secondHtml.Replace(controlMaxLength, dr["TextBoxWidth"].ToString());
                    secondHtml = secondHtml.Replace(controlValue, "");
                    secondHtml = searchContainerItemTemplate.Replace(containerContent, secondHtml);                         // 加父容器如<li>
                    controlHtml = searchContainerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString()); // 添加搜索项标题                    controlHtml = controlHtml + firstHtml + secondHtml;
                    controlHtml = searchContainerTemplate.Replace(containerContent, controlHtml);                           // 添加容器如<ul>
                    sbFieldSearchHtml.Append(controlHtml);
                    break;
                default: // 单文本框
                    tempHtml = controlTemplate.Replace(controlName, dr["Name"].ToString()).Replace(controlValidate,"");
                    tempHtml = tempHtml.Replace(controlID, dr["Name"].ToString());
                    tempHtml = tempHtml.Replace(controlWidth, dr["SearchWidth"].ToString());
                    tempHtml = tempHtml.Replace(controlMaxLength, dr["TextBoxWidth"].ToString());
                    tempHtml = tempHtml.Replace(controlValue, "");
                    tempHtml = searchContainerItemTemplate.Replace(containerContent, tempHtml);                                 // 加父容器如<Li>
                    controlHtml = searchContainerItemHeaderTemplate.Replace(containerContent, dr["FieldAlias"].ToString());     // 添加搜索项标题                    controlHtml = controlHtml + tempHtml;                                                                       // 组合标题与单文本框代码                    controlHtml = searchContainerTemplate.Replace(containerContent, controlHtml);                               // 添加容器如<ul>
                    sbFieldSearchHtml.Append(controlHtml);
                    break;
            }
        }
        #endregion

        #region 搜索解析 -- 系统/预定义字段解析        private bool SysFieldSearch(DataRowView dr, ref StringBuilder sbSearchHtml)
        {
            StringBuilder sbSysFieldSearchHtml;  // 保存解析后的自定义字段解析搜索代码
            string controlHtml;                  // 当前节点的HTML代码
            string itemHtml;                     // 临时变量
            string configPath;                   // 链接配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;      // 链接/按钮配置根节点指针            XPathNodeIterator currentNav;        // 临时指针
            string styleWidth;                   // {#StyleWidth#}标签（控件的宽度）值            bool result;                         // 处理结果

            sbSysFieldSearchHtml = new StringBuilder();
            styleWidth = "0";
            result = true;
            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config";
            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);
            navNodeXPath.MoveNext();
            currentNav = navNodeXPath.Current.Select("field[@id=" + dr["SystemFirerdHtml"].ToString() + "]/search");

            if (currentNav != null && currentNav.Count > 0)
            {
                currentNav.MoveNext();
                controlHtml = currentNav.Current.InnerXml;
                controlHtml = searchContainerItemTemplate.Replace(containerContent, controlHtml);                      // 加标签如 <li>
                currentNav = navNodeXPath.Current.Select("field[@id=" + dr["SystemFirerdHtml"].ToString().Trim() + "]");
                itemHtml = GetSysFieldSearchParam(currentNav, ref styleWidth);                                         // 设置缺省值并获取显示标题
                itemHtml = searchContainerItemHeaderTemplate.Replace(containerContent, itemHtml);                      // 标题
                controlHtml = searchContainerTemplate.Replace(containerContent, itemHtml + controlHtml);
                controlHtml = controlHtml.Replace("{#StyleWidth#}", styleWidth);
                sbSysFieldSearchHtml.Append(controlHtml);
            }
            else
            {
                result = false;
            }

            if (result)     // 解析成功
            {
                sbSearchHtml.Append(sbSysFieldSearchHtml);
            }

            return result;
        }
        #endregion

        #region 设置系统自定义字段参数，用于编辑页中操作
        // 返回值用于显示标题        private string GetSysFieldSearchParam(XPathNodeIterator fieldNav,ref string styleWidth)
        {
            string[] arrFieldName;              // 配置节点配置的系统字段集合            string fieldName;                   // 从配置文件读取的字段名            string title;                       // 显示标题

            title = null;

            if (fieldNav != null && fieldNav.Count > 0)
            {
                fieldNav.MoveNext();
                title = fieldNav.Current.GetAttribute("title", "");

                try
                {
                    fieldName = fieldNav.Current.SelectSingleNode("name").Value;
                }
                catch
                {
                    fieldName = null;
                }

                if (fieldName != null)
                {
                    arrFieldName = fieldName.Split(new char[] { '|' });
                    DataRow[] currentDR = dtField.Select("Name='" + arrFieldName[0] + "'");

                    if (currentDR != null && currentDR.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(currentDR[0]["SearchWidth"].ToString()))
                        {
                            styleWidth = currentDR[0]["SearchWidth"].ToString();
                        }
                        
                        if (arrFieldName.Length < 2)        // 配置节点只有一个字段,时列表表题引用字段表中的标题
                        {
                            if (currentDR.Length > 0 && !string.IsNullOrEmpty(currentDR[0]["FieldAlias"].ToString()))
                            {
                                title = currentDR[0]["FieldAlias"].ToString();
                            }
                        }
                    }
                }
            }

            return title;
        }
        #endregion
    }
}
