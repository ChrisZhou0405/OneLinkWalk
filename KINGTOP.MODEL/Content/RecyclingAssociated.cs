
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武 2010-08-02
// 功能描述：对K_RecyclingAssociated表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model.Content
{
    public class RecyclingAssociated
    {
        #region Model

        private int _id;
        private int _recyclingmanageid;
        private int _associatedid;
        private string _associatedkey;
        private string _associatedwhere;
        private DateTime _adddate;
        private string _addman;
        private bool _isdel;
        private bool _KeyIsWhere;
        public RecyclingAssociated()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public virtual int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int RecyclingManageID
        {
            set { _recyclingmanageid = value; }
            get { return _recyclingmanageid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int AssociatedID
        {
            set { _associatedid = value; }
            get { return _associatedid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string AssociatedKey
        {
            set { _associatedkey = value; }
            get { return _associatedkey; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string AssociatedWhere
        {
            set { _associatedwhere = value; }
            get { return _associatedwhere; }
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

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool KeyIsWhere
        {
            set { _KeyIsWhere = value; }
            get { return _KeyIsWhere; }
        }


        #endregion Model

    }
}
