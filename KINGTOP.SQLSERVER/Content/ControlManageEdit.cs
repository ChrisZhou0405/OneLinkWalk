
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
// 功能描述：模型编辑页操作

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion


namespace KingTop.SQLServer.Content
{
    public partial class ControlManageEdit : IControlManageEdit
    {
        #region 字段数据来源于数据库时初始化  重载+1
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
            sql = "select [" + txtColumn + "],[" + valueColumn + "] from " + tableName;

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

        #region 字段数据来源于数据库时初始化 重载+2
        /// <summary>
        /// 搜索字段数据初始化

        /// </summary>
        public DataTable GetDataTable(string tableName, string txtColumn, string valueColumn, string sqlWhere, string sqlOrder)
        {
            string sql;
            DataTable dt;
            SqlDataReader dataReader;

            dt = new DataTable();
            sql = "select [" + txtColumn + "],[" + valueColumn + "] from " + tableName;

            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                sql = sql + " where " + sqlWhere;
            }

            if (!string.IsNullOrEmpty(sqlOrder))
            {
                sql = sql + " order by " + sqlOrder;
            }
            dataReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            dt.Load(dataReader);


            return dt;
        }
        #endregion

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

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">主键值</param>
        /// <param name="orders">排序</param>
        /// <param name="hsEditField">字段名与值对</param>
        /// <returns></returns>
        public string Add(string tableName, string id, int orders, Hashtable hsEditField,Dictionary<string,string> dicSubModel)
        {
            StringBuilder insertSQL;                                    // insert语句
            StringBuilder insertFieldPart;                              // insert语句的字段列部分
            StringBuilder insertValuePart;                               // insert语句的值部分
            StringBuilder insertSubModelSQL;                             // insert语句
            StringBuilder insertSubModelFieldPart;                       // insert语句的字段列部分
            StringBuilder insertSubModelValuePart;                       // insert语句的值部分
            string msg;
            List<SqlParameter> lstInsertParam;                           // SQL参数
            string subTableName;

            insertSQL = new StringBuilder();
            insertFieldPart = new StringBuilder();
            insertValuePart = new StringBuilder();
            insertSubModelSQL = new StringBuilder();
            insertSubModelFieldPart = new StringBuilder();
            insertSubModelValuePart = new StringBuilder();
            lstInsertParam = new List<SqlParameter>();

           lstInsertParam.Add(new SqlParameter("@ID", id));
           lstInsertParam.Add(new SqlParameter("@Orders", orders));

            insertFieldPart.Append("ID,Orders");
            insertValuePart.Append("@ID,@Orders");

            foreach (object fieldName in hsEditField.Keys)       // 遍历所有字段获取添加字段与字段值
            {
                insertFieldPart.Append(",");
                insertFieldPart.Append("[" + fieldName + "]");
                insertValuePart.Append(",@");
                insertValuePart.Append(fieldName);               // 字段值，用变量表示
            }

            insertSQL.Append("insert into ");                     // 组装insert语句
            insertSQL.Append(tableName);
            insertSQL.Append("(");
            insertSQL.Append(insertFieldPart);
            insertSQL.Append(") values(");
            insertSQL.Append(insertValuePart);
            insertSQL.Append(");");

            foreach (object fieldName in hsEditField.Keys)        // 遍历所有字段值，添加至参数数组中
            {
                if (hsEditField[fieldName] == null)
                {
                   lstInsertParam.Add(new SqlParameter("@" + fieldName.ToString(), System.DBNull.Value));
                }
                else
                {
                    lstInsertParam.Add(new SqlParameter("@" + fieldName.ToString(), hsEditField[fieldName]));
                }
            }

            if (dicSubModel != null && dicSubModel.Count > 1)
            {
                dicSubModel.Add("ParentID", id);
                subTableName = dicSubModel["HQB_TableName"];
                dicSubModel.Remove("HQB_TableName");

                foreach (string fieldName in dicSubModel.Keys)       // 遍历所有子模型字段获取添加字段与字段值
                {
                    if (insertSubModelFieldPart.Length > 1)
                    {
                        insertSubModelFieldPart.Append(",");
                        insertSubModelFieldPart.Append("[" + fieldName + "]");
                        insertSubModelValuePart.Append(",@");
                        insertSubModelValuePart.Append(subTableName + fieldName); // 字段值，用变量表示
                    }
                    else
                    { 
                        insertSubModelFieldPart.Append("[" + fieldName + "]");
                        insertSubModelValuePart.Append("@");
                        insertSubModelValuePart.Append(subTableName + fieldName); // 字段值，用变量表示
                    }
                }

                insertSubModelSQL.Append("insert into ");                     // 组装insert语句
                insertSubModelSQL.Append(subTableName);
                insertSubModelSQL.Append("(");
                insertSubModelSQL.Append(insertSubModelFieldPart);
                insertSubModelSQL.Append(") values(");
                insertSubModelSQL.Append(insertSubModelValuePart);
                insertSubModelSQL.Append(");");

                insertSQL.Insert(0, "Begin tran Tran_Main_SubModel_Field       Begin TRY       ");
                insertSQL.Append(insertSubModelSQL);
                insertSQL.Append("	COMMIT TRAN Tran_Main_SubModel_Field;  END TRY     BEGIN CATCH  	ROLLBACK TRAN Tran_Main_SubModel_Field;    END CATCH");

                foreach (string fieldName in dicSubModel.Keys)        // 遍历所有字模型字段值，添加至参数数组中
                {
                    if (dicSubModel[fieldName] == null)
                    {
                        lstInsertParam.Add(new SqlParameter("@" + subTableName + fieldName.ToString(), System.DBNull.Value));
                    }
                    else
                    {
                        lstInsertParam.Add(new SqlParameter("@" + subTableName + fieldName.ToString(), dicSubModel[fieldName]));
                    }
                }
            }

            try
            {
                msg = "0";
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, insertSQL.ToString(), lstInsertParam.ToArray());
                msg = "1";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">主键值</param>
        /// <param name="hsEditField">字段名与值对</param>
        /// <returns></returns>
        public string Update(string tableName, string id, Hashtable hsEditField,Dictionary<string,string> dicSubModel)
        {
            StringBuilder updateSQL;                               // 更新SQL语句
            StringBuilder subModelUpdateSQL;                       // 更新子模型SQL语句
            List<SqlParameter> lstUpdateParam;                     // SQL参数
            StringBuilder updateSetPart;                           // update语句的字段赋值部分
           StringBuilder subModelUpdateSetPart;                    // 子模型update语句的字段赋值部分
           string msg;                                             // 处理结果
            string subTableName;

            updateSetPart = new StringBuilder();
            subModelUpdateSetPart = new StringBuilder();
            updateSQL = new StringBuilder();
            subModelUpdateSQL = new StringBuilder();
            lstUpdateParam = new List<SqlParameter>();

            lstUpdateParam.Add(new SqlParameter("@ID", id));

            foreach (object fieldName in hsEditField.Keys)                  // 遍历所有字段获取更新字段与字段值
            {
                updateSetPart.Append("[");
                updateSetPart.Append(fieldName);
                updateSetPart.Append("]=@");
                updateSetPart.Append(fieldName);
                updateSetPart.Append(",");
            }
            updateSetPart.Remove(updateSetPart.Length - 1, 1);     // 剪掉多余的逗号

            updateSQL.Append("update ");                           // 组装update语句
            updateSQL.Append(tableName);
            updateSQL.Append(" set ");
            updateSQL.Append(updateSetPart);
            updateSQL.Append(" where ID=@ID;");

            foreach (object fieldName in hsEditField.Keys)         // 遍历所有字段值，添加至参数数组中
            {
                if (hsEditField[fieldName] == null)
                {
                   lstUpdateParam.Add( new SqlParameter("@" + fieldName.ToString(), System.DBNull.Value));
                }
                else
                {
                    lstUpdateParam.Add(new SqlParameter("@" + fieldName.ToString(), hsEditField[fieldName]));
                }
            }

            if (dicSubModel != null && dicSubModel.Count > 1)
            {
                dicSubModel.Add("ParentID", id);
                subTableName = dicSubModel["HQB_TableName"];
                dicSubModel.Remove("HQB_TableName");

                foreach (string fieldName in dicSubModel.Keys)                  // 遍历所有字段获取更新字段与字段值
                {
                    subModelUpdateSetPart.Append("[");
                    subModelUpdateSetPart.Append(fieldName);
                    subModelUpdateSetPart.Append("]=@");
                    subModelUpdateSetPart.Append(subTableName + fieldName);
                    subModelUpdateSetPart.Append(",");
                }

                subModelUpdateSetPart.Remove(subModelUpdateSetPart.Length - 1, 1);     // 剪掉多余的逗号

                subModelUpdateSQL.Append("update ");                           // 组装update语句
                subModelUpdateSQL.Append(subTableName);
                subModelUpdateSQL.Append(" set ");
                subModelUpdateSQL.Append(subModelUpdateSetPart);
                subModelUpdateSQL.Append(" where ParentID=@ID;");

                foreach (string fieldName in dicSubModel.Keys)         // 遍历所有字段值，添加至参数数组中
                {
                    if (dicSubModel[fieldName] == null)
                    {
                        lstUpdateParam.Add(new SqlParameter("@" + subTableName + fieldName.ToString(), System.DBNull.Value));
                    }
                    else
                    {
                        lstUpdateParam.Add(new SqlParameter("@" + subTableName + fieldName.ToString(), dicSubModel[fieldName]));
                    }
                }

                updateSQL.Insert(0, "Begin tran Tran_SubModel_Field_Edit       Begin TRY       ");
                updateSQL.Append(subModelUpdateSQL);
                updateSQL.Append("	COMMIT TRAN Tran_SubModel_Field_Edit;  END TRY     BEGIN CATCH  	ROLLBACK TRAN Tran_SubModel_Field_Edit;    END CATCH");
            }

            try
            {
                msg = "0";
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, updateSQL.ToString(), lstUpdateParam.ToArray());
                msg = "1";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }
        #endregion

        #region 记录添加至专题栏目

        /// <summary>
        /// 记录添加至专题栏目

        /// </summary>
        /// <param name="specialID">专题ID</param>
        /// <param name="specialMenuID">专题栏目ID</param>
        /// <param name="specialInfoID"> 当前更新的记录ID</param>
        public void AppendToSpecial(string[] specialID, string[] specialMenuID, string specialInfoID)
        {
            StringBuilder insertSQL;        // 添加至专题信息表的SQL语句
            int i = 0;                        // 计数

            insertSQL = new StringBuilder();

            // 遍历要附加的专题栏目
            foreach (string special in specialID)
            {
                if (!string.IsNullOrEmpty(special))
                {
                    insertSQL.Append("insert into K_SpecialInfo(SpecialID,SpecialMenuID,InfoID) values('" + special + "','" + specialMenuID[i] + "','" + specialInfoID + "')");
                }

                i++;
            }

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, insertSQL.ToString(), null);
            }
            catch { }
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

