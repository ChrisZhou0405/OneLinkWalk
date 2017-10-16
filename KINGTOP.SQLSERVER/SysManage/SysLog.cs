using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.SysManage;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：袁纯林 ycl@360hqb.com
// 创建日期：2010-03-17
// 功能描述：对K_ManageLog表的的操作

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.SQLServer.SysManage
{
    public class SysLog : ISysLog
    {

        #region 根据传入的参数查询K_SysMessage,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysMessage,返回查询结果
        /// </summary>
        /// <param Name="tranType">操作类型</param>
        /// <param Name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel,Dictionary<string,string> dictWhere)
        {
            string strWhere = GetWhere(dictWhere);
            SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("tranType",tranType),
                    new SqlParameter("I1",paramsModel.I1),
                    new SqlParameter("I2",paramsModel.I2),
                    new SqlParameter("S1",paramsModel.S1),
                    new SqlParameter("S2",strWhere)
                };

            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_K_SysLogSel", prams).Tables[0];
        }
        #endregion

        #region 增、改K_ManageLog表
        /// <summary>
        /// 增、改K_ManageLog表
        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.SysManage.SysLog paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_SysLogSave";
                
                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("NodeCode",paramsModel.NodeCode),
                    new SqlParameter("SiteID",paramsModel.SiteID),
                    new SqlParameter("Content",paramsModel.Content),
                    new SqlParameter("UserNo",paramsModel.UserNo),
                    new SqlParameter("IP",paramsModel.IP),
                    new SqlParameter("ScriptName",paramsModel.ScriptName),
                    new SqlParameter("PostContent",paramsModel.PostContent),
                    new SqlParameter("LogType",paramsModel.LogType),

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
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table","K_SysLog"),             
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
            ds.Dispose();
        }

        //设置条件
        string GetWhere(Dictionary<string, string> DicWhere)
        {
            string sqlWhere = " SiteID= " + DicWhere["SiteID"]+" AND isDel=0";
            if (DicWhere.ContainsKey("Content"))
            {
                sqlWhere += " and (Content like '%" + DicWhere["Content"].Replace("'", "") + "%' or UserNo like '%" + DicWhere["UserNo"].Replace("'", "") + "%' or ScriptName like '%" + DicWhere["ScriptName"].Replace("'", "") + "%')";
            }
            if (DicWhere.ContainsKey("StartDate"))
            {
                sqlWhere += " and DateDiff(dd,AddDate,'" + DicWhere["StartDate"] + "')<=0";
            }
            if (DicWhere.ContainsKey("EndDate"))
            {
                sqlWhere += " and DateDiff(dd,AddDate,'" + DicWhere["EndDate"] + "')>=0";
            }
            if (DicWhere.ContainsKey("Type"))
            {
                sqlWhere += " AND LogType=" + DicWhere["Type"];
            }
            return sqlWhere;
        }
        #endregion

    }
}
