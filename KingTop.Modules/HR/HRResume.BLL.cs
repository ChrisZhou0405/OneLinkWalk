using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Reflection;
using KingTop.Common;
using KingTop.Modules.Entity;
using System.Data.SqlClient;
using System.Web;

namespace KingTop.Modules.BLL
{
    public class HRResume
    {
        #region GetPostData
        public Entity.HRResume GetPostData
        {
            get
            {
                Entity.HRResume model = new Entity.HRResume();
                model.Address = RequestStr("Address");
                model.Birthday = Utils.StrToDate(RequestStr("Birthday"));
                model.CardID = RequestStr("CardID");
                model.City = RequestStr("City");
                model.CompanyExpect = RequestStr("CompanyExpect");
                model.ComputerLevel = RequestStr("ComputerLevel");
                model.CurrentSalary = RequestStr("CurrentSalary");
                model.Degree = RequestStr("Degree");
                model.EducationalBackground = RequestStr("EducationalBackground");
                model.EMail = RequestStr("EMail");
                model.EnglishLevel = RequestStr("EnglishLevel");
                model.Gender = RequestStr("Gender");
                model.Height = Utils.ParseInt(RequestStr("Height"), 0);
                model.Hobbies = RequestStr("Hobbies");
                model.ID = Utils.ParseInt(RequestStr("ID"), 0);
                model.Industry = RequestStr("Industry");
                model.InformationWay = RequestStr("InformationWay");
                model.IsDel = Utils.ParseInt(RequestStr("IsDel"), 0);
                model.IsRead = RequestStr(("IsRead"), false);
                model.IsRoom = RequestStr(("IsRoom"), false);
                model.Marriage = RequestStr("Marriage");
                model.MemberName = RequestStr("MemberName");
                model.Mobile = RequestStr("Mobile");
                model.Nation = RequestStr("Nation");
                model.NativePlace = RequestStr("NativePlace");
                model.NodeCode = RequestStr("NodeCode");
                model.Photo = RequestStr("Photo");
                model.Post = RequestStr("Post");
                model.PostExpect = RequestStr("PostExpect");
                model.QQ = RequestStr("QQ");
                model.ReportDate = Utils.StrToDate(RequestStr("ReportDate"));
                model.RequiresSalary = RequestStr("RequiresSalary");
                model.SiteID = Utils.ParseInt(RequestStr("SiteID"), 0);
                model.SkillsExpertise = RequestStr("SkillsExpertise");
                model.Speciality = RequestStr("Speciality");
                model.Specialty = RequestStr("Specialty");
                model.Tel = RequestStr("Tel");
                model.Train = RequestStr("Train");
                model.Universities = RequestStr("Universities");
                model.UserName = RequestStr("UserName");
                model.Weight = Utils.ParseInt(RequestStr("Weight"), 0);
                model.WorkDescription = RequestStr("WorkDescription");
                model.WorkExperience = RequestStr("WorkExperience");
                model.WorkYear = Utils.ParseInt(RequestStr("WorkYear"), 0);
                model.ZipCode = RequestStr("ZipCode");

                return model;
            }
        }
        #endregion

        #region 接收表单或者参数值（Reqeust）
        private string RequestStr(string queryName)
        {
            //用户控件ID
            string UserControlID = HttpContext.Current.Request.Form["UserControlID"];

            if (string.IsNullOrEmpty(HttpContext.Current.Request.Form[queryName]) &&string.IsNullOrEmpty( HttpContext.Current.Request.Form[UserControlID+queryName]))
                return "";

            else
            {
                string v = string.IsNullOrEmpty(HttpContext.Current.Request.Form[UserControlID + queryName]) ? HttpContext.Current.Request.Form[queryName] : HttpContext.Current.Request.Form[UserControlID + queryName];
                return v;
            }
        }

        /// <summary>
        /// HttpContext.Current.Request[key]
        /// </summary>
        /// <param name="queryName">参数名</param>
        /// <param name="defaultStr">默认值</param>
        /// <returns></returns>
        private bool RequestStr(string queryName, bool defaultStr)
        {
            //用户控件ID
            string UserControlID = HttpContext.Current.Request.Form["UserControlID"];

            if (string.IsNullOrEmpty(HttpContext.Current.Request.Form[queryName]) && string.IsNullOrEmpty(HttpContext.Current.Request.Form[UserControlID + queryName]))
                return defaultStr;

            else
            {

                string v = string.IsNullOrEmpty(HttpContext.Current.Request.Form[UserControlID + queryName]) ? HttpContext.Current.Request.Form[queryName] : HttpContext.Current.Request.Form[UserControlID + queryName];
                return Utils.ParseBool(v);
            }
        }

