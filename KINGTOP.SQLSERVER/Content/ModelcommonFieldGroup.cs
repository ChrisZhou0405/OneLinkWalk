﻿
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.Content;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：袁纯林 ycl@360hqb.com 2010-08-30
// 功能描述：对k_ModelcommonFieldGroup表的的操作

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.SQLServer.Content
{
    public class ModelcommonFieldGroup : IModelcommonFieldGroup
    {
        #region 根据传入的参数查询k_ModelcommonFieldGroup,返回查询结果
        /// <summary>
        /// 根据传入的参数查询k_ModelcommonFieldGroup,返回查询结果
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
                      CommandType.StoredProcedure, "proc_k_ModelcommonFieldGroupSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除k_ModelcommonFieldGroup记录
        /// <summary>
        /// 设置或者删除k_ModelcommonFieldGroup记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModelcommonFieldGroupSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_k_ModelcommonFieldGroupSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion

        #region 增、改k_ModelcommonFieldGroup表
        /// <summary>
        /// 增、改k_ModelcommonFieldGroup表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.Content.ModelcommonFieldGroup paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_k_ModelcommonFieldGroupSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("ID",paramsModel.ID),
                    new SqlParameter("Name",paramsModel.Name),
                    new SqlParameter("Orders",paramsModel.Orders),
                    new SqlParameter("AddMan",paramsModel.AddMan),
                    new SqlParameter("IsDel",paramsModel.IsDel),

                    returnValue
                 };

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
                isOk = returnValue.Value.ToString();
            }
            catch (Exception ex)
            {
                isOk = ex.Message;

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
                new SqlParameter("@Table","k_ModelcommonFieldGroup"),             
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

        //得到查询条件
        string GetWhere(Dictionary<string, string> DicWhere)
        {
            string sqlWhere = "";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    sqlWhere = kvp.Key + "like '%" + kvp.Value + "%'";
                }
            }

            return sqlWhere;
        }
        #endregion
    }
}
