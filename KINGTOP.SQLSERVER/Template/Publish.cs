#region 程序集引用
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

using KingTop.IDAL.Template;
using KingTop.Common;
#endregion

#region 版权注释
/*----------------------------------------------------------------------------------------
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-6
// 功能描述：内容发布----------------------------------------------------------------------------------------*/
#endregion

namespace KingTop.SQLServer.Template
{
    public class Publish : IPublish
    {
        #region 获取自由标签
        /// <summary>
        /// 获取自由标签
        /// </summary>
        /// <returns></returns>
        public DataTable GetFreeLabelList()
        {
            string selSQL;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selSQL = "select LableName, LabelSQL,LableContent,PageSize  from K_T_LableFree";

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 获取自由标签数据源
        /// <summary>
        /// 获取自由标签数据源
        /// </summary>
        /// <param name="selSQL">选取语句</param>
        /// <returns></returns>
        public DataTable GetFreeLabelDataSource(string selSQL)
        {
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 获取自由标签数分页据源
        /// <summary>
        /// 获取自由标签数分页据源
        /// </summary>
        /// <param name="selSQL">数据集ＳＱＬ</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public DataTable GetFreeLabelSplitDataSource(string selSQL, int currentPage, int pageSize, string order)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            SqlParameter[] selParam;

            if (!string.IsNullOrEmpty(order))
            {
                selParam = new SqlParameter[] { new SqlParameter("@CurrentPageIndex", currentPage), new SqlParameter("@SelSql", selSQL), new SqlParameter("@PageSize", pageSize), new SqlParameter("@Order", order) };
            }
            else
            {
                selParam = new SqlParameter[] { new SqlParameter("@CurrentPageIndex", currentPage), new SqlParameter("@SelSql", selSQL), new SqlParameter("@PageSize", pageSize) };
            }

            dt = new DataTable();

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_GetSplitData", selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region  获取自由标签数分页记录总数
        /// <summary>
        /// 获取自由标签数分页记录总数
        /// </summary>
        /// <param name="selSQL">数据集ＳＱＬ</param>
        /// <returns>记录总数</returns>
        public int GetSplitCountRS(string selSQL)
        {
            int countRS;
            object result;

            countRS = 0;
            selSQL = Regex.Replace(selSQL, @"top\s*\d+", " ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            selSQL = "SELECT COUNT(*) FROM (" + selSQL + ")A;";

            try
            {
                result = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                countRS = int.Parse(result.ToString());
            }
            catch
            {
                countRS = 0;
            }

            return countRS;
        }
        #endregion

        #region 获取栏目记录
        /// <summary>
        /// 获取栏目记录
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <returns></returns>
        public DataTable GetMenuList(int siteID)
        {
            SqlParameter[] selParam;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@SiteID", siteID) };

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_MenuSel", selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 获取栏目记录
        /// <summary>
        /// 获取栏目记录(只有NodeCode ParentNode 及 NodeName
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <returns></returns>
        public DataTable GetSimpleMenuList(int siteID)
        {
            SqlParameter[] selParam;
            DataTable dt;
            SqlDataReader sqlReader;
            string selSQL;

            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@SiteID", siteID) };
            selSQL = "select NodeCode as ID,ParentNode as ParentID,NodeName as Name from K_SysModuleNode where IsWeb=1 and IsDel=0 and WebSiteID=@SiteID  order by ParentID asc";

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 获取标签列表
        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <param name="labelType">标签类型 0：静态标签；1：系统标签，2：分页标签</param>
        /// <returns></returns>
        public DataTable GetLabelList(int siteID, int labelType)
        {
            SqlParameter[] selParam;
            DataTable dt;
            string selSQL;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@SiteID", siteID), new SqlParameter("@IsSystem", labelType) };
            selSQL = "select LableName,LableContent from K_T_Lable where (IsSystem=@IsSystem or IsShare=1) and SiteID=@SiteID ";

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 获取栏目数据源
        /// <summary>
        /// 获取栏目数据源
        /// </summary>
        /// <param name="siteID">站点ID</param>
        /// <param name="nodeCode">栏目节点Code</param>
        /// <param name="tableName">表名</param>
        /// <param name="sqlWhere">条件</param>
        /// <returns></returns>
        public DataTable GetMenuDataSource(int siteID, string nodeCode, string tableName, string sqlWhere)
        {
            List<SqlParameter> lstSelParam;
            DataTable dt;
            SqlDataReader sqlReader;
            string selSQL;

            dt = new DataTable();
            lstSelParam = new List<SqlParameter>();

            lstSelParam.Add(new SqlParameter("@SiteID", siteID));

            if (!string.IsNullOrEmpty(nodeCode) && nodeCode.Trim() != "")
            {
                lstSelParam.Add(new SqlParameter("@NodeCode", nodeCode));
                selSQL = "select * from " + tableName + " where SiteID=@SiteID and NodeCode=@NodeCode and IsEnable=1 and IsDel=0 and FlowState=99 ";
            }
            else
            {
                selSQL = "select * from " + tableName + " where SiteID=@SiteID and IsEnable=1 and IsDel=0 and FlowState=99 ";
            }


            if (!string.IsNullOrEmpty(sqlWhere))
            {
                selSQL = selSQL + " and " + sqlWhere;
            }

            if (tableName.Trim() != "")
            {
                try
                {
                    sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, lstSelParam.ToArray());
                    dt.Load(sqlReader);
                }
                catch
                {
                    dt = null;
                }
            }
            else
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 读取单页内容
        /// <summary>
        /// 读取单页内容
        /// </summary>
        /// <param name="siteID">站点ID</param>
        /// <param name="nodeCode">所属栏目</param>
        /// <returns>单页内容</returns>
        public string GetSinglePageContent(int siteID, string nodeCode)
        {
            SqlParameter[] selParam;
            string selSQL;
            object temp;
            string content;

            content = string.Empty;
            selParam = new SqlParameter[] { new SqlParameter("@SiteID", siteID), new SqlParameter("@NodeCode", nodeCode) };
            selSQL = "select Content from K_SinglePage where SiteID=@SiteID and NodeCode=@NodeCode";

            try
            {
                temp = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);

                if (temp != null)
                {
                    content = temp.ToString();
                }
            }
            catch { }

            return content;
        }
        #endregion

        #region 列表类系统标签获取分页数据源
        public DataTable GetSysLabelListDS(int siteID, string tableName, string nodeCodeList, string sqlWhere, string sqlOrder, int pageSize, int currentPage)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            SqlParameter[] selParam;
            string selSQL;

            selSQL = "select * from " + tableName;

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                if (!string.IsNullOrEmpty(nodeCodeList) && nodeCodeList.Trim() != "")
                {
                    sqlWhere = sqlWhere + " and NodeCode IN('" + nodeCodeList.Replace(",", "','") + "')";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(nodeCodeList))
                {
                    sqlWhere = " NodeCode IN('" + nodeCodeList.Replace(",", "','") + "')";
                }
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                selSQL = selSQL + " where " + sqlWhere + " and FlowState=99 AND IsDel=0 ";
            }
            else
            {
                selSQL = selSQL + " where FlowState=99 and IsDel=0 ";
            }

            if (!string.IsNullOrEmpty(sqlOrder))
            {
                selParam = new SqlParameter[] { new SqlParameter("@CurrentPageIndex", currentPage), new SqlParameter("@SelSql", selSQL), new SqlParameter("@PageSize", pageSize), new SqlParameter("@Order", sqlOrder) };
            }
            else
            {
                selParam = new SqlParameter[] { new SqlParameter("@CurrentPageIndex", currentPage), new SqlParameter("@SelSql", selSQL), new SqlParameter("@PageSize", pageSize) };
            }

            dt = new DataTable();

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_GetSplitData", selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 列表类系统标签获取数据源
        public DataTable GetSysLabelListDS(int siteID, string tableName, string nodeCodeList, string sqlWhere, string sqlOrder, int pageSize)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            string selSQL;

            dt = new DataTable();

            if (pageSize > 0)
            {
                selSQL = "select top " + pageSize.ToString() + " * from " + tableName;
            }
            else
            {
                selSQL = "select * from " + tableName;
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                if (!string.IsNullOrEmpty(nodeCodeList))
                {
                    sqlWhere = sqlWhere + " and NodeCode IN('" + nodeCodeList.Replace(",", "','") + "')";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(nodeCodeList))
                {
                    sqlWhere = " NodeCode IN('" + nodeCodeList.Replace(",", "','") + "')";
                }
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                selSQL += " where " + sqlWhere + " and FlowState=99 and IsDel=0 ";
            }
            else
            {
                selSQL += " where FlowState=99 and IsDel=0 ";
            }

            if (!string.IsNullOrEmpty(sqlOrder))
            {
                selSQL += " order by " + sqlOrder;
            }
            else
            {
                selSQL += " order by orders desc";
            }

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 获取可操作的专题栏目
        /// <summary>
        ///  获取可操作的专题栏目
        /// </summary>
        /// <returns></returns>
        public DataTable GetSpecialMenu()
        {
            DataTable dt;
            SqlDataReader sqlReader;
            string selSQL;

            dt = new DataTable();

            selSQL = "SELECT SpecialID,ID,MetaDescription,MetaKeyword,Name,TemplatePath,DirectoryName FROM K_SpecialMenu where IsDel=0 order by Orders asc;";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);

            try
            {
                dt.Load(sqlReader);
            }
            catch { }

            return dt;
        }
        #endregion

        #region 获取可操作的专题
        /// <summary>
        ///  获取可操作的专题
        /// </summary>
        /// <returns></returns>
        public DataTable GetSpecial(int siteID)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            string selSQL;

            dt = new DataTable();

            selSQL = "select ID,Name,TemplatePath,DirectoryName,MetaDescription,MetaKeyword from K_Special where IsDel=0 and SiteID=" + siteID.ToString() + " order by Orders asc;";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);

            try
            {
                dt.Load(sqlReader);
            }
            catch { }

            return dt;
        }
        #endregion

        #region 获取专题/专题栏目数据源
        /// <summary>
        /// 获取专题/专题栏目数据源
        /// </summary>
        /// <param name="specialID">专题ID</param>
        /// <param name="specialMenuID">专题栏目ID</param>
        /// <returns></returns>
        public DataSet GetSpecialMenuDataSource(string specialID, string specialMenuID, int startPageIndex, int endPageIndex)
        {
            DataSet ds;
            List<SqlParameter> lstSelParam;

            lstSelParam = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(specialID) && specialID.Trim() != "")
            {
                lstSelParam.Add(new SqlParameter("@SpecialID", specialID));
            }

            if (!string.IsNullOrEmpty(specialMenuID) && specialMenuID.Trim() != "")
            {
                lstSelParam.Add(new SqlParameter("@SpecialMenuID", specialMenuID));
            }

            lstSelParam.Add(new SqlParameter("@StartPageIndex", startPageIndex));
            lstSelParam.Add(new SqlParameter("@EndPageIndex", endPageIndex));
            lstSelParam.Add(new SqlParameter("@FlowState", 99));
            lstSelParam.Add(new SqlParameter("@IsPublish", 1));

            try
            {
                ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_K_SpecialInfoSel", lstSelParam.ToArray());
            }
            catch
            {
                ds = null;
            }

            return ds;
        }
        #endregion

