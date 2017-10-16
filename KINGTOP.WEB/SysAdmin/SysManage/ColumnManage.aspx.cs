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

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线

    作者:      陈顺
    创建时间： 2010年3月31日

    功能描述： 栏目管理
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class ColumnManage : AdminPage
    {
        protected ModuleNode BllModuleNode = new ModuleNode();
        private StringBuilder sb = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            OnDel();
            if (!IsPostBack)
            {
                
                sb.Append("\n<script Type='text/javascript'>").Append("\n");
                sb.Append("<!--").Append("\n");
                sb.Append("d = new dTree('d');").Append("\n");
                sb.Append("d.config.closeSameLevel=true;");

                //读取数据 
                KingTop.BLL.SysManage.ModuleNode bll = new KingTop.BLL.SysManage.ModuleNode();
                DataTable dt = bll.GetList("SITEWEB", Utils.getOneParams(this.SiteID.ToString()));

                if (dt != null)
                {
                    try
                    {
                        CreateTree(dt, "0");
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
                        nodeName = dt.Rows[0]["NodeName"].ToString().Replace("'","\\'");
                    }
                    string returnMsg = BllModuleNode.ModuleNodeSet("DELONE", "", ID);
                    try
                    {
                        if (Convert.ToInt32(returnMsg) == -1)
                        {
                            Utils.RunJavaScript(this, "alert({msg:'该栏目包含子栏目，不能直接删除，请先删除其子栏目！',title:'提示信息'})");
                        }
                        else
                        {
                            WriteLog("删除" +nodeName + "节点成功！", "", 2);//写入操作日志
                        }
                    }
                    catch
                    {
                        WriteLog("删除" + nodeName + "节点失败！", returnMsg, 2);//写入操作日志
                        Utils.RunJavaScript(this, "alert({msg:'"+returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>")+"',title:'提示信息'})");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有删除栏目的权限，请联系站点管理员！',title:'提示信息'})");
                }
            }
        }

        private void CreateTree(DataTable dt,string parentNode)
        {
            string parentid = "";
            DataRow[] dr1 = dt.Select("ParentNode='" + parentNode + "'", "NodelOrder ASC");
            //foreach (DataRow dr in dt.Rows)
            for (int i = 0; i < dr1.Length; i++) 
            {
                DataRow dr = dr1[i];
                if (dr["NodeCode"].ToString().Length <= 3)
                {
                    parentid = "-1";
                }
                else
                {
                    parentid = dr["NodeCode"].ToString().Substring(0, dr["NodeCode"].ToString().Length - 3);
                }

                string strPer = "&nbsp;&nbsp;";
                if (dr["NodeType"].ToString() == "1")
                {
                    strPer += "<a HREF=ColumnEdit.aspx?Action=New&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&IsParent=" + dr["NodeType"] + " title=添加子栏目><img SRC=../images/folder.gif border=0 style=padding-right:5px;padding-left:5px></a><a HREF=SingleColumn.aspx?Action=New&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&IsParent=" + dr["NodeType"] + " title=添加单页栏目><img SRC=../images/menu2.png border=0  style=padding-right:5px></a><a HREF=OutLinkColumn.aspx?Action=New&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&IsParent=" + dr["NodeType"] + " title=添加外部链接栏目><img SRC=../images/menu3.png border=0></a>";
                    strPer += "<a HREF=ColumnEdit.aspx?Action=Edit&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&ColumnType=" + dr["ColumnType"] + "&IsParent=" + dr["NodeType"] + " title=修改><img SRC=../images/pen.gif border=0   style=padding-right:5px;padding-left:5px></a>";
                    strPer += "<a HREF=ColumnManage.aspx?Action=Del&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&ColumnType=" + dr["ColumnType"] + "&IsParent=" + dr["NodeType"] + " title=删除 onclick=\"selfconfirm({msg:\\'确定要执行删除操作吗？\\',fn:function(data){setAction1(data,\\'" + dr["NodeID"] + "\\',\\'" + dr["NodeCode"] + "\\')}});return false;\"><img SRC=../images/DTree/Del.gif border=0   style=padding-right:5px;padding-left:5px></a>";

                    sb.Append("d.add(").Append(dr["NodeCode"]).Append(",")
                            .Append(parentid).Append(",'").Append(dr["NodeName"].ToString ().Replace("'","\\'")).Append(strPer)
                            .Append("');").Append("\n");

                    CreateTree(dt, dr["NodeCode"].ToString());
                }
                else
                {
                    strPer += "<a HREF=ColumnEdit.aspx?Action=Edit&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&ColumnType=" + dr["ColumnType"] + "&IsParent=" + dr["NodeType"] + " title=修改><img SRC=../images/pen.gif border=0   style=padding-right:5px;padding-left:5px></a>";
                    strPer += "<a HREF=ColumnManage.aspx?Action=Del&NodeID=" + NodeID + "&ID=" + dr["NodeID"] + "&NCode=" + dr["NodeCode"] + "&NodeCode=" + NodeCode + "&ColumnType=" + dr["ColumnType"] + "&IsParent=" + dr["NodeType"] + " title=删除 onclick=\"selfconfirm({msg:\\'确定要执行删除操作吗？\\',fn:function(data){setAction1(data,\\'" + dr["NodeID"] + "\\',\\'" + dr["NodeCode"] + "\\')}});return false;\"><img SRC=../images/DTree/Del.gif border=0   style=padding-right:5px;padding-left:5px></a>";
                    sb.Append("d.add(").Append(dr["NodeCode"]).Append(",")
                            .Append(parentid).Append(",'").Append(dr["NodeName"].ToString().Replace("'", "\\'")).Append(strPer)
                            .Append("');").Append("\n");
                }

                
            }
        }
    }
}
