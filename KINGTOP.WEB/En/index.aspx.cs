using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace KingTop.WEB.En
{
    public partial class index : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "104001";
            Header.NodeCode = "104001";
            if (!IsPostBack)
            {
                BindData();
                BindLocation();
            }
        }
        #endregion

        #region  绑定首页Banner
        /// <summary>
        /// 绑定首页Banner
        /// </summary>
        public void BindData()
        {
            using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Top 3 Title,BigImg,Links from K_U_Banner where nodecode = '104001001' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dt.Rows.Count > 0)
                {
                    rptbanner.DataSource = dt;
                    rptbanner.DataBind();
                    rptli.DataSource = dt;
                    rptli.DataBind();
                }
            }
        }
        #endregion

        #region  绑定首页各个板块的广告
        /// <summary>
        /// 绑定首页各个板块的广告
        /// </summary>
        public void BindLocation()
        {
            using (DataTable dtlocationA = KingTop.Common.SQLHelper.GetDataSet("select Title,SmallImg,IP,Location from K_U_Advertposition where nodecode = '104001002' and Location='1' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dtlocationA.Rows.Count > 0)
                {
                    rptlocationA.DataSource = dtlocationA;
                    rptlocationA.DataBind();
                }
            }
            using (DataTable dtlocationB = KingTop.Common.SQLHelper.GetDataSet("select Title,SmallImg,IP,Location from K_U_Advertposition where nodecode = '104001002' and Location='2' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dtlocationB.Rows.Count > 0)
                {
                    rptlocationB.DataSource = dtlocationB;
                    rptlocationB.DataBind();
                }
            }
            using (DataTable dtlocationC = KingTop.Common.SQLHelper.GetDataSet("select Top 1 Title,SmallImg,IP,Location from K_U_Advertposition where nodecode = '104001002' and Location='3' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dtlocationC.Rows.Count > 0)
                {
                    rptlocationC.DataSource = dtlocationC;
                    rptlocationC.DataBind();
                }
            }
            using (DataTable dtlocationD = KingTop.Common.SQLHelper.GetDataSet("select Top 1 Title,SmallImg,IP,Location from K_U_Advertposition where nodecode = '104001002' and Location='4' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dtlocationD.Rows.Count > 0)
                {
                    rptlocationD.DataSource = dtlocationD;
                    rptlocationD.DataBind();
                }
            }
            using (DataTable dtlocationE = KingTop.Common.SQLHelper.GetDataSet("select Top 1 Title,SmallImg,IP,Location from K_U_Advertposition where nodecode = '104001002' and Location='5' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dtlocationE.Rows.Count > 0)
                {
                    rptlocationE.DataSource = dtlocationE;
                    rptlocationE.DataBind();
                }
            }
        }
        #endregion
    }
}