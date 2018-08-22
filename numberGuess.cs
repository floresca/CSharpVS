using System;


namespace RandoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            int random = new Random().Next(1, 10);

            int i = 0;
            while(i < 4)
            {
                Console.Write("Guess the number between 1 and 10!: ");
                var guess = Console.ReadLine();

                if (Convert.ToInt32(guess) == random)
                {
                    Console.WriteLine("Congrats! you guessed the number " + random + "!");
                    break;
                }
                else if (i == 3)
                {
                    Console.WriteLine("The odds were not in your favor, the number is " + random);
                    break;
                }
                else
                {
                    Console.WriteLine("Nope, you have " + (3 - i) + " tries left!");
                }
                i++;
            }
        }
    }
}
