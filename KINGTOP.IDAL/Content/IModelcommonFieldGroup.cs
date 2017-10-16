using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.Content
{
    public interface IModelcommonFieldGroup
    {
        string Save(string tranType, KingTop.Model.Content.ModelcommonFieldGroup modModel);
        string ModelcommonFieldGroupSet(string tranType, string setValue, string IDList);
        void PageData(KingTop.Model.Pager pager);
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);
    }
}
