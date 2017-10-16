using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using KingTop.Config;
using KingTop.Common;
using KingTop.Web.Admin;
using System.Collections.Generic;

namespace KingTop.WEB.SysAdmin.Category
{
    public partial class Edit : AdminPage
    {
        protected DataTable dt = null;
        protected string sSpit = string.Empty;
        KingTop.Modules.BLL.Categorys bcategory = new KingTop.Modules.BLL.Categorys();
        KingTop.Modules.Entity.Categorys model = new KingTop.Modules.Entity.Categorys();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        #region 初始化数据
        private void BindData()
        {
            string sId = Utils.RequestStr("id");
            string sParentID = Utils.RequestStr("pid");
            hidID.Value = sId;
            hidParentID.Value = sParentID;
            dt = bcategory.GetCategoryCache();

            InnerCategory("0");
            if (Action.Equals("EDIT") && !string.IsNullOrEmpty(sId) && dt != null && dt.Rows.Count > 0)
            {
                Button1.Text = "修 改";
                DataRow[] dr = dt.Select("ID=" + sId);
                if (dr.Length > 0)
                {
                    txtName.Text = dr[0]["Name"].ToString();
                    ddlParentCategory.SelectedValue = dr[0]["ParentID"].ToString();
                    rblIsIndex.SelectedValue = dr[0]["IsIndex"].ToString();
                    rblIsVaild.SelectedValue = dr[0]["IsValid"].ToString();
                    txtOrders.Text = dr[0]["Orders"].ToString();
                    Editor1.Content = dr[0]["Description"].ToString();
                    txtPageTitle.Text = dr[0]["PageTitle"].ToString();
                    this.txtPageKeywords.Text = dr[0]["PageKeywords"].ToString();
                    this.txtPageDescription.Text = dr[0]["PageDescription"].ToString();
                    txtURLRewriter.Text = dr[0]["URLRewriter"].ToString();
                    txtImg.Text = dr[0]["Img"].ToString();
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(sParentID))
                    ddlParentCategory.SelectedValue = sParentID;
            }
        }
       
