using KingTop.Common;
using KingTop.Web.Admin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace KingTop.WEB.SysAdmin.Common
{
    public partial class Commonedit : AdminPage
    {
        protected Hashtable hsFieldValue;                        // 字段初始值
        protected BLL.Content.ControlManageEdit ctrManageEdit;   // 业务操作对象
        protected string jsMessage;                              // js操作提示
        protected string isHasCreateHtmlRight;

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrManageEdit = new KingTop.BLL.Content.ControlManageEdit(hdnFieldValue.Value, hdnTableName.Value, hdnModelID.Value);
            ctrManageEdit.SiteID = SiteID;
            ctrManageEdit.NodeCode = NodeCode;
            ctrManageEdit.UserName = GetLoginAccountName();
            hdnActionType.Value = Utils.CheckSql(Request.QueryString["action"].ToLower());
            if (!Page.IsPostBack)
            {                
                if (!string.IsNullOrEmpty(this.ID))
                {
                    btnModelManageEdit.Text = "修改";
                    hdnBackUrlParam.Value = "";
                    string sql = "select id,title,detail from K_U_Common where id='" + this.ID + "'";
                    DataTable dt=SQLHelper.GetDataSet(sql);
                    if(Utils.CheckDataTable(dt))
                    {
                        this.editor_detail.Content = dt.Rows[0]["detail"].ToString();
                    }
                    
                }
                else
                {
                    btnModelManageEdit.Text = "添加";
                }                      
            }
            this.hsFieldValue = ctrManageEdit.FillField();
            hdnNodeID.Value = Convert.ToString(hsFieldValue["NodeCode"]);
            hdnSiteID.Value = Convert.ToString(hsFieldValue["SiteID"]);         
            
        }

        #region 提交
        protected void btnModelManageEdit_Click(object sender, EventArgs e)
        {
            if (Action == "NEW")
            {
                if (!IsHaveRightByOperCode("New"))
                {
                    Utils.AlertMessage(this, "没有添加权限，请联系站点管理员！");
                    return;
                }
            }
            else
            {
                if (!IsHaveRightByOperCode("Edit"))
                {
                    Utils.AlertMessage(this, "没有修改权限，请联系站点管理员！");
                    return;
                }
            }
            string logTitle = Utils.CheckSql(Request.Form["Title"]);
            string sql = string.Empty;
            
            if (hdnActionType.Value.ToLower() == "new")
            {
                //添加
                //sql = string.Format("INSERT INTO K_U_ProductAlbum ([ID]  ,[IsDel] ,[IsEnable] ,[IsArchiving] ,[Orders] ,[AddDate]  ,[DelTime] ,[SiteID],[NodeCode] ,[FlowState] ,[ReferenceID] ,[AddMan] ,[Title] ,[Album] ,[productID]) VALUES ('{0}'  ,0 ,1 ,0 ,99 ,'{1}' ,'{2}' ,1  ,'{3}' ,3 ,null ,'Admin' ,'{4}','{5}','{6}')", sqlid, DateTime.Now.ToString(), DateTime.Now.ToString(), NodeCode, title, Album, pid);
                //bool result=KingTop.Common.SQLHelper.ExcuteCommand(sql);
                string strMessage = Save(hdnActionType.Value.ToLower());
                int result = Utils.ParseInt(strMessage, 0);

                switch (result)
                {
                    case 1:     //成功
                        WriteLog(GetLogValue(logTitle, Action, "通用功能自定义模块", true), "", 2);
                        string listUrl = "Commonlist.aspx?NodeCode=" + this.NodeCode + "&IsFirst=1";
                        string addUrl = "Commonedit.aspx?action=new&NodeCode=" + this.NodeCode;
                        string updateUrl = "Commonedit.aspx?action=edit&ID=" + this.hdNewID.Value + "&NodeCode=" + this.NodeCode;
                        Utils.RunJavaScript(this, "type=0;title='" + logTitle.Replace("'", "\\'") + "';$(function () { showEditMessage(\"添加成功！\",\"" + listUrl + "\",\"" + addUrl + "\",\"" + updateUrl + "\");})");
                        break;

                    case 0:     //失败
                        WriteLog(GetLogValue(logTitle, Action, "通用功能自定义模块", false), strMessage, 3);
                        Utils.AlertMessage(this, "添加失败！", "系统提示");
                        break;
                }

            }
            else if (hdnActionType.Value.ToLower() == "edit")
            {
                //编辑
                string strMessage = Save(hdnActionType.Value.ToLower());
                int result = Utils.ParseInt(strMessage, 0);

                switch (result)
                {
                    case 1:     //成功
                        WriteLog(GetLogValue(logTitle, Action, "通用功能自定义模块", true), "", 2);
                        string listUrl = "Commonlist.aspx?NodeCode=" + this.NodeCode + "&IsFirst=1";
                        string addUrl = "Commonedit.aspx?action=new&NodeCode=" + this.NodeCode;
                        string updateUrl = "Commonedit.aspx?action=edit&ID=" + this.ID + "&NodeCode=" + this.NodeCode;
                        Utils.RunJavaScript(this, "type=0;title='" + logTitle.Replace("'", "\\'") + "';$(function () { showEditMessage(\"修改成功！\",\"" + listUrl + "\",\"" + addUrl + "\",\"" + updateUrl + "\");})");
                        break;

                    case 0:     //失败
                        WriteLog(GetLogValue(logTitle, Action, "通用功能自定义模块", false), strMessage, 3);
                        Utils.AlertMessage(this, "修改失败！", "系统提示");
                        break;
                }
            }
            
            
            
        }
        #endregion

        #region 添加、修改K_U_Common表
        /// <summary>
        /// 添加、修改K_U_Common表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType)
        {
            string id = Utils.CheckSql(this.ID);
            int IsDel = 0;
            int IsEnable = 1;
            int IsArchiving = 0;
            int Orders=1;
            int SiteID = this.SiteID;
            string NodeCode = this.hdnNodeID.Value;
            int FlowState = 3;
            string ReferenceID = id;
            string AddMan=this.AddMan;
            string Title = Utils.CheckSql(Request.Form["Title"]);
            string UserName = Utils.CheckSql(Request.Form["UserName"]);
            string PubDate = Utils.GetStandardDateTime(Request.Form["PubDate"], "yyyy-MM-dd HH:mm:ss");
            string Intro = Utils.CheckSql(Request.Form["Intro"]);
            string BigImg = Utils.CheckSql(Request.Form["BigImg"]);
            string SmallImg = Utils.CheckSql(Request.Form["SmallImg"]);
            string listimage = Utils.CheckSql(Request.Form["listimage"]);
            string detail = this.editor_detail.Content;
            string Password = Utils.getMD5(Request.Form["Password"]);
            string Attach = Utils.CheckSql(Request.Form["Attach"]);
            string PhotoAlbum = Request.Form["PhotoAlbum"];
            string Sex = Utils.CheckSql(Request.Form["Sex"]);
            string Interest = Utils.CheckSql(Request.Form["Interest"]);
            string AttachList = Utils.CheckSql(Request.Form["AttachList"]);
            string[] arrIDOrders = GetTableID("0", this.hdnTableName.Value);//主键（ID）和排序数组

            //添加
            if (tranType.ToLower() == "new")
            {
                id = arrIDOrders[0];
                ReferenceID = id;
                this.hdNewID.Value = id;
                Orders = Utils.ParseInt(arrIDOrders[1], 1);
            }
            else 
            {
                //修改
                string sql = "select ID,IsDel,IsEnable,IsArchiving,Orders,AddDate,DelTime,SiteID,NodeCode,FlowState,ReferenceID from K_U_Common where id='" + id+"'";
                DataTable dt = SQLHelper.GetDataSet(sql);
                if (Utils.CheckDataTable(dt))
                {
                    IsDel =Utils.ParseInt(dt.Rows[0]["IsDel"].ToString(),0);
                    IsEnable = Utils.ParseInt(dt.Rows[0]["IsEnable"].ToString(), 1);
                    IsArchiving = Utils.ParseInt(dt.Rows[0]["IsArchiving"].ToString(), 0);
                    Orders = Utils.ParseInt(dt.Rows[0]["Orders"].ToString(),1);
                    FlowState = Utils.ParseInt(dt.Rows[0]["FlowState"].ToString(),3);
                }
            }            

            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_U_CommonSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@ID",id),
                    new SqlParameter("@IsDel",IsDel),
                    new SqlParameter("@IsEnable",IsEnable),
                    new SqlParameter("@IsArchiving",IsArchiving),
                    new SqlParameter("@Orders",Orders),
                    //new SqlParameter("@AddDate",paramsModel.AddDate),
                    new SqlParameter("@DelTime",null),
                    new SqlParameter("@SiteID",SiteID),
                    new SqlParameter("@NodeCode",NodeCode),
                    new SqlParameter("@FlowState",FlowState),
                    new SqlParameter("@ReferenceID",ReferenceID),
                    new SqlParameter("@AddMan",AddMan),
                    new SqlParameter("@Title",Title),
                    new SqlParameter("@UserName",UserName),
                    new SqlParameter("@PubDate",PubDate),
                    new SqlParameter("@Intro",Intro),
                    new SqlParameter("@BigImg",BigImg),
                    new SqlParameter("@SmallImg",SmallImg),
                    new SqlParameter("@listimage",listimage),
                    new SqlParameter("@detail",detail),
                    new SqlParameter("@Password",Password),
                    new SqlParameter("@Attach",Attach),
                    new SqlParameter("@PhotoAlbum",PhotoAlbum),
                    new SqlParameter("@Sex",Sex),
                    new SqlParameter("@Interest",Interest),
                    new SqlParameter("@AttachList",AttachList),

                    returnValue
                 };

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                isOk = returnValue.Value.ToString();
            }
            catch (Exception ex)
            {
                isOk = ex.Message;

            }

            return isOk;
        }
        #endregion


    }
}