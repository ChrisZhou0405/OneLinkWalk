using KingTop.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KingTop.WEB.shopping
{
    public partial class site : System.Web.UI.Page
    {
        public string locaiton = string.Empty;
       public  DataTable dtdata0 = new DataTable();
       public DataTable dtdata1 = new DataTable();
       public DataTable datalc2 = new DataTable();
       public DataTable datalc3 = new DataTable();
       public DataTable datalc4 = new DataTable();
       public DataTable datalc5 = new DataTable();
        #region 1.0Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Meta.NodeCode = "101002";
            if (Request.Params.AllKeys.Contains("locaiton"))
            {
                locaiton = Request.Params["locaiton"].ToString();
            }
            else
            {
                locaiton = "0";
            }
            //if (KingTop.Common.Tools.CheckSql(Request.QueryString["locaiton"]) != "")
            //{
            //    string wherestr = string.Empty;
            //    locaiton = KingTop.Common.Utils.CheckSql(Request.QueryString["locaiton"]);
            //    if (locaiton =="0")
            //    {
            //        wherestr = " and Floor ='0'";
            //    }
            //    else if (locaiton == "1")
            //    {
            //        wherestr = " and Floor ='1'";
            //    }
            //    else if (locaiton == "2")
            //    {
            //        wherestr = " and Floor ='3'";
            //    }
            //    else if (locaiton == "3")
            //    {
            //        wherestr = " and Floor ='4'";
            //    }
            //    else if (locaiton == "4")
            //    {
            //        wherestr = " and Floor ='5'";
            //    }
            //    else if (locaiton == "5")
            //    {
            //        wherestr = " and Floor ='6'";
            //    }
            //    string sql = string.Empty;
            //    sql = "SELECT Top 1 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Orders FROM K_U_CategoryGuide where isdel=0 and flowstate=99" + wherestr + " order by orders desc";
            //    DataTable dt = SQLHelper.GetDataSet(sql);
            //    if (Utils.CheckDataTable(dt))
            //    {
            //        rptlist.DataSource = dt;
            //        rptlist.DataBind();
            //    }
            //}
            if (!Page.IsPostBack)
            {
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
            sql = "select ID,NodeCode,Title,Orders FROM K_U_Category where NodeCode='101002001' and  isdel=0 and flowstate=99 order by orders desc";
            DataTable dt = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dt))
            {
                RptRelated.DataSource = dt;
                RptRelated.DataBind();
            }
         //   sql = "select ID,Title,NodeCode,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003001' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords,cg.type as cgType from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003001' and f.isdel=0 and (cg.isdel=0 or cg.isdel is NULL) and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc  ";
             dtdata0 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dtdata0))
            {
                rptdata0lclist.DataSource = dtdata0;
                rptdata0lclist.DataBind();
            }
           // sql = "select ID,Title,NodeCode,lcnum,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003002' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords,cg.type as cgType from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003002' and f.isdel=0 and (cg.isdel=0 or cg.isdel is NULL) and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc  ";
            dtdata1 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(dtdata1))
            {
                rptdatalc1list.DataSource = dtdata1;
                rptdatalc1list.DataBind();
            }
           // sql = "select ID,Title,lcnum,NodeCode,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003003' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords,cg.type as cgType from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003003' and f.isdel=0 and (cg.isdel=0 or cg.isdel is NULL) and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc  ";
          
            datalc2 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc2))
            {
                rptdatalc2list.DataSource = datalc2;
                rptdatalc2list.DataBind();
            }

           // sql = "select ID,Title,lcnum,NodeCode,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003004' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords,cg.type as cgType from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003004' and f.isdel=0 and (cg.isdel=0 or cg.isdel is NULL) and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc  ";
            datalc3 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc3))
            {
                rptdatalc3list.DataSource = datalc3;
                rptdatalc3list.DataBind();
            }
          //  sql = "select ID,Title,lcnum,NodeCode,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003005' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords,cg.type as cgType from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003005' and f.isdel=0 and (cg.isdel=0 or cg.isdel is NULL) and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc  ";
             datalc4 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc4))
            {
                rptdatalc4list.DataSource = datalc4;
                rptdatalc4list.DataBind();
            }
           // sql = "select ID,Title,lcnum,NodeCode,lcoverimgurl,lcx,lcy,lclink,lccoords FROM K_U_Floorguide where NodeCode ='101002003006' and isdel=0 and flowstate=99 order by orders desc";
            sql = " select f.NodeCode,fg.NodeCode, cg.ID as cgID,cg.Title as cgTitle,fg.ID as fgID,fg.Title as fgTitle ,f.Title,f.ID,f.lcnum,f.NodeCode,f.lcoverimgurl,f.lcx,f.lcy,f.lclink,f.lccoords,cg.type as cgType from  K_U_Floorguide as f  left join K_U_CategoryGuide as cg on f.lcnum=cg.ShopNo left join K_U_FoodGuide as fg on f.lcnum=fg.ShopNo where f.NodeCode='101002003006' and f.isdel=0 and (cg.isdel=0 or cg.isdel is NULL) and f.flowstate=99 and (cg.NodeCode='101002002' or fg.NodeCode='101003002') order by f.orders desc  ";
             datalc5 = SQLHelper.GetDataSet(sql);
            if (Utils.CheckDataTable(datalc5))
            {
                rptdatalc5list.DataSource = datalc5;
                rptdatalc5list.DataBind();
            }        
        }
        #endregion

        //f是楼层，dt2是楼层的店铺数据  init(0,<%=this.getDt("0",dtdata0) %>,'2','lc_f_id2')
        public  int  getDt(string f,DataTable dt2)
        {


            //string sql = "  SELECT Top 1 ID,NodeCode,Title,type,ShopNo,Stereogram,LocationImg,TelPhone,SalesPro,SiteURL,IntroDetail,Shopshow,ShopLogo,Orders FROM K_U_CategoryGuide where isdel=0 and flowstate=99 and Floor ='" + f + "' order by orders desc";
            //var dt = SQLHelper.GetDataSet(sql);
            //if (Utils.CheckDataTable(dt))
            //{
            //    string id = dt.Rows[0]["ID"].ToString();
            //    for (int i = 0; i < dt2.Rows.Count; i++)
            //    {
            //        if (dt2.Rows[i]["cID"].ToString() == id)
            //        {
            //            return i;
            //        }
            //    }
            //}
            return 0;
 
        }


        public string geturl(string cgID, string fgID, string cgType)
        {
          
            string url = "";
            if (cgID != "")
            {
                // lclink="/shopping/site2.aspx?id=<%#Eval("cID") %>" lclink2="/shopping/detail2.aspx?nid=<%#Eval("cID") %>
         
                url += "lclink=\"/shopping/site2.aspx?id="+cgID+ "&nid=" + cgType+"\" lclink2=\"/shopping/detail2.aspx?nid=" + cgID+ "&id=" + cgType;
            }
            else if (fgID != "")
            {
                // lclink="/cate/detail.aspx?nid=<%#Eval("fID") %>"  lclink2="/cate/detail.aspx?nid=<%#Eval("fID") %>" 
                url += "lclink=\"/cate/csite.aspx?id=" + fgID + "&nid=" + cgType + "\"  lclink2=\"/cate/detail.aspx?nid=" + fgID + "&id=" + cgType;
            }
            else
            {
               // url += "lclink=\"/cate/nomes.aspx\"  lclink2=\"/cate/nomes.aspx";
                url += "lclink=\"/shopping/site2.aspx?id=" + cgID + "&nid=" + cgType + "\" lclink2=\"/shopping/detail2.aspx?nid=" + cgID + "&id=" + cgType;
            }
            return url+"\"";
        }
    }
}