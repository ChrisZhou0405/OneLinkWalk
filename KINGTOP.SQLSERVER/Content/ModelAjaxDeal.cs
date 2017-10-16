
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
// 创建日期：2010-4-1
// 功能描述：DAL处理SystemField.asmx文件的数据事务

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion

namespace KingTop.SQLServer.Content
{
    public class ModelAjaxDeal : IModelAjaxDeal
    {
        #region 检查字段对应值

        public object CheckFieldValue(string tableName, string fieldName, string fieldValue)
        {
            string selSQL;
            object returnValue;
            SqlParameter[] selParam;

            selParam = new SqlParameter[] { new SqlParameter("@FieldValue", fieldValue) };
            selSQL = "select " + fieldName + " from  " + tableName + "  where " + fieldName + "=@FieldValue";
            returnValue = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);

            return returnValue;
        }
        #endregion

        #region 更新模型列表中的排序
        /// <summary>
        /// 更新模型列表中的排序
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="id">主键值</param>
        /// <param name="orderValue">排序值</param>
        public void SetOrder(string tableName, string id, string orderValue)
        {
            string eidtSQL;
            SqlParameter[] editParam;

            eidtSQL = "update " + tableName + " set Orders=@orderValue where ID=@ID";
            editParam = new SqlParameter[] { 
                new SqlParameter("@orderValue",orderValue),
                new SqlParameter("@ID",id),

            };

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, eidtSQL, editParam);
        }
        #endregion

        #region  获取推荐的作者

        public DataTable GetRecommendAuthor()
        {
            string selSQL;
            SqlDataReader sqlReader;
            DataTable dt;

            dt = new DataTable();
            selSQL = "select Name from K_Author where IsRecommend=1 and IsEnable=1";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
            dt.Load(sqlReader);

            return dt;
        }
        #endregion

        #region  获取推荐的来源

        public DataTable GetRecommendSource()
        {
            string selSQL;
            SqlDataReader sqlReader;
            DataTable dt;

            dt = new DataTable();
            selSQL = "select Name from K_Source where IsRecommend=1 and IsEnable=1";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
            dt.Load(sqlReader);

            return dt;
        }
        #endregion

        #region 通过表名ID获取数据库中表字段

        public DataTable GetTableFieldByID(string tableID)
        {
            string selSQL;
            SqlParameter[] selParam;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selSQL = "SELECT [name] FROM sys.columns WHERE object_id=@TableID order by [name] asc";
            selParam = new SqlParameter[] { new SqlParameter("@TableID", tableID) };
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);

            try
            {
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 模型字段列表中更改所属字段分组

        public void EditFileModelGroupID(string fieldID, string fieldGroupID)
        {
            string editSQL;
            SqlParameter[] editParam;

            editParam = new SqlParameter[] { new SqlParameter("@FieldGroupID", fieldGroupID), new SqlParameter("@FieldID", fieldID) };
            editSQL = "update K_ModelField set ModelFieldGroupId=@FieldGroupID where ID=@FieldID";

            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, editSQL, editParam);
        }
        #endregion

        #region 获取模型名称
        /// <summary>
        ///获取模型名称
        /// </summary>
        /// <param name="modelID">模型主键</param>
        /// <returns></returns>
        public string GetModelName(string modelID)
        {
            string selSQL;
            SqlParameter[] selParam;
            object result;

            selParam = new SqlParameter[] { new SqlParameter("@ID", modelID) };
            selSQL = "select Title from K_ModelManage where ID=@ID";

            try
            {
                result = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
            }
            catch
            {
                result = null;
            }

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return string.Empty;
            }

        }
        #endregion

        #region 通过标签变量ID获取标签变量值
        /// <summary>
        /// 通过标签变量ID获取标签变量值
        /// </summary>
        /// <param name="labelVarID">标签变量ID</param>
        /// <returns></returns>
        public DataTable GetLabelVarValue(string labelVarID)
        {
            string selSQL;
            DataTable dt;
            SqlParameter[] selParm;
            SqlDataReader sqlReader;

            selSQL = "select VarValue from K_CollectionLabelVarValue where CollectionLabelVarID=@LabelVarID";
            selParm = new SqlParameter[] { new SqlParameter("@LabelVarID", labelVarID) };
            dt = new DataTable();

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParm);
                dt.Load(sqlReader);
            }
            catch
            {
                dt = null;
            }

            return dt;
        }
        #endregion

        #region 检测模型是否重名
        public bool CheckModelRepeat(string tbName)
        {
            string selSQL;
            SqlParameter[] selParam;
            object result;

            selParam = new SqlParameter[] { new SqlParameter("@TBName", tbName) };
            selSQL = "select ID from K_ModelManage where  LOWER(SUBSTRING(TableName,5,LEN(TableName)-4)) = Lower(@TBName)";
            result = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);

            if (result != null && result.ToString().Trim() != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 添加子模型分组
        public bool AddSubModelGroup(string groupName, string id)
        {
            bool isTrue;
            string editSQL;
            int effectRow;
            SqlParameter[] editParam;

            editParam = new SqlParameter[] { new SqlParameter("@ID", id), new SqlParameter("@Name", groupName) };
            editSQL = "INSERT INTO K_SubModelGroup([ID],[Name],[Orders],[IsEnable],[IsDel])VALUES(@ID,@Name,0,1,0)";

            try
            {
                effectRow = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, editSQL, editParam);
            }
            catch
            {
                effectRow = 0;
            }

            if (effectRow > 0)
            {
                isTrue = true;
            }
            else
            {
                isTrue = false;
            }

            return isTrue;
        }
        #endregion

        #region 子模型列表
        public DataTable GetSubModelList(string subModelGroupID)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            SqlParameter[] selParam;
            string selSQL;

            selParam = new SqlParameter[] { new SqlParameter("@GroupID", subModelGroupID) };
            dt = new DataTable();

            selSQL = "select TableName,Title from K_ModelManage where SubModelGroupID=@GroupID order by Orders desc";

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

        #region 通过模型表名获取模型ID
        public string GetModelID(string tableName)
        {
            object result;
            string modelID;
            SqlParameter[] selParam;
            string selSQL;

            selParam = new SqlParameter[] { new SqlParameter("@TableName", tableName) };

            selSQL = "select ID from K_ModelManage where TableName=@TableName;";
            modelID = string.Empty;

            try
            {
                result = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
            }
            catch
            {
                result = null;
            }

            if (result != null && result.ToString().Trim() != "")
            {
                modelID = result.ToString();
            }

            return modelID;
        }
        #endregion

        #region 加载字模型记录
        public DataTable GetSubModelRs(string tableName, string parentID)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            SqlParameter[] selParam;
            string selSQL;

            selParam = new SqlParameter[] { new SqlParameter("@ParentID", parentID) };
            dt = new DataTable();

            selSQL = "select * from " + tableName + " where ParentID=@ParentID";

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

        #region 加载关联记录
        public DataTable GetOriginalRelatedRS(string tableName, string sqlWhere)
        {
            string selSQL;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();

            if (tableName != "K_U_Article")
            {
                selSQL = "select id as RSID,title as RSTitle from " + tableName + " where id in(" + sqlWhere + ")";
            }
            else
            {
                selSQL = "select id,title from " + tableName + " where id in(" + sqlWhere + ")";
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

        #region 关联记录 -- 搜索记录
        public DataTable GetSourcelRelatedRS(string tableName, string serachValue, string brandID, string catelogryID, bool isHasPrice)
        {
            string selSQL;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();

            if (!string.IsNullOrEmpty(brandID) && brandID != "")
            {
                selSQL = " Select ID,Title,Price from " + tableName + " where title like '%" + serachValue + "%' and BrandID='" + brandID + "'";

                if (!string.IsNullOrEmpty(catelogryID) && catelogryID.Trim() != "")
                {
                    selSQL += " and CategoryID='" + catelogryID + "'";
                }
            }
            else
            {
                if (isHasPrice)
                {
                    selSQL = " Select ID,Title,Price from " + tableName + " where title like '%" + serachValue + "%'";
                }
                else
                {
                    selSQL = " Select ID,Title from " + tableName + " where title like '%" + serachValue + "%'";
                }

                if (!string.IsNullOrEmpty(catelogryID) && catelogryID.Trim() != "")
                {
                    selSQL += " and CategoryID='" + catelogryID + "'";
                }
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

        #region 加载品牌
        public DataTable GetGoodsBrand(string tableName)
        {
            string selSQL;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();

            if (tableName == "K_Category")
            {
                selSQL = "select ID,Name as Title from K_Category where ClassID='s'";
            }
            else
            {
                selSQL = "select ID,Title from " + tableName + ";";
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

        #region 相册图片同步
        public void AlbumsContentSync(string tableName, string fieldName, string id, string newValue)
        {
            string editSQL;
            SqlParameter[] editParam;

            editParam = new SqlParameter[] { new SqlParameter("@NewValue", newValue), new SqlParameter("@ID", id) };
            editSQL = "update " + tableName + " set " + fieldName + "=@NewValue where id=@ID";

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, editSQL, editParam);
            }
            catch { }
        }
        #endregion

        #region 获取站点目录
        public object GetShopSetConfig(string siteID)
        {
            object result;
            string selSQL;
            SqlParameter[] selParam;

            selParam = new SqlParameter[] { new SqlParameter("@SiteID", siteID) };
            selSQL = "select Directory from K_SysWebSite where SiteID=@SiteID";
            result = SQLHelper.ExecuteScalar(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);

            return result;
        }
        #endregion

        #region 获取会员等级
        public DataTable GetMemberGroup(string siteID)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            string selSQL;
            SqlParameter[] selParam;

            dt = new DataTable();
            selParam = new SqlParameter[] { new SqlParameter("@SiteID", siteID) };

            selSQL = "select ID,GroupName from K_MemberGroup where SiteID=@SiteID";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, selParam);
            dt.Load(sqlReader);
            return dt;
        }
        #endregion

        #region 搜索
        public DataTable Search(string classID, string dataRate, string reach, string brand,string title)
        {
            DataTable dt;
            SqlDataReader sqlReader;
            string selSQL;
            string sqlWhere;
            List<SqlParameter> lstSelParam;

            lstSelParam = new List<SqlParameter>();
            dt = new DataTable();

            selSQL = "select title,data_rate,reach,wavelength,ld,pd,pout,sensitivity,link_budget,operating_case_temperature from   K_U_Transceiver  ";
            sqlWhere = string.Empty;

            if (!string.IsNullOrEmpty(classID))
            {
                sqlWhere = " NodeCode=@NodeCode";
                lstSelParam.Add(new SqlParameter("@NodeCode", classID));
            }

            if (!string.IsNullOrEmpty(dataRate))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere += " and ";
                }

                sqlWhere = " Data_Rate=@Data_Rate ";
                lstSelParam.Add(new SqlParameter("@Data_Rate", dataRate));
            }

            if (!string.IsNullOrEmpty(reach))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere += " and ";
                }

                sqlWhere = " Reach=@Reach ";
                lstSelParam.Add(new SqlParameter("@Reach", dataRate));
            }

            if (!string.IsNullOrEmpty(brand))
            {
                if (!string.IsNullOrEmpty(sqlWhere))
                {
                    sqlWhere += " and ";
                }

                sqlWhere = " Brand=@Brand ";
                lstSelParam.Add(new SqlParameter("@Brand", brand));
            }

            if (!string.IsNullOrEmpty(title))
            {
                sqlWhere = " title like '%" + title + "%'";
            }

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                selSQL = selSQL + " where " + sqlWhere;

                try
                {
                    sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, lstSelParam.ToArray());
                    dt.Load(sqlReader);
                }
                catch { dt = null; }
            }
            else
            {
                dt = null;
            }

            return dt;
        }
        #endregion
    }
}
