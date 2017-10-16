using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年4月6日
//    功能描述： 公共操作模型
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class PublicOper
    {
        #region 私有变量成员
        private string _OperName;              // 操作名称
        private string _Title;              // 标题
        private bool _IsValid;              // 是否有效
        private string _ModuleName;               // 模块名称
        private bool _IsPublic;            // 是否公用        
        #endregion

        #region 构造函数
        public PublicOper()
        { }
        #endregion

        #region 属性

        /// <summary>
        /// 操作名称
        /// </summary>
        public virtual string OperName
        {
            set { this._OperName = value; }
            get { return this._OperName; }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title
        {
            set { this._Title = value; }
            get { return this._Title; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid
        {
            set { this._IsValid = value; }
            get { return this._IsValid; }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModuleName
        {
            set { this._ModuleName = value; }
            get { return this._ModuleName; }
        }
        /// <summary>
        /// 是否公用
        /// </summary>
        public virtual bool IsPublic
        {
            set { this._IsPublic = value; }
            get { return this._IsPublic; }
        }
        #endregion
    }
}
