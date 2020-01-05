using System;

namespace CLX
{
    public interface IObserable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }
}
