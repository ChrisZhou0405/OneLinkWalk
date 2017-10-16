using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月10日
//    功能描述： 账号表
 
//    更新日期        更新人      更新原因/内容
//2010-08-26         胡志瑶       Memorandum
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    [Serializable]
    public class Account
    {      
        #region 私有成员变量
        private int _UserID;              //用户ID
        private string _UserName;      //账户名
        private string _PassWord;       //密码
        private int _Orders;        //排序
        private bool? _IsValid;            //是否有效
        private string _UserGroupCode;         //所属用户组 gavin by 2010-07-12
        #endregion

        public Account()
        { }

        #region 属性
        
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserID
        {
            set { this._UserID = value; }
            get { return this._UserID; }
        }
        /// <summary>
        /// 账户名
        /// </summary>
        public virtual string UserName
        {
            set { this._UserName = value; }
            get { return this._UserName; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PassWord
        {
            set { this._PassWord = value; }
            get { return this._PassWord; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Orders
        {
            set { this._Orders = value; }
            get { return this._Orders; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool? IsValid
        {
            set { this._IsValid = value; }
            get { return this._IsValid; }
        }

        /// <summary>
        /// 用户组 gavin by 2010-07-12
        /// </summary>
        public virtual string UserGroupCode
        {
            set { this._UserGroupCode = value; }
            get { return this._UserGroupCode; }
        }

        /// <summary>
        /// 备忘录 hzy by 2010-8-26
        /// </summary>
        public virtual string Memorandum
        {
            get;
            set;
        }
        #endregion
    }
}
