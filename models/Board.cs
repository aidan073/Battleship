using System;

namespace Models{
    class Board{
        const int size = 10;
        public Tile[,] tiles = new Tile[size, size];

        public void PrintBoard(){
            for(int i = 0; i<tiles.GetLength(0); i++){  
                for(int j = 0; j<tiles.GetLength(1); j++){
                    bool shipHit = tiles[i, j].HasShip && tiles[i, j].WasHit;
                    if(shipHit) Console.ForegroundColor = ConsoleColor.Red; 
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(tiles[i, j].ToString());
                }
                Console.Write("\n");
            }
        }
    }

    public struct Tile{
        private const int height = 3;
        private const int width = 5;
        public bool HasShip{get; set;}
        public bool WasHit{get; set;}
        public override string ToString(){
            return WasHit ? "[X]": "[ ]";
        }
    }
}