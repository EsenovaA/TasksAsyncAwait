using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task1.CancellationTokens;

internal static class Calculator
{
    // todo: change this method to support cancellation token
    public static async Task<long> CalculateAsync(int n, CancellationToken token)
    {
        return await Task.Run(() => CalculateSync(n, token)); //Warning: better to use overloaded Task.Run method that has CancellationToken param
    }

    private static long CalculateSync(int n, CancellationToken token)
    {
        Console.WriteLine($"The task for {n} started... Enter N to cancel the request:");

        long sum = 0;

        for (var i = 0; i < n; i++)
        {
            // i + 1 is to allow 2147483647 (Max(Int32)) 

            token.ThrowIfCancellationRequested();

            sum = sum + (i + 1);
            Thread.Sleep(500);
        }

        return sum;
    }
}
