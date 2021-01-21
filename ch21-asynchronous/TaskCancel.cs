using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IllustratedCSharp.ch21_asynchronous
{
    public class TaskCancel
    {
        public async Task RunAsync(CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;
            await Task.Run(() => CycleMethod(ct), ct);
        }

        void CycleMethod(CancellationToken ct)
        {
            Console.WriteLine("Starting CycleMethod");
            const int max = 5;
            for(int i = 0; i < max; i++)
            {
                if (ct.IsCancellationRequested) return;
                Thread.Sleep(1000);
                Console.WriteLine($"    {i + 1} of {max} iterations completed");
            }
        }

        public static void TaskCancelTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            TaskCancel mc = new TaskCancel();
            Task t = mc.RunAsync(token);

            Thread.Sleep(3000);
            cts.Cancel();

            t.Wait();
            Console.WriteLine($"Was Cancelled: {token.IsCancellationRequested}");
            Console.ReadLine();
        }
    }
}
