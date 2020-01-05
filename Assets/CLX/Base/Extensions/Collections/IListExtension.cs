using System;
using System.Collections.Generic;
using System.Linq;
using CLX.Extensions.Generic;

namespace CLX.Extensions.Collections
{
    public static class IListExtension
    {
        /// <summary>
        /// 交换list列表中idx1号元素与idx2号元素的值
        /// </summary>
        public static void Swap<T>(this IList<T> list,int idx1,int idx2)
        {
            var tmp = list[idx1];
            list[idx1] = list[idx2];
            list[idx2] = tmp;
        }

        /// <summary>
        /// 将列表的元素全部重置为val
        /// </summary>
        public static IList<T> ResetByValue<T>(this IList<T> list,T val)
        {
            for(int i = 0; i < list.Count; i++)
            {
                list[i] = val;
            }
            return list;
        }

        /// <summary>
        /// 将列表中从beg到end-1的元素全部重置为val
        /// end默认为0，当end为0时表示从beg初始化到列表的末尾
        /// </summary>
        public static IList<T> ResetByValue<T>(this IList<T> list, T val,int beg,int end=0)
        {
            if (end == 0) end = list.Count;
            for (int i = beg; i < end; i++)
            {
                list[i] = val;
            }
            return list;
        }

        /// <summary>
        /// 使用一个无参委托来重置列表
        /// </summary>
        public static IList<T> ResetBy<T>(this IList<T> list,Func<T> factroy)
        {
            if (factroy == null) throw new ArgumentNullException("factroy");
            for(int i = 0; i < list.Count; i++)
            {
                list[i] = factroy();
            }
            return list;
        }

        /// <summary>
        /// 使用与数组下标相关的委托来重置列表
        /// </summary>
        public static IList<T> ResetBy<T>(this IList<T> list,Func<int,T> factroy)
        {
            if (factroy == null) throw new ArgumentNullException("factroy");
            for (int i = 0; i < list.Count; i++) list[i] = factroy(i);
            return list;
        }

        /// <summary>
        /// 打乱整个列表
        /// </summary>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            for(int i = list.Count-1; i >= 0; i--)
            {
                var swapItemID = r.Next(0, i + 1);
                list.Swap(i, swapItemID);
            }
            return list;
        }
        
        /// <summary>
        /// 将list划分为row行的二维矩阵
        /// </summary>
        public static IList<IList<T>> ToMatrix<T>(this IList<T> list,int row)
        {
            if ((list.Count % row) != 0)
                throw new ArgumentException("list不能被划分成row行的矩阵");
            T[][] res = new T[row][];
            int nowRow = 0;
            list.Select((value, index) => new { value, index })
                .GroupBy(pair => pair.index / (list.Count/row))
                .ForEach(group =>
                {
                    res[nowRow] = group.Select(pair => pair.value).ToArray();
                    nowRow++;
                });
            return res;
        }
    }
}
