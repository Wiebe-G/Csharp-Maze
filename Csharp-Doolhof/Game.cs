using System;
using System.IO;
using System.Linq;
using System.Runtime;

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
            Position Pos = new();
            RenderMaze(Maze, Pos);
        }

        internal void RenderMaze(char[,] Maze, Position Pos)
        {

            for (int Row = 0; Row < Maze.GetLength(0); Row++)
            {
                for (int Column = 0; Column < Maze.GetLength(1); Column++)
                {
                    if (Maze[Row, Column] != Maze[Pos.PlayerRow, Pos.PlayerColumn])
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write(Maze[Row, Column]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(Maze[Row, Column]);
                    }

                }
                Console.WriteLine("\n");
            }
            ProcessMovement(Maze, Pos);
        }

        internal void ProcessMovement(char[,] Maze, Position Pos)
        {

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.WriteLine($"{Pos.PlayerRow}-{Pos.PlayerColumn}");

                    string Movement = Console.ReadKey(true).Key.ToString();
                    bool IsValid = IsValidPosition(Maze, Pos, Movement);

                    if (!IsValid)
                    {
                        continue;
                    }
                }
            }
        }

        internal bool IsValidPosition(char[,] Maze, Position Pos, string Movement)
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

                Console.WriteLine($"Beweging input ontvangen: {Movement}");
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        private static (bool flowControl, bool value) MoveRight(char[,] Maze, Position Pos)
        {
            if (Maze[Pos.PlayerRow, Pos.PlayerRow + 1].Equals('#'))
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                return (flowControl: false, value: false);
            }
            Pos.PlayerColumn++;
            return (flowControl: true, value: default);
        }

        private static (bool flowControl, bool value) MoveDown(char[,] Maze, Position Pos)
        {
            if (Maze[Pos.PlayerRow - 1, Pos.PlayerColumn].Equals('#'))
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                return (flowControl: false, value: false);
            }
            Pos.PlayerRow--;
            return (flowControl: true, value: default);
        }

        private static (bool flowControl, bool value) MoveLeft(char[,] Maze, Position Pos)
        {
            if (Maze[Pos.PlayerRow, Pos.PlayerColumn - 1].Equals('#'))
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                return (flowControl: false, value: false);
            }
            Pos.PlayerColumn--;
            return (flowControl: true, value: default);
        }

        private static (bool flowControl, bool value) MoveUpwards(char[,] Maze, Position Pos)
        {
            if (Maze[Pos.PlayerRow + 1, Pos.PlayerColumn].Equals('#'))
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                return (flowControl: false, value: false);
            }
            Pos.PlayerRow++;
            return (flowControl: true, value: default);
        }
    }
}
