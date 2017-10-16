using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.about
{
    public partial class contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "104007003";
        }

        protected void btnTijiao_Click(object sender, EventArgs e)
        {
            string txtTitle = Utils.CheckSql(Request.Form["txtTitle"]);
            string txtEmail = Utils.CheckSql(Request.Form["txtEmail"]);
            string txtMessage = Utils.CheckSql(Request.Form["txtMessage"]);
            string NodeCode = "104007003002";
            string sqlid = "1" + DateTime.Now.ToString("yyyyMMddhhmmss");
            DataTable dtmid = KingTop.Common.SQLHelper.GetDataSet("select max(id) as id from K_U_ContactInfo");
            if (dtmid != null && dtmid.Rows.Count > 0)
            {
                if (dtmid.Rows[0]["id"].ToString() == "")
                {
                    sqlid = "100000000853377";
                }
                else
                {
                    sqlid = (Convert.ToInt64(dtmid.Rows[0]["id"].ToString()) + 3).ToString();
                }
            }
            string sql = "INSERT INTO K_U_ContactInfo(ID,IsDel,IsEnable,IsArchiving,Orders,AddDate,DelTime,SiteID,NodeCode,FlowState,ReferenceID,AddMan,Title,Email,Content) VALUES ('" + sqlid + "'  ,0 ,1  ,0  ,1,'" + DateTime.Now.ToString() + "' ,'" + DateTime.Now.ToString() + "' ,1 ,'" + NodeCode + "' ,3  ,'" + sqlid + "','Admin','" + txtTitle + "','" + txtEmail + "','" + txtMessage + "')";
            if (KingTop.Common.SQLHelper.ExcuteCommand(sql))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "<script>alert(\"谢谢您的留言！\")</script>");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "msg", "<script>alert(\"您的留言提交失败，请重新提交！\")</script>");
            }
        }
    }
}