
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武
// 创建日期：2010-03-12
// 功能描述：对K_ModelField表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.Model.Content
{
    public class ModelField
    {
        #region Model

        private string _id;
        private string _modelid;
        private string _name;
        private string _modelfieldgroupid;
        private string _fieldalias;
        private string _message;
        private string _basicfield;
        private string _editorStyle;
        private int _textboxmaxlength;
        private int _textboxwidth;
        private int _textboxhieght;
        private string _textboxvalidation;
        private string _validationtype;
        private string _validationmessage;
        private bool _islink;
        private bool _isfilter;
        private bool _isshield;
        private string _editortype;
        private string _optionsvalue;
        private int _optioncount;
        private bool _isfill;
        private string _minvalue;
        private string _maxvalue;
        private string _defaultvalue;
        private string _datedefaultoption;
        private string _dateformatoption;
        private bool _isupload;
        private int _maxsize;
        private string _imagetype;
        private string _imagenamerules;
        private bool _imageiswatermark;
        private bool _isuploadthumbnail;
        private bool _issavefilesize;
        private string _savefilename;
        private string _urlprefix;
        private bool _isrequired;
        private bool _isenable;
        private int _orders;
        private bool _issearch;
        private int _searchwidth;
        private int _searchorders;
        private string _listwidth;
        private string _listalignment;
        private int _listorders;
        private bool _listislink;
        private string _listlinkurl;
        private bool _listisorder;
        private string _listorderoption;
        private bool _listisdefaultorder;
        private string _listdefaultorderoption;
        private bool _isrss;
        private string _controls;
        private string _usergroupid;
        private string _rolegroupid;
        private string _addtime;
        private string _userno;
        private string _dropdowndatatype;
        private string _dropdowntable;
        private string _dropdowntextcolumn;
        private string _dropdownvaluecolumn;
        private string _dropdownsql;
        private string _dropdownsqlwhere;
        private bool _islistenable;
        private int _numbercount;
        private bool _isInputValue;
        private string _searchUIType;
        private int _dataColumnLength;
        private int _modelFieldType;
        private bool _isListVisible;
        private bool _isMultiFile;
        private int _thumbDisplayType;
        private int _thumbHeight;
        private int _thumbWidth;
        private bool _isInterface;
        private string _subModelGroupID;
        private int _imageBestWidth;  //上传图片最佳宽度
        private int _imageBestHeight; //上传图片最佳高度

        public virtual int ImageBestWidth
        {
            get { return _imageBestWidth; }
            set { _imageBestWidth = value; }
        }

        public virtual int ImageBestHeight
        {
            get { return _imageBestHeight; }
            set { _imageBestHeight = value; }
        }

        public virtual string SubModelGroupID
        {
            get { return this._subModelGroupID; }
            set { this._subModelGroupID = value; }
        }

        public bool IsInterface
        {
            get { return this._isInterface; }
            set { this._isInterface = value; }
        }

        /// <summary>
        /// 缩略图高
        /// </summary>
        public int ThumbHeight
        {
            set { this._thumbHeight = value; }
            get { return this._thumbHeight; }
        }

        /// <summary>
        /// 缩略图宽
        /// </summary>
        public int ThumbWidth
        {
            get { return this._thumbWidth; }
            set { this._thumbWidth = value; }
        }

        /// <summary>
        ///  是否显示在前台列表
        /// </summary>
        public bool IsListVisible
        {
          get { return _isListVisible; }
          set { _isListVisible = value; }
        } 
           
        /// <summary>
        /// 模型字段类型(1)为新闻,2为表单
        /// </summary>
        public int ModelFieldType
        {
            get { return _modelFieldType; }
            set { _modelFieldType = value; }
        }

        public ModelField()
        {

        }
        /// <summary>
        /// 模型字符串长度
        /// </summary>
        public int DataColumnLength
        {
            get { return _dataColumnLength; }
            set { _dataColumnLength = value; }
        }
        /// <summary>
        /// 搜索界面类型，1 分范围，2 列表加文本框
        /// </summary>
        public virtual string SearchUIType
        {
            set { _searchUIType = value; }
            get { return _searchUIType; }
        }
        /// <summary>
        /// 字段是否要从界面输入值
        /// </summary>
        public virtual bool IsInputValue
        {
            set { _isInputValue = value; }
            get { return _isInputValue; }
        }

        /// <summary>
        /// ID
        /// </summary>
        public virtual string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 模型id
        /// </summary>
        public virtual string ModelId
        {
            set { _modelid = value; }
            get { return _modelid; }
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public virtual string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 所属模型字段分组
        /// </summary>
        public virtual string ModelFieldGroupId
        {
            set { _modelfieldgroupid = value; }
            get { return _modelfieldgroupid; }
        }

        /// <summary>
        /// 字段别名
        /// </summary>
        public virtual string FieldAlias
        {
            set { _fieldalias = value; }
            get { return _fieldalias; }
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        public virtual string Message
        {
            set { _message = value; }
            get { return _message; }
        }

        /// <summary>
        /// 基本字段
        /// </summary>
        public virtual string BasicField
        {
            set { _basicfield = value; }
            get { return _basicfield; }
        }

        /// <summary>
        /// 预定义控件
        /// </summary>
        public virtual string EditorStyle
        {
            set { _editorStyle = value; }
            get { return _editorStyle; }
        }

        /// <summary>
        /// 文本框最大字符数
        /// </summary>
        public virtual int TextBoxMaxLength
        {
            set { _textboxmaxlength = value; }
            get { return _textboxmaxlength; }
        }

        /// <summary>
        /// 文本框长度
        /// </summary>
        public virtual int TextBoxWidth
        {
            set { _textboxwidth = value; }
            get { return _textboxwidth; }
        }

        /// <summary>
        /// 文本框高度
        /// </summary>
        public virtual int TextBoxHieght
        {
            set { _textboxhieght = value; }
            get { return _textboxhieght; }
        }

        /// <summary>
        /// 验证规则
        /// </summary>
        public virtual string TextBoxValidation
        {
            set { _textboxvalidation = value; }
            get { return _textboxvalidation; }
        }

        /// <summary>
        /// 验证类别
        /// </summary>
        public virtual string ValidationType
        {
            set { _validationtype = value; }
            get { return _validationtype; }
        }

        /// <summary>
        /// 验证错误提示
        /// </summary>
        public virtual string ValidationMessage
        {
            set { _validationmessage = value; }
            get { return _validationmessage; }
        }

        /// <summary>
        /// 是否启用站内链接功能
        /// </summary>
        public virtual bool IsLink
        {
            set { _islink = value; }
            get { return _islink; }
        }

        /// <summary>
        /// 是否启用字符串过滤
        /// </summary>
        public virtual bool IsFilter
        {
            set { _isfilter = value; }
            get { return _isfilter; }
        }

        /// <summary>
        /// 是否启用字符屏蔽功能
        /// </summary>
        public virtual bool IsShield
        {
            set { _isshield = value; }
            get { return _isshield; }
        }

        /// <summary>
        /// 编辑器类型
        /// </summary>
        public virtual string EditorType
        {
            set { _editortype = value; }
            get { return _editortype; }
        }

        /// <summary>
        /// 选项
        /// </summary>
        public virtual string OptionsValue
        {
            set { _optionsvalue = value; }
            get { return _optionsvalue; }
        }
        /// <summary>
        /// 每页显示项数
        /// </summary>
        public virtual int OptionCount
        {
            set { _optioncount = value; }
            get { return _optioncount; }
        }
        /// <summary>
        /// 是否填充
        /// </summary>
        public virtual bool IsFill
        {
            set { _isfill = value; }
            get { return _isfill; }
        }/// <summary>
        /// 最小值
        /// </summary>
        public virtual string MinValue
        {
            set { _minvalue = value; }
            get { return _minvalue; }
        }/// <summary>
        /// 最大值
        /// </summary>
        public virtual string MaxValue
        {
            set { _maxvalue = value; }
            get { return _maxvalue; }
        }/// <summary>
        /// 默认值
        /// </summary>
        public virtual string DefaultValue
        {
            set { _defaultvalue = value; }
            get { return _defaultvalue; }
        }/// <summary>
        /// 日期默认选项
        /// </summary>
        public virtual string DateDefaultOption
        {
            set { _datedefaultoption = value; }
            get { return _datedefaultoption; }
        }/// <summary>
        /// 日期和时间格式
        /// </summary>
        public virtual string DateFormatOption
        {
            set { _dateformatoption = value; }
            get { return _dateformatoption; }
        }/// <summary>
        /// 是否启用上传
        /// </summary>
        public virtual bool IsUpload
        {
            set { _isupload = value; }
            get { return _isupload; }
        }/// <summary>
        /// 图片最大限制
        /// </summary>
        public virtual int MaxSize
        {
            set { _maxsize = value; }
            get { return _maxsize; }
        }/// <summary>
        /// 允许上传图片类型
        /// </summary>
        public virtual string ImageType
        {
            set { _imagetype = value; }
            get { return _imagetype; }
        }/// <summary>
        /// 上传文件保存规则
        /// </summary>
        public virtual string ImageNameRules
        {
            set { _imagenamerules = value; }
            get { return _imagenamerules; }
        }/// <summary>
        /// 图片是否加水印
        /// </summary>
        public virtual bool ImageIsWatermark
        {
            set { _imageiswatermark = value; }
            get { return _imageiswatermark; }
        }/// <summary>
        /// 是否上传缩略图
        /// </summary>
        public virtual bool IsUploadThumbnail
        {
            set { _isuploadthumbnail = value; }
            get { return _isuploadthumbnail; }
        }/// <summary>
        /// 是否保存文件大小
        /// </summary>
        public virtual bool IsSaveFileSize
        {
            set { _issavefilesize = value; }
            get { return _issavefilesize; }
        }/// <summary>
        /// 保存文件大小数据库字段名称
        /// </summary>
        public virtual string SaveFileName
        {
            set { _savefilename = value; }
            get { return _savefilename; }
        }/// <summary>
        /// 网址前缀
        /// </summary>
        public virtual string UrlPrefix
        {
            set { _urlprefix = value; }
            get { return _urlprefix; }
        }/// <summary>
        /// 是否必填
        /// </summary>
        public virtual bool IsRequired
        {
            set { _isrequired = value; }
            get { return _isrequired; }
        }/// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnable
        {
            set { _isenable = value; }
            get { return _isenable; }
        }/// <summary>
        /// 排序序号
        /// </summary>
        public virtual int Orders
        {
            set { _orders = value; }
            get { return _orders; }
        }/// <summary>
        /// 是否搜索条件
        /// </summary>
        public virtual bool IsSearch
        {
            set { _issearch = value; }
            get { return _issearch; }
        }
        /// <summary>
        /// 搜索框长度
        /// </summary>
        public virtual int SearchWidth
        {
            set { _searchwidth = value; }
            get { return _searchwidth; }
        }/// <summary>
        /// 搜索框排序
        /// </summary>
        public virtual int SearchOrders
        {
            set { _searchorders = value; }
            get { return _searchorders; }
        }/// <summary>
        /// 列表列宽
        /// </summary>
        public virtual string ListWidth
        {
            set { _listwidth = value; }
            get { return _listwidth; }
        }/// <summary>
        /// 列表对齐方式
        /// </summary>
        public virtual string ListAlignment
        {
            set { _listalignment = value; }
            get { return _listalignment; }
        }/// <summary>
        /// 列表位置排序
        /// </summary>
        public virtual int ListOrders
        {
            set { _listorders = value; }
            get { return _listorders; }
        }/// <summary>
        /// 列表是否加链接
        /// </summary>
        public virtual bool ListIsLink
        {
            set { _listislink = value; }
            get { return _listislink; }
        }/// <summary>
        /// 列表链接地址和参数
        /// </summary>
        public virtual string ListLinkUrl
        {
            set { _listlinkurl = value; }
            get { return _listlinkurl; }
        }/// <summary>
        /// 列表是否要排序
        /// </summary>
        public virtual bool ListIsOrder
        {
            set { _listisorder = value; }
            get { return _listisorder; }
        }/// <summary>
        /// 列表排序方式
        /// </summary>
        public virtual string ListOrderOption
        {
            set { _listorderoption = value; }
            get { return _listorderoption; }
        }/// <summary>
        /// 列表是否默认排序
        /// </summary>
        public virtual bool ListIsDefaultOrder
        {
            set { _listisdefaultorder = value; }
            get { return _listisdefaultorder; }
        }/// <summary>
        /// 列表默认排序方式
        /// </summary>
        public virtual string ListDefaultOrderOption
        {
            set { _listdefaultorderoption = value; }
            get { return _listdefaultorderoption; }
        }/// <summary>
        /// 是否RSS字段
        /// </summary>
        public virtual bool IsRss
        {
            set { _isrss = value; }
            get { return _isrss; }
        }/// <summary>
        /// 浏览此字段的会员组
        /// </summary>
        public virtual string UserGroupId
        {
            set { _usergroupid = value; }
            get { return _usergroupid; }
        }/// <summary>
        /// 操作此字段的角色
        /// </summary>
        public virtual string RoleGroupId
        {
            set { _rolegroupid = value; }
            get { return _rolegroupid; }
        }/// <summary>
        /// AddDate
        /// </summary>
        public virtual string AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }/// <summary>
        /// AddMan
        /// </summary>
        public virtual string UserNo
        {
            set { _userno = value; }
            get { return _userno; }
        }/// <summary>
        /// 下拉数据绑定方式
        /// </summary>
        public virtual string DropDownDataType
        {
            set { _dropdowndatatype = value; }
            get { return _dropdowndatatype; }
        }/// <summary>
        /// 选定表名
        /// </summary>
        public virtual string DropDownTable
        {
            set { _dropdowntable = value; }
            get { return _dropdowntable; }
        }/// <summary>
        /// 下拉数据绑定字段TEXT列
        /// </summary>
        public virtual string DropDownTextColumn
        {
            set { _dropdowntextcolumn = value; }
            get { return _dropdowntextcolumn; }
        }/// <summary>
        /// 下拉数据绑定字段Value
        /// </summary>
        public virtual string DropDownValueColumn
        {
            set { _dropdownvaluecolumn = value; }
            get { return _dropdownvaluecolumn; }
        }
        /// <summary>
        /// 下拉数据绑定sql条件
        /// </summary>
        public virtual string DropDownSqlWhere
        {
            set { _dropdownsqlwhere = value; }
            get { return _dropdownsqlwhere; }
        }
        /// <summary>
        /// 下拉数据绑定sql语句
        /// </summary>
        public virtual string DropDownSql
        {
            set { _dropdownsql = value; }
            get { return _dropdownsql; }
        }
        /// <summary>
        /// 列表是否显示
        /// </summary>
        public virtual bool IsListEnable
        {
            set { _islistenable = value; }
            get { return _islistenable; }
        }/// <summary>
        /// 小数位数
        /// </summary>
        public virtual int NumberCount
        {
            set { _numbercount = value; }
            get { return _numbercount; }
        }

        /// <summary>
        /// 是否多文件
        /// </summary>
        public virtual bool IsMultiFile
        {
            set { this._isMultiFile = value; }
            get { return this._isMultiFile; }
        }

        public virtual string Controls
        {
            set { this._controls = value; }
            get { return this._controls; }
        }

        public virtual int ThumbDisplayType
        {
            set { this._thumbDisplayType = value; }
            get { return this._thumbDisplayType; }
        }
        #endregion Model

    }
}
