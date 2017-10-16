using KingTop.Common;
using KingTop.Common.lc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace KingTop.WEB.Phone.activity
{
    /// <summary>
    /// getActivityDetail 的摘要说明
    /// </summary>
    public class getActivityDetail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           string id= context.Request.Params["id"].ToString();
           getDt(id);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        void getDt(string id)
        {
           string sql = "select ID,NodeCode,Title,ActivityTime,TitleImg,AddDate,Content,MetaKeyword,MetaDescript from K_U_News where ID ='" + id+ "' and IsDel = 0 and FlowState = 99";
          var dt=  SQLHelper.GetDataSet(sql);
          divStr(dt);


        }
        void divStr(DataTable dt)
        {
            StringBuilder c = new StringBuilder(200);
            c.Append(" <div class=\"actdt\" id=\"actdt\">");
            if(dt!=null && dt.Rows.Count>0)
            {

                c.Append("<div  class=\"act-tit\"><span class=\"sharebtn\" id=\"sharebtn\">分享</span><h2>" + dt.Rows[0]["Title"] + "</h2><p>" + dt.Rows[0]["ActivityTime"] + "</p></div> ");
                c.Append("" + dt.Rows[0]["Content"] + "");
           
            }
             c.Append("</div> ");
             AjaxMsgHelper.AjaxMsg("ok", "", DataHelper.Obj2Json(c.ToString()));
           
   
        }
    }
}