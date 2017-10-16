using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;
#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年3月29日
    功能描述： 用户组管理接口
    ===============================================================*/
#endregion

namespace KingTop.IDAL.SysManage
{   

    public interface IUserGroupPermit
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.UserGroupPermit ugpModel);

        string UserGroupPermitSet(string tranType, string setValue, string IDList, string ExtendValue);
    }
}
