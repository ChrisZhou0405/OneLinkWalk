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
    public class Account : KingTop.IDAL.SysManage.IAccount
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
                      CommandType.StoredProcedure, "proc_K_SysAccountSel", prams).Tables[0];
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
        public string AccountSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_SysAccountSet", prams);
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
        public string Save(string tranType, KingTop.Model.SysManage.Account paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_SysAccountSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("UserId",paramsModel.UserID),
                    new SqlParameter("UserName",paramsModel.UserName),                    
                    new SqlParameter("Password",paramsModel.PassWord),
                    new SqlParameter("Orders",paramsModel.Orders),
                    new SqlParameter("IsValid",paramsModel.IsValid),
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
                new SqlParameter("@Table","K_SysAccount"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere,1)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","Orders DESC"),                
                new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
            ds.Dispose();           
        }

        /// <summary>
        /// 使用SQL语句获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager, int StrNumber)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id",""),
                new SqlParameter("@Table",""),             
                new SqlParameter("@Where",""),
                new SqlParameter("@Cou",""),
                //new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@NewPageIndex",Utils.ParseInt(System.Web.HttpContext.Current.Request.QueryString["page"],1)),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","Orders DESC,UserID DESC"),                
                new  SqlParameter("@isSql",1),
                new  SqlParameter("@strSql",GetStrSqlByNumber(StrNumber,pager.DicWhere))
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        //选择调用哪条SQL语句
        public string GetStrSqlByNumber(int StrNumber, Dictionary<string, string> DicWhere)
        {
            string strResult = string.Empty;
            string Condition = GetWhere(DicWhere, StrNumber);
            switch (StrNumber)
            {
                case 1:
                    if (Condition != "")
                    {
                        Condition = " and " + Condition;
                    }
                    strResult = GetStrSql1() + Condition;
                    break;
                case 2:
                    if (Condition != "")
                    {
                        Condition = " and " + Condition;
                    }
                    strResult= GetStrSql2() + Condition;
                    break;
                default:
                    strResult = "";
                    break;
            }

            return strResult;
        }

        public string GetStrSql1()
        {
            string strsql = "select distinct A.USERid,UserName,A.IsValid,case A.IsValid when '1' then '活动' when '0' then '禁用' end as StrIsValid,AddDate from K_SysAccount A,K_SysUserRole c where a.userid=c.UserID and A.IsDel=0";
            return strsql;
        }
        public string GetStrSql2()
        {
            string strsql = " select A.UserID,A.UserName,A.Orders,A.AddDate,B.isValid,B.ID,D.UserGroupName,D.NumCode from K_SysAccount A ,K_SysAccountSite B,K_SysUserRole C,k_sysUserGroup D where A.UserID=B.UserID and B.UserID=c.UserID and c.usergroupcode=d.usergroupcode and A.IsDel=0 and B.IsDel=0";
            return strsql;
        }
       
        //设置条件
        string GetWhere(Dictionary<string, string> DicWhere, int StrNumber)
        {
            string sqlWhere = " 1=1 ";
            string sqlWhere1 = "";
            string sqlWhere2 = "";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "SiteID")
                    {
                        if (StrNumber == 1)
                        {
                            sqlWhere += " AND " + kvp.Key + " = " + kvp.Value;
                        }
                        else
                        {
                            sqlWhere += " AND B." + kvp.Key + " = " + kvp.Value;
                            sqlWhere += " AND D." + kvp.Key + " = " + kvp.Value;
                        }
                    }
                    else if (kvp.Key == "NumCode")
                    {
                        sqlWhere2 += " (left(D.NumCode," + kvp.Value.Length + ")='" + kvp.Value + "' AND len(D.NumCode)>" + kvp.Value.Length+")";
                    }
                    else if (kvp.Key == "UserID")
                    {
                        sqlWhere1 = " OR A.UserID=" + kvp.Value;
                    }
                    else
                    {
                        sqlWhere += " AND " + kvp.Key + " like '%" + kvp.Value + "%'";
                    }
                }
            }
            if (!string.IsNullOrEmpty(sqlWhere2) && !string.IsNullOrEmpty(sqlWhere1))
            {
                sqlWhere += " AND (" + sqlWhere2 + sqlWhere1 + ")";
            }

            return sqlWhere;
        }
        #endregion
    }
}
