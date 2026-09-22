using System;

namespace Csharp_Doolhof
{
    internal class Movement
    {

        internal bool HandleMovement(char[,] Maze, Position Pos, string Movement)
        {
            int OldPlayerX = Pos.PlayerX;
            int OldPlayerY = Pos.PlayerY;
            switch (Movement)
            {
                case "W":
                    Pos.PlayerX--;
                    break;
                case "A":
                    Pos.PlayerY--;
                    break;
                case "S":
                    Pos.PlayerX++;
                    break;
                case "D":
                    Pos.PlayerY++;
                    break;
                case "P":
                    Console.Clear();
                    SelectAndValidateFile s = new();
                    s.UserInputForFileSelect();
                    break;
                default:
                    break;
            }

            if (!CheckCollision(Maze, Pos))
            {
                Pos.PlayerX = OldPlayerX;
                Pos.PlayerY = OldPlayerY;
                return false;
            }

            if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('K'))
            {
                Maze[Pos.PlayerX, Pos.PlayerY] = '.';
                Pos.HasKey = true;
            }

            if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('D') && !Pos.HasKey)
            {
                return false;
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
            return true;
        }

        internal bool CheckCollision(char[,] Maze, Position Pos)
        {
            if (
                Maze[Pos.PlayerX, Pos.PlayerY].Equals('#') ||
                Maze[Pos.PlayerX, Pos.PlayerY].Equals('D') && !Pos.HasKey ||
                Pos.PlayerY < 0 ||
                Pos.PlayerX < 0 ||
                Pos.PlayerY > Maze.GetLength(1) ||
                Pos.PlayerX > Maze.GetLength(0))
            {
                return false;
            }

            return true;
        }
    }
}