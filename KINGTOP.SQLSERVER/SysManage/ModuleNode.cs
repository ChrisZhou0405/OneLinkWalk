using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL;
using KingTop.Common;
using System.Text;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：肖丹
// 创建日期：2010-03-22
// 功能描述：对K_SysModuleNode表的的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
namespace KingTop.SQLServer.SysManage
{
    public class ModuleNode : KingTop.IDAL.SysManage.IModuleNode
    {
        #region 根据传入的参数查询K_SysModuleNode,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysModuleNode,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_SysModuleNodeSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_SysModuleNode记录
        /// <summary>
        /// 设置或者删除K_SysModuleNode记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModuleNodeSet(string tranType, string setValue, string IDList)
        {
            string sRe = "";
            try
            {
                SqlParameter[] prams = new SqlParameter[]{
                    new SqlParameter("trantype",tranType),
                    new SqlParameter("SetValue",setValue),
                    new SqlParameter("IDList",IDList)
                };

                int iRe= SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction,
                          CommandType.StoredProcedure, "proc_K_SysModuleNodeSet", prams);

                sRe = iRe.ToString();
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion

        #region 增、改K_SysModuleNode表

        /// <summary>
        /// 增、改K_SysModuleNode表

        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string tranType, KingTop.Model.SysManage.ModuleNode paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_SysModuleNodeSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("NodeID",paramsModel.NodeID),
                    new SqlParameter("NodeCode",paramsModel.NodeCode),
                    new SqlParameter("NodeName",paramsModel.NodeName),
                    new SqlParameter("NodeType",paramsModel.NodeType),
                    new SqlParameter("LinkURL",paramsModel.LinkURL),
                    new SqlParameter("ParentNode",paramsModel.ParentNode),
                    new SqlParameter("ModuleID",paramsModel.ModuleID),
                    new SqlParameter("IsLeftDisplay",paramsModel.IsLeftDisplay),
                    new SqlParameter("IsValid",paramsModel.IsValid),
                    new SqlParameter("NodelOrder",paramsModel.NodelOrder),
                    new SqlParameter("NodelDesc",paramsModel.NodelDesc),
                    new SqlParameter("NodelEngDesc",paramsModel.NodelEngDesc),
                    new SqlParameter("NodelIcon",paramsModel.NodelIcon),
                    new SqlParameter("MouseOverImg",paramsModel.MouseOverImg),
                    new SqlParameter("CurrentImg",paramsModel.CurrentImg),
                    new SqlParameter("IsSystem",paramsModel.IsSystem),
                    new SqlParameter("IsWeb",paramsModel.IsWeb),
                    new SqlParameter("WebSiteID",paramsModel.WebSiteID),
                    new SqlParameter("NodeDir",paramsModel.NodeDir),
                    new SqlParameter("Tips",paramsModel.Tips),
                    new SqlParameter("Meta_Keywords",paramsModel.Meta_Keywords),
                    new SqlParameter("Meta_Description",paramsModel.Meta_Description),
                    new SqlParameter ("PageTitle",paramsModel.PageTitle ),
                    new SqlParameter("Custom_Content",paramsModel.Custom_Content),
                    new SqlParameter("DefaultTemplate",paramsModel.DefaultTemplate),
                    new SqlParameter("ListPageTemplate",paramsModel.ListPageTemplate),
                    new SqlParameter("ContentTemplate",paramsModel.ContentTemplate),
                    new SqlParameter("EnableSubDomain",paramsModel.EnableSubDomain),
                    new SqlParameter("SubDomain",paramsModel.SubDomain),
                    new SqlParameter("Settings",paramsModel.Settings),
                    new SqlParameter("Creater",paramsModel.Creater),
                    new SqlParameter("CreateDate",paramsModel.CreateDate),
                    new SqlParameter("ReviewFlowID",paramsModel.ReviewFlowID),
                    new SqlParameter("OpenType",paramsModel.OpenType),
                    new SqlParameter("PurviewType",paramsModel.PurviewType),
                    new SqlParameter("IsEnableComment",paramsModel.IsEnableComment),
                    new SqlParameter("IsCreateListPage",paramsModel.IsCreateListPage),
                    new SqlParameter("IncrementalUpdatePages",paramsModel.IncrementalUpdatePages),
                    new SqlParameter("IsEnableIndexCache",paramsModel.IsEnableIndexCache),
                    new SqlParameter("ListPageSavePathType",paramsModel.ListPageSavePathType),
                    new SqlParameter("ListPagePostFix",paramsModel.ListPagePostFix),
                    new SqlParameter("IsCreateContentPage",paramsModel.IsCreateContentPage),
                    new SqlParameter("ContentPageHtmlRule",paramsModel.ContentPageHtmlRule),
                    new SqlParameter("AutoCreateHtmlType",paramsModel.AutoCreateHtmlType),
                    new SqlParameter("Custom_Images",paramsModel.Custom_Images),
                    new SqlParameter("IsContainWebContent",paramsModel.IsContainWebContent),
                    new SqlParameter("ColumnType",paramsModel.ColumnType),
                    new SqlParameter("CustomManageLink",paramsModel.CustomManageLink),
                    new SqlParameter("Banner",paramsModel.Banner),
                    new SqlParameter("IsTopMenuShow",paramsModel.IsTopMenuShow),
                    new SqlParameter("IsLeftMenuShow",paramsModel.IsLeftMenuShow),
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
                new SqlParameter("@Id","NodeID"),
                new SqlParameter("@Table","K_SysModuleNode"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","NodelOrder DESC"),                
                 new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };


            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        string GetWhere(Dictionary<string, string> DicWhere)
        {
            string sqlWhere = "";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "SiteID")
                    {
                        sqlWhere = kvp.Key + " = " + kvp.Value;
                    }
                    else
                    {
                        sqlWhere = kvp.Key + " like '%" + kvp.Value + "%'";
                    }
                }
            }

