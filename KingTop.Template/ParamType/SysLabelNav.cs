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
// 功能描述： 系统标签 -- 导航
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    public struct SysLabelNav
    {
        #region 变量成员
        private string _labelName;
        private string _cssClass;
        private LinkOpenType _target;
        private bool _isNavTagWord;
        private string _navTagContent;
        private string _nodeCode;
        private string _htmlCode;   //导航使用的html代码 配置模式 支持：div p span li gavin by 20101117
        #endregion

        #region 属性
        /// <summary>
        /// 标签名（从模板中抓取的标签内容）
        /// </summary>
        public string LabelName
        {
            set { this._labelName = value; }
            get { return this._labelName; }
        }
        /// <summary>
        /// 文字样式
        /// </summary>
        public string CssClass
        {
            set { this._cssClass = value; }
            get { return this._cssClass; }
        }
        /// <summary>
        /// 打开方式
        /// </summary>
        public LinkOpenType Target
        {
            set { this._target = value; }
            get { return this._target; }
        }
        /// <summary>
        /// 导航标识是否为文字
        /// </summary>
        public bool IsNavTagWord
        {
            set { this._isNavTagWord = value; }
            get { return this._isNavTagWord; }
        }
        /// <summary>
        /// 导航标识内容  文字内容或图片路径，与IsNavTagWord取值有关
        /// </summary>
        public string NavTagContent
        {
            set { this._navTagContent = value; }
            get { return this._navTagContent; }
        }

        /// <summary>
        /// 栏目NodeCode
        /// </summary>
        public string NodeCode
        {
            get { return this._nodeCode; }
            set { this._nodeCode = value; }
        }

        /// <summary>
        /// 导航使用的html代码
        /// </summary>
        public string HtmlCode
        {
            set { this._htmlCode = value; }
            get { return this._htmlCode; }
        }
        #endregion
    }
}
