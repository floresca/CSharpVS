using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCC_3_GameBoard
{
    class Program
    {
        static void Main(string[] args)
        {
            var stuff = new GameBoardSetUp();
            stuff.CallSize();

        }
    }

    class GameBoardSetUp : GameBoard
    {
        int width = 0;
        int height = 0;

        public void CallSize()
        {
            Console.Write("Enter a width: ");
            width = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter a height: ");
            height = Convert.ToInt32(Console.ReadLine());

            Draw(width, height);
        }
    }

    class GameBoard : Connect4GameSetUp
    {
        char arrayCount = 'A';

        public void Draw(int width, int height)
        {
            do
            {
                for (int column = 0; column < width; column++)
                {
                    Console.Write(" {0} ", arrayCount++);
                    if (column == width - 1)
                    {
                        break;
                    }
                    Console.Write("|");
                }
                height--;
                Console.WriteLine();

                if (height > 0)
                {
                    for (int row = 0; row < width; row++)
                    {
                        Console.Write("---");
                        if (row == width - 1)
                        {
                            break;
                        }
                        Console.Write("+");
                    }
                    Console.WriteLine();
                }
            }
            while (height > 0);
        }
    }

    class Connect4GameSetUp
    {
        int tokenLocation = 0;
        string userColumnSelect = null;
        List<int> arrayRed = new List<int> { };       
        List<int> arrayBlue = new List<int> { };
        IDictionary<int, string> boardPieces = new Dictionary<int, string>();
        List<string> columnA = new List<string> { };
        bool isSpaceTaken = false;
    }
}