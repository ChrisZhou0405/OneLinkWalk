using System;
using System.Collections.Generic;
using System.Text;

namespace KingTop.Model.SysManage
{
    public class TableInfo
    {
        private string _tableName;
        private string _tableDescription;
        private string _tableType;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        /// <summary>
        /// 表描述
        /// </summary>
        public string TableDescription
        {
            get { return _tableDescription; }
            set { _tableDescription = value; }
        }
        /// <summary>
        /// 表类别
        /// </summary>
        public string TableType
        {
            get { return _tableType; }
            set { _tableType = value; }
        }
    }
}
