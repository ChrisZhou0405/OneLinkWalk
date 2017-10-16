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
    public class UserRole
    {
        #region 私有变量成员

        private string _id;                  // 主键
        private string _userId;              // 用户ID
        private Guid _userGroupCode;       // 用户组编码
        private string _extentvalue;        //扩展字段

        #endregion

        #region 构造函数
        public UserRole()
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
        public virtual string UserId
        {
            set { this._userId = value; }
            get { return this._userId; }
        }
       
        /// <summary>
        /// 用户组编码
        /// </summary>
        public virtual Guid UserGroupCode
        {
            set { this._userGroupCode = value; }
            get { return this._userGroupCode; }
        }

        /// <summary>
        /// 扩展字段
        /// </summary>
        public virtual string ExtentValue
        {
            set { this._extentvalue = value; }
            get { return this._extentvalue; }
        }
        #endregion
    }
}
