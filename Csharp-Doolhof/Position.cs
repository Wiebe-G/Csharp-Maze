using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csharp_Doolhof
{
    internal class Position
    {
        internal int StartRow { get; set; }
        internal int StartColumn { get; set; }

        internal int KeyRow { get; set; }
        internal int KeyColumn { get; set; }
        internal int DoorRow { get; set; }
        internal int DoorColumn { get; set; }
        internal int ExitRow { get; set; }
        internal int ExitColumn { get; set; }
        internal bool HasKey { get; set; } = false;
    }
}
