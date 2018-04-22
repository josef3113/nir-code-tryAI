//14/04/2018  15:00
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
    class Player
    {
        private string m_Name ;
        private short m_Points = 0;

        public Player(string i_NameOfPlayer = null)
        {
            if (i_NameOfPlayer != null)
            {
                Name = i_NameOfPlayer;
            }
           
                
        }
        
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
                    m_Name = value;


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
