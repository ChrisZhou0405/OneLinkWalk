using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Model;
using System.Data;
using KingTop.BLL.SysManage;
using System.Text;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      陈顺
    创建时间： 2010年3月31日
    功能描述： 模块编辑
 
// 更新日期        更新人      更新原因/内容
// 2010-4-2         肖丹       修改功能，代码优化，添加注释
--===============================================================*/
#endregion

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class ModelEdit : KingTop.Web.Admin.AdminPage
    {
        //模块逻辑类

        KingTop.BLL.SysManage.Module BllModule = new KingTop.BLL.SysManage.Module();
        //模块实体类

        KingTop.Model.SysManage.Module mode = new KingTop.Model.SysManage.Module();
        //模块编码
        public string ModuleCode = "";
        //操作实体类

        KingTop.Model.SysManage.ActionPermit Actionmode = new KingTop.Model.SysManage.ActionPermit();
        //操作逻辑类

        KingTop.BLL.SysManage.ActionPermit BllAction = new KingTop.BLL.SysManage.ActionPermit();
        StringBuilder sbLog = new StringBuilder(16);
        public string myOperCode = ",";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                Init();
            CreateControls();
        }

        #region 页面初始化

        private void Init()
        {
            if (this.Action == "EDIT")//修改
            {
                DataTable dt = BllModule.GetList("ONE", Utils.getOneParams(this.ModuleID));

                //按钮文本显示为“修改”

                btnEdit.Text = Utils.GetResourcesValue("Common", "Update");

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    txtModuleID.Text = ModuleID;
                    txtModuleCode.Text = Utils.HtmlDecode(dr["ModuleCode"].ToString());
                    this.hidLogTitle.Value = Utils.HtmlDecode(dr["ModuleName"].ToString());
                    txtModuleName.Text = Utils.HtmlDecode(dr["ModuleName"].ToString());
                    txtModuleEncDesc.Text = Utils.HtmlDecode(dr["ModuleEncDesc"].ToString());
                    txtLinkUrl.Text = Utils.HtmlDecode(dr["LinkUrl"].ToString());
                    RBL_IsValid.SelectedValue = Utils.HtmlDecode(dr["IsValid"].ToString());
                    chkIsSystem.Checked = Utils.ParseBool(dr["IsSystem"]);
                    txtModuleOrder.Text = Utils.HtmlDecode(dr["ModuleOrder"].ToString());
                    txtModuleDesc.Text = Utils.HtmlDecode(dr["ModuleDesc"].ToString());
                    if (Utils.ParseInt(dr["ModuleType"].ToString(), 1) == 2)
                    {
                        chkModuleType.Checked = true;
                    }
                }

                #region 根据模块ID查找操作表有记录选中该CheckBox @author 陈顺 by 2010-04-07
                DataTable dtAction = BllAction.GetList("MODULE", Utils.getOneParams(this.ModuleID));
                if (dtAction.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAction.Rows.Count; i++)
                    {
                        DataRow dr = dtAction.Rows[i];
                        //CheckBox tmpcb = (CheckBox)this.OperTD.FindControl(dr["OperEngDesc"].ToString());
                        //if (tmpcb != null)
                        //{
                        //    tmpcb.Checked = true;
                        //    tmpcb.Enabled = false;
                        //}
                        myOperCode += dr["OperEngDesc"].ToString() + ",";
                    }
                }
                hidoldOperCode.Value = myOperCode;
                #endregion
            }
            if (this.Action == "NEW")//新增
            {
                //按钮文本显示为“修改”

                btnEdit.Text = Utils.GetResourcesValue("Common", "Add");
                //模块ID自动生成
                txtModuleID.Text = Guid.NewGuid().ToString();
            }
        }
        #endregion

        #region 动态创建CheckBox  @author 陈顺 by 2010-04-07
        protected void CreateControls()
        {
            DataTable dt = new DataTable();
            KingTop.BLL.SysManage.PublicOper bllPublicOper = new KingTop.BLL.SysManage.PublicOper();
            dt = bllPublicOper.GetList("ALL", Utils.getOneParams(""));
            DataRow[] dr1 = dt.Select("IsValid=1");
            System.Web.UI.HtmlControls.HtmlTable t = new System.Web.UI.HtmlControls.HtmlTable();
            int k = 0;
            int drLen = dr1.Length;
            if (drLen > 0)
            {
                for (int i = 0; i < drLen; i = i + 6)
                {

                    System.Web.UI.HtmlControls.HtmlTableRow row = new System.Web.UI.HtmlControls.HtmlTableRow();
                    for (int j = 0; j < 6; j++)
                    {
                        k = i + j;
                        if (k == drLen)
                        {
                            break;
                        }
                        DataRow dr = dr1[k];
                        if (dr["IsValid"].ToString() == "False")
                            continue;

                        System.Web.UI.HtmlControls.HtmlTableCell cell = new System.Web.UI.HtmlControls.HtmlTableCell();
                        System.Web.UI.HtmlControls.HtmlInputCheckBox chkbox1 = new System.Web.UI.HtmlControls.HtmlInputCheckBox();
                        chkbox1.Name = "OperName";
                        chkbox1.Value = dr["OperName"].ToString();

                        string s = string.Empty;
                        if (myOperCode.IndexOf("," + dr["OperName"].ToString() + ",") != -1)
                            s = "<input id=\"" + dr["OperName"].ToString() + "\" type=\"checkbox\" name=\"OperName\" value=\"" + dr["OperName"].ToString() + "|" + dr["Title"].ToString() + "\" checked/><label for=\"" + dr["OperName"].ToString() + "\">" + dr["Title"].ToString() + "</label>";
                        else
                            s = "<input id=\"" + dr["OperName"].ToString() + "\" type=\"checkbox\" name=\"OperName\" value=\"" + dr["OperName"].ToString() + "|" + dr["Title"].ToString() + "\" /><label for=\"" + dr["OperName"].ToString() + "\">" + dr["Title"].ToString() + "</label>";

                        CheckBox chkbox = new CheckBox();
                        chkbox.ID = dr["OperName"].ToString();
                        chkbox.Text = dr["Title"].ToString();
                        cell.Width = "120px";
                        cell.InnerHtml = s;
                        row.Cells.Add(cell);
                    }
                    t.Controls.Add(row);
                }
            }
            this.OperTD.Controls.Add(t);
        }
        #endregion

        #region 返回选中的CheckBox信息  @author 陈顺 by 2010-04-07
        protected string GetCheckResout()
        {
            string strRrsout = string.Empty;
            //if (this.Action == "EDIT")
            //{
            //    foreach (Control findc in this.OperTD.Controls)
            //    {
            //        if (findc is CheckBox)
            //        {
            //            CheckBox tmpcb = (CheckBox)findc;
            //            if (tmpcb.Checked && tmpcb.Enabled==true)
            //            {
            //                strRrsout = strRrsout + tmpcb.ID + ",";
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (Control findc in this.OperTD.Controls)
            //    {
            //        if (findc is CheckBox)
            //        {
            //            CheckBox tmpcb = (CheckBox)findc;
            //            if (tmpcb.Checked)
            //            {
            //                strRrsout = strRrsout + tmpcb.ID + ",";
            //            }
            //        }
            //    }
            //}
            strRrsout = Request.Form["OperName"];
            if (strRrsout == null)
                strRrsout = "";

            return strRrsout;
        }
        #endregion

        #region 根据模块ID删除该模块的所有操作 @author 陈顺 by 2010-04-07
        protected void DeleteByModelID(string ModelID)
        {
            DataTable dtAction = BllAction.GetList("MODULE", Utils.getOneParams(ModelID));
            string stridlist = string.Empty;
            if (dtAction.Rows.Count > 0)
            {
                for (int i = 0; i < dtAction.Rows.Count; i++)
                {
                    DataRow dr = dtAction.Rows[i];
                    stridlist = stridlist + dr["ID"].ToString() + ",";
                }
            }
            if (stridlist.Length > 0)
            {
                stridlist = stridlist.Substring(0, stridlist.Length - 1);
                BllAction.ActionPermitSet("DEL", "", stridlist);
            }
        }
        #endregion

        #region 根据模块选的操作向操作表里插数据 @author 陈顺 by 2010-04-07
        protected bool SetNewAction(KingTop.Model.SysManage.Module mode)
        {
            bool result = false;
            string returnMsg2 = "";
            string OperCodeInfo = GetCheckResout();
            OperCodeInfo = OperCodeInfo.Replace(" ", "");
            string[] strOperCode = OperCodeInfo.Split(',');
            string newOperCode = string.Empty;
            for (int i = 0; i < strOperCode.Length; i++)
            {
                if (string.IsNullOrEmpty(strOperCode[i]))
                    continue;

                if (string.IsNullOrEmpty(newOperCode))
                {
                    newOperCode = strOperCode[i].Split('|')[0];
                }
                else
                {
                    newOperCode += "," + strOperCode[i].Split('|')[0];
                }
            }

            if (!string.IsNullOrEmpty(OperCodeInfo))
            {
                //先删除以前选择的操作
                if (Action == "EDIT")
                {
                    //删除旧的操作，现在又去掉了
                    string[] arrAct = hidoldOperCode.Value.Split(',');
                    string newOperCode1 = "," + newOperCode + ",";
                    string deleteOperCode = string.Empty; ;
                    for (int i = 0; i < arrAct.Length; i++)
                    {
                        if (string.IsNullOrEmpty(arrAct[i].Trim()))
                            continue;

                        if (newOperCode1.IndexOf("," + arrAct[i].Trim() + ",") == -1)
                        {
                            if (string.IsNullOrEmpty(deleteOperCode))
                            {
                                deleteOperCode = arrAct[i].Trim();
                            }
                            else
                                deleteOperCode += "," + arrAct[i].Trim();
                        }
                    }

                    if (!string.IsNullOrEmpty(deleteOperCode))
                    {
                        string s = "DELETE FROM K_SysAccountPermit WHERE PermitCode in (SELECT ID FROM K_SysActionPermit WHERE ModuleID='" + mode.ModuleID + "' and OperEngDesc IN ('" + deleteOperCode.Replace(",", "','") + "'));";
                        s += "DELETE FROM K_SysRolePermit WHERE PermitCode in (SELECT ID FROM K_SysActionPermit WHERE ModuleID='" + mode.ModuleID + "' and OperEngDesc IN ('" + deleteOperCode.Replace(",", "','") + "'));";
                        s += "DELETE FROM K_SysUserGroupPermit WHERE PermitCode in (SELECT ID FROM K_SysActionPermit WHERE ModuleID='" + mode.ModuleID + "' and OperEngDesc IN ('" + deleteOperCode.Replace(",", "','") + "'));";
                        s += "DELETE FROM K_SysActionPermit WHERE ModuleID='" + mode.ModuleID + "' and OperEngDesc IN ('" + deleteOperCode.Replace(",", "','") + "')";
                        BllAction.ActionPermitSet("MODULEDEL", "", s);
                    }
                }
                string insertOperCode = string.Empty;
                string[] arrAct2 = newOperCode.Split(',');
                string oldOperCode = "," + hidoldOperCode.Value + ",";
                for (int i = 0; i < arrAct2.Length; i++)
                {
                    if (string.IsNullOrEmpty(arrAct2[i].Trim()))
                        continue;

                    if (oldOperCode.IndexOf("," + arrAct2[i].Trim() + ",") == -1)
                    {
                        if (string.IsNullOrEmpty(insertOperCode))
                        {
                            insertOperCode = arrAct2[i].Trim();
                        }
                        else
                            insertOperCode += "," + arrAct2[i].Trim();
                    }
                }

                if (string.IsNullOrEmpty(insertOperCode))
                {
                    return true;
                }
                string[] arrInsertOperCode = insertOperCode.Split(',');
                for (int i = 0; i < arrInsertOperCode.Length; i++)
                {
                    Actionmode.ID = Guid.NewGuid();
                    Actionmode.ModuleID = mode.ModuleID;
                    //operCode
                    DataTable dt = BllAction.GetList("MAXCODE", Utils.getOneParams(mode.ModuleID.ToString()));
                    if (dt.Rows[0]["opercode"].ToString() != "")
                    {
                        Actionmode.operCode = dt.Rows[0]["opercode"].ToString();
                    }
                    else
                    {
                        Actionmode.operCode = mode.ModuleCode + "001";
                    }
                    //待修改
                    string operName = string.Empty;
                    for (int j = 0; j < strOperCode.Length; j++)
                    {
                        if (strOperCode[j].ToString().Split('|')[0].Trim() == arrInsertOperCode[i])
                        {
                            operName = strOperCode[j].ToString().Split('|')[1];
                            break;
                        }
                    }
                    Actionmode.OperName = operName;
                    Actionmode.OperOrder = "0";
                    Actionmode.IsValid = true;
                    Actionmode.OperDesc = Actionmode.OperName;
                    Actionmode.OperEngDesc = arrInsertOperCode[i];
                    Actionmode.IsDefaultOper = false;
                    Actionmode.IsSystem = false;
                    returnMsg2 = BllAction.Save("NEW", Actionmode);
                    try
                    {
                        if (Convert.ToInt32(returnMsg2) > 0)
                        {
                            result = true;
                        }
                    }
                    catch
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        #endregion

        #region 按钮事件
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string returnMsg = "";

            if (this.Action == "EDIT")
            {
                #region 修改模块
                //判断权限
                if (IsHaveRightByOperCode("Edit"))
                {
                    mode.ModuleID = new Guid(txtModuleID.Text);
                    mode.ModuleCode = Utils.HtmlEncode(txtModuleCode.Text);
                    mode.ModuleName = Utils.HtmlEncode(txtModuleName.Text.Trim());
                    mode.ModuleEncDesc = Utils.HtmlEncode(txtModuleEncDesc.Text.Trim());
                    if (txtLinkUrl.Text.Length > 0)
                    {
                        mode.LinkURL = Utils.HtmlDecode(txtLinkUrl.Text.Trim());
                    }
                    mode.IsSystem = Utils.ParseBool(chkIsSystem.Checked);
                    mode.IsValid = Utils.ParseBool(RBL_IsValid.SelectedValue);
                    mode.ModuleOrder = Utils.HtmlEncode(txtModuleOrder.Text.Trim());
                    mode.ModuleDesc = Utils.HtmlEncode(txtModuleDesc.Text.Trim());
                    if (chkModuleType.Checked == true)
                    {
                        mode.ModuleType = 2;
                    }
                    else
                    {
                        mode.ModuleType = 1;
                    }
                    //修改模块信息
                    returnMsg = BllModule.Save("EDIT", mode);
                    //删除当前模块下所有操作

                    //DeleteByModelID(mode.ModuleID.ToString());
                    //新增操作
                    SetNewAction(mode);

                    string logTitle = Request.Form["hidLogTitle"];
                    if (logTitle != txtModuleName.Text)
                    {
                        logTitle = logTitle + " => " + txtModuleName.Text;
                    }
                    try
                    {
                        if (Convert.ToInt32(returnMsg) > 0)
                        {
                            Utils.RunJavaScript(this, "alertClose({msg:'修改模板成功！',title:'提示信息'},function(){location.href='ModelList.aspx" + StrPageParams + "'})");
                            WriteLog(GetLogValue(logTitle, "Update", "ModelEdit", true), "", 2); //写日志

                        }
                    }
                    catch
                    {
                        Utils.RunJavaScript(this, "alert({msg:" + returnMsg + ",title:'提示信息'})");
                        WriteLog(GetLogValue(logTitle, "Update", "ModelEdit", false), returnMsg, 3); //写日志

                    }
                }
                else
                {
                    sbLog.Append("失败，无权限！");
                    Utils.RunJavaScript(this, "alert({msg:'你没有修改模块的权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }
            if (this.Action == "NEW")
            {
                #region 新增模块
                if (IsHaveRightByOperCode("New"))
                {
                    mode.ModuleID = new Guid(txtModuleID.Text);
                    if (txtLinkUrl.Text.Length > 0)
                    {
                        mode.LinkURL = Utils.HtmlDecode(txtLinkUrl.Text.Trim());
                    }
                    mode.ModuleName = Utils.HtmlEncode(txtModuleName.Text.Trim());
                    mode.ModuleEncDesc = Utils.HtmlEncode(txtModuleEncDesc.Text.Trim());
                    mode.IsSystem = Utils.ParseBool(chkIsSystem.Checked);
                    mode.IsValid = Utils.ParseBool(RBL_IsValid.SelectedValue);
                    mode.ModuleOrder = Utils.HtmlEncode(txtModuleOrder.Text.Trim());
                    mode.ModuleDesc = Utils.HtmlEncode(txtModuleDesc.Text.Trim());
                    if (chkModuleType.Checked == true)
                    {
                        mode.ModuleType = 2;
                    }
                    else
                    {
                        mode.ModuleType = 1;
                    }
                    //自动生成模块编码，比数据库最大的编码加1
                    mode.ModuleCode = BllModule.GetList("MAXCODE", Utils.getOneParams(mode.IsSystem.ToString())).Rows[0]["ModuleCode"].ToString();
                    //如果是非系统模块,模块代码从200开始

                    if (mode.ModuleCode == "")
                    {
                        mode.ModuleCode = "200";
                    }
                    //新增模块信息
                    returnMsg = BllModule.Save("NEW", mode);

                    //新增操作
                    SetNewAction(mode);

                    try
                    {
                        if (Convert.ToInt32(returnMsg) > 0)
                        {
                            //Utils.RunJavaScript(this, "alert({msg:'新增模块成功！',title:'提示信息'})"); 
                            WriteLog("新增" + txtModuleName.Text + "模块成功", "", 2);
                            Utils.RunJavaScript(this, "if(confirm('新增模块成功！点击[确定]继续新增模块，点[取消]回到模块列表页。')){location.href='ModelEdit.aspx?Action=New&nodeid=" + NodeID + "'}else{location.href='ModelList.aspx?&nodeid=" + NodeID + "'}");
                        }
                    }
                    catch
                    {
                        Utils.RunJavaScript(this, "alert({msg:" + returnMsg + ",title:'提示信息'})");
                        WriteLog("新增" + txtModuleName.Text + "模块失败", "", 2);
                    }
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有新增模块的权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }
        }
        #endregion

    }
}
