using System;
using System.Collections.Generic;
using System.Text;
#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     
    创建时间： 
    功能描述： 标签管理/标签列表
 
// 更新日期        更新人      更新原因/内容
//2010-09-10      胡志瑶      添加TempPrjID
--===============================================================*/
#endregion
namespace KingTop.Model.Template
{

    /// <summary>
    /// 标签参数
    /// </summary>
    public struct LabelParameter
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string LPName;
        /// <summary>
        /// 参数值
        /// </summary>
        public string LPValue;
    }
    /// <summary>
    /// 标签分类
    /// </summary>
    public enum LabelType { Free, Custom, Class, Channel }
    /// <summary>
    /// 标签类
    /// </summary>
    /// 

    //实体类K_T_Lable 。(属性说明自动提取数据库字段的描述信息)
    public class LableInfo
    {
        public LableInfo()
        { }
        public LableInfo(string lableName)
        {
            this.LableName = lableName;
        }
        #region Model
        private int _lableid;
        private int _classid;
        private string _lablename;
        private string _lablecontent;
        private string _description;
        private int _issystem;
        private int _siteid;
        private int _isshare;
        /// <summary>
        /// 标签ID
        /// </summary>
        public int LableID
        {
            set { _lableid = value; }
            get { return _lableid; }
        }
        /// <summary>
        /// 标签类别ID
        /// </summary>
        public int ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LableName
        {
            set { _lablename = value; }
            get { return _lablename; }
        }
        /// <summary>
        /// 标签内容
        /// </summary>
        public string LableContent
        {
            set { _lablecontent = value; }
            get { return _lablecontent; }
        }
        /// <summary>
        /// 标签说明/描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 是否为系统标签,系统标签不能删除0-否  1-是
        /// </summary>
        public int IsSystem
        {
            set { _issystem = value; }
            get { return _issystem; }
        }
        /// <summary>
        /// 网站ID
        /// </summary>
        public int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }
        /// <summary>
        /// 是否共享标签0否 1是
        /// </summary>
        public int IsShare
        {
            set { _isshare = value; }
            get { return _isshare; }
        }
        #endregion Model

        //by 胡志瑶 2010-9-8
        /// <summary>
        /// 方案ID
        /// </summary>
        public string TempPrjID
        {
            get;
            set;
        }
        /// <summary>
        /// 标签标识
        /// </summary>
        public int Identification
        {
            get;
            set;
        }
        /// <summary>
        /// 标签名
        /// </summary>
        public string Title
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sequence
        {
            get;
            set;
        }
    }
}
