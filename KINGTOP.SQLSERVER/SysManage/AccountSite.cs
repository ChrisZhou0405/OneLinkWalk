using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL;
using KingTop.Common;
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：肖丹
// 创建日期：2010-03-22
// 功能描述：对K_SysAccount表的的操作


// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.SQLServer.SysManage
{
    public class AccountSite : KingTop.IDAL.SysManage.IAccountSite
    {
        #region 根据传入的参数查询K_SysAccount,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysAccount,返回查询结果
        /// </summary>
        /// <param Name="tranType">操作类型</param>
        /// <param Name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("tranType",tranType),
                    new SqlParameter("I1",paramsModel.I1),
                    new SqlParameter("I2",paramsModel.I2),
                    new SqlParameter("S1",paramsModel.S1),
                    new SqlParameter("S2",paramsModel.S2)
                };

            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_K_SysAccountSiteSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_SysAccount记录
        /// <summary>
        /// 设置或者删除K_SysAccount记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string AccountSiteSet(string tranType, string setValue, string IDList)
        {
            string sRe = "";
            try
            {
                SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("trantype",tranType),
                    new SqlParameter("SetValue",setValue),
                    new SqlParameter("IDList",IDList)
                };

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction,
                          CommandType.StoredProcedure, "proc_K_SysAccountSiteSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion

        #region 增、改K_SysAccount表

        /// <summary>
        /// 增、改K_SysAccount表

        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.SysManage.AccountSite paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_SysAccountSiteSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("ID",paramsModel.ID),
                    new SqlParameter("SiteID",paramsModel.SiteID),
                    new SqlParameter("UserId",paramsModel.UserID),
                    new SqlParameter("IsValid",paramsModel.IsValid),
                    new SqlParameter("LoginDate",paramsModel.LoginDate),
                    new SqlParameter("IP",paramsModel.IP),
                    new SqlParameter("LoginCount",paramsModel.LoginCount),
                    new SqlParameter("LastLoginDate",paramsModel.LastLoginDate),
                    new SqlParameter("LastLoginIP",paramsModel.LastLoginIP),
                    new SqlParameter("Position",paramsModel.PoSition),
                    returnValue
                 };

                

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                isOk = returnValue.Value.ToString();
            }
            catch (Exception ex)
            {
                isOk = ex.Message;

            }

            return isOk;
        }
        #endregion

    }
}
