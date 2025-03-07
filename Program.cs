using System;

using UserInteraction;
using GameManager;

class Program{
    static void Main(String[] args){
        string[] startPrompt = {"Press the 'enter' key to play, or press 'q' to quit."};
        string[] startChoice;

        // ensure valid input
        do{
            startChoice = UserInput.GetUserInput(startPrompt, getKey: true);
        }
        while(!(startChoice[0] == "q" && startChoice[0] != "enter"));
        
        // game loop
        while(startChoice[0] != "q"){        
            Game.Start();
            startChoice = UserInput.GetUserInput(startPrompt, getKey: true);
        }
    }
}