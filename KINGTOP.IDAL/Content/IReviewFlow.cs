using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.Content
{
    public interface IReviewFlow
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.Content.ReviewFlow revModel);

        string ReviewFlowSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);

        void PageData(KingTop.Model.Pager pager, string strModelId, string strFlowId,string strStateValue);

        string GetFlowStateId(string strTableName, string strNewsId);

        DataTable GetdtFlowState(string strFlowStateId);

        string FlowStateUpdate(string strTable, string strNewsId, string strReviewState);
    }
}
