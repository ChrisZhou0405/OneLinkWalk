
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Reflection;
using KingTop.IDAL;
using System.IO;
using System.Web;
using KingTop.Common;
using KingTop.Template;

/*===========================================================================
// Copyright (C) 2010 华强北在线 
// 作者：何伟 2010-09-01
// 功能描述：对K_TemplateProject表的操作

// 更新日期        更新人      更新原因/内容
//
===========================================================================*/

namespace KingTop.BLL
{
    public class TemplateProject
    {
        private static string path = ConfigurationManager.AppSettings["WebDAL"];

        private IDAL.ITemplateProject dal = (IDAL.ITemplateProject)Assembly.Load(path).CreateInstance(path + ".TemplateProject");
        KingTop.Template.LabelUtils lableUtil = new KingTop.Template.LabelUtils();

        #region 根据传入的参数查询K_TemplateProject,返回查询结果
        /// <summary>
        /// 根据传入的参数查询K_TemplateProject,返回查询结果
        /// </summary>
        /// <param name="tranType">操作类型</param>
        /// <param name="paramsModel">条件</param>
        /// <returns>返回DataTable</returns>
        public DataTable GetList(string tranType, KingTop.Model.SelectParams paramsModel)
        {
            return dal.GetList(tranType, paramsModel);
        }
        #endregion

        #region 增、改K_TemplateProject表
        /// <summary>
        /// 增、改K_TemplateProject表
        /// </summary>
        /// <param name="tranType">操作类型，NEW=增，EDIT=改</param>
        /// <param name="TemModel"></param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string Save(string trantype, KingTop.Model.TemplateProject temModel)
        {
            return dal.Save(trantype, temModel);
        }
        #endregion

        #region 设置或者删除K_TemplateProject记录
        /// <summary>
        /// 设置或者删除K_TemplateProject记录
        /// </summary>
        /// <param name="tranType">操作参数：DEL=删除，PUB=发布，TOP=置顶，NEW=置新，HOT=热，COMMEND=推荐</param>
        /// <param name="setValue">设置值</param>
        /// <param name="IDList">ID集合，多个用“,”分隔</param>
        /// <returns>返回大于0的数字操作正常，返回非数字表示操作错误，返回的是系统错误说明</returns>
        public string TemplateProjectSet(string tranType, string setValue, string IDList)
        {
            return dal.TemplateProjectSet(tranType, setValue, IDList);
        }
        #endregion

        #region 得到分页数据
        /// <summary>
        /// 获得分页数据
        /// </summary>
        /// <param name="pager">分页实体类：KingTop.Model.Pager</param>
        /// <returns></returns>
        public void PageData(KingTop.Model.Pager pager)
        {
            dal.PageData(pager);
        }
        #endregion

        #region 创建一个默认的方案

        /// <summary>
        /// 创建默认的方案
        /// </summary>
        /// <param name="siteDir">站点目录名</param>
        /// <param name="nodeCode">NodeCode</param>
        /// <param name="siteId">站点的ID</param>
        /// <returns>结果:1.成功,0或其他表示失败</returns>
        public string CreateDefaultProject(string siteDir, string nodeCode, int siteId)
        {
            KingTop.Model.TemplateProject model = new KingTop.Model.TemplateProject();
            string result = string.Empty;   //返回的目录

            model.ID = Guid.NewGuid().ToString();       //方案对象赋值
            model.Title = "系统方案";
            model.Directory = CreateProDir(siteDir, siteId);
            model.Devise = "Admin";
            model.Width = 1024;
            model.Intro = "系统自动创建的默认方案!";
            model.IsDefault = true;
            model.Thumbnail = "";
            model.IsDel = false;
            model.DeTime = DateTime.Now;
            model.NodeCode = nodeCode;
            model.AddMan = "Admin";
            model.SiteID = siteId;
            model.AddTime = DateTime.Now;

            int ret = Utils.ParseInt(Save("NEW", model), 0);     //创建方案
            if (ret == 1)       //成功
            {
                result = model.Directory;
                //加载默认的样式和脚本到当前系统默认方案风格目录下
                string fromDir = "~/SysAdmin/Template/Temp";                                         //源文件的目录
                string destination = HttpContext.Current.Server.MapPath("~/SysAdmin/Template/" + result + "/Skins"); //当前的风格目录路径
                DirectoryInfo sourceDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(fromDir));            //创建临时存放系统风格文件目录
                DirectoryInfo destinationDir = new DirectoryInfo(destination);                       //创建当前风格目录
                LabelUtils.CopyDirectory(sourceDir, destinationDir);                                 //移动系统风格文件到新建的风格
            }
            return result;
        }

