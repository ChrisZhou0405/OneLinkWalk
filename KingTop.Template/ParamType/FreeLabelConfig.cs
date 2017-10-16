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
// 功能描述：自由标签配置参数
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 自由标签配置
    /// </summary>
    public struct FreeLabelConfig
    {
        #region 变量
        private string _sql;
        private string _content;
        private int _pageSize;
        #endregion

        #region 属性
        /// <summary>
        /// 获取数据源的SQL
        /// </summary>
        public string SQL
        {
            set { this._sql = value; }
            get { return this._sql; }
        }

        /// <summary>
        /// 标签内容
        /// </summary>
        public string Content
        {
            set { this._content = value; }
            get { return this._content; }
        }

        /// <summary>
        /// 页面大小或查取记录条数
        /// </summary>
        public int PageSize
        {
            set { this._pageSize = value; }
            get { return this._pageSize; }
        }
        #endregion
    }
}
