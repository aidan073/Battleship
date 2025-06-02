using System.Text.Json;

namespace UserInteraction;

static class PromptLoader
{
    internal static Dictionary<string, JsonElement> LoadFromPath(string promptsPath)
    {
        if (!File.Exists(promptsPath))
            throw new FileNotFoundException($"File {promptsPath} does not exist");

        string rawPrompts = File.ReadAllText(promptsPath);
        var prompts = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(rawPrompts);

        if (prompts == null)
            throw new InvalidOperationException($"Failed to deserialize prompts from JSON file @ {promptsPath}.");

        return prompts;
    }

    internal static string[] KeyToPromptArray(string promptKey, Dictionary<string, JsonElement> prompts)
    {
        var promptArray = JsonSerializer.Deserialize<string[]>(prompts[promptKey].GetRawText());
        if (promptArray == null)
        {
            throw new InvalidOperationException($"Failed to deserialize prompt array with key: {promptKey}.");
        }
        return promptArray;
    }
}