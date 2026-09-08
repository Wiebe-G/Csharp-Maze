using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

                LoadMaze(FilePath);
            }
            return true;
        }

        internal void EndLoopDueToIllegalCharAtColumnRow(int Column, int Row)
        {
            Console.WriteLine($"Illegaal karakter gevonden op {Column}-{Row}. Verwijder dat karakter.");
            Environment.Exit(1);
        }

        internal void LoadMaze(string FilePath)
        {
            int ColumnCount = File.ReadLines(FilePath).FirstOrDefault().Length;
            int RowCount = File.ReadAllLines(FilePath).Length;

            Console.WriteLine($"Array moet {ColumnCount} kolommen breed zijn en {RowCount} regels hoog zijn");
            ColumnCount--;
            RowCount--;

            char[,] Maze = new char[RowCount, ColumnCount];
            using (StreamReader sr = new(FilePath))
            {
                string line;
                int Row = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    /*
                     * Wat hier foutgaat:
                     * het gaat index 0, oftewel de eerste, en dat kan
                     * dan komt het uitendelijk bij index 6 (bijvoorbeeld), dus de zevende, van de 6 elementen
                     * dat klopt niet
                     * dus error
                     */
                    for (int Column = 0; Column <= ColumnCount; Column++)
                    {
                        Console.Write($"{line[Column]}");
                    }
                    Row++;
                    Console.Write('\n');
                }
            }
        }
    }
}
