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
// 功能描述：模型字段操作
===========================================================================*/
#endregion

namespace KingTop.BLL.Content
{
    public partial class FieldManage
    {
        #region 变量成员
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Content.IFieldManage dal = (IDAL.Content.IFieldManage)Assembly.Load(path).CreateInstance(path + ".Content.FieldManage");
        private string modelID;                 // 所属模型

        private string tableName;               // 所属模型表
        private Hashtable _hsBasicType = null;  // 基本类型名称
        private string _fieldGroupHtml;         // 当前模型的字段分组 
        #endregion

        #region 属性

        /// <summary>
        /// 基本字段类型
        /// </summary>
        public Hashtable BasicType
        {
            get
            {
                if (this._hsBasicType == null)
                {
                    this._hsBasicType = GetBasicType();
                }

                return this._hsBasicType;
            }
        }

        /// <summary>
        /// 字段分组下拉列表HTML代码
        /// </summary>
        public string FieldGroupHtml
        {
            get
            {
                if (string.IsNullOrEmpty(this._fieldGroupHtml))
                {
                    this._fieldGroupHtml = GetFieldGroup();
                }

                return this._fieldGroupHtml;
            }
        }
        #endregion

        #region 初始函数
        public FieldManage(string modelID, string tableName)
        {
            this.modelID = modelID;
            this.tableName = tableName;
        }
        #endregion

        #region 验证规绑定

        /// <summary>
        /// 绑定验证规则
        /// </summary>
        /// <param name="radlValidationType"></param>
        public void ValidationTypeBind(RadioButtonList radlValidationType)
        {
            DataSet ds;             // 保存配置内容
            string configPath;      // 配置路径
            ListItem typeItem;      // 代表一种验证规则的单选按钮


            ds = new DataSet();
            configPath = "../Configuraion/Model/fieldvalidate.xml";
            ds.ReadXml(HttpContext.Current.Server.MapPath(configPath));

            typeItem = new ListItem("无", "-1");
            typeItem.Selected = true;

            radlValidationType.Items.Add(typeItem);     // 缺省没有验证规则

            if (ds != null && ds.Tables[0] != null)     // 加载成功
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    typeItem = new ListItem(dr["name"].ToString(), dr["value"].ToString());
                    typeItem.Attributes.Add("message", dr["message"].ToString());
                    typeItem.Attributes.Add("expression", dr["expression"].ToString());
                    radlValidationType.Items.Add(typeItem);
                }
            }

            radlValidationType.Items.Add(new ListItem("自定义", "0"));
        }
        #endregion

        #region 通过Text设置DropDownList的选中项

        /// <summary>
        /// 通过Text设置DropDownList的选中项

        /// </summary>
        public void DDLByTextSelected(DropDownList ddl, string text)
        {
            foreach (ListItem li in ddl.Items)         // 数据表列表
            {
                if (li.Text == text)
                {
                    li.Selected = true;
                }
                break;
            }
        }
        #endregion

        #region 获取DropDownList选中的Text
        /// <summary>
        /// 获取DropDownList选中的Text
        /// </summary>
        public string DDLSelectedText(DropDownList ddl)
        {
            string textValue;

            textValue = string.Empty;

            foreach (ListItem li in ddl.Items)         // 数据表列表
            {
                if (li.Selected)
                {
                    textValue = li.Text;
                }
                break;
            }

            return textValue;
        }
        #endregion

        #region 当选项数据来源为数据库读取时绑定数据表与表字段
        /// <summary>
        /// 当选项数据来源为数据库读取时绑定数据表与表字段
        /// </summary>
        /// <param name="ddlDropDownTable">绑定表名控件</param>
        /// <returns>tableName表对应的字段</returns>
        public DataTable DropDownDataTypeBind(DropDownList ddlDropDownTable, string tbName)
        {
            DataSet ds;          // ds.Tables[0] 数据表记录,ds.Tables[1]  tableName对应的字段

            DataTable dtField;

            dtField = null;

            if (!string.IsNullOrEmpty(tbName))
            {
                ds = dal.GetTableAndField(tbName);

            }
            else
            {
                ds = dal.GetTableAndField(this.tableName);
                tbName = this.tableName;
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
            {
                ddlDropDownTable.DataTextField = "Name";
                ddlDropDownTable.DataValueField = "ID";
                ddlDropDownTable.DataSource = ds.Tables[0];
                ddlDropDownTable.DataBind();
                ddlDropDownTable.SelectedItem.Text = tbName;
            }

            if (ds.Tables.Count > 1)    // tableName表对应的字段
            {
                dtField = ds.Tables[1];
            }

            return dtField;
        }
        #endregion

        #region 绑定字段分组
        public void ModelFieldGroupBind(DropDownList ddlModelFieldGroupId)
        {
            DataTable dtFieldGroup;

            dtFieldGroup = dal.GetModelFieldGroup(this.modelID);

            if (dtFieldGroup != null)
            {
                ddlModelFieldGroupId.DataTextField = "Name";
                ddlModelFieldGroupId.DataValueField = "ID";
                ddlModelFieldGroupId.DataSource = dtFieldGroup;
                ddlModelFieldGroupId.DataBind();
            }
        }
        #endregion

        #region 绑定子模型分组
        public void SubModelGroupBind(DropDownList ddlSubModelGroup)
        {
            DataTable dt;

            dt = dal.GetSubModelGroupList();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSubModelGroup.DataTextField = "Name";
                ddlSubModelGroup.DataValueField = "ID";
                ddlSubModelGroup.DataSource = dt;
                ddlSubModelGroup.DataBind();
            }
        }
        #endregion

        #region 绑定子模型列表
        public void SubModelBind(DropDownList ddlSubModel)
        {
            DataTable dt;

            dt = dal.GetSubModelList();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSubModel.DataTextField = "Title";
                ddlSubModel.DataValueField = "TableName";
                ddlSubModel.DataSource = dt;
                ddlSubModel.DataBind();
            }
        }
        #endregion

        #region 添加/更新
        /// <summary>
        /// 添加/更新模型字段
        /// </summary>
        public int Save(Hashtable hsFieldParam, string modelFieldID, string originalDefaultValue, string originalDataColumnLength)
        {
            return dal.Save(hsFieldParam, this.tableName, modelFieldID, originalDefaultValue, originalDataColumnLength);
        }
        #endregion

        #region 根据传入的参数查询K_ModelField,返回查询结果
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 设置或者删除K_ModelField记录
        public string ModelFieldSet(string tranType, string setValue, string IDList)
        {
            return dal.ModelFieldSet(tranType, setValue, IDList);
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        public void PageData(KingTop.Model.Pager pager, Hashtable hsWhereEqual, Hashtable hsWhereLike, string sort)
        {
            dal.PageData(pager, hsWhereEqual, hsWhereLike, sort);
        }
        #endregion

    }
}
