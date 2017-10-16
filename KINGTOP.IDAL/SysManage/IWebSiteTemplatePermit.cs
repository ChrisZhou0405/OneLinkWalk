using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.SysManage
{
    public interface IWebSiteTemplatePermit
    {
        DataTable GetList(string tranType, SelectParams paramsModel);
    }
}
