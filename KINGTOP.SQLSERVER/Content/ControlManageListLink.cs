
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
// 功能描述：模型生成列表操作 -- 链接操作

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion


namespace KingTop.SQLServer.Content
{
    public partial class ControlManageList : IControlManageList
    {
        #region 链接按钮的删除操作        /// <summary>
        /// 链接按钮的删除操作        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">要删除的记录主键</param>
        /// <returns></returns>
        public string LinkDelete(string tableName, string id)
        {
            StringBuilder delSQL;
            StringBuilder delSpecialInfoSQL;
            string returnValue;

            delSQL = new StringBuilder();
            delSpecialInfoSQL = new StringBuilder();
            delSQL.Append("delete from ");
            delSQL.Append(tableName);
            delSQL.Append("  where ");
            delSQL.Append("ID in('");
            delSQL.Append(id.Replace(",", "','"));
            delSQL.Append("');");

            // 从专题表中删除相关记录            delSpecialInfoSQL.Append("delete from K_SpecialInfo where ID in(");
            delSpecialInfoSQL.Append("select D.ID,D.InfoID,TableName from K_ModelManage E inner join(	select A.ID,A.InfoID, Coalesce(B.ModelID,C.ModelID) as ModelID from K_SpecialInfo A inner join K_SpecialMenu B on A.SpecialMenuID=B.ID Inner join K_Special C on A.SpecialID=C.ID) D on E.ID = D.ModelID");
            delSpecialInfoSQL.Append("where TableName='" + tableName + "' and InfoID in(" + id.Replace(",", "','") + "));");

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, delSQL.ToString());
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, delSpecialInfoSQL.ToString());
                returnValue = "1";
            }
            catch (Exception ex)
            {
                returnValue = ex.Message;
            }

            return returnValue;
        }

        /// <summary>
        /// 链接的删除操作        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="colName">条件中的列名</param>
        /// <param name="colValue">条件值</param>
        /// <param name="oper">条件中的比较操作符</param>
        /// <returns></returns>
        public string LinkDelete(string tableName, string colName, string colValue, string oper)
        {
            StringBuilder delSQL;
            StringBuilder delSpecialInfoSQL;
            string returnValue;

            delSQL = new StringBuilder();
            delSpecialInfoSQL = new StringBuilder();
            delSQL.Append("delete from ");
            delSQL.Append(tableName);
            delSQL.Append("  where ");
            delSQL.Append(colName);
            delSQL.Append(oper);
            delSQL.Append("'");
            delSQL.Append(colValue);
            delSQL.Append("'");

            // 从专题表中删除相关记录
            delSpecialInfoSQL.Append("delete from K_SpecialInfo where ID in(");
            delSpecialInfoSQL.Append("select D.ID,D.InfoID,TableName from K_ModelManage E inner join(	select A.ID,A.InfoID, Coalesce(B.ModelID,C.ModelID) as ModelID from K_SpecialInfo A inner join K_SpecialMenu B on A.SpecialMenuID=B.ID Inner join K_Special C on A.SpecialID=C.ID) D on E.ID = D.ModelID");
            delSpecialInfoSQL.Append(" where TableName='" + tableName + "' and InfoID in(select id from " + tableName + " where " + colName + oper + "'" + colValue + "'));");
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, delSQL.ToString());
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, delSpecialInfoSQL.ToString());
                returnValue = "1";
            }
            catch(Exception ex)
            {
                returnValue = ex.Message;
            }

            return returnValue;
        }
        #endregion

        #region 链接按钮的更新操作        /// <summary>
        /// 链接按钮的更新操作        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">记录主键</param>
        /// <param name="sqlField">更新字段名</param>
        /// <param name="sqlFieldValue">更新值</param>
        /// <param name="sqlWhereField">更新条件中字段名</param>
        /// <param name="sqlWhereFieldValue">更新条件中字段值</param>
        /// <param name="sqlWhereFieldOpera">更新条件中比较符</param>
        /// <param name="sqlWhereFieldJoin">更新条件中各条件的连接关键字 and or</param>
        /// <returns>操作影响行数</returns>
        public string LinkEdit(string tableName, string id, string[] sqlField, string[] sqlFieldValue, string[] sqlWhereField, string[] sqlWhereFieldValue, string[] sqlWhereFieldOpera, string[] sqlWhereFieldJoin)
        {
            StringBuilder editSQL;
            string msg;

            editSQL = new StringBuilder();

            editSQL.Append("update ");
            editSQL.Append(tableName);
            editSQL.Append("  set ");

            if (sqlField != null && sqlField.Length > 0)
            {
                for (int i = 0; i < sqlField.Length; i++)
                {
                    editSQL.Append(sqlField[i]);
                    editSQL.Append("=");

                    if (IsNumber(sqlFieldValue[i]))
                    {
                        editSQL.Append(sqlFieldValue[i]);
                    }
                    else
                    {
                        editSQL.Append("'");
                        editSQL.Append(sqlFieldValue[i]);
                        editSQL.Append("'");
                    }

                    editSQL.Append(",");
                }

                if (editSQL.ToString().Contains(","))
                {
                    editSQL.Remove(editSQL.Length - 1, 1);
                }
            }
            else
            {
                return "0";
            }

            editSQL.Append(" where ");

            if (string.IsNullOrEmpty(id))
            {
                if (sqlWhereField != null && sqlWhereField.Length > 0)
                {
                    for (int i = 0; i < sqlWhereField.Length; i++)
                    {
                        editSQL.Append(sqlWhereField[i]);
                        editSQL.Append(sqlWhereFieldOpera[i]);

                        if (IsNumber(sqlWhereFieldValue[i]))
                        {
                            editSQL.Append(sqlWhereFieldValue[i]);
                        }
                        else
                        {
                            editSQL.Append("'");
                            editSQL.Append(sqlWhereFieldValue[i]);
                            editSQL.Append("'");
                        }

                        editSQL.Append(sqlWhereFieldJoin[i].Replace("u", " or ").Replace("n", " and "));
                    }
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                editSQL.Append("ID in('");
                editSQL.Append(id.Replace(",", "','"));
                editSQL.Append("')");
            }

            try
            {
                msg = "1";
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, editSQL.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion

        #region 链接按钮 -- 搜索
        /// <summary>
        /// 链接按钮 -- 搜索
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlWhereField">搜索字段名</param>
        /// <param name="sqlWhereFieldValue">搜索字段值</param>
        /// <param name="sqlWhereFieldOpera">搜索比较符</param>
        /// <param name="sqlWhereFieldJoin">搜索各条件的连接关键字 and or</param>
        /// <returns>搜索条件</returns>
        public string LinkSearch(string tableName,string[] sqlWhereField, string[] sqlWhereFieldValue, string[] sqlWhereFieldOpera, string[] sqlWhereFieldJoin)
        {
            StringBuilder sqlWhere;

            sqlWhere = new StringBuilder();

            if (sqlWhereField != null && sqlWhereField.Length > 0)
            {
                for (int i=0;i<sqlWhereField.Length;i++)
                {
                    sqlWhere.Append(tableName.Trim() + "." + sqlWhereField[i]);  // 字段名,要转换成表名限定

                    if (IsNumber(sqlWhereFieldValue[i]))                         // 判断搜索条件值是否要加 ''
                    {
                        sqlWhere.Append(sqlWhereFieldOpera[i]);                  // 操作符 = < >
                        sqlWhere.Append(sqlWhereFieldValue[i]);                  // 搜索值 
                    }
                    else
                    {
                        sqlWhere.Append(sqlWhereFieldOpera[i]);
                        sqlWhere.Append("'");
                        sqlWhere.Append(sqlWhereFieldValue[i]);
                        sqlWhere.Append("'");
                    }

                    sqlWhere.Append(sqlWhereFieldJoin[i].Replace("u", " or ").Replace("n", " and ")); // 条件连接符
                }
            }

            return sqlWhere.ToString();
        }
        #endregion

        #region 链接按钮流程步骤
        /// <summary>
        /// 链接按钮流程步骤
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="flowState">流程状态值</param>
        /// <returns></returns>
        public string FlowStateSearch(string tableName, string flowState)
        {
            string sqlWhere;

            sqlWhere = "  " + tableName + "." + "FlowState in("+ flowState +") ";

            return sqlWhere;
        }
        #endregion

        #region 判断是否为数字,以决定是否要加 ''
        // 判断SQL参数类型,以决定是否要加 ''
        private bool IsNumber(string validateContent)
        {
            if (validateContent.Length > 10)
            {
                return false;
            }
            else
            {
                return Regex.IsMatch(validateContent, @"\d+");
            }
        }
        #endregion

    }
}

