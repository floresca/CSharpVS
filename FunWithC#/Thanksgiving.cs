using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello
{
    class Program
    {
        static void Main(string[] args)
        {
            var greet = new SayHello();
            greet.hola();
        }
    }

    class SayHello
    {
        public string input;
        
        
        public void hola()
        {
            while(true)
            {
                Console.Write("Enter your name: ");
                input = Console.ReadLine();

                if (input == "end")
                {
                    Environment.Exit(0);
                }
                else
                {
                    string welcome = "Hello " + input;
                     
                    Console.WriteLine(welcome);
                }
            }
        }
    }
}