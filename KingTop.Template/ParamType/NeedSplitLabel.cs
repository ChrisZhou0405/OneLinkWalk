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
// 功能描述：需要分页的标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 需要分页的标签
    /// </summary>
    public struct NeedSplitLabel
    {
        #region 变量成员
        private object _label;
        private NeedSplitLabelType _type;
        #endregion

        #region 属性
        /// <summary>
        ///  需要分页的标签的标签对象
        /// </summary>
        public object Label
        {
            get { return this._label; }
            set { this._label = value; }
        }
        /// <summary>
        /// 标签类型
        /// </summary>
        public NeedSplitLabelType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
        #endregion
    }
}
