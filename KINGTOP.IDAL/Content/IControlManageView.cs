using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace KingTop.IDAL.Content
{
    public interface IControlManageView
    {
        DataTable LoadSpecial(string specialInfoID);
        Hashtable GetHashTableByID(string tableName, string id);
        string GetFieldValue(string tableName, string fieldName, string id);
    }
}
