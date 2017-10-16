#region 引用程序集
using System;
using System.Collections.Generic;
using System.Collections;
using System.Xml.XPath;
using System.Xml;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web;

using KingTop.IDAL;
using KingTop.Model;
#endregion

#region 版权注释
/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-06-21
// 功能描述：模型字段列表操作
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class FieldManage
    {
        #region 解析基本类型
        private Hashtable GetBasicType()
        {
            Hashtable hsBasicType = new Hashtable();

            hsBasicType.Add("1", "单行文本");
            hsBasicType.Add("2", "纯文本");
            hsBasicType.Add("3", "HTML文本");
            hsBasicType.Add("4", "单选");
            hsBasicType.Add("5", "多选");
            hsBasicType.Add("6", "下拉列表");
            hsBasicType.Add("7", "多选列表");
            hsBasicType.Add("8", "数字");
            hsBasicType.Add("9", "货币");
            hsBasicType.Add("10", "时间日期");
            hsBasicType.Add("11", "图片");
            hsBasicType.Add("12", "文件");
            hsBasicType.Add("13", "隐藏域");

            return hsBasicType;
        }
        #endregion

        #region 获取字段组列表
        private string GetFieldGroup()
        {
            StringBuilder sbFieldGroup;
            DataTable dt;

            sbFieldGroup = new StringBuilder();
            dt = dal.GetModelFieldGroup(this.modelID);

            sbFieldGroup.Append("<select fieldID=\"{#ID#}\" style=\"width:110px;\" selectedValue=\"{#Selected#}\" onchange=\"setSelected(this)\">");
            sbFieldGroup.Append("<option value=\"0\">--请选择--</option>");

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sbFieldGroup.Append("<option value=\""+ dr["ID"].ToString() +"\">" + dr["Name"].ToString() +"</option>");
                }
            }

            sbFieldGroup.Append("</select>");

            return sbFieldGroup.ToString();
        }
        #endregion

        #region 字段配置
        /// <summary>
        /// 字段配置初始加载
        /// </summary>
        /// <param name="configType">配置类型 1 搜索 2 列表  3 编辑</param>
        /// <param name="fieldID">要配置的字段ID</param>
        /// <returns></returns>
        public DataTable GetSpecialFieldAtrr(string configType,string fieldID)
        {
            if (!string.IsNullOrEmpty(configType) && !string.IsNullOrEmpty(fieldID))
            {
                return dal.GetSpecialFieldAtrr(configType, fieldID);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 更新字段配置
        /// <summary>
        /// 更新字段配置
        /// </summary>
        /// <param name="hsFieldValue">字段值对</param>
        /// <returns></returns>
        public int EditSpecialFieldAtrr(Hashtable hsFieldValue)
        {
            int returnValue;         // 受影响行数

            if (hsFieldValue != null && hsFieldValue.Count > 0)
            {
                returnValue = dal.EditSpecialFieldAtrr(hsFieldValue);
            }
            else
            {
                returnValue = 0;
            }

            return returnValue;
        }
        #endregion
    }
}
