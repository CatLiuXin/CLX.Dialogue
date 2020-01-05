using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLX.Extensions.Collections
{
    public static class ListExtension
    {
        public static string CombineToString(this List<char> list)
        {
            return new StringBuilder().Append(list.ToArray()).ToString();
        }
    }
}
