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
    public class RolePermit
    {
        #region 私有变量成员

        private string _id;                  // 主键
        private Guid _roleCode;              // 角色编码
        private Guid _permitCode;            // 权限编码
        private Guid _NodeID;            // 节点ID

        #endregion

        #region 构造函数
        public RolePermit()
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
        public virtual Guid RoleCode
        {
            set { this._roleCode = value; }
            get { return this._roleCode; }
        }
        /// <summary>
        /// 权限编码
        /// </summary>
        public virtual Guid PermitCode
        {
            set { this._permitCode = value; }
            get { return this._permitCode; }
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
