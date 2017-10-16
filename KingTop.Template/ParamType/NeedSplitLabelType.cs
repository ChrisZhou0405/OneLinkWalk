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
// 功能描述：要分页的标签类型
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 要分页的标签类型
    /// </summary>
    public enum NeedSplitLabelType
    {
        /// <summary>
        /// 自由标签
        /// </summary>
        FreeLabel,
        /// <summary>
        /// 列表类型系统标签
        /// </summary>
        SysLabelList
    }
}
