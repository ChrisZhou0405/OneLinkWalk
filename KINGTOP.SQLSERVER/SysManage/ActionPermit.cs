using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL;
using KingTop.Common;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：肖丹
// 创建日期：2010-03-22
// 功能描述：对K_SysActionPermit表的的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
namespace KingTop.SQLServer.SysManage
{
    public class ActionPermit : KingTop.IDAL.SysManage.IActionPermit
    {

        #region 根据传入的参数查询K_SysActionPermit,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysActionPermit,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_SysActionPermitSel", prams).Tables[0];
        }
        #endregion

        #region 根据传入的参数查询K_SysActionPermit,权限分配页面生成横排权限树
        /// <summary>
        /// 根据传入的参数查询K_SysActionPermit,返回查询结果
        /// </summary>
        /// <param Name="tranType">操作类型</param>
        /// <param Name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetModuleRightList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("tranType",tranType),
                    new SqlParameter("I1",paramsModel.I1),
                    new SqlParameter("strUserGropCode",paramsModel.S1),
                    new SqlParameter("strRoleCode",paramsModel.S2),
                    new SqlParameter("strMdlCode",paramsModel.S3),
                    new SqlParameter("strAccountID",paramsModel.S4)
                };

            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "Proc_GetModuleRightList", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_SysActionPermit记录
        /// <summary>
        /// 设置或者删除K_SysActionPermit记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ActionPermitSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_SysActionPermitSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion
        
        #region 增、改K_SysActionPermit表
        /// <summary>
        /// 增、改K_SysActionPermit表
        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.SysManage.ActionPermit paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_SysActionPermitSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("ID",paramsModel.ID),
                   new SqlParameter("ModuleID",paramsModel.ModuleID),
                    new SqlParameter("operCode",paramsModel.operCode),
                    new SqlParameter("OperName",paramsModel.OperName),
                    new SqlParameter("OperOrder",paramsModel.OperOrder),
                    new SqlParameter("IsValid",paramsModel.IsValid),
                    new SqlParameter("OperDesc",paramsModel.OperDesc),
                    new SqlParameter("OperEngDesc",paramsModel.OperEngDesc),
                    new SqlParameter("IsDefaultOper",paramsModel.IsDefaultOper),
                    new SqlParameter("IsSystem",paramsModel.IsSystem),
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
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table","K_SysActionPermit"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","OperOrder DESC"),                
                 new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }
        //

        /// <summary>
        /// 使用SQL语句获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager, int StrNumber,int SiteID)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id",""),
                new SqlParameter("@Table",""),             
                new SqlParameter("@Where",""),
                new SqlParameter("@Cou",""),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","OperCode asc"),                
                new  SqlParameter("@isSql",1),
                new  SqlParameter("@strSql",GetStrSqlByNumber(StrNumber,GetWhere(pager.DicWhere),SiteID))
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        //选择调用哪条SQL语句
        public string GetStrSqlByNumber(int StrNumber,string Condition,int SiteID)
        {
            string strResult = string.Empty;
            switch (StrNumber)
            {
                case 1:
                    if (Condition != "")
                    {
                        Condition=" and "+Condition;
                    }
                    strResult = GetStrSql1(SiteID)+Condition;
                    break;                
                default:
                    strResult = "";
                    break;
            }
            return strResult;
        }

        public string GetStrSql1(int SiteID)
        {
            string strsql = "select K_SysActionPermit.*,K_SysModule.ModuleName from K_SysActionPermit INNER JOIN K_SysModule ON K_SysActionPermit.ModuleID=K_SysModule.ModuleID WHERE K_SysActionPermit.ModuleID IN (select DISTINCT ModuleID from K_SysModuleNode where WebSiteID=" + SiteID + " and NodeType<>'1')";
            return strsql;
        }        

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
