/*----------------------------------------------------------------------------------------
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-03-09
// 功能描述：对K_ModelManage表的的操作// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.Content;
using KingTop.Common;
using KingTop.Model.Content;

namespace KingTop.SQLServer.Content
{
    public partial class ModelManage : IModelManage
    {
        #region 根据传入的参数查询K_ModelManage,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ModelManage,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_ModelManageSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_ModelManage记录
        /// <summary>
        /// 设置或者删除K_ModelManage记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModelManageSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_ModelManageSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion

        #region 增、改K_ModelManage表
        /// <summary>
        /// 增、改K_ModelManage表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="paramsModel"></param>
        /// <param name="creatSQL"></param>
        /// <param name="sysFieldSql"></param>
        /// <param name="linkList">[0]</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.Content.ModelManage paramsModel, string ddlSQL, string dmlSQL, string operTitle, string operName, int operCount)
        {
            string isOk = "";
            List<SqlParameter> lstParam;

            lstParam = new List<SqlParameter>();

            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);   //当存储过程返回类型改了后，此处要做相应修改

                returnValue.Direction = ParameterDirection.Output;
                lstParam.Add(returnValue);
                string cmdText = "proc_K_ModelManageSave";

                lstParam.Add(new SqlParameter("tranType", tranType));
                lstParam.Add(new SqlParameter("ID", paramsModel.ID));
                lstParam.Add(new SqlParameter("MenuNo", paramsModel.MenuNo));
                lstParam.Add(new SqlParameter("SiteID", paramsModel.SiteID));
                lstParam.Add(new SqlParameter("Title", paramsModel.Title));
                lstParam.Add(new SqlParameter("TableName", paramsModel.TableName));
                lstParam.Add(new SqlParameter("ModuleID", paramsModel.ModuleID));
                lstParam.Add(new SqlParameter("SysField", paramsModel.SysField));
                lstParam.Add(new SqlParameter("ListLink", paramsModel.ListLink));
                lstParam.Add(new SqlParameter("ListButton", paramsModel.ListButton));
                lstParam.Add(new SqlParameter("CustomCol", paramsModel.CustomCol));
                lstParam.Add(new SqlParameter("OperationColumn", paramsModel.OperationColumn));
                lstParam.Add(new SqlParameter("Memo", paramsModel.Memo));
                lstParam.Add(new SqlParameter("Orders", paramsModel.Orders));
                lstParam.Add(new SqlParameter("IsEnable", paramsModel.IsEnable));
                lstParam.Add(new SqlParameter("IsDel", paramsModel.IsDel));
                lstParam.Add(new SqlParameter("IsHtml", paramsModel.IsHtml));
                lstParam.Add(new SqlParameter("@IsOrderEdit", paramsModel.IsOrderEdit));
                lstParam.Add(new SqlParameter("@ddlSql", ddlSQL));
                lstParam.Add(new SqlParameter("@dmlSql", dmlSQL));
                lstParam.Add(new SqlParameter("@operTitle", operTitle));
                lstParam.Add(new SqlParameter("@operName", operName));
                lstParam.Add(new SqlParameter("@operCount", operCount));
                lstParam.Add(new SqlParameter("@IsListContentClip", paramsModel.IsListContentClip));
                lstParam.Add(new SqlParameter("@OperationColumnWidth", paramsModel.OperationColumnWidth));
                lstParam.Add(new SqlParameter("@ConfigMan", paramsModel.ConfigMan));
                lstParam.Add(new SqlParameter("@CommonField", paramsModel.CommonField));
                lstParam.Add(new SqlParameter("@IsSub", paramsModel.IsSub));
                lstParam.Add(new SqlParameter("@NotSearchField", paramsModel.NotSearchField));
                lstParam.Add(new SqlParameter("@BackDeliverUrlParam", paramsModel.BackDeliverUrlParam));
                lstParam.Add(new SqlParameter("@FieldFromUrlParamValue", paramsModel.FieldFromUrlParamValue));
                lstParam.Add(new SqlParameter("@DeliverAndSearchUrlParam",paramsModel.DeliverAndSearchUrlParam));

                if (paramsModel.SubModelGroupID != null && paramsModel.SubModelGroupID.Trim() != "")
                {
                    lstParam.Add(new SqlParameter("@SubModelGroupID", paramsModel.SubModelGroupID));
                }

                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, lstParam.ToArray());
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
                new SqlParameter("@Table","K_ModelManage"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","Orders DESC"),                
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
                    if (string.Equals(kvp.Key.ToLower(), "isdel") || string.Equals(kvp.Key.ToLower(), "issub"))
                    {
                        if (string.IsNullOrEmpty(sqlWhere))
                        {
                            sqlWhere = kvp.Key + "=" + kvp.Value;
                        }
                        else
                        {
                            sqlWhere += " and " + kvp.Key + "=" + kvp.Value;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(sqlWhere))
                        {
                            sqlWhere = kvp.Key + " like '%" + kvp.Value + "%' ";
                        }
                        else
                        {
                            sqlWhere += " and " + kvp.Key + " like '%" + kvp.Value + "%' ";
                        }
                    }
                }
            }

            return sqlWhere;
        }
        #endregion
    }
}

