using System;
using System.Collections.Generic;
using System.Text;
using KingTop.Common;

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
    public class Template
    {
        private static object lockHelper = new object();
        private static TemplateConfig M_TemplateConfig;

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return M_TemplateConfig; }
            set { M_TemplateConfig = (TemplateConfig)value; }
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static TemplateConfig GetConfig(string PhyFilePath)
        {
            M_TemplateConfig = LoadConfig(PhyFilePath);
            return M_TemplateConfig;
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
        public static TemplateConfig LoadConfig(string PhyFilePath)
        {
            M_TemplateConfig = (TemplateConfig)DefaultConfigFileManager.DeserializeInfo(PhyFilePath, typeof(TemplateConfig));
            return M_TemplateConfig;
        }
    }
}
