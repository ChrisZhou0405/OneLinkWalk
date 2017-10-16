using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingTop.Common.lc
{
  
    public static class AjaxMsgHelper
    {
      
        public static void AjaxMsg(string statu, string msg)
        {
            AjaxMsgHelper.AjaxMsg(statu, msg, "null", "null");
        }
      
        public static void AjaxMsg(string statu, string msg, string data)
        {
            AjaxMsgHelper.AjaxMsg(statu, msg, data, "null");
        }
       
        public static void AjaxMsg(string statu, string msg, string data, string nextUrl)
        {
        
            string strMsg = "{\"statu\":\"" + statu + "\",\"msg\":\"" + msg + "\",\"data\":" + data + ",\"nextUrl\":\"" + nextUrl + "\"}";
       
            System.Web.HttpContext.Current.Response.Write(strMsg);
        }
      
    }
}
