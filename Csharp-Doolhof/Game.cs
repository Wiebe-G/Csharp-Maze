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
                    char CurrentChar = Maze[Row, Column];
                    if (Maze[Row, Column] == Maze[Pos.PlayerX, Pos.PlayerY])
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.Write(CurrentChar);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(CurrentChar);
                    }

                }
                Console.WriteLine("\n");

            }
            Console.ResetColor();
            ProcessMovement(Maze);
        }

        internal void ProcessMovement(char[,] Maze)
        {
            for (; ; )
            {
                if (Console.KeyAvailable)
                {
                    string Movement = Console.ReadKey(true).Key.ToString();
                    bool IsValid = IsValidPositionAndMovePlayer(Maze, Movement);

                    if (!IsValid)
                    {
                        continue;
                    }

                    if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('K'))
                    {
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
                        break;
                    }

                    Console.Clear();
                    RenderMaze(Maze);
                }
            }
        }

        internal bool IsValidPositionAndMovePlayer(char[,] Maze, string Movement)
        {
            try
            {
                switch (Movement)
                {
                    case "W":
                        (bool FlowControlUp, bool value) = MoveUpwards(Maze, Pos);
                        if (!FlowControlUp)
                        {
                            return value;
                        }
                        break;
                    case "A":
                        (bool FlowControlLeft, bool valueLeft) = MoveLeft(Maze, Pos);
                        if (!FlowControlLeft)
                        {
                            return valueLeft;
                        }
                        break;
                    case "S":
                        (bool FlowControlDown, bool valueDown) = MoveDown(Maze, Pos);
                        if (!FlowControlDown)
                        {
                            return valueDown;
                        }
                        break;
                    case "D":
                        (bool FlowControlRight, bool valueRight) = MoveRight(Maze, Pos);
                        if (!FlowControlRight)
                        {
                            return valueRight;
                        }
                        break;
                    default:
                        break;
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
