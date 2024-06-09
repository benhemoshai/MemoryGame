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
        public static void startMemoryGame()
        {
            string currentUserName;
            User opponentUser;

            Console.WriteLine("Welcome to Memory Game!");

            // ######### Step 1 #########
            currentUserName = InputValidator.getValidUserName();
            User hostUser = new User(currentUserName, false);

            // ######### Step 2 #########
            int opponentType = InputValidator.getOpponentType();

            // ######### Step 3 #########
            if (opponentType == 1)
            {
                currentUserName = InputValidator.getValidUserName();
                opponentUser = new User(currentUserName, false);
            }
            else
            {
                opponentUser = new User("Computer", true);
            }

            // ######### Step 4 #########
            GameLogic.startGame(hostUser, opponentUser);

        }
        internal static void finishGame()
        {
            Console.WriteLine("Bye Bye...");
            System.Threading.Thread.Sleep(2000);
        }
        internal static void printBoard(Board i_Board)
        {
            int initialLetter = 65;
            string dividerLine = new string('=', 4 * i_Board.getSize()[1] + 1);
            StringBuilder boardTableOutput = new StringBuilder("  ");

            // Adding the columns line
            for (int i = 0; i < i_Board.getSize()[1]; i++)
            {
                boardTableOutput.AppendFormat("  {0} ", (char)initialLetter++);
            }

            boardTableOutput.AppendLine();
            boardTableOutput.Append("  ").AppendLine(dividerLine);

            // Adding the rows
            for (int i = 0; i < i_Board.getSize()[0]; i++)
            {
                boardTableOutput.AppendFormat("{0} |", i + 1);
                for (int j = 0; j < i_Board.getSize()[1]; j++)
                {
                    if (i_Board.getBoardCells()[i, j].IsVisible)
                    {
                        boardTableOutput.AppendFormat(" {0} |", i_Board.getBoardCells()[i, j].CellValue);
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
        internal static void printScoreBoard(User m_User1, User m_User2)
        {
            string scoreBoardOutput = string.Format("Scoreboard: {0} - {1} points | {2} - {3} points", m_User1.UserName, m_User1.UserScore, m_User2.UserName, m_User2.UserScore);

            Console.WriteLine(scoreBoardOutput);
        }

        internal static void printTieMessage(User i_User1, User i_User2)
        {
            Console.WriteLine(string.Format("Its a tie, both {0} and {1} finished the game with {2} points!", i_User1.UserName, i_User2.UserName, i_User1.UserScore));
        }

        internal static void printWinMessage(User i_Winner)
        {
            Console.WriteLine(string.Format("Congratulations {0}, You won the game with {1} points!", i_Winner.UserName, i_Winner.UserScore));
        }

        internal static void printMatchMessage(string i_UserName)
        {
            Console.WriteLine(string.Format("Nice match {0}! Keep it going...", i_UserName));
        }
        internal static void printMissMessage()
        {
            Console.WriteLine("Miss!");
        }

    }
}