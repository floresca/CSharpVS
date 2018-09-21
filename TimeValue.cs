using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeValue
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Time
    {
        string militaryTime;

        public void GetTime()
        {
            Console.Write("Enter a time, 24hr format: ");
            militaryTime = Console.ReadLine();

            Validate(militaryTime);
        }

        public void Validate(string input)
        {

        }
    }
}