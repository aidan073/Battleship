using System;
using System.IO;
using System.Text.Json;

using UserInteraction;
using models;

namespace GameManager{
    class Prompts{
        // For reading in prompts json file
        public string shipPlacement{get; set;}
    }

    class ValidateInputs{
        // User console input validation
        public static bool ValidateInput(int inputNum, string[] input){
            switch(inputNum){
                case 1:
                    string[] coords = input[0].Split(',');
                    if(input.Length < 2 || !int.TryParse(coords[0], out int x) || !int.TryParse(coords[1], out int y) || !Enum.TryParse(input[1], out Direction dir)){
                        return false;
                    }
                    break;
                case 2:
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid inputNum: {inputNum}.");
            }
            return true;
        }
    }
    
    class Game{
        public static void Start(){

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
                
                // get user's ship placement
                string[] prompt = {prompts.shipPlacement};
                string[] userIn = UserInput.GetUserInput(prompt);
                while(!ValidateInputs.ValidateInput(1, userIn)){
                    Console.WriteLine("Invalid input, please try again.");
                    userIn = UserInput.GetUserInput(prompt);
                }
                string[] coords = userIn[0].Split(',');
                ships[idx] = new Ship(currShipType, length, (Direction)Enum.Parse(typeof(Direction), userIn[1]), (int.Parse(coords[0]), int.Parse(coords[1])));
                idx++;
            }
        }
    }
}