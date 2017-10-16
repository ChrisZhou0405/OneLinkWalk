using System;
using System.Collections.Generic;
using KingTop.Model;
using System.Text;

namespace KingTop.IDAL.SysManage
{
    public interface IUserGropManage
    {
        void PageData(KingTop.Model.Pager pager);
        void PageData(KingTop.Model.Pager pager,int strNumber);
    }
}
