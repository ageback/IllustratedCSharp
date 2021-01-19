using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch21_asynchronous
{
    class MyDownloadStringSingleThread
    {
        Stopwatch sw = new Stopwatch();

        public void DoRun()
        {
            const int LargeNumber = 6_000_000;
            sw.Start();
            int t1 = CountCharacters(1, "http://www.163.com");
            int t2 = CountCharacters(2, "https://www.baidu.com");
            CountToALargeNumber(1, LargeNumber);
            CountToALargeNumber(2, LargeNumber);
            CountToALargeNumber(3, LargeNumber);
            CountToALargeNumber(4, LargeNumber);
            Console.WriteLine($"Chars in http://www.163.com    : { t1 }");
            Console.WriteLine($"Chars in https://www.baidu.com    : { t2 }");
        }

        private int CountCharacters(int id, string uriString)
        {
            WebClient wc1 = new WebClient();
            Console.WriteLine("Starting call {0}    :    {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            string result = wc1.DownloadString(new Uri(uriString));
            Console.WriteLine("   Call {0} completed:    {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
            return result.Length;
        }

        private void CountToALargeNumber(int id, int value)
        {
            for (long i = 0; i < value; i++)
                ;
            Console.WriteLine("    End couting {0}  :    {1, 4:N0} ms", id, sw.Elapsed.TotalMilliseconds);
        }
    }
}
