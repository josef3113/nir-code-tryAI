//14/04/2018  15:00
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

        CheckersLogic m_ActiveGame;
        BordToGame m_UiOfGame;
        byte m_Size;

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


            /*
            Ai comp;

            if (vsComp == true)
            {
                comp = new Ai();

            }
            else
            {

                P2 name2 = "Hello";
            }
            */

            string[] gameMoveLazy = { "Dc>Ed", "Ef>Fe", "Cb>Dc", "Fe>Gd", "Ed>Fe" }; // rember to erase one day 
            string[] gameForYosi = { "Hc>Gd", "Gd>He" };
            while (m_ActiveGame.GameOn() == true)
            {
                m_UiOfGame.PrintBoardGame();

                const bool player1 = true;

                Console.WriteLine("{0}Playing now -> {1}{0}", Environment.NewLine, m_ActiveGame.NowPlaying == player1 ? m_player1.Name : m_player2.Name);

                string moveInString;

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
                        //if(indexMoves > 0)
                        //{
                        //    moveInString = gameForYosi[indexMoves];
                        //    indexMoves++;
                        //}
                        moveInString = InputChecking();
                    }
                    else
                    {
                        if (m_player2.Name == "computer")
                        {
                            moveInString = matricxChekers.AiForDamka.TheBestMoveToDo(m_ActiveGame, !player1);
                        }
                        else
                        {
                            moveInString = InputChecking();
                        }
                    }

                }
                
                // string moveInString = Console.ReadLine(); // replace to method

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

                    }
                    
                }

            }

        }

        private void multiEatingByPlayer(string i_MoveInString)
        {
            byte indexX , indexY;

            CheckersLogic.charsToIndex(out indexX, i_MoveInString[3], out indexY, i_MoveInString[4]);

            while (m_ActiveGame.checkingBounderis(indexX, indexY))
            {

                m_UiOfGame.PrintBoardGame();
                Console.WriteLine("Eat Again , From GamePlay !!");
                i_MoveInString = InputChecking();
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

        }

        private string InputChecking()
        {
            string rightInput = null;

            // Console.WriteLine("Insert Move");
            string inputGameMove = Console.ReadLine();
            char capitalEnd = (char)((m_Size - 1) + 'A'), littleEnd = (char)((m_Size - 1) + 'a');
            if (inputGameMove.Length >= 5)
            {
                if (checkNotPassTheLimitChars(inputGameMove[0], inputGameMove[1], inputGameMove[3], inputGameMove[4]) && inputGameMove[2] == '>')
                {
                    rightInput = inputGameMove;
                }
            }
            else if (inputGameMove.Length == 0)
            {
                Console.WriteLine("wow there is nothing here.");
            }
            else if (char.ToUpper(inputGameMove[0]) == 'Q')
            {
                Console.WriteLine("You are sure you want to end the game ? if yes enter Q or q again.");
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
        }


    }
}
