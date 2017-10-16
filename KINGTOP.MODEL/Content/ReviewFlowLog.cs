
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武 ycl@360hqb.com 2010-03-31
// 功能描述：对K_ReviewFlowLog表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model.Content
{
    public class ReviewFlowLog
    {
        #region Model

        private string _id;
        private int _orders;
        private bool _isdel;
        private DateTime _adddate;
        private string _addman;
        private bool _isenable;
        private string _desc;
        private string _notation;
        private string _ip;
        private string _nodeid;
        private string _siteid;
        private string _newsid;
        private string _modeid;
        private bool _issuccess;

        public ReviewFlowLog()
        {

        }


        /// <summary>
        /// ID
        /// </summary>
        public virtual string ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Orders
        {
            set { _orders = value; }
            get { return _orders; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public virtual DateTime AddDate
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
        /// 状态
        /// </summary>
        public virtual bool IsEnable
        {
            set { _isenable = value; }
            get { return _isenable; }
        }

        /// <summary>
        /// 是否通过
        /// </summary>
        public virtual bool IsSuccess
        {
            set { _issuccess = value; }
            get { return _issuccess; }
        }

        /// <summary>
        /// 操作步骤描述
        /// </summary>
        public virtual string Desc
        {
            set { _desc = value; }
            get { return _desc; }
        }

        /// <summary>
        /// 批注
        /// </summary>
        public virtual string Notation
        {
            set { _notation = value; }
            get { return _notation; }
        }

        /// <summary>
        /// 客户IP
        /// </summary>
        public virtual string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public virtual string NodeId
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }

        /// <summary>
        /// 站点ID
        /// </summary>
        public virtual string SiteId
        {
            set { _siteid = value; }
            get { return _siteid; }
        }

        /// <summary>
        /// 新闻Id
        /// </summary>
        public virtual string NewsId
        {
            set { _newsid = value; }
            get { return _newsid; }
        }

        /// <summary>
        /// 模型ID
        /// </summary>
        public virtual string ModeId
        {
            set { _modeid = value; }
            get { return _modeid; }
        }


        #endregion Model

    }
}
