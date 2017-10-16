using System;
using System.Collections.Generic;
using System.Text;

namespace KingTop.Model.Content
{
    /// <summary>
    /// 链接配置配置（list.xml)
    /// </summary>
    public class LinkList
    {
        private string _name;
        private string _alias;
        private string _dataType;
        private string _defaultValue;
        private string _value;
        private bool _isNull;
        private bool _isRadio;
        private int charlength;

        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name
        {
            set { this._name = value; }
            get { return this._name; }
        }

        /// <summary>
        /// 字段别名
        /// </summary>
        public string Alias
        {
            set { this._alias = value; }
            get { return this._alias; }
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType
        {
            set { this._dataType = value; }
            get { return this._dataType; }
        }

        /// <summary>
        /// 字段缺省值
        /// </summary>
        public string DefaultValue
        {
            set { this._defaultValue = value; }
            get { return this._defaultValue; }
        }

        /// <summary>
        /// 单选按钮或多选按钮显示时的键值对
        /// </summary>
        public string Value
        {
            set { this._value = value; }
            get { return this._value; }
        }

        /// <summary>
        /// 字段是否允许为空
        /// </summary>
        public bool IsNull
        {
            set { this._isNull = value; }
            get { return this._isNull; }
        }

        /// <summary>
        /// 是否是单选按钮
        /// </summary>
        public bool IsRadio
        {
            set { this._isRadio = value; }
            get { return this._isRadio; }
        }

        /// <summary>
        /// 字符串类型字符个数
        /// </summary>
        public int CharLength
        {
            set { this.charlength = value; }
            get { return this.charlength; }
        }
    }
}
