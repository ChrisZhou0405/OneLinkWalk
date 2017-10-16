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
// 功能描述：链接打开方式
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 链接打开方式
    /// </summary>
    public enum LinkOpenType
    {
        /// <summary>
        /// 当前窗口中打开
        /// </summary>
        Self = 1,
        /// <summary>
        /// 新窗口中打开
        /// </summary>
        Blank
    }
}
