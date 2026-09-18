using System;

namespace Csharp_Doolhof
{
    internal class Movement
    {
        /*
         * Rijen zijn Maze.GetLength(0), kolommen zijn Maze.GetLength(1)
        */

        internal (bool flowControl, bool value) MoveUpwards(char[,] Maze, Position Pos)
        {
            // Naar boven bewegen, oftewel 1 rij omlaag
            Pos.PlayerY--;
            if (Maze[Pos.PlayerY, Pos.PlayerX].Equals('#') || Pos.PlayerY < 0)
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                Pos.PlayerX++;
                return (flowControl: false, value: false);
            }

            return (flowControl: true, value: default);
        }

        internal (bool flowControl, bool value) MoveDown(char[,] Maze, Position Pos)
        {
            // Naar beneden bewegen, oftewel 1 rij omhoog
            Pos.PlayerY++;
            if (Maze[Pos.PlayerY, Pos.PlayerX].Equals('#') || Pos.PlayerY > Maze.GetLength(0))
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                Pos.PlayerY--;
                return (flowControl: false, value: false);
            }

            return (flowControl: true, value: default);
        }

        internal (bool flowControl, bool value) MoveLeft(char[,] Maze, Position Pos)
        {
            // Naar links bewegen, oftewel 1 kolom minder
            Pos.PlayerX--;
            if (Maze[Pos.PlayerY, Pos.PlayerX].Equals('#') || Pos.PlayerX < 0)
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                Pos.PlayerX++;
                return (flowControl: false, value: false);
            }

            return (flowControl: true, value: default);
        }

        internal (bool flowControl, bool value) MoveRight(char[,] Maze, Position Pos)
        {
            // Naar rechts bewegen, oftewel 1 kolom meer
            Pos.PlayerX++;
            if (Maze[Pos.PlayerY, Pos.PlayerX].Equals('#') || Pos.PlayerX >= Maze.GetLength(1))
            {
                Console.WriteLine("Je probeert tegen een muur te lopen.");
                Pos.PlayerX--;
                return (flowControl: false, value: false);
            }
            return (flowControl: true, value: default);
        }
    }
}