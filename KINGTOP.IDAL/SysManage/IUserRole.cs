using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IUserRole
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        DataTable GetList(string tranType, KingTop.Model.SysManage.UserRole userRoleModel);

        string Save(string tranType, KingTop.Model.SysManage.UserRole userRoleModel);

        string UserRoleSet(string tranType, string setValue, string IDList);
    }
}