        private int RequestStr(string queryName, int defaultStr)
        {
            //用户控件ID
            string UserControlID = HttpContext.Current.Request.Form["UserControlID"];

            if (string.IsNullOrEmpty(HttpContext.Current.Request.Form[queryName]) && string.IsNullOrEmpty(HttpContext.Current.Request.Form[UserControlID + queryName]))
                return defaultStr;
            else
            {
                string v = string.IsNullOrEmpty(HttpContext.Current.Request.Form[UserControlID + queryName]) ? HttpContext.Current.Request.Form[queryName] : HttpContext.Current.Request.Form[UserControlID + queryName];
                return Utils.ParseInt(v,defaultStr);
            }
        }
        #endregion

        #region 添加
        //添加成功返回影响行数和ID号码，失败返回失败错误描述
        //成功arr[0]=1;arr[1]=ID
        //失败arr[0]=错误描述,arr[1]=0
        public string[] CreateItem(Entity.HRResume model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            
            dic.Add("Address", model.Address.ToString());
            dic.Add("Birthday", model.Birthday.ToString());
            dic.Add("CardID", model.CardID.ToString());
            dic.Add("City", model.City.ToString());
            dic.Add("CompanyExpect", model.CompanyExpect.ToString());
            dic.Add("ComputerLevel", model.ComputerLevel.ToString());
            dic.Add("CurrentSalary", model.CurrentSalary.ToString());
            dic.Add("Degree", model.Degree.ToString());
            
            dic.Add("EducationalBackground", model.EducationalBackground.ToString());
            dic.Add("EMail", model.EMail.ToString());
            dic.Add("EnglishLevel", model.EnglishLevel.ToString());
            dic.Add("Gender", model.Gender.ToString());
            dic.Add("Height", model.Height.ToString());
            dic.Add("Hobbies", model.Hobbies.ToString());
            dic.Add("Industry", model.Industry.ToString());
            dic.Add("InformationWay", model.InformationWay.ToString());
            dic.Add("IsDel", model.IsDel.ToString());
            dic.Add("IsRead", model.IsRead.ToString());
            dic.Add("IsRoom", model.IsRoom.ToString());
            dic.Add("Marriage", model.Marriage.ToString());
            dic.Add("MemberName", model.MemberName.ToString());
            dic.Add("Mobile", model.Mobile.ToString());
            dic.Add("Nation", model.Nation.ToString());
            dic.Add("NativePlace", model.NativePlace.ToString());
            dic.Add("NodeCode", model.NodeCode.ToString());
            dic.Add("Photo", model.Photo.ToString());
            dic.Add("Post", model.Post.ToString());
            dic.Add("PostExpect", model.PostExpect.ToString());
            dic.Add("QQ", model.QQ.ToString());
            dic.Add("ReportDate", model.ReportDate.ToString());
            dic.Add("RequiresSalary", model.RequiresSalary.ToString());
            dic.Add("SiteID", model.SiteID.ToString());
            dic.Add("SkillsExpertise", model.SkillsExpertise.ToString());
            dic.Add("Speciality", model.Speciality.ToString());
            dic.Add("Specialty", model.Specialty.ToString());
            dic.Add("Tel", model.Tel.ToString());
            dic.Add("Train", model.Train.ToString());
            dic.Add("Universities", model.Universities.ToString());
            
            dic.Add("UserName", model.UserName.ToString());
            dic.Add("Weight", model.Weight.ToString());
            dic.Add("WorkDescription", model.WorkDescription.ToString());
            dic.Add("WorkExperience", model.WorkExperience.ToString());
            dic.Add("WorkYear", model.WorkYear.ToString());
            dic.Add("ZipCode", model.ZipCode.ToString());
            return InfoHelper.Add("K_HRResume", dic);
        }
        #endregion

