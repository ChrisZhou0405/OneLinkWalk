using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;


namespace KingTop.IDAL.SysManage
{
    public interface IWebSiteTemplateNode
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.WebSiteTemplateNode TemplateNodeModel);

        string TemplateNodeSet(string tranType, string setValue, string IDList);
    }
}
