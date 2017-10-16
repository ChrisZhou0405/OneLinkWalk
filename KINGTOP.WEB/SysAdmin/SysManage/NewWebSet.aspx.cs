using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KingTop.Common;
using KingTop.BLL.SysManage;
using System.Data;
using KingTop.Web.Admin;
using System.Text;
using System.IO;
using KingTop.Template;

#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线

    作者:      陈顺
    创建时间： 2010年4月19日

    功能描述： 站点设置
 
// 更新日期        更新人      更新原因/内容
//
--===============================================================*/
#endregion

namespace KingTop.WEB.SysAdmin.SysManage
{
    public partial class NewWebSet : KingTop.Web.Admin.AdminPage
    {
        KingTop.BLL.SysManage.SysWebSite bll = new SysWebSite();
        KingTop.Model.SysManage.SysWebSite mode = new KingTop.Model.SysManage.SysWebSite();
        KingTop.BLL.TemplateProject bllProject = new KingTop.BLL.TemplateProject();
        string strTemplateID = string.Empty;
        private string RootNodeCode = string.Empty;
        private string RootParentNodeCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                strTemplateID = Request.QueryString["TemplateID"];
                if (string.IsNullOrEmpty(strTemplateID))
                {
                    strTemplateID = "16";   //企业站点
                }
                this.hiddenTemplateID.Value = strTemplateID;
                if (!strTemplateID.Equals(""))
                {
                    //根据模板ID得到模板名称
                    GetTempName(strTemplateID);
                }
                //绑定父站点列表

