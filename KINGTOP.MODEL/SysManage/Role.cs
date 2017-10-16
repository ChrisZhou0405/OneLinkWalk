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
    public class Role
    {
        #region 私有变量成员

        private Guid _roleCode;                  // 角色编码,主键
        private string _roleName;                // 角色名称
        private int _siteID;                     // 站点ID
        private int _inputID;                    // 录入人ID
        private string _inputPerson;               // 录入人
        private DateTime _inputDate;             // 录入时间
        private string _roleDescription;         // 说明
        private int _roleOrder;                  // 排序号
         #endregion

        #region 构造函数
        public Role()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public virtual Guid RoleCode
        {
            set { this._roleCode = value; }
            get { return this._roleCode; }
        }
        /// <summary>
        ///  角色名称
        /// </summary>
        public virtual string RoleName
        {
            set { this._roleName = value; }
            get { return this._roleName; }
        }
        /// <summary>
        ///  站点ID
        /// </summary>
        public virtual int SiteID
        {
            set { this._siteID = value; }
            get { return this._siteID; }
        }
        /// <summary>
        ///  录入人ID
        /// </summary>
        public virtual int InputID
        {
            set { this._inputID = value; }
            get { return this._inputID; }
        }
        /// <summary>
        ///  录入人
        /// </summary>
        public virtual string InputPerson
        {
            set { this._inputPerson = value; }
            get { return this._inputPerson; }
        }
        /// <summary>
        ///  录入时间
        /// </summary>
        public virtual DateTime InputDate
        {
            set { this._inputDate = value; }
            get { return this._inputDate; }
        }
        /// <summary>
        ///  说明
        /// </summary>
        public virtual string RoleDescription
        {
            set { this._roleDescription= value; }
            get { return this._roleDescription; }
        }
        /// <summary>
        ///  排序号
        /// </summary>
        public virtual int RoleOrder
        {
            set { this._roleOrder = value; }
            get { return this._roleOrder; }
        }
        #endregion
    }
}
