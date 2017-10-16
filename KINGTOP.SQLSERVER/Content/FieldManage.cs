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
    public class FieldManage : KingTop.IDAL.Content.IFieldManage
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

        #region 得到模模字段分组
        /// <summary>
        /// 通过模型ID返回当前模型的分组
        /// </summary>
        public DataTable GetModelFieldGroup(string modelID)
        {
            string selSQL;
            SqlParameter[] selParam;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selSQL = "select ID,Name from K_ModelFieldGroup where ModelId=@ModelID";
            selParam = new SqlParameter[] { new SqlParameter("@ModelID", modelID) };
            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction,CommandType.Text,selSQL, selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 添加/更新
        public int Save(Hashtable hsFieldParam, string tableName, string modelFieldID, string originalDefaultValue, string originalDataColumnLength)
        {
            string editFieldSQL;                // 更新字段记录
            StringBuilder editSQL;              // 更新操作
            SqlParameter[] editParam;           // 字段记录值
            int effectRow;                      // 影响行数

            editSQL = new StringBuilder();
            editFieldSQL = string.Empty;

            editParam = GetEditSQL(hsFieldParam,modelFieldID,ref editFieldSQL);

            editSQL.Append("begin transaction CONTENT_MODELFIELD_CREADING         ");
            editSQL.Append("begin try	                                          ");
            editSQL.Append(editFieldSQL);

            if (!string.IsNullOrEmpty(modelFieldID))        // 编辑
            {
               effectRow = AlterColumn(ref hsFieldParam,tableName,originalDefaultValue,originalDataColumnLength);
            }
            else  // 添加
            {
                effectRow = 1;
                editSQL.Append(AddColumnSQL(hsFieldParam,tableName));
            }

            editSQL.Append("COMMIT  transaction CONTENT_MODELFIELD_CREADING;      ");
            editSQL.Append("end try                                               ");
            editSQL.Append("begin catch                                           ");
            editSQL.Append("rollback transaction CONTENT_MODELFIELD_CREADING;     ");
            editSQL.Append("end catch");

            if (effectRow > 0)  // 缺省更新成功
            {
                try
                {
                    effectRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, editSQL.ToString(), editParam);
                }
                catch
                {
                    effectRow = 0;
                }
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
                editSQL.Append("update K_ModelField set ");

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

                    editParam.Add(new SqlParameter("@" + key, hsFieldParam[key]));
                }

                editSQL.Append(tempFieldName);
                editSQL.Append(" where ID=@ID;");
            }
            else  // 添加，插入新记录
            {
                editSQL.Append("insert into  K_ModelField(");

                foreach (string key in hsFieldParam.Keys)   // 遍历添加所有需更新的字段
                {
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

                    editParam.Add(new SqlParameter("@" + key, hsFieldParam[key]));
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

        #region 添加操作时组装添加字段列代码
        private StringBuilder AddColumnSQL(Hashtable hsFieldParam, string tableName)
        {
            StringBuilder alterTableSQL;            // 添加字段列SQL
            int basicField;

            basicField = Utils.ParseInt(hsFieldParam["BasicField"].ToString(),0);
            alterTableSQL = new StringBuilder();

            if (basicField > 0)
            {
                alterTableSQL.Append("alter table ");
                alterTableSQL.Append(tableName);
                alterTableSQL.Append(" add [");
                alterTableSQL.Append(hsFieldParam["Name"].ToString() + "]");

                switch (basicField)
                {
                    case 1:     // 单行文件
                    case 2:     // 多行文本（不支持HTML）                    case 3:     // 多行文本（支持HTML）                    case 4:     // 单选                    case 5:     // 多选                    case 6:     // 下拉列表
                    case 7:     // 列表（可选择多列）                    case 11:    // 图片
                    case 12:    // 文件
                    case 13:    // 隐藏域                    case 14:    // 子模型
                    case 15:    // 子模型                        alterTableSQL.Append(" nvarchar(");

                        if (Utils.ParseInt(hsFieldParam["DataColumnLength"], 0) >= 4000)
                        {
                            alterTableSQL.Append("max");
                        }
                        else
                        {
                            alterTableSQL.Append(hsFieldParam["DataColumnLength"].ToString().Trim());
                        }

                        alterTableSQL.Append(") null ");

                        if (hsFieldParam["DefaultValue"].ToString() != "")
                        {
                            if (hsFieldParam["DefaultValue"].ToString().Contains("()"))       // 为SQL函数
                            {
                                alterTableSQL.Append(" default(" + hsFieldParam["DefaultValue"].ToString() + ") ");
                            }
                            else
                            {
                                alterTableSQL.Append(" default('" + hsFieldParam["DefaultValue"].ToString() + "') ");
                            }
                        }
                        break;
                    case 8:     // 数字
                    case 9:     // 货币
                        alterTableSQL.Append(" float null ");

                        if (hsFieldParam["DefaultValue"].ToString().Trim() != "")
                        {
                            alterTableSQL.Append(" default(" + hsFieldParam["DefaultValue"].ToString() + ") ");
                        }
                        break;
                    case 10:    // 日期时间
                        alterTableSQL.Append(" datetime null ");

                        switch (hsFieldParam["DateDefaultOption"].ToString())   // 默认值方式                        {
                            case "1":   // 无                                break;
                            case "2":   // 当前日期
                                alterTableSQL.Append(" default(getdate()) ");
                                break;
                            case "3":   // 指定日期
                                alterTableSQL.Append(" default('" + hsFieldParam["DefaultValue"].ToString() + "') ");
                                break;
                        }
                        break;
                }

                alterTableSQL.Append(";");
            }

            return alterTableSQL;
        }
        #endregion

        #region 更新列长度及缺省值        private int AlterColumn(ref Hashtable hsFieldParam, string tableName, string originalDefaultValue, string originalDataColumnLength)
        {
            List<SqlParameter> editParam;
            SqlParameter result;
            int returnValue;

            editParam = new List<SqlParameter>();
            result = new SqlParameter("@Result",0);

            result.Direction = ParameterDirection.Output;
            editParam.Add(result);
            editParam.Add(new SqlParameter("@ModelID",hsFieldParam["ModelID"]));
            editParam.Add(new SqlParameter("@TableName",tableName));
            editParam.Add(new SqlParameter("@FieldName",hsFieldParam["Name"]));


            if(hsFieldParam["DefaultValue"] != null && !string.Equals(hsFieldParam["DefaultValue"].ToString(),originalDefaultValue))                        // 更改字段缺省值
            {
                editParam.Add(new SqlParameter("@DefaultValue",hsFieldParam["DefaultValue"].ToString()));
            }

            if (hsFieldParam["DataColumnLength"] != null && hsFieldParam["DataColumnLength"].ToString().Trim() != originalDataColumnLength.Trim()) //  列改字段长度 
            {
                try
                {
                    editParam.Add(new SqlParameter("@Length", int.Parse(hsFieldParam["DataColumnLength"].ToString())));
                }
                catch { }
            }
            hsFieldParam.Remove("Name");
            hsFieldParam.Remove("ModelID");

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction,CommandType.StoredProcedure,"proc_K_FieldManageAlter",editParam.ToArray());
                returnValue = Utils.ParseInt(result.Value,0);
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
       }
        #endregion

        #region 根据传入的参数查询K_ModelField,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ModelField,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_ModelFieldSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_ModelField记录
        /// <summary>
        /// 设置或者删除K_ModelField记录
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
                          CommandType.StoredProcedure, "proc_K_FieldManageSet", prams);
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

            selTotalSQL = "select count(*) from  K_ModelField where " + selSQL.ToString() + ";";

            if (!string.IsNullOrEmpty(sort))    // 加入自定义排序
            {
                selSQL.Insert(0, "select T.* from (select ROW_NUMBER() over(order by "+ sort +") as IndexNum,ID,Name,ModelFieldGroupId,IsRequired,FieldAlias,BasicField,IsSystemFiierd,IsDel,IsEnable,Orders,IsListEnable,IsSearch,IsInputValue from K_ModelField where ");
            }
            else // 默认排序
            {
                selSQL.Insert(0, "select T.* from (select ROW_NUMBER() over(order by orders asc) as IndexNum,ID,Name,ModelFieldGroupId,IsRequired,FieldAlias,BasicField,IsSystemFiierd,IsDel,IsEnable,Orders,IsListEnable,IsSearch,IsInputValue from K_ModelField where ");
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

        #region 字段配置
        /// <summary>
        /// 字段配置初始加载
        /// </summary>
        /// <param name="configType">配置类型 1 搜索 2 列表  3 编辑</param>
        /// <param name="fieldID">要配置的字段ID</param>
        /// <returns></returns>
        public DataTable GetSpecialFieldAtrr(string configType, string fieldID)
        {
            string selSQL;
            SqlParameter[] selParam;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selParam = new SqlParameter[]{new SqlParameter("@ID",fieldID)};
            selSQL = string.Empty;

            switch (configType)
            {
                case "1":       // 搜索
                    selSQL = "select FieldAlias,BasicField,IsSearch,SearchUIType,SearchWidth,SearchOrders from K_ModelField where ID=@ID";
                    break;
                case "2":       // 列表
                    selSQL = "select FieldAlias,BasicField,IsListEnable,ListWidth,ListAlignment,ListOrders,ListIsLink,ListLinkUrl,ListIsOrder,ListOrderOption,ThumbDisplayType from K_ModelField where ID=@ID";
                    break;
                case "3":       // 编辑
                    selSQL = "select FieldAlias,BasicField,[Message],IsInputValue,TextBoxHieght,TextBoxWidth,DefaultValue, DataColumnLength from K_ModelField where ID=@ID";
                    break;
            }

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text,selSQL, selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 更新字段配置
        /// <summary>
        /// 更新字段配置
        /// </summary>
        /// <param name="hsFieldValue">字段值对</param>
        /// <returns></returns>
        public int EditSpecialFieldAtrr(Hashtable hsFieldValue)
        {
            List<SqlParameter> editParam;
            SqlParameter result;
            int returnValue;

            editParam = new List<SqlParameter>();
            result = new SqlParameter("@Result",0);
            result.Direction = ParameterDirection.Output;
            editParam.Add(result);

            foreach (string key in hsFieldValue.Keys)
            {
                if (!string.IsNullOrEmpty(key))   
                {
                    editParam.Add(new SqlParameter("@" + key, hsFieldValue[key]));
                }
            }

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_FieldListConfig", editParam.ToArray());
                returnValue = Utils.ParseInt(result.Value.ToString(),0);
            }
            catch
            {
                returnValue = 0;
            }

            return returnValue;
        }
        #endregion

        #region 子模型分组列表
        public DataTable GetSubModelGroupList()
        {
            SqlDataReader sqlReader;
            DataTable dt;
            string selSQL;
            
            dt = new DataTable();
            selSQL = "select ID,Name from K_SubModelGroup;";

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt.Load(sqlReader);
            }
            catch { }

            return dt;
        }
        #endregion

        #region 子模型列表
        public DataTable GetSubModelList()
        { 
            SqlDataReader sqlReader;
            DataTable dt;
            string selSQL;
            
            dt = new DataTable();
            selSQL = "select TableName,Title from K_ModelManage where IsSub=1;";

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt.Load(sqlReader);
            }
            catch { }

            return dt;
        }
        #endregion
    }
}
