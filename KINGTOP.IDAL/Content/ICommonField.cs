using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using KingTop.Model;
namespace KingTop.IDAL.Content
{
    public interface ICommonField
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);
        string ModelFieldSet(string tranType, string setValue, string IDList);
        DataSet GetTableAndField(string tableName);
        string Save(Hashtable hsFieldParam,string modelFieldID);
        void PageData(KingTop.Model.Pager pager, Hashtable hsWhereEqual, Hashtable hsWhereLike,string sort);
    }
}
