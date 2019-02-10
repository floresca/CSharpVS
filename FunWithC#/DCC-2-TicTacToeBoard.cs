//---------------------------------------------------------------------- VERSION 8 Has been refactored. Simple AI now shuffles an array an picks a move from there. Prevention of stack overflow
//---------------------------------------------------------------------- Pseudo code for intermediate AI included as well. AI designed to block player

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
            var game = new TicTacToe();
            game.Greeting();
        }
    }

    class TicTacToe
    {
        string playLoad = null;                     //This variable takes the user input for playing a new game or loading an existing game
        string savedGame = null;                    //Variable is used when user enters a saved game code
        int location;                               //This variable is the user input for moves
        bool isItXturn = true;                      //Boolean used to switch turns between O player and X player, X always starts the game
        int moves = 0;                              //Keep track of the amount of moves made
        string checkUp = null;                      //Accept the user input and put it through validation to make sure it is good
        bool playerVsAI = false;                    //Bool to track if the AI is playing as well
        string pvp = null;                          //variable to track user preference on playing against AI or not

        List<int> arrayX = new List<int> { };       //Locations of X in the game, used to validate loaded games
        List<int> arrayO = new List<int> { };       //Locations of Y in the game

        //Storage of our pieces
        IDictionary<int, string> boardPieces = new Dictionary<int, string>();       //Dictionary to save key value pairs
        string[] locators = new string[10];                                         //Locators used to track user input
        List<string> spaces = new List<string> { };                                 //Starts with numbers 1 - 9, goes down as spaces are taken so AI can randomize spots

        //Welcome to the game
        public void Greeting()
        {
            Console.WriteLine("Lets play a game of TicTacToe!");
            Console.WriteLine();
            Start();
        }

        //The Start method provides the first user interaction and sets up game conditions
        public void Start()
        {
            Console.Write("What would you like to do, 'Play' a new game or 'Load' a saved game?: "); 
            playLoad = Console.ReadLine();

            if (playLoad == "Play")
            {
                Console.Write("Player vs Computer = 'PvC' or Player vs Player = 'PvP': ");
                pvp = Console.ReadLine();
                if (pvp == "PvC")
                {
                    playerVsAI = true;
                    Console.WriteLine("You will now play against the computer, good luck!");
                    FreshBoard();
                    NewGame();
                }
                else if (pvp == "PvP")
                {
                    FreshBoard();
                    NewGame();
                }
                else
                {
                    Console.WriteLine("Invalid command, plase try again");
                    Console.WriteLine();
                    Start();
                }
            }
            else if (playLoad == "Load")
            {
                Console.Write("Enter your save code!: ");
                savedGame = Console.ReadLine();
                Load(savedGame);
            }
            else if (playLoad == "Clear")
            {
                Clear();
                Console.WriteLine("All cached values have been cleared");
                Start();
            }
            else if (playLoad == "End")
            {
                Console.WriteLine("Good Bye!");
                Exit();
            }
            else
            {
                Console.WriteLine("Invalid command, please enter 'Play', or 'Load' to begin");
                Start();
            }
        }

        //This method Loads a game based on user input array. it also keeps tab of how many X and O are in code and sends it to validate
        public void Load(string input)
        {
            char[] savedGameCode = input.ToCharArray();
            FreshBoard();

            for (int i = 0; i < savedGameCode.Length; i++)
            {
                if (savedGameCode[i] == 'O')
                {
                    boardPieces.Add(i + 1, "O");
                    isItXturn = true;
                    arrayO.Add(i + 1);
                    ReduceList(i + 1);
                    moves++;
                }
                else if (savedGameCode[i] == 'X')
                {
                    boardPieces.Add(i + 1, "X");
                    isItXturn = false;
                    arrayX.Add(i + 1);
                    ReduceList(i + 1);
                    moves++;
                }
                else if (savedGameCode[i] == '0')
                {
                    continue;
                }
                else if (savedGameCode[i] == 'T')
                {
                    playerVsAI = true;
                    pvp = "PvC";
                }
            }
            SavedGameValidation();
        }

        //This method validates that the user code has an equal amount of Xs and Os. If the code fails the user is asked to try again
        public void SavedGameValidation()
        {
            int hello = arrayX.Count() - arrayO.Count();

            if (savedGame.Length == 10)
            {
                if (hello == 1 || hello == -1 || hello == 0)
                {
                    UpdateGameBoard();
                    RunGame();
                }
            }
            else
            {
                Console.WriteLine("Sorry, this code is not valid");
                Clear();
                Start();
            }
        }

        //This method clears all cached values
        public void Clear()
        {
            boardPieces.Clear();
            arrayO.Clear();
            arrayX.Clear();
            moves = 0;

            for (int i = 0; i < locators.Count(); i++)
            {
                locators[i] = null;
            }
        }

        //Freshboard digits are prepared
        public void FreshBoard()
        {
            for (int i = 0; i < locators.Length; i++)
            {
                spaces.Add(Convert.ToString(i));
                locators[i] = Convert.ToString(i);
            }
        }

        //NewGame board is set up, spaces are called out
        public void NewGame()
        {
            Console.WriteLine();
            Console.WriteLine("These are the board locations");
            Draw(locators);
            RunGame();
        }

        string token = null;

        //This method switches between X or O tokens and calls either user input or computer input
        public void RunGame()
        {
            while (moves < 9)
            {
                if (isItXturn == true)
                {
                    token = "X";
                    UserInput();
                }
                else
                {
                    token = "O";
                    if (playerVsAI)
                    {
                        AIinput();
                    }
                    else
                    {
                        UserInput();
                    }
                }
            }
        }

        //User input method. Here we keep track of what the user entered and make sure only valid user input is accepted
        public void UserInput()
        {
            Console.Write("Enter a location for {0}: ", token);
            checkUp = Console.ReadLine();
            if (checkUp == "End")
            {
                Console.WriteLine("Good Bye!");
                Exit();
            }
            else if (checkUp == "Save")
            {
                SaveGame();
            }
            else
            {
                bool didUserEnterANumber = Int32.TryParse(checkUp, out location);
                if( didUserEnterANumber == false)
                {
                    Console.WriteLine("Please enter a number between 1 and 9. You can save by saying 'Save'");
                    RunGame();
                }
                else if (location > 9 || location < 1)
                {
                    Console.WriteLine("Please enter a number between 1 and 9. You can save by saying 'Save'");
                    RunGame();
                }
                else 
                {
                    ReduceList(location);
                    KeyValidation();
                    TokenMove(location);
                }
            }
        }

        //Remove existing token locations in the space
        public void ReduceList(int input)
        {
            if (spaces.Contains(Convert.ToString(input)))
            {
                int itemToRemove = spaces.IndexOf(Convert.ToString(input));
                spaces.RemoveAt(itemToRemove);
            }
        }

        //AI input if user decides to play against AI
        public void AIinput()
        {
            LVL1AI();
            KeyValidation();
            spaces.Remove(spaces[0]);
            Console.WriteLine("It is the Computer's turn");
            TokenMove(location);
        }
        
        public void LVL1AI()
        {
            for (int i = 0; i < spaces.Count; i++)
            {
                Random random = new Random();
                int randomSpot = random.Next(0, spaces.Count);
                string firstSpot = spaces[i];
                string secondSpot = spaces[randomSpot];
                spaces[i] = secondSpot;
                spaces[randomSpot] = firstSpot;
            }
            
            location = Convert.ToInt32(spaces[0]);
        }
        
        public void LVL2AI()
        {
            if (arrayX.Contains(1))
            {
                if(arrayX.Contains(2) && spaces.Contains("3"))
                {
                    location = 3;
                }
                else if (arrayX.Contains(3) && spaces.Contains("2"))
                {
                    location = 2;
                }
                else if (arrayX.Contains(5) && spaces.Contains("9"))
                {
                    location = 9;
                }
                else if (arrayX.Contains(9) && spaces.Contains("5"))
                {
                    location = 5;
                }
                else if (arrayX.Contains(4) && spaces.Contains("7"))
                {
                    location = 7;
                }
                else if (arrayX.Contains(7) && spaces.Contains("4"))
                {
                    location = 4;
                } 
            }
            else if (arrayX.Contains(2))
            {
                if(arrayX.Contains(1) && spaces.Contains("3"))
                {
                    location = 3;
                }
                else if (arrayX.Contains(3) && spaces.Contains("1"))
                {
                    location = 1;
                }
                else if (arrayX.Contains(5) && spaces.Contains("8"))
                {
                    location = 8;
                }
                else if (arrayX.Contains(8) && spaces.Contains("5"))
                {
                    location = 5;
                }
            }
            else if (arrayX.Contains(3))
            {
                if(arrayX.Contains(2) && spaces.Contains("1"))
                {
                    location = 1;
                }
                else if (arrayX.Contains(1) && spaces.Contains("2"))
                {
                    location = 2;
                }
                else if (arrayX.Contains(5) && spaces.Contains("7"))
                {
                    location = 7;
                }
                else if (arrayX.Contains(7) && spaces.Contains("5"))
                {
                    location = 5;
                }
                else if (arrayX.Contains(6) && spaces.Contains("9"))
                {
                    location = 9;
                }
                else if (arrayX.Contains(9) && spaces.Contains("6"))
                {
                    location = 6;
                } 
            }
            else if (arrayX.Contains(4))
            {
                if(arrayX.Contains(1) && spaces.Contains("7"))
                {
                    location = 7;
                }
                else if (arrayX.Contains(7) && spaces.Contains("1"))
                {
                    location = 1;
                }
                else if (arrayX.Contains(5) && spaces.Contains("6"))
                {
                    location = 6;
                }
                else if (arrayX.Contains(6) && spaces.Contains("5"))
                {
                    location = 5;
                }
            }
            else if (arrayX.Contains(5))
            {
                if(arrayX.Contains(9) && spaces.Contains("1"))
                {
                    location = 1;
                }
                else if (arrayX.Contains(1) && spaces.Contains("9"))
                {
                    location = 9;
                }
                else if (arrayX.Contains(2) && spaces.Contains("8"))
                {
                    location = 8;
                }
                else if (arrayX.Contains(8) && spaces.Contains("2"))
                {
                    location = 2;
                }
                else if (arrayX.Contains(3) && spaces.Contains("7"))
                {
                    location = 7;
                }
                else if (arrayX.Contains(7) && spaces.Contains("3"))
                {
                    location = 3;
                }
                else if (arrayX.Contains(4) && spaces.Contains("6"))
                {
                    location = 6;
                }
                else if (arrayX.Contains(6) && spaces.Contains("4"))
                {
                    location = 4;
                } 
            }
            else if (arrayX.Contains(6))
            {
                if(arrayX.Contains(3) && spaces.Contains("9"))
                {
                    location = 9;
                }
                else if (arrayX.Contains(5) && spaces.Contains("4"))
                {
                    location = 4;
                }
                else if (arrayX.Contains(4) && spaces.Contains("5"))
                {
                    location = 5;
                }
                else if (arrayX.Contains(9) && spaces.Contains("3"))
                {
                    location = 3;
                }
            }
            else if (arrayX.Contains(7))
            {
                if(arrayX.Contains(4) && spaces.Contains("1"))
                {
                    location = 1;
                }
                else if (arrayX.Contains(1) && spaces.Contains("4"))
                {
                    location = 4;
                }
                else if (arrayX.Contains(5) && spaces.Contains("3"))
                {
                    location = 3;
                }
                else if (arrayX.Contains(3) && spaces.Contains("5"))
                {
                    location = 5;
                }
                else if (arrayX.Contains(8) && spaces.Contains("9"))
                {
                    location = 9;
                }
                else if (arrayX.Contains(9) && spaces.Contains("8"))
                {
                    location = 8;
                } 
            }
            else if (arrayX.Contains(8))
            {
                else if (arrayX.Contains(5) && spaces.Contains("2"))
                {
                    location = 2;
                }
                else if (arrayX.Contains(2) && spaces.Contains("5"))
                {
                    location = 5;
                }
                else if (arrayX.Contains(7) && spaces.Contains("9"))
                {
                    location = 9;
                }
                else if (arrayX.Contains(9) && spaces.Contains("7"))
                {
                    location = 7;
                } 
            }
            else if (arrayX.Contains(9))
            {
                if(arrayX.Contains(5) && spaces.Contains("1"))
                {
                    location = 1;
                }
                else if (arrayX.Contains(1) && spaces.Contains("5"))
                {
                    location = 5;
                }
                else if (arrayX.Contains(3) && spaces.Contains("6"))
                {
                    location = 6;
                }
                else if (arrayX.Contains(6) && spaces.Contains("3"))
                {
                    location = 3;
                }
                else if (arrayX.Contains(8) && spaces.Contains("7"))
                {
                    location = 7;
                }
                else if (arrayX.Contains(7) && spaces.Contains("8"))
                {
                    location = 8;
                } 
            }
        }
        
        

        //Is the space occupied?
        public void KeyValidation()
        {
            if (boardPieces.ContainsKey(location) == true)
            {
                if (playerVsAI && token == "O")
                {
                    RunGame();
                }
                else
                {
                    Console.WriteLine("Invalid move, enter an empty spot");
                    RunGame();
                }
            }
        }

        //This method takes the location number and validates it then adds it to 
        public void TokenMove(int location)
        {
            boardPieces.Add(location, token);
            UpdateGameBoard();
            moves++;

            if (token == "X")
            {
                arrayX.Add(location);
                Win(arrayX, token);
                isItXturn = false;
            }
            else
            {
                arrayO.Add(location);
                Win(arrayO, token);
                isItXturn = true;
            }
        }
        
        //This method makes an array of saved keys which is given to the draw method to update the board
        public void UpdateGameBoard()
        {
            for (var i = 0; i < locators.Length; i++)
            {
                if (boardPieces.ContainsKey(i))
                {
                    locators[i] = boardPieces[i]; //if the key exists in the dictionary enter it into the array
                }
                else
                {
                    locators[i] = " "; //fill the space with blanks if the key does not exist
                }
            }
            Draw(locators); //send the array to the draw method
        }

        //This method saves the game for the user and ends the round
        public void SaveGame()
        {
            string saveCode = null;
            for (int i = 0; i < locators.Length; i++)
            {
                if (locators[i] == " ")
                {
                    locators[i] = "0";
                }
            }

            for (int i = 1; i < locators.Length; i++)
            {
                saveCode += locators[i];
            }

            if (playerVsAI == true)
            {
                saveCode += "T";
            }
            else
            {
                saveCode += "F";
            }

            Console.WriteLine("Your save code is: {0}, Hope to see you again soon!", saveCode);
            Clear();
            Exit();
        }

        //This method draws the board based on the array. It draws the columns individually and the rows as one (initially used for resizing the grid. Feautre disabled to allow playing in version 3)
        public void Draw(string[] values)
        {
            int columns = 3;
            int repeat = 0;
            int dashedLineRow = 0;
            int finalCount = 0;
            int spaces = 1;

            do
            {
                do
                {
                    Console.Write(" {0} ", values[spaces]);
                    repeat++;
                    spaces++;

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
                
                dashedLineRow++;
                finalCount++;

                if (dashedLineRow < 3)
                {
                    Console.Write("---+---+---");
                    repeat = 0;
                    Console.WriteLine();
                }
            }
            while (finalCount < 3);
            Console.WriteLine();
        }
        
        //This method sets the basic winning conditions. Future addition: predict who is going to win by move 4, predit a draw by move 7
        public void Win(List<int> Local, string token)
        {
            
            if (Local.Contains(1) && Local.Contains(2) && Local.Contains(3))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(4) && Local.Contains(5) && Local.Contains(6))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(7) && Local.Contains(8) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(1) && Local.Contains(4) && Local.Contains(7))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(2) && Local.Contains(5) && Local.Contains(8))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(3) && Local.Contains(6) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(1) && Local.Contains(5) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(3) && Local.Contains(5) && Local.Contains(7))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (moves == 9)
            {
                Console.WriteLine("Looks like tt is a draw!!, play again!");
            }
        }

        //End the game
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}


