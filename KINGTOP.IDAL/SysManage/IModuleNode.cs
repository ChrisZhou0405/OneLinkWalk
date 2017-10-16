using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IModuleNode
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.ModuleNode modModel);

        string ModuleNodeSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);

        string MenuMove(string fromMenu, string toMenu);

        string MenuCopy(string fromMenu, string toMenu, bool isDataCopy);
        string MenuUnite(string fromMenu, string toMenu);
        
    }
}
