using UserInteraction;
using models;

namespace GameManager
{

    class ValidateInputs
    {
        // User console input validation
        public static bool ValidateInput(string promptKey, string[] input)
        {
            switch (promptKey)
            {
                case "shipPlacement":
                    string[] coords = input[0].Split(',');
                    if (input.Length < 2 || !int.TryParse(coords[0], out int x) || !int.TryParse(coords[1], out int y) || !Enum.TryParse(input[1].ToUpper(), out Direction dir))
                    {
                        return false;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Invalid promptKey for validation: {promptKey}.");
            }
            return true;
        }
    }

    class Game
    {
        public static void Start()
        {
            var gameControlPrompts = PromptLoader.LoadFromPath(Path.Join("prompts", "GameControlPrompts.json"));
            ShipType[] allShipTypes = Enum.GetValues<ShipType>();
            Ship[] ships = new Ship[allShipTypes.Length];
            // Construct ships
            for (int i = 0; i < allShipTypes.Length; i++)
            {
                switch (allShipTypes[i])
                {
                    case ShipType.Carrier:
                        ships[i] = new Ship(ShipType.Carrier, 5);
                        break;
                    case ShipType.Battleship:
                        ships[i] = new Ship(ShipType.Battleship, 4);
                        break;
                    case ShipType.Cruiser:
                        ships[i] = new Ship(ShipType.Cruiser, 3);
                        break;
                    case ShipType.Submarine:
                        ships[i] = new Ship(ShipType.Submarine, 3);
                        break;
                    case ShipType.Destroyer:
                        ships[i] = new Ship(ShipType.Destroyer, 2);
                        break;
                    default:
                        throw new ArgumentException($"Failed to construct ship of type: {allShipTypes[i]}.");
                }
            }

            // get user's ship placement
            // TODO: Allow user to select ship type
            // TODO: Validate location of ship placement
            string[] shipPlacementPrompt = PromptLoader.KeyToPromptArray("shipPlacement", gameControlPrompts);
            foreach (Ship currShip in ships)
            {
                string[] userIn = UserInput.GetUserInput(shipPlacementPrompt);
                while (!ValidateInputs.ValidateInput("shipPlacement", userIn))
                {
                    Console.WriteLine("Invalid input, please try again.");
                    userIn = UserInput.GetUserInput(shipPlacementPrompt);
                }
                string[] coords = userIn[0].Split(',');
                currShip.Dir = (Direction)Enum.Parse(typeof(Direction), userIn[1]);
                currShip.BowPosition = (int.Parse(coords[0]), int.Parse(coords[1]));
            }
        }
        public static void End()
        {
            Globals.gameState = GameState.Off;
            return;
        }
    }
}