using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KingTop.Common;
using KingTop.Model;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using KingTop.WEB.Admin;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace KingTop.WEB.SysAdmin.Common
{
    /// <summary>
    /// AjaxCommon 的摘要说明
    /// </summary>
    public class AjaxCommon : BaseHandler
    {
        //public KingTop.WEB.SysAdmin MyPage = new BasePage();

        public override void ProcessRequest(HttpContext context)
        {
            string action = Utils.CheckSql(context.Request.Params["action"]);
            if (string.IsNullOrEmpty(action))
            {
                WritMesg("页面参数错误！");
            }
            switch (action)
            {
                //请求列表数据
                case "list":
                    {
                        if (!IsHaveRightByOperCode("Query"))
                        {
                            WritMesg("你没有查看权限，请联系站点管理员！");
                        }
                        else
                        {
                            GetList(context);
                        }
                        break;
                    }
                //根据ID获取数据
                case "getbyid":
                    {
                        GetById(context);
                        break;
                    }
                //根据ID获取数据
                case "bindflowstate":
                    {
                        GetFlowStateJson(context);
                        break;
                    }
                //删除
                case "del":
                    {
                        if (!IsHaveRightByOperCode("Delete"))
                        {
                            WritMesg("你没有删除权限，请联系站点管理员！");
                        }
                        else
                        {
                            DeleteById(context);
                        }
                        break;
                    }
                //批量通过审核
                case "check":
                    {
                        if (!IsHaveRightByOperCode("Check"))
                        {
                            WritMesg("你没有审核权限，请联系站点管理员！");
                        }
                        else
                        {
                            CheckById(context);
                        }
                        break;
                    }
                //批量取消审核
                case "cancelcheck":
                    {
                        if (!IsHaveRightByOperCode("CancelCheck"))
                        {
                            WritMesg("你没有取消审核权限，请联系站点管理员！");
                        }
                        else
                        {
                            CancelCheckById(context);
                        }
                        break;
                    }
                //添加
                case "add":
                    {
                        if (!IsHaveRightByOperCode("New"))
                        {
                            WritMesg("你没有添加权限，请联系站点管理员！");
                        }
                        else
                        {
                            AddData(context);
                        }
                        break;
                    }
                //编辑
                case "edit":
                    {
                        if (!IsHaveRightByOperCode("Edit"))
                        {
                            WritMesg("你没有修改权限，请联系站点管理员！");
                        }
                        else
                        {
                            EditData(context);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// 获取数据列表，带分页，带搜索
        /// </summary>
        /// <param name="context"></param>
        private void GetList(HttpContext context)
        {
            KingTop.Common.Pages page = new KingTop.Common.Pages();
            string title = Utils.CheckSql(context.Request.Params["sTitle"]);
            string username = Utils.CheckSql(context.Request.Params["sUserName"]);
            Dictionary<string, object> dt = new Dictionary<string, object>();
            int intPageSize = Convert.ToInt32(context.Request.Params["rows"]);
            int intCurrentPage = Convert.ToInt32(context.Request.Params["page"]);
            int RecordCount = 0;

            string strWhere = " isdel=0 and nodecode='" + NodeCode + "' ";
            if (title.Length > 0)
            {
                strWhere += " and title like '%" + title + "%'";
            }
            if (username.Length > 0)
            {
                strWhere += " and username='" + username + "'";
            }


            DataSet ds = page.GetPageList("K_U_Common ", "*", " orders desc ", strWhere, intCurrentPage, intPageSize, out RecordCount, 1);

            List<KingTop.Model.Common.Common> List = new List<KingTop.Model.Common.Common>();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                KingTop.Model.Common.Common model = new KingTop.Model.Common.Common();
                model.Id = item["Id"].ToString();
                model.Title = item["Title"].ToString();
                model.UserName = item["UserName"].ToString();
                if(item["BigImg"].ToString()!="")
                {
                     model.BigImg = "<img src='/UploadFiles/Images/" + item["BigImg"].ToString() + "' height='60'/>";
                }
                else
                {
                     model.BigImg = "<img src='/sysadmin/images/NoPic.jpg' height='60' />";
                }
                
                model.AddDate = Utils.GetStandardDateTime(item["AddDate"].ToString());
                model.FlowState = Utils.GetFlowState(item["FlowState"].ToString());
                model.Orders = Utils.ParseInt(item["Orders"].ToString(), 0);
                List.Add(model);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            dt.Add("total", RecordCount);
            dt.Add("rows", List.ToList<object>());//list.ToList<object>());
            context.Response.Write(js.Serialize(dt));
            context.Response.End();
        }

        /// <summary>
        /// 绑定审核状态数据:字符串拼接json格式，当值为空时，格式一定要为""，不然会报错
        /// </summary>
        /// <param name="context"></param>
        private void GetFlowStateJson(HttpContext context)
        {
            string json = "[{\"Id\":0,\"text\":\"请选择\",\"selected\":true},{\"Id\":99,\"text\":\"审核通过\"},{\"Id\":3,\"text\":\"取消审核\"}]";
            WritMesg(json);
        }

        /// <summary>
        /// 根据id读取一条信息
        /// </summary>
        /// <param name="context"></param>
        private void GetById(HttpContext context)
        {
            string id = context.Request.Params["id"];
            if (!string.IsNullOrEmpty(id))
            {
                string sql = "select Id,Title,UserName,BigImg,AddDate,FlowState,Orders,SiteID,detail from K_U_Common where Id='" + id + "'";
                DataTable dt = SQLHelper.GetDataSet(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    DataRow dr = dt.Rows[0];
                    #region 方法一：字符串拼接json格式，当值为空时，格式一定要为""，不然会报错
                    //string json = "{\"Id\":\"" + dr["Id"].ToString() + "\",\"Title\":\"" + dr["Title"].ToString() + "\",\"UserName\":\"" + dr["UserName"].ToString() + "\",\"BigImg\":\"" + dr["BigImg"].ToString() + "\",\"AddDate\":\"" + dr["AddDate"].ToString() + "\",\"FlowState\":\"" + dr["FlowState"].ToString() + "\",\"Orders\":\"" + dr["Orders"].ToString() + "\"}";
                    //context.Response.Write(json);
                    //context.Response.End();
                    #endregion

                    #region 方法二：实体模型通过Serialize方法转换为json格式
                    KingTop.Model.Common.Common model = new KingTop.Model.Common.Common();
                    model.Id = dr["Id"].ToString();
                    model.Title = dr["Title"].ToString();
                    model.UserName = dr["UserName"].ToString();
                    model.BigImg = dr["BigImg"].ToString();
                    model.FlowState = Utils.ParseInt(dr["FlowState"], 0).ToString();
                    model.Orders = Utils.ParseInt(dr["Orders"].ToString(), 0);
                    model.SiteID = Utils.ParseInt(dr["SiteID"].ToString(), 0);
                    model.Detail = dr["detail"].ToString();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    WritMesg(js.Serialize(model));
                    #endregion
                }
            }
        }

        /// <summary>
        /// 根据id删除信息，假删除，字段isdel
        /// </summary>
        /// <param name="context"></param>
        private void DeleteById(HttpContext context)
        {
            #region 存储过程处理proc_K_U_CommonSet
            KingTop.Model.pageModel.Json j = new KingTop.Model.pageModel.Json();
            SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
            returnValue.Direction = ParameterDirection.Output;

            string cmdText = "proc_K_U_CommonSet";
            string tranType = "FDEL";
            string IDList = Utils.CheckSql(context.Request.Params["id"]);
            string SetValue = "1";
            SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@IDList",IDList),
                    new SqlParameter("@SetValue",SetValue),
                    returnValue
                    };
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
            string isOk = returnValue.Value.ToString();

            if (isOk == "1")
            {
                j.success = true;
                j.msg = "删除成功";
            }
            #endregion

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(j));            
            context.Response.End();
        }

        /// <summary>
        /// 根据id审核通过一条信息
        /// </summary>
        /// <param name="context"></param>
        private void CheckById(HttpContext context)
        {
            #region 存储过程处理proc_K_U_CommonSet
            KingTop.Model.pageModel.Json j = new KingTop.Model.pageModel.Json();
            SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
            returnValue.Direction = ParameterDirection.Output;

            string cmdText = "proc_K_U_CommonSet";
            string tranType = "CHECK";
            string IDList = Utils.CheckSql(context.Request.Params["id"]);
            string SetValue = "99";
            SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@IDList",IDList),
                    new SqlParameter("@SetValue",SetValue),
                    returnValue
                    };
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
            string isOk = returnValue.Value.ToString();

            if (isOk == "1")
            {
                j.success = true;
                j.msg = "审核通过";
            }
            #endregion

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(j));            
            context.Response.End();
        }

        /// <summary>
        /// 根据id取消审核一条信息
        /// </summary>
        /// <param name="context"></param>
        private void CancelCheckById(HttpContext context)
        {
            #region 存储过程处理proc_K_U_CommonSet
            KingTop.Model.pageModel.Json j = new KingTop.Model.pageModel.Json();
            SqlParameter returnValue = new SqlParameter("@ReturnValue", SqlDbType.Int, 4);
            returnValue.Direction = ParameterDirection.Output;

            string cmdText = "proc_K_U_CommonSet";
            string tranType = "CANCELCHECK";
            string IDList = Utils.CheckSql(context.Request.Params["id"]);
            string SetValue = "3";
            SqlParameter[] paras = new SqlParameter[]{
                    new SqlParameter("@tranType", tranType),
                    new SqlParameter("@IDList",IDList),
                    new SqlParameter("@SetValue",SetValue),
                    returnValue
                    };
            SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.StoredProcedure, cmdText, paras);
            string isOk = returnValue.Value.ToString();

            if (isOk == "1")
            {
                j.success = true;
                j.msg = "取消审核";
            }
            #endregion

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(j));
            context.Response.End();
        }
       
        /// <summary>
        /// 添加一条信息
        /// </summary>
        /// <param name="context"></param>
        private void AddData(HttpContext context)
        {
            KingTop.Model.pageModel.Json j = new KingTop.Model.pageModel.Json();
            KingTop.Model.Common.Common model = new KingTop.Model.Common.Common();
            string[] arrIDOrders = KingTop.BLL.Public.GetTableID("0", "K_U_Common");//主键（ID）和排序数组
            
            //添加
            model.Id = arrIDOrders[0];
            model.Title = context.Request.Params["Title"];
            model.NodeCode = context.Request.Params["nodecode"]; 
            model.UserName = context.Request.Params["UserName"];
            model.BigImg = context.Request.Params["BigImg"];
            model.FlowState = Utils.ParseInt(context.Request.Params["FlowState"],3).ToString();
            model.AddDate = Utils.GetDateTime();
            model.Orders = Utils.ParseInt(context.Request.Params["Order"],0);
            model.SiteID = this.SiteID;
            model.Detail = context.Request.Params["TxtDetail"];
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into K_U_Common ");
            builder.Append("(Id,Title,NodeCode,UserName,BigImg,FlowState,AddDate,Orders,SiteID,Detail)");
            builder.Append(" values ");
            builder.Append("(@Id,@Title,@NodeCode,@UserName,@BigImg,@FlowState,@AddDate,@Orders,@SiteID,@Detail)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Id", SqlDbType.VarChar), 
                new SqlParameter("@Title", SqlDbType.NVarChar), 
                new SqlParameter("@NodeCode", SqlDbType.NVarChar), 
                new SqlParameter("@UserName", SqlDbType.NVarChar), 
                new SqlParameter("@BigImg", SqlDbType.NVarChar), 
                new SqlParameter("@FlowState", SqlDbType.Int), 
                new SqlParameter("@AddDate", SqlDbType.NVarChar),
                new SqlParameter("@Orders", SqlDbType.Int),
                new SqlParameter("@SiteID", SqlDbType.Int),
                new SqlParameter("@Detail", SqlDbType.NVarChar)
            };
            cmdParms[0].Value = model.Id;
            cmdParms[1].Value = model.Title;
            cmdParms[2].Value = model.NodeCode;
            cmdParms[3].Value = model.UserName;
            cmdParms[4].Value = model.BigImg;
            cmdParms[5].Value = model.FlowState;
            cmdParms[6].Value = model.AddDate;
            cmdParms[7].Value = model.Orders;
            cmdParms[8].Value = model.SiteID;
            cmdParms[9].Value = model.Detail;

            int isOk = SQLHelper.ExecuteCommand(builder.ToString(), CommandType.Text, cmdParms);
            if (isOk > 0)
            {
                j.success = true;
                j.msg = "添加成功";
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(j));
            context.Response.End();
        }

        

        /// <summary>
        /// 根据id修改一条信息
        /// </summary>
        /// <param name="context"></param>
        private void EditData(HttpContext context)
        {
            KingTop.Model.pageModel.Json j = new KingTop.Model.pageModel.Json();
            KingTop.Model.Common.Common model = new KingTop.Model.Common.Common();
            string id = context.Request.Params["id"];
            if (!string.IsNullOrEmpty(id))
            {
                model.Id = id;
                model.Title = context.Request.Params["Title"];
                model.NodeCode = context.Request.Params["nodecode"];
                model.UserName = context.Request.Params["UserName"];
                model.BigImg = context.Request.Params["BigImg"];
                model.FlowState = Utils.ParseInt(context.Request.Params["FlowState"], 3).ToString();
                model.Orders = Utils.ParseInt(context.Request.Params["Order"], 0);
                model.Detail = context.Request.Params["TxtDetail"];
                StringBuilder builder = new StringBuilder();
                builder.Append("update K_U_Common ");
                builder.Append(" set Title=@Title,NodeCode=@NodeCode,UserName=@UserName,BigImg=@BigImg,FlowState=@FlowState,Orders=@Orders,Detail=@Detail");
                builder.Append(" where id=@Id");
                SqlParameter[] cmdParms = new SqlParameter[] { 
                new SqlParameter("@Id", SqlDbType.VarChar), 
                new SqlParameter("@Title", SqlDbType.NVarChar), 
                new SqlParameter("@NodeCode", SqlDbType.NVarChar), 
                new SqlParameter("@UserName", SqlDbType.NVarChar), 
                new SqlParameter("@BigImg", SqlDbType.NVarChar), 
                new SqlParameter("@FlowState", SqlDbType.Int), 
                new SqlParameter("@Orders", SqlDbType.Int),
                new SqlParameter("@Detail", SqlDbType.NVarChar)
                };
                cmdParms[0].Value = model.Id;
                cmdParms[1].Value = model.Title;
                cmdParms[2].Value = model.NodeCode;
                cmdParms[3].Value = model.UserName;
                cmdParms[4].Value = model.BigImg;
                cmdParms[5].Value = model.FlowState;
                cmdParms[6].Value = model.Orders;
                cmdParms[7].Value = model.Detail;

                int isOk = SQLHelper.ExecuteCommand(builder.ToString(), CommandType.Text, cmdParms);
                if (isOk > 0)
                {
                    j.success = true;
                    j.msg = "修改成功";
                }
                else
                {
                    j.success = false;
                    j.msg = "修改失败";
                }
                
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(j));
            context.Response.End();
        }

    }
}