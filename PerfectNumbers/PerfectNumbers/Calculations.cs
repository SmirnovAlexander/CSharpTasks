using System.Collections.Generic;
using System.Linq;

namespace PerfectNumbers
{
    /// <summary>
    /// Plinq and linq variants of calculation perfect numbers.
    /// </summary>
    public class Calculations
    {
        // Counting using Plinq.
        public static List<int> PlinqPerfectNumbers(int number) {
           
            //Initialising a numbers list
            List<int> numbers = new List<int>();
            for (int i = 0; i < number; i++)
                numbers.Add(i);

            return (from nb in numbers.AsParallel() where CheckNumber(number) select number).ToList();
        }

        // Counting using linq.
        public static List<int> LinqPerfectNumbers(int number)
        {

            //Initialising a numbers list
            List<int> numbers = new List<int>();
            for (int i = 0; i < number; i++)
                numbers.Add(i);

            return (from nb in numbers where CheckNumber(number) select number).ToList();
        }

        // Check if the number is perfect.
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
