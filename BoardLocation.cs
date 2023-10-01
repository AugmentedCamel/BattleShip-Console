using System;


namespace BattleShip_Console
{
    public class BoardLocation : Point
    {
        public Board Board { get; private set; }
        public bool IsHit { get; set; } = false;

        public BoardLocation(int x, int y, Board board) : base(x, y)
        {
            Board = board;

            if (!board.OnBoard(this))
            {
                throw new ArgumentOutOfRangeException(x + "," + y + " is outside the boundaries of the board.");
            }
        }

    }
    
}
