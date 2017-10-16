#region 程序集引用using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using KingTop.IDAL.Content;
using KingTop.Common;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-26
// 功能描述：处理解析后的模型列表 -- 初始化相关处理// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    /// <summary>
    /// 处理解析后的模型列表
    /// </summary>
    public partial class ControlManageList
    {
        #region 私有变量成员
        private string _logContent;
        private string _logException;
        private static string filterUrlParam = ",IsArchiving,IsDel,Action,"; // 要过滤的URL参数名        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Content.IControlManageList dal = (IDAL.Content.IControlManageList)Assembly.Load(path).CreateInstance(path + ".Content.ControlManageList");
        private string tableName;                  // 表名
        private string action;                     // 操作参数
        private string urlParam;                   // URL参数
        private string stepID;                     // 传入的步骤ID参数
        private string modelID;                    // 当前模型ID
        private string _userNo;                    // 当前帐号
        private string _nodeCode;                  // 节点代码
        private int _siteID;                       // 站点ID
        private int _accountID;                    // 用户帐号
        private string _isDel;                     // 逻辑删除标志
        private string _isArchiving;               // 归档标志
        private string _sqlField;                  // SQL选取字段
        private string _sqlWhere;                  // SQL选取条件
        private string _sqlOrder;                  // SQL排序
        private string _sqlFieldForignData;        // 基本字段中引用外表数据的字段参数
        private DataSet dsFlowStepState;           // 当前用户对于当前节点可操作流程步骤及状态        private Hashtable hsFlowState;             // 流程的所有状态        private bool _isAllowFlow;                 // 当前节点是否启用流程审核
        private string _deliverAndSearchUrlParam;  // 需传递且参与查询的参数字段        private string _ip;                        // IP地址
        private string  _notSearchField;          // 不允许搜索的字段
        private System.Collections.Specialized.NameValueCollection _keepParamValue = null;  // 需要传递的URL值        #endregion

        #region 属性
        /// <summary>
        /// 操作日志内容
        /// </summary>
        public string LogContent
        {
            set { this._logContent = value; }
            get { return this._logContent; }
        }

        /// <summary>
        /// 操作日志异常信息
        /// </summary>
        public string LogException
        {
            set { this._logException = value; }
            get { return this._logException; }        }        /// <summary>
        /// 不允许搜索的字段
        /// </summary>
        public string NotSearchField
        {
            get{return this._notSearchField;}
            set { this._notSearchField = value; }
        }

        /// <summary>
        /// 需要传递的URL值        /// </summary>
        public System.Collections.Specialized.NameValueCollection KeepParamValue
        {
            get
            {
                if (_keepParamValue == null)
                {
                    this._keepParamValue = HttpContext.Current.Request.QueryString;
                }
                return this._keepParamValue;
            }
        }

        /// <summary>
        /// 需传递且参与查询的参数字段
        /// </summary>
        public string DeliverAndSearchUrlParam
        {
            set { this._deliverAndSearchUrlParam = value; }
            get { return this._deliverAndSearchUrlParam; }
        }

        /// <summary>
        /// 当前节点是否启用流程审核
        /// </summary>
        public bool IsAllowFlow
        {
            set { this._isAllowFlow = value; }
            get { return this._isAllowFlow; }
        }

        /// <summary>
        /// SQL选取字段
        /// </summary>
        public string SqlField
        {
            set { this._sqlField = value; }
            get { return this._sqlField; }
        }

        /// <summary>
        /// SQL选取条件
        /// </summary>
        public string SqlWhere
        {
            set { this._sqlWhere = value; }
            get { return this._sqlWhere; }
        }

        /// <summary>
        /// SQL排序
        /// </summary>
        public string SqlOrder
        {
            set { this._sqlOrder = value; }
            get { return this._sqlOrder; }
        }

        /// <summary>
        /// 基本字段中引用外表数据的字段参数
        /// </summary>
        public string SqlFieldForignData
        {
            set { this._sqlFieldForignData = value; }
            get { return this._sqlFieldForignData; }
        }

        /// <summary>
        /// 节点代码
        /// </summary>
        public string NodeCode
        {
            set { this._nodeCode = value; }
            get { return this._nodeCode; }
        }

        /// <summary>
        /// 站点ID
        /// </summary>
        public int SiteID
        {
            set { this._siteID = value; }
            get { return this._siteID; }
        }

        /// <summary>
        /// 用户帐号ID
        /// </summary>
        public int AccountID
        {
            set { this._accountID = value; }
        }

        /// <summary>
        /// 用户帐号
        /// </summary>
        public string UserNo
        {
            set { this._userNo = value; }
            get { return this._userNo; }
        }

        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        public string IsDel
        {
            get { return this._isDel; }
        }

        /// <summary>
        /// 归档标志
        /// </summary>
        public string IsArchiving
        {
            get { return this._isArchiving; }
        }

        /// <summary>
        /// 设置IP地址
        /// </summary>
        public string IP
        {
            set { this._ip = value; }
        }
        #endregion

        #region 初始函数
        public ControlManageList(string modelID, string tbName)
        {
            this.urlParam = Utils.ReqUrlParameter("action");
            this.NotSearchField = "";

            if (!string.IsNullOrEmpty(Utils.ReqFromParameter("action")))
            {
                this.action = Utils.ReqFromParameter("action");
            }
            else
            {
                this.action = this.urlParam;
            }

            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["IsDel"])) // 逻辑删除
            {
                this._isDel = "0";
            }
            else
            {
                this._isDel = HttpContext.Current.Request.QueryString["IsDel"];
            }

            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["IsArchiving"])) // 归档
            {
                this._isArchiving = "0";
            }
            else
            {
                this._isArchiving = HttpContext.Current.Request.QueryString["IsArchiving"];
            }

            this.modelID = modelID;
            this.stepID = HttpContext.Current.Request.QueryString["StepID"];
            this.tableName = tbName;
            this.hsFlowState = null;
        }
        #endregion

        #region  确定节点是否启用了流程审核        private bool NodeFlowCheckEnsure()
        {
            string reviewFlowID;
            bool isResult;

            if (string.IsNullOrEmpty(this._nodeCode))
            {
                isResult = false;
            }
            else
            {
                reviewFlowID = dal.GetNodeReviewFlow(this._siteID, this._nodeCode);

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

        #region 获取客户端排序        /// <summary>
        /// 获取客户端排序 
        /// </summary>
        /// <returns></returns>
        public static string GetClientSort()
        {
            string clientTitleSort;                                   // 字段中定义的排序,即列表标题排序            clientTitleSort = Utils.ReqUrlParameter("Sort");          // 获取客户端排序类型            return clientTitleSort;
        }
        #endregion

        #region 初始时的各SQL参数如表名等
        /// <summary>
        ///  string[0] 要显示的表名列表以,隔开  string[1] 连接条件  string[2] 选取的字段  string[3] 排序
        /// </summary>
        /// <param name="hdnCustomColValue">string[3]</param>
        /// <returns></returns>
        public string[] GetSQLParam(string hdnCustomColValue, string showCol)
        {
            string[] customCol;              // 自定义列表列及            string[] sqlJoin;                // sqlJoin[0] 要显示的表名列表以,隔开  sqlJoin[1] 连接条件  sqlJoin[2] 选取的字段  sqlJoin[3] 排序
            string[] col;                    // col[0] 列表表头配置 col[1]  列表项配置            Regex reg;
            MatchCollection matchText;      // 自定义列显示文本
            MatchCollection matchLink;      // 自定列链接            MatchCollection joinCollecton;  // 连接条件

            sqlJoin = new string[4];
            customCol = hdnCustomColValue.Split(new[] { ',' });
            sqlJoin[0] = this.tableName;
            sqlJoin[1] = "";
            sqlJoin[2] = showCol;
            sqlJoin[3] = " " + this.tableName + ".Orders desc,"+ this.tableName +".ID desc ";
            //sqlJoin[3] = " " + this.tableName + ".updatedate desc," + this.tableName + ".ID desc ";  //投关项目初始按照日期排序

            foreach (string strItem in customCol)
            {
                col = strItem.Split(new char[] { '|' });

                if (col.Length > 1)
                {
                    reg = new Regex(@"(\[(?<1>[0-9 a-z A-Z _]+\.[0-9 a-z A-Z _]+)\])"); // 匹配外表
                    string[] link;                                                      // link[0] 链接文本  link[1] 链接  link[3] 连接条件

                    link = col[1].Split(new char[] { '$' });
                    matchText = reg.Matches(link[0]);
                    matchLink = reg.Matches(col[1]);

                    if (matchText.Count > 0)
                    {
                        foreach (Match m in matchText)
                        {
                            string fildName, tbName;

                            fildName = m.Groups[1].Value.Replace("[", "").Replace("]", "");
                            fildName = fildName + " as " + fildName.Replace(".", "_");
                            tbName = fildName.Split(new char[] { '.' })[0];

                            if (!sqlJoin[0].Contains(tbName))
                            {
                                sqlJoin[0] += "," + tbName;         // 添加引用表
                            }

                            if (!sqlJoin[2].Contains(fildName))
                            {
                                sqlJoin[2] += "," + "["+fildName + "]";      // 选取的外表字段
                            }
                        }
                    }

                    if (matchLink.Count > 0)
                    {
                        foreach (Match m in matchLink)
                        {
                            string fildName, tbName;

                            fildName = m.Groups[1].Value.Replace("[", "").Replace("]", "");
                            fildName = fildName + " as " + fildName.Replace(".", "_");
                            tbName = fildName.Split(new char[] { '.' })[0];

                            if (!sqlJoin[0].Contains(tbName))
                            {
                                sqlJoin[0] += "," + tbName;       // 添加引用表
                            }

                            if (!sqlJoin[2].Contains(fildName))
                            {
                                sqlJoin[2] += "," + fildName;    // 选取的外表字段
                            }
                        }
                    }

                    if (col[1].Split(new char[] { '$' }).Length > 2)
                    {
                        reg = new Regex(@"\{(?<1>(.+)|(.+\..+)[=](.+)|(.+\..+));?\}"); // 匹配连接条件
                        joinCollecton = reg.Matches(col[1].Split(new char[] { '$' })[2]);

                        foreach (Match m in joinCollecton)
                        {
                            sqlJoin[1] += " and " + m.Groups[1].Value + " ";
                        }
                    }
                }
            }

            if (sqlJoin[1] != "")
            {
                sqlJoin[1] = sqlJoin[1].Substring(4, sqlJoin[1].Length - 4);
            }

            return sqlJoin;
        }
        #endregion

        #region 分页
        public void PageData(KingTop.Model.Pager pager, string tbName)
        {
            AddSpecialSqlWhere();                                       // 添加特殊条件

            if (!string.IsNullOrEmpty(this._deliverAndSearchUrlParam))  // 添加传递的URL参数条件
            {
                dal.GetKeepUrlParam(ref this._sqlWhere, this.tableName, this._deliverAndSearchUrlParam, filterUrlParam, HttpContext.Current.Request.QueryString);
            }

            dal.PageData(pager, tbName, this._sqlField, this._sqlWhere, this._sqlOrder, this._sqlFieldForignData);
        }
        #endregion

        #region 分页前添加特定的SqlWhere条件
        private void AddSpecialSqlWhere()
        {
            if (!string.IsNullOrEmpty(this._nodeCode) && !NotSearchField.Contains(",nodecode,"))
            {
                // 节点条件
                if (string.IsNullOrEmpty(this._sqlWhere.Trim()) || this._sqlWhere.Trim() == "")
                {
                    this._sqlWhere = " " + this.tableName + ".NodeCode='" + this._nodeCode + "' ";
                }
                else
                {
                    this._sqlWhere += " and " + this.tableName + ".NodeCode='" + this._nodeCode + "' ";
                }
            }

            if (string.IsNullOrEmpty(this._sqlWhere))
            {
                if (string.Equals(this._isArchiving, "1"))  // 归档内容
                {
                    if (!NotSearchField.Contains(",siteid,"))
                    {
                        this._sqlWhere = this.tableName + ".SiteID=" + this._siteID;

                        if (!NotSearchField.Contains(",isArchiving,"))
                        {
                            this._sqlWhere += "  and " + this.tableName + ".IsArchiving=1";
                        }
                    }
                    else
                    {
                         if (!NotSearchField.Contains(",isArchiving,"))
                        {
                            this._sqlWhere =  this.tableName + ".IsArchiving=1";
                        }
                    }
                }
                else if (string.Equals(this._isDel, "1"))  // 回收站内容                {
                    if (!NotSearchField.Contains(",siteid,"))
                    {
                        this._sqlWhere = this.tableName + ".SiteID=" + this._siteID;

                        if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere += " and " + this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=1";
                        }
                    }
                    else
                    {
                         if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere =  this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=1";
                        }
                    }
                }
                else
                {
                    if (!NotSearchField.Contains(",siteid,"))
                    {
                        this._sqlWhere = this.tableName + ".SiteID=" + this._siteID;

                        if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere += " and " + this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=0";
                        }
                    }
                    else
                    {
                        if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere = this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=0";
                        }
                    }
                }
            }
            else
            {
                if (string.Equals(this._isArchiving, "1"))  // 归档内容
                {
                    if (!NotSearchField.Contains(",siteid,"))
                    {
                        this._sqlWhere += " and " + this.tableName + ".SiteID=" + this._siteID;

                        if (!NotSearchField.Contains(",isArchiving,"))
                        {
                            this._sqlWhere += "  and " + this.tableName + ".IsArchiving=1";
                        }
                    }
                    else
                    {
                        if (!NotSearchField.Contains(",isArchiving,"))
                        {
                            this._sqlWhere += " and " + this.tableName + ".IsArchiving=1";
                        }
                    }
                }
                else if (string.Equals(this._isDel, "1"))  // 回收站内容                {
                    if (!NotSearchField.Contains(",siteid,"))
                    {
                        this._sqlWhere += " and " + this.tableName + ".SiteID=" + this._siteID;

                        if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere += " and " + this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=1";
                        }
                    }
                    else
                    {
                        if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere += " and " + this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=1";
                        }
                    }
                }
                else
                {
                    if (!NotSearchField.Contains(",siteid,"))
                    {
                        this._sqlWhere += " and " +  this.tableName + ".SiteID=" + this._siteID;

                        if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere += " and " + this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=0";
                        }
                    }
                    else
                    {
                        if (!NotSearchField.Contains(",isdel,"))
                        {
                            this._sqlWhere += " and " + this.tableName + ".IsArchiving=0 and " + this.tableName + ".IsDel=0";
                        }
                    }
                }
            }
        }
        #endregion

        #region  解析模型基本字段中 单选、多选、列表的文本输入方式
        /// <summary>
        /// 解析模型基本字段中 单选、多选、列表的文本输入方式
        /// </summary>
        /// <param name="id">要解析的基本字段ID</param>
        /// <param name="value">字段值</param>
        /// <returns></returns>
        public string ParseFieldValueToText(string id, object value)
        {
            string text;
            string cacheName;

            cacheName = "HQB_CONTENT_MODEL_MANAGELIST" + id;    // 保存当前字段值的Cache名称
            text = null;

            if (!string.IsNullOrEmpty(value.ToString()))        // 当前字段值为空时忽略
            {
                if (HttpContext.Current.Cache[cacheName] == null)
                {
                    string optionsValue;
                    string[] optionItem;
                    string[] itemValue;
                    Hashtable hsItem;

                    hsItem = new Hashtable();
                    optionsValue = dal.GetFieldValue("K_ModelField", "OptionsValue", id);
                    
                    if (!string.IsNullOrEmpty(optionsValue))    // 字段配置表中没有设置选项数据，当前字段值为非法数据
                    {
                        optionItem = optionsValue.Split(new char[] { ',' });

                        foreach (string item in optionItem)
                        {
                            itemValue = item.Split(new char[] { '|' });

                            if (itemValue.Length > 1)
                            {
                                hsItem.Add(itemValue[1], itemValue[0]);
                            }
                        }

                        HttpContext.Current.Cache[cacheName] = hsItem;
                    }
                }

                try
                {
                    if (value.ToString().Contains(","))
                    {
                        string[] arrValue;

                        arrValue = value.ToString().Split(new char[] { ',' });

                        foreach (string item in arrValue)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                text = text + ((Hashtable)HttpContext.Current.Cache[cacheName])[item].ToString() + ",";
                            }
                        }
                        if (!string.IsNullOrEmpty(text))
                        {
                            text = text.Remove(text.Length - 1, 1);
                        }
                    }
                    else
                    {
                        text = ((Hashtable)HttpContext.Current.Cache[cacheName])[value.ToString()].ToString();
                    }
                }
                catch
                {
                    text = "";
                }
            }

            return text;
        }
        #endregion

        #region  绑定可操作流程步骤        /// <summary>
        /// 绑定用户可操作流程步骤或状态        /// </summary>
        /// <param name="rptFlowStep">流程步骤</param>
        /// <param name="rptFlowState">流程状态</param>
        public void BindEnabledFlowStep(Repeater rptFlowStep, Repeater rptFlowState)
        {
            this._isAllowFlow = NodeFlowCheckEnsure();   // 确定当前节点是否启用了审核流程
            if (this._isAllowFlow)      // 启用流程审核
            {
                if (this.dsFlowStepState == null)
                {
                    if (string.IsNullOrEmpty(this.stepID))
                    {
                        this.dsFlowStepState = dal.GetEnabledFlowStep(this._accountID, this._siteID, this._nodeCode, null);
                    }
                    else
                    {
                        this.dsFlowStepState = dal.GetEnabledFlowStep(this._accountID, this._siteID, this._nodeCode, this.stepID);
                    }
                }

                if (this.dsFlowStepState != null)
                {
                    if (this.dsFlowStepState.Tables.Count > 0 && this.dsFlowStepState.Tables[0] != null) // 有流程步骤                    {
                        if (this.dsFlowStepState.Tables[0].Rows.Count > 1)      // 多级
                        {
                            rptFlowStep.DataSource = this.dsFlowStepState.Tables[0];     // 流程步骤绑定
                            rptFlowStep.DataBind();
                        }
                        else if (this.dsFlowStepState.Tables[0].Rows.Count > 0) // 一级                        {
                            if (!string.Equals(this._isDel, "1") && !string.Equals(this._isArchiving, "1")) // 记录使用状态正常                            {
                                PlaceHolder plLinkButton;      // 存放列表按钮的服务端容器控件
                                PlaceHolder plDLinkButton;     // 动态添加列表按钮服务器容器控件

                                plLinkButton = rptFlowStep.Page.FindControl("plParseModelLinkButton") as PlaceHolder;
                                plDLinkButton = rptFlowStep.Page.FindControl("plDParseModelLinkButton") as PlaceHolder;

                                if (plLinkButton.FindControl("btnPastFlowCheck") == null && plLinkButton.FindControl("btnCheck") == null)   // 不存在审核通过的控件                                {
                                    Button pastFlow;        // 通过审核按钮

                                    pastFlow = new Button();
                                    pastFlow.ID = "btnCheck";
                                    pastFlow.Text = "通过审核";
                                    pastFlow.Attributes.Add("onclick", "return setAction('HQB_PastFlowCheck');");
                                    plDLinkButton.Controls.Add(pastFlow);
                                }

                                if (plLinkButton.FindControl("btnCancelFlowCheck") == null && plLinkButton.FindControl("btnCancelCheck") == null) // 不存在取消审核的控件
                                {
                                    Button cancelFlow;      // 取消审核按钮

                                    cancelFlow = new Button();
                                    cancelFlow.ID = "btnCancelCheck";
                                    cancelFlow.Text = "取消审核";
                                    cancelFlow.Attributes.Add("onclick", "return setAction('HQB_CancelFlowCheck');");
                                    plDLinkButton.Controls.Add(cancelFlow);
                                }
                            }
                        }
                    }

                    if (this.dsFlowStepState.Tables.Count > 1 && this.dsFlowStepState.Tables[1] != null && this.dsFlowStepState.Tables[1].Rows.Count > 2) // 只有两种状态的不显示                    {
                        rptFlowState.DataSource = this.dsFlowStepState.Tables[1];    // 流程状态绑定                        rptFlowState.DataBind();
                    }
                }
            }
        }
        #endregion

        #region 解析流程状态        public object ParseFlowState(object stateValue)
        {
            if (this.hsFlowState == null)
            {
                DataTable dtFlowState;   // 获取所有流程状态值
                this.hsFlowState = new Hashtable();
                dtFlowState = dal.GetFlowState();

                foreach (DataRow dr in dtFlowState.Rows)     // 遍历获取每个值对 
                {
                    this.hsFlowState.Add(dr["StateValue"], dr["Desc"]);
                }
            }

            return this.hsFlowState[stateValue];
        }
        #endregion

        #region 获取返回时传递的参数
        public string GetBackDeliverUrlParam(string backDeliverField)
        {
            string backDeliverUrlParam;
            string[] arrParam;

            backDeliverUrlParam = string.Empty;
            arrParam = backDeliverField.Split(new char[] { ',' });

            foreach (string param in arrParam)
            {
                if (!string.IsNullOrEmpty(param))
                {
                    if (string.IsNullOrEmpty(backDeliverUrlParam))
                    {
                        backDeliverUrlParam = param + "=" + HttpContext.Current.Request.QueryString[param];
                    }
                    else
                    {
                        backDeliverUrlParam += "&" + param + "=" + HttpContext.Current.Request.QueryString[param];
                    }
                }
            }

            return backDeliverUrlParam;

        }
        #endregion
    }
}
