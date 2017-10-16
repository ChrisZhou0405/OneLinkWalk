using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.shopping
{
    public partial class site2 : System.Web.UI.Page
    {
    
        public string Stereogram = string.Empty;
        public string ShopLogo = string.Empty;
        public string TitleStr = string.Empty;
        public string ShopNo = string.Empty;
        public string TelPhone = string.Empty;
        public string IntroDetail = string.Empty;
        public string SalesPro = string.Empty;
        public string idStr = string.Empty;
        public string SiteURL = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;

            if (KingTop.Common.Tools.CheckSql(Request.QueryString["id"]) != "")
            {
                string wherestr = string.Empty;
                id = KingTop.Common.Utils.CheckSql(Request.QueryString["id"]);

                string sql = "SELECT ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Orders,MetaKeyword,MetaDescript,Floor FROM K_U_CategoryGuide Where  isdel=0 and id='" + id + "' and flowstate=99";

                DataTable dt = SQLHelper.GetDataSet(sql);
                if (Utils.CheckDataTable(dt))
                {
                    //rptlist.DataSource = dt;
                    //rptlist.DataBind();
                    getDt(dt);

                }
            }

        }

        void getDt(DataTable dt)
        {
            //StringBuilder s = new StringBuilder(200);
            if (dt.Rows[0]["Stereogram"].ToString() != null)
            {
                Stereogram = "<img src=\"/UploadFiles/images/" + dt.Rows[0]["Stereogram"].ToString() + "\" alt=\"\">";
            }

        
            //s.Append("  <img src=\"/UploadFiles/images/" + dt.Rows[0]["Stereogram"].ToString() + "\" alt=\"\">");
            //s.Append("   <div class=\"subLTxt\">");
            if (dt.Rows[0]["ShopLogo"].ToString() != null)
            { 
            ShopLogo = " <img src=\"/UploadFiles/images/" + dt.Rows[0]["ShopLogo"] + "\" alt=\"\">";
            }
            //s.Append("  <img src=\"/UploadFiles/images/" + dt.Rows[0]["ShopLogo"] + "\" alt=\"\">");
            //s.Append("  <span>");
            SiteURL = dt.Rows[0]["SiteURL"].ToString();
            TitleStr = dt.Rows[0]["Title"].ToString();
            //s.Append("   <i>店铺名称：" + dt.Rows[0]["Title"] + "</i>");
            ShopNo = dt.Rows[0]["ShopNo"].ToString();
            //s.Append(" <i>位置：" + dt.Rows[0]["ShopNo"] + " <em class=\"btnSite\">地图</em></i>");
            TelPhone = dt.Rows[0]["TelPhone"].ToString();
            //s.Append(" <i>电话：" + dt.Rows[0]["TelPhone"] + "</i>");
            SalesPro = dt.Rows[0]["SalesPro"].ToString();
            //s.Append("     <i>销售产品：" + dt.Rows[0]["SalesPro"] + "</i>");
            //s.Append("  <i>网址：<a href=\"http://www.Pandora.com\">www.Pandora.com</a></i>");
            //s.Append("    </span>");
            var IntroDetail = dt.Rows[0]["IntroDetail"].ToString();
            if (IntroDetail.Length > 60)
            {
                IntroDetail = IntroDetail.Substring(0, 57) + "...";
            }
            idStr = dt.Rows[0]["id"].ToString();
            //s.Append("<p>" + IntroDetail + "</p>");
            //s.Append("   <a href=\"/shopping/detail2.aspx?nid=" + dt.Rows[0]["id"] + "\" id=\"bl_txt_a\" target=\"_parent\">了解详情</a>");


            //s.Append("</div>");
            //s.Append("    </div>");
        

        }



    }
}