using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      肖丹 
//    创建时间： 2010年3月22日
//    功能描述： 作者模型
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class ActionPermit
    {
         #region 私有变量成员

        private Guid _id;                  // 主键
        private Guid _ModuleID;                // 模块ID
        private string _operCode;
        private string _operName;            // 操作名称
        private string _operOrder;              // 排序号
        private bool _isValid;               // 是否有效
        private string _operDesc;            // 操作描述
        private string _operEngDesc;         // 操作英文描述
        private bool _isDefaultOper;         // 是否默认操作
        private bool _IsSystem;         // 是否系统操作
         #endregion

        #region 构造函数
        public ActionPermit()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid ID
        {
            set { this._id = value; }
            get { return this._id; }
        }
        /// <summary>
        /// 模块ID
        /// </summary>
        public virtual Guid ModuleID
        {
            set { this._ModuleID = value; }
            get { return this._ModuleID; }
        }
        /// <summary>
        /// 操作编码
        /// </summary>
        public virtual string operCode
        {
            set { this._operCode = value; }
            get { return this._operCode; }
        }
        /// <summary>
        /// 操作名称
        /// </summary>
        public virtual string OperName
        {
            set { this._operName = value; }
            get { return this._operName; }
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public virtual string OperOrder
        {
            set { this._operOrder = value; }
            get { return this._operOrder; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid
        {
            set { this._isValid = value; }
            get { return this._isValid; }
        }
        /// <summary>
        /// 操作描述
        /// </summary>
        public virtual string OperDesc
        {
            set { this._operDesc = value; }
            get { return this._operDesc; }
        }
        /// <summary>
        /// 操作英文描述
        /// </summary>
        public virtual string OperEngDesc
        {
            set { this._operEngDesc = value; }
            get { return this._operEngDesc; }
        }
        /// <summary>
        /// 是否默认操作
        /// </summary>
        public virtual bool IsDefaultOper
        {
            set { this._isDefaultOper = value; }
            get { return this._isDefaultOper; }
        }
        /// <summary>
        /// 是否系统操作
        /// </summary>
        public virtual bool IsSystem
        {
            set { this._IsSystem = value; }
            get { return this._IsSystem; }
        }
        #endregion
    }
}
