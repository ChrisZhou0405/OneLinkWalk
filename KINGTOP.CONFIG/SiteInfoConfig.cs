using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 站点信息设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Config
{
    [Serializable]
    public class SiteInfoConfig : IConfigInfo
    {
        public SiteInfoConfig()
        {
        }

        #region 私有字段
        //网站信息配置
        private string _SiteName = "华强北在线"; //网站名称
        private string _SiteTitle = "华强北在线"; //网站标题
        private string _SiteURL = "http://www.360hqb.com"; //网站地址
        private string _CopyRight = " "; //版权信息
        private string _MetaKeywords = "华强北在线,网站管理系统,内容管理系统(CMS),网上商店管理系统,网站建设"; //Meta关键字
        private string _MetaDescription = "华强北在线,网站管理系统,内容管理系统(CMS),网上商店管理系统,网站建设"; //Meta描述     
        string _SiteID; //站点ID
        string _SiteDir; //站点目录  
        string _Logo;   //网站Logo
        string _favicon; //favicon.ico图标
        #endregion

        #region 属性
        /// <summary>
        /// 网站Logo
        /// </summary>
        public string Logo
        {
            get { return _Logo; }
            set { _Logo = value; }
        }

        /// <summary>
        /// favicon.ico图标
        /// </summary>
        public string Favicon
        {
            get { return _favicon; }
            set { _favicon = value; }
        }

        /// <summary>
        /// 站点目录
        /// </summary>
        public string SiteDir
        {
            get { return _SiteDir; }
            set { _SiteDir = value; }
        }
        /// <summary>
        /// 站点ID
        /// </summary>
        public string SiteID
        {
            get { return _SiteID; }
            set { _SiteID = value; }
        }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        /// <summary>
        /// 网站标题
        /// </summary>
        public string SiteTitle
        {
            get { return _SiteTitle; }
            set { _SiteTitle = value; }
        }
        /// <summary>
        /// 网站地址
        /// </summary>
        public string SiteURL
        {
            get { return _SiteURL; }
            set { _SiteURL = value; }
        }
        /// <summary>
        /// 版权信息
        /// </summary>
        public string CopyRight
        {
            get { return _CopyRight; }
            set { _CopyRight = value; }
        }
        /// <summary>
        /// Meta关键字
        /// </summary>
        public string MetaKeywords
        {
            get { return _MetaKeywords; }
            set { _MetaKeywords = value; }
        }
        /// <summary>
        /// Meta描述
        /// </summary>
        public string MetaDescription
        {
            get { return _MetaDescription; }
            set { _MetaDescription = value; }
        }        
        #endregion
    }
}
