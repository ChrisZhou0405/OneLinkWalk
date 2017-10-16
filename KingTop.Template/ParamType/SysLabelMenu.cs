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
// 功能描述： 系统标签 -- 栏目
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    /// <summary>
    /// 系统标签 -- 栏目
    /// </summary>
    public class SysLabelMenu
    {
        #region 变量成员
        private SysLabelMenuType _type;
        private string _labelName;
        private bool _isBothMenu;
        private int _level;
        private bool _isWordMenu;
        private int _subMenuType;
        private MenuShowType _showType;
        private string _currentCssType;
        private string _nodeCode;
        private string _templateContent;
        private LoopTemplate _item1Template;
        private LoopTemplate _item2Template;
        private LoopTemplate _item3Template;
        #endregion

        #region 构造函数
        public SysLabelMenu()
        {
            this._isBothMenu = false;
            this._isWordMenu = true;
            this._level = 1;
            this._subMenuType = 1;
        }
        #endregion

        #region 属性
        /// <summary>
        /// 标签类型
        /// </summary>
        public SysLabelMenuType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
        /// <summary>
        /// 标签名（从模板中抓取的标签内容）
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }
        /// <summary>
        /// 是否显示二级栏目
        /// </summary>
        public bool IsBothMenu
        {
            get { return this._isBothMenu; }
            set { this._isBothMenu = value; }
        }
        /// <summary>
        /// 栏目级数
        /// </summary>
        public int Level
        {
            get { return this._level; }
            set { this._level = value; }
        }

        /// <summary>
        /// 导航类型  1 文字类型 0 图片类型
        /// </summary>
        public bool IsWordMenu
        {
            get { return this._isWordMenu; }
            set { this._isWordMenu = value; }
        }

        /// <summary>
        /// 显示方式
        /// </summary>
        public MenuShowType ShowType
        {
            get { return this._showType; }
            set { this._showType = value; }
        }
        /// <summary>
        /// 当前栏目样式
        /// </summary>
        public string CurrentCssType
        {
            get { return this._currentCssType; }
            set { this._currentCssType = value; }
        }
        /// <summary>
        /// 所属栏目
        /// </summary>
        public string NodeCode
        {
            get { return this._nodeCode; }
            set { this._nodeCode = value; }
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

        /// <summary>
        /// 当前栏目子栏目显示类型
        /// </summary>
        public int SubMenuType
        {
            get { return this._subMenuType; }
            set { this._subMenuType = value; }
        }
        #endregion
    }
}
