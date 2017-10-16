#region 引用程序集
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-03
// 功能描述： 系统标签 -- 列表
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 系统标签 -- 列表
    /// </summary>
    public class SysLabelList
    {
        #region 变量成员
        private string _labelName;
        private string _tableName;
        private string[] _arrMenu;
        private int _pageSize;
        private string _sqlOrder;
        private string _sqlWhere;
        private int _titleLength;
        private string _titleCssClass;
        private HtmlContainer _container;
        private int _listColumnCount;
        private bool _isShowAddDate;
        private string _dateFormat;
        private string _dateCssClass;
        private bool _isShowBrief;
        private string _briefCssClass;
        private int _briefLength;
        private bool _isShowTitleImage;
        private String _titleImageWidth;
        private string _titleImageHeight;
        private int _titleImageCount;
        private string _titleImageCssClass;
        private string _titleSplitImage;
        private bool _isShowMoreLink;
        private bool _moreLinkIsWord;
        private string _moreLinkWordOrImageUrl;
        private string _lineHeight;
        private LinkOpenType _target;
        private bool _isSplit;
        private string _itemTemplate;
        private bool _isSubModel;
        private string _subModelCTemplate;
        private List<SubModelParam> _lstSubModel;
        #endregion

        #region 构造函数
        /// <summary>
        /// 系统标签 -- 列表
        /// </summary>
        public SysLabelList()
        {
            this.IsSplit = false;
            this.PageSize = 0;
            this.IsShowAddDate = false;
            this.IsShowBrief = false;
            this.IsShowTitleImage = false;
            this.TitleLength = 0;
            this.TitleCssClass = string.Empty;
            this.TitleImageWidth = string.Empty;
            this.TitleImageHeight = string.Empty;
            this.TitleSplitImage = string.Empty;
            this.LineHeight = string.Empty;
            this.Target = LinkOpenType.Blank;
            this.Container = HtmlContainer.LI;
            this.ItemTemplate = string.Empty;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 子模型内容模板
        /// </summary>
        public string SubModelCTemplate
        {
            get { return this._subModelCTemplate; }
            set { this._subModelCTemplate = value; }
        }
        /// <summary>
        /// 要解析的子模型
        /// </summary>
        public List<SubModelParam> LstSubModel
        {
            get { return this._lstSubModel; }
            set { this._lstSubModel = value; }
        }
        /// <summary>
        /// 是否子模型
        /// </summary>
        public bool IsSubModel
        {
            get { return this._isSubModel; }
            set { this._isSubModel = value; }
        }
        /// <summary>
        /// 标签名称(从模板中抓取的标签名)
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsSplit
        {
            get { return this._isSplit; }
            set { this._isSplit = value; }
        }
        /// <summary>
        /// 选择模型
        /// </summary>
        public string TableName
        {
            get { return this._tableName; }
            set { this._tableName = value; }
        }
        /// <summary>
        /// 所属栏目
        /// </summary>
        public string[] LstMenu
        {
            get { return this._arrMenu; }
            set { this._arrMenu = value; }
        }
        /// <summary>
        /// 文章(或分页）数量 
        /// </summary>
        public int PageSize
        {
            get { return this._pageSize; }
            set { this._pageSize = value; }
        }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string SqlOrder
        {
            get { return this._sqlOrder; }
            set { this._sqlOrder = value; }
        }
        /// <summary>
        /// 查询条件 
        /// </summary>
        public string SqlWhere
        {
            get { return this._sqlWhere; }
            set { this._sqlWhere = value; }
        }
        /// <summary>
        /// 标题显示字数
        /// </summary>
        public int TitleLength
        {
            get { return this._titleLength; }
            set { this._titleLength = value; }
        }
        /// <summary>
        /// 标题样式
        /// </summary>
        public string TitleCssClass
        {
            get { return this._titleCssClass; }
            set { this._titleCssClass = value; }
        }
        /// <summary>
        /// 输出类型
        /// </summary>
        public HtmlContainer Container
        {
            get { return this._container; }
            set { this._container = value; }
        }
        /// <summary>
        /// 显示列数 
        /// </summary>
        public int ListColumnCount
        {
            get { return this._listColumnCount; }
            set { this._listColumnCount = value; }
        }
        /// <summary>
        /// 是否显示日期
        /// </summary>
        public bool IsShowAddDate
        {
            get { return this._isShowAddDate; }
            set { this._isShowAddDate = value; }
        }
        /// <summary>
        ///  日期格式
        /// </summary>
        public string DateFormat
        {
            get { return this._dateFormat; }
            set { this._dateFormat = value; }
        }
        /// <summary>
        /// 日期样式
        /// </summary>
        public string DateCssClass
        {
            get { return this._dateCssClass; }
            set { this._dateCssClass = value; }
        }
        /// <summary>
        /// 是否显示导读（简介）
        /// </summary>
        public bool IsShowBrief
        {
            get { return this._isShowBrief; }
            set { this._isShowBrief = value; }
        }
        /// <summary>
        /// 是否显示导读（简介）样式
        /// </summary>
        public string BriefCssClass
        {
            get { return this._briefCssClass; }
            set { this._briefCssClass = value; }
        }
        /// <summary>
        /// 是否显示导读（简介）显示字数
        /// </summary>
        public int BriefLength
        {
            get { return this._briefLength; }
            set { this._briefLength = value; }
        }
        /// <summary>
        /// 是否显标题图片
        /// </summary>
        public bool IsShowTitleImage
        {
            get { return this._isShowTitleImage; }
            set { this._isShowTitleImage = value; }
        }
        /// <summary>
        /// 标题图片宽
        /// </summary>
        public string TitleImageWidth
        {
            get { return this._titleImageWidth; }
            set { this._titleImageWidth = value; }
        }
        /// <summary>
        /// 标题图片高
        /// </summary>
        public string TitleImageHeight
        {
            get { return this._titleImageHeight; }
            set { this._titleImageHeight = value; }
        }
        /// <summary>
        /// 标题图片显示个数
        /// </summary>
        public int TitleImageCount
        {
            get { return this._titleImageCount; }
            set { this._titleImageCount = value; }
        }
        /// <summary>
        /// 图片显示位置
        /// </summary>
        public string TitleImageCssClass
        {
            get { return this._titleImageCssClass; }
            set { this._titleImageCssClass = value; }
        }
        /// <summary>
        /// 标题分隔图片
        /// </summary>
        public string TitleSplitImage
        {
            get { return this._titleSplitImage; }
            set { this._titleSplitImage = value; }
        }

        /// <summary>
        /// 是否显示"更多"链接
        /// </summary>
        public bool IsShowMoreLink
        {
            get { return this._isShowMoreLink; }
            set { this._isShowMoreLink = value; }
        }

        /// <summary>
        /// 更多链接显示类型（文字/图片）
        /// </summary>
        public bool MoreLinkIsWord
        {
            get { return this._moreLinkIsWord; }
            set { this._moreLinkIsWord = value; }
        }
        /// <summary>
        /// 更多链接显示类型参数（文字内容/图片地址）
        /// </summary>
        public string MoreLinkWordOrImageUrl
        {
            get { return this._moreLinkWordOrImageUrl; }
            set { this._moreLinkWordOrImageUrl = value; }
        }
        /// <summary>
        /// 行距
        /// </summary>
        public string LineHeight
        {
            get { return this._lineHeight; }
            set { this._lineHeight = value; }
        }
        /// <summary>
        /// 链接目标
        /// </summary>
        public LinkOpenType Target
        {
            get { return this._target; }
            set { this._target = value; }
        }
        /// <summary>
        /// 自定义显示模板
        /// </summary>
        public string ItemTemplate
        {
            get { return this._itemTemplate; }
            set { this._itemTemplate = value; }
        }
        #endregion
    }
}
