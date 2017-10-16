using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IRole
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.Role roleModel);

        string RoleSet(string tranType, string setValue, string IDList);
        
        void PageData(KingTop.Model.Pager pager);
        
    }
}
