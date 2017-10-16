using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            get3();
        }

        void get()
        {
            //67
            StringBuilder s = new StringBuilder(200);
            string sql = "select * from K_U_CategoryGuide where  NodeCode='101002002'";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            if(dt!=null && dt.Rows.Count>0)
            {
                string sql2 = "select * from  K_U_CategoryGuide where NodeCode='104002002'";
                var dt2 = KingTop.Common.SQLHelper.GetDataSet(sql2);

                for (int i =39; i < 67;i++ )
                {
                    s.Append("UPDATE K_U_CategoryGuide SET [type] = '" + dt.Rows[i]["type"] + "', isrecommend='" + dt.Rows[i]["isrecommend"] + "',IsHot='" + dt.Rows[i]["IsHot"] + "',Banner='" + dt.Rows[i]["Banner"] + "',LikeImg='" + dt.Rows[i]["LikeImg"] + "',Title='" + dt.Rows[i]["Title"] + "',ShopLogo='" + dt.Rows[i]["ShopLogo"] + "',Stereogram='" + dt.Rows[i]["Stereogram"] + "',ShopNo='" + dt.Rows[i]["ShopNo"] + "',LocationImg='" + dt.Rows[i]["LocationImg"] + "',TelPhone='" + dt.Rows[i]["TelPhone"] + "',SalesPro='" + dt.Rows[i]["SalesPro"] + "',SiteURL='" + dt.Rows[i]["SiteURL"] + "',IntroDetail='" + dt.Rows[i]["IntroDetail"] + "',Shopshow='" + dt.Rows[i]["Shopshow"] + "',LetterQuery='" + dt.Rows[i]["LetterQuery"] + "',Floor='" + dt.Rows[i]["Floor"] + "',MetaKeyword='" + dt.Rows[i]["MetaKeyword"] + "',MetaDescript='" + dt.Rows[i]["MetaDescript"] + "' WHERE ID = '" + dt2.Rows[i]["ID"] + "';\n");
                }

              var ins=   KingTop.Common.SQLHelper.ExecuteNonQuery(s.ToString());
            }
        }


        void get2()
        {
            StringBuilder s = new StringBuilder(200);
            string sql = "select * from K_U_FoodGuide where NodeCode='101003002' ";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                string sql2 = "select * from K_U_FoodGuide where NodeCode='104003002'";
                var dt2 = KingTop.Common.SQLHelper.GetDataSet(sql2);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    s.Append("UPDATE K_U_FoodGuide SET [type] = '" + dt.Rows[i]["type"] + "',[Title]='" + dt.Rows[i]["Title"] + "',ShopLogo='" + dt.Rows[i]["ShopLogo"] + "',LikeImg='" + dt.Rows[i]["LikeImg"] + "',Stereogram='" + dt.Rows[i]["Stereogram"] + "',ShopNo='" + dt.Rows[i]["ShopNo"] + "',[Floor]='" + dt.Rows[i]["Floor"] + "',LocationImg='" + dt.Rows[i]["LocationImg"] + "',TelPhone='" + dt.Rows[i]["TelPhone"] + "',SalesPro='" + dt.Rows[i]["SalesPro"] + "',SiteURL='" + dt.Rows[i]["SiteURL"] + "',IntroDetail='" + dt.Rows[i]["IntroDetail"] + "',Shopshow='" + dt.Rows[i]["Shopshow"] + "',MetaKeyword='" + dt.Rows[i]["MetaKeyword"] + "',MetaDescript='" + dt.Rows[i]["MetaDescript"] + "' WHERE ID = '" + dt2.Rows[i]["ID"] + "';\n");
                }

                var ins = KingTop.Common.SQLHelper.ExecuteNonQuery(s.ToString());
            }
        }


        void get3()
        {
                //B1
                //NodeCode=101002003001
                //EN  NodeCode=104002003001

                //L1  NodeCode=101002003002
                //EN  NodeCode=104002003002

                //L2  NodeCode=101002003003
                //EN  NodeCode=104002003003

                //L3  NodeCode=101002003004
                //EN  NodeCode=104002003004

                //L4  NodeCode=101002003005
                //EN  NodeCode=104002003005

                //L5 NodeCode=101002003006
                //EN NodeCode=104002003006
            StringBuilder s = new StringBuilder(200);
            string sql = "select * from K_U_Floorguide where NodeCode='101002003006' ";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                string sql2 = "select * from K_U_Floorguide where NodeCode='104002003006'";
                var dt2 = KingTop.Common.SQLHelper.GetDataSet(sql2);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    s.Append("UPDATE K_U_Floorguide  SET [Title] = '" + dt.Rows[i]["Title"] + "', lcnum='" + dt.Rows[i]["lcnum"] + "',lcoverimgurl='" + dt.Rows[i]["lcoverimgurl"] + "',lcx='" + dt.Rows[i]["lcx"] + "',lcy='" + dt.Rows[i]["lcy"] + "',lclink='" + dt.Rows[i]["lclink"] + "',lccoords='" + dt.Rows[i]["lccoords"] + "' WHERE ID = '" + dt2.Rows[i]["ID"] + "';\n");
                }

                var ins = KingTop.Common.SQLHelper.ExecuteNonQuery(s.ToString());
            }
        }
    }
}