//---------------------------------------------------------------------- VERSION 7 Has been refactored. It now accepts user input as well as play against an simple AI that plays random spots. 
//---------------------------------------------------------------------- AI does not check if a number is repeated and will repeat random until an empty spot can be plaied. This can cause a stack overflow if enough repetitions occur
//---------------------------------------------------------------------- Solution may be a shuffle algorithm vs random selection

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
            var game = new TicTacToe();
            game.Start();
        }
    }

    class TicTacToe
    {
        string playLoad = null;                     //This variable takes the user input for playing a new game or loading an existing game
        string savedGame = null;                    //Variable is used when user enters a saved game code
        int location;                               //This variable is the user input for moves
        bool isItXturn = true;                      //Boolean used to switch turns between O player and X player, X always starts the game
        int moves = 0;                              //Keep track of the amount of moves made
        string checkUp = null;                      //Accept the user input and put it through validation to make sure it is good
        bool playerVsAI = false;                    //Bool to track if the AI is playing as well
        string pvp = null;                          //variable to track user preference on playing against AI or not

        List<int> arrayX = new List<int> { };       //Locations of X in the game
        List<int> arrayO = new List<int> { };       //Locations of Y in the game

        //Storage of our pieces
        //A dictionary for key/value pairs, and a list array to easily manipulate incoming user input
        IDictionary<int, string> boardPieces = new Dictionary<int, string>();
        string[] locators = new string[10];


        //The Start method provides the first user interaction and sets up game conditions
        public void Start()
        {
            Console.WriteLine("Lets play a game of TicTacToe!");
            Console.Write("What would you like to do, 'Play' a new game or 'Load' a saved game?: "); 
            playLoad = Console.ReadLine();

            if (playLoad == "Play")
            {
                Console.Write("Player vs Computer = 'PvC' or Player vs Player = 'PvP': ");
                pvp = Console.ReadLine();
                if (pvp == "PvC")
                {
                    playerVsAI = true;
                    Console.WriteLine("You will now play against the computer, good luck!");
                    NewGame();
                }
                else if (pvp == "PvP")
                {
                    NewGame();
                }
            }
            else if (playLoad == "Load")
            {
                Console.Write("Enter your save code!: ");
                savedGame = Console.ReadLine();
                Load(savedGame);
            }
            else if (playLoad == "Clear")
            {
                Clear();
                Console.WriteLine("All cached values have been cleared");
                Start();
            }
            else if (playLoad == "End")
            {
                Console.WriteLine("Good Bye!");
                Exit();
            }
            else
            {
                Console.WriteLine("Invalid command, please enter 'Play', or 'Load' to begin");
                Start();
            }
        }

        //This method Loads a game based on user input array. it also keeps tab of how many X and O are in code and sends it to validate
        public void Load(string input)
        {
            char[] savedGameCode = input.ToCharArray();

            for (int i = 0; i < savedGameCode.Length; i++)
            {
                if (savedGameCode[i] == 'O')
                {
                    boardPieces.Add(i + 1, "O");
                    isItXturn = true;
                    arrayO.Add(i + 1);
                    moves++;
                }
                else if (savedGameCode[i] == 'X')
                {
                    boardPieces.Add(i + 1, "X");
                    isItXturn = false;
                    arrayX.Add(i + 1);
                    moves++;
                }
                else if (savedGameCode[i] == '0')
                {
                    continue;
                }
                else if (savedGameCode[i] == 'T')
                {
                    playerVsAI = true;
                    pvp = "PvC";
                }
            }
            SavedGameValidation();
        }

        //This method validates that the user code has an equal amount of Xs and Os. If the code fails the user is asked to try again
        public void SavedGameValidation()
        {
            int hello = arrayX.Count() - arrayO.Count();

            if (savedGame.Length == 10)
            {
                if (hello == 1 || hello == -1 || hello == 0)
                {
                    UpdateGameBoard();
                    RunGame();
                }
            }
            else
            {
                Console.WriteLine("Sorry, this code is not valid");
                Clear();
                Start();
            }
        }

        //This method clears all cached values
        public void Clear()
        {
            boardPieces.Clear();
            arrayO.Clear();
            arrayX.Clear();
            moves = 0;

            for (int i = 0; i < locators.Count(); i++)
            {
                locators[i] = null;
            }
        }

        //NewGame board is set up, spaces are called out
        public void NewGame()
        {
            string[] spaces = new string[10];

            for(int i = 0; i < spaces.Length; i++)
            {
                spaces[i] = Convert.ToString(i);
            }

            Console.WriteLine();
            Console.WriteLine("These are the board locations");
            Draw(spaces);
            RunGame();
        }

        string token = null;

        //This method switches between X or O tokens and calls either user input or computer input
        public void RunGame()
        {
            while (moves < 9)
            {
                if (isItXturn == true)
                {
                    token = "X";
                    UserInput();
                }
                else
                {
                    token = "O";
                    if (playerVsAI)
                    {
                        AIinput();
                    }
                    else
                    {
                        UserInput();
                    }
                }
            }
        }

        //User input method. Here we keep track of what the user entered and make sure only valid user input is accepted
        public void UserInput()
        {
            Console.Write("Enter a location for {0}: ", token);
            checkUp = Console.ReadLine();
            if (checkUp == "End")
            {
                Console.WriteLine("Good Bye!");
                Exit();
            }
            else if (checkUp == "Save")
            {
                SaveGame();
            }
            else
            {
                bool didUserEnterANumber = Int32.TryParse(checkUp, out location);
                if( didUserEnterANumber == false)
                {
                    Console.WriteLine("Please enter a number between 1 and 9. You can save by saying 'Save'");
                    RunGame();
                }
                else if (location > 9 || location < 1)
                {
                    Console.WriteLine("Please enter a number between 1 and 9. You can save by saying 'Save'");
                    RunGame();
                }
                else
                {
                    KeyValidation();
                    TokenMove(location);
                }
            }
        }

        //AI input if user decides to play against AI
        public void AIinput()
        {
            Random random = new Random();
            location = random.Next(1, 9);
            KeyValidation();

            Console.WriteLine("It is the Computer's turn");
            TokenMove(location);
        }

        //Is the space occupied?
        public void KeyValidation()
        {
            if (boardPieces.ContainsKey(location) == true)
            {
                if (playerVsAI && token == "O")
                {
                    RunGame();
                }
                else
                {
                    Console.WriteLine("Invalid move, enter an empty spot");
                    RunGame();
                }
            }
        }

        //This method takes the location number and validates it then adds it to 
        public void TokenMove(int location)
        {
            boardPieces.Add(location, token);
            UpdateGameBoard();
            moves++;

            if (token == "X")
            {
                arrayX.Add(location);
                Win(arrayX, token);
                isItXturn = false;
            }
            else
            {
                arrayO.Add(location);
                Win(arrayO, token);
                isItXturn = true;
            }
        }
        
        //This method makes an array of saved keys which is given to the draw method to update the board
        public void UpdateGameBoard()
        {
            for (var i = 1; i < locators.Length; i++)
            {
                if (boardPieces.ContainsKey(i))
                {
                    locators[i] = boardPieces[i]; //if the key exists in the dictionary enter it into the array
                }
                else
                {
                    locators[i] = " "; //fill the space with blanks if the key does not exist
                }
            }
            Draw(locators); //send the array to the draw method
        }

        //This method saves the game for the user and ends the round
        public void SaveGame()
        {
            string saveCode = null;
            for (int i = 0; i < locators.Length; i++)
            {
                if (locators[i] == " ")
                {
                    locators[i] = "0";
                }
            }

            for (int i = 0; i < locators.Length; i++)
            {
                saveCode += locators[i];
            }

            if (playerVsAI == true)
            {
                saveCode += "T";
            }
            else
            {
                saveCode += "F";
            }

            Console.WriteLine("Your save code is: {0}, Hope to see you again soon!", saveCode);
            Clear();
            Exit();
        }

        //This method draws the board based on the array. It draws the columns individually and the rows as one (initially used for resizing the grid. Feautre disabled to allow playing in version 3)
        public void Draw(string[] values)
        {
            int columns = 3;
            int repeat = 0;
            int dashedLineRow = 0;
            int finalCount = 0;
            int spaces = 1;

            do
            {
                do
                {
                    Console.Write(" {0} ", values[spaces]);
                    repeat++;
                    spaces++;

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
                
                dashedLineRow++;
                finalCount++;

                if (dashedLineRow < 3)
                {
                    Console.Write("---+---+---");
                    repeat = 0;
                    Console.WriteLine();
                }
            }
            while (finalCount < 3);
            Console.WriteLine();
        }

        //This method sets the basic winning conditions. Future addition: predict who is going to win by move 4, predit a draw by move 7
        public void Win(List<int> Local, string token)
        {
            
            if (Local.Contains(1) && Local.Contains(2) && Local.Contains(3))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(4) && Local.Contains(5) && Local.Contains(6))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(7) && Local.Contains(8) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(1) && Local.Contains(4) && Local.Contains(7))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(2) && Local.Contains(5) && Local.Contains(8))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(3) && Local.Contains(6) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(1) && Local.Contains(5) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(3) && Local.Contains(5) && Local.Contains(7))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (moves == 9)
            {
                Console.WriteLine("Looks like tt is a draw!!, play again!");
            }
        }

        //End the game
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}


