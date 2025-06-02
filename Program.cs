using System;

using UserInteraction;
using GameManager;

public enum GameState {
    On,
    Off
}

public static class Globals
{
    public static GameState gameState = GameState.On;
}

class Program
{
    static void Main(string[] args)
    {
        // Console.WriteLine(Directory.GetCurrentDirectory());
        var gameStatePrompts = PromptLoader.LoadFromPath(Path.Join("prompts", "GameStatePrompts.json"));
        string[] startPrompt = PromptLoader.KeyToPromptArray("start", gameStatePrompts);
        string[] startChoice;

        // game loop
        while (Globals.gameState == GameState.On)
        {
            do
            {
                startChoice = UserInput.GetUserInput(startPrompt, getKey: true);
            }
            while (startChoice[0].ToLower() != "q" && startChoice[0].ToLower() != "enter");
            
            if (startChoice[0].ToLower() == "q")
            {
                Game.End();
                break; //TODO: Remove if end exits program.
            }
            Game.Start();
        }
    }
}