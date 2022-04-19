using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringExtension
    {
        public static bool EqualsIgnoreCase(this string first, string second)
        {
            return first.ToLower().Contains(second.ToLower());
        }
    }
}
