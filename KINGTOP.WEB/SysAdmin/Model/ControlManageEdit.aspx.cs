#region 程序集引用

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.BLL.Content;
using System.Data;
using System.Text;
using System.Collections;

using KingTop.Common;
#endregion

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线

    作者:      吴岸标

    创建时间： 2010年4月12日

    功能描述： 解析后的页面添加/修改
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.Web.Admin
{
    public partial class ControlManageEdit : AdminPage
    {
        #region 变量成员
        protected Hashtable hsFieldValue;                        // 字段初始值
        protected BLL.Content.ControlManageEdit ctrManageEdit;   // 业务操作对象
        protected string jsMessage;                              // js操作提示
        protected string isHasCreateHtmlRight;
        #endregion

        #region  Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            ctrManageEdit = new KingTop.BLL.Content.ControlManageEdit(hdnFieldValue.Value, hdnTableName.Value, hdnModelID.Value);
            ctrManageEdit.SiteID = SiteID;
            ctrManageEdit.NodeCode = NodeCode;
            ctrManageEdit.UserName = GetLoginAccountName();
            hdnBackUrlParam.Value = ctrManageEdit.GetBackUrlParam();

            if (!IsPostBack)
            {
                PageInit();
            }
        }
        #endregion

        #region 页面初始化加载
        /// <summary>
        /// 初始化
        /// </summary>
        void PageInit()
        {
            hdnActionType.Value = Request.QueryString["action"].ToLower();
            if (!string.IsNullOrEmpty(this.ID))
            {
                btnModelManageEdit.Text = "修改";

                if (this.Page.FindControl("btnAddToOtherNode") != null) // 添加至其它专题
                {
                    this.Page.FindControl("btnAddToOtherNode").Visible = false;
                }
                //if (!string.IsNullOrEmpty(Request.Cookies["ModelPage"].Value))
                //{
                    if (hdnBackUrlParam.Value.ToLower().IndexOf("page=") == -1)
                    {
                        hdnBackUrlParam.Value = hdnBackUrlParam.Value + "&page=" + GetModelCookes ("ModelPage");
                    }
                //}
            }
            else
            {

                btnModelManageEdit.Text = "添加";
            }

            this.hsFieldValue = ctrManageEdit.FillField();
            hdnNodeID.Value = Convert.ToString(hsFieldValue["NodeCode"]);
            hdnSiteID.Value = Convert.ToString(hsFieldValue["SiteID"]);

            // 传递的ID记录不存在或字段参数有误
            if (this.hsFieldValue == null)
            {
                Response.Write("<script>window.alert(\"传递的ID记录不存在或字段参数有误或没有可编辑的字段。\");history.back();</script>");
                Response.End();
            }

            if (IsHaveRightByOperCode("CreateHtml"))
            {
                isHasCreateHtmlRight = "1";
            }
            else
            {
                isHasCreateHtmlRight = "0";
            }
            if (!string.IsNullOrEmpty(hdnHtmlField.Value))
            {
                string[] arrFieldHtml = hdnHtmlField.Value.Split (',');
                for (int i = 0; i < arrFieldHtml.Length; i++)
                {
                    System.Web.UI.UserControl con = new UserControl();

                    KingTop.Web.Admin.Controls.Editor editid = (KingTop.Web.Admin.Controls.Editor)Page.FindControl(arrFieldHtml[i]);
                    editid.Content = hsFieldValue[(arrFieldHtml[i]+"_").Split ('_')[1]].ToString();
                }
            }
        }
        #endregion

        #region 更新
        protected void Edit(object sender, EventArgs e)
        {
            string msg;
            string listUrl;
            bool isValidate;                                          // 权限验证

            ctrManageEdit.SiteID = Utils.ParseInt(hdnSiteID.Value, 0);
            ctrManageEdit.NodeCode = hdnNodeID.Value;
            ctrManageEdit.FieldFromUrlParamValue = hdnFieldFromUrlParamValue.Value;
            listUrl = hdnTableName.Value.Replace("K_U_", "").Replace("K_F_", "") + "list.aspx?" + ctrManageEdit.KeepUrlParam;
            string msgTitle = string.IsNullOrEmpty(Request.Form["Title"]) ? "" : Request.Form["Title"].Replace ("\"","\\\"");

            if (!string.IsNullOrEmpty(this.ID))
            {
                isValidate = IsHaveRightByOperCode("Edit");  // 修改权限认证

                if (!isValidate)
                {
                    jsMessage = "errmsg=\"对不起，您没有修改记录的操作权限，请与管理员联系！\";id=\"" + ID + "\"";
                    this.hsFieldValue = ctrManageEdit.HsEditField;
                    PageInit(); // 得新绑定数据

                    return;
                }
                else
                {
                    msg = ctrManageEdit.Edit(false, this.ID);

                    if (msg == "1")  // 更新成功
                    {
                        jsMessage = "type=1;title=\"" + msgTitle + " \";id=\"" + ctrManageEdit.RecordID + "\"";
                        WriteLog("更新记录 " + Request.Form["Title"] + "(ID:" + ctrManageEdit.RecordID + ") 成功！", null, 2);
                    }
                    else  // 失败
                    {
                        jsMessage = "errmsg=\"对不起，操作失败。\";type=2;id=\"" + ctrManageEdit.RecordID + "\";";
                        WriteLog("对不起，更新记录 " + Request.Form["Title"] + "(ID:" + ctrManageEdit.RecordID + ") 失败！", msg, 3);
                    }
                }
            }
            else
            {
                isValidate = IsHaveRightByOperCode("New");  // 添加权限认证
                SetModelCookies("search", null);
                SetModelCookies("searchlink", null);
                SetModelCookies("ModelPage", null);

                if (!isValidate)
                {
                    jsMessage = "errmsg=\"对不起，您没有添加记录的操作权限，请与管理员联系！\";id=\"" + ID + "\"";
                    this.hsFieldValue = ctrManageEdit.HsEditField;
                    PageInit(); // 得新绑定数据

                    return;
                }
                else
                {

                    msg = ctrManageEdit.Edit(true, null);
                    
                    if (msg == "1")  // 新增成功
                    {
                        jsMessage = "type=0;title=\"" + msgTitle + " \";id=\"" + ctrManageEdit.RecordID + "\"";
                        WriteLog("新增记录 " + Request.Form["Title"] + "(" + ctrManageEdit.RecordID + ") 成功！", null, 2);
                    }
                    else   // 失败
                    {
                        jsMessage = "errmsg=\"对不起，操作失败。\";type=2;id=\"" + ctrManageEdit.RecordID + "\";";
                        WriteLog("对不起，新增记录 " + Request.Form["Title"] + "(" + ctrManageEdit.RecordID + ") 失败！", msg, 3);
                    }
                }
            }

            if (ctrManageEdit.HsEditField.Contains("IsHtml") && Utils.ParseBool(ctrManageEdit.HsEditField.Contains("IsHtml")))   // 公用字段，是否生成HTML
            {
                

                isValidate = IsHaveRightByOperCode("CreateHtml");

                if (isValidate)
                {
                    //模版方式生成静态页面
                    //string[] arrLog;
                    //arrLog = ctrManageEdit.CreateHtml(ctrManageEdit.RecordID, this.SiteDir, GetUploadFileUrl(), GetUploadFileUrl(), GetUploadMediaUrl(), this.GetSiteDomain());
                    //if (!string.IsNullOrEmpty(arrLog[1]))
                    //{
                    //    jsMessage = "alert({msg:\"HTML页生成失败！\",title:\"操作提示\"});";
                    //    WriteLog(arrLog[0], arrLog[1], 3);
                    //}
                    //else
                    //{
                    //    WriteLog(arrLog[0], null, 2);
                    //}

                    //动态转静态
                    KingTop.WEB.SysAdmin.SysManage.AspxToHtml_Publish publish = new KingTop.WEB.SysAdmin.SysManage.AspxToHtml_Publish();
                    string nodeList = "''";
                    List<string> lstMenu = new List<string>();
                    for (int i = 6; i <= NodeCode.Length; i = i + 3)
                    {
                        nodeList += ",'" + NodeCode.Substring(0, i) + "'";
                        lstMenu.Add(NodeCode.Substring(0, i));
                    }
                    publish.IsBar = false;
                    publish.IsContent = true;
                    publish.IsIndex = true;
                    publish.IsMenuList = true;
                    publish.nodeCodeList = nodeList;
                    publish.PublishType = 1;
                    publish.TypeParam = new string[] { ctrManageEdit.RecordID };
                    publish.ListMenu = lstMenu;
                    publish.siteDir = SiteDir;
                    publish.siteID = SiteID;
                    try
                    {
                        publish.Execute();
                    }
                    catch{
                        jsMessage = "alert({msg:\"HTML页生成失败！\",title:\"操作提示\"});";
                    }
                    
                }
                else
                {
                    jsMessage = "alert({msg:\"对不起，您没有 生成HTML 操作权限，请与管理员联系！\",title:\"操作提示\"});";
                }
            }

            this.hsFieldValue = ctrManageEdit.HsEditField;
            PageInit(); // 得新绑定数据
        }
        #endregion


        #region 状态保存
        //两个页面用到,都有此方法 ControlManageList.aspx 和 ControlManageEdit.aspx
        private void SetModelCookies(string pageKey, string cookiesValue)
        {
            string scriptname = Request.ServerVariables["SCRIPT_NAME"].ToLower().Replace("list", "").Replace("edit", "").Replace(".", "");
            if (scriptname.IndexOf("/") != -1)
            {
                string[] arr = scriptname.Split('/');
                pageKey = pageKey + arr[arr.Length - 1];
            }
            pageKey = Session.SessionID + pageKey + NodeCode;

            #region Cookies 保存方式
            string strCookieName = "ModelPageCatch";
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strCookieName];
            if (cookie != null)
            {
                if (cookie[pageKey] == null)  //如果当前cookie子集没有保存,则新增一个
                {
                    int iCount = cookie.Values.Count; //当前保存的cookie子集合数
                    cookie.Values.Add(pageKey, cookiesValue);
                }
                else  //否则 直接改变当前子集的值
                {
                    cookie[pageKey] = cookiesValue;
                }
            }
            else
            {

                cookie = new HttpCookie("ModelPageCatch");
                cookie.Values.Add(pageKey, cookiesValue);
            }
            Response.Cookies.Add(cookie);
            #endregion

            #region Session 保存方式
            //Session[pageKey] = cookiesValue;
            #endregion

            //cookiesValue = cookiesValue == null ? "" : cookiesValue;
            //AppCache.AddCache(pageKey, cookiesValue, 240, true);
        }
        private string GetModelCookes(string pageKey)
        {
            string scriptname = Request.ServerVariables["SCRIPT_NAME"].ToLower().Replace("list", "").Replace("edit", "").Replace(".", "");
            if (scriptname.IndexOf("/") != -1)
            {
                string[] arr = scriptname.Split('/');
                pageKey = pageKey + arr[arr.Length - 1];
            }
            pageKey = Session.SessionID + pageKey + NodeCode;

            HttpCookie cookie = new HttpCookie("ModelPageCatch");
            string cookieValue = Utils.GetCookie("ModelPageCatch", pageKey);
            if (!string.IsNullOrEmpty(cookieValue) && cookieValue.Substring(0, 1) == ",")  //测试结果可能会加上","，需求去掉
            {
                cookieValue = cookieValue.Substring(1);
            }
            return Server.UrlDecode(cookieValue);

            //return Server.UrlDecode(AppCache.Get(pageKey).ToString());
        }
        #endregion
    }
}
