using System;
using System.Collections.Generic;
using System.Text;
using KingTop.Common;

#region 版权注释
/*===========================================================================
//    Copyright (C) 2010 华强北在线
//    作者:      陈顺 
//    创建时间： 2010年5月27日
//    功能描述： 邮件设置
 
//    更新日期        更新人      更新原因/内容
//
===========================================================================*/
#endregion

namespace KingTop.Config
{
    public class Post
    {
        private static object lockHelper = new object();
        private static PostConfig M_PostConfig;

        /// <summary>
        /// 当前配置类的实例
        /// </summary>
        public static IConfigInfo ConfigInfo
        {
            get { return M_PostConfig; }
            set { M_PostConfig = (PostConfig)value; }
        }

        /// <summary>
        /// 重设配置类实例
        /// </summary>
        public static PostConfig GetConfig(string PhyFilePath)
        {
            M_PostConfig = LoadConfig(PhyFilePath);
            return M_PostConfig;
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
        public static PostConfig LoadConfig(string PhyFilePath)
        {
            M_PostConfig = (PostConfig)DefaultConfigFileManager.DeserializeInfo(PhyFilePath, typeof(PostConfig));
            return M_PostConfig;
        }
    }
}
