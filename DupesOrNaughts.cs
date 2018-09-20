using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DupesOrNaughts
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = new Duper();
            start.GetNumbers();
        }
    }

    class Duper
    {
        public List<int> numberList = new List<int>();

        public void GetNumbers()
        {
            Console.Write("Enter a list of numbers separated by dashes: ");
            var numberGroup = Console.ReadLine();

            Validate(numberGroup);
        }

        public void Validate(string input)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                Environment.Exit(0);
            }
            else
            {
                DupeLocator(input);
            }
        }

        public void DupeLocator(string duper)
        {
            string[] newArr = duper.Split('-');
            int control;

            foreach(string dupe in newArr)
            {
                numberList.Add(Convert.ToInt32(dupe));
            }

            do
            {
                control = numberList[0];
                numberList.RemoveAt(0);

                if (numberList.Contains(control))
                {
                    Console.WriteLine("There are Duplicates");
                    break;
                }
            }
            while (numberList.Count > 1);
        }
    }
}