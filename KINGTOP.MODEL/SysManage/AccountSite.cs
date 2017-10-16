using System;
using System.Collections.Generic;
using System.Text;
#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线

 
//    更新日期        更新人      更新原因/内容
//2010-08-26         胡志瑶       添加Position
===========================================================================*/
#endregion
namespace KingTop.Model.SysManage
{
    public class AccountSite
    {
        #region 私有成员变量
        private int _id;
        private int _userID;              //用户ID
        private int _SiteID;              //站点ID
        private bool _isValid;            //是否有效
        private DateTime _LoginDate;      //最后登录日期
        private string _IP;                //IP
        private int _LoginCount;           //登录次数
        private DateTime? _LastLoginDate=null;   //上次登录日期
        private string _LastLoginIP;        //上次登录IP
        #endregion

        public AccountSite()
        { }

        #region 属性
        /// <summary>
        /// ID
        /// </summary>
        public virtual int ID
        {
            set { this._id = value; }
            get { return this._id; }
        }
        /// <summary>
        /// 站点ID
        /// </summary>
        public virtual int SiteID
        {
            set { this._SiteID = value; }
            get { return this._SiteID; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserID
        {
            set { this._userID = value; }
            get { return this._userID; }
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
        /// 登录日期
        /// </summary>
        public virtual DateTime LoginDate
        {
            set { this._LoginDate = value; }
            get { return this._LoginDate; }
        }
        /// <summary>
        /// 上次登录日期
        /// </summary>
        public virtual DateTime? LastLoginDate
        {
            set { this._LastLoginDate = value; }
            get { return this._LastLoginDate; }
        }
        /// <summary>
        /// IP
        /// </summary>
        public virtual string IP
        {
            set { this._IP = value; }
            get { return this._IP; }
        }
        /// <summary>
        /// 上次登录IP
        /// </summary>
        public virtual string LastLoginIP
        {
            set { this._LastLoginIP = value; }
            get { return this._LastLoginIP; }
        }
        /// <summary>
        /// 登录次数
        /// </summary>
        public virtual int LoginCount
        {
            set { this._LoginCount = value; }
            get { return this._LoginCount; }
        }
        #endregion

        /// <summary>
        /// 工作台位置信息
        /// </summary>
        public virtual string PoSition
        {
            get;
            set;

        }
    }
}
