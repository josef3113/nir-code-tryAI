using System;
using System.Collections.Generic;
using System.Text;

namespace matricxChekerNotFork
{
    class AiForDamka   //   jjj
    {
        private List<string> m_VellsOfComputer = new List<string>();

        public void CreatVeesels()
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


        public string active()
        {
            string activToDo = "Non";
            
            foreach (var makor in m_VellsOfComputer)
            {
                if (CanToEat(makor, yaad))
                {
                    activToDo = activToDo = MakeStringOfActive(makor, yaad);
                    break;
                }
            }

            if(activToDo != "Non")
            {
                foreach (var makor in m_VellsOfComputer)
                {
                    if (CanMoveInSave(makor, yaad))
                    {
                        activToDo = activToDo = MakeStringOfActive(makor, yaad);
                        break;
                    }
                }

            }
            if (activToDo != "Non")
            {
                foreach (var makor in m_VellsOfComputer)
                {
                    if (CanMove(makor, yaad))// yaad is out parameter 
                    {
                        activToDo = MakeStringOfActive(makor, yaad);
                        break;
                    }
                }

            }
        }

        public MakeStringOfActive(byte[,] makor, byte[,] yaad)
        {
            StringBuilder active;
            active[0] = makor[1] + 'A';
            active[1] = makor[0] + 'a';
            active[2] = '>';
            active[3] = yaad[1] + 'A';
            active[4] = yaad[0] + 'a';

        }




    }
}
