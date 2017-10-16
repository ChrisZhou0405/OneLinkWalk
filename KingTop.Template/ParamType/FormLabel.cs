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
// 功能描述：自定义表单标签
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Template.ParamType
{
    public struct FormLabel
    {
        #region 变量成员
        private string _labelName;
        private string _formID;
        private string _nodeCode;
        #endregion

        #region 属性
        /// <summary>
        /// 标签名称（从模板中抓取的标签内容）
        /// </summary>
        public string LabelName
        {
            get { return this._labelName; }
            set { this._labelName = value; }
        }

        /// <summary>
        ///所属表单
        /// </summary>
        public string FormID
        {
            get { return this._formID; }
            set { this._formID = value; }
        }

        /// <summary>
        /// 节点NodeCode
        /// </summary>
        public string NodeCode
        {
            get { return this._nodeCode; }
            set { this._nodeCode = value; }
        }
        #endregion
    }
}
