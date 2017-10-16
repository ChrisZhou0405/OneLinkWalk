using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.shopping
{
    public partial class index : System.Web.UI.Page
    {
        public string category = string.Empty;
        public string catelist = string.Empty;
        public string shops = string.Empty;
        public string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Context.Request.Params.AllKeys.Contains("id"))
            {
                id = Request.Params["id"].ToString();
            }
            
            dt();
            s();
        }

        void dt()
        {
            string sql = "select * from K_U_Category where IsDel='0' and FlowState='99' and NodeCode='101002001' order by Orders desc;";
            sql += "select * from K_U_CategoryGuide where ID IN ( select min(ID) from  K_U_CategoryGuide where NodeCode='101002002' and FlowState='99' and IsDel='0'   group by Title) order by Orders";

            var dts = KingTop.Common.SQLHelper.GetDataSets(sql);
            if (dts != null && dts.Count == 2)
            {

                c(dts[0]);
                d(dts[1]);


            }
        }

        void c(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                category += "<li id=\"-11\">全部</li>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    category += "<li id=\"" + dt.Rows[i]["ID"] + "\">" + dt.Rows[i]["Title"] + "</li>";

                }

            }

        }
        void d(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    catelist += " <li><a href=\"/Phone/shopping/detail.aspx?id=" + dt.Rows[i]["ID"] + "\"><img src=\"/UploadFiles/images/" + dt.Rows[i]["ShopLogo"] + "\"></a></li>";
                }
            }

        }

        //按楼层指引

        void s()
        {
            //B1
            string sql = "select Title,lcnum from K_U_Floorguide where IsDel='0' and FlowState='99' and NodeCode='101002003001' and lcnum!=Title  order by Orders desc";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            if (dt != null && dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shops += " <li id=\"" + dt.Rows[i]["lcnum"] + "\">" + dt.Rows[i]["Title"] + "</li>";
                }

            }
        }
    }
}