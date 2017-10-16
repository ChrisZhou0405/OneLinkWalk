using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL;
using KingTop.Common;
using System.Text.RegularExpressions;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：袁纯林 ycl@360hqb.com
// 创建日期：2010-03-10
// 功能描述：通用数据库操作（增、删、改、查）类

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.SQLServer
{
    public class Public : IPublic
    {
        #region 得到表排序号
        /// <summary>
        /// 根据传入的参数查询K_Source,返回查询结果
        /// </summary>
        /// <param Name="tblName">表名称</param>
        /// <returns>返回排序序号</returns>
        public int Orders(string tblName)
        {
            int orders = 1;
            string strSql = "SELECT MAX(Orders) FROM " + tblName;
            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            if (dr.Read())            
            {
                if (dr[0].ToString() == "")
                {
                    orders = 1;
                }
                else
                {
                    orders = int.Parse(dr[0].ToString()) + 7;
                }
            }
            dr.Close();
            dr.Dispose();
            return orders;
        }
        #endregion

        #region 判断是否为数字,以决定是否要加 ''
        // 判断SQL参数类型,以决定是否要加 ''
        public static bool IsNumber(string validateContent)
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
