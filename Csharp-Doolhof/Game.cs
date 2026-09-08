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
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int Column = 0;
                    for (int Row = 0; Row < line.Length; Row++)
                    {
                        Console.Write($"Array {Column}-{Row}: ");
                        Console.WriteLine(line[Row]);
                    }
                    Column++;
                }
            }
            return true;
        }
    }
}
