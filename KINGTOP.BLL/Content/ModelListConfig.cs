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
// 作者：吴岸标
// 创建日期：2010-05-31
// 功能描述：列表列设置

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    /// <summary>
    /// 列表列设置
    /// </summary>
   public  class ModelListConfig
    {
        #region 私有变量
        /// <summary>
        /// 当前模型主键
        /// </summary>
        private string modelID;
        /// <summary>
        /// 模型字段
        /// </summary>
        private DataTable dtField;
        /// <summary>
        /// 保存模型属性
        /// </summary>
        private Hashtable hsModel;
       /// <summary>
       /// 列高
       /// </summary>
        private string listHeight;
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Content.IParseModel dal = (IDAL.Content.IParseModel)Assembly.Load(path).CreateInstance(path + ".Content.ParseModel");
        #endregion

        #region 构造函数
        public ModelListConfig(string modelID)
        {
            this.hsModel = new Hashtable();

            this.modelID = modelID;
        }
        #endregion

        #region 属性
       /// <summary>
        /// 当前模型记录
       /// </summary>
        public Hashtable HsModel
        {
            get { return this.hsModel; }
        }
        #endregion

        #region 初始数据加载
        // 初始数据
        private void Init()
        {
            ModelManage model;
            SelectParams selParams;
            DataTable modelTB;

            model = new ModelManage();

            selParams = new SelectParams();
            selParams.S1 = this.modelID;
            selParams.I1 = 1;
            modelTB = model.GetList("ONE", selParams);
            this.dtField = dal.GetField(this.modelID);

            if (modelTB.Rows.Count > 0)
            {
                foreach (DataColumn item in modelTB.Columns)
                {
                    this.hsModel.Add(item.ColumnName, modelTB.Rows[0][item.ColumnName]);
                }
            }

            if (!string.IsNullOrEmpty(this.hsModel["ListHeight"].ToString().Trim()) && Utils.ParseInt(this.hsModel["ListHeight"].ToString().Trim(), 0) > 0) // 设置列表列显示高
            {
                this.listHeight = "height:" + this.hsModel["ListHeight"].ToString().Trim() + "px;";
            }
            else
            {
                this.listHeight = "";
            }

        }
        #endregion

        #region 列表解析
        /// <summary>
        /// 列表列设置HTML代码生成
        /// </summary>
        public StringBuilder[] ListInfo()
        {
            StringBuilder sbHeaderHtml;       // 列表标题
            StringBuilder sbItemHtml;         // 列表项(用于输入文字测试)
            StringBuilder sbListWidth;        // 列表列宽设置HTML
            StringBuilder sbListItemOrder;    // 列表列显示顺序设置HTML  
            StringBuilder[] sbListInfo;
            bool isCheckColumn;               // 是否加复选框列
            DataView dvModelField;            // 在页面上显示的字段
            string sysFieldIDMarker;          // 标识系统配置ID是否已调用

            sbHeaderHtml = new StringBuilder();
            sbItemHtml = new StringBuilder();
            sbListWidth = new StringBuilder();
            sbListItemOrder = new StringBuilder();
            sbListInfo = new StringBuilder[4];

            this.Init();    // 加载所需记录
            dvModelField = this.dtField.DefaultView;
            dvModelField.RowFilter = "IsListEnable=1 or (IsSystemFiierd=1 and SystemFirerdHtml is not null and SystemFirerdHtml<>'')";
            dvModelField.Sort = "ListOrders asc,Orders asc";
            isCheckColumn = true;
            sysFieldIDMarker = ",";
            // 无连接栏
            if (this.hsModel["ListButton"].ToString().Trim() == "none$")
            {
                isCheckColumn = false;
            }

            sbHeaderHtml.Append("<ul itemType=\"Header\" class=\"ulheader\">");
            sbItemHtml.Append(" <ul itemType=\"Item\" class=\"ulheader ulbody\">");
            sbListWidth.Append("<ul itemType=\"ListWidth\" class=\"config\">");
            sbListItemOrder.Append("<ul itemType=\"ListOrder\" class=\"config\">");

            // 解析复选框列
            if (isCheckColumn)
            {
                sbHeaderHtml.Append("<li  style=\"width:45px;\"><input type=\"checkbox\" name=\"SlectAll\" id=\"SlectAll\" /></li>");
                sbItemHtml.Append("<li style=\"width:45px;"+ this.listHeight +"\"><input type=\"checkbox\" name=\"_chkID\" /></li>");
            }

            //  遍历当前模型字段
            foreach (DataRowView dr in dvModelField)
            {
                if (Utils.ParseBool(dr["IsSystemFiierd"]) && !string.IsNullOrEmpty(dr["SystemFirerdHtml"].ToString()))            // 自定义系统字段
                {
                    string sysFieldID;

                    sysFieldID = dr["SystemFirerdHtml"].ToString().Trim();           // 自定义系统字段配置节点ID
                    if (!sysFieldIDMarker.Contains("," + sysFieldID + ","))          // 自定义系统字段配置未调用
                    {
                        ListSysField(sysFieldID, ref sbHeaderHtml, ref sbItemHtml, ref sbListWidth, ref sbListItemOrder,dr["ListOrders"].ToString());
                        sysFieldIDMarker = sysFieldIDMarker + sysFieldID + ",";
                    }
                    else    // 已调用
                    {
                        continue;
                    }
                }
                else // 基本字段
                {
                    ListBasicField(dr, ref sbHeaderHtml, ref sbItemHtml,ref sbListWidth,ref sbListItemOrder);
                }
            }


            if (Utils.ParseBool(this.hsModel["IsOrderEdit"]))     // 排序编辑列
            {
                string orderListWidth;
                DataRow[] drOrder;

                drOrder = this.dtField.Select("name='orders'");

                // 列表表头
                if (drOrder.Length > 0 && drOrder[0]["ListWidth"] != null && !string.IsNullOrEmpty(drOrder[0]["ListWidth"].ToString()))
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

                sbHeaderHtml.Append("<li  style=\"width:" + orderListWidth + ";\" >排序</li>");

                // 列表项
                sbItemHtml.Append("<li  style=\"width:"+ orderListWidth +";"+ this.listHeight +"\"></li>");

                // 列表列宽设置
                sbListWidth.Append("<li>排序：<input type=\"text\"  value=\"" + orderListWidth.Replace("px", "") + "\" name=\"HQB_ListWidth_0_Orders\"  /></li>");
            }

            OperationColumn(ref sbHeaderHtml, ref sbItemHtml,ref sbListWidth);    // 预定义操作列

            sbHeaderHtml.Append("</ul>");
            sbItemHtml.Append("</ul>");
            sbListWidth.Append("</ul>");
            sbListItemOrder.Append("</ul>");
            sbListInfo[0] = sbHeaderHtml;
            sbListInfo[1] = sbItemHtml;
            sbListInfo[2] = sbListWidth;
            sbListInfo[3] = sbListItemOrder;

            return sbListInfo;
        }
        #endregion 

        #region 基本字段
        private void ListBasicField(DataRowView dr, ref StringBuilder sbHeaderHtml, ref StringBuilder sbItemHtml, ref StringBuilder sbListWidth, ref StringBuilder sbListItemOrder)
        {
            string listWidth;       // 显示列宽度

            if(string.Equals(dr["Name"].ToString().ToLower(),"orders"))
            {
                return;
            }

            if (dr["ListWidth"].ToString().Contains("%"))
            {
                listWidth = dr["ListWidth"].ToString();
            }
            else
            {
                listWidth = dr["ListWidth"].ToString() + "px";
            }

            // 列表标题
            sbHeaderHtml.Append("<li  style=\"width:" + listWidth + ";\">");
            sbHeaderHtml.Append(dr["FieldAlias"]);
            sbHeaderHtml.Append("</li>");

            // 列表项
            sbItemHtml.Append("<li  style=\"width:" + listWidth + ";"+ this.listHeight +"\"></li>");

            // 列表列宽设置
            sbListWidth.Append("<li>" + dr["FieldAlias"].ToString() + "：<input  value=\"" + listWidth.Replace("px", "") + "\"  type=\"text\" name=\"HQB_ListWidth_0_" + dr["Name"].ToString() + "\" /></li>");

            // 列表列顺序设置
            sbListItemOrder.Append("<li>" + dr["FieldAlias"].ToString() + "：<input  value=\"" + dr["ListOrders"].ToString() + "\"  type=\"text\"  name=\"HQB_Orders_0_" + dr["Name"].ToString() + "\"/></li>");
        }
        #endregion

        #region 解析自定义系统字段
        /// <summary>
        /// sysFieldID为模型表ModelManage
        // 中的系统字段SysField的值
        /// </summary>
        /// <returns></returns>
        private void ListSysField(string sysFieldID,ref StringBuilder sbHeaderHtml, ref StringBuilder sbItemHtml, ref StringBuilder sbListWidth, ref StringBuilder sbListItemOrder ,string order)
        {
            string configPath;                  // 配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;     // 字段配置根节点指针
            XPathNodeIterator currentNav;       // 临时指针

            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config";
            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);
            navNodeXPath.MoveNext();

            currentNav = navNodeXPath.Current.Select("field[@id=" + sysFieldID + "]");
            // 节点存在
            if (currentNav != null && currentNav.Count > 0)
            {
                string itemHeader;  // 列表标题
                string width;       // 列显示宽
                string title;       // 列显示标题
                string[] fieldName; // 字段名

                currentNav.MoveNext();
                try
                {
                    title = currentNav.Current.GetAttribute("title", "");
                    width = currentNav.Current.SelectSingleNode("list").GetAttribute("width", "");
                    fieldName = currentNav.Current.SelectSingleNode("name").Value.Split(new char[] { '|' });
                    DataRow[] currentDR = dtField.Select("Name='" + fieldName[0] + "'");

                    // 配置节点只有一个字段,时列表表题引用字段表中的标题
                    if (fieldName.Length < 2)
                    {
                        if (currentDR.Length > 0 && !string.IsNullOrEmpty(currentDR[0]["FieldAlias"].ToString()))
                        {
                            title = currentDR[0]["FieldAlias"].ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(currentDR[0]["ListWidth"].ToString()))  // 优选采用页面配置的值
                    {
                        width = currentDR[0]["ListWidth"].ToString();
                    }
                }
                catch
                {
                    return;
                }


                // 列表标题
                if (width.Contains("%"))
                {
                    itemHeader = "<li  style=\"width:" + width + "; ";
                }
                else
                {
                    itemHeader = "<li  style=\"width:" + width + "px; ";
                }
                sbHeaderHtml.Append(itemHeader + "\">");
                sbHeaderHtml.Append(title);
                sbHeaderHtml.Append("</li>");

                // 列表项
                sbItemHtml.Append(itemHeader + this.listHeight + "\">" + "</li>");

                // 列表列宽设置
                sbListWidth.Append("<li>" + title + "：<input type=\"text\"  value=\"" + width + "\" name=\"HQB_ListWidth_1_" + sysFieldID + "\" /></li>");

                // 列表列顺序设置
                sbListItemOrder.Append("<li>" + title + "：<input type=\"text\"  value=\"" + order + "\" name=\"HQB_ListOrders_1_"+ sysFieldID +"\" /></li>");
            }
        }
        #endregion

        #region Repeater中的预定义操作列
        #region 预定义操作列
        private void OperationColumn(ref StringBuilder sbHeaderHtml, ref StringBuilder sbItemHtml, ref StringBuilder sbListWidth)
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
                sbHeaderHtml.Append("<li style=\"width:" + columnWidth + ";\" >");
            }
            else
            {
                sbHeaderHtml.Append("<li style=\"width:" + columnWidth + "px;\" >");
            }

            sbHeaderHtml.Append("操作");
            sbHeaderHtml.Append("</li>");

            if (columnWidth.Contains("%"))
            {
                sbItemHtml.Append("<li style=\"width:" + columnWidth + ";" + this.listHeight + "\" >");
            }
            else
            {
                sbItemHtml.Append("<li style=\"width:" + columnWidth + "px;" + this.listHeight + "\" >");
            }
            sbItemHtml.Append(sbOperationColumn.ToString());
            sbItemHtml.Append("</li>");

            // 列表列宽设置
            sbListWidth.Append("<li>操作：<input  value=\"" + columnWidth + "\" type=\"text\" name=\"HQB_ListWidth_OpColumn\" /></li>");
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
                        link = currentNav.Current.GetAttribute("btName", "");
                        link = "<input type=\"button\"  value=\"" + link + "\" />";
                        sbOperationColumn.Append(link);
                    }
                }
            }

        }
        #endregion
        #endregion

        #region 保存配置
        public string Save(string modelID)
        {
            string effectRow;                   // 操作受影响行数            Hashtable hsListWidth;              // 列表列宽更新参数
            Hashtable hsListOrder;              // 列表列顺序更新参数            string operColumnWidth;             // 操作列宽度            string configPath;                  // 配置文件路径
            string xpath;
            XPathNodeIterator navNodeXPath;     // 字段配置根节点指针            XPathNodeIterator currentNav;       // 临时指针
            Hashtable advancedConfig;           // 高级设置参数，对应k_ModelManage中的字段

            hsListWidth = new Hashtable();
            hsListOrder = new Hashtable();
            advancedConfig = new Hashtable();

            operColumnWidth = string.Empty;
            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config";
            navNodeXPath = ModelManage.GetNodeIterator(configPath, xpath);
            navNodeXPath.MoveNext();

            // 高级设置赋值            advancedConfig.Add("ListHeight", HttpContext.Current.Request.Form["ListHeight"]);                               // 列表高            advancedConfig.Add("DeliverAndSearchUrlParam", HttpContext.Current.Request.Form["DeliverAndSearchUrlParam"]);   // URL传递且参与查询的参数            advancedConfig.Add("DeliverUrlParam", HttpContext.Current.Request.Form["DeliverUrlParam"]);                     // URL传递参数            advancedConfig.Add("FieldFromUrlParamValue", HttpContext.Current.Request.Form["FieldFromUrlParamValue"]);       // 字段值来源于URL参数值的字段
            advancedConfig.Add("BackDeliverUrlParam", HttpContext.Current.Request.Form["BackDeliverUrlParam"]);             // 页面返回时需传递的参数
            advancedConfig.Add("NotSearchField", HttpContext.Current.Request.Form["NotSearchField"]);                        // 不参与列表显示查询的字段

            foreach (string key in HttpContext.Current.Request.Form.AllKeys)
            {
                string fieldName;       // 字段名
                string fieldValue;      // 字段值

                fieldValue = HttpContext.Current.Request.Form[key];
                if (key.Contains("HQB_ListWidth_0_"))     // 基本字段列宽设置
                {
                    fieldName = key.Replace("HQB_ListWidth_0_","");
                    hsListWidth.Add(fieldName, fieldValue);
                    continue;
                }

                if (key.Contains("HQB_Orders_0_"))        // 基本字段顺序设置
                {
                    fieldName = key.Replace("HQB_Orders_0_", "");
                    hsListOrder.Add(fieldName, fieldValue);
                    continue;
                }

                if (key.Contains("HQB_ListWidth_1_"))      // 自定义系统字段列宽
                {
                    string sysFieldID;
                    string[] arrFieldName;
                    sysFieldID = key.Replace("HQB_ListWidth_1_", "");
                    currentNav = navNodeXPath.Current.Select("field[@id="+ sysFieldID +"]");
                    currentNav.MoveNext();

                    if (currentNav.Current.SelectSingleNode("name") != null)
                    {
                        arrFieldName = currentNav.Current.SelectSingleNode("name").Value.Split(new char[] { '|' });

                        foreach (string field in arrFieldName)
                        {
                            if (!string.IsNullOrEmpty(field))
                            {
                                hsListWidth.Add(field, fieldValue);
                            }
                        }
                    }
                    continue;
                }

                if (key.Contains("HQB_ListOrders_1_"))     // 自定义系统字段顺序设置
                {
                    string sysFieldID;
                    string[] arrFieldName;
                    sysFieldID = key.Replace("HQB_ListOrders_1_", "");

                    if (!string.IsNullOrEmpty(sysFieldID))
                    {
                        currentNav = navNodeXPath.Current.Select("field[@id=" + sysFieldID + "]");
                        currentNav.MoveNext();

                        if (currentNav.Current.SelectSingleNode("name") != null)
                        {
                            arrFieldName = currentNav.Current.SelectSingleNode("name").Value.Split(new char[] { '|' });

                            foreach (string field in arrFieldName)
                            {
                                if (!string.IsNullOrEmpty(field))
                                {
                                    hsListOrder.Add(field, fieldValue);
                                }
                            }
                        }
                    }
                    continue;
                }

                if (key.Contains("HQB_ListWidth_OpColumn"))  // 列表操作列宽度
                {
                    operColumnWidth = fieldValue;
                }
            }

            effectRow = dal.SaveConfig(hsListWidth, hsListOrder, modelID, operColumnWidth,advancedConfig);

            return effectRow;
        }
        #endregion
    }
}
