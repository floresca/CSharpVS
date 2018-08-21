using System;

namespace First_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var TOvalidate = new EnterNumber();
            TOvalidate.validator = -5;
            TOvalidate.Validating();
        }
    }

    public class EnterNumber
    {
        const int lowNumber = 1;
        const int highNumber = 10;
        public int validator;

        public void Validating()
        {
            if (validator < lowNumber)
            {
                Console.WriteLine(validator + " is less than " + lowNumber + " and is INVALID");
            }
            else if (validator > highNumber)
            {
                Console.WriteLine(validator + " is greater than " + highNumber + " and is INVALID");
            }
            else
            {
                Console.WriteLine(validator + " is within " + lowNumber + " and " + highNumber + " and is VALID");
            }
        }
    }
}
