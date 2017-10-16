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
// 功能描述：内容页文件保存方式
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 内容页文件保存方式
    /// </summary>
    public enum ContentPageSaveType
    {
        /// <summary>
        /// 统一保存在指定的“c”文件夹中,并且按月份保存 如： c / 201008 / 100000978028767.html
        /// </summary>
        ContentAndDate = 1,
        /// <summary>
        /// 统一保存在指定的“c”文件夹中 如： c / 100000978028767.htm
        /// </summary>
        Content,
        /// <summary>
        /// 内容页文件保存在所属频道（一级栏目）的“c”文件夹中,并且按月份保存 如： News / c / 201008 / 100000978028767.html
        /// </summary>
        MenuContentAndDate,
        /// <summary>
        /// 内容页文件保存在所属频道（一级栏目）的“c”文件夹中 News / c / 100000978028767.html
        /// </summary>
        MenuAndContent,
        /// <summary>
        /// 内容页文件保存在所属频道（一级栏目）的文件夹中,并且按月份保存 如： News / 201008 / 100000978028767.html
        /// </summary>
        MenuAndDate,
        /// <summary>
        /// 内容页文件保存在所属频道（一级栏目）的文件夹中 News / 100000978028767.html
        /// </summary>
        Menu
    }
}
