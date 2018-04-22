//15/04/2018  16:00
using System;
using System.Collections.Generic;
using System.Text;
using MatrixCheckers;

namespace MatrixCheckers
{
    class GamePlay
    {
        //yosi start
        Player m_player1;
        Player m_player2;
        // yosiend
        const bool player1 = true;
        CheckersLogic m_ActiveGame;
        BordToGame m_UiOfGame;
        byte m_Size;
        bool m_WantToPlay = true;

        public GamePlay(byte i_Size = 8)
        {
            //yosi start 
            m_player1 = new Player();
            m_player2 = new Player();
            m_player1.Name = "avi";
            //yosi end
            m_Size = i_Size;
            m_ActiveGame = new CheckersLogic(i_Size);
            m_UiOfGame = new BordToGame(i_Size);

        }
        // yosi start
        public GamePlay(Player i_player1, Player i_player2, byte i_Size = 8)
        {
            m_player1 = i_player1;
            m_player2 = i_player2;


            m_Size = i_Size;
            m_ActiveGame = new CheckersLogic(i_Size);
            m_UiOfGame = new BordToGame(i_Size);

        }

        // yosi end

        public void StartGameToPlay()
        {
            byte indexMoves = 0; // // rember to erase one day 
            string[] gameMoveLazy = { "Dc>Ed", "Ef>Fe", "Cb>Dc", "Fe>Gd"/*,
                    "Fc>He","Gf>Fe","Ed>Gf","Hg>Fe","He>Gf","Fg>Ef","Gf>Hg" ,"Gh>Fg"*/ }; // rember to erase one day 
           
            string moveInString = null;
            string lastMove = null;

            while (m_ActiveGame.GameOn() == true && m_WantToPlay == true)
            {
                m_UiOfGame.PrintBoardGame();

                
                if(lastMove != null)
                {
                    Console.WriteLine("{2} move was {1}{0}", Environment.NewLine,lastMove, m_ActiveGame.NowPlaying == player1  ? m_player2.Name+" (x)": m_player1.Name+" (o)");

                }
                Console.WriteLine("{0}Playing now -> {1}{0}", Environment.NewLine, m_ActiveGame.NowPlaying == player1 ?  m_player1.Name + "(o)": m_player2.Name + "(x)" );

                

                // yosi start 

                if (indexMoves < gameMoveLazy.Length)
                {

                    // some legal move/eat of the computer. return .
                    moveInString = gameMoveLazy[indexMoves];
                    // the move done.
                    indexMoves++;
                }
                else
                {
                    if (m_ActiveGame.NowPlaying == player1)
                    {
                        // replace that
                       // moveInString = matricxChekers.AiForDamka.TheBestMoveToDoForPlayer1(m_ActiveGame);
                        moveInString = InputChecking();
                    }
                    else
                    {
                        if (m_player2.Name == null)
                        {
                            moveInString = matricxChekers.AiForDamka.TheBestMoveToDoForPlayer2(m_ActiveGame);
                        }
                        else
                        {
                            //replace that
                           // moveInString = matricxChekers.AiForDamka.TheBestMoveToDoForPlayer2(m_ActiveGame);
                             moveInString = InputChecking();
                        }
                    }
                }

                
                if (moveInString != null)
                {

                    m_ActiveGame.PlayingVessel(moveInString);
                    
                    if (m_ActiveGame.IsTurnPass)  
                    {
                        moveInBoard(moveInString);
                        if (m_ActiveGame.IsEated) 
                        {
                            multiEatingByPlayer(moveInString); 
                        }

                        
                        m_ActiveGame.ChangePlayer();
                        lastMove = moveInString;

                    }
                 
                }

            }

            if(m_ActiveGame.GameOn() == false)
            {
                gameOver();
            }
        }

        private void gameOver(byte i_ResonOfExit = (byte) 0 )
        {
            if (i_ResonOfExit == (byte)1)  // type 1 is when player choich Q
            {
                if (m_ActiveGame.NowPlaying == player1)
                {
                    Console.WriteLine("Player 1 Retired so he loser");
                }
                else
                {
                    Console.WriteLine("Player 2 Retired so he loser");

                }
            }
            m_player1.Points += sumOfPointsInList(m_ActiveGame.m_VellsOfPlayer1);
            m_player2.Points += sumOfPointsInList(m_ActiveGame.m_VellsOfPlayer2);

            //
            Console.WriteLine("points of Player 1 is:{0}{1}points of Player 2 is:{2}{1} ",m_player1.Points,Environment.NewLine,m_player2.Points);

            byte choicToAnoterGame;
            Console.WriteLine("if you want play again insert 1 other number smaller 255 if you want to stop play");
            while (byte.TryParse(Console.ReadLine(), out choicToAnoterGame) == false)
            {
                Console.WriteLine("is wrong input, try again");
            }

            if(choicToAnoterGame == (byte) 1)
            {
                m_ActiveGame.resetGame();
                m_UiOfGame.ResetBoardOfGame();
                m_ActiveGame.PrintBoard();

               
            }
            else
            {
                m_WantToPlay = false;
            }


        }

