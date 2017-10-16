using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KingTop.Common
{
    public class UrlRewriter : System.Web.IHttpModule
    {
        public UrlRewriter()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        }

        public void Dispose()
        {

        }

        private void AdminUrlRewrite(object sender, EventArgs e)
        {
            #region 重写后台地址
            HttpApplication app = (HttpApplication)sender;
            string Url = app.Context.Request.RawUrl;
            string lowerUrl = Url.ToLower();
            if (lowerUrl.IndexOf("asmx") != -1)
            {
                return;
            }

            string manageDir = GetManageDir(app);

            if (lowerUrl.IndexOf("kingtop_login.aspx") != -1)
            {
                app.Context.Response.Redirect("/" + manageDir + "/login.aspx");
            }
            else if (manageDir != "" && lowerUrl.IndexOf("/" + manageDir + "/") != -1)
            {
                string toUrl = myReplace(Url, "/" + manageDir + "/", "/sysadmin/");

                app.Context.RewritePath(toUrl);
                return;
            }
            else if (manageDir != "" && lowerUrl.IndexOf("/sysadmin/") != -1 && (lowerUrl.IndexOf("login.aspx") != -1 || lowerUrl.IndexOf("index.aspx") != -1))
            {
                app.Context.RewritePath("/sysadmin/console/error.htm");
            }
            else if (manageDir != "" && lowerUrl.IndexOf("/sysadmin/") != -1 && lowerUrl.IndexOf(".aspx") != -1)
            {
                app.Context.Response.Redirect(myReplace(Url, "sysadmin", manageDir));
            }
            #endregion
        }

        public void QianTaiUrlRewrite(object sender, EventArgs e)
        {
            

            HttpApplication app = (HttpApplication)sender;
            string Url = app.Context.Request.RawUrl;
            string lowerUrl = Url.ToLower();
            string[] arrUrl=(lowerUrl+"?").Split ('?');
            if (lowerUrl.IndexOf(".jpg") != -1 || lowerUrl.IndexOf(".gif") != -1 || lowerUrl.IndexOf(".swf") != -1 || lowerUrl.IndexOf(".bmp") != -1 || lowerUrl.IndexOf(".css") != -1 || lowerUrl.IndexOf(".js") != -1 || lowerUrl.IndexOf(".htc") != -1 || lowerUrl.IndexOf(".png") != -1 || lowerUrl.IndexOf("/sysadmin/") != -1)
            {
                return;
            }
            if (KingTop.Common.AppCache.IsExist("UrlReWriteCatch"))
            {
                List<URLRewriterInfo> T = (List<URLRewriterInfo>)KingTop.Common.AppCache.Get("UrlReWriteCatch");
                foreach (URLRewriterInfo objList in T)
                {
                    if ((lowerUrl==objList.ID.ToLower() || arrUrl[0]==objList.ID.ToLower ()) && objList.filepath == "menu")  //栏目
                    {
                        string url = GetUrlParam(objList.Title, objList.NodeCode, arrUrl[1]);
                        app.Context.RewritePath(url);
                        return;
                    }
                    else if(arrUrl[0]==objList.ID.ToLower () || (arrUrl[0]==objList.Title .ToLower() && objList .filepath!="menu"))
                    {
                        string url = GetInfoUrl(objList.filepath, objList.ID, arrUrl[1]);
                        app.Context.RewritePath(url);
                        return;
                    }
                }

                //如果找不到，则在数据库中找
                GetDircList(sender, lowerUrl);
                
            }
            else
            {
                GetDircList(sender, lowerUrl);
            }
        }

        private string GetInfoUrl(string url, string id, string para)
        {
            string[] id1 = id.Split('/');
            string id2 = id1[id1.Length - 1].Split('.')[0];
            id2 = id2.IndexOf("_") != -1 ? id2.Split('_')[1] : id2;
            if (!string.IsNullOrEmpty(para))
            {
                url = url + "?" + para;
            }

            if (url.IndexOf("?") == -1)
            {
                url = url + "?id=" + id2;
            }
            else if (url.IndexOf("id=") == -1)
            {
                url = url + "&id=" + id2;
            }

            return url;
        }

        private string GetUrlParam(string url, string NodeCode, string para)
        {
            if (!string.IsNullOrEmpty(para))
            {
                url = url + "?" + para;
            }

            if (url.IndexOf("?") == -1)
            {
                url = url + "?nc=" + NodeCode;
            }
            else if (url.IndexOf("nc=") == -1)
            {
                url = url + "&nc=" + NodeCode;
            }
            return url;
        }

        private void context_BeginRequest(object sender, EventArgs e)
        {
            //AdminUrlRewrite(sender, e);
            QianTaiUrlRewrite(sender, e);
            #region SQL危险字符检查，只检查前台
            //string adminUrl;
            //if (string.IsNullOrEmpty(manageDir))
            //{
            //    adminUrl = "/sysadmin/";
            //}
            //else
            //{
            //    adminUrl = "/" + manageDir + "/";
            //}

            //if (Url.IndexOf(adminUrl) == -1)
            //{
            //    ProcessRequest pr = new ProcessRequest();
            //    pr.StartProcessRequest();
            //}
            #endregion
        }

        #region 字符串替换，不区分大小写
        public string myReplace(string strSource, string strRe, string strTo)
        {
            string strSl, strRl;
            strSl = strSource.ToLower();
            strRl = strRe.ToLower();
            int start = strSl.IndexOf(strRl);
            if (start != -1)
            {
                strSource = strSource.Substring(0, start) + strTo
                + myReplace(strSource.Substring(start + strRe.Length), strRe, strTo);
            }
            return strSource;
        }
        #endregion

        #region 获得配置中设置的管理路径
        public string GetManageDir(HttpApplication app)
        {
            string re = "";
            if (AppCache.IsExist("CacheManageDir"))
            {
                re = KingTop.Common.AppCache.Get("CacheManageDir").ToString();
            }
            else
            {
                string siteParamPath = app.Context.Server.MapPath("/sysadmin/Configuraion/SiteInfoManage.config");
                re = Utils.XmlRead(siteParamPath, "/SiteInfoManage/ManageDir", "").ToLower();
                AppCache.AddCache("CacheManageDir", re, 144000);
            }

            return re;
        }
        #endregion



        void GetDircList(object sender, string RawUrl)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            List<URLRewriterInfo> T = new List<URLRewriterInfo>();
            URLRewriterInfo infoModel = new URLRewriterInfo();
            string lowerUrl = RawUrl.ToLower();
            string[] arrUrl = (lowerUrl + "?").Split('?');
            string url = "/";
            string sql = "select * from VIEW_URLRewriter";
            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            while (dr.Read())
            {
                infoModel = new URLRewriterInfo();
                if (dr["filepath"].ToString() == "menu")
                {
                    infoModel.ID = dr["ID"].ToString();
                    infoModel.Title = dr["Title"].ToString();
                    infoModel.filepath = dr["filepath"].ToString();
                    infoModel.NodeCode = dr["NodeCode"].ToString();
                }
                else
                {
                    infoModel.ID = dr["CustomManageLink"].ToString() + dr["ID"].ToString() + ".html";
                    infoModel.Title = dr["CustomManageLink"].ToString() + dr["Title"].ToString()+".html";
                    infoModel.filepath = dr["filepath"].ToString();
                    infoModel.NodeCode = dr["NodeCode"].ToString();
                }
                //T[0].
                T.Add(infoModel);

                if (url != "/")
                {
                    continue;
                }

                if (infoModel.ID.ToLower() == lowerUrl && infoModel.filepath == "menu")  //栏目
                {
                    url = GetUrlParam(infoModel.Title, infoModel.NodeCode, arrUrl[1]);
                }
                else if (arrUrl[0] == infoModel.ID.ToLower() || (arrUrl[0] == infoModel.Title.ToLower() && infoModel.filepath != "menu"))
                {
                    url = GetInfoUrl(infoModel.filepath, infoModel.ID, arrUrl[1]);
                }

                
            }
            dr.Close();
            dr.Dispose();

            if (KingTop.Common.AppCache.IsExist("UrlReWriteCatch"))
            {
                KingTop.Common.AppCache.Remove("UrlReWriteCatch");
            }
            
            KingTop.Common.AppCache.AddCache("UrlReWriteCatch", T, 1440);

            if (url != "/")
            {
                HttpApplication app = (HttpApplication)sender;
                app.Context.RewritePath(url);
            }
        }
    }


    #region 前台提交危险字符检查
    public class ProcessRequest
    {
        private static string SqlStr = System.Configuration.ConfigurationManager.AppSettings["SqlInject"].ToString();
        ///
        /// 用来识别是否是流的方式传输
        ///
        ///
        ///
        bool IsUploadRequest(HttpRequest request)
        {
            return StringStartsWithAnotherIgnoreCase(request.ContentType, "multipart/form-data");
        }
        ///
        /// 比较内容类型
        ///
        ///
        ///
        ///
        private static bool StringStartsWithAnotherIgnoreCase(string s1, string s2)
        {
            return (string.Compare(s1, 0, s2, 0, s2.Length, true, CultureInfo.InvariantCulture) == 0);
        }

        //SQL注入式攻击代码分析
        #region SQL注入式攻击代码分析
        ///
        /// 处理用户提交的请求
        ///
        public void StartProcessRequest()
        {
            HttpRequest Request = System.Web.HttpContext.Current.Request;
            HttpResponse Response = System.Web.HttpContext.Current.Response;
            try
            {
                string getkeys = "";
                if (IsUploadRequest(Request)) return; //如果是流传递就退出
                //字符串参数
                if (Request.QueryString != null)
                {
                    for (int i = 0; i < Request.QueryString.Count; i++)
                    {
                        getkeys = Request.QueryString.Keys[i];
                        if (!ProcessSqlStr(Request.QueryString[getkeys]))
                        {
                            logSqlstr(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + Request.ServerVariables["Url"]);
                            logSqlstr(getkeys + "=" + Request.QueryString[getkeys]);
                            //Response.Redirect(sqlErrorPage + "?errmsg=QueryStringError&sqlprocess=true");
                            Response.Write("您打开页面的URL中有被禁用的字符，访问已被禁止");
                            Response.End();
                        }
                    }
                }
                //form参数
                if (Request.Form != null)
                {
                    for (int i = 0; i < Request.Form.Count; i++)
                    {
                        getkeys = Request.Form.Keys[i];
                        if (!ProcessSqlStr(Request.Form[getkeys]))
                        {
                            logSqlstr(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + Request.ServerVariables["Url"]);
                            logSqlstr(getkeys + "=" + Request.Form[getkeys]);
                            //Response.Redirect(sqlErrorPage + "?errmsg=FormError&sqlprocess=true");
                            Response.Write("您提交的表单有被禁用的字符，访问已被禁止");
                            Response.End();
                        }
                    }
                }
                //cookie参数
                if (Request.Cookies != null)
                {
                    for (int i = 0; i < Request.Cookies.Count; i++)
                    {
                        getkeys = Request.Cookies.Keys[i];
                        if (!ProcessSqlStr(Request.Cookies[getkeys].Value))
                        {
                            logSqlstr(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ":" + Request.ServerVariables["Url"]);
                            logSqlstr(getkeys + "=" + Request.Cookies[getkeys]);
                            Response.Write("您打开页面的URL中有被禁用的字符，访问已被禁止");
                            //Response.Redirect(sqlErrorPage + "?errmsg=CookieError&sqlprocess=true");
                            Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 错误处理: 处理用户提交信息!
                Response.Clear();
                Response.Write("页面访问出错：您提交的表单或者页面URL中包含了被禁用的字符");
                Response.End();
            }
        }

        ///
        /// 分析用户请求是否正常
        ///
        /// 传入用户提交数据
        /// 返回是否含有SQL注入式攻击代码
        private bool ProcessSqlStr(string Str)
        {
            bool ReturnValue = true;
            try
            {
                if (Str != "")
                {
                    Str = Str.ToLower();
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.IndexOf(ss) >= 0)
                        {
                            ReturnValue = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }

        private void logSqlstr(string str)
        {
            HttpRequest req = System.Web.HttpContext.Current.Request;
            string fileName = "/Log/log_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".log";
            fileName = req.MapPath(fileName);
            if (!System.IO.File.Exists(fileName))
            {
                System.IO.FileStream f = System.IO.File.Create(fileName);
                f.Close();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(fileName, true, System.Text.Encoding.GetEncoding("utf-8"));
            f2.WriteLine(str);
            f2.Close();
            f2.Dispose();

        }
        #endregion
    }
    #endregion
}
