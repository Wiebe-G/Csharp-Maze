using System;
using System.IO;
using System.Linq;

namespace Csharp_Doolhof
{
    internal class Game
    {

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
                    for (int Column = 0; Column <= ColumnCount; Column++)
                    {
                        Maze[Row, Column] = line[Row];
                        Console.WriteLine($"Maze op {Column} {Row} is nu {line[Row]}");
                    }
                    Row++;
                }
            }

            Console.WriteLine("MI BOMBO");
        }
    }
}
