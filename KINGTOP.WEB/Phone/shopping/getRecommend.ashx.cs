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
    /// getRecommend 的摘要说明
    /// </summary>
    public class getRecommend : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            getDt();
        }

        void getDt()
        {
            string sql = "select ID,NodeCode,Title,Banner FROM K_U_CategoryGuide where NodeCode='101002002' and isdel=0 and isrecommend ='1' and flowstate=99 order by orders desc;";
            sql += " select ID,NodeCode,Title,Banner FROM K_U_CategoryGuide where NodeCode='101002002'  and  isdel=0 and IsHot ='1' and flowstate=99 order by orders desc;";
            var dts = KingTop.Common.SQLHelper.GetDataSets(sql);
            if(dts!=null )
            {
                rTxt(dts[0], dts[1]);

            }

        }

        void rTxt(DataTable dt,DataTable dt2)
        {
            StringBuilder r = new StringBuilder(200);
            r.Append("  <ul class=\"recommendlist\">");
             if(dt!=null && dt.Rows.Count>0)
             {
                 for (int i = 0; i < dt.Rows.Count;i++ )
                 {
                     r.Append(" <li><a href=\"/Phone/shopping/detail.aspx?id=" + dt.Rows[i]["ID"] + "\"><img src=\"/UploadFiles/Images/" + dt.Rows[i]["Banner"] + "\"><p>" + dt.Rows[i]["Title"] + "</p></a></li>");
                 }
             }
          
            r.Append(" </ul>");

            r.Append(" <div class=\"lookup\"><a href=\"/Phone/shopping/index.aspx\">查看更多</a></div>");
            r.Append("<div class=\"like_tit\"><span>本季热店</span></div>");
            r.Append("<div id=\"focus2\" class=\"focus focus2\">");
            r.Append(" <div class=\"hd\">");
            r.Append(" <ul></ul>");
            r.Append(" </div>");
            r.Append(" <div class=\"bd\">");
            r.Append(" <ul>");
            
            if(dt2!=null && dt2.Rows.Count>0)
            {
                for (int i = 0; i < dt2.Rows.Count;i++ )
                {
                    r.Append("<li><a href=\"/Phone/shopping/detail.aspx?id=" + dt2.Rows[i]["ID"] + "\"><img src=\"/UploadFiles/Images/" + dt2.Rows[i]["Banner"] + "\"><p>" + dt2.Rows[i]["Title"] + "</p></a></li>");
                }
            }
         
            r.Append("</ul>");
            r.Append("</div>");
            r.Append("  <span class=\"prev\"></span>");
            r.Append("<span class=\"next\"></span>");
            r.Append(" </div> ");
          
            AjaxMsgHelper.AjaxMsg("ok", "", DataHelper.Obj2Json(r.ToString()));

           
 
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