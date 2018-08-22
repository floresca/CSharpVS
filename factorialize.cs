using System;

namespace Factorialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a number: ");
            var input = Convert.ToInt32(Console.ReadLine());
            int factorialized;

            if (input > 0)
            {
                factorialized = 1;
                for (var i = input; i > 1; i--)
                {
                    factorialized *= i;
                }

                Console.WriteLine(input + "!" + " = " + factorialized);
            }
        }
    }
}