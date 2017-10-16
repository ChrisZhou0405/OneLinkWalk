using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KingTop.Common;

namespace KingTop.Config
{
    public class SiteInfoManage
    {
        private static object lockHelper = new object();
        private static SiteInfoManageConfig M_SiteInfoManageConfig;

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return M_SiteInfoManageConfig; }
            set { M_SiteInfoManageConfig = (SiteInfoManageConfig)value; }
        }

        /// <summary>
        /// 重设配置类实例

        /// </summary>
        public static SiteInfoManageConfig GetConfig(string PhyFilePath)
        {
            M_SiteInfoManageConfig = LoadConfig(PhyFilePath);
            return M_SiteInfoManageConfig;
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
        public static SiteInfoManageConfig LoadConfig(string PhyFilePath)
        {
            M_SiteInfoManageConfig = (SiteInfoManageConfig)DefaultConfigFileManager.DeserializeInfo(PhyFilePath, typeof(SiteInfoManageConfig));
            return M_SiteInfoManageConfig;
        }
    }
}
