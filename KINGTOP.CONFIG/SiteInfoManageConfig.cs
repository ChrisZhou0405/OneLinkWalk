using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      袁纯林
//    创建时间： 2010年10月14日
//    功能描述： 站点总信息设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Config
{
    [Serializable]
    public class SiteInfoManageConfig : IConfigInfo
    {
        #region 变量
        private string _manageDir;
        private string _isEnableManageCode;
        private string _manageCode;
        private string _siteDWMange;
        private string _clientName;
        private string _clientTel;
        private string _clientEmail;
        private string _clientMobile;
        private string _clientAddr;
        private string _systemType;
        private string _systemVer;
        private string _license;
        #endregion
        
        public SiteInfoManageConfig()
        {
        }

        /// <summary>
        /// 后台管理目录
        /// </summary>
        public string ManageDir
        {
            get { return _manageDir; }
            set { _manageDir = value; }
        }

        /// <summary>
        /// 是否启用认证码
        /// </summary>
        public string IsEnableManageCode
        {
            get { return _isEnableManageCode; }
            set { _isEnableManageCode = value; }
        }

        /// <summary>
        /// 认证码
        /// </summary>
        public string ManageCode
        {
            get { return _manageCode; }
            set { _manageCode = value; }
        }

        /// <summary>
        /// 站点DW插件验证码
        /// </summary>
        public string SiteDWMange
        {
            get { return _siteDWMange; }
            set { _siteDWMange = value; }
        }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName
        {
            get { return _clientName; }
            set { _clientName = value; }
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ClientTel
        {
            get { return _clientTel; }
            set { _clientTel = value; }
        }

        /// <summary>
        /// Email
        /// </summary>
        public string ClientEmail
        {
            get { return _clientEmail; }
            set { _clientEmail = value; }
        }

        /// <summary>
        /// 联系手机
        /// </summary>
        public string ClientMobile
        {
            get { return _clientMobile; }
            set { _clientMobile = value; }
        }
        
        /// <summary>
        /// 联系地址
        /// </summary>
        public string ClientAddr
        {
            get { return _clientAddr; }
            set { _clientMobile = value; }
        }

        /// <summary>
        /// 产品
        /// </summary>
        public string SystemType
        {
            get { return _systemType; }
            set { _systemType = value; }
        }


        /// <summary>
        /// 版本
        /// </summary>
        public string SystemVer
        {
            get { return _systemVer; }
            set { _systemVer = value; }
        }

        /// <summary>
        /// License
        /// </summary>
        public string License
        {
            get { return _license; }
            set { _license = value; }
        }
    }
}
