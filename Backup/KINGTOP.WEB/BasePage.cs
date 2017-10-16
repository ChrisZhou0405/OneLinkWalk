using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using KingTop.Common;

namespace KingTop.WEB
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage()
        {
        }

        #region 发布方式 0=动态、1=伪静态、2=纯静态（暂不支持）
        public static string PublishType = Utils.XmlRead(System.Web.HttpContext.Current.Server.MapPath("/config/SiteParam.config"), "SiteParamConfig/PublishType", "");
        #endregion
        private string _sitedir = string.Empty;
        public string SiteDir
        {
            get { return _sitedir; }
            set { _sitedir = value; }
        }

        #region 根据NodeCode获取栏目路径
        public string GetURLByNode(string nodeCode)
        {
            string re = "/";
            DataTable dt;
            if (KingTop.Common.AppCache.IsExist("PublishNodeCache"))
            {
                dt = (DataTable)KingTop.Common.AppCache.Get("PublishNodeCache");
            }
            else
            {
                KingTop.BLL.SysManage.ModuleNode bll = new KingTop.BLL.SysManage.ModuleNode();
                dt = bll.Publish_GetNodeFromCache();
            }
            DataRow[] dr = dt.Select("NodeCode='" + nodeCode + "'");
            
            if (dr.Length > 0)
            {
                if (PublishType == "0")
                {

                    re = dr[0]["subdomain"].ToString();
                    if (re.IndexOf("?") == -1) //如果没有带参数，则加参数
                    {
                        re = re + "?nc=" + nodeCode;
                        return re;
                    }
                    if (re.IndexOf("nc=") == -1) //有参数，但是没有nodecode，则加上
                    {
                        re = re + "&nc=" + nodeCode;
                    }
                }
                else
                {
                    re = dr[0]["linkUrl"].ToString();
                }
            }
            return re;
        }
        #endregion

        #region
        public string InfoSaveDir(string nodeCode)
        {
            string re = "";
            if (PublishType == "1" || PublishType == "2")
            {
                string sql = "select custommanagelink,IsCreateContentPage from K_SysModuleNode where NodeCode='"+nodeCode+"'";
                DataTable dt = SQLHelper.GetDataSet(sql);
                if (dt.Rows.Count > 0)
                {

                    re = dt.Rows[0]["custommanagelink"].ToString();
                    ContentFileType = dt.Rows[0]["IsCreateContentPage"].ToString();
                }
            }
            return re;
        }

        string _infoDir=string.Empty ;
        public string GetInfoDir
        {
            get { return _infoDir; }
            set { _infoDir = value; }
        }


        //内容页文件名
        string _fileType = string.Empty;
        public string ContentFileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        //adddate 录入时间；ContentAspxFile 动态页文件名
        public string GetInfoUrl(string ID, string Title,string addDate,string nodeCode,string ContentAspxFile)
        {
            string re=string.Empty ;
            if (PublishType == "1" || PublishType=="2")
            {
                if (string.IsNullOrEmpty(GetInfoDir) || string.IsNullOrEmpty(ContentFileType))
                {
                    GetInfoDir = InfoSaveDir(nodeCode);
                }
                //re = InfoSaveDir(nodeCode);
                re = GetInfoDir.Replace("{date}", DateTime.Parse(addDate).ToString("yyyyMM"));
                if (ContentFileType == "True")
                {
                    re += ID + ".html";
                }
                else
                {
                    re += Title + ".html";
                }
            }
            else
            {
                re = string.IsNullOrEmpty(ContentAspxFile) == true ? "content.aspx" : ContentAspxFile;
                re += "?id=" + ID;
            }
            return re;
        }
        #endregion

        #region 读取页面信息
        private string pageTitle = string.Empty;
        public string PageTitle
        {
            get{return pageTitle;}
            set { pageTitle = value; }
        }

        private string pageKeyWords = string.Empty;
        public string PageKeyWords
        {
            get { return pageKeyWords; }
            set { pageKeyWords = value; }
        }

        private string pageDescription = string.Empty;
        public string PageDescription
        {
            get { return pageDescription; }
            set { pageDescription = value; }
        }

        private string logo = string.Empty;
        public string Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        private string favicon = string.Empty;
        public string Favicon
        {
            get { return favicon; }
            set { favicon = value; }
        }


        public void GetPageHeadInfo(string nodeCode)  //栏目页
        {
            GetPageHeadInfo();
            if (Utils.IsNumber(nodeCode))
            {
                string sql = "select NodeName,Meta_Keywords,Meta_description,PageTitle from K_SysModuleNode where NodeCode=@nodeCode";
                SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@nodeCode",nodeCode),
                };
                SqlDataReader dr = SQLHelper.GetReader(sql, param);
                if (dr.Read())
                {
                    if (string.IsNullOrEmpty(dr["PageTitle"].ToString()))
                        PageTitle = dr["NodeName"].ToString() + "-" + PageTitle;
                    else
                        PageTitle = dr["PageTitle"].ToString();

                    PageKeyWords = string.IsNullOrEmpty(dr["Meta_Keywords"].ToString()) == true ? PageKeyWords : dr["Meta_Keywords"].ToString();
                    PageDescription = string.IsNullOrEmpty(dr["Meta_description"].ToString()) == true ? PageKeyWords : dr["Meta_description"].ToString();
                }
                dr.Dispose();
                dr.Close();
            }
        }

        public void GetPageHeadInfo(string ID, string tableName)  //信息
        {
            GetPageHeadInfo();

            if (Utils.IsNumber(ID))
            {
                SqlDataReader dr;
                string sql = "select a.NodeCode,b.NodeName,b.Meta_Keywords,b.Meta_description,Title,MetaKeyword,Metadescript,a.PageTitle from " + tableName + " as a left join K_SysModuleNode as b on a.NodeCode=b.NodeCode where ID=@ID";
                SqlParameter[] param = new SqlParameter[] {
                new SqlParameter("@ID",ID),
                };
                try
                {
                    //判断表中是否存在PageTitle字段
                    dr = SQLHelper.GetReader(sql, param);
                }
                catch
                {
                    //不存在PageTitle字段
                    sql = "select a.NodeCode,b.NodeName,b.Meta_Keywords,b.Meta_description,Title,MetaKeyword,Metadescript,'' as PageTitle from " + tableName + " as a left join K_SysModuleNode as b on a.NodeCode=b.NodeCode where ID=@ID";
                    SqlParameter[] param1 = new SqlParameter[] {
                    new SqlParameter("@ID",ID),
                    };
                    dr = SQLHelper.GetReader(sql, param1);
                }
                if (dr.Read())
                {
                    PageTitle = dr["NodeName"].ToString() + "-" + PageTitle;
                    PageKeyWords = string.IsNullOrEmpty(dr["Meta_Keywords"].ToString()) == true ? PageKeyWords : dr["Meta_Keywords"].ToString();
                    PageDescription = string.IsNullOrEmpty(dr["Meta_description"].ToString()) == true ? PageKeyWords : dr["Meta_description"].ToString();

                    PageTitle = dr["Title"].ToString() + "-" + PageTitle;
                    if (!string.IsNullOrEmpty(dr["PageTitle"].ToString()))
                    {
                        PageTitle = dr["PageTitle"].ToString();
                    }
                    PageKeyWords = string.IsNullOrEmpty(dr["MetaKeyword"].ToString()) == true ? PageKeyWords : dr["MetaKeyword"].ToString();
                    PageDescription = string.IsNullOrEmpty(dr["Metadescript"].ToString()) == true ? PageKeyWords : dr["Metadescript"].ToString();
                }
                dr.Dispose();
                dr.Close();
            }
        }

        public void GetPageHeadInfo()  //首页
        {
            string configpath;
            if(string.IsNullOrEmpty(SiteDir))
                configpath = Server.MapPath("/config/siteinfo.config");
            else
                configpath = Server.MapPath("/" + SiteDir + "/config/siteinfo.config");

            PageTitle = Utils.XmlRead(configpath, "SiteInfoConfig/SiteTitle", "");
            PageKeyWords = Utils.XmlRead(configpath, "SiteInfoConfig/MetaKeywords", "");
            PageDescription = Utils.XmlRead(configpath, "SiteInfoConfig/MetaDescription", "");

            Logo = Utils.XmlRead(configpath, "SiteInfoConfig/Logo", "");
            Favicon = Utils.XmlRead(configpath, "SiteInfoConfig/Favicon", "");
        }
        #endregion

        #region 保存日志
        /// <summary>
        /// 保存日志
        /// </summary>
        /// <param Name="Content">日志内容</param>
        /// <param Name="PostContent">提交内容</param>
        /// <param Name="LogType">日志类型，1=登录日志，2=操作日志，3=错误日志</param>
        public void WriteLog(string Content, string PostContent, int LogType, string NodeCode, int SiteID, string UserNo)
        {
            string ip = Utils.GetIP();       //IP地址
            string ScriptName = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"] + "?" + HttpContext.Current.Request.ServerVariables["QUERY_STRING"]; //提交的页面地址
            KingTop.Model.SysManage.SysLog objlog = new KingTop.Model.SysManage.SysLog();
            KingTop.BLL.SysManage.SysLog objBllLog = new KingTop.BLL.SysManage.SysLog();
            objlog.IP = ip;
            objlog.Content = Content;
            objlog.LogType = LogType;
            objlog.NodeCode = NodeCode;
            objlog.PostContent = PostContent;
            objlog.ScriptName = ScriptName;
            objlog.SiteID = SiteID;
            objlog.UserNo = UserNo;
            objBllLog.Save("NEW", objlog);
        }
        #endregion
    }
}
