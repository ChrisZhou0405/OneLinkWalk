using KingTop.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.activity
{
    public partial class index : System.Web.UI.Page
    {
        public string PageHtml = string.Empty;
        public string sx = string.Empty;
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "104004";
            sx = Request.QueryString["sx"];
            DateTime tnow = DateTime.Now;//现在时间
            Response.Write(tnow);
            ArrayList AlYear = new ArrayList();
            AlYear.Add("Year");
            int i;

            for (i = 2015; i <= int.Parse(tnow.Date.Year.ToString()); i++)
                AlYear.Add(i);
            ArrayList AlMonth = new ArrayList();
            AlMonth.Add("Month");
            for (i = 1; i <= 12; i++)
                AlMonth.Add(i);
           
            if (!this.IsPostBack)
            {
                DDlYear.DataSource = AlYear;
                DDlYear.DataBind();//绑定年
                //选择当前年
              //  DDlYear.SelectedValue = tnow.Year.ToString();
                DDlYear.SelectedValue = "Year";
                DDLMonth.DataSource = AlMonth;
                DDLMonth.DataBind();//绑定月
                //选择当前月
             //   DDLMonth.SelectedValue = tnow.Month.ToString();
                DDLMonth.SelectedValue = "Month";
                BindData();
                BindActivityTop();
                BindActivity();
            }
        }
        #endregion

        #region  绑定Banner
        /// <summary>
        /// 绑定Banner
        /// </summary>
        public void BindData()
        {
            using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Top 3 Title,BigImg,Links from K_U_Banner where nodecode = '104004001001' and isdel=0 and flowstate=99  order by orders desc"))
            {
                if (dt.Rows.Count > 0)
                {
                    rptbanner.DataSource = dt;
                    rptbanner.DataBind();
                    rptli.DataSource = dt;
                    rptli.DataBind();
                }
            }
        }
        #endregion

        #region  绑定活动日志前四条
        /// <summary>
        /// 绑定活动日志前四条
        /// </summary>
        public void BindActivityTop()
        {
            using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet("select Top 4 ID,NodeCode,Title,ActivityTime,TitleImg from K_U_News where nodecode = '104004002' and isdel=0 and flowstate=99  order by AddDate desc"))
            {
                if (dt.Rows.Count > 0)
                {
                    rptPicScroll.DataSource = dt;
                    rptPicScroll.DataBind();
                }
            }
        }
        #endregion

        #region 绑定活动日志
        /// <summary>
        /// 绑定活动日志
        /// </summary>
        private void BindActivity()
        {
            #region 绑定数据
            string strSQL = string.Empty;
            string Year = string.Empty;
            string Month = string.Empty;
            if (DDlYear.SelectedItem.Text != "" && DDLMonth.SelectedIndex.ToString() != "")
            {
                Year = DDlYear.SelectedItem.Text;
                Year = Year == "Year" ? "" : Year;
                Month = DDLMonth.SelectedValue.ToString();
                Month = Month == "Month" ? "" : Month;

            }
            strSQL = sqlStr(Year, Month);
            //string strSQL = "select ID,NodeCode,Title,ActivityTime,TitleImg,AddDate from K_U_News where nodecode = '101004002' and isdel=0 and flowstate=99 ";
            //分页
            int pageSize = 6;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " AddDate desc";
            SqlParameter[] sqlParam;
            string splitTemp = Split.SplitHtmlCode;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["pg"]))
            {
                pageIndex = Utils.ParseInt(HttpContext.Current.Request.QueryString["pg"], 1);
            }
            sqlParam = new SqlParameter[]{
                new SqlParameter("@NewPageIndex",pageIndex),
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@order", order),
                new SqlParameter("@strSql", strSQL)
                };
            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_Pager1", sqlParam);
            if (Utils.CheckDataSet(ds))
            {
                rptNews.DataSource = ds.Tables[1];
                rptNews.DataBind();
                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    PageHtml = Split.GetHtmlCode("", splitTemp, 2, pageIndex, pageSize, rsCount, false);
                }
                else
                {
                    PageHtml = "";
                }
            }
            else
            {
                PageHtml = "";
            }
            #endregion
        }

        private string sqlStr(string Year, string Month)
        {
            //string sql2 = "select ID,NodeCode,Title,ActivityTime,TitleImg,AddDate from K_U_News where nodecode = '101004002' and isdel=0 and flowstate=99 ";
            //if (Year != "")
            //{
            //    sql2 += " and charindex('" + Year + "',ActivityTime)>0 ";
            //}
            //if (Month != "")
            //{
            //    sql2 += " and charindex('" + Month + "',ActivityTime)>0 ";
            //}



            string sql = "select * from K_U_News where IsDel='0' and FlowState='99' and NodeCode='104004002'";
            if (Year != "")
            {
                sql += " and  " + Year + " >=Year(startTime) and " + Year + " <=Year(endTime) ";
            }
            if (Month != "")
            {
                sql += " and "+Month+" >=Month(startTime) and "+Month+" <=Month(endTime) ";
               
            }



            return sql;
        }
        #endregion

        #region 查找往期活动
        /// <summary>
        /// 查找往期活动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            BindActivity();
        }
        #endregion
    }
}