        #region 修改
        public string Edit(Modules.Entity.HRResume paramsModel)
        {
            string reMsg = string.Empty;
            string sql = @"UPDATE K_HRResume SET
	UserName=@UserName,
	Nation=@Nation,
	Gender=@Gender,
	Birthday=@Birthday,
	CardID=@CardID,
	Weight=@Weight,
	Marriage=@Marriage,
	Height=@Height,
	Photo=@Photo,
	City=@City,
	NativePlace=@NativePlace,
	IsRoom=@IsRoom,
	WorkYear=@WorkYear,
	Universities=@Universities,
	Specialty=@Specialty,
	Degree=@Degree,
	EnglishLevel=@EnglishLevel,
	ComputerLevel=@ComputerLevel,
	Industry=@Industry,
	Post=@Post,
	CurrentSalary=@CurrentSalary,
	RequiresSalary=@RequiresSalary,
	Mobile=@Mobile,
	Tel=@Tel,
	EMail=@EMail,
	QQ=@QQ,
	Address=@Address,
	Hobbies=@Hobbies,
	Speciality=@Speciality,
	ZipCode=@ZipCode,
	InformationWay=@InformationWay,
	PostExpect=@PostExpect,
	CompanyExpect=@CompanyExpect,
	ReportDate=@ReportDate,
	WorkExperience=@WorkExperience,
	WorkDescription=@WorkDescription,
	SkillsExpertise=@SkillsExpertise,
	EducationalBackground=@EducationalBackground,
	Train=@Train,
	UpdateDate=getdate()
	WHERE ID=@ID";

            SqlParameter[] param = new SqlParameter[] {
                    new SqlParameter("ID",paramsModel.ID),
                    new SqlParameter("UserName",paramsModel.UserName),
                    new SqlParameter("Nation",paramsModel.Nation),
                    new SqlParameter("Gender",paramsModel.Gender),
                    new SqlParameter("Birthday",paramsModel.Birthday),
                    new SqlParameter("CardID",paramsModel.CardID),
                    new SqlParameter("Weight",paramsModel.Weight),
                    new SqlParameter("Marriage",paramsModel.Marriage),
                    new SqlParameter("Height",paramsModel.Height),
                    new SqlParameter("Photo",paramsModel.Photo),
                    new SqlParameter("City",paramsModel.City),
                    new SqlParameter("NativePlace",paramsModel.NativePlace),
                    new SqlParameter("IsRoom",paramsModel.IsRoom),
                    new SqlParameter("WorkYear",paramsModel.WorkYear),
                    new SqlParameter("Universities",paramsModel.Universities),
                    new SqlParameter("Specialty",paramsModel.Specialty),
                    new SqlParameter("Degree",paramsModel.Degree),
                    new SqlParameter("EnglishLevel",paramsModel.EnglishLevel),
                    new SqlParameter("ComputerLevel",paramsModel.ComputerLevel),
                    new SqlParameter("Industry",paramsModel.Industry),
                    new SqlParameter("Post",paramsModel.Post),
                    new SqlParameter("CurrentSalary",paramsModel.CurrentSalary),
                    new SqlParameter("RequiresSalary",paramsModel.RequiresSalary),
                    new SqlParameter("Mobile",paramsModel.Mobile),
                    new SqlParameter("Tel",paramsModel.Tel),
                    new SqlParameter("EMail",paramsModel.EMail),
                    new SqlParameter("QQ",paramsModel.QQ),
                    new SqlParameter("Address",paramsModel.Address),
                    new SqlParameter("Hobbies",paramsModel.Hobbies),
                    new SqlParameter("Speciality",paramsModel.Speciality),
                    new SqlParameter("ZipCode",paramsModel.ZipCode),
                    new SqlParameter("InformationWay",paramsModel.InformationWay),
                    new SqlParameter("PostExpect",paramsModel.PostExpect),
                    new SqlParameter("CompanyExpect",paramsModel.CompanyExpect),
                    new SqlParameter("ReportDate",paramsModel.ReportDate),
                    new SqlParameter("WorkExperience",paramsModel.WorkExperience),
                    new SqlParameter("WorkDescription",paramsModel.WorkDescription),
                    new SqlParameter("SkillsExpertise",paramsModel.SkillsExpertise),
                    new SqlParameter("EducationalBackground",paramsModel.EducationalBackground),
                    new SqlParameter("Train",paramsModel.Train)
            };
            try
            {
                reMsg = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param).ToString();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
        }
        #endregion

