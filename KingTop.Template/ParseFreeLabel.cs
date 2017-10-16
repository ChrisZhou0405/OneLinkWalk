#region 引用程序集
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.Web;

using KingTop.Template.ParamType;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-02
// 功能描述：内容发布 -- 自由标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template
{
    /// <summary>
    /// 解析自由标签
    /// </summary>
    public class ParseFreeLabel : TPublic
    {
        #region 变量成员
        private const string freeLabelCacheName = "HQB_Publish_FreeLabel";
        private string itemContentTag = "{#ItemContent#}";
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.IPublish dal = (IDAL.Template.IPublish)Assembly.Load(path).CreateInstance(path + ".Template.Publish");
        private LinkOpenType _target;
        private int _siteID;
        private string _nodeCode;
        private string _fileType;
        private DataTable _menuList;
        private string _siteDirUrl;
        private ContentPageSaveType _cSaveType;
        private Hashtable _labelList;
        #endregion

        #region 构造函数
        /// <summary>
        /// 解析自由标签
        /// </summary>
        /// <param name="nodeCode">当前发布的栏目ID</param>
        public ParseFreeLabel(string nodeCode)
        {
            this._nodeCode = nodeCode;
        }
        #endregion

        #region 属性
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
            set { this._menuList = value; }
        }

        /// <summary>
        /// 自由标签列表
        /// </summary>
        private Hashtable LabelList
        {
            get
            {
                if (this._labelList == null)
                {
                    if (HttpContext.Current.Cache[freeLabelCacheName] != null)
                    {
                        this._labelList = HttpContext.Current.Cache[freeLabelCacheName] as Hashtable;
                    }
                    else
                    {
                        this.Load();
                        this._labelList = HttpContext.Current.Cache[freeLabelCacheName] as Hashtable;
                    }

                    if (this._labelList.Count == 0)
                    {
                        this.Load();
                        this._labelList = HttpContext.Current.Cache[freeLabelCacheName] as Hashtable;
                    }
                }

                return this._labelList;
            }
        }

        /// <summary>
        /// 链接打开方式
        /// </summary>
        public LinkOpenType Target
        {
            get { return this._target; }
            set { this._target = value; }
        }

        /// <summary>
        /// 当前站点ID
        /// </summary>
        public int SiteID
        {
            get { return this._siteID; }
            set { this._siteID = value; }
        }

        /// <summary>
        /// 当前发布的栏目ID
        /// </summary>
        public string NodeCode
        {
            get { return this._nodeCode; }
            set { this._nodeCode = value; }
        }

        /// <summary>
        /// 站点URL
        /// </summary>
        public string SiteDirUrl
        {
            get { return this._siteDirUrl; }
            set { this._siteDirUrl = value; }
        }

        /// <summary>
        /// 内容页面保存方式
        /// </summary>
        public ContentPageSaveType CSaveType
        {
            get { return this._cSaveType; }
            set { this._cSaveType = value; }
        }
        #endregion

        #region 解析
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="freeLabel">标签内容</param>
        /// <param name="currentPage">当前页（仅用于分页）</param>
        /// <param name="pageSize">页面大小（仅用于分页）</param>
        /// <returns></returns>
        public string Parse(FreeLabel freeLabel, int currentPage, int pageSize)
        {
            string parseResult;                     // 标签解析结果
            FreeLabelConfig labelConfig;            // 自由标签参数
            bool isException;                       // 是否出现异常
            DataTable dtDataSource;                 // 标签数据源
            LoopLabelParseParam labelParseParam;    // 标签经解析后的参数
            string selSQL;                          // 标签数据源查询语句

            labelConfig = new FreeLabelConfig();
            isException = false;
            parseResult = string.Empty;

            if (string.IsNullOrEmpty(this.NodeCode))
            {
                DataRow[] drMenu;

                drMenu = this.MenuList.Select("NodeCode='" + freeLabel.NodeCode + "'");

                if (drMenu != null && drMenu.Length > 0)
                {
                    this.CSaveType = GetContentPageSaveType(drMenu[0]["ContentPageHtmlRule"].ToString());
                }
            }

            if (LabelList != null && LabelList.Contains(freeLabel.Name))
            {
                try
                {
                    labelConfig = (FreeLabelConfig)LabelList[freeLabel.Name];
                }
                catch { isException = true; }
            }
            else
            {
                isException = true;
            }

            if (!isException && !string.IsNullOrEmpty(labelConfig.SQL) && !string.IsNullOrEmpty(labelConfig.Content))
            {
                selSQL = labelConfig.SQL;
                selSQL = selSQL.Replace("{$NodeCode}", NodeCode);
                selSQL = selSQL.Replace("{$SiteID}", SiteID.ToString());

                if (pageSize > 0)   // 分页
                {
                    selSQL = Regex.Replace(selSQL, @"top\s*\d+", " ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    dtDataSource = dal.GetFreeLabelSplitDataSource(selSQL, currentPage, pageSize, null);
                }
                else
                {
                    dtDataSource = dal.GetFreeLabelDataSource(selSQL);
                }

                labelParseParam = GetLoopLabelParam(labelConfig.Content, this.itemContentTag);
                parseResult = DataSourceBind(dtDataSource, labelParseParam, freeLabel);
                parseResult = labelParseParam.LabelTemplate.Replace(itemContentTag, parseResult);
            }

            return parseResult;
        }
        #endregion

        #region 将标签数据源中内容替换标签/绑定数据源
        /// <summary>
        /// 将标签数据源中内容替换标签/绑定数据源
        /// </summary>
        /// <param name="dtDataSource">数据源</param>
        /// <param name="labelParseParam">标签参数</param>
        /// <param name="freeLabel">当前解析的标签</param>
        /// <returns></returns>
        private string DataSourceBind(DataTable dtDataSource, LoopLabelParseParam labelParseParam, FreeLabel freeLabel)
        {
            DataTable dataSource;
            LoopLabelParseParam labelParam;
            string itemContent;
            StringBuilder sbResult;

            sbResult = new StringBuilder();
            dataSource = dtDataSource;
            labelParam = labelParseParam;

            switch (Target)  // 链接打开窗口
            {
                case LinkOpenType.Blank:
                    labelParam.ItemContent = labelParam.ItemContent.Replace("{$Target}", "_blank");
                    break;
                case LinkOpenType.Self:
                    labelParam.ItemContent = labelParam.ItemContent.Replace("{$Target}", "_self");
                    break;
                default:
                    labelParam.ItemContent = labelParam.ItemContent.Replace("{$Target}", "_self");
                    break;
            }

            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                if (labelParam.IsLoop)  // 循环
                {
                    foreach (DataRow dr in dataSource.Rows)
                    {
                        itemContent = SingleRecordBind(dr, labelParam.LstField, labelParam.ItemContent, freeLabel.MenuDirUrl, this.FileType, this.SiteDirUrl, this.CSaveType);
                        sbResult.Append(itemContent);
                    }
                }
                else  // 单记录
                {
                    itemContent = SingleRecordBind(dataSource.Rows[0], labelParam.LstField, labelParam.ItemContent, freeLabel.MenuDirUrl, this.FileType, this.SiteDirUrl, this.CSaveType);
                    sbResult.Append(itemContent);
                }
            }

            return sbResult.ToString();
        }
        #endregion

        #region 加载自由标签至缓存中
        /// <summary>
        /// 加载自由标签至缓存中
        /// </summary>
        private void Load()
        {
            DataTable dt;
            Hashtable hsFreeLabelConfig;
            FreeLabelConfig freeLabelConfig;
            Regex regLabelName;
            Match match;

            regLabelName = new Regex(@"HQB_L\d+", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            freeLabelConfig = new FreeLabelConfig();
            hsFreeLabelConfig = new Hashtable();
            dt = dal.GetFreeLabelList();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    match = regLabelName.Match(dr["LableName"].ToString().Trim());

                    if (match.Success)
                    {
                        freeLabelConfig.SQL = dr["LabelSQL"].ToString();
                        freeLabelConfig.Content = dr["LableContent"].ToString().Replace("\r\n", "").Replace("\t", "");
                        freeLabelConfig.PageSize = Common.Utils.ParseInt(dr["PageSize"], 0);
                        hsFreeLabelConfig.Add(match.Value, freeLabelConfig);
                    }
                }
            }

            HttpContext.Current.Cache[freeLabelCacheName] = hsFreeLabelConfig;
        }
        #endregion

        #region 获取标签SQL配置
        /// <summary>
        /// 获取标签SQL配置
        /// </summary>
        /// <param name="labelName">自由标签代码</param>
        /// <returns></returns>
        public FreeLabelConfig GetFreeLabelConfig(string labelName)
        {
            FreeLabelConfig freeLabelConfig;

            if (this.LabelList[labelName] != null)
            {
                freeLabelConfig = (FreeLabelConfig)this.LabelList[labelName];
            }
            else
            {
                freeLabelConfig = new FreeLabelConfig();
            }

            return freeLabelConfig;
        }
        #endregion

        #region 从模板内容中抓取自由标签
        /// <summary>
        /// 从模板内容中抓取自由标签
        /// </summary>
        /// <param name="templateContent">模板内容</param>
        /// <returns></returns>
        public List<FreeLabel> GetLabelList(string templateContent)
        {
            Regex reg;
            MatchCollection matchs;
            List<FreeLabel> lstFreeLabel;   // 模块中的自由标签
            FreeLabel freeLabel;            // 自由标签属性
            string nodeCode;                // 所属节点(栏目)
            string splitLabelID;
            DataRow[] drMenu;
            Regex regParam;                     // 匹配列表类型系统标签参数
            MatchCollection collectParam;

            lstFreeLabel = new List<FreeLabel>();
            freeLabel = new FreeLabel();
            reg = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']FREE[""'](?<4>[^}]*)\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regParam = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase);

            matchs = reg.Matches(templateContent);
            nodeCode = string.Empty;
            splitLabelID = string.Empty;

            if (this.MenuList != null)
            {
                foreach (Match matchItem in matchs)
                {
                    if (matchItem.Value.Trim() != "")
                    {
                        freeLabel.Name = "HQB_" + matchItem.Groups[1].Value + "";
                        freeLabel.Config = matchItem.Value;
                        collectParam = regParam.Matches(matchItem.Groups[3].Value + matchItem.Groups[4].Value);

                        foreach (Match item in collectParam)
                        {
                            switch (item.Groups[1].Value.ToLower().Trim())
                            {
                                case "nodecode":
                                    nodeCode = matchItem.Groups[2].Value;
                                    break;
                                case "splitlabelid":
                                    splitLabelID = matchItem.Groups[2].Value;
                                    break;
                            }
                        }


                        if (nodeCode.Trim() == "")
                        {
                            nodeCode = this._nodeCode;
                        }

                        drMenu = this.MenuList.Select("NodeCode='" + nodeCode + "'");

                        if (drMenu != null && drMenu.Length > 0)
                        {
                            freeLabel.NodeCode = nodeCode;
                            freeLabel.MenuDirUrl = drMenu[0]["RootDirPath"].ToString();     // 当前发布栏目URL
                            freeLabel.SplitLabelID = splitLabelID;             // 分页标签ID
                            lstFreeLabel.Add(freeLabel);
                        }
                    }
                }
            }

            return lstFreeLabel;
        }
        #endregion
    }
}
