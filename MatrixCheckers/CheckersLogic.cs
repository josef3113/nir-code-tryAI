﻿//14/04/2018  15:00

using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
    //yosi start
    
    //yosi end
    class CheckersLogic
    {

        private byte[,] m_Mat;
        private byte m_Size;
        private bool m_GameOn;
        private const bool k_Player1 = true;
        //short playerOnePoints;
        //short playerTwoPoints; // to add when king and less when eating .

        public bool NowPlaying { get; private set; } = k_Player1; // it is changed . Look out.
        public bool IsTurnPass { get; private set; } = false; // isTurnPass
        public bool IsEated { get; private set; } = false;
        //yosi start
        public List<Locat> m_VellsOfPlayer1 = new List<Locat>();
        //its public for Ai
        public List<Locat> m_VellsOfPlayer2 = new List<Locat>();

        public void ChanghVeesslInList(List<Locat> i_VellsOfPlayer, Locat i_CurrentVessel, Locat i_NewVessel)
        {
            i_VellsOfPlayer.Remove(i_CurrentVessel);
            i_VellsOfPlayer.Add(i_NewVessel);
        }
        private void sortListOfVesselBecomingFirst(List<Locat> i_ListToSort)
        {
            i_ListToSort.Sort(delegate (Locat locat1, Locat locat2)
            {
                if (locat1.Y == locat2.Y) return 0;
                else if (locat1.Y < locat2.Y) return -1;
                else if (locat1.Y > locat2.Y) return 1;
                else return locat1.Y.CompareTo(locat2.Y);
            });

        }

        public void PrintVeelssInList(List<Locat> i_VellsOfPlayer)
        {
            foreach (var item in i_VellsOfPlayer)
            {
                Console.WriteLine("x is:{0} y is:{1}", item.X, item.Y);

            }
        }
        //yosi end

        public CheckersLogic(byte i_Size = 8)
        {
            m_Size = i_Size;
            m_Mat = new byte[i_Size, i_Size];
            resetGame();
        }

        public void resetGame()
        {

            m_GameOn = true;

            NowPlaying = k_Player1;

            //yosi start
            


            Locat currentVeesel = new Locat();
            m_VellsOfPlayer1.Clear();
            m_VellsOfPlayer2.Clear();
            //yosi end

            //playerOnePoints = playerTwoPoints = (short)(((m_Size - 2) / 2) * (m_Size / 2));

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if (i < (m_Size / 2 - 1) && (i + j) % 2 != 0)
                    {
                        m_Mat[i, j] = (byte)eCheckers.CheckerO;
                        // yossi move vessel
                        // yosi start
                        currentVeesel.X = (byte)j;
                        currentVeesel.Y = (byte)i;
                        m_VellsOfPlayer1.Add(currentVeesel);
                        // yosi end
                        // list1
                    }
                    
                    else if (i >= (m_Size / 2 + 1) && (i + j) % 2 != 0)
                    {
                        m_Mat[i, j] = (byte)eCheckers.CheckerX;
                        // yossi move vessel
                        // yossi start
                        currentVeesel.X = (byte)j;
                        currentVeesel.Y = (byte)i;
                        m_VellsOfPlayer2.Add(currentVeesel);
                        //yosi end 
                    }
                    else
                    {
                        m_Mat[i, j] = (byte)eCheckers.Non;
                    }
                }
            }
        }

        public void PrintBoard()
        {
            Console.WriteLine();
            Console.Write(" ");
            for (int i = 0; i < m_Size; i++)
            {
                Console.Write(" {0}", (char)('A' + i));
            }
            Console.WriteLine();
            for (int i = 0; i < m_Size; i++)
            {
                Console.Write("{0} ", (char)('a' + i));
                for (int j = 0; j < m_Size; j++)
                {
                    Console.Write("{0} ", m_Mat[i, j]);
                }

                Console.WriteLine();
            }

            StringBuilder buttomLines = new StringBuilder(m_Size * 2 + 20);

            buttomLines.Append('=', m_Size + 2);
            buttomLines.AppendFormat("{0}Playing now -> {1}{0}", Environment.NewLine, NowPlaying == true ? '1' : '2');
            buttomLines.Append('-', m_Size + 2);
            if (NowPlaying == k_Player1)
            {
                Console.WriteLine("list of player 1 is:");
                PrintVeelssInList(m_VellsOfPlayer1);
            }
            else
            {
                Console.WriteLine("list of player 2 is:");
                PrintVeelssInList(m_VellsOfPlayer2);
            }

            Console.WriteLine(buttomLines);
        }

        public void ChangePlayer()
        {
            NowPlaying = NowPlaying == k_Player1 ? !k_Player1 : k_Player1;
        }

        public bool PlayingVessel(string i_MovePos) // maybe change to PlayingTurn .
        {
            // here add if  to cover all the method for right or wrong input ! .!!

            IsEated = false;

            //if (IsTurnPass == true)
            //{
            //    ChangePlayer(); // change Player
            //}

            IsTurnPass = false;

            byte vesselOneX, vesselOneY, vesselTwoX, vesselTwoY;
            charsToIndex(out vesselOneX, i_MovePos[0], out vesselOneY, i_MovePos[1]);
            charsToIndex(out vesselTwoX, i_MovePos[3], out vesselTwoY, i_MovePos[4]);

            eCheckers checkers = (eCheckers)m_Mat[vesselOneY, vesselOneX];
            eCheckers soilderToPlay = soilderKind();

            // m_NowPlaying == 1 ? eCheckers.CheckerO | eCheckers.CheckerU : eCheckers.CheckerX | eCheckers.CheckerK;

            if ((checkers & soilderToPlay) == checkers && checkers != eCheckers.Non)
            {
                choisesToPlay(checkers, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else
            {
                Console.WriteLine("Not Your Vessel . try again.");
            }

            // return isTurnPass;

            return IsTurnPass;

        }

        void choisesToPlay(eCheckers soilderPlay, byte vesselOneX, byte vesselOneY, byte vesselTwoX, byte vesselTwoY)
        {
            // bool isPlayed = false;

            switch (soilderPlay) // the vessel that going to play.
            {
                case eCheckers.CheckerO:
                case eCheckers.CheckerX:
                    playRegularVesselAndCheckMoveDirection(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                    break;
                case eCheckers.CheckerU:
                case eCheckers.CheckerK:
                    eatOrMoveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
                    break;
            }

            //return isPlayed;
        }

        void playRegularVesselAndCheckMoveDirection(byte vesselOneX, byte vesselOneY, byte vesselTwoX, byte vesselTwoY)
        {
            if (isMoveFront(vesselOneY, vesselTwoY) == true)
            {
                eatOrMoveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else
            {
                Console.WriteLine("Cant go back . play again.");
            }
        }

        private void eatOrMoveVessel(byte vesselOneX, byte vesselOneY, byte vesselTwoX, byte vesselTwoY)
        {


            const byte oneStepMoveInCross = 1, twoStepsMoveInCross = 2;

            if (checkMoveInCross(oneStepMoveInCross, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY))
            {
                moveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else if (checkMoveInCross(twoStepsMoveInCross, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY))
            {
                eatEnemyVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else
            {
                Console.WriteLine("illegal move , you can only move in cross one step or eat in cross . try again.");
            }

        }

        public bool checkIfBecomeKing(byte i_LineX, byte i_LineY)
        {
            bool becomeKing = true;
            if (i_LineY == 0 && m_Mat[i_LineY, i_LineX] == (byte)eCheckers.CheckerX)
            {
                m_Mat[i_LineY, i_LineX] = (byte)eCheckers.CheckerK;

            }
            else if (i_LineY == (m_Size - 1) && m_Mat[i_LineY, i_LineX] == (byte)eCheckers.CheckerO)
            {

                m_Mat[i_LineY, i_LineX] = (byte)eCheckers.CheckerU;

            }
            else
            {
                becomeKing = false;
            }

            return becomeKing;
        }

        private bool moveVessel(byte i_IndexOfVesselOneX, byte i_IndexOfVesselOneY, byte i_IndexOfVesselTwoX, byte i_IndexOfVesselTwoY)
        {
            bool isMoved = false;

            if (m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] == (byte)eCheckers.Non)
            {
                ExuteMove(i_IndexOfVesselOneX, i_IndexOfVesselOneY, i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);

                IsTurnPass = true;
                // yossi move vessel
                //yosi start
                Locat original = new Locat(i_IndexOfVesselOneX, i_IndexOfVesselOneY);
                Locat newVessel = new Locat(i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);
                if (NowPlaying == k_Player1)
                {
                    ChanghVeesslInList(m_VellsOfPlayer1, original, newVessel);
                    //yosi check
                    sortListOfVesselBecomingFirst(m_VellsOfPlayer1);
                    // yosi end check
                }
                else
                {
                    ChanghVeesslInList(m_VellsOfPlayer2, original, newVessel);
                    // yosi check
                    sortListOfVesselBecomingFirst(m_VellsOfPlayer2);
                    //m_VellsOfPlayer2
                    // yosi end check
                }


                // yosi end 
                // delete origin and add new place of vessel
                isMoved = true;
            }

            return isMoved;
        }

        private void eatEnemyVessel(byte i_IndexOfVesselOneX, byte i_IndexOfVesselOneY, byte i_IndexOfVesselTwoX, byte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .


            byte middleIndexX = (byte)((i_IndexOfVesselTwoX + i_IndexOfVesselOneX) / 2);
            byte middleIndexY = (byte)((i_IndexOfVesselOneY + i_IndexOfVesselTwoY) / 2);

            if (isHaveEnemyInCrossToEat(middleIndexX, middleIndexY, i_IndexOfVesselTwoX, i_IndexOfVesselTwoY))
            {
                IsEated = true;
                ExuteMove(i_IndexOfVesselOneX, i_IndexOfVesselOneY, i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);


                IsTurnPass = true;

                // yossi move vessel
                // if (Player == k_Player1) { } else  { }
                //yosi start
                Locat original = new Locat(i_IndexOfVesselOneX, i_IndexOfVesselOneY);
                Locat newVessel = new Locat(i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);
                Locat isEatedNow = new Locat(middleIndexX, middleIndexY);

                if (NowPlaying == k_Player1)
                {
                    ChanghVeesslInList(m_VellsOfPlayer1, original, newVessel);
                    sortListOfVesselBecomingFirst(m_VellsOfPlayer1);
                    m_VellsOfPlayer2.Remove(isEatedNow);
                }
                else
                {
                    ChanghVeesslInList(m_VellsOfPlayer2, original, newVessel);
                    sortListOfVesselBecomingFirst(m_VellsOfPlayer2);
                    m_VellsOfPlayer1.Remove(isEatedNow);
                }

                // yosi end 

            }
            else
            {
                Console.WriteLine("Cant jump so far without Eat. - you cant eat nothing or yourself - . try again.");
            }

        }

        private void ExuteMove(byte i_IndexOfVesselOneX, byte i_IndexOfVesselOneY, byte i_IndexOfVesselTwoX, byte i_IndexOfVesselTwoY)
        {
            m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX];
            m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = (byte)eCheckers.Non;

            if (IsEated == true)
            {
                byte middleIndexX = (byte)((i_IndexOfVesselTwoX + i_IndexOfVesselOneX) / 2);
                byte middleIndexY = (byte)((i_IndexOfVesselOneY + i_IndexOfVesselTwoY) / 2);
                m_Mat[middleIndexY, middleIndexX] = (byte)eCheckers.Non; // eCheckers.Non = 0
            }
        }

        public void eatWithSameSoilder(byte indexInput1X, byte indexInput1Y, byte indexInput2X, byte indexInput2Y)
        {
            IsEated = false;

            if (checkMoveInCross(2, indexInput1X, indexInput1Y, indexInput2X, indexInput2Y))
            {

                Console.WriteLine("isin cross of 2");

                eatEnemyVessel(indexInput1X, indexInput1Y, indexInput2X, indexInput2Y);

            }

        }

        private bool checkMoveInCross(byte moveDist, byte indexX1, byte indexY1, byte indexX2, byte indexY2)
        {
            byte stepsLineX = (byte)Math.Abs(indexX1 - indexX2), stepsLineY = (byte)Math.Abs(indexY1 - indexY2);
            return stepsLineX == moveDist && stepsLineY == moveDist;
        }

        public bool checkingBounderis(byte i_VesselIndexX, byte i_VesselIndexY) // check that the indexes still in limit.
        {////////////////////////////// Right ///////////////////////////// Down ////////////////////////////////// Left /////////////////////// Up //////////
         //   bool  isInLimit = (i_VesselIndexX + 2 <= m_Size - 1) && (i_VesselIndexY + 2 <= m_Size - 1) && (i_VesselIndexX - 2 >= 0) && (i_VesselIndexY - 2 >= 0);

            byte start = 0, end = (byte)(m_Size - 1);
            bool isRightUpSpotLegal = (i_VesselIndexX + 2 <= end) && (i_VesselIndexY - 2 >= start);
            bool isRightDownSpotLegal = (i_VesselIndexX + 2 <= end) && (i_VesselIndexY + 2 <= end);
            bool isLeftUpSpotLegal = (i_VesselIndexX - 2 >= start) && (i_VesselIndexY - 2 >= start);
            bool isLeftDownSpotLegal = (i_VesselIndexX - 2 >= start) && (i_VesselIndexY + 2 <= end);

            bool isCanEatAgain = false;

            if (isRightUpSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY - 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY - 2)))
                    Console.WriteLine("isRightUpSpotClear");
                /// -- delete2
            }
            if (isRightDownSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY + 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX + 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX + 2), (byte)(i_VesselIndexY + 2)))
                    Console.WriteLine("isRightDownSpotClear");
                /// -- delete2
            }
            if (isLeftUpSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY - 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY - 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY - 2)))
                    Console.WriteLine("isLeftUpSpotClear");
                /// -- delete2
            }
            if (isLeftDownSpotLegal == true && isCanEatAgain == false)
            {
                isCanEatAgain = isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY + 2));
                /// -- delete1
                if (isHaveEnemyInCrossToEat((byte)(i_VesselIndexX - 1), (byte)(i_VesselIndexY + 1), (byte)(i_VesselIndexX - 2), (byte)(i_VesselIndexY + 2)))
                    Console.WriteLine("isLeftDownSpotLegal");
                /// -- delete2
            }

            return isCanEatAgain;
        }

        // MethodNew here. --==

        // yssi start
        //public bool CanToEat(byte[] i_IndexesToPlay, byte[] i_IndexesThatLegal)
        public bool player2CanToEat(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        //yossi end
        {
            bool foundGoodPlace = false;
            byte start = 0, end = (byte)(m_Size - 1);
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
           
            if (CanToEatInRightUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
            {
                foundGoodPlace = true;
            }

            if (foundGoodPlace == false)
            {
                if (CanToEatInLeftUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }
            }

            eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];
            if (foundGoodPlace == false && ((currentSoilder & kings) == currentSoilder))
            {
               
                if(CanToEatInRightDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }
                if (foundGoodPlace == false)
                {
                    if(CanToEatInLeftDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
                    {
                        foundGoodPlace = true;
                    }
                }

            }

            return foundGoodPlace;
        }

        public bool player1CanToEat(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        //yossi end
        {
            bool foundGoodPlace = false;
            byte start = 0, end = (byte)(m_Size - 1);
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;

            if (CanToEatInRightDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
            {
                foundGoodPlace = true;
            }
            if (foundGoodPlace == false)
            {
                if (CanToEatInLeftDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }
            }
            eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];
            if (foundGoodPlace == false && ((currentSoilder & kings) == currentSoilder))
            {
                if (CanToEatInRightUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }

                if (foundGoodPlace == false)
                {
                    if (CanToEatInLeftUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
                    {
                        foundGoodPlace = true;
                    }
                }

            }

            return foundGoodPlace;
        }

        public bool CanToEatInRightUpInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            bool isCanToEatInRightUpInY = false;
            byte start = 0, end = (byte)(m_Size - 1);
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isRightUpSpotLegal = (indexX + 2 <= end) && (indexY - 2 >= start);
            if (isRightUpSpotLegal)
            {
                if (isHaveEnemyInCrossToEat((byte)(indexX + 1), (byte)(indexY - 1), (byte)(indexX + 2), (byte)(indexY - 2)))
                {
                    o_IndexesThatLegal.X = (byte)(indexX + 2);
                    o_IndexesThatLegal.Y = (byte)(indexY - 2);
                    
                    isCanToEatInRightUpInY = true;
                }
            }
            return isCanToEatInRightUpInY;
        }

        public bool CanToEatInLeftUpInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            bool isCanToEatLeftUpInY = false;


            byte start = 0, end = (byte)(m_Size - 1);
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isLeftUpSpotLegal = (indexX - 2 >= start) && (indexY - 2 >= start);
            if (isLeftUpSpotLegal)
            {
                if (isHaveEnemyInCrossToEat((byte)(indexX - 1), (byte)(indexY - 1), (byte)(indexX - 2), (byte)(indexY - 2)))
                {
                    
                    o_IndexesThatLegal.X = (byte)(indexX - 2);
                    o_IndexesThatLegal.Y = (byte)(indexY - 2);

                    isCanToEatLeftUpInY = true;
                }
            }
            return isCanToEatLeftUpInY;



        }

        public bool CanToEatInRightDownInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            bool isCanToEatRightDownInY = false;


            byte start = 0, end = (byte)(m_Size - 1);
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isRightDownSpotLegal = (indexX + 2 <= end) && (indexY + 2 <= end);
            if (isRightDownSpotLegal)
            {
                if (isHaveEnemyInCrossToEat((byte)(indexX + 1), (byte)(indexY + 1), (byte)(indexX + 2), (byte)(indexY + 2)))
                {
                    o_IndexesThatLegal.X = (byte)(indexX + 2);
                    o_IndexesThatLegal.Y = (byte)(indexY + 2);
                    isCanToEatRightDownInY = true;
                }
            }
            return isCanToEatRightDownInY;

        }

        public bool CanToEatInLeftDownInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            bool isCanToEatLeftDownInY = false;


            byte start = 0, end = (byte)(m_Size - 1);
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isLeftDownSpotLegal = (indexX - 2 >= start) && (indexY + 2 <= end);

            if (isLeftDownSpotLegal)
            {
                if (isHaveEnemyInCrossToEat((byte)(indexX - 1), (byte)(indexY + 1), (byte)(indexX - 2), (byte)(indexY + 2)))
                {
                  
                    o_IndexesThatLegal.X = (byte)(indexX - 2);
                    o_IndexesThatLegal.Y = (byte)(indexY + 2);

                   
                    isCanToEatLeftDownInY = true;
                }

            }
            return isCanToEatLeftDownInY;
        }

        // yosi 
        public bool CanToMultiEat(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        //yossi end
        {
            bool foundGoodPlace = false;
            byte start = 0, end = (byte)(m_Size - 1);
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;


            
               foundGoodPlace = CanToEatInRightDownInY(i_IndexesToPlay, out o_IndexesThatLegal);
           
            if (foundGoodPlace == false)
            {
                    foundGoodPlace = CanToEatInLeftDownInY(i_IndexesToPlay, out o_IndexesThatLegal);

            }
        
            if (foundGoodPlace == false)
            {
                
                
                    foundGoodPlace = CanToEatInRightUpInY(i_IndexesToPlay, out o_IndexesThatLegal);
                
            }

            if (foundGoodPlace == false)
            {
                
                
                    foundGoodPlace = CanToEatInLeftUpInY(i_IndexesToPlay, out o_IndexesThatLegal);
                
            }
            
            return foundGoodPlace;
        }
    


        /*
        bool CanToMoveAndClearAround(byte[] i_IndexesToPlay, out byte[] i_IndexesThatLegal)
        {



            return;
        }
        */

        // old CanToMove
        // public bool CanToMove(byte[] i_IndexesToPlay, byte[] i_IndexesThatLegal)
        //public bool CanToMove(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        //// yosi end
        //{
        //    byte start = 0, end = (byte)(m_Size - 1);

        //    bool foundGoodPlace = false;
        //    //yosi start
        //    //byte indexX = i_IndexesToPlay[0], indexY = i_IndexesToPlay[1];
        //    o_IndexesThatLegal = new Locat();
        //    byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
        //    //yosi end 
        //    bool isRightUpSpotLegal = (indexX + 1 <= end) && (indexY - 1 >= start);
        //    bool isLeftUpSpotLegal = (indexX - 1 >= start) && (indexY - 1 >= start);

        //    eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];

        //    if (isRightUpSpotLegal)
        //    {
        //        eCheckers spotToCheck = (eCheckers)m_Mat[indexY - 1, indexX + 1];
        //        if (spotToCheck == eCheckers.Non)
        //        {
        //            // yosi start
        //            //o_IndexesThatLegal[0] = (byte)(indexX + 1);
        //            //o_IndexesThatLegal[1] = (byte)(indexY - 1);

        //            o_IndexesThatLegal.X = (byte)(indexX + 1);
        //            o_IndexesThatLegal.Y = (byte)(indexY - 1);

        //            //yosi end
        //            foundGoodPlace = true;
        //        }
        //    }

        //    if (isLeftUpSpotLegal && foundGoodPlace == false)
        //    {
        //        eCheckers spotToCheck = (eCheckers)m_Mat[indexY - 1, indexX - 1];
        //        if (spotToCheck == eCheckers.Non)
        //        {
        //            //yosi start
        //            //o_IndexesThatLegal[0] = (byte)(indexX - 1);
        //            //o_IndexesThatLegal[1] = (byte)(indexY - 1);

        //            o_IndexesThatLegal.X = (byte)(indexX - 1);
        //            o_IndexesThatLegal.Y = (byte)(indexY - 1);

        //            //yosi end 
        //            foundGoodPlace = true;
        //        }
        //    }

        //    if (foundGoodPlace == false && ((currentSoilder & kings) == currentSoilder))
        //    {
        //        bool isRightDownSpotLegal = (indexX + 1 <= end) && (indexY + 1 <= end);
        //        bool isLeftDownSpotLegal = (indexX - 1 >= start) && (indexY + 1 <= end);

        //        if (isRightDownSpotLegal)
        //        {
        //            eCheckers spotToCheck = (eCheckers)m_Mat[indexY + 1, indexX + 1];
        //            if (spotToCheck == eCheckers.Non)
        //            {
        //                // yosi start
        //                //o_IndexesThatLegal[0] = (byte)(indexX + 1);
        //                //o_IndexesThatLegal[1] = (byte)(indexY + 1);

        //                o_IndexesThatLegal.X = (byte)(indexX + 1);
        //                o_IndexesThatLegal.Y = (byte)(indexY + 1);

        //                // yosi end 
        //                foundGoodPlace = true;
        //            }
        //        }

        //        if (isLeftDownSpotLegal && foundGoodPlace == false)
        //        {
        //            eCheckers spotToCheck = (eCheckers)m_Mat[indexY + 1, indexX - 1];
        //            if (spotToCheck == eCheckers.Non)
        //            {
        //                // yosi start 
        //                //o_IndexesThatLegal[0] = (byte)(indexX - 1);
        //                //o_IndexesThatLegal[1] = (byte)(indexY + 1);

        //                o_IndexesThatLegal.X = (byte)(indexX - 1);
        //                o_IndexesThatLegal.Y = (byte)(indexY + 1);

        //                // yosi end 
        //                foundGoodPlace = true;
        //            }
        //        }
        //    }
        //    //yosi start
        //    //return false;
        //    return foundGoodPlace;
        //    //yosi end
        //}


        //bulit method rightupiny

        public bool Player2CanToMove(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            byte start = 0, end = (byte)(m_Size - 1);

            bool foundGoodPlace = false;
            
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            
            eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];

            if ( CanToMoveRightUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
            {
                foundGoodPlace = true;
            }

            if ( foundGoodPlace == false)
            {
                if (CanToMoveLeftUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }
            }

            if (foundGoodPlace == false && ((currentSoilder & kings) == currentSoilder))
            {
                
                if (CanToMoveRightDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }

                if (foundGoodPlace == false)
                {
                    if(CanToMoveLeftDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
                    {
                        foundGoodPlace = true;
                    }
                }
            }
            
            return foundGoodPlace;
            
        }

        public bool Player1CanToMove(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {

            byte start = 0, end = (byte)(m_Size - 1);

            bool foundGoodPlace = false;

            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;

            eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];
            if (CanToMoveRightDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
            {
                foundGoodPlace = true;
            }

            if (foundGoodPlace == false)
            {
                if (CanToMoveLeftDownInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }
            }


            if (foundGoodPlace == false && ((currentSoilder & kings) == currentSoilder))
            {
                if (CanToMoveRightUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
                {
                    foundGoodPlace = true;
                }

                if (foundGoodPlace == false)
                {
                    if (CanToMoveLeftUpInY(i_IndexesToPlay, out o_IndexesThatLegal))
                    {
                        foundGoodPlace = true;
                    }
                }
            }

            return foundGoodPlace;


        }


        public bool CanToMoveRightUpInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            byte start = 0, end = (byte)(m_Size - 1);

            bool isCanMoveRightUpinYLine = false;
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isRightUpSpotLegal = (indexX + 1 <= end) && (indexY - 1 >= start);

            if (isRightUpSpotLegal)
            {
                eCheckers spotToCheck = (eCheckers)m_Mat[indexY - 1, indexX + 1];
                if (spotToCheck == eCheckers.Non)
                {
                    o_IndexesThatLegal.X = (byte)(indexX + 1);
                    o_IndexesThatLegal.Y = (byte)(indexY - 1);

                    
                    isCanMoveRightUpinYLine = true;
                }
            }
            return isCanMoveRightUpinYLine;

            
        }

        public bool CanToMoveLeftUpInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            byte start = 0, end = (byte)(m_Size - 1);

            bool isCanMoveLefttUpinYLine = false;
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isLeftUpSpotLegal = (indexX - 1 >= start) && (indexY - 1 >= start);

            if (isLeftUpSpotLegal)
            {
                eCheckers spotToCheck = (eCheckers)m_Mat[indexY - 1, indexX - 1];
                if (spotToCheck == eCheckers.Non)
                {
                    //yosi start
                    //o_IndexesThatLegal[0] = (byte)(indexX - 1);
                    //o_IndexesThatLegal[1] = (byte)(indexY - 1);

                    o_IndexesThatLegal.X = (byte)(indexX - 1);
                    o_IndexesThatLegal.Y = (byte)(indexY - 1);

                    //yosi end 
                    isCanMoveLefttUpinYLine = true;
                }
            }
            return isCanMoveLefttUpinYLine;

        }

        public bool CanToMoveRightDownInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            byte start = 0, end = (byte)(m_Size - 1);

            bool isCanMoveRightDowninYLine = false;
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isRightDownSpotLegal = (indexX + 1 <= end) && (indexY + 1 <= end);
            if (isRightDownSpotLegal)
            {
                eCheckers spotToCheck = (eCheckers)m_Mat[indexY + 1, indexX + 1];
                if (spotToCheck == eCheckers.Non)
                {
                    // yosi start
                    //o_IndexesThatLegal[0] = (byte)(indexX + 1);
                    //o_IndexesThatLegal[1] = (byte)(indexY + 1);

                    o_IndexesThatLegal.X = (byte)(indexX + 1);
                    o_IndexesThatLegal.Y = (byte)(indexY + 1);

                    // yosi end 
                    isCanMoveRightDowninYLine = true;
                }
            }
            return isCanMoveRightDowninYLine;

        }

        public bool CanToMoveLeftDownInY(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        {
            byte start = 0, end = (byte)(m_Size - 1);

            bool isCanMoveleftDowninYLine = false;
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            bool isLeftDownSpotLegal = (indexX - 1 >= start) && (indexY + 1 <= end);
            if (isLeftDownSpotLegal)
            {
                eCheckers spotToCheck = (eCheckers)m_Mat[indexY + 1, indexX - 1];
                if (spotToCheck == eCheckers.Non)
                {
                    // yosi start 
                    //o_IndexesThatLegal[0] = (byte)(indexX - 1);
                    //o_IndexesThatLegal[1] = (byte)(indexY + 1);

                    o_IndexesThatLegal.X = (byte)(indexX - 1);
                    o_IndexesThatLegal.Y = (byte)(indexY + 1);

                    // yosi end 
                    isCanMoveleftDowninYLine = true;
                }
            }
            return isCanMoveleftDowninYLine;
        }

        // end try 
       

        private bool CanToMove(byte indexX, byte indexY)
        {
            byte start = 0, end = (byte)(m_Size - 1);

            bool foundGoodPlace = false;

            bool isRightUpSpotLegal = (indexX + 1 <= end) && (indexY - 1 >= start);
            bool isLeftUpSpotLegal = (indexX - 1 >= start) && (indexY - 1 >= start);

            eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];

            if (isRightUpSpotLegal)
            {
                eCheckers spotToCheck = (eCheckers)m_Mat[indexY - 1, indexX + 1];
                if (spotToCheck == eCheckers.Non)
                {
                    foundGoodPlace = true;
                }
            }

            if (isLeftUpSpotLegal && foundGoodPlace == false)
            {
                eCheckers spotToCheck = (eCheckers)m_Mat[indexY - 1, indexX - 1];
                if (spotToCheck == eCheckers.Non)
                {
                    foundGoodPlace = true;
                }
            }

            if (foundGoodPlace == false && ((currentSoilder & kings) == currentSoilder))
            {
                bool isRightDownSpotLegal = (indexX + 1 <= end) && (indexY + 1 <= end);
                bool isLeftDownSpotLegal = (indexX - 1 >= start) && (indexY + 1 <= end);

                if (isRightDownSpotLegal)
                {
                    eCheckers spotToCheck = (eCheckers)m_Mat[indexY + 1, indexX + 1];
                    if (spotToCheck == eCheckers.Non)
                    {
                        foundGoodPlace = true;
                    }
                }

                if (isLeftDownSpotLegal && foundGoodPlace == false)
                {
                    eCheckers spotToCheck = (eCheckers)m_Mat[indexY + 1, indexX - 1];
                    if (spotToCheck == eCheckers.Non)
                    {
                        foundGoodPlace = true;
                    }
                }
            }

            return false;
        }



        // goal is yaad
        private bool isHaveEnemyInCrossToEat(byte indexMiddleX, byte indexMiddleY, byte indexGoalX, byte indexGoalY)
        {
            eCheckers soildersTeam = soilderKind();
            eCheckers checker = (eCheckers)m_Mat[indexMiddleY, indexMiddleX], freeSpot = (eCheckers)m_Mat[indexGoalY, indexGoalX];
            return (freeSpot == eCheckers.Non && ((checker & soildersTeam) != checker && checker != eCheckers.Non));
        }

        public static void charsToIndex(out byte o_VesselIndexX, char i_CapitalLetterX, out byte o_VesselIndexY, char i_SmallLetterY)
        {
            o_VesselIndexX = (byte)(i_CapitalLetterX - 'A');
            o_VesselIndexY = (byte)(i_SmallLetterY - 'a');
        }

        private bool isMoveFront(byte vesselOneY, byte vesselTwoY) // checking if it is go front.!
        {
            sbyte distLineY = (sbyte)(vesselOneY - vesselTwoY);

            if (NowPlaying != k_Player1)
            {
                distLineY *= -1;
            }

            return distLineY < 0 ? true : false;
        }

        private eCheckers soilderKind()
        {
            return NowPlaying == k_Player1 ? eCheckers.CheckerO | eCheckers.CheckerU : eCheckers.CheckerX | eCheckers.CheckerK;
        }

        public bool GameOn()
        {
            // yosi to do 
            if (NowPlaying == k_Player1)
            {
                m_GameOn = matricxChekers.AiForDamka.TheBestMoveToDoForPlayer1(this) != null;
                if(m_GameOn == false)
                {
                    Console.WriteLine("player 1 loser");
                }
               // m_GameOn = true;
            }
            else
            {
                //need to bulit this !!!  AiForDamka.TheBestMoveToDoforPlayerOne
               
                m_GameOn = matricxChekers.AiForDamka.TheBestMoveToDoForPlayer2(this) != null;
                if (m_GameOn == false)
                {
                    Console.WriteLine("player 2 loser");
                }
            }

            return m_GameOn;
        }

        [Flags]
        private enum eCheckers : byte
        {
            Non = 0,
            CheckerO = 1,
            CheckerX = 2,
            CheckerU = 4,
            CheckerK = 8
        }
    }
}
