using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeSmallestNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var smallest = new Numbers();
            smallest.GetNumbers();
        }
    }

    class Numbers
    {
        public List<int> numberList = new List<int>();
        string input;

        public void GetNumbers()
        {
            Console.Write("Enter a list of at least 5 comma separated numbers: ");
            input = Console.ReadLine();

            Validate(input);
        }

        public void Validate(string unique)
        {
            string [] pieces = unique.Split(',');
            
            if (pieces.Length < 5)
            {
                Console.WriteLine("Invalid List");
            }
            else
            {
                foreach(string item in pieces)
                {
                    numberList.Add(Convert.ToInt32(item));
                }

                SortNumbers();
            }
        }

        public void SortNumbers()
        {
            numberList.Sort();
            Publish();
        }

        public void Publish()
        {
            for(int i = 0; i < 3; i++)
            {
                Console.Write("{0} ", numberList[i]);
            }
            Console.WriteLine();
        }
    }
}