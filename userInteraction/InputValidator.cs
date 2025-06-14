using models;

namespace UserInteraction;

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
                int totalShips = (int)kwargs["totalShips"];
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