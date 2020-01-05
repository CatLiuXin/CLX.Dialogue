using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX
{
    public class MaxHeap<T> : Heap<T>
    {
        public MaxHeap()
            : this(Comparer<T>.Default)
        {
        }

        public MaxHeap(Comparer<T> comparer)
            : base(comparer)
        {
        }

        public MaxHeap(IEnumerable<T> collection, Comparer<T> comparer)
            : base(collection, comparer)
        {
        }

        public MaxHeap(IEnumerable<T> collection) : base(collection)
        {
        }

        /// <summary>
        /// 正向比较
        /// </summary>
        protected override bool Dominates(T x, T y)
        {
            return Comparer.Compare(x, y) >= 0;
        }
    }
}