            return sqlWhere;
        }
        #endregion

        #region 栏目移动
        public string MenuMove(string fromMenu, string toMenu)
        {
            string reMsg = string.Empty;
            string strMaxNodeCode = string.Empty;
            //生成目标NodeCode
            DataTable dtNodeCode = GetList("MAXCODE", Utils.getOneParams(toMenu));
            if (dtNodeCode.Rows.Count > 0)
            {
                if (dtNodeCode.Rows[0]["NodeCode"].ToString() != "")
                    strMaxNodeCode = dtNodeCode.Rows[0]["NodeCode"].ToString();
                else  //如果没有NodeCode后面三位从001开始
                    strMaxNodeCode = toMenu + "001";
            }

            //读取源栏目记录
            dtNodeCode = GetList("GETSELFTREE", Utils.getOneParams(fromMenu));
            StringBuilder strSql = new StringBuilder();
            for (int i = 0; i < dtNodeCode.Rows.Count; i++)
            {

                if (dtNodeCode.Rows[i]["ColumnType"].ToString() == "2")  //单页栏目
                {
                    strSql.Append("UPDATE K_SinglePage SET NodeCode='" + strMaxNodeCode + dtNodeCode.Rows[i]["NodeCode"].ToString().Substring(fromMenu.Length) + "' WHERE NodeCode='" + dtNodeCode.Rows[i]["NodeCode"].ToString() + "';");
                }
                else if (!string.IsNullOrEmpty(dtNodeCode.Rows[i]["TableName"].ToString()))
                {
                    strSql.Append("UPDATE " + dtNodeCode.Rows[i]["TableName"].ToString() + " SET NodeCode='" + strMaxNodeCode + dtNodeCode.Rows[i]["NodeCode"].ToString().Substring(fromMenu.Length) + "' WHERE NodeCode='" + dtNodeCode.Rows[i]["NodeCode"].ToString() + "';");
                }

            }
            strSql.Append("UPDATE K_SysModuleNode SET NodeCode='" + strMaxNodeCode + "'+RIGHT(NodeCode,LEN(NodeCode)-" + fromMenu.Length + ") WHERE Left(NodeCode," + fromMenu.Length + ")='" + fromMenu + "';");
            strSql.Append("UPDATE K_SysModuleNode SET ParentNode=Left(NodeCode,Len(NodeCode)-3) WHERE Left(NodeCode," + strMaxNodeCode.Length + ")='" + strMaxNodeCode + "';");
            //if (toMenu.Length == 3)
            //{
            //    strSql.Append("UPDATE K_SysModuleNode SET NodeType=1 WHERE NodeCode='" + strMaxNodeCode + "';");
            //}
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                reMsg = "1";
            }
            catch (Exception ex)
            {
                reMsg = ex.Message;
            }

            return reMsg;
        }
        #endregion

        #region 栏目复制
        public string MenuCopy(string fromMenu, string toMenu, bool isDataCopy)
        {
            string reMsg = string.Empty;
            string strMaxNodeCode = string.Empty;
            string toNodeCode = string.Empty;

            //生成目标NodeCode
            DataTable dtNodeCode = GetList("MAXCODE", Utils.getOneParams(toMenu));
            if (dtNodeCode.Rows.Count > 0)
            {
                if (dtNodeCode.Rows[0]["NodeCode"].ToString() != "")
                    strMaxNodeCode = dtNodeCode.Rows[0]["NodeCode"].ToString();
                else  //如果没有NodeCode后面三位从001开始
                    strMaxNodeCode = toMenu + "001";
            }


            StringBuilder strSql = new StringBuilder();
            //复制节点
            strSql.Append("SELECT * INTO #TempMenuCopy FROM K_SysModuleNode WHERE Left(NodeCode," + fromMenu.Length + ")='" + fromMenu + "';");
            strSql.Append("UPDATE #TempMenuCopy SET NodeCode='" + strMaxNodeCode + "'+RIGHT(NodeCode,LEN(NodeCode)-" + fromMenu.Length + "),NodeID=NEWID();");
            strSql.Append("UPDATE #TempMenuCopy SET ParentNode=Left(NodeCode,Len(NodeCode)-3);");
            strSql.Append("INSERT INTO K_SysModuleNode SELECT * FroM #TempMenuCopy;drop table #TempMenuCopy; ");

            if (isDataCopy)
            {
                //单页栏目信息
                strSql.Append("SELECT * INTO #TempMenuCopy1 FROM K_SinglePage WHERE Left(NodeCode," + fromMenu.Length + ")='" + fromMenu + "';");
                strSql.Append("UPDATE #TempMenuCopy1 SET NodeCode='" + strMaxNodeCode + "'+RIGHT(NodeCode,LEN(NodeCode)-" + fromMenu.Length + "),ID=NEWID();");
                strSql.Append("INSERT INTO K_SinglePage SELECT * FROM #TempMenuCopy1;drop table #TempMenuCopy1; ");

                //模型类栏目
                //读取源栏目记录
                dtNodeCode = GetList("GETSELFTREE", Utils.getOneParams(fromMenu));
                for (int i = 0; i < dtNodeCode.Rows.Count; i++)
                {
                    toNodeCode = strMaxNodeCode + dtNodeCode.Rows[i]["NodeCode"].ToString().Substring(fromMenu.Length);


                    if (!string.IsNullOrEmpty(dtNodeCode.Rows[i]["TableName"].ToString()))
                    {
                        strSql.Append("SELECT * INTO #TempMenuCopy" + i + 2 + " FROM " + dtNodeCode.Rows[i]["TableName"].ToString() + " WHERE NodeCode='" + dtNodeCode.Rows[i]["NodeCode"].ToString() + "';");
                        strSql.Append("UPDATE #TempMenuCopy" + i + 2 + " SET NodeCode='" + strMaxNodeCode + dtNodeCode.Rows[i]["NodeCode"].ToString().Substring(fromMenu.Length) + "',ID=CAST(CAST(ID AS BIGINT)+100001 AS VARCHAR(15)),Orders=Orders+1;");
                        strSql.Append("INSERT INTO " + dtNodeCode.Rows[i]["TableName"].ToString() + " SELECT * FROM #TempMenuCopy" + i + 2 + ";drop table #TempMenuCopy" + i + 2 + "; ");
                    }
                }
            }

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                reMsg = "1";
            }
            catch (Exception ex)
            {
                reMsg = ex.Message;
            }

            return reMsg;
        }
        #endregion

        #region 栏目合并
        public string MenuUnite(string fromMenu, string toMenu)
        {
            string reMsg = string.Empty;
            string nodeId = string.Empty;
            //读取源栏目记录
            DataTable dtNodeCode = GetList("GETSELFTREE", Utils.getOneParams(fromMenu));
            StringBuilder strSql = new StringBuilder();
            if (dtNodeCode.Rows.Count > 0)
            {
                nodeId = dtNodeCode.Rows[0]["NodeId"].ToString();
                if (dtNodeCode.Rows[0]["ColumnType"].ToString() == "2")  //单页栏目
                {
                    strSql.Append("update K_SinglePage set content=convert(nvarchar(max),content)+(select convert(nvarchar(max),Content) From K_SinglePage Where NodeCode='" + fromMenu + "') Where NodeCode='" + toMenu + "';");
                }
                else if (!string.IsNullOrEmpty(dtNodeCode.Rows[0]["TableName"].ToString()))
                {
                    strSql.Append("UPDATE " + dtNodeCode.Rows[0]["TableName"].ToString() + " SET NodeCode='" + toMenu + "' WHERE NodeCode='" + fromMenu + "';");
                }
            }

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                ModuleNodeSet("DEL2", "", nodeId);
                reMsg = "1";
            }
            catch (Exception ex)
            {
                reMsg = ex.Message;
            }

            return reMsg;
        }
        #endregion
    }
}
