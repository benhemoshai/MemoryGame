using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class MemoryGameUI
    {
        private Board<char> m_Board;
        private User m_User1;
        private User m_User2;
        private Computer m_Computer;
        private bool m_IsPlayingAgainstComputer;
        private InputValidator m_InputValidator;

        public void startGame()
        {
            GameLogic game = new GameLogic();
            game.mainLoop();
        }

      /*  public void printBoard() //move to UI layer?
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
                    boardTableOutput.AppendFormat("   |", this.m_boardMatrix[i, j]);
                }
                boardTableOutput.AppendLine().Append("  ").AppendLine(dividerLine);
            }

            Console.WriteLine(boardTableOutput.ToString());

        }*/

    }

    
}
