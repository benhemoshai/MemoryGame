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
            mainLoop();
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

        private static void mainLoop()
        {
            while (!isGameOver())
            {
                if (m_turn == 0)
                {
                    userPlay(m_User1);
                    //ConsoleUtils.Screen.Clear(); ---- move to the inputvalidator? ui?
                    if (m_isCorrectChoice || m_userPressedQ) // we want to seperate in order to send a message when he was correct
                    {
                        continue;
                    }
                    m_turn = 1;
                }
                if (m_turn == 1)
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


        //New!
        private static void userPlay(User i_user)
        {
            int[] firstTurnIndexes = new int[2];
            int[] secondTurnIndexes = new int[2]; 

            firstTurnIndexes = InputValidator.getValidMove();

            if (firstTurnIndexes[0] == -1 && firstTurnIndexes[1] == -1)
            {
                m_userPressedQ = true;
                return;
            }


            userMove(firstTurnIndexes[0], firstTurnIndexes[1]);
            
            
            secondTurnIndexes = InputValidator.getValidMove();

            if (secondTurnIndexes[0] == -1 && secondTurnIndexes[1] == -1) // code duplication
            {
                m_userPressedQ = true;
                return;
            }
            userMove(secondTurnIndexes[0], secondTurnIndexes[1]);
             
            //checks if the values inside of the cells are the same - if the user is correct
            if (m_Board.getBoard()[firstTurnIndexes[0], firstTurnIndexes[1]].CellValue == m_Board.getBoard()[secondTurnIndexes[0], secondTurnIndexes[1]].CellValue)
            {
                i_user.UserScore++;
                m_isCorrectChoice = true; 
                //keep the cards open
            }
            //if the user is wrong
            else 
            {
                m_isCorrectChoice = false;
                //flip the cards
                m_Board.toggleCellVisibility(firstTurnIndexes[0], firstTurnIndexes[1]);
                m_Board.toggleCellVisibility(secondTurnIndexes[0], secondTurnIndexes[1]);
            }
        }

        private static void userMove(int i, int j)
        {
            if (m_Board.getBoard()[i, j].IsVisible) // if the cell is already flipped
            {
                //add a message 
                InputValidator.getValidMove();
            }
            else
            {
                m_Board.toggleCellVisibility(i, j);
            }
        }

        private static void computerMove()
        {
            List<int[]> freeIndexes = m_Board.getFreeIndexes();
            Random rnd = new Random();
            int randomIndex = rnd.Next(freeIndexes.Count());


        }

        //New!
        private static bool isGameOver()
        {
            return m_Board.isBoardFullyFlipped() || m_userPressedQ;
        }
     

    }
}
