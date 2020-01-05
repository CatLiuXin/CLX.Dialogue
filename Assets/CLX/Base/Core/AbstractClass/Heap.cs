using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX
{
    /// <summary>
    /// 堆
    /// </summary>
    public abstract class Heap<T> : IEnumerable<T>
    {
        /// <summary>
        /// 初始化容量
        /// </summary>
        private const int InitialCapacity = 0;
        /// <summary>
        /// 当当前容量达到目前最大容量时，对heap进行扩容时的扩容倍数
        /// </summary>
        private const int GrowFactor = 2;
        /// <summary>
        /// 最小要增长的数量
        /// </summary>
        private const int MinGrow = 1;

        /// <summary>
        /// 最大容量
        /// </summary>
        private int _capacity = InitialCapacity;
        /// <summary>
        /// 堆数组
        /// </summary>
        private T[] _heap = new T[InitialCapacity];
        /// <summary>
        /// 当前容量（尾
        /// </summary>
        private int _tail = 0;

        /// <summary>
        /// 堆内元素数量
        /// </summary>
        public int Count { get { return _tail; } }
        /// <summary>
        /// 堆最大容量
        /// </summary>
        public int Capacity { get { return _capacity; } }

        /// <summary>
        /// 比较器
        /// </summary>
        protected Comparer<T> Comparer { get; private set; }
        /// <summary>
        /// 比较函数，返回x按规则是否大于y
        /// </summary>
        protected abstract bool Dominates(T x, T y);

        protected Heap() : this(Comparer<T>.Default)
        {
        }

        protected Heap(Comparer<T> comparer) : this(Enumerable.Empty<T>(), comparer)
        {
        }

        protected Heap(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {
        }

        protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
        {
            if (collection == null) throw new ArgumentNullException("collection");
            if (comparer == null) throw new ArgumentNullException("comparer");

            Comparer = comparer;

            foreach (var item in collection)
            {
                if (Count == Capacity)
                    Grow();

                _heap[_tail++] = item;
            }

            for (int i = Parent(_tail - 1); i >= 0; i--)
                BubbleDown(i);
        }

        /// <summary>
        /// 加入元素
        /// </summary>
        public void Add(T item)
        {
            if (Count == Capacity)
                Grow();

            _heap[_tail++] = item;
            BubbleUp(_tail - 1);
        }
        /// <summary>
        /// 上浮元素
        /// </summary>
        private void BubbleUp(int i)
        {
            if (i == 0 || Dominates(_heap[Parent(i)], _heap[i]))
                return; //correct domination (or root)

            Swap(i, Parent(i));
            BubbleUp(Parent(i));
        }

        /// <summary>
        /// 得到堆的最前面的元素
        /// </summary>
        public T GetDominating()
        {
            if (Count == 0) throw new InvalidOperationException("Heap is empty");
            return _heap[0];
        }

        /// <summary>
        /// 去掉堆的最前面的元素
        /// </summary>
        /// <returns></returns>
        public T RemoveDominating()
        {
            if (Count == 0) throw new InvalidOperationException("Heap is empty");
            T ret = _heap[0];
            _tail--;
            Swap(_tail, 0);
            BubbleDown(0);
            return ret;
        }

        /// <summary>
        /// 下沉元素
        /// </summary>
        private void BubbleDown(int i)
        {
            int dominatingNode = Dominating(i);
            if (dominatingNode == i) return;
            Swap(i, dominatingNode);
            BubbleDown(dominatingNode);
        }

        /// <summary>
        /// 比较节点的左右俩元素取最大的元素返回其下标
        /// </summary>
        private int Dominating(int i)
        {
            int dominatingNode = i;
            dominatingNode = GetDominating(LeftChild(i), dominatingNode);
            dominatingNode = GetDominating(RightChild(i), dominatingNode);

            return dominatingNode;
        }

        private int GetDominating(int newNode, int dominatingNode)
        {
            if (newNode < _tail && !Dominates(_heap[dominatingNode], _heap[newNode]))
                return newNode;
            else
                return dominatingNode;
        }

        private void Swap(int i, int j)
        {
            T tmp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = tmp;
        }

        private static int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        private static int LeftChild(int i)
        {
            return (i + 1) * 2 - 1;
        }

        private static int RightChild(int i)
        {
            return (i + 1) * 2;
        }
        /// <summary>
        /// 堆增长大小
        /// </summary>
        private void Grow()
        {
            int newCapacity = _capacity * GrowFactor + MinGrow;
            var newHeap = new T[newCapacity];
            Array.Copy(_heap, newHeap, _capacity);
            _heap = newHeap;
            _capacity = newCapacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _heap.Take(Count).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
