using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
    public struct Locat
    {
        public Locat(int x, int y)
        {
            this.X = (byte)x;
            this.Y = (byte)y;

        }
        public byte X { get; set; }
        public byte Y { get; set; }

    }
}
