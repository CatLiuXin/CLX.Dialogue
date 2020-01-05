using CLX.Extensions.Generic;
using CLX.Extensions.Raws;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX
{
    public class ObjectPool<T> : IPool<T>
    {
        Queue<T> queue;
        Func<T> factory;
        Action<T> onReset;
        Action<T> onRecycle;

        public int Count => queue.Count;

        /// <summary>
        /// 对象池构造函数
        /// </summary>
        /// <param name="factory"> 创建对象的工厂方法 </param>
        /// <param name="initNumber"> 对象池内的初始化变量数，默认为4 </param>
        /// <param name="onReset"> 当申请对象时会对对象执行的操作，默认为无操作 </param>
        /// <param name="onReycle"> 当回收对象时会对对象执行的操作，默认为无操作 </param>
        public ObjectPool(Func<T> factory,int initNumber=4,Action<T> onReset=null,Action<T> onRecycle = null)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            queue = new Queue<T>(initNumber);
            1.To(initNumber).ForEach(_ => queue.Enqueue(factory()));
            this.factory  = factory;
            this.onReset  = onReset;
            this.onRecycle = onRecycle;
        }

        /// <summary>
        /// 申请分配一个T对象,并对其执行onReset方法
        /// </summary>
        public T Allocate()
        {
            T obj;
            if(queue.Count == 0)
            {
                obj = factory();
            }
            else
            {
                obj = queue.Dequeue();
            }
            onReset?.Invoke(obj);
            return obj;
        }

        /// <summary>
        /// 回收一个T对象，并对其执行onRecycle方法
        /// </summary>
        public void Recycle(T value)
        {
            onRecycle?.Invoke(value);
            queue.Enqueue(value);
        }

        public void Clear()
        {
            queue.Clear();
        }
    }
}
