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
// 功能描述：循环标签经解析后的参数
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 循环标签经解析后的参数
    /// </summary>
    public struct LoopLabelParseParam
    {
        #region 变量成员
        private string _labelTemplate;
        private string _itemContent;
        private List<Field> _lstField;
        private bool _isLoop;
        #endregion

        #region 属性成员
        /// <summary>
        ///  标签模板内容 {#ItemContent#} 为 标签内容主体
        /// </summary>
        public string LabelTemplate
        {
            get { return this._labelTemplate; }
            set { this._labelTemplate = value; }
        }

        /// <summary>
        /// 标签内容主体
        /// </summary>
        public string ItemContent
        {
            get { return this._itemContent; }
            set { this._itemContent = value; }
        }

        /// <summary>
        /// 字段列
        /// </summary>
        public List<Field> LstField
        {
            set { this._lstField = value; }
            get { return this._lstField; }
        }

        /// <summary>
        /// 是否循环
        /// </summary>
        public bool IsLoop
        {
            set { this._isLoop = value; }
            get { return this._isLoop; }
        }
        #endregion
    }
}
