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
    public partial class HRResumeCountList : AdminPage
    {
        #region 初始参数
        Modules.BLL.HRResume objhr = new Modules.BLL.HRResume();
        protected DataTable dtCount = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //第一次进入该页面时，修改职位表的最后浏览简历时间
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
            if (ddlStartDegree.Items.Count == 0)
                KingTop.Common.ControlUtils.DropDownDataBind(ddlStartDegree, "DegreeFrom");

            if (ddlEndDegree.Items.Count == 0)
                KingTop.Common.ControlUtils.DropDownDataBind(ddlEndDegree, "DegreeFrom");

        }


        #region 数据分页
        // 分页控件数据绑定
        private void SplitDataBind()
        {
            Dictionary<string, string> dicWhere = new Dictionary<string, string>();
            Dictionary<string, string> dicWhere2 = new Dictionary<string, string>();
            Pager p = new Pager();

            string title = Server.UrlDecode(Request.QueryString["K"]);
            string workType = Server.UrlDecode(Request.QueryString["T"]);
            string gender = Server.UrlDecode(Request.QueryString["G"]);
            string startDegree = Server.UrlDecode(Request.QueryString["SD"]);

            string endDegree = Server.UrlDecode(Request.QueryString["ED"]);
            string startWorkYear = Server.UrlDecode(Request.QueryString["SW"]);
            string endWorkYear = Server.UrlDecode(Request.QueryString["EW"]);
            string startAge = Server.UrlDecode(Request.QueryString["SA"]);
            string endAge = Server.UrlDecode(Request.QueryString["EA"]);
            string isRead = Request.QueryString["IsR"];
            string jobid = Request.QueryString["jobid"];
            string resumetype = Request.QueryString["resumetype"];
            string st = Request.QueryString["tm"];
            if (!string.IsNullOrEmpty(jobid))
            {
                dicWhere.Add("JobID", jobid);
            }
            if (!string.IsNullOrEmpty(resumetype))
            {
                dicWhere.Add("ResumeType", resumetype);
            }
            if (!string.IsNullOrEmpty(title))
            {
                dicWhere.Add("Title", title + "|" + workType);
                txtTitle.Text = title;
                if(workType=="2")
                {
                    dicWhere2.Add("Title", title);
                }
            }
            KeyWorkType.SelectedIndex = Utils.ParseInt(workType, 1) - 1;

            if (!string.IsNullOrEmpty(gender))
            {
                dicWhere.Add("gender", gender);
                int index = 0;
                if (gender == "男")
                    index = 1;
                else if (gender == "女")
                    index = 2;
                selGender.SelectedIndex = index;
            }

            if (!string.IsNullOrEmpty(endDegree))
            {
                dicWhere.Add("endDegree", endDegree);
                ddlEndDegree.SelectedValue = endDegree;
            }

            if (!string.IsNullOrEmpty(startDegree))
            {
                dicWhere.Add("startDegree", startDegree);
                ddlStartDegree.SelectedValue = startDegree;
            }

            if (!string.IsNullOrEmpty(startWorkYear))
            {
                dicWhere.Add("StartWorkYear", startWorkYear);
                txtStartWorkYear.Text = startWorkYear;
            }

            if (!string.IsNullOrEmpty(endWorkYear))
            {
                dicWhere.Add("endWorkYear", endWorkYear);
                txtEndWorkYear.Text = endWorkYear;
            }

            if (!string.IsNullOrEmpty(startAge))
            {
                dicWhere.Add("startAge", startAge);
                txtStartAge.Text = startAge;
            }

            if (!string.IsNullOrEmpty(endAge))
            {
                dicWhere.Add("endAge", endAge);
                txtEndAge.Text = endAge;
            }
            if (!string.IsNullOrEmpty(isRead))
            {
                dicWhere.Add("IsRead", isRead);
                selIsRead.SelectedIndex = Utils.ParseInt(isRead,0) + 1;
            }
            if (string.IsNullOrEmpty(st))
            {
                st = "1900-1-1";
            }
            dicWhere.Add("LastViewResume", st);

            dtCount = objhr.GetApplyCount(dicWhere, SiteID);

            dicWhere2.Add("SiteID", SiteID.ToString ());
            Modules.BLL.HRJob hrbll = new Modules.BLL.HRJob();
            p.DicWhere = dicWhere2;
            p.Aspnetpage = AspNetPager1;
            p.RptControls = rptInfo;
            hrbll.PageData(p);

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
                JavascriptMsg("提示信息", "操作成功！");
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
                OnDel(id.Split ('_')[0]);
            }
        }
        #endregion

        #region 根据jobid 和分类状态获得统计数据
        public string GetCount(string jobid, string status, string ty)
        {
            string re="0";
            if (ty == "0")  //分类
            {
                DataRow[] dr = dtCount.Select("ID=" + jobid + " AND st=" + status);
                if (dr.Length > 0)
                {
                    re = dr[0]["co"].ToString();
                }
            }
            else if (ty == "1")
            {
                DataRow[] dr = dtCount.Select("ID=" + jobid);
                if (dr.Length > 0)
                {
                    re = dr[0]["newco"].ToString();
                }
            }
            else
            {
                int cou = 0;
                DataRow[] dr = dtCount.Select("ID=" + jobid);
                if (dr.Length > 0)
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        cou += Utils.ParseInt(dr[i]["co"].ToString(),0);
                    }
                }
                re = cou.ToString();
            }

            return re;
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string title = Server.UrlEncode (Request.Form["txtTitle"]);
            string WorkType = Server.UrlEncode(Request.Form["KeyWorkType"]);
            string gender = Server.UrlEncode(Request.Form["selGender"]);
            string startDegree = Server.UrlEncode(Request.Form["ddlStartDegree"]);
            string endDegree = Server.UrlEncode(Request.Form["ddlEndDegree"]);
            string startWorkYear = Server.UrlEncode(Request.Form["txtStartWorkYear"]);
            string endWorkYear = Server.UrlEncode(Request.Form["txtEndWorkYear"]);
            string isRead = Request.Form["selIsRead"];

            string startAge = Server.UrlEncode(Request.Form["txtStartAge"]);
            string endAge = Server.UrlEncode(Request.Form["txtEndAge"]);
            string jobid = Request.QueryString["jobid"];
            string resumetype = Request.QueryString["resumetype"];
            string tm = Request.QueryString["tm"];
            Response.Redirect("HRResumeCountList.aspx?NodeCode=" + NodeCode + "&tm=" + tm + "&jobid=" + jobid + "&resumetype=" + resumetype + "&K=" + title + "&T=" + WorkType + "&G=" + gender + "&SD=" + startDegree + "&ED=" + endDegree + "&SW=" + startWorkYear + "&EW=" + endWorkYear + "&SA=" + startAge + "&EA=" + endAge + "&IsR=" + isRead);
        }

        public string GetUrlParam()
        {
            string urlParam = Utils.GetUrlParams().ToLower();
            string re = string.Empty;
            if (!string.IsNullOrEmpty(urlParam))
            {
                string[] arr = urlParam.Split('&');
                for (int i = 0; i < arr.Length; i++)
                {

                    if (arr[i].IndexOf("resumetype") == -1 && arr[i].IndexOf ("page")==-1)
                    {
                        if (!string.IsNullOrEmpty(re))
                            re += "&";

                        re += arr[i];
                    }
                }
            }
            return re;
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

            Response.Redirect("hrApplyJobedit.aspx?Action=New&NodeCode=" + NodeCode);
        }
    }
}