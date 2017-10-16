using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingTop.Model.Common
{
    /// <summary>
    /// 通用模型
    /// </summary>
    public class Common
    {        
        private string _id;
        private string _title;
        private string _nodeCode;        
        private string _userName;
        private string _bigImg;
        private string _addDate;
        private string _flowState;
        private int _orders;
        private int _siteID;
        private string _detail;
                
        /// <summary>
        /// 字段ID
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// 字段标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// 栏目NodeCode
        /// </summary>
        public string NodeCode
        {
            get { return _nodeCode; }
            set { _nodeCode = value; }
        }

        /// <summary>
        /// 字段用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// 字段大图
        /// </summary>
        public string BigImg
        {
            get { return _bigImg; }
            set { _bigImg = value; }
        }

        /// <summary>
        /// 字段添加时间
        /// </summary>
        public string AddDate
        {
            get { return _addDate; }
            set { _addDate = value; }
        }

        /// <summary>
        /// 字段审核状态
        /// </summary>
        public string FlowState
        {
            get { return _flowState; }
            set { _flowState = value; }
        }

        /// <summary>
        /// 字段排序数字
        /// </summary>
        public int Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

        /// <summary>
        /// 站点ID
        /// </summary>
        public int SiteID
        {
            get { return _siteID; }
            set { _siteID = value; }
        }

        /// <summary>
        /// 详细内容
        /// </summary>
        public string Detail
        {
            get { return _detail; }
            set { _detail = value; }
        }
    }
}
