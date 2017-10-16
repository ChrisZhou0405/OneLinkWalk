using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.Controls
{
    public partial class Header : System.Web.UI.UserControl
    {
        public string NodeCode = string.Empty;
        public string HeadCss = string.Empty;
        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (NodeCode != "104001")
            {
                HeadCss = "hBg";
            }
            else
            {
                HeadCss = "";
            }
            if (!Page.IsPostBack)
            {
                //绑定购物指南分类
                BindRelated();
            }
        }
        #endregion
        #region 2.0绑定购物指南分类
        /// <summary>
        /// 绑定购物指南分类
        /// </summary>
        private void BindRelated()
        {
            string sql = string.Empty;
            sql = "select ID,NodeCode,Title,Orders FROM K_U_Category where isdel=0 and flowstate=99 and NodeCode='104002001' order by orders desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                RptRelated.DataSource = dt;
                RptRelated.DataBind();
            }
        }
        #endregion
    }
}