//13/04/2018 20:20
using System;
using System.Collections.Generic;
using System.Text;
using MatrixCheckers;

namespace matricxChekers
{
    class AiForDamka   //   jjj
    {
        
        public static string TheBestMoveToDo(CheckersLogic i_TheGameNow)
        {
            string activTheBest = "Non";
            Locat yaad ;

            foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
            {
                if (i_TheGameNow.CanToEat( makor,out yaad))
                {
                    activTheBest = MakeStringOfActive(makor, yaad);
                    break;
                }
            }
            
            if (activTheBest == "Non")
            {
                foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
                {
                    if (i_TheGameNow.CanToMove(makor, out yaad))
                    {
                        activTheBest = MakeStringOfActive(makor, yaad);
                        break;
                    }
                }

            }

            return activTheBest;

            //if (activToDo != "Non")
            //{
            //    foreach (var makor in i_VellsOfComputer)
            //    {
            //        if (CanMove(makor, yaad))// yaad is out parameter 
            //        {
            //            activToDo = MakeStringOfActive(makor, yaad);
            //            break;
            //        }
            //    }

            //}
        }

        private static string MakeStringOfActive(Locat i_makor, Locat i_yaad)
        {
            StringBuilder activeToReturn = new StringBuilder("Aa>Aa");
            activeToReturn[0] = (char)(i_makor.X + (byte)'A');
            activeToReturn[1] = (char)(i_makor.Y + (byte)'a');
            activeToReturn[2] = '>';
            activeToReturn[3] = (char)(i_yaad.X + (byte)'A');
            activeToReturn[4] = (char)(i_yaad.Y + (byte)'a');

            return activeToReturn.ToString();
        }




    }
}
