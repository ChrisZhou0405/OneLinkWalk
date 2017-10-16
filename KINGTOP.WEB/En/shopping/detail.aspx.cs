using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.En.shopping
{
    public partial class detail : System.Web.UI.Page
    {
        public string locaiton = string.Empty;
        public string floorId = string.Empty;
        public string id = string.Empty;
        public string LocationImg = string.Empty;
        public string PageTitle = string.Empty;
        public string PageKeyWords = string.Empty;
        public string PageDescription = string.Empty;
        public string sid = string.Empty;
        public DataTable dtdata0 = new DataTable();
        public DataTable dtdata1 = new DataTable();
        public DataTable datalc2 = new DataTable();
        public DataTable datalc3 = new DataTable();
        public DataTable datalc4 = new DataTable();
        public DataTable datalc5 = new DataTable();
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (KingTop.Common.Tools.CheckSql(Request.QueryString["nid"]) != "")
            {
                id = KingTop.Common.Utils.CheckSql(Request.QueryString["nid"]);
            }
            if (!IsPostBack)
            {
                BindDetails();
                BindRelated();
            }
        }
        #endregion
        #region 购物指南详情
        /// <summary>
        /// 购物指南详情
        /// </summary>
        private void BindDetails()
        {
            using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet("SELECT Floor,ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Orders,MetaKeyword,MetaDescript,Floor FROM K_U_CategoryGuide Where  NodeCode='104002002' and  isdel=0 and id='" + id + "' and flowstate=99"))
            {
                if (dt.Rows.Count > 0)
                {
                    rptlist.DataSource = dt;
                    rptlist.DataBind();

                    LocationImg = dt.Rows[0]["LocationImg"].ToString();
                    sid = dt.Rows[0]["ID"].ToString();
                    floorId = dt.Rows[0]["Floor"].ToString();
                    PageTitle = dt.Rows[0]["Title"].ToString();
                    PageKeyWords = dt.Rows[0]["MetaKeyword"].ToString();
                    PageDescription = dt.Rows[0]["MetaDescript"].ToString();
                }
            }
        }
        #endregion
        #region 绑定多图
        /// <summary>
        /// 图片列表数据
        /// </summary>
        /// <param name="NodeCode"></param>
        public string GetListIMG(string listimg)
        {
            string result = string.Empty;
            string stylePd = string.Empty;
            string[] list = listimg.Replace("$$$", ",").Split(',');
            if (list.Length > 0)
            {
                for (int i = 0; i < list.Length; i++)
                {
                    result += "<li><img src='/uploadfiles/images/" + list[i] + "' /></li>";
                }
            }
            return result;
        }
        #endregion
        #region 绑定你可能还会喜欢
        /// <summary>
        /// 绑定相关资讯
        /// </summary>
        private void BindRelated()
        {
            string sql = string.Empty;
            sql = "select top 5 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,LikeImg,Orders,MetaKeyword,MetaDescript FROM K_U_CategoryGuide where isdel=0  and len(LikeImg)>0 and ID not in (" + id + ") and NodeCode='104002002' and flowstate=99 order by orders desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                RptRelated.DataSource = dt;
                RptRelated.DataBind();
            }
            sql = "select f.Title,ShopNo,c.ID as cID,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from K_U_Floorguide as f  left join K_U_CategoryGuide as c  on f.lcnum =c.ShopNo where f.NodeCode='104002003001' and f.IsDel='0'  and f.flowstate='99' order by f.Orders desc";
            DataTable dtdata0 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dtdata0))
            {
                rptdata0lclist.DataSource = dtdata0;
                rptdata0lclist.DataBind();
            }
            sql = "sselect f.Title,ShopNo,c.ID as cID,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from K_U_Floorguide as f  left join K_U_CategoryGuide as c  on f.lcnum =c.ShopNo where f.NodeCode='104002003002' and f.IsDel='0'  and f.flowstate='99' order by f.Orders desc";
            DataTable datalc = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc))
            {
                rptdatalc1list.DataSource = datalc;
                rptdatalc1list.DataBind();
            }
            sql = "select f.Title,ShopNo,c.ID as cID,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from K_U_Floorguide as f  left join K_U_CategoryGuide as c  on f.lcnum =c.ShopNo where f.NodeCode='104002003003' and f.IsDel='0'  and f.flowstate='99' order by f.Orders desc";
            DataTable datalc2 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc2))
            {
                rptdatalc2list.DataSource = datalc2;
                rptdatalc2list.DataBind();
            }
            sql = "select f.Title,ShopNo,c.ID as cID,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from K_U_Floorguide as f  left join K_U_CategoryGuide as c  on f.lcnum =c.ShopNo where f.NodeCode='104002003004' and f.IsDel='0'  and f.flowstate='99' order by f.Orders desc";
            DataTable datalc3 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc3))
            {
                rptdatalc3list.DataSource = datalc3;
                rptdatalc3list.DataBind();
            }
            sql = "select f.Title,ShopNo,c.ID as cID,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from K_U_Floorguide as f  left join K_U_CategoryGuide as c  on f.lcnum =c.ShopNo where f.NodeCode='104002003005' and f.IsDel='0'  and f.flowstate='99' order by f.Orders desc";
            DataTable datalc4 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc4))
            {
                rptdatalc4list.DataSource = datalc4;
                rptdatalc4list.DataBind();
            }
            sql = "select f.Title,ShopNo,c.ID as cID,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from K_U_Floorguide as f  left join K_U_CategoryGuide as c  on f.lcnum =c.ShopNo where f.NodeCode='104002003006' and f.IsDel='0'  and f.flowstate='99' order by f.Orders desc";
            DataTable datalc5 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc5))
            {
                rptdatalc5list.DataSource = datalc5;
                rptdatalc5list.DataBind();
            }
        }
        #endregion
        //f是楼层，dt2是楼层的店铺数据
        public int getDt(string f)
        {
            DataTable dt2 = new DataTable();
            switch (f)
            {
                case "0":
                    dt2 = dtdata0;
                    break;
                case "1":
                    dt2 = dtdata1;
                    break;
                case "2":
                    dt2 = datalc2;
                    break;
                case "3":
                    dt2 = datalc3;
                    break;
                case "4":
                    dt2 = datalc4;
                    break;
                case "5":
                    dt2 = datalc5;
                    break;


            }



            string id = sid;
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows[i]["cID"].ToString() == id)
                {
                    return i;
                }
            }

            return 0;

        }
    }
}