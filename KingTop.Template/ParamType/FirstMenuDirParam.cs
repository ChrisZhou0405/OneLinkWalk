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
// 功能描述：一级栏目目录参数
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 一级栏目目录参数
    /// </summary>
    public struct FirstMenuDirParam
    {
        private string _menuDir;
        private bool _hasDefaultTemplate;

        /// <summary>
        /// 栏目目录
        /// </summary>
        public string MenuDir
        {
            get { return this._menuDir; }
            set { this._menuDir = value; }
        }

        /// <summary>
        /// 是否有栏目首页模板
        /// </summary>
        public bool HasDefaultTemplate
        {
            get { return this._hasDefaultTemplate; }
            set { this._hasDefaultTemplate = value; }
        }
    }
}
