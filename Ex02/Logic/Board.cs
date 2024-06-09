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
        private BoardCell[,] m_boardMatrix;

        public Board(int[] i_BoardDimensions)
        {
            m_boardRows = i_BoardDimensions[0];
            m_boardColumns = i_BoardDimensions[1];
            m_boardMatrix = new BoardCell[m_boardRows, m_boardColumns];
        }

        public List<char> generateCards() // makes a list of the cards values to put in the board
        {
            char initialSeedCharacter = 'Z';
            int numberOfCellPairs = (m_boardRows * m_boardColumns) / 2;
            List<char> cardsList = new List<char>();

            for (int i = 0; i < numberOfCellPairs; i++)
            {
                cardsList.Add(initialSeedCharacter);
                cardsList.Add(initialSeedCharacter);
                initialSeedCharacter--;
            }

            return cardsList;
        }

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
                    cardsList.RemoveAt(randomIndex);
                }
            }
        }

        public void toggleCellVisibility(int i_RowIndex, int i_ColIndex)
        {
            bool currentVisibility = m_boardMatrix[i_RowIndex, i_ColIndex].IsVisible;
            m_boardMatrix[i_RowIndex, i_ColIndex].toggleCellVisibility();
        }
        public int[] getSize()
        {
            int[] size = { this.m_boardRows, this.m_boardColumns };
            return size;
        }
        public BoardCell[,] getBoardCells()
        {
            return this.m_boardMatrix;
        }
        public bool isBoardFullyVisible() //New - maybe to move to the GameLogic
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

    }
}