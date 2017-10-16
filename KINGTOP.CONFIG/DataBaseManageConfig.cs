using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年7月15日
//    功能描述： 数据库管理参数设置 
//    更新日期        更新人      更新原因/内容
===========================================================================*/
#endregion

namespace KingTop.Config
{
    [Serializable]
    public class DataBaseManageConfig : IConfigInfo
    {
        public DataBaseManageConfig()
        {             
        }

        #region 私有字段
        //网站信息配置
        private string _Server = "192.168.10.34\\SQL2008"; //sql服务器地址
        private string _UserName = "sa";//数据库连接用户名
        private string _Password = "sa";//数据库连接密码
        private string _BakFilePath = @"d:\web\dbbackup";//备份bak文件所在数据库服务器文件夹路径
        private string _SqlFilePath = @"d:\web\dbbackup";//备份sql文件所在网站服务器文件夹路径
        private string _DataBackFileList = "";//备份文件列表
        private string _DataBase = "KINGTOPCMSDB";//要备份的数据库名称
        private int _IsSameServer = 1;//数据库和网站程序是否同一台机器 0=否，1=是
        private int _IsUseOtherUser = 0;//是否其他用户进行还原数据库 0=否，1=是
        
        #endregion

        #region 属性
        /// <summary>
        /// sql服务器地址
        /// </summary>
        public string Server
        {
            get { return _Server; }
            set { _Server = value; }
        }
        /// <summary>
        /// 数据库连接用户名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        /// <summary>
        /// 数据库连接密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        /// <summary>
        /// 备份bak文件所在数据库服务器文件夹路径
        /// </summary>
        public string BakFilePath
        {
            get { return _BakFilePath; }
            set { _BakFilePath = value; }
        }
        /// <summary>
        /// 备份sql文件所在网站服务器文件夹路径
        /// </summary>
        public string SqlFilePath
        {
            get { return _SqlFilePath; }
            set { _SqlFilePath = value; }
        }
        /// <summary>
        /// 备份文件列表
        /// </summary>
        public string DataBackFileList
        {
            get { return _DataBackFileList; }
            set { _DataBackFileList = value; }
        }
        /// <summary>
        /// 要备份的数据库名称
        /// </summary>
        public string DataBase
        {
            get { return _DataBase; }
            set { _DataBase = value; }
        }

        /// <summary>
        /// 数据库和网站程序是否同一台机器 1=是，0=否
        /// </summary>
        public int IsSameServer
        {
            get { return _IsSameServer; }
            set { _IsSameServer = value; }
        }

        /// <summary>
        /// 是否启用其他用户进行还原数据库
        /// </summary> 
        public int IsUseOtherUser
        {
            get { return _IsUseOtherUser; }
            set { _IsUseOtherUser = value; }
        }
        #endregion
    }
}
