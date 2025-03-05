using System;

namespace models{
    public enum Direction{
        North,
        South,
        East,
        West
    }
    
    public struct Boat{
        public string Name;
        public int Length;
        public Direction Dir;
        public (int x, int y) BowPosition;

        public Boat(string name, int length, Direction dir, (int x, int y) bowPosition){
            Name = name;
            Length = length;
            Dir = dir;
            BowPosition = bowPosition;
        }
    }
}