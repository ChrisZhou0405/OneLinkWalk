#region 引用程序集
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Text.RegularExpressions;

using KingTop.Common;
using KingTop.Model.Content;
#endregion


#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-03-26
// 功能描述：对模型自定义系统字段

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ModelManage
    {
        #region 加载系统（自定义）字段
        private DataTable LoadSysField()
        {
            string configPath;                  // 系统字段配置文件路径
            string xpath;                       // 查询语句
            XPathNodeIterator navNodeXPath;
            DataTable dtSysField;               // 系统字段记录
            DataRow dr;

            dtSysField = new DataTable();
            dtSysField.Columns.Add(new DataColumn("Title"));
            dtSysField.Columns.Add(new DataColumn("ID"));
            dtSysField.Columns.Add(new DataColumn("IsPublic"));

            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config/field";
            navNodeXPath = GetNodeIterator(configPath, xpath);

            while (navNodeXPath.MoveNext())
            {
                dr = dtSysField.NewRow();
                dr["Title"] = navNodeXPath.Current.GetAttribute("title", "");
                dr["ID"] = navNodeXPath.Current.GetAttribute("id", "");

                if (!string.IsNullOrEmpty(navNodeXPath.Current.GetAttribute("isPublic", "")))
                {
                    dr["IsPublic"] = 1;
                }
                else
                {
                    dr["IsPublic"] = 0;
                }

                dtSysField.Rows.Add(dr);
            }

            return dtSysField;
        }
        #endregion

        #region 绑定字段
        /// <summary>
        /// 绑定字段
        /// </summary>
        /// <param name="chklField">绑定控件</param>
        /// <param name="isAdd">操作方式</param>
        public void BindField(CheckBoxList chklField, bool isAdd)
        {
            DataTable dtSysField;           // 系统字段
            DataTable dtCommonField;        // 公用字段
              string tempTitle;

            dtSysField = LoadSysField();
            dtCommonField = dal.GetCommonField();


            if (dtCommonField != null && dtCommonField.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCommonField.Rows)
                {
                    AddFieldItem(ref chklField, dr, false, isAdd);

                    if (dtSysField != null && dtSysField.Rows.Count > 0)
                    {
                        tempTitle = dr["Title"].ToString();

                        for (int i = 0; i < dtSysField.Rows.Count; i++)
                        {
                            if (Regex.IsMatch(dtSysField.Rows[i]["Title"].ToString(), "^"+ tempTitle +".*$"))
                            {
                                AddFieldItem(ref chklField, dtSysField.Rows[i], true, isAdd);
                                dtSysField.Rows.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
            }

            if (dtSysField != null && dtSysField.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSysField.Rows)
                {
                    AddFieldItem(ref chklField, dr, true, isAdd);
                }
            }

        }


        // 添加字段(项)
        private static void AddFieldItem(ref CheckBoxList chkl,DataRow dataSource,bool isSys,bool isAdd)
        {
            ListItem chkItem;

            chkItem = new ListItem();
            chkItem.Text = dataSource["Title"].ToString();
        

            if (isSys)
            {
                chkItem.Value = "Sys_" + dataSource["ID"].ToString();
            }
            else
            {
                chkItem.Attributes.Add("ID", dataSource["ID"].ToString());
                chkItem.Attributes.Add("Alias", dataSource["Title"].ToString());
                chkItem.Value = "Com_" + dataSource["ID"].ToString();
            }

            if (Common.Utils.ParseBool(dataSource["IsPublic"]) && isAdd)
            {
                chkItem.Selected = true;
            }

            chkl.Items.Add(chkItem);
        }
        #endregion

        #region 初始字段设置
        /// <summary>
        /// 初始字段设置
        /// </summary>
        /// <param name="chklField">绑定控件</param>
        /// <param name="commonField">公用字段设置内容</param>
        /// <param name="sysField">系统字段设置内容</param>
        /// <param name="isAdd">操作类型</param>
        public void InitField(CheckBoxList chklField, string commonField, string sysField, bool isAdd)
        {
            string[] arrSysField;           // 已设置的系统字段ID
            string[] arrCommonField;        // 已设置的公用字段ID

            arrSysField = null;
            arrCommonField = null;

            if (!string.IsNullOrEmpty(commonField))
            {
                arrCommonField = commonField.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (!string.IsNullOrEmpty(sysField))
            {
                arrSysField = sysField.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            }

            if (!isAdd)  // 修改初始化
            {
                InitCheckBoxListByArrayValue(ref chklField, arrSysField, "Sys_");
                InitCheckBoxListByArrayValue(ref chklField, arrCommonField, "Com_");
            }
        }

        // 设置初始选中
        private static void InitCheckBoxListByArrayValue(ref CheckBoxList chkl, string[] arrValue, string prevStr)
        {
            if (arrValue != null && arrValue.Length > 0)
            {
                foreach (string value in arrValue)
                {
                    foreach (ListItem item in chkl.Items)
                    {
                        if (item.Value == prevStr + value)
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region 获取选取中的字段
        /// <summary>
        /// 获取选取中的字段
        /// </summary>
        /// <param name="chkl">字段显示控件</param>
        /// <param name="isSys">系统字段 True   公用字段  False</param>
        /// <returns></returns>
        public string GetCheckBoxListField(CheckBoxList chkl,bool isSys)
        {
            StringBuilder sbSelectedValue;
            string prevStr;
            string selectedValue;

            sbSelectedValue = new StringBuilder();

            foreach (ListItem item in chkl.Items)
            {
                if (item.Selected == true)
                {
                    prevStr = item.Value.Substring(0, 4);
                    selectedValue = item.Value.Substring(4, item.Value.Length - 4);

                    if (isSys && prevStr == "Sys_")   // 系统字段
                    {
                        if (sbSelectedValue.Length > 0)
                        {
                            sbSelectedValue.Append("," + selectedValue);
                        }
                        else
                        {
                            sbSelectedValue.Append(selectedValue);
                        }
                    }

                    if (!isSys && prevStr == "Com_")        // 公用字段
                    {
                        if (sbSelectedValue.Length > 0)
                        {
                            sbSelectedValue.Append("," + selectedValue);
                        }
                        else
                        {
                            sbSelectedValue.Append(selectedValue);
                        }
                    }
                }
            }

            return sbSelectedValue.ToString();
        }
        #endregion

        #region 获取解析后的预定义字段的设置
        /// <summary>
        /// 获取解析后的预定义字段的设置
        /// </summary>
        /// <param name="originalValue"></param>
        /// <param name="chklSysFieldSelectedValue"></param>
        /// <returns>[0] 需要删除 [1] 需要添加</returns>
        public List<SysField>[] GetSysField(string originalValue, string chklSysFieldSelectedValue)
        {
            string configPath;                  // 自定义字段配置文件路径
            string xpath;                       // 获取根节点的xpath语句
            XPathNodeIterator navNodeXPath;     // 系统自定义配置根节点指针
            XPathNodeIterator currentNav;       // 临时指针
            string currentValue;                // 需更新的值列
            string[] originalNodeID;            // 更新前的配置节点ID
            string[] currentNodeID;             // 需要更新的字段配置节点ID
            List<SysField> originalField;       // 更新前的自定义（系统）字段模（节点）
            List<SysField> currentField;        // 需更新的自定义（系统）字段模（节点）
            List<SysField>[] returnValue;
            List<string[]> lstSysFieldID;       // [0] 要删除 [1] 要添加

            currentValue = chklSysFieldSelectedValue;
            originalField = new List<SysField>();
            currentField = new List<SysField>();
            returnValue = new List<SysField>[2];

            // 没有更改链接选择则返回
            if (string.Equals(originalValue, currentValue))
            {
                return returnValue;
            }

            configPath = AppDomain.CurrentDomain.BaseDirectory + Utils.GetResourcesValue("Model", "SysFieldPath");
            xpath = "/config";
            navNodeXPath = GetNodeIterator(configPath, xpath);
            lstSysFieldID = CHKLValueGrouped(originalValue, currentValue);
            currentNodeID = lstSysFieldID[1];
            originalNodeID = lstSysFieldID[0];

            if (navNodeXPath != null && navNodeXPath.Count > 0)
            {
                navNodeXPath.MoveNext();
            }

            // 选择的字段
            if (currentNodeID.Length > 0)
            {
                // 遍历数组中的ID获取<field>节点的字段配置信息
                foreach (string fieldID in currentNodeID)
                {
                    if (!string.IsNullOrEmpty(fieldID))
                    {
                        currentNav = navNodeXPath.Current.Select("field[@id=" + fieldID + "]");
                    }
                    else
                    {
                        continue;
                    }
                    if (currentNav != null && currentNav.Count > 0)
                    {
                        currentNav.MoveNext();
                    }
                    else
                    {
                        continue;
                    }

                    SysField temp = new SysField();

                    try
                    {
                        temp.ID = currentNav.Current.GetAttribute("id", "");
                        temp.Title = currentNav.Current.GetAttribute("title", "");

                        if (currentNav.Current.SelectSingleNode("name") != null)
                        {
                            temp.Name = ToStringArray(currentNav.Current.SelectSingleNode("name").Value);
                        }

                        if (currentNav.Current.SelectSingleNode("alias") != null)
                        {
                            temp.Alias = ToStringArray(currentNav.Current.SelectSingleNode("alias").Value);
                        }

                        if (currentNav.Current.SelectSingleNode("sql") != null)
                        {
                            temp.DataType = ToStringArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("type", ""));

                            // 只有为字符串类型时来有此属性
                            if (currentNav.Current.SelectSingleNode("sql").GetAttribute("length", "") != null)
                            {
                                temp.TypeLength = ToIntArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("length", ""));
                            }
                            else
                            {
                                temp.TypeLength = null;
                            }

                            temp.DefaultValue = ToStringArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("default", ""));
                            temp.IsNull = ToBollArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("isnull", ""));
                        }

                        if (currentNav.Current.SelectSingleNode("search") != null)
                        {
                            temp.IsSearch = Utils.ParseBool(currentNav.Current.SelectSingleNode("search").GetAttribute("enable", ""));
                        }

                        if (currentNav.Current.SelectSingleNode("list") != null)
                        {
                            temp.IsList = Utils.ParseBool(currentNav.Current.SelectSingleNode("list").GetAttribute("enable", ""));
                        }

                        if (currentNav.Current.SelectSingleNode("edit") != null)
                        {
                            temp.IsEdit = Utils.ParseBool(currentNav.Current.SelectSingleNode("edit").GetAttribute("enable", ""));
                        }

                        currentField.Add(temp);
                    }
                    catch
                    {
                        currentField = null;    // 异常提示信息:链接配置文件读取异常
                    }
                }
            }

            // 取消的字段
            if (originalNodeID.Length > 0)
            {
                // 遍历数组中的ID获取<field>节点的字段配置信息

                foreach (string fieldID in originalNodeID)
                {
                    if (!string.IsNullOrEmpty(fieldID))
                    {
                        currentNav = navNodeXPath.Current.Select("field[@id=" + fieldID + "]");
                    }
                    else
                    {
                        continue;
                    }
                    if (currentNav != null && currentNav.Count > 0)
                    {
                        currentNav.MoveNext();
                    }
                    else
                    {
                        continue;
                    }

                    SysField temp = new SysField();

                    try
                    {
                        temp.ID = currentNav.Current.GetAttribute("id", "");
                        temp.Title = currentNav.Current.GetAttribute("title", "");
                        if (currentNav.Current.SelectSingleNode("name") != null)
                        {
                            temp.Name = ToStringArray(currentNav.Current.SelectSingleNode("name").Value);
                        }
                        if (currentNav.Current.SelectSingleNode("alias") != null)
                        {
                            temp.Alias = ToStringArray(currentNav.Current.SelectSingleNode("alias").Value);
                        }
                        if (currentNav.Current.SelectSingleNode("sql") != null)
                        {
                            temp.DataType = ToStringArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("type", ""));

                            // 只有为字符串类型时来有此属性

                            if (currentNav.Current.SelectSingleNode("sql").GetAttribute("length", "") != null)
                            {
                                temp.TypeLength = ToIntArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("length", ""));
                            }
                            else
                            {
                                temp.TypeLength = null;
                            }

                            temp.DefaultValue = ToStringArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("default", ""));
                            temp.IsNull = ToBollArray(currentNav.Current.SelectSingleNode("sql").GetAttribute("isnull", ""));
                        }
                        if (currentNav.Current.SelectSingleNode("search") != null)
                        {
                            temp.IsSearch = Utils.ParseBool(currentNav.Current.SelectSingleNode("search").GetAttribute("enable", ""));
                        }
                        if (currentNav.Current.SelectSingleNode("list") != null)
                        {
                            temp.IsList = Utils.ParseBool(currentNav.Current.SelectSingleNode("list").GetAttribute("enable", ""));
                        }
                        if (currentNav.Current.SelectSingleNode("edit") != null)
                        {
                            temp.IsList = Utils.ParseBool(currentNav.Current.SelectSingleNode("edit").GetAttribute("enable", ""));
                        }

                        originalField.Add(temp);
                    }
                    catch
                    {
                        // 异常提示信息:链接配置文件读取异常
                        originalField = null;
                    }
                }
            }

            returnValue[0] = originalField;
            returnValue[1] = currentField;
            return returnValue;
        }
        #endregion

        #region 获取模型参数,将系统字段保存至字段表中
        /// <summary>
        /// 获系统（自定义）字段的SQL语句
        /// </summary>
        /// <param name="mSysField">字段设置值通过GetSysField方法获取</param>
        /// <returns>[0] DDL 语句  [1] DML 语句</returns>
        public string[] GetSysFieldSQL(List<SysField>[] mSysField)
        {
            StringBuilder delSysField;              // 从字段表(K_ModelField)中删除的SQL语句列

            StringBuilder dropSysField;             // 从模型表中删除表列的SQL语句列

            StringBuilder addSysField;              // 从模型表中添加表列的SQL语句列

            StringBuilder insertSysField;           // 从字段表(K_ModelField)中插入的SQL语句列

            string[] retunValue;                // [0] DDL 语句  [1] DML 语句
            string tableName;

            dropSysField = new StringBuilder();
            delSysField = new StringBuilder();
            addSysField = new StringBuilder();
            insertSysField = new StringBuilder();
            retunValue = new string[2];

            if (this.mModelManage != null)
            {
                tableName = this.mModelManage.TableName;
            }
            else
            {
                // 提示信息 :获取模型表名失败
                // 写入日志
                return retunValue;
            }

            // 删除重复项

            mSysField[0] = RemoveDuplicate(mSysField[0]);
            mSysField[1] = RemoveDuplicate(mSysField[1]);

            if (mSysField[0] != null)
            {
                // 需删除的字段

                foreach (SysField field in mSysField[0])
                {
                    if (field.DataType != null && field.DataType.Length > 0)
                    {
                        dropSysField.Append(dal.DropTableFieldPackaing(tableName, field.Name));                 // 从创建的模型表中删除列

                    }
                    delSysField.Append(dal.DeleteModelFieldRowPackaing(this.mModelManage.ID, field.Name));  // 从K_ModelField表中删除本字段的记录
                }
            }

            if (mSysField[1] != null)
            {
                // 需添加的字段

                foreach (SysField field in mSysField[1])
                {
                    string[] arrID;
                    int[] order;

                    if (field.DataType != null && field.DataType.Length > 0)  // 需要创建表列的配置节点
                    {
                        arrID = new string[field.Name.Length];
                        order = new int[field.Name.Length];

                        for (int i = 0; i < field.Name.Length; i++)
                        {
                            this.order = this.order + 1;    // 用于保存当前排序(排序的最大值)
                            order[i] = this.order;
                            arrID[i] = Public.GetTableID("0", this.order);
                        }

                        addSysField.Append(dal.AddTableFieldPackaing(tableName, field));
                        insertSysField.Append(dal.InsertModelFieldRowPackaing(field, arrID, this.mModelManage.ID, order));
                    }
                    else if (!string.IsNullOrEmpty(field.Title))   // 不需要创建表列的配置节点
                    {
                        arrID = new string[1];
                        order = new int[1];

                        this.order = this.order + 1;    // 用于保存当前排序(排序的最大值)
                        arrID[0] = Public.GetTableID("0", this.order);
                        order[0] = this.order;

                        insertSysField.Append(dal.InsertModelFieldRowPackaing(field, arrID, this.mModelManage.ID, order));
                    }
                }
            }

            retunValue[0] = dropSysField.ToString() + addSysField.ToString();
            retunValue[1] = delSysField.ToString() + insertSysField.ToString();
            return retunValue;
        }
        #endregion

        #region 将字符串按|字符分隔成字符串数组
        // 将字符串按|字符分隔成字符串数组
        public string[] ToStringArray(string parseStr)
        {
            string[] arrParse;

            if (parseStr.Contains("|"))
            {
                arrParse = parseStr.Split(new char[] { '|' });
            }
            else
            {
                arrParse = new string[] { parseStr };
            }

            return arrParse;
        }
        #endregion

        #region  将字符串按|字符分隔成布尔型数组
        // 将字符串按|字符分隔成布尔型数组
        public bool[] ToBollArray(string parseStr)
        {
            bool[] arrBool;

            if (parseStr.Contains("|"))
            {
                string[] arrParse = parseStr.Split(new char[] { '|' });
                arrBool = new bool[arrParse.Length];

                for (int i = 0; i < arrParse.Length; i++)
                {
                    arrBool[i] = Utils.ParseBool(arrParse[i]);
                }
            }
            else
            {
                arrBool = new bool[] { Utils.ParseBool(parseStr) };
            }

            return arrBool;
        }
        #endregion

        #region 将字符串按|字符分隔成整型数组

        // 将字符串按|字符分隔成字符串数组
        public int?[] ToIntArray(string parseStr)
        {
            int?[] arrInt;

            if (parseStr.Contains("|"))
            {
                string[] arrParse = parseStr.Split(new char[] { '|' });
                arrInt = new int?[arrParse.Length];

                for (int i = 0; i < arrParse.Length; i++)
                {
                    arrInt[i] = Utils.ParseInt(arrParse[i], 0);
                }
            }
            else
            {
                arrInt = new int?[] { Utils.ParseInt(parseStr, 0) };
            }

            return arrInt;
        }
        #endregion

        #region 消除重复项

        private List<SysField> RemoveDuplicate(List<SysField> sysField)
        {
            if (sysField != null)
            {
                for (int i = 0; i < sysField.Count - 1; i++)
                {
                    for (int j = sysField.Count - 1; j > i; j--)
                    {
                        if (string.Equals(sysField[i].Name, sysField[j].Name))
                        {
                            sysField.RemoveAt(j);
                        }
                    }
                }
            }

            return sysField;
        }
        #endregion

        #region 保存/修改公用字段
        public string SaveCommonField(string originalValue,string newValue,bool isAdd)
        {
            string[] tableID;
            string msg;
            int maxOrders;
            string fieldID;
            string resultMsg;
            List<string[]> lstCommonFieldID;                // [0] 删除的ID集合 [1] 要添加的ID集合


            lstCommonFieldID = CHKLValueGrouped(originalValue, newValue);
            resultMsg = "[";
            tableID = Public.GetTableID("0", "K_ModelField");
            maxOrders = Utils.ParseInt(tableID[1], 5000);

            if ((string.IsNullOrEmpty(originalValue) && string.IsNullOrEmpty(newValue)) || (originalValue.Trim() == newValue.Trim()))
            {
                return "";
            }

            foreach (string commonFieldID in lstCommonFieldID[1])       // 添加公用字段
            {
                if (commonFieldID.Trim() != "")
                {
                    fieldID = Public.GetTableID("0", maxOrders);
                    msg = dal.SetCommonField(this.mModelManage.ID, commonFieldID, fieldID, maxOrders, 1);

                    switch (msg.Trim())
                    {
                        case "-100":
                            msg = "ID存在重复";
                            break;
                        case "-200":
                            msg = "当前模型中已经存在当前字段";
                            break;
                        case "-300":
                            msg = "程序执行有误";
                            break;
                    }

                    if (resultMsg.Length < 10)
                    {
                        resultMsg += "{\"Field\":\"" + commonFieldID + "\",Msg:\"" + msg + "\"}";
                    }
                    else
                    {
                        resultMsg += ",{\"Field\":\"" + commonFieldID + "\",Msg:\"" + msg + "\"}";
                    }
                }

                maxOrders++;
            }

            foreach (string commonFieldID in lstCommonFieldID[0])       // 删除公用字段
            {
                if (commonFieldID.Trim() != "")
                {
                    dal.SetCommonField(this.mModelManage.ID, commonFieldID, "", 0, 2);
                }
            }

            resultMsg += "]";
            return resultMsg;
        }
        #endregion
    }
}
