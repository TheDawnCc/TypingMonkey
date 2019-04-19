using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypingMonkey
{
    public static class StringHelper
    {
        public static string ClearRedundantChar(string str)
        {
            string result = str;
            result = result.Replace("\r\n", " ");
            result = result.Replace("\n", " ");
            result = result.Replace("\r", " ");
            result = result.Replace("\0", "");
            return result;
        }
    }
}
