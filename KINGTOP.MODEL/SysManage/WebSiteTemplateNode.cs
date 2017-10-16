using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年4月16日
//    功能描述： 模板节点表
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class WebSiteTemplateNode
    {
        #region 私有变量成员
        private int _ID;             //主键
        private int _templateid;         //模板ID
        private string _nodecode;        //节点编码
        private string _nodename;        //节点名称
        private string _nodetype;        //节点类型
        private string _linkurl;         //自定义链接地址
        private string _parentnode;      //父节点
        private bool _isvalid;           //是否有效
        private Guid _moduleid;        //所属模块ID
        private string _nodelorder;      //排序号
        private string _nodeldesc;       //节点说明
        private string _nodelengdesc;    //节点英文说明
        private bool _issystem;          //是否系统节点
        private bool _isweb;             //是否前台栏目
        private string _ReviewFlowID;    //审核流程
        private bool _IsContainWebContent; //是否包含前台内容
        #endregion 

        #region 构造函数
        public WebSiteTemplateNode()
        { }
        #endregion

        #region 属性

        /// <summary>
        /// ID
        /// </summary>
        public virtual int ID
        {
            set { _ID = value; }
            get { return _ID; }
        }

        /// <summary>
        /// 模板ID
        /// </summary>
        public virtual int TemplateID
        {
            set { _templateid = value; }
            get { return _templateid; }
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
        /// 节点名称
        /// </summary>
        public virtual string NodeName
        {
            set { _nodename = value; }
            get { return _nodename; }
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public virtual string NodeType
        {
            set { _nodetype = value; }
            get { return _nodetype; }
        }

        /// <summary>
        /// 自定义链接地址
        /// </summary>
        public virtual string LinkURL
        {
            set { _linkurl = value; }
            get { return _linkurl; }
        }

        /// <summary>
        /// 父节点
        /// </summary>
        public virtual string ParentNode
        {
            set { _parentnode = value; }
            get { return _parentnode; }
        }       

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }

        /// <summary>
        /// 所属模块ID
        /// </summary>
        public virtual Guid ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public virtual string NodelOrder
        {
            set { _nodelorder = value; }
            get { return _nodelorder; }
        }

        /// <summary>
        /// 节点说明
        /// </summary>
        public virtual string NodelDesc
        {
            set { _nodeldesc = value; }
            get { return _nodeldesc; }
        }

        /// <summary>
        /// 节点英文说明
        /// </summary>
        public virtual string NodelEngDesc
        {
            set { _nodelengdesc = value; }
            get { return _nodelengdesc; }
        }        

        /// <summary>
        /// 是否系统节点
        /// </summary>
        public virtual bool IsSystem
        {
            set { _issystem = value; }
            get { return _issystem; }
        }

        /// <summary>
        /// 是否前台栏目
        /// </summary>
        public virtual bool IsWeb
        {
            set { _isweb = value; }
            get { return _isweb; }
        }

        /// <summary>
        /// 审核流程
        /// </summary>
        public virtual string ReviewFlowID
        {
            set { _ReviewFlowID = value; }
            get { return _ReviewFlowID; }
        }

        /// <summary>
        /// 是否包含前台内容
        /// </summary>
        public virtual bool IsContainWebContent
        {
            set { _IsContainWebContent = value; }
            get { return _IsContainWebContent; }
        }   

        

        #endregion
    }
}
