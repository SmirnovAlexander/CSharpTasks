using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PerfectNumbers
{
    /// <summary>
    /// Class for calculating time and printing it to console.
    /// </summary>
    class PerfectNumbers
    {
        static void Main(string[] args)
        {
            // Two lists with results.
            List<int> perfectNumbers1 = new List<int>();
            List<int> perfectNumbers2 = new List<int>();

            // Initialising stopwatches.
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            // No parallel method.
            Task taskA = Task.Run(() =>
            {
                Console.WriteLine("LINQ way starts...");
                sw1.Start();
                perfectNumbers1 = Calculations.LinqPerfectNumbers(10000);
                sw1.Stop();
                Console.WriteLine("LINQ way ended.");
            });

            // Parallel method.
            Task taskB = Task.Run(() =>
            {
                Console.WriteLine("PLINQ way starts...");
                sw2.Start();
                perfectNumbers2 = Calculations.PlinqPerfectNumbers(10000);
                sw2.Stop();
                Console.WriteLine("PLINQ way ended.");
            });

            // Printing results.
            Task finalTask = Task.Factory.ContinueWhenAll(new Task[] { taskA, taskB }, ant =>
            {
                Console.WriteLine("Numbers: ");
                foreach (var number in perfectNumbers1)
                    Console.WriteLine(number);
                Console.WriteLine("Time of LINQ: " + sw1.ElapsedMilliseconds);
                Console.WriteLine("Time of PLINQ: " + sw2.ElapsedMilliseconds);
            });

            Console.ReadKey();
        }
    }
}
