using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.vip
{
    public partial class vip : System.Web.UI.Page
    {
        public string lisStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            lis();
        }
        void lis()
        {
            string sql = "select Title,BigImg,Links from K_U_Banner where nodecode = '101005001001001' and isdel=0 and flowstate=99  order by orders desc";
            var dt = SQLHelper.GetDataSet(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lisStr += " <li><a href=\"" + dt.Rows[i]["Links"] + "\"><img src=\"/UploadFiles/images/" + dt.Rows[i]["BigImg"] + "\"></a></li>";

            }
        }
    }
}