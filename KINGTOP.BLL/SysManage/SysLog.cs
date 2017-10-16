
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL.SysManage;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：袁纯林 ycl@KingTop.com
// 创建日期：2010-03-17
// 功能描述：对K_ManageLog表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL.SysManage
{
    public class SysLog
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.SysManage.ISysLog dal = (IDAL.SysManage.ISysLog)Assembly.Load(path).CreateInstance(path + ".SysManage.SysLog");

        #region 增、改K_ManageLog表
        /// <summary>
        /// 增、改K_ManageLog表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="ManModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.SysManage.SysLog manModel)
        {
            return dal.Save(trantype, manModel);
        }
        #endregion

        public DataTable GetList(string trantype, KingTop.Model.SelectParams manModel, Dictionary<string, string> dictWhere)
        {
            return dal.GetList(trantype, manModel,dictWhere);
        }

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            dal.PageData(pager);
        }
        #endregion

    }
}
