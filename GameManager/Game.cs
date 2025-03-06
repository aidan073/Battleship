using System;

using models;

namespace GameManager{
    class Game{

        public static void Start(){

            //add functionality for getting directions and bow location:
            Direction dir = Direction.North;
            (int x, int y) bowPos = (0,0);

            //initialize ships
            var allShipTypes = Enum.GetValues(typeof(ShipType));
            Ship[] ships = new Ship[allShipTypes.Length];
            int idx = 0;
            foreach(ShipType currShipType in allShipTypes){
                int length;
                switch(currShipType){
                    case ShipType.Carrier:
                        length = 5;
                        break;
                    case ShipType.Battleship:
                        length = 4;
                        break;
                    case ShipType.Cruiser:
                        length = 3;
                        break;
                    case ShipType.Submarine:
                        length = 3;
                        break;
                    case ShipType.Destroyer:
                        length = 2;
                        break;
                    default:
                        throw new ArgumentException($"Invalid ShipType {currShipType}.");
                }
                ships[idx] = new Ship(currShipType, length, dir, bowPos);
                idx++;
            }
        }
    }
}