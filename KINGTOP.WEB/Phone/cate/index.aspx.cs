using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.Phone.cate
{
    public partial class index : System.Web.UI.Page
    {
        public string contStr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = string.Empty;
            if(Request.Params.AllKeys.Contains("id"))
            {
                id = Request.Params["id"].ToString();
            }


            BindList(id);
        }



        private void BindList(string id)
        {
            string strsql = string.Empty;
            #region 绑定数据
            string sqlWhere = id == "" ? "" : " and [type]='" + id + "' ";

            strsql = "Select * from K_U_FoodGuide Where ID In (Select ID From(SELECT min(ID) as ID ,Title FROM K_U_FoodGuide where isdel=0 and flowstate=99 and NodeCode='101003002' GROUP BY Title HAVING COUNT(*) >0) as T) "+sqlWhere+" ";
            //fenye
            int pageSize = 10;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " Orders desc ";
            SqlParameter[] sqlParam;
            //string splitTemp = Split.lc0;
            string splitTemp2 = Split.lc2;
            // string pagebar = string.Empty;
            string pagebar2 = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["pg"]))
            {
                pageIndex = Utils.ParseInt(HttpContext.Current.Request.QueryString["pg"], 1);
            }
            sqlParam = new SqlParameter[]{
                new SqlParameter("@NewPageIndex",pageIndex),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@order", order),
                new SqlParameter("@strSql", strsql)
                };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_Pager1", sqlParam);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                //数据
                DataTable dt = ds.Tables[1];


                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    // pagebar = Split.GetHtmlCode("", splitTemp, 15, pageIndex, pageSize, rsCount, false);
                    pagebar2 = KingTop.Common.Split.GetHtmlCode("", splitTemp2, 15, pageIndex, pageSize, rsCount, false);

                }
                else
                {
                    //pagebar = string.Empty;
                    pagebar2 = string.Empty;
                }

                cont(dt,pagebar2,id);



            }
            else
            {

                pagebar2 = string.Empty;
            }
            #endregion
        }

        void cont( DataTable dt,string pagebar,string id)
        {
            StringBuilder c = new StringBuilder(200);
            c.Append(" <div class=\"case-about\">");
            c.Append(" <p><img src=\""+getImg(id)+"\"></p>");
            c.Append(" <ul class=\"catelist\">");
            if(dt!=null && dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    c.Append("<li><a href=\"/Phone/cate/detail.aspx?id=" + dt.Rows[i]["ID"] + "\"><img src=\"/UploadFiles/images/" + dt.Rows[i]["ShopLogo"] + "\"></a></li>");
                }
            }
           
            c.Append(" </ul>");
            c.Append(" <div class=\"pages\">");
            c.Append(""+pagebar+"");
            c.Append("</div>");
            c.Append("</div>");
            contStr = c.ToString();

    
        }
         string getImg(string id)
        {
            

            if (id == "")
            {
                return "/UploadFiles/images/pagebanner.jpg";
            }
            else
            {
                string sql = "  select BigImg from K_U_FoodCategory where ID='"+id+"';";
                  var dt = KingTop.Common.SQLHelper.GetDataSet(sql);
                 if(dt!=null && dt.Rows.Count>0)
                 {

                     return "/UploadFiles/images/" + dt.Rows[0]["BigImg"] + "";
                    
                 }
            }
          
           
            return "";
        }
    }
}