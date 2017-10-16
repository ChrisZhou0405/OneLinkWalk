
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：何伟  2010-09-01
// 功能描述：对K_TemplateSkin表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model
{
    public class TemplateSkin
    {
        #region Model

        private string _id;
        private string _projectid;
        private string _skinname;
        private string _devise;
        private string _dirct;
        private bool _isdefault;
        private bool _isdel;
        private DateTime _deltime;
        private int _siteid;
        private string _nodecode;
        private string _addman;
        private DateTime _addtime;

        public TemplateSkin()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public virtual string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ProjectID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SkinName
        {
            set { _skinname = value; }
            get { return _skinname; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Devise
        {
            set { _devise = value; }
            get { return _devise; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Dirct
        {
            set { _dirct = value; }
            get { return _dirct; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsDefault
        {
            set { _isdefault = value; }
            get { return _isdefault; }
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
        public virtual DateTime DelTime
        {
            set { _deltime = value; }
            get { return _deltime; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
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
        public virtual string AddMan
        {
            set { _addman = value; }
            get { return _addman; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }


        #endregion Model

    }
}
