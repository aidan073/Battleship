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
    static void Main(String[] args)
    {
        var gameStatePrompts = PromptLoader.LoadFromPath(Path.Join("prompts", "GameStatePrompts.json"));
        string[] startPrompt = { gameStatePrompts["start"].GetString() ?? "" };
        string[] startChoice;

        // game loop
        while (Globals.gameState == GameState.On)
        {
            do
            {
                startChoice = UserInput.GetUserInput(startPrompt, getKey: true);
            }
            while (startChoice[0] != "q" && startChoice[0] != "enter");
            
            if (startChoice[0] == "q")
            {
                Game.End();
                break; //TODO: Remove if end exits program.
            }
            Game.Start();
        }
    }
}