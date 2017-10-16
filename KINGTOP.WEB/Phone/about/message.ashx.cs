using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingTop.WEB.Phone.about
{
    /// <summary>
    /// message 的摘要说明
    /// </summary>
    public class message : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           string TitleStr=  context.Request.Params["Title"].ToString();
           string EmailStr = context.Request.Params["Email"].ToString();
           string ContentStr = context.Request.Params["Content"].ToString();
           handleDt(TitleStr,EmailStr,ContentStr);
        }

        void handleDt(string t,string e,string c)
        {
            string[] idorders = KingTop.BLL.Public.GetTableID("0", "K_U_ContactInfo");
            string ID = idorders[0];
            string Orders = idorders[1];
            string sql = "insert into K_U_ContactInfo(Title,Email,Content,ID,SiteID,NodeCode,Orders,ReferenceID,AddMan) values('" + t + "','" + e + "','" + c + "','" + ID + "','1','101009007003','" + Orders + "','100000002252739','AddMan');";

            int result = (int)SQLHelper.ExecuteNonQuery(sql);

            string r = result == 1 ? "ok" : "err";
            KingTop.Common.lc.AjaxMsgHelper.AjaxMsg(r, r);
 
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}