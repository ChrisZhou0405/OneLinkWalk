using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using KingTop.Model;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      陈顺
    创建时间： 2010年4月6日
    功能描述： 公共操作
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.IDAL.SysManage
{
    public interface IPublicOper
    {
        DataTable GetList(string tranType, SelectParams paramsModel);
    }
}
