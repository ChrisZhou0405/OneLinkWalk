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
// 功能描述：包含标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 包含标签
    /// </summary>
    public struct IncludeLabel
    {
        #region  变量成员
        private string _content;
        private string _filePath;
        #endregion

        #region  属性
        /// <summary>
        /// 标签内容
        /// </summary>
        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        /// <summary>
        /// 包含的文件路径
        /// </summary>
        public string FilePath
        {
            get { return this._filePath; }
            set { this._filePath = value; }
        }
        #endregion
    }
}
