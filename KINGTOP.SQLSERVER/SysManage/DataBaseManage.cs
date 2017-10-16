using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.Common;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年7月14日
    功能描述： 数据库管理操作类
    ===============================================================*/
#endregion

namespace KingTop.SQLServer.SysManage
{
    class DataBaseManage : KingTop.IDAL.SysManage.IDataBaseManage
    {
        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize)
            };
            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_GetDataTableInfo", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }
        #endregion        

        #region 得到用户表数据
        /// <summary>
        /// 得到用户表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetUserTableInfo()
        {
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                       CommandType.StoredProcedure, "proc_GetDataTableInfo2").Tables[0];
        }
        #endregion        

        #region 直接执行sql语句
        /// <summary>
        /// 直接执行sql语句
        /// </summary>
        /// <returns>是否成功</returns>
        public bool ExecSql(string sql)
        {
            int affRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            if (affRow > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 执行多条select语句返回DataSet的第一个Table
        /// <summary>
        /// 执行多条select语句
        /// </summary>
        /// <returns>返回DataSet的第一个Table</returns>
        public DataTable GetTableExecSql(string sql)
        {
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).Tables[0];
        }
        #endregion
    }
}
