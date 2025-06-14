using UserInteraction;
using models;
using System.Numerics;
using System.Xml.Serialization;

namespace GameManager
{
    
    class ValidateInputs
    {
        // User console input validation
        public static bool ValidateInput(string promptKey, string[] input, Dictionary<string, object>? kwargs = null)
        {
            switch (promptKey)
            {
                case "shipChoice":
                    if (kwargs == null)
                    {
                        throw new NullReferenceException("kwargs needs to be accessed by 'shipChoice' input validator, but is null");
                    }
                    int totalShips = (int) kwargs["totalShips"];
                    if (!int.TryParse(input[0], out int choice))
                    {
                        Console.WriteLine($"Invalid choice of: \"{input[0]}\", please try again.");
                        return false;
                    }
                    if (!(choice >= 1 && choice <= totalShips))
                    {
                        Console.WriteLine($"Your choice: \"{choice}\" is out of allowed range 1 -> {totalShips}, please try again.");
                        return false;
                    }
                    break;

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

    public static class Printer
    {
        public static void GiveShipChoices(IEnumerable<Ship> shipArray)
        {
            Console.WriteLine();
            int idx = 1;
            foreach (Ship ship in shipArray)
            {
                Console.WriteLine(idx + ". " + ship.ShipType.ToString());
                idx++;
            }
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
            string[] shipChoicePrompt = PromptLoader.KeyToPromptArray("shipType", gameControlPrompts);
            string[] shipPlacementPrompt = PromptLoader.KeyToPromptArray("shipPlacement", gameControlPrompts);
            List<Ship> shipsToPlace = ships.ToList();
            for (int i = 0; i < ships.Length; i++)
            {
                string[] shipChoice;
                var choiceKwargs = new Dictionary<String, Object> { ["totalShips"] = shipsToPlace.Count };
                //TODO: Refactor getuserinput so giveshipchoices doesn't need to be called before like this
                do
                {
                    Printer.GiveShipChoices(shipsToPlace);
                    shipChoice = UserInput.GetUserInput(shipChoicePrompt, getKey: true);
                }
                while (!ValidateInputs.ValidateInput("shipChoice", shipChoice, choiceKwargs));
                int currShipIdx = int.Parse(shipChoice[0]) - 1;
                var currShip = shipsToPlace[currShipIdx];
                shipsToPlace.RemoveAt(currShipIdx);
                Console.WriteLine($"You chose '{currShip.ShipType}'");

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