//---------------------------------------------------------------------- VERSION 6 below can save games and reload, it clears old values, refactored WIN and refactored DRAW

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
            var game = new TicTacToe();
            game.Start();
        }
    }

    class TicTacToe
    {
        string playLoad = null;                     //This variable takes the user input for playing a new game or loading an existing game
        string savedGame = null;                    //Variable is used when user enters a saved game code
        int location;                               //This variable is the user input for moves
        bool isItXturn = true;                      //Boolean used to switch turns between O player and X player, X always starts the game
        int moves = 0;                              //Keep track of the amount of moves made
        string checkUp = null;                      //Accept the user input and put it through validation to make sure it is good

        List<int> arrayX = new List<int> { };       //Locations of X in the game
        List<int> arrayO = new List<int> { };       //Locations of Y in the game

        //Storage of our pieces. A dictionary for key/value pairs, a list array to easily manipulate incoming user input
        IDictionary<int, string> boardPieces = new Dictionary<int, string>();
        string[] locators = new string[10];

        //The Start method provides the first user interaction
        public void Start()
        {
            Console.Write("Lets play a game of TicTacToe! What would you like to do, 'Play' a new game or 'Load' a saved game?: ");
            playLoad = Console.ReadLine();

            if (playLoad == "Play")
            {
                NewGame();
            }
            else if (playLoad == "Load")
            {
                Console.Write("Enter your save code!: ");
                savedGame = Console.ReadLine();
                Load(savedGame);
            }
            else if (playLoad == "Clear")
            {
                Clear();
                Console.WriteLine("All chached values have been cleared");
                Start();
            }
            else if (playLoad == "End")
            {
                Console.WriteLine("Good Bye!");
                Exit();
            }
            else
            {
                Console.WriteLine("Invalid command, please enter 'Play', 'Load', 'Clear', or 'End' to begin");
            }
        }

        //This method Loads a game based on user input array. it also keeps tab of how many X and O are in code and sends it to validate
        public void Load(string input)
        {
            char[] savedGameCode = input.ToCharArray();

            for (int i = 0; i < savedGameCode.Length; i++)
            {
                if (savedGameCode[i] == 'O')
                {
                    boardPieces.Add(i + 1, "O");
                    isItXturn = true;
                    arrayO.Add(i + 1);
                    moves++;
                }
                else if (savedGameCode[i] == 'X')
                {
                    boardPieces.Add(i + 1, "X");
                    isItXturn = false;
                    arrayX.Add(i + 1);
                    moves++;
                }
                else if (savedGameCode[i] == '0')
                {
                    continue;
                }
            }
            SavedGameValidation();
        }

        //This method validates that the user code has an equal amount of Xs and Os. If the code fails the user is asked to try again
        public void SavedGameValidation()
        {
            int hello = arrayX.Count() - arrayO.Count();

            if (hello == 1 || hello == -1 || hello == 0)
            {
                UpdateGameBoard();
                RunGame();
            }
            else
            {
                Console.WriteLine("Sorry, this code is not valid");
                Clear();
                Start();
            }
        }

        //This method clears all cached values
        public void Clear()
        {
            boardPieces.Clear();
            arrayO.Clear();
            arrayX.Clear();
            moves = 0;

            for (int i = 0; i < locators.Count(); i++)
            {
                locators[i] = null;
            }
        }

        //NewGame board is set up, spaces are called out
        public void NewGame()
        {
            string[] spaces = new string[10];

            for(int i = 0; i < spaces.Length; i++)
            {
                spaces[i] = Convert.ToString(i);
            }

            Draw(spaces);
            RunGame();
        }

        //This method asks for either X or O key locations. It sends the value to validate, if its good it adds value to dictionary
        public void RunGame()
        {
            while (moves < 9)
            {
                if (isItXturn == true)
                {
                    Xmove();
                }
                else
                {
                    Omove();
                }
            }
        }

        public void Xmove()
        {
            Console.Write("Enter a location for X: ");
            checkUp = Console.ReadLine();
            if (checkUp == "End")
            {
                Exit();
            }
            else if (checkUp == "Save")
            {
                SaveGame();
            }
            else
            {
                location = Convert.ToInt32(checkUp);
                KeyValidation();
                isItXturn = false;
                boardPieces.Add(location, "X");
                arrayX.Add(location);
                UpdateGameBoard();
                moves++;
                Win(arrayX, "X");
            }
        }

        public void Omove()
        {
            Console.Write("Enter a location for O: ");
            checkUp = Console.ReadLine();
            if (checkUp == "End")
            {
                Exit();
            }
            else if (checkUp == "Save")
            {
                SaveGame();
            }
            else
            {
                location = Convert.ToInt32(checkUp);
                KeyValidation();
                isItXturn = true;
                boardPieces.Add(location, "O");
                arrayO.Add(location);
                UpdateGameBoard();
                moves++;
                Win(arrayO, "O");
            }
        }

        //Is the space occupied?
        public void KeyValidation()
        {
            if (boardPieces.ContainsKey(location) == true)
            {
                Console.WriteLine("Invalid move, enter an empty spot");
                RunGame();
            }
        }

        //This method makes an array of saved keys which is given to the draw method to update the board
        public void UpdateGameBoard()
        {
            for (var i = 1; i < locators.Length; i++)
            {
                if (boardPieces.ContainsKey(i))
                {
                    locators[i] = boardPieces[i]; //if the key exists in the dictionary enter it into the array
                }
                else
                {
                    locators[i] = " "; //fill the space with blanks if the key does not exist
                }
            }
            Draw(locators); //send the array to the draw method
        }

        //This method saves the game for the user and ends the round
        public void SaveGame()
        {
            string saveCode = null;
            for (int i = 0; i < locators.Length; i++)
            {
                if (locators[i] == " ")
                {
                    locators[i] = "0";
                }
            }

            for (int i = 0; i < locators.Length; i++)
            {
                saveCode += locators[i];
            }

            Console.WriteLine("Your save code is: {0}, Hope to see you again soon!", saveCode);
            Clear();
            Exit();
        }

        //This method draws the board based on the array. It draws the columns individually and the rows as one (initially used for resizing the grid. Feautre disabled to allow playing in version 3)
        public void Draw(string[] values)
        {
            int columns = 3;
            int repeat = 0;
            int dashedLineRow = 0;
            int finalCount = 0;
            int spaces = 1;

            do
            {
                do
                {
                    Console.Write(" {0} ", values[spaces]);
                    repeat++;
                    spaces++;

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
                
                dashedLineRow++;
                finalCount++;

                if (dashedLineRow < 3)
                {
                    Console.Write("---+---+---");
                    repeat = 0;
                    Console.WriteLine();
                }
            }
            while (finalCount < 3);
            Console.WriteLine();
        }

        //This method sets the basic winning conditions. Future addition: predict who is going to win by move 4, predit a draw by move 7
        public void Win(List<int> Local, string token)
        {
            
            if (Local.Contains(1) && Local.Contains(2) && Local.Contains(3))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(4) && Local.Contains(5) && Local.Contains(6))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(7) && Local.Contains(8) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(1) && Local.Contains(4) && Local.Contains(7))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(2) && Local.Contains(5) && Local.Contains(8))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(3) && Local.Contains(6) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(1) && Local.Contains(5) && Local.Contains(9))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (Local.Contains(3) && Local.Contains(5) && Local.Contains(7))
            {
                Console.WriteLine("Congrats player {0}, you win!", token);
                Exit();
            }
            else if (moves == 9)
            {
                Console.WriteLine("Looks like tt is a draw!!, play again!");
            }
        }

        //End the game
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}



