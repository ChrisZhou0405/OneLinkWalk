using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using KingTop.Common;
using KingTop.Model;
using KingTop.BLL.SysManage;
using KingTop.Web.Admin;
using System.Data;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class PublicOperList : AdminPage
    {
        KingTop.Model.Pager p = new KingTop.Model.Pager();
        KingTop.BLL.SysManage.Module objModule = new KingTop.BLL.SysManage.Module();
        StringBuilder sbLog = new StringBuilder(16);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
        }
        private void PageInit()
        {
            SplitDataBind();
            Utils.SetVisiteList(SystemConst.COOKIES_PAGE_KEY, Session.SessionID, Utils.GetUrlParams().Replace("&", "|"), SystemConst.intMaxCookiePageCount); //把当前url全部参数存入cookie中      
            //页面权限管理
            SetRight(this.Page, rptModelList);
        }

        // 分页控件数据绑定
        private void SplitDataBind()
        {
            string sqlStr = "select Title,OperName,IsValid from K_SysPublicOper ";
            SqlParameter[] selParam;
            string order = " OperName ";

            p.Aspnetpage = Split;
            
            selParam = new SqlParameter[]{
                new SqlParameter("@NewPageIndex",p.PageIndex),
                new SqlParameter("@PageSize",p.PageSize),
                new SqlParameter("@order",order),
                new SqlParameter("@strSql",sqlStr)
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_Pager1", selParam);//proc_Pager1

            
            p.RptControls = rptModelList;
            p.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            p.PageData(ds.Tables[1]);

        }

        public void ModelList_Del(object sender, CommandEventArgs e)
        {
            OnDel(e.CommandArgument.ToString());
        }
        void OnDel(string id)
        {
            if (base.IsHaveRightByOperCode("Delete"))
            {
                string sql = "delete K_SysPublicOper where OperName in ('" + id.Replace(",", "','") + "')";
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);

                PageInit();
            }
            else
            {
                sbLog.Append("删除模块失败，没有权限！");
                Utils.RunJavaScript(this, "alert({msg:'删除模块失败，没有权限！',title:'提示信息'})");
            }
        }

        protected void Split_PageChanged(object src, EventArgs e)
        {
            if (base.IsPageChanged())
            {
                SplitDataBind();
            }
        }

        //新增按钮
        protected void btnNew_Click(object sender, EventArgs e)
        {
            if (IsHaveRightByOperCode("New"))
            {
                Response.Redirect("PublicOperEdit.aspx?Action=New&NodeCode=" + NodeCode);
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'你没有新增模块的权限，请联系站点管理员！',title:'提示信息'})");
            }
        }

        //删除按钮
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            OnDel(Request.Form["chkId"].Replace(", ", ","));
        }

       
    }
}
