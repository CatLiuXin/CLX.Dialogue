using System;

namespace CLX
{
    public class EventValue<T>
    {
        T value;
        Action<T, T> onValueChange;

        public T Value
        {
            get { return value; }
            set
            {
                onValueChange(this.value, value);
                this.value = value;
            }
        }

        /// <summary>
        /// 事件值类型
        /// 每次改变值时，都会执行onChangeValue
        /// </summary>
        /// <param name="initValue"> 初始化值 </param>
        /// <param name="onValueChange"> 
        /// 当值发生改变时会执行的函数
        /// 其第一个T类型参数是之前的值
        /// 后一个T类型参数是所赋予的值
        /// </param>
        public EventValue(T initValue,Action<T,T> onValueChange)
        {
            if (onValueChange == null) throw new ArgumentNullException("onValueChange");
            value = initValue;
            this.onValueChange = onValueChange;
        }
    }
}
