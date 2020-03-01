using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch14_delegates
{
    /// <summary>
    /// 声明一个 delegate
    /// </summary>

    delegate void MyDel(int value);
    class DelegateDemo
    {
        void PrintLow(int value)
        {
            Console.WriteLine($"{ value } - Low Value");
        }

        void PrintHigh(int value)
        {
            Console.WriteLine($"{ value } - High Value");
        }

        public static void TestMyDel()
        {
            DelegateDemo demo = new DelegateDemo();

            // 声明 MyDel 类型的变量 
            MyDel del;
            
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int randomValue = rand.Next(99);

                del = randomValue < 50 ? new MyDel(demo.PrintLow) : new MyDel(demo.PrintHigh);
                del?.Invoke(randomValue);
            }
        }
    }
}
