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

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      陈顺
    创建时间： 2010年3月30日
    功能描述： 用户组编辑
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.WEB.SysAdmin.HR
{
    public partial class HRJobCateEdit : KingTop.Web.Admin.AdminPage
    {
        KingTop.Modules.BLL.HRJobCate objBll = new Modules.BLL.HRJobCate();
        DataTable dtJobCate=null;
        protected void Page_Load(object sender, EventArgs e)
        {
            checkCateType();
            if (!IsPostBack)
            {
                BindDll();               
                PageInit();
            }
        }



        private void BindDll()
        {
            oparate();
            HRJobCate.DataSource = dtJobCate.DefaultView;
            HRJobCate.DataTextField = "Title";
            HRJobCate.DataValueField = "ID";
            HRJobCate.DataBind();

            ListItem ls = new ListItem("请选择", "0");//追加一项
            this.HRJobCate.Items.Insert(0, ls);
        }

        #region 无限分类的用户组显示结构处理
        public void oparate()
        {
            dtJobCate = objBll.dt(Request.QueryString["cateType"],SiteID);
            string temp_str = "";
            int numCode = 0;
            string strCode = "";
            for (int i = 0; i < dtJobCate.Rows.Count; i++)
            {                strCode = dtJobCate.Rows[i]["ID"].ToString();

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
        }
        #endregion

        #region 页面初始化        private void PageInit()
        {
            if (this.Action == "EDIT")
            {
                KingTop.Modules.Entity.HRJobCate model = new Modules.Entity.HRJobCate();
                model = objBll.GetItemByID(this.ID);
                btnSource.Text = Utils.GetResourcesValue("Common", "Update");
                txtTitle.Text = model.Title;
                hidLogTitle.Value = model.Title;
                hdPCode.Value = model .ParentID ;
                hdNCode.Value =model .ID ;
                hdnID.Value = model.ID;
                HRJobCate.SelectedValue = model.ParentID;
                Orders.Text = model.Orders.ToString ();
                Intro.Text = model.Intro;
                HRJobCate.Enabled = false;
            }
        }
        #endregion
 
        #region 按钮事件
        protected void btnSource_Click(object sender, EventArgs e)
        {
            string id = hdnID.Value;
            string returnMsg = "";
            KingTop.Modules.Entity.HRJobCate mode = objBll.GetPostData;
            checkCateType();
            if (Action == "EDIT")
            {
                #region 修改用户组
                // 权限验证，是否具有修改权限
                if (IsHaveRightByOperCode("Edit"))
                {
                    mode.ID = id;
                    returnMsg = objBll.Edit(mode);
                    int reNum = Utils.ParseInt(returnMsg, 0);
                    string logTitle = Request.Form["hidLogTitle"];
                    if (logTitle != txtTitle.Text)
                    {
                        logTitle = logTitle + " 为 " + txtTitle.Text;
                    }

                    if (reNum == 1)
                    {
                        WriteLog("修改类别：" + logTitle+" 成功", "", 2);
                        Utils.RunJavaScript(this, "type=1;title='" + txtTitle.Text.Replace("'", "\\'") + "';");

                    }
                    else
                    {
                        if (returnMsg == "0")
                            returnMsg = "未修改任何数据";

                        WriteLog("修改类别：" + logTitle + " 失败", returnMsg, 3);
                        Utils.RunJavaScript(this, "type=2;errmsg='" + returnMsg.Replace("'", "\\'").Replace("\r\n","<br>") + "';");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有编辑类别的权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }
            else
            {
                #region 新增用户组
                //判断是否有权限
                if (IsHaveRightByOperCode("New"))
                {
                    mode.SiteID = SiteID;
                    mode.NodeCode = NodeCode;
                    mode.CateType = Request["CateType"];
                    mode.ParentID = Request.Form["HRJobCate"];
                    string[] reMsg = objBll.CreateItem(mode);
                    returnMsg = reMsg[0];
                    int reNum = Utils.ParseInt(returnMsg, 0);

                    if (reNum == 1)
                    {
                        WriteLog("添加类别:" + txtTitle.Text + " 成功！", "", 2);
                        Utils.RunJavaScript(this, "type=0;title='" + txtTitle.Text.Replace("'", "\\'") + "';");
                    }
                    else if (reNum == -100)
                    {
                        WriteLog("添加类别：" + txtTitle.Text + " 失败！", "参数不足", 3); //写日志
                        Utils.RunJavaScript(this, "type=2;errmsg='参数不足！';");
                    }
                    else
                    {
                        WriteLog("添加类别：" + txtTitle.Text + " 失败！", returnMsg, 2);// 写入操作日志
                        Utils.RunJavaScript(this, "type=2;errmsg='" + returnMsg.Replace("'", "\\'").Replace("\r\n","<br>") + "';");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有新增类别的权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }

        }
        #endregion

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HRJobCateList.aspx?NodeCode=" + NodeCode+"&cateType="+Request.QueryString ["cateType"]);
        }

        void checkCateType()
        {
            if(string.IsNullOrEmpty (Request["CateType"]))
            {
                Response.Write ("<div style='width:100%'><div style='padding-top:50;text-align:center'>未取到Request[\"CateType\"]参数或者表单的值，此值不能为空，一般在url传入，请确认该模块是否配置正确</div></div>");
                Response.End ();
            }
        }

    }
}
