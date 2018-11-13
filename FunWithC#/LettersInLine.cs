using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LettersInLine
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Enter a bunch of letters: ");
            string input = Console.ReadLine();

            char[] parts = input.ToCharArray();

            Array.Sort(parts);

            foreach(char letter in parts)
            {
                Console.WriteLine(letter);
            }

        }
    }
}
