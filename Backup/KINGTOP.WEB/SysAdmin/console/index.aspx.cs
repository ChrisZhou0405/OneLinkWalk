using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using KingTop.BLL.SysManage;
using KingTop.Common;
using KingTop.Web.Admin;

#region 版权注释
/*================================================================
    Copyright (C) 2010 图派科技

    作者:      阿波
    创建时间： 2010年4月23日

    功能描述： 头部菜单
 
// 更新日期        更新人      更新原因/内容
// 5月10日         陈顺         套新模板
--===============================================================*/
#endregion


namespace KingTop.WEB.SysAdmin.console
{
    public partial class index1 : AdminPage
    {
       
        ModuleNode objMNode = new ModuleNode();
        protected string strNode = "";
        protected string strNodes = "";
        protected string strUserGrop = "";
        protected string userid = "0";
        protected int siteid = 0;
        protected string stringdata = "";
        private DataTable dt;
        private StringBuilder sb = new StringBuilder();
        private int arrNum = 1;
        public string modulecode = string.Empty;
        public string dataArr = string.Empty;
        public int j = 0;
        public string strSite = "<select name=\"selectSite\" onchange=\"location.href=this.value;\" style=\"width:80px;\">";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["siteid"]))
            {
                SiteID = Utils.ParseInt(Request.QueryString["siteid"], SiteID);
                SiteDir = Request.QueryString["sitedir"];
                Response.Redirect("index.aspx");
                Response.End();
            }
            dt = objMNode.GetModeNodeFromCache();
            userid = GetLoginAccountId();
            BindSite();
            if (!Page.IsPostBack)
            {
                siteid = SiteID;
                Bind();
                GetUserGropName();
                stringdata = sb.ToString();
            }
        }


        private void BindSite()
        {
            KingTop.BLL.SysManage.SysWebSite bllSysWebSite = new KingTop.BLL.SysManage.SysWebSite();
            DataTable dt = bllSysWebSite.GetList("USERIDSITE", Utils.getOneParams(userid));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (int.Parse(dt.Rows[i]["SiteID"].ToString()) == SiteID)
                    strSite += "<option value=\"index.aspx?siteid=" + dt.Rows[i]["SiteID"].ToString() + "&sitedir="+ dt.Rows[i]["Directory"].ToString()+"\" selected>" + dt.Rows[i]["SiteName"].ToString() + "</option>";
                else
                    strSite += "<option value=\"index.aspx?siteid=" + dt.Rows[i]["SiteID"].ToString() + "&sitedir=" + dt.Rows[i]["Directory"].ToString() + "\">" + dt.Rows[i]["SiteName"].ToString() + "</option>";

            }
            strSite += "</select>";
        }

        private void Bind()
        {
            DataTable dtNode = objMNode.GetModeNodeFromCache();
            DataRow[] dr;
            KingTop.BLL.SysManage.Top top = new KingTop.BLL.SysManage.Top();

            //公告  201008027         胡志瑶       1.加上长度
            //rptMessage.DataSource = top.GetMessage(3, siteid.ToString(), UserNo, userid);
            //rptMessage.DataBind();

            dr = dtNode.Select("WebSiteID=" + siteid + " AND ParentNode='0'", "NodelOrder asc,NodeCode asc");
            for (int i = 0; i < dr.Length; i++)
            {
                if (userid == "0" || HasLeftMenuRights(dr[i]["NodeCode"].ToString()))
                {
                    string strIcon = dr[i]["NodelIcon"].ToString() == "" ? "../img/sicon.gif" : dr[i]["NodelIcon"].ToString();
                    strNode = strNode + "<li><a style='color:#fff' href='javascript:treecontent(" + j + ");'  title='" + dr[i]["NodeName"].ToString() + "'>" + dr[i]["NodeName"].ToString() + "</a></li>";
                    //if(i<5)
                    strNodes = strNodes + "<div id=\"treecontent" + j + "\" title=\"" + dr[i]["NodeName"].ToString() + "\"><div style=\"cursor:pointer; padding-top: 5px; \" id=\"tree" + (j + 1) + "\"></div></div>";
                    modulecode = dr[i]["NodeCode"].ToString();
                    CreateSubTree(dr[i]["NodeCode"].ToString(), "data" + j);
                    dataArr += "var data" + j + " = new Array();";
                    j++;
                }
            }
        }

       

        private void BindChild(string NodeCode, DataTable dt)
        {
            //DataTable dtMNode = objMNode.GetList("GETPARENTNODE", Utils.getTwoParams(NodeCode, SiteID.ToString()));
            DataRow[] dr = dt.Select("ParentNode='" + NodeCode + "' and WebSiteID=" + siteid, "NodelOrder asc,NodeCode asc");
            strNode = strNode + "<ul style='display:none;'>";
            for (int k = 0; k < dr.Length; k++)
            {
                if (userid == "0" || HasLeftMenuRights(dr[k]["NodeCode"].ToString()))
                {
                    strNode = strNode + "<li><a href='left.aspx?modulecode=" + dr[k]["NodeCode"] + "' target='frameLeft'>" + dr[k]["NodeName"].ToString() + "</a></li>";
                }
            }
            strNode = strNode + "</ul></li>";
        }

        /// <summary>
        /// 递归生成根编号为NoId的树
        /// </summary>
        /// <param Name="NoId">所要生成子树的根节点</param>
        private void CreateSubTree(string NoId, string DataName)
        {
            DataRow[] dr2 = dt.Select("NodeCode='" + NoId + "'");
            if (dr2.Length > 0)
            {
                if (HasLeftMenuRights(NoId) || int.Parse(userid) == 0)
                {
                    DataRow[] dr1 = dt.Select("ParentNode='" + NoId + "' AND IsLeftDisplay=1", "NodelOrder asc,NodeCode asc");
                    if (dr1.Length > 0)
                    {
                        if (NoId == modulecode.Substring(0, 3))
                        {
                            CreateSubTree(dr1, DataName);
                        }
                        else
                        {
                            if (dr2[0]["parentnode"].ToString() == modulecode)
                                arrNum = 1;

                            sb.Append("" + DataName + ".push({ text: '" + dr2[0]["NodeName"].ToString ().Replace ("'","\\'").Replace ("\"","\\\"") + "', treelevel: " + arrNum + ", isleaf: 0 });");

                            arrNum++;

                            CreateSubTree(dr1, DataName);
                            arrNum--;
                        }
                    }
                    else
                    {
                        if (dr2[0]["ModuleUrl"].ToString().IndexOf('?') > 0)
                        {
                            sb.Append("" + DataName + ".push({ text: '" + dr2[0]["NodeName"].ToString().Replace("'", "\\'").Replace("\"", "\\\"") + "', treelevel: " + arrNum + ", isleaf: 1, tabid: '" + dr2[0]["NodeCode"] + "', url: '" + dr2[0]["ModuleUrl"] + "&NodeCode=" + dr2[0]["NodeCode"] + "&IsFirst=1' });");
                        }
                        else
                        {
                            sb.Append("" + DataName + ".push({ text: '" + dr2[0]["NodeName"].ToString().Replace("'", "\\'").Replace("\"", "\\\"") + "', treelevel: " + arrNum + ", isleaf: 1, tabid: '" + dr2[0]["NodeCode"] + "', url: '" + dr2[0]["ModuleUrl"] + "?NodeCode=" + dr2[0]["NodeCode"] + "&IsFirst=1' });");
                        }
                    }
                }
                else if (modulecode == NoId)
                {
                    modulecode = null;
                }
            }
        }

        void CreateSubTree(DataRow[] dr1, string DataName)
        {
            for (int i = 0; i < dr1.Length; i++)
            {
                if (modulecode == null)
                    modulecode = dr1[i]["NodeCode"].ToString();



                CreateSubTree(dr1[i]["NodeCode"].ToString(), DataName);//递归
            }
        }

        private void GetUserGropName()
        {
            if (userid == "0")
            {
                string xml = Server.MapPath("~/SysAdmin/Configuraion/adminLoginInfo.config");
                strUserGrop = Utils.XmlRead(xml, "/root/UserGropName", "");
            }
            else
            {
                UserGroup bllUserGrop = new UserGroup();
                DataTable dt = bllUserGrop.GetList("LOGINUSERGROP", Utils.getTwoParams(siteid.ToString(), userid)).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    strUserGrop = dt.Rows[0]["UserGroupName"].ToString();
                }
            }
        }
    }
}
