using System;
using System.Collections.Generic;
using System.Text;

namespace KingTop.Modules
{
    public enum PageParam
    {
        /// <summary>
        /// 不等于
        /// </summary>
        NoEqual,
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 模糊查询
        /// </summary>
        Like,
        /// <summary>
        /// 小于
        /// </summary>
        LT, 
        /// <summary>
        /// 小于等于
        /// </summary>
        LTAndEqual,  
        /// <summary>
        /// 大于
        /// </summary>
        GT,
        /// <summary>
        /// 大于等于
        /// </summary>
        GTAndEqual,        
        /// <summary>
        /// 小于 and 大于
        /// </summary>
        Between

    }
    #region 常用枚举
    /// <summary>
    /// sql参数类型
    /// </summary>
    public enum SqlParamType
    {
        Str, Int, Date
    }
    #endregion
    /// <summary>
    /// 前台分页参数
    /// </summary>
    public class PageParams
    {
        private string strFieldName, strFieldValue, strFieldValue2;

        private PageParam param = PageParam.Like;       
        private SqlParamType paramType =SqlParamType.Str;

        public PageParams()
        {

        }
        
        /// <summary>
        /// 前台分页参数
        /// </summary>
        /// <param name="strFieldName">列名</param>
        /// <param name="param">参数类型</param>
        /// <param name="strFieldValue">列值</param>
        public PageParams(string strFieldName, PageParam param, string strFieldValue)
        {
            this.strFieldName = strFieldName;
            this.param = param;
            this.strFieldValue = strFieldValue;           
        }

        /// <summary>
        ///列类型
        /// </summary>
        public SqlParamType ParamType
        {
            get { return paramType; }
            set { paramType = value; }
        }
        /// <summary>
        /// 前台分页参数
        /// </summary>
        /// <param name="strFieldName">列名</param>
        /// <param name="param">参数类型</param>
        /// <param name="paramType">列类型</param>
        /// <param name="strFieldValue">列值</param>
        /// <param name="strFieldValue2">列值2</param>   
        public PageParams(string strFieldName, PageParam param,SqlParamType paramType, string strFieldValue, string strFieldValue2)
        {
            this.strFieldName = strFieldName;
            this.param = param;
            this.strFieldValue = strFieldValue;
            this.strFieldValue2 = strFieldValue2;
            this.paramType = paramType;
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public PageParam Param
        {
            get { return param; }
            set { param = value; }
        }

        /// <summary>
        /// 列值
        /// </summary>
        public string StrFieldValue
        {
            get { return strFieldValue; }
            set { strFieldValue = value; }
        }

        /// <summary>
        /// 列值
        /// </summary>
        public string StrFieldValue2
        {
            get { return strFieldValue2; }
            set { strFieldValue2 = value; }
        }


        /// <summary>
        /// 列名
        /// </summary>
        public string StrFieldName
        {
            get { return strFieldName; }
            set { strFieldName = value; }
        }
    }
}
