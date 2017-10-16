using KingTop.Common.lc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KingTop.WEB.Phone.shopping
{
    /// <summary>
    /// getShopS 的摘要说明
    /// </summary>
    public class getShopS : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var nc = context.Request.Params["nc"].ToString();
            dt(nc);

        }
        void dt(string nc)
        {
            string s = string.Empty;

            string sql = " select Title,lcnum from K_U_Floorguide where IsDel='0' and FlowState='99' and NodeCode='"+nc+"' and lcnum!=Title order by Orders desc";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            if(dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    s += " <li id=\"" + dt.Rows[i]["lcnum"] + "\">" + dt.Rows[i]["Title"] + "</li>";

                }
            }
            

            var data = DataHelper.Obj2Json(s);
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