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
// 功能描述：栏目模板类型
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 栏目模板类型
    /// </summary>
    public enum TemplateType
    {
        /// <summary>
        /// 栏目首页模板
        /// </summary>
        Index = 1,
        /// <summary>
        /// 栏目列表页模板
        /// </summary>
        List,
        /// <summary>
        /// 栏目内容模板
        /// </summary>
        Content,
        /// <summary>
        /// 单页模板
        /// </summary>
        Single
    }
}