        private byte sumOfPointsInList(List<Locat> i_listToSum)
        {
            byte sumOfPoints = (byte)0;
            foreach (var item in i_listToSum)
            {
                // char yy =(char) (stam.X + (byte)'A');
                char checkCurrntLocat = m_UiOfGame[ (char)(item.Y + (byte)'a'), (char)(item.X + (byte)'A')];
                if ( checkCurrntLocat == 'U' || checkCurrntLocat == 'K')
                {
                    sumOfPoints += (byte)4;
                }
                else
                {
                    sumOfPoints += (byte)1;
                }

            }

            return sumOfPoints;
        }
        private void multiEatingByPlayer(string i_MoveInString)
        {
            byte indexX , indexY;


            CheckersLogic.charsToIndex(out indexX, i_MoveInString[3], out indexY, i_MoveInString[4]);

            while (m_ActiveGame.checkingBounderis(indexX, indexY))
            {

                m_UiOfGame.PrintBoardGame();
                Console.WriteLine("Eat Again , From GamePlay !!");
                
                if (m_ActiveGame.NowPlaying == player1)
                {
                    // replace that
                     i_MoveInString = InputChecking();
                   // i_MoveInString = matricxChekers.AiForDamka.TheMoveToDoForMultiEating(m_ActiveGame, i_MoveInString);
                }
                else
                {
                    if (m_player2.Name == null)
                    {
                        i_MoveInString = matricxChekers.AiForDamka.TheMoveToDoForMultiEating(m_ActiveGame, i_MoveInString);
                    }
                    else
                    {
                        // replace that
                        i_MoveInString = InputChecking();
                      //  i_MoveInString = matricxChekers.AiForDamka.TheMoveToDoForMultiEating(m_ActiveGame, i_MoveInString);
                    }

                }

                if (i_MoveInString != null)
                {
                    byte inputIndex2X, inputIndex2Y, inputIndex1X, inputIndex1Y;
                    CheckersLogic.charsToIndex(out inputIndex2X, i_MoveInString[3], out inputIndex2Y, i_MoveInString[4]);
                    CheckersLogic.charsToIndex(out inputIndex1X, i_MoveInString[0], out inputIndex1Y, i_MoveInString[1]);

                    if (indexX == inputIndex1X && indexY == inputIndex1Y)
                    {
                        Console.WriteLine("Hey im here , the same as befor is playing");
                        m_ActiveGame.eatWithSameSoilder(inputIndex1X, inputIndex1Y, inputIndex2X, inputIndex2Y);
                        Console.WriteLine("answer if it eated the soidler ===> {0}", m_ActiveGame.IsEated);

                        if (m_ActiveGame.IsEated)
                        {
                            moveInBoard(i_MoveInString);

                            indexX = inputIndex2X;
                            indexY = inputIndex2Y;
                        }
                    }

                    //    m_ActiveGame
                }
            }
            // check it
            if (m_ActiveGame.GameOn() == false)
            {
                m_UiOfGame.PrintBoardGame();
            }
           

        }

        private string InputChecking()
        {
            string rightInput = null;

            // Console.WriteLine("Insert Move");
            string inputGameMove = Console.ReadLine();
            char capitalEnd = (char)((m_Size - 1) + 'A'), littleEnd = (char)((m_Size - 1) + 'a');
            if (inputGameMove.Length == 5)
            {
                if (checkNotPassTheLimitChars(inputGameMove[0], inputGameMove[1], inputGameMove[3], inputGameMove[4]) && inputGameMove[2] == '>')
                {
                    rightInput = inputGameMove;
                }
                else
                {
                    Console.WriteLine("input not legal");
                }
            }
            else if (inputGameMove.Length == 0)
            {
                Console.WriteLine("wow there is nothing here.");
            }
            else if (char.ToUpper(inputGameMove[0]) == 'Q')
            {
                // yosi todo 
                // Console.WriteLine("You are sure you want to end the game ? if yes enter Q or q again.");
                gameOver(1);
                
            }      
               
            return rightInput;
        }

        private bool checkNotPassTheLimitChars(char i_CapitalLetterA, char i_LittleLetterA, char i_CapitalLetterB, char i_LittleLetterB)
        {
            char capitalEnd = (char)((m_Size - 1) + 'A'), littleEnd = (char)((m_Size - 1) + 'a');
            bool capitalLetters = (i_CapitalLetterA >= 'A' && i_CapitalLetterA <= capitalEnd) && (i_CapitalLetterB >= 'A' && i_CapitalLetterB <= capitalEnd);
            bool littleLetters = (i_LittleLetterA >= 'A' && i_LittleLetterA <= littleEnd) && (i_LittleLetterB >= 'A' && i_LittleLetterB <= littleEnd);
            return capitalLetters && littleLetters;
        }

        private void moveInBoard(string i_MoveInString)
        {
            const char emptyPlace = ' '; // check to naming 
            char yaadX = i_MoveInString[3], yaadY = i_MoveInString[4], makorX = i_MoveInString[0], makorY = i_MoveInString[1];

            m_UiOfGame[yaadY, yaadX] = m_UiOfGame[makorY, makorX];
            m_UiOfGame[makorY, makorX] = emptyPlace;

            if (m_ActiveGame.IsEated)
            {
                char MiddleX = (char)((yaadX + makorX) / 2), MiddleY = (char)((yaadY + makorY) / 2);
                m_UiOfGame[MiddleY, MiddleX] = emptyPlace;

            }

            byte indexDestX , indexDestY ;

            CheckersLogic.charsToIndex(out indexDestX, yaadX, out indexDestY, yaadY);

            if (m_ActiveGame.checkIfBecomeKing(indexDestX, indexDestY))
            {
                const char xKing = 'K', oKing = 'U';
                m_UiOfGame[yaadY, yaadX] = indexDestY == 0 ? xKing : oKing;
            }
        }


    }
}
