using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace KingTop.IDAL.Content
{
    public interface IControlManageList
    {
        string LinkSearch(string tableName, string[] sqlWhereField, string[] sqlWhereFieldValue, string[] sqlWhereFieldOpera, string[] sqlWhereFieldJoin);
        string FlowStateSearch(string tableName, string flowState);
        string LinkEdit(string tableName, string id, string[] sqlField, string[] sqlFieldValue, string[] sqlWhereField, string[] sqlWhereFieldValue, string[] sqlWhereFieldOpera, string[] sqlWhereFieldJoin);
        string LinkDelete(string tableName, string id);
        string LinkDelete(string tableName, string colName, string colValue, string oper);

        Hashtable GetHashTable(string tableName, string txtColumn, string valueColumn, string sqlWhere);
        void PageData(KingTop.Model.Pager pager, string tableName, string sqlField, string sqlWhere, string sqlOrder ,string sqlFieldForignData);
        string GetFieldValue(string tableName,string fieldName, string id);
        void SearchForignTable(ref StringBuilder sbSearch, string paramValue, string keyValue, string[] arrParam, string[] arrField);
        void SearchRange(ref StringBuilder sbSearch, string fieldName, string keyValue, string paramValue);
        void SearchCompare(ref StringBuilder sbSearch, string fieldName, string paramValue, string keyValue);
        void SearchBasic(ref StringBuilder sbSearch, string fieldName, string keyValue, string type);
        DataSet GetEnabledFlowStep(int accountID, int siteID,string nodeCode, string stepID);
        DataTable GetFlowState();
        string GetNodeReviewFlow(int siteID, string nodeCode);
        void GetKeepUrlParam(ref string sqlWhere, string tableName, string keepParamList, string filterUrlParam, System.Collections.Specialized.NameValueCollection queryStringParam);
        void AlterFlowState(string tableName, string infoID, int infoCount, bool state,string addMan,string ip,string nodeCode,int siteID,int accountID);
    }
}
