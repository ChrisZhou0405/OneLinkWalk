using System;
using System.Collections.Generic;
using System.Text;
using KingTop.Common;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 站点参数设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion


namespace KingTop.Config
{
    public class SiteParam
    {
        private static object lockHelper = new object();
        private static SiteParamConfig M_SiteParamConfig;

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return M_SiteParamConfig; }
            set { M_SiteParamConfig = (SiteParamConfig)value; }
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static SiteParamConfig GetConfig(string PhyFilePath)
        {
            M_SiteParamConfig = LoadConfig(PhyFilePath);
            return M_SiteParamConfig;
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
        public static SiteParamConfig LoadConfig(string PhyFilePath)
        {
            M_SiteParamConfig = (SiteParamConfig)DefaultConfigFileManager.DeserializeInfo(PhyFilePath, typeof(SiteParamConfig));
            return M_SiteParamConfig;
        }
    }
}
