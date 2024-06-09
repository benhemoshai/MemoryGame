using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class GameLogicManager
    {
        private static Board s_Board;
        private static User s_User1;
        private static User s_User2;
        private static bool s_isCorrectChoice = false;
        private static bool s_IsUserAskForQuit = false;

        public static void StartGame(User i_User1, User i_User2) // Steps 4-5
        {
            int[] boardDimensions = InputValidator.GetValidBoardSize();
            s_User1 = i_User1;
            s_User2 = i_User2;
            s_User1.UserScore = 0;
            s_User2.UserScore = 0;
            s_Board = new Board(boardDimensions);
            s_Board.InitializeBoard();

            gameRoutine(); 
        }

        private static void gameRoutine() // Steps 6-13
        {
            User currentPlayingUser = s_User1;
            bool isFirstPlayerTurn = true;
            
            while (!s_IsUserAskForQuit)
            {
                cleanAndPrintBoard();

                if (isFirstPlayerTurn)
                {
                    currentPlayingUser = s_User1;
                }
                else
                {
                    currentPlayingUser = s_User2;
                }

                userPlay(currentPlayingUser);

                if (!s_isCorrectChoice)
                {
                    isFirstPlayerTurn = !isFirstPlayerTurn;
                }
                
                if (s_Board.IsBoardFullyVisible())
                {
                    checkForWinner();
                    break;
                }
            }

            if(s_IsUserAskForQuit)
            {
                MemoryGameManager.FinishGame();
            }
            else if(InputValidator.DoesUserWantAnotherRound())
            {
                StartGame(s_User1, s_User2);
            }
        }


        private static void checkForWinner()
        {
            User winner = s_User2;

            if (s_User1.UserScore == s_User2.UserScore)
            {
                MemoryGameManager.PrintTieMessage(s_User1, s_User2);
            }
            else
            {
                if (s_User1.UserScore > s_User2.UserScore)
                {
                    winner = s_User1;
                }
                MemoryGameManager.PrintWinMessage(winner);
            }
        }

        private static void cleanAndPrintBoard()
        {
            System.Console.Clear();
            MemoryGameManager.PrintBoard(s_Board);
            MemoryGameManager.PrintScoreBoard(s_User1, s_User2);
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
                firstTurnIndexes = InputValidator.GetValidMove(i_CurrentPlayingUser.UserName, s_Board);
                userMove(firstTurnIndexes[0], firstTurnIndexes[1]);
                if (s_IsUserAskForQuit)
                {
                    return;
                }

                secondTurnIndexes = InputValidator.GetValidMove(i_CurrentPlayingUser.UserName, s_Board);
                userMove(secondTurnIndexes[0], secondTurnIndexes[1]);

                if (s_IsUserAskForQuit)
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
                s_Board.ToggleCellVisibility(i_RowIndex, i_ColumnIndex);
                cleanAndPrintBoard();
            }
            else
            {
                s_IsUserAskForQuit = true;
            }
        }
        
        private static void checkForMatch(User i_User, int i_FirstTurnRow, int i_FirstTurnColumn, int i_secondTurnRow, int i_secondTurnColumn)
        {
            object firstTurnValue = s_Board.GetBoardCells()[i_FirstTurnRow, i_FirstTurnColumn].CellValue;
            object secondTurnValue = s_Board.GetBoardCells()[i_secondTurnRow, i_secondTurnColumn].CellValue;

            if (firstTurnValue.Equals(secondTurnValue))
            {
                i_User.UserScore++;
                s_isCorrectChoice = true;
                if(!s_Board.IsBoardFullyVisible())
                {
                    MemoryGameManager.PrintMatchMessage(i_User.UserName);
                }
            }
            else
            {
                MemoryGameManager.PrintMissMessage();
                s_isCorrectChoice = false;
                s_Board.ToggleCellVisibility(i_FirstTurnRow, i_FirstTurnColumn);
                s_Board.ToggleCellVisibility(i_secondTurnRow, i_secondTurnColumn);
            }
        }

        private static int [] getValidRandomCell()
        {
            List<int[]> freeIndexes = s_Board.GetFreeIndexes();
            int[] computerMoveIndexes = new int[2];

            Random rnd = new Random();
            int randomIndexOfElementsFromList = rnd.Next(freeIndexes.Count());
     
            computerMoveIndexes[0] = freeIndexes[randomIndexOfElementsFromList][0];
            computerMoveIndexes[1] = freeIndexes[randomIndexOfElementsFromList][1];

            return computerMoveIndexes;
        }
    }
}