        #region 获取单条数据
        public Entity.HRResume GetItemByID(string ID)
        {
            string sSql = "select AddDate,Address,Birthday,CardID,City,CompanyExpect,ComputerLevel,CurrentSalary,Degree,DelTime,EducationalBackground,EMail,EnglishLevel,Gender,Height,Hobbies,ID,Industry,InformationWay,IsDel,IsRead,IsRoom,Marriage,MemberName,Mobile,Nation,NativePlace,NodeCode,Photo,Post,PostExpect,QQ,ReportDate,RequiresSalary,SiteID,SkillsExpertise,Speciality,Specialty,Tel,Train,Universities,UpdateDate,UserName,Weight,WorkDescription,WorkExperience,WorkYear,ZipCode from K_HRResume where @ID=ID";
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@ID", ID) };

            return GetResume(sSql,param);
        }

        public Entity.HRResume GetItemByID(string MemberName,int SiteID)
        {
            string sSql = "select AddDate,Address,Birthday,CardID,City,CompanyExpect,ComputerLevel,CurrentSalary,Degree,DelTime,EducationalBackground,EMail,EnglishLevel,Gender,Height,Hobbies,ID,Industry,InformationWay,IsDel,IsRead,IsRoom,Marriage,MemberName,Mobile,Nation,NativePlace,NodeCode,Photo,Post,PostExpect,QQ,ReportDate,RequiresSalary,SiteID,SkillsExpertise,Speciality,Specialty,Tel,Train,Universities,UpdateDate,UserName,Weight,WorkDescription,WorkExperience,WorkYear,ZipCode from K_HRResume where @MemberName=MemberName and SiteID=@SiteID";
            SqlParameter[] param = new SqlParameter[] {
                new SqlParameter ("@MemberName", MemberName),
                new SqlParameter ("@SiteID", SiteID)
            };

            return GetResume(sSql, param);
        }

