
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.Model;
using System.Data;
using System.Text;
using KingTop.BLL.SysManage;
using System.Data.SqlClient;

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class PublicOperEdit : KingTop.Web.Admin.AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PageInit();
            }
        }

        #region 初始化页面控件赋值

        private void PageInit()
        {
            if (this.Action == "EDIT")
            {
                string sql = "select * from K_SysPublicOper Where OperName=@OperName";
                SqlParameter param =  new SqlParameter("@OperName", this.ID);

                SqlDataReader  dr=SQLHelper .ExecuteReader(SQLHelper.ConnectionStringLocalTransaction,CommandType.Text ,sql,param);

     
                btnEdit.Text = Utils.GetResourcesValue("Common", "Update");

                //赋值
                if (dr.Read())
                {
                    txtTitle.Text =dr["Title"].ToString ();
                    txtOperName.Text =dr["OperName"].ToString ();
                    txID.Text = dr["OperName"].ToString();
                    txtModuleName.Text =dr["ModuleName"].ToString ();
                    hidLogTitle .Value =dr["OperName"].ToString ();
                    RBL_IsValid.SelectedValue = dr["IsValid"].ToString() == "False" ? "0" : "1";
                }
            }
            else
            {
                //按钮文本改为“添加”

                btnEdit.Text = Utils.GetResourcesValue("Common", "Add");

            }
        }
        #endregion

        #region 按钮事件
        protected void btnEdit_Click(object sender, EventArgs e)
        {

            string sql = string.Empty;
            string sql1 = string.Empty;
            SqlParameter[] param1;
            SqlDataReader dr;
            SqlParameter[] param;
            // 修改操作
            if (this.Action == "EDIT")
            {
                #region 修改操作
                // 权限验证，是否具有修改权限

                if (IsHaveRightByOperCode("Edit"))
                {
                     sql1 = "select * from K_SysPublicOper Where OperName=@OperName and OperName<>@OldOperName";
                     param1 = new SqlParameter[]{
                        new SqlParameter ("@OperName", txtOperName.Text),
                        new SqlParameter ("@OldOperName",txID .Text ),
                    };
                     dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql1, param1);
                    if (dr.HasRows)
                    {
                        Utils.RunJavaScript(this, "type=2;errmsg='英文名称"+txtOperName.Text.Replace ("'","")+"已经存在，修改失败';");
                        return;
                    }

                    sql = @"update K_SysPublicOper set
                          OperName=@OperName,
                          Title=@Title,
                          IsValid=@IsValid,
                          ModuleName=@ModuleName
                          WHERE
                          OperName=@OldOperName";

                    string moduleName = txtModuleName.Text;
                    if (moduleName.Length > 50)
                    {
                        moduleName = moduleName.Substring(0, 50);
                    }
                    param = new SqlParameter[]{
                        new SqlParameter ("@OperName",txtOperName .Text ),
                        new SqlParameter ("@OldOperName",txID .Text ),
                        new SqlParameter ("@Title",txtTitle.Text ),
                        new SqlParameter ("@IsValid",Utils.ParseBool(RBL_IsValid.SelectedValue)),
                        new SqlParameter ("@ModuleName",moduleName),
                    };

                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param);
                    Utils.RunJavaScript(this, "type=1;title='" + txtOperName.Text.Replace("'", "\\'") + "';");

                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'无权限,请联系管理员！',title:'提示信息'});window.location.href='PublicOperList.aspx?nodeCode=" + NodeCode + "'");
                }
                #endregion
            }
            else  //添加操作
            {
                #region   添加操作
                // 权限验证，是否具有新增权限

                if (IsHaveRightByOperCode("New"))
                {
                    //如果数据库存在相同记录就不能再添加

                    sql1 = "select * from K_SysPublicOper Where OperName=@OperName";
                    param1 = new SqlParameter[]{
                        new SqlParameter ("@OperName", txtOperName.Text),
                    };
                    dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql1, param1);
                    if (dr.HasRows)
                    {
                        Utils.RunJavaScript(this, "type=2;errmsg='英文名称" + txtOperName.Text.Replace("'", "") + "已经存在，修改失败';");
                        return;
                    }

                    sql = "insert into K_SysPublicOper(OperName,Title,IsValid,ModuleName,IsPublic) values(@OperName,@Title,@IsValid,@ModuleName,0)";
                    string moduleName = txtModuleName.Text;
                    if (moduleName.Length > 50)
                    {
                        moduleName = moduleName.Substring(0, 50);
                    }
                    param = new SqlParameter[]{
                        new SqlParameter ("@OperName",txtOperName .Text ),
                        new SqlParameter ("@Title",txtTitle.Text ),
                        new SqlParameter ("@IsValid",Utils.ParseBool(RBL_IsValid.SelectedValue)),
                        new SqlParameter ("@ModuleName",moduleName),
                    };

                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param);
                    Utils.RunJavaScript(this, "type=1;title='" + txtOperName.Text.Replace("'", "\\'") + "';");
                }
                else
                {
                    Utils.RunJavaScript(this, "alert({msg:'你没有新增操作的权限，请联系站点管理员！',title:'提示信息'})");
                }
                #endregion
            }
        }
        #endregion
    }
}
