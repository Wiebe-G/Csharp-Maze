using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Csharp_Doolhof
{
    internal class Game
    {

        internal void LoadMaze(string FilePath)
        {
            int ColumnCount = File.ReadLines(FilePath).FirstOrDefault().Length;
            int RowCount = File.ReadAllLines(FilePath).Length;

            //Console.WriteLine($"Array moet {ColumnCount} kolommen breed zijn en {RowCount} regels hoog zijn");
            //ColumnCount--;
            //RowCount--;

            char[,] Maze = new char[RowCount, ColumnCount];
            using (StreamReader sr = new(FilePath))
            {
                string line;
                int Row = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    for (int Column = 0; Column < ColumnCount; Column++)
                    {
                        Maze[Row, Column] = line[Column];
                    }
                    Row++;
                }
            }

            Console.WriteLine("Bestand correct ingeladen");
            RenderMaze(Maze);
        }

        internal void RenderMaze(char[,] Maze)
        {
            for (int Row = 0; Row < Maze.GetLength(0); Row++)
            {
                for (int Column = 0; Column < Maze.GetLength(1); Column++)
                {
                    Console.Write(Maze[Row, Column]);
                }
                Console.WriteLine("\n");
            }
        }
    }
}
