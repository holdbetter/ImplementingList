using System;
using System.Collections.Generic;
using System.Text;

namespace ImplementingList
{
    interface ICustomCollection<T> : IEnumerable<T>
    {
        int Count { get; }

        void Add(T item);
        void Clear();
        bool Contains(T item);
        bool Remove(T item);
    }
}
