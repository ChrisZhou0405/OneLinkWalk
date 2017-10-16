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
// 功能描述：字段参数
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 自由标签中字段参数
    /// </summary>
    public struct Field
    {
        #region 变量成员
        private string _name;
        private string _outType;
        private string[] _outParam;
        #endregion

        #region 属性
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        /// <summary>
        /// 输出类型  0 直接输出 1 文本类型 2 数字类型 3 日期类型  4 布尔型
        /// </summary>
        public string OutType
        {
            set { this._outType = value; }
            get { return this._outType; }
        }
        /// <summary>
        /// 文本类型 [0] 截取长度 [1] 截断处理     
        /// 数字类型 [0] 数字类型 [1] 数字类型 
        /// 日期类型 [0]  格式 yyyy-MM-dd
        /// 是否（布尔）型 [0] 为真时输出的内容 [1] 为假输出的内容
        /// </summary>
        public string[] OutParam
        {
            set { this._outParam = value; }
            get { return this._outParam; }
        }
        #endregion
    }
}
