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
// 功能描述：对K_SysAccount表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
namespace KingTop.BLL.SysManage
{
    public class Account
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.SysManage.IAccount dal = (IDAL.SysManage.IAccount)Assembly.Load(path).CreateInstance(path + ".SysManage.Account");

        #region 根据传入的参数查询K_SysAccount,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysAccount,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        public KingTop.Model.SysManage.Account GetAccountByName(string UserName, string SiteID)
        {
            DataTable DTAccount = dal.GetList("LOGININFO", Utils.getTwoParams(UserName, SiteID));
            KingTop.Model.SysManage.Account clsAcc = new KingTop.Model.SysManage.Account();
            if (DTAccount.Rows.Count >= 1)
            {
                clsAcc.UserID = Utils.ParseInt(DTAccount.Rows[0]["UserID"], 1);
                clsAcc.UserName = DTAccount.Rows[0]["UserName"].ToString();
                clsAcc.PassWord = DTAccount.Rows[0]["Password"].ToString();
                clsAcc.Orders = Utils.ParseInt(DTAccount.Rows[0]["Orders"], 0);
                clsAcc.IsValid = Utils.ParseBool(DTAccount.Rows[0]["IsValid"]);
                clsAcc.UserGroupCode = DTAccount.Rows[0]["UserGroupCode"].ToString();
            }
            return clsAcc;
        }

        #region 增、改K_SysAccount表
        /// <summary>
        /// 增、改K_SysAccount表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="AutModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.SysManage.Account AccountModel)
        {
            return dal.Save(trantype, AccountModel);
        }
        #endregion

        #region 设置或者删除K_SysAccount记录
        /// <summary>
        /// 设置或者删除K_SysAccount记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string AccountSet(string tranType, string setValue, string IDList)
        {
            return dal.AccountSet(tranType, setValue, IDList);
        }
        #endregion

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

        #region 按SQL语句得到分页数据
        /// <summary>
        /// 用SQL语句获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager, int StrNumber)
        {
            dal.PageData(pager, StrNumber);
        }
        #endregion
    }
}
