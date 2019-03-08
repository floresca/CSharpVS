using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC_2_TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var stuff = new TicTacToe();
            stuff.CallSize();

        }
    }

    class TicTacToe
    {
        int input = 0;

        public void CallSize()
        {
            Console.Write("Enter a size: ");
            input = Convert.ToInt32(Console.ReadLine());
            Draw(input);
        }

        public void Draw(int size)
        {
            int countDown = size;
            int columnRow = size;


            do
            {
                for (int column = 0; column < columnRow; column++)
                {
                    Console.Write("   ");
                    if (column == columnRow - 1)
                    {
                        break;
                    }
                    Console.Write("|");
                }
                countDown--;
                Console.WriteLine();

                if (countDown > 0)
                {
                    for (int row = 0; row < columnRow; row++)
                    {
                        Console.Write("---");
                        if (row == columnRow - 1)
                        {
                            break;
                        }
                        Console.Write("+");
                    }
                    Console.WriteLine();
                }
            }
            while (countDown > 0);
        }
    }
}