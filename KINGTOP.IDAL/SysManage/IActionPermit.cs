using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IActionPermit
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);
        DataTable GetModuleRightList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.ActionPermit actModel);

        string ActionPermitSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
        void PageData(KingTop.Model.Pager pager, int StrNumber,int SiteID);
    }
}
