using System.Linq;
using System.Collections.Generic;
using CLX.Extensions.Raws;
using System;

namespace CLX.Extensions.Helper
{
    public static class DisplayExtension
    {
        private static Action<string> logAction;
        private static string defaultSeparator = " ";

        static DisplayExtension()
        {
            logAction = Console.WriteLine;
        }

        /// <summary>
        /// 转变输出的方式
        /// </summary>
        public static void ChangeLogActionTo(Action<string> logAction)
        {
            DisplayExtension.logAction = logAction;
        }

        /// <summary>
        /// 转变默认分隔字符串
        /// </summary>
        public static void ChangeDefaultSeparatorTo(string separator)
        {
            defaultSeparator = separator;
        }

        public static void Display(this string str)
        {
            logAction(str);
        }

        public static void Display<T>(this IEnumerable<T> list,string separator=null)
        {
            separator = separator ?? defaultSeparator;
            list.Select(item => item.ToString()).JoinWith(separator).Display();
        }

        public static void Display(this IEnumerable<string>list,string separator = null)
        {
            separator = separator ?? defaultSeparator;
            list.JoinWith(separator).Display();
        }

        public static void Display(this object obj)
        {
            Display(obj.ToString());
        }
    }
}
