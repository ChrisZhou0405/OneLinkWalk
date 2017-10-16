using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.cate
{
    public partial class csite : System.Web.UI.Page
    {
        public string str = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;

            if (KingTop.Common.Tools.CheckSql(Request.QueryString["id"]) != "")
            {
                string wherestr = string.Empty;
                id = KingTop.Common.Utils.CheckSql(Request.QueryString["id"]);

                string sql = "SELECT ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Orders,MetaKeyword,MetaDescript,Floor FROM K_U_FoodGuide Where isdel=0 and id='" + id + "' and flowstate=99";

                DataTable dt = SQLHelper.GetDataSet(sql);
                if (Utils.CheckDataTable(dt))
                {
                    //rptlist.DataSource = dt;
                    //rptlist.DataBind();
                    str = getDt(dt);

                }
            }

        }

        string getDt(DataTable dt)
        {
            StringBuilder s = new StringBuilder(200);
            s.Append("  <img src=\"/UploadFiles/images/" + dt.Rows[0]["Stereogram"].ToString() + "\" alt=\"\">");
            s.Append("   <div class=\"subLTxt\">");
            s.Append("  <img src=\"/UploadFiles/images/" + dt.Rows[0]["ShopLogo"] + "\" alt=\"\">");
            s.Append("  <span>");
            s.Append("   <i>店铺名称：" + dt.Rows[0]["Title"] + "</i>");
            s.Append(" <i>位置：" + dt.Rows[0]["ShopNo"] + " <em class=\"btnSite\">地图</em></i>");
            s.Append(" <i>电话：" + dt.Rows[0]["TelPhone"] + "</i>");
            s.Append("     <i>销售产品：" + dt.Rows[0]["SalesPro"] + "</i>");
            s.Append("  <i>网址：<a target=\"_blank\" href=\"http://"+dt.Rows[0]["SiteURL"] +"\">"+dt.Rows[0]["SiteURL"] +"</a></i>");
            s.Append("    </span>");
            var d = dt.Rows[0]["IntroDetail"].ToString();
            if (d.Length > 60)
            {
                d = d.Substring(0, 57) + "...";
            }

            s.Append("<p>" + d + "</p>");
            s.Append("   <a href=\"/En/cate/detail.aspx?nid=" + dt.Rows[0]["id"] + "\" id=\"bl_txt_a\" target=\"_parent\">了解详情</a>");


            s.Append("</div>");
            s.Append("    </div>");
            return s.ToString();

        }
    }
}