        protected string InnerCategory(string sParentID)
        {
            StringBuilder sb = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("ParentID=" + sParentID, " Orders desc");
                for (int i = 0; i < dr.Length; i++)
                {
                    ddlParentCategory.Items.Add(new ListItem(GetDepthStr(Utils.ParseInt(dr[i]["Depth"].ToString(), 0)) + dr[i]["Name"].ToString(), dr[i]["ID"].ToString()));
                    InnerCategory(dr[i]["ID"].ToString());
                }

            }
            return sb.ToString(); ;
        }
        protected string GetDepthStr(int depth)
        {
            string sStr = string.Empty;
            for (int i = 0; i < depth; i++)
            {
                if (i == 0)
                    sStr = "├─";
                else
                    sStr += "─";
            }
            return sStr;
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 确认付款
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            #region 数据操作
            try
            {
                #region 操作变量,权限验证

                int flagType = 0;
                string error = Utils.GetResourcesValue("model", "AddError");
                if (Action == "NEW")
                {
                    if (!IsHaveRightByOperCode("New"))
                    {
                        Utils.AlertMessage(this, "你没有添加权限！");
                        return;
                    }
                }
                else
                {
                    if (!IsHaveRightByOperCode("Edit"))
                    {
                        Utils.AlertMessage(this, "你没有修改权限！");
                        return;
                    }
                }

                #endregion

                #region 获取提交数据
                model.ID = Utils.ParseInt(hidID.Value, 0);

                model.Name = txtName.Text;
                model.ParentID = Utils.ParseInt(ddlParentCategory.SelectedValue, 0);

                if (model.ParentID < 1)
                {
                    model.ArrayParentID = model.ParentID.ToString();
                    model.Depth = 1;
                }
                else
                {
                    dt = bcategory.GetCategoryCache();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow[] dr = dt.Select("ID=" + model.ParentID);
                        if (dr.Length > 0)
                        {
                            model.ArrayParentID = dr[0]["ArrayParentID"].ToString() + "," + model.ParentID;
                            model.Depth = Utils.ParseInt(dr[0]["Depth"].ToString(), 0) + 1;
                            model.ParentName = dr[0]["Name"].ToString();
                        }
                    }
                    else
                        model.Depth = 1;

                }
                model.IsIndex = Utils.ParseInt(rblIsIndex.SelectedValue, 1);
                model.IsValid = Utils.ParseInt(rblIsVaild.SelectedValue, 1);
                model.Orders = Utils.ParseInt(txtOrders.Text, 0);
                model.Description = Editor1.Content;
                model.PageTitle = txtPageTitle.Text;
                model.PageKeywords = txtPageKeywords.Text;
                model.PageDescription = txtPageDescription.Text;
                model.URLRewriter = txtURLRewriter.Text;
                model.Img = txtImg.Text;

                model.IsDel = 0;
                model.SiteID = 1;
                model.AddDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                model.AddMan = LoginInfoHelper.GetLoginAccount(Session, Response).UserName.ToString();
                model.ColumnID = "commodity";//商品分类
                model.NodeCode = KingTop.Common.Utils.CheckSql(Request["NodeCode"]);

                #endregion


                if (Action.Equals("EDIT") && model.ID > 0)
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Name", model.Name);
                    dic.Add("ParentID", model.ParentID.ToString());
                    dic.Add("ParentName", model.ParentName);
                    dic.Add("ArrayParentID", model.ArrayParentID);
                    dic.Add("Orders", model.Orders.ToString());
                    dic.Add("AddDate", model.AddDate.ToString());
                    dic.Add("IsValid", model.IsValid.ToString());
                    dic.Add("IsIndex", model.IsIndex.ToString());
                    dic.Add("Description", model.Description);
                    dic.Add("UpdateDate", model.UpdateDate.ToString());
                    dic.Add ("PageTitle",model.PageTitle);
                    dic.Add ("PageKeywords",model.PageKeywords);
                    dic.Add ("PageDescription",model.PageDescription);
                    dic.Add ("URLRewriter",model.URLRewriter);
                    dic.Add ("Img",model.Img);
                    int counts = InfoHelper.Edit("K_Category", dic, "ID=" + model.ID);
                    if (counts > 0)
                    {
                        bcategory.SetCategoryCache();
                        //更新成功,提示
                        flagType = 1;
                        WriteLog(GetLogValue(model.ID.ToString(), Action, "Orders", true), "", 2);
                        Utils.RunJavaScript(this, "type=" + flagType + "" + ";title='" + Utils.AlertMessage(model.ID.ToString()) + "';id='" + model.ID.ToString() + "';msg();");
                    }
                    else
                    {
                        //并发冲突提示
                        WriteLog(GetLogValue(model.ID.ToString(), Action, "Orders", false), "修改失败!", 3);
                        Utils.RunJavaScript(this, "alert({msg:'保存失败!',title:'提示信息'})");
                    }

                }
                else
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("Name", model.Name);
                    dic.Add("ParentID", model.ParentID.ToString());
                    dic.Add("ParentName", model.ParentName);
                    dic.Add("ArrayParentID", model.ArrayParentID);
                    dic.Add("Orders", model.Orders.ToString());
                    dic.Add("AddMan", model.AddMan);
                    dic.Add("AddDate", model.AddDate.ToString());
                    dic.Add("IsValid", model.IsValid.ToString());
                    dic.Add("IsIndex", model.IsIndex.ToString());
                    dic.Add("SiteID", model.SiteID.ToString());
                    dic.Add("Description", model.Description);
                    dic.Add("IsDel", model.IsDel.ToString());
                    dic.Add("Depth", model.Depth.ToString());
                    dic.Add("UpdateDate", model.UpdateDate.ToString());
                    dic.Add("ColumnID", model.ColumnID);
                    dic.Add("NodeCode", model.NodeCode);
                    dic.Add("PageTitle", model.PageTitle);
                    dic.Add("PageKeywords", model.PageKeywords);
                    dic.Add("PageDescription", model.PageDescription);
                    dic.Add("URLRewriter", model.URLRewriter);
                    dic.Add("Img", model.Img);

                    int counts = InfoHelper.Add1("K_Category", dic);
                    if (counts > 0)
                    {
                        bcategory.SetCategoryCache();
                        //添加成功,提示
                        flagType = 0;
                        WriteLog(GetLogValue(model.ID.ToString(), Action, "Category", true), "", 2);
                        //Utils.UrlRedirect(this.Page, "list.aspx", "添加成功");
                        Utils.RunJavaScript(this, "type=" + flagType + "" + ";title='" + Utils.AlertMessage(model.Name.ToString()) + "';id='" + counts.ToString() + "';msg();");
                    }
                    else
                    {
                        //并发冲突提示
                        WriteLog(GetLogValue(model.ID.ToString(), Action, "Category", false), "添加失败!", 3);
                        Utils.RunJavaScript(this, "alert({msg:'添加失败!',title:'提示信息'})");
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.AlertMessage(this, ex.Message);
            }
            BindData();
            #endregion
        }
        #endregion
    }
}
