using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface ISysWebSite
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.SysWebSite SysWebSiteModel);

        string SysWebSiteSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
    }
}
