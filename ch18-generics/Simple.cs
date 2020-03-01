using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch18_generics
{
    class Simple
    {
        static public void ReverseAndPrint<T>(T[] arr)
        {
            Array.Reverse(arr);
            foreach(T item in arr)
            {
                Console.WriteLine($"{ item.ToString()}, ");
            }
            Console.WriteLine("");
        }
    }
}
