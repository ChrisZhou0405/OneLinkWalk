using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using KingTop.Web.Admin;
using KingTop.Config;
using KingTop.BLL.SysManage;
using KingTop.Common;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 站点信息设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.WEB.SysAdmin.HR
{
    public partial class HRSet : AdminPage
    {
        PostConfig M_PostConfig = new PostConfig();
        //文件实际物理路径
        public string PhyFilePath = string.Empty;
        private string hrFilePath = string.Empty ;
        private string siteName = "图派科技";
        protected void Page_Load(object sender, EventArgs e)
        {
            string FilePath = "~/" + SiteDir + "/config/Post.config";
            PhyFilePath = HttpContext.Current.Server.MapPath(FilePath);
            hrFilePath = Server.MapPath("~/" + SiteDir + "/config/HR.config");

            if (!Page.IsPostBack)
            {
                string path = Server.MapPath("~/" + SiteDir + "/config/SiteInfo.config");
                if (File.Exists(path))
                {
                    siteName = Utils.XmlRead(path, "SiteInfoConfig/SiteName", "");  //站点名称
                }
                ResetConfig();
                BindDate();
            }
        }


        //判断config文件是否存在，如果不存在重置config
        protected void ResetConfig()
        {
            Post.ConfigInfo = M_PostConfig;
            if (!File.Exists(PhyFilePath))
            {
                Post.SaveConfig(PhyFilePath);
            }
            
            //判断HR.config文件是否存在
            
            if (!File.Exists(hrFilePath))
            {
                string content = @"<?xml version=""1.0""?>
<HRConfig xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
	<IsMember>0</IsMember>
	<IsSendToEmail>0</IsSendToEmail>  
    <EMailTitle>{SiteName}面试通知</EMailTitle>
    <EMailContent>
<![CDATA[{UserName}，您好!
<br>
<br>
　　我司收到您投递的简历，请您于{MSDate}";
            content+=@"来我司面试，面试时需要您提供简历、学历证等
<br><br>
　　　　　　{SiteName}<br><br>　　　　　　";
            content+="{SendDate}]]> "; 
content+=@"</EMailContent>    

    <ResumeDetailFilePath>/{SiteDir}/hr/resumedetail.aspx</ResumeDetailFilePath>
</HRConfig>";
                try
                {
                    File.WriteAllText(hrFilePath, content);
                }
                catch
                {
                    Response.Write("<div width=100%><div style='padding-top:50px' align='center'>"+Server.MapPath("~/" + SiteDir + "/config") + "没有修改权限，请先赋权后在进行设置!</div></div>");
                    Response.End();
                }
            }
        }

        protected void BindDate()
        {
            M_PostConfig = Post.GetConfig(PhyFilePath);
            this.txtEmail.Text = M_PostConfig.Email;
            //this.txtPassword.Text = M_PostConfig.Password;
            this.txtPassword.Attributes.Add("value", DesSecurity.DesDecrypt(M_PostConfig.Password, "emailpwd"));
            this.txtSmtpServer.Text = M_PostConfig.SmtpServer;
            this.txtPort.Text = M_PostConfig.Port;

            string memberValue = Utils.XmlRead(hrFilePath, "HRConfig/IsMember", "");
            string mailValue = Utils.XmlRead(hrFilePath, "HRConfig/IsSendToEmail", "");
            txtResumeDetailFilePath.Value = Utils.XmlRead(hrFilePath, "HRConfig/ResumeDetailFilePath", "");
            txtEMailTitle.Text = Utils.XmlRead(hrFilePath, "HRConfig/EMailTitle", "").Replace("{SiteName}", siteName);
            txtEMailContent.Content = Utils.XmlRead(hrFilePath, "HRConfig/EMailContent", "").Replace("{SiteName}", siteName);
            if (memberValue == "1")
            {
                chkIsMember.Checked = true;
            }
            if (mailValue == "1")
            {
                chkIsEmail.Checked = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsHaveRightByOperCode("Edit"))
            {
                M_PostConfig.Email = this.txtEmail.Text;
                M_PostConfig.Password = DesSecurity.DesEncrypt(this.txtPassword.Text, "emailpwd");
                M_PostConfig.SmtpServer = this.txtSmtpServer.Text;
                M_PostConfig.Port = this.txtPort.Text;
                Post.ConfigInfo = M_PostConfig;
                bool b = Post.SaveConfig(PhyFilePath);
                string memberValue = "0";
                string mailValue = "0";
                if (chkIsMember.Checked)
                {
                    memberValue = "1";
                }
                if (chkIsEmail.Checked)
                {
                    mailValue = "1";
                }
                bool b1 = Utils.XmlUpdate(hrFilePath, "HRConfig/IsMember", "", memberValue);
                bool b2 = Utils.XmlUpdate(hrFilePath, "HRConfig/IsSendToEmail", "", mailValue);
                Utils.XmlUpdate(hrFilePath, "HRConfig/EMailTitle", "", txtEMailTitle.Text);
                Utils.XmlCDATAUpdate(hrFilePath, "HRConfig/EMailContent", txtEMailContent.Content );
                if (!b)
                {
                    Utils.RunJavaScript(this, "alert({msg:'设置失败，" + PhyFilePath.Replace("\\", "\\\\") + "文件没有修改权限!',status: '2', title: '提示信息', time: 10000, width: 400})");
                    return;
                }
                if (!b1)
                {
                    Utils.RunJavaScript(this, "alert({msg:'设置失败，" + hrFilePath.Replace("\\", "\\\\") + "文件没有修改权限!',status: '2', title: '提示信息', time: 10000, width: 400})");
                    return;
                }
                WriteLog("人力资源参数设置成功！", "", 2);
                Utils.RunJavaScript(this, "alert({msg:'设置成功!',title:'提示信息'})");
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'你没有设置的权限，请联系站点管理员！',title:'提示信息'})");
            }
        }
    }
}
