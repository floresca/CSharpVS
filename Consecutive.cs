using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consecutive
{
    class Program
    {
        static void Main(string[] args)
        {
            var initializer = new CongaLine();
            initializer.GetNumbers();
        }
    }

    class CongaLine
    {
        public List<int> numberList = new List<int>();
        int control;

        public void GetNumbers()
        {
            Console.Write("Enter a list of numbers separated by dashes: ");
            var numberGroup = Console.ReadLine();

            Validate(numberGroup);
        }

        public void Validate(string input)
        {
            string[] parts = input.Split('-');
            control = Convert.ToInt32(parts[0]);
            int counter;

            for(counter = 1; counter < parts.Length; counter++)
            {
                var newNum = Convert.ToInt32(parts[counter]);

                if(newNum < control)
                {
                    Console.WriteLine("Not consecutive");
                    break;
                }
                else
                {
                    control = newNum;
                }
            }

            if (counter == parts.Length)
            {
                Console.WriteLine("Consecutive");
            }
        }
    }
}