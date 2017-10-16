using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     陈顺
    创建时间： 2010年7月14日
    功能描述： 数据库管理操作类
    ===============================================================*/
#endregion

namespace KingTop.IDAL.SysManage
{
    public interface IDataBaseManage
    {
        void PageData(KingTop.Model.Pager pager);
        DataTable GetUserTableInfo();
        bool ExecSql(string sql);
        DataTable GetTableExecSql(string sql);
    }
}