        #region 期刊标签数据源
        public DataSet GetPeriodicalDataSource(string periodicalID)
        {
            DataSet ds;
            string selSQL;

            selSQL = "select ID,Title from K_U_PeriodicalCatalog where PeriodicalID='" + periodicalID + "';select * from K_U_PeriodicalArticle where PeriodicalID='" + periodicalID + "' and PeriodicalCatalogID Is NOT NULL;";
            ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);

            return ds;
        }
        #endregion

        #region 获取类型
        public DataTable GetSubCategoryList(string parentID, bool isSibling)
        {
            SqlParameter[] selParam;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@ParentID", parentID), new SqlParameter("@IsSibling", isSibling) };

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_SubCategorySel", selParam);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 按分类查找商品记录
        /// <summary>
        /// 按分类查找商品记录
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <param name="catID">商品分类ID</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public DataSet GetGoodsByCatID(int siteID, string catID, string sqlWhere, int pageSize, int pageIndex)
        {
            List<SqlParameter> lstSelParam;
            DataSet ds;

           
            lstSelParam = new List<SqlParameter>();

            lstSelParam.Add(new SqlParameter("@SiteID", siteID));
            lstSelParam.Add(new SqlParameter("@CatID", catID));
            lstSelParam.Add(new SqlParameter("@CurrentPageIndex", pageIndex));
            lstSelParam.Add(new SqlParameter("@PageSize", pageSize));
            lstSelParam.Add(new SqlParameter("@TranType", "content"));

            if (!string.IsNullOrEmpty(sqlWhere) && sqlWhere.Trim() != "")
            {
                lstSelParam.Add(new SqlParameter("@SqlWhere", sqlWhere));
            }

            try
            {
                ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_GoodsSel", lstSelParam.ToArray());
            }
            catch
            {
                ds = null;
            }

            return ds;
        }
        #endregion

        #region 执行SQL语句
        /// <summary>
        ///  执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataTable ExecSQL(string sql)
        {
            string selSQL;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();

            selSQL = sql;

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;

        }
        #endregion
    }
}
