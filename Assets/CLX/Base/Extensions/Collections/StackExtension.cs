using System.Collections.Generic;

namespace CLX.Extensions.Collections
{
    public static class StackExtension
    {
        /// <summary>
        /// 方便使用 new Stack{} 的形式初始化栈
        /// </summary>
        public static Stack<T> Add<T>(this Stack<T> stack,T value)
        {
            stack.Push(value);
            return stack;
        }

        /// <summary>
        /// 判断一个栈是否为空
        /// </summary>
        public static bool IsEmpty<T>(this Stack<T> stack)
        {
            return stack.Count == 0;
        }
    }
}
