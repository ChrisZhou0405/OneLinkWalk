using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KingTop.Common;
using System.Data;
using KingTop.Config;
using System.Configuration;
using System.IO;
using System.Reflection;

/*================================================================
    Copyright (C) 2010 华强北在线    作者:      何伟    创建时间： 2010年9月6日    功能描述： 模板风格文件操作公共类
 * 
// 更新日期        更新人      更新原因/内容
--===============================================================*/

namespace KingTop.Template
{
    public class LabelUtils
    {
        #region 变量成员
        private static string path = ConfigurationManager.AppSettings["WebDAL"];
        private IDAL.Template.ILabelUtils dal = (IDAL.Template.ILabelUtils)Assembly.Load(path).CreateInstance(path + ".Template.LabelUtils");
        #endregion

        #region 文件/文件目录操作 By 何伟 2010-09-08
        /// <summary>
        /// 获取配置文件的实例
        /// </summary>
        /// <returns></returns>
        public static TemplateConfig GetTemplateConfig()
        {
            TemplateConfig templatedobj = KingTop.Config.Template.GetConfig(System.Web.HttpContext.Current.Server.MapPath("~/config/Template.config"));
            return templatedobj;
        }

        /// <summary>
        /// 获取当前站点默认的方案下模板目录路径
        /// </summary>
        /// <returns></returns>
        public static string GetTemplateProjectPath(int siteId)
        {
            string root = "";
            string htmlSaveRootPath = "";
            LabelUtils labUtils;

            labUtils = new LabelUtils();

            DataTable dt = labUtils.TP_GetList("ISDEFAULT", Utils.getOneNumParams(siteId));                          //获取当前站点默认方案的目录名
            if (dt != null && dt.Rows.Count != 0)
            {
                htmlSaveRootPath = dt.Rows[0]["Directory"].ToString();
            }

            //判断目录是否为空
            if (htmlSaveRootPath.Length > 0)
            {
                root = string.Format("{0}/{1}", "/SysAdmin/Template", htmlSaveRootPath);
                if (!Directory.Exists(root))                                                                        //如果不存在目录则先创建
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(root));
                }
            }
            else
            {
                Utils.AlertMessage("请先设置一个默认的方案!");
            }
            return root;
        }


        /// <summary>
        /// 获取项目文件存放根目录
        /// </summary>
        /// <returns></returns>
        public static string GetWebSavePath(string siteid)
        {
            LabelUtils labUtils;

            labUtils = new LabelUtils();
            string htmlSaveRootPath = string.Empty;//文件根目录
            if (HttpContext.Current.Session["SiteDir"] == null)
            {


                string directoryName = string.Empty;
                TemplateConfig templatedobj = GetTemplateConfig();
                DataTable dt = labUtils.GetList("GETROOT", Utils.getOneParams(siteid));          //获取当前站点的目录名
                if (dt != null && dt.Rows.Count != 0)
                {
                    HttpContext.Current.Session["SiteDir"] = dt.Rows[0]["Directory"].ToString();
                    htmlSaveRootPath = "/" + dt.Rows[0]["Directory"].ToString();
                }
            }
            else
            {
                htmlSaveRootPath = "/" + HttpContext.Current.Session["SiteDir"].ToString();
            }
            return htmlSaveRootPath;
        }

        /// <summary>
        /// 获取项目文件存放根目录名字,不包含"/"或"//"  By 何伟 2010-09-08
        /// </summary>
        /// <returns></returns>
        public static string GetWebSavePathName(string siteid)
        {
            LabelUtils labUtils;

            labUtils = new LabelUtils();
            string htmlSaveRootPath = string.Empty;//文件根目录
            if (HttpContext.Current.Session["SiteDir"] == null)
            {
                string directoryName = string.Empty;
                TemplateConfig templatedobj = GetTemplateConfig();
                DataTable dt = labUtils.GetList("GETROOT", Utils.getOneParams(siteid));          //获取当前站点的目录名
                if (dt != null && dt.Rows.Count != 0)
                {
                    HttpContext.Current.Session["SiteDir"] = dt.Rows[0]["Directory"].ToString();
                    htmlSaveRootPath = dt.Rows[0]["Directory"].ToString();
                }
            }
            else
            {
                htmlSaveRootPath = HttpContext.Current.Session["SiteDir"].ToString();
            }
            return htmlSaveRootPath;
        }

        /// <summary>
        /// 获取项目文件存放根目录
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetWebSavePathForAspx(string type, string siteid)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string htmlSaveRootPath = string.Empty;//文件根目录
            string directoryName = string.Empty;
            TemplateConfig templatedobj = GetTemplateConfig();

            DataTable dt = GetList("GETROOT", Utils.getOneParams(siteid));          //获取当前站点的目录名
            if (dt != null && dt.Rows.Count != 0)
            {
                htmlSaveRootPath = "/" + dt.Rows[0]["Directory"].ToString();
            }
            if (type.ToString().Trim().ToLower() == "news")
            {
                directoryName = templatedobj.NewsFileName;
                htmlSaveRootPath = string.Format("{0}/{1}", htmlSaveRootPath, directoryName);
                dict.Add("root", htmlSaveRootPath);
            }
            string parseAspx = templatedobj.ParseAspx;
            dict.Add("parse", parseAspx);
            return dict;
        }

        /// <summary>  
        /// 删除指定路径的目录及文件  
        /// </summary>  
        /// <param name="dir">带文件夹名的路径</param>   
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之   
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                    {
                        File.Delete(d); //直接删除其中的文件                          
                    }
                    else
                    {
                        DeleteFolder(d); //递归删除子文件夹   
                    }
                }
                Directory.Delete(dir, true); //删除已空文件夹                   
            }
        }

        /// <summary>
        /// 复制文件/目录
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="destination">目标目录</param>
        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            // 如果两个目录相同，则无须复制
            if (destination.FullName.Equals(source.FullName))
            {
                return;
            }

            // 如果目标目录不存在，创建它
            if (!destination.Exists)
            {
                destination.Create();
            }

            // 复制所有文件
            FileInfo[] files = source.GetFiles();
            foreach (FileInfo file in files)
            {
                // 将文件复制到目标目录
                file.CopyTo(Path.Combine(destination.FullName, file.Name), true);
            }

            // 处理子目录
            DirectoryInfo[] dirs = source.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                string destinationDir = Path.Combine(destination.FullName, dir.Name);

                // 递归处理子目录
                CopyDirectory(dir, new DirectoryInfo(destinationDir));
            }
        }

        /// <summary>
        /// 复制文件到指定目录
        /// </summary>
        /// <param name="source">源文件目录路径</param>
        /// <param name="destination">目标文件夹/目录</param>
        /// <param name="fileName">文件名</param>
        public static void CopyFile(string source, string destination, string fileName)
        {
            // 获取文件的绝对地址
            string sourceFile = System.IO.Path.Combine(source, fileName);
            string destFile = System.IO.Path.Combine(destination, fileName);

            //判断是否存目标文件目录没有则创建
            if (!System.IO.Directory.Exists(destination))
            {
                System.IO.Directory.CreateDirectory(destination);
            }

            //复制并替换重名的文件
            System.IO.File.Copy(sourceFile, destFile, true);
        }

        /// <summary>
        /// 验证当前风格是否是默认滴
        /// </summary>
        /// <param name="sid">风格的ID</param>
        /// <returns>是否默认</returns>
        public static bool ValidateSkinIsDefault(string sid)
        {
            bool ret = false;                       //结果
            LabelUtils labUtils;

            labUtils = new LabelUtils();

            DataTable dt = labUtils.TS_GetList("ONE", Utils.getOneParams(sid));
            try
            {
                if (dt.Rows.Count > 0)             //如果不为空则取出当前的风格是否默认选项
                {
                    int defaultValue = Convert.ToInt32(dt.Rows[0]["IsDefault"]);
                    if (defaultValue > 0)
                    {
                        ret = true;
                    }
                }
            }
            finally
            {
                dt.Dispose();                       //释放
            }
            return ret;
        }

        /// <summary>
        /// 获取当前默认滴风格目录
        /// </summary>
        /// <returns>目录名</returns>
        public static string GetSkinIsDefault()
        {
            string dirName = string.Empty;                                              //目录名
            LabelUtils labUtils;

            labUtils = new LabelUtils();
            DataTable dt = labUtils.TS_GetList("DEFAULTONE", new KingTop.Model.SelectParams());      //返回风格对象

            try
            {
                if (dt.Rows.Count > 0)             //如果不为空则取出当前的风格是否默认选项
                {
                    dirName = Convert.ToString(dt.Rows[0]["Dirct"]);
                }
            }
            finally
            {
                dt.Dispose();                       //释放
            }
            return dirName;
        }

        /// <summary>
        /// 获取当前月份
        /// </summary>
        /// <returns></returns>
        public static string GetMonth()
        {
            return DateTime.Now.ToString("yyyyMM");
        }

        /// <summary>
        /// 获取当前月份
        /// </summary>
        /// <returns></returns>
        public static string GetMonth(string time)
        {
            string t = "";
            if (!string.IsNullOrEmpty(time))
            {
                DateTime dt = Convert.ToDateTime(time);
                t = dt.ToString("yyyyMM");
            }
            else
            {
                t = GetMonth();
            }
            return t;
        }
        #endregion

        #region     移动过来的方法
        #region 根据传入的参数查询K_SysWebSite,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_SysWebSite,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 根据传入的参数查询K_TemplateProject,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_TemplateProject,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable TP_GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.TP_GetList(tranType, paramsModel);
        }
        #endregion

        #region 根据传入的参数查询K_TemplateSkin,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_TemplateSkin,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable TS_GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.TS_GetList(tranType, paramsModel);
        }
        #endregion

        #endregion
    }
}