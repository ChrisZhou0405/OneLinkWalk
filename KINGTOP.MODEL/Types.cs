
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2012 图派科技 
// 作者：阿波  abo@toprand.com 2012-07-24
// 功能描述：对K_Types表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model
{
    public class Types
    {
        #region Model

        private string _typeid;
        private string _typename;
        private string _typeparent;
        private int _orders;
        private DateTime _adddate;
        private string _menuid;
        private int _ispub;
        private string _digest;
        private string _menustring;
        private int _typeexpandint;
        private string _typeexpandchar1;
        private string _typeexpandchar2;
        private string _typeexpandchar3;

        public Types()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeParent
        {
            set { _typeparent = value; }
            get { return _typeparent; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Orders
        {
            set { _orders = value; }
            get { return _orders; }
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
        public virtual string MenuID
        {
            set { _menuid = value; }
            get { return _menuid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int IsPub
        {
            set { _ispub = value; }
            get { return _ispub; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Digest
        {
            set { _digest = value; }
            get { return _digest; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string MenuString
        {
            set { _menustring = value; }
            get { return _menustring; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int TypeExpandInt
        {
            set { _typeexpandint = value; }
            get { return _typeexpandint; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeExpandChar1
        {
            set { _typeexpandchar1 = value; }
            get { return _typeexpandchar1; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeExpandChar2
        {
            set { _typeexpandchar2 = value; }
            get { return _typeexpandchar2; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TypeExpandChar3
        {
            set { _typeexpandchar3 = value; }
            get { return _typeexpandchar3; }
        }


        #endregion Model

    }
}
