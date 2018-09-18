using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniqueNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var arrayedNumbers = new Uniqueness();
            arrayedNumbers.GetNumbers();
            arrayedNumbers.Publish();
        }
    }

    class Uniqueness
    {
        public List<int> numberList = new List<int>();

        public void GetNumbers()
        {
            while (true)
            {
                Console.Write("Enter a number or type 'Quit' to end: ");
                var isItaNumber = Console.ReadLine();

                if (isItaNumber.Contains("Quit"))
                {
                    break;
                }
                else
                {
                    var inputNumber = Convert.ToInt32(isItaNumber);
                    RemoveDuplicates(inputNumber);
                }
            }
        }

        public void RemoveDuplicates(int unique)
        {
            if (numberList.Contains(unique))
            {
                numberList.RemoveAt(numberList.IndexOf(unique));
            }
            else
            {
                numberList.Add(unique);
            }
        }

        public void Publish()
        {
            foreach (int uniqueNumber in numberList)
            {
                Console.Write("{0}, ", uniqueNumber);
            }
            Console.WriteLine();
        }
    }
}