using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public static class NullHandler
    {
        public static string ConvertString(object obj)
        {
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
    }
}
