using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
    class Player
    {
        private string m_Name = "computer";
        private short m_Points = 0;

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (value.Contains(" ") || value.Length > 20)
                {
                    while ((value.Contains(" ") || value.Length > 20) == true)
                    {
                        Console.WriteLine("wrong name insert another name");
                        value = Console.ReadLine();
                    }


                }
                else
                {
                    m_Name = value;
                }
            }
        }
        public short Points
        {
            get
            {
                return m_Points;
            }
            set
            {
                m_Points = value;
            }
        }

    }
}
