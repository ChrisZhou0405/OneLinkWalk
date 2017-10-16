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
// 功能描述：发布/生成类型
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    /// <summary>
    ///  发布/生成类型
    /// </summary>
    public enum PublishType
    {
        /// <summary>
        /// 生成内容的ID 多个ID可由 , 分隔
        /// </summary>
        ContentIDEnum = 1,
        /// <summary>
        /// 生成更新时间区间
        /// </summary>
        AddDateRange
    }
}
