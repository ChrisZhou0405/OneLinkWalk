using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.SysManage;
using KingTop.Common;
//using SQLDMO;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-06-18
// 功能描述：top.aspx中显示公告

// 更新日期        更新人      更新原因/内容
//20100709         gavin       1.加上超级用户查询，2.查询加上top3和排序
//201008027         胡志瑶       1.GetMessage方法加上一个长度参数 2.加GetDataBaseInfo方法  3.加GetPendingAudi
===========================================================================*/
namespace KingTop.SQLServer.SysManage
{
    public class Top : ITop
    {
        public DataTable GetMessage(int count, string siteID, string userName, string userid)
        {
            string selSQL;
            DataTable dtMessage;
            SqlDataReader sqlReader;

            dtMessage = new DataTable();
            //selSQL = "select * from K_U_Message where IsDel=0 and IsEnable=1";
            if (userid == "0")  //超级用户
            {
                selSQL = "select top " + count + " ID,Title,PublishDate from K_U_Message where ','+PublishRange like '," + siteID + "$$%' and IsDel=0 and IsEnable=1 Order By ID Desc";
            }
            else
            {
                selSQL = "select top " + count + " ID,Title,PublishDate from K_U_Message where charindex('," + siteID + "$$" + userName + ",',',' +PublishRange + ',') > 0 and IsDel=0 and IsEnable=1 Order By ID Desc";
            }
            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dtMessage.Load(sqlReader);
            }
            catch
            {
                dtMessage = null;
            }

            return dtMessage;
        }
        /// <summary>
        /// 获得数据库信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetDataBaseInfo()
        {
            DataTable dt = new DataTable();
            List<string> dbInfo = new List<string>();
            SqlDataReader sqlReader;
            //数据库版本
            string strSql = "SELECT @@VERSION";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, null);
            dt.Load(sqlReader);
            dbInfo.Add(dt.Rows[0][0].ToString().Substring(0, dt.Rows[0][0].ToString().IndexOf('-')));


            //获得数据库的大小
            dt = new DataTable();
            strSql = "exec sp_spaceused";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, null);
            dt.Load(sqlReader);
            dbInfo.Add(dt.Rows[0][1].ToString());

            //获得当前数据库的日志大小

            SqlDmoHelper dmo = new SqlDmoHelper("", "", "", "");


            dbInfo.Add(dmo.GetDatabaseInfo(dmo.CmsDbName).LogSize.ToString());

            //string tests = dmo.CreateTableSql("HQBCMSDB","K_SinglePage");
            //tests = dmo.CreateTableSql("HQBCMSDB", "K_RecyclingManage");

            return dbInfo;
        }

        /// <summary>
        /// 获得待审核的信息
        /// </summary>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public DataTable GetPendingAudi(KingTop.Model.SelectParams modelPrams)
        {
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();

            SqlParameter[] prams = new SqlParameter[] {               
                new SqlParameter("SiteID", modelPrams.I1),
                new SqlParameter("AccountID ", modelPrams.I2)
            };
            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_WaitCheckRSCounter", prams);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
    }
}
