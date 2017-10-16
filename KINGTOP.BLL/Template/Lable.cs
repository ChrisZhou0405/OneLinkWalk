using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using KingTop.Common;
using System.Drawing;
using System.Drawing.Imaging;
#region 版权注释
/*================================================================
    Copyright (C) 2010 华强北在线
    作者:     
    创建时间： 
    功能描述： 标签管理/标签列表
 
// 更新日期        更新人      更新原因/内容
//2010-09-20      胡志瑶      添加CreateDwPlugins方法
--===============================================================*/
#endregion
namespace KingTop.BLL.Template
{
    public class Lable
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.Template.ILable dal = (IDAL.Template.ILable)Assembly.Load(path).CreateInstance(path + ".Template.Lable");

        /// <summary>
        /// 返回一个字符串
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public string GetObject(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            return dal.GetObject(tranType, modelPrams);
        }
        /// <summary>
        /// 删除标签分类
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public int DeleteLableClass(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            return dal.DeleteLableClass(tranType, modelPrams);
        }
        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public int DeleteLable(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            return dal.DeleteLable(tranType, modelPrams);
        }
        /// <summary>
        /// 获取标签数据
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public DataSet GetLable(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            return dal.GetLable(tranType, modelPrams);
        }
        /// <summary>
        /// 新增/修改标签分类
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveLableClass(string tranType, KingTop.Model.Template.LableClassInfo model)
        {


            return dal.SaveLableClass(tranType, model);
        }
        /// <summary>
        /// 新增/修改标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveLable(string tranType, KingTop.Model.Template.LableInfo model)
        {

            bool isExist = dal.IsExistLableName(model.Title.Trim(), model.SiteID, model.LableID);
            if (isExist)
            {
                return 2;
            }
            return dal.SaveLable(tranType, model);
        }
        /// <summary>
        /// 获取标签数据By站点ID
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public DataSet GetLabelContentBySiteId(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            return dal.GetLabelContentBySiteId(tranType, modelPrams);
        }
        /// <summary>
        /// 数据库，表信息
        /// </summary>
        /// <returns></returns>
        public IList<Model.SysManage.TableInfo> GetDbTable()
        {
            return dal.GetDbTable();
        }

        /// <summary>
        /// 自由标签
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="modelPrams"></param>
        /// <returns></returns>
        public DataSet GetLableFreeList(string tranType, KingTop.Model.SelectParams modelPrams)
        {
            return dal.GetLableFreeList(tranType, modelPrams);
        }

