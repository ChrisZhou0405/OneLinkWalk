#region 引用程序集
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using KingTop.Common;
using System.Web;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2011 图派科技 
// 作者：杨迪
// 创建日期：2011-12-05
// 功能描述：
===========================================================================*/
#endregion

namespace KingTop.Common
{
    /// <summary>
    /// 数据库操作通用类
    /// </summary>
    public static class InfoHelper
    {
        #region 添加
        /// <summary>
        /// 添加 返回影响行数 为-1时执行SQL异常 -100 没有获取要添加的字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="lstKeys"></param>
        /// <param name="isInclude">true lstKeys内容为要添加的字段名 false 为排除的</param>
        /// <returns></returns>
        public static int Add(string tableName, List<string> lstKeys, bool isInclude)
        {
            int effectRow;
            List<SqlParameter> lstParam;
            string requestValue;
            StringBuilder sbInsert;
            StringBuilder sbInsertValue;

            sbInsert = new StringBuilder();
            sbInsertValue = new StringBuilder();
            lstParam = new List<SqlParameter>();
            effectRow = 0;

            if (!lstKeys.Contains("__VIEWSTATE"))
            {
                lstKeys.Add("__VIEWSTATE");
            }

            sbInsert.Append("INSERT INTO " + tableName + "(");

            if (isInclude)
            {
                foreach (string key in lstKeys)
                {
                    if (sbInsertValue.Length > 1)
                    {
                        sbInsertValue.Append(",");
                    }

                    sbInsert.Append(key + ",");
                    sbInsertValue.Append("@" + key);
                    requestValue = HttpContext.Current.Request.Form[key];

                    if (string.IsNullOrEmpty(requestValue) || requestValue.Trim() == "")
                    {
                        lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                    }
                    else
                    {
                        lstParam.Add(new SqlParameter("@" + key, requestValue));
                    }
                }
            }
            else
            {
                foreach (string key in HttpContext.Current.Request.Form.Keys)
                {
                    if (!lstKeys.Contains(key))
                    {

                        if (sbInsertValue.Length > 1)
                        {
                            sbInsertValue.Append(",");
                        }

                        sbInsert.Append(key + ",");
                        sbInsertValue.Append("@" + key);
                        requestValue = HttpContext.Current.Request.Form[key];

                        if (string.IsNullOrEmpty(requestValue) || requestValue.Trim() == "")
                        {
                            lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                        }
                        else
                        {
                            lstParam.Add(new SqlParameter("@" + key, requestValue));
                        }
                    }
                }
            }

            if (lstParam.Count > 0)
            {
                sbInsert.Remove(sbInsert.Length - 1, 1);
                sbInsert.Append(") VALUES(");
                sbInsert.Append(sbInsertValue);
                sbInsert.Append(");");

                try
                {
                    effectRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbInsert.ToString(), lstParam.ToArray());
                }
                catch { effectRow = -1; }
            }
            else
            {
                effectRow = -100;
            }