        #region 编辑专题栏目信息
        public int EditSpecialInfo(string[] delSpecialID, string[] delSpecialMenuID, string[] addSpecialID, string[] addSpecialMenuID, string specialInfoID)
        {
            StringBuilder insertSQL;
            StringBuilder deleteSQL;
            int returnValue;

            insertSQL = new StringBuilder();
            deleteSQL = new StringBuilder();

            if (delSpecialID != null && delSpecialMenuID != null && delSpecialID.Length > 0 && delSpecialMenuID.Length > 0)
            {
                for (int i = 0; i < delSpecialID.Length; i++)
                {
                    deleteSQL.Append("delete from K_SpecialInfo where SpecialID='" + delSpecialID[i] + "' and SpecialMenuID='" + delSpecialMenuID[i] + "' and InfoID='" + specialInfoID + "';");
                }
            }
            if (addSpecialID != null && addSpecialMenuID != null && addSpecialID.Length > 0 && addSpecialMenuID.Length > 0)
            {
                for (int i = 0; i < addSpecialID.Length; i++)
                {
                    if (!string.IsNullOrEmpty(addSpecialID[i]))
                    {
                        insertSQL.Append("insert into K_SpecialInfo(SpecialID,SpecialMenuID,InfoID) values('" + addSpecialID[i] + "','" + addSpecialMenuID[i] + "','" + specialInfoID + "')");
                    }
                }
            }

            returnValue = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, deleteSQL.ToString() + insertSQL.ToString(), null);
            return returnValue;
        }
        #endregion

        #region 加载当前节点信息
        /// <summary>
        /// 加载当前节点信息
        /// </summary>
        /// <param name="nodeCode">当前节点代码</param>
        /// <returns></returns>
        public DataTable GetNodeInfoByNodeCode(string nodeCode)
        {
            SqlParameter[] selParam;
            SqlDataReader sqlReader;
            DataTable dt;

            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@NodeCode", nodeCode) };
            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_GetNodePath", selParam);
                dt.Load(sqlReader);
            }
            catch { dt = null; }

            return dt;
        }
        #endregion

        #region 添加信息至节点

        /// <summary>
        ///添加信息至节点

        /// </summary>
        /// <param name="tableName">要添加信息的表</param>
        /// <param name="siteID">要添加信息的站点ID,多个用逗号隔开</param>
        /// <param name="nodeID">站点对应的节点ID,多个用逗号隔开</param>
        /// <param name="id"> 记录ID,多个用逗号隔开</param>
        /// <param name="orders">记录排序，多个用逗号隔开</param>
        /// <param name="nodeCount">节点数即要添加的记录条数</param>
        /// <param name="infoID">要添加的信息ID</param>
        /// <returns></returns>
        public int AppendToNode(string tableName, string siteID, string nodeCode, string id, string orders, int nodeCount, string infoID)
        {
            SqlParameter[] insertParam;
            int result;

            insertParam = new SqlParameter[]{
                new SqlParameter("@TableName",tableName),
                new SqlParameter("@SiteID",siteID),
                new SqlParameter("@NodeCode",nodeCode),
                new SqlParameter("@ID",id),
                new SqlParameter("@Orders",orders),
                new SqlParameter("@NodeCount",nodeCount),
                new SqlParameter("@InfoID",infoID)
            };

            result = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_AppendToNode", insertParam);
            return result;
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

    }
}

