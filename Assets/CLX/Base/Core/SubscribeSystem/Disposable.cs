using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLX
{
    public static class Disposable
    {
        public static readonly IDisposable Empty = EmptyDisposable.Instance;

        public static IDisposable Create(Action action)
        {
            return new AnonymousDisposable(action);
        }

        class EmptyDisposable : IDisposable
        {
            public static EmptyDisposable Instance=new EmptyDisposable();
            EmptyDisposable()
            {
            }
            public void Dispose()
            {
                
            }
        }

        class AnonymousDisposable : IDisposable
        {
            bool isDisposed = false;
            readonly Action dispose;

            public AnonymousDisposable(Action dispose)
            {
                this.dispose = dispose;
            }

            public void Dispose()
            {
                if (!isDisposed)
                {
                    isDisposed = true;
                    dispose();
                }
            }
        }
    }
}
