using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年4月16日
//    功能描述： 模板主表
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class WebSiteTemplate
    {
        #region 私有变量成员
        private int _templateid;           //模板ID
        private string _templatename;      //模板名称
        #endregion 

        #region 构造函数
        public WebSiteTemplate()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 模板ID
        /// </summary>
        public virtual int TemplateID
        {
            set { _templateid = value; }
            get { return _templateid; }
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public virtual string TemplateName
        {
            set { _templatename = value; }
            get { return _templatename; }
        }
        #endregion
    }
}
