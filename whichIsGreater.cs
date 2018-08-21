using System;

namespace WhichIsGreater
{
    class Program
    {
        static void Main(string[] args)
        {
            var greater = new EnterNumber();
            greater.Testers(7, 5);
        }
    }

    public class EnterNumber
    {

        public void Testers(int a, int b)
        {
            if (a > b)
            {
                Console.WriteLine(a);
            }
            else
            {
                Console.WriteLine(b);
            }
        }
    }
}