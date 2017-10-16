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
using System.Text.RegularExpressions;
using System.IO;
using Wuqi.Webdiyer;
using KingTop.Common;
using KingTop.BLL.Content;
using KingTop.Web.Admin;
#endregion


namespace KingTop.WEB.SysAdmin.Model
{
    public partial class ModelMangeCopy : AdminPage
    {
        #region 变量成员
        private string _modelID;                       // 所属模型
        private string _isDel = "0";                   // 逻辑删除
        private string _isSub = string.Empty;          // 子模型
        private string _tableName = null;              // 所属表名
        private string _searchType = null;             // 搜索字段
        private string _searchValue = null;            // 搜索值
        private string _sort;                          // 排序
        protected string jsMessage;                    // 操作返回的JS代码
        protected string urlParam;                     // 要传递的URL参数
        protected BLL.Content.FieldManage fieldManage; // 业务操作对象
        private string _modelName;                      // 所属模型名称
        public string qianzui = string.Empty;
        private string noCopyFields = ",";
        #endregion

        #region 属性
        /// <summary>
        /// 所属模型名称
        /// </summary>
        public string ModelName
        {
            get 
            {
                if (string.IsNullOrEmpty(this._modelName))
                {
                    this._modelName = Request.QueryString["ModelName"];
                }

                return this._modelName; 
            }
        }

        /// <summary>
        /// 所属模型表
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(this._tableName))
                {
                    this._tableName = Request.QueryString["TableName"];
                }

