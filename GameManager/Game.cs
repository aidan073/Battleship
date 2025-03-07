using System;
using System.IO;
using System.Text.Json;

using UserInteraction;
using models;

namespace GameManager{
    class Prompts{
        public string shipPlacement{get; set;}
    }
    class Game{

        public static void Start(){

            //add functionality for getting directions and bow location:
            string promptsPath = "prompts/UserPrompts.json";
            string rawPrompts;

            

            if(File.Exists(promptsPath)){
                rawPrompts = File.ReadAllText(promptsPath);
            }
            else{
                throw new FileNotFoundException($"File {promptsPath} does not exist");
            }
            Prompts prompts = JsonSerializer.Deserialize<Prompts>(rawPrompts);


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
                string[] prompt = {prompts.shipPlacement};
                string[] userIn = UserInput.GetUserInput(prompt);
                ships[idx] = new Ship(currShipType, length, userIn[1], userIn[0]);
                idx++;
            }
        }
    }
}