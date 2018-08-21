using System;

namespace Speedster
{
    class Program
    {
        static void Main(string[] args)
        {
            var speedster = new EnterNumber();
            speedster.HowFast(130);
        }
    }

    public class EnterNumber
    {
        const int speedLimit = 70;

        public void HowFast(int speed)
        {
            var toofast = (speed - speedLimit) / 5;

            if (toofast >= 12)
            {
                Console.WriteLine("You have too many infractions, License Suspended");
            }
            else if (toofast > 0)
            {
                Console.WriteLine(toofast + " demerits");
            }
            else
            {
                Console.WriteLine("you are ok, carry on");
            }
            
        }
    }
}
