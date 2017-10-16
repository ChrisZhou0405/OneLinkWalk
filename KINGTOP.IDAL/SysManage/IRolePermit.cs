using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IRolePermit
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.RolePermit RolePermitModel);

        string RolePermitSet(string tranType, string setValue, string IDList, string ExtendValue);
    }
}
