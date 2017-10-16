using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KingTop.WEB.about
{
    public partial class traffic : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "101007002";
            if (!IsPostBack)
            {
                BindData();
            }
        }
        #endregion

        #region  绑定首页Banner
        /// <summary>
        /// 绑定首页Banner
        /// </summary>
        public void BindData()
        {
            using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Title,BigImg,ReachIntro from K_U_ModeArrival where nodecode = '101007002' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dt.Rows.Count > 0)
                {
                    rptbanner.DataSource = dt;
                    rptbanner.DataBind();
                }
            }
        }
        #endregion
    }
}