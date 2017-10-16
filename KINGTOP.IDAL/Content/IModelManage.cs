using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model.Content;

namespace KingTop.IDAL.Content
{
    public interface IModelManage
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);
        DataTable GetCommonField();
        string SetCommonField(string modelID, string commonFieldID, string fieldID, int orders, int tranType);
        string Save(string tranType, ModelManage paramsModel, string ddlSQL, string dmlSQL,string operTitle,string  operName,int operCount);

        string ModelManageSet(string tranType, string setValue, string IDList);
        string DropTableFieldPackaing(string tableName, string fieldName);
        string DeleteModelFieldRowPackaing(string modelID, string fieldName);
        string AddTableFieldPackaing(string tableName, LinkList link);
        string InsertModelFieldRowPackaing(LinkList link, string id, string modelID, int orders);

        string DropTableFieldPackaing(string tableName, string[] arrFieldName);
        string DeleteModelFieldRowPackaing(string modelID, string[] arrFieldName);
        string AddTableFieldPackaing(string tableName, SysField mSysField);
        string InsertModelFieldRowPackaing(SysField mSysField, string[] id, string modelID, int[] orders);

        string InsertInitField(string[] id, int[] orders, string modelID,bool isSub);
        string CreateTable(string tableName,bool isSub);
        void PageData(KingTop.Model.Pager pager);

        bool PublicOperSynchronization(string operName, string operTitle, int operCount);
        DataTable GetSubModelGroupList();
    }
}
