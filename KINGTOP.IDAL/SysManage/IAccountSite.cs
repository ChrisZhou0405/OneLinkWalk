using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IAccountSite
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.AccountSite accModel);

        string AccountSiteSet(string tranType, string setValue, string IDList);
    }
}
