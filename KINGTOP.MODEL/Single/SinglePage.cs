
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：严辉 2010-05-25
// 功能描述：对K_SinglePage表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model.Single
{
    public class SinglePage
    {
        #region Model

        private string _id;
        private string _title;
        private string _content;
        private int _classid;
        private int _ispub;
        private DateTime? _adddate;
        private string _addman;
        private string _nodecode;
        private int _siteid;

        public SinglePage()
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
        /// 标题
        /// </summary>
        public virtual string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Content
        {
            set { _content = value; }
            get { return _content; }
        }

        /// <summary>
        /// 分类 ID
        /// </summary>
        public virtual int ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }

        /// <summary>
        /// 是否发布
        /// </summary>
        public virtual int IsPub
        {
            set { _ispub = value; }
            get { return _ispub; }
        }

        /// <summary>
        /// 添加日期
        /// </summary>
        public virtual DateTime? AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

        /// <summary>
        /// 添加人
        /// </summary>
        public virtual string AddMan
        {
            set { _addman = value; }
            get { return _addman; }
        }

        /// <summary>
        /// 节点编码
        /// </summary>
        public virtual string NodeCode
        {
            set { _nodecode = value; }
            get { return _nodecode; }
        }

        /// <summary>
        /// 站点 ID
        /// </summary>
        public virtual int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }


        #endregion Model

    }
}
