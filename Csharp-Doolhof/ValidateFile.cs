using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Csharp_Doolhof
{
    internal class ValidateFile
    {
        internal void IsValidFile(string FilePath)
        {
            char[] AllowedChars = ['#', '.', 'E', 'S', 'K', 'D',];
            bool HasStart = false;
            bool HasKey = false;
            bool HasDoor = false;
            bool HasEnd = false;
            using (StreamReader sr = new(FilePath))
            {
                string line;
                int Column = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    for (int Row = 0; Row < line.Length; Row++)
                    {
                        char CurrentChar = Char.ToUpper(line[Row]);
                        if (!AllowedChars.Contains(CurrentChar))
                        {
                            Console.WriteLine($"Illegaal karakter gevonden op {Column}-{Row}. Verwijder dat karakter.");
                            Environment.Exit(1);
                        }

                        switch (CurrentChar)
                        {
                            case 'S':
                                HasStart = true;
                                break;
                            case 'K':
                                HasKey = true;
                                break;
                            case 'D':
                                HasDoor = true;
                                break;
                            case 'E':
                                HasEnd = true;
                                break;
                        }
                    }
                    Column++;
                }
            }
            if (!HasStart || !HasDoor || !HasKey || !HasEnd)
            {
                Console.WriteLine("Bestand bevat niet een start, sleutel, deur, en/of einde. Voeg deze toe en probeer opnieuw");
                return;
            }
            Console.WriteLine("Bestand is goedgekeurd! Verder met laden...");
            //Thread.Sleep(3000);
            Console.Clear();

            Game g = new();
            g.LoadMaze(FilePath);
        }
    }
}
