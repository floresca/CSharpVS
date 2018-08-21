using System;

namespace Landscaping
{
    class Program
    {
        static void Main(string[] args)
        {
            var greater = new EnterNumber();
            greater.HowWide(85, 100);
        }
    }

    public class EnterNumber
    {

        public void HowWide(int height, int width)
        {
            if (height > width)
            {
                Console.WriteLine("This image is a portrait");
            }
            else
            {
                Console.WriteLine("This image is a landscape");
            }
        }
    }
}
