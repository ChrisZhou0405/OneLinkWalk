using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      肖丹 
//    创建时间： 2010年3月22日
//    功能描述： 作者模型
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class ModuleNode
    {
        #region 私有变量成员

        private Guid _nodeId;                  // 节点ID,主键
        private string _nodeCode;              // 节点编码
        private string _nodeName;              // 节点名称
        private string _nodeType;              // 树形菜单中该节点的类型

        private string _linkURL;               // 自定义链接地址
        private string _parentNode;            // 父节点

        private Guid _ModuleID;                // 所属模块

        private bool _isLeftDisplay;           // 是否显示在左边菜单栏
        private bool _isValid;                 // 是否有效
        private string _nodelOrder;            // 排序号

        private string _nodelDesc;             // 节点说明
        private string _nodelEngDesc;          // 节点英文说明
        private string _nodelIcon;             // 节点图标
        private string _mouseOverImg;          // 鼠标移上去栏目图片（前台栏目）
        private string _currentImg;            // 当前栏目图片（前台栏目）
        private bool _IsSystem;                // 是否系统节点
        private bool _IsWeb;                   // 是否前台栏目
        private int _WebSiteID;                // 所属站点

        private string _NodeDir;               // 栏目目录名

        private string _Tips;                  // 栏目提示
        private string _Meta_Keywords;         // Meta关键字

        private string _Meta_Description;      // Meta说明
        private string _PageTitle;              // 页面标题
        private string _Custom_Content;        // 栏目自设内容
        private string _DefaultTemplate;       // 栏目首页模板
        private string _ListPageTemplate;      // 栏目列表页模板

        private string _ContentTemplate;       // 内容页模板

        private bool _EnableSubDomain;         // 是否允许子域名

        private string _SubDomain;             // 子域名地址
        private string _Settings;              // 栏目设置
        private string _Creater;               // 创建人

        private DateTime _CreateDate;          // 创建时间
        private string _ReviewFlowID;          // 流程ID
        private int _OpenType;                 // 打开方式
        private int _PurviewType;              // 访问权限
        private bool _IsEnableComment;         // 是否允许评论
        private bool _IsCreateListPage;        // 列表页生成方式

        private int _IncrementalUpdatePages;   // 增量更新HTML页数
        private bool _IsEnableIndexCache;      // 是否缓存动态页首页
        private int _ListPageSavePathType;     // 列表页生成类型

        private string _ListPagePostFix;       // 列表页后缀
        private bool _IsCreateContentPage;     // 内容页生成方式

        private string _ContentPageHtmlRule;   // 内容页的文件名规则

        private int _AutoCreateHtmlType;       // 自动生成Html类型
        private string _Custom_Images;         // 自设上传图片
        private bool _IsContainWebContent;     // 是否包含前台内容
        private int _ColumnType;                //栏目类型 1:普通栏目,2:单页栏目,3:外部链接
        private string _CustomManagelink;        //自定义后台连接，如果此项不为空，则后台地址为这个字段的值，否则取模块的值
        private string _banner;  //栏目banner
        private bool _IsTopMenuShow;   //是否前台头部栏目显示
        private bool _IsLeftMenuShow;  //是否前台左边栏目显示

        #endregion

        #region 构造函数

        public ModuleNode()
        { }
        #endregion

        #region 属性

        /// <summary>
        /// 节点ID,主键
        /// </summary>
        public virtual Guid NodeID
        {
            set { this._nodeId = value; }
            get { return this._nodeId; }
        }
        /// <summary>
        /// 节点编码
        /// </summary>
        public virtual string NodeCode
        {
            set { this._nodeCode = value; }
            get { return this._nodeCode; }
        }
        /// <summary>
        /// 节点名称
        /// </summary>
        public virtual string NodeName
        {
            set { this._nodeName = value; }
            get { return this._nodeName; }
        }
        /// <summary>
        /// 树形菜单中该节点的类型

        /// </summary>
        public virtual string NodeType
        {
            set { this._nodeType = value; }
            get { return this._nodeType; }
        }
        /// <summary>
        /// 自定义链接地址
        /// </summary>
        public virtual string LinkURL
        {
            set { this._linkURL = value; }
            get { return this._linkURL; }
        }
        /// <summary>
        /// 父节点

        /// </summary>
        public virtual string ParentNode
        {
            set { this._parentNode = value; }
            get { return this._parentNode; }
        }
        /// <summary>
        /// 所属模块

        /// </summary>
        public virtual Guid ModuleID
        {
            set { this._ModuleID = value; }
            get { return this._ModuleID; }
        }
        /// <summary>
        /// 是否显示在左边菜单栏
        /// </summary>
        public virtual bool IsLeftDisplay
        {
            set { this._isLeftDisplay = value; }
            get { return this._isLeftDisplay; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid
        {
            set { this._isValid = value; }
            get { return this._isValid; }
        }
        /// <summary>
        /// 排序号

        /// </summary>
        public virtual string NodelOrder
        {
            set { this._nodelOrder = value; }
            get { return this._nodelOrder; }
        }
        /// <summary>
        /// 节点说明
        /// </summary>
        public virtual string NodelDesc
        {
            set { this._nodelDesc = value; }
            get { return this._nodelDesc; }
        }
        /// <summary>
        /// 节点英文说明
        /// </summary>
        public virtual string NodelEngDesc
        {
            set { this._nodelEngDesc = value; }
            get { return this._nodelEngDesc; }
        }
        /// <summary>
        /// 节点图标
        /// </summary>
        public virtual string NodelIcon
        {
            set { this._nodelIcon = value; }
            get { return this._nodelIcon; }
        }
        /// <summary>
        /// 是否系统节点
        /// </summary>
        public virtual bool IsSystem
        {
            set { this._IsSystem = value; }
            get { return this._IsSystem; }
        }
        /// <summary>
        /// 是否前台栏目
        /// </summary>
        public virtual bool IsWeb
        {
            set { this._IsWeb = value; }
            get { return this._IsWeb; }
        }
        /// <summary>
        /// 所属站点

        /// </summary>
        public virtual int WebSiteID
        {
            set { this._WebSiteID = value; }
            get { return this._WebSiteID; }
        }

        /// <summary>
        /// 栏目目录名

        /// </summary>
        public virtual string NodeDir
        {
            set { this._NodeDir = value; }
            get { return this._NodeDir; }
        }

        /// <summary>
        /// 栏目提示
        /// </summary>
        public virtual string Tips
        {
            set { this._Tips = value; }
            get { return this._Tips; }
        }
        /// <summary>
        /// Meta关键字

        /// </summary>
        public virtual string Meta_Keywords
        {
            set { this._Meta_Keywords = value; }
            get { return this._Meta_Keywords; }
        }
        /// <summary>
        /// Meta说明
        /// </summary>
        public virtual string Meta_Description
        {
            set { this._Meta_Description = value; }
            get { return this._Meta_Description; }
        }

        /// <summary>
        /// Meta说明
        /// </summary>
        public virtual string PageTitle
        {
            set { this._PageTitle = value; }
            get { return this._PageTitle; }
        }

        /// <summary>
        /// 栏目自设内容
        /// </summary>
        public virtual string Custom_Content
        {
            set { this._Custom_Content = value; }
            get { return this._Custom_Content; }
        }
        /// <summary>
        /// 栏目首页模板
        /// </summary>
        public virtual string DefaultTemplate
        {
            set { this._DefaultTemplate = value; }
            get { return this._DefaultTemplate; }
        }
        /// <summary>
        /// 栏目列表页模板

        /// </summary>
        public virtual string ListPageTemplate
        {
            set { this._ListPageTemplate = value; }
            get { return this._ListPageTemplate; }
        }
        /// <summary>
        /// 内容页模板

        /// </summary>
        public virtual string ContentTemplate
        {
            set { this._ContentTemplate = value; }
            get { return this._ContentTemplate; }
        }

        /// <summary>
        /// 是否允许子域名

        /// </summary>
        public virtual bool EnableSubDomain
        {
            set { this._EnableSubDomain = value; }
            get { return this._EnableSubDomain; }
        }
        /// <summary>
        /// 栏目设置
        /// </summary>
        public virtual string Settings
        {
            set { this._Settings = value; }
            get { return this._Settings; }
        }
        /// <summary>
        /// 子域名地址
        /// </summary>
        public virtual string SubDomain
        {
            set { this._SubDomain = value; }
            get { return this._SubDomain; }
        }
        /// <summary>
        /// 创建人

        /// </summary>
        public virtual string Creater
        {
            set { this._Creater = value; }
            get { return this._Creater; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateDate
        {
            set { this._CreateDate = value; }
            get { return this._CreateDate; }
        }
        /// <summary>
        /// 流程ID
        /// </summary>
        public virtual string ReviewFlowID
        {
            set { this._ReviewFlowID = value; }
            get { return this._ReviewFlowID; }
        }

        /// <summary>
        /// 打开方式
        /// </summary>
        public virtual int OpenType
        {
            set { this._OpenType = value; }
            get { return this._OpenType; }
        }

        /// <summary>
        /// 访问权限
        /// </summary>
        public virtual int PurviewType
        {
            set { this._PurviewType = value; }
            get { return this._PurviewType; }
        }

        /// <summary>
        /// 是否允许评论
        /// </summary>
        public virtual bool IsEnableComment
        {
            set { this._IsEnableComment = value; }
            get { return this._IsEnableComment; }
        }

        /// <summary>
        /// 列表页生成方式

        /// </summary>
        public virtual bool IsCreateListPage
        {
            set { this._IsCreateListPage = value; }
            get { return this._IsCreateListPage; }
        }

        /// <summary>
        /// 增量更新HTML页数
        /// </summary>
        public virtual int IncrementalUpdatePages
        {
            set { this._IncrementalUpdatePages = value; }
            get { return this._IncrementalUpdatePages; }
        }

        /// <summary>
        /// 是否缓存动态页首页
        /// </summary>
        public virtual bool IsEnableIndexCache
        {
            set { this._IsEnableIndexCache = value; }
            get { return this._IsEnableIndexCache; }
        }

        /// <summary>
        /// 列表页生成类型

        /// </summary>
        public virtual int ListPageSavePathType
        {
            set { this._ListPageSavePathType = value; }
            get { return this._ListPageSavePathType; }
        }

        /// <summary>
        /// 列表页后缀
        /// </summary>
        public virtual string ListPagePostFix
        {
            set { this._ListPagePostFix = value; }
            get { return this._ListPagePostFix; }
        }

        /// <summary>
        /// 内容页生成方式

        /// </summary>
        public virtual bool IsCreateContentPage
        {
            set { this._IsCreateContentPage = value; }
            get { return this._IsCreateContentPage; }
        }

        /// <summary>
        /// 内容页的文件名规则

        /// </summary>
        public virtual string ContentPageHtmlRule
        {
            set { this._ContentPageHtmlRule = value; }
            get { return this._ContentPageHtmlRule; }
        }

        /// <summary>
        /// 自动生成Html类型
        /// </summary>
        public virtual int AutoCreateHtmlType
        {
            set { this._AutoCreateHtmlType = value; }
            get { return this._AutoCreateHtmlType; }
        }

        /// <summary>
        /// 自设上传图片
        /// </summary>
        public virtual string Custom_Images
        {
            set { this._Custom_Images = value; }
            get { return this._Custom_Images; }
        }

        /// <summary>
        /// 是否包含前台内容
        /// </summary>
        public virtual bool IsContainWebContent
        {
            set { this._IsContainWebContent = value; }
            get { return this._IsContainWebContent; }
        }

        /// <summary>
        /// 栏目类型
        /// </summary>
        public virtual int ColumnType
        {
            set { this._ColumnType = value; }
            get { return this._ColumnType; }
        }

        /// <summary>
        /// 自定义后台连接
        /// </summary>
        public virtual string CustomManageLink
        {
            set { this._CustomManagelink = value; }
            get { return this._CustomManagelink; }
        }

        /// <summary>
        /// 鼠标移上去栏目图片
        /// </summary>
        public virtual string MouseOverImg
        {
            set { this._mouseOverImg = value; }
            get { return this._mouseOverImg; }
        }

        /// <summary>
        /// 当前栏目图片
        /// </summary>
        public virtual string CurrentImg
        {
            set { this._currentImg = value; }
            get { return this._currentImg; }
        }
        /// <summary>
        /// 栏目Banner
        /// </summary>
        public virtual string Banner
        {
            set { this._banner = value; }
            get { return this._banner; }
        }

        /// <summary>
        /// 是否前台头部栏目显示
        /// </summary>
        public virtual bool IsTopMenuShow
        {
            set { this._IsTopMenuShow = value; }
            get { return this._IsTopMenuShow; }
        }
        /// <summary>
        /// 是否前台左边栏目显示
        /// </summary>
        public virtual bool IsLeftMenuShow
        {
            set { this._IsLeftMenuShow = value; }
            get { return this._IsLeftMenuShow; }
        }
        #endregion
    }
}
