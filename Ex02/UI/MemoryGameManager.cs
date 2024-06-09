using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class MemoryGameManager
    {
        public static void StartMemoryGame()
        {
            string currentUserName;
            User opponentUser;

            Console.WriteLine("Welcome to Memory Game!");

            // ######### Step 1 #########
            currentUserName = InputValidator.GetValidUserName();
            User hostUser = new User(currentUserName, false);

            // ######### Step 2 #########
            int opponentType = InputValidator.GetOpponentType();

            // ######### Step 3 #########
            if (opponentType == 1)
            {
                currentUserName = InputValidator.GetValidUserName();
                opponentUser = new User(currentUserName, false);
            }
            else
            {
                opponentUser = new User("Computer", true);
            }

            // ######### Step 4 #########
            GameLogicManager.StartGame(hostUser, opponentUser);
        }
        public static void FinishGame()
        {
            Console.WriteLine("Bye Bye...");
            System.Threading.Thread.Sleep(2000);
        }
        public static void PrintBoard(Board i_Board)
        {
            int initialLetter = 65;
            string dividerLine = new string('=', 4 * i_Board.GetSize()[1] + 1);
            StringBuilder boardTableOutput = new StringBuilder("  ");

            // Adding the columns line
            for (int i = 0; i < i_Board.GetSize()[1]; i++)
            {
                boardTableOutput.AppendFormat("  {0} ", (char)initialLetter++);
            }

            boardTableOutput.AppendLine();
            boardTableOutput.Append("  ").AppendLine(dividerLine);

            // Adding the rows
            for (int i = 0; i < i_Board.GetSize()[0]; i++)
            {
                boardTableOutput.AppendFormat("{0} |", i + 1);
                for (int j = 0; j < i_Board.GetSize()[1]; j++)
                {
                    if (i_Board.GetBoardCells()[i, j].IsVisible)
                    {
                        boardTableOutput.AppendFormat(" {0} |", i_Board.GetBoardCells()[i, j].CellValue);
                    }
                    else
                    {
                        boardTableOutput.Append("   |");
                    }
                }
                boardTableOutput.AppendLine().Append("  ").AppendLine(dividerLine);
            }

            Console.WriteLine(boardTableOutput.ToString());

        }
        public static void PrintScoreBoard(User m_User1, User m_User2)
        {
            string scoreBoardOutput = string.Format("Scoreboard: {0} - {1} points | {2} - {3} points", m_User1.UserName, m_User1.UserScore, m_User2.UserName, m_User2.UserScore);
            Console.WriteLine(scoreBoardOutput);
        }

        public static void PrintTieMessage(User i_User1, User i_User2)
        {
            Console.WriteLine(string.Format("Its a tie, both {0} and {1} finished the game with {2} points!", i_User1.UserName, i_User2.UserName, i_User1.UserScore));
        }

        public static void PrintWinMessage(User i_Winner)
        {
            Console.WriteLine(string.Format("Congratulations {0}, You won the game with {1} points!", i_Winner.UserName, i_Winner.UserScore));
        }

        public static void PrintMatchMessage(string i_UserName)
        {
            Console.WriteLine(string.Format("Nice match {0}! Keep it going...", i_UserName));
        }
        public static void PrintMissMessage()
        {
            Console.WriteLine("Miss!");
        }

    }
}