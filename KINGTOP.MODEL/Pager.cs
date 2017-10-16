using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Wuqi.Webdiyer;
using System.Data;

namespace KingTop.Model
{
    public class Pager
    {
        #region 参数定义
        protected int _recordCount = 0;		//记录数
        protected int _pageIndex = 0;		//当前页索引
        protected int _pageSize = 0;			//每页大小
        protected string _pkField = "*";		//索引字段
        protected string _order = "Id desc";		//排序 
        protected string _searField = "*";
        private AspNetPager aspnetpage;      //aspnetpage控件
        private System.Web.UI.WebControls.Repeater rptControls;      //rpt控件
        Dictionary<string, string> dicWhere = new Dictionary<string, string>();
        private int _pageindex=1;

        public Pager()
        {
            dicWhere.Add("", "");
        }

        public void PageData(DataTable dt)
        {
            _pageSize = aspnetpage.PageSize;
            rptControls.DataSource = dt;
            rptControls.DataBind();
            aspnetpage.RecordCount = _recordCount;
            aspnetpage.CurrentPageIndex = PageIndex;
            _pageIndex = aspnetpage.CurrentPageIndex;
        }
        #endregion

        #region 属性

        // <summary>
        /// 查询条件：字段名、值
        /// </summary>
        public Dictionary<string,string> DicWhere
        {
            get { return dicWhere; }
            set { dicWhere = value; }
        }

        

        /// <summary>
        /// aspnetpage控件
        /// </summary>
        public AspNetPager Aspnetpage
        {
            get { return aspnetpage; }
            set { aspnetpage = value; }
        }

        /// <summary>
        /// rpt 控件
        /// </summary>
        public System.Web.UI.WebControls.Repeater RptControls
        {
            get { return rptControls; }
            set { rptControls = value; }
        }
        /// <summary>
        /// Return 记录数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
            set
            {
                if (_recordCount != value)
                    _recordCount = value;
            }
        }
        /// <summary>
        /// 设置页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                //_pageindex = Aspnetpage.CurrentPageIndex;
                if (_pageindex == 1)
                {
                    try
                    {
                        _pageindex = int.Parse(System.Web.HttpContext.Current.Request.QueryString["page"]);
                    }
                    catch
                    {
                    }
                }
                return _pageindex;
            }
            set { _pageindex = value; }
        }
        /// <summary>
        /// 设置每页显示记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return Aspnetpage.PageSize;
            }
        }
        

        /// <summary>
        /// 设置表索引字段
        /// </summary>

        public string PKField
        {
            get
            {
                return _pkField;
            }
            set
            {
                if (_pkField != value)
                    _pkField = value;
            }
        }

        /// <summary>
        /// 设置降序 如:ID DESC,publishdate ASC
        /// </summary>
        public string Order
        {
            get
            {
                return _order;
            }
            set
            {
                if (_order != value)
                    _order = value;
            }
        }

        /// <summary>
        /// 查询字段 字段中应包含排序的字段
        /// </summary>

        public string SearField
        {
            get { return _searField; }
            set { if (_searField != value)_searField = value; }
        }
        #endregion
    }
}
