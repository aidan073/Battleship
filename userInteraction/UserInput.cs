using System;

namespace UserInteraction{
    class UserInput{
        public static string[] GetUserInput(string[] prompt, bool getKey=false){
            string[] userInputs = new string[prompt.Length];
            for(int i = 0; i < userInputs.Length; i++){
                Console.WriteLine(prompt[i]);
                if(getKey){ // get key press instead of line
                   ConsoleKeyInfo inputKeyInfo = Console.ReadKey(intercept: true); 
                   userInputs[i] = inputKeyInfo.Key.ToString();
                   Console.WriteLine();
                }
                else{
                    userInputs[i] = Console.ReadLine() ?? ""; // default to empty string to avoid null
                }
            }
            return userInputs;
        }

        public static bool EnsureValidShipLocation(){
            return true;
        }
    }
}