                BinParentSite();
            }
        }

        //根据模板ID得到模板名称
        protected void GetTempName(string strTemplateID)
        {
            WebSiteTemplate bllTemplate = new WebSiteTemplate();
            DataTable tmpdt = bllTemplate.GetList("ONE", Utils.getOneParams(strTemplateID));
            if (tmpdt.Rows.Count > 0)
            {
                this.lblTemplateName.Text = tmpdt.Rows[0]["TemplateName"].ToString();
            }
        }

        //绑定父站点列表

        private void BinParentSite()
        {
            DataTable dt = bll.GetList("ALL", Utils.getOneParams(""));
            if (dt.Rows.Count > 0)
            {
                //this.ddlParentSite.DataSource = dt.DefaultView;
                //this.ddlParentSite.DataTextField = "SiteName";
                //this.ddlParentSite.DataValueField = "SiteID";
                //this.ddlParentSite.DataBind();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlSiteTemplate.Items .Add (new ListItem (dt.Rows [i]["SiteName"].ToString (),dt.Rows [i]["SiteID"].ToString ()+"|"+dt.Rows [i]["Directory"].ToString ()));
                }
                //ddlSiteTemplate.DataSource = dt;
                //ddlSiteTemplate.DataTextField = "SiteName";
                //ddlSiteTemplate.DataValueField = "SiteID";
                //ddlSiteTemplate.DataBind();

                //有记录选中子站，无法更改

                this.rdoMain.Checked = false;
                this.rdoLeaf.Checked = true;
            }
            else
            {
                //没有记录选中主站，无法更改

                this.rdoMain.Checked = true;
                this.rdoLeaf.Checked = false;
                ////主站，无上级站点
                //ListItem ls = new ListItem("无上级站点", "0");//追加一项

                //this.ddlParentSite.Items.Insert(0, ls);
                //this.ddlParentSite.Enabled = false;
            }

            this.rdoMain.Enabled = false;
            this.rdoLeaf.Enabled = false;


        }


        #region 文件夹复制
        public int DirectoryName(string DirectoryPath)//获取文件夹名，截取“\”
        {
            int j = 0; char[] c = DirectoryPath.ToCharArray();
            for (int i = c.Length - 1; i >= 0; i--)//从后面截取
            {
                j = i;
                if (c[i] == '\\')
                {
                    break;//遇"\"调处,并返回"\"的位置
                }
            }
            return j + 1;
        }
        public bool CopyDirectory(string DirectoryPath, string DirAddress)//复制文件夹，
        {
            if (!Directory.Exists(DirectoryPath))
                return true;

            #region//递归
            try
            {
                string s = DirectoryPath.Substring(DirectoryName(DirectoryPath));//获取文件夹名
                if (Directory.Exists(DirAddress + "\\" + s))
                {
                    Directory.Delete(DirAddress + "\\" + s, true);//若文件夹存在，不管目录是否为空，删除
                    Directory.CreateDirectory(DirAddress + "\\" + s);//删除后，重新创建文件夹
                }
                else
                {
                    Directory.CreateDirectory(DirAddress + "\\" + s);//文件夹不存在，创建
                }
                DirectoryInfo DirectoryArray = new DirectoryInfo(DirectoryPath);
                FileInfo[] Files = DirectoryArray.GetFiles();//获取该文件夹下的文件列表
                DirectoryInfo[] Directorys = DirectoryArray.GetDirectories();//获取该文件夹下的文件夹列表
                foreach (FileInfo inf in Files)//逐个复制文件
                {
                    string fileContent = Utils.showFileContet(DirectoryPath + "\\" + inf.Name);
                    Utils.WriteFile(DirAddress + "\\" + s + "\\" + inf.Name, fileContent);

                    //File.Copy(DirectoryPath + "\\" + inf.Name, DirAddress + "\\" + s + "\\" + inf.Name);
                }
                foreach (DirectoryInfo Dir in Directorys)//逐个获取文件夹名称，并递归调用方法本身
                {
                    CopyDirectory(DirectoryPath + "\\" + Dir.Name, DirAddress + "\\" + s);
                }
            }
            catch (Exception ex)
            {
                Utils.RunJavaScript(this, "alert({msg:'保存站点失败：" + ex.Message.Replace("'", "\'").Replace("\r\n", "<br>").Replace("\r", "<br>") + "！',title:'提示信息'})");
                return false;
            }

            #endregion

            return true;
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            strTemplateID = this.hiddenTemplateID.Value;
            string returnMsg = string.Empty;
            string strSiteID = string.Empty;
            string strUserGropCode = string.Empty;
            string siteDir = txtDir.Text.ToString().ToLower().Trim();
            StringBuilder sbsql = new StringBuilder();
            #region 保存站点信息
            //得到站点设置
            if (Directory.Exists(Server.MapPath("/" + siteDir)) || siteDir == "main")
            {
                Utils.RunJavaScript(this, "alert({msg:'保存站点失败：文件夹名称已存在，请重新填写文件夹名称！',title:'提示信息'})");
                return;
            }
            if (chkIsDomain.Checked && txtSiteURL.Text.Trim() == "")
            {
                Utils.RunJavaScript(this, "alert({msg:'保存站点失败：网站域名必须填写！',title:'提示信息'})");
                return;
            }

            try
            {
                //在根目录下建立站点文件夹
                Directory.CreateDirectory(Server.MapPath("/" + siteDir));
            }
            catch (Exception ex)
            {
                Utils.RunJavaScript(this, "alert({msg:'保存站点失败："+ex.Message .Replace ("'","\'").Replace ("\r\n","<br>").Replace ("\r","<br>")+"',title:'提示信息'})");
                return;
            }

            mode.SiteName = Utils.HtmlDecode(txtSiteName.Text.ToString());
            mode.Directory = Utils.HtmlDecode(siteDir);
            mode.IsMainSite = rdoMain.Checked ? true : false;

            StringBuilder sb = new StringBuilder();
            sb.Append("TemlateID=").Append(strTemplateID);//模板ID
            //if (ddlSiteCode.SelectedValue != "0")
            //{
            //    sb.Append("&CharSet=").Append(ddlSiteCode.SelectedValue);//站点编码
            //}
            if (chkIsImportNode.Checked)
            {
                sb.Append("&IsImprotNode=").Append("True");//是否导入节点信息
            }
            else
            {
                sb.Append("&IsImprotNode=").Append("False");
            }
            //if (chkImportFormStyle.Checked)
            //{
            //    sb.Append("&IsImportFormStyle=").Append("True");//是否导入表单样式
            //}
            //else
            //{
            sb.Append("&IsImportFormStyle=").Append("False");
            //}
            if (chkIsDomain.Checked)
            {
                mode.SiteUrl = Utils.HtmlDecode(txtSiteURL.Text.ToString());
                sb.Append("&IsDomain=").Append("True");
            }
            else
            {
                mode.SiteUrl = "";
                sb.Append("&IsDomain=").Append("False");
            }
            mode.SettingsXML = sb.ToString();

            if (!SetSiteXML(siteDir))
            {
                return;
            }

            //保存站点设置
            strSiteID = bll.Save("NEW", mode);
            if (strSiteID.IndexOf("Error") != -1)
            {
                Utils.RunJavaScript(this, "alert({msg:'保存站点失败："+strSiteID.Replace ("'","\'").Replace ("\r\n","<br>").Replace ("\r","<br>").Replace ("Error:","")+"',title:'提示信息'})");
                DeleteDir(Server.MapPath("/" + siteDir));
                return;
            }
            //判断SiteID是否有值
            UpdateSiteId(siteDir, strSiteID);
            int intSiteid = Utils.ParseInt(strSiteID, 0);
            #endregion

            #region 新增一个用户组(strSiteID+"站点管理员"),该用户组属于站点管理员角色

            KingTop.BLL.SysManage.UserGroup UserGropBll = new UserGroup();
            KingTop.Model.SysManage.UserGroup UserGropMode = new KingTop.Model.SysManage.UserGroup();
            UserGropMode.UserGroupCode = Guid.NewGuid();
            UserGropMode.UserGroupName = strSiteID + "站点管理员";
            UserGropMode.SiteID = intSiteid;
            UserGropMode.IsParent = true;
            UserGropMode.NumCode = UserGropBll.GetList("GETMAXPNUMCODE", Utils.getOneParams("0")).Tables[0].Rows[0]["MaxCode"].ToString();
            UserGropMode.ParentNumCode = "0";
            UserGropMode.UserGroupOrder = 0;
            UserGropMode.InputDate = System.DateTime.Now;
            try
            {
                UserGropBll.Save("new", UserGropMode);
            }
            catch { }
            strUserGropCode = UserGropMode.UserGroupCode.ToString();
            #endregion

            #region 导入栏目节点设置  导入用户组权限
            //根据strTemplateID得到该模板下所有栏目

            KingTop.BLL.SysManage.WebSiteTemplateNode bllTmpNode = new WebSiteTemplateNode();
            KingTop.Model.SysManage.WebSiteTemplateNode modeTmpNode = new KingTop.Model.SysManage.WebSiteTemplateNode();
            KingTop.BLL.SysManage.ModuleNode bllModeNode = new ModuleNode();
            KingTop.Model.SysManage.ModuleNode modeNode = new KingTop.Model.SysManage.ModuleNode();
            DataTable dtTmpNode = bllTmpNode.GetList("WEBSITE", Utils.getOneParams(strTemplateID));
            if (dtTmpNode.Rows.Count > 0)
            {
                //当前ModelNode的最大NodeCode
                string strMaxNodeCode = string.Empty;
                //MaxNodeCode和100的差量

                int intSubNodeCode = 0;
                DataTable dtNodeCode = bllModeNode.GetList("MAXCODE", Utils.getOneParams("0"));
                if (dtNodeCode != null && dtNodeCode.Rows.Count == 1)
                {
                    strMaxNodeCode = dtNodeCode.Rows[0]["NodeCode"].ToString();
                }

                try
                {
                    intSubNodeCode = Convert.ToInt32(strMaxNodeCode) - 100 + 1;
                }
                catch { throw new Exception("获取当前最大节点数失败！"); }
                foreach (DataRow dr in dtTmpNode.Rows)
                {
                    modeNode.WebSiteID = intSiteid;
                    modeNode.NodeID = Guid.NewGuid();
                    modeNode.NodeCode = dr["NodeCode"].ToString();
                    modeNode.NodeName = dr["NodeName"].ToString();
                    modeNode.NodeType = dr["NodeType"].ToString();
                    modeNode.LinkURL = dr["LinkURL"].ToString();
                    modeNode.ParentNode = dr["ParentNode"].ToString();
                    modeNode.IsValid = Utils.ParseBool(dr["IsValid"].ToString());
                    modeNode.ModuleID = new Guid(dr["ModuleID"].ToString());
                    modeNode.NodelOrder = dr["NodelOrder"].ToString();
                    modeNode.NodelDesc = dr["NodelDesc"].ToString();
                    modeNode.NodelEngDesc = dr["NodelEngDesc"].ToString();
                    modeNode.IsSystem = Utils.ParseBool(dr["IsSystem"].ToString());
                    modeNode.IsWeb = Utils.ParseBool(dr["IsWeb"].ToString());
                    modeNode.ReviewFlowID = dr["ReviewFlowID"].ToString();
                    modeNode.IsContainWebContent = Utils.ParseBool(dr["IsContainWebContent"].ToString());
                    modeNode.IsLeftDisplay = Utils.ParseBool(dr["IsLeftDisplay"].ToString());

                    //NodeCode唯一
                    if (modeNode.NodeCode.Length > 3)
                    {
                        modeNode.NodeCode = (Utils.ParseInt(modeNode.NodeCode.Substring(0, 3), 0) + intSubNodeCode).ToString() + modeNode.NodeCode.Substring(3, modeNode.NodeCode.Length - 3);
                    }
                    else
                    {
                        modeNode.NodeCode = (Utils.ParseInt(modeNode.NodeCode.Substring(0, 3), 0) + intSubNodeCode).ToString();
                    }
                    if (modeNode.ParentNode != "0")
                    {
                        if (modeNode.ParentNode.Length > 3)
                        {
                            modeNode.ParentNode = (Utils.ParseInt(modeNode.ParentNode.Substring(0, 3), 0) + intSubNodeCode).ToString() + modeNode.ParentNode.Substring(3, modeNode.ParentNode.Length - 3);
                        }
                        else
                        {
                            modeNode.ParentNode = (Utils.ParseInt(modeNode.ParentNode.Substring(0, 3), 0) + intSubNodeCode).ToString();
                        }
                    }

                    //插入ModeNode表sql
                    sbsql.Append(GetInsertModeNodeSql(modeNode));
                    //得到插入用户组权限表的sql(根据modeNode.NodeID)
                    sbsql.Append(GetInsertUserGroupPermitSql(strUserGropCode, dr["ID"].ToString(), modeNode.NodeID.ToString()));
                }

            }
            //执行生成的sql
            try
            {
                SQLHelper.ExecuteNonQuery(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, sbsql.ToString());
            }
            catch (System.Exception exp)
            {
                throw new Exception(exp.Message);
            }
            #endregion

            #region 最后把生成的站点赋给当前账户

            //admin拥有所有站点的所有权限，不用新增数据
            if (GetLoginAccountId().ToString() != "0")
            {
                KingTop.BLL.SysManage.AccountSite AccountSiteBll = new AccountSite();
                KingTop.Model.SysManage.AccountSite AccountSiteModel = new KingTop.Model.SysManage.AccountSite();
                AccountSiteModel.UserID = Utils.ParseInt(base.GetLoginAccountId().ToString(), 0);
                AccountSiteModel.SiteID = intSiteid;
                AccountSiteModel.IsValid = true;
                try
                {
                    AccountSiteBll.Save("new", AccountSiteModel);
                }
                catch (System.Exception exp)
                {
                    throw new Exception(exp.Message);
                }
            }
            #endregion

            #region 最后把用户填的账户插入账户表并把生成的用户组赋给该账户(这里直接给当前登陆账户赋该网站管理员用户组)
            //admin拥有所有站点的所有权限，不用新增数据
            if (GetLoginAccountId().ToString() != "0")
            {
                KingTop.BLL.SysManage.UserRole UserRoleBll = new UserRole();
                KingTop.Model.SysManage.UserRole UserRoleModel = new KingTop.Model.SysManage.UserRole();
                UserRoleModel.UserGroupCode = new Guid(strUserGropCode);
                UserRoleModel.UserId = base.GetLoginAccountId();
                try
                {
                    UserRoleBll.Save("new", UserRoleModel);
                }
                catch (System.Exception exp)
                {
                    throw new Exception(exp.Message);
                }
            }


            //更新栏目缓存

            AppCache.Remove("ModeNodeAndModuleCache");
            ModuleNode objmodulenode = new ModuleNode();
            objmodulenode.GetModeNodeFromCache();

            AppCache.Remove("PublishNodeCache");
            objmodulenode.Publish_GetNodeFromCache();
            #region 创建一个系统默认的方案

            //创建默认方案
            bllProject.CreateDefaultProject(mode.Directory, NodeCode, modeNode.WebSiteID);
            #endregion

            #endregion

            #region 导入栏目
            string[] parSite = ddlSiteTemplate.SelectedValue.Split ('|');
            returnMsg = InsertSiteMenu(parSite[0], intSiteid.ToString(), siteDir, parSite[1]);
            #endregion

            #region 复制文件
            bool IsMenu = false;
            if (string.IsNullOrEmpty(returnMsg))
            {
                IsMenu = true;
                returnMsg = CopySiteFile(Server.MapPath("/" + parSite[1]), Server.MapPath("/" + siteDir), parSite[1], siteDir);
            }
            else
                returnMsg += "<br>" + CopySiteFile(Server.MapPath("/" + parSite[1]), Server.MapPath("/" + siteDir), parSite[1], siteDir);
            #endregion

            if (chkIsImportNode.Checked && IsMenu)
            {
                Response.Redirect("NewWebSetMenuEdit.aspx?NodeCode=" + NodeCode + "&parentNodeCode=" + RootNodeCode);
            }
            else
            {
                Utils.RunJavaScript(this, "alert({msg:'" + returnMsg + "',title:'提示信息'});NewSiteName='" + mode.SiteName.Replace("'", "\\'") + "';NewSiteID=" + modeNode.WebSiteID);
            }
        }

        #region 复制文件方法
        private string CopySiteFile(string forPath, string toPath,string forSiteDir,string toSiteDir)
        {
            string re=string.Empty ;
            if (!Directory.Exists(forPath))
                return "";

            if (chkIsCopyFiles.Checked == false)
            {
                return "";
            }

            string replaceExt="|aspx|cs|html|htm|ascx|css|";  //替换内容的文件类型
            string dirs="|" ;
            for (int i = 0; i < ddlSiteTemplate.Items.Count; i++)  //未选择的站点不能复制
            {
                if (ddlSiteTemplate.Items[i].Selected == false)
                {
                    dirs += ddlSiteTemplate.Items[i].Value.Split('|')[1]+"|";
                }
            }
            dirs += "sysadmin|config|App_Code|App_Data|App_GlobalResources|aspnet_client|bin|_IntoRecordLog|ClientBin|UploadFiles|Properties|" + toSiteDir+"|";
            string noCopyFiles = "|Global.asax|Global.asax.cs|KingTop.WEB.csproj|KingTop.WEB.csproj.user|SilverlightUploadService.asmx|Web.config|";

            try
            {
                string s = forPath.Substring(DirectoryName(forPath));//获取文件夹名
                if (dirs.ToLower().IndexOf("|"+s.ToLower()+"|")!=-1)   //不可以复制的文件夹
                   return "" ;

                if (!Directory.Exists(toPath + "\\" + s))
                {
                    Directory.CreateDirectory(toPath + "\\" + s);//删除后，重新创建文件夹
                }
                DirectoryInfo DirectoryArray = new DirectoryInfo(forPath);
                FileInfo[] Files = DirectoryArray.GetFiles();//获取该文件夹下的文件列表
                DirectoryInfo[] Directorys = DirectoryArray.GetDirectories();//获取该文件夹下的文件夹列表
                
                foreach (FileInfo inf in Files)//逐个复制文件
                {
                    if (noCopyFiles.ToLower().IndexOf ("|"+inf.Name.ToLower()+"|")!=-1)  //不用复制的文件
                        continue;

                    if (chkReplaceNodeCode.Checked == true || !string.IsNullOrEmpty(txtReplaceStr.Text))
                    {
                        string fileExt = string.Empty;
                        if(inf.Name.IndexOf (".")!=-1)
                        {
                            string[] fileExtArr = inf.Name.Split('.');
                            fileExt = fileExtArr[fileExtArr.Length - 1].ToLower();
                        }
                        if (replaceExt.IndexOf("|" + fileExt + "|")!=-1)
                        {
                            string fileContent = Utils.showFileContet(forPath + "\\" + inf.Name);
                            if (chkReplaceNodeCode.Checked)  //替换编码
                            {
                                fileContent = fileContent.Replace("\"" + RootParentNodeCode, "\"" + RootNodeCode);  //编码一般是赋值给变量
                                fileContent = fileContent.Replace("'" + RootParentNodeCode, "'" + RootNodeCode);
                            }
                            if (!string.IsNullOrEmpty(txtReplaceStr.Text))  //替换字符串
                            {
                                string[] strArr = Utils.strSplit (txtReplaceStr.Text,"\r\n");
                                for (int i = 0; i < strArr.Length; i++)
                                {
                                    if (strArr[i].IndexOf("|") == -1)
                                        continue;

                                    string[] strArr1 = strArr[i].Split('|');
                                    fileContent = fileContent.Replace(strArr1[0], strArr1[1]);
                                }
                            }
                            if (!string.IsNullOrEmpty(forSiteDir))
                            {
                                if (fileContent.IndexOf("Inherits=\"KingTop.WEB." + forSiteDir) != -1)
                                {
                                    fileContent = fileContent.Replace("Inherits=\"KingTop.WEB." + forSiteDir, "Inherits=\"KingTop.WEB." + toSiteDir);  //替换aspx、ascx文件的Inherits

                                }
                                else
                                {
                                    fileContent = fileContent.Replace("Inherits=\"KingTop.WEB", "Inherits=\"KingTop.WEB." + toSiteDir);  //替换aspx、ascx文件的Inherits
                                }

                                if (fileContent.IndexOf("namespace KingTop.WEB." + forSiteDir) != -1)
                                    fileContent = fileContent.Replace("namespace KingTop.WEB." + forSiteDir, "namespace KingTop.WEB." + toSiteDir);  //替换cs文件的namespace
                                else
                                    fileContent = fileContent.Replace("namespace KingTop.WEB", "namespace KingTop.WEB." + toSiteDir);  //替换cs文件的namespace
                            }
                            else
                            {
                                fileContent = fileContent.Replace("Inherits=\"KingTop.WEB", "Inherits=\"KingTop.WEB." + toSiteDir);  //替换aspx、ascx文件的Inherits
                                fileContent = fileContent.Replace("namespace KingTop.WEB", "namespace KingTop.WEB." + toSiteDir);  //替换cs文件的namespace
                            }

                            Utils.WriteFile(toPath + "\\" + s + "\\" + inf.Name, fileContent);
                        }
                        else
                        {
                            File.Copy(forPath + "\\" + inf.Name, toPath + "\\" +s+"\\"+ inf.Name);
                        }
                    }
                    else
                    {
                        File.Copy(forPath + "\\" + inf.Name, toPath + "\\" + s + "\\" + inf.Name);
                    }
                }
                foreach (DirectoryInfo Dir in Directorys)//逐个获取文件夹名称，并递归调用方法本身
                {
                    CopySiteFile(forPath + "\\" + Dir.Name, toPath + "\\" + s, forSiteDir, toSiteDir);
                }
            }
            catch (Exception ex)
            {
                re="复制文件报错：" + ex.Message.Replace("'", "\'").Replace("\r\n", "<br>").Replace("\r", "<br>") + "！";
            }
            return re;
        }
        #endregion

        #region 导入栏目方法
        private string InsertSiteMenu(string parentSiteId, string newSiteId,string newSiteDir,string parentSiteDir)
        {
            string re = string.Empty;

            if (chkIsImportNode.Checked == false)
                return "";

            string linkUrl;
            string SubDomain;
            if(string.IsNullOrEmpty (parentSiteDir))
            {
                linkUrl = "replace(replace((''/" + newSiteDir + "/''+LinkURL),''//'',''/''),''='+@ParentNodeCode+''',''='+@NodeCode+'''),";
                SubDomain = "replace(replace((''/" + newSiteDir + "/''+SubDomain),''//'',''/''),''='+@ParentNodeCode+''',''='+@NodeCode+'''),";
            }
            else
            {
                linkUrl = "replace(replace(replace(''/''+LinkURL,''/" + parentSiteDir + "/'',''/" + newSiteDir + "/''),''//'',''/''),''='+@ParentNodeCode+''',''='+@NodeCode+'''),";
                SubDomain = "replace(replace(replace(''/''+SubDomain,''/" + parentSiteDir + "/'',''/" + newSiteDir + "/''),''//'',''/''),''='+@ParentNodeCode+''',''='+@NodeCode+'''),";
            }
            string strSql = @"declare @NodeCode varchar(15)
                declare @ParentNodeCode varchar(15)
                declare @sqlStr varchar(max)
                declare @count int
                set @count=0
                select @NodeCode=nodecode from K_SysModuleNode where nodename='内容管理' and websiteid=" + newSiteId;
            strSql +=@"
                ;select @ParentNodeCode=nodeCode from K_SysModuleNode where nodename='"+txtRootNodeName .Text +"' and websiteid="+parentSiteId;
            strSql += @"
                set @sqlStr='
                insert into K_SysModuleNode 
                select 
                newid(),
                '''+@NodeCode+'''+Convert(varchar(50),right(nodecode,len(nodecode)-len('''+@ParentNodeCode+'''))),  --编码
                NodeName,
                NodeType,";
            strSql +=linkUrl;
            strSql += @"
                '''+@NodeCode+'''+Convert(varchar(50),right(ParentNode,len(ParentNode)-len('''+@ParentNodeCode+'''))),  --父编码
                ModuleID,
                IsLeftDisplay,
                IsValid,
                NodelOrder,
                NodelDesc,
                NodelEngDesc,
                NodelIcon,
                MouseOverImg,
                CurrentImg,
                IsSystem,
                IsWeb,
                " + newSiteId+",";
            strSql +=@"
                NodeDir,
                Tips,
                Meta_Keywords,
                Meta_Description,
                Custom_Content,
                DefaultTemplate,
                ListPageTemplate,
                ContentTemplate,
                EnableSubDomain,";
            strSql +=SubDomain;
            strSql += @"
                Settings,
                Creater,
                CreateDate,
                ReviewFlowID,
                OpenType,
                PurviewType,
                IsEnableComment,
                IsCreateListPage,
                IncrementalUpdatePages,
                IsEnableIndexCache,
                ListPageSavePathType,
                ListPagePostFix,
                IsCreateContentPage,
                ContentPageHtmlRule,
                AutoCreateHtmlType,
                Custom_Images,
                IsContainWebContent,
                ColumnType,
                IsCreateColumn,
                IsDel,
                CustomManageLink,
                PageTitle,
                DelTime,
                Banner,
                IsTopMenuShow,
                IsLeftMenuShow
                from K_SysModuleNode where left(nodecode,len('''+@ParentNodeCode+'''))='''+@ParentNodeCode+''' and nodecode<>'''+@ParentNodeCode+''''

                exec(@sqlStr)
                exec('select count(NodeCode) as col1,'''+@NodeCode+''' as col2,'''+@ParentNodeCode+''' as col3 from K_SysModuleNode where NodeCode like '''+@NodeCode+'%''')
                ";
            DataSet  ds = SQLHelper.ExecuteDataSet(SQLHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            DataTable dt=ds.Tables [ds.Tables.Count -1];
            
            if (dt.Rows.Count > 0)
            {
                if (Utils.ParseInt(dt.Rows[0][0].ToString(), 0) < 2)
                {
                    return "新建站点成功，但是栏目导入失败，你可以通过手动导入";
                }
                RootNodeCode = dt.Rows[0][1].ToString();
                RootParentNodeCode = dt.Rows[0][2].ToString();
            }
            else
            {
                if (Utils.ParseInt(dt.Rows[0][0].ToString(), 0) < 2)
                {
                    return "新建站点成功，但是栏目导入失败，你可以通过手动导入";
                }
            }

            return re;
        }
        #endregion

        private bool SetSiteXML(string strSiteDir)
        {
            //将主站点的config文件夹拷贝到子文件夹中
            DataTable dt = bll.GetList("Main", Utils.getOneParams(""));
            if (dt.Rows.Count > 0)
            {
                //需测试主站没有main情况
                string mainSiteDir = dt.Rows[0]["Directory"].ToString();
                if (!string.IsNullOrEmpty(mainSiteDir))
                    mainSiteDir+=mainSiteDir.Substring (mainSiteDir.Length -1)!="/"?"/":"";

                if (Directory.Exists(Server.MapPath("~/" + mainSiteDir + "config")))
                {
                    if (!CopyDirectory(Server.MapPath("~/" + mainSiteDir + "config"), Server.MapPath("/" + strSiteDir)))
                    {
                        DeleteDir(Server.MapPath("/" + strSiteDir));
                        return false;
                    }

                    //拷贝plus
                    CopyDirectory(Server.MapPath("~/" + mainSiteDir + "Plus"), Server.MapPath("/" + strSiteDir));
                    CopyDirectory(Server.MapPath("~/" + mainSiteDir + "IncludeFile"), Server.MapPath("/" + strSiteDir));
                    //File.Copy(Server.MapPath("~/" + mainSiteDir + "search.aspx"), Server.MapPath("/" + strSiteDir + "/search.aspx"));

                    //如果填写域名，则修改SiteInfo.config和SiteParam.config中的域名和目录
                    string xmlPath = Server.MapPath("/" + strSiteDir + "/config/SiteInfo.config");
                    string mainDomain = Utils.XmlRead(xmlPath, "/SiteInfoConfig/SiteURL", "");
                    if (!Utils.XmlUpdate(xmlPath, "/SiteInfoConfig/SiteURL", "", mode.SiteUrl))
                    {
                        Utils.RunJavaScript(this, "alert({msg:'保存站点失败:" + xmlPath + "没有修改权限！',title:'提示信息'})");
                        DeleteDir(Server.MapPath("/" + strSiteDir));
                        return false;
                    }
                    Utils.XmlUpdate(xmlPath, "/SiteInfoConfig/SiteDir", "", mode.Directory);
                    //Utils.XmlUpdate(xmlPath, "/SiteInfoConfig/SiteID", "", intSiteid.ToString());
                    Utils.XmlUpdate(xmlPath, "/SiteInfoConfig/SiteName", "", mode.SiteName);
                    Utils.XmlUpdate(xmlPath, "/SiteInfoConfig/SiteTitle", "", mode.SiteName);

                    xmlPath = Server.MapPath("/" + strSiteDir + "/config/SiteParam.config");
                    if (!Utils.XmlUpdate(xmlPath, "/SiteParamConfig/SiteRootURL", "", mainDomain))
                    {
                        Utils.RunJavaScript(this, "alert({msg:'保存站点失败:" + xmlPath + "没有修改权限！',title:'提示信息'})");
                        DeleteDir(Server.MapPath("/" + strSiteDir));
                        return false;
                    }
                    //Utils.XmlUpdate(xmlPath, "/SiteParamConfig/SiteID", "", intSiteid.ToString());
                    Utils.XmlUpdate(xmlPath, "/SiteParamConfig/SiteDir", "", mode.Directory);
                    Utils.XmlUpdate(xmlPath, "/SiteParamConfig/SiteDomain", "", mode.SiteUrl);
                    if (chkIsDomain.Checked) //启用独立域名，需要将web.config拷贝到目录中
                    {
                        File.Copy(Server.MapPath("~/web.config"), Server.MapPath("/" + strSiteDir + "/web.config"));
                    }
                }
            }

            return true;
        }

        private void UpdateSiteId(string strSiteDir, string intSiteid)
        {
            string xmlPath = Server.MapPath("/" + strSiteDir + "/config/SiteInfo.config");
            Utils.XmlUpdate(xmlPath, "/SiteInfoConfig/SiteID", "", intSiteid);
            xmlPath = Server.MapPath("/" + strSiteDir + "/config/SiteParam.config");
            Utils.XmlUpdate(xmlPath, "/SiteParamConfig/SiteID", "", intSiteid);
            
            //修改DWAPI校验码
            xmlPath = Server.MapPath("/sysadmin/Configuraion/SiteInfoManage.config");
            string siteDwManage = Utils.XmlRead(xmlPath, "SiteInfoManage/SiteDWMange", "");
            if (string.IsNullOrEmpty(siteDwManage))
            {
                siteDwManage = intSiteid + "," + mode.SiteName.Replace(",", "") + ",360hqb";
            }
            else
            {
                siteDwManage += "|" + intSiteid + "," + mode.SiteName.Replace(",", "") + ",360hqb";
            }
            Utils.XmlUpdate(xmlPath, "SiteInfoManage/SiteDWMange", "", siteDwManage);
        }

        //根据ModeNode实体得到插入K_SysModeNode的sql
        protected string GetInsertModeNodeSql(KingTop.Model.SysManage.ModuleNode mode)
        {
            string ReturnSql = string.Empty;
            ReturnSql = "INSERT INTO K_SysModuleNode(";
            ReturnSql += "NodeID,NodeCode,NodeName,NodeType,LinkURL,ParentNode,ModuleID,IsValid,";
            ReturnSql += "NodelOrder,NodelDesc,NodelEngDesc,NodelIcon,IsSystem,IsWeb,WebSiteID,";
            ReturnSql += "ReviewFlowID,IsContainWebContent,IsLeftDisplay)";
            ReturnSql += "VALUES('";
            ReturnSql += mode.NodeID + "','" + mode.NodeCode + "','" + mode.NodeName + "','" + mode.NodeType + "','" + mode.LinkURL + "','" + mode.ParentNode + "','" + mode.ModuleID + "','" + mode.IsValid + "','";
            ReturnSql += mode.NodelOrder + "','" + mode.NodelDesc + "','" + mode.NodelEngDesc + "','" + mode.NodelIcon + "','" + mode.IsSystem + "','" + mode.IsWeb + "','" + mode.WebSiteID + "','";
            ReturnSql += mode.ReviewFlowID + "','" + mode.IsContainWebContent + "','" + mode.IsLeftDisplay + "')";
            return ReturnSql;
        }

        private void DeleteDir(string path)
        {
            try
            {
                Directory.Delete(path);
            }
            catch { }
        }

        //根据ModeNode.ID和ModeNode.TemplateID得到K_WebSiteTemplatePermit的操作编码并生成插入K_SysUserGroupPermit表相关sql
        protected string GetInsertUserGroupPermitSql(string UserGropCode, string NodeID, string ModeNodeID)
        {
            string ReturnSql = string.Empty;
            //根据ModeNode.ID和ModeNode.TemplateID得到K_WebSiteTemplatePermit的操作编码

            KingTop.BLL.SysManage.WebSiteTemplatePermit WebSitePerBll = new WebSiteTemplatePermit();
            DataTable PerDt = WebSitePerBll.GetList("WEBSITENODE", Utils.getTwoParams(strTemplateID, NodeID));
            if (PerDt.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow dr in PerDt.Rows)
                    {
                        ReturnSql += "INSERT INTO K_SysUserGroupPermit(";
                        ReturnSql += "UserGroupCode,PermitCode,NodeID)";
                        ReturnSql += "VALUES('";
                        ReturnSql += UserGropCode + "','" + dr["PermitCode"] + "','" + ModeNodeID + "')";
                    }
                }
                catch { }
            }
            return ReturnSql;
        }

        #region 返回

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateWebSite.aspx?NodeCode=" + NodeCode);
        }
        #endregion
    }
}
