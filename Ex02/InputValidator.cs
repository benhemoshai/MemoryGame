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
        private int[] m_BoardSize;
        
        public InputValidator()
        {
            this.m_BoardSize = new int[2];
            this.m_BoardSize[0] = 6; this.m_BoardSize[1] = 6;
        }

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
        public int [] getValidBoardSize()
        {
            int[] board = new int[2];
            int numberOfRows = 0;
            int numberOfCols = 0;
            bool isValidNumOfRows = false;
            bool isValidNumOfCols = false;
            bool isValidBoardSize = false;

            while (!isValidBoardSize)
            {
                Console.WriteLine("Enter a valid number of rows (between 4 to 6): ");
                isValidNumOfRows = int.TryParse(Console.ReadLine(), out this.m_BoardSize[0]);
                numberOfRows = this.m_BoardSize[0];
                
                if (!isValidNumOfRows)
                {
                    Console.WriteLine("You didn't enter a number!");
                    continue;
                }
                else if (numberOfRows < 4 || numberOfRows > 6)
                {
                    Console.WriteLine(String.Format("The rows number {0} is not between 4 to 6!", this.m_BoardSize[0]));
                    continue;
                }

                Console.WriteLine("Enter a valid number of cols (between 4 to 6): ");
                isValidNumOfCols = int.TryParse(Console.ReadLine(), out this.m_BoardSize[1]);
                numberOfCols = this.m_BoardSize[1];
                if (!isValidNumOfCols)
                {
                    Console.WriteLine("You didn't enter a number!");
                    continue;
                }
                else if (numberOfCols < 4 || numberOfCols > 6)
                {
                    Console.WriteLine(String.Format("The columns number {0} is not between 4 to 6!", this.m_BoardSize[1]));
                    continue;
                }

                if (numberOfRows * numberOfCols % 2 != 0)
                {
                    Console.WriteLine("The number of cells in the board has to be even!");
                    continue;
                }

                isValidBoardSize = true;
            }

            return this.m_BoardSize;
        }

        public int [] getValidMove() 
        {
            int[] boardSize = this.m_BoardSize;
            int[] chosenIndexes = new int[2]; 
            int rows = boardSize[0];
            int cols = boardSize[1]-1;

            bool isValidMove = false;

            string moveIndexes = "";
            while (!isValidMove) //add a condition for entering Q
            {
                Console.WriteLine("Enter a valid move: "); //A3
                moveIndexes = Console.ReadLine();
                if (moveIndexes.Length != 2)
                {
                    if (moveIndexes.Equals("Q")) //New!
                    {
                        chosenIndexes[0] = -1;
                        chosenIndexes[1] = -1;
                        return chosenIndexes;
                    }
                    Console.WriteLine("You entered an invalid move, it has to be 2 letters!");
                    continue;
                }
                
                else if (moveIndexes[0] - 65 < 0 || moveIndexes[0] - 65 > cols){ // decrease by 65 to parse it to int 
                    Console.WriteLine("You entered an invalid column index");
                    continue;
                }
                else if (moveIndexes[1] - 48 < 1 || moveIndexes[1] - 48 > rows) // decrease by 48 to parse it to int. changed the condition < 1
                {
                    Console.WriteLine("You entered an invalid row index");
                    continue;
                }
                isValidMove = true;
            }

            chosenIndexes[0] = int.Parse(moveIndexes.Substring(1)) - 1; // or moveIndexes[1] - 48 - 1;
            chosenIndexes[1] = moveIndexes[0] - 65;

            return chosenIndexes;

        }
    }
}
