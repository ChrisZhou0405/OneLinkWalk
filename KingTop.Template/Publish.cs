#region 引用程序集
using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.XPath;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web;
using System.Net;
using System.IO;

using KingTop.Template.ParamType;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-02
// 功能描述：内容发布
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template
{
    public class Publish : TPublic
    {
        #region 变量成员
        /// <summary>
        /// 站点标题
        /// </summary>
        private string siteTitle;
        /// <summary>
        /// 完成的百分比
        /// </summary>
        private int finishedPercentage;
        /// <summary>
        /// 每个栏目所占百分比
        /// </summary>
        private int menuPercentage;
        /// <summary>
        /// 只生成未生成页面
        /// </summary>
        private bool unPublished;
        /// <summary>
        /// 站点ID
        /// </summary>
        private int siteID;
        /// <summary>
        /// 站点URL
        /// </summary>
        private string siteDirUrl;
        /// <summary>
        /// 站点根目录路径
        /// </summary>
        private string siteDir;
        /// <summary>
        /// 生成的文件类型
        /// </summary>
        private string fileType;
        /// <summary>
        /// 栏目列表缓存名
        /// </summary>
        private const string menuListCacheName = "HQB_Publish_MenuList";
        /// <summary>
        /// 静态类标签解析
        /// </summary>
        private ParseStaticLabel staticLabelParser;
        /// <summary>
        /// 系统标签解析
        /// </summary>
        private ParseSystemLabel systemLabelParser;
        /// <summary>
        /// 分页操作
        /// </summary>
        private Split split;
        /// <summary>
        /// 子模型首页模板
        /// </summary>
        private List<string> lstSubIndexTemplate;
        /// <summary>
        /// 子模型内容页模板
        /// </summary>
        private List<string> lstSubContentTemplate;
        private Dictionary<string, List<SubModelParam>> dicSubModel;
        private Dictionary<string, SubModelContentParam> dicSubModelContentParam;
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.IPublish dal = (IDAL.Template.IPublish)Assembly.Load(path).CreateInstance(path + ".Template.Publish");
        private DataTable _menuList;
        private Dictionary<string, string> _splitLabelList;
        private string _rootTemplate;
        private bool _isDisplayProgress;
        private DataTable _dtSpecial;
        private DataTable _dtSpecialMenu;
        private string _uploadImgUrl;
        private string _mediasUrl;
        private string _filesUrl;

        #endregion

        #region 构造函数
        public Publish(int webSiteID, string webSiteDir, string siteURL)
        {
            lstSubContentTemplate = new List<string>();
            lstSubIndexTemplate = new List<string>();
            finishedPercentage = 0;
            unPublished = false;
            this.siteID = webSiteID;
            this.siteDirUrl = siteURL;
            GetSiteConfig(webSiteDir);
            split = new Split();
            staticLabelParser = new ParseStaticLabel(this.siteID);
            systemLabelParser = new ParseSystemLabel(this.siteDirUrl);
            systemLabelParser.SiteID = this.siteID;
            systemLabelParser.UploadImgUrl = this.UploadImgUrl;
            systemLabelParser.MenuList = this.MenuList;
            systemLabelParser.FileType = this.fileType;
            this._isDisplayProgress = true;
            this.dicSubModel = new Dictionary<string, List<SubModelParam>>();
            this.dicSubModelContentParam = new Dictionary<string, SubModelContentParam>();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 图片上传目录的UR
        /// </summary>
        public string UploadImgUrl
        {
            get { return this._uploadImgUrl; }
            set
            {
                if (value.Length > 1 && value.Substring(value.Length - 1, 1) != "/")
                {
                    this._uploadImgUrl = value + "/";
                }
                else
                {
                    this._uploadImgUrl = value;
                }
            }
        }
        /// <summary>
        /// 上传视频URL
        /// </summary>
        public string MediasUrl
        {
            get { return this._mediasUrl; }
            set
            {
                if (value.Length > 1 && value.Substring(value.Length - 1, 1) != "/")
                {
                    this._mediasUrl = value + "/";
                }
                else
                {
                    this._mediasUrl = value;
                }
            }
        }
        /// <summary>
        /// 上传文件URL
        /// </summary>
        public string FilesUrl
        {
            get { return this._filesUrl; }
            set
            {
                if (value.Length > 1 && value.Substring(value.Length - 1, 1) != "/")
                {
                    this._filesUrl = value + "/";
                }
                else
                {
                    this._filesUrl = value;
                }
            }
        }

        /// <summary>
        /// 专题栏目
        /// </summary>
        public DataTable DtSpecialMenu
        {
            get
            {
                if (this._dtSpecialMenu == null)
                {
                    if (HttpContext.Current.Cache["HQB_Publish_SpecialMenuList"] == null)
                    {
                        HttpContext.Current.Cache["HQB_Publish_SpecialMenuList"] = dal.GetSpecialMenu();
                    }

                    this._dtSpecialMenu = HttpContext.Current.Cache["HQB_Publish_SpecialMenuList"] as DataTable;
                }

                return this._dtSpecialMenu;
            }
        }
        /// <summary>
        /// 专题
        /// </summary>
        public DataTable DtSpecial
        {
            get
            {
                if (this._dtSpecial == null)
                {
                    if (HttpContext.Current.Cache["HQB_Publish_SpecialList"] == null)
                    {
                        HttpContext.Current.Cache["HQB_Publish_SpecialList"] = dal.GetSpecial(this.siteID);
                    }

                    this._dtSpecial = HttpContext.Current.Cache["HQB_Publish_SpecialList"] as DataTable;
                }

                return this._dtSpecial;
            }
        }
        /// <summary>
        /// 是否显示生成进度
        /// </summary>
        public bool IsDisplayProgress
        {
            get { return this._isDisplayProgress; }
            set { this._isDisplayProgress = value; }
        }
        /// <summary>
        /// 模板根路径
        /// </summary>
        public string RootTemplate
        {
            get
            {
                if (string.IsNullOrEmpty(this._rootTemplate))
                {
                    try
                    {
                        this._rootTemplate = HttpContext.Current.Server.MapPath(LabelUtils.GetTemplateProjectPath(this.siteID));

                        if (this._rootTemplate.Substring(this._rootTemplate.Length - 2, 2) != "\\")
                        {
                            this._rootTemplate = this._rootTemplate + "\\";
                        }
                    }
                    catch
                    {
                        this._rootTemplate = string.Empty;
                    }
                }

                return this._rootTemplate;
            }
        }
        /// <summary>
        /// 栏目列表
        /// </summary>
        public DataTable MenuList
        {
            get
            {
                if (this._menuList == null)
                {
                    if (HttpContext.Current.Cache[menuListCacheName] == null)
                    {

                        this._menuList = DealMenuList(this.siteID, this.siteDirUrl, this.RootTemplate, this.fileType);
                        HttpContext.Current.Cache[menuListCacheName] = this._menuList;
                    }
                    else
                    {
                        this._menuList = HttpContext.Current.Cache[menuListCacheName] as DataTable;
                    }
                }

                return this._menuList;
            }
            set { this._menuList = value; }
        }

        /// <summary>
        /// 分页标签列表
        /// </summary>
        public Dictionary<string, string> SplitLabelList
        {
            get
            {
                if (this._splitLabelList == null)
                {
                    this._splitLabelList = split.LoadSplitLabel(this.siteID);
                }

                return this._splitLabelList;
            }
        }
        #endregion

        #region 内容解析

        #region 发布
        public void Execute(PublishParam pubParam)
        {
            DataRow[] drMenu;
            string siteIndexTemplatePath;
            string headFieldName;
            string footFileName;

            if (!Directory.Exists(this.siteDir + "IncludeFile\\"))
            {
                Directory.CreateDirectory(this.siteDir + "IncludeFile\\");
            }

            headFieldName = "Head.html";
            footFileName = "Foot.html";

            if (Check())
            {
                ClearCache();
                unPublished = pubParam.UnPublished;

                if (this.IsDisplayProgress)
                {
                    HProgressBar.Start();        //进度条
                }

                if (File.Exists(this.RootTemplate + headFieldName))     // 解析头部文件模板
                {
                    ParseTemplate(null, false, this.RootTemplate + headFieldName, string.Empty, this.siteDir, new ContentPageSaveType(), this.siteDir + "IncludeFile\\" + headFieldName);
                }

                if (File.Exists(this.RootTemplate + footFileName))      // 解析底部文件模板
                {
                    ParseTemplate(null, false, this.RootTemplate + footFileName, string.Empty, this.siteDir, new ContentPageSaveType(), this.siteDir + "IncludeFile\\" + footFileName);
                }

                if (pubParam.IsSiteIndex)   // 解析站点首页模板
                {
                    siteIndexTemplatePath = this.RootTemplate + "index.html";

                    if (!File.Exists(siteIndexTemplatePath))
                    {
                        siteIndexTemplatePath = this.RootTemplate + "default.html";
                    }

                    if (File.Exists(siteIndexTemplatePath))
                    {
                        if (this.IsDisplayProgress)
                        {
                            HProgressBar.Roll("正在发布站点首页...", finishedPercentage);
                        }

                        ParseTemplate(null, false, siteIndexTemplatePath, string.Empty, this.siteDir, new ContentPageSaveType(), null);
                    }
                }

                if (pubParam.IsMenuList || pubParam.IsContent || pubParam.IsMenuIndex)
                {
                    if (pubParam.LstMenu != null && pubParam.LstMenu.Count > 0) // 发布指定栏目
                    {
                        menuPercentage = 100 / pubParam.LstMenu.Count;

                        foreach (string nodeCode in pubParam.LstMenu)
                        {
                            drMenu = MenuList.Select("NodeCode='" + nodeCode + "'");

                            if (drMenu != null && drMenu.Length > 0)
                            {
                                if (this.IsDisplayProgress)
                                {
                                    HProgressBar.Roll("正在发布栏目" + drMenu[0]["NodeName"].ToString() + "...", finishedPercentage);
                                }

                                if (drMenu[0]["NodeType"].ToString().Trim() == "0")  // 内容节点
                                {
                                    if (drMenu[0]["TableName"].ToString().Trim() != "K_SinglePage")
                                    {
                                        PublishMenu(drMenu[0], pubParam);
                                    }
                                    else  // 单页
                                    {
                                        ParseSinglePageTemplate(drMenu[0]);
                                    }
                                }
                                else
                                {
                                    List<string> lstNodeName;

                                    lstNodeName = new List<string>();
                                    lstNodeName.Add(nodeCode);
                                    GetChildMenu(nodeCode, ref lstNodeName, false);

                                    foreach (string cNodeCode in lstNodeName)
                                    {
                                        drMenu = MenuList.Select("NodeCode='" + cNodeCode + "'");

                                        if (drMenu != null && drMenu.Length > 0)
                                        {
                                            if (drMenu[0]["TableName"].ToString().Trim() != "K_SinglePage")
                                            {
                                                PublishMenu(drMenu[0], pubParam);
                                            }
                                            else // 单页
                                            {
                                                ParseSinglePageTemplate(drMenu[0]);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else  // 发布全部栏目
                    {
                        drMenu = MenuList.Select("NodeType=0");     // 内容节点
                        menuPercentage = 100 / drMenu.Length;

                        foreach (DataRow dr in MenuList.Rows)
                        {
                            if (this.IsDisplayProgress)
                            {
                                HProgressBar.Roll("正在发布栏目" + dr["NodeName"].ToString() + "...", finishedPercentage);
                            }

                            if (dr["TableName"].ToString().Trim() != "K_SinglePage")
                            {
                                PublishMenu(dr, pubParam);
                            }
                            else  // 单页
                            {
                                ParseSinglePageTemplate(dr);
                            }
                        }
                    }
                }

                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("发布完成,进行文件同步....", 100);
                }

                SyncFile(pubParam.IsCopyFile);     // 文件同步

                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("发布完成。&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>", 100);
                }
            }

            ParseCodeBlock();
        }
        #endregion

        #region 发布单个栏目
        private void PublishMenu(DataRow dr, PublishParam pubParam)
        {
            DataTable dataSource;           // 栏目要发布的数据
            string sqlWhere;                // 栏目数据源查询条件
            string menuPath;                // 栏目文件路径
            string menuUrl;                 // 栏目URL路径
            ContentPageSaveType cSaveType;  // 内容保存方式
            string templatePath;            // 模板文件路径
            string nodeCode;                // 栏目NodeCode
            int currentProg;

            sqlWhere = string.Empty;
            nodeCode = dr["NodeCode"].ToString();
            menuPath = this.siteDir + dr["RootDirPath"].ToString().Replace("/", "\\");
            menuUrl = this.siteDirUrl + dr["RootDirPath"].ToString();
            currentProg = 0;

            if (!Directory.Exists(menuPath))    // 栏目目录不存在则创建
            {
                Directory.CreateDirectory(menuPath);
            }

            cSaveType = GetContentPageSaveType(dr["ContentPageHtmlRule"].ToString()); // 内容保存方式

            switch (pubParam.Type)   //  发布参数
            {
                case PublishType.AddDateRange:      //更新时间
                    sqlWhere = "  AddDate between '" + pubParam.PublishTypeParam[0] + "' and '" + pubParam.PublishTypeParam[1] + "'";
                    break;
                case PublishType.ContentIDEnum:     // 生成内容的ID 多个ID可由 , 分隔
                    sqlWhere = " ID in('" + pubParam.PublishTypeParam[0].Replace(",", "','") + "') ";
                    break;
            }

            if (pubParam.IsMenuList && dr["NodeType"].ToString().Trim() == "0" && dr["ListPageTemplate"].ToString().Trim() != "")    // 生成栏目列表
            {
                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("正在发布栏目 " + dr["NodeName"].ToString() + " 的列表页....", finishedPercentage);
                }

                templatePath = this.RootTemplate + dr["ListPageTemplate"].ToString();
                templatePath = templatePath.Replace("/", "\\");

                if (File.Exists(templatePath))
                {
                    ParseTemplate(dr, true, templatePath, nodeCode, menuPath, cSaveType, null);
                }

                currentProg = (int)0.25 * menuPercentage;

                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("栏目 " + dr["NodeName"].ToString() + " 的列表页完成....", finishedPercentage + currentProg);
                }
            }

            if (pubParam.IsContent && dr["ContentTemplate"].ToString().Trim() != "")     // 生成内容页面 
            {
                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("正在发布栏目 " + dr["NodeName"].ToString() + " 的内容页....", finishedPercentage);
                }

                dataSource = dal.GetMenuDataSource(this.siteID, nodeCode, dr["TableName"].ToString(), sqlWhere);

                if (dataSource != null && dataSource.Rows.Count > 0)
                {
                    templatePath = this.RootTemplate + dr["ContentTemplate"].ToString();
                    templatePath = templatePath.Replace("/", "\\");
                    ParseContentTemplate(templatePath, menuUrl, nodeCode, cSaveType, dataSource, null);
                }

                currentProg = (int)0.5 * menuPercentage;

                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("栏目 " + dr["NodeName"].ToString() + " 的内容页完成....", finishedPercentage);
                }

                ParseSubModel();    // 内容中的子模型列表内容页
            }

            if (pubParam.IsMenuIndex && dr["DefaultTemplate"].ToString().Trim() != "")  // 生成栏目首页
            {
                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("正在发布栏目 " + dr["NodeName"].ToString() + " 的首页....", finishedPercentage);
                }

                templatePath = this.RootTemplate + dr["DefaultTemplate"].ToString();
                templatePath = templatePath.Replace("/", "\\");

                if (File.Exists(templatePath))
                {
                    ParseTemplate(dr, false, templatePath, nodeCode, menuPath, cSaveType, null);
                }

                currentProg = (int)0.25 * menuPercentage;

                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("栏目 " + dr["NodeName"].ToString() + " 的首页完成....", finishedPercentage + currentProg);
                }
            }

            if (this.IsDisplayProgress)
            {
                HProgressBar.Roll("栏目 " + dr["NodeName"].ToString() + " 的发布完成....", finishedPercentage + menuPercentage - currentProg);
            }
        }
        #endregion

        #region 解析列表系统标签
        private string ParseSysLabelList(List<SysLabelList> lstSysLabelList, string tempContent, string parentID, SubModelParam subModel)
        {
            SubModelContentParam subModelContentParam;
            string parsedContent;
            string templateContent;

            templateContent = tempContent;

            if (lstSysLabelList == null)
            {
                lstSysLabelList = systemLabelParser.GetSysLabelList(templateContent);  // 抓取列表类型系统标签
            }

            foreach (SysLabelList label in lstSysLabelList)                        // 解析模板中列表类型的系统标签
            {
                if (!label.IsSplit)
                {
                    if (!string.IsNullOrEmpty(parentID) && label.IsSubModel)
                    {
                        label.LstMenu = null;

                        if (string.IsNullOrEmpty(label.SqlWhere) || label.SqlWhere.Trim() == "")
                        {
                            label.SqlWhere += " ParentID='" + parentID + "' ";
                        }
                        else
                        {
                            label.SqlWhere += " and ParentID='" + parentID + "' ";
                        }

                        if (!string.IsNullOrEmpty(label.SubModelCTemplate))
                        {
                            subModelContentParam = new SubModelContentParam();
                            subModelContentParam.ParentID = parentID;
                            subModelContentParam.TableName = label.TableName;

                            if (!label.SubModelCTemplate.Contains(":"))
                            {
                                subModelContentParam.TemplatePath = this.RootTemplate + label.SubModelCTemplate;
                            }
                            else
                            {
                                subModelContentParam.TemplatePath = label.SubModelCTemplate;
                            }

                            if (!this.dicSubModelContentParam.ContainsKey(parentID))
                            {
                                this.dicSubModelContentParam.Add(parentID, subModelContentParam);
                            }
                        }
                    }

                    parsedContent = systemLabelParser.ParseList(label, 0, this.siteDirUrl);
                    templateContent = templateContent.Replace(label.LabelName, parsedContent);

                    if (label.LstSubModel != null && label.LstSubModel.Count > 0)
                    {
                        this.dicSubModel.Add(label.TableName, label.LstSubModel);
                    }

                    if (subModel != null && !string.IsNullOrEmpty(subModel.ContentTemplate))
                    {
                        subModelContentParam = new SubModelContentParam();

                        if (!subModel.ContentTemplate.Contains(":"))
                        {
                            subModelContentParam.TemplatePath = this.RootTemplate + subModel.ContentTemplate;
                        }
                        else
                        {
                            subModelContentParam.TemplatePath = subModel.ContentTemplate;
                        }

                        subModelContentParam.ParentID = parentID;
                        subModelContentParam.TableName = label.TableName;

                        if (!this.dicSubModelContentParam.ContainsKey(parentID))
                        {
                            this.dicSubModelContentParam.Add(parentID, subModelContentParam);
                        }
                    }
                }
            }

            return templateContent;
        }
        #endregion

        #region 解析各模块公用标签块
        private string ParseCommonBlock(string tempContent, string nodeCode, ParseFreeLabel freeLabelParser)
        {
            List<FreeLabel> lstFreeLabel;                // 当前模板的所有自由标签
            List<string> lstStaticLabel;                 // 静态标签
            List<IncludeLabel> lstIncludeLabel;          // 包含标签
            List<SinglePageLabel> lstSinglePageLabel;    // 单页标签
            List<SysLabelMenu> lstSysLabelMenu;          // 栏目类型系统标签
            List<SysLabelNav> lstSysLabelNav;            // 导航类型系统标签
            List<FormLabel> lstSysLabelForm;             // 自定义表单
            List<SysLabelCategory> lstSysLabelCategory;  // 分类类型系统标签
            List<SysLabelContent> lstSysLabelContent;    // 详细页类型系统标签
            string parsedContent;
            string templateContent;
            DataTable dt;

            freeLabelParser = new ParseFreeLabel(nodeCode);

            templateContent = tempContent;
            lstFreeLabel = freeLabelParser.GetLabelList(templateContent);                       // 抓取自由标签
            lstStaticLabel = staticLabelParser.GetStaticLabel(templateContent);                 // 抓取静态标签
            lstIncludeLabel = staticLabelParser.GetIncludeLabel(templateContent);               // 抓取包含标签
            lstSinglePageLabel = systemLabelParser.GetSingleLabel(templateContent);             // 抓取单页标签
            lstSysLabelMenu = systemLabelParser.GetSysLabelMenu(templateContent, nodeCode);     // 抓取栏目类型系统标签
            lstSysLabelNav = systemLabelParser.GetSysLabelNav(templateContent, nodeCode);       // 抓取栏目类型系统标签
            lstSysLabelForm = systemLabelParser.GetSysLabelFormLabel(templateContent);          // 抓取自定义表单标签
            lstSysLabelCategory = systemLabelParser.GetSysLabelCategory(templateContent);       // 抓取分类系统标签
            lstSysLabelContent = systemLabelParser.GetSysLabelContent(templateContent, true);   // 抓取详细页类型系统标签

            if ((lstSysLabelMenu != null && lstSysLabelMenu.Count > 0) || (lstSysLabelNav != null && lstSysLabelNav.Count > 0))
            {
                AddFileRef("<link href=\"Skins/Css/sysMenu.css\" rel=\"stylesheet\" type=\"text/css\" /><script type=\"text/javascript\" src=\"Skins/JS/sysMenu.js\"></script>", ref templateContent);
            }

            foreach (SysLabelCategory label in lstSysLabelCategory)            // 分类系统标签                   
            {
                parsedContent = systemLabelParser.ParseCategory(label, templateContent);
                templateContent = templateContent.Replace(label.LabelName, parsedContent);
            }

            foreach (SysLabelContent label in lstSysLabelContent)  // 详细页标签
            {
                if (!string.IsNullOrEmpty(label.SQL) && label.SQL.Trim() != "")
                {
                    dt = dal.ExecSQL(label.SQL);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        parsedContent = systemLabelParser.ParseContent(label, dt.Rows[0]);
                        templateContent = templateContent.Replace(label.LabelName, parsedContent);
                    }
                }
            }

            foreach (FormLabel label in lstSysLabelForm)                   // 自定义表单标签
            {
                parsedContent = systemLabelParser.ParserFormLabel(label, templateContent);
                templateContent = templateContent.Replace(label.LabelName, parsedContent);
            }

            foreach (FreeLabel label in lstFreeLabel)                      // 解析模板中的自由标签
            {
                if (string.IsNullOrEmpty(label.SplitLabelID) || label.SplitLabelID.Trim() == "")
                {
                    freeLabelParser.SiteDirUrl = this.siteDirUrl;
                    parsedContent = freeLabelParser.Parse(label, 0, 0);
                    templateContent = templateContent.Replace(label.Config, parsedContent);
                }
            }

            foreach (SysLabelMenu label in lstSysLabelMenu)                // 解析模板中栏目类型的系统标签
            {
                parsedContent = systemLabelParser.ParseMenu(label, this.siteDirUrl);
                templateContent = templateContent.Replace(label.LabelName, parsedContent);
            }

            foreach (SysLabelNav label in lstSysLabelNav)                   // 解析模板中导航类型的系统标签
            {
                parsedContent = systemLabelParser.ParseNav(label, this.siteDirUrl, this.UploadImgUrl);
                templateContent = templateContent.Replace(label.LabelName, parsedContent);
            }

            foreach (string labelName in lstStaticLabel)                   // 解析模板中的静态标签
            {
                parsedContent = staticLabelParser.ParseStatic(labelName, this.siteDirUrl, this.UploadImgUrl, this.MenuList, this.fileType);
                templateContent = templateContent.Replace(labelName, parsedContent);
            }

            foreach (IncludeLabel label in lstIncludeLabel)                // 解析模板中的包含标签
            {
                parsedContent = staticLabelParser.ParseInclude(label, this.RootTemplate);
                templateContent = templateContent.Replace(label.Content, parsedContent);
            }

            foreach (SinglePageLabel label in lstSinglePageLabel)         // 解析单页标签
            {
                parsedContent = systemLabelParser.ParseSinglePage(label);
                templateContent = templateContent.Replace(label.Content, parsedContent);
            }

            return templateContent;
        }
        #endregion

        #region 解析单页
        private void ParseSinglePageTemplate(DataRow drMenu)
        {
            string templateContent;                      // 模板文件内容
            ParseFreeLabel freeLabelParser;              // 自由标签解析类
            string savePath;                             // 保存路径
            FirstMenuDirParam param;                     // 一级栏目目录参数
            string nodeCode;
            string templatePath;                         // 模板路径
            ContentPageSaveType cSaveType;               // 内容页保存方式
            bool hasPrevSinglePage;                      // 当前单页栏目这前是否有单页栏目

            freeLabelParser = new ParseFreeLabel(drMenu["NodeCode"].ToString());

            nodeCode = drMenu["NodeCode"].ToString();
            systemLabelParser.NodeCode = nodeCode;
            freeLabelParser.FileType = this.fileType;
            cSaveType = GetContentPageSaveType(drMenu["ContentPageHtmlRule"].ToString());                   // 内容保存方式
            freeLabelParser.CSaveType = cSaveType;
            templatePath = this.RootTemplate + drMenu["DefaultTemplate"].ToString().Replace("/", "\\");
            param = GetFirstMenuDir(nodeCode);                                                              //  一级栏目目录
            savePath = param.MenuDir;
            hasPrevSinglePage = HasPrevSingleMenu(drMenu["NodeCode"].ToString());

            if (!string.IsNullOrEmpty(savePath) || drMenu["NodeCode"].ToString().Length <= 6)
            {
                savePath = this.siteDir + savePath.Replace("/", "\\");

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                if (drMenu["NodeCode"].ToString().Length <= 6 || param.HasDefaultTemplate || hasPrevSinglePage)   // 一级栏目存在首页模板会生成首页 或 当前栏目之前存在单页栏目
                {
                    savePath = savePath + drMenu["NodelEngDesc"].ToString() + "." + this.fileType;
                }
                else  // 生成栏目首页
                {
                    savePath = savePath + defaultFileName + "." + this.fileType;
                }

                if (unPublished && File.Exists(savePath))  // 只生成未生成的文件，且文件存在
                {
                    return;
                }

                systemLabelParser.NodeCode = nodeCode;
                freeLabelParser.MenuList = this.MenuList;

                if (File.Exists(templatePath))
                {
                    templateContent = File.ReadAllText(templatePath);
                    templateContent = templateContent.Replace("\r\n", "");
                    templateContent = ParseCommonBlock(templateContent, nodeCode, freeLabelParser);
                    templateContent = ParseSysLabelList(null, templateContent, null, null);
                    templateContent = templateContent.Replace("{$MetaTitle}", this.siteTitle + " － " + drMenu["NodeName"].ToString());
                    templateContent = templateContent.Replace("{$MetaDescription}", drMenu["Meta_Description"].ToString());
                    templateContent = templateContent.Replace("{$MetaKeyword}", drMenu["Meta_Keywords"].ToString());

                    try
                    {
                        File.WriteAllText(savePath, PreSave(HttpContext.Current.Server.HtmlEncode(templateContent), nodeCode));    // 保存内容页
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region 解析内容模板
        private void ParseContentTemplate(string templatePath, string menuDirUrl, string nodeCode, ContentPageSaveType cSaveType, DataTable dataSource, string parentID)
        {
            string templateContent;                                  // 模板文件内容
            ParseFreeLabel freeLabelParser;                          // 自由标签解析类
            List<SysLabelContent> lstSysLabelContent;                // 详细页类型系统标签
            List<SysLabelPeriodical> lstPeriodical;                  // 期刊标签 
            List<SysLabelList> lstSysLabelList;                      // 系统标签
            List<SysLabelCommentSubmit> lstSysLabelCommentSubmit;    // 评论内容提交类型标签
            string parsedContent;                                    // 经解析后的标签内容
            string savePath;                                         // 保存路径
            Regex fieldReg;                                          // 匹配字段
            MatchCollection fieldMatch;                              // 字段匹配内容
            List<Field> lstFieldParam;                               // 所有字段参数
            Field fieldParam;                                        // 字段参数
            string fieldValue;                                       // 临时变量,字段值
            string contentLabelParsed;                               // 详细页标签解析内容
            int perProgPageAmount;                                   // 1%进度所需的内容页
            int counter;

            freeLabelParser = new ParseFreeLabel(nodeCode);
            lstFieldParam = new List<Field>();
            lstSysLabelList = new List<SysLabelList>();

            systemLabelParser.NodeCode = nodeCode;
            freeLabelParser.CSaveType = cSaveType;
            freeLabelParser.MenuList = this.MenuList;
            freeLabelParser.FileType = this.fileType;

            perProgPageAmount = (int)((float)dataSource.Rows.Count / (int)(0.5 * menuPercentage));

            if (perProgPageAmount == 0)
            {
                perProgPageAmount = 1;
            }

            counter = 1;

            if (File.Exists(templatePath))
            {
                templateContent = File.ReadAllText(templatePath);
                templateContent = templateContent.Replace("\r\n", "");
                templateContent = ParseCommonBlock(templateContent, nodeCode, freeLabelParser);
                fieldReg = new Regex(@"\{\$Field\((?<1>\d),(?<2>[^,]+),(?<3>[^)]*)\)\}|\{\$Field\((?<1>\d),(?<2>[^,]+)\)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                lstSysLabelContent = systemLabelParser.GetSysLabelContent(templateContent, false);               // 抓取详细页类型系统标签
                lstPeriodical = systemLabelParser.GetSysLabelPeriodical(templateContent);                           // 抓取期刊标签
                lstSysLabelList = systemLabelParser.GetSysLabelList(templateContent);                               // 抓取列表类型系统标签
                lstSysLabelCommentSubmit = systemLabelParser.GetSysLabelCommentSubmit(templateContent, nodeCode);   // 抓取评论内容提交标签 

                foreach (DataRow dr in dataSource.Rows)
                {
                    savePath = GetContentDirPath(this.siteDir, menuDirUrl, cSaveType, dr["AddDate"].ToString());

                    if (!Directory.Exists(savePath))        // 需创建目录
                    {
                        Directory.CreateDirectory(savePath);
                    }

                    savePath = savePath + dr["ID"].ToString() + "." + fileType;

                    if (!unPublished || !File.Exists(savePath))
                    {
                        if (counter % perProgPageAmount == 0)
                        {
                            finishedPercentage = finishedPercentage + 1;
                        }

                        if (this.IsDisplayProgress)
                        {
                            HProgressBar.Roll("正在生成内容文件 " + dr["ID"].ToString() + "." + fileType + " ...", finishedPercentage);
                        }

                        parsedContent = ParseSysLabelList(lstSysLabelList, templateContent, dr["ID"].ToString(), null);

                        foreach (SysLabelCommentSubmit label in lstSysLabelCommentSubmit) // 解析评论提交内容标签
                        {
                            contentLabelParsed = systemLabelParser.ParseCommentSubmit(label, dr["ID"].ToString());
                            parsedContent = parsedContent.Replace(label.LabelName, contentLabelParsed);
                        }

                        foreach (SysLabelPeriodical label in lstPeriodical)            // 期刊标签
                        {
                            DataTable periodicalDS;

                            periodicalDS = systemLabelParser.ParsePeriodical(label, dr["ID"].ToString(), menuDirUrl, cSaveType, out contentLabelParsed);
                            parsedContent = parsedContent.Replace(label.LabelName, contentLabelParsed);

                            if (periodicalDS != null && !string.IsNullOrEmpty(label.ContentTemplate))
                            {
                                ParseContentTemplate(this.RootTemplate + label.ContentTemplate, menuDirUrl, nodeCode, cSaveType, periodicalDS, parentID);
                            }
                        }

                        foreach (SysLabelContent label in lstSysLabelContent)  // 详细页标签
                        {
                            contentLabelParsed = systemLabelParser.ParseContent(label, dr);
                            parsedContent = parsedContent.Replace(label.LabelName, contentLabelParsed);
                        }

                        fieldMatch = fieldReg.Matches(parsedContent);                                                // 匹配循环体中的所有字段

                        foreach (Match matchItem in fieldMatch)             // 抓取所有字段
                        {
                            fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                            parsedContent = parsedContent.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                            lstFieldParam.Add(fieldParam);
                        }

                        foreach (Field fieldItem in lstFieldParam) // 绑定字段值
                        {
                            if (dr[fieldItem.Name] != null)
                            {
                                fieldValue = dr[fieldItem.Name].ToString();

                                if (fieldItem.OutParam != null && fieldItem.OutParam.Length > 0)
                                {
                                    fieldValue = FormatFieldContent(fieldValue, fieldItem.OutType, fieldItem.OutParam);
                                }

                                parsedContent = parsedContent.Replace("{[#" + fieldItem.Name + "#]}", fieldValue);
                            }
                        }

                        if (dr.Table.Columns.Contains("Title") && dr["Title"] != null)
                        {
                            parsedContent = parsedContent.Replace("{$MetaTitle}", dr["Title"].ToString());
                        }
                        else
                        {
                            parsedContent = parsedContent.Replace("{$MetaTitle}", this.siteTitle);
                        }

                        if (dr.Table.Columns.Contains("MetaDescript") && dr["MetaDescript"] != null)
                        {
                            parsedContent = parsedContent.Replace("{$MetaKeyword}", dr["MetaDescript"].ToString());
                        }
                        else
                        {
                            parsedContent = parsedContent.Replace("{$MetaDescription}", this.siteTitle);
                        }

                        if (dr.Table.Columns.Contains("MetaKeyword") && dr["MetaKeyword"] != null)
                        {
                            parsedContent = parsedContent.Replace("{$MetaKeyword}", dr["MetaKeyword"].ToString());
                        }
                        else
                        {
                            parsedContent = parsedContent.Replace("{$MetaKeyword}", this.siteTitle);
                        }

                        try
                        {
                            parsedContent = parsedContent.Replace("{$NewsId}", dr["ID"].ToString());
                            File.WriteAllText(savePath, PreSave(parsedContent, dr["NodeCode"].ToString()));     // 保存内容页
                        }
                        catch { }
                    }
                }
            }

            finishedPercentage += (int)0.5 * menuPercentage - perProgPageAmount;
        }
        #endregion

        #region 解析列表/首页模板
        private void ParseTemplate(DataRow drMenu, bool isList, string templatePath, string nodeCode, string listDirPath, ContentPageSaveType cSaveType, string saveFilePath)
        {
            string templateContent;                  // 模板文件内容
            ParseFreeLabel freeLabelParser;          // 自由标签解析类
            List<FreeLabel> lstFreeLabel;            // 当前模板的所有自由标签
            List<SplitLabel> lstSplitLabel;          // 当前模板的所有分页标签
            List<SysLabelList> lstSysLabelList;      // 列表类型系统标签
            FreeLabelConfig freeLabelConfig;         // 自由标签配置
            NeedSplitLabel needSplitLabel;           // 保存要分页的标签
            SplitLabel splitLabel;                   // 分页标签参数
            string parsedContent;                    // 经解析后的标签内容
            int rsCount;                             // 记录总数
            int pageCount;                           // 分页总数
            string savePath;                         // 保存路径
            string splitHtmlCode;                    // 分页HTML代码
            string fileName;                         // 保存的文件名
            string labelName;                        // 从模板中抓取的标签
            int pageSize;                            // 分页大小
            string splitSQL;                         // 分页SQL

            needSplitLabel = new NeedSplitLabel();
            freeLabelParser = new ParseFreeLabel(nodeCode);
            splitLabel = new SplitLabel();
            freeLabelConfig = new FreeLabelConfig();

            systemLabelParser.NodeCode = nodeCode;
            freeLabelParser.CSaveType = cSaveType;
            freeLabelParser.MenuList = this.MenuList;
            freeLabelParser.FileType = this.fileType;
            templateContent = File.ReadAllText(templatePath);
            templateContent = templateContent.Replace("\r\n", "");
            FormatUrl(ref templateContent, this.siteDirUrl);                                 // 将路径地址加上站点URL
            lstFreeLabel = freeLabelParser.GetLabelList(templateContent);                    // 抓取自由标签
            lstSplitLabel = split.GetSplitLabel(templateContent, SplitLabelList);            // 抓取分页标签
            lstSysLabelList = systemLabelParser.GetSysLabelList(templateContent);            // 抓取列表类型系统标签

            rsCount = 0;
            splitHtmlCode = string.Empty;
            splitSQL = string.Empty;
            pageSize = 0;
            labelName = string.Empty;
            parsedContent = string.Empty;

            if (isList)     // 列表
            {
                fileName = listFileName;
            }
            else            // 首页
            {
                fileName = defaultFileName;
            }

            if (!Directory.Exists(listDirPath) && !string.IsNullOrEmpty(listDirPath))     // 创建目录
            {
                Directory.CreateDirectory(listDirPath);
            }

            foreach (FreeLabel label in lstFreeLabel)  // 解析模板中的自由标签
            {
                if (!string.IsNullOrEmpty(label.SplitLabelID) && label.SplitLabelID.Trim() != "")  // 分页
                {
                    if (needSplitLabel.Label == null)
                    {
                        needSplitLabel.Label = label;
                        needSplitLabel.Type = NeedSplitLabelType.FreeLabel;
                    }
                }
            }

            foreach (SysLabelList label in lstSysLabelList)          // 解析模板中的列表类型系统标签
            {
                if (label.IsSplit && needSplitLabel.Label == null)  // 分页
                {
                    needSplitLabel.Label = label;
                    needSplitLabel.Type = NeedSplitLabelType.SysLabelList;
                }
            }

            templateContent = ParseCommonBlock(templateContent, nodeCode, freeLabelParser);
            templateContent = ParseSysLabelList(lstSysLabelList, templateContent, null, null);

            if (needSplitLabel.Label != null && lstSplitLabel.Count > 0)  // 存在分页
            {
                switch (needSplitLabel.Type)
                {
                    case NeedSplitLabelType.FreeLabel:      // 自由标签
                        try
                        {
                            FreeLabel freeLabel;
                            freeLabel = (FreeLabel)needSplitLabel.Label;
                            labelName = freeLabel.Config;
                            freeLabelConfig = freeLabelParser.GetFreeLabelConfig(freeLabel.Name);
                            pageSize = freeLabelConfig.PageSize;
                            splitSQL = freeLabelConfig.SQL;
                            splitLabel = GetSplitLabel(lstSplitLabel, NeedSplitLabelType.FreeLabel, ((FreeLabel)needSplitLabel.Label).SplitLabelID);
                        }
                        catch { }
                        break;
                    case NeedSplitLabelType.SysLabelList:   // 列表类型系统标签
                        try
                        {
                            SysLabelList sysLabelList;

                            sysLabelList = (SysLabelList)needSplitLabel.Label;
                            labelName = sysLabelList.LabelName;
                            splitLabel = GetSplitLabel(lstSplitLabel, NeedSplitLabelType.SysLabelList, null);
                            pageSize = sysLabelList.PageSize;

                            splitSQL = "select * from " + sysLabelList.TableName;

                            if (!string.IsNullOrEmpty(sysLabelList.SqlWhere))
                            {
                                splitSQL += " where " + sysLabelList.SqlWhere;

                                if (sysLabelList.LstMenu.Length > 0)
                                {
                                    splitSQL += " and NodeCode in('" + string.Join(",", sysLabelList.LstMenu).Replace(",", "','") + "')";
                                }
                            }
                            else
                            {
                                if (sysLabelList.LstMenu.Length > 0)
                                {
                                    splitSQL += " where NodeCode in('" + string.Join(",", sysLabelList.LstMenu).Replace(",", "','") + "')";
                                }
                            }

                            splitSQL += " and IsDel=0 and FlowState=99 and SiteID=" + this.siteID;
                        }
                        catch { }
                        break;
                }

                if (!string.IsNullOrEmpty(splitLabel.Name))
                {

                    switch (splitLabel.SplitType)
                    {
                        case "0":       // 简单型
                            split.Parser = new SimpleSplit();
                            break;
                        case "1":       // 前几页 中间几页 未尾几页的显示方式
                            split.Parser = new IntermittentSplit();
                            break;
                        default:
                            split.Parser = new SimpleSplit();
                            break;      // 简单型 
                    }

                    split.Parser.RootUrl = "";
                    split.Parser.PageType = this.fileType;
                    split.Parser.PageSize = pageSize;
                    split.Parser.PageName = fileName;
                    split.Parser.LabelContent = splitLabel.Content;
                }

                if (!string.IsNullOrEmpty(splitSQL))
                {
                    rsCount = dal.GetSplitCountRS(splitSQL);

                    if (rsCount < pageSize)
                    {
                        pageCount = 1;
                    }
                    else
                    {
                        if (rsCount % pageSize == 0)
                        {
                            pageCount = rsCount / pageSize;
                        }
                        else
                        {
                            pageCount = rsCount / pageSize + 1;
                        }
                    }

                    if (split.Parser != null)
                    {
                        split.Parser.TotalPage = pageCount;

                        for (int k = 1; k <= pageCount; k++)
                        {
                            switch (needSplitLabel.Type)
                            {
                                case NeedSplitLabelType.FreeLabel:
                                    parsedContent = freeLabelParser.Parse((FreeLabel)needSplitLabel.Label, k, pageSize);
                                    break;
                                case NeedSplitLabelType.SysLabelList:
                                    parsedContent = systemLabelParser.ParseList((SysLabelList)needSplitLabel.Label, k, this.siteDirUrl);
                                    break;
                            }

                            if (k == 1)
                            {
                                savePath = listDirPath + fileName + "." + fileType;
                            }
                            else
                            {
                                savePath = listDirPath + fileName + "_" + k.ToString() + "." + fileType;
                            }

                            if (!unPublished || !File.Exists(savePath))
                            {
                                parsedContent = templateContent.Replace(labelName, parsedContent);
                                splitHtmlCode = split.Parser.GetHtmlCode(k);
                                parsedContent = parsedContent.Replace(splitLabel.Config, splitHtmlCode);

                                if (!string.IsNullOrEmpty(saveFilePath))
                                {
                                    savePath = saveFilePath;
                                }

                                if (drMenu != null)
                                {
                                    parsedContent = parsedContent.Replace("{$MetaTitle}", this.siteTitle + " － " + drMenu["NodeName"].ToString());
                                    parsedContent = parsedContent.Replace("{$MetaDescription}", drMenu["Meta_Description"].ToString());
                                    parsedContent = parsedContent.Replace("{$MetaKeyword}", drMenu["Meta_Keywords"].ToString());
                                }
                                else
                                {
                                    parsedContent = parsedContent.Replace("{$MetaTitle}", this.siteTitle);
                                    parsedContent = parsedContent.Replace("{$MetaDescription}", this.siteTitle);
                                    parsedContent = parsedContent.Replace("{$MetaKeyword}", this.siteTitle);
                                }

                                try
                                {
                                    File.WriteAllText(savePath, PreSave(parsedContent, nodeCode), Encoding.UTF8);     // 保存列表页
                                }
                                catch { }
                            }
                        }

                        if (needSplitLabel.Type == NeedSplitLabelType.SysLabelList && ((SysLabelList)needSplitLabel.Label).LstSubModel != null && ((SysLabelList)needSplitLabel.Label).LstSubModel.Count > 0)
                        {
                            this.dicSubModel.Add(((SysLabelList)needSplitLabel.Label).TableName, ((SysLabelList)needSplitLabel.Label).LstSubModel);
                        }
                    }
                }
            }
            else   // 不存在分页
            {
                savePath = listDirPath + fileName + "." + fileType;
                templateContent = HttpContext.Current.Server.HtmlEncode(templateContent);

                if (!string.IsNullOrEmpty(saveFilePath))
                {
                    savePath = saveFilePath;
                }

                if (!unPublished || !File.Exists(savePath))
                {
                    if (drMenu != null)
                    {
                        templateContent = templateContent.Replace("{$MetaTitle}", this.siteTitle + " － " + drMenu["NodeName"].ToString());
                        templateContent = templateContent.Replace("{$MetaDescription}", drMenu["Meta_Description"].ToString());
                        templateContent = templateContent.Replace("{$MetaKeyword}", drMenu["Meta_Keywords"].ToString());
                    }
                    else
                    {
                        templateContent = templateContent.Replace("{$MetaTitle}", this.siteTitle);
                        templateContent = templateContent.Replace("{$MetaDescription}", this.siteTitle);
                        templateContent = templateContent.Replace("{$MetaKeyword}", this.siteTitle);
                    }

                    try
                    {
                        templateContent = PreSave(templateContent, nodeCode);
                        File.WriteAllText(savePath, templateContent, Encoding.UTF8);     // 保存列表页
                    }
                    catch { }
                }
            }

            ParseSubModel();
        }
        #endregion

        #region 子模型解析

        #region 主函数
        public void ParseSubModel()
        {
            string templatePath;
            List<SubModelParam> lstSubModel;
            string tableName;

            if (this.dicSubModel != null && this.dicSubModel.Count > 0)
            {
                foreach (string key in this.dicSubModel.Keys)
                {
                    tableName = key;
                    lstSubModel = this.dicSubModel[key];

                    foreach (SubModelParam subModel in lstSubModel)
                    {
                        if (subModel.LstParentID != null)
                        {
                            foreach (string parentID in subModel.LstParentID)
                            {
                                templatePath = subModel.IndexTemplate;

                                if (!templatePath.Contains(":"))
                                {
                                    templatePath = this.RootTemplate + templatePath;
                                }

                                ParseSubModelList(templatePath, subModel.FieldName, parentID, subModel);
                            }
                        }
                    }
                }

                this.dicSubModel.Clear();
            }

            if (this.dicSubModelContentParam != null && this.dicSubModelContentParam.Count > 0)
            {
                foreach (string key in this.dicSubModelContentParam.Keys)
                {
                    ParseSubModelContent(this.dicSubModelContentParam[key]);
                }

                this.dicSubModelContentParam.Clear();
            }
        }
        #endregion

        #region 列表
        private void ParseSubModelList(string templatePath, string fieldName, string parentID, SubModelParam subModel)
        {
            string templateContent;                  // 模板文件内容
            ParseFreeLabel freeLabelParser;          // 自由标签解析类
            List<FreeLabel> lstFreeLabel;            // 当前模板的所有自由标签
            List<SplitLabel> lstSplitLabel;          // 当前模板的所有分页标签
            List<SysLabelList> lstSysLabelList;      // 列表类型系统标签
            FreeLabelConfig freeLabelConfig;         // 自由标签配置
            NeedSplitLabel needSplitLabel;           // 保存要分页的标签
            SplitLabel splitLabel;                   // 分页标签参数
            string parsedContent;                    // 经解析后的标签内容
            int rsCount;                             // 记录总数
            int pageCount;                           // 分页总数
            string savePath;                         // 保存路径
            string splitHtmlCode;                    // 分页HTML代码
            string fileName;                         // 保存的文件名
            string labelName;                        // 从模板中抓取的标签
            int pageSize;                            // 分页大小
            string splitSQL;                         // 分页SQL
            string listDirPath;
            SubModelContentParam subModelContentParam;

            needSplitLabel = new NeedSplitLabel();
            freeLabelParser = new ParseFreeLabel(null);
            splitLabel = new SplitLabel();
            freeLabelConfig = new FreeLabelConfig();

            systemLabelParser.NodeCode = null;
            freeLabelParser.CSaveType = ContentPageSaveType.Content;
            freeLabelParser.MenuList = this.MenuList;
            freeLabelParser.FileType = this.fileType;
            templateContent = File.ReadAllText(templatePath);
            templateContent = templateContent.Replace("\r\n", "");
            FormatUrl(ref templateContent, this.siteDirUrl);                                 // 将路径地址加上站点URL
            lstFreeLabel = freeLabelParser.GetLabelList(templateContent);                    // 抓取自由标签
            lstSplitLabel = split.GetSplitLabel(templateContent, SplitLabelList);            // 抓取分页标签
            lstSysLabelList = systemLabelParser.GetSysLabelList(templateContent);            // 抓取列表类型系统标签

            rsCount = 0;
            splitHtmlCode = string.Empty;
            splitSQL = string.Empty;
            pageSize = 0;
            labelName = string.Empty;
            parsedContent = string.Empty;
            fileName = parentID + fieldName;
            listDirPath = this.siteDir + "List\\";

            if (!Directory.Exists(listDirPath) && !string.IsNullOrEmpty(listDirPath))     // 创建目录
            {
                Directory.CreateDirectory(listDirPath);
            }

            foreach (FreeLabel label in lstFreeLabel)  // 解析模板中的自由标签
            {
                if (!string.IsNullOrEmpty(label.SplitLabelID) && label.SplitLabelID.Trim() != "")  // 分页
                {
                    if (needSplitLabel.Label == null)
                    {
                        needSplitLabel.Label = label;
                        needSplitLabel.Type = NeedSplitLabelType.FreeLabel;
                    }
                }
            }

            foreach (SysLabelList label in lstSysLabelList)          // 解析模板中的列表类型系统标签
            {
                if (label.IsSplit && needSplitLabel.Label == null)  // 分页
                {
                    if (label.IsSubModel)
                    {
                        if (!string.IsNullOrEmpty(label.SqlWhere) && label.SqlWhere.Trim() != "")
                        {
                            label.SqlWhere += " and ParentID='" + parentID + "' ";
                        }
                        else
                        {
                            label.SqlWhere = " ParentID='" + parentID + "' ";
                        }
                    }

                    needSplitLabel.Label = label;
                    needSplitLabel.Type = NeedSplitLabelType.SysLabelList;
                }
            }

            templateContent = ParseCommonBlock(templateContent, null, freeLabelParser);
            templateContent = ParseSysLabelList(lstSysLabelList, templateContent, parentID, subModel);

            if (needSplitLabel.Label != null && lstSplitLabel.Count > 0)  // 存在分页
            {
                switch (needSplitLabel.Type)
                {
                    case NeedSplitLabelType.FreeLabel:      // 自由标签
                        try
                        {
                            FreeLabel freeLabel;
                            freeLabel = (FreeLabel)needSplitLabel.Label;
                            labelName = freeLabel.Config;
                            freeLabelConfig = freeLabelParser.GetFreeLabelConfig(freeLabel.Name);
                            pageSize = freeLabelConfig.PageSize;
                            splitSQL = freeLabelConfig.SQL;
                            splitLabel = GetSplitLabel(lstSplitLabel, NeedSplitLabelType.FreeLabel, ((FreeLabel)needSplitLabel.Label).SplitLabelID);
                        }
                        catch { }
                        break;
                    case NeedSplitLabelType.SysLabelList:   // 列表类型系统标签
                        try
                        {
                            SysLabelList sysLabelList;

                            sysLabelList = (SysLabelList)needSplitLabel.Label;
                            labelName = sysLabelList.LabelName;
                            splitLabel = GetSplitLabel(lstSplitLabel, NeedSplitLabelType.SysLabelList, null);
                            pageSize = sysLabelList.PageSize;

                            splitSQL = "select * from " + sysLabelList.TableName;

                            if (!string.IsNullOrEmpty(sysLabelList.SqlWhere))
                            {
                                splitSQL += " where " + sysLabelList.SqlWhere;

                                if (sysLabelList.LstMenu.Length > 0)
                                {
                                    splitSQL += " and NodeCode in('" + string.Join(",", sysLabelList.LstMenu).Replace(",", "','") + "')";
                                }
                            }
                            else
                            {
                                if (sysLabelList.LstMenu.Length > 0)
                                {
                                    splitSQL += " where NodeCode in('" + string.Join(",", sysLabelList.LstMenu).Replace(",", "','") + "')";
                                }
                            }

                            splitSQL += " and IsDel=0 and FlowState=99 and SiteID=" + this.siteID;
                        }
                        catch { }
                        break;
                }

                if (!string.IsNullOrEmpty(splitLabel.Name))
                {

                    switch (splitLabel.SplitType)
                    {
                        case "0":       // 简单型
                            split.Parser = new SimpleSplit();
                            break;
                        case "1":       // 前几页 中间几页 未尾几页的显示方式
                            split.Parser = new IntermittentSplit();
                            break;
                        default:
                            split.Parser = new SimpleSplit();
                            break;      // 简单型 
                    }

                    split.Parser.RootUrl = "";
                    split.Parser.PageType = this.fileType;
                    split.Parser.PageSize = pageSize;
                    split.Parser.PageName = fileName;
                    split.Parser.LabelContent = splitLabel.Content;
                }

                if (!string.IsNullOrEmpty(splitSQL))
                {
                    rsCount = dal.GetSplitCountRS(splitSQL);

                    if (rsCount < pageSize)
                    {
                        pageCount = 1;
                    }
                    else
                    {
                        if (rsCount % pageSize == 0)
                        {
                            pageCount = rsCount / pageSize;
                        }
                        else
                        {
                            pageCount = rsCount / pageSize + 1;
                        }
                    }

                    if (split.Parser != null)
                    {
                        split.Parser.TotalPage = pageCount;

                        for (int k = 1; k <= pageCount; k++)
                        {
                            switch (needSplitLabel.Type)
                            {
                                case NeedSplitLabelType.FreeLabel:
                                    parsedContent = freeLabelParser.Parse((FreeLabel)needSplitLabel.Label, k, pageSize);
                                    break;
                                case NeedSplitLabelType.SysLabelList:
                                    parsedContent = systemLabelParser.ParseList((SysLabelList)needSplitLabel.Label, k, this.siteDirUrl);
                                    break;
                            }

                            if (k == 1)
                            {
                                savePath = listDirPath + fileName + "." + fileType;
                            }
                            else
                            {
                                savePath = listDirPath + fileName + "_" + k.ToString() + "." + fileType;
                            }

                            if (!unPublished || !File.Exists(savePath))
                            {
                                parsedContent = templateContent.Replace(labelName, parsedContent);
                                splitHtmlCode = split.Parser.GetHtmlCode(k);
                                parsedContent = parsedContent.Replace(splitLabel.Config, splitHtmlCode);

                                parsedContent = parsedContent.Replace("{$MetaTitle}", this.siteTitle);
                                parsedContent = parsedContent.Replace("{$MetaDescription}", this.siteTitle);
                                parsedContent = parsedContent.Replace("{$MetaKeyword}", this.siteTitle);


                                try
                                {
                                    File.WriteAllText(savePath, PreSave(parsedContent, string.Empty), Encoding.UTF8);     // 保存列表页
                                }
                                catch { }
                            }

                            if (subModel != null && !string.IsNullOrEmpty(subModel.ContentTemplate) && needSplitLabel.Type == NeedSplitLabelType.SysLabelList)
                            {
                                subModelContentParam = new SubModelContentParam();

                                if (!subModel.ContentTemplate.Contains(":"))
                                {
                                    subModelContentParam.TemplatePath = this.RootTemplate + subModel.ContentTemplate;
                                }
                                else
                                {
                                    subModelContentParam.TemplatePath = subModel.ContentTemplate;
                                }

                                subModelContentParam.ParentID = parentID;
                                subModelContentParam.TableName = ((SysLabelList)needSplitLabel.Label).TableName;

                                if (!this.dicSubModelContentParam.ContainsKey(parentID))
                                {
                                    this.dicSubModelContentParam.Add(parentID, subModelContentParam);
                                }
                            }
                        }
                    }
                }
            }
            else   // 不存在分页
            {
                savePath = listDirPath + fileName + "." + fileType;
                templateContent = HttpContext.Current.Server.HtmlEncode(templateContent);

                if (!unPublished || !File.Exists(savePath))
                {
                    templateContent = templateContent.Replace("{$MetaTitle}", this.siteTitle);
                    templateContent = templateContent.Replace("{$MetaDescription}", this.siteTitle);
                    templateContent = templateContent.Replace("{$MetaKeyword}", this.siteTitle);

                    try
                    {
                        File.WriteAllText(savePath, PreSave(templateContent, string.Empty), Encoding.UTF8);     // 保存列表页
                    }
                    catch { }
                }
            }
        }
        #endregion

        #region 内容
        private void ParseSubModelContent(SubModelContentParam param)
        {
            DataTable dataSource;
            string sqlWhere;

            sqlWhere = " ParentID ='" + param.ParentID + "'";
            dataSource = dal.GetMenuDataSource(this.siteID, null, param.TableName, sqlWhere);

            if (dataSource != null)
            {
                ParseContentTemplate(param.TemplatePath, null, null, ContentPageSaveType.ContentAndDate, dataSource, param.ParentID);
            }
        }
        #endregion

        #endregion

        #region 获取列表的分页标签
        private SplitLabel GetSplitLabel(List<SplitLabel> lstSplitLabel, NeedSplitLabelType labelType, string splitLabelID)
        {
            SplitLabel splitLabel;

            splitLabel = new SplitLabel();

            switch (labelType)
            {
                case NeedSplitLabelType.FreeLabel:
                    foreach (SplitLabel itemSplit in lstSplitLabel)
                    {
                        if (itemSplit.ID == splitLabelID)
                        {
                            splitLabel = itemSplit;
                        }
                    }
                    break;
                default:
                    splitLabel = lstSplitLabel[0];
                    break;
            }

            return splitLabel;
        }
        #endregion

        #region 获取站点配置信息
        /// <summary>
        /// 获取站点配置信息
        /// </summary>
        /// <param name="siteDir"></param>
        private void GetSiteConfig(string siteDir)
        {
            string configPath;
            XPathDocument xpathDOC;
            XPathNavigator xpathNav;

            this.siteDir = HttpContext.Current.Server.MapPath("~/" + siteDir);

            if (this.siteDir.Substring(this.siteDir.Length - 2, 2) != "\\")
            {
                this.siteDir = this.siteDir + "\\";
            }

            configPath = HttpContext.Current.Server.MapPath("~/" + siteDir + "/config/SiteParam.config");
            xpathDOC = new XPathDocument(configPath);
            xpathNav = xpathDOC.CreateNavigator();

            xpathNav = xpathNav.SelectSingleNode("/SiteParamConfig/CreateFileExt");

            if (xpathNav != null && xpathNav.Value.Trim() != "")
            {
                this.fileType = xpathNav.Value;
            }
            else
            {
                this.fileType = "html";
            }

            configPath = HttpContext.Current.Server.MapPath("~/" + siteDir + "/config/SiteInfo.config");
            xpathDOC = new XPathDocument(configPath);
            xpathNav = xpathDOC.CreateNavigator();
            xpathNav = xpathNav.SelectSingleNode("/SiteInfoConfig/SiteTitle");

            if (xpathNav != null && xpathNav.Value.Trim() != "")
            {
                this.siteTitle = xpathNav.Value;
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

            if (this.siteDirUrl == null)
            {
                isTrue = false;
            }

            if (this.MenuList == null || this.MenuList.Rows.Count == 0)
            {
                isTrue = false;
            }

            if (this.siteDir == "")
            {
                isTrue = false;
            }

            if (string.IsNullOrEmpty(this.RootTemplate))
            {
                isTrue = false;
            }

            return isTrue;
        }
        #endregion

        #region 获取子栏目
        /// <summary>
        /// 获取子栏目
        /// </summary>
        /// <param name="nodeCode">栏目NodeCode</param>
        /// <param name="lstNodeCode">子内容栏目NodeCode</param>
        /// <param name="isNavMenu">是否是导航栏目</param>
        private void GetChildMenu(string nodeCode, ref List<string> lstNodeCode, bool isNavMenu)
        {
            DataRow[] arrDR;

            arrDR = MenuList.Select("ParentNode='" + nodeCode + "'");

            if (arrDR != null)
            {
                foreach (DataRow dr in arrDR)
                {
                    if (isNavMenu) // 导航栏目
                    {
                        if (dr["NodeType"].ToString().Trim() == "1")
                        {
                            lstNodeCode.Add(dr["NodeCode"].ToString());
                            GetChildMenu(dr["NodeCode"].ToString(), ref lstNodeCode, isNavMenu);
                        }
                    }
                    else  // 内容栏目
                    {
                        if (dr["NodeType"].ToString().Trim() == "0")
                        {

                            lstNodeCode.Add(dr["NodeCode"].ToString());
                        }
                        else
                        {
                            GetChildMenu(dr["NodeCode"].ToString(), ref lstNodeCode, isNavMenu);
                        }
                    }
                }
            }
        }
        #endregion

        #region 导航（非内容）栏目首页生成
        /// <summary>
        /// 导航（非内容）栏目首页生成
        /// </summary>
        /// <param name="lstNodeCode">导航（非内容）栏目首页生成</param>
        private void PublishNavMenu(List<string> lstNodeCode)
        {
            DataRow[] arrNavMenu;                   // 导航栏目记录集
            string templatePath;                    // 生成栏目首页的模板路径
            string menuDirPath;                     // 当前栏目文件路径
            ContentPageSaveType cSaveType;          // 内容文件保存方式

            if (lstNodeCode == null)
            {
                arrNavMenu = MenuList.Select("NodeType=1");  // 导航栏目
            }
            else
            {
                foreach (string nodeCode in lstNodeCode)
                {
                    arrNavMenu = this.MenuList.Select("NodeCode='" + nodeCode + "'");
                    templatePath = string.Empty;

                    if (arrNavMenu != null && arrNavMenu.Length > 0)
                    {
                        menuDirPath = this.siteDir + arrNavMenu[0]["RootDirPath"].ToString().Replace("/", "\\");

                        if (!Directory.Exists(menuDirPath)) // 需创建目录
                        {
                            Directory.CreateDirectory(menuDirPath);
                        }

                        if (arrNavMenu[0]["DefaultTemplate"].ToString().Trim() != "")  // 首页模板
                        {
                            templatePath = this.RootTemplate + arrNavMenu[0]["DefaultTemplate"].ToString();
                        }

                        if (!string.IsNullOrEmpty(templatePath))  // 生成栏目首页
                        {
                            cSaveType = GetContentPageSaveType(arrNavMenu[0]["ContentPageHtmlRule"].ToString());  // 内容保存方式

                            if (File.Exists(templatePath))
                            {
                                ParseTemplate(arrNavMenu[0], false, templatePath, nodeCode, menuDirPath, cSaveType, null);
                            }
                        }
                    }
                }
            }

        }
        #endregion

        #region 文件同步
        /// <summary>
        /// 文件同步
        /// </summary>
        /// <param name="isCopyFile">是否同步样式文件</param>
        private void SyncFile(bool isCopyFile)
        {
            string plusDir;
            string targetPlusDir;
            string skinsDir;          // 模板风格（样式）目录
            string targetSkinsDir;    // 当前站点下的风格（样式）目录
            string htmlDir;           // 模板静态页面
            string targetHtmlDir;     // 前台静态页面 
            List<string> lstFileType; // 要进行路径替换的文件类型

            lstFileType = new List<string>();

            targetPlusDir = this.siteDir + "plus\\";
            targetSkinsDir = this.siteDir + "Skins\\";
            targetHtmlDir = this.siteDir + "IncludeFile\\";
            plusDir = this.RootTemplate + "plus\\";
            skinsDir = this.RootTemplate + "Skins\\";
            htmlDir = this.RootTemplate + "IncludeFile\\";
            lstFileType.Add(".html");
            lstFileType.Add(".htm");
            lstFileType.Add(".css");
            lstFileType.Add(".js");

            if (Directory.Exists(skinsDir) && isCopyFile)
            {
                MoveFile(skinsDir, targetSkinsDir, lstFileType);
            }

            if (Directory.Exists(htmlDir))
            {
                MoveFile(htmlDir, targetHtmlDir, lstFileType);
            }

            if (Directory.Exists(plusDir) && isCopyFile)
            {
                MoveFile(plusDir, targetPlusDir, lstFileType);
            }

            if (File.Exists(this.RootTemplate + "search.aspx") && isCopyFile)
            {
                File.Copy(this.RootTemplate + "search.aspx", this.siteDir + "search.aspx", true);
            }
        }

        // 复制文件与文件夹
        private void MoveFile(string sourceDirPath, string targetDirPath, List<string> lstFileType)
        {
            string[] arrFilePath;
            string[] arrDirPath;
            string fileContent;
            string targetFilePath;

            arrFilePath = Directory.GetFiles(sourceDirPath);
            arrDirPath = Directory.GetDirectories(sourceDirPath);

            if (!Directory.Exists(targetDirPath))        // 目标文件夹不存在时
            {
                Directory.CreateDirectory(targetDirPath);
            }

            foreach (string filePath in arrFilePath)  // 复制所有文件
            {
                targetFilePath = filePath.Replace(sourceDirPath, targetDirPath);

                if (lstFileType.Contains(Path.GetExtension(filePath).ToLower()))
                {
                    fileContent = File.ReadAllText(filePath);
                    FormatUrl(ref fileContent, this.siteDirUrl);        // 为文件中URL添加站点路径前缀

                    try
                    {
                        File.WriteAllText(targetFilePath, fileContent);
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        File.Copy(filePath, targetFilePath, true);
                    }
                    catch { }
                }
            }

            foreach (string dirPath in arrDirPath)   // 复制文件夹
            {
                try
                {
                    MoveFile(dirPath, dirPath.Replace(sourceDirPath, targetDirPath), lstFileType);
                }
                catch { }
            }
        }
        #endregion

        #region 内容保存前处理
        private string PreSave(string content, string nodeCode)
        {
            Regex regVar;
            MatchCollection collectVar;
            DataRow[] menuDR;

            content = HttpContext.Current.Server.HtmlDecode(content);
            regVar = new Regex(@"\{\$MenuName\s+NodeCode\s*=\s*[""'](?<1>[^""']*)[""']\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);          // 栏目名称
            collectVar = regVar.Matches(content);

            foreach (Match matchItem in collectVar)
            {
                if (matchItem.Groups[1].Value.Trim() != "")
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + matchItem.Groups[1].Value + "'");
                }
                else
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");
                }

                if (menuDR != null && menuDR.Length > 0)
                {
                    content = content.Replace(matchItem.Value, menuDR[0]["NodeName"].ToString());
                }
            }

            regVar = new Regex(@"\{\$MenuURL\s+NodeCode\s*=\s*[""'](?<1>[^""']*)[""']\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);          // 栏目路径
            collectVar = regVar.Matches(content);

            foreach (Match matchItem in collectVar)
            {
                if (matchItem.Groups[1].Value.Trim() != "")
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + matchItem.Groups[1].Value + "'");
                }
                else
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");
                }

                if (menuDR != null && menuDR.Length > 0)
                {
                    content = content.Replace(matchItem.Value, this.siteDirUrl + menuDR[0]["RootDirPath"].ToString());
                }
            }

            regVar = new Regex(@"\{\$MenuIntro\s+NodeCode\s*=\s*[""'](?<1>[^""']*)[""']\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);          // 栏目描述
            collectVar = regVar.Matches(content);

            foreach (Match matchItem in collectVar)
            {
                if (matchItem.Groups[1].Value.Trim() != "")
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + matchItem.Groups[1].Value + "'");
                }
                else
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");
                }

                if (menuDR != null && menuDR.Length > 0)
                {
                    content = content.Replace(matchItem.Value, menuDR[0]["NodelDesc"].ToString());
                }
            }

            regVar = new Regex(@"\{\$MenuBanner\s+NodeCode\s*=\s*[""'](?<1>[^""']*)[""']\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);          // 栏目banner图片（flash）
            collectVar = regVar.Matches(content);

            foreach (Match matchItem in collectVar)
            {
                if (matchItem.Groups[1].Value.Trim() != "")
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + matchItem.Groups[1].Value + "'");
                }
                else
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");
                }

                if (menuDR != null && menuDR.Length > 0)
                {
                    if (menuDR[0]["Banner"].ToString() != "" && menuDR[0]["Banner"].ToString().Length > 1 && (menuDR[0]["Banner"].ToString().IndexOf("http://") > -1 || menuDR[0]["Banner"].ToString().Substring(0, 1) == "/"))
                    {
                        content = content.Replace(matchItem.Value, menuDR[0]["Banner"].ToString());
                    }
                    else
                    {
                        content = content.Replace(matchItem.Value, this.UploadImgUrl + menuDR[0]["Banner"].ToString());
                    }
                }
            }

            regVar = new Regex(@"\{\$MenuIcon\s+NodeCode\s*=\s*[""'](?<1>[^""']*)[""']\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);          // 栏目图标
            collectVar = regVar.Matches(content);

            foreach (Match matchItem in collectVar)
            {
                if (matchItem.Groups[1].Value.Trim() != "")
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + matchItem.Groups[1].Value + "'");
                }
                else
                {
                    menuDR = this.dtMenuList.Select("NodeCode='" + nodeCode + "'");
                }

                if (menuDR != null && menuDR.Length > 0)
                {
                    if (menuDR[0]["NodelIcon"].ToString() != "" && menuDR[0]["NodelIcon"].ToString().Length > 1 && (menuDR[0]["NodelIcon"].ToString().IndexOf("http://") > -1 || menuDR[0]["NodelIcon"].ToString().Substring(0, 1) == "/"))
                    {
                        content = content.Replace(matchItem.Value, menuDR[0]["NodelIcon"].ToString());
                    }
                    else
                    {
                        content = content.Replace(matchItem.Value, this.UploadImgUrl + menuDR[0]["NodelIcon"].ToString());
                    }
                }
            }

            content = content.Replace("{$SiteID}", this.siteID.ToString());
            content = Regex.Replace(content, @"\{\$NodeCode\}", nodeCode, RegexOptions.IgnoreCase | RegexOptions.Singleline);                             // 节点NodeCode
            content = Regex.Replace(content, @"\{\$SiteUrl\}", this.siteDirUrl, RegexOptions.IgnoreCase | RegexOptions.Singleline);                       // 当前站点URL
            content = Regex.Replace(content, @"\{\$UploadImg\}", this.UploadImgUrl, RegexOptions.IgnoreCase | RegexOptions.Singleline);                   // 上传图片URL
            content = Regex.Replace(content, @"\{\$MediasURL\}", this.MediasUrl, RegexOptions.Singleline | RegexOptions.IgnoreCase);                      // 上传视频URL                   
            content = Regex.Replace(content, @"\{\$FilesURL\}", this.FilesUrl, RegexOptions.Singleline | RegexOptions.IgnoreCase);                         // 上传文件URL                   
            FormatUrl(ref content, this.siteDirUrl);                                                                                                      // 横板内容中路径添加站点URL
            return content;
        }
        #endregion

        #region 清除所有缓存
        private void ClearCache()
        {
            IDictionaryEnumerator collect = HttpContext.Current.Cache.GetEnumerator();

            while (collect.MoveNext())
            {
                HttpContext.Current.Cache.Remove(collect.Key.ToString());
            }
        }
        #endregion

        #region 生成栏目（下）列表
        private void BindDropDownList(DropDownList ddl, string nodeCode, string nodeNamePrevFix, bool isTop)
        {
            string prevFixContent;
            DataRow[] arrCurrentDR;

            prevFixContent = string.Empty;
            arrCurrentDR = this.MenuList.Select("ParentNode='" + nodeCode + "'");
        }
        #endregion

        #region 解析代码块文件夹中模板
        private void ParseCodeBlock()
        {
            string codeBlockPath;
            string templateContent;
            ParseFreeLabel freeParser;

            codeBlockPath = this.RootTemplate + "CodeBlock//";
            freeParser = new ParseFreeLabel(string.Empty);

            if (!Directory.Exists(this.siteDir + "CodeBlock//"))
            {
                Directory.CreateDirectory(this.siteDir + "CodeBlock//");
            }

            if (Directory.Exists(codeBlockPath))
            {
                foreach (string filePath in Directory.GetFiles(codeBlockPath))
                {
                    templateContent = File.ReadAllText(filePath, Encoding.UTF8);
                    templateContent = ParseCommonBlock(templateContent, null, freeParser);
                    templateContent = ParseSysLabelList(null, templateContent, string.Empty, null);
                    File.WriteAllText(this.siteDir + "CodeBlock//" + filePath.Replace(codeBlockPath, ""), PreSave(templateContent, string.Empty), Encoding.UTF8);
                }
            }
        }
        #endregion

        #endregion

        #region 专题解析

        #region 发布专题
        /// <summary>
        /// 发布专题
        /// </summary>
        /// <param name="lstSpecial">专题ID</param>
        /// <param name="lstSpecialMenu">专题栏目</param>
        public void Execute(List<string> lstSpecial, List<string> lstSpecialMenu)
        {
            string tempatePath;
            string savePath;
            DataRow[] arrSpecialDR;
            DataRow[] arrSepcialMenuDR;

            if (lstSpecial.Count > 0)
            {
                menuPercentage = 30 / lstSpecial.Count;
            }

            if (menuPercentage == 0)
            {
                menuPercentage = 1;
            }

            if (!Directory.Exists(this.siteDir + "ZT/"))
            {
                Directory.CreateDirectory(this.siteDir + "ZT/");
            }

            HProgressBar.Start();        //进度条

            foreach (string specialID in lstSpecial)
            {
                arrSpecialDR = this.DtSpecial.Select("ID='" + specialID + "'");

                if (arrSpecialDR != null && arrSpecialDR.Length > 0)
                {
                    savePath = this.siteDir + "ZT/" + arrSpecialDR[0]["DirectoryName"].ToString() + "/";
                    tempatePath = this.RootTemplate + arrSpecialDR[0]["TemplatePath"].ToString();
                    HProgressBar.Roll("正在发布专题 " + arrSpecialDR[0]["Name"].ToString() + " 列表页...", finishedPercentage);

                    if (File.Exists(tempatePath))
                    {
                        SpiderSpecialTemplate(tempatePath, savePath, null, specialID, arrSpecialDR[0]["DirectoryName"].ToString(), arrSpecialDR[0]);
                    }
                }

                foreach (DataRow dr in this.DtSpecialMenu.Rows)
                {
                    if (dr["SpecialID"].ToString().Trim() == specialID.Trim())
                    {
                        if (!lstSpecialMenu.Contains(dr["ID"].ToString()))
                        {
                            lstSpecialMenu.Add(dr["ID"].ToString());
                        }
                    }
                }

                finishedPercentage += menuPercentage;
            }

            HProgressBar.Roll("开始栏目列表页发布完成...", 30);

            if (lstSpecialMenu.Count > 0)
            {
                if (lstSpecial.Count > 0)
                {
                    menuPercentage = 70 / lstSpecialMenu.Count;
                }
                else
                {
                    menuPercentage = 100 / lstSpecialMenu.Count;
                }
            }

            foreach (string menuID in lstSpecialMenu)
            {
                arrSepcialMenuDR = this.DtSpecialMenu.Select("ID='" + menuID + "'");

                if (arrSepcialMenuDR != null && arrSepcialMenuDR.Length > 0)
                {
                    HProgressBar.Roll("正在发布栏目 " + arrSepcialMenuDR[0]["Name"].ToString() + " 列表页...", finishedPercentage);
                    arrSpecialDR = this.DtSpecial.Select("ID='" + arrSepcialMenuDR[0]["SpecialID"].ToString() + "'");

                    if (arrSpecialDR != null && arrSpecialDR.Length > 0)
                    {
                        savePath = this.siteDir + "ZT/" + arrSpecialDR[0]["DirectoryName"] + "/";
                        tempatePath = this.RootTemplate + arrSepcialMenuDR[0]["TemplatePath"].ToString();

                        if (!Directory.Exists(savePath))
                        {
                            Directory.CreateDirectory(savePath);
                        }

                        if (File.Exists(tempatePath))
                        {
                            SpiderSpecialTemplate(tempatePath, savePath, arrSepcialMenuDR[0], arrSepcialMenuDR[0]["SpecialID"].ToString(), arrSpecialDR[0]["DirectoryName"].ToString(), null);
                        }
                    }
                }

                finishedPercentage += menuPercentage;
            }

            HProgressBar.Roll("专题列表页发布完成...", 100);
        }
        #endregion

        #region 专题模板解析
        private void SpiderSpecialTemplate(string templatePath, string savePathDir, DataRow drSpecialMenu, string specialID, string specialName, DataRow drSpecial)
        {
            string templateContent;                       // 模板文件内容
            List<SplitLabel> lstSplitLabel;               // 当前模板的所有分页标签
            SplitLabel splitLabel;                        // 分页标签参数
            string parsedContent;                         // 经解析后的标签内容
            int rsCount;                                  // 记录总数
            int pageCount;                                // 分页总数
            SysLabelList needSplitLabel;                  // 保存要分页的标签
            string splitHtmlCode;                         // 分页HTML代码
            string fileName;                              // 保存的文件名
            List<string> lstStaticLabel;                  // 静态标签
            List<IncludeLabel> lstIncludeLabel;           // 包含标签
            List<SysLabelList> lstSysLabelList;           // 列表类型系统标签
            List<SpecialMenuLabel> lstSpecialMenuLabel;   // 专题栏目标签
            string labelName;                             // 从模板中抓取的标签
            int pageSize;                                 // 分页大小
            DataSet dataSource;
            string savePath;

            templateContent = File.ReadAllText(templatePath);
            templateContent = templateContent.Replace("\r\n", "");
            FormatUrl(ref templateContent, this.siteDirUrl);                     // 将路径地址加上站点URL
            lstSplitLabel = split.GetSplitLabel(templateContent, SplitLabelList);            // 抓取分页标签
            lstStaticLabel = staticLabelParser.GetStaticLabel(templateContent);              // 抓取静态标签
            lstIncludeLabel = staticLabelParser.GetIncludeLabel(templateContent);            // 抓取包含标签
            lstSysLabelList = systemLabelParser.GetSysLabelList(templateContent);            // 抓取列表类型系统标签
            lstSpecialMenuLabel = systemLabelParser.GetSysLabelSpecialMenu(templateContent, specialID);

            dataSource = null;
            rsCount = 0;
            splitHtmlCode = string.Empty;
            pageSize = 0;
            labelName = string.Empty;
            parsedContent = string.Empty;
            needSplitLabel = null;

            if (drSpecialMenu != null)
            {
                fileName = drSpecialMenu["DirectoryName"].ToString();
            }
            else
            {
                fileName = defaultFileName;
            }

            foreach (SpecialMenuLabel label in lstSpecialMenuLabel) // 解析专题栏目标签
            {
                parsedContent = systemLabelParser.ParseSpecialMenu(label, this.siteDirUrl + "ZT/" + specialName + "/");
                templateContent = templateContent.Replace(label.LabelName, parsedContent);
            }

            foreach (SysLabelList label in lstSysLabelList)         // 解析模板中的列表类型系统标签
            {
                if (needSplitLabel == null && label.IsSplit)  // 分页
                {
                    needSplitLabel = label;
                }
                else  // 不分页
                {
                    if (!string.IsNullOrEmpty(label.TableName) && label.TableName.Trim() != "")
                    {
                        parsedContent = systemLabelParser.ParseList(label, 0, this.siteDirUrl);
                    }
                    else
                    {
                        if (label.LstMenu != null && label.LstMenu.Length > 0 && !string.IsNullOrEmpty(label.LstMenu[0]))
                        {
                            dataSource = dal.GetSpecialMenuDataSource(specialID, label.LstMenu[0], 0, label.PageSize);
                        }
                        else
                        {
                            dataSource = dal.GetSpecialMenuDataSource(drSpecialMenu["SpecialID"].ToString(), drSpecialMenu["ID"].ToString(), 0, label.PageSize);
                        }

                        if (dataSource != null)
                        {
                            parsedContent = systemLabelParser.ParseList(label, dataSource.Tables[1], this.siteDirUrl);
                        }
                        else
                        {
                            parsedContent = string.Empty;
                        }
                    }

                    templateContent = templateContent.Replace(label.LabelName, parsedContent);
                }
            }

            foreach (string label in lstStaticLabel)                 // 解析静态标签
            {
                parsedContent = staticLabelParser.ParseStatic(label, this.siteDirUrl, this.UploadImgUrl, this.MenuList, this.fileType);
                templateContent = templateContent.Replace(label, parsedContent);
            }

            foreach (IncludeLabel label in lstIncludeLabel)         // 解析模板中的包含标签
            {
                parsedContent = staticLabelParser.ParseInclude(label, this.RootTemplate);
                templateContent = templateContent.Replace(label.Content, parsedContent);
            }

            if (drSpecialMenu != null)
            {
                templateContent = templateContent.Replace("{$MetaTitle}", this.siteTitle + " － " + drSpecialMenu["Name"].ToString());
                templateContent = templateContent.Replace("{$MetaDescription}", drSpecialMenu["MetaDescription"].ToString());
                templateContent = templateContent.Replace("{$MetaKeyword}", drSpecialMenu["MetaKeyword"].ToString());
            }

            if (drSpecial != null)
            {
                templateContent = templateContent.Replace("{$MetaTitle}", this.siteTitle + " － " + drSpecial["Name"].ToString());
                templateContent = templateContent.Replace("{$MetaDescription}", drSpecial["MetaDescription"].ToString());
                templateContent = templateContent.Replace("{$MetaKeyword}", drSpecial["MetaKeyword"].ToString());
            }

            if (needSplitLabel != null && lstSplitLabel.Count > 0)  // 存在分页
            {
                labelName = needSplitLabel.LabelName;
                splitLabel = GetSplitLabel(lstSplitLabel, NeedSplitLabelType.SysLabelList, null);
                pageSize = needSplitLabel.PageSize;

                if (needSplitLabel.LstMenu != null && needSplitLabel.LstMenu.Length > 0 && !string.IsNullOrEmpty(needSplitLabel.LstMenu[0]))
                {
                    dataSource = dal.GetSpecialMenuDataSource(specialID, needSplitLabel.LstMenu[0], 0, pageSize);
                }
                else
                {
                    dataSource = dal.GetSpecialMenuDataSource(specialID, drSpecialMenu["ID"].ToString(), 0, pageSize);
                }

                if (dataSource != null && dataSource.Tables.Count > 1)
                {
                    if (!string.IsNullOrEmpty(splitLabel.Name))
                    {
                        switch (splitLabel.SplitType)
                        {
                            case "0":       // 简单型
                                split.Parser = new SimpleSplit();
                                break;
                            case "1":       // 前几页 中间几页 未尾几页的显示方式
                                split.Parser = new IntermittentSplit();
                                break;
                            default:
                                split.Parser = new SimpleSplit();
                                break;      // 简单型 
                        }

                        split.Parser.RootUrl = "";
                        split.Parser.PageType = this.fileType;
                        split.Parser.PageSize = pageSize;
                        split.Parser.PageName = drSpecialMenu["DirectoryName"].ToString();
                        split.Parser.LabelContent = splitLabel.Content;
                    }

                    rsCount = Common.Utils.ParseInt(dataSource.Tables[0].Rows[0][0].ToString(), 0);

                    if (rsCount < pageSize)
                    {
                        pageCount = 1;
                    }
                    else
                    {
                        if (rsCount % pageSize == 0)
                        {
                            pageCount = rsCount / pageSize;
                        }
                        else
                        {
                            pageCount = rsCount / pageSize + 1;
                        }
                    }

                    if (split.Parser != null)
                    {
                        split.Parser.TotalPage = pageCount;

                        for (int k = 1; k <= pageCount; k++)
                        {
                            if (needSplitLabel.LstMenu != null && needSplitLabel.LstMenu.Length > 0 && !string.IsNullOrEmpty(needSplitLabel.LstMenu[0]))
                            {
                                dataSource = dal.GetSpecialMenuDataSource(specialID, needSplitLabel.LstMenu[0], (k - 1) * pageSize, pageSize * k);
                            }
                            else
                            {
                                dataSource = dal.GetSpecialMenuDataSource(specialID, drSpecialMenu["ID"].ToString(), (k - 1) * pageSize, pageSize * k);
                            }

                            parsedContent = systemLabelParser.ParseList(needSplitLabel, dataSource.Tables[1], this.siteDirUrl);

                            if (k == 1)
                            {
                                savePath = savePathDir + fileName + "." + fileType;
                            }
                            else
                            {

                                savePath = savePathDir + fileName + "_" + k.ToString() + "." + fileType;
                            }

                            if (!unPublished || !File.Exists(savePath))
                            {
                                parsedContent = templateContent.Replace(labelName, parsedContent);
                                splitHtmlCode = split.Parser.GetHtmlCode(k);
                                parsedContent = parsedContent.Replace(splitLabel.Config, splitHtmlCode);

                                try
                                {
                                    File.WriteAllText(savePath, PreSave(parsedContent, string.Empty));     // 保存列表页
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else   // 不存在分页
            {
                savePath = savePathDir + fileName + "." + fileType;

                if (!unPublished || !File.Exists(savePath))
                {
                    try
                    {
                        File.WriteAllText(savePath, PreSave(templateContent, string.Empty));     // 保存列表页
                    }
                    catch { }
                }
            }
        }
        #endregion

        #endregion

        #region 商品发布

        #region 发布
        /// <summary>
        /// 商品发布
        /// </summary>
        /// <param name="pubParam">发布参数</param>
        /// <param name="pageSize">批处理数</param>
        public void Execute(PublishParam pubParam, int pageSize)
        {
            string sqlWhere;                             // 栏目数据源查询条件
            DataTable dtCategory;                        // 当前站点商品分类
            Dictionary<string, string> dicCategoryID;     // 要发布的商口分类
            int publishedCount;
            string siteIndexTemplatePath;

            dicCategoryID = new Dictionary<string, string>();
            publishedCount = 0;

            dtCategory = dal.GetSubCategoryList("3597", false);
            sqlWhere = string.Empty;
            unPublished = pubParam.UnPublished;


            if (this.IsDisplayProgress)
            {
                HProgressBar.Start();        //进度条
            }

            if (pubParam.IsSiteIndex)   // 解析站点首页模板
            {
                siteIndexTemplatePath = this.RootTemplate + "index.html";

                if (!File.Exists(siteIndexTemplatePath))
                {
                    siteIndexTemplatePath = this.RootTemplate + "default.html";
                }

                if (File.Exists(siteIndexTemplatePath))
                {
                    if (this.IsDisplayProgress)
                    {
                        HProgressBar.Roll("正在发布站点首页...", finishedPercentage);
                    }

                    ParseTemplate(null, false, siteIndexTemplatePath, string.Empty, this.siteDir, new ContentPageSaveType(), null);
                }
            }

            if (pubParam.LstMenu != null && pubParam.LstMenu.Count > 0)
            {
                foreach (string catID in pubParam.LstMenu)
                {
                    GetLeafCategory(dtCategory, catID, ref dicCategoryID);
                }
            }
            else  // 全部发布
            {
                GetLeafCategory(dtCategory, "3597", ref dicCategoryID);
            }

            if (this.IsDisplayProgress)
            {
                menuPercentage = 100 / dicCategoryID.Count;
            }

            switch (pubParam.Type)   //  发布参数
            {
                case PublishType.AddDateRange:      //更新时间
                    sqlWhere = "  AddDate between '" + pubParam.PublishTypeParam[0] + "' and '" + pubParam.PublishTypeParam[1] + "'";
                    break;
                case PublishType.ContentIDEnum:     // 生成内容的ID 多个ID可由 , 分隔
                    sqlWhere = " ID in('" + pubParam.PublishTypeParam[0].Replace(",", "','") + "') ";
                    break;
            }

            foreach (string catID in dicCategoryID.Keys)
            {
                if (this.IsDisplayProgress)
                {
                    HProgressBar.Roll("正在发布商品分类" + dicCategoryID[catID] + "...", finishedPercentage);
                }

                SpiderGoodsContent(catID, pageSize, sqlWhere);
                publishedCount++;
                finishedPercentage = (int)(publishedCount / (float)dicCategoryID.Count * 100);
            }

            if (this.IsDisplayProgress)
            {
                HProgressBar.Roll("发布完成,进行文件同步....", 100);
            }

            SyncFile(pubParam.IsCopyFile);     // 文件同步

            if (this.IsDisplayProgress)
            {
                HProgressBar.Roll("发布完成。&nbsp;<a class=\"list_link\" href=\"javascript:history.back();\">返 回</a>", 100);
            }
        }
        #endregion

        #region 解析商品详细页
        public void SpiderGoodsContent(string catID, int pageSize, string sqlWhere)
        {
            DataSet dataSource;
            int pageIndex;
            int pageCount;
            int totalRS;
            string contentTemplate;

            dataSource = null;
            pageIndex = 1;
            pageCount = 1;
            totalRS = 0;
            contentTemplate = this.RootTemplate + "Content.html";

            do
            {
                dataSource = dal.GetGoodsByCatID(this.siteID, catID, sqlWhere, pageSize, pageIndex);

                if (dataSource.Tables.Count > 1)
                {
                    totalRS = Common.Utils.ParseInt(dataSource.Tables[1].Rows[0][0].ToString(), 0);

                    if (totalRS < pageSize)
                    {
                        pageCount = 1;
                    }
                    else
                    {
                        if (totalRS % pageSize == 0)
                        {
                            pageCount = totalRS / pageSize;
                        }
                        else
                        {
                            pageCount = totalRS / pageSize + 1;
                        }
                    }
                }

                if (dataSource != null && dataSource.Tables.Count > 0 && totalRS > 0)
                {
                    ParseGoodsContentTemplate(contentTemplate, ContentPageSaveType.ContentAndDate, dataSource.Tables[0], catID);
                }
                else
                {
                    break;
                }

                pageIndex++;

            } while (pageIndex <= pageCount);
        }
        #endregion

        #region 筛选要发布的商品分类
        private void GetLeafCategory(DataTable dtCategory, string parentID, ref Dictionary<string, string> dicID)
        {
            DataRow[] currentDR;
            DataRow[] childDR;

            currentDR = dtCategory.Select("ParentID=" + parentID);

            if (currentDR != null && currentDR.Length > 0)
            {
                foreach (DataRow dr in currentDR)
                {
                    childDR = dtCategory.Select("ParentID=" + dr["ID"].ToString());

                    if (childDR != null && childDR.Length > 0)
                    {
                        GetLeafCategory(dtCategory, dr["ID"].ToString(), ref dicID);
                    }
                    else
                    {
                        if (!dicID.ContainsKey(dr["ID"].ToString()))
                        {
                            dicID.Add(dr["ID"].ToString(), dr["Name"].ToString());
                        }
                    }
                }
            }
        }
        #endregion

        #region 解析内容模板
        private void ParseGoodsContentTemplate(string templatePath, ContentPageSaveType cSaveType, DataTable dataSource, string catID)
        {
            string templateContent;                               // 模板文件内容
            ParseFreeLabel freeLabelParser;                       // 自由标签解析类
            List<SysLabelContent> lstSysLabelContent;             // 详细页类型系统标签
            List<SysLabelPeriodical> lstPeriodical;               // 期刊标签 
            List<SysLabelList> lstSysLabelList;                   // 系统标签
            List<SysLabelCommentSubmit> lstSysLabelCommentSubmit; // 系统评论提交内容标签
            string parsedContent;                                 // 经解析后的标签内容
            string savePath;                                      // 保存路径
            Regex fieldReg;                                       // 匹配字段
            MatchCollection fieldMatch;                           // 字段匹配内容
            List<Field> lstFieldParam;                            // 所有字段参数
            Field fieldParam;                                     // 字段参数
            string fieldValue;                                    // 临时变量,字段值
            string contentLabelParsed;                            // 详细页标签解析内容
            int perProgPageAmount;                                // 1%进度所需的内容页
            int counter;

            freeLabelParser = new ParseFreeLabel(string.Empty);
            lstFieldParam = new List<Field>();
            lstSysLabelList = new List<SysLabelList>();

            systemLabelParser.NodeCode = string.Empty;
            freeLabelParser.CSaveType = cSaveType;
            freeLabelParser.MenuList = this.MenuList;
            freeLabelParser.FileType = this.fileType;

            perProgPageAmount = (int)((float)dataSource.Rows.Count / (int)(0.5 * menuPercentage));

            if (perProgPageAmount == 0)
            {
                perProgPageAmount = 1;
            }

            counter = 1;

            if (File.Exists(templatePath))
            {
                templateContent = File.ReadAllText(templatePath);
                templateContent = templateContent.Replace("\r\n", "");
                fieldReg = new Regex(@"\{\$Field\((?<1>\d),(?<2>[^,]+),(?<3>[^)]*)\)\}|\{\$Field\((?<1>\d),(?<2>[^,]+)\)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                lstSysLabelContent = systemLabelParser.GetSysLabelContent(templateContent, false);          // 抓取详细页类型系统标签
                lstPeriodical = systemLabelParser.GetSysLabelPeriodical(templateContent);                          // 抓取期刊标签
                templateContent = ParseCommonBlock(templateContent, string.Empty, freeLabelParser);
                lstSysLabelList = systemLabelParser.GetSysLabelList(templateContent);                              // 抓取列表类型系统标签
                lstSysLabelCommentSubmit = systemLabelParser.GetSysLabelCommentSubmit(templateContent, catID);     // 抓取评论提交内容标签

                if (lstSysLabelCommentSubmit.Count > 0)
                {
                    AddFileRef("<script type=\"text/javascript\" src=\"Skins/JS/Comment.js\"></script>", ref templateContent);
                }

                foreach (DataRow dr in dataSource.Rows)
                {
                    savePath = this.siteDir + "c\\" + string.Format("{0:yyMMdd}", DateTime.Parse(dr["AddDate"].ToString())) + "\\";

                    if (!Directory.Exists(savePath))        // 需创建目录
                    {
                        Directory.CreateDirectory(savePath);
                    }

                    savePath = savePath + dr["ID"].ToString() + "." + fileType;

                    if (!unPublished || !File.Exists(savePath))
                    {
                        if (this.IsDisplayProgress)
                        {
                            HProgressBar.Roll("正在生成内容文件 " + dr["ID"].ToString() + "." + fileType + " ...", finishedPercentage);
                        }

                        if (counter % perProgPageAmount == 0)
                        {
                            finishedPercentage = finishedPercentage + 1;
                        }

                        parsedContent = ParseSysLabelList(lstSysLabelList, templateContent, dr["ID"].ToString(), null);

                        foreach (SysLabelCommentSubmit label in lstSysLabelCommentSubmit) // 解析评论提交内容标签
                        {
                            contentLabelParsed = systemLabelParser.ParseCommentSubmit(label, dr["ID"].ToString());
                            parsedContent = parsedContent.Replace(label.LabelName, contentLabelParsed);
                        }

                        foreach (SysLabelContent label in lstSysLabelContent)  // 详细页标签
                        {
                            contentLabelParsed = systemLabelParser.ParseContent(label, dr);
                            parsedContent = parsedContent.Replace(label.LabelName, contentLabelParsed);
                        }

                        fieldMatch = fieldReg.Matches(parsedContent);                                                // 匹配循环体中的所有字段

                        foreach (Match matchItem in fieldMatch)             // 抓取所有字段
                        {
                            fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                            parsedContent = parsedContent.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                            lstFieldParam.Add(fieldParam);
                        }

                        foreach (Field fieldItem in lstFieldParam) // 绑定字段值
                        {
                            if (dr[fieldItem.Name] != null)
                            {
                                fieldValue = dr[fieldItem.Name].ToString();

                                if (fieldItem.OutParam != null && fieldItem.OutParam.Length > 0)
                                {
                                    fieldValue = FormatFieldContent(fieldValue, fieldItem.OutType, fieldItem.OutParam);
                                }

                                parsedContent = parsedContent.Replace("{[#" + fieldItem.Name + "#]}", fieldValue);
                            }
                        }

                        if (dr.Table.Columns.Contains("Title") && dr["Title"] != null)
                        {
                            parsedContent = parsedContent.Replace("{$MetaTitle}", dr["Title"].ToString());
                        }
                        else
                        {
                            parsedContent = parsedContent.Replace("{$MetaTitle}", this.siteTitle);
                        }

                        if (dr.Table.Columns.Contains("MetaDescript") && dr["MetaDescript"] != null)
                        {
                            parsedContent = parsedContent.Replace("{$MetaKeyword}", dr["MetaDescript"].ToString());
                        }
                        else
                        {
                            parsedContent = parsedContent.Replace("{$MetaDescription}", this.siteTitle);
                        }

                        if (dr.Table.Columns.Contains("MetaKeyword") && dr["MetaKeyword"] != null)
                        {
                            parsedContent = parsedContent.Replace("{$MetaKeyword}", dr["MetaKeyword"].ToString());
                        }
                        else
                        {
                            parsedContent = parsedContent.Replace("{$MetaKeyword}", this.siteTitle);
                        }

                        try
                        {
                            parsedContent = parsedContent.Replace("{$NewsId}", dr["ID"].ToString());
                            File.WriteAllText(savePath, PreSave(parsedContent, dr["NodeCode"].ToString()));     // 保存内容页
                        }
                        catch { }
                    }
                }
            }

            finishedPercentage += (int)0.5 * menuPercentage - perProgPageAmount;
        }
        #endregion


        #endregion
    }
}