using System;

namespace models{

    public enum ShipType{
        Carrier, // 5 length
        Battleship, // 4 length
        Cruiser, // 3 length
        Submarine, // 3 length
        Destroyer, // 2 length
    }

    public enum Direction{
        N,
        E,
        S,
        W
    }
    
    public class Ship{
        public ShipType ShipType;
        public int Length;
        public Direction Dir;
        public (int x, int y) BowPosition;

        public Ship(ShipType shipType, int shipLength)
        {
            ShipType = shipType;
            Length = shipLength;
        }

        public Ship(ShipType shipType, int shipLength, Direction dir, (int x, int y) bowPosition)
        {
            ShipType = shipType;
            Length = shipLength;
            Dir = dir;
            BowPosition = bowPosition;
        }
    }
}