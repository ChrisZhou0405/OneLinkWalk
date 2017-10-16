
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL;
using System.Xml;
using KingTop.Common;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：周武
// 创建日期：2010-03-12
// 功能描述：对K_ModelField表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL.Content
{
    public class ModelField
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];


        private IDAL.Content.IModelField dal = (IDAL.Content.IModelField)Assembly.Load(path).CreateInstance(path + ".Content.ModelField");

        #region 根据传入的参数查询K_ModelField,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_ModelField,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        { 
            return dal.GetList(tranType, paramsModel);
        }
        #endregion
       

        #region 设置或者删除K_ModelField记录
        /// <summary>
        /// 设置或者删除K_ModelField记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string ModelFieldSet(string tranType, string setValue, string IDList)
        {
            return dal.ModelFieldSet(tranType, setValue, IDList);
        }

        
        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            dal.PageData(pager);
        }
        #endregion

        /// <summary>
        /// 获取当前字段类型
        /// </summary>
        /// <param name="strControls"></param>
        /// <param name="strBasicField"></param>
        /// <returns></returns>
        public string GetControlsName(string strControls, string strBasicField)
        {
            string strValue = null;
            if (strControls != "")
            {
                XmlNode xmlnode = Utils.XmlSelectSingleNode("../Configuraion/model/FieldControls.xml","//FieldControl[value="+strControls+"]");
                strValue = xmlnode.FirstChild.InnerText;
            }
            else
            {
                switch (strBasicField)
                {
                    case "1":
                        strValue = "单行文本";
                        break;
                    case "2":
                        strValue = "多行文本（不支持HTML）";
                        break;
                    case "3":
                        strValue = "多行文本（支持HTML）";
                        break;
                    case "4":
                        strValue = "单选";
                        break;
                    case "5":
                        strValue = "多选";
                        break;
                    case "6":
                        strValue = "下拉列表";
                        break;
                    case "7":
                        strValue = "列表（可选择多列）";
                        break;
                    case "8":
                        strValue = "数字";
                        break;
                    case "9":
                        strValue = "货币";
                        break;
                    case "10":
                        strValue = "日期时间";
                        break;
                    case "11":
                        strValue = "图片";
                        break;
                    case "12":
                        strValue = "文件";
                        break;

                }
            }
            return strValue;
        }

        /// <summary>
        /// 获取当前字段值
        /// </summary>
        /// <param name="strControls"></param>
        /// <param name="strBasicField"></param>
        /// <returns></returns>
        public string GetControlsValue(string strControls, string strBasicField,string strColumnName,bool isSystemFiled)
        {
            string strValue = null;
            if (isSystemFiled) //是否自定义字段
            {
                strValue = Utils.ReqFromParameter(strColumnName);
            }
            else
            {
                if (strControls != "")  //系统字段暂时不处理
                {
                    strValue = "";
                }
                else
                {
                    switch (strBasicField)
                    {
                        case "1":
                        case "2":
                        case "12":
                            strValue = Utils.ReqFromParameter(strColumnName);
                            break;
                        case "10": //时间
                            string strDateFormat = Utils.ReqFromParameter("hiddatetimes");
                            switch (strDateFormat)
                            {
                                case "1":
                                    strValue = Utils.GetStandardDateTime(Utils.ReqFromParameter(strColumnName), "yyyy-MM-dd");
                                    break;
                                case "2":
                                    strValue = Utils.GetStandardDateTime(Utils.ReqFromParameter(strColumnName), "hh:mm:ss");
                                    break;
                                case "3":
                                    strValue = Utils.GetStandardDateTime(Utils.ReqFromParameter(strColumnName), "yyyy-MM-dd hh:mm:ss");
                                    break;
                                default:
                                    strValue = Utils.ReqFromParameter(strColumnName);
                                    break;
                            }
                            break;
                        case "3":
                            string strEditType = Utils.ReqFromParameter("hid" + strColumnName);  //编辑器类型
                            switch (strEditType)
                            {
                                case "1": //fck
                                    strValue = Utils.ReqFromParameter(strColumnName, 0);
                                    break;
                                default:
                                    strValue = Utils.ReqFromParameter(strColumnName);
                                    break;
                            }
                            break;
                        default:
                            strValue = Utils.ReqFromParameter(strColumnName);
                            break;
                    }
                }
            }
            return strValue;
        }

        /// <summary>
        /// 获取当前字段列类型
        /// </summary>
        /// <param name="strControls"></param>
        /// <param name="strBasicField"></param>
        /// <returns></returns>
        public string GetControlsColumnType(string strControls, string strBasicField, int iDataColumnLength)
        {
           return dal.GetControlsColumnType(strControls, strBasicField, iDataColumnLength);
        }

        /// <summary>
        /// 添加/更新模板
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="sqlParms">sql参数</param>
        /// <returns>返回执行结果</returns>
        public bool SaveOrUpdate(string strTable, Dictionary<string, string> dicWhere, bool isSave, string strID)
        {
            return dal.SaveOrUpdate(strTable, dicWhere, isSave, strID);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="sqlParms">sql参数</param>
        /// <returns>返回执行结果</returns>
        public DataSet GetTable(string strTable,string strId)
        {
            return dal.GetTable(strTable, strId);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="tableName">引用表名</param>
        /// <param name="txtColumn">显示文本引用列</param>
        /// <param name="valueColumn">值引用列</param>
        /// <param name="sqlWhere">引用条件</param>   
        /// <returns>返回执行结果</returns>
        public DataSet GetTable(string tableName, string txtColumn, string valueColumn, string sqlWhere)
        {
            return dal.GetTable(tableName,txtColumn,valueColumn,sqlWhere);
        }

        public DataSet GetTableOrColumn(Dictionary<string, string> dtcWhere)
        {
            return dal.GetTableOrColumn(dtcWhere);
        }

        public DataTable GetTableOrColumnSel(Dictionary<string, string> dtcWhere)
        {
            return dal.GetTableOrColumnSel(dtcWhere);
        }

        /// <summary>
        /// 保存或者修改下一步
        /// </summary>
        /// <param name="isSaveFileSize"></param>
        /// <param name="strAction"></param>
        /// <param name="modelFiled"></param>
        /// <param name="page"></param>
        /// <param name="strTableName"></param>
        /// <param name="strPageParams"></param>
        public string[] SavaOrUpdateNext(bool isSaveFileSize, string strAction, Model.Content.ModelField modelFiled, string strTableName)
        {          
            string strColumnType = GetControlsColumnType(modelFiled.Controls, modelFiled.BasicField,modelFiled.DataColumnLength); //获取字段类型
            return dal.SavaOrUpdateNext(isSaveFileSize, strAction, modelFiled,strColumnType, strTableName);
        }
        #endregion
    }
}
