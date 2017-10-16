using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KingTop.Common;
using KingTop.Config;

namespace KingTop.WEB.SysAdmin.HR
{
    public partial class sendemail : KingTop.Web.Admin.AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Data_Bind();
            }
        }

        private void Data_Bind()
        {
            string FilePath;
            string FilePath1;
            if (SiteDir == "")
            {
                FilePath = "~/" + GetVirtualPath + "config/SiteInfo.config";
                FilePath1 = "~/" + GetVirtualPath + "config/HR.config";
            }
            else
            {
                FilePath = "~/" + GetVirtualPath + SiteDir + "/config/SiteInfo.config";
                FilePath1 = "~/" + GetVirtualPath + SiteDir + "/config/HR.config";
            }
            FilePath1 = Server.MapPath(FilePath1);
            SiteInfoConfig M_SiteInfoConfig = new SiteInfoConfig();
            M_SiteInfoConfig = SiteInfo.GetConfig(Server.MapPath(FilePath ));
            string title = Utils.XmlRead(FilePath1, "HRConfig/EMailTitle", "").Replace("{SiteName}", M_SiteInfoConfig.SiteName);
            string con = Utils.XmlRead(FilePath1, "HRConfig/EMailContent", "");
            string UserName = string.Empty;
            int id = KingTop.Common.Utils .ParseInt (Request.QueryString["id"],0);
            string sql = "SELECT EMail,UserName From K_HRResume Where ID="+id;
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                txtEMail.Value = dt.Rows[0]["EMail"].ToString();
                UserName = dt.Rows[0]["UserName"].ToString();
            }
            con = con.Replace("{UserName}", UserName).Replace("{SiteName}", M_SiteInfoConfig.SiteName);
            con = con.Replace("{MSDate}", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"));
            con = con.Replace("{SendDate}", DateTime.Today.ToString("yyyy-MM-dd"));
            Editor1.Content = con;
            txtTitle.Value = title;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string[] email = {txtEMail.Value};
            string title = txtTitle.Value;
            string con = Editor1.Content;
            if (!Utils.IsEmail(email[0]))
            {
                Utils.RunJavaScript(this, "alert({msg:'发送失败，邮件地址必须正确填写！',title:'提示信息'})");
            }
            if (string.IsNullOrEmpty(title))
            {
                Utils.RunJavaScript(this, "alert({msg:'发送失败，标题必须填写！',title:'提示信息'})");
            }
            if (string.IsNullOrEmpty(con))
            {
                Utils.RunJavaScript(this, "alert({msg:'发送失败，内容必须填写！',title:'提示信息'})");
            }
            Utils.SendMail(SiteDir, email, title, con);
            Utils.RunJavaScript(this, "alert({msg:'恭喜，邮件发送成功！',title:'提示信息'})");
        }
    }
}