            return effectRow;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改 返回影响行数 为-1时执行SQL异常 -100 没有获取要修改的字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="lstKeys"></param>
        /// <param name="sqlWhere">要修改的记录条件 为空时全部更新</param>
        /// <param name="isInclude">true lstKeys内容为要添加的字段名 false 为排除的</param>
        /// <returns></returns>
        public static int Edit(string tableName, List<string> lstKeys, string sqlWhere, bool isInclude)
        {
            int effectRow;
            List<SqlParameter> lstParam;
            string requestValue;
            StringBuilder sbEdit;

            sbEdit = new StringBuilder();
            lstParam = new List<SqlParameter>();
            effectRow = 0;

            if (!lstKeys.Contains("__VIEWSTATE"))
            {
                lstKeys.Add("__VIEWSTATE");
            }

            sbEdit.Append("UPDATE " + tableName + "  SET ");

            if (isInclude)
            {
                foreach (string key in lstKeys)
                {
                    sbEdit.Append(key).Append("=@").Append(key).Append(",");
                    requestValue = HttpContext.Current.Request.Form[key];

                    if (string.IsNullOrEmpty(requestValue) || requestValue.Trim() == "")
                    {
                        lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                    }
                    else
                    {
                        lstParam.Add(new SqlParameter("@" + key, requestValue));
                    }
                }
            }
            else
            {
                foreach (string key in HttpContext.Current.Request.Form.Keys)
                {
                    if (!lstKeys.Contains(key))
                    {
                        sbEdit.Append(key).Append("=@").Append(key).Append(",");
                        requestValue = HttpContext.Current.Request.Form[key];

                        if (string.IsNullOrEmpty(requestValue) || requestValue.Trim() == "")
                        {
                            lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                        }
                        else
                        {
                            lstParam.Add(new SqlParameter("@" + key, requestValue));
                        }
                    }
                }
            }

            if (lstParam.Count > 0)
            {
                sbEdit.Remove(sbEdit.Length - 1, 1);

                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sbEdit.Append(" where ");
                    sbEdit.Append(sqlWhere);
                }

                try
                {
                    effectRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbEdit.ToString(), lstParam.ToArray());
                }
                catch { effectRow = -1; }
            }
            else
            {
                effectRow = -100;
            }

            return effectRow;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="sqlWhere">删除条件</param>
        /// <returns>删除的记录数</returns>
        public static int Delete(string tableName, string sqlWhere)
        {
            StringBuilder sbDelete;
            int effectRow;

            sbDelete = new StringBuilder();

            if (string.IsNullOrEmpty(sqlWhere))
            {
                sbDelete.Append("DELETE FROM ");
                sbDelete.Append(tableName);
            }
            else
            {
                sbDelete.Append("DELETE FROM ");
                sbDelete.Append(tableName);
                sbDelete.Append(" WHERE ");
                sbDelete.Append(sqlWhere);
            }

            try
            {
                effectRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbDelete.ToString(), null);
            }
            catch { effectRow = 0; }

