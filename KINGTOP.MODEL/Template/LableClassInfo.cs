using System;
using System.Collections.Generic;
using System.Text;

namespace KingTop.Model.Template
{
    public class LableClassInfo
    {
        public LableClassInfo()
        { }
        #region Model
        private int _classid;
        private string _classname;
        private string _description;
        private DateTime _adddate;
        private int _siteid;
        private int _issystem;
        /// <summary>
        /// 类别ID
        /// </summary>
        public int ClassID
        {
            set { _classid = value; }
            get { return _classid; }
        }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string ClassName
        {
            set { _classname = value; }
            get { return _classname; }
        }
        /// <summary>
        /// 描述
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
        /// <summary>
        /// 是否为系统分类
        /// </summary>
        public int IsSystem
        {
            set { _issystem = value; }
            get { return _issystem; }
        }
        #endregion Model
    }
}
