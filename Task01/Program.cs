using System;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome");

            Console.WriteLine("First Currency:");
            string cur1 = Console.ReadLine().ToUpper();

            Console.WriteLine("Second Currency:");
            string cur2 = Console.ReadLine().ToUpper();

            var rt = new RateReader();
            var rate = rt.GetRate(cur1, cur2).Result;

            Console.WriteLine("Rate: " + rate.ToString("N4"));

            Console.ReadLine();
        }
    }
}
