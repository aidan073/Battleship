using System;

namespace UserInteraction{
    class UserInput{
        public static GetUserInput(string[] prompt){
            string[] userInputs = new string[prompt.Length];
            for(int i = 0; i < prompt.Length; i++){
                Console.WriteLine(prompt[i]);
                userInputs[i] = Console.ReadLine();
            }
            return userInputs;
        }
    }
}