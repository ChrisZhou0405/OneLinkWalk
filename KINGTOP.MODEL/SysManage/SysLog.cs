
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：袁纯林 ycl@KingTop.com
// 创建日期：2010-03-17
// 功能描述：对K_ManageLog表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Model.SysManage
{
    public class SysLog
    {
        #region Model

        private string _nodecode;
        private int _siteid;
        private string _content;
        private string _userno;
        private string _ip;
        private string _scriptname;
        private string _postcontent;
        private int _logtype;

        public SysLog()
        {

        }

        /// <summary>
        /// 栏目编码
        /// </summary>
        public virtual string NodeCode
        {
            set { _nodecode = value; }
            get { return _nodecode; }
        }

        /// <summary>
        /// 所属站点
        /// </summary>
        public virtual int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public virtual string Content
        {
            set { _content = value; }
            get { return _content; }
        }

        /// <summary>
        /// 操作人员
        /// </summary>
        public virtual string UserNo
        {
            set { _userno = value; }
            get { return _userno; }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public virtual string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }

        /// <summary>
        /// 操作页面
        /// </summary>
        public virtual string ScriptName
        {
            set { _scriptname = value; }
            get { return _scriptname; }
        }

        /// <summary>
        /// 提交内容
        /// </summary>
        public virtual string PostContent
        {
            set { _postcontent = value; }
            get { return _postcontent; }
        }

        /// <summary>
        /// 日志类型，1=登录日志，2=操作日志，3=错误日志
        /// </summary>
        public virtual int LogType
        {
            set { _logtype = value; }
            get { return _logtype; }
        }


        #endregion Model

    }
}
