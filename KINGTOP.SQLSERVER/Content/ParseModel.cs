
﻿#region 程序集引用using System;
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
// 作者：吴岸标// 创建日期：2010-04-16
// 功能描述：模型解析// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion


namespace KingTop.SQLServer.Content
{
    public class ParseModel : IParseModel
    {
        #region 获取字段表及字段组名
        /// <summary>
        /// 获取字段表及字段组名
        /// </summary>
        /// <param name="modelID">模型ID</param>
        /// <returns></returns>
        public DataTable GetField(string modelID)
        {
            string selSQL;
            SqlParameter[] sqlParam;
            SqlDataReader sqlReader;
            DataTable dt;

            sqlParam = new SqlParameter[] { new SqlParameter("@ModelID", modelID) };
            dt = new DataTable();
            selSQL = "select A.*,B.Name AS ModelGroupName from  K_ModelField A left join K_ModelFieldGroup B on A.ModelFieldGroupId=B.ID where A.IsEnable=1 and A.IsDel=0 and A.ModelId=@ModelID";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, sqlParam);
            dt.Load(sqlReader);

            return dt;
        }
        #endregion

        #region 保存列表列设置        public string SaveConfig(Hashtable hsListWidth, Hashtable hsListOrder, string modelID, string operColumnWidth, Hashtable advancedConfig)
        {
            string effectRow;                       // 受影响的行数
            StringBuilder editSQL;               // 批量更新配置
            StringBuilder advancedConfigSQL;     // 高级配置更新
            List<SqlParameter> advancedConfigParam;  // 高级配置更新SQL参数
            int i;                               // 临时变量，计数器

            editSQL = new StringBuilder();
            advancedConfigSQL = new StringBuilder();
            advancedConfigParam = new List<SqlParameter>();
            i = 0;

            if (!string.IsNullOrEmpty(operColumnWidth))// 操作列宽
            {
                editSQL.Append("update K_ModelManage set OperationColumnWidth='"+ operColumnWidth +"' where ID='"+ modelID +"';");
            }

            foreach (string key in hsListWidth.Keys)  // 更新列表列宽
            {
                if (!string.IsNullOrEmpty(hsListWidth[key].ToString()))
                {
                    editSQL.Append("update K_ModelField set ListWidth='" + hsListWidth[key].ToString() + "' where Name='" + key + "' and ModelId='" + modelID + "';");
                }
            }

            foreach (string key in hsListOrder.Keys)  // 更新列表列顺序
            {
                if (!string.IsNullOrEmpty(hsListOrder[key].ToString()))
                {
                    editSQL.Append("UPDATE K_ModelField set ListOrders=" + hsListOrder[key].ToString() + " where Name='" + key + "' and ModelId='" + modelID + "';");
                }
            }

            if (advancedConfig.Keys.Count > 0)        // 存在更需更新的高级配置参数            {
                foreach (string key in advancedConfig.Keys)
                {
                    if (advancedConfig[key] != null)
                    {
                        advancedConfigSQL.Append("UPDATE K_ModelManage set " + key + "=@" + key + " where ID='" + modelID + "';");
                        advancedConfigParam.Add(new SqlParameter("@" + key, advancedConfig[key]));
                    }
                    i++;
                }
            }

            try
            {
                 SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, editSQL.ToString(), null);                                      // 基本参数更新
               
                if (advancedConfigSQL.Length > 0)
                {
                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, advancedConfigSQL.ToString(), advancedConfigParam.ToArray()); // 高级参数更新
                }

                effectRow = "1";
            }
            catch(Exception ex)
            {
                effectRow = ex.Message;
            }
            return effectRow;
        }

        #endregion

        #region 获取推荐区域位置
        public DataTable GetRecommendAreaPosition(string areaID)
        {
            DataTable dtAreaPosition;
            SqlDataReader sqlReader;
            string selSQL;
            SqlParameter[] selParam;

            selParam = new SqlParameter[] { new SqlParameter("@AreaID", areaID) };
            dtAreaPosition = new DataTable();
            selSQL = "select Name,Tags,FontSylte,FontColor from K_RecommendAreaPosition where RecommendAreaID=@AreaID and IsDel=0 and IsEnable=1 order by Orders desc";

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
                dtAreaPosition.Load(sqlReader);
            }
            catch
            {
                dtAreaPosition = null;
            }

            return dtAreaPosition;
        }
        #endregion
    }
}

