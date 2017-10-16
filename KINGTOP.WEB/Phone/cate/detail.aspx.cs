using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.cate
{
    public partial class detail : System.Web.UI.Page
    {
        public string shopName = string.Empty;
        public string ShopLogo = string.Empty;
        public string showImg = string.Empty;

        public string postion = string.Empty;
        public string tel = string.Empty;
        public string sale = string.Empty;
        public string urlStr = string.Empty;
        public string detailStr = string.Empty;
        public string likeStr = string.Empty;
        public string typeID = string.Empty;
        public string typeStr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Context.Request.Params["id"].ToString();
            dt(id);
        }

        void dt(string id)
        {
            string sql = "select * from K_U_FoodGuide  where ID='" + id + "';select top 5 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,LikeImg,Orders FROM K_U_FoodGuide where NodeCode='101003002' and isdel=0 and  len(LikeImg)>0 and ID not in ('" + id + "') and flowstate=99 order by orders desc";
            var dts = KingTop.Common.SQLHelper.GetDataSets(sql);
            if (dts != null)
            {
                typeID = dts[0].Rows[0]["type"].ToString();
                typeStr = currentStr(typeID);

                cont(dts[0], dts[1]);
            }


        }
        void cont(DataTable dt, DataTable dt2)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                shopName = "" + dt.Rows[0]["Title"] + "";
                ShopLogo = "/uploadfiles/images/" + dt.Rows[0]["ShopLogo"] + "";
                showImg = "" + GetListIMG(dt.Rows[0]["Shopshow"].ToString()) + "";
                postion = "" + dt.Rows[0]["ShopNo"] + "";
                tel = "" + dt.Rows[0]["TelPhone"] + "";
                sale = "" + dt.Rows[0]["SalesPro"] + "";

                urlStr = "<a href=\"https://" + dt.Rows[0]["SiteURL"] + "\" target=\"_blank\">" + dt.Rows[0]["SiteURL"] + "</a>";
                detailStr = "" + dt.Rows[0]["IntroDetail"] + "";

            }

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    likeStr += "  <li><a href=\"/Phone/cate/detail.aspx?id=" + dt2.Rows[i]["ID"] + "\"><img src=\"/uploadfiles/images/" + dt2.Rows[i]["LikeImg"] + "\"><p>" + dt2.Rows[i]["Title"] + "</p></a></li>";
                }
            }
        }


        public string GetListIMG(string listimg)
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


        string currentStr(string id)
        {

            if (id == "")
            {
                return "<img src=\"/Phone/images/cicon1.png\" width=\"28\">全部美食";
            }
            else if (id == "100000000144283")
            {
                return "<img src=\"/Phone/images/cicon2.png\" width=\"28\"> 亚洲美食</a>";
            }
            else if (id == "100000000825845")
            {
                return "<img src=\"/Phone/images/cicon3.png\" width=\"28\"> 中式佳肴</a>";
            }
            else if (id == "100000001534222")
            {
                return "<img src=\"/Phone/images/cicon4.png\" width=\"28\">  西方美馔</a>";
            }
            else if (id == "100000002274564")
            {
                return "<img src=\"/Phone/images/cicon5.png\" width=\"28\">  轻便美食/甜点</a>";
            }


            return "<img src=\"/Phone/images/cicon1.png\" width=\"28\">全部美食";

        }
    }
}