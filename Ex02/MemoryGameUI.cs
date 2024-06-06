using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class MemoryGameUI
    {
        public static void startMemoryGame()
        {
            while (true) {

                GameLogic.startGame();
                string userChoice = InputValidator.getUserChoiceWhenGameFinishes();

                if (userChoice.Equals("Y")) {
                    Ex02.ConsoleUtils.Screen.Clear();
                    continue;
                }
                else
                {
                    finishGame();   
                    break;
                }
            }
        }

        public static void finishGame()
        {
            Console.WriteLine("Bye Bye");
            System.Threading.Thread.Sleep(2000);

        }

        public static void printBoard(Board i_Board)
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
        public static void printScoreBoard(User m_User1, User m_User2)
        {
            string scoreBoardOutput = string.Format("Scoreboard: {0} - {1} points | {2} - {3} point\n", m_User1.UserName, m_User1.UserScore, m_User2.UserName, m_User2.UserScore);

            Console.WriteLine(scoreBoardOutput);
        }

        public static void printTieMessage(User i_User1, User i_User2)
        {
            Console.WriteLine(string.Format("Its a tie! both {0} and {1} finished the game with {2} points!", i_User1.UserName, i_User2.UserName, i_User1.UserScore));
        }

        public static void printWinMessage(User i_Winner)
        {
            Console.WriteLine(string.Format("{0} won the game with {1} points!", i_Winner.UserName, i_Winner.UserScore));
        }

        public static void printMatchMessage()
        {
            Console.WriteLine("Match!");
        }

        public static void printMissMessage()
        {
            Console.WriteLine("Miss!");
        }


    }



    
}
