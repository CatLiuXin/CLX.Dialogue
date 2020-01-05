using CLX.Extensions.Generic;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CLX
{
    public class EventCollection<T>: ICollection<T>
    {
        ICollection<T> collection;
        readonly Action<T> onAdd;
        readonly Action<T> onRemove;
        readonly Action<T> onClear;
        public EventCollection(ICollection<T> collection,Action<T>onAdd=null,Action<T>onRemove=null,Action<T>onClear=null)
        {
            this.collection = collection;
            this.onAdd = onAdd;
            this.onClear = onClear;
            this.onRemove = onRemove;
        }

        public int Count => collection.Count;

        public bool IsReadOnly => collection.IsReadOnly;

        public void Add(T item)
        {
            collection.Add(item);
            onAdd?.Invoke(item);
        }

        public void Clear()
        {
            if (onClear != null)
            {
                collection.ForEach(v => onClear(v));
            }
            collection.Clear();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public bool Remove(T item)
        {
            if (collection.Remove(item))
            {
                onRemove?.Invoke(item);
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
    }
}
