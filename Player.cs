using System.Drawing;

namespace WindowsMemoryGame
{
    public class Player
    {
        public Color m_BackgroundColor;
        private string m_PlayerName;
        private bool m_IsComputer;
        private int m_Points;

        public Player(string i_Name, bool i_IsComputer, Color i_BackgroundColor)
        {
            m_PlayerName = i_Name;
            m_IsComputer = i_IsComputer;
            m_Points = 0;
            m_BackgroundColor = i_BackgroundColor;
        }

        public int GetPoints()
        {
            return m_Points;
        }

        public void AddPoint()
        {
            m_Points++;
        }

        public string GetPlayerName()
        {
            return m_PlayerName;
        }

        public bool GetIfIsComputer()
        {
            return m_IsComputer;
        }
    }
}