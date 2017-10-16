
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武
// 创建日期：2010-03-26
// 功能描述：对K_ReviewFlow表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.Model.Content
{
    public class ReviewFlow
    {
        #region Model

        private string _id;
        private int _orders;
        private bool _isdel;
        private DateTime _adddate;
        private string _addman;
        private bool _isenable;
        private string _name;
        private string _desc;
        private string _type;
        private string _siteid;
        private string _nodeid;
        private string _nodecode;

        public ReviewFlow()
        {

        }



        /// <summary>
        /// NodeCode
        /// </summary>
        public virtual string NodeCode
        {
            set { _nodecode = value; }
            get { return _nodecode; }
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
        /// Orders
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
        /// 名称
        /// </summary>
        public virtual string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Desc
        {
            set { _desc = value; }
            get { return _desc; }
        }

        /// <summary>
        /// 方案类型
        /// </summary>
        public virtual string Type
        {
            set { _type = value; }
            get { return _type; }
        }

        /// <summary>
        /// 站点id
        /// </summary>
        public virtual string SiteId
        {
            set { _siteid = value; }
            get { return _siteid; }
        }

        /// <summary>
        /// 栏目ID
        /// </summary>
        public virtual string NodeId
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }


        #endregion Model

    }
}
