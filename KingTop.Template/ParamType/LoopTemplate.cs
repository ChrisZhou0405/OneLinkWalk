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
// 功能描述：循环模板
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 循环模板
    /// </summary>
    public class LoopTemplate
    {
        #region 变量成员
        private string _headTemplate;
        private string _footTemplate;
        private string _itemTemplate;
        #endregion

        #region 属性
        /// <summary>
        /// 顶部不循环部份
        /// </summary>
        public string HeadTemplate
        {
            get { return this._headTemplate; }
            set { this._headTemplate = value; }
        }

        /// <summary>
        /// 底部不循环部份
        /// </summary>
        public string FootTemplate
        {
            get { return this._footTemplate; }
            set { this._footTemplate = value; }
        }

        /// <summary>
        /// 循环模板
        /// </summary>
        public string ItemTemplate
        {
            get { return this._itemTemplate; }
            set { this._itemTemplate = value; }
        }

        #endregion
    }
}
