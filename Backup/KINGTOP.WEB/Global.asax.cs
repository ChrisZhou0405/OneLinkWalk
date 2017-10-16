using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using KingTop.Common;

namespace KingTop.WEB
{
    public class Global : System.Web.HttpApplication
    {
        public System.Threading.Thread TaskThread = null;

        protected void Application_Start(object sender, EventArgs e)
        {
            //TaskThread=KingTop.BLL.SysManage.TaskTool.CreateTimerTaskService();
            //TaskThread.Start();
            Application["SysStartTime"] = DateTime.Now;

            //网站发布方式
            Application["publisType"] = Utils.XmlRead(Server.MapPath("./config/SiteParam.config"), "SiteParamConfig/PublishType", "");
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                string strLang = Utils.GetCookie(SystemConst.COOKIES_KEY, SystemConst.COOKIES_LANG_KEY);
                if (strLang != "")  //如果当前选定了语言包,则使用选定的语言包
                {
                    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(strLang);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(strLang);
                }
            }
            catch (Exception ex)
            { }

            //try
            //{
            //    string session_param_name = "ASPSESSID";
            //    string session_cookie_name = "ASP.NET_SESSIONID";

            //    if (HttpContext.Current.Request.Form[session_param_name] != null)
            //    {
            //        UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
            //    }
            //    else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
            //    {
            //        UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
            //    }
            //}
            //catch (Exception)
            //{
            //}

            if (KingTop.WEB.BasePage.PublishType == "1")//伪静态；
            {
                KingTop.Common.UrlRewriter urlre = new UrlRewriter();
                urlre.QianTaiUrlRewrite(sender, e);
            }

            // * * * * * * * * * * * * * * * * * * * * * * * *
            // 全局防注入
            // Author:依依秋寒
            // * * * * * * * * * * * * * * * * * * * * * * * *

            //在接收到一个应用程序请求时触发。
            string[] KeyWords = new string[] { ";", "'", "--", "xp_", "XP_", "xP_", "Xp_", "0x" };
            string[] kw2 = new string[] { "exec", "insert", "select", "delete", "update", "count", "*", "chr", "mid", "master", "truncate", "char", "declare", "script", "script", "object", "%27" };
            string[] safeKeys = "&#59;|&#39;|&#45;&#45;|&#120;&#112;&#95;|&#88;&#80;&#95;|&#120;&#80;&#95;|&#88;&#112;&#95;|Ox".Split('|');
            string QueryString = Server.UrlDecode(Request.QueryString.ToString());
            string url = Request.Url.AbsolutePath;

            //排除的扩展名
            string[] dotFileName = url.Split('.');
            string dotName = dotFileName[dotFileName.Length - 1];
            dotFileName = new string[] { "axd" };

            //出现被排除的扩展名时，直接退出
            foreach (string str in dotFileName)
            {
                if (str == dotName)
                    return;
            }

            for (int i = 0; i < kw2.Length; i++)
            {
                string key1 = kw2[i];
                if (QueryString.ToLower().Contains(key1))
                {
                    System.Web.HttpContext.Current.Response.Write("<div align=center style='padding-top:30px'>非法访问</div>");
                    System.Web.HttpContext.Current.Response.End();
                }
            }

            for (int i = 0; i < KeyWords.Length; i++)
            {
                string key = KeyWords[i];

                if (QueryString.Contains(key))
                {
                    //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
                    //记录注入时的信息
                    //* * * * * * * * * * * * * * * * * * * * * * * * * * * * * *

                    string IntoRecord = System.DateTime.Now.ToString();
                    IntoRecord += " " + Request.Url.Host;
                    IntoRecord += " " + Request.RequestType;
                    IntoRecord += " " + Request.Url.AbsolutePath;
                    IntoRecord += " " + Server.UrlDecode(Request.QueryString.ToString());
                    IntoRecord += " " + Request.UserHostAddress;
                    IntoRecord += " " + Request.UserAgent;
                    IntoRecord += "\r";

                    try
                    {
                        string path = Server.MapPath(@"/_IntoRecordLog/");
                        if (!System.IO.Directory.Exists(path))
                            System.IO.Directory.CreateDirectory(path);
                        System.IO.File.AppendAllText(path + DateTime.Now.ToString("yyyyMMdd") + ".log", IntoRecord, System.Text.Encoding.Default);
                    }
                    catch { }
                    //替换注入的URL，并进行跳转
                    QueryString = QueryString.Replace(key, safeKeys[i]);
                    Response.Redirect(url + "?" + QueryString);
                    Response.End();
                }
            }

        }

        //void UpdateCookie(string cookie_name, string cookie_value)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
        //    if (cookie == null)
        //    {
        //        HttpCookie cookie1 = new HttpCookie(cookie_name, cookie_value);
        //        Response.Cookies.Add(cookie1);
        //    }
        //    else
        //    {
        //        cookie.Value = cookie_value;
        //        HttpContext.Current.Request.Cookies.Set(cookie);
        //    }
        //}

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (null != TaskThread)
            {
                TaskThread.Abort();
            }
        }
    }
}