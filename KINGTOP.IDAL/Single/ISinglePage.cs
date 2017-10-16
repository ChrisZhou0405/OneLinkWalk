using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.Single
{
    public interface ISinglePage
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.Single.SinglePage sinModel);

        string SinglePageSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
    }
}
