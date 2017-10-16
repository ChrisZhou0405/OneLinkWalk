using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace KingTop.IDAL.Content
{
    public interface IControlManageEdit
    {
        Hashtable GetHashTable(string tableName, string txtColumn, string valueColumn, string sqlWhere);
        DataTable GetDataTable(string tableName, string txtColumn, string valueColumn, string sqlWhere, string sqlOrder);
        Hashtable GetHashTableByID(string tableName, string id);
        string Add(string tableName, string id, int orders, Hashtable hsEditField, Dictionary<string, string> dicSubModel);
        string Update(string tableName, string id, Hashtable hsEditField,Dictionary<string,string> dicSubModel);
        void AppendToSpecial(string[] specialID, string[] specialMenuID,string specialInfoID);
        DataTable LoadSpecial(string specialInfoID);
        int EditSpecialInfo(string[] delSpecialID, string[] delSpecialMenuID, string[] addSpecialID, string[] addSpecialMenuID, string specialInfoID);
        DataTable GetNodeInfoByNodeCode(string nodeCode);
        int AppendToNode(string tableName,string siteID,string nodeID,string id,string orders,int nodeCount,string infoID);
        string GetNodeReviewFlow(int siteID, string nodeCode);
    }
}
