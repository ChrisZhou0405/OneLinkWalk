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
// 功能描述：导航
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    public struct Navigator
    {
        #region 变量成员
        private string _name;
        private string _url;
        #endregion

        #region 属性
        /// <summary>
        /// 导航名称
        /// </summary>
        public string Name
        {
            set { this._name = value; }
            get { return this._name; }
        }
        /// <summary>
        /// 导航链接
        /// </summary>
        public string Url
        {
            set { this._url = value; }
            get { return this._url; }
        }
        #endregion
    }
}
