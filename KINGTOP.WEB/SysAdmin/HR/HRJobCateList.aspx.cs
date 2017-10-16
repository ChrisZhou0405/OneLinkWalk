using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using System.Data;
using KingTop.Web.Admin;
using System.Text;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      陈顺
    创建时间： 2010年3月31日
    功能描述： 用户组管理
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.WEB.SysAdmin.HR
{
    public partial class HRJobCate : AdminPage
    {
        KingTop.Modules.BLL.HRJobCate objBll = new KingTop.Modules.BLL.HRJobCate();
        private DataTable dt = null;
        DataTable dt1 = null;
        public bool isAllShow = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["isajax"] == "1")
            {
                Response.Write(GetChildCate());
                Response.End();
            }
            if (!IsPostBack)
            {
                PageInit();
            }

        }

        private void PageInit()
        {
            SplitDataBind();
            Utils.SetVisiteList(SystemConst.COOKIES_PAGE_KEY, Session.SessionID, Utils.GetUrlParams().Replace("&", "|"), SystemConst.intMaxCookiePageCount); //把当前url全部参数存入cookie中 
            SetRight(this, rptInfoList);
        }

        /// <summary>
        /// 无限分类的用户组显示结构处理
        /// </summary>
        public string GetName(string strName, string strCode)
        {             
            string temp_str="";
            int    numCode = strCode.Length / 3;
                if (numCode == 1)
                { }
                else
                {
                    for (int p = 1; p < numCode; p++)
                    {
                        if (p == numCode - 1)
                        {
                            temp_str = temp_str + "├";
                        }
                        else
                        {
                            temp_str = temp_str + "　";
                        }
                    }
                }
                temp_str = temp_str + strName;
                return temp_str;
            }

        public string GetCateImg(string id)
        {
            //<a href=""><img src="../images/DTree/plus.gif" border=0/></a>
            DataRow[] dr = dt.Select("ParentID='" + id + "'");
            int leng = ((id.Length/3)-1)*13;

            if (dr.Length > 0)
            {
                return "<a href=\"javascript:void(0);\" onclick=\"GetChildCate('" + id + "');\"><img src=\"../images/DTree/plus.gif\" border=0 style='margin-left:"+leng+"px'/></a> ";
            }
            else
            {
                return "<img src=\"../images/DTree/minus.gif\" border=0 style='margin-left:" + leng + "px'/> ";
            }
        }

        void GetOrderDataTable(string parentID)
        {
            DataRow[] dr = dt.Select("ParentID='"+parentID+"'","Orders");
            for (int i = 0; i < dr.Length; i++)
            {
                dt1.ImportRow(dr[i]);
                GetOrderDataTable(dr[i]["ID"].ToString());
            }
        }

        // 数据绑定
        private void SplitDataBind()
        {
            dt = objBll.dt(Request.QueryString["catetype"],SiteID,Server.UrlDecode(Request.QueryString ["keyword"]));
            dt1 = dt.Clone();
            if (dt.Rows.Count < 100)  //如果记录数小于100，则全部列出来，否则用户点击父节点后再列出子节点
            {
                isAllShow = true;
                GetOrderDataTable("0");
            }
            else  //记录数大于100，则加载一级节点，查看子节点，点击页面“+”即可
            {
                DataRow[] dr = dt.Select("ParentID=0", "Orders");
                for (int i = 0; i < dr.Length; i++)
                {
                    dt1.ImportRow(dr[i]);
                }
            }

            rptInfoList.DataSource = dt1;
            rptInfoList.DataBind();
            txtSearch.Text = Server.UrlDecode(Request.QueryString["keyword"]);
        }

        public string GetFatherName(string parentID)
        {
            DataRow[] dr = dt.Select("ID='" + parentID + "'");
            if (dr.Length > 0)
            {
                return dr[0]["Title"].ToString();
            }

            return "无";
        }

        protected void Butnew_Click(object sender, EventArgs e)
        {
            if (IsHaveRightByOperCode("New"))
            {
                Response.Redirect("HrJobCateEdit.aspx?Action=New&NodeCode=" + NodeCode+"&catetype="+Request.QueryString ["cateType"]);
            }
        }

        void OnDel(string id)
        {
            if (base.IsHaveRightByOperCode("Delete"))
            {
                string returnMsg = "1";   // 事务返回信息
                returnMsg = objBll.Delete(id);
                int result = Utils.ParseInt(returnMsg, 0);

                if (result > 0)  //操作失败
                {
                    WriteLog("删除类别：" + LogTitle + " 成功", "", 2); //写日志
                    //Utils.RunJavaScript(this, "nmsgtitle='提示信息';nmsgcontent='操作成功';");
                }
                else
                {
                    WriteLog("删除类别："+LogTitle + "失败", returnMsg, 3); //写日志
                    Utils.RunJavaScript(this, "nmsgtitle='提示信息';nmsgcontent='" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "';");
                }
                PageInit();
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'删除失败，没有权限！',title:'提示信息'})");
            }
        }

        void OnCheck(string id,int checkValue)
        {
            string checkTitle = "审核";
            string operCode = "Check";
            if (checkValue == 0)
            {
                checkTitle = "取消审核";
                operCode = "CancelCheck";
            }

            if (base.IsHaveRightByOperCode(operCode))
            {
                string returnMsg = "1";   // 事务返回信息

                returnMsg = objBll.Check(id, checkValue);
                int result = Utils.ParseInt(returnMsg, 0);
                

                if (result > 0)  //操作失败
                {
                    WriteLog(checkTitle+"类别：" + LogTitle + " 成功", "", 2); //写日志
                    //Utils.RunJavaScript(this, "nmsgtitle='提示信息';nmsgcontent='操作成功';");
                }
                else
                {
                    WriteLog(checkTitle + "类别：" + LogTitle + "失败", returnMsg, 3); //写日志
                    Utils.RunJavaScript(this, "nmsgtitle='提示信息';nmsgcontent='" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "';");
                }
                PageInit();
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'"+checkTitle+"失败，没有权限！',title:'提示信息'})");
            }
        }

        protected void butdel_Click(object sender, EventArgs e)
        {
            OnDel(Request.Form["chkId"].Replace(", ", ","));
        }


        protected void btnCheck_Click(object sender, EventArgs e)
        {
            OnCheck(Request.Form["chkId"].Replace(", ", ","),99);
        }

        protected void btnCancelCheck_Click(object sender, EventArgs e)
        {
            OnCheck(Request.Form["chkId"].Replace(", ", ","),0);
        }
        
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (IsHaveRightByOperCode("Query"))
            {
                Utils.UrlRedirect("HrJobCateList.aspx?NodeCode=" + NodeCode + "&keyword=" + Utils.UrlEncode(txtSearch.Text.Trim())+"&cateType="+Request.QueryString ["cateType"]); //页面跳转
            }
        }

        public string GetChildCate()
        {
            StringBuilder sb=new StringBuilder ();
            string id = Utils.CheckSql(Request.QueryString["id"]);
            dt = objBll.dt(Request.QueryString["cateType"], SiteID);
            DataRow[] dr = dt.Select("ParentID='" + id + "'");
            for (int i = 0; i < dr.Length; i++)
            {
                sb.Append("<ul class=\"ulheader ulbody\" id=\"id").Append(dr[i]["ID"].ToString()).Append("\">");
                sb.Append("<li style=\"width: 5%;text-align:center\"><input type=\"checkbox\" value='").Append(dr[i]["ID"].ToString()).Append("' name=\"chkId\" id=\"chkId\" /></li>");
                sb.Append("<li style=\"width: 15%\">").Append(GetCateImg(dr[i]["ID"].ToString())).Append(dr[i]["ID"].ToString()).Append("</li>");
                sb.Append("<li style=\"width: 15%\"><span id=\"Title").Append(dr[i]["ID"].ToString()).Append("\" style=\"display:none\">")
                    .Append(dr[i]["Title"].ToString()).Append("</span>").Append(GetName(dr[i]["Title"].ToString(), dr[i]["ID"].ToString())).Append("</li>");
                sb.Append("<li style=\"width: 15%\">").Append(GetFatherName(dr[i]["ParentID"].ToString())).Append("</li>");
                sb.Append("<li style=\"width: 10%\">").Append(GetCheckName(dr[i]["FlowState"].ToString())).Append("</li>");

                string s = string.IsNullOrEmpty(dr[i]["AddDate"].ToString())?"":DateTime.Parse(dr[i]["AddDate"].ToString()).ToString("yyyy-MM-dd");
                
                sb.Append("<li style=\"width: 10%\">").Append(s).Append("</li>");
                sb.Append("<li style=\"width: 20%\">");
                if (IsHaveRightByOperCode("Edit"))
                {
                    sb.Append("<a class=\"abtn\" href=\"HrJobCateEdit.aspx?action=Edit&ID=").Append(dr[i]["id"].ToString())
                        .Append("&NodeCode=").Append(NodeCode)
                        .Append("&cateType=").Append(Request.QueryString["cateType"])
                        .Append ("\">修改</a>");
                }
                if (IsHaveRightByOperCode("Delete"))
                {
                    sb.Append("<a class=\"abtn\" title=\"").Append(dr[i]["Title"].ToString().Replace ("\"","\\\""))
                        .Append("\" onclick='selectThisRow();selfconfirm({msg:\"确定要执行删除操作吗？此操作会将子类一起删除\",fn:function(data){setAction(data)}});return false;'>删除</a>");
                }
                sb.Append("</li></ul>");
            }

            return sb.ToString();
        }
    }
}
