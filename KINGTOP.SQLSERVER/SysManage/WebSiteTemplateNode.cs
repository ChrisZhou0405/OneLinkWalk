using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.SysManage;
using KingTop.Common;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：陈顺
// 创建日期：2010-04-16
// 功能描述：模板节点表相关操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.SQLServer.SysManage
{
    public class WebSiteTemplateNode : IWebSiteTemplateNode
    {
        #region 根据传入的参数查询K_TemplateNode,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_TemplateNode,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_WebSiteTemplateNodeSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_TemplateNode记录
        /// <summary>
        /// 设置或者删除K_TemplateNode记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string TemplateNodeSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_WebSiteTemplateNodeSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion


        #region 增、改K_TemplateNode表
        /// <summary>
        /// 增、改K_TemplateNode表
        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.SysManage.WebSiteTemplateNode paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_WebSiteTemplateNodeSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("ID", paramsModel.ID),
                    new SqlParameter("TemplateID",paramsModel.TemplateID),
                    new SqlParameter("NodeCode",paramsModel.NodeCode),
                    new SqlParameter("NodeName",paramsModel.NodeName),
                    new SqlParameter("NodeType",paramsModel.NodeType),
                    new SqlParameter("LinkURL",paramsModel.LinkURL),
                    new SqlParameter("ParentNode",paramsModel.ParentNode),
                    new SqlParameter("IsValid",paramsModel.IsValid),
                    new SqlParameter("ModuleID",paramsModel.ModuleID),
                    new SqlParameter("NodelOrder",paramsModel.NodelOrder),
                    new SqlParameter("NodelDesc",paramsModel.NodelDesc),
                    new SqlParameter("NodelEngDesc",paramsModel.NodelEngDesc),
                    new SqlParameter("IsSystem",paramsModel.IsSystem),
                    new SqlParameter("IsWeb",paramsModel.IsWeb),
                    new SqlParameter("ReviewFlowID",paramsModel.ReviewFlowID),
                    new SqlParameter("IsContainWebContent",paramsModel.IsContainWebContent),
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
    }
}
