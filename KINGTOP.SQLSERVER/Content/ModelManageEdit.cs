#region 程序集引用
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using KingTop.IDAL.Content;
using KingTop.Common;
using KingTop.Model.Content;
#endregion

#region 版权注释
/*----------------------------------------------------------------------------------------
// Copyright (C) 2010 华强北在线 
// 作者：吴岸标
// 创建日期：2010-03-09
// 功能描述：对K_ModelManage表更新操作

// 更新日期        更新人      更新原因/内容
//
----------------------------------------------------------------------------------------*/
#endregion


namespace KingTop.SQLServer.Content
{
    public partial class ModelManage : IModelManage
    {
        #region 获取公用字段相关记录
        /// <summary>
        /// 获取公用字段相关记录
        /// </summary>
        /// <returns></returns>
        public DataTable GetCommonField()
        {
            string selSQL;
            DataTable dt;
            SqlDataReader sqlReader;

            dt = new DataTable();
            selSQL = "SELECT ID,FieldAlias as Title,IsDefault as IsPublic,Name FROM K_ModelCommonField where IsDel=0 order by Orders asc";
            sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
            dt.Load(sqlReader);

            return dt;
        }
        #endregion

        #region 保存配置
        /// <summary>
        /// 保存单个公用字段
        /// </summary>
        /// <param name="modelID">模型ID</param>
        /// <param name="commonFieldID">公用字段ID</param>
        /// <param name="fieldID">字段ID</param>
        /// <param name="orders">排序</param>
        /// <param name="tranType">1 添加 2 删除</param>
        /// <returns></returns>
        public string SetCommonField(string modelID, string commonFieldID, string fieldID, int orders,int tranType)
        {
            SqlParameter[] addParam;
            SqlParameter outPutParam;
            string result;

            outPutParam = new SqlParameter();
            outPutParam.ParameterName = "@Result";
            outPutParam.SqlDbType = SqlDbType.Int;
            outPutParam.Direction = ParameterDirection.Output;

            addParam = new SqlParameter[] {
                new SqlParameter("@ModelID",modelID),
                new SqlParameter("@CommonFieldID",commonFieldID),
                new SqlParameter("@FieldID",fieldID),
                new SqlParameter("@Orders",orders),outPutParam,
                new SqlParameter("@TranType",tranType)
            };

            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "PROC_K_ConfigCommonField", addParam);
                result = outPutParam.Value.ToString();
            }
            catch
            {
                result = "-300";
            }
            return result;
        }
        #endregion

        #region LinkList(链接)的相关操作
        #region 从表中删除列  DropTableFieldPackaing(string tableName, string fieldName)
        /// <summary>
        /// 从表中删除列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string DropTableFieldPackaing(string tableName, string fieldName)
        {
            string dropSQL;

             // 删除字段的缺省约束
            dropSQL = "exec DropDefautConstraint '" + tableName + "' ,'" + fieldName + "';";
            // 判断字段是否存在，存在则删除
            dropSQL = dropSQL + "if exists(select * from sys.columns where object_id=object_id('"+ tableName +"') and name='"+ fieldName +"')";
            // 删除字段
            dropSQL = dropSQL + "alter table " + tableName + " drop column  " + fieldName + ";";     

            return dropSQL;
        }
        #endregion

        #region 从字段表中删除记录DeleteModelFieldRowPackaing(string modelID, string fieldName)
        /// <summary>
        /// 从字段表中删除条件等于ModelID 和 fieldName的记录
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string DeleteModelFieldRowPackaing(string modelID, string fieldName)
        {
            string delSQL;

            delSQL = "delete from K_ModelField where [Name]='" + fieldName + "' and [ModelId]='" + modelID + "';";

            return delSQL;
        }
        #endregion

        #region 添加表列 AddTableFieldPackaing(string tableName, LinkList link)
        /// <summary>
        /// 添加表列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        public string AddTableFieldPackaing(string tableName, LinkList link)
        {
            StringBuilder addSQL = new StringBuilder();

            addSQL.Append("alter table ");
            addSQL.Append(tableName);
            addSQL.Append(" add  ");
            addSQL.Append(link.Name);

            if (link.DataType.ToLower() == "bool")       // 布置尔型
            {
                addSQL.Append(" bit ");
            }
            else if (link.DataType.ToLower() == "int")    // 整型
            {
                addSQL.Append(" int ");
            }
            else if (link.DataType.ToLower() == "string") // 字符串型
            {
                addSQL.Append(" nvarchar("+ link.CharLength.ToString() +") ");
            }

            // 列允许空值
            if (link.IsNull)
            {
                addSQL.Append(" null ");
            }
            else // 列不允许空值
            {
                addSQL.Append(" not null ");
            }

            // 列缺省值
            if (link.DefaultValue.Trim() != "")
            {
                addSQL.Append(" default(");
                addSQL.Append(link.DefaultValue);
                addSQL.Append(")");
            }

            addSQL.Append(";");
            return addSQL.ToString();
        }
        #endregion

        #region 往字段表中插入记录 InsertModelFieldRowPackaing(LinkList link, string id, string modelID, int orders)
        /// <summary>
        /// 往字段表中插入记录
        /// </summary>
        /// <param name="link"></param>
        /// <param name="id"></param>
        /// <param name="modelID"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public string InsertModelFieldRowPackaing(LinkList link, string id, string modelID, int orders)
        {
            StringBuilder insertSQL;
            insertSQL = new StringBuilder();

            insertSQL.Append("insert into K_ModelField(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,BasicField,OptionsValue,DropDownDataType)");
            insertSQL.Append("values('");
            insertSQL.Append(id);           // ID字段 主键
            insertSQL.Append("','");
            insertSQL.Append("0");          //  所属字段组
            insertSQL.Append("','");
            insertSQL.Append(modelID);      //  所属模型
            insertSQL.Append("','");
            insertSQL.Append(link.Name);    //  字段名称
            insertSQL.Append("','");
            insertSQL.Append(link.Alias);   // 别名
            insertSQL.Append("',");
            insertSQL.Append(1);            // 是否是系统字段
            insertSQL.Append(",");
            insertSQL.Append(1);            // 是否需在编辑页解析显示
            insertSQL.Append(",");
            insertSQL.Append(orders);       // 排序
            insertSQL.Append(",");

            if ((link.DataType.ToLower() == "string"))
            {
                insertSQL.Append(1);
            }
            else
            {
                // 单选按钮
                if (link.IsRadio)
                {
                    insertSQL.Append(4);
                }
                else // 复选框
                {
                    insertSQL.Append(5);
                }
            }
            insertSQL.Append(",'");
            insertSQL.Append(link.Value);    // 单选按钮或复选框值列
            insertSQL.Append("'");

            insertSQL.Append(",1);");

            return insertSQL.ToString();
        }
        #endregion

        #endregion

        #region SysField(自定义/系统字段)的相关操
        #region 从表中删除列 DropTableFieldPackaing(string tableName, string[] arrFieldName)
        /// <summary>
        /// 自定义/系统字段操作从表中删除列 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="arrFieldName"></param>
        /// <returns></returns>
        public string DropTableFieldPackaing(string tableName, string[] arrFieldName)
        {
            StringBuilder dropSQL;
            dropSQL = new StringBuilder();

            if (arrFieldName != null && !string.IsNullOrEmpty(tableName))
            {
                foreach (string fieldName in arrFieldName)
                {
                    dropSQL.Append("exec DropDefautConstraint '" + tableName + "' ,'" + fieldName + "';");  // 删除字段的缺省约束
                    dropSQL.Append("alter table " + tableName + " drop column  " + fieldName + ";");   // 删除字段
                }
            }

            return dropSQL.ToString();
        }
        #endregion

        #region 从字段表中删除记录 DeleteModelFieldRowPackaing(string modelID, string[] arrFieldName)
        /// <summary>
        ///  自定义/系统字段操作
        ///  从字段表中删除条件等于ModelID 和 fieldName[]的记录
        /// </summary>
        /// <param name="modelID"></param>
        /// <param name="arrFieldName"></param>
        /// <returns></returns>
        public string DeleteModelFieldRowPackaing(string modelID, string[] arrFieldName)
        {
            StringBuilder delSQL;
            delSQL = new StringBuilder();

            if (arrFieldName != null && !string.IsNullOrEmpty(modelID))
            {
                foreach (string fieldName in arrFieldName)
                {
                    delSQL.Append("delete from K_ModelField where [Name]='" + fieldName + "' and [ModelId]='" + modelID + "';");
                }
            }

            return delSQL.ToString();
        }
        #endregion

        #region 添加表列 AddTableFieldPackaing(string tableName, SysField mSysField)
        /// <summary>
        /// 自定义/系统字段操作 添加表列
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="mSysField"></param>
        /// <returns></returns>
        public string AddTableFieldPackaing(string tableName, SysField mSysField)
        {
            StringBuilder addSQL = new StringBuilder();

            for(int i=0;i<mSysField.Name.Length;i++)
            {
                addSQL.Append("alter table ");
                addSQL.Append(tableName);
                addSQL.Append(" add  ");
                addSQL.Append(mSysField.Name[i]);

                //类型
                addSQL.Append(" ");
                if(mSysField.TypeLength !=null)
                {
                    addSQL.Append(ToSqlServerType(mSysField.DataType[i],mSysField.TypeLength[i]));
                }
                else
                {
                    addSQL.Append(ToSqlServerType(mSysField.DataType[i], null));
                }
                addSQL.Append(" ");

                // 列允许空值
                if (mSysField.IsNull[i])
                {
                    addSQL.Append(" null ");
                }
                else // 列不允许空值
                {
                    addSQL.Append(" not null ");
                }

                // 缺省值
                addSQL.Append(" default(");
                addSQL.Append(ParseDefaultValue(mSysField.DefaultValue[i],mSysField.DataType[i]));
                addSQL.Append(")");
                addSQL.Append(";");
            }

            return addSQL.ToString();
        }
        #endregion

        #region 往字段表中插入记录 InsertModelFieldRowPackaing(SysField mSysField, string[] id, string modelID, int[] orders)
        /// <summary>
        ///  自定义/系统字段操作 往字段表中插入记录
        /// </summary>
        /// <param name="mSysField"></param>
        /// <param name="id"></param>
        /// <param name="modelID"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        public string InsertModelFieldRowPackaing(SysField mSysField, string[] id, string modelID, int[] orders)
        {
            StringBuilder insertSQL;
            insertSQL = new StringBuilder();

            for (int i = 0; i < id.Length; i++)
            {
                insertSQL.Append("if not exists(select top 1 * from K_ModelField where Name='" + mSysField.Name[i] + "' and ModelId='" + modelID + "')");
                insertSQL.Append("insert into K_ModelField(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,IsSearch,IsListEnable,SystemFirerdHtml,SysFieldType,TextBoxMaxLength,TextBoxWidth,TextBoxHieght)");
                insertSQL.Append("values('");
                insertSQL.Append(id[i]);                 // ID字段 主键
                insertSQL.Append("','");
                insertSQL.Append("0");                  //  所属模型组
                insertSQL.Append("','");
                insertSQL.Append(modelID);              // 所属模型
                insertSQL.Append("','");
                insertSQL.Append(mSysField.Name[i]);    //  字段名称
                insertSQL.Append("','");
                insertSQL.Append(mSysField.Alias[i]);   // 别名
                insertSQL.Append("',");
                insertSQL.Append(1);                    // 是否是系统字段
                insertSQL.Append(",");

                if (mSysField.IsEdit)
                {
                    insertSQL.Append(0);                // 在编辑页解析显示
                }
                else
                {
                    insertSQL.Append(0);               // 不在编辑页解析显示
                }

                insertSQL.Append(",");
                insertSQL.Append(orders[i]);           // 排序
                insertSQL.Append(",");

                if (mSysField.IsSearch)                // 是否搜索
                { 
                    insertSQL.Append(0);
                }
                else
                {
                    insertSQL.Append(0);
                }

                insertSQL.Append(",");

                if (mSysField.IsList)                  // 是否在列表页显示
                {
                    insertSQL.Append(1);
                }
                else
                {
                    insertSQL.Append(0);
                }

                insertSQL.Append(",'");
                insertSQL.Append(mSysField.ID);
                insertSQL.Append("',1,512,180,40");             
                insertSQL.Append(");");
            }
            return insertSQL.ToString();
        }
        #endregion
        #endregion

        #region 预设类型转换为SQLSERVER类型
        private string ToSqlServerType(string type, int? length)
        {
            string sqlType;

            switch (type.ToLower().Trim())
            {
                case "bool":
                    sqlType = "bit";
                    break;
                case "int":
                    sqlType = "int";
                    break;
                case "float":
                    sqlType = "float";
                    break;
                case "money":
                    sqlType = "money";
                    break;
                case "date":
                    sqlType = "datetime";
                    break;
                case "string":
                    if (length != null && length != 0)
                    {
                        sqlType = "nvarchar(" + length + ")";
                    }
                    else
                    {
                        sqlType = "nvarchar(max)";
                    }
                    break;
                default :
                    sqlType = "nvarchar(max)";
                    break;
            }

            return sqlType;
        }
        #endregion

        #region 将缺省值转换成SQLSERVER可接受的值
        private string ParseDefaultValue(string value, string type)
        {
            string sqlDefault; ;

            switch (type.ToLower().Trim())
            {
                case "date":                // 日期的日期部分
                case "time":                // 日期的时间部分
                case "datetime":            // 日期
                    if (value.Contains("-"))
                    {
                        sqlDefault = "'" + value + "'";
                    }
                    else
                    {
                        sqlDefault = value;
                    }
                    break;
                case "string":
                    sqlDefault = "'" + value + "'";
                    break;
                default:
                    sqlDefault = value;
                    break;
            }

            return sqlDefault;
        }
        #endregion

        #region 创建表        /// <summary>
        /// 创建表        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string CreateTable(string tableName,bool isSub)
        {
            StringBuilder createSQL;
            createSQL = new StringBuilder();

            // 按照模型建表
            createSQL.Append("CREATE TABLE ");
            createSQL.Append("[" + tableName + "]");
            createSQL.Append(" (");
            createSQL.Append("[ID] varchar(15) NOT NULL,");
            createSQL.Append("[IsDel] int default(0) null,");
            createSQL.Append("[IsEnable] int default(1) null,");
            createSQL.Append("[IsArchiving] int default(0) null,");
            createSQL.Append("[Orders] bigint default(0) null,");
            createSQL.Append("[AddDate] datetime default(getdate()) null,");
            createSQL.Append("[DelTime] datetime default(getdate()) null,");
            createSQL.Append("[SiteID] int not null,");
            createSQL.Append("[NodeCode] varchar(50) not null,");
            createSQL.Append("[FlowState] int null default(3) CHECK([FlowState] >= 0 AND [FlowState] <= 99),");

            if (isSub)
            {
                createSQL.Append("[ParentID] varchar(15)  NULL,");
            }
            createSQL.Append("[ReferenceID] varchar(15)  NULL,");
            createSQL.Append("[AddMan] varchar(50)  NULL,");
            createSQL.Append("CONSTRAINT [PK_K_U_CLU_" + tableName + "] PRIMARY KEY CLUSTERED ([ID] ASC)");
            createSQL.Append(");");

            return createSQL.ToString();
        }
        #endregion

        #region 创建表时的初始字段记录加入字段表中        /// <summary>
        /// 创建表时的初始字段记录加入字段表中        /// </summary>
        /// <param name="id">记录ID数组应与orders数组值一一对应</param>
        /// <param name="orders"></param>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public string InsertInitField(string[] id,int[] orders,string modelID,bool isSub)
        {
            StringBuilder insertSQL;

            insertSQL = new StringBuilder();

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders)values('");
            insertSQL.Append(id[0] + "','0','" + modelID + "','ID','主键',1,0," + orders[0] + ");");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders)values('");
            insertSQL.Append(id[1] + "','0','" + modelID + "','SiteID','所属站点',1,0," + orders[1] + ");");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders)values('");
            insertSQL.Append(id[2] + "','0','" + modelID + "','NodeCode','所属节点',1,0," + orders[2] + ");");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,BasicField,OptionsValue,DropDownDataType,DefaultValue)values('");
            insertSQL.Append(id[3] + "','0','" + modelID + "','IsDel','是否删除',1,0," + orders[3] + ",'4','是|1,否|0',1,0);");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,BasicField,OptionsValue,DropDownDataType,DefaultValue)values('");
            insertSQL.Append(id[4] + "','0','" + modelID + "','IsArchiving','是否归档',1,0," + orders[4] + ",'4','是|1,否|0',1,0);");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,BasicField,OptionsValue,DropDownDataType,DefaultValue)values('");
            insertSQL.Append(id[5] + "','0','" + modelID + "','IsEnable','是否启用',1,0," + orders[5] + ",'4','是|1,否|0',1,1);");
 
            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,BasicField,OptionsValue)values('");
            insertSQL.Append(id[6] + "','0','" + modelID + "','Orders','排序',1,0," + orders[6] + ",'8','');");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,BasicField)values('");
            insertSQL.Append(id[7] + "','0','" + modelID + "','AddDate','添加日期',1,0," + orders[7] + ",'10');");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,DropDownDataType,DefaultValue)values('");
            insertSQL.Append(id[8] + "','0','" + modelID + "','FlowState','审核状态',1,0," + orders[8] + ",1,3);");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders)values('");
            insertSQL.Append(id[9] + "','0','" + modelID + "','ReferenceID','引用ID',1,0," + orders[9] + ");");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders)values('");
            insertSQL.Append(id[10] + "','0','" + modelID + "','AddMan','添加用户',1,0," + orders[10] + ");");

            insertSQL.Append("insert into K_ModelField");
            insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders,BasicField)values('");
            insertSQL.Append(id[11] + "','0','" + modelID + "','DelTime','删除时间',1,0," + orders[11] + ",'10');");

            if (isSub)
            {
                insertSQL.Append("insert into K_ModelField");
                insertSQL.Append("(ID,ModelFieldGroupId,ModelID,Name,FieldAlias,IsSystemFiierd,IsInputValue,Orders)values('");
                insertSQL.Append(id[12] + "','0','" + modelID + "','ParentID','主模型表ID',1,0," + orders[12] + ");");
            }


            return insertSQL.ToString();
        }
        #endregion

        #region 按钮操作与公共操作表同步
        public bool PublicOperSynchronization(string operName, string operTitle, int operCount)
        {
            bool result;                 
            SqlParameter[] editParam;

            editParam = new SqlParameter[] { new SqlParameter("@OperName", operName), new SqlParameter("@Title", operTitle), new SqlParameter("@OperCount", operCount) };
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, "proc_k_AddPublicOper", editParam);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 子模型分组列表
        public DataTable GetSubModelGroupList()
        {
            SqlDataReader sqlReader;
            DataTable dt;
            string selSQL;

            dt = new DataTable();
            selSQL = "select ID,Name from K_SubModelGroup;";

            try
            {
                sqlReader = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, selSQL, null);
                dt.Load(sqlReader);
            }
            catch { }

            return dt;
        }
        #endregion

    }
}
