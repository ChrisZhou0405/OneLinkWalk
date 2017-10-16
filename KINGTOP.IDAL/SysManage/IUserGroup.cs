using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IUserGroup
    {
        DataSet GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.UserGroup userGroupModel);

        string UserGroupSet(string tranType, string setValue, string IDList);
    }
}
