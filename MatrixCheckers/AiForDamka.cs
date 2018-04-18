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
        public static string TheBestMoveToDoForPlayer2(CheckersLogic i_TheGameNow ) // true for player 1 and false for player 2
        {
            bool foundActiveToDo = false;
            string activTheBest = null ;
           
            foundActiveToDo = Player2CanToEat(i_TheGameNow, out activTheBest );
            
            if (! foundActiveToDo)
            {

                foundActiveToDo = Player2CanToMove(i_TheGameNow, out activTheBest );
                
            }
            return activTheBest;
        }

        public static string TheBestMoveToDoForPlayer1(CheckersLogic i_TheGameNow) // true for player 1 and false for player 2
        {
            bool foundActiveToDo = false;
            string activTheBest = null;

            foundActiveToDo = Player1CanToEat(i_TheGameNow, out activTheBest);

            if (!foundActiveToDo)
            {

                foundActiveToDo = Player1CanToMove(i_TheGameNow, out activTheBest);

            }
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

        private static bool Player2CanToEat(CheckersLogic i_TheGameNow, out string o_ActiveToEat )
        {
            //o_ActiveToEat = "NonActive";
            o_ActiveToEat = null;
            bool playerCanToEat = false;
            Locat yaad;

            
            foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
            {
                if (i_TheGameNow.player2CanToEat(makor, out yaad))
                {
                    o_ActiveToEat = makeStringOfActive(makor, yaad);
                    playerCanToEat = true;
                    break;
                }
            }

            return playerCanToEat;

        }


        private static bool Player1CanToEat(CheckersLogic i_TheGameNow, out string o_ActiveToEat)
        {
            //o_ActiveToEat = "NonActive";
            o_ActiveToEat = null;
            bool playerCanToEat = false;
            Locat yaad;


            foreach (var makor in i_TheGameNow.m_VellsOfPlayer1)
            {
                if (i_TheGameNow.player1CanToEat(makor, out yaad))
                {
                    o_ActiveToEat = makeStringOfActive(makor, yaad);
                    playerCanToEat = true;
                    break;
                }
            }

            return playerCanToEat;

        }

        

        private static bool Player2CanToMove(CheckersLogic i_TheGameNow, out string o_ActiveToMove)
        {
            //o_ActiveToMove = "NonActive";
            o_ActiveToMove = null;
            bool playerCanToMove = false;
            Locat yaad;
            foreach (var makor in i_TheGameNow.m_VellsOfPlayer2)
            {
                if (i_TheGameNow.Player2CanToMove(makor, out yaad))
                {
                    o_ActiveToMove = makeStringOfActive(makor, yaad);
                    playerCanToMove = true;
                    break;
                }
            }

            return playerCanToMove;

        }

        private static bool Player1CanToMove(CheckersLogic i_TheGameNow, out string o_ActiveToMove)
        {
            //o_ActiveToMove = "NonActive";
            o_ActiveToMove = null;
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

            return playerCanToMove;

        }


        //public static string player1CanToMOveTry(CheckersLogic i_TheGameNow /*, out string o_ActiveToMove*/)
        //{
        //    string o_ActiveToMove = null; // o_ActiveToMove
        //    bool playerCanToMove = false;
        //    Locat yaad;
        //    foreach (var makor in i_TheGameNow.m_VellsOfPlayer1)
        //    {
        //        if (i_TheGameNow.Player1CanToMove(makor, out yaad))
        //        {
        //            o_ActiveToMove = makeStringOfActive(makor, yaad);
        //            playerCanToMove = true;
        //            break;
        //        }
        //    }
        //    return o_ActiveToMove;
        //    // return playerCanToMove;

        //}


    }
}
