//14/04/2018  15:00

using System;
using System.Text;


namespace MatrixCheckers
{
    class Program
    {
        static void Main(string[] args)
        {
            //yosi start
            Playingyosi();
            //yosi end 

            // Playing();

            // Playing2();

            //  Playing3();

           // Playing4();
            

            // PlayingTestLogic();

            // ArrayTry();

            //while (true)
            //{

            //    string str = InputChecking(8);
            //    if (str != null)
            //    {
            //        Console.WriteLine("The move is : {0}",str);
            //    } 

            //}
            // Console.ReadLine();
        }


        //yosi start
        public static void Playingyosi()
        {
            

            
            Player p1 = new Player();
            Player p2 = new Player();
            p1.Name = Console.ReadLine();
            p2.Name = Console.ReadLine();

            // here add switch case whit enum about size board and player 2

            GamePlay game = new GamePlay(p1,p2,8);
            game.StartGameToPlay();



        }
        // yosi end

        public static void PlayingTestLogic()
        {
            CheckersLogic Board = new CheckersLogic(8);

            Board.PrintBoard();

            string str = Console.ReadLine();
            while (char.ToUpper(str[0]) != 'Q')
            {
                Board.PlayingVessel(str);
                Board.PrintBoard();
                str = Console.ReadLine();
            }


        }

        public static string InputChecking(byte m_Size)
        {
            string rightInput = null;

            // Console.WriteLine("Insert Move");
            string inputGameMove = Console.ReadLine();
            char capitalEnd = (char)((m_Size - 1) + 'A'), littleEnd = (char)((m_Size - 1) + 'a');
            if (inputGameMove.Length >= 5)
            {
                if (checkNotPassTheLimitChars(inputGameMove[0], inputGameMove[1], inputGameMove[3], inputGameMove[4], m_Size) && inputGameMove[2] == '>')
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

        public static bool checkNotPassTheLimitChars(char i_CapitalLetterA, char i_LittleLetterA, char i_CapitalLetterB, char i_LittleLetterB, byte m_Size)
        {
            char capitalEnd = (char)((m_Size - 1) + 'A'), littleEnd = (char)((m_Size - 1) + 'a');
            bool capitalLetters = (i_CapitalLetterA >= 'A' && i_CapitalLetterA <= capitalEnd) && (i_CapitalLetterB >= 'A' && i_CapitalLetterB <= capitalEnd);
            bool littleLetters = (i_LittleLetterA >= 'A' && i_LittleLetterA <= littleEnd) && (i_LittleLetterB >= 'A' && i_LittleLetterB <= littleEnd);
            return capitalLetters && littleLetters;
        }

        public static void ArrayTry()
        {
            byte[] ary = { 0, 1, 2, 3, 4 };

            Console.WriteLine("ary -> {0},{1},{2},{3}", ary[0], ary[1], ary[2], ary[3]);

            CheckArys(ary);

            Console.WriteLine("ary -> {0},{1},{2},{3}", ary[0], ary[1], ary[2], ary[3]);

            Console.ReadLine();
        }

        public static void CheckArys(byte[] ary)
        {
            ary[0] = 12;
            ary[1] = 21;
            ary[2] = 61;
            ary[3] = 19;

        }

        public static void Playing4()
        {


            GamePlay game = new GamePlay(8);

            game.StartGameToPlay();

            // menu
            // name P1
            // size of board
            // P2 or computer


            // ctor here or something?




        }

        public static void Playing3()
        {
            CheckersLogic Board = new CheckersLogic(8);

            Board.PrintBoard();

            BordToGame bord = new BordToGame(8);

            bord.PrintBoardGame();
            Board.PrintBoard();

            string str = Console.ReadLine();
            while (char.ToUpper(str[0]) != 'Q')
            {
                Board.PlayingVessel(str);
                Board.PrintBoard();
                str = Console.ReadLine();
            }
        }

        public static void Playing2()
        {
            Console.Title = "Damka , Good Luck";

            CheckersLogic Board = new CheckersLogic(8);
            Board.PlayingVessel("Ac>Bd"); // Bf>Ce
            Board.PlayingVessel("Bf>Ce");
            Board.PlayingVessel("Cc>Dd");
            Board.PlayingVessel("Hf>Ge");
            Board.PlayingVessel("Dd>Ee");
            Board.PlayingVessel("Df>Fd");
            Board.PlayingVessel("Ec>Dd");
            Board.PlayingVessel("Fd>Ec");
            Board.PlayingVessel("Bb>Cc");

            Board.PlayingVessel("Gg>Hf");
            Board.PlayingVessel("Aa>Bb");
            Board.PlayingVessel("Fh>Gg");


            //// ------- Multi eat
            Board.PlayingVessel("Db>Fd");
            Board.PlayingVessel("Ge>Ec");
            Board.PlayingVessel("Gc>Hd");
            Board.PlayingVessel("Hf>Ge");
            Board.PlayingVessel("Bd>Df");
            //// ------ end.

            //Board.PlayingVessel("");
            //Board.PlayingVessel("");

            Board.PrintBoard();



            string str = Console.ReadLine();
            while (char.ToUpper(str[0]) != 'Q')
            {
                Board.PlayingVessel(str);
                Board.PrintBoard();
                str = Console.ReadLine();
            }

        }

        public static void Playing()
        {

            CheckersLogic Board = new CheckersLogic(8);
            // Board.PrintBoard();

            Board.PlayingVessel("Ac>Bd"); // Bf>Ce
            Board.PlayingVessel("Bf>Ce");
            Board.PlayingVessel("Cc>Dd");
            Board.PlayingVessel("Hf>Ge");
            Board.PlayingVessel("Dd>Ee");
            Board.PlayingVessel("Df>Fd");
            Board.PlayingVessel("Ec>Dd");
            Board.PlayingVessel("Fd>Ec");
            Board.PlayingVessel("Bb>Cc");
            //Board.PlayingVessel("Ce>Ec"); // didnt work and it is Good!!!
            Board.PlayingVessel("Ce>Ac");
            Board.PlayingVessel("Ca>Bb");
            Board.PlayingVessel("Ac>Ca"); // should become to King by number 1King(3), 2King(4)
            Board.PlayingVessel("Dd>Ce");
            Board.PlayingVessel("Ca>Bb");
            Board.PlayingVessel("Ce>Bf");
            Board.PlayingVessel("Ff>Ee");
            Board.PlayingVessel("Cc>Bd");
            Board.PlayingVessel("Ag>Ce");
            Board.PlayingVessel("Ce>Ac");
            Board.PlayingVessel("Db>Fd");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            // -- from  here to
            //Board.PlayingVessel("Db>Fd");
            //Board.PlayingVessel("Cg>Df");
            //Board.PlayingVessel("Fd>Ee");
            //Board.PlayingVessel("Bb>Ac");
            //Board.PlayingVessel("Gc>Hd");
            //Board.PlayingVessel("Ff>Dd");
            // -- to here its just to see if all work back and front eating.            
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");
            //Board.PlayingVessel("");

            //Board.PrintBoard();

            string str = Console.ReadLine();
            while (char.ToUpper(str[0]) != 'Q')
            {
                Board.PlayingVessel(str);
                Board.PrintBoard();
                str = Console.ReadLine();
            }

        }

    }

}

/*

class GamePlay
{

     P
    GameLogic logi;
    BoradUI borad;


    borad[1, 2];

        logi.PlayMove(if , j , i+ , j+)== true { borad[if , j, i + , j +] 
};
  if (isAte()) {
  
    }

};

 */
