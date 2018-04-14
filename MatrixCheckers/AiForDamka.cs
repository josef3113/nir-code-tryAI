//14/04/2018 15:00
using System;
using System.Collections.Generic;
using System.Text;
using MatrixCheckers;

namespace matricxChekers
{
    class AiForDamka   //   jjj
    {
       const bool player1 = true;
        public static string TheBestMoveToDo(CheckersLogic i_TheGameNow , bool i_PlayerToCheck = !player1 )
        {
            bool foundActiveToDo = false;
            string activTheBest = null ;
            Locat yaad ;

            //foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
            //{
            //    if (i_TheGameNow.CanToEat( makor,out yaad))
            //    {
            //        activTheBest = makeStringOfActive(makor, yaad);
            //        break;
            //    }
            //}
            //if(computerCanToEat(i_TheGameNow,out activTheBest))
            //{
            //    foundActiveToDo = true;
            //}
            foundActiveToDo = computerCanToEat(i_TheGameNow, out activTheBest );
            
            if (! foundActiveToDo)
            {

                foundActiveToDo = computerCanToMove(i_TheGameNow, out activTheBest);
                //foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
                //{
                //    if (i_TheGameNow.CanToMove(makor, out yaad))
                //    {
                //        activTheBest = makeStringOfActive(makor, yaad);
                //        break;
                //    }
                //}
            }



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

            return activTheBest;
        }

        private static string makeStringOfActive(Locat i_makor, Locat i_yaad)
        {
            StringBuilder activeToReturn = new StringBuilder("Aa>Aa");
            activeToReturn[0] = (char)(i_makor.X + (byte)'A');
            activeToReturn[1] = (char)(i_makor.Y + (byte)'a');
            activeToReturn[2] = '>';
            activeToReturn[3] = (char)(i_yaad.X + (byte)'A');
            activeToReturn[4] = (char)(i_yaad.Y + (byte)'a');

            return activeToReturn.ToString();
        }

        private static bool computerCanToEat(CheckersLogic i_TheGameNow, out string o_ActiveToEat, bool i_PlayerToCheck = !player1)
        {
            o_ActiveToEat = "NonActive";
            bool computerCanToEat = false;
            Locat yaad;
            foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
            {
                if (i_TheGameNow.CanToEat(makor, out yaad))
                {
                    o_ActiveToEat = makeStringOfActive(makor, yaad);
                    computerCanToEat = true;
                    break;
                }
            }

            return computerCanToEat;

        }

        private static bool computerCanToMove(CheckersLogic i_TheGameNow, out string o_ActiveToMove, bool i_PlayerToCheck = !player1)
        {
            o_ActiveToMove = "NonActive";
            bool computerCanToMove = false;
            Locat yaad;
            foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
            {
                if (i_TheGameNow.CanToMove(makor, out yaad))
                {
                    o_ActiveToMove = makeStringOfActive(makor, yaad);
                    computerCanToMove = true;
                    break;
                }
            }

            return computerCanToMove;

        }




    }
}
