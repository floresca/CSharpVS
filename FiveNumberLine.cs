using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveNumberLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberCongaLine = new NumberList();
            numberCongaLine.GetNumbers();
            numberCongaLine.SortNumbers();
            numberCongaLine.Publish();
        }
    }

    class NumberList
    {
        int inputNumber = 0;
        public List<int> numberList = new List<int>();
        int counter = 5;

        public void GetNumbers()
        {
            while(numberList.Count < 5)
            {
                Console.Write("Enter a unique number: ");
                inputNumber = Convert.ToInt32(Console.ReadLine());

                AvoidDuplicates(inputNumber);

                if (counter > 0)
                {
                    Console.WriteLine("Please enter {0} more numbers", counter);
                }
            }
        }

        public void AvoidDuplicates(int unique)
        {
            if(numberList.Contains(unique))
            {
                Console.WriteLine("That number has already been entered, please try again");
            }
            else
            {
                numberList.Add(unique);
                counter--;
            }
        }

        public void SortNumbers()
        {
            numberList.Sort();
        }

        public void Publish()
        {
            Console.WriteLine(numberList[numberList.Count - 1]);
        }
    }
}