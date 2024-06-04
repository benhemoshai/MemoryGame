using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class BoardCell<cellType>
    {
        private cellType m_CellValue;
        private bool m_IsVisible;

        public BoardCell(cellType i_CellValue)
        {
            this.m_CellValue = i_CellValue;
            this.m_IsVisible = false;
        }

        public cellType CellValue
        {
            get { return CellValue; }
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