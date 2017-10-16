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
    /// getActivity 的摘要说明
    /// </summary>
    public class getActivity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
          string y= context.Request.Params["y"];
          string m=context.Request.Params["m"];
          getDt(y,m);
        }

        void  getDt(string y,string m)
        {
            string sql = sqlStr(y,m);
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            lis(dt);
           

        }

        string sqlStr(string y,string m)
        {
            string sql = "select * from K_U_News where IsDel='0' and FlowState='99' and NodeCode='101004002'";
            if (y != "-1")
            {
                sql += " and  " + y + " >=Year(startTime) and " + y + " <=Year(endTime) ";
            }
            if (m != "-1")
            {
                sql += " and " + m + " >=Month(startTime) and " + m + " <=Month(endTime) ";

            }



            return sql;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        void lis(DataTable dt)
        {
            StringBuilder c = new StringBuilder(200);
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                c.Append("<li id=\""+dt.Rows[i]["ID"]+"\"><a><img src=\"/uploadfiles/images/" + dt.Rows[i]["TitleImg"] + "\"><h3>" + dt.Rows[i]["Title"] + "</h3><p>" + dt.Rows[i]["ActivityTime"] + "</p></a></li>");

            }
            AjaxMsgHelper.AjaxMsg("ok", "", DataHelper.Obj2Json(c.ToString()));
        }
    }
}