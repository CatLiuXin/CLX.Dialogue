using System.Collections.Generic;

namespace CLX.Extensions.Collections
{
    public static class QueueExtension
    {
        /// <summary>
        /// 便于使用 new Queue<T>{} 来初始化Queue
        /// </summary>
        public static Queue<T> Add<T>(this Queue<T> queue,T value)
        {
            queue.Enqueue(value);
            return queue;
        } 

        public static bool IsEmpty<T>(this Queue<T> queue)
        {
            return queue.Count == 0;
        }
    }
}
