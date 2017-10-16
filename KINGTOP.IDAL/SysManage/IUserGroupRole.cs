using System;
using System.Collections.Generic;
using System.Text;
using KingTop.Model;
using System.Data;

namespace KingTop.IDAL.SysManage
{
    public interface IUserGroupRole
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        //DataTable GetList(string tranType, KingTop.Model.SysManage.UserGroupRole userGroupRoleModel);

        string Save(string tranType, KingTop.Model.SysManage.UserGroupRole userGroupRoleModel);

        DataSet GetUserGroupRole(KingTop.Model.Pager pager,int SiteID);

        string UserGroupRoleSet(string tranType, string setValue, string IDList);
    }
}
