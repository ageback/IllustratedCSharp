﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch21_asynchronous
{
    class MyDownloadString
    {
        Stopwatch sw = new Stopwatch();

        public void DoRun()
        {
            const int LargeNumber = 30000000;
            sw.Start();
            Task<int> t1 = CounterCharactersAsync(1, "https://www.baidu.com");
            Task<int> t2 = CounterCharactersAsync(2, "http://www.qq.com");

            //Task.WaitAll(t1, t2);
            //Task.WaitAny(t1, t2);

            Console.WriteLine("Task 1: {0} Finished", t1.IsCompleted ? "" : "Not");
            Console.WriteLine("Task 2: {0} Finished", t2.IsCompleted ? "" : "Not");

            Console.ReadLine();

            CountToALargeNumber(1, LargeNumber);
            CountToALargeNumber(2, LargeNumber);
            CountToALargeNumber(3, LargeNumber);
            CountToALargeNumber(4, LargeNumber);

            Console.WriteLine($"Chars in https://www.baidu.com      : { t1.Result }");
            Console.WriteLine($"Chars in http://www.qq.com    : { t2.Result }");
        }

        private async Task<int> CounterCharactersAsync(int id,string uriString)
        {
            WebClient wc1 = new WebClient();
            Console.WriteLine("Starting call {0}    :    {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            string result = await wc1.DownloadStringTaskAsync(new Uri(uriString));
            Console.WriteLine("    Call {0} completed:    {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            return result.Length;
        }

        private void CountToALargeNumber(int id,int value)
        {
            for (long i = 0; i < value; i++)
                ;
            Console.WriteLine("    End couting {0}  :    {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
        }
    }
}
