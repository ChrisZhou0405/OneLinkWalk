#region 引用程序集
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using SQLDMO;
using Wuqi.Webdiyer;
using KingTop.Common;
using KingTop.BLL.Content;
using KingTop.Web.Admin;
#endregion

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线    作者:      吴岸标    创建时间： 2010年3月16日    功能描述： 内容模型列表
--===============================================================*/
#endregion

namespace KingTop.Web.Admin
{
    public partial class ModelManageList : AdminPage
    {
        #region 解析模块类型
        private Hashtable _parseModel = null;

        public Hashtable ParseModel
        {
            get
            {
                if (this._parseModel == null)
                {
                    this._parseModel = new Hashtable();

                    // 解析模板绑定
                    string configPath = Utils.GetResourcesValue("Model", "ParseModePath");
                    DataTable dt = Utils.GetXmlDataSet(configPath).Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        this._parseModel.Add(dr["ID"], dr["Name"]);
                    }
                }

                return this._parseModel;
            }
        }
        #endregion

        #region 变量成员
        private string _searchType = null;
        private string _searchValue = null;
        private string _isDel = null;
        private string _isSub = string.Empty;
        protected string jsMessage = string.Empty;    // 提示信息
        private string fileTitle = string.Empty;
        #endregion

        #region 属性
        // 搜索类型
        public string SearchType
        {
            get
            {
                if (this._searchType == null)
                {
                    this._searchType = Utils.ReqUrlParameter("SearchType");
                }

                return this._searchType;
            }
        }

        // 搜索值
        public string SearchValue
        {
            get
            {
                if (this._searchValue == null)
                {
                    this._searchValue = Utils.ReqUrlParameter("SearchValue");
                }

                return this._searchValue;
            }
        }

        // 逻辑删除
        public string IsDel
        {
            get
            {
                if (this._isDel == null)
                {
                    this._isDel = Utils.ReqUrlParameter("IsDel");
                }

                return this._isDel;
            }
        }

