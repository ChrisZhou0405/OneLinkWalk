using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using KingTop.IDAL;
using System.Data;
using System.Configuration;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：肖丹
// 创建日期：2010-03-22
// 功能描述：对K_SysUserRole表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL.SysManage
{
    public class UserGroupRole
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.SysManage.IUserGroupRole dal = (IDAL.SysManage.IUserGroupRole)Assembly.Load(path).CreateInstance(path + ".SysManage.UserGroupRole");

        #region 根据传入的参数查询K_SysUserRole,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysUserRole,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        //从缓存中获取K_SysUserGroupRole表
        public DataTable GetUserGroupRoleFromCache()
        {
            if (AppCache.IsExist("UserGroupRoleCache"))
            {
                return (DataTable)AppCache.Get("UserGroupRoleCache");
            }
            else
            {
                DataTable dt = GetList("Cache", Utils.getOneParams(""));
                AppCache.Add("UserGroupRoleCache", dt, "K_SysUserGroupRole");
                return dt;
            }
        }

        #region 增、改K_SysUserRole表
        /// <summary>
        /// 增、改K_SysUserRole表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="AutModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.SysManage.UserGroupRole userGroupRoleModel)
        {
            return dal.Save(trantype, userGroupRoleModel);
        }
        #endregion

        #region 设置或者删除K_SysUserRole记录
        /// <summary>
        /// 设置或者删除K_SysUserRole记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string UserGroupRoleSet(string tranType, string setValue, string IDList)
        {
            return dal.UserGroupRoleSet(tranType, setValue, IDList);
        }
        #endregion

        public DataSet GetUserGroupRole(KingTop.Model.Pager pager,int SiteID)
        {
            return dal.GetUserGroupRole(pager,SiteID);
        }
    }
}
