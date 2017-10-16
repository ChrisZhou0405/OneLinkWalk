using KingTop.Common;
using KingTop.Web.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.SysAdmin.Common
{
    public partial class Commonlist : AdminPage
    {
        public string id = string.Empty;
        public new string NodeCode = string.Empty;
        public string sortList = string.Empty;
        protected string backUrlParam = string.Empty;       // 返回时传递的参数
        protected BLL.Content.ControlManageList ctrManageList;
        protected string jsMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            NodeCode = KingTop.Common.Utils.CheckSql(Request.QueryString["NodeCode"]);
            ctrManageList = new BLL.Content.ControlManageList(hdnModelID.Value, hdnTableName.Value);
            if (!Page.IsPostBack)
            {
                IsHaveRight("binddata");
            }
        }

        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Keywords = Request.Form["Title"];
            Response.Redirect("Commonlist.aspx?NodeCode=" + NodeCode + "&IsFirst=1&k=" + Keywords + "");
            return;
        }
        #endregion

        #region 绑定数据分页，存储过程proc_Pager2005
        protected void PageData()
        {
            KingTop.Common.Pages page = new KingTop.Common.Pages();
            string strWhere = " isdel=0 and nodecode='" + NodeCode + "' ";
            string Keywords = Request.QueryString["k"];
            if (!string.IsNullOrEmpty(Keywords))
            {
                strWhere += " and title like '%" + Keywords + "%' ";
                Title.Value = Keywords;
            }           

            try
            {
                int recordCount = 0;
                DataSet ds = page.GetPageList("K_U_Common ", "*", "orders desc ", strWhere, Split.CurrentPageIndex, Split.PageSize, out recordCount, 1);
                Split.RecordCount = recordCount;
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        rptListInfo.DataSource = ds;
                        rptListInfo.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.AlertMessage(this, ex.Message, "系统提示");
            }
        }
        #endregion

        #region 分页
        protected void AspNetPager1_PageChanged(object src, EventArgs e)
        {
            PageData();
        }
        #endregion

        #region 批量删除
        protected void btndel_Click(object sender, EventArgs e)
        {
            IsHaveRight("del");

            try
            {
                #region 直接SQL处理，服务器控件CheckBox前台要设置显示
                //int count = 0;
                //for (int i = 0; i < this.rptListInfo.Items.Count; i++)
                //{
                //    string id = ((HiddenField)this.rptListInfo.Items[i].FindControl("hidId")).Value;
                //    CheckBox box = (CheckBox)this.rptListInfo.Items[i].FindControl("_chkID");
                //    if (box.Checked)
                //    {
                //        if (KingTop.Common.SQLHelper.ExcuteCommand("update K_U_Common set isdel=1 where id='" + id + "'"))
                //            count++;
                //    }
                //}
                //if (count > 0)
                //{
                //    rptListInfo.Controls.Clear();
                //    PageData();
                //}
                #endregion

                #region 存储过程处理proc_K_U_CommonSet
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_U_CommonSet";
                string tranType = "FDEL";
                string IDList = Request.Form["_chkID"];
                string SetValue = "1";
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@IDList",IDList),
                    new SqlParameter("@SetValue",SetValue),
                    returnValue
                    };
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                string isOk = returnValue.Value.ToString();

                if (isOk == "1")
                {
                    rptListInfo.Controls.Clear();
                    PageData();
                }
                #endregion
            }
            catch (Exception ex)
            {

                Utils.AlertMessage(this, ex.Message, "系统提示");
            }
        }
        #endregion

        #region 单行删除
        protected void rptListInfo_OnItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            IsHaveRight("del");
            try
            {
                #region 直接SQL处理
                //if (e.CommandName == "del")
                //{
                //    string id = ((HiddenField)e.Item.FindControl("hidId")).Value;
                //    if (KingTop.Common.SQLHelper.ExcuteCommand("update K_U_Common set isdel=1 where id='" + id + "'"))
                //    {
                //        WriteLog(GetLogValue(LogTitle, "Del", "", true), "", 2);    //写日志
                //        rptListInfo.Controls.Clear();
                //        PageData();
                //    }
                //}

                //if (e.Item.Controls.Count >= 0)
                //{
                //    string id = ((HiddenField)e.Item.FindControl("hidId")).Value;
                //    if (KingTop.Common.SQLHelper.ExcuteCommand("update K_U_Common set isdel=1 where id='" + id + "'"))
                //    {
                //        WriteLog(GetLogValue(LogTitle, "Del", "", true), "", 2);    //写日志
                //        rptListInfo.Controls.Clear();
                //        PageData();
                //    }
                //}                
                #endregion

                #region 存储过程处理proc_K_U_CommonSet
                if (e.CommandName == "del")
                {
                    SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                    returnValue.Direction = ParameterDirection.Output;

                    string cmdText = "proc_K_U_CommonSet";
                    string tranType = "FDEL";
                    string IDList = ((HiddenField)e.Item.FindControl("hidId")).Value;
                    string SetValue = "1";
                    SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@IDList",IDList),
                    new SqlParameter("@SetValue",SetValue),
                    returnValue
                    };
                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                    string isOk = returnValue.Value.ToString();

                    if (isOk == "1")
                    {
                        rptListInfo.Controls.Clear();
                        PageData();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

                Utils.AlertMessage(this, ex.Message, "系统提示");
            }
        }
        #endregion

        #region 通过审核
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            IsHaveRight("check");
            try
            {
                #region 直接SQL处理，服务器控件CheckBox前台要设置显示
                //int count = 0;
                //for (int i = 0; i < this.rptListInfo.Items.Count; i++)
                //{
                //    string id = ((HiddenField)this.rptListInfo.Items[i].FindControl("hidId")).Value;
                //    CheckBox box = (CheckBox)this.rptListInfo.Items[i].FindControl("_chkID");
                //    if (box.Checked)
                //    {
                //        if (KingTop.Common.SQLHelper.ExcuteCommand("update K_U_Common set flowstate=99 where id='" + id + "'"))
                //            count++;
                //    }                    
                //}
                //if (count > 0)
                //{
                //    rptListInfo.Controls.Clear();
                //    PageData();
                //}
                #endregion

                #region 存储过程处理proc_K_U_CommonSet
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_U_CommonSet";
                string tranType = "CHECK";
                string IDList = Request.Form["_chkID"];
                string SetValue = "99";
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@IDList",IDList),
                    new SqlParameter("@SetValue",SetValue),
                    returnValue
                    };
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                string isOk = returnValue.Value.ToString();

                if (isOk == "1")
                {
                    rptListInfo.Controls.Clear();
                    PageData();
                }
                #endregion

            }
            catch (Exception ex)
            {
                Utils.AlertMessage(this, ex.Message, "系统提示");
            }
        }
        #endregion

        #region 取消审核
        protected void btnCancelCheck_Click(object sender, EventArgs e)
        {
            IsHaveRight("cancelcheck");
            try
            {
                #region 直接SQL处理，服务器控件CheckBox前台要设置显示
                //int count = 0;
                //for (int i = 0; i < this.rptListInfo.Items.Count; i++)
                //{
                //    string id = ((HiddenField)this.rptListInfo.Items[i].FindControl("hidId")).Value;
                //    CheckBox box = (CheckBox)this.rptListInfo.Items[i].FindControl("_chkID");
                //    if (box.Checked)
                //    {
                //        if (KingTop.Common.SQLHelper.ExcuteCommand("update K_U_Common set flowstate=3 where id='" + id + "'"))
                //            count++;
                //    }
                //}
                //if (count > 0)
                //{
                //    rptListInfo.Controls.Clear();
                //    PageData();
                //}
                #endregion

                #region 存储过程处理proc_K_U_CommonSet
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_U_CommonSet";
                string tranType = "CANCELCHECK";
                string IDList = Request.Form["_chkID"];
                string SetValue = "3";
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@IDList",IDList),
                    new SqlParameter("@SetValue",SetValue),
                    returnValue
                    };
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                string isOk = returnValue.Value.ToString();

                if (isOk == "1")
                {
                    rptListInfo.Controls.Clear();
                    PageData();
                }
                #endregion
            }
            catch (Exception ex)
            {

                Utils.AlertMessage(this, ex.Message, "系统提示");
            }
        }
        #endregion

        #region 判断用户名是否有查看、编辑、添加、删除、审核、取消审核的权限
        protected void IsHaveRight(string action)
        {
            switch (action)
            {
                case "binddata":
                    {
                        if (!IsHaveRightByOperCode("Query"))
                        {
                            Utils.AlertMessage(this, "没有查看权限，请联系站点管理员！", "系统提示");
                            //jsMessage = "alertClose({msg:\"没有查看权限，请联系站点管理员！\",title:\"系统提示\"});";
                            return;
                            //Utils.RunJavaScript(this, "type=0;title='" + txtProName.Text.Replace("'", "\\'") + "';$(function () { showEditMessage(\"你没有查看权限，请联系站点管理员！\",\"" + listUrl + "\",\"" + addUrl + "\",\"" + updateUrl + "\");})");
                        }
                        else
                        {
                            PageData();
                        }
                        break;
                    }
                //添加
                case "new":
                    {
                        if (!IsHaveRightByOperCode("New"))
                        {
                            Utils.AlertMessage(this, "没有添加权限，请联系站点管理员！", "系统提示");
                            return;
                        }
                        else
                        {
                            //Add();
                        }
                        break;
                    }
                //编辑
                case "edit":
                    {
                        if (!IsHaveRightByOperCode("Edit"))
                        {
                            Utils.AlertMessage(this, "没有修改权限，请联系站点管理员！", "系统提示");
                            return;
                        }
                        else
                        {
                            //Edit();
                        }
                        break;
                    }
                //删除
                case "del":
                    {
                        if (!IsHaveRightByOperCode("Delete"))
                        {
                            Utils.AlertMessage(this, "没有删除权限，请联系站点管理员！", "系统提示");
                            return;
                        }
                        else
                        {
                            
                        }
                        break;
                    }
                //通过审核
                case "check":
                    {
                        if (!IsHaveRightByOperCode("Check"))
                        {
                            Utils.AlertMessage(this, "没有通过审核权限，请联系站点管理员！", "系统提示");
                            return;
                        }
                        else
                        {
                            
                        }
                        break;
                    }
                //取消审核
                case "cancelcheck":
                    {
                        if (!IsHaveRightByOperCode("CancelCheck"))
                        {
                            Utils.AlertMessage(this, "没有取消审核权限，请联系站点管理员！", "系统提示");
                            return;
                        }
                        else
                        {
                            
                        }
                        break;
                    }
            }
            //return;
        }
        #endregion

        #region 获得排序号
        public string GetSortList(string orders)
        {
            if (string.IsNullOrEmpty(sortList))
            {
                sortList = orders;
            }
            else
                sortList += "," + orders;

            return "";
        }
        #endregion
    }
}