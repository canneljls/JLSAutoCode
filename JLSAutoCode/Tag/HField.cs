using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLSAutoCode
{
    [Serializable]
    public class HField
    {
        public string CHName
        {
            get;
            set;
        }

        public string ENName
        {
            get;
            set;
        }

        public HFieldType Type
        {
            get;
            set;
        }

        public int Length
        {
            get;
            set;
        }
    }

    public enum HFieldType
    {
        NVARCHAR2,
        NUMBER,
        DATE,
        FLOAT,
        INT,
        BINARY,
        CLOB
    }
}
