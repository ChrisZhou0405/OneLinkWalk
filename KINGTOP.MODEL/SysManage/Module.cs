using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:      陈顺
    创建时间： 2010年3月31日
    功能描述： 来源编辑
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.Model.SysManage
{
    public class Module
    {
        #region 私有变量成员

        private Guid _ModuleID;                  // 模块ID,主键
        private string _ModuleCode;              //模块编码
        private string _ModuleName;              // 模块名称
        private string _LinkURL;                 // 链接地址
        private bool _IsValid;                   // 是否有效
        private string _ModuleOrder;             // 排序号
        private string _ModuleDesc;              // 模块说明
        private string _ModuleEncDesc;           // 模块英文说明
        private bool _IsSystem;                  // 是否系统模块
        private int _ModuleType;                 //模块类型 1:节点模块,2:栏目模块
        private string _TableName;              //模块使用的数据表
        #endregion

        #region 构造函数
        public Module()
        { }
        #endregion

        #region 属性
        /// <summary>
        /// 模块ID,主键
        /// </summary>
        public virtual Guid ModuleID
        {
            set { this._ModuleID = value; }
            get { return this._ModuleID; }
        }
        /// <summary>
        /// 模块编码
        /// </summary>
        public virtual string ModuleCode
        {
            set { this._ModuleCode = value; }
            get { return this._ModuleCode; }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public virtual string ModuleName
        {
            set { this._ModuleName = value; }
            get { return this._ModuleName; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public virtual string LinkURL
        {
            set { this._LinkURL = value; }
            get { return this._LinkURL; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual bool IsValid
        {
            set { this._IsValid = value; }
            get { return this._IsValid; }
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public virtual string ModuleOrder
        {
            set { this._ModuleOrder = value; }
            get { return this._ModuleOrder; }
        }
        /// <summary>
        /// 模块说明
        /// </summary>
        public virtual string ModuleDesc
        {
            set { this._ModuleDesc = value; }
            get { return this._ModuleDesc; }
        }
        /// <summary>
        /// 模块英文说明
        /// </summary>
        public virtual string ModuleEncDesc
        {
            set { this._ModuleEncDesc = value; }
            get { return this._ModuleEncDesc; }
        }
        /// <summary>
        /// 是否系统模块
        /// </summary>
        public virtual bool IsSystem
        {
            set { this._IsSystem = value; }
            get { return this._IsSystem; }
        }

        /// <summary>
        /// 模块类型
        /// </summary>
        public virtual int ModuleType
        {
            set { this._ModuleType = value; }
            get { return this._ModuleType; }
        }

        /// <summary>
        /// 数据表
        /// </summary>
        public virtual string TableName
        {
            set { this._TableName = value; }
            get { return this._TableName; }
        }

        #endregion
    }
}
