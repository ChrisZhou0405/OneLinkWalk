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

namespace KingTop.Web.Admin
{
    public partial class MemberSet : AdminPage
    {
        //文件实际物理路径
        private string filePath = string.Empty;
        private string siteName = "图派科技";
        protected void Page_Load(object sender, EventArgs e)
        {
            filePath = Server.MapPath("~/" + SiteDir + "/config/Member.config");
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
            //判断HR.config文件是否存在
            if (!File.Exists(filePath))
            {
                string content = @"<?xml version=""1.0""?>
<MemberConfig xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
    <RegisterType>0</IsMember>
    <DisUserName>admin|administrator|system|operator|support|root|postmaster|webmaster|security</DisUserName>
    <ActEMailTitle>{SiteName}会员帐号激活</ActEMailTitle>
    <ActEMailContent>
<![CDATA[尊敬的{UserName},您好<br><br>
　　欢迎加入{SiteName}<br><br><br>
点击以下激活链接，以激活您的账号：<br>
{URL}
<br>
(如果不能点击该链接地址，请复制并粘贴到浏览器的地址输入框)<br><br>
此邮件为系统所发，请勿直接回复。]]>   
</ActEMailContent>
    <FindPwdEMailTitle>{SiteName}找回密码</FindPwdEMailTitle>
    <FindPwdEMailContent>
<![CDATA[尊敬的{UserName},您好<br><br>
　　您收到这封电子邮件是因为您 (也可能是某人冒充您的名义) 申请了一个新的密码。假如这不是您本人所申请, 请不用理会这封电子邮件<br><br><br>
该操作在24小时内确认有效，过期将无法再执行激活操作。：<br>
{URL}
<br>
<br>
(如果不能点击该链接地址，请复制并粘贴到浏览器的地址输入框)<br><br>
此邮件为系统所发，请勿直接回复。]]>  
</FindPwdEMailContent>
</MemberConfig>";
                content = content.Replace("{SiteName}", siteName);
                try
                {
                    File.WriteAllText(filePath, content);
                }
                catch
                {
                    Response.Write("<div width=100%><div style='padding-top:50px' align='center'>" + Server.MapPath("~/" + SiteDir + "/config") + "没有修改权限，请先赋权后在进行设置!</div></div>");
                    Response.End();
                }
            }
        }

        protected void BindDate()
        {
            int typeValue = Utils.ParseInt(Utils.XmlRead(filePath, "MemberConfig/RegisterType", ""),0);
            string noUserValue = Utils.XmlRead(filePath, "MemberConfig/DisUserName", "");
            txtJHEmailTitle.Text = Utils.XmlRead(filePath, "MemberConfig/ActEMailTitle", "").Replace("{SiteName}", siteName);
            txtJHEMailContent.Content = Utils.XmlRead(filePath, "MemberConfig/ActEMailContent", "").Replace("{SiteName}", siteName);
            txtFindPwdEMailTitle.Text = Utils.XmlRead(filePath, "MemberConfig/FindPwdEMailTitle", "").Replace("{SiteName}", siteName);
            txtFindPwdEMailContent.Content = Utils.XmlRead(filePath, "MemberConfig/FindPwdEMailContent", "").Replace("{SiteName}", siteName);
            switch (typeValue)
            {
                case 1:
                    cbRegType1.Checked = true;
                    break;
                case 2:
                    cbRegType2.Checked = true;
                    break;
                case 3:
                    cbRegType1.Checked = true;
                    cbRegType2.Checked = true;
                    break;
            }
            txtDisUserName.Text = noUserValue;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsHaveRightByOperCode("Edit"))
            {
                string regType = "0";
                if (cbRegType1.Checked == true && cbRegType2.Checked == true)
                {
                    regType = "3";
                }
                else if(cbRegType2.Checked == true)
                {
                    regType = "2";
                }
                else if (cbRegType1.Checked == true)
                {
                    regType = "1";
                }
                bool b1 = Utils.XmlUpdate(filePath, "MemberConfig/RegisterType", "", regType);
                bool b2 = Utils.XmlUpdate(filePath, "MemberConfig/DisUserName", "", Request.Form["txtDisUserName"]);
                Utils.XmlUpdate(filePath, "MemberConfig/ActEMailTitle", "", Request.Form["txtJHEmailTitle"]);
                Utils.XmlCDATAUpdate(filePath, "MemberConfig/ActEMailContent",  txtJHEMailContent.Content);
                Utils.XmlUpdate(filePath, "MemberConfig/FindPwdEMailTitle", "", Request.Form["txtFindPwdEMailTitle"]);
                Utils.XmlCDATAUpdate(filePath, "MemberConfig/FindPwdEMailContent",  txtFindPwdEMailContent.Content);
                
                if (!b1)
                {
                    Utils.RunJavaScript(this, "alert({msg:'设置失败，" + filePath.Replace("\\", "\\\\") + "文件没有修改权限!',status: '2', title: '提示信息', time: 10000, width: 400})");
                    return;
                }
                WriteLog("会员管理参数设置成功！", "", 2);
                Utils.RunJavaScript(this, "alert({msg:'设置成功!',title:'提示信息'})");
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'你没有设置的权限，请联系站点管理员！',title:'提示信息'})");
            }
        }
    }
}
