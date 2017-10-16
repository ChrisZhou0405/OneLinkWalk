using System;

namespace KingTop.Modules.Entity
{
	public class HRJobCate
	{
		/// <summary>
		/// 类别编码 例如：100001002
		/// </summary>
		private string m_ID;
		public string ID
		{
			get { return m_ID; }
			set { m_ID = value; }
		}
		/// <summary>
		/// 父编码，一级编码为0
		/// </summary>
		private string m_ParentID;
		public string ParentID
		{
			get { return m_ParentID; }
			set { m_ParentID = value; }
		}
		/// <summary>
		/// 名称
		/// </summary>
		private string m_Title;
		public string Title
		{
			get { return m_Title; }
			set { m_Title = value; }
		}
		/// <summary>
		/// 描述
		/// </summary>
		private string m_Intro;
		public string Intro
		{
			get { return m_Intro; }
			set { m_Intro = value; }
		}
		/// <summary>
		/// 添加日期
		/// </summary>
		private DateTime? m_AddDate;
		public DateTime? AddDate
		{
			get { return m_AddDate; }
			set { m_AddDate = value; }
		}
		/// <summary>
		/// 审核状态
		/// </summary>
		private int? m_FlowState;
		public int? FlowState
		{
			get { return m_FlowState; }
			set { m_FlowState = value; }
		}

        /// <summary>
        /// 是否删除
        /// </summary>
        private int? m_IsDel;
        public int? IsDel
        {
            get { return m_IsDel; }
            set { m_IsDel = value; }
        }

		/// <summary>
		/// 删除时间
		/// </summary>
		private DateTime? m_DelTime;
		public DateTime? DelTime
		{
			get { return m_DelTime; }
			set { m_DelTime = value; }
		}
		/// <summary>
		/// 站点ID
		/// </summary>
		private int? m_SiteID;
		public int? SiteID
		{
			get { return m_SiteID; }
			set { m_SiteID = value; }
		}
		/// <summary>
		/// 栏目编码
		/// </summary>
		private string m_NodeCode;
		public string NodeCode
		{
			get { return m_NodeCode; }
			set { m_NodeCode = value; }
		}

        /// <summary>
        /// 类型（A=职位类型，B=招聘单位，C=工作地点）
        /// </summary>
        private string m_CateType;
        public string CateType
        {
            get { return m_CateType; }
            set { m_CateType = value; }
        }

        /// <summary>
        /// 类型（A=职位类型，B=招聘单位，C=工作地点）
        /// </summary>
        private int m_Orders;
        public int Orders
        {
            get { return m_Orders; }
            set { m_Orders = value; }
        }
	}
}