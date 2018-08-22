using System;

namespace SumOfNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int total = 0;
            int newNum;

            while(true)
            {
                Console.Write("Enter a number or OK to exit: ");
                var value = Console.ReadLine();

                if (value.Contains("OK"))
                {
                    break;
                }
                else
                {
                    newNum = Convert.ToInt32(value);
                    total += newNum;
                    Console.WriteLine(total);
                }
            }
        }
    }
}