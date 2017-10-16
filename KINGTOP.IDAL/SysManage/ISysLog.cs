using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface ISysLog
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel, Dictionary<string, string> dictWhere);
        string Save(string tranType, KingTop.Model.SysManage.SysLog manModel);
        void PageData(KingTop.Model.Pager pager);
    }
}
