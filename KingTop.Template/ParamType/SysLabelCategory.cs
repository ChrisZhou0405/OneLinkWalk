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
// 功能描述： 系统标签 -- 类型
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    public class SysLabelCategory
    {
        #region 变量成员
        private string _labelName;
        private int _level;
        private string _cssFile;
        private string _jsFile;
        private string _categoryID;
        private bool _isSibling;
        private string _templateContent;
        private LoopTemplate _item1Template;
        private LoopTemplate _item2Template;
        private LoopTemplate _item3Template;
        #endregion

        #region 构造函数
        public SysLabelCategory()
        {
            this.IsSibling = false;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 从模板中抓取的标签内容
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }
        /// <summary>
        /// 级数
        /// </summary>
        public int Level
        {
            get { return this._level; }
            set { this._level = value; }
        }
        /// <summary>
        /// 样式文件路径
        /// </summary>
        public string CssFile
        {
            get { return this._cssFile; }
            set { this._cssFile = value; }
        }
        /// <summary>
        /// JS文件路径
        /// </summary>
        public string JsFile
        {
            get { return this._jsFile; }
            set { this._jsFile = value; }
        }
        /// <summary>
        /// 类型ID
        /// </summary>
        public string CategoryID
        {
            get { return this._categoryID; }
            set { this._categoryID = value; }
        }
        /// <summary>
        /// 显示当前类型的平级类型
        /// </summary>
        public bool IsSibling
        {
            get { return this._isSibling; }
            set { this._isSibling = value; }
        }
        /// <summary>
        /// 一级栏目显示模板
        /// </summary>
        public LoopTemplate Item1Template
        {
            get { return this._item1Template; }
            set { this._item1Template = value; }
        }
        /// <summary>
        /// 二级栏目显示模板
        /// </summary>
        public LoopTemplate Item2Template
        {
            get { return this._item2Template; }
            set { this._item2Template = value; }
        }
        /// <summary>
        /// 三级栏目显示模板
        /// </summary>
        public LoopTemplate Item3Template
        {
            get { return this._item3Template; }
            set { this._item3Template = value; }
        }
        /// <summary>
        /// 生成栏目的模板内容 仅代码模式时用
        /// </summary>
        public string TemplateContent
        {
            get { return this._templateContent; }
            set { this._templateContent = value; }
        }
        #endregion
    }
}
