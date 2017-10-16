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
// 功能描述：模板表相关操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.SQLServer.SysManage
{
    public class WebSiteTemplatePermit : IWebSiteTemplatePermit
    {
        #region 根据传入的参数查询K_Template,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_Template,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_WebSiteTemplatePermitSel", prams).Tables[0];
        }
        #endregion
    }
}
