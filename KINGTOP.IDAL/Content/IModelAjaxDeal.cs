using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;

namespace KingTop.IDAL.Content
{
    public interface IModelAjaxDeal
    {
        object CheckFieldValue(string tableName, string fieldName, string fieldValue);
        void SetOrder(string tableName, string id, string orderValue);
        DataTable GetRecommendAuthor();
        DataTable GetRecommendSource();
        DataTable GetTableFieldByID(string tableID);
        void EditFileModelGroupID(string fieldID, string fieldGroupID);
        string GetModelName(string modelID);
        DataTable GetLabelVarValue(string labelVarID);
        bool CheckModelRepeat(string tbName);
        bool AddSubModelGroup(string groupName,string id);
        DataTable GetSubModelList(string subModelGroupID);
        string GetModelID(string tableName);
        DataTable GetSubModelRs(string tableName, string parentID);
        DataTable GetOriginalRelatedRS(string tableName, string sqlWhere);
        DataTable GetSourcelRelatedRS(string tableName, string serachValue, string brandID,string catelogryID, bool isHasPrice);
        DataTable GetGoodsBrand(string tableName);
        void AlbumsContentSync(string tableName, string fieldName, string id, string newValue);
        object GetShopSetConfig(string siteID);
        DataTable GetMemberGroup(string siteID);
        DataTable Search( string classID,string dataRate,string reach,string brand,string title);
    }
}
