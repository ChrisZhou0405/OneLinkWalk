using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KingTop.Common;

namespace KingTop.WEB.En.vip
{
    public partial class nonmember : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "104005001";
            Header.NodeCode = "104005001";
            if (!IsPostBack)
            {
                BindData();
                BindRule();
            }
        }
        #endregion

        #region  绑定首页Banner
        /// <summary>
        /// 绑定首页Banner
        /// </summary>
        public void BindData()
        {
            using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Title,BigImg,Links from K_U_Banner where nodecode = '104005001001001' and isdel=0 and flowstate=99  order by orders desc"))
               
            {
                if (dt.Rows.Count > 0)
                {
                    rptli.DataSource = dt;
                    rptli.DataBind();
                }
            }
        }
        #endregion

        #region 绑定条款及细则
        /// <summary>
        /// 绑定条款及细则
        /// </summary>
        private void BindRule()
        {
            string sql = string.Empty;
            sql = "select nodecode,NodelEngDesc,NodeName,SubDomain,NodelEngDesc from K_SysModuleNode where ParentNode='104005003004' and IsDel=0 order by NodelOrder asc";
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