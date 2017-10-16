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
// 功能描述：发布参数
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 发布参数
    /// </summary>
    public struct PublishParam
    {
        #region 变量成员
        private PublishType _publishType;
        private bool _isSiteIndex;
        private bool _isMenuIndex;
        private bool _isMenuList;
        private bool _isContent;
        private bool _unPublished;
        private bool _isCopyFile;
        private List<string> _lstMenu;
        private string[] _publishTypeParam;
        #endregion

        #region 属性
        /// <summary>
        /// 只生成未生成页面
        /// </summary>
        public bool UnPublished
        {
            get { return this._unPublished; }
            set { this._unPublished = value; }
        }
        /// <summary>
        /// 要发布的栏目
        /// </summary>
        public List<string> LstMenu
        {
            get { return this._lstMenu; }
            set { this._lstMenu = value; }
        }
        /// <summary>
        /// 生成网站首页
        /// </summary>
        public bool IsSiteIndex
        {
            get { return this._isSiteIndex; }
            set { this._isSiteIndex = value; }
        }

        /// <summary>
        /// 生成栏目首页
        /// </summary>
        public bool IsMenuIndex
        {
            get { return this._isMenuIndex; }
            set { this._isMenuIndex = value; }
        }

        /// <summary>
        /// 生成栏目列表
        /// </summary>
        public bool IsMenuList
        {
            get { return this._isMenuList; }
            set { this._isMenuList = value; }
        }

        /// <summary>
        /// 生成内容页面
        /// </summary>
        public bool IsContent
        {
            get { return this._isContent; }
            set { this._isContent = value; }
        }

        /// <summary>
        /// 发布类型
        /// </summary>
        public PublishType Type
        {
            get { return this._publishType; }
            set { this._publishType = value; }
        }

        /// <summary>
        /// 保存与Type属性相关的参数 生成更新时间 类型的起始时间与结束时间
        /// </summary>
        public string[] PublishTypeParam
        {
            get { return this._publishTypeParam; }
            set { this._publishTypeParam = value; }
        }

        /// <summary>
        /// 是否复制样式文件文件
        /// </summary>
        public bool IsCopyFile
        {
            get { return this._isCopyFile; }
            set { this._isCopyFile = value; }
        }
        #endregion
    }
}
