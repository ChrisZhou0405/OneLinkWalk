using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace KingTop.WEB.vip
{
    public partial class member : System.Web.UI.Page
    {
        #region 属性

        #endregion

        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            BindRule();
        }
        #endregion

        #region 绑定条款及细则
        /// <summary>
        /// 绑定条款及细则
        /// </summary>
        private void BindRule()
        {
            string sql = string.Empty;
            sql = "select nodecode,NodeName,SubDomain,NodelEngDesc from K_SysModuleNode where ParentNode='101005003004' and IsTopMenuShow=1 and IsDel=0 order by NodelOrder asc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                rpttabcon.DataSource = dt;
                rpttabcon.DataBind();
            }
        }
        #endregion
      
    }
}