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
// 功能描述：子模型内容页解析参数
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 子模型内容页解析参数
    /// </summary>
    public struct SubModelContentParam
    {
        #region 变量成员
        private string _templatePath;
        private string _tableName;
        private string _parentID;
        #endregion

        #region 属性
        /// <summary>
        /// 模板路径 
        /// </summary>
        public string TemplatePath
        {
            get { return this._templatePath; }
            set { this._templatePath = value; }
        }
        /// <summary>
        ///表名
        /// </summary>
        public string TableName
        {
            get { return this._tableName; }
            set { this._tableName = value; }
        }
        /// <summary>
        /// 父ID
        /// </summary>
        public string ParentID
        {
            get { return this._parentID; }
            set { this._parentID = value; }
        }
        #endregion
    }
}
