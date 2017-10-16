using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Model;
using System.Data;
using System.Text;
using KingTop.Web.Admin;

namespace KingTop.WEB.SysAdmin.HR
{
    public partial class HRJobEdit : KingTop.Web.Admin.AdminPage
    {
        KingTop.Modules.BLL.HRJob objBll = new Modules.BLL.HRJob();
        DataTable jobTypeDT = null;
        DataTable workUnitDT = null;
        DataTable workPlaceDT = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDll();               
                PageInit();
            }
        }



        private void BindDll()
        {
            Modules.BLL.HRJobCate objBll = new Modules.BLL.HRJobCate();
            jobTypeDT = objBll.dt("A", SiteID);
            workUnitDT = objBll.dt("B", SiteID);
            workPlaceDT = objBll.dt("C", SiteID);
            DropDownListBind(ddlJobType, oparate(jobTypeDT));
            DropDownListBind(ddlWorkUnit, oparate(workUnitDT));
            DropDownListBind(ddlWorkPlace, oparate(workPlaceDT));
            KingTop.Common.ControlUtils.DropDownDataBind(ddlDegreeFrom, "DegreeFrom");
        }

        private void DropDownListBind(DropDownList ddl, DataTable dt)
        {
            ddl.DataSource = dt;
            ddl.DataTextField = "Title";
            ddl.DataValueField = "ID";
            ddl.DataBind();

            ListItem ls = new ListItem("--请选择--", "");//追加一项
            ddl.Items.Insert(0, ls);
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

        #region 页面初始化        private void PageInit()
        {
            if (this.Action == "EDIT")
            {
                KingTop.Modules.Entity.HRJob model = new Modules.Entity.HRJob();
                model = objBll.GetItemByID(Utils.ParseInt(this.ID,0));
                BtnSave.Text = Utils.GetResourcesValue("Common", "Update");
                txtTitle.Value = model.Title;
                hidLogTitle.Value = model.Title;
                ddlJobType.SelectedValue = model.JobType;
                ddlWorkPlace.SelectedValue = model.WorkPlace;
                ddlWorkUnit.SelectedValue = model.WorkUnit;
                txtSalary.Value = model.Salary;
                ddlDegreeFrom.SelectedValue = model.Degree;
                txtAge.Value = model.Age.ToString();
                txtExperience.Value = model.Experience;
                txtNumber.Value = model.Number.Trim ();
                txtEMail.Value = model.EMail;
                txtPublishDate.Value = string.IsNullOrEmpty(model.PublishDate.ToString()) == true ? "" : DateTime.Parse(model.PublishDate.ToString ()).ToString("yyyy-MM-dd");
                txtEndDate.Value = string.IsNullOrEmpty(model.EndDate.ToString()) == true ? "" : DateTime.Parse(model.EndDate.ToString()).ToString("yyyy-MM-dd");
                Editor1.Content = model.WorkDuty;
                Editor2.Content = model.Qualifications;
                Editor3.Content = model.Welfare;
                hidID.Value = this.ID;
                txtPageTitle.Text = model.PageTitle;
                txtMetaDesc.Text = model.MetaDescript;
                txtKeyWords.Text = model.MetaKeyword;
            }
        }
        #endregion
 
        #region 按钮事件
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string id = hidID.Value;
            string returnMsg = "";
            KingTop.Modules.Entity.HRJob mode = objBll.GetPostData;
            mode.ID = Utils.ParseInt(id, 0);
            mode.NodeCode = NodeCode;
            mode.SiteID = SiteID;
            mode.AddMan = GetLoginAccountName();
            mode.WorkDuty = Editor1.Content;
            mode.Qualifications = Editor2.Content;
            mode.Welfare = Editor3.Content;

            string listUrl = string.Empty;
            string addUrl = string.Empty;
            string updateUrl = string.Empty;
                
            if (Action == "EDIT")
            {
                #region 修改用户组
                // 权限验证，是否具有修改权限
                if (IsHaveRightByOperCode("Edit"))
                {
                    returnMsg = objBll.Edit(mode);
                    int reNum = Utils.ParseInt(returnMsg, 0);
                    string logTitle = Request.Form["hidLogTitle"];
                    listUrl = "hrjoblist.aspx?" + StrPageParams;
                    addUrl = "hrjobedit.aspx?Action=New&NodeCode="+NodeCode;
                    updateUrl = "hrjobedit.aspx?" + KingTop.Common.Utils.GetUrlParams();
                    if (logTitle != Request.Form["txtTitle"])
                    {
                        logTitle = logTitle + " 为 " + Request.Form["txtTitle"];
                    }

                    if (reNum == 1)
                    {
                        WriteLog("修改：" + logTitle+" 成功", "", 2);
                        //$(function () { showEditMessage("职位", "hrjoblist.aspx?NodeCode=<%=NodeCode%>", "hrjobedit.aspx?<%=KingTop.Common.Utils.GetUrlParams() %>", "") });
                        Utils.RunJavaScript(this, "type=1;title='" + Request.Form["txtTitle"].Replace("'", "\\'") + "';$(function () { showEditMessage(\"职位\",\""+listUrl+"\",\""+addUrl+"\",\""+updateUrl+"\");})");

                    }
                    else
                    {
                        if (returnMsg == "0")
                            returnMsg = "未修改任何数据";

                        WriteLog("修改：" + logTitle + " 失败", returnMsg, 3);
                        Utils.RunJavaScript(this, "type=2;errmsg='" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "';$(function () { showEditMessage(\"职位\",\"" + listUrl + "\",\"" + addUrl + "\",\"" + updateUrl + "\");})");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有编辑权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }
            else
            {
                #region 新增用户组
                //判断是否有权限
                if (IsHaveRightByOperCode("New"))
                {
                    string[] reMsg = objBll.CreateItem(mode);
                    returnMsg = reMsg[0];
                    id = reMsg[1];
                    listUrl = "hrjoblist.aspx?NodeCode=" + NodeCode;
                    addUrl = "hrjobedit.aspx?" + KingTop.Common.Utils.GetUrlParams(); ;
                    updateUrl = "hrjobedit.aspx?NodeCode=" + NodeCode+"&Action=Edit&ID="+id;
                    int reNum = Utils.ParseInt(returnMsg, 0);

                    if (reNum == 1)
                    {
                        WriteLog("添加:" + mode.Title + " 成功！", "", 2);
                        Utils.RunJavaScript(this, "type=0;title='" + mode.Title.Replace("'", "\\'") + "';$(function () { showEditMessage(\"职位\",\"" + listUrl + "\",\"" + addUrl + "\",\"" + updateUrl + "\");})");
                    }
                    else if (reNum == -100)
                    {
                        WriteLog("添加：" + mode.Title + " 失败！", "参数不足", 3); //写日志
                        Utils.RunJavaScript(this, "type=2;errmsg='参数不足！';$(function () { showEditMessage(\"职位\",\"" + listUrl + "\",\"" + addUrl + "\",\"" + updateUrl + "\");})");
                    }
                    else
                    {
                        WriteLog("添加：" + mode.Title + " 失败！", returnMsg, 2);// 写入操作日志
                        Utils.RunJavaScript(this, "type=2;errmsg='" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "';$(function () { showEditMessage(\"职位\",\"" + listUrl + "\",\"" + addUrl + "\",\"" + updateUrl + "\");})");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有新增权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }

        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRJobList.aspx?NodeCode=" + NodeCode+"&cateType="+Request.QueryString ["cateType"]);
        }
    }
}
