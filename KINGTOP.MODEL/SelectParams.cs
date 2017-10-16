using System;
using System.Collections.Generic;
using System.Text;

namespace KingTop.Model
{
    public class SelectParams
    {
        /// <summary>
        /// 数据库查询参数实体包含３个整型，３个字符串型，２个日期时间型.
        /// </summary>
        #region Model
        private int _i1 = 0;
        private int _i2 = 0;
        private int _i3 = 0;
        private string _s1 = "";
        private string _s2 = "";
        private string _s3 = "";
        private string _s4 = "";
        private DateTime _t1 = DateTime.Today;
        private DateTime _t2 = DateTime.Today;

        public SelectParams()
        {
        }

        //static void Main()
        //{
        //    SelectParams selectparams = new SelectParams();
        //}

        public SelectParams(int _I1, int _I2, int _I3, string _S1, string _S2, string _S3,string _S4, DateTime _T1, DateTime _T2)
        {
            this._i1 = _I1;
            this._i2 = _I2;
            this._i3 = _I3;
            this._s1 = _S1;
            this._s2 = _S2;
            this._s3 = _S3;
            this._s4 = _S4;
            this._t1 = _T1;
            this._t2 = _T2;
        }
        /// <summary>
        /// 整型参数1
        /// </summary>
        public virtual int I1
        {
            set
            {
                _i1 = value;
            }
            get
            {
                return _i1;
            }
        }
        /// <summary>
        /// 整型参数2
        /// </summary>
        public virtual int I2
        {
            set
            {
                _i2 = value;
            }
            get
            {
                return _i2;
            }
        }
        /// <summary>
        /// 整型参数3
        /// </summary>
        public virtual int I3
        {
            set
            {
                _i3 = value;
            }
            get
            {
                return _i3;
            }
        }
        /// <summary>
        /// 字符串参数1
        /// </summary>
        public virtual string S1
        {
            set
            {
                _s1 = value;
            }
            get
            {
                return _s1;
            }
        }
        /// <summary>
        /// 字符串参数2
        /// </summary>
        public virtual string S2
        {
            set
            {
                _s2 = value;
            }
            get
            {
                return _s2;
            }
        }
        /// <summary>
        /// 字符串参数3
        /// </summary>
        public virtual string S3
        {
            set
            {
                _s3 = value;
            }
            get
            {
                return _s3;
            }
        }
        /// <summary>
        /// 字符串参数4
        /// </summary>
        public virtual string S4
        {
            set
            {
                _s4 = value;
            }
            get
            {
                return _s4;
            }
        }
        /// <summary>
        /// 日期时间参数1
        /// </summary>
        public virtual DateTime T1
        {
            set
            {
                _t1 = value;
            }
            get
            {
                return _t1;
            }
        }
        /// <summary>
        /// 日期时间参数2
        /// </summary>
        public virtual DateTime T2
        {
            set
            {
                _t2 = value;
            }
            get
            {
                return _t2;
            }
        }
        #endregion Model
    }
}
