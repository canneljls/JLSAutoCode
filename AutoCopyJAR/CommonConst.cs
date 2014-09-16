using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoCopyJAR
{
    public class CommonConst
    {
        public readonly static string BinPath = AppDomain.CurrentDomain.BaseDirectory;

        public readonly static string ConfigFilePath = BinPath + "Config.resx";

        public readonly static string MainConfigKey = "MainConfigKey";

        public readonly static string JarPathKey = "JarPathKey";

        public readonly static string TarPathKey = "TarPathKey";
    }
}
