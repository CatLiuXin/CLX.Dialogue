using System;
using System.Collections.Generic;

namespace CLX.Extensions.Generic
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            if (action == null) throw new NullReferenceException();
            foreach (T value in values)
            {
                action(value);
            }
        } 
    }
}
