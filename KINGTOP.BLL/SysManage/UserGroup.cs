using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using KingTop.IDAL;
using System.Data;
using System.Configuration;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：肖丹
// 创建日期：2010-03-22
// 功能描述：对K_SysUserGroup表的的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
namespace KingTop.BLL.SysManage
{
    public class UserGroup
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.SysManage.IUserGroup dal = (IDAL.SysManage.IUserGroup)Assembly.Load(path).CreateInstance(path + ".SysManage.UserGroup");

        #region 根据传入的参数查询K_SysUserGroup,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysUserGroup,返回查询结果   
        /// 操作：GROUPANDROLENAME=所有用户组和角色名，ALL=得到K_SysUserGroup表的所有记录，ONE=得到K_SysUserGroup的一条记录
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataSet（因GROUPANDROLENAME条件有两个查询，所有用DataSet返回）</returns>
        public DataSet GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 增、改K_SysUserGroup表
        /// <summary>
        /// 增、改K_SysUserGroup表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="AutModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.SysManage.UserGroup UserGroupModel)
        {
            return dal.Save(trantype, UserGroupModel);

            //更新缓存 无法访问
            KingTop.Common.AppCache.Remove("UserGroupRoleCache");
            KingTop.BLL.SysManage.RightTool bllRight = new RightTool();
            bllRight.GetUserGroupRoleFromCache();
        }
        #endregion

        #region 设置或者删除K_SysUserGroup记录
        /// <summary>
        /// 设置或者删除K_SysUserGroup记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，DELONE=删除一条</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string UserGroupSet(string tranType, string setValue, string IDList)
        {
            return dal.UserGroupSet(tranType, setValue, IDList);
        }
        #endregion
    }
}