//---------------------------------------------------------------------- VERSION 5 below can save games and keeps track of X and Os entered by user

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
            stuff.Start();
        }
    }

    class TicTacToe
    {
        string playLoad = null; //this variable is the user input for playing a new game or loading an existing game
        string savedGame = null; //user inputed saved game
        int location; //this variable is the user input for moves
        bool isItXturn = true; //Used to switch turns between O player and X player
        int moves = 0; //Count the amount of moves made
        string checkUp = null; //accept the user input and put it through validation to make sure it is good

        List<int> arrayX = new List<int> { };
        List<int> arrayO = new List<int> { };

        //initialized where the pieces will be stored
        IDictionary<int, string> boardPieces = new Dictionary<int, string>();
        string[] locators = new string[10];

        public void Start()
        {
            Console.Write("What would you like to do, 'Play' a new game or 'Load' a saved game?: ");
            playLoad = Console.ReadLine();

            if (playLoad == "Play")
            {
                RunGame();
            }
            else if (playLoad == "Load")
            {
                Console.Write("Enter your save code!: ");
                savedGame = Console.ReadLine();
                Load(savedGame);
            }
            else if (playLoad == "End")
            {
                Exit();
            }
        }

        //This method Loads a game based on user input array. it also keeps tab of how many X and O are in code and sends it to validate
        public void Load(string input)
        {
            char[] savedGameCode = input.ToCharArray();

            for (int i = 0; i < savedGameCode.Length; i++)
            {
                if (savedGameCode[i] == 'O')
                {
                    boardPieces.Add(i, "O");
                    isItXturn = true;
                    arrayX.Add(i);
                    moves++;
                }
                else if (savedGameCode[i] == 'X')
                {
                    boardPieces.Add(i + 1, "X");
                    isItXturn = false;
                    arrayO.Add(i);
                    moves++;
                }
                else if (savedGameCode[i] == '0')
                {
                    continue;
                }
            }
            SavedGameValidation();
        }

        //This method validates that the user code has an equal amount of Xs and Os. If the code fails the user is asked to plat again
        public void SavedGameValidation()
        {
            int hello = arrayX.Count() - arrayO.Count();

            if (hello == 1 || hello == -1 || hello == 0)
            {
                RunGame();
            }
            else
            {
                Console.WriteLine("Sorry, this code is not valid");
                boardPieces.Clear();
                arrayO.Clear();
                arrayX.Clear();
                moves = 0;
                Start();
            }
        }

        //This method asks for either X or O key locations. It sends the value to validate, if its good it adds value to dictionary
        public void RunGame()
        {
            while (moves < 9)
            {
                if (isItXturn == true)
                {
                    Console.Write("Enter a location for X: ");
                    checkUp = Console.ReadLine();
                    if (checkUp == "End")
                    {
                        Exit();
                    }
                    else if (checkUp == "Save")
                    {
                        SaveGame();
                    }
                    else
                    {
                        location = Convert.ToInt32(checkUp);
                        KeyValidation();
                        isItXturn = false;
                        boardPieces.Add(location, "X");
                        UpdateGameBoard();
                    }
                    
                }
                else
                {
                    Console.Write("Enter a location for O: ");
                    checkUp = Console.ReadLine();
                    if (checkUp == "End")
                    {
                        Exit();
                    }
                    else if (checkUp == "Save")
                    {
                        SaveGame();
                    }
                    else
                    {
                        location = Convert.ToInt32(checkUp);
                        KeyValidation();
                        isItXturn = true;
                        boardPieces.Add(location, "O");
                        UpdateGameBoard();
                    }
                }
            }
        }

        //Is the space occupied?
        public void KeyValidation()
        {
            if (boardPieces.ContainsKey(location) == true)
            {
                Console.WriteLine("Invalid move, enter an empty spot");
                RunGame();
            }
        }

        //This method makes an array of saved keys which is also given to the draw method to update the board
        public void UpdateGameBoard()
        {
            for (var i = 1; i < locators.Length; i++)
            {
                if (boardPieces.ContainsKey(i))
                {
                    locators[i] = boardPieces[i]; //if the key exists in the dictionary enter it into the array
                }
                else
                {
                    locators[i] = " "; //fill the space with blanks if the key does not exist
                }
            }

            Draw(locators); //send the array to the draw method
            Win(locators);
        }

        //This method saves the game for the user and ends the round
        public void SaveGame()
        {
            string saveCode = null;
            for (int i = 0; i < locators.Length; i++)
            {
                if (locators[i] == " ")
                {
                    locators[i] = "0";
                }
            }

            for (int i = 0; i < locators.Length; i++)
            {
                saveCode += locators[i];
            }

            Console.WriteLine("Your save code is {0}", saveCode);
            Exit();
        }

        //This method draws the board based on the array. It draws the columns and rows individually (initially used for resizing the grid. Feautre disabled to allow playing in version 3)
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

        //This method sets the winning condition. It is in need of refactoring
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
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[2]);
                Environment.Exit(0);
            }
            else if ((areThereWinners[3] == areThereWinners[6] && areThereWinners[6] == areThereWinners[9]) && areThereWinners[3] != " ")
            {
                Console.WriteLine("Congrats player {0}, you win!", areThereWinners[3]);
                Environment.Exit(0);
            }
        }

        //End the game
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}


