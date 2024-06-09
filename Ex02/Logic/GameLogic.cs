using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    /*TODO 
     * 1.change static members from m_member to s_member
     * 2.public methods start with capital letter. private with small.
     * 
     */
    class GameLogic
    {
        private static Board m_Board;
        private static User m_User1;
        private static User m_User2;
        private static bool m_isCorrectChoice = false;
        private static bool m_IsUserAskForQuit = false;

        public static void startGame(User i_User1, User i_User2) // Steps 4-5
        {
            int[] boardDimensions = InputValidator.getValidBoardSize();
            m_User1 = i_User1;
            m_User2 = i_User2;
            m_User1.UserScore = 0;
            m_User2.UserScore = 0;
            m_Board = new Board(boardDimensions);
            m_Board.initializeBoard();

            gameRoutine(); 
        }

        private static void gameRoutine() // Steps 6-13
        {
            User currentPlayingUser = m_User1;
            bool isFirstPlayerTurn = true;
            
            while (!m_IsUserAskForQuit)
            {
                cleanAndPrintBoard();
                printVisibleBoard();

                if (isFirstPlayerTurn)
                {
                    currentPlayingUser = m_User1;
                }
                else
                {
                    currentPlayingUser = m_User2;
                }

                userPlay(currentPlayingUser);

                if (!m_isCorrectChoice)
                {
                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
                
                if (m_Board.isBoardFullyVisible())
                {
                    checkForWinner();
                    break;
                }
            }

            if(m_IsUserAskForQuit)
            {
                MemoryGameManager.finishGame();
            }
            else if(InputValidator.isUserWantsAnotherRound())
            {
                startGame(m_User1, m_User2);
            }
        }


        private static void checkForWinner()
        {
            User winner = m_User2;

            if (m_User1.UserScore == m_User2.UserScore)
            {
                MemoryGameManager.printTieMessage(m_User1, m_User2);
            }
            else
            {
                if (m_User1.UserScore > m_User2.UserScore)
                {
                    winner = m_User1;
                }
                MemoryGameManager.printWinMessage(winner);
            }
        }

        // FOR DEBUGGING!!
        public static void printVisibleBoard()
        {
            System.Console.WriteLine();
            for (int i = 0; i < m_Board.getSize()[0]; i++)
            {
                for (int j = 0; j < m_Board.getSize()[1]; j++)
                {
                    System.Console.Write(m_Board.getBoardCells()[i, j].CellValue + " ");
                }
                System.Console.WriteLine();
            }
        }
        private static void cleanAndPrintBoard()
        {
            System.Console.Clear();
            MemoryGameManager.printBoard(m_Board);
            MemoryGameManager.printScoreBoard(m_User1, m_User2);
        }


        private static void userPlay(User i_CurrentPlayingUser)
        { 
            int[] firstTurnIndexes = new int[2];
            int[] secondTurnIndexes = new int[2];

            if (i_CurrentPlayingUser.IsAI)
            {
                Console.WriteLine("Computer is guessing a move...");
                firstTurnIndexes = getValidRandomCell();
                userMove(firstTurnIndexes[0], firstTurnIndexes[1]);
                System.Threading.Thread.Sleep(2000);
                secondTurnIndexes = getValidRandomCell();
                userMove(secondTurnIndexes[0], secondTurnIndexes[1]);
            }
            else
            {
                firstTurnIndexes = InputValidator.getValidMove(i_CurrentPlayingUser.UserName, m_Board);
                userMove(firstTurnIndexes[0], firstTurnIndexes[1]);
                if (m_IsUserAskForQuit)
                {
                    return;
                }

                secondTurnIndexes = InputValidator.getValidMove(i_CurrentPlayingUser.UserName, m_Board);
                userMove(secondTurnIndexes[0], secondTurnIndexes[1]);

                if (m_IsUserAskForQuit)
                {
                    return;
                }
            }
            checkForMatch(i_CurrentPlayingUser, firstTurnIndexes[0], firstTurnIndexes[1], secondTurnIndexes[0], secondTurnIndexes[1]);
            System.Threading.Thread.Sleep(2000);
            
            cleanAndPrintBoard();
        }        

        private static void userMove(int i_RowIndex, int i_ColumnIndex)// the user plays only when it possible
        {
            if (i_RowIndex != -1 && i_ColumnIndex != -1)
            {
                m_Board.toggleCellVisibility(i_RowIndex, i_ColumnIndex);
                cleanAndPrintBoard();
            }
            else
            {
                m_IsUserAskForQuit = true;
            }
        }
        
        private static void checkForMatch(User i_User, int i_FirstTurnRow, int i_FirstTurnColumn, int i_secondTurnRow, int i_secondTurnColumn)
        {
            object firstTurnValue = m_Board.getBoardCells()[i_FirstTurnRow, i_FirstTurnColumn].CellValue;
            object secondTurnValue = m_Board.getBoardCells()[i_secondTurnRow, i_secondTurnColumn].CellValue;

            if (firstTurnValue.Equals(secondTurnValue))
            {
                i_User.UserScore++;
                m_isCorrectChoice = true;
                if(!m_Board.isBoardFullyVisible())
                {
                    MemoryGameManager.printMatchMessage(i_User.UserName);
                }
            }
            else
            {
                MemoryGameManager.printMissMessage();
                m_isCorrectChoice = false;
                m_Board.toggleCellVisibility(i_FirstTurnRow, i_FirstTurnColumn);
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

    }
}