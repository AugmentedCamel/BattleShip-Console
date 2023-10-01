// See https://aka.ms/new-console-template for more information
using BattleShip_Console;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Battleship_Console
{
    

    public enum GameMode
    {
        PlayerVsPlayer,
        PlayerVsAI
    }

    public class Game
    {
        //Console.WriteLine("Game started");
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Board BoardA { get; private set; }
        public Board BoardB { get; private set; }
        public GameMode Mode { get; private set; }
        //public string ColorStyle { get; private set; }
        private bool isGameOver = false;
        private bool devMode = false; //to start the game without initializing
        public Game(bool devMode)
        {
            //if devmode is true, the game should initialize already withouh asking for inputs.
            //Mode = mode;
            //ColorStyle = colorStyle;
            InitializeBoards(devMode);
            InitializePlayers(devMode);
            Start();

        }

        private void InitializeBoards(bool devMode)
        {
            if (devMode) {
                        BoardA = new Board(10, 10, "boardA");
                        BoardB = new Board(10, 10, "boardB");
                        }
            else //future feature: initalize board according to user inputs, or saved boards with obstructions etc.
            {
                BoardA = new Board(10, 10, "boardA"); 
                BoardB = new Board(10, 10, "boardB");
            }
            
        }
        private void InitializePlayers(bool devMode)
        {
            if (devMode)
            {
                Player1 = new Player("dev P1", BoardA, BoardB);
                Player2 = new Player("dev P2", BoardB, BoardA);
                Player1.InitializePlayer(Player2);
                Player2.InitializePlayer(Player1);
                return;
            }
            
            //instantiate 2 players, that can set new boardlocations each turn.
            Console.WriteLine("Player 1, what is your name?");
            string p1Name = Console.ReadLine();
            Player1 = new Player($"'{p1Name}'", BoardA, BoardB);
            

            Console.WriteLine("Player 2, what is your name?");
            string p2Name = Console.ReadLine();
            Player2 = new Player($"'{p2Name}'", BoardA, BoardB);

            Player1.InitializePlayer(Player2);
            Player2.InitializePlayer(Player1);

        }

        private void Start()
        {
            Console.WriteLine("-----------The game will now Start------------");
            
            Player currentPlayer = Player1;
            Player opponentPlayer = Player2;

            while (!isGameOver)
            {
                bool isEndCommandReceived = PlayTurn(currentPlayer, opponentPlayer);

                if (isEndCommandReceived)
                {
                    break;  // Exit the game loop if the "end" command was received
                }

                // Swap the roles of the players for the next turn
                Player temp = currentPlayer;
                currentPlayer = opponentPlayer;
                opponentPlayer = temp;
            }

            // Optionally, return to main menu or exit the game
            // ... (your code here)


        }

        public bool PlayTurn(Player currentPlayer, Player opponentPlayer) //this is the game, tried to keep this method simpel
        {
            while (true)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine($"-----------Turn of {currentPlayer.PlayerName}-----------");
                Console.WriteLine("-------Enter action: (shoot, view, etc.)------");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "shoot":
                        currentPlayer.Shoot(opponentPlayer);
                        //Console.WriteLine($"{currentPlayer} shot at {opponentPlayer}");

                        //check if the game has ended:
                        if (opponentPlayer.didPlayerLose()) { return true; }
                        return false; //ends the turn but not the game
                    case "view":
                        //Console.WriteLine($"{currentPlayer} viewed his own ship position on {currentPlayer.BoardSelf.BoardName}");
                        currentPlayer.ShowShipPositions();
                        break;
                    case "end":
                        Console.WriteLine($"Game should, end and return to homescreen");
                        isGameOver = true;
                        return true; //ends the turn and also the game
                    //insert more cases maybe move, or check shots whatever
                    default:
                        Console.WriteLine("Invalid action, please try again");
                        break;
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            //main menu launches
            Console.WriteLine("Welcome to Battleship!");
            Console.WriteLine("1. Change Color Style");
            Console.WriteLine("2. 2 Player Game");
            Console.WriteLine("3. Play against AI");
            Console.WriteLine("4. Developer Mode (start after init)");
            // ... (other menu options)
            string choice = Console.ReadLine();

            Game game;
            switch (choice)
            {
                case "1":
                    Console.WriteLine("feature for in the future");
                    //string colorStyle = Console.ReadLine();
                    //game = new Game(GameMode.PlayerVsPlayer, colorStyle);  // Defaulting to PlayerVsPlayer for this example
                    break;
                case "2":
                    game = new Game(false);  // Default color style
                    break;
                case "3":
                    Console.WriteLine("feature for in the future");
                    //game = new Game(GameMode.PlayerVsAI, "Default");  // Default color style
                    break;
                case "4":
                    game = new Game(true); //launches dev mode
                    break;
                default:
                    Console.WriteLine("Invalid choice, exiting.");
                    return;
            }

            //Console.WriteLine("-----------The game will now Start------------");
            
            




            /*Board boardA = new Board(10, 10, "boardA");
            Board boardB = new Board(10, 10, "boardB");

            try
            {
                //instantiate 2 players, that can set new boardlocations each turn.
                Player playerA = new Player("'player A'", boardA, boardB);
                Player playerB = new Player("'player B'", boardB, boardA);

                
                //first turn to place ships
                playerA.Turn();
                playerB.Turn();

                //game starts
                PlayTurn(playerA, playerB);









            }
            catch (ArgumentOutOfRangeException ex) { Console.WriteLine(ex.Message);  }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            */

        }
    }
}
 