using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_Doolhof
{
    internal class ValidateFile
    {
        internal void IsValidFile(string FilePath)
        {
            char[] AllowedChars = ['#', '.', 'E', 'e', 'S', 's', 'K', 'k', 'D', 'd'];
            using (StreamReader sr = new(FilePath))
            {
                string line;
                int Column = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    for (int Row = 0; Row < line.Length; Row++)
                    {
                        Console.Write($"{Column}-{Row}: ");
                        Console.WriteLine(line[Row]);
                        if (!AllowedChars.Contains(line[Row]))
                        {
                            EndLoopDueToIllegalCharAtColumnRow(Column, Row);
                        }
                    }
                    Column++;
                }
                Console.WriteLine("Bestand is goedgekeurd!. Verder met laden...");
                //Thread.Sleep(3000);
                Console.Clear();

                Game g = new();
                g.LoadMaze(FilePath);
            }
        }

        internal void EndLoopDueToIllegalCharAtColumnRow(int Column, int Row)
        {
            Console.WriteLine($"Illegaal karakter gevonden op {Column}-{Row}. Verwijder dat karakter.");
            Environment.Exit(1);
        }
    }
}
