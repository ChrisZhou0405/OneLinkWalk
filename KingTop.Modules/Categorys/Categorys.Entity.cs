using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingTop.Modules.Entity
{
    public class Categorys
    {
        private DateTime? m_AddDate;
        public DateTime? AddDate
        {
            get { return m_AddDate; }
            set { m_AddDate = value; }
        }
        private string m_AddMan;
        public string AddMan
        {
            get { return m_AddMan; }
            set { m_AddMan = value; }
        }
        private string m_ArrayChildID;
        public string ArrayChildID
        {
            get { return m_ArrayChildID; }
            set { m_ArrayChildID = value; }
        }
        private string m_ArrayParentID;
        public string ArrayParentID
        {
            get { return m_ArrayParentID; }
            set { m_ArrayParentID = value; }
        }
        private int? m_Child;
        public int? Child
        {
            get { return m_Child; }
            set { m_Child = value; }
        }
        private DateTime? m_delTime;
        public DateTime? delTime
        {
            get { return m_delTime; }
            set { m_delTime = value; }
        }
        private string m_Description;
        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        private int? m_ID;
        public int? ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private int? m_IsDel;
        public int? IsDel
        {
            get { return m_IsDel; }
            set { m_IsDel = value; }
        }
        private int? m_IsIndex;
        public int? IsIndex
        {
            get { return m_IsIndex; }
            set { m_IsIndex = value; }
        }
        private int? m_IsValid;
        public int? IsValid
        {
            get { return m_IsValid; }
            set { m_IsValid = value; }
        }

        private string m_Name;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private string m_NodeCode;
        public string NodeCode
        {
            get { return m_NodeCode; }
            set { m_NodeCode = value; }
        }
        private int? m_Orders;
        public int? Orders
        {
            get { return m_Orders; }
            set { m_Orders = value; }
        }
        private int? m_ParentID;
        public int? ParentID
        {
            get { return m_ParentID; }
            set { m_ParentID = value; }
        }
        private string m_ParentName;
        public string ParentName
        {
            get { return m_ParentName; }
            set { m_ParentName = value; }
        }
        private int? m_SiteID;
        public int? SiteID
        {
            get { return m_SiteID; }
            set { m_SiteID = value; }
        }

        private string m_PageTitle;
        private string m_PageKeywords;
        private string m_PageDescription;
        private string m_URLRewriter;
        private string m_Img;

        public string Img
        {
            get { return m_Img; }
            set { m_Img = value; }
        }

        public string PageTitle
        {
            get { return m_PageTitle; }
            set { m_PageTitle = value; }
        }

        public string PageKeywords
        {
            get { return m_PageKeywords; }
            set { m_PageKeywords = value; }
        }

        public string PageDescription
        {
            get { return m_PageDescription; }
            set { m_PageDescription = value; }
        }
        public string URLRewriter
        {
            get { return m_URLRewriter; }
            set { m_URLRewriter = value; }
        }
        public DateTime UpdateDate { get; set; }
        public int Depth { get; set; }
        public string ColumnID { get; set; }

    }
}
