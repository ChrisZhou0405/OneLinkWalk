using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 邮件设置类
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Config
{
    [Serializable]
    public class PostConfig : IConfigInfo
    {
        public PostConfig()
        {
        }

        #region 私有字段
        //邮件参数配置
        private string _Email = "someone@360hqb.com"; //发送人邮箱
        private string _Password = ""; //发送人邮箱密码
        private string _SmtpServer = "smtp.360hqb.com"; //SMTP服务器
        private string _Port = "25"; //端口号
        #endregion

        #region 属性
        #region 邮件参数配置
        /// <summary>
        /// 发送人邮箱
        /// </summary>
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        /// <summary>
        /// 发送人邮箱密码
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        /// <summary>
        /// SMTP服务器
        /// </summary>
        public string SmtpServer
        {
            get { return _SmtpServer; }
            set { _SmtpServer = value; }
        }
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port
        {
            get { return _Port; }
            set { _Port = value; }
        }
        #endregion
        #endregion
    }
}
