﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Model;
using System.Data;
using KingTop.BLL.SysManage;
using KingTop.Web.Admin;
using System.Text;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      陈顺
    创建时间： 2010年3月31日
    功能描述： 栏目复制
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class ColumnCopy : AdminPage
    {
        #region 变量成员
        //逻辑类
        KingTop.BLL.SysManage.ModuleNode bllModuleNode = new KingTop.BLL.SysManage.ModuleNode();
        //实体类
        KingTop.Model.SysManage.ModuleNode mode = new KingTop.Model.SysManage.ModuleNode();
        //栏目列表
        private DataTable dt;
        //IsContainWebContent=1的节点的NodeCode 用于不产生"└"
        private string SysNodeCode = string.Empty;
        private StringBuilder sbsql = new StringBuilder();
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定ListBox
                this.BindDate();
            }
        }
        #endregion

        #region 绑定数据
        //绑定栏目名称
        private void BindDate()
        {
            lsbColumn1.Items.Clear();
            lsbColumn2.Items.Clear();
            dt = bllModuleNode.GetList("SITEWEB", Utils.getOneParams(this.SiteID.ToString()));
            if (dt.Rows.Count > 0)
            {
                ListItem ls = new ListItem(dt.Rows[0]["NodeName"].ToString(), dt.Rows[0]["NodeCode"].ToString());
                this.lsbColumn1.Items.Add(ls);
                this.lsbColumn2.Items.Add(ls);
            }
            for (int j = 1; j < dt.Rows.Count; j++)
            {
                DataRow dr = dt.Rows[j];
                //文件栏目
                if (dr["NodeType"].ToString() == "0")
                {
                    //根据NodeCode的长度产生多个"└"
                    string strMark = "└─";
                    for (int i = 6; i < dr["NodeCode"].ToString().Length; i = i + 3)
                    {
                        strMark = strMark + "└─";
                    }
                    ListItem ls = new ListItem(strMark + dr["NodeName"].ToString(), dr["NodeCode"].ToString());
                    this.lsbColumn1.Items.Add(ls);
                    //this.lsbColumn2.Items.Add(ls);
                }
                //文件夹栏目

                else
                {
                    //如果是IsContainWebContent=1的节点下面的栏目，不产生"└"
                    if (dr["ParentNode"].ToString() == SysNodeCode)
                    {
                        ListItem ls = new ListItem(dr["NodeName"].ToString(), dr["NodeCode"].ToString());
                        this.lsbColumn1.Items.Add(ls);
                        this.lsbColumn2.Items.Add(ls);
                    }
                    else
                    {
                        //根据NodeCode的长度产生多个"└"
                        string strMark = "└─";
                        for (int i = 6; i < dr["NodeCode"].ToString().Length; i = i + 3)
                        {
                            strMark = strMark + "└─";
                        }
                        ListItem ls = new ListItem(strMark + dr["NodeName"].ToString(), dr["NodeCode"].ToString());
                        this.lsbColumn1.Items.Add(ls);
                        this.lsbColumn2.Items.Add(ls);
                    }

                }
            }
        }
        #endregion

        #region 用到方法
        //判断传入NodeCode的栏目是否是父栏目

        private bool isParentColumn(string NodeCode)
        {
            bool boolReturn = false;

            dt = bllModuleNode.GetList("SITEWEB", Utils.getOneParams(this.SiteID.ToString()));
            DataRow[] dr = dt.Select("NodeCode='" + NodeCode + "'");
            if (dr[0]["NodeType"].ToString() == "1")
            {
                boolReturn = true;
            }
            return boolReturn;
        }

        #endregion

        #region 保存，复制栏目
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsHaveRightByOperCode("Edit"))
            {
                string fromMenuTitle = string.Empty;
                string toMenuTitle = string.Empty;
                string strNodeCode1 = Request.Form["lsbColumn1"];
                string strNodeCode2 = Request.Form["lsbColumn2"];
                bool isDataCopy = Utils.ParseBool(Utils.ParseInt(Request.Form["chkIsDataCopy"],0));
                for (int i = 0; i < lsbColumn1.Items.Count; i++)
                {
                    if (lsbColumn1.Items[i].Value == strNodeCode1)
                    {
                        fromMenuTitle = lsbColumn1.Items[i].Text;
                        break;
                    }
                }

                for (int i = 0; i < lsbColumn2.Items.Count; i++)
                {
                    if (lsbColumn2.Items[i].Value == strNodeCode2)
                    {
                        toMenuTitle = lsbColumn2.Items[i].Text;
                        break;
                    }
                }

                //移动项和目标项相同
                if (strNodeCode1 == strNodeCode2)
                {
                    Utils.RunJavaScript(this, "alert({msg:'你选择的复制项和目标项相同，不能复制栏目！',title:'提示信息'})");
                    return;
                }
                //移动项已经在目标项下
                if (strNodeCode1.Substring(0, strNodeCode1.Length - 3) == strNodeCode2)
                {
                    Utils.RunJavaScript(this, "alert({msg:'你选择的复制项已经在目标项下，不能复制栏目！',title:'提示信息'})");
                    return;
                }
                //目标项是移动项的子项
                if (strNodeCode2.IndexOf(strNodeCode1) == 0)
                {
                    Utils.RunJavaScript(this, "alert({msg:'你选择的复制项包含目标项，不能复制栏目！',title:'提示信息'})");
                    return;
                }
                //目标项不是父栏目
                if (!isParentColumn(strNodeCode2))
                {
                    Utils.RunJavaScript(this, "alert({msg:'目标项不是父节点，不能复制栏目！',title:'提示信息'})");
                    return;
                }

                string returnMsg = bllModuleNode.MenuCopy(strNodeCode1, strNodeCode2,isDataCopy);

                
                if(Utils .ParseInt(returnMsg,0)==1)
                {
                    WriteLog("栏目<" + fromMenuTitle + ">复制到<" + toMenuTitle + ">成功", "", 2);
                    Utils.RunJavaScript(this, "alert({msg:'复制栏目成功！',title:'提示信息'})");
                }
                else {
                    WriteLog("栏目<" + fromMenuTitle + ">复制到<" + toMenuTitle + ">失败", returnMsg, 3);
                    Utils.RunJavaScript(this, "alert({msg:'复制栏目失败:" + returnMsg.Replace("'", "\\'").Replace("\r\n", "<br>") + "！',title:'提示信息'})");
                }
                BindDate();
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'你没有复制栏目的权限，请联系站点管理员！',title:'提示信息'})");
            }
        }
        #endregion

        #region 根据传入TableName取FieldName
        private ArrayList getArrFieldName(string TableName)
        {
            ArrayList arrFieldName = new ArrayList();
            KingTop.BLL.Content.ModelField bllModelField = new KingTop.BLL.Content.ModelField();
            DataTable dtModeField = bllModelField.GetList("GETFIELDBYTABLENAME", Utils.getOneParams(TableName));
            foreach (DataRow dr in dtModeField.Rows)
            {
                arrFieldName.Add(dr["Name"].ToString());
            }
            return arrFieldName;
        }
        #endregion
    }
}
