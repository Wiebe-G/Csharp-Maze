using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Csharp_Doolhof
{
    internal class SelectFile
    {
        internal void UserInputForFileSelect()
        {
            string RootPath = AppDomain.CurrentDomain.BaseDirectory;
            IEnumerable<string> Files = Directory.EnumerateFiles(
                path: RootPath,
                searchPattern: "*.txt",
                searchOption: SearchOption.AllDirectories
                );
            int index = 0;
            foreach (string File in Files)
            {
                Console.WriteLine($"{index}: {File}");
                index++;
            }
            Console.WriteLine("Welk bestand wilt u gebruiken?");

            bool IsValidInput = false;
            bool Input;
            while (!IsValidInput)
            {
                Input = int.TryParse(Console.ReadLine(), out index);
                if (Input)
                {
                    Console.WriteLine($"Bestand {Files.ElementAt(index)} gekozen, laden...");
                    IsValidInput = true;
                    break;
                }
                Console.WriteLine("Input klopt niet, probeer het opnieuw.");
            }
            string ChosenFile = Files.ElementAt(index);
            ValidateFile MazeGame = new();
            MazeGame.IsValidFile(ChosenFile);
        }
    }
}
