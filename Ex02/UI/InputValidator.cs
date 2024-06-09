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
        public static string getValidUserName()
        {
            bool isValidName = false;
            string userName = "";
            while (!isValidName)
            {
                Console.WriteLine("Enter your name: ");
                userName = Console.ReadLine();
                if (userName.Length < 1)
                {
                    Console.WriteLine("Invalid name! Please enter a valid name.");
                    continue;
                }
                else
                {
                    isValidName = true;
                }
            }

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
                    Console.WriteLine("Invalid choice! Please enter 1 or 2.");
                    isValidChoice = false;
                    continue;
                }
                else
                {
                    isValidChoice = true;
                }
            }

            return choice;
        }

         public static int[] getValidBoardSize()
        {
            int numOfRows = askForBoardDimensions("rows");
            int numOfColums = askForBoardDimensions("columns");
            int[] boardDimensions = { numOfRows, numOfColums };

            return boardDimensions;
        }

        private static int askForBoardDimensions(string i_DimensionType)
        {
            string userAnswer;
            int userAnswerInt = -1;
            bool isValidDimension = false;

            while (!isValidDimension)
            {
                Console.WriteLine(string.Format("Enter a valid number of {0} (between 4 to 6): ", i_DimensionType));
                userAnswer = Console.ReadLine();
                bool isValidInteger = int.TryParse(userAnswer, out userAnswerInt);

                if (!isValidInteger)
                {
                    Console.WriteLine("You should enter an integer number!");
                    continue;
                }

                if (userAnswerInt < 4 || userAnswerInt > 6)
                {
                    Console.WriteLine(string.Format("{0} is not in range!", userAnswerInt));
                    continue;
                }

                isValidDimension = true;
            }

            return userAnswerInt;
        }

        public static int[] getValidMove(string i_UserName, Board i_Board)
        {
            int[] chosenIndexes = new int[2];
            bool isValidMove = false;
            int currentBoardRows = i_Board.getSize()[0];
            int currentBoardColumns = i_Board.getSize()[1];
            string moveIndexes = "";

            while (!isValidMove)
            {
                Console.WriteLine(string.Format("({0}) Enter a valid move: ", i_UserName)); 
                moveIndexes = Console.ReadLine();
                if (moveIndexes.Length != 2)
                {
                    if (moveIndexes.Equals("Q")) //New!
                    {
                        chosenIndexes[0] = -1;
                        chosenIndexes[1] = -1;
                        break;
                    }

                    Console.WriteLine("A valid move contains only 2 letters!");
                    continue;
                }

                else if (moveIndexes[0] - 65 < 0 || moveIndexes[0] - 65 > currentBoardColumns - 1)
                { // decrease by 65 to parse it to int 
                    Console.WriteLine("Invalid column index!");
                    continue;
                }
                else if (moveIndexes[1] - 48 < 1 || moveIndexes[1] - 48 > currentBoardRows) // decrease by 48 to parse it to int. changed the condition < 1
                {
                    Console.WriteLine("Invalid row index!");
                    continue;
                }

                chosenIndexes[0] = int.Parse(moveIndexes.Substring(1)) - 1; // or moveIndexes[1] - 48 - 1;
                chosenIndexes[1] = moveIndexes[0] - 65;
                if (i_Board.getBoardCells()[chosenIndexes[0], chosenIndexes[1]].IsVisible)
                {
                    Console.WriteLine("This is cell is already taken!");
                    continue;
                }
                else
                {
                    isValidMove = true;
                    continue;
                }
            }

            return chosenIndexes;
        }
        public static bool isUserWantsAnotherRound()
        {
            bool isValidChoice = false;
            bool isPlayingAnotherGame = false;

            while(!isValidChoice)
            {
                Console.WriteLine("Do you want to play again? (press Y or N)");
                string userChoice = Console.ReadLine();
                if (userChoice.Equals("Y"))
                {
                    isValidChoice = true;
                    isPlayingAnotherGame = true;
                    
                }
                else if (userChoice.Equals("N") || userChoice.Equals("Q"))
                {
                    isValidChoice = true;   
                }
                else
                {
                    Console.WriteLine("Enter only Y or N to continue!");
                    continue;
                }
            }
            
            return isPlayingAnotherGame;
        }        

        
    }

}