using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class BoardCell
    {
        private char m_CellValue;
        private bool m_IsVisible;

        public BoardCell(char i_CellValue)
        {
            this.m_CellValue = i_CellValue;
            this.m_IsVisible = false;
        }

        public char CellValue
        {
            get { return m_CellValue; }
            set { m_CellValue = value; }
        }

        public bool IsVisible
        {
            get { return m_IsVisible; }
        }

        public void toggleCellVisibility()
        {
            m_IsVisible = !m_IsVisible;
        }



    }

}