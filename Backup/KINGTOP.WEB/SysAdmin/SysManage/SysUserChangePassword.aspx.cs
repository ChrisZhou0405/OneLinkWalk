using System;
using System.Data;
using System.Web.UI;
using KingTop.BLL.SysManage;
using KingTop.Common;
using KingTop.Web.Admin;
using System.Configuration;
using System.Xml;

/*==========================================
Copyright (C) 2010 华强北在线

作者:陈顺
创建时间：2010-03-27
功能描述：修改密码

更新日期        更新人      更新原因/内容
--=========================================*/

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class SysUserChangePassword : AdminPage
    {
        #region  URL参数
        private string _userid = "0";
        public string UserID
        {
            get
            {
                if (this._userid == "0")
                {
                    this._userid = Utils.ReqUrlParameter("UserId");
                    // 参数格式验证，非法则重置为空字符串

                }
                return this._userid;
            }
        }
        #endregion
        //用户逻辑类

        Account BllAccount = new Account();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                PageInit();
                BtnBack.Attributes.Add("onclick", "location.href='SysUserList.aspx"+StrPageParams2("sysuserlist.aspx",Request.QueryString["NodeCode"])+"'");
            }
        }
        private void PageInit()
        {
            string strUserID = UserID;
            if (strUserID == "" || strUserID == null)
            {
                strUserID = GetLoginAccountId();
                BtnBack.Visible = false;
            }
            else
            {
                txtAccountPwd.Visible = false;
                dlOldPwd.Visible = false;
            }
            DataTable dt = BllAccount.GetList("ONE", Utils.getOneParams(strUserID));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtAccountName.Text = dr["UserName"].ToString();
                    //hiddenPwd.Value = dr["PassWord"].ToString();
                }
            }
            if (strUserID == "0")
            {
                txtAccountName.Text = "admin";
            }
            txtAccountName.Enabled = false;
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SysUserList.aspx"+StrPageParams2("sysuserlist.aspx",Request.QueryString["NodeCode"]));
        }

        //修改web.config
        public void UpdateWebConfig_appSettings(string key, string value)
        {

            string filename = Server.MapPath("/") + @"\web.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            XmlNodeList _node2 = doc.SelectSingleNode("configuration/appSettings").ChildNodes;
            for (int i = 0; i < _node2.Count; i++)
            {
                if (_node2.Item(i).Name.ToLower() == "add")
                {
                    if (_node2.Item(i).Attributes["key"].InnerXml.ToLower() == key.ToLower())
                    {
                        _node2.Item(i).Attributes["value"].Value = value;
                        try
                        {
                            doc.Save(filename);
                        }
                        catch (Exception ex)
                        {
                            Utils.RunJavaScript(this, "alert({msg:'修改失败，原因是web.config文件只读',title:'提示信息'})");
                        }

                        return;
                    }
                }
            }

            Utils.RunJavaScript(this, "alert({msg:'修改失败，未找到相关节点',title:'提示信息'})");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string returnMsg = string.Empty;
            if (GetLoginAccountId() == "0" && string.IsNullOrEmpty(UserID))  //如果用户ID=0和用户ID的参数为空（超级管理员修改用户密码）
            {
                string SuperUserPassword = ConfigurationManager.AppSettings[SystemConst.CONFIG_SUPERUSER_PASSWORD];
                if (!SecurityHelper.MD5(txtAccountPwd.Text).Equals(SuperUserPassword))
                {
                    Utils.RunJavaScript(this, "alert({msg:'输入的旧密码不正确，请重新输入！',title:'提示信息'})");
                    return;
                }
                else
                {
                    UpdateWebConfig_appSettings("SuperUserPassword", SecurityHelper.MD5(txtAccountNewPwd1.Text).ToString());
                    returnMsg = "1";
                }
            }
            else
            {

                if (string.IsNullOrEmpty(UserID))  //如果是用户自己修改自己的密码，需要比对旧密码是否输入正确
                {
                    DataTable dt = BllAccount.GetList("ONE", Utils.getOneParams(GetLoginAccountId()));
                    string oldPwd = string.Empty;
                    if (dt.Rows.Count > 0)
                    {
                        oldPwd = dt.Rows[0]["PassWord"].ToString();
                    }
                    if (SecurityHelper.MD5(txtAccountPwd.Text) != oldPwd)
                    {
                        Utils.RunJavaScript(this, "alert({msg:'输入的旧密码不正确，请重新输入！',title:'提示信息'})");
                        return;
                    }
                }

                KingTop.Model.SysManage.Account mode = new KingTop.Model.SysManage.Account();
                mode.UserID = Utils.ParseInt(this.UserID, Utils.ParseInt(GetLoginAccountId(), 0));
                mode.PassWord = SecurityHelper.MD5(txtAccountNewPwd1.Text).ToString();
                returnMsg = BllAccount.Save("CHANGEPWD", mode);
            }

            try
            {
                if (Convert.ToInt32(returnMsg) > 0)
                {
                    Utils.RunJavaScript(this, "alert({msg:'修改密码成功！',title:'提示信息'})"); 
                    WriteLog("修改" + txtAccountName.Text + "密码成功", "", 2);// 写入操作日志
                    PageInit();
                }
            }
            catch
            {
                Utils.RunJavaScript(this, "alert({msg:" + returnMsg + ",title:'提示信息'})");
                WriteLog("修改" + txtAccountName.Text + "密码失败", "", 2);// 写入操作日志
            }
        }

    }
}
