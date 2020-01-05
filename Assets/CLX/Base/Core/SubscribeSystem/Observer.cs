using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX
{
    

    public static class Observer
    {
        #region Create Functions
        public static IObserver<T> Create<T>(Action<T> onNext)
        {
            return new CommonObserver<T>(onNext, e => { }, () => { });
        }

        public static IObserver<T> Create<T>(Action<T> onNext, Action<Exception> onError)
        {
            return new CommonObserver<T>(onNext, onError, () => { });
        }

        public static IObserver<T> Create<T>(Action<T> onNext,Action onComplete)
        {
            return new CommonObserver<T>(onNext, e => { }, onComplete);
        }
        public static IObserver<T> Create<T>(Action<T>onNext,Action<Exception>onError,Action onComplete)
        {
            return new CommonObserver<T>(onNext, onError, onComplete);
        }
        #endregion

        class CommonObserver<T> : IObserver<T>
        {
            private Action onComplete;
            private Action<Exception> onError;
            private Action<T> onNext;
            public void OnComplete()
            {
                onComplete();
            }

            public void OnError(Exception ex)
            {
                onError(ex);
            }

            public void OnNext(T value)
            {
                onNext(value);
            }

            public CommonObserver(Action<T> onNext, Action<Exception> onError, Action onComplete)
            {
                this.onNext = onNext;
                this.onError = onError;
                this.onComplete = onComplete;
            }

        }
    }
}
