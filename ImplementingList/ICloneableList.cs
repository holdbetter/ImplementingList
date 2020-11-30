using System;
using System.Collections.Generic;
using System.Text;

namespace ImplementingList
{
    interface ICloneableList<T>
    {
        CustomList<T> Clone();
    }
}
