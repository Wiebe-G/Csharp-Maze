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
            }

            bool IsMoveValid = CheckCollision(OldPlayerX, OldPlayerY, Maze, Pos);
            if (!IsMoveValid)
            {
                Console.WriteLine($"Je staat op {Pos.PlayerX} - {Pos.PlayerY}, maar je mag niet verder. Ik zet je nu terug naar {OldPlayerX}-{OldPlayerY}");
                Pos.PlayerX = OldPlayerX;
                Pos.PlayerY = OldPlayerY;
                return false;
            }
            return true;
        }

        internal bool CheckCollision(int OldPlayerX, int OldPlayerY, char[,] Maze, Position Pos)
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