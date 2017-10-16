using System;
using System.Collections.Generic;
using System.Text;

namespace KingTop.Model.Content
{
    /// <summary>
    /// 系统及预定义配置节点模型
    /// </summary>
    public class SysField
    {
        private string _id;
        private string _title;
        private string[] _name;
        private string[] _alias;
        private string[] _dataType;
        private int?[] _typeLength;
        private string[] _defaultValue;
        private bool _isSearch;
        private bool _isList;
        private bool _isEdit;
        private bool[] _isNull;

        /// <summary>
        /// 节点ID，唯一
        /// </summary>
        public string ID
        {
            set { this._id = value; }
            get { return this._id; }
        }

        /// <summary>
        /// 节点在模型编辑页显示标题
        /// </summary>
        public string Title
        {
            set { this._title = value; }
            get { return this._title; }
        }

        /// <summary>
        /// 字段名称，如有多个字段用|隔开
        /// </summary>
        public string[] Name
        {
            set { this._name = value; }
            get { return this._name; }
        }

        /// <summary>
        /// 字段别名，个数如字段名称对应用|隔开
        /// </summary>
        public string[] Alias
        {
            set { this._alias = value; }
            get { return this._alias; }
        }

        /// <summary>
        /// 字段数据类型，个数与字段名称相同用|隔开
        /// </summary>
        public string[] DataType
        {
            set { this._dataType = value; }
            get { return this._dataType; }
        }

        /// <summary>
        /// 类型长度，个数与字段名称相同用|隔开
        /// </summary>
        public int?[] TypeLength
        {
            set { this._typeLength = value; }
            get { return this._typeLength; }
        }

        /// <summary>
        /// 字段缺省值，个数与字段名称相同用|隔开
        /// </summary>
        public string[] DefaultValue
        {
            set { this._defaultValue = value; }
            get { return this._defaultValue; }
        }

        /// <summary>
        /// 是否显示在搜索栏中
        /// </summary>
        public bool IsSearch
        {
            set { this._isSearch = value; }
            get { return this._isSearch; }
        }

        /// <summary>
        /// 是否在列表页显示
        /// </summary>
        public bool IsList
        {
            set { this._isList = value; }
            get { return this._isList; }
        }

        /// <summary>
        /// 是否在编辑页显示
        /// </summary>
        public bool IsEdit
        {
            set { this._isEdit = value; }
            get { return this._isEdit; }
        }

        /// <summary>
        /// 字段是否为空
        /// </summary>
        public bool[] IsNull
        {
            set { this._isNull = value; }
            get { return this._isNull; }
        }
    }
}