        /// <summary>
        /// 是否显示子模型
        /// </summary>
        public string IsSub
        {
            get
            {
                if (string.IsNullOrEmpty(this._isSub))
                {
                    this._isSub = Request.QueryString["IsSub"];

                    if (this._isSub == null || this._isSub.Trim() == "")
                    {
                        this._isSub = "0";
                    }
                }

                return this._isSub;
            }
        }
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            PageInit();
            SetRight(this.Page, rptModelManage);
            Utils.SetVisiteList(SystemConst.COOKIES_PAGE_KEY, Session.SessionID, Utils.GetUrlParams().Replace("&", "|"), SystemConst.intMaxCookiePageCount);
        }
        
        private void PageInit()
        {
            Dictionary<string, string> dicWhere;

            hdnNodeCode.Value = NodeCode;
            dicWhere = GetSqlWhere();
            SplitDataBind(dicWhere);
        }
        #endregion

        #region 删除单条模板记录
        public void ModelManage_Del(object sender, CommandEventArgs e)
        {
            string id;
            string tranType;    // 事务类型
            string returnMsg;   // 事务返回信息
            KingTop.BLL.Content.ModelManage model;
            bool isValidate;

            isValidate = IsHaveRightByOperCode("Delete");

            if (isValidate)
            {
                if (IsPostBack)
                {
                    id = e.CommandArgument.ToString();

                    if (e.CommandName == "del")
                    {
                        tranType = "DELONE";
                    }
                    else if (string.Equals(e.CommandName, "deldp"))
                    {
                        string tableName;

                        tranType = "DELDP";
                        tableName = ((Button)sender).ToolTip;
                        tableName = tableName.Replace("K_U_", "");
                        File.Delete(Server.MapPath(tableName + "list.aspx"));       // 删除生成的列表文件
                        File.Delete(Server.MapPath(tableName + "edit.aspx"));       // 删除生成的编辑文件
                        File.Delete(Server.MapPath(tableName + "view.aspx"));       // 删除生成的编辑文件
                    }
                    else if (string.Equals(e.CommandName, "revert"))
                    {
                        tranType = "REVERT";
                    }
                    else
                    {
                        tranType = "DELONE";
                    }

                    returnMsg = "";
                    model = new KingTop.BLL.Content.ModelManage();
                    returnMsg = model.ModelManageSet(tranType, null, id);

                    if (returnMsg == "1")
                    {
                        WriteLog("删除模型 " + LogTitle + " 至回收站成功", string.Empty, 2);
                    }
                    else
                    {
                        WriteLog("删除模型 " + LogTitle + " 至回收站失败", returnMsg, 3);
                    }

                    PageInit();
                }
            }
            else
            {
                jsMessage += "alertClose({msg:\"对不起，您没有删除模型的操作权限，请与管理员联系！\",title:\"操作提示\"});";
            }
        }
        #endregion

        #region 更新
        protected void SetIsEnable(object sender, CommandEventArgs e)
        {
            KingTop.BLL.Content.ModelManage model;
            string tranType;
            string returnMsg;
            string[] arrArg;        // arrArg[0]  记录ID  arrArg[1] 记录值
            int isEnable;
            bool isValidate;
            string opName;          // 操作名称，用于记录日志

            isValidate = IsHaveRightByOperCode("Edit");

            if (isValidate)
            {
                model = new KingTop.BLL.Content.ModelManage();
                arrArg = e.CommandArgument.ToString().Split(new char[] { '|' });
                tranType = e.CommandName;
                isEnable = 0;

                switch (e.CommandName)
                {
                    case "STATE":
                        opName = "状态";
                        break;
                    case "HTML":
                        opName = "是否生成HTML";
                        break;
                    default:
                        opName = string.Empty;
                        break;
                }

                if (arrArg.Length > 1)
                {
                    if (arrArg[1] == "False" || arrArg[1] == "0")
                    {
                        isEnable = 1;
                    }

                    returnMsg = model.ModelManageSet(tranType, isEnable.ToString(), arrArg[0]);

                    if (returnMsg == "1")
                    {
                        WriteLog("设置模型 " + LogTitle + opName + arrArg[1].ToString() + " => " + isEnable.ToString() + "成功", null, 2);
                    }
                    else
                    {
                        WriteLog("设置模型 " + LogTitle + opName + arrArg[1].ToString() + " => " + isEnable.ToString() + "失败", returnMsg, 3);
                    }

                    PageInit();
                }
            }
            else
            {
                jsMessage += "alert({msg:\"对不起，您没有修改模型属性的操作权限，请与管理员联系！\",title:\"操作提示\"});";
            }
        }
        #endregion

        #region 搜索
        // 获取搜索条件
        private Dictionary<string, string> GetSqlWhere()
        {
            Dictionary<string, string> dicWhere = new Dictionary<string, string>();

            if (SearchType != "" && SearchValue != "")
            {
                dicWhere.Add(SearchType, SearchValue);
            }
            if (IsDel == "")
            {
                dicWhere.Add("IsDel", "0");
            }
            else
            {
                dicWhere.Add("IsDel", IsDel);
            }

            if (IsSub == "")
            {
                dicWhere.Add("IsSub", "0");
            }
            else
            {
                dicWhere.Add("IsSub", IsSub);
            }

            return dicWhere;
        }
        /// <summary>
        /// 按条件搜索
        /// </summary>
        /// <param Name="sender"></param>
        /// <param Name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder redirectUrl;

            redirectUrl = new StringBuilder();
            redirectUrl.Append("ModelManageList.aspx?IsSub="+ IsSub +"&NodeCode=" + NodeCode);
            redirectUrl.Append("&SearchType=");
            redirectUrl.Append(ddlSearchType.SelectedValue);
            redirectUrl.Append("&SearchValue=");
            redirectUrl.Append(Utils.cutBadStr(txtSearch.Text.Trim()));

            Utils.UrlRedirect(redirectUrl.ToString());
        }
        #endregion

        #region 分页
        // 分页控件数据绑定
        private void SplitDataBind(Dictionary<string, string> dicWhere)
        {
            KingTop.Model.Pager p = new KingTop.Model.Pager();
            KingTop.BLL.Content.ModelManage objsource = new KingTop.BLL.Content.ModelManage();

            p.Aspnetpage = Split;
            p.RptControls = rptModelManage;
            p.DicWhere = dicWhere;
            objsource.PageData(p);
        }
        #endregion

        #region 创建模型
        protected void CreateModel(object sender, CommandEventArgs e)
        {
            string strModelId;      // 模型ID
            string browseUrl;       // 浏览模型地址
            bool isValidate;

            isValidate = IsHaveRightByOperCode("CreateModel");

            if (isValidate)
            {
                strModelId = e.CommandArgument.ToString();

                if (Utils.ParseBool(e.CommandName))
                {
                    BLL.Content.ParseModel model = new ParseModel(strModelId, BLL.Content.ParserType.Content);
                    browseUrl = model.CutTableNamePreFix(((Button)sender).ToolTip) + "edit.aspx?Action=browse&NodeCode=" + NodeCode;

                    try
                    {
                        model.Create();
                        jsMessage = "CreateModelConfirm(\"" + browseUrl + "\", \"恭喜您，创建成功,是否浏览生成后的模型！\")";
                    }
                    catch (Exception ex)
                    {
                        jsMessage = "alert({ msg: '操作失败，请重试。', title: '操作结果' })";
                        WriteLog("生成模型 " + LogTitle + " 失败", ex.Message, 3);
                    }

                    WriteLog("生成模型 " + LogTitle + " 成功", string.Empty, 2);
                }
                else
                {
                    jsMessage = "alert({ msg: '对不起，当前模型已被禁用，如需使用请先解禁当前模型。', title: '操作提示' })";
                }
            }
            else
            {
                jsMessage += "alert({msg:\"对不起，您没有创建模型属性的操作权限，请与管理员联系！\",title:\"操作提示\"});";
            }
        }
        #endregion

        #region 创建所有模型
        protected void CreateAllModel(object sender, CommandEventArgs e)
        {
            BLL.Content.ParseModel model;
            BLL.Content.ModelManage modelManage;
            DataTable dtModel;
            string faileModelName;
            StringBuilder sbMsg;
            bool isValidate;

            isValidate = IsHaveRightByOperCode("CreateModel");
            sbMsg = new StringBuilder();

            if (isValidate)
            {
                faileModelName = string.Empty;
                modelManage = new BLL.Content.ModelManage();
                dtModel = modelManage.GetList("IDANDNAME", new Model.SelectParams());

                foreach (DataRow dr in dtModel.Rows)        // 遍历生成所有模型
                {
                    model = new ParseModel(dr["ID"].ToString(), BLL.Content.ParserType.Content);

                    try { model.Create(); }
                    catch (Exception ex)
                    {
                        if (string.IsNullOrEmpty(faileModelName))
                        {
                            faileModelName = dr["Title"].ToString();
                        }
                        else
                        {
                            faileModelName += "," + dr["Title"].ToString();
                        }

                        sbMsg.Append(ex.Message);
                    }
                }

                if (string.IsNullOrEmpty(faileModelName))
                {
                    jsMessage = "alert({ msg: '全部生成成功,禁用和删除在回收站的模型除外！', title: '操作结果' })";
                    WriteLog("全部生成成功,禁用和删除在回收站的模型除外！", null, 2);
                }
                else
                {
                    jsMessage = "alert({ msg: '对不起，以下模型生成失败 " + faileModelName + "。', title: '操作提示' })";
                    WriteLog("以下模型生成失败 " + faileModelName, sbMsg.ToString(), 3);
                }
            }
            else
            {
                jsMessage += "alert({msg:\"对不起，您没有创建模型属性的操作权限，请与管理员联系！\",title:\"操作提示\"});";
            }
        }
        #endregion
        
        #region 导出
        private string GetTablesScript(string dtName)
        {
            try
            {
                SQLDMO.SQLServer oserver = new SQLDMO.SQLServer();
                string connStr = KingTop.Common.SQLHelper.ConnectionStringLocalTransaction;
                string[] arrConn = connStr.Split(';');
                string server = string.Empty;
                string database = string.Empty;
                string login = string.Empty;
                string password = string.Empty;

                for (int i = 0; i < arrConn.Length; i++)
                {
                    string[] itemArr = arrConn[i].Split('=');
                    if (itemArr[0].ToLower() == "server")
                        server = itemArr[1];
                    else if (itemArr[0].ToLower() == "database")
                        database = itemArr[1];
                    else if (itemArr[0].ToLower() == "uid")
                        login = itemArr[1];
                    else if (itemArr[0].ToLower() == "pwd")
                        password = itemArr[1];
                }
                oserver.Connect(server, login, password);
                SQLDMO._Database mydb = oserver.Databases.Item(database, "owner");
                SQLDMO._Table mytable = mydb.Tables.Item(dtName, "dbo");
                string tableScript = mytable.Script(SQLDMO.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default, null, null, SQLDMO.SQLDMO_SCRIPT2_TYPE.SQLDMOScript2_Default);

                tableScript = tableScript.Replace("[nvarchar] (0)", "[nvarchar] (max)");
                tableScript = tableScript.Replace("[varchar] (-1)", "[varchar] (max)");
                //去掉GO
                int lastPosNum = tableScript.LastIndexOf("GO");
                if (lastPosNum > 0)
                    tableScript = tableScript.Substring(0, lastPosNum);

                oserver.DisConnect();

                return tableScript.Replace("'", "''");
            }
            catch
            {
                Response.Write("<div align=center style='padding:20px'>导出失败，原因是sqldmo.dll未注册，注册方法如下:<br><br> 打开开始，在运行中输入 regsvr32 \"C:\\Program Files\\Microsoft SQL Server\\80\\Tools\\Binn\\sqldmo.dll\" 注册sqldmo.dll。<br><br>在注册前请确认sqldmo.dll是否存在，不存在请从网上下载sqldmo.dll到相应目录，再进行注册");
                Response.Write("<br><br><a href=# onclick='history.back();'>[返回]</a></div>");
                Response.End();
                return "";

            }
        }

        private string GetFieldValue(string FieldValue, string defaultValue, string Fields)
        {
            string re = "@" + Fields+"=";
            if (string.IsNullOrEmpty(FieldValue))
            {
                re += defaultValue;
            }
            else if (FieldValue == "True" || FieldValue == "False")
            {
                re += FieldValue.ToLower();
            }
            else
            {
                re += "N'" + FieldValue.Replace("'", "''") + "'";
            }
            re += ",\r\n";
            return re;
        }
        private string[] GetTableAction(string dtName)
        {
            string[] actionArr={"","",""};
            string sqlStr = "select OperName,OperEngDesc from K_SysActionPermit where ModuleID=(select moduleid from K_SysModule where  LinkUrl ='../Model/"+dtName .Replace ("K_U_","")+"list.aspx')";
            DataTable dt = SQLHelper.GetDataSet(sqlStr);
            string actionName = string.Empty;
            string actionOper = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(actionName))
                {
                    actionName = dt.Rows[i]["OperName"].ToString().Replace ("'","''");
                    actionOper = dt.Rows[i]["OperEngDesc"].ToString().Replace("'", "''");
                }
                else
                {
                    actionName += "," + dt.Rows[i]["OperName"].ToString().Replace("'", "''");
                    actionOper += "," + dt.Rows[i]["OperEngDesc"].ToString().Replace("'", "''");
                }
            }
            actionArr[0] = actionName;
            actionArr[1] = actionOper;
            actionArr[2] = dt.Rows.Count.ToString();
            return actionArr;
        }
        private string GetModelSql(string id, out string title)
        {
            #region 存储过程执行示例
            /*
        DECLARE	@return_value int,
		@ReturnValue int

        SELECT	@ReturnValue = 0

EXEC	@return_value = [dbo].[proc_K_ModelManageSave]
		@TranType = N'NEW',
		@ID = N'100000007863852',
		@MenuNo = N'76d6c241-548b-4633-9c2c-c33f8e09c7eb',
		@SiteID = 1,
		@Title = N'测试模型',
		@TableName = N'K_U_test22',
		@ModuleID = 1,
		@SysField = N'7',
		@ListLink = N'none$',
		@ListButton = N'1,2$',
		@CustomCol = N'',
		@OperationColumn = N'1,2',
		@Memo = N'',
		@Orders = 78,
		@IsEnable = true,
		@IsDel = false,
		@IsHtml = true,
		@IsOrderEdit = true,
		@ddlSql = N'CREATE TABLE [K_U_test22] ([ID] varchar(15) NOT NULL,[IsDel] int default(0) null,[IsEnable] int default(1) null,[IsArchiving] int default(0) null,[Orders] bigint default(0) null,[AddDate] datetime default(getdate()) null,[DelTime] datetime default(getdate()) null,[SiteID] int not null,[NodeCode] varchar(50) not null,[FlowState] int null default(3) CHECK([FlowState] >= 0 AND [FlowState] <= 99),[ReferenceID] varchar(15)  NULL,[AddMan] varchar(50)  NULL,CONSTRAINT [PK_K_U_CLU_K_U_test22] PRIMARY KEY CLUSTERED ([ID] ASC));alter table K_U_test22 add  Source nvarchar(256)  null  default('''');',
		@dmlSql = N'''''',
		@operName = N'添加,删除,修改',
		@operTitle = N'New,Delete,Edit',
		@operCount = 3,
		@IsListContentClip = true,
		@OperationColumnWidth = N'''''',
		@ConfigMan = N'admin',
		@CommonField = N'c5926f2e-d778-4aa3-8a61-cfa5bc983c8c,49defabe-7431-4147-ad9b-d0b7d423110d,e3bbb9df-afff-409d-bf13-64f92382e5ae,1f69d33c-5e44-4cdc-8095-9777971232b2,b08df817-d5bb-4de1-94c8-c04e507d26ff,237ad893-d2b4-441a-b1b7-e66f4825f914,f217c6c8-f938-46bf-a2c9-6ba06df0240c,8399c733-b017-4faa-b404-20eaf53e6f42,1cac109f-e00c-4c96-a129-438ee61e14e8',
		@IsSub = false,
		@SubModelGroupID = null,
		@NotSearchField = N'null',
		@BackDeliverUrlParam = N'nodecode',
		@FieldFromUrlParamValue = N'null',
		@DeliverAndSearchUrlParam = N'null',
		@ReturnValue = @ReturnValue OUTPUT

SELECT	@ReturnValue as N'@ReturnValue'
             */
            #endregion

            string re = @"
                        SELECT	@ReturnValue = 0
                        SET @MMID=CONVERT(BIGINT,@MMID)+6
                        SET @MMORDERS=CONVERT(BIGINT,@MMORDERS)+6
                EXEC	@return_value = [dbo].[proc_K_ModelManageSave]
		                @TranType = N'NEW',
                        @ID = @MMID,
";
            ModelManage mmObj = new ModelManage();
            DataTable dt = mmObj.GetList("ONE", Utils.getOneParams(id));
            title = string.Empty;
            string InserPublicOperSql = string.Empty;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string[] arr = GetTableAction(dr["TableName"].ToString());

                re += GetFieldValue(dr["MenuNo"].ToString(), "N''", "MenuNo");
                re += GetFieldValue(SiteID.ToString(), "1", "SiteID");
                re += GetFieldValue(dr["Title"].ToString(), "N''", "Title");
                re += GetFieldValue(dr["TableName"].ToString(), "N''", "TableName");
                re += GetFieldValue(dr["ModuleID"].ToString(), "NULL", "ModuleID");
                re += GetFieldValue(dr["SysField"].ToString(), "N''", "SysField");
                re += GetFieldValue(dr["ListLink"].ToString(), "N''", "ListLink");
                re += GetFieldValue(dr["ListButton"].ToString(), "N''", "ListButton");
                re += GetFieldValue(dr["OperationColumn"].ToString(), "N''", "OperationColumn");
                re += GetFieldValue(dr["CustomCol"].ToString(), "N''", "CustomCol");
                re += GetFieldValue(dr["Memo"].ToString(), "N''", "Memo");
                re += "@Orders=@MMORDERS,\r\n";
                re += GetFieldValue(dr["IsEnable"].ToString(), "true", "IsEnable");
                re += GetFieldValue(dr["IsDel"].ToString(), "false", "IsDel");
                re += GetFieldValue(dr["IsHtml"].ToString(), "false", "IsHtml");
                re += GetFieldValue(dr["IsOrderEdit"].ToString(), "true", "IsOrderEdit");
                re += "@ddlSql=N'" + GetTablesScript(dr["TableName"].ToString()) + "',\r\n";
                re += "@dmlSql=N'',\r\n";
                re += "@operName=N'" + arr[1] + "',\r\n";
                re += "@operTitle=N'" + arr[0] + "',\r\n";
                re += "@operCount=" + arr[2] + ",\r\n";
                re += GetFieldValue(dr["IsListContentClip"].ToString(), "true", "IsListContentClip");
                re += GetFieldValue(dr["DeliverAndSearchUrlParam"].ToString(), "NULL", "DeliverAndSearchUrlParam");
                re += GetFieldValue(dr["FieldFromUrlParamValue"].ToString(), "NULL", "FieldFromUrlParamValue");
                re += GetFieldValue(dr["OperationColumnWidth"].ToString(), "N''", "OperationColumnWidth");
                re += GetFieldValue(dr["ConfigMan"].ToString(), "N'admin'", "ConfigMan");
                re += GetFieldValue(dr["NotSearchField"].ToString(), "NULL", "NotSearchField");
                re += GetFieldValue(dr["BackDeliverUrlParam"].ToString(), "'NodeCode'", "BackDeliverUrlParam");
                re += GetFieldValue(dr["CommonField"].ToString(), "N''", "CommonField");
                re += GetFieldValue(dr["IsSub"].ToString(), "false", "IsSub");
                re += GetFieldValue(dr["SubModelGroupID"].ToString(), "NULL", "SubModelGroupID");

                title = dr["Title"].ToString();
                string[] operArr = arr[0].Split(',');
                string[] operArrTitle = arr[1].Split(',');
                for (int i = 0; i < operArr.Length; i++) 
                {
                    InserPublicOperSql += "IF not exists(select top 1 * from K_SysPublicOper where OperName='" + operArrTitle[i].Replace("'", "''") + "')\r\n";
                    InserPublicOperSql += "      INSERT INTO K_SysPublicOper(OperName,Title,IsValid) VALUES ('" + operArrTitle[i].Replace("'", "''") + "','" + operArr[i].Replace("'", "''") + "',1);\r\n";
                } 
            }
            re += "@ReturnValue = @ReturnValue OUTPUT\r\n";
            re += "IF @ReturnValue=1\r\n";
            re += "BEGIN\r\n";
            re += InserPublicOperSql;

            return re;
        }

        private string GetModelFieldGroupSql(string modelID, out Dictionary<string, string> dicModelGroup)
        {

            string re = "";
            ModelFieldGroup mfgObj = new ModelFieldGroup();
            DataTable dt = mfgObj.GetList("ALL", Utils.getOneParams(modelID));
            dicModelGroup = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                re += @"SET @MFGID=CONVERT(BIGINT,@MFGID)+6;
                    SET @MFGORDERS=@MFGORDERS+6;
                    INSERT K_ModelFieldGroup VALUES (@MFGID,@MMID
                    ";
                re += GetFieldValue(dr["Name"].ToString(), "''");
                re += ",@MFGORDERS";
                re += GetFieldValue(dr["IsEnable"].ToString(), "0");
                re += GetFieldValue(dr["AddDate"].ToString(), "NULL");
                re += ")\r\n";
                dicModelGroup.Add(dr["ID"].ToString(), dr["Name"].ToString());
            }
            return re;
        }

        private string GetGroupTitle(string groupID, Dictionary<string, string> dicGroup)
        {
            if (dicGroup == null||dicGroup.Count ==0)
                return "0";

            return dicGroup[groupID];
        }

        private string GetModelFieldSql(string modelID, Dictionary<string, string> dicGroup)
        {
            /*SET @MFID=CONVERT(BIGINT,@MFID)+6
            SET @MFORDERS=@MFORDERS+6
            INSERT K_ModelField VALUES (@MFID ,@MMID,'ttt2','基本资料3','ttt2','',3,'mini500',NULL,100,30,NULL,NULL,NULL,0,0,0,2,NULL,NULL,0,NULL,NULL,'',NULL,NULL,0,NULL,NULL,NULL,0,0,0,NULL,NULL,0,1,@MFORDERS,0,NULL,NULL,NULL,NULL,NULL,0,NULL,0,NULL,0,NULL,1,NULL,NULL,'2012-11-19 16:28:36.757',NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,0,0,1,NULL,NULL,100,1,1,NULL,NULL,NULL,1,1,NULL,NULL,NULL,NULL,0,NULL,0,NULL,NULL,0,0)
             */
            ;
            Dictionary<string, string> dicModelGroup = new Dictionary<string, string>();
            string re = "";
            ModelField mfObj = new ModelField();
            Model.SelectParams msObj = new KingTop.Model.SelectParams();
            msObj.S1 = modelID;
            msObj.I1 = 1;   //模型字段类型(1)为新闻,2为表单,3为商家  这里为内容模型，故为1
            DataTable dt = mfObj.GetList("ALL", msObj);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                re += @"SET @MFID=CONVERT(BIGINT,@MFID)+6;
                        SET @MFORDERS=@MFORDERS+6;
                        INSERT K_ModelField VALUES (@MFID ,@MMID
                ";
                re += GetFieldValue(dr["Name"].ToString(), "''");
                re += GetFieldValue(GetGroupTitle(dr["ModelFieldGroupId"].ToString(), dicGroup), "'0'");
                re += GetFieldValue(dr["FieldAlias"].ToString(), "''");
                re += GetFieldValue(dr["Message"].ToString(), "''");
                re += GetFieldValue(dr["BasicField"].ToString(), "NULL");
                re += GetFieldValue(dr["EditorStyle"].ToString(), "''");
                re += GetFieldValue(dr["TextBoxMaxLength"].ToString(), "NULL");
                re += GetFieldValue(dr["TextBoxWidth"].ToString(), "0");
                re += GetFieldValue(dr["TextBoxHieght"].ToString(), "0");
                re += GetFieldValue(dr["TextBoxValidation"].ToString(), "NULL");
                re += GetFieldValue(dr["ValidationType"].ToString(), "NULL");
                re += GetFieldValue(dr["ValidationMessage"].ToString(), "NULL");
                re += GetFieldValue(dr["IsLink"].ToString(), "0");
                re += GetFieldValue(dr["IsFilter"].ToString(), "0");
                re += GetFieldValue(dr["IsShield"].ToString(), "0");
                re += GetFieldValue(dr["EditorType"].ToString(), "2");
                re += GetFieldValue(dr["OptionsValue"].ToString(), "NULL");
                re += GetFieldValue(dr["OptionCount"].ToString(), "NULL");
                re += GetFieldValue(dr["IsFill"].ToString(), "0");
                re += GetFieldValue(dr["MinValue"].ToString(), "NULL");
                re += GetFieldValue(dr["MaxValue"].ToString(), "NULL");
                re += GetFieldValue(dr["DefaultValue"].ToString(), "''");
                re += GetFieldValue(dr["DateDefaultOption"].ToString(), "NULL");
                re += GetFieldValue(dr["DateFormatOption"].ToString(), "NULL");
                re += GetFieldValue(dr["IsUpload"].ToString(), "0");
                re += GetFieldValue(dr["MaxSize"].ToString(), "NULL");
                re += GetFieldValue(dr["ImageType"].ToString(), "NULL");
                re += GetFieldValue(dr["ImageNameRules"].ToString(), "NULL");
                //0,0,0,NULL,NULL,0,1,@MFORDERS,0,NULL,NULL,NULL,NULL,NULL,0,NULL,0,NULL,0,NULL,1,NULL,NULL,'2012-11-19 16:28:36.757',
                re += GetFieldValue(dr["ImageIsWatermark"].ToString(), "0");
                re += GetFieldValue(dr["IsUploadThumbnail"].ToString(), "0");
                re += GetFieldValue(dr["IsSaveFileSize"].ToString(), "0");
                re += GetFieldValue(dr["SaveFileName"].ToString(), "NULL");
                re += GetFieldValue(dr["UrlPrefix"].ToString(), "NULL");
                re += GetFieldValue(dr["IsRequired"].ToString(), "0");
                re += GetFieldValue(dr["IsEnable"].ToString(), "1");
                re += GetFieldValue(dr["Orders"].ToString(), "@MFORDERS");
                re += GetFieldValue(dr["IsSearch"].ToString(), "0");
                re += GetFieldValue(dr["SearchWidth"].ToString(), "NULL");
                re += GetFieldValue(dr["SearchOrders"].ToString(), "NULL");
                re += GetFieldValue(dr["ListWidth"].ToString(), "NULL");
                re += GetFieldValue(dr["ListAlignment"].ToString(), "NULL");
                re += GetFieldValue(dr["ListOrders"].ToString(), "NULL");
                re += GetFieldValue(dr["ListIsLink"].ToString(), "0");
                re += GetFieldValue(dr["ListLinkUrl"].ToString(), "NULL");
                re += GetFieldValue(dr["ListIsOrder"].ToString(), "0");
                re += GetFieldValue(dr["ListOrderOption"].ToString(), "NULL");
                re += GetFieldValue(dr["ListIsDefaultOrder"].ToString(), "0");
                re += GetFieldValue(dr["ListDefaultOrderOption"].ToString(), "NULL");
                re += GetFieldValue(dr["IsRss"].ToString(), "1");
                re += GetFieldValue(dr["UserGroupId"].ToString(), "NULL");
                re += GetFieldValue(dr["RoleGroupId"].ToString(), "NULL");
                re += GetFieldValue(dr["AddTime"].ToString(), "NULL");
                //NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,0,0,1,NULL,NULL,100,1,1,NULL,NULL,NULL,1,1,
                re += GetFieldValue(dr["UserNo"].ToString(), "NULL");
                re += GetFieldValue(dr["DropDownDataType"].ToString(), "NULL");
                re += GetFieldValue(dr["DropDownTable"].ToString(), "NULL");
                re += GetFieldValue(dr["DropDownTextColumn"].ToString(), "NULL");
                re += GetFieldValue(dr["DropDownValueColumn"].ToString(), "NULL");
                re += GetFieldValue(dr["DropDownSql"].ToString(), "NULL");
                re += GetFieldValue(dr["IsListEnable"].ToString(), "0");
                re += GetFieldValue(dr["NumberCount"].ToString(), "NULL");
                re += GetFieldValue(dr["IsSystemFiierd"].ToString(), "0");
                re += GetFieldValue(dr["IsDel"].ToString(), "0");
                re += GetFieldValue(dr["IsInputValue"].ToString(), "1");
                re += GetFieldValue(dr["SearchUIType"].ToString(), "NULL");
                re += GetFieldValue(dr["DropDownSqlWhere"].ToString(), "NULL");
                re += GetFieldValue(dr["DataColumnLength"].ToString(), "0");
                re += GetFieldValue(dr["ModelFieldType"].ToString(), "1");
                re += GetFieldValue(dr["IsListVisible"].ToString(), "1");
                re += GetFieldValue(dr["SystemFirerdHtml"].ToString(), "NULL");
                re += GetFieldValue(dr["IsMultiFile"].ToString(), "NULL");
                re += GetFieldValue(dr["Controls"].ToString(), "NULL");
                re += GetFieldValue(dr["ThumbDisplayType"].ToString(), "1");
                re += GetFieldValue(dr["SysFieldType"].ToString(), "1");
                //NULL,NULL,NULL,NULL,0,NULL,0,NULL,NULL,0,0)
                re += GetFieldValue(dr["ThumbWidth"].ToString(), "NULL");
                re += GetFieldValue(dr["ThumbHeight"].ToString(), "NULL");
                re += GetFieldValue(dr["DelTime"].ToString(), "NULL");
                re += GetFieldValue(dr["NodeCode"].ToString(), "NULL");
                re += GetFieldValue(dr["IsInterface"].ToString(), "0");
                re += GetFieldValue(dr["SubModelGroupID"].ToString(), "NULL");
                re += GetFieldValue(dr["IsAlbums"].ToString(), "0");
                re += GetFieldValue(dr["RelatedType"].ToString(), "NULL");
                re += GetFieldValue(dr["SubModelName"].ToString(), "NULL");
                re += GetFieldValue(dr["IsAlbumsUploadThumb"].ToString(), "0");
                re += GetFieldValue(dr["IsAlbumsDesc"].ToString(), "0");
                re += GetFieldValue(dr["ImageBestWidth"].ToString(), "0");
                re += GetFieldValue(dr["ImageBestHeight"].ToString(), "0");
                re += ");\r\n";
            }
            return re;
        }

        private string GetFieldValue(string FieldValue,string isEmptyReturnValue)
        {
            string re = string.Empty;
            if (string.IsNullOrEmpty(FieldValue))
            {
                return ","+isEmptyReturnValue;
            }
            if (FieldValue == "True" || FieldValue == "False")
            {
                return ","+Utils.ParseBoolToInt(FieldValue);
            }

            return ",'"+FieldValue.Replace ("'","''")+"'";
        }

        public string GetContent(string idList)
        {
            string content = string.Empty;
            string outTitle = string.Empty;
            if (string.IsNullOrEmpty(idList))
                return "";

            string[] idArr = idList.Replace(", ", ",").Split(',');
            content = @"
                DECLARE @MMID varchar(15)
                DECLARE @MMORDERS INT
                DECLARE @MFGID varchar(15)
                DECLARE @MFGORDERS INT
                DECLARE @MFID varchar(15)
                DECLARE @MFORDERS INT
                DECLARE	@return_value int,
		                @ReturnValue int
                
                SELECT @MMID=ISNULL(CONVERT(BIGINT,MAX(ID)),0)+6,@MMORDERS=ISNULL(MAX(Orders),0)+6 FROM K_ModelManage ;
                SELECT @MFGID=ISNULL(CONVERT(BIGINT,MAX(ID)),0)+6,@MFGORDERS=ISNULL(MAX(Orders),0)+6 FROM K_ModelFieldGroup;                
                SELECT @MFID=ISNULL(CONVERT(BIGINT,MAX(ID)),0)+6,@MFORDERS=ISNULL(MAX(Orders),0)+6 FROM K_ModelField;
            ";

            for (int i = 0; i < idArr.Length; i++)
            {
                content += GetModelSql(idArr[i], out outTitle);
                Dictionary<string, string> dicGroup = new Dictionary<string, string>();
                content += GetModelFieldGroupSql(idArr[i], out dicGroup);
                content += GetModelFieldSql(idArr[i], dicGroup);

                content += "UPDATE K_ModelField SET ModelFieldGroupId=A.ID FROM K_ModelFieldGroup AS A WHERE ModelFieldGroupId=A.Name AND K_ModelField.ModelId=@MMID;\r\n";
                content += "END\r\n";
                if (string.IsNullOrEmpty(fileTitle))
                {
                    fileTitle = outTitle;
                }
                else if (!string.IsNullOrEmpty(outTitle))
                {
                    fileTitle += "-" + outTitle;
                }
            }

            return content;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string idList = Request.Form["chkId"];

            string content = GetContent(idList);
            //return;
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileTitle + ".sql");
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(content);
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.End();

            #region SQL示例
            /*
             
            DECLARE @MMID varchar(15)   --K_ModelManage.ID
            DECLARE @MMORDERS INT
            DECLARE @MFGID varchar(15)	--K_ModelFieldGroup.ID
            DECLARE @MFGORDERS INT
            DECLARE @MFID varchar(15)	--K_ModelField.ID
            DECLARE @MFORDERS INT
            SELECT @MMID=CONVERT(BIGINT,MAX(ID))+6,@MMORDERS=MAX(Orders)+6 FROM K_ModelManage 
            
            SELECT @MFGID=CONVERT(BIGINT,MAX(ID))+6,@MFGORDERS=MAX(Orders)+6 FROM K_ModelFieldGroup
            
            SELECT @MFID=CONVERT(BIGINT,MAX(ID))+6,@MFORDERS=MAX(Orders)+6 FROM K_ModelField
            

            --导入K_ModelManage
            INSERT K_ModelManage VALUES(@MMID,'76d6c241-548b-4633-9',1,'友情连接2','K_U_AuxiliaryLinks',1,'','none$','1,2$','1,2','','',@MMORDERS,'2012-06-13 11:12:17.467',1,0,0,1,1,0,'','','20%','','','admin','','NodeCode','e3bbb9df-afff-409d-bf13-64f92382e5ae,bb116c72-8d70-4d79-8271-e3f35932c07c,8399c733-b017-4faa-b404-20eaf53e6f42','2012-08-21 16:11:00.513','',0,NULL)

            --导入K_ModelFieldGroup
            INSERT K_ModelFieldGroup VALUES (@MFGID,@MMID,'基本资料2',@MFGORDERS,0,'2010-11-27 14:59:00')
            SET @MFGID=CONVERT(BIGINT,@MFGID)+6
            SET @MFGORDERS=@MFGORDERS+6
            INSERT K_ModelFieldGroup VALUES (@MFGID,@MMID,'基本资料3',@MFGORDERS,0,'2010-11-27 14:59:00')

            --导入K_ModelField
            INSERT K_ModelField VALUES (@MFID ,@MMID,'ttt1','基本资料2','ttt1','',3,'mini500',NULL,100,30,NULL,NULL,NULL,0,0,0,2,NULL,NULL,0,NULL,NULL,'',NULL,NULL,0,NULL,NULL,NULL,0,0,0,NULL,NULL,0,1,@MFORDERS,0,NULL,NULL,NULL,NULL,NULL,0,NULL,0,NULL,0,NULL,1,NULL,NULL,'2012-11-19 16:28:36.757',NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,0,0,1,NULL,NULL,100,1,1,NULL,NULL,NULL,1,1,NULL,NULL,NULL,NULL,0,NULL,0,NULL,NULL,0,0)
            SET @MFID=CONVERT(BIGINT,@MFID)+6
            SET @MFORDERS=@MFORDERS+6
            INSERT K_ModelField VALUES (@MFID ,@MMID,'ttt2','基本资料3','ttt2','',3,'mini500',NULL,100,30,NULL,NULL,NULL,0,0,0,2,NULL,NULL,0,NULL,NULL,'',NULL,NULL,0,NULL,NULL,NULL,0,0,0,NULL,NULL,0,1,@MFORDERS,0,NULL,NULL,NULL,NULL,NULL,0,NULL,0,NULL,0,NULL,1,NULL,NULL,'2012-11-19 16:28:36.757',NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,0,0,1,NULL,NULL,100,1,1,NULL,NULL,NULL,1,1,NULL,NULL,NULL,NULL,0,NULL,0,NULL,NULL,0,0)

            UPDATE K_ModelField SET ModelFieldGroupId=A.ID FROM K_ModelFieldGroup AS A WHERE ModelFieldGroupId=A.Name AND K_ModelField.ModelId=@MMID

             */
            #endregion
        }
        #endregion

        #region 导入
        protected void btnImport_Click(object sender, EventArgs e)
        {
            //导入功能暂时不做，因为考虑安全性，如果要做，则必须是在线执行SQL语句，这样比较危险
        }
        #endregion

        #region 复制

        #endregion
    }
}
