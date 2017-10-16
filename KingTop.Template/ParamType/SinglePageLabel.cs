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
// 功能描述：单页标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    public struct SinglePageLabel
    {
        #region 变量成员
        private string _content;
        private string _nodeCode;
        private int _length;
        private bool _isDot;
        #endregion

        #region 属性
        /// <summary>
        /// 标签内容
        /// </summary>
        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        /// <summary>
        /// 所属栏目 
        /// </summary>
        public string NodeCode
        {
            get { return this._nodeCode; }
            set { this._nodeCode = value; }
        }

        /// <summary>
        /// 截取长度
        /// </summary>
        public int Length
        {
            get { return this._length; }
            set { this._length = value; }
        }

        /// <summary>
        /// 是否加省略号
        /// </summary>
        public bool IsDot
        {
            get { return this._isDot; }
            set { this._isDot = value; }
        }
        #endregion
    }
}
