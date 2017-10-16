#region 程序集引用
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Xml.XPath;

using KingTop.IDAL;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-4-1
// 功能描述：处理SystemField.asmx文件,处理系统自定义字段

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public class ModelAjaxDeal
    {
        #region 变量成员
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Content.IModelAjaxDeal dal = (IDAL.Content.IModelAjaxDeal)Assembly.Load(path).CreateInstance(path + ".Content.ModelAjaxDeal");
        #endregion

        #region 检查字段对应值
        public bool CheckTitleRepeat(string tableName, string fieldName, string fieldValue)
        {
            object afterFieldValue;
            bool returnValue;

            afterFieldValue = dal.CheckFieldValue(tableName, fieldName, fieldValue);
            if (afterFieldValue == null || string.IsNullOrEmpty(afterFieldValue.ToString()))
            {
                returnValue = false;
            }
            else
            {
                returnValue = true;
            }

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
            dal.SetOrder(tableName, id, orderValue);
        }
        #endregion

        #region  获取推荐的作者
        public string[] GetRecommendAuthor()
        {
            DataTable dt;
            string[] authorName;

            dt = dal.GetRecommendAuthor();
            authorName = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                authorName = new string[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    authorName[i] = dt.Rows[i][0].ToString();
                }

            }

            return authorName;
        }
        #endregion

        #region  获取推荐的来源
        public string[] GetRecommendSource()
        {
            DataTable dt;
            string[] sourceName;

            dt = dal.GetRecommendSource();
            sourceName = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                sourceName = new string[dt.Rows.Count];

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sourceName[i] = dt.Rows[i][0].ToString();
                }

            }

            return sourceName;
        }
        #endregion

        #region 通过表名ID获取表字段
        public string[] GetTableFieldByID(string tableID)
        {
            DataTable dtField;      // 表的所有字段

            List<string> field;     // 保存表的所有字段名

            field = new List<string>();
            dtField = dal.GetTableFieldByID(tableID);

            if (dtField != null && dtField.Rows.Count > 0)
            {
                foreach (DataRow dr in dtField.Rows)
                {
                    field.Add(dr["Name"].ToString());
                }
            }

            if (field.Count > 0)
            {
                return field.ToArray();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 模型字段列表中更改所属字段分组
        public void EditFileModelGroupID(string fieldID, string fieldGroupID)
        {
            dal.EditFileModelGroupID(fieldID, fieldGroupID);
        }
        #endregion

        #region 获取模型名称
        /// <summary>
        /// 获取特定模型表字段值
        /// </summary>
        /// <param name="modelID">模型主键</param>
        /// <returns></returns>
        public string GetModelName(string modelID)
        {
            string result;

            result = string.Empty;

            if (!string.IsNullOrEmpty(modelID))
            {
                result = dal.GetModelName(modelID);
            }

            return result;
        }
        #endregion

        #region 通过标签变量ID获取标签变量值
        public StringBuilder GetLabelVarValue(string labelVarID)
        {
            DataTable dtLabelVarValue;              // 变量标签值
            StringBuilder sbLabelVarValueHtml;      // 值显示HTML代码

            sbLabelVarValueHtml = new StringBuilder();
            dtLabelVarValue = dal.GetLabelVarValue(labelVarID);

            if (dtLabelVarValue != null)
            {
                foreach (DataRow dr in dtLabelVarValue.Rows)
                {
                    sbLabelVarValueHtml.Append(dr["VarValue"].ToString() + "<br/>");
                }
            }

            return sbLabelVarValueHtml;
        }
        #endregion

        #region 检测模型是否重名
        public bool CheckModelRepeat(string tbName)
        {
            bool result;

            result = dal.CheckModelRepeat(tbName);

            return result;
        }
        #endregion

        #region 添加子模型分组
        public string AddSubModelGroup(string groupName)
        {
            string id;
            bool isTrue;

            id = Guid.NewGuid().ToString();
            groupName = Common.Utils.cutBadStr(groupName);
            isTrue = dal.AddSubModelGroup(groupName, id);

            if (!isTrue)
            {
                id = string.Empty;
            }

            return id;
        }
        #endregion

        #region 子模型列表
        public DataTable GetSubModelList(string subModelGroupID)
        {
            return dal.GetSubModelList(subModelGroupID);
        }
        #endregion

        #region 获取子模型字段
        public string GetSubModelField(string tableName)
        {
            string modelID;
            string fieldHtmlCode;
            ParseModel parser;

            fieldHtmlCode = string.Empty;
            modelID = dal.GetModelID(tableName);

            if (modelID != null && modelID.Trim() != "")
            {
                parser = new ParseModel(modelID, ParserType.SubModel);
                fieldHtmlCode = parser.ParseEdit("IsInputValue=1");
                fieldHtmlCode = Regex.Replace(fieldHtmlCode, @"\s+name\s*=\s*[""'](?<1>[^""']+)[""']", " name=\"" + tableName + "___${1}\" ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                fieldHtmlCode = Regex.Replace(fieldHtmlCode, @"\s+id\s*=\s*[""'](?<1>[^""']+)[""']", " id=\"" + tableName + "___${1}\" ", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            }

            return fieldHtmlCode;
        }
        #endregion

        #region 加载子模型记录
        public string GetSubModelRs(string tableName, string parentID)
        {
            DataTable dt;
            StringBuilder sbJsonRS;

            sbJsonRS = new StringBuilder();

            sbJsonRS.Append("[");
            dt = dal.GetSubModelRs(tableName, parentID);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (sbJsonRS.Length > 5)
                    {
                        sbJsonRS.Append(",{\"fieldName\":\"" + col.ColumnName + "\",\"fieldValue\":\"" + dt.Rows[0][col.ColumnName].ToString() + "\"}");
                    }
                    else
                    {
                        sbJsonRS.Append("{\"fieldName\":\"" + col.ColumnName + "\",\"fieldValue\":\"" + dt.Rows[0][col.ColumnName].ToString() + "\"}");
                    }
                }
            }

            sbJsonRS.Append("]");
            return sbJsonRS.ToString();
        }
        #endregion

        #region 加载模型编辑页记录关联
        public DataTable GetOriginalRelatedRS(int relateType, string originalValue)
        {
            Dictionary<string, string> dicRelated;
            string[] arrRelated;
            string[] temp;
            DataTable dtRS;
            string tableName;
            string sqlWhere;

            dicRelated = new Dictionary<string, string>();

            arrRelated = originalValue.Split(new char[] { ',' });
            tableName = string.Empty;
            sqlWhere = string.Empty;

            switch (relateType)
            {
                case 0:
                    tableName = "K_U_Article";
                    sqlWhere = "'" + originalValue.Replace(",", "','") + "'";
                    break;
                case 1:         // 商品
                case 2:         // 配件
                    foreach (string rs in arrRelated)
                    {
                        temp = rs.Split(new char[] { '$' });

                        if (temp.Length > 1)
                        {
                            dicRelated.Add(temp[0], temp[1]);

                            if (string.IsNullOrEmpty(sqlWhere))
                            {
                                sqlWhere = "'" + temp[0] + "'";
                            }
                            else
                            {
                                sqlWhere += ",'" + temp[0] + "'";
                            }
                        }
                    }
                    tableName = "K_G_Goods";
                    break;
            }

            dtRS = dal.GetOriginalRelatedRS(tableName, sqlWhere);
            dtRS.TableName = "Related";

            if (dtRS != null)
            {
                if (tableName != "K_U_Article")
                {
                    dtRS.Columns.Add(new DataColumn("id"));
                    dtRS.Columns.Add(new DataColumn("title"));
                }

                foreach (DataRow dr in dtRS.Rows)
                {
                    switch (relateType)
                    {
                        case 1:         // 商品
                            if (dicRelated[dr["RSID"].ToString()] == "0")  // 单向关联
                            {
                                dr["id"] = dr["RSID"].ToString() + "$0";
                                dr["title"] = dr["RSTitle"].ToString() + " --  [单向关联]";
                            }

                            if (dicRelated[dr["RSID"].ToString()] == "1")  // 双向关联
                            {
                                dr["id"] = dr["RSID"].ToString() + "$1";
                                dr["title"] = dr["RSTitle"].ToString() + " --  [双向关联]";
                            }

                            break;
                        case 2:         // 配件
                            dr["id"] = dr["RSID"].ToString() + "$" + dicRelated[dr["RSID"].ToString()];
                            dr["title"] = dr["RSTitle"].ToString() + " --  [" + dicRelated[dr["RSID"].ToString()] + "]";
                            break;
                    }
                }

                if (tableName != "K_U_Article")
                {
                    dtRS.Columns.Remove("RSID");
                    dtRS.Columns.Remove("RSTitle");
                }
            }

            return dtRS;

        }
        #endregion

        #region 关联记录 -- 搜索记录
        public DataTable GetSourcelRelatedRS(string serachValue, string brandID, string catelogryID, int relateType)
        {
            DataTable dt;
            string tableName;

            dt = null;
            tableName = string.Empty;

            switch (relateType)
            {
                case 0:  // 文章
                    tableName = "K_U_Article";
                    break;
                case 1: // 商品
                case 2: // 配件
                    tableName = "K_G_Goods";
                    break;
            }

            if (relateType == 2)
            {
                dt = dal.GetSourcelRelatedRS(tableName, serachValue, brandID, catelogryID, true);
            }
            else if (relateType == 1 || relateType == 0)
            {
                dt = dal.GetSourcelRelatedRS(tableName, serachValue, brandID, catelogryID, false);
            }

            dt.TableName = "Related";
            return dt;
        }
        #endregion

        #region 加载品牌
        public DataTable GetGoodsBrand(string tableName)
        {
            DataTable dt;

            dt = dal.GetGoodsBrand(tableName);
            dt.TableName = "Related";

            return dt;
        }
        #endregion

        #region 相册图片同步
        public void AlbumsContentSync(string tableName, string fieldName, string newValue, string id, string imgPath)
        {
            imgPath = imgPath.Replace("/", "\\");

            if (!imgPath.Contains(":"))
            {
                imgPath = HttpContext.Current.Server.MapPath(imgPath);
            }

            if (id != null && id.Trim() != "")
            {
                dal.AlbumsContentSync(tableName, fieldName, id, newValue);
            }

            File.Delete(imgPath);
            File.Delete(imgPath + ".gif");
        }
        #endregion

        #region 获取商城配置
        public string GetShopSetConfig(string siteID)
        {
            object siteDir;
            StringBuilder jsonConfig;
            string configPath;
            XPathDocument xpathDoc;
            XPathNavigator xpathNav;

            jsonConfig = new StringBuilder();
            jsonConfig.Append("({");

            if (!string.IsNullOrEmpty(siteID))
            {
                siteDir = dal.GetShopSetConfig(siteID);

                if (siteDir != null)
                {
                    configPath = siteDir.ToString();

                    if (configPath.Substring(0, 1) != "/")
                    {
                        configPath = "/" + configPath;
                    }

                    if (configPath.Substring(configPath.Length - 2, 1) != "/")
                    {
                        configPath = configPath + "/";
                    }

                    configPath += "config/ShopSet.config";
                    configPath = HttpContext.Current.Server.MapPath(configPath);

                    if (File.Exists(configPath))
                    {
                        xpathDoc = new XPathDocument(configPath);
                        xpathNav = xpathDoc.CreateNavigator();
                        xpathNav = xpathNav.SelectSingleNode("/ShopConfig/MarketRatio");

                        if (xpathNav != null)
                        {
                            jsonConfig.Append("MarketRatio:'" + xpathNav.Value + "'");
                        }

                        xpathNav = xpathNav.SelectSingleNode("/ShopConfig/IntegralPayRatio");

                        if (xpathNav != null)
                        {
                            if (jsonConfig.Length > 2)
                            {
                                jsonConfig.Append(",IntegralPayRatio:'" + xpathNav.Value + "'");
                            }
                            else
                            {
                                jsonConfig.Append("IntegralPayRatio:'" + xpathNav.Value + "'");
                            }
                        }
                    }
                }
            }

            jsonConfig.Append("})");
            return jsonConfig.ToString();
        }
        #endregion

        #region 获取会员等级
        public DataTable GetMemberGroup(string siteID)
        {
            DataTable dt;
            dt = dal.GetMemberGroup(siteID);
            dt.TableName = "MenberGroup";
            return dt;
        }
        #endregion

        #region 搜索
        public DataTable Search()
        {
            string classID;
            string dataRate;
            string reach;
            string brand;
            string title;
            DataTable dt;

            classID = string.Empty;
            dataRate = string.Empty;
            reach = string.Empty;
            brand = string.Empty;
            title = string.Empty;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["brand"]))
            {
                brand = HttpContext.Current.Request.QueryString["brand"];
            }

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["rate"]))
            {
                dataRate = HttpContext.Current.Request.QueryString["rate"];
            }

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["reach"]))
            {
                reach = HttpContext.Current.Request.QueryString["reach"];
            }

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["classid"]))
            {
                classID = HttpContext.Current.Request.QueryString["classid"];
            }

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["title"]))
            {
                title = HttpContext.Current.Request.QueryString["title"].Trim();
            }

            dt = dal.Search(classID, dataRate, reach, brand,title);
            return dt;
        }
        #endregion
    }
}
