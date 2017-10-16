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
    public class UserGroup
    {
        #region 私有成员变量
        private int _id;                //主键
        private Guid _userGroupCode;    //用户组编码
        private string _userGroupName;  //用户组名称
        private int _siteID;            //站点ID
        private bool _isParent;         //所属角色编码
        private string _numCode;        //用户组CODE
        private string _parentNumCode;  //父用户组Code
        private int _inputID;           //录入人ID
        private string _inputPerson;    //录入人
        private DateTime _inputDate;    //录入时间
        private string _userGroupRemark;//用户组说明
        private int _userGroupOrder;    //排序号
        #endregion

        #region 构造函数
        public UserGroup()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 用户组编码，主键
        /// </summary>
        public virtual int ID
        {
            set { this._id = value; }
            get { return this._id; }
        }

        public virtual string NumCode
        {
            set { this._numCode = value; }
            get { return this._numCode; }
        }

        public virtual string ParentNumCode
        {
            set { this._parentNumCode = value; }
            get { return this._parentNumCode; }
        }

        public virtual Guid UserGroupCode
        {
            set { this._userGroupCode = value; }
            get { return this._userGroupCode; }
        }
        /// 用户组名称
        public virtual string UserGroupName
        {
            set { this._userGroupName = value; }
            get { return this._userGroupName; }
        }
        /// <summary>
        ///  站点ID
        /// </summary>
        public virtual int SiteID
        {
            set { this._siteID = value; }
            get { return this._siteID; }
        }
        //所属角色编码
        public virtual bool IsParent
        {
            set { this._isParent = value; }
            get { return this._isParent; }
        }
        //录入人ID
        public virtual int InputID
        {
            set { this._inputID = value; }
            get { return this._inputID; }
        }
        //录入人
        public virtual string InputPerson
        {
            set { this._inputPerson = value; }
            get { return this._inputPerson; }
        }
        //录入时间
        public virtual DateTime InputDate
        {
            set { this._inputDate = value; }
            get { return this._inputDate; }
        }
        //用户组说明
        public virtual string UserGroupRemark
        {
            set { this._userGroupRemark = value; }
            get { return this._userGroupRemark; }
        }
        //排序号
        public virtual int UserGroupOrder
        {
            set { this._userGroupOrder = value; }
            get { return this._userGroupOrder; }
        }
        #endregion
    }
}
