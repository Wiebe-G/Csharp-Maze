using System;
using System.IO;
using System.Linq;
using System.Runtime;

namespace Csharp_Doolhof
{
    internal class Game : Movement
    {
        Position Pos = new();
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
                        if (Maze[Row, Column].Equals('S'))
                        {
                            Pos.PlayerY = Row;
                            Pos.PlayerX = Column;
                        }
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
                    RenderGraphicsWithRtxEnabled(Maze, Row, Column);

                }
                Console.WriteLine("\n");
            }
            Console.ResetColor();
            ProcessMovement(Maze);
        }

        private void RenderGraphicsWithRtxEnabled(char[,] Maze, int Row, int Column)
        {
            char CurrentChar = Maze[Row, Column];
            if (Row == Pos.PlayerX && Column == Pos.PlayerY && CurrentChar != '#')
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write('P');
                Console.ResetColor();
            }
            else if (CurrentChar.Equals('.'))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(CurrentChar);
                Console.ResetColor();
            }
            else if (CurrentChar.Equals('#'))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(CurrentChar);
                Console.ResetColor();
            }
            else if (CurrentChar.Equals('K'))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(CurrentChar);
                Console.ResetColor();
            }
            else
            {
                Console.Write(CurrentChar);
            }
        }

        internal void ProcessMovement(char[,] Maze)
        {
            for (; ; )
            {
                if (Console.KeyAvailable)
                {
                    string Movement = Console.ReadKey(true).Key.ToString();
                    bool IsValid = IsMovementValid(Maze, Movement);

                    if (!IsValid)
                    {
                        continue;
                    }

                    if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('K'))
                    {
                        Maze[Pos.PlayerX, Pos.PlayerY] = '.';
                        Pos.HasKey = true;
                    }

                    if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('D') && !Pos.HasKey)
                    {
                        continue;
                    }

                    if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('D') && Pos.HasKey)
                    {
                        Maze[Pos.PlayerX, Pos.PlayerY] = '.';
                    }

                    if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('E'))
                    {
                        Console.WriteLine("YOU WIN!!!!! YIPPEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!");
                        Environment.Exit(0);
                    }

                    Console.Clear();
                    RenderMaze(Maze);
                }
            }
        }

        internal bool IsMovementValid(char[,] Maze, string Movement)
        {
            try
            {
                bool DoMovementOrSomething = HandleMovement(Maze, Pos, Movement);
                if (!DoMovementOrSomething)
                {
                    return false;
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }
    }
}
