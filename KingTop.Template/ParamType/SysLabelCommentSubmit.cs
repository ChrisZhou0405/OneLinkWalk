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
// 作者：吴岸标// 创建日期：2010-12-21
// 功能描述： 系统标签 -- 评论内容提交标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Template.ParamType
{
    public class SysLabelCommentSubmit
    {
        #region 变量
        private string _labelName;
        private string _content;
        private string _nodeCodeOrCategory;
        private string _loginedMark;
        #endregion

        #region 属性
        /// <summary>
        /// 标签名称(从模板中抓取的标签名)
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }
        /// <summary>
        /// 标签内容
        /// </summary>
        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }
        /// <summary>
        ///商城站点为分类，内容站点为栏目
        /// </summary>
        public string NodeCodeOrCategory
        {
            get { return this._nodeCodeOrCategory; }
            set { this._nodeCodeOrCategory = value; }
        }
        /// <summary>
        /// 标识用户是否登陆的Cookie名
        /// </summary>
        public string LoginedMark
        {
            get { return this._loginedMark; }
            set { this._loginedMark = value; }
        }
        #endregion
    }
}
