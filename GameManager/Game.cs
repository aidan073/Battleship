using UserInteraction;
using models;

namespace GameManager
{

    class ValidateInputs
    {
        // User console input validation
        public static bool ValidateInput(int inputNum, string[] input)
        {
            switch (inputNum)
            {
                case 1:
                    string[] coords = input[0].Split(',');
                    if (input.Length < 2 || !int.TryParse(coords[0], out int x) || !int.TryParse(coords[1], out int y) || !Enum.TryParse(input[1], out Direction dir))
                    {
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

    class Game
    {
        public static void Start()
        {

            //initialize ships
            var gameControlPrompts = PromptLoader.LoadFromPath(Path.Join("prompts", "GameControlPrompts"));
            var allShipTypes = Enum.GetValues(typeof(ShipType));
            Ship[] ships = new Ship[allShipTypes.Length];
            int idx = 0;
            foreach (ShipType currShipType in allShipTypes)
            {
                int length;
                switch (currShipType)
                {
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
                string[] shipPlacementPrompt = { gameControlPrompts["shipPlacement"].ToString() ?? "" };
                string[] userIn = UserInput.GetUserInput(shipPlacementPrompt);
                while (!ValidateInputs.ValidateInput(1, userIn))
                {
                    Console.WriteLine("Invalid input, please try again.");
                    userIn = UserInput.GetUserInput(shipPlacementPrompt);
                }
                string[] coords = userIn[0].Split(',');
                ships[idx] = new Ship(currShipType, length, (Direction)Enum.Parse(typeof(Direction), userIn[1]), (int.Parse(coords[0]), int.Parse(coords[1])));
                idx++;
            }
        }
        public static void End()
        {
            Globals.gameState = GameState.Off;
            return;
        }
    }
}