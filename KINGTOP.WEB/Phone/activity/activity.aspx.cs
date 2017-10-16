using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.activity
{
    public partial class activity : System.Web.UI.Page
    {
        public string yearStr = string.Empty;
        public string monthStr = string.Empty;
        public string lisStr = string.Empty;
        public string did = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            yearAndMonth();
            getlisDt();
            if(Context.Request.Params.AllKeys.Contains("id"))
            {

                did = Context.Request.Params["id"].ToString();
            }
        }

        void yearAndMonth()
        {
            DateTime tnow = DateTime.Now;//现在时间
            yearStr += "<li value=\"-1\">年</li>";
            monthStr += "<li value=\"-1\">月</li>";
            for ( int i = 2015; i <= int.Parse(tnow.Date.Year.ToString()); i++)
            {
             
                yearStr += " <li value=\""+i+"\">"+i+"</li>";
 
            }
            for (int k = 1; k <= 12; k++)
                monthStr += " <li value=\""+k+"\">"+k+"</li>";

        }


        void getlisDt()
        {
            string sql = "select * from K_U_News where IsDel='0' and FlowState='99' and NodeCode='101004002'";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            lis(dt);
            
 
        }
        void lis(DataTable dt)
        {
            StringBuilder c = new StringBuilder(200);
            if(dt!=null && dt.Rows.Count>0)
            {
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                c.Append("<li id=\""+dt.Rows[i]["ID"]+"\"><a><img src=\"/uploadfiles/images/" + dt.Rows[i]["TitleImg"] + "\"><h3>" + dt.Rows[i]["Title"] + "</h3><p>" + dt.Rows[i]["ActivityTime"] + "</p></a></li>");

            }
            }
            lisStr = c.ToString();
        }


    }
}