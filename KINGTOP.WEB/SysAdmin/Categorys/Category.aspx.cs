using System;
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

namespace KingTop.WEB.SysAdmin.ajax
{
    public partial class Category : AdminPage
    {
        KingTop.Modules.BLL.Categorys bcategory = new Modules.BLL.Categorys();
        DataTable dt;
        protected string sList = string.Empty;
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);
            dt = bcategory.GetCategoryCache();
            switch (FAction)
            {
                case "getcategorylist": GetCategoryList(); break;
                case "removeitem": RemoveItem(); break;
                case "showindex": ShowIndex(); break;
                case "showvalid": ShowValid(); break;
                case "updateorders": UpdateOrders(); break;
                case "getselect": GetSelect(); break;
                case "getselectedlist": GetSelectedList(); break;
                case "deletespevalue": DeleteSpeValue();break;
                
            }
        }
        

        #region 绑定扩展分类
        protected void GetSelectedList()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string sCategoryID = Utils.RequestStr("id");
            string sCategoryIDS = Utils.RequestStr("ids");
            string sName = Utils.RequestStr("name");
            if (string.IsNullOrEmpty(sName))
                sName = "CategoryIDS";
            try
            {
                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(sCategoryIDS)&&!string.IsNullOrEmpty(sCategoryID))
                {
                    KingTop.Modules.BLL.Categorys bCategory = new Modules.BLL.Categorys();
                    dt = bcategory.GetCategoryCache();

                    string[] ids = sCategoryIDS.Split(',');
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (ids[i] != sCategoryID)
                        {
                            sb.Append("<span class=\"spancategory\">");
                            sb.AppendFormat("<select name=\"{0}\" class=\"addcategorycss\">", sName);
                            sb.Append("<option value=\"0\">请选择分类</option>");
                            sb.Append(InnerSelectCategory("0", ids[i]));
                            sb.Append("</select>");
                            sb.Append("<a href=\"javascript:\" onclick=\"removeCategory(this)\">删除</a>");
                            sb.Append("</span>");
                        }
                    }
                    
                }
                msg.Code = 200;
                msg.Message = sb.ToString();
            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }
            ShowText(msg.ToJson());
        }
        protected string InnerSelectCategory(string sParentID,string id)
        {
            StringBuilder sb = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("ParentID=" + sParentID + " and IsValid=1", " Orders");
                for (int i = 0; i < dr.Length; i++)
                {
                    if(dr[i]["ID"].ToString()==id)
                        sb.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", dr[i]["ID"].ToString(), GetStr(Utils.ParseInt(dr[i]["Depth"].ToString(), 0)) + dr[i]["Name"].ToString());
                    else
                        sb.AppendFormat("<option value=\"{0}\">{1}</option>", dr[i]["ID"].ToString(), GetStr(Utils.ParseInt(dr[i]["Depth"].ToString(), 0)) + dr[i]["Name"].ToString());
                    sb.Append(InnerSelectCategory(dr[i]["ID"].ToString(),id));
                }

            }
            return sb.ToString();
        } 
        #endregion

        #region 获取扩展分类
        

        protected void GetSelect()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string sName = Utils.RequestStr("name");
            if (string.IsNullOrEmpty(sName))
                sName = "CategoryIDS";
            try
            {
                KingTop.Modules.BLL.Categorys bCategory = new KingTop.Modules.BLL.Categorys();
                dt = bcategory.GetCategoryCache();
                StringBuilder sb = new StringBuilder();
                sb.Append("<span class=\"spancategory\">");
                sb.AppendFormat("<select name=\"{0}\" class=\"addcategorycss\">", sName);
                sb.Append("<option value=\"0\">请选择分类</option>");
                sb.Append(InnerCategory("0"));
                sb.Append("</select>");
                sb.Append("<a href=\"javascript:\" onclick=\"removeCategory(this)\">删除</a>");
                sb.Append("</span>");
                msg.Code = 200;
                msg.Message = sb.ToString();
            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }
            ShowText(msg.ToJson());
        }

        protected string InnerCategory(string sParentID)
        {
            StringBuilder sb = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("ParentID=" + sParentID + " and IsValid=1", " Orders");
                for (int i = 0; i < dr.Length; i++)
                {
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", dr[i]["ID"].ToString(), GetStr(Utils.ParseInt(dr[i]["Depth"].ToString(), 0)) + dr[i]["Name"].ToString());
                    sb.Append(InnerCategory(dr[i]["ID"].ToString()));
                }

            }
            return sb.ToString();
        }
        protected string GetStr(int depth)
        {
            string sStr = string.Empty;
            for (int i = 0; i < depth; i++)
            {
                if (i == 0)
                    sStr = "├──";
                else
                    sStr += "──";
            }
            return sStr;
        }


        #endregion

        #region 更新排序顺序
        protected void UpdateOrders()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string sId = Utils.RequestStr("id");
            string sOrders = Utils.RequestStr("orders");
            try
            {
                KingTop.Modules.BLL.Categorys bCategory = new KingTop.Modules.BLL.Categorys();
                string sSql = string.Format("update K_Category set Orders={0} where ID={1}", sOrders, sId);
                int iCounts = SQLHelper.ExecuteNonQuery(sSql);
                if (iCounts > 0)
                {
                    msg.Code = 200;
                    bCategory.SetCategoryCache();
                }
                else
                {
                    msg.Code = 404;
                    msg.Message = "数据不存在";
                }

            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }
            ShowText(msg.ToJson());
        }
        #endregion

        #region 更新是否显示
        protected void ShowValid()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string sId = Utils.RequestStr("id");
            try
            {
                string sStr = string.Empty;
                KingTop.Modules.BLL.Categorys bCategory = new KingTop.Modules.BLL.Categorys();
                DataTable dt = bcategory.GetCategoryCache();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("ID=" + sId);
                    if (dr.Length > 0)
                        sStr = dr[0]["IsValid"].ToString() == "1" ? "0" : "1";
                    string sSql = string.Format("update K_Category set IsValid={0} where ID={1}",sStr,sId);
                    int iCounts = SQLHelper.ExecuteNonQuery(sSql);
                    if (iCounts > 0)
                    {
                        msg.Code = 200;
                        msg.Data = sStr;
                        bCategory.SetCategoryCache();
                    }
                    else
                    {
                        msg.Code = 404;
                        msg.Message = "数据不存在";
                    }
                }
                else
                {
                    msg.Code = 404;
                    msg.Message = "数据不存在";
                }

            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }
            ShowText(msg.ToJson());
        }
        #endregion

        #region 更新是否首页显示
        protected void ShowIndex()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string sId = Utils.RequestStr("id");
            try
            {
                string sStr = string.Empty;
                KingTop.Modules.BLL.Categorys bCategory = new KingTop.Modules.BLL.Categorys();
                DataTable dt = bcategory.GetCategoryCache();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow[] dr = dt.Select("ID=" + sId);
                    if (dr.Length > 0)
                        sStr = dr[0]["IsIndex"].ToString() == "1" ? "0" : "1";
                    string sSql = string.Format("update K_Category set IsIndex={0} where ID={1}", sStr, sId);
                    int iCounts = SQLHelper.ExecuteNonQuery(sSql);
                    if (iCounts > 0)
                    {
                        msg.Code = 200;
                        msg.Data = sStr;
                        bCategory.SetCategoryCache();
                    }
                    else
                    {
                        msg.Code = 404;
                        msg.Message = "数据不存在";
                    }
                }
                else
                {
                    msg.Code = 404;
                    msg.Message = "数据不存在";
                }

            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }
            ShowText(msg.ToJson());
        } 
        #endregion

        #region 删除分类
        protected void RemoveItem()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string sId = Utils.RequestStr("id");
            try
            {
                string sSql = string.Format("update K_Category set IsDel=1 where ID=" + sId);
                int iCounts = SQLHelper.ExecuteNonQuery(sSql);
                if (iCounts > 0)
                {
                    msg.Code = 200;
                    KingTop.Modules.BLL.Categorys bCategory = new KingTop.Modules.BLL.Categorys();
                    bCategory.SetCategoryCache();
                }
                else
                {
                    msg.Code = 404;
                    msg.Message = "数据不存在";
                }
            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }


            ShowText(msg.ToJson());
        } 
        #endregion

        #region 获取分类列表
        protected void GetCategoryList()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string nodeCode = KingTop.Common.Utils.CheckSql(Request["NodeCode"]);
            try
            {

                msg.Code = 200;
                msg.Message = GetList("0", nodeCode);
            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }


            ShowText(msg.ToJson());
        }
        protected string GetList(string sParentID)
        {
            StringBuilder sb = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("ParentID=" + sParentID, " Orders");
                for (int i = 0; i < dr.Length; i++)
                {
                    
                    sb.AppendFormat("<ul class=\"ulheader ulbody\" ishow=\"1\" ref=\"{0}\">", dr[i]["Depth"].ToString());
                    sb.AppendFormat("   <li class=\"li1\"><span style=\"margin:0 0 0 {1}px;\"><img src=\"/sysadmin/images/DTree/minus.gif\" onclick=\"rowClicked(this)\"  class=\"cursor\" /> {0}</span></li>", dr[i]["Name"].ToString(), (Utils.ParseInt(dr[i]["Depth"].ToString(), 1) - 1) * 20);//GetDepthStr(Utils.ParseInt(dr[i]["Depth"].ToString(), 0)) +
                    sb.AppendFormat("   <li class=\"li2\"><a href=\"edit.aspx?Action=NEW&pid={0}\"><img src=\"/sysadmin/images/icon_add.gif\" width=\"10\" height=\"10\" />添加</a></li>", dr[i]["ID"].ToString());
                    sb.AppendFormat("   <li class=\"li3\"><img alt=\"是否显示\" class=\"cursor\" src=\"/sysadmin/images/{0}\" onclick=\"ShowValid('{1}',this)\" /></li>", Utils.ParseBool(dr[i]["IsValid"].ToString()) ? "yes.gif" : "no.gif",dr[i]["ID"].ToString());
                    sb.AppendFormat("   <li class=\"li4\"><img alt=\"首页显示\" class=\"cursor\" src=\"/sysadmin/images/{0}\" onclick=\"ShowIndex('{1}',this)\" /></li>", Utils.ParseBool(dr[i]["IsIndex"].ToString()) ? "yes.gif" : "no.gif", dr[i]["ID"].ToString());
                    sb.AppendFormat("   <li class=\"li5\"><span class=\"orders\" ref=\"{1}\">{0}</span></li>", dr[i]["Orders"].ToString(), dr[i]["ID"].ToString());
                    sb.AppendFormat("   <li class=\"li6\"><a href='edit.aspx?Action=EDIT&id={0}' class='abtn'>编辑 </a> <a href='javascript:' onclick=\"Del('{0}',this)\" class='abtn'> 删除 </a></li>", dr[i]["ID"].ToString());
                    sb.AppendFormat("</ul>");

                    sb.AppendFormat(GetList(dr[i]["ID"].ToString()));
                }

            }
            return sb.ToString(); ;
        }
        protected string GetList(string sParentID,string nodeCode)
        {
            StringBuilder sb = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] dr = dt.Select("ParentID=" + sParentID + " AND NodeCode='" + nodeCode + "'", " Orders");
                for (int i = 0; i < dr.Length; i++)
                {

                    sb.AppendFormat("<ul class=\"ulheader ulbody\" ishow=\"1\" ref=\"{0}\">", dr[i]["Depth"].ToString());
                    sb.AppendFormat("   <li class=\"li1\"><span style=\"margin:0 0 0 {1}px;\"><img src=\"/sysadmin/images/DTree/minus.gif\" onclick=\"rowClicked(this)\"  class=\"cursor\" /> {0}</span></li>", dr[i]["Name"].ToString(), (Utils.ParseInt(dr[i]["Depth"].ToString(), 1) - 1) * 20);//GetDepthStr(Utils.ParseInt(dr[i]["Depth"].ToString(), 0)) +
                    sb.AppendFormat("   <li class=\"li2\"><a href=\"edit.aspx?Action=NEW&pid={0}&NodeCode={1}\"><img src=\"/sysadmin/images/icon_add.gif\" width=\"10\" height=\"10\" />添加</a></li>", dr[i]["ID"].ToString(), nodeCode);
                    sb.AppendFormat("   <li class=\"li3\"><img alt=\"是否显示\" class=\"cursor\" src=\"/sysadmin/images/{0}\" onclick=\"ShowValid('{1}',this)\" /></li>", Utils.ParseBool(dr[i]["IsValid"].ToString()) ? "yes.gif" : "no.gif", dr[i]["ID"].ToString());
                    sb.AppendFormat("   <li class=\"li4\"><img alt=\"首页显示\" class=\"cursor\" src=\"/sysadmin/images/{0}\" onclick=\"ShowIndex('{1}',this)\" /></li>", Utils.ParseBool(dr[i]["IsIndex"].ToString()) ? "yes.gif" : "no.gif", dr[i]["ID"].ToString());
                    sb.AppendFormat("   <li class=\"li5\"><span class=\"orders\" ref=\"{1}\">{0}</span></li>", dr[i]["Orders"].ToString(), dr[i]["ID"].ToString());
                    sb.AppendFormat("   <li class=\"li6\"><a href='edit.aspx?Action=EDIT&id={0}&NodeCode={1}' class='abtn'>编辑 </a> <a href='javascript:' onclick=\"Del('{0}',this)\" class='abtn'> 删除 </a></li>", dr[i]["ID"].ToString(),nodeCode);
                    sb.AppendFormat("</ul>");

                    sb.AppendFormat(GetList(dr[i]["ID"].ToString(), nodeCode));
                }

            }
            return sb.ToString(); ;
        }
        protected string GetDepthStr(int depth)
        {
            string sStr = string.Empty;
            for (int i = 0; i < depth; i++)
            {
                sStr += "  ";
            }
            return sStr;
        } 
        #endregion

        #region 删除规格值
        protected void DeleteSpeValue()
        {
            KingTop.Common.AjaxMessage msg = new KingTop.Common.AjaxMessage();
            string sId = Utils.RequestStr("id");
            try
            {
                string sSql = string.Format("delete K_M_SpecificationValue where ID=" + sId);
                int iCounts = SQLHelper.ExecuteNonQuery(sSql);
                if (iCounts > 0)
                {
                    msg.Code = 200;
                }
                else
                {
                    msg.Code = 404;
                    msg.Message = "数据不存在";
                }
            }
            catch (Exception err)
            {
                msg.Code = 500;
                msg.Message = err.Message;
            }


            ShowText(msg.ToJson());
        }
        #endregion
    }
}
