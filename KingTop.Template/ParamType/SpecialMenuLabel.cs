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
// 功能描述：专题栏目标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    public struct SpecialMenuLabel
    {
        #region 变量成员
        private string _labelName;
        private string _spcialID;
        private string _cssClass;
        #endregion

        #region 属性
        /// <summary>
        /// 标签配置内容（从模板中抓取的内容）
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }
        /// <summary>
        /// 所属专题
        /// </summary>
        public string SpecialID
        {
            get { return this._spcialID; }
            set { this._spcialID = value; }
        }

        /// <summary>
        /// 样式类名
        /// </summary>
        public string CssClass
        {
            get { return this._cssClass; }
            set { this._cssClass = value; }
        }
        #endregion
    }
}
