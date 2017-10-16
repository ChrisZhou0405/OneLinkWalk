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
// 功能描述：栏目
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    public struct Menu
    {
        #region 变量成员
        private string _nodeCode;
        private string _name;
        private string _toolTip;
        private string _url;
        private string _overImageUrl;
        private string _imageUrl;
        private List<Menu> _lstChildMenu;
        private LinkOpenType _target;
        #endregion

        #region 属性
        /// <summary>
        /// 栏目NodeCode
        /// </summary>
        public string NodeCode
        {
            get { return this._nodeCode; }
            set { this._nodeCode = value; }
        }
        /// <summary>
        /// 子栏目
        /// </summary>
        public List<Menu> ChildMenu
        {
            get { return this._lstChildMenu; }
            set { this._lstChildMenu = value; }
        }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// 栏目URL地址
        /// </summary>
        public string Url
        {
            get { return this._url; }
            set { this._url = value; }
        }
        /// <summary>
        /// 栏目提示
        /// </summary>
        public string ToolTip
        {
            set { this._toolTip = value; }
            get { return this._toolTip; }
        }

        /// <summary>
        /// 栏目鼠标经过图片
        /// </summary>
        public string OverImageUrl
        {
            set { this._overImageUrl = value; }
            get { return this._overImageUrl; }
        }

        /// <summary>
        /// 栏目(鼠标移开时)图片
        /// </summary>
        public string ImageUrl
        {
            set { this._imageUrl = value; }
            get { return this._imageUrl; }
        }

        /// <summary>
        /// 链接打开方式
        /// </summary>
        public LinkOpenType Target
        {
            set { this._target = value; }
            get { return this._target; }
        }
        #endregion

    }
}
