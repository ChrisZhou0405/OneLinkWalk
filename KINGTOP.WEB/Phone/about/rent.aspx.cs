using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.about
{
    public partial class rent : System.Web.UI.Page
    {
        public string lis = string.Empty;
        public string tabStr = string.Empty;
        public string lisImgStr = string.Empty;
        public string titleStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;
           if(Context.Request.Params.AllKeys.Contains("id"))
           {
               id = Context.Request.Params["id"].ToString();
           }
            dt(id);
        }

        void dt(string id)
        {
            string sql = "select ID,Title,RetalIntro,BigImg,listimage from K_U_Rental where NodeCode='101007004' and IsDel = 0 and FlowState = 99 order by orders asc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            getLis(dt,id);

        }
       
        void getLis(DataTable dt,string id)
        {
            int ii = 0;
            if(dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    if(dt.Rows[i]["ID"].ToString()==id)
                    {
                        ii = i;
                    }
                    titleStr = dt.Rows[ii]["Title"].ToString();
                    lis += "<li id=\""+dt.Rows[i]["ID"]+"\">" + dt.Rows[i]["Title"] + "</li>";
                }
                Gettab(dt,ii);
                lisImg(dt,ii);
            }
            


          
        }
        void Gettab(DataTable dt,int ii)
        {
            StringBuilder c = new StringBuilder(200);
          
            c.Append(" <p><img src=\"/uploadfiles/images/" + dt.Rows[ii]["BigImg"] + "\"></p>");
            c.Append(" <div class=\"con lxus\">");
            c.Append("  <p>&nbsp;</p>");
            c.Append("" + dt.Rows[ii]["RetalIntro"] + "");
            c.Append("</div>");
            tabStr = c.ToString();
         
       
                 
        }
        void lisImg(DataTable dt,int ii)
        {
            string s = dt.Rows[ii]["listimage"].ToString();
            lisImgStr = GetListIMG(s);
            
        }
         public string GetListIMG(string listimg)
        {
            string result = string.Empty;
            string stylePd = string.Empty;
            string[] list = listimg.Replace("$$$", ",").Split(',');
            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    
                    result += "<li><img src='/uploadfiles/images/" + list[i] + "'/></li>";
                }
            }
            return result;
        }
    }
}