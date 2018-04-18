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

        //private int Compare(Locat i_Other)
        //{
        //    int result = 0;
        //    if (this.Y > i_Other.Y)
        //    {
        //        result = 1;
        //    }
        //    return result;
        //}

    }
}