//---------------------------------------------------------------------- VERSION 4 BELOW prints a game with X and O, it can load saved games, refactored some items, validate that keys are good 

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
            stuff.Start();
        }
    }

    class TicTacToe
    {
        string playLoad = null; //this variable is the user input for playing a new game or loading an existing game
        string savedGame = null; //user inputed saved game
        int location; //this variable is the user input for moves
        bool isItXturn = true; //this boolean will be used to switch turns between O player and X player
        int moves = 0; //This variable will count the amount of moves made
        string checkUp = null;

        List<int> arrayX = new List<int> { };
        List<int> arrayO = new List<int> { };

        //initialized where the pieces will be stored
        IDictionary<int, string> boardPieces = new Dictionary<int, string>();
        string[] locators = new string[10];

        public void Start()
        {
            Console.Write("What would you like to do, 'Play' a new game or 'Load' a saved game?: ");
            playLoad = Console.ReadLine();

            if (playLoad == "Play")
            {
                RunGame();
            }
            else if (playLoad == "Load")
            {
                Console.Write("Enter your save code!: ");
                savedGame = Console.ReadLine();
                Load(savedGame);
            }
            else if (playLoad == "End")
            {
                Exit();
            }
        }

        public void Load(string input)
        {
            char[] savedGameCode = input.ToCharArray();

            for (int i = 0; i < savedGameCode.Length; i++)
            {
                if (savedGameCode[i] == 'O')
                {
                    boardPieces.Add(i, "O");
                    isItXturn = true;
                    arrayX.Add(i);
                    moves++;
                }
                else if (savedGameCode[i] == 'X')
                {
                    boardPieces.Add(i + 1, "X");
                    isItXturn = false;
                    arrayO.Add(i);
                    moves++;
                }
                else if (savedGameCode[i] == '0')
                {
                    continue;
                }
            }
            SavedGameValidation();
        }

        public void SavedGameValidation()
        {
            int hello = arrayX.Count() - arrayO.Count();

            if (hello == 1 || hello == -1 || hello == 0)
            {
                RunGame();
            }
            else
            {
                Console.WriteLine("Sorry, this code is not valid");
                boardPieces.Clear();
                arrayO.Clear();
                arrayX.Clear();
                moves = 0;
                Start();
            }
        }

        //Ask for either X or O key locations
        public void RunGame()
        {
            while (moves < 9)
            {
                if (isItXturn == true)
                {
                    Console.Write("Enter a location for X: ");
                    checkUp = Console.ReadLine();
                    if (checkUp == "End")
                    {
                        Exit();
                    }
                    else
                    {
                        location = Convert.ToInt32(checkUp);
                        KeyValidation();
                        isItXturn = false;
                        boardPieces.Add(location, "X");
                        UpdateGameBoard();
                    }
                    
                }
                else
                {
                    Console.Write("Enter a location for O: ");
                    checkUp = Console.ReadLine();
                    if (checkUp == "End")
                    {
                        Exit();
                    }
                    else
                    {
                        location = Convert.ToInt32(checkUp);
                        KeyValidation();
                        isItXturn = true;
                        boardPieces.Add(location, "O");
                        UpdateGameBoard();
                    }
                }
            }
        }

        //Is the space occupied?
        public void KeyValidation()
        {
            if (boardPieces.ContainsKey(location) == true)
            {
                Console.WriteLine("Invalid move, enter an empty spot");
                RunGame();
            }
        }

        public void UpdateGameBoard()
        {
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

        //End the game (to be turned into a save function later)
        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}



//---------------------------------------------------------------------- VERSION 3 BELOW prints a game keeping track of X and O, next we will validate that the space is empty and we can load saved games

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
