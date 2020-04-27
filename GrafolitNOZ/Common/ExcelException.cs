using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafolitNOZ.Common
{
    public class ExcelException : Exception
    {
        public ExcelException(string message)
            : base(message)
        {
        }

        public ExcelException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}