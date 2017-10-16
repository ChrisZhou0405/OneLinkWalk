using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using KingTop.Common;
using KingTop.BLL.Content;
using KingTop.Web.Admin;

namespace KingTop.WEB.SysAdmin.Model
{
    public partial class ModelManageCode : AdminPage
    {
        #region 变量成员
        private string _modelID;                       // 所属模型
        private string _isSub = string.Empty;          // 子模型
        private string _tableName = null;              // 所属表名
        protected string jsMessage;                    // 操作返回的JS代码
        protected string urlParam;                     // 要传递的URL参数
        protected BLL.Content.FieldManage fieldManage; // 业务操作对象
        private string _modelName;                      // 所属模型名称
        public string qianzui = string.Empty;
        public int iLoop = 0;
        private string noCopyFields = ",";
        #endregion

        #region 属性
        /// <summary>
        /// 所属模型名称
        /// </summary>
        public string ModelName
        {
            get
            {
                if (string.IsNullOrEmpty(this._modelName))
                {
                    this._modelName = Request.QueryString["ModelName"];
                }

                return this._modelName;
            }
        }

        /// <summary>
        /// 所属模型表
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrEmpty(this._tableName))
                {
                    this._tableName = Request.QueryString["TableName"];
                }

                return this._tableName;
            }
        }

        /// <summary>
        /// 所属模型ID
        /// </summary>
        public string ModelID
        {
            get
            {
                if (string.IsNullOrEmpty(this._modelID))
                {
                    this._modelID = Request.QueryString["ModelID"];
                }

                return this._modelID;
            }
        }
        /// <summary>
        /// 是否显示子模型
        /// </summary>
        public string IsSub
        {
            get
            {
                if (string.IsNullOrEmpty(this._isSub))
                {
                    this._isSub = Request.QueryString["IsSub"];

                    if (this._isSub == null || this._isSub.Trim() == "")
                    {
                        this._isSub = "0";
                    }
                }

                return this._isSub;
            }
        }
        #endregion

        #region  Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            fieldManage = new FieldManage(ModelID, TableName);
            urlParam = "IsSub=" + IsSub + "&ModelID=" + ModelID + "&TableName=" + TableName + "&NodeCode=" + NodeCode + "&ModelName=" + ModelName; // 要传递的参数
            if (!string.IsNullOrEmpty(TableName) && TableName.Length > 4)
            {
                qianzui = TableName.Substring(0, 4);
            }
            if (!IsPostBack && !string.IsNullOrEmpty(this.ModelID))
            {
                PageInit();
            }
        }
        #endregion

        #region 初始加载
        /// <summary>
        /// 初始化方法
        /// </summary>
        public void PageInit()
        {
            if (!string.IsNullOrEmpty(ModelID.Trim()) && !string.IsNullOrEmpty(TableName.Trim()))
            {
                SplitDataBind();
            }
        }
        #endregion

        #region 加载数据
        // 分页控件数据绑定
        private void SplitDataBind()
        {
            KingTop.Model.SelectParams kmsObj = new KingTop.Model.SelectParams();
            kmsObj.S1 = ModelID;
            kmsObj.I1 = 1;
            DataTable dt = fieldManage.GetList("ALL", kmsObj);
            rptField1.DataSource = dt;
            rptField1.DataBind();
            rptField2.DataSource = dt;
            rptField2.DataBind();
            rptField3.DataSource = dt;
            rptField3.DataBind();
            rptField4.DataSource = dt;
            rptField4.DataBind();
            rptField5.DataSource = dt;
            rptField5.DataBind();
            string fieldsName = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (string.IsNullOrEmpty(fieldsName))
                {
                    fieldsName = dt.Rows[i]["Name"].ToString();
                }
                else
                {
                    fieldsName += "," + dt.Rows[i]["Name"].ToString();
                }
            }

            hidFields.Value = fieldsName;
        }
        #endregion

        #region TOP N条数据
        private void GetTopDataCode()
        {
            string fields = Request.Form["chkField1"];  //选择字段
            string csStr = "void BindTopData(string nc)";  //CS代码
            string aspxStr = "<asp:Repeater ID=\"rptInfo\" runat=\"server\">\r\n";  //ASPX代码
            string strSql = "   strSQL=\"SELECT TOP " + Request.Form["txtTOPNum"] + " ";   //组装CS代码里的SQL语句
            string all = Request.Form["chkAll1"];       //是否全部
            string tableFields = hidFields.Value.ToLower();     //表的所有字段

            aspxStr += "<ItemTemplate >\r\n";
            aspxStr += "    <li> <a href=\"<%#GetInfoUrl(Eval(\"ID\").ToString(), Eval(\"Title\").ToString(),Eval(\"AddDate\").ToString(),Eval(\"NodeCode\").ToString(),\"\")%>\">\r\n";
            if(tableFields.IndexOf (",pubdate,")!=-1)
            {
                aspxStr += "    <span><%#Eval(\"pubDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }
            else if(tableFields.IndexOf(",publishdate,")!=-1)
            {
                aspxStr += "    <span><%#Eval(\"publishDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }
            else if (tableFields.IndexOf(",updatedate,") != -1)
            {
                aspxStr += "    <span><%#Eval(\"updateDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }
            else
            {
                aspxStr += "    <span><%#Eval(\"AddDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }

            if (!string.IsNullOrEmpty(fields))
            {
                string[] arr = fields.Split(',');
                string otherFields = ",id,title,adddate,nodecode,pubdate,publishdate,updatedate,adddate,";
                for (int i = 0; i < arr.Length; i++)
                {
                    if (otherFields.IndexOf("," + arr[i].Trim().ToLower() + ",") == -1)
                    {
                        aspxStr += "    <%#Eval(\"" + arr[i].Trim() + "\")%>\r\n";
                    }
                }
            }
            aspxStr += "    </li>\r\n";
            aspxStr += "</ItemTemplate>\r\n";
            aspxStr += "</asp:Repeater>\r\n";
            
            csStr += "{\r\n";
            if (string.IsNullOrEmpty(all) && string.IsNullOrEmpty(fields))
            {
                jsMessage += "列表（最新N条）生成不成功，原因是未选择字段！\\r\\n";
                return;
            }
            if (all == "1")
            {
                strSql += "*";
            }
            else
            {
                strSql += fields;
            }
            strSql += " FROM " + TableName + " " + Request.Form["txtWhere1"] + " " + Request.Form["txtOrder"];
            csStr += strSql+"\";\r\n";
            csStr += "    DataTable dt = SQLHelper.GetDataSet(strSQL);\r\n";
            csStr += "    if (dt.Rows.Count > 0)\r\n";
            csStr += "    {\r\n";
            csStr += "      rptInfo.DataSource = dt;\r\n";
            csStr += "      rptInfo.DataBind();\r\n";
            csStr += "  }\r\n";
            csStr += "}";

            txtAspx1.Text = aspxStr;
            txtCs1.Text = csStr;
        }

        public string IsChecked1(string name)
        {
            string selectFields = ",ID,Title,AddDate,NodeCode,pubDate,publishdate,updatedate,titleimg,bigimg,smallimg,images,";

            return GetChecked(name,selectFields );
        }
        #endregion

        #region 分页
        public string IsChecked2(string name)
        {
            string selectFields = ",ID,Title,AddDate,NodeCode,pubDate,publishdate,updatedate,orders,titleimg,bigimg,smallimg,images,";

            return GetChecked(name, selectFields);
        }

        private void GetSplitDataCode()
        {
            string fields = Request.Form["chkField2"];  //选择字段
            string csStr = "void Data_Bind(string nc)";  //CS代码
            string aspxStr = "<ul class=\"news_ul pldq\">\r\n   <asp:Repeater ID=\"rptSplit\" runat=\"server\">\r\n";  //ASPX代码
            string strSql = "   strSQL=\"SELECT ";   //组装CS代码里的SQL语句
            string all = Request.Form["chkAll2"];       //是否全部
            string tableFields = hidFields.Value.ToLower();     //表的所有字段

            aspxStr += "    <ItemTemplate >\r\n";
            aspxStr += "        <li> <a href=\"<%#GetInfoUrl(Eval(\"ID\").ToString(), Eval(\"Title\").ToString(),Eval(\"AddDate\").ToString(),Eval(\"NodeCode\").ToString(),\"\")%>\">\r\n";
            if (tableFields.IndexOf(",pubdate,") != -1)
            {
                aspxStr += "        <span><%#Eval(\"pubDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }
            else if (tableFields.IndexOf(",publishdate,") != -1)
            {
                aspxStr += "        <span><%#Eval(\"publishDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }
            else if (tableFields.IndexOf(",updatedate,") != -1)
            {
                aspxStr += "        <span><%#Eval(\"updateDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }
            else
            {
                aspxStr += "        <span><%#Eval(\"AddDate\",\"{0:yyyy-MM-dd}\") %></span><%#Eval(\"Title\") %> </a>\r\n";
            }

            if (!string.IsNullOrEmpty(fields))
            {
                string[] arr = fields.Split(',');
                string otherFields = ",id,title,adddate,nodecode,pubdate,publishdate,updatedate,adddate,";
                for (int i = 0; i < arr.Length; i++)
                {
                    if (otherFields.IndexOf("," + arr[i].Trim().ToLower() + ",") == -1)
                    {
                        aspxStr += "        <%#Eval(\"" + arr[i].Trim() + "\")%>\r\n";
                    }
                }
            }
            aspxStr += "        </li>\r\n";
            aspxStr += "    </ItemTemplate>\r\n";
            aspxStr += "    </asp:Repeater>\r\n</ul>\r\n<div class=\"clear\"></div>\r\n<ul class=\"page\"><%=splitHtmlCode%></ul>";
            csStr += "{\r\n";
            if (string.IsNullOrEmpty(all) && string.IsNullOrEmpty(fields))
            {
                jsMessage += "列表（分页）生成不成功，原因是未选择字段！\\r\\n";
                return;
            }
            if (all == "1")
            {
                strSql += "*";
            }
            else
            {
                strSql += fields;
            }
            strSql += " FROM " + TableName + " " + Request.Form["txtWhere2"];
            csStr += strSql + "\";\r\n";
            csStr += "int pageSize = "+Request.Form["txtPageSize"]+";\r\n";
            csStr += "int pageIndex =1;\r\n";
            csStr += "int rsCount =0;\r\n";
            csStr += "string order = \"" + Request.Form["txtOrder2"] + "\";\r\n";
            csStr += "SqlParameter[] sqlParam;\r\n";
            csStr += "string splitTemp = Split.SplitHtmlCode;\r\n";
            csStr += "if (!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[\"pg\"]))\r\n";
            csStr += "{\r\n";
            csStr += "    pageIndex = Utils.ParseInt(HttpContext.Current.Request.QueryString[\"pg\"], 1);\r\n";
            csStr += "}\r\n";
            csStr += "sqlParam = new SqlParameter[]{\r\n";
            csStr += "    new SqlParameter(\"@NewPageIndex\",pageIndex),\r\n";
            csStr += "    new SqlParameter(\"@PageSize\", pageSize),\r\n";
            csStr += "    new SqlParameter(\"@order\", order),\r\n";
            csStr += "    new SqlParameter(\"@strSql\", strSQL)\r\n";
            csStr += "    };\r\n";
            csStr += "DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, \"proc_Pager1\", sqlParam);\r\n";
            csStr += "if (ds != null && ds.Tables.Count > 1)\r\n";
            csStr += "{\r\n";
            csStr += "    rptSplit.DataSource = ds.Tables[1];\r\n";
            csStr += "    rptSplit.DataBind();\r\n";
            csStr += "    rsCount = Utils.ParseInt(ds.Tables[0].Rows[0][0], 0);\r\n";
            csStr += "    {\r\n";
            csStr += "        splitHtmlCode = Split.GetHtmlCode(\"\", splitTemp, 3, pageIndex, pageSize, rsCount, false);\r\n";
            csStr += "    }\r\n";
            csStr += "}\r\n";
            txtAspx2.Text = aspxStr;
            txtCs2.Text = csStr;
        }
        #endregion

        #region 详细内容
        public string IsChecked3(string name)
        {
            string selectFields = ",ID,Title,AddDate,NodeCode,pubDate,publishdate,updatedate,titleimg,bigimg,smallimg,images,content,detail,";

            return GetChecked(name, selectFields);
        }
        private void GetDetailCode()
        {
            string fields = Request.Form["chkField3"];  //选择字段
            string all = Request.Form["chkAll3"];       //是否全部
            string tableFields = hidFields.Value.ToLower();     //表的所有字段
            #region Repeater绑定方式
            string csStr1 = @"if (!Page.IsPostBack)
            {
                string id = Request.QueryString[""id""];
                string nc = Request.QueryString[""nc""];
                
                if (string.IsNullOrEmpty(id))
                {
                    id = ""0"";
                }
                if (!Utils.IsNumber(id))
                {
                    id = ""0"";
                }
                if (id.Length > 50)
                {
                    id = id.Substring(0, 50);
                }";

            csStr1 += "\r\n sql=\"Select ";
            if (string.IsNullOrEmpty(all) && string.IsNullOrEmpty(fields))
            {
                jsMessage += "详细内容生成不成功，原因是未选择字段！\\r\\n";
                return;
            }
            if (all == "1")
            {
                csStr1 += "*";
            }
            else
            {
                csStr1 += fields;
            }
            csStr1 += " FROM " + TableName + "  where ID='\" + id + \"'\";\r\n";

            csStr1 += @"DataTable dt = SQLHelper.GetDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    nc = dt.Rows[0][""NodeCode""].ToString();
                    rptInfo.DataSource = dt;
                    rptInfo.DataBind();
                }

                //Left1.NodeCode = nc;  //左边导航
                //Nav1.NodeCode = nc;   //当前位置

                //GetPageHeadInfo(id,""+TableName+"");    //SEO信息 需要继承类
            }";  //CS代码
            csStr1 = csStr1.Replace("+TableName+", TableName);
            string strSql = "   strSQL=\"SELECT TOP " + Request.Form["txtTOPNum"] + " ";   //组装CS代码里的SQL语句

            string strAspx1 = @"<asp:Repeater ID=""rptInfo"" runat=""server"">
                <ItemTemplate >
                      <div class=""news_title""><%#Eval(""Title"") %></div>
                <h4 class=""news_date"">发布日期：<%#Eval(""AddDate"",""{0:yyyy-MM-dd}"") %></h4>
                <div class=""news_text"">
                	<%#Eval(""Content"") %>
                </div>
                </ItemTemplate>
                </asp:Repeater>";
            if (tableFields.IndexOf(",publishdate,") != -1)
            {
                strAspx1 = strAspx1.Replace("AddDate", "PublishDate");
            }
            else if (tableFields.IndexOf(",updatedate,") != -1)
            {
                strAspx1 = strAspx1.Replace("AddDate", "UpdateDate");
            }
            else if (tableFields.IndexOf(",pubdate,") != -1)
            {
                strAspx1 = strAspx1.Replace("AddDate", "PubDate");
            }
            if (!string.IsNullOrEmpty(fields))
            {
                string[] arr = fields.Split(',');
                string otherFields = ",id,title,adddate,nodecode,pubdate,publishdate,updatedate,adddate,content,";
                for (int i = 0; i < arr.Length; i++)
                {
                    if (otherFields.IndexOf("," + arr[i].Trim().ToLower() + ",") == -1)
                    {
                        strAspx1 += "    <%#Eval(\"" + arr[i].Trim() + "\")%>\r\n";
                    }
                }
            }
            txtAspx3_1.Text = strAspx1;
            txtCs3_1.Text = csStr1;
            #endregion

            #region 变量方式
            string strAspx2 = string.Empty;
            string strCs2 = string.Empty; ;
            if (string.IsNullOrEmpty(fields))
            {
                fields = tableFields;
            }
            string[] fieldArr = fields.Split(',');
            for (int i = 0; i < fieldArr.Length; i++)
            {
                strCs2 += "public string str" + fieldArr[i].Trim() + "=string.Empty;\r\n";
            }
            strCs2 += @"protected void Page_Load(object sender, EventArgs e)
                        {
                          string id = Request.QueryString[""id""];
                string nc = Request.QueryString[""nc""];
                
                if (string.IsNullOrEmpty(id))
                {
                    id = ""0"";
                }
                if (!Utils.IsNumber(id))
                {
                    id = ""0"";
                }
                if (id.Length > 50)
                {
                    id = id.Substring(0, 50);
                }";

            strCs2 += "\r\n str=\"Select ";
            if (string.IsNullOrEmpty(all) && string.IsNullOrEmpty(fields))
            {
                jsMessage += "详细内容生成不成功，原因是未选择字段！\\r\\n";
                return;
            }
            if (all == "1")
            {
                strCs2 += "*";
            }
            else
            {
                strCs2 += fields;
            }
            strCs2 += " FROM " + TableName + "  where ID='\" + id + \"'\";\r\n";

            strCs2 += @"DataTable dt = SQLHelper.GetDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                    ";
            for (int i = 0; i < fieldArr.Length; i++)
            {
                strCs2 += "str" + fieldArr[i].Trim() + "=dt.Rows[0][\"" + fieldArr[i].Trim() + "\"].ToString();\r\n";
                strAspx2 += "<%=str" + fieldArr[i].Trim() + "%>\r\n";
            }
            strCs2 += " }\r\n";

            //Left1.NodeCode = nc;  //左边导航
            //Nav1.NodeCode = nc;   //当前位置

            //GetPageHeadInfo(id,""+TableName+"");    //SEO信息 需要继承类
            strCs2 += " }\r\n";  //CS代码  
            strCs2 += "          }\r\n";

            txtCs3_2.Text = strCs2;
            txtAspx3_2.Text = strAspx2;
            #endregion
        }
        #endregion

        #region 添加
        private void GetAddCode()
        {
            string fields = Request.Form["chkField4"];  //选择字段
            string all = Request.Form["chkAll4"];       //是否全部
            string tableFields = hidFields.Value;     //表的所有字段
            string strAspx4 = string.Empty;
            string strCs4 = string.Empty;
            string strAspx4_2 = string.Empty;
            string strParam = string.Empty;
            string insertStr1 = "string sql = \"Insert Into " + TableName + "(";

            if (all == "1")
            {
                fields = tableFields;
            }
            string[] fieldArr = fields.Split(',');
            for (int i = 0; i < fieldArr.Length; i++)
            {
                strAspx4 += "<input type=\"text\" id=\"txt" + fieldArr[i].Trim() + "\" name=\"txt" + fieldArr[i].Trim() + "\" value=\"<%=s"+fieldArr[i].Trim()+"%>\" />\r\n";
                strCs4 += "string s" + fieldArr[i].Trim() + "=Server.HtmlEncode (Request.Form[\"txt" + fieldArr[i].Trim() + "\"]);\r\n";
                if (fieldArr[i].Trim().ToLower() != "id" && fieldArr[i].Trim().ToLower() != "orders")
                {
                    strParam += "    new SqlParameter (\"@" + fieldArr[i].Trim() + "\",s" + fieldArr[i].Trim() + "),\r\n";
                }
                strAspx4_2 += "<asp:TextBox ID=\"txt" + fieldArr[i] + "\" runat=\"server\"></asp:TextBox>\r\n";
            }

            string[] fieldArr2 = tableFields.Split(',');
            string filedsInsert = string.Empty;
            string filedsValues = ") VALUES(";
            for (int i = 0; i < fieldArr2.Length; i++)
            {
                if (string.IsNullOrEmpty(filedsInsert))
                {
                    filedsInsert = "[" + fieldArr2[i] + "]";
                }
                else
                {
                    filedsInsert += "," + "[" + fieldArr2[i] + "]";
                }
                switch (fieldArr2[i].Trim().ToLower())
                {
                    case "isdel":
                        filedsValues += "0,";
                        break;
                    case "adddate":
                    case "deltime":
                        filedsValues += "getdate(),";
                        break;
                    case "siteid":
                        filedsValues += SiteID + ",";
                        break;
                    case "flowstate":
                        filedsValues += "99,";
                        break;
                    case "id":
                        filedsValues += "@ID,";
                        break;
                    case "orders":
                        filedsValues += "@Orders,";
                        break;
                    default:
                        if (strParam.IndexOf("\"@" + fieldArr2[i].Trim() + "\",") != -1)
                        {
                            filedsValues += "@" + fieldArr2[i] + ",";
                        }
                        else
                        {
                            filedsValues += "NULL,";
                        }
                        break;
                }
            }
            filedsValues = filedsValues.Substring(0, filedsValues.Length - 1);
            insertStr1 += filedsInsert + filedsValues + ")\";\r\n";
            txtAspx4_1.Text = strAspx4;
            txtAspx4_2.Text = strAspx4_2;


            strCs4 += "KingTop.Web.Admin.AdminPage ap = new KingTop.Web.Admin.AdminPage();\r\n";
            strCs4 += "string[] arr = ap.GetTableID(\"0\", \"" + TableName + "\");\r\n";

            strCs4 += "SqlParameter[] para = new SqlParameter[] {\r\n";
            strCs4 += "    new SqlParameter (\"@ID\",arr[0]),\r\n";
            strCs4 += "    new SqlParameter (\"@Orders\",arr[1]),\r\n";
            strCs4 += strParam;
            strCs4 += "};\r\n";
            strCs4 += insertStr1;
            strCs4 += "SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, para);";

            txtCs4_1.Text = strCs4;

        }
        #endregion

        #region 编辑
        private void GetEditCode()
        {
            string fields = Request.Form["chkField4"];  //选择字段
            string all = Request.Form["chkAll4"];       //是否全部
            string tableFields = hidFields.Value;     //表的所有字段
            string strAspx4 = string.Empty;
            string strCs4 = string.Empty;
            string strAspx4_2 = string.Empty;
            string strParam = string.Empty;
            string insertStr1 = "string sql = @\"UPDATE " + TableName + "SET\r\n";

            if (all == "1")
            {
                fields = tableFields;
            }
            string[] fieldArr = fields.Split(',');
            string cs1 = string.Empty;
            string cs2 = @"
                string id = Request.QueryString[""id""];
                
                if (string.IsNullOrEmpty(id))
                {
                    id = ""0"";
                }
                if (!Utils.IsNumber(id))
                {
                    id = ""0"";
                }
                if (id.Length > 50)
                {
                    id = id.Substring(0, 50);
                }";

            cs2 += "\r\n sql=\"Select ";
            if (string.IsNullOrEmpty(all) && string.IsNullOrEmpty(fields))
            {
                jsMessage += "详细内容生成不成功，原因是未选择字段！\\r\\n";
                return;
            }
            if (all == "1")
            {
                cs2 += "*";
            }
            else
            {
                cs2 += fields;
            }
            cs2 += " FROM " + TableName + "  where ID='\" + id + \"'\";\r\n";

            cs2 += @"DataTable dt = SQLHelper.GetDataSet(sql);
                if (dt.Rows.Count > 0)
                {
                   ";

            string cs3 =cs2;
            string strAspx5 = string.Empty;
            for (int i = 0; i < fieldArr.Length; i++)
            {
                cs1 += "public string s" + fieldArr[i].Trim() + "=string.Empty;\r\n";
                cs2 += "s" + fieldArr[i].Trim() + "=dt.Rows[0][\"" + fieldArr[i].Trim() + "\"].ToString();\r\n";

                cs3 += "txt" + fieldArr[i].Trim() + ".Text=dt.Rows[0][\"" + fieldArr[i].Trim() + "\"].ToString();\r\n";
                strAspx4 += "<input type=\"text\" id=\"txt" + fieldArr[i].Trim() + "\" name=\"txt" + fieldArr[i].Trim() + "\" />\r\n";
                
                strCs4 += "string s" + fieldArr[i].Trim() + "=Server.HtmlEncode (Request.Form[\"txt" + fieldArr[i].Trim() + "\"]);\r\n";

                if (fieldArr[i].Trim().ToLower() != "id" && fieldArr[i].Trim().ToLower() != "orders")
                {
                    strParam += "    new SqlParameter (\"@" + fieldArr[i].Trim() + "\",s" + fieldArr[i].Trim() + "),\r\n";
                }
                strAspx4_2 += "<asp:TextBox ID=\"txt" + fieldArr[i] + "\" runat=\"server\"></asp:TextBox>\r\n";

                insertStr1 += "[" + fieldArr[i].Trim() + "]=@" + fieldArr[i].Trim() + ",\r\n";
            }
            cs2 += "};\r\n";
            strAspx4 = cs2 + "\r\n===========================================================================\r\n" + strAspx4;
            strAspx4 = cs1 + "\r\n===========================================================================\r\n" + strAspx4;
            strAspx4_2 = cs3 + "\r\n===========================================================================\r\n" + strAspx4_2;

            insertStr1 = insertStr1.Substring(0, insertStr1.Length - 3);
            insertStr1 += " WHERE ID=@ID\";\r\n";
            txtAspx5_1.Text = strAspx4;
            txtAspx5_2.Text = strAspx4_2;

            strCs4 += "SqlParameter[] para = new SqlParameter[] {\r\n";
            strCs4 += "    new SqlParameter (\"@ID\",id),\r\n";
            strCs4 += strParam;
            strCs4 += "};\r\n";
            strCs4 += insertStr1;
            strCs4 += "SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, para);";

            txtCs.Text = strCs4;

        }
        #endregion

        private string GetChecked(string name, string selectFields)
        {
            string reStr = string.Empty;
            if (selectFields.ToLower().IndexOf("," + name.ToLower () + ",") != -1)
            {
                reStr = "checked";
            }

            return reStr;
        }
        

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GetTopDataCode();
            GetSplitDataCode();
            GetDetailCode();
            GetAddCode();
            GetEditCode();
            if (!string.IsNullOrEmpty(jsMessage))
            {
                jsMessage = "alert({msg:\""+jsMessage +"\",title:\"操作提示\"});";
            }
        }
    }
}
