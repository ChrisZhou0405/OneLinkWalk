
﻿#region 程序集引用
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

using KingTop.IDAL.Content;
using KingTop.Common;
#endregion

#region 版权注释
/*----------------------------------------------------------------------------------------
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-03-23
// 功能描述：模型列表页操作

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion


namespace KingTop.SQLServer.Content
{
    public partial class ControlManageList : IControlManageList
    {
        #region 搜索字段数据对于数据来源于数据库时初始化
        /// <summary>
        /// 搜索字段数据初始化
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="txtColumn"></param>
        /// <param name="valueColumn"></param>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public Hashtable GetHashTable(string tableName, string txtColumn, string valueColumn, string sqlWhere)
        {
            string sql;
            DataTable dt;
            SqlDataReader dataReader;
            Hashtable hsReturnValue;

            dt = new DataTable();
            hsReturnValue = new Hashtable();
            sql = "select " + txtColumn + "," + valueColumn + " from " + tableName;

            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                sql = sql + " where " + sqlWhere;
            }

            dataReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            dt.Load(dataReader);

            foreach (DataRow dr in dt.Rows)
            {
                hsReturnValue.Add(dr[valueColumn], dr[txtColumn]);
            }

            return hsReturnValue;
        }
        #endregion

        #region 通过ID返回表的特定字段值
        public string GetFieldValue(string tableName, string fieldName, string id)
        {
            StringBuilder selSQL;
            string returnValue;

            selSQL = new StringBuilder();
            selSQL.Append("select ");
            selSQL.Append(fieldName);
            selSQL.Append(" from ");
            selSQL.Append(tableName);
            selSQL.Append(" where ID='");
            selSQL.Append(id);
            selSQL.Append("'");

            returnValue = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL.ToString()).ToString();
            return returnValue;
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public void PageData(KingTop.Model.Pager pager, string tableName, string sqlField, string sqlWhere, string sqlOrder, string sqlFieldForignData)
        {
            string sql;

            sql = SplitSQLPacking(pager, tableName, sqlField, sqlWhere, sqlOrder, sqlFieldForignData);
            sql = Regex.Replace(sql, @"(?<3>(?<1>[0-9a-zA-Z_]+)\.(?<2>[0-9a-zA-Z_]+))", "${1}.[${2}]");                // 格式化SQL中的字段名成[字段名]格式
            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.Text, sql);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }
        #endregion

        #region 组装SQL分页
        public string SplitSQLPacking(KingTop.Model.Pager pager, string tableName, string sqlField, string sqlWhere, string sqlOrder, string sqlFieldForignData)
        {
            StringBuilder splitSQL; // 分页SQL语句
            string outOrder;
            Regex reg;
            MatchCollection matchCollection;

            // 匹配表名
            reg = new Regex(@"\s+(?<1>[0-9a-zA-Z_]+)\.[0-9a-zA-Z_]+\s+");
            splitSQL = new StringBuilder();

            matchCollection = reg.Matches(sqlOrder);
            outOrder = sqlOrder;
            // 格式化外表引用字段
            sqlField = FormatField(sqlField, sqlFieldForignData);

            // 由排序创建表联合后的外排序
            foreach (Match match in matchCollection)
            {
                outOrder = sqlOrder.Replace(match.Groups[1].Value, "A");
            }

            // 记录总数
            splitSQL.Append("select count(1) from ");
            splitSQL.Append(tableName);
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                splitSQL.Append(" where ");
                splitSQL.Append(sqlWhere);
            }
            splitSQL.Append(";");

            // 分页SQL
            splitSQL.Append("select * from (");
            splitSQL.Append("select row_number() over(order by ");
            splitSQL.Append(sqlOrder);
            splitSQL.Append(") as RowNum,");
            splitSQL.Append(sqlField);
            splitSQL.Append(" from ");
            splitSQL.Append(tableName);
            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                splitSQL.Append(" where ");
                splitSQL.Append(sqlWhere);
            }
            splitSQL.Append(")A where ");
            splitSQL.Append(" A.RowNum between ");
            splitSQL.Append(pager.PageSize * (pager.PageIndex - 1) + 1);
            splitSQL.Append(" and ");
            splitSQL.Append(pager.PageSize * pager.PageIndex);
            splitSQL.Append(" order by ");
            splitSQL.Append(outOrder);

            return splitSQL.ToString();

        }
        #endregion

        #region  格式化基本字段中引用外表数据的字段参数
        private string FormatField(string strField, string sqlFieldForignData)
        {
            string[] arrField;          // 需要格式化的字段
            string[] fieldParam;        // 字段参数  [0] 引用表名 [1] 字段名 [2] 字段显示引用列 [3] 字段值引用列
            string sqlField;            // 格式化后的值
            StringBuilder tempSql;      // 临时变量
            string asFieldName;         // 格式化后的字段名

            sqlField = strField;
            arrField = sqlFieldForignData.Split(new char[] { ',' });
            tempSql = new StringBuilder();

            if (arrField.Length > 0)
            {
                foreach (string field in arrField)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        continue;
                    }

                    fieldParam = field.Split(new char[] { '|' });

                    // 检测参数是否有四个
                    if (fieldParam.Length > 3)
                    {
                        asFieldName = Regex.Replace(fieldParam[1], @"[0-9a-zA-Z_]+\.", "");
                        tempSql.Remove(0, tempSql.Length);
                        tempSql.Append("(select " + fieldParam[0] + "." + fieldParam[2]);       // 外表选择列
                        tempSql.Append(" from " + fieldParam[0]);                               // 获取数据的外表
                        tempSql.Append("  where " + fieldParam[0] + "." + fieldParam[3] + " in(" + fieldParam[1] + ")"); // 选取条件
                        tempSql.Append(") as " + asFieldName);
                        sqlField = sqlField.Replace(fieldParam[1], tempSql.ToString());
                    }
                    else // 不符合则忽略
                    {
                        continue;
                    }
                }
            }

            return sqlField;
        }
        #endregion

        #region 显示当前用户可操作流程步骤及状态        /// <summary>
        /// 显示当前用户可操作流程步骤及状态        /// </summary>
        /// <param name="accountID">账户ID</param>
        /// <param name="nodeCode">节点代码</param>
        /// <param name="stepID">流程步骤</param>
        /// <returns>Tables[0] 流程步骤 Tables[1]流程状态 </returns>
        public DataSet GetEnabledFlowStep(int accountID, int siteID, string nodeCode, string stepID)
        {
            SqlParameter[] selParam;
            DataSet ds;

            if (string.IsNullOrEmpty(stepID))
            {
                selParam = new SqlParameter[] { new SqlParameter("@AccountID", accountID), new SqlParameter("@SiteID", siteID), new SqlParameter("@NodeCode", nodeCode) };
            }
            else
            {
                selParam = new SqlParameter[] { new SqlParameter("@AccountID", accountID), new SqlParameter("@SiteID", siteID), new SqlParameter("@NodeCode", nodeCode), new SqlParameter("@StepID", stepID) };
            }

            try
            {
                ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_GetEnabledFlowStep", selParam);
            }
            catch
            {
                ds = null;
            }

            return ds;
        }
        #endregion

        #region 获取所有可能的流程状态        public DataTable GetFlowState()
        {
            DataTable dtFlowState;
            string selSQL;
            SqlDataReader sqlReader;

            dtFlowState = new DataTable();
            selSQL = "select  StateValue,[Desc] from K_ReviewFlowState";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
            dtFlowState.Load(sqlReader);

            return dtFlowState;
        }
        #endregion

        #region 获取节点审核流程
        public string GetNodeReviewFlow(int siteID, string nodeCode)
        {
            object reviewFlowID;
            string selSQL;
            SqlParameter[] selSQLParam;

            selSQL = "select top 1 ReviewFlowID from K_SysModuleNode where WebSiteID=@SiteID and NodeCode=@NodeCode";
            selSQLParam = new SqlParameter[] { new SqlParameter("@SiteID", siteID), new SqlParameter("@NodeCode", nodeCode) };

            reviewFlowID = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selSQLParam);
            if (reviewFlowID != null)
            {
                return reviewFlowID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region 获取需要传递的URL
        public void GetKeepUrlParam(ref string sqlWhere, string tableName, string keepParamList, string filterUrlParam, System.Collections.Specialized.NameValueCollection queryStringParam)
        {
            string[] arrUrlParam;

            arrUrlParam = keepParamList.Split(new char[] { ',' });
            foreach (string key in queryStringParam.Keys)
            {
                if (key == null)
                    continue;

                if (!filterUrlParam.ToLower().Contains("," + key.ToLower() + ",") && !string.IsNullOrEmpty(queryStringParam[key]))      // 不在过滤参之内
                {
                    foreach (string param in arrUrlParam)
                    {
                        if (string.Equals(key.ToLower(), param.ToLower()) && !sqlWhere.Contains(tableName + "." + param))
                        {
                            if (string.IsNullOrEmpty(sqlWhere))
                            {
                                if (IsNumber(queryStringParam[param].ToString()))   // 数字类型
                                {
                                    sqlWhere = " " + tableName + "." + key + "=" + queryStringParam[param].ToString() + " ";
                                }
                                else  // 字符串类
                                {
                                    sqlWhere = " " + tableName + "." + key + "='" + queryStringParam[param].ToString() + "' ";
                                }
                            }
                            else
                            {
                                if (IsNumber(queryStringParam[param].ToString()))
                                {
                                    sqlWhere += " and " + tableName + "." + key + "=" + queryStringParam[param].ToString() + " ";
                                }
                                else
                                {
                                    sqlWhere += " and " + tableName + "." + key + "='" + queryStringParam[param].ToString() + "' ";
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 通过/取消审核
        public void AlterFlowState(string tableName, string infoID, int infoCount, bool state,string addMan,string ip,string nodeCode,int siteID,int accountID)
        {
            SqlParameter[] editParam;

            editParam = new SqlParameter[]{
                new SqlParameter("@TableName",tableName),
                new SqlParameter("@InfoID",infoID),
                new SqlParameter("@InfoCount",infoCount),
                new SqlParameter("@State",state),
                new SqlParameter("@AddMan",addMan),
                new SqlParameter("@IP",ip),
                new SqlParameter("@NodeCode",nodeCode),
                new SqlParameter("@SiteID",siteID),
                new SqlParameter("@AccountID",accountID)
            };

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_AlterFlowState", editParam);
        }
        #endregion
    }
}

