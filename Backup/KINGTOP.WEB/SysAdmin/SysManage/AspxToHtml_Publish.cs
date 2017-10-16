using System; 
using System.Data; 
using System.Configuration; 
using System.Collections.Generic;
using System.Web; 
using System.Web.Security; 
using System.Web.UI; 
using System.Web.UI.WebControls; 
using System.Web.UI.WebControls.WebParts; 
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text; 
using System.IO;
using KingTop.Common;
using KingTop.Template;

namespace KingTop.WEB.SysAdmin.SysManage
{
    public class AspxToHtml_Publish
    {
        #region 变量成员
        /// <summary>
        /// 完成的百分比
        /// </summary>
        private float finishedPercentage;
        /// <summary>
        /// 每个所占百分比
        /// </summary>
        private float  menuPercentage;
        protected  readonly int listPageLoopNum=1000000;  //列表页面循环次数（生成最后一页后会自动跳出）
        

        #endregion

        #region 生成静态页面类型
        public bool IsIndex;  //首页
        public bool IsMenuIndex;  //栏目首页
        public bool IsMenuList;   //栏目列表
        public bool IsContent;    //内容
        public bool UnPublished;  //只生成未生成页面
        public int PublishType=0;  //0=没有选择生成类型，1=生成内容的ID，2=生成更新时间为
        public List<string> ListMenu;
        public string[] TypeParam;
        public int siteID;
        /// <summary>
        /// 站点URL
        /// </summary>
        public string siteDir;
        public string NodeCode;
        public string nodeCodeList;
        public bool IsBar = true;
        #endregion

        #region 构造函数
        public AspxToHtml_Publish()
        {
            
        }
        #endregion

        #region 统计生成页面数
        private int CreateHtmlCount(DataTable MenuDt)
        {
            int re=0;
            if (IsIndex)
                re++;
            if (IsMenuList)
                re += ListMenu.Count;
            if (IsContent)
            {
                string nodeList = "''";
                string sql = "";
                string dtList = "";
                string sWhere = "";
                    //根据ID读取内容信息
                    if (UnPublished)
                    {
                        sWhere += " AND IsHtml<>1";
                    }
                    if (PublishType == 1)
                    {
                        sWhere += " AND ID in('" + TypeParam[0].Replace (",","','")+"')";
                    }
                    else if (PublishType == 2)
                    {
                        if (!string.IsNullOrEmpty(TypeParam[0]))
                        {
                            sWhere += " And AddDate>='" + TypeParam[0] + "'";
                        }
                        if (!string.IsNullOrEmpty(TypeParam[1]))
                        {
                            sWhere += " And AddDate<='" + DateTime.Parse(TypeParam[1]).AddDays(1) + "'";
                        }
                    }
                for (int i = 0; i < ListMenu.Count; i++)
                {
                    nodeList = nodeList + ",'" + ListMenu[i] + "'";
                }
                if(nodeList=="")
                {
                    return re;
                }
                for (int j = 0; j < MenuDt.Rows.Count; j++)
                {
                    string tableName=string.IsNullOrEmpty(MenuDt .Rows[j]["TableName"].ToString ())==true?"":MenuDt .Rows[j]["TableName"].ToString ();
                    if (tableName.IndexOf("K_U_") != -1 && dtList.IndexOf (tableName)==-1)
                    {
                        dtList += "," + tableName;
                        sql += "select count(id) from " + tableName + " where 1=1" + sWhere + " and nodeCode in(" + nodeList + ");";
                    }
                }
                if (sql != "")
                {
                    DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,CommandType.Text,sql);
                    for (int i = 0; i < ds.Tables.Count; i++)
                    {
                        DataTable dt = ds.Tables[i];
                        if (dt.Rows.Count > 0)
                        {
                            re += Utils.ParseInt(dt.Rows[0][0].ToString(), 0);
                        }
                    }
                }
            }

            return re;
        }
        #endregion

