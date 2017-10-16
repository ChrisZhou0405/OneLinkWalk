using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.about
{
    public partial class ways : System.Web.UI.Page
    {
        public string translate = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            getDt();
        }

        void getDt()
        {
            string sql = "select Title,BigImg,ReachIntro from K_U_ModeArrival where nodecode = '101007002' and isdel=0 and flowstate=99  order by orders desc";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            StringBuilder cont = new StringBuilder(200);
            if(dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                
                cont.Append("<p class=\"f15\">");
                cont.Append(" <span><img src=\"/UploadFiles/images/" + dt.Rows[i]["BigImg"] + "\" height=\"25\"></span>");
                cont.Append("" + dt.Rows[i]["Title"] + "");
                cont.Append("</p>");
                cont.Append(" <p>" + dt.Rows[i]["ReachIntro"] + "</p>");
                cont.Append("<p>&nbsp;</p>");
                }
                translate = cont.ToString();
                
        
         
            }
        }
    }
}