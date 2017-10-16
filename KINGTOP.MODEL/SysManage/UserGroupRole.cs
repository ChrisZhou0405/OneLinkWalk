using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      肖丹 
//    创建时间： 2010年4月16日
//    功能描述： 用户组角色 模型
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class UserGroupRole
    {
        #region 私有变量成员
        private string _id;                  // 主键
        private Guid _userGroupCode;         // 用户组编码
        private Guid _roleCode;              // 用户ID

        #endregion

        #region 构造函数
        public UserGroupRole()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string ID
        {
            set { this._id = value; }
            get { return this._id; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual Guid RoleCode
        {
            set { this._roleCode = value; }
            get { return this._roleCode; }
        }
       
        /// <summary>
        /// 用户组编码
        /// </summary>
        public virtual Guid UserGroupCode
        {
            set { this._userGroupCode = value; }
            get { return this._userGroupCode; }
        }
        #endregion
    }
}
