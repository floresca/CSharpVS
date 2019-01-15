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
        int location = 0; //this variable is the user input
        bool isItX = false; //this boolean will be used to switch turns between O player and X player
        int moves = 0; //This variable will count the amount of moves made

        //initialized where the pieces will be stored
        IDictionary<int, string> boardPieces = new Dictionary<int, string>();

        //This first method will prompt the user for a location to place their pieces
        public void CallSize()
        {
            //While there are moves still left to play the user will get prompted to enter a location
            while (moves < 9)
            {
                if (isItX == false)
                {
                    Console.Write("Enter a location for X: ");
                    location = Convert.ToInt32(Console.ReadLine());
                    isItX = true;
                    GameKeys(location, "X");
                }
                else
                {
                    Console.Write("Enter a location for O: ");
                    location = Convert.ToInt32(Console.ReadLine());
                    isItX = false;
                    GameKeys(location, "O");
                }
            }
        }

        public void GameKeys(int incomingKey, string incomingString)
        {
            boardPieces.Add(incomingKey, incomingString); //add the user input location to the dictionary

            string[] locators = new string[10];

            for (var i = 1; i < locators.Length; i++)
            {
                if (boardPieces.ContainsKey(i))
                {
                    locators[i] = boardPieces[i]; //if the key exists enter it into the array
                }
                else
                {
                    locators[i] = " "; //fill the space with blanks if the key does not exist
                }
            }

            Draw(locators); //send the array to the draw method
            Win(locators);

        }

        //the following method redraws the board based on the location array. It draws the columns and rows individually (used for resizing the grid. Feautre disabled to allow playing in version 3)
        public void Draw(string[] values)
        {
            int size = 1;
            int columns = 3;
            int repeat = 0;
            int specialRow = 0;
            int finalCount = 0;
            int spaces = 1;

            do
            {
                for (int i = 0; i < size; i++)
                {
                    do
                    {
                        for (int j = 0; j < size; j++)
                        {
                            Console.Write(" {0} ", values[spaces]);
                            repeat++;
                            spaces++;
                        }

                        if (repeat == columns)
                        {
                            break;
                        }
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
                finalCount++;

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

        //the following method is in need of massive refactoring but it sets the winning condition
        public void Win(string[] areThereWinners)
        {
            if ((areThereWinners[1] == areThereWinners[2] && areThereWinners[2] == areThereWinners[3]) && areThereWinners[1] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[1]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[4] == areThereWinners[5] && areThereWinners[5] == areThereWinners[6]) && areThereWinners[4] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[4]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[7] == areThereWinners[8] && areThereWinners[8] == areThereWinners[9]) && areThereWinners[7] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[7]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[1] == areThereWinners[5] && areThereWinners[5] == areThereWinners[9]) && areThereWinners[1] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[1]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[3] == areThereWinners[5] && areThereWinners[5] == areThereWinners[7]) && areThereWinners[3] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[3]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[1] == areThereWinners[4] && areThereWinners[4] == areThereWinners[7]) && areThereWinners[1] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[7]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[2] == areThereWinners[5] && areThereWinners[5] == areThereWinners[8]) && areThereWinners[2] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[1]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[3] == areThereWinners[6] && areThereWinners[6] == areThereWinners[9]) && areThereWinners[3] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[3]);
                Environment.Exit(0);
            }
        }
    }
}



//---------------------------------------------------------------------- VERSION 2 BELOW prints a grid based on the size entered by the user

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
//             stuff.CallSize();

//         }
//     }

//     class TicTacToe
//     {
//         int input = 0;

//         public void CallSize()
//         {
//             Console.Write("Enter a size: ");
//             input = Convert.ToInt32(Console.ReadLine());
//             Draw(input);
//         }

//         public void Draw(int size)
//         {
//             int columns = 3 * size;
//             int rows = 3 * size;
//             int repeat = 0;
//             int specialRow = 0;
//             int finalCount = 0;
//             char spaces = 'A';
            
//             do
//             {
//                 //Rows and column pipes print with the following loop
//                 for (int i = 0; i < size; i++)
//                 {
//                     do
//                     {
//                         for (int j = 0; j < size; j++)
//                         {
//                             Console.Write(" {0} ", spaces);
//                             repeat++;
//                             repeat++;
//                         }

//                         if (repeat == columns)
//                         {
//                             break;
//                         }
//                         //Pipes will only print as long as blank spaces have already printed
//                         else if (repeat < columns)
//                         {
//                             Console.Write("|");
//                         }
//                     }
//                     while (repeat < columns);
//                     repeat = 0;
//                     Console.WriteLine();
//                 }
//                 specialRow++;
                
//                 //Using final count to keep track of how many special rows are being printed. I only want 2 special rows
//                 finalCount++;
                
//                 //SPecial rows iterate through the following loop
//                 if (specialRow < 3)
//                 {
//                     do
//                     {
//                         for (int j = 0; j < size; j++)
//                         {
//                             Console.Write("---");
//                             repeat++;
//                         }

//                         if (repeat == columns)
//                         {
//                             break;
//                         }
//                         else if (repeat < columns)
//                         {
//                             Console.Write("+");
//                         }
//                     }
//                     while (repeat < columns);
//                     repeat = 0;
//                     Console.WriteLine();
//                 }
//             }
//             while (finalCount < 3);
//         }
//     }
    
//     class TicTacToeGame : TicTacToe
//     {
//         public List<string> gamePositions = new List<string>();

//         public void CallGamePieces()
//         {
//             while (true)
//             {
//                 Console.Write("Enter X or O and a position: ");
//                 string input = Console.ReadLine();

//                 if (input == "end")
//                 {
//                     Environment.Exit(0);
//                 }
//                 else
//                 {
//                     gamePositions.Add(input);

//                     foreach (string piece in gamePositions)
//                     {
//                         Console.WriteLine(piece);
//                     }
//                 }
//             }
//         }
//     }
// }
// }



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
