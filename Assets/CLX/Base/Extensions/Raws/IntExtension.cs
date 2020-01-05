using CLX.Extensions.Generic;
using System;
using System.Collections.Generic;

namespace CLX.Extensions.Raws
{
    public static class IntExtension
    {
        /// <summary>
        /// 生成一个从begin到end
        /// 步长为step的序列
        /// </summary>
        public static IEnumerable<int> To(this int begin,int end,int step=1)
        {
            if ((end - begin) * step <= 0) throw new ArgumentException("Begin 无法到达 End");
            while((end - begin) * step >= 0)
            {
                yield return begin;
                begin += step;
            }
        }

        /// <summary>
        /// 运行times次action
        /// </summary>
        public static void LoopsFor(this int times,Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            1.To(times).ForEach(_ => action());
        }
    }
}
