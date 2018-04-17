//14/04/2018 15:00
using System;
using System.Collections.Generic;
using System.Text;
using MatrixCheckers;

namespace matricxChekers
{
    static class AiForDamka   //   jjj
    {
       private const bool m_player1 = true;

        // need to built best move for player one 
        public static string TheBestMoveToDo(CheckersLogic i_TheGameNow , bool i_PlayerToCheck ) // true for player 1 and false for player 2
        {
            bool foundActiveToDo = false;
            string activTheBest = null ;
           // Locat yaad ;

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
            foundActiveToDo = thisPlayerCanToEat(i_TheGameNow, out activTheBest , i_PlayerToCheck );
            
            if (! foundActiveToDo)
            {

                foundActiveToDo = thisPlayerCanToMove(i_TheGameNow, out activTheBest , i_PlayerToCheck);
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


        public static string TheMoveToDoForMultiEating(CheckersLogic i_TheGameNow, string i_LastMove)
        {
            string activeToContinueEat = null;
            Locat forChech = new Locat((int)(i_LastMove[3]-'A'), (int)(i_LastMove[4]-'a'));
            Locat yaad;
            i_TheGameNow.CanToMultiEat(forChech, out yaad);
            activeToContinueEat = makeStringOfActive(forChech, yaad);


            return activeToContinueEat;
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

        private static bool thisPlayerCanToEat(CheckersLogic i_TheGameNow, out string o_ActiveToEat, bool i_PlayerToCheck )// true for player 1 and false for player 2
        {
            //o_ActiveToEat = "NonActive";
            o_ActiveToEat = null;
            bool playerCanToEat = false;
            List<Locat> listOfPlayerToCheck;
            Locat yaad;

            if (i_PlayerToCheck == m_player1)
            {
                listOfPlayerToCheck = i_TheGameNow.m_VellsOfPlayer1;
            }
            else
            {
                listOfPlayerToCheck = i_TheGameNow.m_VellsOfPlayer2;
            }
            
            foreach (var makor in listOfPlayerToCheck)
            {
                if (i_TheGameNow.CanToEat(makor, out yaad))
                {
                    o_ActiveToEat = makeStringOfActive(makor, yaad);
                    playerCanToEat = true;
                    break;
                }
            }

            return playerCanToEat;

        }


        // need to be bool 
        public static string Player1CanToMove(CheckersLogic i_TheGameNow /*, out string o_ActiveToMove*/)
        {
           string o_ActiveToMove = null; // o_ActiveToMove
            bool playerCanToMove = false;
            Locat yaad;
            foreach (var makor in i_TheGameNow.m_VellsOfPlayer1)
            {
                if (i_TheGameNow.Player1CanToMove(makor, out yaad))
                {
                    o_ActiveToMove = makeStringOfActive(makor, yaad);
                    playerCanToMove = true;
                    break;
                }
            }
            return o_ActiveToMove;
           // return playerCanToMove;

        }


        private static bool thisPlayerCanToMove(CheckersLogic i_TheGameNow, out string o_ActiveToMove, bool i_PlayerToCheck )// true for player 1 and false for player 2
        {
            //o_ActiveToMove = "NonActive";
            o_ActiveToMove = null;
            bool playerCanToMove = false;
            Locat yaad;
            List<Locat> listOfPlayerToCheck;
            if (i_PlayerToCheck == m_player1)
            {
                listOfPlayerToCheck = i_TheGameNow.m_VellsOfPlayer1;
            }
            else
            {
                listOfPlayerToCheck = i_TheGameNow.m_VellsOfPlayer2;
            }
            foreach (var makor in listOfPlayerToCheck)
            {
                if (i_TheGameNow.CanToMoveTry(makor, out yaad))
                {
                    o_ActiveToMove = makeStringOfActive(makor, yaad);
                    playerCanToMove = true;
                    break;
                }
            }

            return playerCanToMove;

        }




    }
}
