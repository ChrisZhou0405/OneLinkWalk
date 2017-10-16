using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;
namespace KingTop.IDAL.Content
{
    public interface IModelField
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);      

        string ModelFieldSet(string tranType, string setValue, string IDList);     

        void PageData(KingTop.Model.Pager pager);

        /// <summary>
        /// 添加/更新模板
        /// </summary>
        /// <param Name="strSql">sql语句</param>
        /// <param Name="sqlParms">sql参数</param>
        /// <returns>返回执行结果</returns>
        bool SaveOrUpdate(string strTable, Dictionary<string, string> dicWhere, bool isSave, string strID);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param Name="strSql">sql语句</param>
        /// <param Name="sqlParms">sql参数</param>
        /// <returns>返回执行结果</returns>
        DataSet GetTable(string strTable, string strId);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param Name="strSql">sql语句</param>       
        /// <returns>返回执行结果</returns>
        DataSet GetTable(string tableName, string txtColumn, string valueColumn, string sqlWhere);

        /// <summary>
        /// 查询表或者表中的数据
        /// </summary>              
        DataTable GetTableOrColumnSel(Dictionary<string, string> dtcWhere);

        DataSet GetTableOrColumn(Dictionary<string, string> dtcWhere);

        string[] SavaOrUpdateNext(bool isSaveFileSize, string strAction, Model.Content.ModelField modelFiled, string strColumnType, string strTableName);

          /// <summary>
        /// 获取当前字段列类型
        /// </summary>
        /// <param Name="strControls"></param>
        /// <param Name="strBasicField"></param>
        /// <returns></returns>
        string GetControlsColumnType(string strControls, string strBasicField, int iDataColumnLength);

    }
}
