using CLX.Extensions.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX.Extensions.Test
{
    public static class TestExtension
    {
        /// <summary>
        /// 进行名为title的测试
        /// </summary>
        /// <param name="title">测试主题</param>
        /// <param name="action">测试行为</param>
        public static void Test(this string title,Action action)
        {
            ("* "+title+" Begin:").Display();
            action?.Invoke();
            ("* " + title + " End\n").Display();
        }
    }
}