        private Entity.HRResume GetResume(string sql, SqlParameter[] param)
        {
            Entity.HRResume model = new Entity.HRResume();
            using (DbDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param))
            {
                while (dr.Read())
                {
                    model.Address = dr["Address"].ToString();
                    model.Birthday = Utils.StrToDate(dr["Birthday"].ToString());
                    model.CardID = dr["CardID"].ToString();
                    model.City = dr["City"].ToString();
                    model.CompanyExpect = dr["CompanyExpect"].ToString();
                    model.ComputerLevel = dr["ComputerLevel"].ToString();
                    model.CurrentSalary = dr["CurrentSalary"].ToString();
                    model.Degree = dr["Degree"].ToString();
                    model.EducationalBackground = dr["EducationalBackground"].ToString();
                    model.EMail = dr["EMail"].ToString();
                    model.EnglishLevel = dr["EnglishLevel"].ToString();
                    model.Gender = dr["Gender"].ToString();
                    model.Height = Utils.ParseInt(dr["Height"].ToString(), 0);
                    model.Hobbies = dr["Hobbies"].ToString();
                    model.ID = Utils.ParseInt(dr["ID"].ToString(), 0);
                    model.Industry = dr["Industry"].ToString();
                    model.InformationWay = dr["InformationWay"].ToString();
                    model.IsRead = Utils.ParseBool(dr["IsRead"].ToString());
                    model.IsRoom = Utils.ParseBool(dr["IsRoom"].ToString());
                    model.Marriage = dr["Marriage"].ToString();
                    model.MemberName = dr["MemberName"].ToString();
                    model.Mobile = dr["Mobile"].ToString();
                    model.Nation = dr["Nation"].ToString();
                    model.NativePlace = dr["NativePlace"].ToString();
                    model.Photo = dr["Photo"].ToString();
                    model.Post = dr["Post"].ToString();
                    model.PostExpect = dr["PostExpect"].ToString();
                    model.QQ = dr["QQ"].ToString();
                    model.ReportDate = Utils.StrToDate(dr["ReportDate"].ToString());
                    model.RequiresSalary = dr["RequiresSalary"].ToString();
                    model.SkillsExpertise = dr["SkillsExpertise"].ToString();
                    model.Speciality = dr["Speciality"].ToString();
                    model.Specialty = dr["Specialty"].ToString();
                    model.Tel = dr["Tel"].ToString();
                    model.Train = dr["Train"].ToString();
                    model.Universities = dr["Universities"].ToString();
                    model.UserName = dr["UserName"].ToString();
                    model.Weight = Utils.ParseInt(dr["Weight"].ToString(), 0);
                    model.WorkDescription = dr["WorkDescription"].ToString();
                    model.WorkExperience = dr["WorkExperience"].ToString();
                    model.WorkYear = Utils.ParseInt(dr["WorkYear"].ToString(), 0);
                    model.ZipCode = dr["ZipCode"].ToString();
                }
            }
            return model;
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Modules.Pager pager)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table","K_HRResume"),             
                new SqlParameter("@Where",GetWhere(pager.DicWhere)),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","UpdateDate Desc"),                
                 new  SqlParameter("@isSql",0),
                new  SqlParameter("@strSql","")
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        string GetWhere(Dictionary<string, string> DicWhere)
        {
            string sql = "IsDel=0";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "Title")
                    {
                        string[] arr = kvp.Value.Split('|');
                        string title = arr[0];
                        string type = arr[1];
                        switch (type)
                        {
                            case "1":
                                sql += " AND UserName like '%" + title + "%'";
                                break;
                            case "3":
                                sql += " AND Universities like '%" + title + "%'";
                                break;
                            case "4":
                                sql += " AND Specialty like '%" + title + "%'";
                                break;
                            case "5":
                                sql += " AND City like '%" + title + "%'";
                                break;
                        }
                    }
                    else if (kvp.Key == "gender")
                    {
                        sql += " AND Gender = '" + kvp.Value+"'";
                    }
                    else if (kvp.Key == "startDegree")
                    {
                        sql += " AND Degree>='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "endDegree")
                    {
                        sql += " AND Degree<='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "StartWorkYear")
                    {
                        sql += " AND WorkYear>=" + kvp.Value;
                    }
                    else if (kvp.Key == "endWorkYeare")
                    {
                        sql += " AND WorkYear<=" + kvp.Value;
                    }
                    else if (kvp.Key == "endAge")
                    {
                        sql += " AND DATEADD(dd,1,BIRTHDAY)>DATEADD (yy,-" + kvp.Value + ",getdate())";
                    }
                    else if (kvp.Key == "startAge")
                    {
                        sql += " AND BIRTHDAY<=DATEADD (yy,-" + kvp.Value + ",getdate())";
                    }
                }
            }
            //System.Web.HttpContext.Current.Response.Write(sql);
            return sql;
        }
        #endregion

        #region 分页---应聘岗位
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param Name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData_ApplyJob(KingTop.Modules.Pager pager,int siteid)
        {
            SqlParameter[] prams = new SqlParameter[]{
                new SqlParameter("@Id","ID"),
                new SqlParameter("@Table","K_HRResume"),             
                new SqlParameter("@Where",""),
                new SqlParameter("@Cou","*"),
                new SqlParameter("@NewPageIndex",pager.PageIndex),
                new SqlParameter("@PageSize",pager.PageSize), 
                new SqlParameter("@order","AddDate Desc"),                
                new  SqlParameter("@isSql",1),
                new  SqlParameter("@strSql",GetSql(pager.DicWhere,siteid))
            };

            DataSet ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction,
                      CommandType.StoredProcedure, "proc_Pager", prams);
            pager.RecordCount = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            pager.PageData(ds.Tables[1]);
        }

        string GetSql(Dictionary<string, string> DicWhere,int siteid)
        {
            string sql = "SELECT B.ID,A.ID AS ResumeID,A.UserName,A.Gender,A.Birthday,A.WorkYear,A.Universities,A.IsRead,B.JobID,B.AddDate,B.Status,C.Title AS JobTitle From K_HRResume AS A Join K_HRApplyJob AS B ON A.ID=B.ResumeID Left Join K_HRJob AS C ON B.JobID=C.ID WHERE A.SiteID=" + siteid+" AND A.IsDel=0";
            string st = "1900-1-1";
            st = DicWhere["LastViewResume"];
            if (string.IsNullOrEmpty(st))
            {
                st = "1900-1-1";
            }
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "Title")
                    {
                        string[] arr = kvp.Value.Split('|');
                        string title = arr[0];
                        string type = arr[1];
                        switch (type)
                        {
                            case "1":
                                sql += " AND A.UserName like '%" + title + "%'";
                                break;
                            case "2":
                                sql += " AND C.Title like '%" + title + "%'";
                                break;
                            case "3":
                                sql += " AND A.Universities like '%" + title + "%'";
                                break;
                            case "4":
                                sql += " AND A.Specialty like '%" + title + "%'";
                                break;
                            case "5":
                                sql += " AND A.City like '%" + title + "%'";
                                break;
                        }
                    }
                    else if (kvp.Key == "gender")
                    {
                        sql += " AND A.Gender = '" + kvp.Value+"'";
                    }
                    else if (kvp.Key == "startDegree")
                    {
                        sql += " AND A.Degree>='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "endDegree")
                    {
                        sql += " AND A.Degree<='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "StartWorkYear")
                    {
                        sql += " AND A.WorkYear>=" + kvp.Value;
                    }
                    else if (kvp.Key == "endWorkYear")
                    {
                        sql += " AND A.WorkYear<=" + kvp.Value; 
                    }
                    else if (kvp.Key == "endAge")
                    {
                        sql += " AND DATEADD(dd,1,A.BIRTHDAY)>DATEADD (yy,-"+kvp.Value+",getdate())";
                    }
                    else if (kvp.Key == "startAge")
                    {
                        sql += " AND A.BIRTHDAY<=DATEADD (yy,-" + kvp.Value + ",getdate())";
                    }
                    else if (kvp.Key == "JobID")
                    {
                        sql += " AND C.ID=" + kvp.Value;
                    }
                    else if (kvp.Key == "ResumeType" && kvp.Value !="100")
                    {
                        sql += " AND B.Status=" + kvp.Value;
                    }
                    else if (kvp.Key == "ResumeType" && kvp.Value == "100")
                    {
                        sql += " AND B.AddDate>'" + st+"'";
                    }
                    else if (kvp.Key == "IsRead")
                    {
                        sql += " AND A.IsRead=" + kvp.Value;
                    }
                }
            }
            //System.Web.HttpContext.Current.Response.Write(sql);
            return sql;
        }
        #endregion

        #region 删除 将删除字段isdel值修改为1
        /// <summary>
        /// 成功返回1 失败返回错误描述
        /// </summary>
        /// <param name="idList">删除多条记录，id用“,”隔开，例如：1，2，3</param>
        /// <returns></returns>
        public string Delete(string idList)
        {
            if (string.IsNullOrEmpty(idList))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                string id = Utils.CheckSql(ArrID[i]);
                sql += "Update K_HRResume set IsDel=1,DelTime=getdate() where id=" + id + ";";  //删除自己和子节点记录
            }

            try
            {
                reMsg = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).ToString();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
        }
        #endregion

        #region 删除应聘岗位
        /// <summary>
        /// 成功返回1 失败返回错误描述
        /// </summary>
        /// <param name="idList">删除多条记录，id用“,”隔开，例如：1，2，3</param>
        /// <returns></returns>
        public string DelApplyJob(string idList)
        {
            if (string.IsNullOrEmpty(idList))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                string id = Utils.CheckSql(ArrID[i]);
                sql += "Delete K_HRApplyJob where id=" + id + ";";  //删除自己和子节点记录
            }

            try
            {
                reMsg = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).ToString();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
        }
        #endregion

        #region 转移
        /// <summary>
        /// 成功返回1 失败返回错误描述
        /// </summary>
        /// <param name="idList">删除多条记录，id用“,”隔开，例如：1，2，3</param>
        /// <returns></returns>
        public string Move(string idList,string MoveValue)
        {
            if (string.IsNullOrEmpty(idList)||string.IsNullOrEmpty (MoveValue))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                string id = Utils.CheckSql(ArrID[i]);
                sql += "Update K_HRApplyJob set JobID="+MoveValue +" where id=" + id + ";"; 
            }

            try
            {
                reMsg = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).ToString();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
        }
        #endregion

        #region 分类
        /// <summary>
        /// 成功返回1 失败返回错误描述
        /// </summary>
        /// <param name="idList">删除多条记录，id用“,”隔开，例如：1，2，3</param>
        /// <returns></returns>
        public string SetType(string idList, string MoveValue)
        {
            if (string.IsNullOrEmpty(idList) || string.IsNullOrEmpty(MoveValue))
                return "";

            string reMsg = string.Empty;
            string sql = string.Empty;
            string[] ArrID = idList.Split(',');
            for (int i = 0; i < ArrID.Length; i++)
            {
                string id = Utils.CheckSql(ArrID[i]);
                sql += "Update K_HRApplyJob set status=" + MoveValue + " where id=" + id + ";";
            }

            try
            {
                reMsg = SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql).ToString();
            }
            catch (Exception e)
            {
                reMsg = e.Message;
            }

            return reMsg;
        }
        #endregion

        #region 单条招聘的简历统计
        public int[] GetApplyTotal(int jobid, Dictionary<string, string> DicWhere, int siteid)
        {
            int[] Arr = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            string st = "1900-1-1";
            st = DicWhere["LastViewResume"];
            if (string.IsNullOrEmpty(st))
            {
                st = "1900-1-1";
            }
            string sql = "SELECT B.Status AS st,B.AddDate into #Temp From K_HRResume AS A Join K_HRApplyJob AS B ON A.ID=B.ResumeID Left Join K_HRJob AS C ON B.JobID=C.ID WHERE A.SiteID=" + siteid + " AND A.IsDel=0";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "Title")
                    {
                        string[] arr = kvp.Value.Split('|');
                        string title = arr[0];
                        string type = arr[1];
                        switch (type)
                        {
                            case "1":
                                sql += " AND A.UserName like '%" + title + "%'";
                                break;
                            case "2":
                                sql += " AND C.Title like '%" + title + "%'";
                                break;
                            case "3":
                                sql += " AND A.Universities like '%" + title + "%'";
                                break;
                            case "4":
                                sql += " AND A.Specialty like '%" + title + "%'";
                                break;
                            case "5":
                                sql += " AND A.City like '%" + title + "%'";
                                break;
                        }
                    }
                    else if (kvp.Key == "gender")
                    {
                        sql += " AND A.Gender = '" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "startDegree")
                    {
                        sql += " AND A.Degree>='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "endDegree")
                    {
                        sql += " AND A.Degree<='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "StartWorkYear")
                    {
                        sql += " AND A.WorkYear>=" + kvp.Value;
                    }
                    else if (kvp.Key == "endWorkYear")
                    {
                        sql += " AND A.WorkYear<=" + kvp.Value;
                    }
                    else if (kvp.Key == "endAge")
                    {
                        sql += " AND DATEADD(dd,1,A.BIRTHDAY)>DATEADD (yy,-" + kvp.Value + ",getdate())";
                    }
                    else if (kvp.Key == "startAge")
                    {
                        sql += " AND A.BIRTHDAY<=DATEADD (yy,-" + kvp.Value + ",getdate())";
                    }
                    else if (kvp.Key == "JobID")
                    {
                        sql += " AND C.ID=" + kvp.Value;
                    }
                    else if (kvp.Key == "IsRead")
                    {
                        sql += " AND A.IsRead=" + kvp.Value;
                    }
                    else if (kvp.Key == "ResumeType" && kvp.Value == "100")
                    {
                        sql += " AND B.AddDate>'" + st + "'";
                    }
                }
            }


            sql += @";
            DECLARE @ALL INT
            DECLARE @I1 INT
            DECLARE @I2 INT
            DECLARE @I3 INT
            DECLARE @I4 INT
            DECLARE @I5 INT
            DECLARE @I6 INT
            DECLARE @I7 INT
            SET @ALL=0
            SET @I1=0
            SET @I2=0
            SET @I3=0
            SET @I4=0
            SET @I5=0
            SET @I6=0
            SET @I7=0
            SELECT @ALL=Count(*) FROM #Temp;  --所有
            SELECT @I1=Count(*) FROM #Temp Where st=1;  --一般
            SELECT @I2=Count(*) FROM #Temp Where st=2;  --优秀
            SELECT @I3=Count(*) FROM #Temp Where st=3;  --面试
            SELECT @I4=Count(*) FROM #Temp Where st=4;  --录用
            SELECT @I5=Count(*) FROM #Temp Where st=10;  --不合格
            --回收站            
            SELECT @I6=Count(*) FROM #Temp Where st=11;  ";
            sql += @"
            SELECT @I7=Count(*) FROM #Temp Where AddDate>='" + st + "'";
            sql += @"DROP TABLE #Temp;
            SELECT @ALL,@I1,@I2,@I3,@I4,@I5,@I6,@I7
            ";

            SqlParameter param = new SqlParameter("@jobid", jobid);
            SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param);
            if (dr.Read())
            {
                Arr[0] = dr.GetInt32(0);
                Arr[1] = dr.GetInt32(1);
                Arr[2] = dr.GetInt32(2);
                Arr[3] = dr.GetInt32(3);
                Arr[4] = dr.GetInt32(4);
                Arr[5] = dr.GetInt32(5);
                Arr[6] = dr.GetInt32(6);
                Arr[7] = dr.GetInt32(7);
            }
            dr.Close();
            dr.Dispose();

            return Arr;
        }
        #endregion

        #region 应聘统计
        public DataTable GetApplyCount(Dictionary<string, string> DicWhere, int siteid)
        {
            string sql = "SELECT C.Title,C.ID,C.LastViewResume, B.Status AS st,B.AddDate into #Temp From K_HRResume AS A Join K_HRApplyJob AS B ON A.ID=B.ResumeID Left Join K_HRJob AS C ON B.JobID=C.ID WHERE A.SiteID=" + siteid + " AND A.IsDel=0";
            foreach (KeyValuePair<string, string> kvp in DicWhere)
            {
                if (!string.IsNullOrEmpty(kvp.Key) && !string.IsNullOrEmpty(kvp.Value))
                {
                    if (kvp.Key == "Title")
                    {
                        string[] arr = kvp.Value.Split('|');
                        string title = arr[0];
                        string type = arr[1];
                        switch (type)
                        {
                            case "1":
                                sql += " AND A.UserName like '%" + title + "%'";
                                break;
                            case "2":
                                sql += " AND C.Title like '%" + title + "%'";
                                break;
                            case "3":
                                sql += " AND A.Universities like '%" + title + "%'";
                                break;
                            case "4":
                                sql += " AND A.Specialty like '%" + title + "%'";
                                break;
                            case "5":
                                sql += " AND A.City like '%" + title + "%'";
                                break;
                        }
                    }
                    else if (kvp.Key == "gender")
                    {
                        sql += " AND A.Gender = '" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "startDegree")
                    {
                        sql += " AND A.Degree>='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "endDegree")
                    {
                        sql += " AND A.Degree<='" + kvp.Value + "'";
                    }
                    else if (kvp.Key == "StartWorkYear")
                    {
                        sql += " AND A.WorkYear>=" + kvp.Value;
                    }
                    else if (kvp.Key == "endWorkYear")
                    {
                        sql += " AND A.WorkYear<=" + kvp.Value;
                    }
                    else if (kvp.Key == "endAge")
                    {
                        sql += " AND DATEADD(dd,1,A.BIRTHDAY)>DATEADD (yy,-" + kvp.Value + ",getdate())";
                    }
                    else if (kvp.Key == "startAge")
                    {
                        sql += " AND A.BIRTHDAY<=DATEADD (yy,-" + kvp.Value + ",getdate())";
                    }
                    else if (kvp.Key == "IsRead")
                    {
                        sql += " AND A.IsRead=" + kvp.Value;
                    }
                }
            }


            sql += @";
            SELECT A.*,B.newco FROM (SELECT COUNT(ID) as co,ID,st FROM #Temp GROUP By st,ID) AS A LEFT jOIN 
            (SELECT COUNT(ID) as newco,ID FROM #Temp WHERE AddDate>IsNull(LastViewResume,'1900-1-1') GROUP BY ID) AS B
            ON A.ID=B.ID
            ";

            return SQLHelper.GetDataSet(sql);
        }
        #endregion


        #region 修改备注
        public string UpdateMemo(string idList, string Memo)
        {
            if (Memo.Length > 100)
            {
                Memo = Memo.Substring(0, 100);
            }
            string sql = "UPDATE K_HRResume Set Memo=@Memo Where ID in (SELECT * FROM DBO.FU_Split(@IDList,','))";
            SqlParameter[] param = new SqlParameter[] { 
            new SqlParameter ("@Memo",Memo),
            new SqlParameter ("@IDList",idList )
            };
            try
            {
                return SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, param).ToString ();
                
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        #endregion
    }
}