
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-03-09
// 功能描述：对K_ModelManage表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.Model.Content
{
    [Serializable]
    public class ModelManage  
    {
        #region Model

        private string _id;
        private string _menuno;
        private int _siteid;
        private string _title;
        private string _tablename;
        private string _moduleid;
        private string _sysfield;
        private string _listlink;
        private string _listbutton;
        private string _customcol;
        private string _memo;
        private int _orders;
        private bool _isenable;
        private bool _isdel;
        private bool _ishtml;
        private bool _isOrderEdit;
        private string _operationColumn;
        private bool _isListContentClip;
        private string _operationColumnWidth;
        private string _configMan;
        private string _commonField;
        private bool _isSub;
        private string _subModelGroupID;
        private string _notSearchField;
        private string _backDeliverUrlParam;
        private string _fieldFromUrlParamValue;
        private string _deliverAndSearchUrlParam;


        public ModelManage()
        {
            this._isSub = false;
            this._backDeliverUrlParam = "NodeCode";
        }

        public string NotSearchField
        {
            get { return this._notSearchField; }
            set { this._notSearchField = value; }
        }

        public string DeliverAndSearchUrlParam
        {
            get { return  this._deliverAndSearchUrlParam; }
            set { this._deliverAndSearchUrlParam = value; }
        }
        
        public string BackDeliverUrlParam
        {
            get { return this._backDeliverUrlParam; }
            set { this._backDeliverUrlParam = value; }
        }

        public string FieldFromUrlParamValue
        {
            get { return this._fieldFromUrlParamValue; }
            set { this._fieldFromUrlParamValue = value; }
        }

        public virtual bool IsSub
        {
            get { return this._isSub; }
            set { this._isSub = value; }
        }

        public virtual string SubModelGroupID
        {
            get { return this._subModelGroupID; }
            set { this._subModelGroupID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 栏目编码
        /// </summary>
        public virtual string MenuNo
        {
            set { _menuno = value; }
            get { return _menuno; }
        }

        /// <summary>
        /// 站点ID
        /// </summary>
        public virtual int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }

        /// <summary>
        /// 模型名称
        /// </summary>
        public virtual string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public virtual string TableName
        {
            set { _tablename = value; }
            get { return _tablename; }
        }

        /// <summary>
        /// 模块ID
        /// </summary>
        public virtual string ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }

        /// <summary>
        /// 系统字段
        /// </summary>
        public virtual string SysField
        {
            set { _sysfield = value; }
            get { return _sysfield; }
        }

        /// <summary>
        /// 列表中的链接
        /// </summary>
        public virtual string ListLink
        {
            set { _listlink = value; }
            get { return _listlink; }
        }

        /// <summary>
        /// 列表中的按钮
        /// </summary>
        public virtual string ListButton
        {
            set { _listbutton = value; }
            get { return _listbutton; }
        }

        /// <summary>
        /// 自定义列
        /// </summary>
        public virtual string CustomCol
        {
            set { _customcol = value; }
            get { return _customcol; }
        }

        /// <summary>
        /// 模型简介        /// </summary>
        public virtual string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Orders
        {
            set { _orders = value; }
            get { return _orders; }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnable
        {
            set { _isenable = value; }
            get { return _isenable; }
        }

        /// <summary>
        /// 删除到回收站 1=删除，0=未删除        /// </summary>
        public virtual bool IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }

        /// <summary>
        /// 删除到回收站 1=删除，0=未删除        /// </summary>
        public virtual bool IsHtml
        {
            set { _ishtml = value; }
            get { return _ishtml; }
        }

        /// <summary>
        /// 是否在界面显示排序编辑列
        /// </summary>
        public virtual bool IsOrderEdit
        {
            set { this._isOrderEdit = value; }
            get { return this._isOrderEdit; }
        }

        /// <summary>
        /// 列表操作列        /// </summary>
        public virtual string OperationColumn
        {
            set { this._operationColumn = value; }
            get { return this._operationColumn; }
        }

        /// <summary>
        /// 是否可编辑列表宽度        /// </summary>
        public virtual bool IsListContentClip
        {
            set { this._isListContentClip = value; }
            get { return this._isListContentClip; }
        }

        /// <summary>
        /// 列表操作列宽度        /// </summary>
        public virtual string OperationColumnWidth
        {
            set { this._operationColumnWidth = value; }
            get { return this._operationColumnWidth; }
        }
        
        /// <summary>
        /// 模型配置人        /// </summary>
        public virtual string ConfigMan
        {
            set { this._configMan = value; }
            get { return this._configMan; }
        }

        /// <summary>
        /// 公用字段
        /// </summary>
        public virtual string CommonField
        {
            get { return this._commonField; }
            set { this._commonField = value; }
        }
        #endregion Model

    }
}