                return this._tableName;
            }
        }

        /// <summary>
        /// 所属模型ID
        /// </summary>
        public string ModelID
        {
            get
            {
                if (string.IsNullOrEmpty(this._modelID))
                {
                    this._modelID = Request.QueryString["ModelID"];
                }

                return this._modelID;
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

        #region  Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            fieldManage = new FieldManage(ModelID, TableName);
            urlParam = "IsSub="+ IsSub + "&ModelID=" + ModelID + "&TableName=" + TableName + "&NodeCode=" + NodeCode + "&ModelName=" + ModelName; // 要传递的参数
            if (!string.IsNullOrEmpty(TableName) && TableName.Length > 4)
            {
                qianzui = TableName.Substring(0, 4);
            }
            if (!IsPostBack && !string.IsNullOrEmpty(this.ModelID))
            {
                PageInit();
            }

            SetRight(this.Page, rptModelField);
        }
        #endregion

        #region 初始加载
        /// <summary>
        /// 初始化方法
        /// </summary>
        public void PageInit()
        {
            hdnUrlParm.Value = this.urlParam;

            if (!string.IsNullOrEmpty(ModelID.Trim()) && !string.IsNullOrEmpty(TableName.Trim()))
            {
                SplitDataBind();
            }
        }
        #endregion

        #region 分页
        // 分页控件数据绑定
        private void SplitDataBind()
        {
            KingTop.Model.SelectParams kmsObj = new KingTop.Model.SelectParams();
            kmsObj.S1 = ModelID;
            kmsObj.I1 = 1;
            DataTable dt = fieldManage.GetList("ALL", kmsObj);
            rptModelField.DataSource = dt;
            rptModelField.DataBind();
            string fieldsName = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["IsSystemFiierd"].ToString() == "True")
                    continue;

                if (string.IsNullOrEmpty(fieldsName))
                {
                    fieldsName = dt.Rows[i]["Name"].ToString();
                }
                else
                {
                    fieldsName += "," + dt.Rows[i]["Name"].ToString();
                }
            }
            hidFields.Value = fieldsName;
        }

        #endregion

        protected void btn_OnCopy(object sender, EventArgs e)
        {
            bool isValidate;

            isValidate = IsHaveRightByOperCode("Copy");
            string moduleTitle = txtName.Text;
            string dbTableName = Request.Form ["hidQianZui"]+ txtdbTableName.Text;

            if (string.IsNullOrEmpty(moduleTitle) || string.IsNullOrEmpty(txtdbTableName.Text))
            {
                jsMessage += "alert({msg:\"模型名称和表名不能为空！\",title:\"操作提示\"});";
                return;
            }

            KingTop.BLL.Content.ModelAjaxDeal ajaDeal = new KingTop.BLL.Content.ModelAjaxDeal();
            bool IsExistsTable=ajaDeal.CheckModelRepeat(txtdbTableName.Text);
            if (IsExistsTable)
            {
                jsMessage += "alert({msg:\"复制失败，数据库表："+dbTableName+"已存在，请重新填写！\",title:\"操作提示\"});";
                return;
            }

            if (isValidate)
            {
                string moduleNameList = ","+Request.Form["chkId"]+",";
                moduleNameList = moduleNameList.Replace(", ", ",");
                string hidFieldList = hidFields.Value;
                string[] fieldArr = hidFieldList.Split(',');
                int noCopyFieldsNum = 0;
                for (int i = 0; i < fieldArr.Length; i++)
                {

                    if (moduleNameList.IndexOf("," + fieldArr[i] + ",") == -1 && fieldArr[i]!="ID")
                    {
                        noCopyFields += fieldArr[i] + ",";
                        noCopyFieldsNum++;
                    }
                }
                if (noCopyFieldsNum<fieldArr .Length-1)
                {
                    GetContent(ModelID);
                }
                else
                {
                    jsMessage += "alert({msg:\"复制失败，必须选择列！\",title:\"操作提示\"});";
                    return;
                }
            }
        }


        #region 复制
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

                //注释掉不需复制的字段
                string[] arrFields = noCopyFields.Split(',');
                for (int i = 0; i < arrFields.Length; i++)
                {
                    if (!string.IsNullOrEmpty(arrFields[i]) && tableScript.ToLower ().IndexOf ("["+arrFields[i].ToLower ()+"]")!=-1)
                    {
                        tableScript = Regex.Replace (tableScript,"\\[" + arrFields[i] + "\\]", "--[" + arrFields[i] + "]",RegexOptions.IgnoreCase);
                    }
                }

                return tableScript.Replace("'", "''");
            }
            catch
            {
                Response.Write("<div align=center style='padding:20px'>复制失败，原因是sqldmo.dll未注册，注册方法如下:<br><br> 打开开始，在运行中输入 regsvr32 \"C:\\Program Files\\Microsoft SQL Server\\80\\Tools\\Binn\\sqldmo.dll\" 注册sqldmo.dll。<br><br>在注册前请确认sqldmo.dll是否存在，不存在请从网上下载sqldmo.dll到相应目录，再进行注册");
                Response.Write("<br><br><a href=# onclick='history.back();'>[返回]</a></div>");
                Response.End();
                return "";

            }
        }

        private string GetFieldValue(string FieldValue, string defaultValue, string Fields)
        {
            string re = "@" + Fields + "=";
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
            string[] actionArr = { "", "", "" };
            string sqlStr = "select OperName,OperEngDesc from K_SysActionPermit where ModuleID=(select moduleid from K_SysModule where  LinkUrl ='../Model/" + dtName.Replace("K_U_", "") + "list.aspx')";
            DataTable dt = SQLHelper.GetDataSet(sqlStr);
            string actionName = string.Empty;
            string actionOper = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(actionName))
                {
                    actionName = dt.Rows[i]["OperName"].ToString().Replace("'", "''");
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
        private string GetModelSql(string id)
        {
            string re = @"
                        SELECT	@ReturnValue = 0
                        SET @MMID=CONVERT(BIGINT,@MMID)+6
                EXEC	@return_value = [dbo].[proc_K_ModelManageSave]
		                @TranType = N'NEW',
                        @ID = @MMID,
";
            ModelManage mmObj = new ModelManage();
            DataTable dt = mmObj.GetList("ONE", Utils.getOneParams(id));
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
            }
            re += "@ReturnValue = @ReturnValue OUTPUT\r\n";
            re += "IF @ReturnValue=1\r\n";
            re += "BEGIN\r\n";

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
            if (dicGroup == null || dicGroup.Count == 0)
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
            KingTop.Model.SelectParams msObj = new KingTop.Model.SelectParams();
            msObj.S1 = modelID;
            msObj.I1 = 1;   //模型字段类型(1)为新闻,2为表单,3为商家  这里为内容模型，故为1
            DataTable dt = mfObj.GetList("ALL", msObj);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (noCopyFields.ToLower().IndexOf("," + dr["Name"].ToString().ToLower() + ",") != -1)
                    continue;
                
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

        private string GetFieldValue(string FieldValue, string isEmptyReturnValue)
        {
            string re = string.Empty;
            if (string.IsNullOrEmpty(FieldValue))
            {
                return "," + isEmptyReturnValue;
            }
            if (FieldValue == "True" || FieldValue == "False")
            {
                return "," + Utils.ParseBoolToInt(FieldValue);
            }

            return ",'" + FieldValue.Replace("'", "''") + "'";
        }

        private void GetContent(string id)
        {
            string content = string.Empty;
            if (string.IsNullOrEmpty(id))
                return ;

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


            content += GetModelSql(id);
            Dictionary<string, string> dicGroup = new Dictionary<string, string>();
            content += GetModelFieldGroupSql(id, out dicGroup);
            content += GetModelFieldSql(id, dicGroup);

            content += "UPDATE K_ModelField SET ModelFieldGroupId=A.ID FROM K_ModelFieldGroup AS A WHERE ModelFieldGroupId=A.Name AND K_ModelField.ModelId=@MMID;\r\n";
            content += "END\r\n";

            content = Regex.Replace(content, TableName, Request.Form["hidQianZui"] + txtdbTableName.Text, RegexOptions.IgnoreCase);
            content = Regex.Replace(content, ModelName, txtName.Text, RegexOptions.IgnoreCase);

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, content);
            jsMessage += "alert({msg:\"模型复制完毕！\",title:\"操作提示\"});";
        }
        #endregion
    }
}

