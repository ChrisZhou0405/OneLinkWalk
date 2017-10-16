using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using KingTop.Model;
namespace KingTop.IDAL.Content
{
    public interface IFieldManage
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);
        string ModelFieldSet(string tranType, string setValue, string IDList);
        DataSet GetTableAndField(string tableName);
        DataTable GetModelFieldGroup(string modelID);
        int Save(Hashtable hsFieldParam, string tableName, string modelFieldID, string originalDefaultValue, string originalDataColumnLength);
        void PageData(KingTop.Model.Pager pager, Hashtable hsWhereEqual, Hashtable hsWhereLike,string sort);
        DataTable GetSpecialFieldAtrr(string configType, string fieldID);
        int EditSpecialFieldAtrr(Hashtable hsFieldValue);
        DataTable GetSubModelGroupList();
        DataTable GetSubModelList();
    }
}
