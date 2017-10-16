
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.Content;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武
// 创建日期：2010-03-12
// 功能描述：对K_ModelField表的的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.SQLServer.Content
{
    public class ModelField : KingTop.IDAL.Content.IModelField
    {
        #region 根据传入的参数查询K_ModelField,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ModelField,返回查询结果
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
                      CommandType.StoredProcedure, "proc_K_ModelFieldSel", prams).Tables[0];
        }
        #endregion

        #region 设置或者删除K_ModelField记录
        /// <summary>
        /// 设置或者删除K_ModelField记录
        /// </summary>
        /// <param Name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param Name="setValue">设置值</param>
        /// <param Name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModelFieldSet(string tranType, string setValue, string IDList)
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
                          CommandType.StoredProcedure, "proc_K_ModelFieldSet", prams);
                sRe = "1";
            }
            catch (Exception ex)
            {
                sRe = ex.Message;
            }

            return sRe;
        }
        #endregion


        #region 增、改K_ModelField表
        /// <summary>
        /// 增、改K_ModelField表
        /// </summary>
        /// <param Name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param Name="paramsModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(SqlTransaction sqlTran, string tranType, KingTop.Model.Content.ModelField paramsModel)
        {
            string isOk = "";
            try
            {
                SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
                returnValue.Direction = ParameterDirection.Output;

                string cmdText = "proc_K_ModelFieldSave";

                SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("tranType", tranType),
                    new SqlParameter("ID",paramsModel.ID),
                    new SqlParameter("ModelId",paramsModel.ModelId),
                    new SqlParameter("Name",paramsModel.Name),
                    new SqlParameter("ModelFieldGroupId",paramsModel.ModelFieldGroupId),
                    new SqlParameter("FieldAlias",paramsModel.FieldAlias),
                    new SqlParameter("Message",paramsModel.Message),
                    new SqlParameter("BasicField",paramsModel.BasicField),
                    new SqlParameter("EditorStyle",paramsModel.EditorStyle),
                    new SqlParameter("TextBoxMaxLength",paramsModel.TextBoxMaxLength),
                    new SqlParameter("TextBoxWidth",paramsModel.TextBoxWidth),
                    new SqlParameter("TextBoxHieght",paramsModel.TextBoxHieght),
                    new SqlParameter("TextBoxValidation",paramsModel.TextBoxValidation),
                    new SqlParameter("ValidationType",paramsModel.ValidationType),
                    new SqlParameter("ValidationMessage",paramsModel.ValidationMessage),
                    new SqlParameter("IsLink",paramsModel.IsLink),
                    new SqlParameter("IsFilter",paramsModel.IsFilter),
                    new SqlParameter("IsShield",paramsModel.IsShield),
                    new SqlParameter("EditorType",paramsModel.EditorType),
                    new SqlParameter("Controls",paramsModel.Controls),
                    new SqlParameter("OptionsValue",paramsModel.OptionsValue),
                    new SqlParameter("OptionCount",paramsModel.OptionCount),
                    new SqlParameter("IsFill",paramsModel.IsFill),
                    new SqlParameter("MinValue",paramsModel.MinValue),
                    new SqlParameter("MaxValue",paramsModel.MaxValue),
                    new SqlParameter("DefaultValue",paramsModel.DefaultValue),
                    new SqlParameter("DateDefaultOption",paramsModel.DateDefaultOption),
                    new SqlParameter("DateFormatOption",paramsModel.DateFormatOption),
                    new SqlParameter("IsUpload",paramsModel.IsUpload),
                    new SqlParameter("MaxSize",paramsModel.MaxSize),
                    new SqlParameter("ImageType",paramsModel.ImageType),
                    new SqlParameter("ImageNameRules",paramsModel.ImageNameRules),
                    new SqlParameter("ImageIsWatermark",paramsModel.ImageIsWatermark),
                    new SqlParameter("IsUploadThumbnail",paramsModel.IsUploadThumbnail),
                    new SqlParameter("IsSaveFileSize",paramsModel.IsSaveFileSize),
                    new SqlParameter("SaveFileName",paramsModel.SaveFileName),
                    new SqlParameter("UrlPrefix",paramsModel.UrlPrefix),
                    new SqlParameter("IsRequired",paramsModel.IsRequired),
                    new SqlParameter("IsInputValue",paramsModel.IsInputValue),
                    new SqlParameter("IsEnable",paramsModel.IsEnable),
                    new SqlParameter("Orders",paramsModel.Orders),
                    new SqlParameter("IsSearch",paramsModel.IsSearch),
                    new SqlParameter("SearchWidth",paramsModel.SearchWidth),
                    new SqlParameter("SearchOrders",paramsModel.SearchOrders),
                    new SqlParameter("ListWidth",paramsModel.ListWidth),
                    new SqlParameter("ListAlignment",paramsModel.ListAlignment),
                    new SqlParameter("ListOrders",paramsModel.ListOrders),
                    new SqlParameter("ListIsLink",paramsModel.ListIsLink),
                    new SqlParameter("ListLinkUrl",paramsModel.ListLinkUrl),
                    new SqlParameter("ListIsOrder",paramsModel.ListIsOrder),
                    new SqlParameter("ListOrderOption",paramsModel.ListOrderOption),
                    new SqlParameter("ListIsDefaultOrder",paramsModel.ListIsDefaultOrder),
                    new SqlParameter("ListDefaultOrderOption",paramsModel.ListDefaultOrderOption),
                    new SqlParameter("IsRss",paramsModel.IsRss),
                    new SqlParameter("UserGroupId",paramsModel.UserGroupId),
                    new SqlParameter("RoleGroupId",paramsModel.RoleGroupId),                   
                    new SqlParameter("UserNo",paramsModel.UserNo),
                    new SqlParameter("DropDownDataType",paramsModel.DropDownDataType),
                    new SqlParameter("DropDownTable",paramsModel.DropDownTable),
                    new SqlParameter("DropDownTextColumn",paramsModel.DropDownTextColumn),
                    new SqlParameter("DropDownValueColumn",paramsModel.DropDownValueColumn),
                    new SqlParameter("DropDownSql",paramsModel.DropDownSql),
                    new SqlParameter("IsListEnable",paramsModel.IsListEnable),
                    new SqlParameter("IsMultiFile",paramsModel.IsMultiFile),
                    new SqlParameter("NumberCount",paramsModel.NumberCount),
                    new SqlParameter("DropDownSqlWhere",paramsModel.DropDownSqlWhere),
                    new SqlParameter("searchUIType",paramsModel.SearchUIType),
                    new SqlParameter("DataColumnLength",paramsModel.DataColumnLength),
                     new SqlParameter("ModelFieldType",paramsModel.ModelFieldType),
                      new SqlParameter("IsListVisible",paramsModel.IsListVisible),
                      new SqlParameter("ThumbDisplayType",paramsModel.ThumbDisplayType),
                      new SqlParameter("ThumbHeight",paramsModel.ThumbHeight),
                      new SqlParameter("ThumbWidth",paramsModel.ThumbWidth),
                      new SqlParameter("ImageBestWidth",paramsModel.ImageBestWidth),
                      new SqlParameter("ImageBestHeight",paramsModel.ImageBestHeight),
                    returnValue
                };
                

                SQLHelper.ExecuteNonQuery(sqlTran, CommandType.StoredProcedure, cmdText, paras);
                isOk = returnValue.Value.ToString();
            }
            catch (Exception ex)
            {
                isOk = ex.Message;

            }

            return isOk;
        }

       /// <summary>
        /// 添加模型列
       /// </summary>
       /// <param Name="strTableName">表名</param>
       /// <param Name="strColumnName">列名</param>
       /// <returns></returns>
        public string AddModelColumn(SqlTransaction sqlTran, string strTableName, string strColumnName, string strColumnType, string strIsNull, string strDefaultValue)
        {
            SqlParameter[] paras = new SqlParameter[]{
                new SqlParameter("tableName",strTableName),
                new SqlParameter("columnName", strColumnName),
                new SqlParameter("columnType",strColumnType),
                new SqlParameter("columnIsNull",strIsNull),
                new SqlParameter("columnDefaultValue",strDefaultValue)
            };
            object objValue = SQLHelper.ExecuteScalar(sqlTran, CommandType.StoredProcedure, "proc_K_ModelAddColumn", paras);
            if (objValue == null)
            {
                return "";
            }
            return objValue.ToString();
        }

        public DataSet GetTableOrColumn(Dictionary<string, string> dtcWhere)
        {
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_TableOrColumnSel", Utils.DictToSqlParams(dtcWhere));
        }

        /// <summary>
        /// 添加/更新模板
        /// </summary>
        /// <param Name="strSql">sql语句</param>
        /// <param Name="sqlParms">sql参数</param>
        /// <returns>返回执行结果</returns>
        public bool SaveOrUpdate(string strTable, Dictionary<string,string> dicWhere,bool isSave,string strID)
        {
             StringBuilder sbSql = new StringBuilder(160);
             List<SqlParameter> listSqlParam = new List<SqlParameter>();
            if (isSave)
            {
                StringBuilder sbSqlValue = new StringBuilder(160);
                sbSql.Append("insert into " + strTable + " (");
                sbSqlValue.Append("values(");               
                foreach (KeyValuePair<string, string> kvp in dicWhere)
                {
                    sbSql.Append("["+kvp.Key + "],");
                    sbSqlValue.Append("@" + kvp.Key + ",");
                    listSqlParam.Add(new SqlParameter("@"+kvp.Key,kvp.Value));                   
                }
                sbSql.Remove(sbSql.Length - 1, 1); //删除最后一个字符,
                sbSql.Append(")");
                sbSqlValue.Remove(sbSqlValue.Length - 1, 1);
                sbSqlValue.Append(")");
                sbSql.Append(sbSqlValue);  //合成一个sql
            }
            else
            {
                sbSql.Append("update " + strTable + " set ");
                foreach (KeyValuePair<string, string> kvp in dicWhere)
                {
                    sbSql.Append("["+kvp.Key + "] = @"+kvp.Key+",");
                    listSqlParam.Add(new SqlParameter("@"+kvp.Key,kvp.Value));                   
                }
                sbSql.Remove(sbSql.Length - 1, 1); //删除最后一个字符,
                sbSql.Append(" where ID='" + strID + "'");
            }
            int i = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbSql.ToString(), listSqlParam.ToArray());
            if (i != -1)  //不等于-1 则执行成功
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param Name="strSql">sql语句</param>
        /// <param Name="sqlParms">sql参数</param>
        /// <returns>返回执行结果</returns>
        public DataSet GetTable(string strTable, string strId)
        {
            SqlParameter[] sqlParms = new SqlParameter[]{
                new SqlParameter("@ID",strId)
            };
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, "select * from " + strTable + " where ID=@ID", sqlParms);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param Name="strSql">sql语句</param>       
        /// <returns>返回执行结果</returns>
        public DataSet GetTable(string tableName, string txtColumn, string valueColumn, string sqlWhere)
        {
            string sql;          
            sql = "select " + txtColumn + "," + valueColumn + " from " + tableName;

            if (!string.IsNullOrEmpty(sqlWhere.Trim()))
            {
                sql = sql + " where " + sqlWhere;
            }
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);
        }
          

        /// <summary>
        /// 查询表或者表中的数据
        /// </summary>      
        public DataTable GetTableOrColumnSel(Dictionary<string,string> dctWhere)
        {
            return SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_TableOrColumnSel", Utils.DictToSqlParams(dctWhere)).Tables[0];
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
                new SqlParameter("@Table","K_ModelField"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*,(SELECT Name FROM K_ModelFieldGroup WHERE ID = ModelFieldGroupId) AS ModelName"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order",pager.Order),                
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
            StringBuilder sbSql = new StringBuilder(40);
            sbSql.Append(" 1=1 ");
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    Utils.GetWhereAppend(kvp.Key, kvp.Value, "=", KingTop.Model.SqlParamType.Str, ref sbSql);
                }
            }

            return sbSql.ToString();
        }

        /// <summary>
        /// 模型操作
        /// </summary>
        /// <param Name="isSaveFileSize"></param>
        /// <param Name="strAction"></param>
        /// <param Name="modelFiled"></param>
        /// <param Name="page"></param>
        /// <param Name="strColumnType"></param>
        /// <param Name="strTableName"></param>
        /// <param Name="strPageParams"></param>
        public string[] SavaOrUpdateNext(bool isSaveFileSize, string strAction, Model.Content.ModelField modelFiled, string strColumnType, string strTableName)
        {
            //事务开启(以后优化)
            SqlConnection sqlConn = new SqlConnection(KingTop.Common.SQLHelper.ConnectionStringLocalTransaction);
            sqlConn.Open();
            SqlTransaction sqlTran = sqlConn.BeginTransaction();
            string[] strMessage = new string[2];
            if (isSaveFileSize && strAction != "EDIT" && modelFiled.SaveFileName == "")
            {
                strMessage[0] = "0";
                strMessage[1] = Utils.GetResourcesValue("Model", "AddFiledErrorMessage");               
            }           
            string strSaveMessage = Save(sqlTran, strAction, modelFiled);
            if (strSaveMessage == "1") //如果数据插入成功,则要更新模型表
            {
                if (strAction != "EDIT")  //如果操作不为更新, 则还要在相应的模板表中插入字段
                {               
                    string strIsNull = ""; //是否必填 
                    if (modelFiled.IsRequired)  //如果选择必填,则插入不为空
                    {
                        strIsNull = "";
                    }
                    string strDefaultValue = ""; //默认值
                    if (modelFiled.DefaultValue != "")
                    {
                        if (Public.IsNumber(modelFiled.DefaultValue))
                        {
                            strDefaultValue = "default(" + modelFiled.DefaultValue + ")";
                        }
                        else
                        {
                            strDefaultValue = " default('"+ modelFiled.DefaultValue +"')";
                        }
                    }

                    string strAddMessage = AddModelColumn(sqlTran, strTableName, Utils.GetFilterKeyword(modelFiled.Name), strColumnType, strIsNull, strDefaultValue);  //新加列
                    if (strAddMessage == "1")
                    {
                        if (isSaveFileSize)  //如果保存文件大小字段,则还要插入在表中插入一个字段
                        {
                            strAddMessage = AddModelColumn(sqlTran, strTableName, Utils.GetFilterKeyword(modelFiled.SaveFileName), "int", "", "");  //新加列
                            if (strAddMessage == "1")
                            {
                                sqlTran.Commit(); //事务提交       
                                 strMessage[0] = "1";
                                strMessage[1] =Utils.GetResourcesValue("Model", "AddSucess");                                   
                            }
                            else
                            {
                                sqlTran.Rollback();  //事务回滚     
                                strMessage[0] = "0";
                                strMessage[1] = Utils.GetResourcesValue("Model", "AddDataBaseMessage");                               
                            }
                        }
                        else
                        {
                           sqlTran.Commit();//事务提交   
                           strMessage[0] = "1";
                           strMessage[1] =  Utils.GetResourcesValue("Model", "AddSucess"); 
                        }
                        //  bllModelField.CommitTran();
                    }
                    else
                    {
                        sqlTran.Rollback();//事务回滚       
                         strMessage[0] = "0";
                           strMessage[1] =   Utils.GetResourcesValue("Model", "AddDataBaseFiledMessage");
                       
                    }
                }
                else
                {
                    sqlTran.Commit();//事务提交       
                     strMessage[0] = "1";
                           strMessage[1] =   Utils.GetResourcesValue("Model", "AddSucess");                    
                }
            }
            else
            {
                sqlTran.Rollback(); //事务回滚         
              
                if (strAction != "EDIT")
                {
                    strMessage[0] = "0";
                    strMessage[1] = Utils.GetResourcesValue("Model", "AddFiledMessage");         
                 
                }
                else
                {
                    strMessage[0] = "0";
                    strMessage[1] = Utils.GetResourcesValue("Model", "UpdateError");                   
                }
            }
            sqlConn.Close();
            sqlTran.Dispose();
            sqlConn.Dispose();
            return strMessage;
        }

        /// <summary>
        /// 获取当前字段列类型
        /// </summary>
        /// <param Name="strControls"></param>
        /// <param Name="strBasicField"></param>
        /// <returns></returns>
        public string GetControlsColumnType(string strControls, string strBasicField, int iDataColumnLength)
        {
            string strValue = null;
            string strColumnLength = null;
            if (iDataColumnLength >= 4000)
            {
                strColumnLength = "nvarchar(max)";
            }
            else
            {
                strColumnLength = "nvarchar(" + iDataColumnLength.ToString() + ")";
            }
            if (strControls != "")  //系统字段
            {
                strValue = "nvarchar(50)";
            }
            else
            {
                switch (strBasicField)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "11":
                    case "12":
                    case "7":
                        strValue = strColumnLength;
                        break;
                    case "8":
                        strValue = "int";
                        break;
                    case "9":
                        strValue = "float";
                        break;
                    case "10":
                        strValue = "datetime";
                        break;
                    default:
                        strValue = "nvarchar(50)";
                        break;
                }
            }
            return strValue;
        }
      
        #endregion
    }
}
