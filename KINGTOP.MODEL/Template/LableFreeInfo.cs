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
//2010-09-10      胡志瑶      添加TempPrjID、IsShare、ClassId、PageSize
--===============================================================*/
#endregion
namespace KingTop.Model.Template
{
    public class LableFreeInfo
    {
        public LableFreeInfo()
        { }
        #region Model
        private int _lableid;
        private string _lablename;
        private string _labelsql;
        private string _lablecontent;
        private string _description;
        private DateTime _adddate;
        private int _siteid;
        /// <summary>
        /// 自由标签ID
        /// </summary>
        public int LableID
        {
            set { _lableid = value; }
            get { return _lableid; }
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
        /// 标签读取DB的SQL语句
        /// </summary>
        public string LabelSQL
        {
            set { _labelsql = value; }
            get { return _labelsql; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string LableContent
        {
            set { _lablecontent = value; }
            get { return _lablecontent; }
        }
        /// <summary>
        /// 描述/说明
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }
        /// <summary>
        /// 网站ID
        /// </summary>
        public int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }
        //by 胡志瑶 2010-9-9
        /// <summary>
        /// 方案ID
        /// </summary>
        public string TempPrjID
        {
            get;
            set;
        }
        //by 胡志瑶 2010-9-9
        /// <summary>
        /// 是否通用
        /// </summary>
        public int IsShare
        {
            get;
            set;
        }
        //by 胡志瑶 2010-9-9
        /// <summary>
        /// 标签类别ID
        /// </summary>
        public int ClassId
        {
            get;
            set;
        }
        //by 胡志瑶 2010-9-13
        /// <summary>
        /// 查询或分页数量
        /// </summary>
        public int PageSize
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
        #endregion 
    }
}
