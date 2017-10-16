#region 引用程序集
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.IO;

using KingTop.IDAL;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-12-10
// 功能描述：商品相关 WebService// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.BLL.Template
{
    public class Publish
    {
        #region 变量成员
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.IPublish dal = (IDAL.Template.IPublish)Assembly.Load(path).CreateInstance(path + ".Template.Publish");
        #endregion

        #region 绑定发布面节点列表控件
        public void DropDownDataBind(ListBox ddl, int type, int siteID)
        {
            DataTable dt;

            dt = null;

            switch (type)
            {
                case 1:    // 前台栏目
                    dt = dal.GetSimpleMenuList(siteID);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        TreeBind(ddl, dt, dt.Rows[0]["ParentID"].ToString(), string.Empty,type);
                    }
                    break;
                case 2:     // 商城分类
                    dt = dal.GetSubCategoryList("3597", false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        TreeBind(ddl, dt, "3597", string.Empty, type);
                    }
                    break;
            }

        }
        #endregion

        #region 添加项
        private void TreeBind(ListBox ddl, DataTable dataSource, string parentID, string parentPrevStr,int type)
        {
            DataRow[] drNode;
            DataRow[] drChildNode;
            ListItem listItem;
            string currentPrevStr;

            drNode = null;
            drChildNode = null;

            switch (type)
            {
                case 1:     // 栏目
                    drNode = dataSource.Select("ParentID='" + parentID + "'");
                    break;
                case 2:     // 商品类型
                    drNode = dataSource.Select("ParentID=" + parentID);
                    break;
            }

            currentPrevStr = parentPrevStr;

            if (drNode != null)
            {
                foreach (DataRow dr in drNode)
                {
                    listItem = new ListItem();
                    listItem.Value = dr["ID"].ToString();

                    switch (type)
                    {
                        case 1:     // 栏目
                            drChildNode = dataSource.Select("ParentID='" + dr["ID"].ToString() + "'");
                            break;
                        case 2:     // 商品类型
                            drChildNode = dataSource.Select("ParentID=" + dr["ID"].ToString());
                            break;
                    }

                    if (drChildNode.Length > 0)
                    {
                        listItem.Text = currentPrevStr + "|-" + dr["Name"].ToString();
                    }
                    else
                    {
                        listItem.Text = currentPrevStr + "∟" + dr["Name"].ToString();
                    }

                    ddl.Items.Add(listItem);
                    TreeBind(ddl, dataSource, dr["ID"].ToString(), currentPrevStr + " | ", type);
                }
            }
        }
        #endregion
    }
}
