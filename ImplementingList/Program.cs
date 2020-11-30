using System;
using System.Collections.Generic;

namespace ImplementingList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> integers = new List<int>() { 19, 12, 89, 33 };
            CustomList<int> customList = new CustomList<int>()
            {
                19, 12, 89, 33
            };

            customList.Reverse(1, 3);
            integers.Reverse(1, 2);

            foreach (var item in integers)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();

            Console.WriteLine(customList.ToString());
        }
    }
}
