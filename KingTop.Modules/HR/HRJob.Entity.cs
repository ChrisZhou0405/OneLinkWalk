using System;

namespace KingTop.Modules.Entity
{
	public class HRJob
	{
        /// <summary>
        /// 职位名称
        /// </summary>
        private int m_ID;
        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }

		/// <summary>
		/// 职位名称
		/// </summary>
		private string m_Title;
		public string Title
		{
			get { return m_Title; }
			set { m_Title = value; }
		}
		/// <summary>
		/// 职位类型
		/// </summary>
		private string m_JobType;
		public string JobType
		{
			get { return m_JobType; }
			set { m_JobType = value; }
		}
		/// <summary>
		/// 工作单位(公司)
		/// </summary>
		private string m_WorkUnit;
		public string WorkUnit
		{
			get { return m_WorkUnit; }
			set { m_WorkUnit = value; }
		}
		/// <summary>
		/// 工作地点
		/// </summary>
		private string m_WorkPlace;
		public string WorkPlace
		{
			get { return m_WorkPlace; }
			set { m_WorkPlace = value; }
		}
		/// <summary>
		/// 薪水待遇(0和空表示面议）
		/// </summary>
		private string m_Salary;
		public string Salary
		{
			get { return m_Salary; }
			set { m_Salary = value; }
		}
		/// <summary>
		/// 学历要求
		/// </summary>
        private string m_Degree;
        public string Degree
		{
            get { return m_Degree; }
            set { m_Degree = value; }
		}
		/// <summary>
		/// 年龄要求
		/// </summary>
		private int? m_Age;
		public int? Age
		{
			get { return m_Age; }
			set { m_Age = value; }
		}
		/// <summary>
		/// 经验要求(0和空表示不限）
		/// </summary>
		private string m_Experience;
		public string Experience
		{
			get { return m_Experience; }
			set { m_Experience = value; }
		}
		/// <summary>
		/// 招聘人数（0和空表示不限）
		/// </summary>
		private string m_Number;
		public string Number
		{
			get { return m_Number; }
			set { m_Number = value; }
		}
		/// <summary>
		/// 接收邮箱（多个邮箱用,分开）
		/// </summary>
		private string m_EMail;
		public string EMail
		{
			get { return m_EMail; }
			set { m_EMail = value; }
		}
		/// <summary>
		/// 发布日期
		/// </summary>
		private DateTime? m_PublishDate;
		public DateTime? PublishDate
		{
			get { return m_PublishDate; }
			set { m_PublishDate = value; }
		}
		/// <summary>
		/// 截止日期
		/// </summary>
		private DateTime? m_EndDate;
		public DateTime? EndDate
		{
			get { return m_EndDate; }
			set { m_EndDate = value; }
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
		/// 工作职责
		/// </summary>
		private string m_WorkDuty;
		public string WorkDuty
		{
			get { return m_WorkDuty; }
			set { m_WorkDuty = value; }
		}
		/// <summary>
		/// 任职资格
		/// </summary>
		private string m_Qualifications;
		public string Qualifications
		{
			get { return m_Qualifications; }
			set { m_Qualifications = value; }
		}
		/// <summary>
		/// 福利情况
		/// </summary>
		private string m_Welfare;
		public string Welfare
		{
			get { return m_Welfare; }
			set { m_Welfare = value; }
		}
		/// <summary>
		/// 审核情况（3未审核、99已审核）
		/// </summary>
		private int? m_FlowState;
		public int? FlowState
		{
			get { return m_FlowState; }
			set { m_FlowState = value; }
		}
		/// <summary>
		/// 录入人
		/// </summary>
		private string m_AddMan;
		public string AddMan
		{
			get { return m_AddMan; }
			set { m_AddMan = value; }
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
		/// 站点ID
		/// </summary>
		private int? m_SiteID;
		public int? SiteID
		{
			get { return m_SiteID; }
			set { m_SiteID = value; }
		}
		/// <summary>
		/// 排序号
		/// </summary>
		private int? m_Orders;
		public int? Orders
		{
			get { return m_Orders; }
			set { m_Orders = value; }
		}
		/// <summary>
		/// 是否删除（0=否，1=是）
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
		/// 页面标题（SEO优化用）
		/// </summary>
		private string m_PageTitle;
		public string PageTitle
		{
			get { return m_PageTitle; }
			set { m_PageTitle = value; }
		}
		/// <summary>
		/// 页面关键字（SEO优化用）
		/// </summary>
		private string m_MetaKeyword;
		public string MetaKeyword
		{
			get { return m_MetaKeyword; }
			set { m_MetaKeyword = value; }
		}
		/// <summary>
		/// 页面描述（SEO优化）
		/// </summary>
		private string m_MetaDescript;
		public string MetaDescript
		{
			get { return m_MetaDescript; }
			set { m_MetaDescript = value; }
		}
		/// <summary>
		/// 是否生成静态页面
		/// </summary>
		private bool? m_IsHtml;
		public bool? IsHtml
		{
			get { return m_IsHtml; }
			set { m_IsHtml = value; }
		}
	}
}