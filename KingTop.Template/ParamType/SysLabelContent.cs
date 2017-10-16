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
// 功能描述：系统标签 -- 详细页标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    /// <summary>
    ///  系统标签 -- 详细页标签
    /// </summary>
    public struct SysLabelContent
    {
        #region 变量成员
        private string _labelName;
        private string _templateContent;
        private string _sql;
        #endregion

        #region 属性
        /// <summary>
        /// 标签名（从模板中抓取的标签内容）
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string TemplateContent
        {
            set { this._templateContent = value; }
            get { return this._templateContent; }
        }

        /// <summary>
        /// 要执行的SQL语句
        /// </summary>
        public string SQL
        {
            get { return this._sql; }
            set { this._sql = value; }
        }
        #endregion
    }
}
