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
            stuff.Verticals();

        }
    }

    class TicTacToe
    {
        public void Verticals()
        {
            int columnCount = 0;
            int rowCount = 0;
            int dashCount = 0;
            int repeat = 0;

            if (repeat < 1)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (columnCount < 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i < 2)
                            {
                                Console.Write(" ");
                            }
                            else if (i == 2)
                            {
                                Console.Write("|");
                                i = -1;
                                columnCount++;
                                if (columnCount == 2)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (columnCount == 2)
                    {
                        repeat++;
                        Console.WriteLine();
                        for (int i = 0; i < 3; i++)
                        {
                            if (i < 2)
                            {
                                Console.Write("-");
                                dashCount++;
                            }
                            else if (i == 2)
                            {
                                if (rowCount == 2)
                                {
                                    rowCount = 0;
                                    columnCount = 0;
                                    dashCount = 0;
                                    break;
                                }
                                else
                                {
                                    Console.Write("+");
                                    i = -1;
                                    rowCount++;
                                }
                            }
                        }
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
