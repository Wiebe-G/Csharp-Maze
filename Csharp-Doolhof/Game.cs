using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Csharp_Doolhof
{
    internal class Game : Movement
    {
        readonly Position Pos = new();
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
                            Pos.PlayerX = Row;
                            Pos.PlayerY = Column;
                        }
                    }
                    Row++;
                }
            }

            Console.WriteLine("Bestand correct ingeladen");

            RenderMaze(Maze);

            //ProcessMovement(Maze);
        }

        internal void RenderMaze(char[,] Maze)
        {
            Console.SetCursorPosition(0, 0);
            for (int Row = 0; Row < Maze.GetLength(0); Row++)
            {
                for (int Column = 0; Column < Maze.GetLength(1); Column++)
                {
                    RenderGraphicsWithRtxEnabled(Maze, Row, Column);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine("Beweeg met W,A,S,D, en ga terug naar het menu met P");
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
                //sb.Append('P');
                Console.ResetColor();
            }
            else if (CurrentChar.Equals('.'))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(CurrentChar);
                //sb.Append(CurrentChar);
                Console.ResetColor();
            }
            else if (CurrentChar.Equals('#'))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(CurrentChar);
                //sb.Append(CurrentChar);
                Console.ResetColor();
            }
            else if (CurrentChar.Equals('K'))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(CurrentChar);
                //sb.Append(CurrentChar);
                Console.ResetColor();
            }
            else
            {
                Console.Write(CurrentChar);
                //sb.AppendLine(CurrentChar.ToString());
            }
            //return sb;
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
                        return;
                    }

                    if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('D') && Pos.HasKey)
                    {
                        Maze[Pos.PlayerX, Pos.PlayerY] = '.';
                    }

                    if (Maze[Pos.PlayerX, Pos.PlayerY].Equals('E'))
                    {
                        WinScreen();
                    }

                    //Console.Clear();
                    RenderMaze(Maze);
                }
            }
        }

        internal bool IsMovementValid(char[,] Maze, string Movement)
        {
            try
            {
                if (!HandleMovement(Maze, Pos, Movement))
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

        private void WinScreen()
        {
            Console.WriteLine("YOU WIN!!!!! YIPPEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!");
            Console.WriteLine("Wilt u opnieuw spelen? [Y]es/[N]o");
            string Choice = Console.ReadLine().ToUpper();
            switch (Choice)
            {
                case string when Choice.StartsWith("Y"):
                    Console.WriteLine("Ok");
                    SelectAndValidateFile s = new();
                    Thread.Sleep(2000);
                    Console.Clear();
                    s.UserInputForFileSelect();
                    break;
                case string when Choice.StartsWith("N"):
                    Console.WriteLine("Ok");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Ongeldige keuze. Dat is dan waarschijnlijk een nee");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
