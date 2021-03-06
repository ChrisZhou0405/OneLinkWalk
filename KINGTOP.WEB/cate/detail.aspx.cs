﻿using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.cate
{
    public partial class detail : System.Web.UI.Page
    {

        public string floorId = string.Empty;
        public string sid = string.Empty;
        public DataTable dtdata0 = new DataTable();
        public DataTable dtdata1 = new DataTable();
        public DataTable datalc2 = new DataTable();
        public DataTable datalc3 = new DataTable();
        public DataTable datalc4 = new DataTable();
        public DataTable datalc5 = new DataTable();
        #region 属性
        public string id = string.Empty;
        public string LocationImg = string.Empty;
        public string PageTitle = string.Empty;
        public string PageKeyWords = string.Empty;
        public string PageDescription = string.Empty;
        public string locaiton = string.Empty;
        #endregion

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

        #region 美食荟萃详情
        /// <summary>
        /// 美食荟萃详情
        /// </summary>
        private void BindDetails()
        {
            using (DataTable dt = KingTop.Common.SQLHelper.GetDataSet("SELECT ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Floor,MetaKeyword,MetaDescript FROM K_U_FoodGuide Where  isdel=0  and id='" + id + "' and flowstate=99"))
            {
                if (dt.Rows.Count > 0)
                {
                    rptlist.DataSource = dt;
                    rptlist.DataBind();
                    string floor = dt.Rows[0]["Floor"].ToString();
                    if (floor == "0")
                    {
                        locaiton = "0";
                    }
                    else if (floor == "1")
                    {
                        locaiton = "1";
                    }
                    else if (floor == "2")
                    {
                        locaiton = "2";
                    }
                    else if (floor == "3")
                    {
                        locaiton = "3";
                    }
                    else if (floor == "4")
                    {
                        locaiton = "4";
                    }
                    else if (floor == "5")
                    {
                        locaiton = "5";
                    }
                    floorId = dt.Rows[0]["Floor"].ToString();
                    sid = dt.Rows[0]["ID"].ToString();
                    LocationImg = dt.Rows[0]["LocationImg"].ToString();
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
            sql = "select top 5 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,LikeImg,Orders FROM K_U_FoodGuide where isdel=0 and  len(LikeImg)>0 and ID not in (" + id + ") and flowstate=99 order by orders desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                RptRelated.DataSource = dt;
                RptRelated.DataBind();
            }
          //  sql = "select ID,Title,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003001' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003001' and f.isdel=0 and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc ";
              dtdata0 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dtdata0))
            {
                rptdata0lclist.DataSource = dtdata0;
                rptdata0lclist.DataBind();
            }
          //  sql = "select ID,Title,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003002' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003002' and f.isdel=0 and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc ";
            dtdata1 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dtdata1))
            {
                rptdatalc1list.DataSource = dtdata1;
                rptdatalc1list.DataBind();
            }
           // sql = "select ID,Title,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003003' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003003' and f.isdel=0 and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc ";
            datalc2 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc2))
            {
                rptdatalc2list.DataSource = datalc2;
                rptdatalc2list.DataBind();
            }
          //  sql = "select ID,Title,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003004' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003004' and f.isdel=0 and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc ";
            datalc3 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc3))
            {
                rptdatalc3list.DataSource = datalc3;
                rptdatalc3list.DataBind();
            }
           // sql = "select ID,Title,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003005' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003005' and f.isdel=0 and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc ";
            datalc4 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc4))
            {
                rptdatalc4list.DataSource = datalc4;
                rptdatalc4list.DataBind();
            }
           // sql = "select ID,Title,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003006' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003006' and f.isdel=0 and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc ";
            datalc5 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc5))
            {
                rptdatalc5list.DataSource = datalc5;
                rptdatalc5list.DataBind();
            }
        }
        #endregion

        //f是楼层，dt2是楼层的店铺数据 //   lMapFun(<%=this.floorId%>,<%=this.getDt(""+floorId+"") %>,'2','lc_f_id2');
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
                if (dt2.Rows[i]["cgID"].ToString() == id)
                {
                    return i;
                }
                else if (dt2.Rows[i]["fgID"].ToString() == id)
                {
                    return i;
                }
            }

            return 0;

        }

     
        public string geturl(string cgID, string fgID)
        {

            string url = "";
            if (cgID != "")
            {
            
                // lclink="/shopping/site2.aspx?id=<%#Eval("cID") %>" lclink2="/shopping/detail2.aspx?nid=<%#Eval("cID") %>

                url += "lclink=\"/shopping/site2.aspx?id=" + cgID + "\" lclink2=\"/shopping/detail2.aspx?nid=" + cgID;
            }
            else if (fgID != "")
            {
              
                // lclink="/cate/detail.aspx?nid=<%#Eval("fID") %>"  lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>" 
                url += "lclink=\"/cate/csite.aspx?id=" + fgID + "\"  lclink2=\"/cate/detail.aspx?nid=" + fgID;
            }
            else
            {
               // url += "lclink=\"/cate/nomes.aspx\"  lclink2=\"/cate/nomes.aspx";

                url += "lclink=\"/cate/csite.aspx?id=" + fgID + "\"  lclink2=\"/cate/detail.aspx?nid=" + fgID;
            }
            return url + "\"";
        }

    }
}