using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL
{
    public interface ITemplateProject
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.TemplateProject temModel);

        string TemplateProjectSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
    }
}
