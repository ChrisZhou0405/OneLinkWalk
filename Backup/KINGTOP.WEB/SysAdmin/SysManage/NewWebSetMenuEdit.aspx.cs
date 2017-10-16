
using System;
using System.Data;
using KingTop.BLL.SysManage;
using KingTop.Common;
using KingTop.Web.Admin;

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class NewWebSetMenuEdit : AdminPage
    {
        ModuleNode bllModuleNode = new ModuleNode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OnDel();
                PageInit();
            }
        }

        private void OnDel()
        {
            if (Request.QueryString["act"] == "d")
            {
                string ID = Request.QueryString["id"];
                string returnMsg = bllModuleNode.ModuleNodeSet("DEL", "", ID);
                if (returnMsg == "-1")
                {
                    Utils.RunJavaScript(this, "alert({msg:'栏目删除失败，可能该栏目存在子栏目，应先删除子栏目',title:'提示信息'})");
                }
            }
        }

        private void PageInit()
        {
            BLL.SysManage.ModuleNode obj = new ModuleNode();
            string parentNodeCode=Request.QueryString ["parentNodeCode"];
            string strSql = "select NodeID,NodeCode,NodeName,NodelEngDesc,NodeDir,SubDomain,LinkURL,PageTitle,Meta_Keywords,Meta_Description from K_SysModuleNode Where NodeCode like '" + parentNodeCode + "%' and NodeCode<>'" + parentNodeCode + "' order by NodeCode";
            
            DataTable dt=SQLHelper .GetDataSet (strSql );
            rptInfo.DataSource = dt;
            rptInfo.DataBind();

            string nodeCodeList = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(nodeCodeList))
                {
                    nodeCodeList = dt.Rows[i]["NodeCode"].ToString();
                }
                else
                {
                    nodeCodeList += "," + dt.Rows[i]["NodeCode"].ToString();
                }
            }
            hidNodeCodeList.Value = nodeCodeList;
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            string nodeCodeList = Request.Form["hidNodeCodeList"];
            if (!string.IsNullOrEmpty(nodeCodeList))
            {
                string[] nodeCodeArr = nodeCodeList.Split(',');
                string strSql = string.Empty;
                for (int i = 0; i < nodeCodeArr.Length; i++)
                {
                    if (!string.IsNullOrEmpty(nodeCodeArr[i]))
                    {
                        string NodeName = Request.Form[nodeCodeArr[i] + "_1"].Replace ("'","''");
                        string NodelEngDesc = Request.Form[nodeCodeArr[i] + "_2"].Replace ("'","''");
                        string NodeDir = Request.Form[nodeCodeArr[i] + "_3"].Replace ("'","''");
                        string SubDomain = Request.Form[nodeCodeArr[i] + "_4"].Replace ("'","''");
                        string LinkURL = Request.Form[nodeCodeArr[i] + "_5"].Replace ("'","''");
                        string PageTitle = Request.Form[nodeCodeArr[i] + "_6"].Replace ("'","''");
                        string Meta_Keywords = Request.Form[nodeCodeArr[i] + "_7"].Replace ("'","''");
                        string Meta_Description = Request.Form[nodeCodeArr[i] + "_8"].Replace ("'","''");
                        strSql+="update K_SysModuleNode SET NodeName='"+NodeName+"',NodelEngDesc='"+NodelEngDesc+"',NodeDir='"+NodeDir+"',SubDomain='"+SubDomain+"',LinkURL='"+LinkURL+"',PageTitle='"+PageTitle+"',Meta_Keywords='"+Meta_Keywords+"',Meta_Description='"+Meta_Description+"' WHERE NodeCode='"+nodeCodeArr[i]+"';";
                    }
                }
                if (string.IsNullOrEmpty(strSql))
                    return;

                try
                {
                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
                    PageInit();
                    Utils.RunJavaScript(this, "alert({msg:'栏目修改成功',title:'提示信息'})");
                }
                catch (Exception ex)
                {
                    Utils.RunJavaScript(this, "alert({msg:'修改失败：" + ex.Message.Replace("'", "\'").Replace("\r\n", "<br>").Replace("\r", "<br>").Replace("\n", "<br>") + "',title:'提示信息'})");
                }
            }
        }
    }
}
