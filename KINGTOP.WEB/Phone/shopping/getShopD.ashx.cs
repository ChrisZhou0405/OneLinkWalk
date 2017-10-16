using KingTop.Common.lc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace KingTop.WEB.Phone.shopping
{
    /// <summary>
    /// getShopD 的摘要说明
    /// </summary>
    public class getShopD : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var shopno = context.Request.Params["shopno"].ToString();
            get(shopno);

        }

        void get(string shopno)
        {
            string sql = "select top 1 * from  K_U_CategoryGuide where ShopNo='"+shopno+"' order by Orders;";
            sql += "select top 1 * from  K_U_FoodGuide where ShopNo='"+shopno+"' order by Orders;";
            var dts = KingTop.Common.SQLHelper.GetDataSets(sql);
            if (dts != null)
            {
                if (dts[0] != null && dts[0].Rows.Count > 0)
                {
                    shopD(dts[0]);
                }
                else if (dts[1] != null && dts[1].Rows.Count > 0)
                {
                    shopD(dts[1]);
                }
                else
                {
                    var data = DataHelper.Obj2Json("没有相关数据");
                    AjaxMsgHelper.AjaxMsg("err", "", data);
                }
            }
            
        }

        void shopD(DataTable dt)
        {
            StringBuilder d = new StringBuilder(200);
            d.Append(" <div id=\"actdt\" class=\"actdt\">");
            d.Append(" <p><img src=\"/UploadFiles/images/" + dt.Rows[0]["Stereogram"] + "\"></p>");
             d.Append("<div class=\"shop\">");
             d.Append("  <div class=\"pic\"><img src=\"/UploadFiles/images/" + dt.Rows[0]["ShopLogo"] + "\"></div>");
             d.Append("<div class=\"txt\">");
             d.Append(" <p>店铺名称：" + dt.Rows[0]["Title"] + "</p>");
             d.Append(" <p>位置：" + dt.Rows[0]["ShopNo"] + "</p>");
             d.Append(" <p>电话：" + dt.Rows[0]["TelPhone"] + "</p>");
             d.Append("<p>销售产品：" + dt.Rows[0]["SalesPro"] + "</p>");
             d.Append("  <p>网址：" + dt.Rows[0]["SiteURL"] + "</p>");
             d.Append(" </div>");
             d.Append("</div>");
             d.Append("<p>" + dt.Rows[0]["IntroDetail"] + "</p>");
             d.Append(" <div class=\"lookup\"><a href=\"/Phone/shopping/detail.aspx?id="+dt.Rows[0]["ID"]+"\">了解详情</a></div>");
            d.Append(" </div> ");

            var data = DataHelper.Obj2Json(d.ToString());
            AjaxMsgHelper.AjaxMsg("ok","",data);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}