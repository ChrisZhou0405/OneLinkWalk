
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：何伟 2010-09-01
// 功能描述：对K_TemplateProject表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model
{
    public class TemplateProject
    {
        #region Model

        private string _id;
        private string _title;
        private string _directory;
        private string _devise;
        private int _width;
        private string _intro;
        private bool _isdefault;
        private string _thumbnail;
        private bool _isdel;
        private DateTime _detime;
        private string _nodecode;
        private int _siteid;
        private string _addman;
        private DateTime _addtime;

        public TemplateProject()
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
        public virtual string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Directory
        {
            set { _directory = value; }
            get { return _directory; }
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
        public virtual int Width
        {
            set { _width = value; }
            get { return _width; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Intro
        {
            set { _intro = value; }
            get { return _intro; }
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
        public virtual string Thumbnail
        {
            set { _thumbnail = value; }
            get { return _thumbnail; }
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
        public virtual DateTime DeTime
        {
            set { _detime = value; }
            get { return _detime; }
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
        public virtual int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
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
