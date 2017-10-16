using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using KingTop.IDAL.SysManage;
using System.Data;
using System.Configuration;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：陈顺
// 创建日期：2010-04-16
// 功能描述：对模板权限表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL.SysManage
{
    public class WebSiteTemplatePermit
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.SysManage.IWebSiteTemplatePermit dal = (IDAL.SysManage.IWebSiteTemplatePermit)Assembly.Load(path).CreateInstance(path + ".SysManage.WebSiteTemplatePermit");

        #region 根据传入的参数查询K_TemplateNode,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_TemplateNode,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion
    }
}
