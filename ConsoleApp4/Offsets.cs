using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    internal static class Offsets
    {
        public static int GetOffset(int coords, int sensitivity = 10000) 
        {
            int offset = (coords - 32767) / sensitivity;
            offset = offset * Math.Abs(offset);
            return offset;
        }
    }
}
