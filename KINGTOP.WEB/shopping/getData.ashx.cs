using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace KingTop.WEB.shopping
{
    /// <summary>
    /// getData 的摘要说明
    /// </summary>
    public class getData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string locaiton = string.Empty;
       
            if (KingTop.Common.Tools.CheckSql(context.Request .QueryString["locaiton"]) != "")
            {
                string wherestr = string.Empty;
                locaiton = KingTop.Common.Utils.CheckSql(context.Request.QueryString["locaiton"]);
                if (locaiton =="0")
                {
                    wherestr = " and Floor ='0'";
                }
                else if (locaiton == "1")
                {
                    wherestr = " and Floor ='1'";
                }
                else if (locaiton == "2")
                {
                    wherestr = " and Floor ='3'";
                }
                else if (locaiton == "3")
                {
                    wherestr = " and Floor ='4'";
                }
                else if (locaiton == "4")
                {
                    wherestr = " and Floor ='5'";
                }
                else if (locaiton == "5")
                {
                    wherestr = " and Floor ='6'";
                }
                string sql = string.Empty;
                sql = "SELECT Top 1 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Orders FROM K_U_CategoryGuide  where   NodeCode='104002002' and  isdel=0 and flowstate=99" + wherestr + " order by orders desc";
                DataTable dt = SQLHelper.GetDataSet(sql);
                if (Utils.CheckDataTable(dt))
                {
                    //rptlist.DataSource = dt;
                    //rptlist.DataBind();
                    var obj = getDt(dt);
                     JavaScriptSerializer jss = new JavaScriptSerializer();
                      var str=  jss.Serialize(obj);
                 
                    string strMsg = "{\"statu\":\"ok\",\"msg\":\"成功\",\"data\":" + str + "}";

                    System.Web.HttpContext.Current.Response.Write(strMsg);
                  
                }
            }
          
        
        }
        string  getDt(DataTable dt)
        {
            StringBuilder s = new StringBuilder(200);
            s.Append("  <img src=\"/UploadFiles/images/"+dt.Rows[0]["Stereogram"].ToString()+"\" alt=\"\">");
            s.Append("   <div class=\"subLTxt\">");  
            s.Append("  <img src=\"/UploadFiles/images/"+dt.Rows[0]["ShopLogo"]+">\" alt=\"\">");
            s.Append("  <span>");
             s.Append("   <i>店铺名称："+dt.Rows[0]["Title"]+"></i>");
             s.Append(" <i>位置："+dt.Rows[0]["ShopNo"]+" <em class=\"btnSite\">地图</em></i>");
             s.Append(" <i>电话："+dt.Rows[0]["TelPhone"]+"</i>");
             s.Append("     <i>销售产品："+dt.Rows[0]["SalesPro"]+"</i>");
             s.Append("  <i>网址：<a href=\"http://www.Pandora.com\">www.Pandora.com</a></i>");
             s.Append("    </span>");
             var d = dt.Rows[0]["IntroDetail"].ToString();
            if(d.Length>120)
            {
                d = d.Substring(0, 118) + "...";
            }

             s.Append("<p>" +d+ "</p>");
             s.Append("  <a href=\"/detail2.aspx?nid="+dt.Rows[0]["id"]+" id=\"bl_txt_a\">了解详情</a>");
                s.Append( "</div>");
              s.Append("    </div>");
            return s.ToString();
                 
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