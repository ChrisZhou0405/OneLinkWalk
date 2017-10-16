using System;
using System.Collections.Generic;
using System.Text;
using KingTop.Common;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 站点信息设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Config
{
    public class SiteInfo
    {
        private static object lockHelper = new object();
        private static SiteInfoConfig M_SiteInfoConfig;

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return M_SiteInfoConfig; }
            set { M_SiteInfoConfig = (SiteInfoConfig)value; }
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static SiteInfoConfig GetConfig(string PhyFilePath)
        {
            M_SiteInfoConfig = LoadConfig(PhyFilePath);
            return M_SiteInfoConfig;
        }

        /// <summary>
        /// 保存配置类实例
        /// </summary>
        /// <param name="Uploadconfiginfo"></param>
        /// <returns></returns>
        public static bool SaveConfig(string PhyFilePath)
        {
            return SerializationHelper.Save(ConfigInfo, PhyFilePath);
        }

        /// <summary>
        /// 返回配置类实例
        /// </summary>
        /// <returns></returns>
        public static SiteInfoConfig LoadConfig(string PhyFilePath)
        {
            M_SiteInfoConfig = (SiteInfoConfig)DefaultConfigFileManager.DeserializeInfo(PhyFilePath, typeof(SiteInfoConfig));
            return M_SiteInfoConfig;
        }
       
    }
}
