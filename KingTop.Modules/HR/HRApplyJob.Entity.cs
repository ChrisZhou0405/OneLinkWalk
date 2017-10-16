
using System;
using System.Collections.Generic;
using System.Text;

/*===========================================================================
// Copyright (C) 2013 图派科技 
// 作者：袁纯林 ycl@360hqb.com 2013-02-21
// 功能描述：对K_HRApplyJob表的字段定义属性

// 更新日期        更新人      更新原因/内容
//
============================================================================*/

namespace KingTop.Modules.Entity
{
    public class HRApplyJob
    {
        #region Model

        private int _id;
        private int _resumeid;
        private string _membername;
        private int _jobid;
        private DateTime _adddate;
        private int _status;

        public HRApplyJob()
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
        /// 简历ID（关联K_HRResume.ID)
        /// </summary>
        public virtual int ResumeID
        {
            set { _resumeid = value; }
            get { return _resumeid; }
        }

        /// <summary>
        /// 会员ID（如果需要用户先注册会员，在提交简历，则这个字段关联会员表ID）
        /// </summary>
        public virtual string MemberName
        {
            set { _membername = value; }
            get { return _membername; }
        }

        /// <summary>
        /// 职位ID
        /// </summary>
        public virtual int JobID
        {
            set { _jobid = value; }
            get { return _jobid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

        /// <summary>
        /// 申请状态（0=初始、1=一般、2=优秀、3=面试、4=录用、10=不合格、11=回收站）
        /// </summary>
        public virtual int Status
        {
            set { _status = value; }
            get { return _status; }
        }


        #endregion Model

    }
}
