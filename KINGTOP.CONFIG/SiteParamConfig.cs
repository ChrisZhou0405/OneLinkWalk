using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 站点参数设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Config
{
    [Serializable]
    public class SiteParamConfig : IConfigInfo
    {
        public SiteParamConfig()
        {
        }

        #region 私有字段
        //网站信息配置
        //private string _ManageDir = "admin"; //后台管理目录
        //private string _IsEnableManageCode = "0"; //是否启用后台管理认证码
        //private string _ManageCode = ""; //后台管理认证码
        private string _SiteRootURL = ""; //网站根域名
        private string _SiteDomain = ""; //本站点域名（子域名）
        private string _SiteDir = ""; //网站生成目录
        private string _CreateFileExt = "html";  //生成文件扩展名
        private string _SiteTempletDir = ""; //网站模板根目录        
        //已经移到会员配置中设置
        //private string _DisUserName = "admin|administrator|system|operator|support|root|postmaster|webmaster|security"; //禁止注册的用户名
        private string _EditControl = "CKEditor";//编辑器选择 CKEditor eWebEditor
        private string _EditorStyle = "0";   //编辑器类型 0标准，1mini型
        private string _LogType = "0";//日志记录方式 0数据库 1文件
        private string _LogDir = "C:/KingTopCMSLog/";//日志目录
        private string _siteid = "1";  //站点ID
        private string _publisType="0";  //站点发布方式 0动态、1伪静态、2纯静态
        #endregion

        #region 属性

        /// <summary>
        /// 编辑器类型
        /// </summary>
        public string EditorStyle
        {
            get { return _EditorStyle; }
            set { _EditorStyle = value; }
        }

        /// <summary>
        /// 后台管理目录
        /// </summary>
        //public string ManageDir
        //{
        //    get { return _ManageDir; }
        //    set { _ManageDir = value; }
        //}

        ///// <summary>
        ///// 是否启用后台管理认证码        ///// </summary>
        //public string IsEnableManageCode
        //{
        //    get { return _IsEnableManageCode; }
        //    set { _IsEnableManageCode = value; }
        //}
        ///// <summary>
        ///// 后台管理认证码        ///// </summary>
        //public string ManageCode
        //{
        //    get { return _ManageCode; }
        //    set { _ManageCode = value; }
        //}
        /// <summary>
        /// 网站根域名        /// </summary>
        public string SiteRootURL
        {
            get { return _SiteRootURL; }
            set { _SiteRootURL = value; }
        }

        /// <summary>
        /// 本网站域名（子域名）        /// </summary>
        public string SiteDomain
        {
            get { return _SiteDomain; }
            set { _SiteDomain = value; }
        }

        /// <summary>
        /// 网站生成目录
        /// </summary>
        public string SiteDir
        {
            get { return _SiteDir; }
            set { _SiteDir = value; }
        }
        /// <summary>
        /// 网站模板根目录
        /// </summary>
        public string SiteTempletDir
        {
            get { return _SiteTempletDir; }
            set { _SiteTempletDir = value; }
        }
        ///// <summary>
        ///// 禁止注册的用户名
        ///// </summary>
        //public string DisUserName
        //{
        //    get { return _DisUserName; }
        //    set { _DisUserName = value; }
        //}

        /// <summary>
        /// 生成文件扩展名 html，htm,shtml,shtm,aspx
        /// </summary>
        public string CreateFileExt
        {
            get { return _CreateFileExt; }
            set { _CreateFileExt = value; }
        }

        /// <summary>
        /// 编辑器选择 CKEditor eWebEditor
        /// </summary>
        public string EditControl
        {
            get { return _EditControl; }
            set { _EditControl = value; }
        }
        /// <summary>
        /// 日志记录方式 0数据库 1文件
        /// </summary>
        public string LogType
        {
            get { return _LogType; }
            set { _LogType = value; }
        }
        /// <summary>
        /// 日志目录
        /// </summary>
        public string LogDir
        {
            get { return _LogDir; }
            set { _LogDir = value; }
        }

        /// <summary>
        /// 站点目录
        /// </summary>
        public string SiteID
        {
            get { return _siteid; }
            set { _siteid = value; }
        }

        /// <summary>
        /// 站点目录
        /// </summary>
        public string PublishType
        {
            get { return _publisType; }
            set { _publisType = value; }
        }
        #endregion
    }
}
