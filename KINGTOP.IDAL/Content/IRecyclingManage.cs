using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

namespace KingTop.IDAL.Content
{
    public interface IRecyclingManage
    {
        DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.Content.RecyclingManage recModel);

        string RecyclingManageSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);

        void PageNewsData(KingTop.Model.Pager pager);

        void CreateView();

           /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="Id"></param>
        void Del(string Id);
    }
}
