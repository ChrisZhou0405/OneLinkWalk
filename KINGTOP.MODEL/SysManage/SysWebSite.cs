using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年4月16日
//    功能描述： 站点表模型
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class SysWebSite
    {
        #region 私有变量成员
        private int _siteid;               //站点ID
        private string _sitename;          //站点名称
        private string _directory;         //所属文件夹
        private string _siteurl;           //站点URL
        private bool _ismainsite;          //是否主站点
        private string _settingsxml;       //站点设置
        #endregion 

        #region 构造函数
        public SysWebSite()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 站点ID
        /// </summary>
        public virtual int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }

        /// <summary>
        /// 站点名称
        /// </summary>
        public virtual string SiteName
        {
            set { _sitename = value; }
            get { return _sitename; }
        }

        /// <summary>
        /// 所属文件夹
        /// </summary>
        public virtual string Directory
        {
            set { _directory = value; }
            get { return _directory; }
        }

        /// <summary>
        /// 站点URL
        /// </summary>
        public virtual string SiteUrl
        {
            set { _siteurl = value; }
            get { return _siteurl; }
        }

        /// <summary>
        /// 是否主站点
        /// </summary>
        public virtual bool IsMainSite
        {
            set { _ismainsite = value; }
            get { return _ismainsite; }
        }      

        /// <summary>
        /// 站点设置
        /// </summary>
        public virtual string SettingsXML
        {
            set { _settingsxml = value; }
            get { return _settingsxml; }
        }
 
        #endregion
    }
}
