using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class Board//<cellType>
    {
        private int m_boardRows = 0;
        private int m_boardColumns = 0;
        private int m_boardPairs = 0;
        private BoardCell[,] m_boardMatrix;

        public Board(int[] i_BoardDimensions)
        {
            this.m_boardRows = i_BoardDimensions[0];
            this.m_boardColumns = i_BoardDimensions[1];
            this.m_boardPairs = (this.m_boardRows * this.m_boardColumns) / 2;
            this.m_boardMatrix = new BoardCell[m_boardRows, m_boardColumns];
            //initializeBoard();
        }

        public List<char> generateCards() // makes a list of the cards values to put in the board
        {
            char firstCharacter = 'Z';
            List<char> cardsList = new List<char>();
            for (int i = 0; i < this.m_boardPairs; i++)
            {
                cardsList.Add(firstCharacter); //to change it 
                cardsList.Add(firstCharacter--);
            }
            return cardsList;
        }
        // {Z,Z,Y,Y,X,X} 

        public void initializeBoard()
        {

            List<char> cardsList = generateCards();

            Random rand = new Random();
            int randomIndex = -1;
            for (int i = 0; i < this.m_boardRows; i++)
            {
                for (int j = 0; j < this.m_boardColumns; j++)
                {
                    randomIndex = rand.Next(cardsList.Count());
                    this.m_boardMatrix[i, j] = new BoardCell(cardsList[randomIndex]);
                    // this.m_boardMatrix[i, j] = null; //temporary for printing
                    cardsList.RemoveAt(randomIndex);
                }
            }
        }

        public void toggleCellVisibility(int i_RowIndex, int i_ColIndex)
        {
            //first line
            bool currentVisibility = m_boardMatrix[i_RowIndex, i_ColIndex].IsVisible;
            m_boardMatrix[i_RowIndex, i_ColIndex].toggleCellVisibility();
        }

        public int[] getSize()
        {
            int[] size = { this.m_boardRows, this.m_boardColumns };
            return size;
        }


        public BoardCell[,] getBoard()
        {
            return this.m_boardMatrix;
        }

        public bool isBoardFullyFlipped() //New - maybe to move to the GameLogic
        {
            int flippedCells = 0;
            for (int i = 0; i < this.m_boardRows; i++)
            {
                for (int j = 0; j < this.m_boardColumns; j++)
                {
                    if (this.m_boardMatrix[i, j].IsVisible)
                    {
                        flippedCells++;
                    }
                }
            }
            return (flippedCells == m_boardColumns * m_boardRows);
        }

        public List<int[]> getFreeIndexes()
        {
            List<int[]> freeIndexes = new List<int[]>();

            for (int i = 0; i < this.m_boardRows; i++)
            {
                for (int j = 0; j < this.m_boardColumns; j++)
                {
                    if (!this.m_boardMatrix[i, j].IsVisible)
                    {
                        int[] a = { i, j };
                        freeIndexes.Add(a);
                    }
                }
            }
            return freeIndexes;
        }


        public void printBoard() //move to UI layer?
        {
            int initialLetter = 65;
            string dividerLine = new string('=', 4 * this.m_boardColumns + 1);
            StringBuilder boardTableOutput = new StringBuilder("  ");

            // Adding the columns line
            for (int i = 0; i < this.m_boardColumns; i++)
            {
                boardTableOutput.AppendFormat("  {0} ", (char)initialLetter++);
            }

            boardTableOutput.AppendLine();
            boardTableOutput.Append("  ").AppendLine(dividerLine);

            // Adding the rows
            for (int i = 0; i < this.m_boardRows; i++)
            {
                boardTableOutput.AppendFormat("{0} |", i + 1);
                for (int j = 0; j < this.m_boardColumns; j++)
                {
                    if (this.m_boardMatrix[i, j].IsVisible)
                    {
                        boardTableOutput.AppendFormat(" {0} |", this.m_boardMatrix[i, j].CellValue);
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

    }
}