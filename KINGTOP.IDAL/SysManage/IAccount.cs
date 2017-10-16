using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IAccount
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.Account accModel);

        string AccountSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
        void PageData(KingTop.Model.Pager pager, int StrNumber);
    }
}
