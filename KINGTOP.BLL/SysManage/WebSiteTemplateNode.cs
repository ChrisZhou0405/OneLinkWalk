using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using KingTop.IDAL.SysManage;
using System.Data;
using System.Configuration;
using KingTop.Common;


/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：陈顺
// 创建日期：2010-04-16
// 功能描述：对模板节点表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL.SysManage
{
    public class WebSiteTemplateNode
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.SysManage.IWebSiteTemplateNode dal = (IDAL.SysManage.IWebSiteTemplateNode)Assembly.Load(path).CreateInstance(path + ".SysManage.WebSiteTemplateNode");

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

        #region 增、改K_TemplateNode表
        /// <summary>
        /// 增、改K_TemplateNode表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="TemModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.SysManage.WebSiteTemplateNode temModel)
        {
            string re= dal.Save(trantype, temModel);

            return re;
        }
        #endregion

        #region 设置或者删除K_TemplateNode记录
        /// <summary>
        /// 设置或者删除K_TemplateNode记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string TemplateNodeSet(string tranType, string setValue, string IDList)
        {
            string re= dal.TemplateNodeSet(tranType, setValue, IDList);

            return re;
        }
        #endregion
    }
}
