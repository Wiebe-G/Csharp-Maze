using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_Doolhof
{
    internal class Game
    {
        internal void Maze(string FilePath)
        {
            if (!IsValidFile(FilePath))
            {
                Console.WriteLine("Bestand is ongeldig. Unlocko, probeer het opnieuw.");
                Environment.Exit(67);
            }
        }

        internal bool IsValidFile(string FilePath)
        {
            //char[,] Maze = new char[,];
            char[] AllowedChars = ['#', '.', 'E', 'e', 'S', 's', 'K', 'k', 'D', 'd'];
            using (StreamReader sr = new(FilePath))
            {
                string line;
                int Column = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    for (int Row = 0; Row < line.Length; Row++)
                    {
                        Console.Write($"Array {Column}-{Row}: ");
                        //Maze[Column, Row] = line[Column][Row];
                        Console.WriteLine(line[Row]);
                        if (!AllowedChars.Contains(line[Row]))
                        {
                            EndLoopDueToIllegalCharAtColumnRow(Column, Row);
                        }

                    }
                    Console.WriteLine("Nieuwe rij");
                    Column++;
                }
            }
            return true;
        }

        internal void EndLoopDueToIllegalCharAtColumnRow(int Column, int Row)
        {
            Console.WriteLine($"Illegaal karakter gevonden op {Column}-{Row}. Verwijder dat karakter.");
            Environment.Exit(1);
        }
    }
}
