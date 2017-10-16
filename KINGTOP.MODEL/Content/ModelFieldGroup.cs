
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武
// 创建日期：2010-03-10
// 功能描述：对K_ModelFieldGroup表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.Model.Content
{
    public class ModelFieldGroup
    {
        #region Model

        private string _id;
        private string _modelid;
        private string _name;
        private int _orders;
        private bool _isenable;
        private bool _adddate;

        public ModelFieldGroup()
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
        /// 所属模型
        /// </summary>
        public virtual string ModelId
        {
            set { _modelid = value; }
            get { return _modelid; }
        }

        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string Name
        {
            set { _name = value; }
            get { return _name; }
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
        /// 是否启用
        /// </summary>
        public virtual bool IsEnable
        {
            set { _isenable = value; }
            get { return _isenable; }
        }

        /// <summary>
        /// 添加时间
        /// </summary>
        public virtual bool AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }


        #endregion Model

    }
}
