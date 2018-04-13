﻿using System;
using System.Collections.Generic;
using System.Text;
using MatrixCheckers;

namespace MatrixCheckers
{
    class GamePlay
    {
        matricxChekers.AiForDamka smarter = new matricxChekers.AiForDamka();
        CheckersLogic m_ActiveGame;
        BordToGame m_UiOfGame;
        byte m_Size;

        public GamePlay(byte i_Size = 8)
        {
            m_Size = i_Size;
            m_ActiveGame = new CheckersLogic(i_Size);
            m_UiOfGame = new BordToGame(i_Size);
        }

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

            string[] gameMoveLazy = { "Bc>Cd", "Af>Be", "Cd>Af" }; // rember to erase one day 

            while (m_ActiveGame.GameOn() == true)
            {
                m_UiOfGame.PrintBoardGame();

                const bool player1 = true;
                
                Console.WriteLine("{0}Playing now -> {1}{0}", Environment.NewLine, m_ActiveGame.NowPlaying == player1 ? "Player1" : "Player2");

                string moveInString;

                // yosi start 

                bool itsVScomputer = true;

                //if (indexMoves < gameMoveLazy.Length)
                //{

                //    // some legal move/eat of the computer. return .
                //    moveInString = gameMoveLazy[indexMoves];
                //    // the move done.
                //    indexMoves++;
                //}
                //else
                //{
                //    // moveInString = Console.ReadLine();
                //    moveInString = InputChecking();
                //}

                if (m_ActiveGame.NowPlaying == player1)
                {
                    moveInString = InputChecking();
                }
                else
                {
                    if (itsVScomputer)
                    {
                        moveInString = smarter.TheBestMoveToDo(m_ActiveGame);
                    }
                    else
                    {
                        moveInString = InputChecking();
                    }
                }

                // yosi end

                //if (Comp == false)
                //{
                //    moveInString = InputChecking();
                //}
                //else {

                //    moveInString =  Comp.BestMove();
                //}



                // string moveInString = Console.ReadLine(); // replace to method

                if (moveInString != null)
                {

                    m_ActiveGame.PlayingVessel(moveInString);

                    if (m_ActiveGame.IsTurnPass)
                    {

                        /*
                        if (m_ActiveGame.IsEated == true)
                        {


                        }
                        */
                        // moveInBoard

                        moveInBoard(moveInString);
                        m_ActiveGame.ChangePlayer();

                    }


                    //  m_UiOfGame.PrintBoardGame();

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