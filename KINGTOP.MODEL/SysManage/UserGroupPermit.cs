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
    public class UserGroupPermit
    {
        #region 私有变量成员

        private string _id;                  // 主键
        private Guid _userGroupCode;         // 用户组编码
        private Guid _PermitCode;            // 权限编码
        private Guid _NodeID;            // 节点ID

        #endregion

        #region 构造函数
        public UserGroupPermit()
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
        /// 角色编码
        /// </summary>
        public virtual Guid UserGroupCode
        {
            set { this._userGroupCode = value; }
            get { return this._userGroupCode; }
        }
        /// <summary>
        /// 权限编码
        /// </summary>
        public virtual Guid PermitCode
        {
            set { this._PermitCode = value; }
            get { return this._PermitCode; }
        }
        /// <summary>
        /// 节点ID
        /// </summary>
        public virtual Guid NodeID
        {
            set { this._NodeID = value; }
            get { return this._NodeID; }
        }
        #endregion
    }
}
