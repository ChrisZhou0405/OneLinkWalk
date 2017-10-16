using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.Content
{
    public interface IReviewFlowLog
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.Content.ReviewFlowLog revModel);

        string ReviewFlowLogSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
    }
}
