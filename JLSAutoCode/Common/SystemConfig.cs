using JLSAutoCode.Tag;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JLSAutoCode.Common
{
    public class SystemConfig
    {
        /// <summary>
        /// 系统配置文件路径
        /// </summary>
        public static string SystemConfigTagPath = AppDomain.CurrentDomain.BaseDirectory + "\\SystemConfigTag.conf";

        /// <summary>
        /// 系统配置Key
        /// </summary>
        public static string SystemConfigTagKey = "SystemConfigTagKey";

        private static SystemConfigTag m_SystemConfigTag = new SystemConfigTag();

        public static SystemConfigTag SystemConfigTag
        {
            get { return m_SystemConfigTag; }
            set { m_SystemConfigTag = value; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            //初始化系统配置
            if (File.Exists(SystemConfigTagPath))
            {
                ResourceHelper resourceHelper = new ResourceHelper(SystemConfigTagPath);

                m_SystemConfigTag = resourceHelper.GetObject(SystemConfigTagKey) as SystemConfigTag;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public static void Save()
        {
            ResourceHelper resourceHelper = new ResourceHelper(SystemConfigTagPath);

            resourceHelper.SetObject(SystemConfigTagKey, m_SystemConfigTag);

            resourceHelper.Save();
        }
    }
}
