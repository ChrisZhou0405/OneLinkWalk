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
// 功能描述：分页标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 分页标签
    /// </summary>
    public struct SplitLabel
    {
        #region 变量
        private string _name;
        private string _config;
        private string _splitType;
        private string _id;
        private string _content;
        #endregion

        #region 属性
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        /// <summary>
        /// 从模板中抓取的标签内容
        /// </summary>
        public string Config
        {
            get { return this._config; }
            set { this._config = value; }
        }

        /// <summary>
        /// 分页样式 
        /// </summary>
        public string SplitType
        {
            get { return this._splitType; }
            set { this._splitType = value; }
        }

        /// <summary>
        /// 分页标签ID
        /// </summary>
        public string ID
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// 分页标签内容
        /// </summary>
        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }
        #endregion
    }
}
