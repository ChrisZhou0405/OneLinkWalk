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
// 功能描述：对K_SysUser表的的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.SQLServer.SysManage
{
    public class User : KingTop.IDAL.SysManage.IUser
    {
        #region 根据传入的参数查询K_SysUser,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysUser,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_SysUserSel", prams).Tables[0];
        }
        #endregion


        #region 设置或者删除K_SysUser记录
        /// <summary>
        /// 设置或者删除K_SysUser记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string UserSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_SysUserSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion

        #region 增、改K_SysUser表
        /// <summary>
        /// 增、改K_SysUser表
        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.SysManage.User paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_SysUserSave";
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("UserID",paramsModel.UserID),
                    new SqlParameter("TrueName",paramsModel.TrueName),
                    new SqlParameter("Sex",paramsModel.Sex),
                    new SqlParameter("Birthday",paramsModel.Birthday),
                    new SqlParameter("Nation",paramsModel.Nation),
                    new SqlParameter("NativePlace",paramsModel.NativePlace),
                    new SqlParameter("MarriageState",paramsModel.MarriageState),
                    new SqlParameter("HomeAddress",paramsModel.HomeAddress),
                    new SqlParameter("CurrentAddress",paramsModel.CurrentAddress),
                    new SqlParameter("OfficePhone",paramsModel.OfficePhone),
                    new SqlParameter("Extension",paramsModel.Extension),
                    new SqlParameter("Mobile",paramsModel.Mobile),
                    new SqlParameter("HomePhone",paramsModel.HomePhone),
                    new SqlParameter("Fax",paramsModel.Fax),
                    new SqlParameter("Email",paramsModel.Email),
                    new SqlParameter("InputPerson",paramsModel.InputPerson),
                    new SqlParameter("InputID",paramsModel.InputID),
                    new SqlParameter("InputDate",paramsModel.InputDate),
                    new SqlParameter("Remark",paramsModel.Remark),
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

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","UserID"),
                new SqlParameter("@Table","K_SysUser"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","UserID DESC"),                
                new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        /// <summary>
        /// 使用SQL语句获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager,int StrNumber)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id",""),
                new SqlParameter("@Table",""),             
                new SqlParameter("@Where",""),
                new SqlParameter("@Cou",""),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","UserID asc"),                
                new  SqlParameter("@isSql",1),
                new  SqlParameter("@strSql",GetStrSqlByNumber(StrNumber))
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        //选择调用哪条SQL语句
        public string GetStrSqlByNumber(int StrNumber)
        {
            string strResult = string.Empty;
            switch (StrNumber)
            { 
                case 1:
                    strResult = GetStrSql1();
                    break;
                case 2:
                    strResult = GetStrSql2();
                    break;
                default:
                    strResult = "";
                    break;
            }
            return strResult;
        }

        public string GetStrSql1()
        {
            string strsql = "select distinct A.USERid,UserName,truename,A.IsValid,case A.IsValid when '1' then '活动' when '0' then '禁用' end as StrIsValid,InputDate from K_SysAccount A,K_SysUser b,K_SysUserRole c where A.UserID =b.UserID and a.userid=c.UserID";
            return strsql;
        }

        public string GetStrSql2()
        {
            string strsql = "select distinct A.USERid,UserName,truename,A.IsValid,case A.IsValid when '1' then '活动' when '0' then '禁用' end as StrIsValid,UserGroupName from K_SysAccount A,K_SysUser b,K_SysUserRole c,k_sysUserGroup d where A.UserID =b.UserID and a.userid=c.UserID and d.UserGroupCode=c.UserGroupCode";
            return strsql;
        }

        //设置条件
        string GetWhere(Dictionary<string, string> DicWhere)
        {
            string sqlWhere = "";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "SiteID")
                    {
                        sqlWhere = kvp.Key + " = " + kvp.Value;
                    }
                    else
                    {
                        sqlWhere = kvp.Key + " like '%" + kvp.Value + "%'";
                    }
                }
            }

            return sqlWhere;
        }
        #endregion
    }
}
