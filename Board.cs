using BattleShip_Console;
using System;
using System.Drawing;

namespace BattleShip_Console
{
    public class Board
    {
        public readonly int Width;
        public readonly int Height;
        public readonly string BoardName;

        public Board(int width, int height, string boardName)
        {
            Width = width;
            Height = height;
            //Console.WriteLine($"board: {boardName} set for X {width - 1} and Y {height - 1}");
            BoardName = boardName;
        }

        public bool OnBoard(Point point)
        {
            //Console.WriteLine($"point {point} is not in bounds");
            return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        }
        public bool OnBoard(int x, int y) 
        { 
            Point point = new Point(x, y);
            return OnBoard(point) ? true : false; 
            
        }
    }
}
