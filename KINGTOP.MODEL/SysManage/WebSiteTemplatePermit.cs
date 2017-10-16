using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年4月16日
//    功能描述： 模板全新表
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class WebSiteTemplatePermit
    {
         #region 私有变量成员
        private int _id;                        //ID，自动增长
        private int _templateid;                //模板ID
        private string _permitcode;             //操作编码
        private int _templatenodeid;            //操作编码对应的模板节点ID
        #endregion 

        #region 构造函数
        public WebSiteTemplatePermit()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// ID，自动增长
        /// </summary>
        public virtual int ID
        {
            set { _id = value; }
            get { return _id; }
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
        /// 操作编码
        /// </summary>
        public virtual string PermitCode
        {
            set { _permitcode = value; }
            get { return _permitcode; }
        }

        /// <summary>
        /// 操作编码对应的模板节点ID
        /// </summary>
        public virtual int TemplateNodeID
        {
            set { _templatenodeid = value; }
            get { return _templatenodeid; }
        }
 
        #endregion
    }
}
