using System;
using Models;
using UserInteraction;

class Program{
    static void Main(String[] args){
        string startPrompt = "store this somewhere";
        string startChoice = "";
        do{
            startChoice = UserInput(startPrompt);
            //validate here
        }
        while(startChoice == "play");
    }
}