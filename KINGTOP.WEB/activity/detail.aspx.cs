using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.activity
{
    public partial class detail : System.Web.UI.Page
    {
        public string PageTitle = string.Empty;
        public string PageKeyWords = string.Empty;
        public string PageDescription = string.Empty;
        public string Id = string.Empty;
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["nid"] != null)
            {
                Id = Utils.CheckSql(Request.QueryString["nid"]);
            }
            if (!Page.IsPostBack)
            {
                //绑定活动日志
                BindMenu();
            }
        }
        #endregion

        #region 绑定活动日志
        /// <summary>
        /// 绑定活动日志
        /// </summary>
        private void BindMenu()
        {
            string sql = string.Empty;
            sql = "select ID,NodeCode,Title,ActivityTime,TitleImg,AddDate,Content,MetaKeyword,MetaDescript from K_U_News where ID ='" + Id + "' and IsDel = 0 and FlowState = 99";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                rptNews.DataSource = dt;
                rptNews.DataBind();
                PageTitle = dt.Rows[0]["Title"].ToString();
                PageKeyWords = dt.Rows[0]["MetaKeyword"].ToString();
                PageDescription = dt.Rows[0]["MetaDescript"].ToString();
            }
        }
        #endregion
    }
}