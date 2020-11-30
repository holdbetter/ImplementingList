using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ImplementingList
{
    class CustomList<T> : ICustomList<T>, ICustomCollection<T>, IEnumerable<T>, ICloneableList<T>
    {
        private const int DEFAULT_CAPACITY = 4;

        private T[] sourceArray = new T[0];
        private int count;

        public int Capacity
        {
            get => sourceArray.Length;
            set
            {
                if (value > sourceArray.Length)
                {
                    ArrayResize(ref sourceArray, value);
                }
                else
                {
                    throw new Exception("Capacity can't be less than current count");
                }
            }
        }

        public int Count
        {
            get => count;
            private set => count = value;
        }

        public T this[int index]
        {
            get => sourceArray[index];
            set => sourceArray[index] = value;
        }

        public CustomList()
        {
            this.Capacity = DEFAULT_CAPACITY;
        }

        public CustomList(int capacity)
        {
            this.Capacity = capacity;
        }

        public CustomList(IEnumerable<T> list)
        {
            AddRange(list);
        }

        public void Add(T item)
        {
            if (Count < Capacity)
            {
                sourceArray[Count] = item;
            }
            else
            {
                Capacity = Capacity < 4 ? 4 : Capacity * 2; 
            }

            Count += 1;
        }

        public void AddRange(IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                Add(item);
            }
        }

        public void Clear()
        {
            sourceArray = new T[0];
            Count = 0;
        }
        
        public CustomList<T> Clone()
        {
            CustomList<T> clonedList = new CustomList<T>();
            foreach (var item in sourceArray)
            {
                if (item is ICloneable)
                {
                    clonedList.Add(item);
                }
                else
                {
                    throw new Exception("Can't clone this list, because element aren't ICloneable");
                }
            }

            return clonedList;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (sourceArray[i].Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (sourceArray[i].Equals(item))
                {
                    return i;
                }
            }
            return -1;
        }

        public int IndexOf(T item, int index)
        {
            if (index > 0 && index < Count)
            {
                for (int i = index; i < Count; i++)
                {
                    if (sourceArray[i].Equals(item))
                    {
                        return i;
                    }
                }
                return -1;
            }
            throw new ArgumentOutOfRangeException();
        }

        public int IndexOf(T item, int index, int count)
        {
            int lastInItemIndex = index + count;
            if (index > 0 && lastInItemIndex < Count)
            {
                for (int i = index; i < lastInItemIndex; i++)
                {
                    if (sourceArray[i].Equals(item))
                    {
                        return i;
                    }
                }
                return -1;
            }
            throw new ArgumentOutOfRangeException();
        }

        public void Insert(T item, int index)
        {
            if (index > 0 && index < Count)
            {
                if (Count + 1 > Capacity)
                {
                    Capacity += 1;
                }

                int lastEmptyIndex = Count;
                for (int i = lastEmptyIndex; i > index; i--)
                {
                    sourceArray[i] = sourceArray[i - 1]; 
                }
                sourceArray[index] = item;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public bool Remove(T item)
        {
            int itemIndex = IndexOf(item);
            if (itemIndex != -1)
            {
                for (int i = itemIndex; i < Count; i++)
                {
                    sourceArray[itemIndex] = sourceArray[itemIndex - 1];
                }

                // Clear last element in array after left shifting
                sourceArray[Count - 1] = default;
                Count -= 1;
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                for (int i = index; i < Count; i++)
                {
                    sourceArray[index] = sourceArray[index - 1];
                }

                // Clear last element in array after left shifting
                sourceArray[Count - 1] = default;
                Count -= 1;
                return;
            }
            throw new ArgumentOutOfRangeException();
        }

        public void Reverse()
        {
            for (int i = 0, j = Count - 1; i < Count / 2; i++, j--)
            {
                T temp = sourceArray[j];
                sourceArray[j] = sourceArray[i];
                sourceArray[i] = temp;
            }
        }

        public void Reverse(int index, int count)
        {
            int lastInItemIndex = index + count - 1;
            if (index > 0 && lastInItemIndex < Count)
            {
                for (int i = index, j = lastInItemIndex, times = 0;
                    times < count / 2; 
                    i++, j--, times++)
                {
                    T temp = sourceArray[j];
                    sourceArray[j] = sourceArray[i];
                    sourceArray[i] = temp;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            string result = "{";
            foreach (var item in this)
            {
                result += $"  {item}";
            }
            result += "  }";
            return result;
        }

        private void ArrayResize(ref T[] source, int newSize)
        {
            T[] newArray = new T[newSize];
            for (int i = 0; i < source.Length; i++)
            {
                newArray[i] = source[i];
            }
            source = newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class ListEnumerator : IEnumerator<T>
        {
            private readonly CustomList<T> sourceArray;
            private int position = -1;

            public T Current => sourceArray[position];
            object IEnumerator.Current => Current;

            public ListEnumerator(CustomList<T> sourceArray)
            {
                this.sourceArray = sourceArray;
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (position + 1 < sourceArray.Count)
                {
                    position += 1;
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}
