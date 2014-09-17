using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLSAutoCode.Hibernate
{
    public class HHelperFactory
    {
        /// <summary>
        /// 获取生成代码的Helper
        /// </summary>
        /// <param name="DBType">数据库类型</param>
        /// <returns></returns>
        public static HHelperDefault GetHHelper(string DBType)
        {
            if (DBType == "mdb")
            {
                return new HHelperMdb();
            }
            else
            {
                return new HHelperSQLite();
            }
        }
    }
}
