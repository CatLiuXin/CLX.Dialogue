using System;
using System.Collections.Generic;

namespace CLX.Extensions.Generic
{
    public static class NormalExtension
    {
        public static T CloneWithType<T>(this T cloneable)where T:ICloneable
        {
            return (T)cloneable.Clone();
        }

        public static T Do<T>(this T obj,Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            action();
            return obj;
        }

        public static T Do<T>(this T obj,Action<T> action)
        {
            if (action == null) throw new ArgumentNullException("action");
            action(obj);
            return obj;
        }

        /// <summary>
        /// 使用IComparer类的对象进行比较，看t的值大小是否在lowerBound与UpperBound中间
        /// </summary>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound,
            IComparer<T> comparer,bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");

            var lowerCompareResult = comparer.Compare(t, lowerBound);
            var upperCompareResult = comparer.Compare(t, upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }

        /// <summary>
        /// 使用委托比较方法，看t的值大小是否在lowerBound与UpperBound中间
        /// </summary>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound,
            Comparison<T> comparison, bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (comparison == null) throw new ArgumentNullException("comparison");

            var lowerCompareResult = comparison(t, lowerBound);
            var upperCompareResult = comparison(t, upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }

        /// <summary>
        /// 使用默认的比较方法进行比较，看t的值大小是否在lowerBound与UpperBound中间
        /// </summary>
        public static bool IsBetweenDefault<T>(this T t,T lowerBound,T upperBound,
            bool includeLowerBound=false,bool includeUpperBound = false)
        {
            return t.IsBetween(lowerBound, upperBound,
                Comparer<T>.Default, includeLowerBound, includeUpperBound);
        }
        
    }
}
