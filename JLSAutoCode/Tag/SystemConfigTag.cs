using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLSAutoCode.Tag
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Serializable]
    public class SystemConfigTag
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName = "";

        /// <summary>
        /// 数据库类型
        /// </summary>
        public string DBType = "";

        /// <summary>
        /// 打开和保存的默认路径
        /// </summary>
        public string OpenSaveDefaultPath = "";
    }
}
