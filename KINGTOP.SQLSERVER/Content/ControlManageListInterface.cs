#region 程序集引用
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
// 功能描述：模型生成列表操作  -- 外部调用

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion


namespace KingTop.SQLServer.Content
{
    public partial class ControlManageList : IControlManageList
    {
        #region 组装SQL分页
        public string SplitSQLPacking(int pageSize,int pageIndex, string tableName, string sqlField, string sqlWhere, string sqlOrder, string sqlFieldForignData)
        {
            StringBuilder splitSQL; // 分页SQL语句
            string outOrder;
            Regex reg;
            MatchCollection matchCollection;

            // 匹配表名
            reg = new Regex(@"\s+(?<1>[0-9 a-z A-Z _]+)\.[0-9 a-z A-Z _]+\s+");
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
            splitSQL.Append(pageSize * (pageIndex - 1) + 1);
            splitSQL.Append(" and ");
            splitSQL.Append(pageSize * pageIndex);
            splitSQL.Append(" order by ");
            splitSQL.Append(outOrder);

            return splitSQL.ToString();

        }
        #endregion
    }
}
