using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class InputValidator
    {
        private static int[] m_BoardSize;
        private static List<int[]> m_TakenIndexes = new List<int[]>();

        public static string getValidUserName()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            return userName;
        }

        public static int getOpponentType()
        {
            int choice = -1;
            bool isValidChoice = false;

            while (!isValidChoice)
            {
                Console.WriteLine("Press 1 and enter for two players game");
                Console.WriteLine("Press 2 and enter for playing against the computer");
                isValidChoice = int.TryParse(Console.ReadLine(), out choice);
                if (choice > 2 || choice < 1)
                {
                    Console.WriteLine("You entered a wrong input.");
                    isValidChoice = false;
                    continue;
                }

                isValidChoice = true;
            }
            return choice;
        }

        /*
         * To check if theres a better way to implement this
         * 
         */
        public static int[] getValidBoardSize()
        {
            int[] boardDimensions = new int[2];
            bool isValidNumOfRows = false;
            bool isValidNumOfCols = false;
            bool isValidBoardSize = false;

            while (!isValidBoardSize)
            {
                string userAnswer;
                Console.WriteLine("Enter a valid number of rows (between 4 to 6): ");
                userAnswer = Console.ReadLine();
                isValidNumOfRows = int.TryParse(userAnswer, out boardDimensions[0]);

                if (!isValidNumOfRows)
                {
                    Console.WriteLine("You should enter a number!");
                    continue;
                }
                else if (boardDimensions[0] < 4 || boardDimensions[0] > 6)
                {
                    Console.WriteLine(String.Format("{0} is not in range!", boardDimensions[0]));
                    continue;
                }

                Console.WriteLine("Enter a valid number of columns (between 4 to 6): ");
                isValidNumOfCols = int.TryParse(Console.ReadLine(), out boardDimensions[1]);
                if (!isValidNumOfCols)
                {
                    Console.WriteLine("You should enter a number!");
                    continue;
                }
                else if (boardDimensions[1] < 4 || boardDimensions[1] > 6)
                {
                    Console.WriteLine(String.Format("{0} is not in range!", m_BoardSize[1]));
                    continue;
                }

                if (boardDimensions[0] * boardDimensions[1] % 2 != 0)
                {
                    Console.WriteLine("The number of cells in the board has to be even!");
                    continue;
                }

                m_BoardSize = boardDimensions;
                isValidBoardSize = true;
            }

            return boardDimensions;
        }

        public static int[] getValidMove(string i_UserName, Board i_CurrentBoard)//maybe to receive a list of free indexes as parameter
        {
            int[] boardSize = m_BoardSize;
            int[] chosenIndexes = new int[2];
            int rows = boardSize[0];
            int cols = boardSize[1] - 1;
            bool isValidMove = false;

            string moveIndexes = "";
            while (!isValidMove) //add a condition for entering Q
            {
                Console.WriteLine(string.Format("({0}) Enter a valid move: ", i_UserName)); //A3
                moveIndexes = Console.ReadLine();
                if (moveIndexes.Length != 2)
                {
                    if (moveIndexes.Equals("Q")) //New!
                    {
                        chosenIndexes[0] = -1;
                        chosenIndexes[1] = -1;
                        break;
                    }

                    Console.WriteLine("You entered an invalid move, it has to be 2 letters!");
                    continue;
                }

                else if (moveIndexes[0] - 65 < 0 || moveIndexes[0] - 65 > cols)
                { // decrease by 65 to parse it to int 
                    Console.WriteLine("Invalid column index!");
                    continue;
                }
                else if (moveIndexes[1] - 48 < 1 || moveIndexes[1] - 48 > rows) // decrease by 48 to parse it to int. changed the condition < 1
                {
                    Console.WriteLine("Invalid row index!");
                    continue;
                }

                chosenIndexes[0] = int.Parse(moveIndexes.Substring(1)) - 1; // or moveIndexes[1] - 48 - 1;
                chosenIndexes[1] = moveIndexes[0] - 65;
                //if (i_TakenIndexes.Any(indexes => indexes.SequenceEqual(chosenIndexes)))
                if (i_CurrentBoard.getBoardCells()[chosenIndexes[0], chosenIndexes[1]].IsVisible)
                {
                    Console.WriteLine("This is cell is already taken!");
                    continue;
                }
                else
                {
                    // m_TakenIndexes.Add(chosenIndexes);
                    isValidMove = true;
                    continue;
                }
            }

            return chosenIndexes;
        }

        public static string getUserChoiceWhenGameFinishes()
        {
            Console.WriteLine("Do you want to play again? (press Y or N)");
            string userChoice = "";
            bool isValidChoice = false;
            while(!isValidChoice)
            {
                userChoice = Console.ReadLine();
                if (!userChoice.Equals("Y") && !userChoice.Equals("N"))
                {
                    Console.WriteLine("Enter only Y or N to continue!");
                }
                isValidChoice = true;
            }
            return userChoice;
        }
    }
}