using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.shopping
{
    public partial class index : System.Web.UI.Page
    {
        public string PageHtml = string.Empty;
        public string id = string.Empty;
        public string Loacation = string.Empty;
        public string[] ls = new string[] { "1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20","21","22","23","24","25","26"};
        public string[] ls1 = new string[] { "1", "2", "3", "4"};
        public string[] ls2 = new string[] {  "5", "6", "7", "8" };
        public string[] ls3 = new string[] { "9", "10", "11", "12" };
        public string[] ls4 = new string[] { "13", "14", "15", "16" };
        public string[] ls5 = new string[] { "17", "18", "19", "20" };
        public string[] ls6 = new string[] { "21", "22", "23", "24" };
        public string[] ls7 = new string[] { "25", "26" };
       
        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            Meta.NodeCode = "104002";
            if (!Page.IsPostBack)
            {
                if (KingTop.Common.Tools.CheckSql(Request.QueryString["id"]) != "")
                {
                    id = KingTop.Common.Utils.CheckSql(Request.QueryString["id"]);
                }
                if (KingTop.Common.Tools.CheckSql(Request.QueryString["Loacation"]) != "")
                {
                    Loacation = KingTop.Common.Utils.CheckSql(Request.QueryString["Loacation"]);
                }
                //绑定购物指南
                BindIntroduction(id, Loacation);
                //绑定购物指南分类
                BindRelated();
            }
        }

        #endregion

        #region 2.0绑定购物指南分类
        /// <summary>
        /// 绑定购物指南分类
        /// </summary>
        private void BindRelated()
        {
            string sql = string.Empty;
            sql = "select ID,NodeCode,Title,Orders FROM K_U_Category where NodeCode='104002001' and  isdel=0 and flowstate=99 order by orders desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                RptRelated.DataSource = dt;
                RptRelated.DataBind();
            }
        }
        #endregion

        #region 3.0绑定购物指南
        /// <summary>
        /// 绑定购物指南
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Loacation"></param>
        private void BindIntroduction(string id, string Loacation)
        {
            string wherestr = string.Empty;
            #region 绑定数据
            if (id != "")
            {
                wherestr = " and type ='" + id + "'";
            }
            if (Loacation != "")
            {
                wherestr = " and LetterQuery ='" + Loacation + "'";
            }
            //string strSQL = "SELECT distinct(Title),ID,NodeCode,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Floor FROM K_U_CategoryGuide where isdel=0 and flowstate=99 " + wherestr;

            //select * from K_U_CategoryGuide where ID IN ( select min(ID) from  K_U_CategoryGuide where NodeCode='101002002' and FlowState='99' and IsDel='0' and LetterQuery='15' group by Title)
            string strSQL = "select * from K_U_CategoryGuide where ID IN ( select min(ID) from  K_U_CategoryGuide where NodeCode='104002002' and FlowState='99' and IsDel='0'  " + wherestr + " group by Title) ";
            //分页
            int pageSize = 12;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " Orders  asc";
            SqlParameter[] sqlParam;
            string splitTemp = KingTop.Common.Split.SplitHtmlCode;
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
                this.rptlist.DataSource = ds.Tables[1];
                this.rptlist.DataBind();
                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    PageHtml = KingTop.Common.Split.GetHtmlCode("", splitTemp, 2, pageIndex, pageSize, rsCount, false);
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
        #endregion


    }
}