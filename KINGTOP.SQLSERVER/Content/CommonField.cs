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
// 创建日期：2010-06-22
// 功能描述：模型字段操作
----------------------------------------------------------------------------------------*/
#endregion

namespace KingTop.SQLServer.Content
{
    public class CommonField : KingTop.IDAL.Content.ICommonField
    {
        #region  获取数据库中数据表与表字段        /// <summary>
        /// 获取数据库中数据表与表字段        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetTableAndField(string tableName)
        {
            string selSQL;
            SqlParameter[] selParam;
            DataSet ds;

            selParam = new SqlParameter[]{new SqlParameter("@TableName", tableName)};
            selSQL = "SELECT [Name],[object_id] as ID FROM sys.tables order by [name] asc;SELECT [name] FROM sys.columns WHERE [object_id]=(select [object_id] from sys.tables where [name]=@TableName) order by [Name] asc";
            try
            {
                ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
            }
            catch
            {
                ds = null;
            }

            return ds;
        }
        #endregion

        #region 添加/更新
        /// <summary>
        ///  添加/更新
        /// </summary>
        /// <param name="hsFieldParam">字段值对</param>
        /// <param name="modelFieldID">字段ID</param>
        /// <returns></returns>
        public string Save(Hashtable hsFieldParam,string modelFieldID)
        {
            string editFieldSQL;                // 更新字段记录
            SqlParameter[] editParam;           // 字段记录值            string effectRow;                      // 影响行数

            editFieldSQL = string.Empty;

            editParam = GetEditSQL(hsFieldParam,modelFieldID,ref editFieldSQL);

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, editFieldSQL, editParam);
                effectRow = "1";
            }
            catch(Exception ex)
            {
                effectRow = ex.Message;
            }

            return effectRow;
        }
        #endregion

        #region 添加/更新SQL
        private SqlParameter[] GetEditSQL(Hashtable hsFieldParam, string modelFieldID,ref string editCmdSQL)
        {
            StringBuilder editSQL;          // 更新SQL语句
            List<SqlParameter> editParam;   // SQL参数
            string tempFieldName;           // 临时变量,要添加的字段
            string tempFieldValue;          // 临时变量，对应的字段值      

            editSQL = new StringBuilder();
            editParam = new List<SqlParameter>();
            tempFieldName = string.Empty;
            tempFieldValue = string.Empty;

            if (!string.IsNullOrEmpty(modelFieldID))        // 编辑，更新记录
            {
                editParam.Add(new SqlParameter("@ID", modelFieldID));
                editSQL.Append("update K_ModelCommonField set ");

                foreach (string key in hsFieldParam.Keys)   // 遍历添加所有需更新的字段
                {
                    if (string.IsNullOrEmpty(tempFieldName))
                    {
                        tempFieldName = "[" + key + "]=@" + key;
                    }
                    else
                    {
                        tempFieldName += "," + key + "=@" + key;
                    }

                    if (hsFieldParam[key] == null)
                    {
                        editParam.Add(new SqlParameter("@" + key, DBNull.Value));
                    }
                    else
                    {
                        editParam.Add(new SqlParameter("@" + key, hsFieldParam[key]));
                    }
                }

                editSQL.Append(tempFieldName);
                editSQL.Append(" where ID=@ID;");
            }
            else  // 添加，插入新记录
            {
                editSQL.Append("insert into  K_ModelCommonField(");

                foreach (string key in hsFieldParam.Keys)   // 遍历添加所有需更新的字段                {
                    if (string.IsNullOrEmpty(tempFieldName))
                    {
                        tempFieldName = "[" + key + "]";
                        tempFieldValue = "@" + key;
                    }
                    else
                    {
                        tempFieldName += ",[" + key + "]";
                        tempFieldValue += ",@" + key;
                    }

                    if (hsFieldParam[key] == null)
                    {
                        editParam.Add(new SqlParameter("@" + key, DBNull.Value));
                    }
                    else
                    {
                        editParam.Add(new SqlParameter("@" + key, hsFieldParam[key]));
                    }
                }

                editSQL.Append(tempFieldName);
                editSQL.Append(")values(");
                editSQL.Append(tempFieldValue);
                editSQL.Append(");");
            }

            editCmdSQL = editSQL.ToString();
            return editParam.ToArray();
        }
        #endregion

        #region 根据传入的参数查询,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ModelCommonField,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_ModelCommonFieldSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除记录
        /// <summary>
        /// 设置或者删除K_ModelCommonField记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModelFieldSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_ModelCommonFieldSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion

        #region 得到分页数据
        public void PageData(KingTop.Model.Pager pager,Hashtable hsWhereEqual,Hashtable hsWhereLike,string sort)
        {
            StringBuilder selSQL;               // 查询SQL
            string selTotalSQL;                 // 查询所有记录条数SQL
            List<SqlParameter> selParam;        // 查询参数
            DataSet ds;

            selSQL = new StringBuilder();
            selParam = new List<SqlParameter>();

            selSQL.Append("ModelFieldType=@ModelFieldType");            // 一般模型字段
            selParam.Add(new SqlParameter("@ModelFieldType",1));

            foreach (string key in hsWhereEqual.Keys)   // 添加查询条件
            {
                if (hsWhereEqual[key] != null && !string.IsNullOrEmpty(hsWhereEqual[key].ToString().Trim()))
                {
                    selSQL.Append(" and " + key + "=@" + key);
                }
                selParam.Add(new SqlParameter("@" + key, hsWhereEqual[key]));
            }

            foreach (string key in hsWhereLike.Keys)    // 添加搜索条件
            {
                selSQL.Append(" and " + key + "  like '%" + hsWhereLike[key] + "%' ");
            }

            selTotalSQL = "select count(*) from  K_ModelCommonField where " + selSQL.ToString() + ";";

            if (!string.IsNullOrEmpty(sort))    // 加入自定义排序
            {
                selSQL.Insert(0, "select T.* from (select ROW_NUMBER() over(order by " + sort + ") as IndexNum,ID,Name,IsRequired,IsDefault,FieldAlias,BasicField,IsDel,IsEnable,Orders,IsListEnable,IsSearch,IsInputValue from K_ModelCommonField where ");
            }
            else // 默认排序
            {
                selSQL.Insert(0, "select T.* from (select ROW_NUMBER() over(order by orders asc) as IndexNum,ID,Name,IsRequired,FieldAlias,IsDefault,IsDel,BasicField,IsEnable,Orders,IsListEnable,IsSearch,IsInputValue from K_ModelCommonField where ");
            }

            if (!string.IsNullOrEmpty(sort))    // 加入自定义排序
            {
                selSQL.Append(")T where T.IndexNum Between " + (pager.PageIndex - 1) * pager.PageSize + " and " + pager.PageSize * pager.PageIndex + " order by T."+ sort.Trim() +";");
            }
            else // 默认排序
            { 
                selSQL.Append(")T where T.IndexNum Between " + (pager.PageIndex - 1) * pager.PageSize + " and " + pager.PageSize * pager.PageIndex + " order by T.orders asc;");
            }

            selSQL.Insert(0, selTotalSQL);

            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,CommandType.Text, selSQL.ToString(), selParam.ToArray());
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        #endregion

    }
}
