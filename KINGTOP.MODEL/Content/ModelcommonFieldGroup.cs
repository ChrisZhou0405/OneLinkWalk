
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：袁纯林 ycl@360hqb.com 2010-08-30
// 功能描述：对k_ModelcommonFieldGroup表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model.Content
{
    public class ModelcommonFieldGroup
    {
        #region Model

        private string _id;
        private string _name;
        private int _orders;
        private string _addman;
        private bool _isdel;

        public ModelcommonFieldGroup()
        {

        }


        /// <summary>
        /// 表主键
        /// </summary>
        public virtual string ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 操作人
        /// </summary>
        public virtual string AddMan
        {
            set { _addman = value; }
            get { return _addman; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual bool IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }


        #endregion Model

    }
}
