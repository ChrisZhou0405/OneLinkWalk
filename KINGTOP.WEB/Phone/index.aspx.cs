using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone
{
    public partial class index : System.Web.UI.Page
    {
        public string bd = string.Empty;
        public string focus1 = string.Empty;
        public string focus2 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            get();
        }

        void get()
        {
            string sql = "  select Links,BigImgBanner,Title from K_U_pBanner where IsDel='0' and FlowState='99' and NodeCode='101009001001' order by Orders  desc;";
            sql += "select * from K_U_Advertposition where IsDel='0' and FlowState='99' and NodeCode='101001002'  and Location='1' order by Orders desc;";
            sql += "select * from K_U_Advertposition where IsDel='0' and FlowState='99' and NodeCode='101001002'  and Location='2' order by Orders desc;";
            var dts = KingTop.Common.SQLHelper.GetDataSets(sql);
            if (dts != null)
            {
                bdM(dts[0]);
                focus1M(dts[1]);
                focus2M(dts[2]);
               
            }

        }

        void bdM(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    bd += "<li><a href=" + dt.Rows[i]["Links"] + "><img src=\"/UploadFiles/images/" + dt.Rows[i]["BigImgBanner"] + "\"></a></li>";
                }
            }
        }

        void focus1M(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    focus1 += "<li><a href=\"" + dt.Rows[i]["IPIphone"] + "\"><img src=\"/UploadFiles/images/" + dt.Rows[i]["SmallImg"] + "\"><span>" + dt.Rows[i]["Title"] + "</span></a></li>";
                }
            }
        }


        void focus2M(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    focus2 += "<li><a href=\"" + dt.Rows[i]["IPIphone"] + "\"><img src=\"/UploadFiles/images/" + dt.Rows[i]["SmallImg"] + "\"><span>" + dt.Rows[i]["Title"] + "</span></a></li>";
                }
            }
        }
    }
}