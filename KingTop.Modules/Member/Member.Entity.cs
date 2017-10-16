using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：严辉 2010-04-28
// 功能描述：对K_Member表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Modules.Entity
{
    [Serializable]
    public class Member
    {
        #region Model

        private string _memberid;
        private string _username;
        private string _gender;
        private string _password;
        private string _groupid;
        private string _modleid;
        private string _email;
        private float _funds;
        private int _point;
        private int _integral;
        private int _stateid;
        private int _ischeck;
        private int _isemailvalid;
        private int _isdel;
        private string _regitip;
        private DateTime? _regitdate;
        private string _lastloginip;
        private DateTime? _lastlogindate;
        private int _logintimes;
        private int _siteID;
        private string _nodeCode;
        private int _bestanswercount;
        private string _headimg;
        private string _token;
        private string _pwdve;
        private string _realname;
        private DateTime? _birthday;
        private string _mobile;
        private int _marriage;
        private string _intro;

        public string Pwdve
        {
            get { return _pwdve; }
            set { _pwdve = value; }
        }

        public string Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public Member()
        {

        }


        /// <summary>
        /// 会员 ID
        /// </summary>
        public virtual string MemberID
        {
            set { _memberid = value; }
            get { return _memberid; }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string Gender
        {
            set { _gender = value; }
            get { return _gender; }
        }

        /// <summary>
        /// 用户名

        /// </summary>
        public virtual string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password
        {
            set { _password = value; }
            get { return _password; }
        }

        /// <summary>
        /// 会员组 ID
        /// </summary>
        public virtual string GroupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }

        /// <summary>
        /// 会员模型 ID
        /// </summary>
        public virtual string ModleID
        {
            set { _modleid = value; }
            get { return _modleid; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email
        {
            set { _email = value; }
            get { return _email; }
        }

        /// <summary>
        /// 资金
        /// </summary>
        public virtual float Funds
        {
            set { _funds = value; }
            get { return _funds; }
        }

        /// <summary>
        /// 点数
        /// </summary>
        public virtual int Point
        {
            set { _point = value; }
            get { return _point; }
        }

        /// <summary>
        /// 积分
        /// </summary>
        public virtual int Integral
        {
            set { _integral = value; }
            get { return _integral; }
        }

        /// <summary>
        /// 会员状态ID(1/正常，2/锁定)
        /// </summary>
        public virtual int StateID
        {
            set { _stateid = value; }
            get { return _stateid; }
        }

        /// <summary>
        /// 是否审核
        /// </summary>
        public virtual int IsCheck
        {
            set { _ischeck = value; }
            get { return _ischeck; }
        }

        /// <summary>
        /// 是否邮箱验证
        /// </summary>
        public virtual int IsEmailValid
        {
            set { _isemailvalid = value; }
            get { return _isemailvalid; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }

        /// <summary>
        /// 注册 IP
        /// </summary>
        public virtual string RegitIP
        {
            set { _regitip = value; }
            get { return _regitip; }
        }

        /// <summary>
        /// 注册日期
        /// </summary>
        public virtual DateTime? RegitDate
        {
            set { _regitdate = value; }
            get { return _regitdate; }
        }

        /// <summary>
        /// 最后登录 IP
        /// </summary>
        public virtual string LastLoginIP
        {
            set { _lastloginip = value; }
            get { return _lastloginip; }
        }

        /// <summary>
        /// 最后登录时间

        /// </summary>
        public virtual DateTime? LastLoginDate
        {
            set { _lastlogindate = value; }
            get { return _lastlogindate; }
        }

        /// <summary>
        /// 登录次数
        /// </summary>
        public virtual int LoginTimes
        {
            set { _logintimes = value; }
            get { return _logintimes; }
        }

        /// <summary>
        /// 站点 ID
        /// </summary>
        public virtual int SiteID
        {
            set { _siteID = value; }
            get { return _siteID; }
        }

        /// <summary>
        /// 节点编码
        /// </summary>
        public virtual string NodeCode
        {
            set { _nodeCode = value; }
            get { return _nodeCode; }
        }
        /// <summary>
        /// 知道里面 帮助解决问题的数量

        /// </summary>
        public virtual int BestAnswerCount
        {
            set { _bestanswercount = value; }
            get { return _bestanswercount; }
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public virtual string HeadImg
        {
            set { _headimg = value; }
            get { return _headimg; }
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName
        {
            get { return _realname; }
            set { _realname = value; }
        }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public int Marriage
        {
            get { return _marriage; }
            set { _marriage = value; }
        }

        /// <summary>
        /// 自我介绍
        /// </summary>
        public string Intro
        {
            get { return _intro; }
            set { _intro = value; }
        }
        #endregion Model
    }
}
