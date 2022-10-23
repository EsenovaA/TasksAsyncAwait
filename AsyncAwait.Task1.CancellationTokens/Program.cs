/*
* Study the code of this application to calculate the sum of integers from 0 to N, and then
* change the application code so that the following requirements are met:
* 1. The calculation must be performed asynchronously.
* 2. N is set by the user from the console. The user has the right to make a new boundary in the calculation process,
* which should lead to the restart of the calculation.
* 3. When restarting the calculation, the application should continue working without any failures.
*/

using System;
using System.Threading;

namespace AsyncAwait.Task1.CancellationTokens;

internal class Program
{
    /// <summary>
    /// The Main method should not be changed at all.
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
        CancellationTokenSource cts = null;

        Console.WriteLine("Mentoring program L2. Async/await.V1. Task 1");
        Console.WriteLine("Calculating the sum of integers from 0 to N.");
        Console.WriteLine("Use 'q' key to exit...");
        Console.WriteLine();

        Console.WriteLine("Enter N: ");

        var input = Console.ReadLine();
        while (input.Trim().ToUpper() != "Q") // Warning: theoretically input could be null here
        {
            if (int.TryParse(input, out var n))
            {
                cts?.Cancel(); // Warning: I wouldn't call it first time, but I'd call it when Q is pressed
                cts = new CancellationTokenSource();    
                CalculateSum(n, cts.Token);
            }
            else
            {
                Console.WriteLine($"Invalid integer: '{input}'. Please try again.");
                Console.WriteLine("Enter N: ");
            }

            input = Console.ReadLine();
        }

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

    private static async void CalculateSum(int n, CancellationToken token)
    {
        // todo: make calculation asynchronous
        try
        {
            var sum = await Calculator.CalculateAsync(n, token);
            Console.WriteLine($"Sum for {n} = {sum}.");
        }
        catch (Exception) // Error: only cancellation exception should be caught
        {
            Console.WriteLine($"Sum for {n} cancelled...");
            return;
        }
        
        Console.WriteLine();

        Console.WriteLine("Enter N: ");
    }
}
