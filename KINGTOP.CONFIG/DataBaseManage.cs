using System;
using System.Collections.Generic;
using System.Text;
using KingTop.Common;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 数据库管理参数设置 
//    更新日期        更新人      更新原因/内容
===========================================================================*/
#endregion

namespace KingTop.Config
{
    public class DataBaseManage
    {
        private static object lockHelper = new object();
        private static DataBaseManageConfig M_DataBaseManageConfig;

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return M_DataBaseManageConfig; }
            set { M_DataBaseManageConfig = (DataBaseManageConfig)value; }
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static DataBaseManageConfig GetConfig(string PhyFilePath)
        {
            M_DataBaseManageConfig = LoadConfig(PhyFilePath);
            return M_DataBaseManageConfig;
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
        public static DataBaseManageConfig LoadConfig(string PhyFilePath)
        {
            M_DataBaseManageConfig = (DataBaseManageConfig)DefaultConfigFileManager.DeserializeInfo(PhyFilePath, typeof(DataBaseManageConfig));
            return M_DataBaseManageConfig;
        }
    }
}
