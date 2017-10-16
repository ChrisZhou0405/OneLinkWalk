
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武 2010-08-02
// 功能描述：对K_RecyclingManage表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model.Content
{
    public class RecyclingManage
    {
        #region Model

        private int _id;
        private string _pkey;
        private string _delkey;
        private string _titlekey;
        private string _deltimekey;
        private string _nodecode;
        private bool _ismanage;
        private bool _isreductive;
        private string _listurl;
        private string _desc;
        private bool _isprocdel;
        private string _procdeltext;
        private bool _isprocreductive;
        private string _procreductivetext;
        private DateTime _adddate;
        private string _addman;
        private string _tableName;
        private bool _isondesc;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        public RecyclingManage()
        {

        }

        /// <summary>
        /// IsOnDesc
        /// </summary>
        public virtual bool IsOnDesc
        {
            set { _isondesc = value; }
            get { return _isondesc; }
        }



        /// <summary>
        /// ID
        /// </summary>
        public virtual int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string pKey
        {
            set { _pkey = value; }
            get { return _pkey; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DelKey
        {
            set { _delkey = value; }
            get { return _delkey; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TitleKey
        {
            set { _titlekey = value; }
            get { return _titlekey; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DelTimeKey
        {
            set { _deltimekey = value; }
            get { return _deltimekey; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string NodeCode
        {
            set { _nodecode = value; }
            get { return _nodecode; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsManage
        {
            set { _ismanage = value; }
            get { return _ismanage; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsReductive
        {
            set { _isreductive = value; }
            get { return _isreductive; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ListUrl
        {
            set { _listurl = value; }
            get { return _listurl; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Desc
        {
            set { _desc = value; }
            get { return _desc; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsProcDel
        {
            set { _isprocdel = value; }
            get { return _isprocdel; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ProcDelText
        {
            set { _procdeltext = value; }
            get { return _procdeltext; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsProcReductive
        {
            set { _isprocreductive = value; }
            get { return _isprocreductive; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ProcReductiveText
        {
            set { _procreductivetext = value; }
            get { return _procreductivetext; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string AddMan
        {
            set { _addman = value; }
            get { return _addman; }
        }


        #endregion Model

    }
}
