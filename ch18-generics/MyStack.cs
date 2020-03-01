using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch18_generics
{
    class MyStack<T> 
    {
        T[] StackArray;
        int StackPointer = 0;

        const int MaxStack = 10;
        bool IsStackFull
        {
            get { return StackPointer >= MaxStack; }
        }

        bool IsStackEmpty
        {
            get { return StackPointer <= 0; }
        }

        public void Push(T x)
        {
            if(!IsStackFull)
            {
                StackArray[StackPointer++] = x;
            }
        }

        public T Pop()
        {
            return (!IsStackEmpty) ? StackArray[--StackPointer] : StackArray[0];
        }

        public MyStack()
        {
            StackArray = new T[MaxStack];
        }

        public void Print()
        {
            for (int i = StackPointer - 1; i >= 0; i--)
            {
                Console.WriteLine($"    Value:{StackArray[i]}");
            }
        }

        
    }
}
