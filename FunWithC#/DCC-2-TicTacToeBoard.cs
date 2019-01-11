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
            int columns = 3 * size;
            int rows = 3 * size;
            int repeat = 0;
            int specialRow = 0;
            int finalCount = 0;
            char spaces = 'A';
            
            do
            {
                //Rows and column pipes print with the following loop
                for (int i = 0; i < size; i++)
                {
                    do
                    {
                        for (int j = 0; j < size; j++)
                        {
                            Console.Write(" {0} ", spaces);
                            repeat++;
                            repeat++;
                        }

                        if (repeat == columns)
                        {
                            break;
                        }
                        //Pipes will only print as long as blank spaces have already printed
                        else if (repeat < columns)
                        {
                            Console.Write("|");
                        }
                    }
                    while (repeat < columns);
                    repeat = 0;
                    Console.WriteLine();
                }
                specialRow++;
                
                //Using final count to keep track of how many special rows are being printed. I only want 2 special rows
                finalCount++;
                
                //SPecial rows iterate through the following loop
                if (specialRow < 3)
                {
                    do
                    {
                        for (int j = 0; j < size; j++)
                        {
                            Console.Write("---");
                            repeat++;
                        }

                        if (repeat == columns)
                        {
                            break;
                        }
                        else if (repeat < columns)
                        {
                            Console.Write("+");
                        }
                    }
                    while (repeat < columns);
                    repeat = 0;
                    Console.WriteLine();
                }
            }
            while (finalCount < 3);
        }
    }
    
    class TicTacToeGame : TicTacToe
    {
        public List<string> gamePositions = new List<string>();

        public void CallGamePieces()
        {
            while (true)
            {
                Console.Write("Enter X or O and a position: ");
                string input = Console.ReadLine();

                if (input == "end")
                {
                    Environment.Exit(0);
                }
                else
                {
                    gamePositions.Add(input);

                    foreach (string piece in gamePositions)
                    {
                        Console.WriteLine(piece);
                    }
                }
            }
        }
    }
}
}



//---------------------------------------------------------------------- VERSION 1 BELOW

// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace DCC_2_TicTacToe
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var stuff = new TicTacToe();
//             stuff.Verticals();

//         }
//     }

//     class TicTacToe
//     {
//         public void Verticals()
//         {
//             int columnCount = 0;
//             int rowCount = 0;
//             int dashCount = 0;
//             int repeat = 0;

//             if (repeat < 1)
//             {
//                 for (int j = 0; j < 5; j++)
//                 {
//                     if (columnCount < 1)
//                     {
//                         for (int i = 0; i < 3; i++)
//                         {
//                             if (i < 2)
//                             {
//                                 Console.Write(" ");
//                             }
//                             else if (i == 2)
//                             {
//                                 Console.Write("|");
//                                 i = -1;
//                                 columnCount++;
//                                 if (columnCount == 2)
//                                 {
//                                     break;
//                                 }
//                             }
//                         }
//                     }
//                     else if (columnCount == 2)
//                     {
//                         repeat++;
//                         Console.WriteLine();
//                         for (int i = 0; i < 3; i++)
//                         {
//                             if (i < 2)
//                             {
//                                 Console.Write("-");
//                                 dashCount++;
//                             }
//                             else if (i == 2)
//                             {
//                                 if (rowCount == 2)
//                                 {
//                                     rowCount = 0;
//                                     columnCount = 0;
//                                     dashCount = 0;
//                                     break;
//                                 }
//                                 else
//                                 {
//                                     Console.Write("+");
//                                     i = -1;
//                                     rowCount++;
//                                 }
//                             }
//                         }
//                         Console.WriteLine();
//                     }
//                 }
//             }
//             Console.WriteLine();
//         }
//     }
// }
