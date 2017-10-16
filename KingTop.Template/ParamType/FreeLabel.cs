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
// 功能描述：自由标签属性
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 自由标签属性
    /// </summary>
    public struct FreeLabel
    {
        #region 变量成员
        private string _menuDirUrl;
        private string _name;
        private string _config;
        private string _splitLabelID;
        private string _nodeCode;
        #endregion

        #region 属性
        /// <summary>
        /// 栏目ID
        /// </summary>
        public string MenuDirUrl
        {
            set { this._menuDirUrl = value; }
            get { return this._menuDirUrl; }
        }

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name
        {
            set { this._name = value; }
            get { return this._name; }
        }

        /// <summary>
        /// 栏目代码
        /// </summary>
        public string NodeCode
        {
            set { this._nodeCode = value; }
            get { return this._nodeCode; }
        }

        /// <summary>
        /// 抓取
        /// </summary>
        public string Config
        {
            get { return this._config; }
            set { this._config = value; }
        }

        /// <summary>
        /// 分页标签ID
        /// </summary>
        public string SplitLabelID
        {
            get { return this._splitLabelID; }
            set { this._splitLabelID = value; }
        }
        #endregion
    }
}
