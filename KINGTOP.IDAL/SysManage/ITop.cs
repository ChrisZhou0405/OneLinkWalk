using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;


namespace KingTop.IDAL.SysManage
{
    public interface ITop
    {
        DataTable GetMessage(int count,string siteID,string userName,string userid);
        List<string> GetDataBaseInfo();
        DataTable GetPendingAudi(KingTop.Model.SelectParams modelPrams);

    }
}
