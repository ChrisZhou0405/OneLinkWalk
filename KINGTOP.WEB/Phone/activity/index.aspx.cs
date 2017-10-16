using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.activity
{
    public partial class index : System.Web.UI.Page
    {
        public string lisStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            getDt();
        }

        void getDt()
        {
            string sql = "select Top 4 ID,NodeCode,Title,ActivityTime,TitleImg from K_U_News where nodecode = '101004002' and isdel=0 and flowstate=99  order by AddDate desc";
            var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
            lis(dt);
        }

        void  lis(DataTable dt)
        {
            if(dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    lisStr += "<li id=\""+dt.Rows[i]["ID"]+"\"><a><img src=\"/UploadFiles/Images/" + dt.Rows[i]["TitleImg"] + "\"><span>" + dt.Rows[i]["Title"] + "</span></a></li>";

                }
            }

        }
    }
}