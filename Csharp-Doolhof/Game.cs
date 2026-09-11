using System;
using System.Collections.Generic;
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
            Position Pos = new();
            for (int Row = 0; Row < Maze.GetLength(0); Row++)
            {
                for (int Column = 0; Column < Maze.GetLength(1); Column++)
                {
                    Console.Write(Maze[Row, Column]);
                    switch (Maze[Row, Column])
                    {
                        case 'S':
                            Pos.StartRow = Row;
                            Pos.StartColumn = Column;
                            break;
                        case 'K':
                            Pos.KeyRow = Row;
                            Pos.KeyColumn = Column;
                            break;
                        case 'D':
                            Pos.DoorRow = Row;
                            Pos.DoorColumn = Column;
                            break;
                        case 'E':
                            Pos.ExitRow = Row;
                            Pos.ExitColumn = Column;
                            break;
                    }
                }
                Console.WriteLine("\n");
            }

            char Movement = Console.ReadKey().KeyChar;

            ProcessMovement(Maze, Pos, Movement);
        }

        internal void ProcessMovement(char[,] Maze, Position player, char Movement)
        {
            Console.WriteLine($"Speler moet van {player.StartColumn}-{player.StartRow} eerst naar de key op {player.KeyColumn}-{player.KeyRow}," +
                $"om daarna naar de deur bij {player.DoorColumn}-{player.DoorRow} te komen, en dan naar de exit bij {player.ExitColumn}-{player.ExitRow} te gaan");
        }

        internal bool IsValidPosition(char[,] Maze, Position position)
        {
            return false;
        }
    }
}
