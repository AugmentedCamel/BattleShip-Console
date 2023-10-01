

using Battleship_Console;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace BattleShip_Console
{
    public class Player
    {
        public readonly string ?PlayerName;
        public Board BoardSelf { get; set; }
        public Board BoardOpponent { get; set; }
        //private Player opponent;
        //private Player self;
        
        public static readonly int MaxShips = 1;
        private readonly string? Retry;
        public Ship[] ownFleet = new Ship[MaxShips];
        private bool areShipsPlaced = false;

        public Player(string ?playerName, Board self, Board other)
        {
            PlayerName = playerName;
            BoardSelf = self;
            BoardOpponent = other;
        }

        public void InitializePlayer(Player other)
        {
            Console.WriteLine($"___Welcome to the Battlefield {PlayerName}, Setup your start positions:");
            Console.WriteLine($"---The field size has {BoardSelf.Width} width and {BoardSelf.Height} length. Place your ships within boundaries");
            
 
            //are the ships placed?
            if(!areShipsPlaced)
            {
                SetShipLocations();
                areShipsPlaced = true;

                Console.WriteLine($"----------Well done, all your vessels are combat ready------------");
                Console.WriteLine($"--------------------------End of turn-----------------------------");

                //this should end the turn
                return;
            }
            
            //this is the main void of player,
            //a player can only shoot at the opponent board once.
        }

        public void ShowShipPositions()
        {
            Console.WriteLine("The current positon of your vessels:");

            foreach (Ship ship in ownFleet)
            {
                
                foreach (BoardLocation location in ship.Shipbody)
                {
                    if (location.Board.BoardName != "testBoard") 
                    {
                        Console.WriteLine($"Current ship location {location.X},{location.Y} on board: {location.Board.BoardName}");
                        Console.WriteLine($"This ship is {(ship.IsSunken ? "SUNKEN" : "ALIVE")}");
                    }
                   
                }
                //Console.WriteLine($"This ship is {(ship.IsSunken ? "SUNKEN" : "ALIVE")}");




                //Console.WriteLine($"Current ship location {(ship.ShipLocation.X)},{(ship.ShipLocation.Y)} on board: {ship.ShipLocation.Board.BoardName}");
                //Console.WriteLine($"This ship is (SUNKEN/ALIVE)");
            }
    
        }

        public void SetShipLocations() //instantiates a ship on userinput and stores it to an array
        {

            
            //first instantiate the ships to be placed on a testboard
            Board testBoard = new Board(100, 100, "testBoard");
            for (int i = 0; i < MaxShips; i++)
            {
                ownFleet[i] = new Ship(new BoardLocation(50, 50, testBoard));
            }
            
            int fleetIndex = 1;
            int fleetLength = ownFleet.Length;

            //for each ship on the testboard, ask the user to place it on the real board
            foreach (Ship ship in ownFleet)
            {
                while (true)
                {
                    Console.WriteLine($" ****Player {PlayerName}, Place your vessel: [{fleetIndex}/{fleetLength}]****");
                    BoardLocation locToPlace = GetCoordinates(BoardSelf); //only returns if the coordinates are on the board passed.

                    if (IsLocationOccupied(ownFleet, locToPlace)) //does not place a ship and returns true if location is occupied
                    {
                        Console.WriteLine("Location is already occupied!");
                        //should restart
                    }
                    else
                    {

                        Console.WriteLine($"Ship placed at location {locToPlace.X},{locToPlace.Y}.");
                        ship.PlaceShip(locToPlace);
                        fleetIndex++;
                        //now the while loop should be ended becaue the ship is placed
                        break;
                    }
                }

            }
            
            //the location of all the ships are set 
            

        }

        public void ReceiveShot(BoardLocation location)
        {
            bool missed = true;
            foreach (Ship ship in ownFleet)
            {
                ship.CheckIfHit(location);
                missed = false;
            }

            if(missed) { Console.WriteLine("Missed");  }

            
        }

        public void Shoot(Player otherPlayer)
        {
            // first get a correct location, player should be able to retry if the location is invalid
            Console.WriteLine("Enter your aimed loction [X,Y]");
            BoardLocation aimedLocation = GetCoordinates(BoardOpponent);
            Console.WriteLine($"Shot at coordinates: {aimedLocation.X},{aimedLocation.Y} on board: {aimedLocation.Board.BoardName}");

            // check if the location is a hit
            otherPlayer.ReceiveShot(aimedLocation);
            
            //return back with information if the hit succeeded
            

        }

        public BoardLocation GetCoordinates(Board board) //this asks a location on the board to the user and checks if location is valid on the board
        {
            while (true)  // This will loop until a return statement is hit
            {
                //Console.WriteLine($"Insert ship location for player {PlayerName} [x,y]");
                string answer = Console.ReadLine();

                string[] coordinates = answer.Split(',');

                if (coordinates.Length != 2)
                {
                    Console.WriteLine("Invalid Input, Please enter coordinates X,Y exactly like that");
                    continue;  // This will skip the rest of the loop body and start the next iteration
                }

                bool successX = int.TryParse(coordinates[0], out int x);
                bool successY = int.TryParse(coordinates[1], out int y);

                if (!successX || !successY)
                {
                    Console.WriteLine("Invalid input. Please enter valid integer coordinates.");
                    continue;  // This will skip the rest of the loop body and start the next iteration
                }

                bool succesboard = board.OnBoard(x, y);

                if (!succesboard)
                {
                    Console.WriteLine("Invalid input, Please enter a value withing range of the board");
                    continue;
                }

                return new BoardLocation(x, y, board);  // This will exit the method and return the result
            }
        }

        public bool IsLocationOccupied(Ship[] vessels, BoardLocation testLocation) //check in the array if any of the ships is already located on passed boardlocation
        {
            foreach (Ship ship in vessels)
            {
                foreach (BoardLocation location in ship.Shipbody)
                {
                    if ((testLocation.X == location.X) && (testLocation.Y == location.Y) && (testLocation.Board.BoardName == location.Board.BoardName))
                    {
                        //Console.WriteLine($"location checked and occupied!!!");
                        return true;
                    }
                }
                
            }
            
            return false;
        }

        public bool didPlayerLose()
        {
            foreach (Ship ship in ownFleet)
            {
                foreach (BoardLocation location in ship.Shipbody)
                {
                    if (location.Board.BoardName != "testBoard")
                    {
                        //return true if this player lost
                        if (!ship.IsSunken) { return false; } //if a ship is not sunken, the player did not lose
                        
                        Console.WriteLine($"Player {PlayerName} Lost");
                        return true;
                    }
                }

            }

            return false;
        }

    }
}
