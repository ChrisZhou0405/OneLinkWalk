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
    /// getShopL 的摘要说明
    /// </summary>
    public class getShopL : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lq = context.Request.Params["lq"].ToString();
            getDt(lq);
        }


        void getDt(string lq)
        {
            string sqlwhere = string.Empty;
            sqlwhere = lq == "-1" ? "" : "and LetterQuery in(" + lq + ")";


            string sql = "select * from  K_U_CategoryGuide   where NodeCode='101002002' and IsDel='0' and FlowState='99' " + sqlwhere + " order by LetterQuery;";
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