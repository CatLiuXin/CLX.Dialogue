using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX
{
    public class MinHeap<T> : Heap<T>
    {
        public MinHeap()
            : this(Comparer<T>.Default)
        {
        }

        public MinHeap(Comparer<T> comparer)
            : base(comparer)
        {
        }

        public MinHeap(IEnumerable<T> collection) : base(collection)
        {
        }

        public MinHeap(IEnumerable<T> collection, Comparer<T> comparer)
            : base(collection, comparer)
        {
        }

        /// <summary>
        /// 反向比较
        /// </summary>
        protected override bool Dominates(T x, T y)
        {
            return Comparer.Compare(x, y) <= 0;
        }
    }
}
