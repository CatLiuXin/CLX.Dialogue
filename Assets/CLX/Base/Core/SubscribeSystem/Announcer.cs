using System;
using System.Collections.Generic;

namespace CLX
{
    public class Announcer<T> : IObserable<T>
    {
        LinkedList<IObserver<T>> list=new LinkedList<IObserver<T>>();
        public IDisposable Subscribe(IObserver<T> observer)
        {
            list.AddLast(new LinkedListNode<IObserver<T>>(observer));
            return Disposable.Create(() => {
                Unsubscribe(observer);
            });
        }
        public void Announce(T value)
        {
            foreach(var observer in list)
            {
                try
                {
                    observer.OnNext(value);
                }
                catch(Exception ex)
                {
                    observer.OnError(ex);
                }
            }
        }

        public void Unsubscribe(IObserver<T> observer)
        {
            list.Remove(observer);
        }
        public void OnComplete()
        {
            foreach (var observer in list)
            {
                observer.OnComplete();
            }
            list.Clear();
        }
    }
}
