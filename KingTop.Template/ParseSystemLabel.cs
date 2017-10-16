#region 引用程序集
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Net;
using System.IO;
using System.Web;

using KingTop.Template.ParamType;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-17
// 功能描述：内容发布 -- 系统标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template
{
    public class ParseSystemLabel : TPublic
    {
        #region  变量成员
        private string siteUrl;
        private const string labelCacheName = "HQB_Publish_SystemLabel";
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.IPublish dal = (IDAL.Template.IPublish)Assembly.Load(path).CreateInstance(path + ".Template.Publish");
        private string itemContentTag = "{#ItemContent#}";
        private int _siteID;
        private string _nodeCode;
        private string _fileType;
        private string _uploadImgUrl;
        private DataTable _menuList;
        private Dictionary<string, string> _dicLabelList;
        private DataTable _dtSpecialMenu;
        #endregion

        #region 属性
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
        /// 站点ID
        /// </summary>
        public int SiteID
        {
            get { return this._siteID; }
            set { this._siteID = value; }
        }
        /// <summary>
        /// 当前栏目NodeCode
        /// </summary>
        public string NodeCode
        {
            get { return this._nodeCode; }
            set { this._nodeCode = value; }
        }
        /// <summary>
        /// 生成的文件扩展名
        /// </summary>
        public string FileType
        {
            get { return this._fileType; }
            set { this._fileType = value; }
        }
        /// <summary>
        /// 栏目列表
        /// </summary>
        public DataTable MenuList
        {
            get { return this._menuList; }
            set
            {
                this._menuList = value;
                base.dtMenuList = value;
            }
        }
        /// <summary>
        /// 系统标签列表
        /// </summary>
        private Dictionary<string, string> LabelList
        {
            get
            {
                if (this._dicLabelList == null)
                {
                    if (HttpContext.Current.Cache[labelCacheName] != null)
                    {
                        this._dicLabelList = HttpContext.Current.Cache[labelCacheName] as Dictionary<string, string>;
                    }
                    else
                    {
                        this.Load();
                        this._dicLabelList = HttpContext.Current.Cache[labelCacheName] as Dictionary<string, string>;
                    }

                    if (this._dicLabelList.Count == 0)
                    {
                        this.Load();
                        this._dicLabelList = HttpContext.Current.Cache[labelCacheName] as Dictionary<string, string>;
                    }
                }

                return this._dicLabelList;
            }
        }
        /// <summary>
        /// 图片上传路径
        /// </summary>
        public string UploadImgUrl
        {
            set { this._uploadImgUrl = value; }
            get { return this._uploadImgUrl; }
        }
        #endregion

        #region 构造函数
        public ParseSystemLabel(string webSiteUrl)
        {
            this.siteUrl = webSiteUrl;
        }
        #endregion

        #region 加载系统标签至缓存中
        /// <summary>
        /// 加载系统标签至缓存中
        /// </summary>
        private void Load()
        {
            DataTable dt;
            Dictionary<string, string> dicLabelList;
            string labeContent;

            dicLabelList = new Dictionary<string, string>();
            dt = dal.GetLabelList(this.SiteID, 1);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    labeContent = dr["LableContent"].ToString();
                    labeContent = labeContent.Replace("\r\n", "");
                    labeContent = labeContent.Replace("\t", "");
                    labeContent = Regex.Replace(labeContent, @"Skins/", this.siteUrl + "Skins/", RegexOptions.IgnoreCase);

                    dicLabelList.Add(dr["LableName"].ToString(), labeContent);
                }
            }

            HttpContext.Current.Cache[labelCacheName] = dicLabelList;
        }
        #endregion

        #region 单页标签

        #region 抓取单页标签
        /// <summary>
        /// 抓取单页标签
        /// </summary>
        /// <param name="templateContent">模板内容</param>
        /// <returns></returns>
        public List<SinglePageLabel> GetSingleLabel(string templateContent)
        {
            Regex regLabel;                         // 匹配标签
            Regex regParam;                         // 匹配参数
            MatchCollection collectLabel;           // 标签列
            MatchCollection collectParam;           // 参数列
            List<SinglePageLabel> lstSingleLable;   // 抓取的单页标签列
            SinglePageLabel singlePageLabel;        // 单页标签对象

            singlePageLabel = new SinglePageLabel();
            lstSingleLable = new List<SinglePageLabel>();
            regLabel = new Regex(@"\{SinglePage(?<1>[^}]*)\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            regParam = new Regex(@"(?<1>[a-zA-Z]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            collectLabel = regLabel.Matches(templateContent);

            foreach (Match itemLabel in collectLabel)
            {
                singlePageLabel.Content = itemLabel.Value;

                collectParam = regParam.Matches(itemLabel.Groups[1].Value);

                foreach (Match itemParam in collectParam)
                {
                    switch (itemParam.Groups[1].Value.ToLower())
                    {
                        case "length":  // 截取长度
                            singlePageLabel.Length = Common.Utils.ParseInt(itemParam.Groups[2].Value, 0);
                            break;
                        case "isdot":   // 添加省略号
                            singlePageLabel.IsDot = Common.Utils.ParseBool(itemParam.Groups[2].Value);
                            break;
                        case "nodecode": // 所属栏目
                            singlePageLabel.NodeCode = itemParam.Groups[2].Value;
                            break;
                    }
                }

                if (string.IsNullOrEmpty(singlePageLabel.NodeCode)) // 没有设置NodeCode属性
                {
                    singlePageLabel.NodeCode = this.NodeCode;
                }

                lstSingleLable.Add(singlePageLabel);
            }

            return lstSingleLable;
        }
        #endregion

        #region 解析单页标签
        /// <summary>
        /// 解析单页标签
        /// </summary>
        /// <param name="label">单页标签</param>
        /// <returns></returns>
        public string ParseSinglePage(SinglePageLabel label)
        {
            string resultContent;

            resultContent = dal.GetSinglePageContent(this.SiteID, label.NodeCode);

            if (label.Length > 0)  // 截取
            {
                resultContent = Regex.Replace(resultContent, @"(<[^>]+>)|(</[^>]+>)", "", RegexOptions.IgnoreCase);
                resultContent = resultContent.Trim();
                resultContent = resultContent.Replace("\r\n", "").Replace("\t", "");

                if (resultContent.Length > label.Length)
                {
                    resultContent = resultContent.Substring(0, label.Length);
                }
            }

            if (label.IsDot)
            {
                resultContent += "...";
            }

            return resultContent;
        }
        #endregion

        #endregion

        #region 列表

        #region 解析

        #region 入口
        public string ParseList(SysLabelList label, int currentPage, string siteUrl)
        {
            string parseResult;
            string nodeCodeList;
            DataTable dataSource;

            nodeCodeList = string.Empty;

            if (label.LstMenu != null && label.LstMenu.Length > 0)
            {
                nodeCodeList = string.Join(",", label.LstMenu);
            }

            if (currentPage > 0)   // 分页数据源
            {
                dataSource = dal.GetSysLabelListDS(this.SiteID, label.TableName, nodeCodeList, label.SqlWhere, label.SqlOrder, label.PageSize, currentPage);
            }
            else
            {
                dataSource = dal.GetSysLabelListDS(this.SiteID, label.TableName, nodeCodeList, label.SqlWhere, label.SqlOrder, label.PageSize);
            }

            if (!string.IsNullOrEmpty(label.ItemTemplate) && label.ItemTemplate.Trim() != "") // 自定义
            {
                parseResult = ParseListSelfDef(label, dataSource, siteUrl);
            }
            else  // 预定义
            {
                parseResult = ParseListPrevDef(label, dataSource, siteUrl);
            }

            if (dataSource != null && dataSource.Rows.Count > 0 && label.LstSubModel != null && label.LstSubModel.Count > 0)
            {
                foreach (DataRow dr in dataSource.Rows)
                {
                    foreach (SubModelParam subModel in label.LstSubModel)
                    {
                        subModel.LstParentID.Add(dr["ID"].ToString());
                    }
                }
            }

            parseResult = parseResult.Replace("{$SiteUrl}", siteUrl);
            return parseResult;
        }

        public string ParseList(SysLabelList label, DataTable dataSource, string siteUrl)
        {
            string parseResult;

            if (!string.IsNullOrEmpty(label.ItemTemplate) && label.ItemTemplate.Trim() != "") // 自定义
            {
                parseResult = ParseListSelfDef(label, dataSource, siteUrl);
            }
            else  // 预定义
            {
                parseResult = ParseListPrevDef(label, dataSource, siteUrl);
            }

            parseResult = parseResult.Replace("{$SiteUrl}", siteUrl);
            return parseResult;
        }
        #endregion

        #region 自定义
        private string ParseListSelfDef(SysLabelList label, DataTable dataSource, string siteUrl)
        {
            LoopLabelParseParam labelParseParam;    // 标签经解析后的参数
            string nodeCode;                        // 当前栏目NODECODE
            StringBuilder sbResult;                 // 解析结果
            string itemContent;                     // 循环体解析内容
            DataRow[] drMenu;                       // 临时变量，栏目记录
            ContentPageSaveType cSaveType;          // 内容页保存方式
            int counter;

            sbResult = new StringBuilder();
            cSaveType = new ContentPageSaveType();
            labelParseParam = GetLoopLabelParam(label.ItemTemplate, this.itemContentTag);
            itemContent = string.Empty;
            nodeCode = string.Empty;
            counter = 0;

            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                if (labelParseParam.IsLoop)  // 循环
                {
                    foreach (DataRow dr in dataSource.Rows)
                    {
                        nodeCode = dr["NodeCode"].ToString();

                        drMenu = this.MenuList.Select("NodeCode='" + nodeCode + "'");

                        if (drMenu != null && drMenu.Length > 0)
                        {
                            cSaveType = GetContentPageSaveType(drMenu[0]["ContentPageHtmlRule"].ToString());
                        }

                        if (label.LstSubModel != null && label.LstSubModel.Count > 0)
                        {
                            itemContent = SingleRecordBind(dr, labelParseParam.LstField, labelParseParam.ItemContent, nodeCode, this.FileType, siteUrl, ContentPageSaveType.ContentAndDate, label.LstSubModel);
                        }
                        else
                        {
                            itemContent = SingleRecordBind(dr, labelParseParam.LstField, labelParseParam.ItemContent, nodeCode, this.FileType, siteUrl, cSaveType);
                        }

                        itemContent = itemContent.Replace("{$HQB.Num}", counter.ToString());
                        sbResult.Append(itemContent);
                        counter++;
                    }
                }
                else  // 单记录
                {
                    nodeCode = dataSource.Rows[0]["NodeCode"].ToString();
                    drMenu = this.MenuList.Select("NodeCode='" + nodeCode + "'");

                    if (drMenu != null && drMenu.Length > 0)
                    {
                        cSaveType = GetContentPageSaveType(drMenu[0]["ContentPageHtmlRule"].ToString());
                    }

                    if (label.LstSubModel != null && label.LstSubModel.Count > 0)
                    {
                        itemContent = SingleRecordBind(dataSource.Rows[0], labelParseParam.LstField, labelParseParam.ItemContent, nodeCode, this.FileType, siteUrl, cSaveType, label.LstSubModel);
                    }
                    else
                    {
                        itemContent = SingleRecordBind(dataSource.Rows[0], labelParseParam.LstField, labelParseParam.ItemContent, nodeCode, this.FileType, siteUrl, cSaveType);
                    }

                    itemContent = itemContent.Replace("{$HQB.Num}", "0");
                    sbResult.Append(itemContent);
                }
            }

            return sbResult.ToString();
        }
        #endregion

        #region 预定义
        private string ParseListPrevDef(SysLabelList label, DataTable dataSource, string siteUrl)
        {
            string itemTemplate;                // 循环体模板
            string itemContent;                 // 解析后的循环体内容
            bool isFirst;                       // 是否是列表的第一行
            StringBuilder sbContent;            // 列表解析结果
            string cDirUrl;                     // 内容目录URL
            string menuDirUrl;                  // 栏目目录URL
            DataRow[] drMenu;                   // 临时变量，栏目记录
            string nodeCode;                    // 当前记录的NODECODE
            string addDate;                     // 当前记录的添加日期
            ContentPageSaveType cSaveType;      // 内容页保存方式
            int counter;

            sbContent = new StringBuilder();
            itemTemplate = GetListItemTemplate(label);
            isFirst = true;
            menuDirUrl = string.Empty;
            counter = 0;

            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                foreach (DataRow dr in dataSource.Rows)
                {
                    try
                    {
                        nodeCode = dr["NodeCode"].ToString();
                        addDate = dr["AddDate"].ToString();
                    }
                    catch
                    {
                        break;
                    }

                    drMenu = this.MenuList.Select("NodeCode='" + nodeCode + "'");

                    if (drMenu != null && drMenu.Length > 0)
                    {
                        cSaveType = GetContentPageSaveType(drMenu[0]["ContentPageHtmlRule"].ToString());
                        cDirUrl = GetContentDirUrl(siteUrl, nodeCode, cSaveType, addDate);
                    }
                    else
                    {
                        break;
                    }

                    if (!isFirst && !string.IsNullOrEmpty(label.TitleSplitImage))   // 标题分隔图片
                    {
                        switch (label.Container)
                        {
                            case HtmlContainer.Div:
                                sbContent.Append("<div><img src=\"" + this.UploadImgUrl + label.TitleSplitImage + "\"/></div>");
                                break;
                            case HtmlContainer.LI:
                                sbContent.Append("<ul><li><img src=\"" + this.UploadImgUrl + label.TitleSplitImage + "\"/></li></ul>");
                                break;
                        }
                    }
                    else
                    {
                        drMenu = this.MenuList.Select("NodeCode='" + dr["NodeCode"].ToString() + "'");

                        if (drMenu != null && drMenu.Length > 0)
                        {
                            menuDirUrl = siteUrl + drMenu[0]["RootDirPath"].ToString();
                        }
                    }

                    if (label.TitleLength > 0 && dr["Title"].ToString().Length > label.TitleLength)
                    {
                        itemContent = itemTemplate.Replace("{[#Title#]}", dr["Title"].ToString().Substring(0, label.TitleLength));
                    }
                    else
                    {
                        itemContent = itemTemplate.Replace("{[#Title#]}", dr["Title"].ToString());
                    }

                    itemContent = itemContent.Replace("{Link}", cDirUrl + dr["ID"].ToString() + "." + this.FileType);

                    if (label.IsShowTitleImage) // 显示标题图片
                    {
                        itemContent = itemContent.Replace("{[#TitleImage#]}", dr["TitleImage"].ToString());
                    }

                    if (label.IsShowAddDate)    //    显示日期
                    {
                        if (!string.IsNullOrEmpty(label.DateFormat))
                        {
                            itemContent = itemContent.Replace("{[#AddDate#]}", string.Format("{0:" + label.DateFormat + "}", addDate));
                        }
                        else
                        {
                            itemContent = itemContent.Replace("{[#AddDate#]}", addDate);
                        }
                    }

                    if (label.IsShowBrief)    // 是否显示导读（简介）
                    {
                        if (label.BriefLength > 0 && dr["Brief"].ToString().Length > label.BriefLength)
                        {
                            itemContent = itemContent.Replace("{[#Brief#]}", dr["Brief"].ToString().Substring(0, label.BriefLength));
                        }
                        else
                        {
                            itemContent = itemContent.Replace("{[#Brief#]}", dr["Brief"].ToString());
                        }
                    }

                    itemContent = itemContent.Replace("{$HQB.Num}", counter.ToString());
                    sbContent.Append(itemContent);
                    isFirst = false;
                    counter++;
                }
            }

            if (label.IsShowMoreLink)       // 更多链接
            {
                sbContent.Append(GetListMoreLink(label, menuDirUrl));
            }

            return sbContent.ToString();
        }
        #endregion

        #endregion

        #region "更多" 链接模板
        private string GetListMoreLink(SysLabelList label, string linkUrl)
        {
            string moreLink;

            moreLink = string.Empty;

            if (label.MoreLinkIsWord)   // 文字
            {
                switch (label.Container)
                {
                    case HtmlContainer.Div:
                        if (label.Target == LinkOpenType.Self)
                        {
                            moreLink = "<div><a href=\"" + linkUrl + "\" target=\"_self\">" + label.MoreLinkWordOrImageUrl + "</a></div>";
                        }
                        else
                        {
                            moreLink = "<div><a href=\"" + linkUrl + "\" target=\"_blank\">" + label.MoreLinkWordOrImageUrl + "</a></div>";
                        }
                        break;
                    case HtmlContainer.LI:
                        if (label.Target == LinkOpenType.Self)
                        {
                            moreLink = "<ul><li><a href=\"" + linkUrl + "\" target=\"_self\">" + label.MoreLinkWordOrImageUrl + "</a></li></ul>";
                        }
                        else
                        {
                            moreLink = "<ul><li><a href=\"" + linkUrl + "\" target=\"_blank\">" + label.MoreLinkWordOrImageUrl + "</a></li></ul>";
                        }
                        break;
                }
            }
            else  // 图片
            {
                switch (label.Container)
                {
                    case HtmlContainer.Div:
                        if (label.Target == LinkOpenType.Self)
                        {
                            moreLink = "<div><a href=\"" + linkUrl + "\" target=\"_self\"><img src=\"" + this.UploadImgUrl + label.MoreLinkWordOrImageUrl + "\"/></a></div>";
                        }
                        else
                        {
                            moreLink = "<div><a href=\"" + linkUrl + "\" target=\"_blank\"><img src=\"" + this.UploadImgUrl + label.MoreLinkWordOrImageUrl + "\"/></a></div>";
                        }
                        break;
                    case HtmlContainer.LI:
                        if (label.Target == LinkOpenType.Self)
                        {
                            moreLink = "<ul><li><a href=\"" + linkUrl + "\" target=\"_self\"><img src=\"" + this.UploadImgUrl + label.MoreLinkWordOrImageUrl + "\"/></a></li></ul>";
                        }
                        else
                        {
                            moreLink = "<ul><li><a href=\"" + linkUrl + "\" target=\"_blank\"><img src=\"" + this.UploadImgUrl + label.MoreLinkWordOrImageUrl + "\"/></a></li></ul>";
                        }
                        break;
                }
            }


            return moreLink;
        }
        #endregion

        #region  组装显示模板
        private string GetListItemTemplate(SysLabelList label)
        {
            StringBuilder sbItemTemplate;           // 记录显示模板
            string fieldContainer;                  // 字段显示容器  如 <li>
            string rsContainer;                     // 记录显示容器  如 <ul>

            sbItemTemplate = new StringBuilder();

            switch (label.Container)
            {
                case HtmlContainer.Div:
                    fieldContainer = "div";
                    rsContainer = "div";
                    break;
                case HtmlContainer.LI:
                    fieldContainer = "li";
                    rsContainer = "ul";
                    break;
                default:
                    fieldContainer = "li";
                    rsContainer = "ul";
                    break;
            }

            if (!string.IsNullOrEmpty(label.LineHeight))
            {
                sbItemTemplate.Append("<" + rsContainer + " style=\"line-height:" + label.LineHeight + "px;\">");
            }
            else
            {
                sbItemTemplate.Append("<" + rsContainer + ">");
            }

            if (label.IsShowTitleImage)    // 显示标题图片
            {
                switch (label.Target)
                {
                    case LinkOpenType.Self:
                        sbItemTemplate.Append("<a href=\"{Link}\" target=\"_self\">");
                        break;
                    default:
                        sbItemTemplate.Append("<a href=\"{Link}\" target=\"_blank\">");
                        break;
                }

                sbItemTemplate.Append("<img src=\"{[#TitleImage#]}\" ");

                if (!string.IsNullOrEmpty(label.TitleImageCssClass))        // 图片样式
                {
                    sbItemTemplate.Append(" class=\"" + label.TitleImageCssClass + "\" ");
                }

                if (!string.IsNullOrEmpty(label.TitleImageWidth))           // 图片宽
                {
                    sbItemTemplate.Append(" width=\"" + label.TitleImageWidth + "\" ");
                }

                if (!string.IsNullOrEmpty(label.TitleImageHeight))          // 图片高
                {
                    sbItemTemplate.Append(" height=\"" + label.TitleImageHeight + "\" ");
                }

                sbItemTemplate.Append("/></a>");
            }

            sbItemTemplate.Append("<" + fieldContainer + ">");

            if (!string.IsNullOrEmpty(label.TitleCssClass))                 //  标题
            {
                sbItemTemplate.Append("<" + fieldContainer + " class=\"" + label.TitleCssClass + "\">");
            }

            switch (label.Target)
            {
                case LinkOpenType.Self:
                    sbItemTemplate.Append("<a href=\"{Link}\" target=\"_self\">{[#Title#]}</a>");
                    break;
                default:
                    sbItemTemplate.Append("<a href=\"{Link}\" target=\"_blank\">{[#Title#]}</a>");
                    break;
            }

            sbItemTemplate.Append("</" + fieldContainer + ">");

            if (label.IsShowAddDate)    // 显示日期
            {
                if (!string.IsNullOrEmpty(label.DateCssClass))
                {
                    sbItemTemplate.Append("<" + fieldContainer + " class=\"" + label.DateCssClass + "\">{[#AddDate#]}</" + fieldContainer + ">");
                }
                else
                {
                    sbItemTemplate.Append("<" + fieldContainer + ">{[#AddDate#]}</" + fieldContainer + ">");
                }
            }

            if (label.IsShowBrief)      // 显示导读（简介）
            {
                if (!string.IsNullOrEmpty(label.BriefCssClass))
                {
                    sbItemTemplate.Append("<" + fieldContainer + " class=\"" + label.BriefCssClass + "\">{[#Brief#]}</" + fieldContainer + ">");
                }
                else
                {
                    sbItemTemplate.Append("<" + fieldContainer + ">{[#Brief#]}</" + fieldContainer + ">");
                }
            }

            sbItemTemplate.Append("</" + rsContainer + ">");

            if (!string.IsNullOrEmpty(label.TitleSplitImage))
            {
                if (label.Container == HtmlContainer.Div)
                {
                    sbItemTemplate.Append("<div><img src=\"" + label.TitleSplitImage + "\"/></div>");
                }
                else
                {
                    sbItemTemplate.Append("<" + rsContainer + "><" + fieldContainer + "><img src=\"" + label.TitleSplitImage + "\"/></" + fieldContainer + "></" + rsContainer + ">");
                }
            }
            return sbItemTemplate.ToString();
        }
        #endregion

        #region 抓取标签参数
        public List<SysLabelList> GetSysLabelList(string templateContent)
        {
            List<SysLabelList> lstLabel;                // 系统标签对象列
            Regex regSystemLabel;                       // 匹配封装了的系统列表标签
            MatchCollection collectSystemLabel;
            string labelContent;                        // 标签内容

            lstLabel = GetListParam(templateContent, null);
            regSystemLabel = new Regex(@"\{HQB_L(\d+)_[^\s]+\s*LableType\s*=\s*[""']SYSTEM[""']\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            collectSystemLabel = regSystemLabel.Matches(templateContent);

            foreach (Match label in collectSystemLabel)
            {
                if (this.LabelList.ContainsKey(label.Value))
                {
                    labelContent = this.LabelList[label.Value];
                    lstLabel.AddRange(GetListParam(labelContent, label.Value));
                }
            }

            return lstLabel;
        }

        private List<SysLabelList> GetListParam(string content, string labelName)
        {
            SysLabelList label;                 // 列表类型系统标签对象
            List<SysLabelList> lstLabel;        // 系统标签对象列
            Regex regLabel;                     // 匹配列表类型系统标签
            Regex regParam;                     // 匹配列表类型系统标签参数
            MatchCollection collectParam;
            MatchCollection collectLabel;
            string paramContent;                // 参数内容

            lstLabel = new List<SysLabelList>();
            regLabel = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']LIST[""'](?<4>[^}]*)\}(?<2>.*?)\{/HQB_\k<1>\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regParam = new Regex(@"(?<1>[\w-]+)\s*=\s*[""](?<2>[^""]+)[""]", RegexOptions.IgnoreCase);

            collectLabel = regLabel.Matches(content);

            foreach (Match labelItem in collectLabel)
            {
                label = new SysLabelList();

                if (!string.IsNullOrEmpty(labelName))
                {
                    label.LabelName = labelName;
                }
                else
                {
                    label.LabelName = labelItem.Value;
                }

                label.ItemTemplate = labelItem.Groups[2].Value;
                label.LstSubModel = GetSubModelLabel(label.ItemTemplate);
                paramContent = labelItem.Groups[3].Value + labelItem.Groups[4].Value;

                if (!string.IsNullOrEmpty(paramContent))
                {
                    collectParam = regParam.Matches(paramContent);

                    foreach (Match paramItem in collectParam)
                    {
                        SetListAttributeValue(paramItem.Groups[1].Value, paramItem.Groups[2].Value, ref label);

                        if (label.LstMenu == null || label.LstMenu.Length == 0)
                        {
                            label.LstMenu = new string[] { this.NodeCode };
                        }
                    }
                }

                lstLabel.Add(label);
            }

            return lstLabel;
        }
        #endregion

        #region 设置属性
        private void SetListAttributeValue(string name, string value, ref SysLabelList label)
        {
            switch (name.Trim().ToLower())
            {
                case "tablename":              // 所属模型
                    label.TableName = value.Trim();
                    break;
                case "menu":                   // 所属栏目
                    if (value.Trim() != "")
                    {
                        label.LstMenu = value.Split(new char[] { ',' });
                    }
                    break;
                case "pagesize":               // 文章(或分页）数量
                    label.PageSize = Common.Utils.ParseInt(value, 0);
                    break;
                case "sqlorder":               // 排序方式
                    label.SqlOrder = value;
                    break;
                case "sqlwhere":                // 查询条件
                    label.SqlWhere = value;
                    break;
                case "titlelength":             // 标题显示字数
                    label.TitleLength = Common.Utils.ParseInt(value, 0);
                    break;
                case "titlecssclass":           // 标题样式
                    label.TitleCssClass = value;
                    break;
                case "container":               // 输出类型 
                    switch (value.ToLower())
                    {
                        case "div":
                            label.Container = HtmlContainer.Div;
                            break;
                        case "li":
                            label.Container = HtmlContainer.LI;
                            break;
                    }
                    break;
                case "listcolumncount":         // 显示列数
                    label.ListColumnCount = Common.Utils.ParseInt(value, 1);
                    break;
                case "isshowadddate":           // 是否显示日期
                    label.IsShowAddDate = Common.Utils.ParseBool(value);
                    break;
                case "dateformat":              // 日期格式
                    label.DateFormat = value;
                    break;
                case "datacssclass":            // 日期样式
                    label.DateCssClass = value;
                    break;
                case "isshowbrief":             // 是否显示导读（简介）
                    label.IsShowBrief = Common.Utils.ParseBool(value);
                    break;
                case "isshowtitleimage":        // 是否显标题图片
                    label.IsShowTitleImage = Common.Utils.ParseBool(value);
                    break;
                case "titleimagewidth":         // 标题图片宽
                    label.TitleImageWidth = value;
                    break;
                case "titleimageheight":        // 标题图片高
                    label.TitleImageHeight = value;
                    break;
                case "titleimagecount":         // 标题图片个数
                    label.TitleImageCount = Common.Utils.ParseInt(value, 1);
                    break;
                case "titleimagecssclass":           // 标题图片位置
                    label.TitleImageCssClass = value;
                    break;
                case "briefcssclass":           // 是否显示导读（简介）样式
                    label.BriefCssClass = value;
                    break;
                case "brieflength":             // 是否显示导读（简介）显示字数
                    label.BriefLength = Common.Utils.ParseInt(value, 0);
                    break;
                case "morelinkisword":          // 更多链接是文字或图片
                    label.MoreLinkIsWord = Common.Utils.ParseBool(value);
                    break;
                case "moreLinkwordorimageurl":  // 链接文字或图片内容
                    label.MoreLinkWordOrImageUrl = value;
                    break;
                case "lineheight":              // 文章行距
                    label.LineHeight = value;
                    break;
                case "titlesplitimage":         // 标题分隔图片
                    label.TitleSplitImage = value;
                    break;
                case "target":                  // 链接目标
                    switch (value)
                    {
                        case "0":
                            label.Target = LinkOpenType.Self;
                            break;
                        case "1":
                            label.Target = LinkOpenType.Blank;
                            break;
                    }
                    break;
                case "issplit":
                    label.IsSplit = Common.Utils.ParseBool(value);
                    break;
                case "isshowmorelink":
                    label.IsShowMoreLink = Common.Utils.ParseBool(value);
                    break;
                case "issubmodel":          // 是否子模型
                    label.IsSubModel = Common.Utils.ParseBool(value);
                    break;
                case "submodelctemplate":   // 内容模板
                    label.SubModelCTemplate = value.Trim();
                    break;
            }
        }
        #endregion

        #endregion

        #region 抓取子模型调用标签
        private List<SubModelParam> GetSubModelLabel(string strContent)
        {
            List<SubModelParam> lstSubModelParam;
            Regex regLabel;
            MatchCollection collectLabel;
            SubModelParam subModelParam;
            bool isTrue;

            lstSubModelParam = new List<SubModelParam>();
            regLabel = new Regex(@"\{\$SubModel\((?<FieldName>[^,]*),(?<IndexTemp>[^,]*),(?<CTemp>[^)]*)\)\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            collectLabel = regLabel.Matches(strContent);

            foreach (Match label in collectLabel)
            {
                subModelParam = new SubModelParam();

                isTrue = true;
                subModelParam.LabelName = label.Value;
                subModelParam.FieldName = label.Groups["FieldName"].Value;
                subModelParam.IndexTemplate = label.Groups["IndexTemp"].Value;
                subModelParam.ContentTemplate = label.Groups["CTemp"].Value;

                foreach (SubModelParam param in lstSubModelParam)
                {
                    if (param.FieldName == subModelParam.FieldName)
                    {
                        isTrue = false;
                        break;
                    }
                }

                if (isTrue)
                {
                    lstSubModelParam.Add(subModelParam);
                }
            }

            return lstSubModelParam;
        }
        #endregion

        #region 栏目及导航

        #region 解析 -- 栏目
        public string ParseMenu(SysLabelMenu label, string siteUrl)
        {
            List<Menu> dataSource;
            string parsedResult;
            DataRow[] arrCurrentDR;
            string nodeCode;

            parsedResult = string.Empty;
            dataSource = null;
            arrCurrentDR = null;
            nodeCode = string.Empty;

            switch (label.Type)
            {
                case SysLabelMenuType.TopNavigator:
                    dataSource = GetChildMenuData(siteUrl, null, label.Level);
                    break;
                case SysLabelMenuType.SubMenu:
                    switch (label.SubMenuType)
                    {
                        case 1:     // 显示当前栏目子栏目
                            nodeCode = label.NodeCode;
                            break;
                        case 2:     // 显示当前栏目兄弟栏目（子栏目）
                            arrCurrentDR = this.MenuList.Select("NodeCode='" + label.NodeCode + "'");

                            if (arrCurrentDR.Length > 0)
                            {
                                nodeCode = arrCurrentDR[0]["ParentNode"].ToString();
                                arrCurrentDR = this.MenuList.Select("NodeCode='" + nodeCode + "'");

                                if (arrCurrentDR.Length > 0)
                                {
                                    label.Item1Template.HeadTemplate = label.Item1Template.HeadTemplate.Replace("{$Url}", siteUrl + arrCurrentDR[0]["MenuUrl"].ToString()).Replace("{$Name}", arrCurrentDR[0]["NodeName"].ToString()).Replace("{$ToolTip}", arrCurrentDR[0]["Tips"].ToString());
                                }
                            }
                            break;
                        case 3:     // 显示当前栏目一级栏目下的所有子栏目
                            if (!string.IsNullOrEmpty(label.NodeCode) && label.NodeCode.Length > 6)
                            {
                                nodeCode = label.NodeCode.Substring(0, 6);
                            }
                            break;
                        default:   // 显示当前栏目子栏目
                            nodeCode = label.NodeCode;
                            break;
                    }

                    dataSource = GetChildMenuData(siteUrl, nodeCode, label.Level);
                    break;
            }

            if (dataSource != null && dataSource.Count > 0 && label.Level > 0)
            {
                if (label.TemplateContent.Trim() == "" || label.Item1Template.ItemTemplate.Trim() == "")  // 预定义
                {
                    SetPrevDefinedTemplate(ref label);
                    parsedResult = ParseSelfDefinedMenu(dataSource, label);

                    switch (label.Type)
                    {
                        case SysLabelMenuType.TopNavigator:
                            if (label.ShowType == MenuShowType.Horizontal)
                            {
                                parsedResult = "<script type=\"text/javascript\">hqb_ShowMenuSwitch(\"NAV_HR\")</script>" + parsedResult;
                            }
                            else
                            {
                                parsedResult = "<script type=\"text/javascript\">hqb_ShowMenuSwitch(\"NAV_VR\")</script>" + parsedResult;
                            }

                            break;
                        case SysLabelMenuType.SubMenu:
                            parsedResult = "<script type=\"text/javascript\">hqb_ShowMenuSwitch(\"MENU\")</script>" + parsedResult;
                            break;
                    }
                }
                else  // 代码模式，自定义
                {
                    parsedResult = ParseSelfDefinedMenu(dataSource, label);
                }

                parsedResult = label.Item1Template.HeadTemplate + parsedResult + label.Item1Template.FootTemplate;
            }

            return parsedResult;
        }

        private void SetPrevDefinedTemplate(ref SysLabelMenu label)
        {
            LoopTemplate[] arrLoop;

            arrLoop = new LoopTemplate[label.Level];

            switch (label.Type)
            {
                case SysLabelMenuType.TopNavigator:
                    arrLoop[0] = new LoopTemplate();

                    if (label.ShowType == MenuShowType.Horizontal)
                    {
                        arrLoop[0].HeadTemplate = "<ul id=\"HQB_SYS_NAV_HR\">";
                    }
                    else
                    {
                        arrLoop[0].HeadTemplate = "<ul id=\"HQB_SYS_NAV_VR\">";
                    }

                    arrLoop[0].FootTemplate = "</ul>";

                    if (label.IsWordMenu)
                    {
                        arrLoop[0].ItemTemplate = "<li><a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\">{$Name}</a></li>";
                    }
                    else
                    {
                        arrLoop[0].ItemTemplate = "<li><a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\"><img src=\"{$Image}\" /></a></li>";
                    }


                    if (label.IsBothMenu && arrLoop.Length > 1)  // 二级栏目
                    {
                        arrLoop[1] = new LoopTemplate();

                        arrLoop[1].HeadTemplate = "<ul>";
                        arrLoop[1].FootTemplate = "</ul>";

                        if (label.IsWordMenu)
                        {
                            arrLoop[1].ItemTemplate = "<li><a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\">{$Name}</a></li>";
                        }
                        else
                        {
                            arrLoop[1].ItemTemplate = "<li><a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\"><img src=\"{$Image}\" /></a></li>";
                        }
                    }
                    break;
                case SysLabelMenuType.SubMenu:
                    arrLoop[0] = new LoopTemplate();
                    arrLoop[0].HeadTemplate = "<div id=\"HQB_SYS_MENU\">";
                    arrLoop[0].FootTemplate = "</div>";

                    if (label.IsWordMenu)
                    {
                        arrLoop[0].ItemTemplate = "<h4><a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\">{$Name}</a></h4>";
                    }
                    else
                    {
                        arrLoop[0].ItemTemplate = "<a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\"><img src=\"{$Image}\" /></a>";
                    }

                    if (label.IsBothMenu && arrLoop.Length > 1)  // 二级栏目
                    {
                        arrLoop[1] = new LoopTemplate();
                        arrLoop[1].HeadTemplate = "<ul>";
                        arrLoop[1].FootTemplate = "</ul>";

                        if (label.IsWordMenu)
                        {
                            arrLoop[1].ItemTemplate = "<li><a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\">{$Name}</a></li>";
                        }
                        else
                        {
                            arrLoop[1].ItemTemplate = "<li><a href=\"{$Url}\" target=\"$Target\" title=\"{$ToolTip}\"><img src=\"{$Image}\" /></a></li>";
                        }
                    }
                    break;
            }

            label.Item1Template = arrLoop[0];

            if (arrLoop.Length > 1)
            {
                label.Item2Template = arrLoop[1];
            }

            if (arrLoop.Length > 2)
            {
                label.Item3Template = arrLoop[2];
            }
        }

        // 解析自定义（代码模式）型栏目
        private string ParseSelfDefinedMenu(List<Menu> dataSource, SysLabelMenu label)
        {
            StringBuilder sbResult;
            string menuHtmlCode;
            int serialNumber;       //序号 gavin by 20101117

            sbResult = new StringBuilder();
            menuHtmlCode = string.Empty;

            serialNumber = 0;
            foreach (Menu menu in dataSource)
            {
                menuHtmlCode = ParseChildMenu(menu, label, 1, serialNumber);
                sbResult.Append(menuHtmlCode);
                serialNumber++;
            }

            return sbResult.ToString();
        }

        //num 序号 gavin by 20101117
        private string ParseChildMenu(Menu menu, SysLabelMenu label, int level, int num)
        {
            string menuHtmlCode;
            string nextHeadTemplate;
            string nextFootTemplate;
            LoopTemplate loopTemplate;

            menuHtmlCode = string.Empty;
            nextHeadTemplate = string.Empty;
            nextFootTemplate = string.Empty;

            switch (level)
            {
                case 1:
                    loopTemplate = label.Item1Template;

                    if (label.Item2Template != null)
                    {
                        nextHeadTemplate = label.Item2Template.HeadTemplate;
                        nextFootTemplate = label.Item2Template.FootTemplate;
                    }
                    break;
                case 2:
                    loopTemplate = label.Item2Template;

                    if (label.Item3Template != null)
                    {
                        nextHeadTemplate = label.Item3Template.HeadTemplate;
                        nextFootTemplate = label.Item3Template.FootTemplate;
                    }
                    break;
                case 3:
                    loopTemplate = label.Item3Template;
                    break;
                default:
                    loopTemplate = new LoopTemplate();
                    break;
            }

            if (level <= label.Level && loopTemplate != null && !string.IsNullOrEmpty(loopTemplate.ItemTemplate))
            {
                if (menu.ChildMenu != null && menu.ChildMenu.Count > 0)
                {
                    menuHtmlCode = ReplaceMenuVarLabel(loopTemplate.ItemTemplate, menu, num);
                    menuHtmlCode += nextHeadTemplate;
                    int serialNumber;       //序号 gavin by 20101117
                    serialNumber = 0;
                    foreach (Menu childMenu in menu.ChildMenu)
                    {
                        menuHtmlCode += ParseChildMenu(childMenu, label, level + 1, serialNumber);
                        serialNumber++;
                    }

                    menuHtmlCode += nextFootTemplate;
                }
                else
                {
                    menuHtmlCode = ReplaceMenuVarLabel(loopTemplate.ItemTemplate, menu, num);
                    menuHtmlCode += nextHeadTemplate;
                    menuHtmlCode += nextFootTemplate;
                }
            }

            return menuHtmlCode;
        }

        //num 序号 gavin by 20101117
        private string ReplaceMenuVarLabel(string content, Menu menu, int num)
        {
            string newContent;

            newContent = content;

            if (!string.IsNullOrEmpty(newContent))
            {
                newContent = newContent.Replace("{$Name}", menu.Name);                // 栏目名称
                newContent = newContent.Replace("{$Url}", menu.Url);                  // 栏目路径
                newContent = newContent.Replace("{$NodeCode}", menu.NodeCode);        // 栏目编码（NodeCode）
                newContent = newContent.Replace("{$Image}", menu.ImageUrl);           // 栏目图片
                newContent = newContent.Replace("{$OverImage}", menu.OverImageUrl);   // 鼠标经过图片
                newContent = newContent.Replace("{$ToolTip}", menu.ToolTip);          // 栏目提示信息
                newContent = newContent.Replace("{$HQB.Num}", num.ToString());       // 栏目序号，主要用在样式后面加上一个数字

                //当前栏目样式 gavin by 2010-12-02
                if (menu.NodeCode == NodeCode && newContent.IndexOf("CurrentStyle=") > 0)
                {
                    if (newContent.IndexOf("CurrentStyle=") > 0)
                    {
                        newContent = Regex.Replace(newContent, "class[^=]*=[^\"]*\"[^\"]*\"", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                        newContent = newContent.Replace("CurrentStyle=", "class=");
                    }
                }
                else
                {
                    newContent = Regex.Replace(newContent, "CurrentStyle[^=]*=[^\"]*\"[^\"]*\"", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                }

                if (menu.Target == LinkOpenType.Self)
                {
                    newContent = newContent.Replace("$Target", "_self");
                }
                else
                {
                    newContent = newContent.Replace("$Target", "_blank");
                }
            }
            return newContent;
        }
        #endregion

        #region 解析 -- 导航
        public string ParseNav(SysLabelNav label, string siteUrl, string uploadImgUrl)
        {
            StringBuilder sbResult;
            Stack<Navigator> stkNav;
            Navigator nav;
            string navTemplate;
            string tagTempate;

            stkNav = new Stack<Navigator>();
            sbResult = new StringBuilder();

            GetNavData(siteUrl, label.NodeCode, ref stkNav);

            if (!string.IsNullOrEmpty(label.CssClass) && label.CssClass.Trim() != "")
            {
                navTemplate = "<li><a href=\"{$Url}\" target=\"{$Target}\"><span class=\"" + label.CssClass + "\">{$Name}</span></a></li>";
            }
            else
            {
                navTemplate = "<li><a href=\"{$Url}\" target=\"{$Target}\">{$Name}</a></li>";
            }

            if (label.IsNavTagWord)
            {
                tagTempate = "<li>" + label.NavTagContent + "</li>";
            }
            else
            {
                tagTempate = "<li><img src=\"" + uploadImgUrl + label.NavTagContent + "\"/></li>";
            }

            if (stkNav.Count > 0)
            {
                nav = stkNav.Pop();

                if (label.Target == LinkOpenType.Self)
                {
                    sbResult.Append(navTemplate.Replace("{$Url}", nav.Url).Replace("{$Name}", nav.Name).Replace("{$Target}", "_self"));
                }
                else
                {
                    sbResult.Append(navTemplate.Replace("{$Url}", nav.Url).Replace("{$Name}", nav.Name).Replace("{$Target}", "_blank"));
                }

                while (stkNav.Count > 0)
                {
                    nav = stkNav.Pop();
                    sbResult.Append(tagTempate);

                    if (label.Target == LinkOpenType.Self)
                    {
                        sbResult.Append(navTemplate.Replace("{$Url}", nav.Url).Replace("{$Name}", nav.Name).Replace("{$Target}", "_self"));
                    }
                    else
                    {
                        sbResult.Append(navTemplate.Replace("{$Url}", nav.Url).Replace("{$Name}", nav.Name).Replace("{$Target}", "_blank"));
                    }

                }
            }

            //导航html代码 gavin by 20101117
            if (!string.IsNullOrEmpty(label.HtmlCode))
            {
                return sbResult.ToString().Replace("li", label.HtmlCode);
            }
            else
            {
                return sbResult.ToString().Replace("<li>", "").Replace("</li>", "");
            }
        }
        #endregion

        #region 获取导航数据
        private void GetNavData(string siteUrl, string nodeCode, ref Stack<Navigator> stkNav)
        {
            DataRow[] currentNav;
            Navigator nav;

            nav = new Navigator();
            currentNav = this.MenuList.Select("NodeCode='" + nodeCode + "'");

            if (currentNav != null && currentNav.Length > 0)
            {
                nav.Name = currentNav[0]["NodeName"].ToString();

                if (currentNav[0]["LinkURL"].ToString().Trim() != "")
                {
                    nav.Url = currentNav[0]["LinkURL"].ToString().Trim();
                }
                else
                {
                    nav.Url = siteUrl + currentNav[0]["MenuUrl"].ToString();
                }

                stkNav.Push(nav);
                currentNav = this.MenuList.Select("NodeCode='" + currentNav[0]["ParentNode"].ToString() + "'");

                if (currentNav != null && currentNav.Length > 0)
                {
                    GetNavData(siteUrl, currentNav[0]["NodeCode"].ToString(), ref stkNav);
                }
            }
        }
        #endregion

        #region 获取子栏目
        private List<Menu> GetChildMenuData(string siteUrl, string nodeCode, int level)
        {
            List<Menu> lstMenu;
            DataRow[] currentDR;
            string topNodeCode;
            Menu menu;

            lstMenu = new List<Menu>();

            if (level > 0 && level < 4 && this.MenuList != null && this.MenuList.Rows.Count > 0)  // 栏目一至三级深度
            {
                if (string.IsNullOrEmpty(nodeCode))     // 顶级导航
                {
                    topNodeCode = this.MenuList.Rows[0]["NodeCode"].ToString().Substring(0, 3);
                    currentDR = this.MenuList.Select("ParentNode='" + topNodeCode + "'");

                }
                else  // 子栏目
                {
                    currentDR = this.MenuList.Select("ParentNode='" + nodeCode + "'");
                }

                if (currentDR.Length > 0)
                {
                    foreach (DataRow dr in currentDR)
                    {
                        if (string.IsNullOrEmpty(nodeCode))     // 顶级导航
                        {
                            if (Common.Utils.ParseBool(dr["IsTopMenuShow"]))
                            {
                                menu = GetMenuAttribute(dr, siteUrl, level,false);
                                lstMenu.Add(menu);
                            }
                        }
                        else   // 子栏目
                        {
                            if (Common.Utils.ParseBool(dr["IsLeftMenuShow"])) 
                            {
                                menu = GetMenuAttribute(dr, siteUrl, level,true);
                                lstMenu.Add(menu);
                            }
                        }
                    }
                }
            }

            return lstMenu;
        }
        #endregion

        #region 获取栏目属性
        private Menu GetMenuAttribute(DataRow drMenu, string siteUrl, int level,bool isSub)
        {
            Menu menu;
            List<Menu> lstMenu;
            DataRow[] childDR;
            FirstMenuDirParam param;                     // 一级栏目目录参数
            bool hasPrevSinglePage;                      // 当前单页栏目这前是否有单页栏目

            menu = new Menu();
            lstMenu = null;
            childDR = null;

            menu.Name = drMenu["NodeName"].ToString();
            menu.ToolTip = drMenu["Tips"].ToString();
            menu.NodeCode = drMenu["NodeCode"].ToString();

            if (drMenu["CurrentImg"].ToString().Trim() != "")
            {
                menu.ImageUrl = drMenu["CurrentImg"].ToString().Trim();
            }

            if (drMenu["MouseOverImg"].ToString().Trim() != "")
            {
                menu.OverImageUrl = drMenu["MouseOverImg"].ToString();
            }

            if (drMenu["LinkURL"].ToString().Trim() != "")
            {
                menu.Url = drMenu["LinkURL"].ToString().Trim();
            }
            else
            {
                if (drMenu["TableName"].ToString().Trim() != "K_SinglePage")
                {
                    menu.Url = siteUrl + drMenu["MenuUrl"].ToString();
                }
                else   // 单页
                {
                    param = GetFirstMenuDir(drMenu["NodeCode"].ToString());  //  一级栏目目录
                    menu.Url = siteUrl + param.MenuDir;
                    hasPrevSinglePage = HasPrevSingleMenu(drMenu["NodeCode"].ToString());
                    if (drMenu["NodeCode"].ToString().Length <= 6 || param.HasDefaultTemplate || hasPrevSinglePage)       // 一级栏目存在首页模板会生成首页 或 当前栏目之前存在单页栏目
                    {
                        menu.Url = menu.Url + drMenu["NodelEngDesc"].ToString() + "." + this.FileType;
                    }
                    else  // 栏目首页
                    {
                        menu.Url = menu.Url + defaultFileName + "." + this.FileType;
                    }
                }
            }

            if (Common.Utils.ParseBool(drMenu["OpenType"].ToString()))
            {
                menu.Target = LinkOpenType.Self;
            }
            else
            {
                menu.Target = LinkOpenType.Blank;
            }

            if (level > 0)
            {
                childDR = this.MenuList.Select("ParentNode='" + drMenu["NodeCode"].ToString() + "'");

                if (childDR.Length > 0)
                {
                    lstMenu = new List<Menu>();

                    foreach (DataRow dr in childDR)
                    {
                        if (isSub)
                        {
                            if (Common.Utils.ParseBool(dr["IsLeftMenuShow"]))
                            {
                                lstMenu.Add(GetMenuAttribute(dr, siteUrl, level - 1,isSub));
                            }
                        }
                        else
                        {
                            if (Common.Utils.ParseBool(dr["IsTopMenuShow"]))
                            {
                                lstMenu.Add(GetMenuAttribute(dr, siteUrl, level - 1,isSub));
                            }
                        }
                    }
                }

                menu.ChildMenu = lstMenu;
            }

            return menu;
        }
        #endregion

        #region 抓取栏目标签
        public List<SysLabelMenu> GetSysLabelMenu(string templateContent, string nodeCode)
        {
            List<SysLabelMenu> lstLabel;                // 系统标签对象列
            Regex regSystemLabel;                       // 匹配封装了的系统列表标签
            MatchCollection collectSystemLabel;
            string labelContent;                        // 标签内容

            lstLabel = GetMenuParam(templateContent, null, nodeCode);
            regSystemLabel = new Regex(@"\{HQB_L(\d+)_[^\s]+\s*LableType\s*=\s*[""']SYSTEM[""']\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            collectSystemLabel = regSystemLabel.Matches(templateContent);

            foreach (Match label in collectSystemLabel)
            {
                if (this.LabelList.ContainsKey(label.Value))
                {
                    labelContent = this.LabelList[label.Value];
                    lstLabel.AddRange(GetMenuParam(labelContent, label.Value, nodeCode));
                }
            }

            return lstLabel;
        }

        private List<SysLabelMenu> GetMenuParam(string templateContent, string labelName, string nodeCode)
        {
            Regex regLabel;                         // 匹配标签
            Regex regParam;                         // 匹配标签属性
            MatchCollection collectLabel;
            MatchCollection collectAttribute;
            string param;                           // 标签参数
            SysLabelMenu sysLabelMenu;
            List<SysLabelMenu> lstSysLabelMenu;     // 抓取的栏目标签
            LoopTemplate loopTemplate;              // 循环模板参数

            lstSysLabelMenu = new List<SysLabelMenu>();
            regLabel = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']MENU[""'](?<4>[^}]*)\}(?<2>\s*\[HQB\.Loop\](?<Head1>.*?)\[Item\](?<Item1>.*?)\[/Item\]\s*\[HQB\.Loop\](?<Head2>.*?)\[Item\](?<Item2>.*? )\[/Item\]\s*\[HQB\.Loop\](?<Head3>.*?)\[Item\](?<Item3>.*?)\[/Item\](?<Foot3>.*?)\[/HQB\.Loop\](?<Foot2>.*?)\[/HQB\.Loop\](?<Foot1>.*?)\[/HQB\.Loop\]\s*)\{/HQB_\k<1>\s*}|\{HQB_(?<1>[^\s}]+)(?<3>[^}]*)LableType\s*=\s*[""']MENU[""'](?<4>[^}]*)\}(?<2>\s*\[HQB\.LOOP\](?<Head1>.*?)\[Item\](?<Item1>.*?)\[/Item\]\s*\[HQB\.LOOP\](?<Head2>.*?)\[Item\](?<Item2>.*?)\[/Item\](?<Foot2>.*?)\[/HQB\.Loop\](?<Foot1>.*?)\[/HQB\.Loop\]\s*)\{/HQB_\k<1>\s*}|\{HQB_(?<1>[^\s}]+)(?<3>[^}]*)LableType\s*=\s*[""']MENU[""'](?<4>[^}]*)\}(?<2>\s*\[HQB\.LOOP\](?<Head1>.*?)\[Item\](?<Item1>.*?)\[/Item\](?<Foot1>.*?)\[/HQB\.Loop\]\s*)?\s*\{/HQB_\k<1>\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regParam = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            collectLabel = regLabel.Matches(templateContent);

            foreach (Match label in collectLabel)
            {
                sysLabelMenu = new SysLabelMenu();

                if (!string.IsNullOrEmpty(labelName))
                {
                    sysLabelMenu.LabelName = labelName;
                }
                else
                {
                    sysLabelMenu.LabelName = label.Value;
                }

                param = label.Groups[3].Value + label.Groups[4].Value;         // 属性匹配
                sysLabelMenu.TemplateContent = label.Groups[2].Value;          // 自定义内容

                loopTemplate = new LoopTemplate();

                loopTemplate.HeadTemplate = label.Groups["Head1"].Value;       // 第一级循环参数
                loopTemplate.ItemTemplate = label.Groups["Item1"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot1"].Value;
                sysLabelMenu.Item1Template = loopTemplate;

                loopTemplate = new LoopTemplate();

                loopTemplate.HeadTemplate = label.Groups["Head2"].Value;       // 第二级循环参数
                loopTemplate.ItemTemplate = label.Groups["Item2"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot2"].Value;
                sysLabelMenu.Item2Template = loopTemplate;

                loopTemplate = new LoopTemplate();

                loopTemplate.HeadTemplate = label.Groups["Head3"].Value;       // 第三级循环参数
                loopTemplate.ItemTemplate = label.Groups["Item3"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot3"].Value;
                sysLabelMenu.Item3Template = loopTemplate;

                if (param.Trim() != "")
                {
                    collectAttribute = regParam.Matches(param);

                    foreach (Match attribute in collectAttribute)
                    {
                        SetMenuAttribute(ref sysLabelMenu, attribute.Groups[1].Value, attribute.Groups[2].Value);
                    }
                }

                if (string.IsNullOrEmpty(sysLabelMenu.NodeCode))
                {
                    sysLabelMenu.NodeCode = nodeCode;
                }

                lstSysLabelMenu.Add(sysLabelMenu);
            }

            return lstSysLabelMenu;
        }

        private void SetMenuAttribute(ref SysLabelMenu sysLabelMenu, string name, string value)
        {
            switch (name.ToLower())
            {
                case "isbothmenu":      //是否显示二级栏目
                    sysLabelMenu.IsBothMenu = Common.Utils.ParseBool(value);
                    sysLabelMenu.Level = 2;
                    break;
                case "level":           // 栏目级数
                    sysLabelMenu.Level = Common.Utils.ParseInt(value, 1);
                    break;
                case "iswordmenu":      // 是否是文字导航类型
                    sysLabelMenu.IsWordMenu = Common.Utils.ParseBool(value);
                    break;
                case "showtype":        // 显示方式 1 横向 2 纵向
                    switch (value.Trim())
                    {
                        case "1":       // 横向
                            sysLabelMenu.ShowType = MenuShowType.Horizontal;
                            break;
                        case "2":       // 纵向
                            sysLabelMenu.ShowType = MenuShowType.Vertical;
                            break;
                        default:       // 横向
                            sysLabelMenu.ShowType = MenuShowType.Horizontal;
                            break;
                    }
                    break;
                case "currentcsstype":  // 当前栏目样式
                    sysLabelMenu.CurrentCssType = value;
                    break;
                case "nodecode":        // 所属栏目
                    sysLabelMenu.NodeCode = value;
                    break;
                case "type":
                    switch (value)
                    {
                        case "1":   // 总栏目导航
                            sysLabelMenu.Type = SysLabelMenuType.TopNavigator;
                            break;
                        case "2":    // 栏目子菜单
                            sysLabelMenu.Type = SysLabelMenuType.SubMenu;
                            break;
                    }
                    break;
                case "submenutype":
                    sysLabelMenu.SubMenuType = Common.Utils.ParseInt(value,1);
                    break;
            }
        }
        #endregion

        #region 抓取导航标签
        public List<SysLabelNav> GetSysLabelNav(string templateContent, string nodeCode)
        {
            List<SysLabelNav> lstLabel;                // 系统标签对象列
            Regex regSystemLabel;                       // 匹配封装了的系统列表标签
            MatchCollection collectSystemLabel;
            string labelContent;                        // 标签内容

            lstLabel = GetNavParam(templateContent, null, nodeCode);
            regSystemLabel = new Regex(@"\{HQB_L(\d+)_[^\s]+\s*LableType\s*=\s*[""']SYSTEM[""']\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            collectSystemLabel = regSystemLabel.Matches(templateContent);

            foreach (Match label in collectSystemLabel)
            {
                if (this.LabelList.ContainsKey(label.Value))
                {
                    labelContent = this.LabelList[label.Value];
                    lstLabel.AddRange(GetNavParam(labelContent, label.Value, nodeCode));
                }
            }

            return lstLabel;
        }

        public List<SysLabelNav> GetNavParam(string templateContent, string labelName, string nodeCode)
        {
            Regex regLabel;                         // 匹配标签
            Regex regParam;                         // 匹配标签属性
            MatchCollection collectLabel;
            MatchCollection collectAttribute;
            string param;                           // 标签参数
            SysLabelNav sysLabelNav;
            List<SysLabelNav> lstSysLabelNav;       // 抓取的导航标签

            lstSysLabelNav = new List<SysLabelNav>();
            sysLabelNav = new SysLabelNav();
            regLabel = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']NAV[""'](?<4>[^}]*)\}\s*\{/HQB_\k<1>\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regParam = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            collectLabel = regLabel.Matches(templateContent);

            foreach (Match label in collectLabel)
            {
                param = label.Groups[3].Value + label.Groups[4].Value;

                if (!string.IsNullOrEmpty(labelName))
                {
                    sysLabelNav.LabelName = labelName;
                }
                else
                {
                    sysLabelNav.LabelName = label.Value;
                }

                if (param.Trim() != "")
                {
                    collectAttribute = regParam.Matches(param);

                    foreach (Match attribute in collectAttribute)
                    {
                        SetNavAttribute(ref sysLabelNav, attribute.Groups[1].Value, attribute.Groups[2].Value);
                    }
                }

                if (string.IsNullOrEmpty(sysLabelNav.NodeCode) || sysLabelNav.NodeCode.Trim() == "")
                {
                    sysLabelNav.NodeCode = nodeCode;
                }

                lstSysLabelNav.Add(sysLabelNav);
            }

            return lstSysLabelNav;
        }

        private void SetNavAttribute(ref SysLabelNav sysLabelNav, string name, string value)
        {
            switch (name.ToLower())
            {
                case "cssclass":            // 文字样式
                    sysLabelNav.CssClass = value;
                    break;
                case "target":              // 打开方式 1 当前窗口打开 2 新窗口打开
                    switch (value.Trim())
                    {
                        case "1":
                            sysLabelNav.Target = LinkOpenType.Self;
                            break;
                        case "2":
                            sysLabelNav.Target = LinkOpenType.Blank;
                            break;
                    }
                    break;
                case "isnavtagword":        // 导航标识方式 1 为文字 0 为图片
                    sysLabelNav.IsNavTagWord = Common.Utils.ParseBool(value);
                    break;
                case "navtagcontent":       // 导航标识内容 文字内容或图片路径，与IsNavTagWord取值有关
                    sysLabelNav.NavTagContent = value;
                    break;
                case "nodecode":
                    sysLabelNav.NodeCode = value;
                    break;
                case "htmlcode":             //导航html代码
                    sysLabelNav.HtmlCode = value;
                    break;
            }
        }
        #endregion

        #endregion

        #region 详细页标签
        #region 解析
        public string ParseContent(SysLabelContent sysLabelContent, DataRow dataSource)
        {
            Regex fieldReg;                              // 匹配字段
            MatchCollection fieldMatch;                  // 字段匹配内容
            List<Field> lstFieldParam;                   // 所有字段参数
            Field fieldParam;                            // 字段参数
            string templateContent;
            string fieldValue;

            lstFieldParam = new List<Field>();

            fieldReg = new Regex(@"\{\$Field\((?<1>\d),(?<2>[^,]+),(?<3>[^)]*)\)\}|\{\$Field\((?<1>\d),(?<2>[^,]+)\)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            fieldValue = string.Empty;
            templateContent = sysLabelContent.TemplateContent;
            fieldMatch = fieldReg.Matches(templateContent);                                  // 匹配循环体中的所有字段

            foreach (Match matchItem in fieldMatch)             // 抓取所有字段
            {
                fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                templateContent = templateContent.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                lstFieldParam.Add(fieldParam);
            }

            foreach (Field fieldItem in lstFieldParam) // 绑定字段值
            {
                if (dataSource[fieldItem.Name] != null)
                {
                    fieldValue = dataSource[fieldItem.Name].ToString();

                    if (fieldItem.OutParam != null && fieldItem.OutParam.Length > 0)
                    {
                        fieldValue = FormatFieldContent(fieldValue, fieldItem.OutType, fieldItem.OutParam);
                    }

                    templateContent = templateContent.Replace("{[#" + fieldItem.Name + "#]}", fieldValue);
                }
            }

            return templateContent;
        }
        #endregion

        #region 抓取标签
        public List<SysLabelContent> GetSysLabelContent(string templateContent,bool paramHasID)
        {
            List<SysLabelContent> lstLabel;                // 系统标签对象列
            Regex regSystemLabel;                          // 匹配封装了的系统列表标签
            MatchCollection collectSystemLabel;
            string labelContent;                           // 标签内容

            lstLabel = GetContentParam(templateContent, null,paramHasID);
            regSystemLabel = new Regex(@"\{HQB_L(\d+)_[^\s]+\s*LableType\s*=\s*[""']SYSTEM[""']\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            collectSystemLabel = regSystemLabel.Matches(templateContent);

            foreach (Match label in collectSystemLabel)
            {
                if (this.LabelList.ContainsKey(label.Value))
                {
                    labelContent = this.LabelList[label.Value];
                    lstLabel.AddRange(GetContentParam(labelContent, label.Value,paramHasID));
                }
            }

            return lstLabel;
        }

        private List<SysLabelContent> GetContentParam(string templateContent, string labelName, bool paramHasID)
        {
            Regex regLabel;                           // 匹配标签
            MatchCollection collectLabel;
            SysLabelContent sysLabelContent;
            List<SysLabelContent> lstSysLabelContent;  // 抓取的详细页s标签

            lstSysLabelContent = new List<SysLabelContent>();
            sysLabelContent = new SysLabelContent();

            if (paramHasID)
            {
                regLabel = new Regex(@"\{HQB_(?<5>[^\s}]+)(?<3>[^}]*)LableType\s*=\s*[""]CONTENT[""]\s*SQL\s*=\s*[""](?<4>[^""]+)[""]\s*\}(?<1>.*?)\{/HQB_\k<5>\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }
            else
            {
                regLabel = new Regex(@"\{HQB_(?<5>[^\s}]+)(?<3>[^}]*)LableType\s*=\s*[""]CONTENT[""'](?<4>[^}]*)\}(?<1>.*?)\{/HQB_\k<5>\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }

            collectLabel = regLabel.Matches(templateContent);

            foreach (Match label in collectLabel)
            {
                if (!string.IsNullOrEmpty(labelName))
                {
                    sysLabelContent.LabelName = labelName;
                }
                else
                {
                    sysLabelContent.LabelName = label.Value;
                }

                sysLabelContent.TemplateContent = label.Groups[1].Value;       // 显示模板

                if (paramHasID)
                {
                    sysLabelContent.SQL = label.Groups[4].Value;
                }

                lstSysLabelContent.Add(sysLabelContent);
            }

            return lstSysLabelContent;
        }
        #endregion
        #endregion

        #region 专题栏目

        #region 解析
        public string ParseSpecialMenu(SpecialMenuLabel label, string specialRootUrl)
        {
            StringBuilder sbParsedResult;
            DataRow[] arrMenuDR;
            string menuLink;

            sbParsedResult = new StringBuilder();

            arrMenuDR = this.DtSpecialMenu.Select("SpecialID='" + label.SpecialID + "'");
            menuLink = string.Empty;

            if (!string.IsNullOrEmpty(label.CssClass))
            {
                sbParsedResult.Append("<ul class=\"" + label.CssClass + "\">");
            }
            else
            {
                sbParsedResult.Append("<ul>");
            }

            sbParsedResult.Append("<li><a href=\"" + specialRootUrl + "\" target=\"_self\">首页</a></li>");

            foreach (DataRow dr in arrMenuDR)
            {
                menuLink = "<li><a href=\"" + specialRootUrl + dr["DirectoryName"].ToString() + "." + this.FileType + "\" target=\"_self\">" + dr["Name"].ToString() + "</a></li>";
                sbParsedResult.Append(menuLink);
            }

            sbParsedResult.Append("</ul>");

            return sbParsedResult.ToString();
        }
        #endregion

        #region 抓取标签
        public List<SpecialMenuLabel> GetSysLabelSpecialMenu(string templateContent, string specialID)
        {
            Regex labReg;
            Regex attributeReg;
            MatchCollection collectLabel;
            MatchCollection collectAttribute;
            List<SpecialMenuLabel> lstSpecialMenuLabel;
            SpecialMenuLabel specialMenuLabel;

            lstSpecialMenuLabel = new List<SpecialMenuLabel>();
            labReg = new Regex(@"\{SpecialMenu\s*(?<1>([\w-]+\s*=\s*[""'][^""']+[""']\s*)*)\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            attributeReg = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.Singleline);

            collectLabel = labReg.Matches(templateContent);

            foreach (Match matchItem in collectLabel)
            {
                specialMenuLabel = new SpecialMenuLabel();

                specialMenuLabel.LabelName = matchItem.Value;

                if (matchItem.Groups[1].Value.Trim() != "")
                {
                    collectAttribute = attributeReg.Matches(matchItem.Groups[1].Value.Trim());

                    foreach (Match attributeItem in collectAttribute)
                    {
                        SetSpecialMenuAttribute(ref specialMenuLabel, attributeItem.Groups[1].Value, attributeItem.Groups[2].Value);
                    }
                }

                if (string.IsNullOrEmpty(specialMenuLabel.SpecialID))
                {
                    specialMenuLabel.SpecialID = specialID;
                }

                lstSpecialMenuLabel.Add(specialMenuLabel);
            }

            return lstSpecialMenuLabel;
        }

        private void SetSpecialMenuAttribute(ref SpecialMenuLabel specialMenuLabel, string name, string value)
        {
            switch (name.ToLower())
            {
                case "specialid":
                    specialMenuLabel.SpecialID = value;
                    break;
                case "class":
                    specialMenuLabel.CssClass = value;
                    break;
            }
        }
        #endregion

        #endregion

        #region 自定义表单标签
        public string ParserFormLabel(FormLabel label, string templateContent)
        {
            string parsedResult;
            string filePath;
            Regex regAction;
            Match matchAction;
            string actionLink;
            string formAction;

            regAction = new Regex(@"action\s*=\s*[""'](?<1>[^""']+)[""']", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            filePath = HttpContext.Current.Server.MapPath("../Model/FormModel/" + label.FormID + ".html");
            parsedResult = string.Empty;

            if (File.Exists(filePath))
            {
                parsedResult = File.ReadAllText(filePath);
                matchAction = regAction.Match(parsedResult);

                if (matchAction.Success)
                {
                    formAction = matchAction.Value;
                    actionLink = matchAction.Groups[1].Value;

                    if (actionLink.Contains("?"))
                    {
                        formAction = formAction.Replace(actionLink, actionLink + "&NodeCode=" + label.NodeCode);
                    }
                    else
                    {
                        formAction = formAction.Replace(actionLink, actionLink + "?NodeCode=" + label.NodeCode);
                    }

                    parsedResult = parsedResult.Replace(matchAction.Value, formAction);
                }
            }

            return parsedResult;
        }

        public List<FormLabel> GetSysLabelFormLabel(string templateContent)
        {
            List<FormLabel> lstLabel;
            FormLabel label;
            Regex regLabel;
            MatchCollection collectLabel;

            regLabel = new Regex(@"\{form\[(?<1>[^]]+)\]\s*,\s*NodeCode\s*=\s*[""](?<2>[^""']+)[""']\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            lstLabel = new List<FormLabel>();

            collectLabel = regLabel.Matches(templateContent);

            foreach (Match item in collectLabel)
            {
                label = new FormLabel();
                label.LabelName = item.Value;
                label.FormID = item.Groups[1].Value;
                label.NodeCode = item.Groups[2].Value;

                lstLabel.Add(label);
            }

            return lstLabel;
        }
        #endregion

        #region 期刊

        #region 解析
        public DataTable ParsePeriodical(SysLabelPeriodical label, string PeriodicalID, string menuDir, ContentPageSaveType cSaveType, out string returnContent)
        {
            DataSet ds;
            StringBuilder sbResult;
            string catalogID;
            string catalogName;
            DataRow[] arrArticle;
            Regex fieldReg;                              // 匹配字段
            MatchCollection fieldMatch;                  // 字段匹配内容
            List<Field> lstFieldParam;                   // 所有字段参数
            Field fieldParam;                            // 字段参数
            string loopItem;
            string parsedContent;

            lstFieldParam = new List<Field>();
            sbResult = new StringBuilder();
            fieldReg = new Regex(@"\{\$Field\((?<1>\d),(?<2>[^,]+),(?<3>[^)]*)\)\}|\{\$Field\((?<1>\d),(?<2>[^,]+)\)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            ds = null;
            loopItem = label.Template2.ItemTemplate;
            fieldMatch = fieldReg.Matches(loopItem);                                                // 匹配循环体中的所有字段
            sbResult.Append(label.Template1.HeadTemplate);

            foreach (Match matchItem in fieldMatch)             // 抓取所有字段
            {
                fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                loopItem = loopItem.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                lstFieldParam.Add(fieldParam);
            }

            if (!string.IsNullOrEmpty(PeriodicalID))
            {
                ds = dal.GetPeriodicalDataSource(PeriodicalID);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)   // 期刊分类
                    {
                        catalogID = dr["ID"].ToString();
                        catalogName = dr["Title"].ToString();
                        sbResult.Append(Regex.Replace(label.Template1.ItemTemplate, @"\{\$CatalogName\}", catalogName, RegexOptions.IgnoreCase));
                        sbResult.Append(label.Template2.HeadTemplate);

                        if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            arrArticle = ds.Tables[1].Select("PeriodicalCatalogID='" + catalogID + "'");

                            if (arrArticle != null && arrArticle.Length > 0)
                            {
                                foreach (DataRow drRS in arrArticle)
                                {
                                    parsedContent = SingleRecordBind(drRS, lstFieldParam, loopItem, menuDir, this.FileType, this.siteUrl, cSaveType);
                                    sbResult.Append(parsedContent);
                                }
                            }
                        }

                        sbResult.Append(label.Template2.FootTemplate);
                    }
                }
            }

            sbResult.Append(label.Template1.FootTemplate);
            returnContent = sbResult.ToString();

            if (ds != null && ds.Tables[1] != null)
            {
                return ds.Tables[1];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 抓取标签
        public List<SysLabelPeriodical> GetSysLabelPeriodical(string templateContent)
        {
            List<SysLabelPeriodical> lstLabel;          // 系统标签对象列
            Regex regSystemLabel;                       // 匹配封装了的系统期刊标签
            MatchCollection collectSystemLabel;
            string labelContent;                        // 标签内容

            lstLabel = GetPeriodicalParam(templateContent, null);
            regSystemLabel = new Regex(@"\{HQB_L(\d+)_[^\s]+\s*LableType\s*=\s*[""']SYSTEM[""']\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            collectSystemLabel = regSystemLabel.Matches(templateContent);

            foreach (Match label in collectSystemLabel)
            {
                if (this.LabelList.ContainsKey(label.Value))
                {
                    labelContent = this.LabelList[label.Value];
                    lstLabel.AddRange(GetPeriodicalParam(labelContent, label.Value));
                }
            }

            return lstLabel;
        }

        private List<SysLabelPeriodical> GetPeriodicalParam(string templateContent, string labelName)
        {
            Regex regLabel;                                     // 匹配标签
            MatchCollection collectLabel;
            SysLabelPeriodical sysLabelPeriodical;
            List<SysLabelPeriodical> lstSysLabelPeriodical;     // 抓取的栏目标签
            LoopTemplate loopTemplate;                          // 循环模板参数

            lstSysLabelPeriodical = new List<SysLabelPeriodical>();
            regLabel = new Regex(@"\{HQB_(?<4>L\d+)_[^\s}]+(\s*LableType\s*=\s*[""']Periodical[""']\s*ContentTemplate\s*=\s*[""'](?<1>[^""']+)[""']\s*|\s*ContentTemplate\s*=\s*[""'](?<1>[^""']+)[""']\s*LableType\s*=\s*[""']Periodical[""']\s*)\}\s*\[HQB\.Loop\](?<Head1>.*?)\[Item\](?<Item1>.*?)\[/Item\]\s*\[HQB\.Loop\](?<Head2>.*?)\[Item\](?<Item2>.*?)\[/Item\](?<Foot2>.*?)\[/HQB\.Loop\](?<Foot1>.*?)\[/HQB\.Loop\]\s*\{/HQB_\k<4>\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            collectLabel = regLabel.Matches(templateContent);

            foreach (Match label in collectLabel)
            {
                sysLabelPeriodical = new SysLabelPeriodical();

                if (!string.IsNullOrEmpty(labelName))
                {
                    sysLabelPeriodical.LabelName = labelName;
                }
                else
                {
                    sysLabelPeriodical.LabelName = label.Value;
                }

                sysLabelPeriodical.ContentTemplate = label.Groups[1].Value;

                loopTemplate = new LoopTemplate();
                loopTemplate.HeadTemplate = label.Groups["Head1"].Value;
                loopTemplate.ItemTemplate = label.Groups["Item1"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot1"].Value;
                sysLabelPeriodical.Template1 = loopTemplate;
                loopTemplate = new LoopTemplate();
                loopTemplate.HeadTemplate = label.Groups["Head2"].Value;
                loopTemplate.ItemTemplate = label.Groups["Item2"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot2"].Value;
                sysLabelPeriodical.Template2 = loopTemplate;

                lstSysLabelPeriodical.Add(sysLabelPeriodical);
            }

            return lstSysLabelPeriodical;
        }
        #endregion

        #endregion

        #region 分类

        #region 解析
        public string ParseCategory(SysLabelCategory label, string siteUrl)
        {
            DataTable dataSource;
            string parsedResult;

            parsedResult = string.Empty;
            dataSource = dal.GetSubCategoryList(label.CategoryID, label.IsSibling);

            if (dataSource != null && dataSource.Rows.Count > 0 && label.Level > 0)
            {
                parsedResult = ParseCategoryTemplate(dataSource, label);
            }
            parsedResult = label.Item1Template.HeadTemplate + parsedResult + label.Item1Template.FootTemplate;

            return parsedResult;
        }

        private string ParseCategoryTemplate(DataTable dataSource, SysLabelCategory label)
        {
            StringBuilder sbResult;
            Regex fieldReg;
            MatchCollection collectField;
            List<Field> lst1Field;
            List<Field> lst2Field;
            List<Field> lst3Field;
            Field fieldParam;
            DataRow[] topDR;
            DataRow[] level2DR;
            DataRow[] level3DR;
            string itemHtmlCode;

            fieldReg = new Regex(@"\{\$Field\((?<1>\d),(?<2>[^,]+),(?<3>[^)]*)\)\}|\{\$Field\((?<1>\d),(?<2>[^,]+)\)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            sbResult = new StringBuilder();
            lst1Field = new List<Field>();
            lst2Field = new List<Field>();
            lst3Field = new List<Field>();

            collectField = fieldReg.Matches(label.Item1Template.ItemTemplate);
            topDR = null;

            foreach (Match matchItem in collectField)             // 抓取所有字段
            {
                fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                label.Item1Template.ItemTemplate = label.Item1Template.ItemTemplate.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                lst1Field.Add(fieldParam);
            }

            if (label.Level > 1)
            {
                collectField = fieldReg.Matches(label.Item2Template.ItemTemplate);

                foreach (Match matchItem in collectField)             // 抓取所有字段
                {
                    fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                    label.Item2Template.ItemTemplate = label.Item2Template.ItemTemplate.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                    lst2Field.Add(fieldParam);
                }
            }

            if (label.Level > 2)
            {
                collectField = fieldReg.Matches(label.Item3Template.ItemTemplate);

                foreach (Match matchItem in collectField)             // 抓取所有字段
                {
                    fieldParam = GetFieldParam(matchItem.Groups[2].Value, matchItem.Groups[1].Value, matchItem.Groups[3].Value);
                    label.Item3Template.ItemTemplate = label.Item3Template.ItemTemplate.Replace(matchItem.Value, "{[#" + fieldParam.Name + "#]}");
                    lst3Field.Add(fieldParam);
                }
            }

            if (label.IsSibling)
            {
                topDR = dataSource.Select("ID=" + label.CategoryID);

                if (topDR != null && topDR.Length > 0)
                {
                    topDR = dataSource.Select("ParentID=" + topDR[0]["ParentID"].ToString());
                }
            }
            else
            {
                topDR = dataSource.Select("ParentID=" + label.CategoryID);
            }

            if (topDR != null && topDR.Length > 0)
            {
                foreach (DataRow dr in topDR)
                {
                    itemHtmlCode = ParseCategoryItem(dr, lst1Field, label.Item1Template.ItemTemplate);
                    sbResult.Append(itemHtmlCode);

                    if (label.Level > 1)
                    {
                        level2DR = dataSource.Select("ParentID=" + dr["ID"].ToString());

                        if (level2DR != null && level2DR.Length > 0)
                        {
                            sbResult.Append(label.Item2Template.HeadTemplate);

                            foreach (DataRow level2 in level2DR)
                            {
                                itemHtmlCode = ParseCategoryItem(level2, lst2Field, label.Item2Template.ItemTemplate);
                                sbResult.Append(itemHtmlCode);

                                if (label.Level > 2)
                                {
                                    level3DR = dataSource.Select("ParentID=" + level2["ID"].ToString());

                                    if (level3DR != null && level3DR.Length > 0)
                                    {
                                        sbResult.Append(label.Item3Template.HeadTemplate);

                                        foreach (DataRow level3 in level3DR)
                                        {
                                            itemHtmlCode = ParseCategoryItem(level3, lst3Field, label.Item3Template.ItemTemplate);
                                            sbResult.Append(itemHtmlCode);
                                        }

                                        sbResult.Append(label.Item3Template.FootTemplate);
                                    }
                                }
                                else
                                {
                                    sbResult.Append(label.Item3Template.HeadTemplate);
                                    sbResult.Append(label.Item3Template.FootTemplate);
                                }
                            }

                            sbResult.Append(label.Item2Template.FootTemplate);
                        }
                        else
                        {
                            sbResult.Append(label.Item2Template.HeadTemplate);
                            sbResult.Append(label.Item2Template.FootTemplate);
                        }
                    }
                }
            }

            return sbResult.ToString();
        }

        private string ParseCategoryItem(DataRow dr, List<Field> lstField,string itemTemplate)
        {
            string fieldValue;
            string parsedContent;

            fieldValue = string.Empty;
            parsedContent = string.Empty;

            if (dr != null)
            {
                parsedContent = itemTemplate;

                foreach (Field fieldItem in lstField) // 绑定字段值
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
            }

            return parsedContent;
        }
        #endregion

        #region 抓取分类标签
        public List<SysLabelCategory> GetSysLabelCategory(string templateContent)
        {
            List<SysLabelCategory> lstLabel;                // 系统标签对象列
            Regex regSystemLabel;                       // 匹配封装了的系统列表标签
            MatchCollection collectSystemLabel;
            string labelContent;                        // 标签内容

            lstLabel = GetCategoryParam(templateContent, null);
            regSystemLabel = new Regex(@"\{HQB_L(\d+)_[^\s]+\s*LableType\s*=\s*[""']SYSTEM[""']\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            collectSystemLabel = regSystemLabel.Matches(templateContent);

            foreach (Match label in collectSystemLabel)
            {
                if (this.LabelList.ContainsKey(label.Value))
                {
                    labelContent = this.LabelList[label.Value];
                    lstLabel.AddRange(GetCategoryParam(labelContent, label.Value));
                }
            }

            return lstLabel;
        }

        private List<SysLabelCategory> GetCategoryParam(string templateContent, string labelName)
        {
            Regex regLabel;                             // 匹配标签
            Regex regParam;                             // 匹配标签属性
            MatchCollection collectLabel;
            MatchCollection collectAttribute;
            string param;                               // 标签参数
            SysLabelCategory sysLabelCategory;
            List<SysLabelCategory> lstSysLabelCategory; // 抓取的类型标签
            LoopTemplate loopTemplate;                  // 循环模板参数

            lstSysLabelCategory = new List<SysLabelCategory>();
            regLabel = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']Category[""'](?<4>[^}]*)\}(?<2>\s*\[HQB\.Loop\](?<Head1>.*?)\[Item\](?<Item1>.*?)\[/Item\]\s*\[HQB\.Loop\](?<Head2>.*?)\[Item\](?<Item2>.*? )\[/Item\]\s*\[HQB\.Loop\](?<Head3>.*?)\[Item\](?<Item3>.*?)\[/Item\](?<Foot3>.*?)\[/HQB\.Loop\](?<Foot2>.*?)\[/HQB\.Loop\](?<Foot1>.*?)\[/HQB\.Loop\]\s*)\{/HQB_\k<1>\s*}|\{HQB_(?<1>[^\s}]+)(?<3>[^}]*)LableType\s*=\s*[""']Category[""'](?<4>[^}]*)\}(?<2>\s*\[HQB\.LOOP\](?<Head1>.*?)\[Item\](?<Item1>.*?)\[/Item\]\s*\[HQB\.LOOP\](?<Head2>.*?)\[Item\](?<Item2>.*?)\[/Item\](?<Foot2>.*?)\[/HQB\.Loop\](?<Foot1>.*?)\[/HQB\.Loop\]\s*)\{/HQB_\k<1>\s*}|\{HQB_(?<1>[^\s}]+)(?<3>[^}]*)LableType\s*=\s*[""']Category[""'](?<4>[^}]*)\}(?<2>\s*\[HQB\.LOOP\](?<Head1>.*?)\[Item\](?<Item1>.*?)\[/Item\](?<Foot1>.*?)\[/HQB\.Loop\]\s*)?\s*\{/HQB_\k<1>\s*}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regParam = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            collectLabel = regLabel.Matches(templateContent);

            foreach (Match label in collectLabel)
            {
                sysLabelCategory = new SysLabelCategory();

                if (!string.IsNullOrEmpty(labelName))
                {
                    sysLabelCategory.LabelName = labelName;
                }
                else
                {
                    sysLabelCategory.LabelName = label.Value;
                }

                param = label.Groups[3].Value + label.Groups[4].Value;         // 属性匹配
                sysLabelCategory.TemplateContent = label.Groups[2].Value;      // 自定义内容

                loopTemplate = new LoopTemplate();

                loopTemplate.HeadTemplate = label.Groups["Head1"].Value;       // 第一级循环参数
                loopTemplate.ItemTemplate = label.Groups["Item1"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot1"].Value;
                sysLabelCategory.Item1Template = loopTemplate;

                loopTemplate = new LoopTemplate();

                loopTemplate.HeadTemplate = label.Groups["Head2"].Value;       // 第二级循环参数
                loopTemplate.ItemTemplate = label.Groups["Item2"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot2"].Value;
                sysLabelCategory.Item2Template = loopTemplate;

                loopTemplate = new LoopTemplate();

                loopTemplate.HeadTemplate = label.Groups["Head3"].Value;       // 第三级循环参数
                loopTemplate.ItemTemplate = label.Groups["Item3"].Value;
                loopTemplate.FootTemplate = label.Groups["Foot3"].Value;
                sysLabelCategory.Item3Template = loopTemplate;

                if (param.Trim() != "")
                {
                    collectAttribute = regParam.Matches(param);

                    foreach (Match attribute in collectAttribute)
                    {
                        SetCategoryAttribute(ref sysLabelCategory, attribute.Groups[1].Value, attribute.Groups[2].Value);
                    }
                }

                lstSysLabelCategory.Add(sysLabelCategory);
            }

            return lstSysLabelCategory;
        }

        private void SetCategoryAttribute(ref SysLabelCategory sysLabelCategory, string name, string value)
        {
            switch (name.ToLower())
            {
                case "level":           // 类型级数
                    sysLabelCategory.Level = Common.Utils.ParseInt(value, 1);
                    break;
                case "cssfile":         // 样式文件路径
                    sysLabelCategory.CssFile = value;
                    break;
                case "jsfile":          // JS文件路径
                    sysLabelCategory.JsFile = value;
                    break;
                case "categoryid":      // 类型ID
                    sysLabelCategory.CategoryID = value.Trim();
                    break;
                case "issibling":       // 显示当前类型的平级类型
                    sysLabelCategory.IsSibling = Common.Utils.ParseBool(value);
                    break;
            }
        }
        #endregion

        #endregion

        #region 评论标签

        #region 解析评论提交标签
        public string ParseCommentSubmit(SysLabelCommentSubmit label,string rsID)
        {
            string result;

            result = label.Content;
            result = result.Replace("{$ID}",rsID);
            result = result.Replace("{$SiteID}",this.SiteID.ToString());
            result = result.Replace("{$NodeCodeOrCategory}", label.NodeCodeOrCategory);
            result = result.Replace("{$ValidateCodeURL}", this.siteUrl + "Plus/Comment/ValidateCode.aspx");

            return result;
        }
        #endregion

        #region 抓取评论提交内容标签
        public List<SysLabelCommentSubmit> GetSysLabelCommentSubmit(string templateContent, string nodeCodeOrCategory) 
        {
            List<SysLabelCommentSubmit> lstLabel;                // 系统标签对象列
            Regex regSystemLabel;                                // 匹配封装了的评论提交内容标签
            MatchCollection collectSystemLabel;
            string labelContent;                                 // 标签内容

            lstLabel = GetCommentSubmitParam(templateContent, null,nodeCodeOrCategory);
            regSystemLabel = new Regex(@"\{HQB_L(\d+)_[^\s]+\s*LableType\s*=\s*[""']SYSTEM[""']\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            collectSystemLabel = regSystemLabel.Matches(templateContent);

            foreach (Match label in collectSystemLabel)
            {
                if (this.LabelList.ContainsKey(label.Value))
                {
                    labelContent = this.LabelList[label.Value];
                    lstLabel.AddRange(GetCommentSubmitParam(labelContent, label.Value,nodeCodeOrCategory));
                }
            }

            return lstLabel;
        }

        public List<SysLabelCommentSubmit> GetCommentSubmitParam(string content, string labelName, string nodeCodeOrCategory)
        {
            SysLabelCommentSubmit label;                 // 评论提交内容类型系统标签对象
            List<SysLabelCommentSubmit> lstLabel;        // 系统标签对象列
            Regex regLabel;                              // 匹配列表类型系统标签
            Regex regParam;                              // 匹配列表类型系统标签参数
            MatchCollection collectParam;
            MatchCollection collectLabel;
            string paramContent;                         // 参数内容

            lstLabel = new List<SysLabelCommentSubmit>();
            regLabel = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']CommentSubmit[""'](?<4>[^}]*)\}(?<2>.*?)\{/HQB_\k<1>\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regParam = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase);

            collectLabel = regLabel.Matches(content);

            foreach (Match labelItem in collectLabel)
            {
                label = new SysLabelCommentSubmit();
                label.NodeCodeOrCategory = nodeCodeOrCategory;

                if (!string.IsNullOrEmpty(labelName))
                {
                    label.LabelName = labelName;
                }
                else
                {
                    label.LabelName = labelItem.Value;
                }

                label.Content = labelItem.Groups[2].Value;
                paramContent = labelItem.Groups[3].Value + labelItem.Groups[4].Value;

                if (!string.IsNullOrEmpty(paramContent))
                {
                    collectParam = regParam.Matches(paramContent);

                    foreach (Match paramItem in collectParam)
                    {
                        SetCommentSubmitAttributeValue(paramItem.Groups[1].Value, paramItem.Groups[2].Value, ref label);
                    }
                }

                lstLabel.Add(label);
            }

            return lstLabel;
        }

        private void SetCommentSubmitAttributeValue(string name, string value, ref SysLabelCommentSubmit label)
        {
            switch (name.ToLower())
            {
                case "loginedmark":
                    label.LoginedMark = value;
                    break;
            }
        }
        #endregion

        #endregion
    }
}