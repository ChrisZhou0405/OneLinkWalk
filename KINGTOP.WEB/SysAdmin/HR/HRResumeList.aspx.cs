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
    public partial class HRResumeList : AdminPage
    {
        #region 初始参数
        Modules.BLL.HRResume objhr = new Modules.BLL.HRResume();
        public string resumeDetailPath = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            resumeDetailPath = "/" + (string.IsNullOrEmpty(SiteDir) ? "" : SiteDir + "/") + "hr/resumedetail.aspx";
            if (!Page.IsPostBack)
            {
                SaveMemo();
                BindDll();
                PageInit();
            }
        }

        private void SaveMemo()
        {
            string reMsg = "no";
            if (Request.Form["ac"] == "memo")
            {
                string memo = Request.Form["memo"];
                if (!string.IsNullOrEmpty(memo))
                {
                    memo = memo.Replace("'", "’");
                }
                reMsg=objhr.UpdateMemo(Request.Form["id"], memo);
                if (Utils.IsNumber(reMsg))
                {
                    reMsg = "ok";
                }
                else
                {
                    WriteLog("简历备注失败：" + reMsg, reMsg, 3); //写日志
                    reMsg = "error";
                }

                //Response.Write("{\"result\":\""+reMsg+"\"}");
                Response.Write(reMsg);
                Response.End();
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
            if(ddlStartDegree.Items.Count ==0)
                KingTop.Common.ControlUtils.DropDownDataBind(ddlStartDegree, "DegreeFrom");

            if(ddlEndDegree.Items .Count ==0)
                KingTop.Common.ControlUtils.DropDownDataBind(ddlEndDegree, "DegreeFrom");

            KingTop.Modules.BLL.HRJob objjob = new Modules.BLL.HRJob();
            //DataTable dt = objjob.dt(SiteID);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    selJob.Items.Add(new ListItem(dt.Rows[i]["Title"].ToString(), dt.Rows[i]["ID"].ToString()));
            //}

        }


        #region 数据分页
        // 分页控件数据绑定
        private void SplitDataBind()
        {
            Dictionary<string, string> dicWhere = new Dictionary<string, string>();
            Pager p = new Pager();

            string FilePath = Server.MapPath("~/" + SiteDir + "/config/HR.config");
            resumeDetailPath = Utils.XmlRead(FilePath, "HRConfig/ResumeDetailFilePath", "");
            if (!string.IsNullOrEmpty(resumeDetailPath))
            {
                resumeDetailPath = resumeDetailPath.Replace("{SiteDir}", SiteDir).Replace("//", "/");
            }

            string title = Server.UrlDecode(Request.QueryString["K"]);
            string workType = Server.UrlDecode(Request.QueryString["T"]);
            string gender = Server.UrlDecode(Request.QueryString["G"]);
            string startDegree = Server.UrlDecode(Request.QueryString["SD"]);

            string endDegree = Server.UrlDecode(Request.QueryString["ED"]);
            string startWorkYear = Server.UrlDecode(Request.QueryString["SW"]);
            string endWorkYeare = Server.UrlDecode(Request.QueryString["EW"]);
            string startAge = Server.UrlDecode(Request.QueryString["SA"]);
            string endAge = Server.UrlDecode(Request.QueryString["EA"]);

            if (!string.IsNullOrEmpty(title))
            {
                dicWhere.Add("Title", title + "|" + workType);
                txtTitle.Text = title;
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

            if (!string.IsNullOrEmpty(endWorkYeare))
            {
                dicWhere.Add("endWorkYeare", endWorkYeare);
                txtEndWorkYear.Text = endWorkYeare;
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

        #region 转移
        void OnMove(string id,string CheckValue)
        {
            string returnMsg = "1";   // 事务返回信息
            returnMsg = objhr.Move (id, CheckValue);

            if (Utils.ParseInt(returnMsg, 0) == 0)  //操作失败
            {
                WriteLog("转移：" + LogTitle + "失败", returnMsg, 3); //写日志
                JavascriptMsg("提示信息", "操作失败：" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "，请重试！");
            }
            else //操作成功
            {
                WriteLog("转移：" + LogTitle + "成功", "", 2); //写日志
                //JavascriptMsg("提示信息", "操作成功！");
            }
            BindDll();
            PageInit();
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            if (!base.IsHaveRightByOperCode("Check"))
            {
                JavascriptMsg("提示信息", "转移操作失败，没有权限！");
                return;
            }
            string id = Request.Form["chkId"];
            string moveValue = Request.Form["selJob"];
            if (!string.IsNullOrEmpty(id))
            {
                OnMove(id,moveValue);
            }
        }
        #endregion

        #region 分类
        void OnSetType(string id, string CheckValue)
        {
            string returnMsg = "1";   // 事务返回信息
            returnMsg = objhr.SetType(id, CheckValue);

            if (Utils.ParseInt(returnMsg, 0) == 0)  //操作失败
            {
                WriteLog("分类：" + LogTitle + "失败", returnMsg, 3); //写日志
                JavascriptMsg("提示信息", "操作失败：" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "，请重试！");
            }
            else //操作成功
            {
                WriteLog("分类：" + LogTitle + "成功", "", 2); //写日志
                //JavascriptMsg("提示信息", "操作成功！");
            }
            BindDll();
            PageInit();
        }
        protected void btnSetType_Click(object sender, EventArgs e)
        {
            if (!base.IsHaveRightByOperCode("CancelCheck"))
            {
                JavascriptMsg("提示信息", "分类操作失败，没有权限！");
                return;
            }
            string id = Request.Form["chkId"];
            string typeValue = Request.Form["selType"];
            if (!string.IsNullOrEmpty(id))
            {
                OnSetType(id, typeValue);
            }
        }
        #endregion

        #region 获得分类
        protected string GetType(string status)
        {
            string reMsg = "未分类";
            //0=初始、1=一般、2=优秀、3=面试、4=录用、10=不合格、11=回收站
            switch (status)
            {
                case "1":
                    reMsg = "一般";
                    break;
                case "2":
                    reMsg = "优秀";
                    break;
                case "3":
                    reMsg = "面试";
                    break;
                case "4":
                    reMsg = "录用";
                    break;
                case "10":
                    reMsg = "不合格";
                    break;
                case "11":
                    reMsg = "回收站";
                    break;
            }

            return reMsg;

        }
        #endregion

        #region 根据出生日期获得年龄
        protected string GetAge(string date)
        {
            string age="";
            if (!string.IsNullOrEmpty(date))
            {
                age = (DateTime.Now.Year - Convert.ToDateTime(date).Year).ToString (); 
            }

            return age;
        }
        #endregion

        #region 获得学历
        protected string GetDegree(string degree)
        {
            KingTop.Common.OptionsDictionary od = new OptionsDictionary("degreefrom");
            return od.getDictionaryValue(degree);
        }
        #endregion

        #region 获得月薪
        protected string GetSalary(string salary)
        {
            if (string.IsNullOrEmpty(salary) || salary == "0")
            {
                return "面议";
            }

            return salary;
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
            string endWorkYeare = Server.UrlEncode(Request.Form["txtEndWorkYear"]);

            string startAge = Server.UrlEncode(Request.Form["txtStartAge"]);
            string endAge = Server.UrlEncode(Request.Form["txtEndAge"]);

            Response.Redirect("HrResumeList.aspx?NodeCode=" + NodeCode + "&K=" + title + "&T=" + WorkType + "&G=" + gender + "&SD=" + startDegree + "&ED=" + endDegree + "&SW=" + startWorkYear + "&EW=" + endWorkYeare + "&SA=" + startAge + "&EA=" + endAge);
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

            Response.Redirect("hrresumeedit.aspx?Action=New&NodeCode=" + NodeCode);
        }
    }
}