        /// <summary>
        /// 自由标签Model
        /// </summary>
        /// <param name="lableId"></param>
        /// <returns></returns>
        public KingTop.Model.Template.LableFreeInfo GetLableFreeInfo(int lableId)
        {
            DataTable dt = GetLableFreeList("LableFreeDetail", KingTop.Common.Utils.getOneParams(lableId.ToString())).Tables[0];
            Model.Template.LableFreeInfo model = new KingTop.Model.Template.LableFreeInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                model.AddDate = Convert.ToDateTime(dt.Rows[0]["AddDate"]);
                model.Description = dt.Rows[0]["Description"].ToString();
                model.LableName = dt.Rows[0]["LableName"].ToString();
                model.LabelSQL = dt.Rows[0]["LabelSQL"].ToString();
                model.LableContent = dt.Rows[0]["LableContent"].ToString();
                model.LableID = lableId;
                model.SiteID = Convert.ToInt32(dt.Rows[0]["SiteID"]);
                model.IsShare = Convert.ToInt32(dt.Rows[0]["IsShare"]);
                model.TempPrjID = dt.Rows[0]["TempPrjID"].ToString();
                model.ClassId = Convert.ToInt32(dt.Rows[0]["ClassId"]);
                model.Title = dt.Rows[0]["Title"].ToString();
                model.Identification = Convert.ToInt32(dt.Rows[0]["Identification"]);
                model.Sequence = Convert.ToInt32(dt.Rows[0]["Sequence"]);
            }
            return model;
        }

        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IList<KingTop.Model.SysManage.TableInfo> GetFields(string tableName)
        {
            return dal.GetFields(tableName);
        }

        /// <summary>
        /// 保存自由标签相关信息
        /// </summary>
        /// <param name="tranType"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SaveLableFree(string tranType, KingTop.Model.Template.LableFreeInfo model)
        {
            bool isExist = dal.IsExistLableName(model.Title, model.SiteID, model.LableID);
            if (isExist)
            {
                return 2;
            }
            return dal.SaveLableFree(tranType, model);
        }

        /// <summary>
        /// 删除自由标签
        /// </summary>
        /// <param name="lableId"></param>
        /// <returns></returns>
        public int DeleteLableFree(string lableId)
        {
            return dal.DeleteLableFree(lableId);
        }

        /// <summary>
        /// 执行SQL，返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecSqlString(string sql)
        {
            return dal.ExecSqlString(sql);
        }

        public string LableEnable(int isEnable, int lableid, int tableType)
        {
            return dal.LableEnable(isEnable, lableid, tableType);
        }

        #region 生成Dreamweaver插件 by 胡志瑶 2010-09-20
        /// <summary>   
        /// 生成Dreamweaver插件  
        /// </summary>
        /// <param name="rowIDs">选中的标签RowID</param>
        public string CreateDwPlugins(int siteID)
        {
            string rand = KingTop.Common.Rand.Str(5);  //产生随机数
            string path = HttpContext.Current.Server.MapPath("../template") + "\\" + rand + DateTime.Now.ToShortDateString();
            KingTop.Model.SelectParams prams = new KingTop.Model.SelectParams();
            prams.I2 = 0;
            prams.I3 = -1;
            prams.S3 = siteID.ToString();
            prams.S4 = " AND IsEnable=1";

            DataSet ds = new KingTop.BLL.Template.Lable().GetLable("LableList", prams);
            DataTable dt = ds.Tables[1];


            //生成DW扩展文件
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            CreateFile(path, "hqblable.mxi");
            StreamWriter sw = new StreamWriter(path + "\\hqblable.mxi", false, Encoding.UTF8);

            StringBuilder head = new StringBuilder();
            StringBuilder content = new StringBuilder();
            StringBuilder files = new StringBuilder();  //配置File的html
            StringBuilder tags = new StringBuilder();  //Dreamweaver中显示标签图片的xml
            head.Append("<macromedia-extension \r\n   ");
            head.Append("      name=\"华强北网络标签插件\" \r\n");
            head.Append("      version=\"1.0.0\"  \r\n");
            head.Append("      type=\"object\">\r\n");

            head.Append("      <author name=\"华强北在线\" />\r\n");
            head.Append("      <products>\r\n		");
            head.Append("              <product name=\"Dreamweaver\" version=\"8\" primary=\"true\" />\r\n");
            head.Append("      </products>\r\n");
            head.Append("      <description>	<![CDATA[	[这是由华强北在线自动生成的标签插件]]	]]>	</description>\r\n");
            head.Append("      <ui-access>\r\n	<![CDATA[\r\n	http://www.360HQB.com\r\n	]]>\r\n	</ui-access>\r\n");
            head.Append("      <configuration-changes>\r\n");

            content.Append("          <menu-insert insertAfter=\"DWMenu_Site\">\r\n");
            content.Append("          <menu name=\"HQB模板管理(_H)\" id=\"DWMenu_HQB_Site\" />\r\n");
            content.Append("          </menu-insert>\r\n");
            content.Append("          <menu-insert appendTo=\"DWMenu_HQB_Site\" skipSeparator=\"true\"> \r\n");
            content.Append("          <menuitem name=\"登陆HQB站点\" file=\"objects/HQB模板管理/登陆HQB站点.htm\" id=\"HQB_LoginSite\" />\r\n");
            content.Append("          </menu-insert>\r\n");
            content.Append("          <menu-insert insertAfter=\"HQB_LoginSite\">\r\n");
            content.Append("          <separator />\r\n");
            content.Append("          <menuitem name=\"获取模板\" file=\"objects/HQB模板管理/获取模板.htm\" id=\"HQB_GetSite\" />\r\n");
            content.Append("          <menuitem name=\"保存模板\" file=\"objects/HQB模板管理/保存模板.htm\" id=\"HQB_SaveSite\" />\r\n");
            content.Append("          </menu-insert>\r\n");


            //分类
            string[] strComuns = { "ClassName", "ClassID" };
            DataTable dtClass = (new DataView(dt)).ToTable(true, strComuns);
            bool isCreate = false;
            string imgName = string.Empty;  //生成的图片名
            string title = string.Empty;  //文件名

            //引入文件

            files.Append("        <files>  \r\n");
            files.Append("             <file name=\"HQB模板管理/登陆HQB站点.htm\" destination=\"$dreamweaver/configuration/objects/HQB模板管理\" /> \r\n");
            files.Append("             <file name=\"HQB模板管理/保存模板.htm\" destination=\"$dreamweaver/configuration/objects/HQB模板管理\" /> \r\n");
            files.Append("             <file name=\"HQB模板管理/获取模板.htm\" destination=\"$dreamweaver/configuration/objects/HQB模板管理\" /> \r\n");

            files.Append("             <file name=\"ThirdPartyTags/HQBTags.xml\" destination=\"$dreamweaver/configuration/ThirdPartyTags\" />");


            files.Append("             <file name=\"HQB模板管理/Site.txt\" destination=\"$dreamweaver/configuration/objects/HQB模板管理\" /> \r\n");
            files.Append("             <file name=\"HQB模板管理/dir.gif\" destination=\"$dreamweaver/configuration/objects/HQB模板管理\" /> \r\n");
            files.Append("             <file name=\"HQB模板管理/other.gif\" destination=\"$dreamweaver/configuration/objects/HQB模板管理\" /> \r\n");
            files.Append("             <file name=\"HQB模板管理/SystemLable.js\" destination=\"$dreamweaver/configuration/objects/HQB模板管理\" /> \r\n");
            CreateFile(path + "\\HQB模板管理", "Site.txt");   //管理登陆站点信息

            CopyFile("..\\LableAPI", path + "\\HQB模板管理", ""); //把LableAPI文件夹下的文件copy过去
            CreateFile(path + "\\ThirdPartyTags", "HQBTags.xml"); //产生Dreamweaver显示图片xml
            foreach (DataRow drClass in dtClass.Rows)
            {
                if (!System.IO.Directory.Exists(path + "\\" + drClass["ClassName"] + "")) //根据分类创建文件夹
                {
                    System.IO.Directory.CreateDirectory(path + "\\" + drClass["ClassName"] + "");
                }


                foreach (DataRow dr in dt.Rows)
                {
                    if (drClass["ClassID"].ToString() == dr["ClassID"].ToString())
                    {
                        isCreate = false;

                        string lblContent = dr["LableContent"].ToString();
                        title = dr["Title"].ToString();
                        string[] names = dr["LableName"].ToString().Substring(1).Split('_');
                        if (Convert.ToInt32(dr["IsSystem"]) == 1)
                        {
                            if (lblContent.IndexOf(" LableType=\"LIST\"") != -1)   //为系统标签中的通用列表标签     
                            {
                                if (lblContent.IndexOf("TableName=\"K_U_Commend\"") == -1) ////通用列表类型和专题列表类型 除幻灯片类型之外
                                {
                                    CreatePublicHTML(names[1], title, path + "\\" + drClass["ClassName"], dr["LableContent"].ToString(), "");
                                    isCreate = true;

                                }
                            }
                            else if (lblContent.IndexOf(" LableType=\"MENU\"") != -1 || lblContent.IndexOf(" LableType=\"NAV\"") != -1)  //系统标签中的栏目类型
                            {
                                CreateMenuHTML(names[1], title, path + "\\" + drClass["ClassName"], dr["LableContent"].ToString(), "");
                                isCreate = true;
                            }
                            else if (lblContent.IndexOf(" LableType=\"Category\"") != -1)  //类型导航标签
                            {
                                CreateCategoryHTML(names[1], title, path + "\\" + drClass["ClassName"], dr["LableContent"].ToString(), "");
                                isCreate = true;
                            }
                        }
                        if (!isCreate)
                        {
                            CreateHTML(title, dr["LableName"].ToString(), path + "\\" + drClass["ClassName"]);
                        }
                        imgName = "hqb." + dr["Title"].ToString() + ".gif";
                        CreateImg(path + "\\ThirdPartyTags", dr["Title"].ToString());  //创建Dreamweaver中标签显示的图片
                        files.Append("             <file name=\"ThirdPartyTags/" + imgName + "\" destination=\"$dreamweaver/configuration/ThirdPartyTags\" /> \r\n");
                        files.Append("             <file source=\"" + drClass["ClassName"] + "/" + title + ".htm\" destination=\"$dreamweaver/configuration/objects/" + drClass["ClassName"] + "\" /> \r\n");

                        tags.Append("<directive_spec tag_name=\"HQB_" + title + "\" start_string=\"{HQB_" + names[1] + "\" end_string=\"" + (isCreate ? "{/HQB_" + names[1] : "") + "}\" detect_in_attribute=\"true\" icon=\"" + imgName + "\" icon_width=\"83\" icon_height=\"22\"/>\r\n");

                    }
                }

                content.Append("          <insertbar-changes>\r\n");
                content.Append("              <insertbar-insert insertAfter=\"DW_Insertbar_Favorites\">\r\n");
                content.Append("                  <category MMString:name=\"" + drClass["ClassName"] + "\" folder=\"" + drClass["ClassName"] + "\" id=\"DWBar_HQB_" + drClass["ClassName"] + "\">\r\n");
                content.Append("                      <button file=\"objects/" + drClass["ClassName"] + "/" + title + ".htm\" image=\"ThirdPartyTags/sf_label.gif\" id=\"DWBar_HQB_" + title + "\" name=\"" + drClass["ClassName"] + "\" /> \r\n");
                content.Append("                  </category>\r\n");
                content.Append("              </insertbar-insert>\r\n");
                content.Append("         </insertbar-changes> \r\n");
            }
            files.Append("        </files>\r\n");
            content.Append("      </configuration-changes>\r\n");
            content.Append("</macromedia-extension>");
            sw.WriteLine(head.ToString() + files.ToString() + content.ToString());
            sw.Close();


            StreamWriter imgXml = new StreamWriter(path + "\\ThirdPartyTags\\HQBTags.xml");
            imgXml.WriteLine(tags.ToString());
            imgXml.Close();
            return path;

        }
        //产生处理标签的html文件（不带参数页面）
        private void CreateHTML(string title, string lableName, string path)
        {
            CreateFile(path, title + ".htm");
            StreamWriter sw = new StreamWriter(path + "\\" + title + ".htm");
            StringBuilder str = new StringBuilder();
            str.Append("<html>\r\n");
            str.Append("<head>\r\n");

            str.Append("    <script language='javascript'>\r\n");
            str.Append("             function isDOMRequired() {  \r\n");
            str.Append("                return false;\r\n");
            str.Append("             }\r\n");
            str.Append("             function objectTag(){\r\n");

            str.Append("                dw.getDocumentDOM().source.wrapSelection('" + lableName + "','');\r\n");
            str.Append("                 return;\r\n");
            str.Append("             }\r\n");
            str.Append("    </script>\r\n");
            str.Append("</head>\r\n");
            str.Append("</html>");
            sw.WriteLine(str.ToString());
            sw.Close();
        }
        //生成通用列表类型的html

        private void CreatePublicHTML(string name, string fileName, string path, string content, string jsPath)
        {
            CreateFile(path, fileName + ".htm");
            StreamWriter sw = new StreamWriter(path + "\\" + fileName + ".htm");
            StringBuilder str = new StringBuilder();
            str.Append("<!DOCTYPE HTML SYSTEM \"-//Macromedia//DWExtension layout-engine 5.0//dialog\">\r\n");
            str.Append("<html>\r\n");
            str.Append("<head>\r\n");
            str.Append(" <meta http-equiv=\"Content-Type\"  content=\"text/html; charset=utf-8\"  />\r\n");
            str.Append("<title>插入" + fileName + "</title>\r\n");

            if (string.IsNullOrEmpty(jsPath))
                str.Append(" <script src=\"../HQB模板管理/SystemLable.js\" type=\"text/javascript\"></script>");
            else
                str.Append(" <script src=\"" + jsPath + "\" type=\"text/javascript\"></script>");

            str.Append("<script type=\"text/javascript\">\r\n");
            str.Append("function commandButtons() {\r\n");
            str.Append("return new Array(\"插入标签\", \"objectTag(0)\", \"取消\", \"window.close()\")\r\n}\r\n");
            str.Append("function windowDimensions() { return \"500,320\"; }\r\n");
            str.Append("</script>");
            str.Append("</head>\r\n");
            str.Append("<body>\r\n");
            str.Append("<form name=\"theform\" method=\"post\" action=\"\">\r\n");

            Regex reg;
            MatchCollection collectMatch;
            reg = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.IgnoreCase);
            collectMatch = reg.Matches(content);

            str.Append("<input type='hidden' id='hidName' value='" + name + "_" + fileName + "'/><table width=\"450\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\">\r\n");
            string value = ""; //属性值
            string dataValue = string.Empty;  //日期
            string readValue = string.Empty; //导读
            string picValue = string.Empty;//标题图片
            string moreTagValue = string.Empty; //更多链接
            string subModel = string.Empty;//子模型

            KingTop.BLL.SysManage.Module module = new KingTop.BLL.SysManage.Module();
            DataTable oneModule;
            string moduleNmae = string.Empty;

            foreach (Match m in collectMatch)
            {
                value = m.Groups[2].ToString().Trim();

                switch (m.Groups[1].ToString())
                {

                    case "TableName":
                        oneModule = module.GetList("ONEBYTABLENAME", KingTop.Common.Utils.getOneParams(value));
                        moduleNmae = oneModule.Rows.Count > 0 ? oneModule.Rows[0]["ModuleName"].ToString() : "";  //模型名称
                        str.Append("<tr><td align=\"right\"> 模型</td><td><select id='TableName'><option value='" + value + "' selected=\"selected\">" + moduleNmae + "</option></select></td></tr>\r\n");
                        break;
                    case "Menu":
                        str.Append("<tr><td align=\"right\"> 所属栏目</td><td>");
                        if (content.IndexOf(" TableName=") != -1)  //通用列表类型
                        {
                            DataTable menuDt = new KingTop.BLL.SysManage.ModuleNode().GetList("LISTBYNODECODE", Utils.getOneParams(Utils.ConvertString(value))); //栏目名称
                            str.Append("<select id='Menu' multiple=\"multiple\" style=\"width: 200px; height: 90px\"> <option value=''> --请选择--</option>");
                            foreach (DataRow modDr in menuDt.Rows)
                            {
                                str.Append("<option value='" + modDr["NodeCode"] + "'>" + modDr["NodeName"] + "</option> ");
                            }
                        }
                        else  //专题列表类型
                        {
                            //DataTable dtMenu = new KingTop.BLL.SysManage.SpecialMenu().GetList("ONE", Utils.getOneParams(value));
                            //str.Append("<select id='Menu' style=\"width: 200px;\"> <option value=''> --请选择--</option>");
                            //foreach (DataRow modDr in dtMenu.Rows)
                            //{
                            //    str.Append("<option value='" + modDr["ID"] + "'>" + modDr["Name"] + "</option> ");
                            //}
                        }
                        str.Append("</select>(不选则为当前发布栏目)</td></tr>\r\n");
                        break;
                    case "PageSize":
                        str.Append("<tr><td align=\"right\"> 文章(或分页）数量</td><td><input type='text' id='PageSize' style='width: 45px' maxlength='2' value='" + value + "'/> </td></tr>\r\n");
                        break;
                    case "IsSplit":
                        str.Append("<tr><td align=\"right\"> 是否分页</td><td><input id='IsSplit' type='checkbox' " + (value == "1" ? "checked='checked'" : "") + "/> </td></tr>\r\n");
                        break;
                    case "SqlOrder":
                        str.Append("<tr><td align=\"right\"> 排序方式</td><td><input type='text' id='SqlOrder' value='" + value + "'/> </td></tr>\r\n");
                        break;
                    case "SqlWhere":
                        str.Append("<tr><td align=\"right\"> 查询条件</td><td><input type='text' id='SqlWhere' value='" + value + "'/> </td></tr>\r\n");
                        break;
                    case "IsSubModel":
                        subModel += "<input id='IsSubModel' type='checkbox' " + (value == "1" ? "checked='checked'" : "") + "/>";
                        break;
                    case "SubModelCTemplate":
                        subModel += "&nbsp;&nbsp;子模型内容模板：<input type='text' id='SubModelCTemplate' value='" + value + "'/>";
                        break;
                    case "TitleLength":
                        str.Append("<tr><td align=\"right\"> 标题显示字数</td><td><input type='text' id='TitleLength' value='" + value + "' style='width: 45px' maxlength='2'/> </td></tr>\r\n");
                        break;
                    case "TitleCssClass":
                        str.Append("<tr><td align=\"right\"> 标题样式</td><td><input type='text' id='TitleCssClass' value='" + value + "'/> </td></tr>\r\n");
                        break;
                    case "Container":
                        str.Append("<tr><td align=\"right\"> 输出类型</td><td><select id='Container'> <option value='div' " + (value == "div" ? "selected='selected'" : "") + ">div</option> <option value='li'  " + (value == "li" ? "selected='selected'" : "") + ">li</option></select></td></tr>\r\n");
                        break;
                    case "DateFormat":
                        dataValue += "格式：&nbsp;<input type='text' id='DateFormat' value='" + value + "'/>&nbsp;&nbsp;";
                        break;
                    case "DataCssClass":
                        dataValue += "<br/>样式：&nbsp;<input type='text' id='DataCssClass' value='" + value + "'/>&nbsp;&nbsp;";
                        break;
                    case "BriefCssClass":
                        readValue += "样式：&nbsp;<input type='text' id='BriefCssClass' value='" + value + "'/>&nbsp;&nbsp;";
                        break;
                    case "BriefLength":
                        readValue += "显示字数：&nbsp;<input type='text' id='BriefLength' value='" + value + "' style='width: 45px' maxlength='2'/>&nbsp;&nbsp;";
                        break;
                    case "TitleImageWidth":
                        picValue += "图片宽*高：&nbsp;<input type='text' id='TitleImageWidth' value='" + value + "' style='width: 45px' maxlength='2'/>";
                        break;
                    case "TitleImageHeight":
                        picValue += " * <input type='text' id='TitleImageHeight' value='" + value + "' style='width: 45px' maxlength='2'/>&nbsp;&nbsp;";
                        break;
                    case "TitleImageCount":
                        picValue += "图片显示个数：&nbsp;<input type='text' id='TitleImageCount' value='" + value + "' style='width: 45px' maxlength='2'/>&nbsp;&nbsp;";
                        break;
                    case "MoreLinkIsWord":
                        moreTagValue += "<input type='text' id='MoreLinkIsWord' value='" + (value == "0" ? "文字" : "图片") + "' style='width: 45px' disabled=\"disabled\"/>&nbsp;&nbsp;";
                        break;
                    case "MoreLinkWordOrImageUrl":
                        moreTagValue += "<input type='text' id='MoreLinkWordOrImageUrl' value='" + value + "' disabled=\"disabled\"/>&nbsp;&nbsp;";
                        break;

                    case "TitleSplitImage":
                        str.Append("<tr><td align=\"right\"> 标题分隔图片</td><td><input type='text' id='TitleSplitImage' value='" + value + "' style='width:200px'/> </td></tr>\r\n");
                        break;
                    case "LineHeight":
                        str.Append("<tr><td align=\"right\"> 文章行距</td><td><input type='text' id='LineHeight' value='" + value + "' style='width: 45px' maxlength='2'/> </td></tr>\r\n");
                        break;
                    case "Target":
                        str.Append("<tr><td align=\"right\"> 输出类型</td><td><select id='Target'> <option value='1' " + (value == "1" ? "selected='selected'" : "") + ">新窗口 _blank</option> <option value='0'  " + (value == "0" ? "selected='selected'" : "") + ">本窗口 _self</option></select></td></tr>\r\n");
                        break;

                }




            }
            if (subModel != string.Empty)
            {
                str.Append("<tr><td align=\"right\"> 是否子模型</td><td>" + subModel + "</td></tr>\r\n");
            }
            if (dataValue != string.Empty)
            {
                str.Append("<tr><td align=\"right\">日期</td><td><input type=\"checkbox\" id=\"IsShowAddDate\" checked=\"checked\" disabled=\"disabled\"/>显示&nbsp;&nbsp;" + dataValue + "</td></tr>\r\n");
            }
            if (readValue != string.Empty)
            {
                str.Append("<tr><td align=\"right\">导读（简介）</td><td> <input type=\"checkbox\" id=\"IsShowBrief\" checked=\"checked\" disabled=\"disabled\" />显示&nbsp;&nbsp;" + readValue + "</td></tr>\r\n");
            }
            if (picValue != string.Empty)
            {
                str.Append("<tr><td align=\"right\">是否显标题图片</td><td> <input type=\"checkbox\" id=\"IsShowTitleImage\" checked=\"checked\" disabled=\"disabled\" />显示&nbsp;&nbsp;" + picValue + "</td></tr>\r\n");
            }
            if (moreTagValue != string.Empty)
            {
                str.Append("<tr><td align=\"right\">更多链接</td><td><input type=\"checkbox\" id=\"IsShowMoreLink\" checked=\"checked\" disabled=\"disabled\"/>显示&nbsp;&nbsp;" + moreTagValue + "</td></tr>\r\n");
            }

            //匹配循环的内容
            reg = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']LIST[""'](?<4>[^}]*)\}(?<2>.*?)\{/HQB_\k<1>\s*\}", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match match = reg.Match(content);
            if (match.Groups[2].Value.Trim() != "")
            {
                str.Append("<tr><td align=\"right\">内容</td><td><textarea style=\"width:350px\" rows=\"10\" wrap=\"off\" id=\"txtContent\">" + Utils.HtmlEncode(match.Groups[2].Value).Replace("[", "::[::").Replace("]", "::]::") + "</textarea></td></tr>");
            }

            str.Append("</table>\r\n");
            str.Append("</form>\r\n");
            str.Append("<script type=\"text/javascript\">resetContent('txtContent');</script>");
            str.Append("</body>\r\n");

            str.Append("</html>");
            sw.WriteLine(str.ToString());
            sw.Close();
        }

        //复制已有的html
        private void CopyFile(string orginalPath, string path, string fileName)
        {
            if (!System.IO.Directory.Exists(path)) //根据分类创建文件夹
            {
                System.IO.Directory.CreateDirectory(path);
            }

            if (fileName == "")  //复制文件夹中的文件（不需要遍历其中的文件夹）
            {
                DirectoryInfo directory = new DirectoryInfo(HttpContext.Current.Server.MapPath(orginalPath));
                DirectoryInfo[] Directorys = directory.GetDirectories();//获取该文件夹下的文件夹列表

                FileInfo[] fileInfo = directory.GetFiles(); //目录下的文件 
                foreach (FileInfo fInfo in fileInfo)
                {
                    try
                    {
                        File.Copy(fInfo.FullName, path + "\\" + fInfo.Name, true);
                    }
                    catch { }
                }

                foreach (DirectoryInfo dir in Directorys)//逐个获取文件夹名称，并递归调用方法本身
                {
                    CopyFile(orginalPath + "\\" + dir.Name, path + "\\" + dir.Name, "");
                }
            }
            else
            {
                FileInfo newFile = new FileInfo(path + "\\" + fileName);
                if (!newFile.Exists)
                {
                    string orginalFile = orginalPath + "\\" + fileName;

                    File.Copy(HttpContext.Current.Server.MapPath(orginalFile), path + "\\" + fileName, true);
                }

            }
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fileName">文件名</param>
        private void CreateFile(string path, string fileName)
        {
            if (!System.IO.Directory.Exists(path)) //根据分类创建文件夹
            {
                System.IO.Directory.CreateDirectory(path);
            }
            FileInfo createFile = new FileInfo(path + "\\" + fileName + "");
            if (!createFile.Exists)
            {
                FileStream fs = createFile.Create();
                fs.Close();
            }
        }
        //生成栏目类型的html
        private void CreateMenuHTML(string name, string fileName, string path, string content, string jsPath)
        {
            CreateFile(path, fileName + ".htm");
            string value = ""; //属性值
            string bothMenu = string.Empty;  //二级栏目
            string isNavTagWord = string.Empty; //导航模式
            StreamWriter sw = new StreamWriter(path + "\\" + fileName + ".htm");
            StringBuilder str = new StringBuilder();
            int level = 0;
            str.Append("<!DOCTYPE HTML SYSTEM \"-//Macromedia//DWExtension layout-engine 5.0//dialog\">\r\n");
            str.Append("<html>\r\n");
            str.Append("<head>\r\n");
            str.Append(" <meta http-equiv=\"Content-Type\"  content=\"text/html; charset=utf-8\"  />\r\n");
            str.Append("<title>插入" + fileName + "</title>\r\n");


            if (string.IsNullOrEmpty(jsPath))
                str.Append(" <script src=\"../HQB模板管理/SystemLable.js\" type=\"text/javascript\"></script>\r\n");
            else
                str.Append(" <script src=\"" + jsPath + "\" type=\"text/javascript\"></script>\r\n");

            str.Append("<script type=\"text/javascript\">\r\n");
            str.Append("function commandButtons() {\r\n");
            str.Append("return new Array(\"插入标签\", \"objectTag(1)\",  \"取消\", \"window.close()\")\r\n}\r\n");
            str.Append("function windowDimensions() { return \"500,250\"; }\r\n");
            str.Append("</script>\r\n");
            str.Append("</head>\r\n");
            str.Append("<body>\r\n");
            str.Append("<form name=\"theform\" method=\"post\" action=\"\">\r\n");

            str.Append("<input type='hidden' id='hidName' value='" + name + "_" + fileName + "'/><table width=\"450\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\">\r\n");



            Regex regLabel;
            Regex regAttribute;
            string selfCode;
            MatchCollection collectAttribute;
            Match matchLabel;

            regLabel = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']MENU[""'](?<4>[^}]*)\}(?<2>.*?)\{/HQB_\k<1>\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regAttribute = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']*)[""']", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            collectAttribute = regAttribute.Matches(content);

            #region 获得html
            foreach (Match item in collectAttribute)
            {
                value = item.Groups[2].Value;
                switch (item.Groups[1].Value)
                {
                    case "Type":
                        switch (value)
                        {
                            case "1":
                                value = "网站总栏目导航";
                                break;
                            case "2":
                                value = "栏目页子菜单";
                                break;
                            default:
                                value = "网站位置导航";
                                break;
                        }
                        str.Append("<tr><td align=\"right\"> 标签类型</td><td><select id='Type'> <option value='" + item.Groups[2].Value + "' selected='selected'>" + value + "</option> </select></td></tr>\r\n");
                        break;
                    case "NodeCode":
                        DataTable moduleOne = new KingTop.BLL.SysManage.ModuleNode().GetList("LISTBYNODECODE", KingTop.Common.Utils.getOneParams("'" + value + "'")); //栏目名称
                        if (moduleOne.Rows.Count > 0)
                        {
                            str.Append("<tr><td align=\"right\"> 所属栏目</td><td><select id='NodeCode'> <option value='" + value + "' selected='selected'>" + moduleOne.Rows[0]["NodeName"] + "</option> </select></td></tr>\r\n");
                        }
                        break;
                    case "IsBothMenu":
                        bothMenu += "<input id='IsBothMenu' readonly=\"readonly\"  type='checkbox' " + (value == "1" ? "checked='checked'" : "") + "/>&nbsp;&nbsp;";
                        break;
                    case "ShowType":
                        bothMenu += "显示方式 <select id='ShowType'> <option value='1' " + (value == "1" ? "selected='selected'" : "") + ">横向</option> <option value='2'  " + (value == "2" ? "selected='selected'" : "") + ">纵向</option></select>";
                        break;
                    case "IsWordMenu":
                        str.Append("<tr><td align=\"right\"> 导航类型</td><td> <select id='IsWordMenu'> <option value='1' " + (value == "1" ? "selected='selected'" : "") + ">文字类型</option> <option value='0'  " + (value == "0" ? "selected='selected'" : "") + ">图片类型</option></select></td></tr>\r\n");
                        break;
                    case "CurrentCssType":
                        str.Append("<tr><td align=\"right\"> 当前栏目样式</td><td><input type='text' id='CurrentCssType' value='" + value + "'/> </td></tr>\r\n");
                        break;
                    case "CssClass":
                        str.Append("<tr><td align=\"right\"> 文字样式</td><td><input id=\"CssClass\" type=\"text\"  value=\"" + value + "\" /></td></tr>\r\n");
                        break;
                    case "Target":
                        str.Append("<tr><td align=\"right\"> 打开方式</td><td> <select id='Target'> <option value='1' " + (value == "1" ? "selected='selected'" : "") + ">当前窗口打开</option> <option value='2'  " + (value == "2" ? "selected='selected'" : "") + ">新窗口打开</option></select></td></tr>\r\n");
                        break;
                    case "IsNavTagWord":
                        isNavTagWord = " <select id='IsNavTagWord'><option value='" + value + "' selected='selected'>" + (value == "1" ? "文字" : "图片") + "</option></select>&nbsp;&nbsp;";
                        break;
                    case "NavTagContent":
                        isNavTagWord += "<input type='text' id='NavTagContent' value='" + value + "'/>&nbsp;&nbsp;";
                        break;
                    case "SubMenuType":
                        str.Append("<tr><td align=\"right\"> 栏目显示类型</td><td>");
                        str.Append("<select id=\"SubMenuType\">");
                        str.Append(value == "1" ? "<option value=\"1\" selected>显示当前栏目子栏目</option>" : "<option value=\"1\">显示当前栏目子栏目</option>");
                        str.Append(value == "2" ? "<option value=\"2\" selected>显示当前栏目及兄弟栏目（子栏目）</option>" : "<option value=\"2\">显示当前栏目及兄弟栏目（子栏目）</option>");
                        str.Append(value == "3" ? "<option value=\"3\" selected>显示当前栏目顶级栏目下的所有子栏目</option>" : "<option value=\"3\">显示当前栏目顶级栏目下的所有子栏目</option>");
                        str.Append("</td></tr>\r\n");
                        break;
                    case "Level":
                        level = int.Parse(value);
                        break;
                    case "HtmlCode":
                        str.Append("<tr><td align=\"right\"> 导航的HTML</td><td><select name=\"HtmlCode\" id=\"HtmlCode\"><option value=\"\">无</option>");
                        str.Append(value == "li" ? "<option value=\"li\" selected>li</option>" : "<option value=\"li\">li</option>");
                        str.Append(value == "p" ? "<option value=\"p\" selected>p</option>" : "<option value=\"p\">p</option>");
                        str.Append(value == "div" ? "<option value=\"div\" selected>div</option>" : "<option value=\"div\">div</option>");
                        str.Append(value == "span" ? "<option value=\"span\" selected>span</option>" : "<option value=\"span\">span</option>");
                        str.Append("</select></td></tr>\r\n");
                        break;

                }


            }
            if (bothMenu != string.Empty)
            {
                str.Append("<tr><td align=\"right\"> 是否显示二级栏目</td><td>" + bothMenu + " </td></tr>\r\n");
            }
            if (isNavTagWord != string.Empty)
            {
                str.Append("<tr><td align=\"right\"> 导航方式</td><td>" + isNavTagWord + " </td></tr>\r\n");
            }
            #endregion
            matchLabel = regLabel.Match(content);
            if (matchLabel.Success)
            {
                selfCode = matchLabel.Groups[2].Value;
                #region 获得循环内容
                if (level > 0)
                {
                    //子栏目级数
                    str.Append("<tr><td>标签内容</td><td><input id='Level' value='" + level + "' type=\"hidden\"  /><textarea style=\"width:350px\" rows=\"10\" wrap=\"off\" id=\"code\">" + Utils.HtmlEncode(selfCode).Replace("[", "::[::").Replace("]", "::]::") + "</textarea></td></tr>\r\n");
                }
            }
                #endregion

            str.Append("</table>\r\n");
            str.Append("</form>\r\n");
            str.Append("<script type=\"text/javascript\">resetContent('code');</script>");
            str.Append("</body>\r\n");
            str.Append("</html>");
            sw.WriteLine(str.ToString());
            sw.Close();
        }


        /// <summary>
        /// 产生dreamwear插件提示图片
        /// </summary>
        /// <param name="filename">标签名</param>
        private void CreateImg(string path, string filename)
        {
            string _FontName = "黑体";
            int _FontSize = 12;



            Bitmap objBitmap = null;
            Graphics g = null;

            FontFamily ff = new FontFamily(_FontName);

            Font stringFont = new Font(ff, _FontSize, FontStyle.Regular);
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.NoWrap;

            try
            {
                objBitmap = new Bitmap(1, 1);
                g = Graphics.FromImage(objBitmap);
                SizeF stringSize = g.MeasureString(filename, stringFont);
                int nWidth = (int)stringSize.Width;
                int nHeight = (int)stringSize.Height;
                g.Dispose();
                objBitmap.Dispose();

                objBitmap = new Bitmap(nWidth, nHeight);
                g = Graphics.FromImage(objBitmap);
                g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(0, 0, nWidth, nHeight));
                g.DrawString(filename, stringFont, new SolidBrush(Color.White), new PointF(0, 2), stringFormat);
                objBitmap.Save(path + "\\hqb." + filename + ".gif", ImageFormat.Gif);
            }
            catch
            {

            }
            finally
            {
                if (null != g) g.Dispose();
                if (null != objBitmap) objBitmap.Dispose();

            }


        }


        #endregion


        /// <summary>
        /// 获得嘎标签的标识符，规则为小于三位的“L001” 大于等于的就不做处理
        /// </summary>
        /// <param name="siteID"></param>
        /// <returns></returns>
        public string GetIdentification(int siteID)
        {
            string index = (int.Parse(dal.GetObject("MAXIDENTIFICATION", Utils.getOneNumParams(siteID))) + 1).ToString();

            return FormartIdentification(index);
        }
        /// <summary>
        /// 格式化标识
        /// </summary>
        /// <param name="identification"></param>
        /// <returns></returns>
        public string FormartIdentification(object identification)
        {
            string str = identification.ToString();
            if (str.Length < 3)
            {
                str = Convert.ToInt32(identification).ToString("000");
            }
            return str;
        }
        //生成类型导航标签
        private void CreateCategoryHTML(string name, string fileName, string path, string content, string jsPath)
        {
            CreateFile(path, fileName + ".htm");
            string value = ""; //属性值         
            StreamWriter sw = new StreamWriter(path + "\\" + fileName + ".htm");
            StringBuilder str = new StringBuilder();
            int level = 0;
            str.Append("<!DOCTYPE HTML SYSTEM \"-//Macromedia//DWExtension layout-engine 5.0//dialog\">\r\n");
            str.Append("<html>\r\n");
            str.Append("<head>\r\n");
            str.Append(" <meta http-equiv=\"Content-Type\"  content=\"text/html; charset=utf-8\"  />\r\n");
            str.Append("<title>插入" + fileName + "</title>\r\n");


            if (string.IsNullOrEmpty(jsPath))
                str.Append(" <script src=\"../HQB模板管理/SystemLable.js\" type=\"text/javascript\"></script>\r\n");
            else
                str.Append(" <script src=\"" + jsPath + "\" type=\"text/javascript\"></script>\r\n");

            str.Append("<script type=\"text/javascript\">\r\n");
            str.Append("function commandButtons() {\r\n");
            str.Append("return new Array(\"插入标签\", \"objectTag(2)\",  \"取消\", \"window.close()\")\r\n}\r\n");
            str.Append("function windowDimensions() { return \"500,250\"; }\r\n");
            str.Append("</script>\r\n");
            str.Append("</head>\r\n");
            str.Append("<body>\r\n");
            str.Append("<form name=\"theform\" method=\"post\" action=\"\">\r\n");

            str.Append("<input type='hidden' id='hidName' value='" + name + "_" + fileName + "'/><table width=\"450\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\">\r\n");



            Regex regLabel;
            Regex regAttribute;
            string selfCode;
            MatchCollection collectAttribute;
            Match matchLabel;

            regLabel = new Regex(@"\{HQB_(?<1>L\d+)_[^\s}]+(?<3>[^}]*)LableType\s*=\s*[""']Category[""'](?<4>[^}]*)\}(?<2>.*?)\{/HQB_\k<1>\s*\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            regAttribute = new Regex(@"(?<1>[\w-]+)\s*=\s*[""'](?<2>[^""']+)[""']", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            collectAttribute = regAttribute.Matches(content);

            #region 获得html
            foreach (Match item in collectAttribute)
            {
                value = item.Groups[2].Value;
                switch (item.Groups[1].Value)
                {

                    case "CategoryID":
                        str.Append("<tr><td align=\"right\"> 类型ID</td><td><input type='text' id='CategoryID' value='" + value + "'/></td></tr>\r\n");
                        break;
                    case "CssFile":
                        str.Append("<tr><td align=\"right\"> 样式文件路径</td><td><input type='text' id='CssFile' value='" + value + "'/></td></tr>\r\n");
                        break;
                    case "JsFile":
                        str.Append("<tr><td align=\"right\"> JS文件路径</td><td><input type='text' id='JsFile' value='" + value + "'/></td></tr>\r\n");
                        break;
                    case "IsSibling":
                        str.Append("<tr><td align=\"right\"> 显示当前类型的平级类型</td><td><input type=\"checkbox\" id=\"IsSibling\" checked=\"checked\" /></td></tr>\r\n");
                        break;
                    case "Level":
                        level = int.Parse(value);
                        break;
                }
            }
            #endregion
            matchLabel = regLabel.Match(content);
            if (matchLabel.Success)
            {
                selfCode = matchLabel.Groups[2].Value;
                #region 获得循环内容
                if (level > 0)
                {
                    //子栏目级数
                    str.Append("<tr><td>标签内容</td><td><input id='Level' value='" + level + "' type=\"hidden\"  /><textarea style=\"width:350px\" rows=\"10\" wrap=\"off\" id=\"code\">" + Utils.HtmlEncode(selfCode).Replace("[", "::[::").Replace("]", "::]::") + "</textarea></td></tr>\r\n");
                }
            }
                #endregion

            str.Append("</table>\r\n");
            str.Append("</form>\r\n");
            str.Append("<script type=\"text/javascript\">resetContent('code');</script>");
            str.Append("</body>\r\n");
            str.Append("</html>");
            sw.WriteLine(str.ToString());
            sw.Close();
        }


        //生成可替换的标签
        public void CreateDiscoverLable(string path, string siteID)
        {
            path = HttpContext.Current.Server.MapPath(path);  //模板路径        
            CopyFile("../Template/LabelTemp", path, "");  //1.先把labelTemp文件移到当前模板目录下

            KingTop.Model.SelectParams prams = new KingTop.Model.SelectParams();
            prams.I2 = 0;
            prams.I3 = -1;
            prams.S3 = siteID.ToString();
            prams.S4 = " AND IsEnable=1";
            DataSet ds = new KingTop.BLL.Template.Lable().GetLable("LableList", prams);
            DataTable dt = ds.Tables[1];  //标签列表

            UpdateCodeHints(path, dt);  //2.生成提示文件

            //分类
            string[] strComuns = { "ClassName", "ClassID" };
            DataTable dtClass = (new DataView(dt)).ToTable(true, strComuns);
            bool isCreate = false;
            string title = "";
            string src = "";  //标签显示的图片
            string imgName = string.Empty;  //生成的图片名
            StringBuilder insertbarStr = new StringBuilder();  //Insertbar.xml中的字符串
            StringBuilder tagsStr = new StringBuilder();  //Tags.xml中的字符串
            foreach (DataRow drClass in dtClass.Rows)
            {
                if (!System.IO.Directory.Exists(path + "\\Inspectors\\" + drClass["ClassName"] + "")) //根据分类创建文件夹
                {
                    System.IO.Directory.CreateDirectory(path + "\\Inspectors\\" + drClass["ClassName"] + "");
                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (drClass["ClassID"].ToString() == dr["ClassID"].ToString())
                    {
                        isCreate = false;

                        string lblContent = dr["LableContent"].ToString();
                        title = dr["Title"].ToString();
                        string[] names = dr["LableName"].ToString().Substring(1).Split('_');
                        #region 生成html
                        if (Convert.ToInt32(dr["IsSystem"]) == 1)
                        {
                            if (lblContent.IndexOf(" LableType=\"LIST\"") != -1)   //为系统标签中的通用列表标签     
                            {
                                if (lblContent.IndexOf("TableName=\"K_U_Commend\"") == -1) ////通用列表类型和专题列表类型 除幻灯片类型之外
                                {
                                    CreatePublicHTML(names[1], title, path + "\\Inspectors\\" + drClass["ClassName"], dr["LableContent"].ToString(), "../SystemLable.js");
                                    src = "select.gif";
                                    isCreate = true;

                                }
                                else //幻灯片类型
                                {
                                    src = "image.gif";

                                }
                            }
                            else if (lblContent.IndexOf(" LableType=\"MENU\"") != -1 || lblContent.IndexOf(" LableType=\"NAV\"") != -1)  //系统标签中的栏目类型
                            {
                                CreateMenuHTML(names[1], title, path + "\\Inspectors\\" + drClass["ClassName"], dr["LableContent"].ToString(), "../SystemLable.js");
                                src = "menu.gif";
                                isCreate = true;

                            }
                            else if (lblContent.IndexOf(" LableType=\"CONTENT\"") != -1)  //内容标签
                            {
                                src = "contents.gif";

                            }
                            else if (lblContent.IndexOf(" LableType=\"Category\"") != -1)  //类型导航标签
                            {
                                CreateCategoryHTML(names[1], title, path + "\\Inspectors\\" + drClass["ClassName"], dr["LableContent"].ToString(), "../SystemLable.js");
                                isCreate = true;

                            }
                        }
                        if (!isCreate)
                        {
                            CreateHTML(title, dr["LableName"].ToString(), path + "\\Inspectors\\" + drClass["ClassName"]);

                        }
                        #endregion
                        CreateImg(path + "\\ThirdPartyTags\\HQBTags", dr["Title"].ToString());  //创建Dreamweaver中标签显示的图片
                        imgName = "hqb." + dr["Title"].ToString() + ".gif";
                        tagsStr.Append("<tagspec tag_name=\"HQB_" + title + "\" start_string=\"{HQB_" + names[1] + "\" end_string=\"" + (isCreate ? "{/HQB_" + names[1] : "") + "}\" detect_in_attribute=\"true\" icon=\"HQBTags/" + imgName + "\"/>\r\n");
                        insertbarStr.Append("                      <button file=\"../Inspectors/" + drClass["ClassName"] + "/" + title + ".htm\" image=\"../ThirdPartyTags/HQBTags/Objects/" + src + "\" id=\"DWBar_HQB_" + title + "\" name=\"" + drClass["ClassName"] + "\" /> \r\n");
                    }
                }
                if (src == "")
                {
                    src = "channels.gif";
                }
            }
            UpdateInsertbar(path, insertbarStr.ToString());  //3.修改Insertbar.xml文件
            UpdateTags(path, tagsStr.ToString());//3.修改tags.xml文件

        }

        /// <summary>
        /// 修改CodeHints.xml文件
        /// </summary>
        private void UpdateCodeHints(string path, DataTable dtLable)
        {
            StringBuilder remind = new StringBuilder();
            foreach (DataRow dr in dtLable.Rows)
            {
                if (dr["LableContent"].ToString().Length > 0)
                {
                    string labelName = dr["LableName"].ToString();
                    labelName = labelName.Substring(0, labelName.IndexOf(" LableType"));
                    if (!string.IsNullOrEmpty(dr["description"].ToString()))
                        labelName = labelName + " －－ " + dr["description"].ToString();

                    labelName = labelName.Replace("\"", "&quot;");
                    remind.Append("<menuitem label=\"" + labelName + "\" value=\"" + dr["LableContent"].ToString().Substring(1).Replace("\"", "&quot;") + "\" icon=\"shared/mm/images/hintMisc.gif\" />\r\n");
                }
            }
            string fileStr = Utils.showFileContet(path + "/CodeHints/CodeHints.xml");  //获得文件内容
            fileStr = fileStr.Replace(@"<!--KIngTopCMSLabel-->", remind.ToString());

            Utils.WriteFile(path + "/CodeHints/CodeHints.xml", fileStr);
        }

        /// <summary>
        /// 修改Insertbar.xml文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dtLable"></param>
        private void UpdateInsertbar(string path, string content)
        {
            string fileStr = Utils.showFileContet(path + "/Objects/insertbar.xml");  //获得文件内容
            fileStr = fileStr.Replace(@"<!--KIngTopCMSLabelFun-->", content);

            Utils.WriteFile(path + "/Objects/insertbar.xml", fileStr);
        }

        /// <summary>
        /// 修改Tags.xml文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="dtLable"></param>
        private void UpdateTags(string path, string content)
        {
            string fileStr = Utils.showFileContet(path + "/ThirdPartyTags/Tags.xml");  //获得文件内容
            fileStr = fileStr.Replace(@"<!--KIngTopCMSLabel-->", content);

            Utils.WriteFile(path + "/ThirdPartyTags/Tags.xml", fileStr);
        }
    }
}