        #region 判断表中是否存在IsHtml字段，没有则自动加上
        public void CheckIsHtmlField(DataTable menuDt)
        {
            string dtList = "";
            string sql = "";
            for (int i = 0; i < menuDt.Rows.Count; i++)
            {
                string datatable=menuDt.Rows[i]["TableName"].ToString ();
                if (dtList.IndexOf(datatable) == -1 && datatable.IndexOf("K_U_") != -1)
                {
                    dtList+=","+datatable;
                    sql += "if not exists(select * from syscolumns where id=object_id('" + datatable + "') and name='IsHtml') begin ALTER TABLE "+datatable+" ADD IsHtml INT NOT NULL DEFAULT 0 end;";
                }
            }
            if (!string.IsNullOrEmpty(sql))
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            }
        }
        #endregion

        #region 发布
        public void Execute()
        {
            if (Check())  //判断是否有发布条件
            {
                if (IsBar)
                {
                    HProgressBar.Start();  //进度条
                }
                #region 查询字段说明
                /*
                                a.NodeID, 栏目ID
                                a.NodeCode,栏目节点编码
                                a.ParentNode,栏目节点父编码
                                a.NodelDesc,栏目描述
                                a.CurrentImg,栏目图片
                                a.MouseOverImg,鼠标移到栏目显示的图片
                                a.NodeName,栏目名称
                                a.NodeType,是否父节点（1表示有子栏目，0 表示叶节点）
                                a.IsLeftDisplay,是否左边显示（后台）
                                a.NodelOrder,栏目排序
	                            a.NodelIcon,栏目图标
	                            a.WebSiteID,站点ID
	                            a.ColumnType,栏目类型（2=单页，1=模型）
	                            a.NodeDir,栏目目录
	                            a.ModuleID,模块ID，关联K_SysModule表
	                            CASE a.ColumnType WHEN 2 THEN 'K_SinglePage' ELSE b.TableName END AS TableName, 栏目对应的表，例如公司新闻的表是K_U_News
	                            a.linkUrl,自定义前台连接（静态路径）
	                            a.subdomain,前台程序连接例如：/cn/news/list.aspx
	                            a.IsTopMenuShow,前台头部是否显示
	                            a.IsLeftMenuShow,前台左边是否显示
	                            a.NodelEngDesc,栏目英文名称
	                            a.ContentTemplate,自定义内容页程序路径 例如 /cn/news/detail.aspx
	                            a.custommanagelink,栏目内容页保存路径
	                            a.IsCreateContentPage，内容页生成方式 1=ID文件命名，0=Title文件命名
                                */
                #endregion
                string menuSql = @"SELECT  a.NodeID,a.NodeCode,a.ParentNode,a.NodelDesc,a.CurrentImg,a.MouseOverImg,a.NodeName,a.NodeType,a.IsLeftDisplay,a.NodelOrder,
	                            a.NodelIcon,a.WebSiteID,a.ColumnType,
	                            a.NodeDir,a.ModuleID,
	                            CASE a.ColumnType WHEN 2 THEN 'K_SinglePage' ELSE b.TableName END AS TableName,
	                            a.linkUrl,a.subdomain,a.IsTopMenuShow,a.IsLeftMenuShow,a.NodelEngDesc,a.ContentTemplate,a.custommanagelink,a.IsCreateContentPage
	                            FROM K_SysModuleNode AS a Left JOIN K_SysModule AS b ON a.ModuleID=b.ModuleID 
                                Where a.IsValid=1 AND a.IsDel=0 And IsWeb =1 AND WebSiteID=" + siteID;
                if (!string.IsNullOrEmpty(nodeCodeList))
                {
                    menuSql += " AND a.NodeCode In(" + nodeCodeList + ")";

                }

                //获得栏目缓存数据
                DataTable dt = SQLHelper.GetDataSet(menuSql);

                CheckIsHtmlField(dt);

                if (IsBar)
                {
                    int countHtml = CreateHtmlCount(dt);
                    menuPercentage = 100 / (float)countHtml;
                }
                //生成首页
                if (IsIndex)
                {
                    if (IsBar)
                    {
                        finishedPercentage = finishedPercentage + menuPercentage;
                        //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                        HProgressBar.Roll("正在发布首页...", (int)finishedPercentage);
                    }
                    CreateHtmlByAspx("/" + siteDir + "/index.aspx", "/" + siteDir + "/index.html");
                }

                //生成栏目页
                if (IsMenuList)
                {
                    CreateMenuListHtml(dt);
                }

                //生成内容页
                if (IsContent)
                {
                    switch (PublishType)
                    {
                        case 1:
                            CreateContentByIDList(dt);
                            break;
                        case 2:
                            CreateContentByDate(dt);
                            break;
                        default:
                            CreateContent(dt);
                            break;


                    }
                }

            }
            if (IsBar)
            {
                if (finishedPercentage < 100)
                {
                    HProgressBar.Roll("", 100);
                }

                HProgressBar.Roll("发布完成。&nbsp;<a class=\"list_link\" href=\"javascript:location.href=\\'?NodeCode=" + NodeCode + "\\';\">返 回</a>", 100);
            }
        }
        #endregion

        #region 发布前参数验证
        /// <summary>
        /// 发布前参数验证
        /// </summary>
        /// <returns></returns>
        private bool Check()
        {
            bool isTrue;

            isTrue = true;
            if (this.ListMenu == null || this.ListMenu.Count == 0)
            {
                isTrue = false;
            }

            if (IsIndex)
            {
                isTrue = true;
            }
            return isTrue;
        }
        #endregion

        #region 通过动态连接，生成静态页面
        private bool CreateHtmlByAspxList(string aspxUrl, string htmlUrl,string htmlPageName)
        {

            string PageUrl=("/"+htmlUrl+"?").Split ('?')[0];
            string[] arrPageName = PageUrl.Split('/');
            string pageName = arrPageName[arrPageName.Length - 1];
            string[] arrPage=pageName.Split ('.');
            string pageDir = "/";

            if (arrPageName.Length > 1)
            {
                for (int j = 0; j < arrPageName.Length - 1; j++)
                {
                    if (!string.IsNullOrEmpty(arrPageName[j]))
                    {
                        pageDir += arrPageName[j] + "/";
                    }
                }
            }
 
            if (aspxUrl.IndexOf("pg=") == -1)
            {
                preContent = "";
                for (int i = 1; i < listPageLoopNum; i++)
                {
                    string strsApxUrl =aspxUrl+ "&pg=" + i;
                    if(i==1)
                    {
                        htmlUrl=pageDir+pageName;
                    }
                    else
                    {
                        htmlUrl = pageDir + arrPage[0] + "_" + i + "." + arrPage[1];
                    }

                    string re = CreateHtmlByAspx(strsApxUrl + "&publish_pagename=" + htmlPageName, htmlUrl, 0);
                    if (re == "End"||re=="Error")
                    {
                        return true;
                    }
                }
           }
            else
            {
                CreateHtmlByAspx(aspxUrl + "&publish_pagename=" + htmlPageName, htmlUrl);
            }

            return true;
        }

        private bool CreateHtmlByAspx(string aspxUrl, string htmlUrl)
        {
            StringWriter sw = new StringWriter();
            try
            {
                HttpContext.Current.Server.Execute(aspxUrl.Replace("//", "/"), sw);
                htmlUrl = HttpContext.Current.Server.MapPath(htmlUrl);
                if (File.Exists(htmlUrl))
                {
                    KingTop.Common.Utils.FileDelete(htmlUrl);
                }
                else
                {
                    CheckHtmlPath(htmlUrl); //如果生成的文件夹不存在，则先生成文件夹
                }

                string content = sw.ToString();

                using (FileStream fs = new FileStream(htmlUrl, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter stw = new StreamWriter(fs, HttpContext.Current.Response.ContentEncoding))
                    {
                        stw.Write(content);
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        //列表页面生成静态页面
        private string preContent = string.Empty;
        private string CreateHtmlByAspx(string aspxUrl, string htmlUrl, int i)
        {
            StringWriter sw = new StringWriter();
            try
            {
                HttpContext.Current.Server.Execute(aspxUrl.Replace("//", "/"), sw);
                htmlUrl = HttpContext.Current.Server.MapPath(htmlUrl);
                if (File.Exists(htmlUrl))
                {
                    KingTop.Common.Utils.FileDelete(htmlUrl);
                }
                else
                {
                    CheckHtmlPath(htmlUrl);  //如果生成的文件夹不存在，则先生成文件夹
                }

                string content = sw.ToString();
                if (preContent == content)
                {
                    return "End";
                }
                preContent = content;

                using (FileStream fs = new FileStream(htmlUrl, FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter stw = new StreamWriter(fs, HttpContext.Current.Response.ContentEncoding))
                    {
                        stw.Write(content);
                        if (content.IndexOf("<!--尾页(End Page)-->") > 0)
                        {
                            return "End";
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
            catch
            {
                return "Error";
            }
        }

        private void CheckHtmlPath(string htmlPath)
        {
            if(File .Exists(htmlPath))
            {
                return;
            }
            htmlPath=htmlPath.Substring (0,htmlPath.LastIndexOf("\\"));
            if (!Directory.Exists(htmlPath))
            {
                Directory.CreateDirectory(htmlPath);
            }
        }
        #endregion

        #region 生成栏目页面
        private bool CreateMenuListHtml(DataTable MenuDt)
        {
            for (int i = 0; i < ListMenu.Count; i++)
            {
                string nodeCode=ListMenu[i].ToString ();
                DataRow[] dr = MenuDt.Select("NodeCode='" + nodeCode + "'");
                if (dr.Length > 0)
                {
                    string tableName = dr[0]["TableName"].ToString();  //模块对应的数据表
                    string linkUrl = dr[0]["LinkUrl"].ToString();      //静态页面地址
                    string aspxUrl = dr[0]["subdomain"].ToString();    //动态页面地址
                    if(string.IsNullOrEmpty (linkUrl) || string.IsNullOrEmpty (aspxUrl))
                    {
                        continue ;
                    }
                    if (IsBar)
                    {
                        finishedPercentage = finishedPercentage + menuPercentage;
                        //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                        HProgressBar.Roll("正在发布栏目" + dr[0]["NodeName"].ToString().Replace("'", "") + "...", (int)finishedPercentage);
                    }
                    if (aspxUrl.IndexOf("?") == -1)
                    {
                        aspxUrl += "?nc=" + nodeCode;
                    }
                    else if (aspxUrl.IndexOf("nc=") == -1)
                    {
                        aspxUrl += "&nc=" + nodeCode;
                    }

                    //单页
                    if (tableName == "K_SinglePage")
                    {
                        CreateHtmlByAspx(aspxUrl, linkUrl);
                    }
                    else  //列表
                    {
                        string pageUrl = ("/"+linkUrl + "?").Split('?')[0];
                        string[] arrPageUrl = pageUrl.Split('/');

                        CreateHtmlByAspxList(aspxUrl, linkUrl, arrPageUrl[arrPageUrl.Length - 1]);
                    }
                }
            }
            return true;
        }
        #endregion

        #region 生成内容
        #region 根据内容ID生成静态页面
        private void CreateContentByIDList(DataTable MenuDt)
        {
            if (TypeParam.Length == 0)
            {
                return;
            }
            string idList = TypeParam[0];

            for (int i = 0; i < ListMenu.Count; i++)
            {
                string nodeCode = ListMenu[i].ToString();
                DataRow[] dr = MenuDt.Select("NodeCode='" + nodeCode + "' AND TableName<>'K_SinglePage'");
                if (dr.Length > 0)
                {
                    string tableName = dr[0]["TableName"].ToString();  //模块对应的数据表
                    string linkUrl = dr[0]["LinkUrl"].ToString();      //静态页面地址
                    string aspxUrl = dr[0]["subdomain"].ToString();    //动态页面地址
                    string contentUrl = dr[0]["ContentTemplate"].ToString();    
                    string htmlDir = dr[0]["custommanagelink"].ToString();
                    string ContentFileType = dr[0]["IsCreateContentPage"].ToString();

                    if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(aspxUrl) || string.IsNullOrEmpty (htmlDir))
                    {
                        continue;
                    }

                    string sWhere = "";
                    //根据ID读取内容信息
                    if (UnPublished)
                    {
                        sWhere += " AND IsHtml<>1";
                    }
                    string infoSql = "select Title,ID,AddDate from " + tableName + " where id in('" + idList.Replace(",", "','") + "') AND NODECODE='"+nodeCode+"'"+sWhere;
                    SqlDataReader infodr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, infoSql);
                    if (!infodr.HasRows)
                    {
                        return;
                    }
                    //内容程序路径
                    if (string.IsNullOrEmpty(contentUrl))
                    {
                        string url = (aspxUrl + "?").Split('?')[0];
                        if (url.IndexOf("/") != -1)
                        {
                            string[] urlArr = url.Split('/');
                            contentUrl = url.Replace(urlArr[urlArr.Length - 1], "") + "content.aspx";
                        }
                        else
                        {
                            contentUrl = "content.aspx";
                        }
                    }

                    //判断内容程序路径是否已经有参数
                    if(contentUrl.IndexOf ("?")==-1)
                    {
                        contentUrl+="?id=";
                    }
                    else
                    {
                        contentUrl+="&id=";
                    }

                    //内容页生成方式：True=ID，False=标题
                    bool iscreate = false;
                    //内容页生成方式：True=ID，False=标题
                    if (ContentFileType == "True")
                    {
                        while (infodr.Read())
                        {
                            if (IsBar)
                            {
                                finishedPercentage = finishedPercentage + menuPercentage;
                                //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                                HProgressBar.Roll("正在发布内容" + infodr["ID"].ToString() + ".html" + "...", (int)finishedPercentage);
                            }
                            string htmlUrl = htmlDir.Replace("{date}", DateTime.Parse(infodr["AddDate"].ToString()).ToString("yyyyMM")) + infodr["ID"].ToString() + ".html";
                            iscreate = CreateHtmlByAspx(contentUrl + infodr["ID"].ToString(), htmlUrl);
                            if (iscreate)
                            {
                                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "update " + tableName + " set IsHtml=1 where id='" + infodr["ID"].ToString() + "'");
                            }
                        }
                    }
                    else
                    {
                        while (infodr.Read())
                        {
                            if (IsBar)
                            {
                                finishedPercentage = finishedPercentage + menuPercentage;
                                //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                                HProgressBar.Roll("正在发布内容" + infodr["Title"].ToString().Replace("'", "") + ".html" + "...", (int)finishedPercentage);
                            }
                            string htmlUrl = htmlDir.Replace("{date}", DateTime.Parse(infodr["AddDate"].ToString()).ToString("yyyyMM")) + infodr["Title"].ToString() + ".html";
                            iscreate = CreateHtmlByAspx(contentUrl + infodr["ID"].ToString(), htmlUrl);
                            if (iscreate)
                            {
                                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "update " + tableName + " set IsHtml=1 where id='" + infodr["ID"].ToString() + "'");
                            }
                        }
                    }
                    infodr.Close();
                    infodr.Dispose();

                }
            }
        }
        #endregion

        #region 根据日期生成静态页面
        private void CreateContentByDate(DataTable MenuDt)
        {
            if(TypeParam.Length ==0)
            {
                return;
            }
            string startDate = TypeParam[0];
            string endDate = TypeParam[1];
            string sWhere=" 1=1";
            if (!string.IsNullOrEmpty(startDate))
            {
                sWhere += " And AddDate>='" + startDate + "'";
            }
            if(!string.IsNullOrEmpty (endDate ))
            {
                sWhere += " And AddDate<='" + DateTime.Parse (endDate).AddDays(1) + "'";
            }
            if (UnPublished)
            {
                sWhere += " AND IsHtml<>1";
            }

            for (int i = 0; i < ListMenu.Count; i++)
            {
                string nodeCode = ListMenu[i].ToString();
                DataRow[] dr = MenuDt.Select("NodeCode='" + nodeCode + "' AND TableName<>'K_SinglePage'");
                if (dr.Length > 0)
                {
                    string tableName = dr[0]["TableName"].ToString();  //模块对应的数据表
                    string linkUrl = dr[0]["LinkUrl"].ToString();      //静态页面地址
                    string aspxUrl = dr[0]["subdomain"].ToString();    //动态页面地址
                    string contentUrl = dr[0]["ContentTemplate"].ToString();    //动态页面地址
                    string htmlDir = dr[0]["custommanagelink"].ToString();
                    string ContentFileType = dr[0]["IsCreateContentPage"].ToString();

                    if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(aspxUrl) || string.IsNullOrEmpty(htmlDir))
                    {
                        continue;
                    }

                    //根据ID读取内容信息
                    string infoSql = "select Title,ID,AddDate from " + tableName + " where "+sWhere+" AND NODECODE='"+nodeCode+"'";
                    SqlDataReader infodr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, infoSql);
                    if (!infodr.HasRows)
                    {
                        return;
                    }
                    //内容程序路径
                    if (string.IsNullOrEmpty(contentUrl))
                    {
                        string url = (aspxUrl + "?").Split('?')[0];
                        if (url.IndexOf("/") != -1)
                        {
                            string[] urlArr = url.Split('/');
                            contentUrl = url.Replace(urlArr[urlArr.Length - 1], "") + "content.aspx";
                        }
                        else
                        {
                            contentUrl = "content.aspx";
                        }
                    }

                    //判断内容程序路径是否已经有参数
                    if (contentUrl.IndexOf("?") == -1)
                    {
                        contentUrl += "?id=";
                    }
                    else
                    {
                        contentUrl += "&id=";
                    }

                    bool iscreate = false;
                    //内容页生成方式：True=ID，False=标题
                    if (ContentFileType == "True")
                    {
                        while (infodr.Read())
                        {
                            if (IsBar)
                            {
                                finishedPercentage = finishedPercentage + menuPercentage;
                                //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                                HProgressBar.Roll("正在发布内容" + infodr["ID"].ToString() + ".html" + "...", (int)finishedPercentage);
                            }
                            string htmlUrl = htmlDir.Replace("{date}", DateTime.Parse(infodr["AddDate"].ToString()).ToString("yyyyMM")) + infodr["ID"].ToString() + ".html";
                            iscreate=CreateHtmlByAspx(contentUrl + infodr["ID"].ToString(), htmlUrl);
                            if (iscreate)
                            {
                                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "update " + tableName + " set IsHtml=1 where id='" + infodr["ID"].ToString() + "'");
                            }
                        }
                    }
                    else
                    {
                        while (infodr.Read())
                        {
                            if (IsBar)
                            {
                                finishedPercentage = finishedPercentage + menuPercentage;
                                //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                                HProgressBar.Roll("正在发布内容" + infodr["Title"].ToString().Replace("'", "") + ".html" + "...", (int)finishedPercentage);
                            }
                            string htmlUrl = htmlDir.Replace("{date}", DateTime.Parse(infodr["AddDate"].ToString()).ToString("yyyyMM")) + infodr["Title"].ToString() + ".html";
                            iscreate=CreateHtmlByAspx(contentUrl + infodr["ID"].ToString(), htmlUrl);
                            if (iscreate)
                            {
                                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "update " + tableName + " set IsHtml=1 where id='" + infodr["ID"].ToString() + "'");
                            }
                        }
                    }
                    infodr.Close();
                    infodr.Dispose();
                }
            }
        }
        #endregion
        #region 根据日期生成静态页面
        private void CreateContent(DataTable MenuDt)
        {
            string sWhere = " 1=1";
            if (UnPublished)
            {
                sWhere += " AND IsHtml<>1";
            }

            for (int i = 0; i < ListMenu.Count; i++)
            {
                string nodeCode = ListMenu[i].ToString();
                DataRow[] dr = MenuDt.Select("NodeCode='" + nodeCode + "' AND TableName<>'K_SinglePage'");
                if (dr.Length > 0)
                {
                    string tableName = dr[0]["TableName"].ToString();  //模块对应的数据表
                    string linkUrl = dr[0]["LinkUrl"].ToString();      //静态页面地址
                    string aspxUrl = dr[0]["subdomain"].ToString();    //动态页面地址
                    string contentUrl = dr[0]["ContentTemplate"].ToString();    //动态页面地址
                    string htmlDir = dr[0]["custommanagelink"].ToString();
                    string ContentFileType = dr[0]["IsCreateContentPage"].ToString();

                    if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(aspxUrl) || string.IsNullOrEmpty(htmlDir))
                    {
                        continue;
                    }

                    //根据ID读取内容信息
                    string infoSql = "select Title,ID,AddDate from " + tableName + " where " + sWhere + " AND NODECODE='" + nodeCode + "'";
                    SqlDataReader infodr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, infoSql);
                    if (!infodr.HasRows)
                    {
                        return;
                    }
                    //内容程序路径
                    if (string.IsNullOrEmpty(contentUrl))
                    {
                        string url = (aspxUrl + "?").Split('?')[0];
                        if (url.IndexOf("/") != -1)
                        {
                            string[] urlArr = url.Split('/');
                            contentUrl = url.Replace(urlArr[urlArr.Length - 1], "") + "content.aspx";
                        }
                        else
                        {
                            contentUrl = "content.aspx";
                        }
                    }

                    //判断内容程序路径是否已经有参数
                    if (contentUrl.IndexOf("?") == -1)
                    {
                        contentUrl += "?id=";
                    }
                    else
                    {
                        contentUrl += "&id=";
                    }

                    //内容页生成方式：True=ID，False=标题
                    bool iscreate = false;
                    //内容页生成方式：True=ID，False=标题
                    if (ContentFileType == "True")
                    {
                        while (infodr.Read())
                        {
                            if (IsBar)
                            {
                                finishedPercentage = finishedPercentage + menuPercentage;
                                //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                                HProgressBar.Roll("正在发布内容" + infodr["ID"].ToString() + ".html" + "...", (int)finishedPercentage);
                            }
                            string htmlUrl = htmlDir.Replace("{date}", DateTime.Parse(infodr["AddDate"].ToString()).ToString("yyyyMM")) + infodr["ID"].ToString() + ".html";
                            iscreate = CreateHtmlByAspx(contentUrl + infodr["ID"].ToString(), htmlUrl);
                            if (iscreate)
                            {
                                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "update " + tableName + " set IsHtml=1 where id='" + infodr["ID"].ToString() + "'");
                            }
                        }
                    }
                    else
                    {
                        while (infodr.Read())
                        {
                            if (IsBar)
                            {
                                finishedPercentage = finishedPercentage + menuPercentage;
                                //首页动态页面路径 /index.aspx或者/default.aspx;如果是其他语言版本，则为/en(站点目录）/index.aspx
                                HProgressBar.Roll("正在发布内容" + infodr["Title"].ToString().Replace("'", "") + ".html" + "...", (int)finishedPercentage);
                            }
                            string htmlUrl = htmlDir.Replace("{date}", DateTime.Parse(infodr["AddDate"].ToString()).ToString("yyyyMM")) + infodr["Title"].ToString() + ".html";
                            iscreate = CreateHtmlByAspx(contentUrl + infodr["ID"].ToString(), htmlUrl);
                            if (iscreate)
                            {
                                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "update " + tableName + " set IsHtml=1 where id='" + infodr["ID"].ToString() + "'");
                            }
                        }
                    }
                    infodr.Close();
                    infodr.Dispose();
                }
            }
        }
        #endregion
        #endregion
    }
}
