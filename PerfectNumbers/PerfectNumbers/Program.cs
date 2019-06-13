using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PerfectNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            //Two connections of results
            List<int> perfectNumbers1 = new List<int>();
            List<int> perfectNumbers2 = new List<int>();

            //Initialising a numbers list
            List<int> numbers = new List<int>();
            for (int i = 0; i < 100000; i++)
                numbers.Add(i);

            //Initialising stopwatches
            Stopwatch sw1 = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            //No parallel method
            Task taskA = Task.Run(() =>
            {
                Console.WriteLine("Не параллельный способ начался...");
                sw1.Start();
                perfectNumbers1 = (from number in numbers where CheckNumber(number) select number).ToList(); //ToList() - to make them not lazy
                sw1.Stop();
                Console.WriteLine("Не параллельный способ завершился");
            });

            //Parallel method
            Task taskB = Task.Run(() =>
            {
                Console.WriteLine("Параллельный способ начался...");
                sw2.Start();
                perfectNumbers2 = (from number in numbers.AsParallel() where CheckNumber(number) select number).ToList();
                sw2.Stop();
                Console.WriteLine("Параллельный способ завершился");
            });

            //Printing
            Task finalTask = Task.Factory.ContinueWhenAll(new Task[] { taskA, taskB }, ant =>
            {
                Console.WriteLine("Числа: ");
                foreach (var number in perfectNumbers1)
                    Console.WriteLine(number);
                Console.WriteLine("Время работы обычной LINQ: " + sw1.ElapsedMilliseconds);
                Console.WriteLine("Время работы PLINQ: " + sw2.ElapsedMilliseconds);
            });

            Console.ReadKey();
        }

        //Check if the number is perfect
        public static bool CheckNumber(int num)
        {
            int sum = 1;
            for (int i = 2; i < num / 2 + 1; i++)
                if (num % i == 0)
                    sum += i;
            return (sum == num);
        }
    }
}
