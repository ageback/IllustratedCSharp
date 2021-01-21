using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IllustratedCSharp.ch21_asynchronous
{
    public static class DoAsyncStuff
    {
        public static async ValueTask<int> CalculateSumAsync(int i1, int i2)
        {
            if (i1 == 0) return i2;
            int sum = await Task<int>.Run(() => GetSum(i1, i2));
            return sum;
        }

        private static int GetSum(int i1, int i2) => i1 + i2;
    }
}
