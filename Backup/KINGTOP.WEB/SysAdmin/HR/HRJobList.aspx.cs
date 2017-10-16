using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Modules;
using System.Data;
using KingTop.Web.Admin;
using System.Text;

namespace KingTop.WEB.SysAdmin.HR
{
    public partial class HRJobList : AdminPage
    {
        #region 初始参数
        DataTable jobTypeDT = null;
        DataTable workUnitDT = null;
        DataTable workPlaceDT = null;
        Modules.BLL.HRJob objhr = new Modules.BLL.HRJob();
        public string sortList = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDll();
                PageInit();
            }
        }

        private void PageInit()
        {
            //数据绑定
            SplitDataBind();
            Utils.SetVisiteList(SystemConst.COOKIES_PAGE_KEY, Session.SessionID, Utils.GetUrlParams().Replace("&", "|"), SystemConst.intMaxCookiePageCount); //把当前url全部参数存入cookie中      
            SetRight(this.Page, rptInfo);
        }

        private void BindDll()
        {
            Modules.BLL.HRJobCate objBll=new Modules.BLL.HRJobCate ();
            jobTypeDT=objBll.dt ("A",SiteID);
            workUnitDT=objBll .dt("B",SiteID);
            workPlaceDT=objBll.dt ("C",SiteID);
            DropDownListBind(ddlJobType, oparate(jobTypeDT));
            DropDownListBind(ddlWorkUnit, oparate(workUnitDT));
            DropDownListBind(ddlWorkPlace, oparate(workPlaceDT));
        }

        private void DropDownListBind(DropDownList ddl, DataTable dt)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "Title";
            ddl.DataValueField = "ID";
            ddl.DataBind();

            ListItem ls = new ListItem("请选择", "");//追加一项
            ddl.Items.Insert(0, ls);
        }
        public string GetCateTitle(string id, string cateType)
        {
            if (string.IsNullOrEmpty(id))
                return "";

            string reMsg = "";
            DataRow[] dr=null;
            switch (cateType)
            {
                case "A":
                    dr = jobTypeDT.Select("ID='" + id + "'");
                    break;
                case "B":
                    dr = workUnitDT.Select("ID='" + id + "'");
                    break;
                case "C":
                    dr = workPlaceDT.Select("ID='" + id + "'");
                    break;
            }
            if (dr.Length > 0)
            {
                reMsg = dr[0]["Title"].ToString();
            }

            return reMsg;
        }
        #region 无限分类的用户组显示结构处理
        public DataTable oparate(DataTable dt)
        {
            DataTable dtJobCate = dt.Copy();
            string temp_str = "";
            int numCode = 0;
            string strCode = "";
            for (int i = 0; i < dtJobCate.Rows.Count; i++)
            {
                strCode = dtJobCate.Rows[i]["ID"].ToString();

                numCode = strCode.Length / 3;
                if (numCode == 1)
                { }
                else
                {
                    for (int p = 1; p < numCode; p++)
                    {
                        if (p == numCode - 1)
                        {
                            temp_str = temp_str + "├";
                        }
                        else
                        {
                            temp_str = temp_str + "　";
                        }
                    }
                }
                dtJobCate.Rows[i]["Title"] = temp_str + dtJobCate.Rows[i]["Title"].ToString();
                temp_str = "";
            }

            return dtJobCate;
        }
        #endregion

        #region 数据分页
        // 分页控件数据绑定
        private void SplitDataBind()
        {
            Dictionary<string, string> dicWhere = new Dictionary<string, string>();
            Pager p = new Pager();
            string title = Server.UrlDecode(Request.QueryString["Title"]);
            string jobType = Server.UrlDecode(Request.QueryString["JobType"]);
            string workUnit = Server.UrlDecode(Request.QueryString["WorkUnit"]);
            string workPlace = Server.UrlDecode(Request.QueryString["WorkPlace"]);
            if (!string.IsNullOrEmpty(title))
            {
                dicWhere.Add("Title", title);
                txtTitle.Text = title;
            }
            if (!string.IsNullOrEmpty(jobType))
            {
                dicWhere.Add("JobType", jobType);
                ddlJobType.SelectedValue = jobType;
            }
            if (!string.IsNullOrEmpty(workUnit))
            {
                dicWhere.Add("WorkUnit", workUnit);
                ddlWorkUnit.SelectedValue = workUnit;
            }
            if (!string.IsNullOrEmpty(workPlace))
            {
                dicWhere.Add("WorkPlace", workPlace);
                ddlWorkPlace.SelectedValue = workPlace;
            }

            dicWhere.Add("SiteID", this.SiteID.ToString());
            p.DicWhere = dicWhere;
            p.Aspnetpage = AspNetPager1;
            p.RptControls = rptInfo;
            objhr.PageData(p);
        }

        #endregion

        #region 删除
        public void HRJobList_Del(object sender, CommandEventArgs e)
        {
            if (!base.IsHaveRightByOperCode("Delete"))
            {
                JavascriptMsg("提示信息", "删除操作失败，没有权限！");
                return;
            }
            OnDel(e.CommandArgument.ToString());
        }

        void OnDel(string id)
        {
            string returnMsg = "1";   // 事务返回信息
            
            returnMsg = objhr.Delete(id);

            if (Utils.ParseInt(returnMsg,0) ==0)  //操作失败
            {
                WriteLog("删除：" + LogTitle+" 失败", returnMsg, 3); //写日志
                JavascriptMsg("提示信息", "操作失败："+returnMsg.Replace ("'","\\'").Replace ("\r\n","<br>")+"，请重试！");
            }
            else //操作成功
            {
                WriteLog("删除：" + LogTitle + " 成功", "", 2); //写日志
                //JavascriptMsg("提示信息", "操作成功！");
            }
            BindDll();
            PageInit();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (!base.IsHaveRightByOperCode("Delete"))
            {
                JavascriptMsg("提示信息", "删除操作失败，没有权限！");
                return;
            }
            string id = Request.Form["chkId"];
            if (!string.IsNullOrEmpty(id))
            {
                OnDel(id);
            }
        }
        #endregion

        #region 审核
        void OnCheck(string id,int CheckValue)
        {
            string returnMsg = "1";   // 事务返回信息
            Modules.BLL.HRJob objhr = new Modules.BLL.HRJob();
            returnMsg = objhr.Check(id, CheckValue);

            if (Utils.ParseInt(returnMsg, 0) == 0)  //操作失败
            {
                WriteLog("审核职位：" + LogTitle + "失败", returnMsg, 3); //写日志
                JavascriptMsg("提示信息", "操作失败：" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "，请重试！");
            }
            else //操作成功
            {
                WriteLog("审核职位：" + LogTitle + "成功", "", 2); //写日志
                //JavascriptMsg("提示信息", "操作成功！");
            }
            BindDll();
            PageInit();
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (!base.IsHaveRightByOperCode("Check"))
            {
                JavascriptMsg("提示信息", "审核操作失败，没有权限！");
                return;
            }
            string id = Request.Form["chkId"];
            if (!string.IsNullOrEmpty(id))
            {
                OnCheck(id,99);
            }
        }

        protected void btnCancelCheck_Click(object sender, EventArgs e)
        {
            if (!base.IsHaveRightByOperCode("CancelCheck"))
            {
                JavascriptMsg("提示信息", "取消审核操作失败，没有权限！");
                return;
            }
            string id = Request.Form["chkId"];
            if (!string.IsNullOrEmpty(id))
            {
                OnCheck(id, 0);
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string title = Server.UrlEncode (Request.Form["txtTitle"]);
            string jobType = Server.UrlEncode(Request.Form["ddlJobType"]);
            string workUnit = Server.UrlEncode(Request.Form["ddlWorkUnit"]);
            string workPlace = Server.UrlEncode(Request.Form["ddlWorkPlace"]);
            Response.Redirect ("HrJobList.aspx?NodeCode="+NodeCode +"&title="+title+"&jobType="+jobType +"&workUnit="+workUnit +"&workPlace="+workPlace );
        }

        private void JavascriptMsg(string msgTitle, string msgContent)
        {
            Utils.RunJavaScript(this, "nmsgtitle='" + msgTitle + "';nmsgcontent='" + msgContent + "';");
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if (!base.IsHaveRightByOperCode("New"))
            {
                JavascriptMsg("提示信息", "新增操作失败，没有权限！");
                return;
            }
            Response.Redirect("hrjobedit.aspx?Action=New&NodeCode=" + NodeCode);
        }

        public string GetSortList(string orders)
        {
            if (string.IsNullOrEmpty(sortList))
            {
                sortList = orders;
            }
            else
            {
                sortList += "," + orders;
            }

            return null;
        }
    }
}