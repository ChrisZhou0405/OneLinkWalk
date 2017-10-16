
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
// 创建日期：2010-04-27
// 功能描述：模型生成浏览操作

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion

namespace KingTop.SQLServer.Content
{
    public  class ControlManageView :IControlManageView
    {
        #region 加载记录
        /// <summary>
        /// 通过记录ID加载记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">记录ID</param>
        /// <returns></returns>
        public Hashtable GetHashTableByID(string tableName, string id)
        {
            string selSQL;                  // 查询语句：通过ID查询记录
            SqlParameter[] selParam;        // 记录ID参数
            SqlDataReader sqlDataReader;
            DataTable dt;                   // 加载记录的数据表
            Hashtable hsFieldValue;         // 

            hsFieldValue = new Hashtable();
            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@ID", id) };

            selSQL = "select * from " + tableName + "  where ID=@ID";
            sqlDataReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
            dt.Load(sqlDataReader);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    hsFieldValue.Add(col.ColumnName, dt.Rows[0][col.ColumnName]);
                }
            }
            else
            {
                hsFieldValue = null;
            }

            return hsFieldValue;
        }
        #endregion

        #region 加载所属专题栏目
        /// <summary>
        /// 加载所属专题栏目
        /// </summary>
        /// <param name="specialInfoID"> 当前更新的记录ID</param>
        /// <returns></returns>
        public DataTable LoadSpecial(string specialInfoID)
        {
            string selSQL;
            SqlParameter[] selParam;
            SqlDataReader sqlReader;
            DataTable dt;

            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@specialInfoID", specialInfoID) };
            selSQL = "select  A.SpecialID,A.SpecialMenuID,B.Name AS SpecialName,C.Name as SpecialMenuName from K_SpecialInfo A inner join K_Special B on A.SpecialID=B.ID inner join K_SpecialMenu C on A.SpecialMenuID=C.ID where A.InfoID=@specialInfoID";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
            dt.Load(sqlReader);

            return dt;
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
    }
}

