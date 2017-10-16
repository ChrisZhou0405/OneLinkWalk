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
// 功能描述：子模型参数
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 子模型参数
    /// </summary>
    public class SubModelParam
    {
        #region 变量成员
        private string _indexTemplate;
        private string _contentTemplate;
        private string _fieldName;
        private string _labelName;
        private List<string> _lstParentID;
        #endregion

        public SubModelParam()
        {
            this.LstParentID = new List<string>();
        }

        #region 属性
        /// <summary>
        /// 子模型首页模板
        /// </summary>
        public string IndexTemplate
        {
            get { return this._indexTemplate; }
            set { this._indexTemplate = value; }
        }
        /// <summary>
        /// 子模型内容页模板+
        /// </summary>
        public string ContentTemplate
        {
            get { return this._contentTemplate; }
            set { this._contentTemplate = value; }
        }
        /// <summary>
        /// 子模型类型字段-
        /// </summary>
        public string FieldName
        {
            get { return this._fieldName; }
            set { this._fieldName = value; }
        }

        /// <summary>
        /// 从模板中抓取的标签配置内容
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }

        }
        /// <summary>
        /// 需解析（子模型）的记录
        /// </summary>
        public List<string> LstParentID
        {
            get { return this._lstParentID; }
            set { this._lstParentID = value; }
        }
        #endregion
    }
}
