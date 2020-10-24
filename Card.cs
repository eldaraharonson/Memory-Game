using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMemoryGame
{
    public class Card : Button
    {
        public int m_Row;
        public int m_Column;

        public Card(int i_Row, int i_Column)
        {
            m_Row = i_Row;
            m_Column = i_Column;
        }
    }
}
