using System;
using System.Collections.Generic;
using System.Text;

namespace ImplementingList
{
    interface ICustomList<T> : ICustomCollection<T>, IEnumerable<T>
    {
        T this[int index] { get; set; }

        int IndexOf(T item);
        void Insert(T item, int index);
        void RemoveAt(int index);
    }
}
