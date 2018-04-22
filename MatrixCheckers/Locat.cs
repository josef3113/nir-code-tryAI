using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
    public struct Locat
    {
        private byte m_x;
        private byte m_y;

        public Locat(byte x, byte y)
        {
            m_x = x;
            m_y = y;

        }        

        public byte X
        {
            get
            {
                return m_x;
            }
            set
            {
                m_x = value;
            }
        }

        public byte Y
        {
            get
            {
                return m_y;
            }
            set
            {
                m_y = value;
            }
        }

    }
}
