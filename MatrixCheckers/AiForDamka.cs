//14/04/2018 15:00
using System.Text;
using MatrixCheckers;

namespace matricxChekers
{
    static class AiForDamka 
    {      
        public static string TheBestMoveToDoForPlayer2(CheckersLogic i_TheGameNow )
        {
            bool foundActiveToDo = false;
            string activeTheBestMove = null ;
           
            foundActiveToDo = Player2CanToEat(i_TheGameNow, out activeTheBestMove );
            
            if (! foundActiveToDo)
            {

                foundActiveToDo = Player2CanToMove(i_TheGameNow, out activeTheBestMove );
                
            }

            return activeTheBestMove;
        }

        public static string TheBestMoveToDoForPlayer1(CheckersLogic i_TheGameNow)
        {
            bool foundActiveToDo = false;
            string o_ActiveTheBestMove = null;

            foundActiveToDo = Player1CanToEat(i_TheGameNow, out o_ActiveTheBestMove);

            if (!foundActiveToDo)
            {

                foundActiveToDo = Player1CanToMove(i_TheGameNow, out o_ActiveTheBestMove);

            }

            return o_ActiveTheBestMove;
        }

        public static string TheMoveToDoForMultiEating(CheckersLogic i_TheGameNow, string i_LastMove)
        {
            string activeToContinueEat = null;
            Locat forChech = new Locat((byte)(i_LastMove[3]-'A'), (byte)(i_LastMove[4]-'a'));
            Locat yaad = new Locat();
            i_TheGameNow.CanToMultiEat(forChech, out yaad);
            activeToContinueEat = makeStringOfActive(forChech, yaad);


            return activeToContinueEat;
        }


        private static string makeStringOfActive(Locat i_Source, Locat i_Destination)
        {
            StringBuilder activeToReturn = new StringBuilder("Aa>Aa");
            activeToReturn[0] = (char)(i_Source.X + (byte)'A');
            activeToReturn[1] = (char)(i_Source.Y + (byte)'a');
            activeToReturn[2] = '>';
            activeToReturn[3] = (char)(i_Destination.X + (byte)'A');
            activeToReturn[4] = (char)(i_Destination.Y + (byte)'a');

            return activeToReturn.ToString();
        }

        private static bool Player2CanToEat(CheckersLogic i_TheGameNow, out string o_ActiveToEat )
        {
            
            o_ActiveToEat = null;
            bool playerCanToEat = false;
            Locat yaad = new Locat();

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
            
            o_ActiveToEat = null;
            bool playerCanToEat = false;
            Locat yaad = new Locat();


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
            
            o_ActiveToMove = null;
            bool playerCanToMove = false;
            Locat yaad = new Locat();

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
           
            o_ActiveToMove = null;
            bool playerCanToMove = false;
            Locat yaad = new Locat();
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


        


    }
}
