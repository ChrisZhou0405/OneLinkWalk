using System;
using System.Collections.Generic;
using System.Text;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      卜向阳
//    创建时间： 2010年7月30日
//    功能描述： 模板目录
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Config
{
    [Serializable]
    public class TemplateConfig : IConfigInfo
    {
        public TemplateConfig()
        {
        }
        #region 私有字段
        private string _templateRelativeDirRoot;         //获取模板目录相对根目录
        private string _webRelativeDirRoot; //静态HTML文件存放目录
        private string _newsFileName;      //有关资讯类，存放在项目中文件夹目录名称

        private string _detailFileName;//资讯详细页名称
        private string _listFileName;//资讯列表页名称

        private string _parseAspx;//负责解析文件

        #endregion

        #region 属性
        /// <summary>
        /// 模板相对根目录
        /// </summary>
        public string TemplateRelativeDirRoot
        {
            get { return _templateRelativeDirRoot; }
            set { _templateRelativeDirRoot = value; }
        }
        /// <summary>
        /// 生成页面相对根目录
        /// </summary>
        public string WebRelativeDirRoot
        {
            get { return _webRelativeDirRoot; }
            set { _webRelativeDirRoot = value; }
        }
        /// <summary>
        /// 资讯文件夹名称
        /// </summary>
        public string NewsFileName
        {
            get { return _newsFileName; }
            set { _newsFileName = value; }
        }
        /// <summary>
        /// 详细页文件夹名称
        /// </summary>
        public string DetailFileName
        {
            get { return _detailFileName; }
            set { _detailFileName = value; }
        }
        /// <summary>
        /// 列表页文件夹名称
        /// </summary>
        public string ListFileName
        {
            get { return _listFileName; }
            set { _listFileName = value; }
        }
        /// <summary>
        /// 负责解析文件
        /// </summary>
        public string ParseAspx
        {
            get { return _parseAspx; }
            set { _parseAspx = value; }
        }
        #endregion
    }
}