using System;

namespace Csharp_Doolhof
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Beste doolhof OAT";
            SelectAndValidateFile f = new();
            f.UserInputForFileSelect();
        }
    }
}
