using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixCheckers
{
    //yosi start
    public struct Locat
    {
        public Locat(int x, int y)
        {
            this.X = (byte)x;
            this.Y = (byte)y;

        }
        public byte X { get; set; }
        public byte Y { get; set; }
    }
    //yosi end
    class CheckersLogic
    {

        private byte[,] m_Mat;
        private byte m_Size;
        private bool m_GameOn;
        private const bool k_Player1 = true;
        short playerOnePoints;
        short playerTwoPoints; // to add when king and less when eating .

        public bool NowPlaying { get; private set; } = k_Player1; // it is changed . Look out.
        public bool IsTurnPass { get; private set; } = false; // isTurnPass
        public bool IsEated { get; private set; } = false;
        //yosi st
        private List<Locat> m_VellsOfPlayer1 = new List<Locat>();
        //its public for Ai
        public List<Locat> m_VellsOfPlayer2 = new List<Locat>(); 

        public void ChanghVeesslInList(List<Locat> i_VellsOfPlayer, Locat i_CurrentVessel, Locat i_NewVessel)
        {
            i_VellsOfPlayer.Remove(i_CurrentVessel);
            i_VellsOfPlayer.Add(i_NewVessel);
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

        private void resetGame()
        {

            m_GameOn = true;

            NowPlaying = k_Player1;

            //yosi start
            Locat currentVeesel = new Locat();
            //yosi end

            playerOnePoints = playerTwoPoints = (short)(((m_Size - 2) / 2) * (m_Size / 2));

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

                    if (i >= (m_Size / 2 + 1) && (i + j) % 2 != 0)
                    {
                        m_Mat[i, j] = (byte)eCheckers.CheckerX;
                        // yossi move vessel
                        // yossi start
                        currentVeesel.X = (byte)j;
                        currentVeesel.Y = (byte)i;
                        m_VellsOfPlayer2.Add(currentVeesel);
                        //yosi end 
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
            if (i_MovePos[2] != '>')
            {
                return false;
            }

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

        private bool eatOrMoveVessel(byte vesselOneX, byte vesselOneY, byte vesselTwoX, byte vesselTwoY)
        {
            bool turnWellPlayed = false;

            const byte oneStepMoveInCross = 1, twoStepsMoveInCross = 2;

            if (checkMoveInCross(oneStepMoveInCross, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY))
            {
                turnWellPlayed = moveVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else if (checkMoveInCross(twoStepsMoveInCross, vesselOneX, vesselOneY, vesselTwoX, vesselTwoY))
            {
                turnWellPlayed = eatEnemyVessel(vesselOneX, vesselOneY, vesselTwoX, vesselTwoY);
            }
            else
            {
                Console.WriteLine("illegal move , you can only move in cross one step or eat in cross . try again.");
            }

            return turnWellPlayed;
        }

        private bool checkIfBecomeKing(ref byte io_Vessel, byte i_LineY)
        {
            bool becomeKing = false;
            if (i_LineY == 0 || i_LineY == (m_Size - 1))
            {
                if (io_Vessel == (byte)eCheckers.CheckerO || io_Vessel == (byte)eCheckers.CheckerX)
                {
                    io_Vessel = NowPlaying == k_Player1 ? (byte)eCheckers.CheckerU : (byte)eCheckers.CheckerK;
                    becomeKing = true;
                }
            }

            return becomeKing;
        }

        private bool moveVessel(byte i_IndexOfVesselOneX, byte i_IndexOfVesselOneY, byte i_IndexOfVesselTwoX, byte i_IndexOfVesselTwoY)
        {
            bool isMoved = false;

            if (m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] == (byte)eCheckers.Non)
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX];
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = (byte)eCheckers.Non; // (byte) eCheckers.Non == 0   
                checkIfBecomeKing(ref m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX], i_IndexOfVesselTwoY);

                IsTurnPass = true;
                // yossi move vessel
                //yosi start
                Locat original = new Locat(i_IndexOfVesselOneX, i_IndexOfVesselOneY);
                Locat newVessel = new Locat(i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);
                if (NowPlaying == k_Player1)
                {
                    ChanghVeesslInList(m_VellsOfPlayer1, original, newVessel);
                }
                else
                {
                    ChanghVeesslInList(m_VellsOfPlayer2, original, newVessel);
                }


                // yosi end 
                // delete origin and add new place of vessel
                isMoved = true;
            }

            return isMoved;
        }

        private bool eatEnemyVessel(byte i_IndexOfVesselOneX, byte i_IndexOfVesselOneY, byte i_IndexOfVesselTwoX, byte i_IndexOfVesselTwoY)
        { //// te bool return is for check if all did appened and not need replay turn . if the bool not needed so to replace to void .

            bool isEated = false;

            byte middleIndexX = (byte)((i_IndexOfVesselTwoX + i_IndexOfVesselOneX) / 2);
            byte middleIndexY = (byte)((i_IndexOfVesselOneY + i_IndexOfVesselTwoY) / 2);

            if (isHaveEnemyInCrossToEat(middleIndexX, middleIndexY, i_IndexOfVesselTwoX, i_IndexOfVesselTwoY))
            {
                m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX] = m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX];
                m_Mat[middleIndexY, middleIndexX] = (byte)eCheckers.Non; // eCheckers.Non = 0
                m_Mat[i_IndexOfVesselOneY, i_IndexOfVesselOneX] = (byte)eCheckers.Non;

                checkIfBecomeKing(ref m_Mat[i_IndexOfVesselTwoY, i_IndexOfVesselTwoX], i_IndexOfVesselTwoY);
                mulitiEatingCheckAndDo(i_IndexOfVesselTwoX, i_IndexOfVesselTwoY);

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
                    m_VellsOfPlayer2.Remove(isEatedNow);
                }
                else
                {
                    ChanghVeesslInList(m_VellsOfPlayer2, original, newVessel);
                    m_VellsOfPlayer1.Remove(isEatedNow);
                }

                // yosi end 
                IsEated = true;

                // isEated = true;
            }
            else
            {
                Console.WriteLine("Cant jump so far without Eat. - you cant eat nothing or yourself - . try again.");
            }

            return isEated;
        }

        public void mulitiEatingCheckAndDo(byte i_VesselIndexX, byte i_VesselIndexY)
        {
            bool continueEating = true;
            bool checkIfOptionToEat = checkingBounderis(i_VesselIndexX, i_VesselIndexY);

            while (checkIfOptionToEat == true) // this while is what i need and i need it for the out method to keep cycling..!!
            { /// -- delete1
                if (NowPlaying == k_Player1)
                {
                    Console.WriteLine("Hey im player1 and i can eat double.!!11");
                }
                else // p2
                {
                    Console.WriteLine("Hey im player2 and i can eat double.!!22");
                }
                /// -- delete2
                ///                

                string inputMove = allInputIsOk();
                if (inputMove != null)
                {
                    byte indexInput1X, indexInput1Y, indexInput2X, indexInput2Y;
                    charsToIndex(out indexInput1X, inputMove[0], out indexInput1Y, inputMove[1]);
                    charsToIndex(out indexInput2X, inputMove[3], out indexInput2Y, inputMove[4]);
                    //// ++ == from here its the main of the multi eating
                    if (indexInput1X == i_VesselIndexX && indexInput1Y == i_VesselIndexY)
                    {
                        continueEating = eatWithSameSoilder(indexInput1X, indexInput1Y, indexInput2X, indexInput2Y);

                        if (continueEating == true)
                        {
                            i_VesselIndexX = indexInput2X;
                            i_VesselIndexY = indexInput2Y;
                        }

                        checkIfOptionToEat = checkingBounderis(i_VesselIndexX, i_VesselIndexY);
                        //// ++ == untill here !!
                        Console.WriteLine("im here the input is good the indexes of target was {0}{1} and the checkToEatAgain is -> {2}", (char)(i_VesselIndexY + 'A'), (char)(i_VesselIndexX + 'a'), checkIfOptionToEat);
                    }
                }
            }
        }

        private string allInputIsOk()
        {
            string rightInput = null;
            PrintBoard();
            Console.WriteLine("Eat Again");
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

        private bool eatWithSameSoilder(byte indexInput1X, byte indexInput1Y, byte indexInput2X, byte indexInput2Y)
        {
            bool isEated = false;

            if (checkMoveInCross(2, indexInput1X, indexInput1Y, indexInput2X, indexInput2Y))
            {
                byte middleIndexX = (byte)((indexInput1X + indexInput2X) / 2); // vesselMovementLineX
                byte middleIndexY = (byte)((indexInput1Y + indexInput2Y) / 2); // vesselMovementLineY

                if (isHaveEnemyInCrossToEat(middleIndexX, middleIndexY, indexInput2X, indexInput2Y))
                {
                    m_Mat[indexInput2Y, indexInput2X] = m_Mat[indexInput1Y, indexInput1X];
                    m_Mat[middleIndexY, middleIndexX] = (byte)eCheckers.Non; // eCheckers.Non = 0
                    m_Mat[indexInput1Y, indexInput1X] = (byte)eCheckers.Non;

                    checkIfBecomeKing(ref m_Mat[indexInput2Y, indexInput2X], indexInput2Y);

                    IsEated = true;

                    // isEated = true;
                }
            }

            return isEated;
        }

        private bool checkMoveInCross(byte moveDist, byte indexX1, byte indexY1, byte indexX2, byte indexY2)
        {
            byte stepsLineX = (byte)Math.Abs(indexX1 - indexX2), stepsLineY = (byte)Math.Abs(indexY1 - indexY2);
            return stepsLineX == moveDist && stepsLineY == moveDist;
        }

        private bool checkingBounderis(byte i_VesselIndexX, byte i_VesselIndexY) // check that the indexes still in limit.
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
        public bool CanToEat(Locat i_IndexesToPlay,out Locat o_IndexesThatLegal)
        //yossi end
        {
            bool foundGoodPlace = false;
            

            byte start = 0, end = (byte)(m_Size - 1);
            //yosi start
            // byte indexX = i_IndexesToPlay[0], indexY = i_IndexesToPlay[1];
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            //yosi end
            bool isRightUpSpotLegal = (indexX + 2 <= end) && (indexY - 2 >= start);
            bool isLeftUpSpotLegal = (indexX - 2 >= start) && (indexY - 2 >= start);

            if (isRightUpSpotLegal)
            {
                if (isHaveEnemyInCrossToEat((byte)(indexX + 1), (byte)(indexY - 1), (byte)(indexX + 2), (byte)(indexY - 2)))
                {
                    //yosi start 
                    //i_IndexesThatLegal[0] = (byte)(indexX + 2);
                    // i_IndexesThatLegal[1] = (byte)(indexY - 2);

                    o_IndexesThatLegal.X = (byte)(indexX + 2);
                    o_IndexesThatLegal.Y = (byte)(indexY - 2);
                    //yosiend
                    foundGoodPlace = true;
                }
            }

            if (isLeftUpSpotLegal && foundGoodPlace == false)
            {
                if (isHaveEnemyInCrossToEat((byte)(indexX - 1), (byte)(indexY - 1), (byte)(indexX - 2), (byte)(indexY - 2)))
                {
                    //yosi start
                    o_IndexesThatLegal.X = (byte)(indexX - 2);
                    o_IndexesThatLegal.Y = (byte)(indexY - 2);

                    // i_IndexesThatLegal[0] = (byte)(indexX - 2);
                    // i_IndexesThatLegal[1] = (byte)(indexY - 2);
                    //yosi end 
                    foundGoodPlace = true;
                }
            }

            eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];
            if (foundGoodPlace == false && ((currentSoilder & kings) == currentSoilder))
            {
                bool isRightDownSpotLegal = (indexX + 2 <= end) && (indexY + 2 <= end);
                bool isLeftDownSpotLegal = (indexX - 2 >= start) && (indexY + 2 <= end);

                if (isRightDownSpotLegal)
                {
                    if (isHaveEnemyInCrossToEat((byte)(indexX + 1), (byte)(indexY + 1), (byte)(indexX + 2), (byte)(indexY + 2)))
                    {
                        //yosi start
                        // i_IndexesThatLegal[0] = (byte)(indexX + 2);
                        // i_IndexesThatLegal[1] = (byte)(indexY + 2);

                        o_IndexesThatLegal.X = (byte)(indexX + 2);
                        o_IndexesThatLegal.Y = (byte)(indexY + 2);

                        //yosi end
                        foundGoodPlace = true;
                    }
                }

                if (isLeftDownSpotLegal && foundGoodPlace == false)
                {
                    if (isHaveEnemyInCrossToEat((byte)(indexX - 1), (byte)(indexY + 1), (byte)(indexX - 2), (byte)(indexY + 2)))
                    {
                        //yosi start
                        //i_IndexesThatLegal[0] = (byte)(indexX - 2);
                        // i_IndexesThatLegal[1] = (byte)(indexY + 2);

                        o_IndexesThatLegal.X = (byte)(indexX - 2);
                        o_IndexesThatLegal.Y = (byte)(indexY + 2);

                        //yosi end
                        foundGoodPlace = true;
                    }
                }

            }

            return foundGoodPlace;
        }


        /*
        bool CanToMoveAndClearAround(byte[] i_IndexesToPlay, out byte[] i_IndexesThatLegal)
        {



            return;
        }
        */

        // yosi start
       // public bool CanToMove(byte[] i_IndexesToPlay, byte[] i_IndexesThatLegal)
        public bool CanToMove(Locat i_IndexesToPlay, out Locat o_IndexesThatLegal)
        // yosi end
        {
            byte start = 0, end = (byte)(m_Size - 1);

            bool foundGoodPlace = false;
            //yosi start
            //byte indexX = i_IndexesToPlay[0], indexY = i_IndexesToPlay[1];
            o_IndexesThatLegal = new Locat();
            byte indexX = i_IndexesToPlay.X, indexY = i_IndexesToPlay.Y;
            //yosi end 
            bool isRightUpSpotLegal = (indexX + 1 <= end) && (indexY - 1 >= start);
            bool isLeftUpSpotLegal = (indexX - 1 >= start) && (indexY - 1 >= start);

            eCheckers kings = eCheckers.CheckerK | eCheckers.CheckerU, currentSoilder = (eCheckers)m_Mat[indexY, indexX];

            if (isRightUpSpotLegal)
            {
                eCheckers spotToCheck = (eCheckers)m_Mat[indexY - 1, indexX + 1];
                if (spotToCheck == eCheckers.Non)
                {
                    // yosi start
                    //o_IndexesThatLegal[0] = (byte)(indexX + 1);
                    //o_IndexesThatLegal[1] = (byte)(indexY - 1);

                    o_IndexesThatLegal.X = (byte)(indexX + 1);
                    o_IndexesThatLegal.Y = (byte)(indexY - 1);

                    //yosi end
                    foundGoodPlace = true;
                }
            }

            if (isLeftUpSpotLegal && foundGoodPlace == false)
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
                        // yosi start
                        //o_IndexesThatLegal[0] = (byte)(indexX + 1);
                        //o_IndexesThatLegal[1] = (byte)(indexY + 1);

                        o_IndexesThatLegal.X = (byte)(indexX + 1);
                        o_IndexesThatLegal.Y = (byte)(indexY + 1);

                        // yosi end 
                        foundGoodPlace = true;
                    }
                }

                if (isLeftDownSpotLegal && foundGoodPlace == false)
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
                        foundGoodPlace = true;
                    }
                }
            }
            //yosi start
            //return false;
            return foundGoodPlace;
            //yosi end
        }


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

        private void charsToIndex(out byte o_VesselIndexX, char i_CapitalLetterX, out byte o_VesselIndexY, char i_SmallLetterY)
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

        public bool GameOn() { return m_GameOn; }

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
