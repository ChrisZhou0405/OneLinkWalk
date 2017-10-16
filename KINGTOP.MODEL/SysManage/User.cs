using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      肖丹 
//    创建时间： 2010年3月23日
//    功能描述： 作者模型
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
     public class User
    {
        #region 私有成员变量
        private int _userID;            //用户ID,主键
        private string _trueName;       //姓名
        private string _sex;            //性别
        private DateTime _birthday;     //出生日期
        private string _nation;         //民族
        private string _nativePlace;    //籍贯
        private string _marriageState;  //婚否
        private string _homeAddress;    //家庭地址
        private string _currentAddress; //现住地址
        private string _officePhone;    //办公室电话
        private string _extension;      //分机
        private string _mobile;         //手机
        private string _homePhone;      //家庭电话
        private string _fax;            //传真
        private string _email;
        private int _inputID;           //录入人ID
        private string _inputPerson;    //录入人
        private DateTime _inputDate;    //录入时间
        private string _remark;         //备注
        #endregion

        #region 构造函数
        public User()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 用户ID，主键
        /// </summary>
        public virtual int UserID
        {
            set { this._userID = value; }
            get { return this._userID; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string TrueName
        {
            set { this._trueName = value; }
            get { return this._trueName; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Sex
        {
            set { this._sex = value; }
            get { return this._sex; }
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        public virtual DateTime Birthday
        {
            set { this._birthday = value; }
            get { return this._birthday; }
        }
        /// <summary>
        /// 民族
        /// </summary>
        public virtual string Nation
        {
            set { this._nation = value; }
            get { return this._nation; }
        }
        /// <summary>
        /// 籍贯
        /// </summary>
        public virtual string NativePlace
        {
            set { this._nativePlace = value; }
            get { return this._nativePlace; }
        }
        /// <summary>
        /// 婚否
        /// </summary>
        public virtual string MarriageState
        {
            set { this._marriageState = value; }
            get { return this._marriageState; }
        }
         /// <summary>
        /// 家庭地址
        /// </summary>
        public virtual string HomeAddress
        {
            set { this._homeAddress = value; }
            get { return this._homeAddress; }
        }
          /// <summary>
        /// 现住地址
        /// </summary>
        public virtual string CurrentAddress
        {
            set { this._currentAddress = value; }
            get { return this._currentAddress; }
        }
          /// <summary>
        /// 办公电话
        /// </summary>
        public virtual string OfficePhone
        {
            set { this._officePhone = value; }
            get { return this._officePhone; }
        }
        /// <summary>
        /// 分机号
        /// </summary>
        public virtual string Extension
        {
            set { this._extension = value; }
            get { return this._extension; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public virtual string Mobile
        {
            set { this._mobile = value; }
            get { return this._mobile; }
        }
        /// <summary>
        /// 家庭电话
        /// </summary>
        public virtual string HomePhone
        {
            set { this._homePhone = value; }
            get { return this._homePhone; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public virtual string Fax
        {
            set { this._fax = value; }
            get { return this._fax; }
        }
        /// <summary>
        /// Email
        /// </summary>
        public virtual string Email
        {
            set { this._email = value; }
            get { return this._email; }
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
            get { return this.InputPerson; }
        }
        //录入时间
        public virtual DateTime InputDate
        {
            set { this._inputDate = value; }
            get { return this._inputDate; }
        }
        /// <summary>
        /// remark
        /// </summary>
        public virtual string Remark
        {
            set { this._remark = value; }
            get { return this._remark; }
        }
        #endregion
    }
}
