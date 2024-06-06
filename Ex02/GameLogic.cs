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
                    computerMove();
                    userPlay(m_User2);
                    if (m_isCorrectChoice || m_userPressedQ)
                    {
                        continue;
                    }
                    m_turn = 0;
                }

            }

            declareWinner();
        }

        private static void declareWinner()
        {
            User wonUser;
            if(m_User1.UserScore > m_User2.UserScore)
            {
                wonUser = m_User1;
            }
            else
            {
                wonUser = m_User2;
            }

            Console.WriteLine(string.Format("{0} won the game with {1} points!", wonUser.UserName, wonUser.UserScore));
            
        }

        private static void cleanAndPrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            m_Board.printBoard();
            m_Board.printScoreBoard(m_User1, m_User2);
        }


        //New!
        private static void userPlay(User i_User)
        {
            User currentPlayingUser = i_User;
            int[] firstTurnIndexes = new int[2];
            int[] secondTurnIndexes = new int[2];

            firstTurnIndexes = InputValidator.getValidMove(i_User.UserName, m_Board);

            if (firstTurnIndexes[0] == -1 && firstTurnIndexes[1] == -1)
            {
                m_userPressedQ = true;
                return;
            }

            userMove(firstTurnIndexes[0], firstTurnIndexes[1]);

            secondTurnIndexes = InputValidator.getValidMove(i_User.UserName, m_Board);

            // 

            if (secondTurnIndexes[0] == -1 && secondTurnIndexes[1] == -1) // code duplication
            {
                m_userPressedQ = true;
                return;
            }

            userMove(secondTurnIndexes[0], secondTurnIndexes[1]);

            //checks if the values inside of the cells are the same - if the user is correct
            object firstTurnValue = m_Board.getBoard()[firstTurnIndexes[0], firstTurnIndexes[1]].CellValue;
            object secondTurnValue = m_Board.getBoard()[secondTurnIndexes[0], secondTurnIndexes[1]].CellValue;
            if (firstTurnValue.Equals(secondTurnValue))
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

            System.Threading.Thread.Sleep(2000);
            cleanAndPrintBoard();
        }

        /*
         * 
         *  if (m_Board.getBoard()[i_RowIndex, i_ColumnIndex].IsVisible) // if the cell is already flipped
            {
                Console.WriteLine("Invalid cell, choose another one.");
                InputValidator.getValidMove(i_UserName);
            }
        */

        private static void userMove(int i_RowIndex, int i_ColumnIndex)// the user plays only when it possible
        {
            m_Board.toggleCellVisibility(i_RowIndex, i_ColumnIndex);
            cleanAndPrintBoard();
        }

        private static void computerMove()
        {
            List<int[]> freeIndexes = m_Board.getFreeIndexes();
            Random rnd = new Random();
            int randomIndexOfElementsFromList = rnd.Next(freeIndexes.Count());
            int[] a = new int[2];
            a[0] = freeIndexes[randomIndexOfElementsFromList][0];
            a[1] = freeIndexes[randomIndexOfElementsFromList][1];

        }

        private static bool isGameOver()
        {
            return m_Board.isBoardFullyFlipped() || m_userPressedQ;
        }


    }
}