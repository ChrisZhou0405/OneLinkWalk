using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using KingTop.Common;
using KingTop.Model;
using KingTop.BLL.SysManage;
using KingTop.Web.Admin;
using System.Data;
using System.Text;

namespace KingTop.WEB.SysAdmin.SysManage
{
    /*===========================================================================
   // Copyright (C) 2010 华强北在线 
   // 作者：肖丹
   // 创建日期：2010-03-25
   // 功能描述：模块节点列表

   // 更新日期        更新人      更新原因/内容
   //
   ===========================================================================*/

    public partial class ModelNodeList : AdminPage
    {
        protected string strMenuStr = "";
        protected string strMenu = "",strNo="";
        protected ModuleNode BllModuleNode = new ModuleNode();
        public static DataTable DtNode;

        protected void Page_Load(object sender, EventArgs e)
        {
            ////判断是否有当前操作的新建权限           
            if (!IsHaveRightByOperCode("New"))
            {
                btnNew.Visible = false;
            }
            OnDel();
            if (!IsPostBack)
            {
                Utils.SetVisiteList(SystemConst.COOKIES_PAGE_KEY, Session.SessionID, Utils.GetUrlParams().Replace("&", "|"), SystemConst.intMaxCookiePageCount); //把当前url全部参数存入cookie中       
                StringBuilder sb = new StringBuilder();
                sb.Append("\n<script Type='text/javascript'>").Append("\n");
                sb.Append("<!--").Append("\n");
                sb.Append("d = new dTree('d');").Append("\n");
                sb.Append("d.config.closeSameLevel=true;");

                //读取数据 
                KingTop.BLL.SysManage.ModuleNode bll = new KingTop.BLL.SysManage.ModuleNode();
                DataTable dt = bll.GetList("SITESYS", Utils.getOneParams(this.SiteID.ToString()));

                if (dt != null)
                {
                    try
                    {
                        string stremp2 = dt.Rows.Count.ToString();
                        string parentid = "";
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (dr["NodeCode"].ToString().Length <= 3)
                            {
                                parentid = "-1";
                            }
                            else
                            {
                                parentid = dr["NodeCode"].ToString().Substring(0, dr["NodeCode"].ToString().Length - 3);
                            }
                            string strPer = "&nbsp;&nbsp;";
                            if (dr["NodeType"].ToString () == "1")
                            {
                                strPer += "<a HREF=ModelNodeEdit.aspx?Action=New&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&IsParent=" + dr["NodeType"] + " title=添加子栏目><img SRC=../images/folder.gif border=0></a>&nbsp;&nbsp;";
                            }
                            strPer += "<a HREF=ModelNodeEdit.aspx?Action=Edit&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&IsParent=" + dr["NodeType"] + " title=修改><img SRC=../images/pen.gif border=0></a>";
                            strPer += "<a HREF=ModelNodeList.aspx?Action=Del&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&ColumnType=" + dr["ColumnType"] + "&IsParent=" + dr["NodeType"] + " title=删除 onclick=\"selfconfirm({msg:\\'确定要执行删除操作吗？\\',fn:function(data){setAction1(data,\\'" + dr["NodeID"] + "\\',\\'" + dr["NodeCode"] + "\\')}});return false;\"><img SRC=../images/DTree/Del.gif border=0   style=padding-right:5px;padding-left:5px></a>";
                            sb.Append("d.add(").Append(dr["NodeCode"]).Append(",")
                                    .Append(parentid).Append(",'").Append(dr["NodeName"]).Append(strPer)
                                    .Append("');").Append("\n");
                        }
                    }
                    catch (Exception err)
                    {
                        string stremp = err.Message;
                    }
                }

                sb.Append("document.write(d);").Append("\n");
                //打开所有节点     
                sb.Append(" d.openAll();").Append("\n");
                sb.Append("//-->").Append("\n");
                sb.Append("</script>").Append("\n");
                menutree.InnerHtml = sb.ToString();
            }
        }

        private void OnDel()
        {
            if (Request.QueryString["Action"] == "Del")
            {
                string ID = Request.QueryString["NodeID"];
                //判断权限
                if (IsHaveRightByOperCode("Delete"))
                {
                    DataTable dt = BllModuleNode.GetList("ONE", Utils.getOneParams(ID));
                    string nodeName = string.Empty;
                    if (dt.Rows.Count > 0)
                    {
                        nodeName = dt.Rows[0]["NodeName"].ToString().Replace("'", "\\'");
                    }
                    string returnMsg = BllModuleNode.ModuleNodeSet("DEL2", "", ID);
                    try
                    {
                        if (Convert.ToInt32(returnMsg) == -1)
                        {
                            Utils.RunJavaScript(this, "alert({msg:'该栏目包含子栏目，不能直接删除，请先删除其子栏目！',title:'提示信息'})");
                        }
                        else
                        {
                            WriteLog("删除" + nodeName + "节点成功！", "", 2);//写入操作日志
                        }
                    }
                    catch
                    {
                        WriteLog("删除" + nodeName + "节点失败！", returnMsg, 2);//写入操作日志
                        Utils.RunJavaScript(this, "alert({msg:'" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "',title:'提示信息'})");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有删除栏目的权限，请联系站点管理员！',title:'提示信息'})");
                }
            }
        }

        protected void Butnew_Click(object sender, EventArgs e)
        {
            if (IsHaveRightByOperCode("New"))
            {
                Response.Redirect("ModelNodeEdit.aspx?Action=New&NCode=0&NodeCode=" + NodeCode + "&IsFolder=1&NodeId=" + NodeID);
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'你没有新增节点的权限，请联系站点管理员！',title:'提示信息'})");
            }
        }
    }
}
