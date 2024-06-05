using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class GameLogic
    {
        private static Board m_Board;
        private static User m_User1;
        private static User m_User2;
        private static Computer m_Computer;
        private static int m_turn = 0;
        private static bool m_isCorrectChoice = false;
        private static bool m_userPressedQ = false;

        public static void startGame()
        {
            initializeGame();
            gameRoutine();
        }

        private static void initializeGame()
        {

            string userName = InputValidator.getValidUserName();
            m_User1 = new User(userName);
            int gameType = InputValidator.getOpponentType();

            if (gameType == 1)
            {
                userName = InputValidator.getValidUserName();
                m_User2 = new User(userName);
            }
            else
            {
                m_Computer = new Computer();
            }

            int[] boardDimensions = InputValidator.getValidBoardSize();
            m_Board = new Board(boardDimensions);
            m_Board.initializeBoard();
        }

        private static void gameRoutine()
        {
            while (!isGameOver())
            {
                cleanAndPrintBoard();
                if (m_turn == 0)
                {
                    userPlay(m_User1);
                  
                    if (m_isCorrectChoice || m_userPressedQ) // we want to seperate in order to send a message when he was correct
                    {
                        continue;
                    }
                    m_turn = 1;
                }
                else if (m_turn == 1)
                {
                    userPlay(m_User2);
                    if (m_isCorrectChoice || m_userPressedQ)
                    {
                        continue;
                    }
                    m_turn = 0;
                }

            }

        }

        private static void cleanAndPrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_Board.printBoard();
        }


        //New!
        private static void userPlay(User i_User)
        {
            int[] firstTurnIndexes = new int[2];
            int[] secondTurnIndexes = new int[2];

            firstTurnIndexes = InputValidator.getValidMove(i_User.UserName);

            if (firstTurnIndexes[0] == -1 && firstTurnIndexes[1] == -1)
            {
                m_userPressedQ = true;
                return;
            }

            userMove(i_User.UserName, firstTurnIndexes[0], firstTurnIndexes[1]);

            secondTurnIndexes = InputValidator.getValidMove(i_User.UserName);

            if (secondTurnIndexes[0] == -1 && secondTurnIndexes[1] == -1) // code duplication
            {
                m_userPressedQ = true;
                return;
            }

            userMove(i_User.UserName, secondTurnIndexes[ 0], secondTurnIndexes[1]);

            //checks if the values inside of the cells are the same - if the user is correct
            if (m_Board.getBoard()[firstTurnIndexes[0], firstTurnIndexes[1]].CellValue.Equals(m_Board.getBoard()[secondTurnIndexes[0], secondTurnIndexes[1]].CellValue))
            {
                Console.WriteLine("Match!");
                i_User.UserScore++;
                m_isCorrectChoice = true;
            }
            //if the user is wrong
            else
            {
                Console.WriteLine("Miss!");
                m_isCorrectChoice = false;
                m_Board.toggleCellVisibility(firstTurnIndexes[0], firstTurnIndexes[1]);
                m_Board.toggleCellVisibility(secondTurnIndexes[0], secondTurnIndexes[1]);
                
            }

            //System.Threading.Thread.Sleep(2000);
            cleanAndPrintBoard();
        }

        private static void userMove(string i_UserName, int i_RowIndex, int i_ColumnIndex)
        {
            if (m_Board.getBoard()[i_RowIndex, i_ColumnIndex].IsVisible) // if the cell is already flipped
            {
                Console.WriteLine("Invalid cell, choose another one.");
                InputValidator.getValidMove(i_UserName);
            }
            else
            {
                m_Board.toggleCellVisibility(i_RowIndex, i_ColumnIndex);
                cleanAndPrintBoard();
            }
        }

        private static void computerMove()
        {
            List<int[]> freeIndexes = m_Board.getFreeIndexes();
            Random rnd = new Random();
            int randomIndex = rnd.Next(freeIndexes.Count());


        }

        private static bool isGameOver()
        {
            return m_Board.isBoardFullyFlipped() || m_userPressedQ;
        }


    }
}