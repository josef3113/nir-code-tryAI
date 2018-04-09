using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
    class AiForDamka   //   jjj
    {
        private List<string> m_VellsOfComputer = new List<string>();

        public void creatActivity()
        {
            m_VellsOfComputer.Add("af");
            m_VellsOfComputer.Add("bE");



        }

        public void ChanghVeessl(string i_CurrentVessel, string  i_NewVessel)
        {
            m_VellsOfComputer.Remove(i_CurrentVessel);
            m_VellsOfComputer.Add(i_NewVessel);
        }
        public void PrintVeelssOfComputer()
        {
            foreach (var item in m_VellsOfComputer)
            {
                Console.WriteLine(item);

            }
        }







    }
}
