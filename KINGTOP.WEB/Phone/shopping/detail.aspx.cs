using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.shopping
{
    public partial class detail : System.Web.UI.Page
    {
        public string category = string.Empty;
        public string catelist = string.Empty;
        public string shops = string.Empty;
        public string likeShopStr = string.Empty;
        public string shopDetail = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            dt();
            s();
            if (Context.Request.Params.AllKeys.Contains("id"))
            {
                string id = Context.Request.Params["id"].ToString();
                getShopdetail(id);

            }


        }

        void dt()
        {
            string sql = "select * from K_U_Category where IsDel='0' and FlowState='99' and NodeCode='101002001' order by Orders desc;";
            sql += "select * from K_U_CategoryGuide where IsDel='0' and FlowState='99' and  NodeCode='101002002' order by Orders desc;";

            var dts = KingTop.Common.SQLHelper.GetDataSets(sql);
            if (dts != null)
            {
                c(dts[0]);



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





        //店铺详情
        void getShopdetail(string id)
        {
            //购物指南 K_U_CategoryGuide
            // 美食荟萃 K_U_FoodGuide
            string sql = " select * from  K_U_CategoryGuide where  ID='" + id + "'";
            var dt = SQLHelper.GetDataSet(sql);
            DataTable dt2 = new DataTable();
            IList<DataTable> dts = new List<DataTable>();
            if (dt != null && dt.Rows.Count > 0)
            {
                string sql2 = " select top 10 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,LikeImg,Orders,MetaKeyword,MetaDescript FROM K_U_CategoryGuide where isdel=0  and len(LikeImg)>0 and ID not in ('" + id + "') and flowstate=99 and NodeCode='101002002' order by orders desc";
                dt2 = SQLHelper.GetDataSet(sql2);

            }
            else
            {
                string sqll = " select * from  K_U_FoodGuide where  ID='" + id + "';";
                sqll += "select top 10 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,LikeImg,Orders,MetaKeyword,MetaDescript FROM K_U_FoodGuide where isdel=0  and len(LikeImg)>0 and ID not in ('" + id + "') and flowstate=99 and NodeCode='101003002' order by orders desc;";
                dts = SQLHelper.GetDataSets(sqll);


            }

            if (dt != null && dt.Rows.Count > 0)
            {
                shopInfo(dt);
                likeShop(dt2);
            }
            else
            {
                shopInfo(dts[0]);
                likeShop(dts[1]);

            }





        }

        void shopInfo(DataTable dt)
        {
            StringBuilder t = new StringBuilder(200);
            t.Append("<div class=\"actdt\" id=\"actdt\">");
            if (dt != null && dt.Rows.Count > 0)
            {
                t.Append(" <div class=\"act-tit\"><span class=\"sharebtn\" id=\"sharebtn\">分享</span><h2>" + dt.Rows[0]["Title"] + "</h2></div> ");


                t.Append("<div id=\"focus1\" class=\"focus focus2\" style=\" margin:10px 0;\">");
                t.Append("<div class=\"hd\">");
                t.Append(" <ul></ul>");
                t.Append("</div>");
                t.Append("<div class=\"bd\">");
                t.Append("<ul>");
                var imgStr = dt.Rows[0]["Shopshow"].ToString();

                t.Append("" + GetListIMG(imgStr) + "");

                t.Append("</ul>");
                t.Append("</div>");
                t.Append("<span class=\"prev\"></span>");
                t.Append("<span class=\"next\"></span>");
                t.Append(" </div>");
                t.Append(" <div class=\"shop_tit\"><h3>店铺信息</h3></div>");
                t.Append("<div class=\"shop\">");
                t.Append(" <div class=\"pic\"><img src=\"/uploadfiles/images/" + dt.Rows[0]["ShopLogo"] + "\"></div>");
                t.Append("<div class=\"txt\">");
                t.Append("<p>店铺名称：" + dt.Rows[0]["Title"] + "</p>");

                t.Append(" <p>位置：" + dt.Rows[0]["ShopNo"] + "</p>");
                t.Append("<p>电话：" + dt.Rows[0]["TelPhone"] + "</p>");
                t.Append("<p>销售产品：" + dt.Rows[0]["SalesPro"] + "</p>");
                t.Append(" <p>网址：");
                t.Append(" <a href=\"https://" + dt.Rows[0]["SiteURL"] + "\" target=\"_blank\">" + dt.Rows[0]["SiteURL"] + "</a>");
                t.Append("</p>");
                t.Append("</div>");
                t.Append("</div>");
                t.Append("<p>" + dt.Rows[0]["IntroDetail"] + "</p>");
            }
            t.Append("</div> ");
            shopDetail = t.ToString();



        }


        string GetListIMG(string listimg)
        {
            string result = string.Empty;
            string stylePd = string.Empty;
            string[] list = listimg.Replace("$$$", ",").Split(',');
            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    result += "<li><img src='/uploadfiles/images/" + list[i] + "' /></li>";
                }
            }
            return result;
        }



        void likeShop(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder ls = new StringBuilder(200);
                ls.Append(" <div id=\"liketit\" class=\"like_tit\"><span>你可能还会喜欢</span></div>");
                ls.Append("<div id=\"focus2\" class=\"focus focus2\">");
                ls.Append(" <div class=\"hd\">");
                ls.Append(" <ul></ul>");
                ls.Append(" </div>");
                ls.Append(" <div class=\"bd\">");
                ls.Append("  <ul>");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Append(" <li><a href=\"/Phone/shopping/detail.aspx?id=" + dt.Rows[i]["ID"] + "\"><img src=\"/UploadFiles/Images/" + dt.Rows[i]["LikeImg"] + "\"><p>" + dt.Rows[i]["Title"] + "</p></a></li>");
                }
                ls.Append(" </ul>");
                ls.Append(" </div>");
                ls.Append(" <span class=\"prev\"></span>");
                ls.Append("<span class=\"next\"></span>");
                ls.Append(" </div>  ");
                likeShopStr = ls.ToString();
            }
        }

    }
}