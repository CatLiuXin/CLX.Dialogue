using System;

namespace CLX
{
    public interface IObserver<T>
    {
        void OnComplete();
        void OnError(Exception ex);
        void OnNext(T value);
    }
}
