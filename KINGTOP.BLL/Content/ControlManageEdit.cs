#region 程序集引用
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL.Content;

using KingTop.Template;
using KingTop.Template.ParamType;
using KingTop.Common;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-04-12
// 功能描述：处理解析后的模型编辑页
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class ControlManageEdit
    {
        #region 变量成员
        public string strlist = "";
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private static string filterUrlParam = ",Action,ID,"; // 要过滤的URL参数名
        private IDAL.Content.IControlManageEdit dal = (IDAL.Content.IControlManageEdit)Assembly.Load(path).CreateInstance(path + ".Content.ControlManageEdit");
        /// <summary>
        /// 各字段缺省值
        /// </summary>
        private string fieldDefaultValue;
        /// <summary>
        /// 表名
        /// </summary>
        private string tableName;
        /// <summary>
        /// 所属模型
        /// </summary>
        private string modelId;
        /// <summary>
        /// 各字段更新值对,以字段名称为键名
        /// </summary>
        private Hashtable hsEditField;
        /// <summary>
        /// 站点ID
        /// </summary>
        private int siteID;
        /// <summary>
        /// 当前节点代码(ID)
        /// </summary>
        private string nodeCode;
        /// <summary>
        /// 是否启用添加到专题
        /// </summary>
        private bool _isEnableSpecial = true;
        /// <summary>
        /// 是否启用添加至节点
        /// </summary>
        private bool _isEnableNode = true;
        /// <summary>
        /// 当前登陆的用户名
        /// </summary>
        private string _userName;
        /// <summary>
        /// 保存传递URL参数
        /// </summary>
        private string _keepUrlParam = string.Empty;
        /// <summary>
        /// 保存通过URL参数赋值的字段名,多个用逗号隔开
        /// </summary>
        private string _fieldFromUrlParamValue;
        /// <summary>
        /// 当前编辑或新增的记录ID 
        /// </summary>
        private string _recordID;
        #endregion

        #region 属性
        /// <summary>
        /// 当前编辑或新增的记录ID 
        /// </summary>
        public string RecordID
        {
            get { return this._recordID; }
            set { this._recordID = value; }
        }
        /// <summary>
        /// 保存通过URL参数赋值的字段名,多个用逗号隔开
        /// </summary>
        public string FieldFromUrlParamValue
        {
            set { this._fieldFromUrlParamValue = value; }
        }

        /// <summary>
        /// 保存传递URL参数
        /// </summary>
        public string KeepUrlParam
        {
            get
            {
                if (string.IsNullOrEmpty(this._keepUrlParam))
                {
                    SetKeepUrlParam();
                }
                return this._keepUrlParam;
            }
        }

        /// <summary>
        /// 各字段更新值对,以字段名称为键名
        /// </summary>
        public Hashtable HsEditField
        {
            get { return this.hsEditField; }
        }

        /// <summary>
        /// 站点ID
        /// </summary>
        public int SiteID
        {
            set { this.siteID = value; }
            get { return this.siteID; }
        }

        /// <summary>
        /// 节点ID
        /// </summary>
        public string NodeCode
        {
            set { this.nodeCode = value; }
            get { return this.nodeCode; }
        }

        /// <summary>
        /// 是否启用添加到专题
        /// </summary>
        public bool IsEnableSpecial
        {
            set { this._isEnableSpecial = value; }
        }

        /// <summary>
        /// 是否启用添加至节点
        /// </summary>
        public bool IsEnableNode
        {
            set { this._isEnableNode = value; }
        }

        /// <summary>
        /// 当前登陆的用户名
        /// </summary>
        public string UserName
        {
            set { this._userName = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 创建模型编辑页处理对象
        /// </summary>
        /// <param name="defaultValue">保存各字段缺省值的字符串</param>
        /// <param name="tbName">表名</param>
        /// <param name="mID">模型ID</param>
        public ControlManageEdit(string defaultValue, string tbName, string mID)
        {
            this.modelId = mID;
            this.tableName = tbName;
            this.fieldDefaultValue = defaultValue;
            this.hsEditField = new Hashtable();
        }
        #endregion

        #region 绑定自定义字段(其值来源于数据库) 重载 +1
        /// <summary>
        /// 绑定自定义字段值
        /// </summary>
        /// <param name="tableName">引用表名</param>
        /// <param name="txtColumn">显示文本引用列</param>
        /// <param name="valueColumn">值引用列</param>
        /// <param name="sqlWhere">引用条件</param>
        /// <returns></returns>
        public Hashtable FieldBind(string tableName, string txtColumn, string valueColumn, string sqlWhere)
        {
            Regex regParam;                         // 匹配条件中值来自于URL参数值
            MatchCollection collectParam;

            regParam = new Regex(@"\{#\s*(?<1>[^#]+)\s*#\}");
            collectParam = regParam.Matches(sqlWhere);

            foreach (Match matchItem in collectParam)
            {
                sqlWhere = sqlWhere.Replace(matchItem.Value, HttpContext.Current.Request.QueryString[matchItem.Groups[1].Value]);
            }

            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            return dal.GetHashTable(tableName, txtColumn, valueColumn, sqlWhere);
        }
        #endregion

        #region MyRegion
        public string FieldBind(string Fieldvalue, string mark, string markes, string Field, string listtext, string listvalue, string parentid, string tableName, string orders, string menuid, string productclass)
        {


            string sql = "select * from " + tableName + " where menuid='{0}' and ispub=1  order by " + orders + " asc";
            sql = string.Format(sql, menuid);
            DataTable dt = KingTop.Common.Tools.GetDataSet(sql);

            getclass(dt, Fieldvalue, mark, markes, Field, listtext, listvalue, parentid, productclass);

            return strlist;
        }
        #endregion

        #region 无限极分类1
        /// <summary>
        /// 无极分类
        /// </summary>
        /// <param name="droplist">分类控件</param>
        /// <param name="dt">分类表</param>
        /// <param name="Fieldvalue">分类刷选值</param>
        /// <param name="mark">主分类区分</param>
        /// <param name="markes">次分类区分</param>
        /// <param name="Field">分类刷选字段</param>
        /// <param name="listtext">分类text值字段</param>
        /// <param name="listvalue">分类value值字段</param>
        /// <param name="parentid">子类筛选值字段</param>
        public void getclass(DataTable dt, string Fieldvalue, string mark, string markes, string Field, string listtext, string listvalue, string parentid, string productclass)
        {

            try
            {
                DataRow[] dr = dt.Select("" + Field + "='" + Fieldvalue + "'", "");
                if (dr.Length > 0)
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        if (mark == "null")
                        {
                            if (dr[i][listvalue].ToString() == productclass)
                                strlist += "<option selected='selected'  value='" + dr[i][listvalue].ToString() + "'>" + markes + dr[i][listtext].ToString() + "</option>";
                            else
                                strlist += "<option   value='" + dr[i][listvalue].ToString() + "'>" + markes + dr[i][listtext].ToString() + "</option>";
                            // droplist.Items.Add(new ListItem(markes + dr[i][listtext].ToString(), dr[i][listvalue].ToString()));
                        }
                        else
                        {
                            if (dr[i][listvalue].ToString() == productclass)
                                strlist += "<option selected='selected'  value='" + dr[i][listvalue].ToString() + "'>" + mark + dr[i][listtext].ToString() + "</option>";
                            else
                                strlist += "<option   value='" + dr[i][listvalue].ToString() + "'>" + mark + dr[i][listtext].ToString() + "</option>";

                            //droplist.Items.Add(new ListItem(mark + dr[i][listtext].ToString(), dr[i][listvalue].ToString()));
                        }

                        getclass(dt, dr[i][parentid].ToString(), "null", markes + markes, Field, listtext, listvalue, parentid, productclass);
                    }
                }
            }
            catch (Exception ce)
            {
                //Pager.ClientScript.RegisterStartupScript(this.GetType(), "key", "<script>alert('传入参数有误！');</script>");
            }

        }
        #endregion

        #region 绑定自定义字段(其值来源于数据库) 重载 +2
        /// <summary>
        /// 绑定自定义字段值
        /// </summary>
        /// <param name="tableName">引用表名</param>
        /// <param name="txtColumn">显示文本引用列</param>
        /// <param name="valueColumn">值引用列</param>
        /// <param name="sqlWhere">引用条件</param>
        /// <param name="sqlOrder">排序</param>
        /// <returns></returns>
        public DataTable FieldBind(string tableName, string txtColumn, string valueColumn, string sqlWhere, string sqlOrder)
        {
            Regex regParam;                         // 匹配条件中值来自于URL参数值
            MatchCollection collectParam;

            regParam = new Regex(@"\{#\s*(?<1>[^#]+)\s*#\}");
            collectParam = regParam.Matches(sqlWhere);

            foreach (Match matchItem in collectParam)
            {
                sqlWhere = sqlWhere.Replace(matchItem.Value, HttpContext.Current.Request.QueryString[matchItem.Groups[1].Value]);
            }

            if (string.IsNullOrEmpty(tableName))
            {
                return null;
            }

            return dal.GetDataTable(tableName, txtColumn, valueColumn, sqlWhere, sqlOrder);
        }
        #endregion

        #region 加载字段默认/初始值
        /// <summary>
        /// 加载字段默认/初始值
        /// </summary>
        /// <returns></returns>
        public Hashtable FillField()
        {
            Hashtable hsInitField;      // 各字段初始值,以字段名称为键名

            if (HttpContext.Current.Request["action"] != null && HttpContext.Current.Request["action"].ToString() == "edit")  // 编辑操作
            {
                string id;             // 记录ID

                id = HttpContext.Current.Request.QueryString["ID"];

                if (!string.IsNullOrEmpty(id))
                {
                    hsInitField = dal.GetHashTableByID(this.tableName, id);
                    LoadSpecial(id, ref hsInitField);
                    LoadNode(ref hsInitField);
                }
                else
                {
                    hsInitField = null;
                }
            }
            else // 添加操作
            {
                hsInitField = ParseFieldParam();
                LoadSpecial(null, ref hsInitField);
                LoadNode(ref hsInitField);
            }

            return hsInitField;
        }
        #endregion

        #region 解析字段参数
        // 字段参数格式  字段名|字段缺省值|类型 多个用逗号隔开
        private Hashtable ParseFieldParam()
        {
            Hashtable hsFieldParam;            // 字段缺省值
            string[] arrField;                // 字段键值对 [0]字段名 [1] 字段缺省值
            string[] fieldItem;               // 字段参数项 格式  字段名|字段缺省值
            string defaultValue;            // 保存各字段缺省值的字符串

            hsFieldParam = new Hashtable();
            defaultValue = this.fieldDefaultValue;

            if (!string.IsNullOrEmpty(defaultValue))    // 设置了字段参数
            {
                fieldItem = defaultValue.Split(new char[] { ',' });

                foreach (string field in fieldItem)     // 遍历每个字段
                {
                    if (!string.IsNullOrEmpty(field))
                    {
                        arrField = field.Split(new char[] { '|' });

                        if (arrField.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(arrField[0]))
                            {
                                hsFieldParam.Add(arrField[0], arrField[1]);  // 缺省值
                            }
                        }
                    }
                }
            }
            else  // 没有设置字段参数
            {
                hsFieldParam = null;
            }

            return hsFieldParam;
        }

        private Hashtable ParseFieldParam(ref Hashtable hsFieldType)
        {
            Hashtable hsFieldParam;            // 字段缺省值
            string[] arrField;                // 字段键值对 [0]字段名 [1] 字段缺省值 [2] 字段类型
            string[] fieldItem;               // 字段参数项 格式  字段名|字段缺省值
            string defaultValue;            // 保存各字段缺省值的字符串

            hsFieldParam = new Hashtable();
            defaultValue = this.fieldDefaultValue;

            if (!string.IsNullOrEmpty(defaultValue))    // 设置了字段参数
            {
                fieldItem = defaultValue.Split(new char[] { ',' });

                foreach (string field in fieldItem)     // 遍历每个字段
                {
                    if (!string.IsNullOrEmpty(field))
                    {
                        arrField = field.Split(new char[] { '|' });

                        if (arrField.Length > 2 && !string.IsNullOrEmpty(arrField[0]))
                        {
                            hsFieldParam.Add(arrField[0], arrField[1]);  // 缺省值

                            hsEditField.Add(arrField[0], arrField[2]);   // 字段基本类型
                        }
                    }
                }
            }
            else  // 没有设置字段参数
            {
                hsFieldParam = null;
            }

            return hsFieldParam;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="isAdd">是否是添加操作</param>
        /// <param name="id">修改时记录ID</param>
        /// <returns>返回处理结果</returns>
        public string Edit(bool isAdd, string id)
        {
            string msg;                                         // 操作影响行数
            Hashtable hsFieldParam;                             // 字段缺省值
            bool isAllowFlow;                                   // 是否启用了审核流程
            Dictionary<string, string> dicSubModel;

            isAllowFlow = NodeFlowCheckEnsure();
            hsFieldParam = ParseFieldParam();
            SetFieldFromUrlParamValue(ref hsEditField);       // 用URL参数值给字段赋值
            this.RecordID = id;
            dicSubModel = GetSubModelContent();

            if (hsFieldParam == null)                         // 字段参数有误
            {
                msg = "字段参数有误";
            }
            else
            {
                if (!isAllowFlow)
                {
                    hsEditField.Add("FlowState", 99);
                }
                string htmlField = HttpContext.Current.Request.Form["hdnHtmlField"] + ",";
                if (isAdd)   // 添加
                {
                    string[] tableID;

                    tableID = Public.GetTableID("0", this.tableName);
                    this.RecordID = tableID[0];

                    foreach (object fieldName in hsFieldParam.Keys)      // 遍历获取字段值
                    {
                        string fieldValue;                               // 当前字段值
                        if (htmlField.IndexOf("_" + fieldName.ToString() + ",") == -1)
                        {
                            fieldValue = HttpContext.Current.Request.Form[fieldName.ToString()];
                        }
                        else
                        {
                            fieldValue = HttpContext.Current.Request.Form["editor_"+fieldName+"$txtEditorContent"];
                        }

                        //gavin20140108加
                        //===========
                        fieldValue = fieldValue == null ? "" : fieldValue;
                        //===========

                        if (fieldValue != null)
                        {
                            hsEditField.Add(fieldName, fieldValue);
                        }
                    }

                    hsEditField.Add("NodeCode", this.nodeCode);
                    hsEditField.Add("SiteID", this.siteID);
                    hsEditField.Add("ReferenceID", tableID[0]);
                    hsEditField.Add("AddMan", this._userName);
                    msg = dal.Add(this.tableName, tableID[0], Utils.ParseInt(tableID[1], 0), hsEditField, dicSubModel);

                    if (this._isEnableNode && msg == "1")           // 若记录添加成功且启用了附加节点的功能则附加至节点
                    {
                        AppendTolNode(tableID[0]);
                    }

                    if (this._isEnableSpecial && msg == "1")         // 若记录添加成功且启用了附加至专题的功能则附加至专题信息表
                    {
                        AppendToSpecial(tableID[0]);
                    }
                }
                else        //修改
                {
                    foreach (object fieldName in hsFieldParam.Keys)      // 遍历获取字段值
                    {
                        string fieldValue;                               // 当前字段值

                        if (htmlField.IndexOf("_" + fieldName.ToString() + ",") == -1)
                        {
                            fieldValue = HttpContext.Current.Request.Form[fieldName.ToString()];
                        }
                        else
                        {
                            fieldValue = HttpContext.Current.Request.Form["editor_" + fieldName + "$txtEditorContent"];
                        }
                        //gavin20140108加
                        //===========
                        fieldValue = fieldValue == null ? "" : fieldValue;
                        //===========

                        if (fieldValue != null)
                        {
                            hsEditField.Add(fieldName, fieldValue);
                        }
                    }

                    hsEditField.Add("NodeCode", this.nodeCode);
                    hsEditField.Add("SiteID", this.siteID);
                    msg = dal.Update(this.tableName, id, hsEditField, dicSubModel);

                    if (this._isEnableSpecial)  // 启用了附加至专题的功能
                    {
                        EditSpecialInfo(id);
                        LoadSpecial(id, ref hsEditField);
                    }

                    if (this._isEnableNode)     // 启用了附加至节点的功能 
                    {
                        LoadNode(ref hsEditField);
                    }
                }
            }
            return msg;
        }
        #endregion

        #region  修改附加到专题
        private void EditSpecialInfo(string infoID)
        {
            string originalSpecialMenuParam;   // 更新前所附加的传题栏目
            string specialMenuParam;           // 选取要附加的专题栏目，格式：专题ID|专题栏目ID
            string[] addSpecialID;             // 要添加的记录对应的专题ID
            string[] delSpecialID;             // 要删除的记录对应的专题ID
            string[] addSpecialMenuID;         // 要添加的记录对应的专题栏目ID
            string[] delSpecialMenuID;         // 要删除的记录对应的专题栏目ID
            string[] arrOriginal;              // 临时变量
            string[] arrCurrent;               // 临时变量
            string[] item;                     // 临时变量，记录专题信息中的专题ID与专题栏目ID

            specialMenuParam = Utils.ReqFromParameter("SpecialID");
            originalSpecialMenuParam = Utils.ReqFromParameter("OriginalSpecialID");
            arrOriginal = originalSpecialMenuParam.Split(new char[] { ',' });
            arrCurrent = specialMenuParam.Split(new char[] { ',' });

            if (arrOriginal != null && arrCurrent != null && arrCurrent.Length > 0 && arrOriginal.Length > 0)
            {

                foreach (string original in arrOriginal)        // 消除不更改的记录
                {
                    foreach (string current in arrCurrent)
                    {
                        if (string.Equals(original, current))
                        {
                            if (!string.IsNullOrEmpty(specialMenuParam))
                            {
                                specialMenuParam = specialMenuParam.Replace(original + ",", "");
                                specialMenuParam = specialMenuParam.Replace(original, "");
                            }
                            if (!string.IsNullOrEmpty(originalSpecialMenuParam))
                            {
                                originalSpecialMenuParam = originalSpecialMenuParam.Replace(original + ",", "");
                                originalSpecialMenuParam = originalSpecialMenuParam.Replace(original, "");
                            }
                            break;
                        }
                    }
                }

                arrOriginal = originalSpecialMenuParam.Split(new char[] { ',' });   // 要删除的记录
                arrCurrent = specialMenuParam.Split(new char[] { ',' });            // 要添加的记录

                if (arrOriginal.Length > 0)  // 存在要删除的记录
                {
                    delSpecialID = new string[arrOriginal.Length];
                    delSpecialMenuID = new string[arrOriginal.Length];

                    for (int i = 0; i < arrOriginal.Length; i++)     // 遍历所有要删除的专题信息记录
                    {
                        if (!string.IsNullOrEmpty(arrOriginal[i]))
                        {
                            item = arrOriginal[i].Split(new char[] { '|' });
                            if (item.Length > 1)
                            {
                                delSpecialID[i] = item[0];
                                delSpecialMenuID[i] = item[1];
                            }
                        }
                    }

                }
                else
                {
                    delSpecialID = null;
                    delSpecialMenuID = null;
                }

                if (arrCurrent.Length > 0)
                {
                    addSpecialID = new string[arrCurrent.Length];
                    addSpecialMenuID = new string[arrCurrent.Length];

                    for (int i = 0; i < arrCurrent.Length; i++)     // 遍历所有要添加的专题信息记录
                    {
                        if (!string.IsNullOrEmpty(arrCurrent[i]))
                        {
                            item = arrCurrent[i].Split(new char[] { '|' });
                            if (item.Length > 1)
                            {
                                addSpecialID[i] = item[0];
                                addSpecialMenuID[i] = item[1];
                            }
                        }
                    }
                }
                else
                {
                    addSpecialID = null;
                    addSpecialMenuID = null;
                }

                if (delSpecialID != null || addSpecialID != null)
                {
                    dal.EditSpecialInfo(delSpecialID, delSpecialMenuID, addSpecialID, addSpecialMenuID, infoID);
                }
            }

        }
        #endregion

        #region 添加到的专题
        // infoID 当前更新的记录ID
        private void AppendToSpecial(string infoID)
        {
            string specialMenuParam;        // 选取要附加的专题栏目，格式：专题ID|专题栏目ID
            string[] specialID;             // 专题ID
            string[] specialMenuID;         // 专题栏目ID
            string[] specialItem;           // 临时变量
            string[] item;                  // 临时变量
            int i;                          // 计数

            specialMenuParam = Utils.ReqFromParameter("SpecialID");
            i = 0;

            if (!string.IsNullOrEmpty(specialMenuParam))
            {
                specialItem = specialMenuParam.Split(new char[] { ',' });
                specialID = new string[specialItem.Length];
                specialMenuID = new string[specialItem.Length];

                foreach (string specialMenu in specialItem)
                {
                    if (!string.IsNullOrEmpty(specialMenu))
                    {

                        item = specialMenu.Split(new char[] { '|' });
                        if (item.Length > 1)
                        {
                            specialID[i] = item[0];
                            specialMenuID[i] = item[1];
                            i++;
                        }
                    }
                }

                dal.AppendToSpecial(specialID, specialMenuID, infoID);
            }
        }
        #endregion

        #region 加载所属专题栏目
        private void LoadSpecial(string specialInfoID, ref Hashtable hsInitField)
        {
            DataTable dt;
            StringBuilder specialMenuID;           // 记录所附加的栏目
            StringBuilder specialMenuTitle;        // 记录所附加的栏目（名称)

            specialMenuID = new StringBuilder();
            specialMenuTitle = new StringBuilder();

            if (!string.IsNullOrEmpty(specialInfoID))
            {

                dt = dal.LoadSpecial(specialInfoID);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        specialMenuID.Append(dr["SpecialID"]);
                        specialMenuID.Append("|");
                        specialMenuID.Append(dr["SpecialMenuID"]);
                        specialMenuID.Append(",");
                        specialMenuTitle.Append(dr["SpecialName"]);
                        specialMenuTitle.Append(">>");
                        specialMenuTitle.Append(dr["SpecialMenuName"]);
                        specialMenuTitle.Append(",");
                    }

                    specialMenuTitle.Remove(specialMenuTitle.Length - 1, 1);
                    specialMenuID.Remove(specialMenuID.Length - 1, 1);
                }
            }

            if (hsInitField != null)
            {
                hsInitField.Add("SpecialID", specialMenuID);
                hsInitField.Add("SpecialName", specialMenuTitle);
            }
        }
        #endregion

        #region 添加到其它节点
        // infoID 当前更新的记录ID
        private void AppendTolNode(string infoID)
        {
            string addSiteID;           // 要添加信息的站点ID,多个用逗号隔开
            string addNodeID;           // 站点对应的节点ID,多个用逗号隔开
            string id;                  // 记录ID,多个用逗号隔开
            string orders;              // 记录排序，多个用逗号隔开
            int nodeCount;              // 要添加的记录条数
            int maxOrders;              // 保存最大排序数
            string currentID;           // 保存当前要当加的记录的ID

            addSiteID = string.Empty;
            addNodeID = string.Empty;
            id = string.Empty;
            orders = string.Empty;
            nodeCount = 0;
            maxOrders = Utils.ParseInt(Public.GetTableID("0", this.tableName)[1], 0);

            foreach (string key in HttpContext.Current.Request.Form.Keys)
            {
                if (key.Contains("hdnOtherNode_"))
                {
                    string otherNodeValue;
                    string[] otherNode;

                    otherNodeValue = HttpContext.Current.Request.Form[key];
                    otherNode = otherNodeValue.Split(new char[] { '_' });
                    currentID = Public.GetTableID("0", maxOrders);

                    if (!string.IsNullOrEmpty(otherNodeValue) && otherNode.Length > 1 && !string.IsNullOrEmpty(otherNode[0]))
                    {
                        if (string.Equals(this.nodeCode, otherNode[1]))  // 如果是当前节点
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(addSiteID))
                        {
                            addSiteID = otherNode[0];
                            addNodeID = otherNode[1];
                            id = currentID;
                            orders = maxOrders.ToString();
                        }
                        else
                        {
                            addSiteID += "," + otherNode[0];
                            addNodeID += "," + otherNode[1];
                            id += "," + currentID;
                            orders += "," + maxOrders.ToString();
                        }

                        maxOrders++;
                        nodeCount++;
                    }
                }
            }

            dal.AppendToNode(this.tableName, addSiteID, addNodeID, id, orders, nodeCount, infoID);
        }
        #endregion

        #region 加载当前节点信息
        // 当前节点代码
        private void LoadNode(ref Hashtable hsInitField)
        {
            DataTable nodeDT;       // 当前节点相关（父节点及当前节点）的记录
            string nodeName;        // 根节点至当前节点的节点名导航
            DataRow nodeDR;         // 根节点

            nodeName = null;

            if (hsInitField.ContainsKey("NodeCode"))     // 修改
            {
                nodeDT = dal.GetNodeInfoByNodeCode(hsInitField["NodeCode"].ToString());
            }
            else   // 添加
            {
                nodeDT = dal.GetNodeInfoByNodeCode(this.nodeCode);
                hsInitField.Add("NodeCode", this.nodeCode);
            }

            try
            {
                nodeDR = nodeDT.Select("ParentNode=0")[0];
            }
            catch
            {
                nodeDR = null;
            }

            while (nodeDR != null)
            {
                if (string.IsNullOrEmpty(nodeName))
                {
                    nodeName = nodeDR["NodeName"].ToString();
                }
                else
                {
                    nodeName = nodeName + " >> " + nodeDR["NodeName"].ToString();
                }

                try
                {
                    nodeDR = nodeDT.Select("ParentNode='" + nodeDR["NodeCode"].ToString() + "'")[0];
                }
                catch
                {
                    nodeDR = null;
                }
            }

            hsInitField.Add("NodeName", nodeName);      // 加入节点名导航

            if (!hsInitField.ContainsKey("SiteID"))     // 添加站点ID
            {
                hsInitField.Add("SiteID", this.siteID);
            }
        }

        #endregion

        #region 设置传递URL参数,即返回时要带的参数
        private void SetKeepUrlParam()
        {
            foreach (string key in HttpContext.Current.Request.QueryString.Keys)
            {
                if (!string.IsNullOrEmpty(key) && !filterUrlParam.ToLower().Contains("," + key.ToLower() + ","))
                {
                    if (string.IsNullOrEmpty(this._keepUrlParam))
                    {
                        this._keepUrlParam = key + "=" + HttpContext.Current.Request.QueryString[key];
                    }
                    else
                    {
                        this._keepUrlParam += "&" + key + "=" + HttpContext.Current.Request.QueryString[key];
                    }
                }
            }
        }
        #endregion

        #region 用URL参数为字段赋值
        private void SetFieldFromUrlParamValue(ref Hashtable hsEditField)
        {
            string[] arrField;    // 通过URL参数赋值的字段名

            if (!string.IsNullOrEmpty(this._fieldFromUrlParamValue))
            {
                arrField = this._fieldFromUrlParamValue.Split(new char[] { ',' });

                foreach (string filedName in arrField)
                {
                    if (!string.IsNullOrEmpty(filedName.Trim()))
                    {
                        hsEditField.Add(filedName, HttpContext.Current.Request.QueryString[filedName.Trim()]);
                    }
                }
            }
        }
        #endregion

        #region  确定节点是否启用了流程审核
        private bool NodeFlowCheckEnsure()
        {
            string reviewFlowID;
            bool isResult;

            if (string.IsNullOrEmpty(this.nodeCode))
            {
                isResult = false;
            }
            else
            {
                reviewFlowID = dal.GetNodeReviewFlow(this.siteID, this.nodeCode);

                if (string.IsNullOrEmpty(reviewFlowID))
                {
                    isResult = false;
                }
                else
                {
                    isResult = true;
                }
            }

            return isResult;
        }
        #endregion

        #region 获取返回列表页时需传递的参数
        /// <summary>
        /// 获取返回列表页时需传递的参数
        /// </summary>
        /// <returns></returns>
        public string GetBackUrlParam()
        {
            StringBuilder sbBackUrlParam;
            string paramValue;

            sbBackUrlParam = new StringBuilder();

            foreach (string key in HttpContext.Current.Request.QueryString.Keys)
            {
                if (key != null && key.ToLower() != "id" && key.ToLower() != "action")
                {
                    paramValue = HttpContext.Current.Request.QueryString[key];

                    if (paramValue.Contains(",") && !Regex.IsMatch(paramValue, @"\{[^}]+\}"))
                    {
                        paramValue = paramValue.Split(new char[] { ',' })[0];
                    }

                    if (sbBackUrlParam.Length > 0)
                    {
                        sbBackUrlParam.Append("&" + key + "=" + paramValue);
                    }
                    else
                    {
                        sbBackUrlParam.Append(key + "=" + paramValue);
                    }
                }
            }

            return sbBackUrlParam.ToString();
        }
        #endregion

        #region 生成HTML
        public string[] CreateHtml(string idStr, string siteDir, string uploadImgDir, string uploadFileUrl, string uploadMediaUrl, string siteURL)
        {
            Publish publish;            // HTML发布处理
            PublishParam publishParam;  // 发布参数配置
            List<string> lstMenu;
            string[] arrLog;

            arrLog = null;

            if (!string.IsNullOrEmpty(idStr.Trim()))
            {
                publish = new Publish(this.SiteID, siteDir, siteURL);
                publish.UploadImgUrl = uploadImgDir;
                publish.FilesUrl = uploadFileUrl;
                publish.MediasUrl = uploadMediaUrl;
                publishParam = new PublishParam();
                lstMenu = new List<string>();
                arrLog = new string[2];

                publish.IsDisplayProgress = false;         // 不显示发布进度条
                publishParam.IsSiteIndex = true;           // 站点首页
                publishParam.IsMenuIndex = true;           // 栏目首页
                publishParam.IsMenuList = true;            // 栏目列表
                publishParam.IsContent = true;             // 内容页
                publishParam.UnPublished = false;          // 只生成未生成页面
                lstMenu.Add(this.NodeCode);

                publishParam.Type = PublishType.ContentIDEnum;
                publishParam.PublishTypeParam = new string[] { idStr };             //内容ID 多个ID可由 , 分隔
                publishParam.LstMenu = lstMenu;

                try
                {
                    publish.Execute(publishParam);      // 执行发布
                    arrLog[0] = "内容记录 " + HttpContext.Current.Request.Form["Title"] + " 发布HTML 成功。";
                    arrLog[1] = string.Empty;
                }
                catch (Exception ex)
                {
                    arrLog[0] = "内容记录 " + HttpContext.Current.Request.Form["Title"] + " 发布HTML 失败。";
                    arrLog[1] = ex.Message;
                }
            }

            return arrLog;
        }
        #endregion

        #region 保存子模型内容
        public Dictionary<string, string> GetSubModelContent()
        {
            Dictionary<string, string> dicSubModel;
            string[] fieldName;
            string[] tableID;

            dicSubModel = new Dictionary<string, string>();

            foreach (string key in HttpContext.Current.Request.Form.Keys)
            {
                if (key.Contains("___"))
                {
                    fieldName = key.Split(new string[] { "___" }, StringSplitOptions.RemoveEmptyEntries);

                    if (fieldName.Length > 1 && fieldName[1] != null && fieldName[1].Trim() != "")
                    {
                        if (!dicSubModel.ContainsKey("HQB_TableName"))
                        {
                            dicSubModel.Add("HQB_TableName", fieldName[0]);
                        }

                        if (HttpContext.Current.Request.Form[key] != null)
                        {
                            dicSubModel.Add(fieldName[1], HttpContext.Current.Request.Form[key]);
                        }
                    }
                }
            }

            if (dicSubModel.Count > 1)
            {
                tableID = Public.GetTableID("0", dicSubModel["HQB_TableName"]);

                if (tableID != null && tableID.Length > 1)
                {
                    dicSubModel.Add("ID", tableID[0]);
                    dicSubModel.Add("Orders", tableID[1]);
                    dicSubModel.Add("NodeCode", this.nodeCode);
                    dicSubModel.Add("ReferenceID", tableID[0]);
                    dicSubModel.Add("AddMan", this._userName);
                    dicSubModel.Add("SiteID", this.siteID.ToString());
                }
            }

            return dicSubModel;
        }
        #endregion
    }
}
