using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_Doolhof
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SelectFile f = new SelectFile();
            f.UserInputForFileSelect();
        }
    }
}
