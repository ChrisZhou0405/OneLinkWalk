using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      陈顺
    创建时间： 2010年3月31日
    功能描述： 模块
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.IDAL.SysManage
{
    public interface IModule
    {
        DataTable GetList(string tranType, SelectParams paramsModel);

        string Save(string tranType, KingTop.Model.SysManage.Module modModel);

        string ModuleSet(string tranType, string setValue, string IDList);

        void PageData(KingTop.Model.Pager pager);
    }
}
