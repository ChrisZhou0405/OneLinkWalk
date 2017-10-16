using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.shopping
{
    public partial class shop : System.Web.UI.Page
    {
        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "101002";
            if (!Page.IsPostBack)
            {
                //绑定推荐店铺
                BindRelated();
                //绑定本季热店
                BindIsHot();
            }
        }
        #endregion

        #region 2.0绑定推荐店铺
        /// <summary>
        /// 绑定推荐店铺
        /// </summary>
        private void BindRelated()
        {
            string sql = string.Empty;
            sql = "select ID,NodeCode,Title,Banner FROM K_U_CategoryGuide where NodeCode='101002002' and isdel=0 and isrecommend ='1' and flowstate=99 order by orders desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                RptIsRecommd.DataSource = dt;
                RptIsRecommd.DataBind();
            }
        }
        #endregion

        #region 3.0绑定本季热店
        /// <summary>
        /// 绑定本季热店
        /// </summary>
        private void BindIsHot()
        {
            string sql = string.Empty;
            sql = "select ID,NodeCode,Title,Banner FROM K_U_CategoryGuide where NodeCode='101002002'  and  isdel=0 and IsHot ='1' and flowstate=99 order by orders desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                rptIsHot.DataSource = dt;
                rptIsHot.DataBind();
            }
        }
        #endregion
    }
}