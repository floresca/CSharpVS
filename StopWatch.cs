using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StopWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.UserPrompt();
        }
    }

    class Stopwatch
    {
        bool running = false;
        int hours;
        int minutes;
        int seconds;

        public void UserPrompt()
        {
            while (true)
            {
                Console.Write("Enter 'start' to begin and 'stop' to end the program: ");
                string input = Console.ReadLine();

                if (input == "start")
                {
                    Start();
                }
                else if (input == "stop")
                {
                    End();
                }
                else
                {
                    Console.WriteLine("Invalid, please try again");
                }
            }
        }

        public void Start()
        {
            if (running == true)
            {
                Console.WriteLine("Time already Running, type 'stop' to stop the clock");
            }
            else
            {
                var time = DateTime.Now;
                hours = time.Hour;
                minutes = time.Minute;
                seconds = time.Second;
                running = true;
            }
        }

        int endHours;
        int endMinutes;
        int endSeconds;

        public void End()
        {
            if (running == false)
            {
                Console.WriteLine("The watch is not running, type 'start' to start");
            }
            else
            {
                var time = DateTime.Now;
                endHours = time.Hour;
                endMinutes = time.Minute;
                endSeconds = time.Second;

                Result();
            }
        }

        public void Result()
        {
            Console.WriteLine("The clock ran for {0} hours, {1} minutes, and {2} seconds", Math.Abs(hours - endHours), Math.Abs(minutes - endMinutes), Math.Abs(seconds - endSeconds));
            Environment.Exit(0);
        }
    }
}