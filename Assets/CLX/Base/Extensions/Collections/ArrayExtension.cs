using CLX.Extensions.Generic;
using System;
using System.Collections.Generic;

namespace CLX.Extensions.Collections
{
    #region Sort Functions
    public static partial class ArrayExtension
    {
        /// <summary>
        /// 将数组按指定方法排序后返回
        /// </summary>
        public static T[] Sort<T>(this T[] array,Comparison<T> comparison)
        {
            Array.Sort<T>(array, comparison);
            return array;
        }

        /// <summary>
        /// 将数组从index个元素起的length个元素进行排序返回
        /// </summary>
        public static T[] Sort<T>(this T[] array, int index, int length, Comparer<T> comparer=null)
        {
            comparer = comparer ?? Comparer<T>.Default;
            Array.Sort<T>(array,index,length, comparer);
            return array;
        }

        /// <summary>
        /// 将数组按默认排序后返回
        /// </summary>
        public static T[] Sort<T>(this T[] array)
        {
            Array.Sort(array, Comparer<T>.Default);
            return array;
        }

        /// <summary>
        /// 返回一个排序后的副本
        /// </summary>
        public static T[] Sorted<T>(this T[] array, Comparison<T> comparison)
        {
            return array.CloneWithType()
                        .Sort(comparison);
        }

        /// <summary>
        /// 将数组的副本从index个元素起的length个元素进行排序返回
        /// </summary>
        public static T[] Sorted<T>(this T[] array, int index, int length, Comparer<T> comparer=null)
        {
            return array.CloneWithType()
                        .Sort( index, length,comparer);
        }

        /// <summary>
        /// 将数组的副本按默认排序后返回
        /// </summary>
        public static T[] Sorted<T>(this T[] array)
        {
            return array.CloneWithType().Sort();
        }
    }
    #endregion

    #region Reverse Functions
    public static partial class ArrayExtension
    {
        /// <summary>
        /// 将数组反转返回
        /// </summary>
        public static T[] ReverseArray<T>(this T[] array)
        {
            Array.Reverse(array);
            return array;
        }

        /// <summary>
        /// 将数组从index个元素起后的length个元素进行反转返回
        /// </summary>
        public static T[] ReverseArray<T>(this T[] array, int index, int length)
        {
            Array.Reverse(array, index, length);
            return array;
        }

        /// <summary>
        /// 将数组的副本反转返回
        /// </summary>
        public static T[] ReversedArray<T>(this T[] array)
        {
            return array.CloneWithType()
                        .ReverseArray();
        }

        /// <summary>
        /// 将数组的副本从index个元素起后的length个元素进行反转返回
        /// </summary>
        public static T[] ReversedArray<T>(this T[] array, int index, int length)
        {
            return array.CloneWithType()
                        .ReverseArray(index, length);
        }

        public static string CombineToString(this char[] array)
        {
            return new System.Text.StringBuilder().Append(array).ToString();
        }
    }
    #endregion
    
}