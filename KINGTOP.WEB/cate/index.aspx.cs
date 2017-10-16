using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.cate
{
    public partial class index : System.Web.UI.Page
    {
        public string AllFoodPageHtml = string.Empty;
        public string AreaFoodHtml = string.Empty;
        public string ChinaFoodHtml = string.Empty;
        public string EastFoodHtml = string.Empty;
        public string SweetFoodHtml = string.Empty;
        public string sx = string.Empty;
        public string ImgURl = string.Empty;
        public string imgStr = string.Empty;
        public string htmlshow = string.Empty;
        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "101003";
            if(Request.Params.AllKeys.Contains("sx"))
            {
                sx = Request.QueryString["sx"];
            }

          


            if ( sx=="" ||sx == "1" )
            {
                using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Top 1 BigImg from K_U_FoodCategory where ID = '100000000144283' and isdel=0 and flowstate=99  order by orders desc"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        
                        ImgURl = "/UploadFiles/images/" + dt.Rows[0]["BigImg"].ToString();
                        imgStr = "<div class=\"banCate\"><img src=\""+ImgURl+"\" alt=\"\" /></div>";
                    }
                }
            }
            else  if (sx == "2")
            {
                using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Top 1 BigImg from K_U_FoodCategory where ID = '100000000825845' and isdel=0 and flowstate=99  order by orders desc"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        ImgURl = "/UploadFiles/images/" + dt.Rows[0]["BigImg"].ToString();
                        imgStr = "<div class=\"banCate\"><img src=\"" + ImgURl + "\" alt=\"\" /></div>";
                    }
                }

            }
            else  if (sx == "3")
            {
                using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Top 1 BigImg from K_U_FoodCategory where ID = '100000001534222' and isdel=0 and flowstate=99  order by orders desc"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        ImgURl = "/UploadFiles/images/" + dt.Rows[0]["BigImg"].ToString();
                        imgStr = "<div class=\"banCate\"><img src=\"" + ImgURl + "\" alt=\"\" /></div>";
                    }
                }

            }
            else   if (sx == "4")
            {
                using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet(" select Top 1 BigImg from K_U_FoodCategory where ID = '100000002274564' and isdel=0 and flowstate=99  order by orders desc"))
                {
                    if (dt.Rows.Count > 0)
                    {
                        ImgURl = "/UploadFiles/images/" + dt.Rows[0]["BigImg"].ToString();
                        imgStr = "<div class=\"banCate\"><img src=\"" + ImgURl + "\" alt=\"\" /></div>";
                    }
                }
            }



          
            if (!Page.IsPostBack)
            {
                //绑定全部美食荟萃
                BindIntroduction();
                //绑定亚洲美食荟萃
                BindAreaIntro();
                //绑定中式佳肴
                BindChinaFood();
                //绑定西式美食
                BindEastFood();
                //绑定轻便美食
                BindSweetFood();
            }
        }
        #endregion

        #region 2.0绑定全部美食荟萃
        /// <summary>
        ///绑定全部美食荟萃
        /// </summary>
        private void BindIntroduction()
        {
            #region 绑定数据
            string strSQL = "Select * from K_U_FoodGuide Where ID In (Select ID From(SELECT min(ID) as ID ,Title FROM K_U_FoodGuide where isdel=0 and flowstate=99 and NodeCode='101003002' GROUP BY Title HAVING COUNT(*) >0) as T)";
            //分页
            int pageSize = 10;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " Orders asc";
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
                this.RptAllFoodList.DataSource = ds.Tables[1];
                this.RptAllFoodList.DataBind();
                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    AllFoodPageHtml = KingTop.Common.Split.GetHtmlCode("", splitTemp, 2, pageIndex, pageSize, rsCount, false);
                }
                else
                {
                    AllFoodPageHtml = "";
                }
            }
            else
            {
                AllFoodPageHtml = "";
            }
            #endregion

        }
        #endregion

        #region 3.0绑定亚洲美食荟萃
        /// <summary>
        ///绑定亚洲美食荟萃
        /// </summary>
        private void BindAreaIntro()
        {
            #region 绑定数据
            string strSQL = "SELECT ID,NodeCode,Title,type,ShopNo,Orders,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Floor FROM K_U_FoodGuide where NodeCode='101003002' and isdel=0 and flowstate=99 and type ='100000000144283' ";
            //分页
            int pageSize = 10;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " Orders asc";
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
                this.rptAreaFood.DataSource = ds.Tables[1];
                this.rptAreaFood.DataBind();
                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    AreaFoodHtml = KingTop.Common.Split.GetHtmlCode("", splitTemp, 2, pageIndex, pageSize, rsCount, false);
                }
                else
                {
                    AreaFoodHtml = "";
                }
            }
            else
            {
                AreaFoodHtml = "";
            }
            #endregion

        }
        #endregion

        #region 4.0绑定中式佳肴
        /// <summary>
        ///绑定中式佳肴
        /// </summary>
        private void BindChinaFood()
        {
            #region 绑定数据
            string strSQL = "SELECT ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Floor,Orders FROM K_U_FoodGuide where NodeCode='101003002' and isdel=0 and flowstate=99 and type ='100000000825845' ";
            //分页
            int pageSize = 10;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " Orders asc";
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
                this.rptChinaFood.DataSource = ds.Tables[1];
                this.rptChinaFood.DataBind();
                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    ChinaFoodHtml = KingTop.Common.Split.GetHtmlCode("", splitTemp, 2, pageIndex, pageSize, rsCount, false);
                }
                else
                {
                    ChinaFoodHtml = "";
                }
            }
            else
            {
                ChinaFoodHtml = "";
            }
            #endregion

        }
        #endregion

        #region 5.0绑定西式美食
        /// <summary>
        ///绑定西式美食
        /// </summary>
        private void BindEastFood()
        {
            #region 绑定数据
            string strSQL = "SELECT ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Floor,Orders FROM K_U_FoodGuide where NodeCode='101003002' and isdel=0 and flowstate=99 and type ='100000001534222' ";
            //分页
            int pageSize = 10;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " Orders asc";
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
                this.rptEastFood.DataSource = ds.Tables[1];
                this.rptEastFood.DataBind();
                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    EastFoodHtml = KingTop.Common.Split.GetHtmlCode("", splitTemp, 2, pageIndex, pageSize, rsCount, false);
                }
                else
                {
                    EastFoodHtml = "";
                }
            }
            else
            {
                EastFoodHtml = "";
            }
            #endregion

        }
        #endregion

        #region 6.0绑定轻便美食
        /// <summary>
        ///绑定轻便美食
        /// </summary>
        private void BindSweetFood()
        {
            #region 绑定数据
            string strSQL = "SELECT ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Floor,Orders FROM K_U_FoodGuide  where ID IN(( select min(ID) from  K_U_FoodGuide where NodeCode='101003002' and FlowState='99' and IsDel='0' and type ='100000002274564'  group by Title)) ";
            //分页
            int pageSize = 10;
            int pageIndex = 1;
            int rsCount = 0;
            string order = " Orders asc";
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
                this.rptSweetFood.DataSource = ds.Tables[1];
                this.rptSweetFood.DataBind();
                rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
                if (rsCount > 0)
                {
                    SweetFoodHtml = KingTop.Common.Split.GetHtmlCode("", splitTemp, 2, pageIndex, pageSize, rsCount, false);
                }
                else
                {
                    SweetFoodHtml = "";
                }
            }
            else
            {
                SweetFoodHtml = "";
            }
            #endregion

        }
        #endregion
    }
}