
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.Content;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武 2010-08-02
// 功能描述：对K_RecyclingManage表的的操作

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.SQLServer.Content
{
    public class RecyclingManage : IRecyclingManage
    {
        #region 根据传入的参数查询K_RecyclingManage,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_RecyclingManage,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
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
                      CommandType.StoredProcedure, "proc_K_RecyclingManageSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_RecyclingManage记录
        /// <summary>
        /// 设置或者删除K_RecyclingManage记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string RecyclingManageSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_RecyclingManageSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion


        #region 增、改K_RecyclingManage表
        /// <summary>
        /// 增、改K_RecyclingManage表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.Content.RecyclingManage paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_RecyclingManageSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("ID",paramsModel.ID),
                    new SqlParameter("pKey",paramsModel.pKey),
                    new SqlParameter("DelKey",paramsModel.DelKey),
                    new SqlParameter("TitleKey",paramsModel.TitleKey),
                    new SqlParameter("DelTimeKey",paramsModel.DelTimeKey),
                    new SqlParameter("NodeCode",paramsModel.NodeCode),
                    new SqlParameter("IsManage",paramsModel.IsManage),
                    new SqlParameter("IsReductive",paramsModel.IsReductive),
                    new SqlParameter("ListUrl",paramsModel.ListUrl),
                    new SqlParameter("Desc",paramsModel.Desc),
                    new SqlParameter("IsProcDel",paramsModel.IsProcDel),
                    new SqlParameter("ProcDelText",paramsModel.ProcDelText),
                    new SqlParameter("IsProcReductive",paramsModel.IsProcReductive),
                    new SqlParameter("ProcReductiveText",paramsModel.ProcReductiveText),
                    new SqlParameter("tableName",paramsModel.TableName),
                    new SqlParameter("AddMan",paramsModel.AddMan),
                      new SqlParameter("IsOnDesc",paramsModel.IsOnDesc),
                    returnValue
                 };

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                isOk = returnValue.Value.ToString();
            }
            catch (Exception ex)
            {
                isOk = "0|"+ex.Message;

            }

            return isOk;
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table","K_RecyclingManage"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","ID DESC"),                
                new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        /// <summary>
        /// 获得新闻分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageNewsData(KingTop.Model.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table","view_recycling"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou",pager.SearField),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","delTime DESC"),                
                new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        /// <summary>
        /// 创建视图
        /// </summary>
       public  void CreateView()
        {
            DataTable dt = GetList("all", Utils.getOneParams(""));
            StringBuilder sb = new StringBuilder(64);          
            sb.AppendLine("CREATE VIEW VIEW_RECYCLING AS ");
            sb.AppendLine("select * from (");
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {                
                if (!Utils.ParseBool(dr["IsManage"].ToString()))  //如果当前表不接受管理,则进入下一记录
                {
                    continue;  
                }
                if (i > 0)
                {
                    sb.AppendLine(" union ");                    
                }
                if(Utils.ParseBool(dr["IsOnDesc"].ToString())) //所属节点是否使用描述
                {
                 sb.AppendFormat("select cast([{0}] as nvarchar(50)) as ID,[{1}] as isDel,[{2}] as title,[{3}] as delTime,'{4}' as NodeName,'{5}' as tableName2 from {5} where isdel=1 ",
                    dr["pkey"], dr["delKey"], dr["titleKey"], dr["delTimeKey"], dr["Desc"], dr["tableName"]);
                }
                else
                {
                     sb.AppendFormat("select cast([{0}] as nvarchar(50)) as ID,[{1}] as isDel,[{2}] as title,[{3}] as delTime,(select a.NodeName from K_SysModuleNode as a where a.NodeCode ={5}.[{4}]) as NodeName,'{5}' as tableName2 from {5} where isdel=1 ",
                    dr["pkey"], dr["delKey"], dr["titleKey"], dr["delTimeKey"], dr["nodecode"], dr["tableName"]);
                }
                i += 1;
            }
            sb.AppendLine(") a");
            SqlParameter[] parms = new SqlParameter[] { 
                new SqlParameter("str",sb.ToString())
            };
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "PROC_RECYCLINGADD", parms);
        }


        //得到查询条件
        string GetWhere(Dictionary<string, string> DicWhere)
        {
            StringBuilder sbSql = new StringBuilder(40);
            sbSql.Append(" 1=1 ");
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "tableName" || kvp.Key == "title")
                    {
                        Utils.GetWhereAppend(kvp.Key, kvp.Value, "LIKE", KingTop.Model.SqlParamType.Str, ref sbSql);
                    }
                    else
                    {
                        Utils.GetWhereAppend(kvp.Key, kvp.Value, "=", KingTop.Model.SqlParamType.Str, ref sbSql);
                    }
                }
            }

            return sbSql.ToString();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
       public void Del(string Id)
        {
            string[] strsId = Utils.strSplit(Id, "|");
            DataTable dt = GetList("ALLORONE", Utils.getOneParams(strsId[1]));
            StringBuilder sb = new StringBuilder();           
            foreach (DataRow dr in dt.Rows)
            {
                if (Utils.ParseBool(dr["IsProcDel"].ToString()))  //是否使用存储过程
                {
                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "exec sp_executesql \""+dr["procdeltext"].ToString().Replace("{ID}",strsId[0])+"\"", null);
                }
                else
                {
                    sb.AppendLine("begin tran ");
                    Del_Next(strsId[0], ref sb, dr, true);
                    sb.AppendLine("if @@error>0 ");
                    sb.AppendLine("rollback ");
                    sb.AppendLine("else ");
                    sb.AppendLine("commit");
                    SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sb.ToString(), null);
                }
            }            
        }

        void Del_Next(string strId, ref StringBuilder sb, DataRow dr, bool isTop)
        {
            DataTable dt = null;          
            if (isTop)
            {
                dt = GetList("ASSOCIATED", Utils.getOneParams(dr["id"].ToString()));              
            }
            else
            {
                dt = GetList("ASSOCIATED", Utils.getOneParams(dr["AssociatedID"].ToString()));               
            }

            foreach (DataRow dr2 in dt.Rows)  //如果有关联表
            {
                string strSql = null;
                if (!Utils.ParseBool(dr2["KeyIsWhere"].ToString()))  //如果不把外键当条件
                {
                    strSql = "SELECT [" + dr2["AssociatedKey"].ToString() + "] FROM " + dr2["tableName"].ToString() + " WHERE 1=1 " + dr2["AssociatedWhere"].ToString().Replace("{ID}", strId);
                }
                else
                {
                    strSql = "SELECT [" + dr2["AssociatedKey"].ToString() + "] FROM " + dr2["tableName"].ToString() + " WHERE [" + dr2["AssociatedKey"].ToString() + "]='" + strId + "' " + dr2["AssociatedWhere"].ToString().Replace("{ID}", strId);
                }
                DataTable dt3 = GetList("ASSOCIATEDNEWS", Utils.getOneParams(strSql));
                if (dt3.Rows.Count == 0)  //如果关联表没有数据  则不用再作添加
                {
                    continue;
                }
                DataTable dt2 = GetList("ASSOCIATED", Utils.getOneParams(dr2["AssociatedID"].ToString()));
                if (dt2.Rows.Count > 0)  //如果关联表还有关联表   代码可以优化 不用返回表来做判断
                {
                    foreach (DataRow dr3 in dt3.Rows)
                    {
                        Del_Next(dr3["id"].ToString(), ref sb, dr2, false);
                    }
                }
                else
                {
                    if (!Utils.ParseBool(dr2["KeyIsWhere"].ToString()))  //如果不把外键当条件
                    {
                        sb.AppendLine("delete from " + dr2["tableName"].ToString() + " where 1=1 " + dr2["AssociatedWhere"].ToString().Replace("{ID}", strId));
                    }
                    else
                    {
                        sb.AppendLine("delete from " + dr2["tableName"].ToString() + " where [" + dr2["AssociatedKey"].ToString() + "]='" + strId + "' " + dr2["AssociatedWhere"].ToString().Replace("{ID}", strId));
                    }
                }
            }

            if (isTop)
            {               
                sb.AppendLine("delete from " + dr["tableName"].ToString() + " where "+dr["pkey"].ToString()+"='" + strId + "'");
            }
            else
            {
                sb.AppendLine("delete from " + dr["tableName"].ToString() + " where " + dr["pkey"].ToString() + "='" + strId + "' " + dr["AssociatedWhere"].ToString().Replace("{ID}", strId));
            }
        }
        #endregion
    }
}
