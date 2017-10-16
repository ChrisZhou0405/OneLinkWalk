#region 程序集引用
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标// 创建日期：2010-09-06
// 功能描述：内容发布
// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.IDAL.Template
{
    public interface IPublish
    {
        DataTable GetFreeLabelList();
        DataTable GetFreeLabelDataSource(string selSQL);
        /// <summary>
        /// 获取栏目记录
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <returns></returns>
        DataTable GetMenuList(int siteID);
                /// <summary>
        /// 获取栏目记录(只有NodeCode ParentNode 及 NodeName
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <returns></returns>
        DataTable GetSimpleMenuList(int siteID);
        /// <summary>
        /// 按分类查找商品记录
        /// </summary>
        /// <param name="siteID">当前站点ID</param>
        /// <param name="catID">商品分类ID</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        DataSet GetGoodsByCatID(int siteID, string catID, string sqlWhere, int pageSize, int pageIndex);
        /// <summary>
        ///  执行SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        DataTable ExecSQL(string sql);
        DataTable GetLabelList(int siteID,int labelType);
        int GetSplitCountRS(string selSQL);
        DataTable GetFreeLabelSplitDataSource(string selSQL, int currentPage, int pageSize, string order);
        DataTable GetMenuDataSource(int siteID, string nodeCode, string tableName, string sqlWhere);
        string GetSinglePageContent(int siteID, string nodeCode);
        DataTable GetSysLabelListDS(int siteID, string tableName, string nodeCodeList, string sqlWhere, string sqlOrder, int pageSize, int currentPage);
        DataTable GetSysLabelListDS(int siteID, string tableName, string nodeCodeList, string sqlWhere, string sqlOrder,int pageSize);
        DataTable GetSpecialMenu();
        DataTable GetSpecial(int siteID);
        DataSet GetSpecialMenuDataSource(string specialID, string specialMenuID, int startPageIndex, int endPageIndex);
        DataSet GetPeriodicalDataSource(string periodicalID);
        DataTable GetSubCategoryList(string parentID, bool isSibling);
    }
}
