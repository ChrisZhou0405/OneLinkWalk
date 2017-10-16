
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2013 图派科技 
// 作者：袁纯林 ycl@toprand.com 2013-03-06
// 功能描述：对K_HRResume表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Modules.Entity
{
    public class HRResume
    {
        #region Model

        private int _id;
        private string _membername;
        private string _username;
        private string _nation;
        private string _gender;
        private DateTime? _birthday;
        private string _cardid;
        private int _weight;
        private string _marriage;
        private int _height;
        private string _photo;
        private string _city;
        private string _nativeplace;
        private bool _isroom;
        private int _workyear;
        private string _universities;
        private string _specialty;
        private string _degree;
        private string _englishlevel;
        private string _computerlevel;
        private string _industry;
        private string _post;
        private string _currentsalary;
        private string _requiressalary;
        private string _mobile;
        private string _tel;
        private string _email;
        private string _qq;
        private string _address;
        private string _hobbies;
        private string _speciality;
        private string _zipcode;
        private string _informationway;
        private string _postexpect;
        private string _companyexpect;
        private DateTime? _reportdate;
        private string _workexperience;
        private string _workdescription;
        private string _skillsexpertise;
        private string _educationalbackground;
        private string _train;
        private int _siteid;
        
        private int _isdel;
        
        private string _nodecode;
        private bool _isread;
        

        public HRResume()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        public virtual int ID
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string MemberName
        {
            set { _membername = value; }
            get { return _membername; }
        }

        /// <summary>
        /// Re姓名
        /// </summary>
        public virtual string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 民族
        /// </summary>
        public virtual string Nation
        {
            set { _nation = value; }
            get { return _nation; }
        }

        /// <summary>
        /// 性别（男、女、保密）
        /// </summary>
        public virtual string Gender
        {
            set { _gender = value; }
            get { return _gender; }
        }

        /// <summary>
        /// 出身日期
        /// </summary>
        public virtual DateTime? Birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public virtual string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }

        /// <summary>
        /// 体重
        /// </summary>
        public virtual int Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public virtual string Marriage
        {
            set { _marriage = value; }
            get { return _marriage; }
        }

        /// <summary>
        /// 身高（单位CM）
        /// </summary>
        public virtual int Height
        {
            set { _height = value; }
            get { return _height; }
        }

        /// <summary>
        /// 头像
        /// </summary>
        public virtual string Photo
        {
            set { _photo = value; }
            get { return _photo; }
        }

        /// <summary>
        /// 目前所在地
        /// </summary>
        public virtual string City
        {
            set { _city = value; }
            get { return _city; }
        }

        /// <summary>
        /// 户口所在地(籍贯）
        /// </summary>
        public virtual string NativePlace
        {
            set { _nativeplace = value; }
            get { return _nativeplace; }
        }

        /// <summary>
        /// 是否需要公司提供住宿
        /// </summary>
        public virtual bool IsRoom
        {
            set { _isroom = value; }
            get { return _isroom; }
        }

        /// <summary>
        /// 工作年限
        /// </summary>
        public virtual int WorkYear
        {
            set { _workyear = value; }
            get { return _workyear; }
        }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public virtual string Universities
        {
            set { _universities = value; }
            get { return _universities; }
        }

        /// <summary>
        /// 所学专业
        /// </summary>
        public virtual string Specialty
        {
            set { _specialty = value; }
            get { return _specialty; }
        }

        /// <summary>
        /// 最高学历
        /// </summary>
        public virtual string Degree
        {
            set { _degree = value; }
            get { return _degree; }
        }

        /// <summary>
        /// 英语水平
        /// </summary>
        public virtual string EnglishLevel
        {
            set { _englishlevel = value; }
            get { return _englishlevel; }
        }
        /// <summary>
        /// 计算机水平
        /// </summary>
        public virtual string ComputerLevel
        {
            set { _computerlevel = value; }
            get { return _computerlevel; }
        }/// <summary>
        /// 目前行业
        /// </summary>
        public virtual string Industry
        {
            set { _industry = value; }
            get { return _industry; }
        }/// <summary>
        /// 目前岗位
        /// </summary>
        public virtual string Post
        {
            set { _post = value; }
            get { return _post; }
        }/// <summary>
        /// 目前月薪
        /// </summary>
        public virtual string CurrentSalary
        {
            set { _currentsalary = value; }
            get { return _currentsalary; }
        }/// <summary>
        /// 期望月薪
        /// </summary>
        public virtual string RequiresSalary
        {
            set { _requiressalary = value; }
            get { return _requiressalary; }
        }/// <summary>
        /// 手机
        /// </summary>
        public virtual string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }/// <summary>
        /// 固定电话
        /// </summary>
        public virtual string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }/// <summary>
        /// 邮件地址
        /// </summary>
        public virtual string EMail
        {
            set { _email = value; }
            get { return _email; }
        }/// <summary>
        /// QQ号
        /// </summary>
        public virtual string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }/// <summary>
        /// 通讯地址
        /// </summary>
        public virtual string Address
        {
            set { _address = value; }
            get { return _address; }
        }/// <summary>
        /// 兴趣爱好
        /// </summary>
        public virtual string Hobbies
        {
            set { _hobbies = value; }
            get { return _hobbies; }
        }/// <summary>
        /// 专长
        /// </summary>
        public virtual string Speciality
        {
            set { _speciality = value; }
            get { return _speciality; }
        }/// <summary>
        /// 邮编
        /// </summary>
        public virtual string ZipCode
        {
            set { _zipcode = value; }
            get { return _zipcode; }
        }/// <summary>
        /// 获得职位信息途径
        /// </summary>
        public virtual string InformationWay
        {
            set { _informationway = value; }
            get { return _informationway; }
        }/// <summary>
        /// 对该职位的期望及对职业的规划
        /// </summary>
        public virtual string PostExpect
        {
            set { _postexpect = value; }
            get { return _postexpect; }
        }/// <summary>
        /// 对公司的期望
        /// </summary>
        public virtual string CompanyExpect
        {
            set { _companyexpect = value; }
            get { return _companyexpect; }
        }/// <summary>
        /// 到任日期
        /// </summary>
        public virtual DateTime? ReportDate
        {
            set { _reportdate = value; }
            get { return _reportdate; }
        }/// <summary>
        /// 工作经验
        /// </summary>
        public virtual string WorkExperience
        {
            set { _workexperience = value; }
            get { return _workexperience; }
        }/// <summary>
        /// 工作经验详细说明
        /// </summary>
        public virtual string WorkDescription
        {
            set { _workdescription = value; }
            get { return _workdescription; }
        }/// <summary>
        /// 技能专长
        /// </summary>
        public virtual string SkillsExpertise
        {
            set { _skillsexpertise = value; }
            get { return _skillsexpertise; }
        }
        /// <summary>
        /// 教育背景
        /// </summary>
        public virtual string EducationalBackground
        {
            set { _educationalbackground = value; }
            get { return _educationalbackground; }
        }/// <summary>
        /// 培训经历
        /// </summary>
        public virtual string Train
        {
            set { _train = value; }
            get { return _train; }
        }/// <summary>
        /// 简历语言
        /// </summary>
        public virtual int SiteID
        {
            set { _siteid = value; }
            get { return _siteid; }
        }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual string NodeCode
        {
            set { _nodecode = value; }
            get { return _nodecode; }
        }/// <summary>
        /// 是否阅读
        /// </summary>
        public virtual bool IsRead
        {
            set { _isread = value; }
            get { return _isread; }
        }

        #endregion Model

    }
}
