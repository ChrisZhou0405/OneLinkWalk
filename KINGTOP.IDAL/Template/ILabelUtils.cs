using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.Template
{
    public interface ILabelUtils
    {
        DataTable GetList(string tranType, SelectParams paramsModel);
        DataTable TP_GetList(string tranType, KingTop.Model.SelectParams paramsModel);
        DataTable TS_GetList(string tranType, KingTop.Model.SelectParams paramsModel);

    }
}
