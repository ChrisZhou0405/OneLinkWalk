using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.about
{
    public partial class index : System.Web.UI.Page
    {
        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "104007001";
            if (!Page.IsPostBack)
            {
                //绑定项目概述
                BindProject();
            }
        }
        #endregion

        #region 2.0绑定项目概述
        /// <summary>
        /// 绑定项目概述
        /// </summary>
        private void BindProject()
        {
            string sql = string.Empty;
            sql = "select Title,ProjectIntro from K_U_ProjectOver where NodeCode='104007001' and IsDel = 0 and FlowState = 99 order by orders asc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                rptmonth.DataSource = dt;
                rptmonth.DataBind();
                rptontent.DataSource = dt;
                rptontent.DataBind();
            }
        }
        #endregion
    }
}