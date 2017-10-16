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
// 功能描述：期刊标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    public struct SysLabelPeriodical
    {
        #region 变量成员
        private string _labelName;
        private string _contentTemplate;
        private LoopTemplate _template1;
        private LoopTemplate _template2;
        #endregion

        #region 属性
        /// <summary>
        /// 显示期刊分类名
        /// </summary>
        public LoopTemplate Template1
        {
            get { return this._template1; }
            set { this._template1 = value; }
        }
        /// <summary>
        /// 显示期刊文章列表
        /// </summary>
        public LoopTemplate Template2
        {
            get { return this._template2; }
            set { this._template2 = value; }
        }
        /// <summary>
        /// 标签名称(从模板中抓取的标签)
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }

        /// <summary>
        /// 期刊文章内容模板
        /// </summary>
        public string ContentTemplate
        {
            get { return this._contentTemplate; }
            set { this._contentTemplate = value; }
        }
        #endregion
    }
}