            return effectRow;
        }
        #endregion

        #region 添加 重载+1
        /// <summary>
        /// 添加 返回影响行数 为-1时执行SQL异常 -100 没有获取要添加的字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dicParam">参数列</param>
        /// <returns></returns>
        public static int Add(string tableName, Dictionary<string, object> dicParam)
        {
            int effectRow;
            List<SqlParameter> lstParam;
            StringBuilder sbInsert;
            StringBuilder sbInsertValue;

            sbInsert = new StringBuilder();
            sbInsertValue = new StringBuilder();
            lstParam = new List<SqlParameter>();
            effectRow = 0;

            sbInsert.Append("INSERT INTO " + tableName + "(");

            foreach (string key in dicParam.Keys)
            {
                if (sbInsertValue.Length > 1)
                {
                    sbInsertValue.Append(",");
                }

                sbInsert.Append(key + ",");
                sbInsertValue.Append("@" + key);

                if (dicParam[key] == null)
                {
                    lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                }
                else
                {
                    lstParam.Add(new SqlParameter("@" + key, dicParam[key]));
                }
            }

            if (lstParam.Count > 0)
            {
                sbInsert.Remove(sbInsert.Length - 1, 1);
                sbInsert.Append(") VALUES(");
                sbInsert.Append(sbInsertValue);
                sbInsert.Append(");");

                try
                {
                    effectRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbInsert.ToString(), lstParam.ToArray());
                }
                catch { effectRow = -1; }
            }
            else
            {
                effectRow = -100;
            }

            return effectRow;
        }
        #endregion

        #region 添加 重载+2
        /// <summary>
        /// 添加 添加成功返回影响行数和ID号码，失败返回失败错误描述
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dicParam">参数列</param>
        /// <returns></returns>
        public static string[] Add(string tableName, Dictionary<string, string> dicParam)
        {
            string effectRow;
            List<SqlParameter> lstParam;
            StringBuilder sbInsert;
            StringBuilder sbInsertValue;

            sbInsert = new StringBuilder();
            sbInsertValue = new StringBuilder();
            lstParam = new List<SqlParameter>();
            string[] reMsg = new string[2]{"",""};
            effectRow = "0";

            sbInsert.Append("INSERT INTO " + tableName + "(");

            foreach (string key in dicParam.Keys)
            {
                if (sbInsertValue.Length > 1)
                {
                    sbInsertValue.Append(",");
                }

                sbInsert.Append(key + ",");
                sbInsertValue.Append("@" + key);

                if (dicParam[key] == null)
                {
                    lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                }
                else
                {
                    lstParam.Add(new SqlParameter("@" + key, dicParam[key]));
                }
            }

            if (lstParam.Count > 0)
            {
                sbInsert.Remove(sbInsert.Length - 1, 1);
                sbInsert.Append(") VALUES(");
                sbInsert.Append(sbInsertValue);
                sbInsert.Append(");select scope_identity() returnid");

                try
                {
                    effectRow = "1";
                    SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbInsert.ToString(), lstParam.ToArray());
                    if (dr.Read())
                    {
                        reMsg[1] = dr[0].ToString();
                    }
                    dr.Close();
                }
                catch (Exception e) { effectRow=e.Message; }
            }
            else
            {
                effectRow = "-100";
            }
            reMsg[0] = effectRow;

            return reMsg;
        }
        #endregion

        #region 添加 Add1
        /// <summary>
        /// 添加 返回刚添加数据的scope_identity()
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dicParam">参数列</param>
        /// <returns></returns>
        public static int Add1(string tableName, Dictionary<string, string> dicParam)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            List<SqlParameter> lstParam;
            StringBuilder sbInsert;
            StringBuilder sbInsertValue;

            sbInsert = new StringBuilder();
            sbInsertValue = new StringBuilder();
            lstParam = new List<SqlParameter>();
            dt = new DataTable();

            sbInsert.Append("INSERT INTO " + tableName + "(");

            foreach (string key in dicParam.Keys)
            {
                if (sbInsertValue.Length > 1)
                {
                    sbInsertValue.Append(",");
                }

                sbInsert.Append(key + ",");
                sbInsertValue.Append("@" + key);
                if (dicParam[key] == null)
                {
                    lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                }
                else
                {
                    lstParam.Add(new SqlParameter("@" + key, dicParam[key]));
                }
            }

            if (lstParam.Count > 0)
            {
                sbInsert.Remove(sbInsert.Length - 1, 1);
                sbInsert.Append(") VALUES (");
                sbInsert.Append(sbInsertValue);
                sbInsert.Append(");select scope_identity() returnid;");

                try
                {
                    sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbInsert.ToString(), lstParam.ToArray());
                    dt.Load(sqlReader);
                }
                catch
                {
                    sqlReader = null;
                }
            }
            else
            {
                dt = null;
            }
            int result = 0;
            if (dt != null)
            {
                try
                {
                    result = Convert.ToInt32(dt.Rows[0]["returnid"]);
                }
                catch
                {
                    result = 0;
                }
            }

            return result;
        }
        public static string Add1(string tableName, Dictionary<string, string> dicParam,bool IsString)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            List<SqlParameter> lstParam;
            StringBuilder sbInsert;
            StringBuilder sbInsertValue;

            sbInsert = new StringBuilder();
            sbInsertValue = new StringBuilder();
            lstParam = new List<SqlParameter>();
            dt = new DataTable();

            sbInsert.Append("INSERT INTO " + tableName + "(");

            foreach (string key in dicParam.Keys)
            {
                if (sbInsertValue.Length > 1)
                {
                    sbInsertValue.Append(",");
                }

                sbInsert.Append(key + ",");
                sbInsertValue.Append("@" + key);
                if (dicParam[key] == null)
                {
                    lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                }
                else
                {
                    lstParam.Add(new SqlParameter("@" + key, dicParam[key]));
                }
            }

            if (lstParam.Count > 0)
            {
                sbInsert.Remove(sbInsert.Length - 1, 1);
                sbInsert.Append(") VALUES (");
                sbInsert.Append(sbInsertValue);
                sbInsert.Append(");select scope_identity() returnid;");

                try
                {
                    sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbInsert.ToString(), lstParam.ToArray());
                    dt.Load(sqlReader);
                }
                catch
                {
                    sqlReader = null;
                }
            }
            else
            {
                dt = null;
            }
            string result = "0";
            if (dt != null)
            {
                try
                {
                    result =dt.Rows[0]["returnid"].ToString();
                }
                catch
                {
                    result = "0";
                }
            }

            return result;
        }
        #endregion

        #region 修改 重载+1
        /// <summary>
        /// 修改 返回影响行数 为-1时执行SQL异常 -100 没有获取要修改的字段
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dicParam">参数列</param>
        /// <param name="sqlWhere">修改条件</param>
        /// <returns></returns>
        public static int Edit(string tableName, Dictionary<string, object> dicParam, string sqlWhere)
        {
            int effectRow;
            List<SqlParameter> lstParam;
            StringBuilder sbEdit;

            sbEdit = new StringBuilder();
            lstParam = new List<SqlParameter>();
            effectRow = 0;

            sbEdit.Append("UPDATE " + tableName + "  SET ");


            foreach (string key in dicParam.Keys)
            {
                sbEdit.Append(key).Append("=@").Append(key).Append(",");

                if (dicParam[key] == null)
                {
                    lstParam.Add(new SqlParameter("@" + key, DBNull.Value));
                }
                else
                {
                    lstParam.Add(new SqlParameter("@" + key, dicParam[key]));
                }
            }

            if (lstParam.Count > 0)
            {
                sbEdit.Remove(sbEdit.Length - 1, 1);

                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sbEdit.Append(" where ");
                    sbEdit.Append(sqlWhere);
                }

                try
                {
                    effectRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbEdit.ToString(), lstParam.ToArray());
                }
                catch { effectRow = -1; }
            }
            else
            {
                effectRow = -100;
            }

            return effectRow;
        }
        #endregion

        #region 查询分页列表
        /// <summary>
        /// 查询单表分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每天条数</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <param name="sqlOrder">排序条件</param>
        /// <param name="field">返回的查询字段</param>
        /// <param name="rsCount">返回 总行数</param>
        /// <returns></returns>
        public static DataTable List(string tableName, int pageIndex, int pageSize, string sqlWhere, string sqlOrder, string field, out int rsCount)
        {
            DataSet ds;
            DataTable dt;
            string selSQL;
            int startIndex;
            int endIndex;



            startIndex = (pageIndex - 1) * pageSize + 1;
            endIndex = pageIndex * pageSize;

            if (string.IsNullOrEmpty(field))
            {
                field = "*";
            }

            if (string.IsNullOrEmpty(sqlOrder))
            {
                sqlOrder = " adddate desc ";
            }

            if (string.IsNullOrEmpty(sqlWhere))
            {
                selSQL = "SELECT COUNT(*) FROM " + tableName + ";SELECT " + field + " FROM (SELECT " + field + ",row_number() OVER(ORDER BY " + sqlOrder + ") AS SYS_RowNum FROM (select " + field + " from  " + tableName + " ) Inner_A)SYS_A WHERE SYS_A.SYS_RowNum BETWEEN " + startIndex + "  AND " + endIndex;
            }
            else
            {
                selSQL = "SELECT COUNT(*) FROM " + tableName + " WHERE " + sqlWhere + " ;SELECT " + field + " FROM (SELECT " + field + ",row_number() OVER(ORDER BY " + sqlOrder + ") AS SYS_RowNum FROM (select " + field + " from  " + tableName + " where " + sqlWhere + " ) Inner_A)SYS_A WHERE SYS_A.SYS_RowNum BETWEEN " + startIndex + "  AND " + endIndex;
            }

            try
            {
                
                ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt = ds.Tables[1];
                rsCount = Common.Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
            }
            catch
            {
                dt = null;
                rsCount = 0;
            }

            return dt;
        }
        /// <summary>
        /// 查询多表分页数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每天条数</param>
        /// <param name="sqlWhere">查询条件(t.id)</param>
        /// <param name="sqlOrder">排序条件(别名)</param>
        /// <param name="field">返回的查询字段(别名)</param>
        /// <param name="field1">筛选时的查询字段(t.id=1)</param>
        /// <param name="rsCount">返回 总行数</param>
        /// <returns></returns>
        public static DataTable List(string tableName, int pageIndex, int pageSize, string sqlWhere, string sqlOrder, string field, string field1, out int rsCount)
        {
            DataSet ds;
            DataTable dt;
            string selSQL;
            int startIndex;
            int endIndex;



            startIndex = (pageIndex - 1) * pageSize + 1;
            endIndex = pageIndex * pageSize;

            if (string.IsNullOrEmpty(field))
            {
                field = "*";
            }

            if (string.IsNullOrEmpty(sqlOrder))
            {
                sqlOrder = " adddate desc ";
            }

            if (string.IsNullOrEmpty(sqlWhere))
            {
                selSQL = "SELECT COUNT(*) FROM " + tableName + ";SELECT " + field + " FROM (SELECT " + field + ",row_number() OVER(ORDER BY " + sqlOrder + ") AS SYS_RowNum FROM (select " + field1 + " from  " + tableName + " ) Inner_A)SYS_A WHERE SYS_A.SYS_RowNum BETWEEN " + startIndex + "  AND " + endIndex;
            }
            else
            {
                selSQL = "SELECT COUNT(*) FROM " + tableName + " WHERE " + sqlWhere + " ;SELECT " + field + " FROM (SELECT " + field + ",row_number() OVER(ORDER BY " + sqlOrder + ") AS SYS_RowNum FROM (select " + field1 + " from  " + tableName + " where " + sqlWhere + " ) Inner_A)SYS_A WHERE SYS_A.SYS_RowNum BETWEEN " + startIndex + "  AND " + endIndex;
            }

            try
            {
                ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt = ds.Tables[1];
                rsCount = Common.Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);
            }
            catch
            {
                dt = null;
                rsCount = 0;
            }

            return dt;
        }
        #endregion

        #region 执行SQL
        public static DataTable ExecuteSQL(string selSQL)
        {
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt.Load(sqlReader);
            }
            catch { dt = null; }

            if (dt != null)
            {
                if (dt.Rows.Count == 0) dt = null;
            }

            return dt;
        }
        #endregion
        #region 执行SQL
        public static bool ExecuteSQL(string selSQL,bool result)
        {
            bool bl = result;
            try
            {
                bl = SQLHelper.ExcuteCommand(selSQL);
            }
            catch { bl = false; }
            return bl;
        }
        #endregion

        #region 获取字段值
        public static object GetFieldValue(string tableName, string fieldName, string sqlWhere)
        {
            object fieldValue;
            StringBuilder sbSelSQL;

            sbSelSQL = new StringBuilder();

            sbSelSQL.Append("select ");
            sbSelSQL.Append(fieldName);
            sbSelSQL.Append(" from ");
            sbSelSQL.Append(tableName);
            sbSelSQL.Append(" where ");
            sbSelSQL.Append(sqlWhere);

            try
            {
                fieldValue = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbSelSQL.ToString(), null);
            }
            catch { fieldValue = null; }
            if (fieldValue == null)
                fieldValue = "";
            return fieldValue;
        }
        #endregion
    }
}
