using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IUser
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.User userModel);

        string UserSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);

        void PageData(KingTop.Model.Pager pager, int StrNumber);
    }
}
