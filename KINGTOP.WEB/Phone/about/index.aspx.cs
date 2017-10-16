using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.about
{
    public partial class index : System.Web.UI.Page
    {
       public string TitleStr = string.Empty;
       public string lisTitle = string.Empty;
       public string tabsStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;
            if(Request.Params.AllKeys.Contains("id"))
            {
                id = Request.Params["id"].ToString();
            }

            getDt(id);
        }

        void getDt(string id)
        {
            string sql = "select ID,Title,ProjectIntro from K_U_ProjectOver where NodeCode='101009007001' and IsDel = 0 and FlowState = 99 order by orders asc";
            var dt = SQLHelper.GetDataSet(sql);
            getCont(dt,id);
        }

        void getCont(DataTable dt,string id)
        { 
            if(dt!=null && dt.Rows.Count>0)
            {
                string bn = string.Empty;
                string t = string.Empty;
                string idStr = string.Empty;
               
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    t = dt.Rows[i]["Title"].ToString();
                    idStr = dt.Rows[i]["ID"].ToString();
                    TitleStr = id != "" && idStr == id ? t : dt.Rows[0]["Title"].ToString();
                    bn = id != "" && idStr == id ? "style=\"\"" : i == 0 ? "style=\"\"" : "style=\"display:none;\"";
                    
                   

                    lisTitle += " <li>" + dt.Rows[i]["Title"] + "</li>";
                    tabsStr += "<div class=\"tab\" "+bn+">";
                    tabsStr += "" + dt.Rows[i]["ProjectIntro"] + "";
                    tabsStr += "</div>";
                }


            }
        }
    }
}