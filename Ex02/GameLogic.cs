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
                m_User2 = new User("Computer", true);
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

            declareWinner();
            return;
        }

        private static void declareWinner()
        {
            User winner;
            if (m_User1.UserScore == m_User2.UserScore)
            {
                MemoryGameUI.printTieMessage(m_User1,m_User2);
            }
            else
            {
                if (m_User1.UserScore > m_User2.UserScore)
                {
                    winner = m_User1;
                }
                else
                {
                    winner = m_User2;
                }

                MemoryGameUI.printWinMessage(winner);
            }
        }

        private static void cleanAndPrintBoard()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            MemoryGameUI.printBoard(m_Board);
            MemoryGameUI.printScoreBoard(m_User1, m_User2);
        }


        //New!
        private static void userPlay(User i_User)
        {
            //User currentPlayingUser = i_User;
            int[] firstTurnIndexes = new int[2];
            int[] secondTurnIndexes = new int[2];

            if (!i_User.IsAI) // not good if the user chooses its name to be computer
            {
                firstTurnIndexes = InputValidator.getValidMove(i_User.UserName, m_Board);
                //isPressedQ(firstTurnIndexes[0], firstTurnIndexes[1]);

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
            }
            else
            {
                firstTurnIndexes = getValidRandomCell();
                userMove(firstTurnIndexes[0], firstTurnIndexes[1]);

                System.Threading.Thread.Sleep(4000);

                secondTurnIndexes = getValidRandomCell();
                userMove(secondTurnIndexes[0], secondTurnIndexes[1]);
                
            }


            //checks if the values inside of the cells are the same - if the user is correct
            checkForAMatch(i_User, firstTurnIndexes[0], firstTurnIndexes[1], secondTurnIndexes[0], secondTurnIndexes[1]);
        
            System.Threading.Thread.Sleep(2000);
            cleanAndPrintBoard();
        }

        private static void userMove(int i_RowIndex, int i_ColumnIndex)// the user plays only when it possible
        {
            m_Board.toggleCellVisibility(i_RowIndex, i_ColumnIndex);
            cleanAndPrintBoard();
        }
        
        private static void checkForAMatch(User i_User, int i_firstTurnRow, int i_firstTurnColumn, int i_secondTurnRow, int i_secondTurnColumn)
        {
            object firstTurnValue = m_Board.getBoardCells()[i_firstTurnRow, i_firstTurnColumn].CellValue;
            object secondTurnValue = m_Board.getBoardCells()[i_secondTurnRow, i_secondTurnColumn].CellValue;
            if (firstTurnValue.Equals(secondTurnValue))
            {
                MemoryGameUI.printMatchMessage();
                i_User.UserScore++;
                m_isCorrectChoice = true;
            }
            //if the user is wrong
            else
            {
                MemoryGameUI.printMissMessage();
                m_isCorrectChoice = false;
                m_Board.toggleCellVisibility(i_firstTurnRow, i_firstTurnColumn);
                m_Board.toggleCellVisibility(i_secondTurnRow, i_secondTurnColumn);
            }
        }

        private static int [] getValidRandomCell()
        {
            List<int[]> freeIndexes = m_Board.getFreeIndexes();
            int[] computerMoveIndexes = new int[2];

            Random rnd = new Random();
            int randomIndexOfElementsFromList = rnd.Next(freeIndexes.Count());
     
            computerMoveIndexes[0] = freeIndexes[randomIndexOfElementsFromList][0];
            computerMoveIndexes[1] = freeIndexes[randomIndexOfElementsFromList][1];
            return computerMoveIndexes;
        }

        private static bool isPressedQ(int rowIndex, int columnIndex)
        {
            return (rowIndex == -1 && columnIndex == -1);
        }

        private static bool isGameOver()
        {
            return m_Board.isBoardFullyFlipped() || m_userPressedQ;
        }


    }
}