        #region 生成目录
        /// <summary>
        /// 自动创建目录
        /// </summary>
        /// <returns>自动生成的名字</returns>
        public string CreateProDir(string siteDir, int siteId)
        {
            string dirName = "";                                                                        //方案目录名
            string indexPath = "~/SysAdmin/Template/";                                                  //根目录
            DataTable dt = GetList("LASTONE", Utils.getOneNumParams(siteId));                         //取出最后创建的一个条记录
            string strDirectory = "";                                                                   //最后一个添加的方案目录名
            if (dt.Rows.Count > 0)
            {
                strDirectory = dt.Rows[0]["Directory"].ToString();                                      //得到最后的一个目录名
            }
            try
            {
                if (!strDirectory.Equals("") && strDirectory.Length > 0)                               //当目录名不为空时,对目录名加一如:P01,则返回P02
                {
                    dirName = GetNo(strDirectory);
                }
                else                                                                                   //否则从生成第一个初始值目录名
                {
                    dirName = siteDir + "P01";
                }

                if (!Directory.Exists(dirName))                                                         //验证目录是否已经存在
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(indexPath + dirName));                     //创建方案目录
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(indexPath + dirName + "/IncludeFile"));    //创建包含文件目录
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(indexPath + dirName + "/Skins"));          //创建HTML文件目录
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dirName;
        }

        /// <summary>
        /// 取得编号
        /// 如GetNo("P01"),返回"P02"
        /// </summary>
        /// <param name="strIn">基础编号</param>
        /// <returns>新编号</returns>
        protected string GetNo(string strIn)
        {
            string res = "";                             //返回的编号

            if (!strIn.Equals("") && strIn.Length > 0)  //目录名不为空
            {
                string[] chars = strIn.Split('P');        //取出目录名和编号的字符串数组           
                string dName = chars[0];               //前面的目录名字
                string strNo = chars[1];               //获取后面的数字编号

                string strNum = "";                  //新的编号
                int num = Int32.Parse(strNo);        //转换成数字对其加1操作,并返回01,02这种类型   
                num += 1;
                strNum = num.ToString("00");

                res = dName + "P" + strNum;
            }
            return res;
        }

        #endregion

        #region 创建默认的系统风格

        /// <summary>
        ///系统默认风格 
        /// </summary>
        /// <param name="ret">方案是否创建成功</param>
        /// <param name="projectDir">方案的目录</param>
        public void CreateDefaultSkin(int ret, string projectDir)
        {
            if (ret == 1)       //成功
            {
                //加载默认的样式和脚本到当前系统默认方案风格目录下
                string fromDir = "~/SysAdmin/Template/Temp";                                         //源文件的目录
                string destination = HttpContext.Current.Server.MapPath("~/SysAdmin/Template/" + projectDir + "/Skins"); //当前的风格目录路径
                DirectoryInfo sourceDir = new DirectoryInfo(HttpContext.Current.Server.MapPath(fromDir));                //创建临时存放系统风格文件目录
                DirectoryInfo destinationDir = new DirectoryInfo(destination);                       //创建当前风格目录
                LabelUtils.CopyDirectory(sourceDir, destinationDir);                                 //移动系统风格文件到新建的风格
            }
        }
        #endregion

        #endregion
    }
}
