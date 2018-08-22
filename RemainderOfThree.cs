using System;

namespace DivisibleByThree
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 100; i++)
            {
                if (i%3 == 0)
                    Console.WriteLine(i);
            }
        }
    }
}