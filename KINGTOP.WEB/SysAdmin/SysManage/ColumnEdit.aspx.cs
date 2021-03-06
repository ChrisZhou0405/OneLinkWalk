﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using KingTop.Common;
using KingTop.Model;
using System.Data;
using KingTop.BLL.SysManage;
using KingTop.Web.Admin;
using System.Text.RegularExpressions;

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
    public partial class ColumnEdit : AdminPage
    {
        //逻辑类

        KingTop.BLL.SysManage.ModuleNode bllModuleNode = new KingTop.BLL.SysManage.ModuleNode();
        //实体类

        KingTop.Model.SysManage.ModuleNode mode = new KingTop.Model.SysManage.ModuleNode();
        public string strurlpar;

        #region  URL参数
        private string _Isparent = null;
        public string IsParent
        {
            get
            {
                if (this._Isparent == null)
                {
                    this._Isparent = Utils.ReqUrlParameter("IsParent");
                    // 参数格式验证，非法则重置为空字符串

                }
                return this._Isparent;
            }
        }

        public string NCode
        {
            get { return Request.QueryString["NCode"]; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            strurlpar = "?Action=Edit&NodeID=" + Request.QueryString["NodeID"] + "&ID=" + Request.QueryString["ID"] + "&NCode=" + NCode + "&NodeCode=" + NodeCode;
            if (Request.QueryString["ColumnType"] == "2")
            {
                Response.Redirect("SingleColumn.aspx" + strurlpar);
            }
            if (Request.QueryString["ColumnType"] == "3")
            {
                Response.Redirect("OutLinkColumn.aspx" + strurlpar);
            }

            if (!IsPostBack)
            {
                BindDate();
                BindReviewFlowDate();
                BindCustomContentCount();
                PageInit();
                MetaInit();
            }
            BindGridView();
        }

        #region 绑定页面DropDownList控件
        //绑定栏目类型
        private void BindDate()
        {
            Module bllModule = new Module();
            DataTable dt = bllModule.GetList("AllMenu", Utils.getOneParams(""));
            if (dt.Rows.Count > 0)
            {
                this.ddlModel.DataSource = dt.DefaultView;
                this.ddlModel.DataTextField = "ModuleName";
                this.ddlModel.DataValueField = "ModuleID";
                this.ddlModel.DataBind();
                ListItem ls = new ListItem("请选择", "0");//追加一项

                this.ddlModel.Items.Insert(0, ls);
            }
        }

        //绑定审核流程
        private void BindReviewFlowDate()
        {
            KingTop.BLL.Content.ReviewFlow bllReviewFlow = new KingTop.BLL.Content.ReviewFlow();
            DataTable dt = bllReviewFlow.GetList("ALL", Utils.getOneParams(""));
            if (dt.Rows.Count > 0)
            {
                this.ddlReviewFlow.DataSource = dt.DefaultView;
                this.ddlReviewFlow.DataTextField = "Name";
                this.ddlReviewFlow.DataValueField = "ID";
                this.ddlReviewFlow.DataBind();
                ListItem ls = new ListItem("请选择...", "0");//追加一项

                this.ddlReviewFlow.Items.Insert(0, ls);
            }
        }

        //自定义内容数量

        private void BindCustomContentCount()
        {
            for (int i = 1; i <= 10; i++)
            {
                ListItem ls = new ListItem(i.ToString(), i.ToString());
                this.ddlCustomContentCount.Items.Add(ls);
            }
            ddlCustomContentCount.SelectedValue = "1";
        }
        #endregion


        #region 动态生成模板列实现ITemplate接口
        public class Add_CheckBoxToView : ITemplate
        {
            private string CheckBox_ID; //要创建的CheckBoxID=permitID
            private string strNodeID; //传过来的Nodeid
            private string strAction; //传过来的Action

            //初始化

            public Add_CheckBoxToView(string strColumDate_ID, string NodeID, string Action)
            {
                CheckBox_ID = strColumDate_ID;
                strNodeID = NodeID;
                strAction = Action;
            }

            #region 用于选中和禁用的方法
            //得到用户组权限数组

            private ArrayList GetUserGropArray(string strUserGropCode)
            {
                //现在每判断一个单元格CheckBox都要取数据库，待修改
                ArrayList arrUserGroupList = new ArrayList();
                ColumnRightTool bllColumnRightTool = new ColumnRightTool();
                arrUserGroupList = bllColumnRightTool.GetUserGroupRightList("UserGroup", strUserGropCode, strNodeID);
                return arrUserGroupList;
            }

            //得到角色权限数组
            private ArrayList GetRoleArray(string strUserGropCode)
            {
                //现在每判断一个单元格CheckBox都要取数据库，待修改
                ArrayList arrUserGroupList = new ArrayList();
                ColumnRightTool bllColumnRightTool = new ColumnRightTool();
                arrUserGroupList = bllColumnRightTool.GetUserGroupRightList("Role", strUserGropCode, strNodeID);
                return arrUserGroupList;
            }

            //判断是否选中(用户组已有权限选中)
            private bool SetCheck(string strUserGropCode, string strOprCode)
            {
                if (strAction == "NEW")
                {
                    return false;
                }
                else
                {
                    return HaveRight(GetUserGropArray(strUserGropCode), strOprCode);
                }
            }

            //判断是否禁用(用户组所属角色已有权限禁用)
            private bool SetEnable(string strUserGropCode, string strOprCode)
            {
                if (strAction == "NEW")
                {
                    return false;
                }
                else
                {
                    return HaveRight(GetRoleArray(strUserGropCode), strOprCode);
                }
            }

            //判断一个字符串是否在指定数组中
            private bool HaveRight(ArrayList arrModulOpr, string strOprCode)
            {
                if (arrModulOpr == null) { arrModulOpr = new ArrayList(); }
                bool blnReturn = false;
                for (int i = 0; i < arrModulOpr.Count; i++)
                {
                    if (strOprCode == arrModulOpr[i].ToString())
                    {
                        blnReturn = true; break;
                    }
                }
                return blnReturn;
            }
            #endregion

            //定义需要创建的控件，重写ITemplate接口的InstantiateIn方法
            public void InstantiateIn(System.Web.UI.Control container)
            {
                CheckBox chb = new CheckBox();//动态加入CheckBox
                chb.ID = "ckb" + CheckBox_ID;
                chb.Checked = false;
                chb.DataBinding += delegate(object sender, EventArgs e)
                {
                    object dataItem = ((sender as Control).NamingContainer as GridViewRow).DataItem;
                    string strUserGroupCode = DataBinder.Eval(dataItem, "UserGroupCode").ToString();
                    chb.Checked = SetCheck(strUserGroupCode, CheckBox_ID);
                    chb.Enabled = !SetEnable(strUserGroupCode, CheckBox_ID);
                };
                container.Controls.Add(chb);
            }
        }
        #endregion

        #region 绑定GridView控件
        private void BindGridView()
        {
            //移除现有columns
            grvRight.Columns.Clear();

            //得到用户组列表

            KingTop.BLL.SysManage.UserGroup bllUserGroup = new KingTop.BLL.SysManage.UserGroup();
            DataTable dt = bllUserGroup.GetList("ALL", Utils.getOneParams(SiteID.ToString())).Tables[0];

            //得到操作列表           
            KingTop.BLL.SysManage.ActionPermit bllActionPer = new ActionPermit();
            DataTable dt2 = bllActionPer.GetList("MODULE", Utils.getOneParams(ddlModel.SelectedValue));

            //给GridView新增用户组绑定列
            BoundField UserGroupCodeColumn = new BoundField();
            UserGroupCodeColumn.DataField = "UserGroupCode";
            grvRight.Columns.Add(UserGroupCodeColumn);

            BoundField UserGroupNameColumn = new BoundField();
            UserGroupNameColumn.HeaderText = "用户组名";
            UserGroupNameColumn.DataField = "UserGroupName";
            grvRight.Columns.Add(UserGroupNameColumn);

            //给GridView新增操作模板列

            if (dt2.Rows.Count > 0)
            {
                foreach (DataRow dr2 in dt2.Rows)
                {
                    TemplateField OperColumn = new TemplateField();
                    OperColumn.HeaderText = dr2["OperName"].ToString();
                    OperColumn.ItemTemplate = new Add_CheckBoxToView(dr2["ID"].ToString(), txtNodeID.Text, Action);
                    //给GridView新增列

                    grvRight.Columns.Add(OperColumn);
                }
            }

            grvRight.DataSource = dt;
            grvRight.DataBind();
        }
        #endregion

        private void PageInit()
        {
            //页面控件数据加载
            if (this.Action == "EDIT")
            {
                DataTable dt = bllModuleNode.GetList("ONE", Utils.getOneParams(ID));
                btnEdit.Text = Utils.GetResourcesValue("Common", "Update");
                dlMenuType.Visible = true;
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtNodeID.Text = dr["NodeID"].ToString();
                    txtNodeCode.Text = dr["NodeCode"].ToString();
                    txtNodeName.Text = dr["NodeName"].ToString();
                    hidLogTitle.Value = dr["NodeName"].ToString();
                    if (Utils.ParseBool(dr["NodeType"].ToString()))
                    {
                        chkIsFolder.Checked = Utils.ParseBool(dr["NodeType"].ToString());
                        this.lblModel.Text = "*";
                        dlContentTemplate.Visible = false;
                        dlListPageTemplate.Visible = false;
                    }
                    //else
                    //{
                    //    ddlModel.Enabled = false;
                    //}
                    txtLinkURL.Text = dr["LinkURL"].ToString();
                    if (dr["ModuleID"].ToString().Length == 0 || dr["ModuleID"].ToString() == Guid.Empty.ToString())
                    {
                        ddlModel.SelectedValue = "0";
                    }
                    else
                    {
                        ddlModel.SelectedValue = dr["ModuleID"].ToString();
                    }
                    chkIsLeftDisplay.Checked = Utils.ParseBool(Convert.ToInt32(dr["IsLeftDisplay"]));
                    chkIsTopMenuShow.Checked = Utils.ParseBool(dr["IsTopMenuShow"].ToString());
                    chkIsLeftMenuShow.Checked = Utils.ParseBool(dr["IsLeftMenuShow"].ToString());
                    RBL_IsValid.SelectedValue = dr["IsValid"].ToString();
                    txtNodelOrder.Text = dr["NodelOrder"].ToString();
                    //txtNodelDesc.Text = dr["NodelDesc"].ToString();
                    Editor1.Content = dr["NodelDesc"].ToString();
                    txtNodelEngDesc.Text = dr["NodelEngDesc"].ToString();
                    txtNodelIcon.Text = dr["NodelIcon"].ToString();
                    txtCurrentImg.Text = dr["CurrentImg"].ToString();
                    txtMouseOverImg.Text = dr["MouseOverImg"].ToString();
                    txtNodeDir.Text = dr["NodeDir"].ToString();
                    txtTips.Text = dr["Tips"].ToString();
                    txtPageTitle.Text = dr["PageTitle"].ToString();
                    txtKeyWords.Text = dr["Meta_Keywords"].ToString();
                    txtMetaDesc.Text = dr["Meta_Description"].ToString();
                    txtDefaultTemplate.Text = dr["DefaultTemplate"].ToString();
                    txtListPageTemplate.Text = dr["ListPageTemplate"].ToString();
                    txtContentTemplate.Text = dr["ContentTemplate"].ToString();
                    txtSubDomain.Text = dr["SubDomain"].ToString();
                    RbtEnableSubDomain.SelectedValue = dr["EnableSubDomain"].ToString();
                    if (!string.IsNullOrEmpty(dr["ReviewFlowID"].ToString()))
                    {
                        ddlReviewFlow.SelectedValue = dr["ReviewFlowID"].ToString();
                    }
                    switch (dr["OpenType"].ToString())
                    {
                        case ("1"): { radSelf.Checked = true; break; }
                        case ("2"): { radNew.Checked = true; break; }
                        default: { radSelf.Checked = true; break; }
                    }
                    switch (dr["PurviewType"].ToString())
                    {
                        case ("1"): { radkf.Checked = true; break; }
                        case ("2"): { radbkf.Checked = true; break; }
                        case ("3"): { radrz.Checked = true; break; }
                        case ("4"): { radgd.Checked = true; break; }
                        default: { radSelf.Checked = true; break; }
                    }
                    chkIsEnableComment.Checked = Utils.ParseBool(dr["IsEnableComment"].ToString());
                    switch (dr["IsCreateListPage"].ToString())
                    {
                        case ("False"): { radCreateListPageTrue.Checked = true; break; }
                        case ("True"): { radCreateListPageFalse.Checked = true; break; }
                        default: { radCreateListPageTrue.Checked = true; break; }
                    }
                    txtIncrementalUpdatePages.Text = dr["IncrementalUpdatePages"].ToString();
                    chkIsEnableIndexCache.Checked = Utils.ParseBool(dr["IsEnableIndexCache"].ToString());
                    switch (dr["ListPageSavePathType"].ToString())
                    {
                        case ("1"): { radListPageSavePathType1.Checked = true; break; }
                        case ("2"): { radListPageSavePathType2.Checked = true; break; }
                        case ("3"): { radListPageSavePathType3.Checked = true; break; }
                        default: { radListPageSavePathType1.Checked = true; break; }
                    }
                    if (string.IsNullOrEmpty(dr["ListPagePostFix"].ToString()))
                    {
                        try
                        {
                            ddlListPagePostFix.SelectedValue = dr["ListPagePostFix"].ToString();
                        }
                        catch
                        {
                            throw new Exception("没有相应项与之匹配");
                        }
                    }
                    switch (dr["IsCreateContentPage"].ToString())
                    {
                        case ("False"): { radCreateContentPageTrue.Checked = true; break; }
                        case ("True"): { radCreateContentPageFalse.Checked = true; break; }
                        default: { radCreateContentPageTrue.Checked = true; break; }
                    }
                    switch (dr["ContentPageHtmlRule"].ToString())
                    {
                        case ("4"): { radContentPageSavePathType4.Checked = true; break; }
                        case ("2"): { radContentPageSavePathType2.Checked = true; break; }
                        case ("3"): { radContentPageSavePathType3.Checked = true; break; }
                        case ("5"): { radContentPageSavePathType5.Checked = true; break; }
                        case ("1"): { radContentPageSavePathType1.Checked = true; break; }
                        case ("6"): { radContentPageSavePathType6.Checked = true; break; }
                        case ("8"): { radContentPageSavePathType8.Checked = true; break; }
                        default: {
                            radContentPageSavePathType7.Checked = true;
                            txtzdyURL.Text = mode.CustomManageLink;
                            break;
                        }
                    }
                    switch (dr["AutoCreateHtmlType"].ToString())
                    {
                        case ("1"): { radAutoCreateHtmlType1.Checked = true; break; }
                        case ("2"): { radAutoCreateHtmlType2.Checked = true; break; }
                        case ("3"): { radAutoCreateHtmlType3.Checked = true; break; }
                        case ("4"): { radAutoCreateHtmlType4.Checked = true; break; }
                        case ("5"): { radAutoCreateHtmlType5.Checked = true; break; }
                        case ("6"): { radAutoCreateHtmlType6.Checked = true; break; }
                        default: { radAutoCreateHtmlType1.Checked = true; break; }
                    }

                    //自定义内容绑定

                    string strCustomContent = dr["Custom_Content"].ToString();
                    InitEditor(strCustomContent);
                    txtBanner.Text = dr["Banner"].ToString();
                    //if (strCustomContent.IndexOf("###") > 0)
                    //{
                    //    //有多个自定义内容
                    //    string[] arrCustomContent = Regex.Split(strCustomContent, "###");
                    //    //选中下拉框

                    //    ddlCustomContentCount.SelectedValue = arrCustomContent.Length.ToString();
                    //    //给第一个TextBox赋值

                    //    txtCustomContent1.Text = arrCustomContent[0];
                    //    //创建控件并赋值

                    //    for (var i = 2; i <= arrCustomContent.Length; i++)
                    //    {
                    //       tdCustomContent.InnerHtml += "自设内容" + i.ToString() + "：";
                    //       tdCustomContent.InnerHtml += "<textarea rows='10' cols='50' name='" + "txtCustomContent" + i.ToString() + "' ID='" + "txtCustomContent" + i.ToString() + "'>" + arrCustomContent[i-1] + "</textarea><br>";
                    //    }

                    //}
                    //else
                    //{ 
                    //    //只有一个自定义内容
                    //    //选中下拉框

                    //    ddlCustomContentCount.SelectedValue = "1";
                    //    //给第一个TextBox赋值

                    //    txtCustomContent1.Text = strCustomContent;                        
                    //}
                    ////隐藏域保存数据库信息
                    //this.hiddenCustomContent.Value = strCustomContent;     

                }
            }
            else
            {
                btnDel.Visible = false;
                dlMenuType.Visible = false;
                btnEdit.Text = Utils.GetResourcesValue("Common", "Add");
                //生成新GUID用于新增操作产生Nodeid
                txtNodeID.Text = Guid.NewGuid().ToString();

                //新增选中一级审核

                ddlReviewFlow.SelectedValue = "000000000000000";
            }
        }

        private void InitEditor(string customContent)
        {
            string[] arrOneItem = Utils.strSplit(customContent, "###");
            for (int i = 0; i < arrOneItem.Length; i++)
            {
                string[] arrTxtAndCheck = Utils.strSplit(arrOneItem[i], "<hqb>");
                TextBox txt = (TextBox)Page.FindControl("txtCustomContent" + (i + 1).ToString());
                System.Web.UI.HtmlControls.HtmlInputCheckBox chk = (System.Web.UI.HtmlControls.HtmlInputCheckBox)Page.FindControl("chkIsHtmlEditor" + (i + 1).ToString());
                if (arrTxtAndCheck.Length > 0)
                {
                    txt.Text = arrTxtAndCheck[0];
                    chk.Checked = Utils.ParseBool(Utils.ParseInt(arrTxtAndCheck[1], 0));
                }
            }
            ddlCustomContentCount.SelectedValue = arrOneItem.Length.ToString();
            Utils.RunJavaScript(this, "showTotal=" + arrOneItem.Length);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            string returnMsg = "";
            string editSelfMenuUrl = "";
            DataTable dtNodeCode = null;

            #region 实体类赋值

            mode.NodeID = new Guid(txtNodeID.Text);
            mode.NodeName = txtNodeName.Text.Trim();
            if (txtLinkURL.Text.Length > 0)
            {
                //mode.LinkURL = Utils.HtmlEncode(txtLinkURL.Text.Trim());
                mode.LinkURL = txtLinkURL.Text.Trim();
            }
            if (ddlModel.SelectedValue != "0")
            {
                mode.ModuleID = new Guid(ddlModel.SelectedValue.Trim());
            }
            mode.IsLeftDisplay = Utils.ParseBool(chkIsLeftDisplay.Checked);
            mode.IsTopMenuShow = Utils.ParseBool(chkIsTopMenuShow.Checked);
            mode.IsLeftMenuShow = Utils.ParseBool(chkIsLeftMenuShow.Checked);
            mode.IsValid = Utils.ParseBool(RBL_IsValid.SelectedValue);
            mode.IsWeb = true;
            mode.NodelOrder = Utils.HtmlEncode(txtNodelOrder.Text.Trim());
            //mode.NodelDesc = Utils.HtmlEncode(txtNodelDesc.Text.Trim());
            mode.NodelDesc = Editor1.Content;
            mode.CustomManageLink = "";
            mode.NodelEngDesc = txtNodelEngDesc.Text.Trim();
            mode.NodelIcon = txtNodelIcon.Text.Trim();
            mode.CurrentImg = Utils.HtmlEncode(txtCurrentImg.Text.Trim());
            mode.MouseOverImg = Utils.HtmlEncode(txtMouseOverImg.Text.Trim());
            mode.WebSiteID = Convert.ToInt32(this.SiteID);
            mode.Creater = base.GetLoginAccountId();
            mode.NodeDir = txtNodeDir.Text;
            mode.Settings = "";
            mode.Tips = Utils.HtmlEncode(txtTips.Text);
            mode.PageTitle = Utils.HtmlEncode(txtPageTitle.Text);
            mode.Meta_Keywords = Utils.HtmlEncode(txtKeyWords.Text);
            mode.Meta_Description = Utils.HtmlEncode(txtMetaDesc.Text);
            mode.DefaultTemplate = Utils.HtmlEncode(txtDefaultTemplate.Text);
            mode.ListPageTemplate = Utils.HtmlEncode(txtListPageTemplate.Text);
            mode.ContentTemplate = Utils.HtmlEncode(txtContentTemplate.Text);
            mode.SubDomain = txtSubDomain.Text;
            mode.EnableSubDomain = Utils.ParseBool(RbtEnableSubDomain.SelectedValue);
            mode.CreateDate = DateTime.Now;
            if (ddlReviewFlow.SelectedValue != "0")
            {
                mode.ReviewFlowID = this.ddlReviewFlow.SelectedValue;
            }
            mode.OpenType = radSelf.Checked ? 1 : 2;
            if (radkf.Checked)
            {
                mode.PurviewType = 1;
            }
            else if (radbkf.Checked)
            {
                mode.PurviewType = 2;
            }
            else if (radrz.Checked)
            {
                mode.PurviewType = 3;
            }
            else
            {
                mode.PurviewType = 4;
            }
            mode.IsEnableComment = Utils.ParseBool(chkIsEnableComment.Checked);
            mode.IsCreateListPage = radCreateListPageTrue.Checked ? false : true;
            mode.IncrementalUpdatePages = Utils.ParseInt(txtIncrementalUpdatePages.Text, 0);
            mode.IsEnableIndexCache = Utils.ParseBool(chkIsEnableIndexCache.Checked);
            if (radListPageSavePathType1.Checked)
            {
                mode.ListPageSavePathType = 1;
            }
            else if (radListPageSavePathType2.Checked)
            {
                mode.ListPageSavePathType = 2;
            }
            else
            {
                mode.ListPageSavePathType = 3;
            }
            if (ddlListPagePostFix.SelectedValue != "")
            {
                mode.ListPagePostFix = this.ddlListPagePostFix.SelectedValue;
            }
            mode.IsCreateContentPage = radCreateContentPageTrue.Checked ? false : true;
            
            if (radContentPageSavePathType1.Checked)
            {
                mode.ContentPageHtmlRule = "1";
            }
            else if (radContentPageSavePathType2.Checked)
            {
                mode.ContentPageHtmlRule = "2";
            }
            else if (radContentPageSavePathType3.Checked)
            {
                mode.ContentPageHtmlRule = "3";
            }
            else if (radContentPageSavePathType4.Checked)
            {
                mode.ContentPageHtmlRule = "4";
            }
            else if (radContentPageSavePathType5.Checked)
            {
                mode.ContentPageHtmlRule = "5";
            }
            else if (radContentPageSavePathType6.Checked)
            {
                mode.ContentPageHtmlRule = "6";
            }
            else if (radContentPageSavePathType8.Checked)
            {
                mode.ContentPageHtmlRule = "8";
            }
            else if (radContentPageSavePathType7.Checked)
            {
                if (txtzdyURL.Text.Trim() == "")
                {
                    Utils.RunJavaScript(this, "type=2;errmsg='内容页路径规则你选择的是自定义，必须填写自定义路径';showMessage();");
                    return;
                }
                string zdyURL = "/"+txtzdyURL.Text+"/";
                zdyURL = zdyURL.Replace("//", "/");
                mode.ContentPageHtmlRule = "7";
                mode.CustomManageLink = zdyURL;
            }

            //检测伪静态路径是否存在重复
            if (chkIsFolder.Checked == false)
            {
                string[] checkDirArr = CheckDirPath(mode.NodeName.Replace (" ",""), mode.NodeDir.Replace (" ",""), mode.ContentPageHtmlRule, mode.CustomManageLink,mode.ModuleID.ToString());
                if (checkDirArr[0] == "True")
                {
                    if (txtzdyURL.Text.Trim() == "")
                    {
                        Utils.RunJavaScript(this, "type=2;errmsg='根据你设置的内容页路径（路径是“" + checkDirArr[1] + "”选项，存在重复，请重新设置';showMessage();");
                        return;
                    }
                }
                mode.CustomManageLink = checkDirArr[1];
            }

            if (radAutoCreateHtmlType1.Checked)
            {
                mode.AutoCreateHtmlType = 1;
            }
            else if (radAutoCreateHtmlType2.Checked)
            {
                mode.AutoCreateHtmlType = 2;
            }
            else if (radAutoCreateHtmlType3.Checked)
            {
                mode.AutoCreateHtmlType = 3;
            }
            else if (radAutoCreateHtmlType4.Checked)
            {
                mode.AutoCreateHtmlType = 4;
            }
            else if (radAutoCreateHtmlType5.Checked)
            {
                mode.AutoCreateHtmlType = 5;
            }
            else
            {
                mode.AutoCreateHtmlType = 6;
            }
            mode.ColumnType = 1;

            //自定义内容赋值

            //取自定义数量
            int CustomContentCount = Utils.ParseInt(ddlCustomContentCount.SelectedValue, 1);
            //自定义内容，初始化为第一个TextBox.Text
            string strCustomContent = txtCustomContent1.Text + "<hqb>" + Utils.ParseInt(Request.Form["chkIsHtmlEditor1"], 0);
            for (int i = 2; i <= CustomContentCount; i++)
            {
                string strTemp = Request.Form["txtCustomContent" + i.ToString()];
                strCustomContent += "###" + strTemp + "<hqb>" + Utils.ParseInt(Request.Form["chkIsHtmlEditor" + i.ToString()], 0);
            }
            mode.Custom_Content = strCustomContent;
            mode.Banner = txtBanner.Text;
            #endregion

            if (Action == "EDIT")
            {
                #region 修改节点
                // 权限验证，是否具有修改权限

                if (IsHaveRightByOperCode("Edit"))
                {
                    mode.NodeCode = Utils.HtmlEncode(txtNodeCode.Text.Trim());
                    mode.ParentNode = NCode.Substring(0, NCode.Length - 3);


                    //根据NodeCode判断它下面是否有子栏目

                    dtNodeCode = bllModuleNode.GetList("ALLBY", Utils.getTwoParams(SiteID.ToString(), NCode));
                    if (dtNodeCode.Rows.Count == 0)
                    {
                        //如果没有子栏目，用户可随意修改[父级栏目]属性，如果有子栏目，则不给它赋值

                        mode.NodeType = (Utils.ParseBoolToInt(chkIsFolder.Checked)).ToString();
                    }
                    else  //如果有子栏目，则NodeType必须为1
                    {
                        mode.NodeType = "1";
                    }

                    if (mode.ParentNode == "")
                    {
                        mode.ParentNode = "0";
                    }

                    returnMsg = bllModuleNode.Save("EDIT", mode);
                    string logTitle = Request.Form["hidLogTitle"];
                    if (logTitle != txtNodeName.Text)
                    {
                        logTitle = logTitle + " => " + txtNodeName.Text;
                    }
                    returnMsg = bllModuleNode.Save("EDIT", mode);
                    if (Utils.ParseInt(returnMsg, 0) > 0)
                    {
                        DataTable parentDt = bllModuleNode.GetModeNodeFromCache();
                        string parentNodeCode;
                        if (NCode.Length > 3)
                            parentNodeCode = NCode.Substring(0, NCode.Length - 3);
                        else
                            parentNodeCode = NCode;

                        DataRow[] parentdr = parentDt.Select("NodeCode='" + parentNodeCode + "'");
                        if (parentdr.Length > 0)
                        {
                            editSelfMenuUrl = "ColumnEdit.aspx?Action=New&NodeID=" + parentdr[0]["NodeId"].ToString() + "&ID=" + parentdr[0]["NodeId"].ToString() + "&NCode=" + parentNodeCode + "&NodeCode=" + NodeCode + "&IsParent=1";
                        }
                        WriteLog(GetLogValue(logTitle, "Update", "ModelNodeEdit", true), "", 2); //写日志
                        Utils.RunJavaScript(this, "type=1;title='" + txtNodeName.Text.Trim().Replace("'", "\\'") + "';editSelfMenuUrl='" + editSelfMenuUrl + "';showMessage();");
                    }
                    else
                    {
                        WriteLog(GetLogValue(logTitle, "Update", "ModelNodeEdit", false), returnMsg, 3); //写日志
                        Utils.RunJavaScript(this, "type=2;errmsg='" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "';showMessage();");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有修改栏目的权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }
            else
            {
                #region 新增节点
                //判断是否有新增权限

                if (IsHaveRightByOperCode("New"))
                {
                    //根据IsParent判断它是不是父节点，如果不是则不能添加子节点
                    if (IsParent == "1")
                    {
                        //如果是子栏目，则必须要选择栏目类型
                        if (!chkIsFolder.Checked && ddlModel.SelectedValue == "0")
                        {
                            Utils.RunJavaScript(this, "alert({msg:'请选择栏目类型！',title:'提示信息'})");
                            return;
                        }
                        //根据NodeCode得到子栏目下最大的NodeCode
                        dtNodeCode = bllModuleNode.GetList("MAXCODE", Utils.getOneParams(this.NCode));
                        if (dtNodeCode != null && dtNodeCode.Rows.Count == 1)
                        {
                            if (dtNodeCode.Rows[0]["NodeCode"].ToString() != "")
                                mode.NodeCode = dtNodeCode.Rows[0]["NodeCode"].ToString();
                            else  //如果没有NodeCode后面三位从001开始

                                mode.NodeCode = this.NCode + "001";
                        }

                        mode.NodeType = (Utils.ParseBoolToInt(chkIsFolder.Checked)).ToString();
                        mode.ParentNode = NCode;

                        //如果是添加一级栏目，得到一级栏目最大的NodeCode+1
                        if (NCode == "0")
                        {
                            DataTable dt = bllModuleNode.GetList("MAXPCODE", Utils.getOneParams(""));
                            if (dt.Rows.Count < 1)
                            {
                                mode.NodeCode = this.NCode;
                                mode.ParentNode = "0";
                            }
                        }

                        returnMsg = bllModuleNode.Save("NEW", mode);
                        editSelfMenuUrl = "ColumnEdit.aspx?Action=Edit&NodeID=" + mode.NodeID + "&ID=" + mode.NodeID + "&NCode=" + mode.NodeCode + "&NodeCode=" + NodeCode + "&ColumnType=1";
                        if (Utils.ParseInt(returnMsg, 0) > 0)
                        {
                            WriteLog("新增栏目" + txtNodeName.Text + "成功！", "", 2);//写入操作日志
                            Utils.RunJavaScript(this, "type=0;title='" + txtNodeName.Text.Trim().Replace("'", "\\'") + "';editSelfMenuUrl='" + editSelfMenuUrl + "';showMessage();");
                        }
                        else
                        {

                            WriteLog("新增栏目" + txtNodeName.Text + "失败！", returnMsg, 2);//写入操作日志
                            Utils.RunJavaScript(this, "type=2;errmsg='" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "';showMessage();");
                        }
                    }
                    else//如果不是则不能添加子节点
                    {
                        Utils.RunJavaScript(this, "alert({msg:'上级栏目不是父栏目，不能添加子栏目！',title:'提示信息'})");
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有新增栏目的权限，请联系站点管理员！',title:'提示信息'})");
                }

                //当JS报错，不弹出框，用户可能会重复点击新增，避免产生的NodeID重复
                txtNodeID.Text = Guid.NewGuid().ToString();  

                #endregion
            }

            //保存权限
            SaveRightDate(mode.NodeID.ToString());
        }

        #region 检查伪静态路径是否存在重复
        string[] CheckDirPath(string NodeName, string NodelEngDesc, string ContentPageHtmlRule, string CustomManageLink, string ModuleID)
        {
            string[] reArr = new string[2];
            string re = "";
            string publishtype = KingTop.WEB.BasePage.PublishType;
            



            if (ContentPageHtmlRule == "7")
            {
                re = CustomManageLink;
            }
            else
            {
                string nodeList = string.Empty;
                string parentCode = NCode;
                if (Action == "EDIT")
                {
                    parentCode = NCode.Substring(0, NCode.Length - 3);
                }


                for (int i = 3; i <= parentCode.Length; i = i + 3)
                {
                    nodeList += "','" + parentCode.Substring(0, i);
                }
                //nodeList += "'" + NCode + "'";
                nodeList = "'" + nodeList + "'";
                string sql = "select ContentPageHtmlRule,NodeName,NodelEngDesc,IsCreateContentPage,NodeDir from K_SysModuleNode where NodeCode in (" + nodeList + ") order by NodeCode";
                DataTable dt = SQLHelper.GetDataSet(sql);
                string[] DirArr = new string[8] { "", "", "", "", "", "", "", "" };

                if (dt.Rows.Count > 0)
                {
                    string dirStr1 = string.Empty;  //英文路径
                    string dirStr2 = string.Empty;  //栏目名称路径
                    string firstDir = string.Empty; //第一个栏目英文名称
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[j]["NodeDir"].ToString()))
                        {
                            if (string.IsNullOrEmpty(firstDir))
                            {
                                firstDir = "/" + dt.Rows[j]["NodeDir"].ToString().Replace(" ", "") + "/";
                            }
                            dirStr1 += dt.Rows[j]["NodeDir"].ToString().Replace(" ", "") + "/";
                            dirStr2 += dt.Rows[j]["NodeName"].ToString().Replace(" ", "") + "/";
                        }
                    }

                    if (string.IsNullOrEmpty(firstDir))  //为空的情况，只有一级栏目，一级栏目的信息保存到英文名称下
                    {
                        firstDir = "/" + NodelEngDesc + "/";
                    }
                    DirArr[0] = "/c/{date}/";
                    DirArr[1] = "/c/";
                    DirArr[2] = firstDir;
                    DirArr[3] = firstDir + "{date}/";
                    DirArr[4] = "/" + dirStr2;
                    DirArr[5] = "/" + dirStr1;

                }

                switch (ContentPageHtmlRule)
                {
                    case "1":
                        re = DirArr[0];
                        break;
                    case "2":
                        re = DirArr[1];
                        break;
                    case "3":
                        re = DirArr[2];
                        break;
                    case "4":
                        re = DirArr[3];
                        break;
                    case "5":
                        re = DirArr[4] + NodeName + "/";
                        break;
                    case "6":
                        re = DirArr[5] + NodelEngDesc + "/";
                        break;
                    case "8":
                        re ="/"+ DirArr[2].Replace ("//","").Replace("/","")+"_";
                        break;

                }
            }

            if (publishtype == "1")  //当是动态的时候，即使有重复，也不提示错误
            {
                string sqlStr;
                if (Action == "EDIT")
                {
                    sqlStr = "select NodeCode From K_SysModuleNode Where CustomManageLink='" + re.Replace ("//","/") + "' and ModuleID<>'" + ModuleID + "' and NodeCode!='" + NCode + "'";
                }
                else
                {
                    sqlStr = "select NodeCode From K_SysModuleNode Where CustomManageLink='" + re.Replace("//", "/") + "' and ModuleID<>'" + ModuleID + "'";
                }

                DataTable dt2 = SQLHelper.GetDataSet(sqlStr);
                reArr[0] = "False";
                if (dt2.Rows.Count > 0)
                {
                    reArr[0] = "True";
                }
            }
            else
            {
                reArr[0] = "False";
            }
            reArr[1] = re;

            return reArr;
        }
        #endregion


        protected void btnDel_Click(object sender, EventArgs e)
        {
            //判断权限
            if (IsHaveRightByOperCode("Delete"))
            {
                string returnMsg = bllModuleNode.ModuleNodeSet("DELONE", "", ID);
                try
                {
                    if (Convert.ToInt32(returnMsg) == -1)
                    {
                        Utils.RunJavaScript(this, "alert({msg:'该栏目包含子栏目，不能直接删除，请先删除其子栏目！',title:'提示信息'})");
                    }
                    else
                    {
                        WriteLog("删除" + hidLogTitle.Value + "节点成功！", "", 2);//写入操作日志
                        Utils.RunJavaScript(this, "type=3;title='" + hidLogTitle.Value.Replace("'", "\\'") + "';showMessage();");
                    }
                }
                catch
                {
                    WriteLog("删除" + hidLogTitle.Value + "节点失败！", returnMsg, 2);//写入操作日志
                    Utils.RunJavaScript(this, "type=2;errmsg='" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "';showMessage();");
                }
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'你没有删除栏目的权限，请联系站点管理员！',title:'提示信息'})");
            }
        }

        protected void chkIsFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkIsFolder.Checked)
            {
                this.ddlModel.SelectedValue = "0";
                this.ddlModel.Enabled = false;
                lblModel.Text = "";
                dlContentTemplate.Visible = false;
                dlListPageTemplate.Visible = false;
            }
            else
            {
                this.ddlModel.Enabled = true;
                lblModel.Text = "*";
                dlContentTemplate.Visible = true;
                dlListPageTemplate.Visible = true;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ColumnManage.aspx?NodeCode=" + NodeCode);
        }

        protected void SaveRightDate(string strNodeID)
        {
            //得到操作列表           
            KingTop.BLL.SysManage.ActionPermit bllActionPer = new ActionPermit();
            DataTable dt2 = bllActionPer.GetList("MODULE", Utils.getOneParams(ddlModel.SelectedValue));
            if (AppCache.IsExist("ModuleActionCache"))
            {
                AppCache.Remove("ModuleActionCache");
            }

            //if (AppCache.IsExist("ModeNodeAndModuleCache"))
            //{
            //    AppCache.Remove("ModeNodeAndModuleCache");
            //}

            if (AppCache.IsExist("UserGroupPermitCache"))
            {
                AppCache.Remove("UserGroupPermitCache");
            }

            for (int i = 0; i < grvRight.Rows.Count; i++)
            {
                ArrayList arrRightSet = new ArrayList();
                foreach (DataRow dr in dt2.Rows)
                {
                    string ckbid = "ckb" + dr["ID"].ToString();
                    CheckBox ckb = (CheckBox)grvRight.Rows[i].FindControl(ckbid);
                    if (ckb.Checked && ckb.Enabled == true)
                    {
                        arrRightSet.Add(dr["ID"].ToString());
                    }
                }
                //更新权限
                if (arrRightSet.Count > 0)
                {
                    ColumnRightTool coluRightTool = new ColumnRightTool();
                    string strUserGroupCode = grvRight.Rows[i].Cells[0].Text;
                    coluRightTool.SaveData(strUserGroupCode, arrRightSet, strNodeID);
                }
            }
        }

        //解决隐藏GridView隐藏列取不到值的问题
        protected void grvRight_RowCreated1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow ||
                    e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Visible = false;
            }
        }

        #region 初始化栏目的MetaKeyword和MetaDiscript
        private void MetaInit()
        {
            string metaKW = string.Empty;
            string metaDisc = string.Empty;
            if (Action == "NEW")
            {
                if (NCode.Length == 3)  //一级栏目，读取配置
                {
                    string xmlPath = Server.MapPath("/" + SiteDir + "/config/SiteInfo.config");
                    metaKW = Utils.XmlRead(xmlPath, "SiteInfoConfig/MetaKeywords", "");
                    metaDisc = Utils.XmlRead(xmlPath, "SiteInfoConfig/MetaDescription", "");
                }
                else  //读取上级栏目
                {
                    DataTable dt = bllModuleNode.GetList("LISTBYNODECODE", Utils.getOneParams(NCode));
                    if (dt.Rows.Count > 0)
                    {
                        metaKW = dt.Rows[0]["Meta_Keywords"].ToString();
                        metaDisc = dt.Rows[0]["Meta_Description"].ToString();
                    }
                }
                this.txtMetaDesc.Text = metaDisc;
                this.txtKeyWords.Text = metaKW;
            }
        }
        #endregion
    }
}
