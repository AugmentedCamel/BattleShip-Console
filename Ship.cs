/*using System;


namespace BattleShip_Console
{
    public class Ship
    {
        public BoardLocation ShipLocation { get; set; } // is always on a specific board x, y, board
        private BoardLocation[] Shipbody = new BoardLocation[1]; //ship body is always 1 long
        public bool IsSunken { get; private set; } = false; //ship information should be located in the ship class. 

        public Ship(BoardLocation shipLocation)
        {
            ShipLocation = shipLocation;
            //Shipbody = ShipLocation;
        }

        //method for ship damage
        public void PlaceShip(BoardLocation location)
        {
            ShipLocation = location;
        }

        public bool CheckIfHit(BoardLocation location)
        {
            
            if (ShipLocation.X == location.X && ShipLocation.Y == location.Y)
            {
                BoardLocation hitLocation = location;
                TakeDamage(hitLocation);
                return true;
            }
            else 
            {
                return false;
            }
           
        }
        public void TakeDamage(BoardLocation shipLoc)
        {
            //later add a loop that checks if the ship took damage at all locations in the ship body

            IsSunken = true;
        }
        
    }
}*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShip_Console
{
    public class Ship
    {
        public List<BoardLocation> Shipbody { get; private set; } = new List<BoardLocation>();
        public bool IsSunken { get; private set; } = false;

        public Ship(BoardLocation initialLocation)
        {
            PlaceShip(initialLocation);
        }

        public void PlaceShip(BoardLocation location)
        {
            Shipbody.Add(location);
        }

        public bool CheckIfHit(BoardLocation location)
        {
            foreach (var shipLocation in Shipbody)
            {
                if (shipLocation.X == location.X && shipLocation.Y == location.Y && shipLocation.IsHit)
                {
                    Console.WriteLine("You already hit that location!");
                    return true;
                }
                 

                if (shipLocation.X == location.X && shipLocation.Y == location.Y)
                {
                    Console.WriteLine("HIT!");
                    TakeDamage(shipLocation);
                    return true;
                }
            }
            return false;
        }

        public void TakeDamage(BoardLocation hitLocation)
        {
            hitLocation.IsHit = true;  // Assuming BoardLocation has an IsHit property
            Console.WriteLine("Ship took severe damage!");

            IsSunken = true;
            // Check if all locations in Shipbody are hit
            
        }

        

    }
}
