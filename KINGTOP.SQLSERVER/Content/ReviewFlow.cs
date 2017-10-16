
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.Content;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武
// 创建日期：2010-03-26
// 功能描述：对K_ReviewFlow表的的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.SQLServer.Content
{
    public class ReviewFlow : IReviewFlow
    {
        #region 根据传入的参数查询K_ReviewFlow,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ReviewFlow,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_ReviewFlowSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_ReviewFlow记录
        /// <summary>
        /// 设置或者删除K_ReviewFlow记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ReviewFlowSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_ReviewFlowSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion


        #region 增、改K_ReviewFlow表
        /// <summary>
        /// 增、改K_ReviewFlow表
        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.Content.ReviewFlow paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_ReviewFlowSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("ID",paramsModel.ID),
                    new SqlParameter("Orders",paramsModel.Orders),                 
                    new SqlParameter("AddMan",paramsModel.AddMan),
                    new SqlParameter("IsEnable",paramsModel.IsEnable),
                    new SqlParameter("Name",paramsModel.Name),
                    new SqlParameter("Desc",paramsModel.Desc),                  
                    new SqlParameter("SiteId",paramsModel.SiteId),
                    new SqlParameter("NodeId",paramsModel.NodeId),
                      new SqlParameter("NodeCode",paramsModel.NodeCode),
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
                new SqlParameter("@Table","K_ReviewFlow"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","ID DESC"),                
                 new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        string GetWhere(Dictionary<string, string> DicWhere)
        {
            StringBuilder sbSql = new StringBuilder(40);
            sbSql.Append(" 1=1 ");
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    Utils.GetWhereAppend(kvp.Key, kvp.Value, "=", KingTop.Model.SqlParamType.Int, ref sbSql);
                }
            }

            return sbSql.ToString();
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager,string strModelId,string strFlowId,string strStateValue)
        {
            ModelManage sqlModelManage = new ModelManage();
            DataTable dtModelManage = sqlModelManage.GetList("ONE",Utils.getOneParams(strModelId));
            string strTableName = dtModelManage.Rows[0]["tableName"].ToString();
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table",strTableName),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere,strFlowId,strStateValue)),
                new SqlParameter("@Cou","*,(SELECT Name FROM K_ReviewFlowState WHERE K_ReviewFlowState.StateValue=FlowState) AS StateName"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","ID DESC"),                
                 new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        string GetWhere(Dictionary<string, string> DicWhere, string strFlowStateValue,string strStateValue)
        {
            StringBuilder sbSql = new StringBuilder(40);
            sbSql.Append(" 1=1 ");
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    Utils.GetWhereAppend(kvp.Key, kvp.Value, "=", KingTop.Model.SqlParamType.Int, ref sbSql);
                }
            }
            if (strFlowStateValue != "")
            {
                sbSql.Append(" AND FlowState = " + strFlowStateValue);
            }
            else
            {
                strStateValue += ",";
                sbSql.Append(" AND CHARINDEX(CAST(FlowState AS VARCHAR(10)),'" + strStateValue + "')>0");
            }
            return sbSql.ToString();
        }
        #endregion

        /// <summary>
        /// 获取状态ID
        /// </summary>
        /// <param Name="strTableName"></param>
        /// <param Name="strNewsId"></param>
        /// <returns></returns>
        public string GetFlowStateId(string strTableName, string strNewsId)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@TableName",strTableName),
                new SqlParameter("@NewsId",strNewsId)      
            };
            return SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "PROC_FlowStateUpdate", prams).ToString();
        }

        public DataTable GetdtFlowState(string strFlowStateId)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@FlowStateId",strFlowStateId)
            };
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "PROC_FlowStateUpdateNext1", prams).Tables[0];
        }

        /// <summary>
        /// 状态更新
        /// </summary>
        /// <param Name="strTable"></param>
        /// <param Name="strNewsId"></param>
        /// <param Name="strReviewState"></param>
        /// <returns></returns>
        public string FlowStateUpdate(string strTable, string strNewsId, string strReviewState)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@tableName",strTable),
                new SqlParameter("@newsId",strNewsId),
                new SqlParameter("@ReviewState",strReviewState)
            };
            return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "PROC_FlowStateUpdateNext2", prams).ToString();
        }
    }
}
