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
    /// getShopC 的摘要说明
    /// </summary>
    public class getShopC : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
          string cid=   context.Request.Params["cid"].ToString();
          getDt(cid);
        }

        void getDt(string cid)
        {
            string sqlWhere = cid == "-11" ? "" : " and type='" + cid + "'";

            string sql = "select * from K_U_CategoryGuide where ID IN ( select min(ID) from  K_U_CategoryGuide where NodeCode='101002002' and FlowState='99' and IsDel='0' "+sqlWhere+"   group by Title) order by Orders";
            //string sql = "select* from  K_U_CategoryGuide where NodeCode='101002002' " + sqlWhere+";";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            getCatelist(dt);
 
        }


        void  getCatelist(DataTable dt)
        {
            StringBuilder s = new StringBuilder(200);
            s.Append("<ul class=\"catelist\" id=\"catelist\">");
            if(dt.Rows!=null && dt.Rows.Count>0)
            {
           
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                s.Append(" <li><a href=\"/Phone/shopping/detail.aspx?id=" + dt.Rows[i]["ID"] + "\"><img src=\"/UploadFiles/images/" + dt.Rows[i]["ShopLogo"] + "\"></a></li>");
            }
            }
           
            s.Append(" </ul>");
            AjaxMsgHelper.AjaxMsg("ok", "", DataHelper.Obj2Json(s.ToString()));

         
        
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