using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsMemoryGame
{
    public class Board
    {
       
        public char[,] m_BoardOfGame;
        public bool[,] m_ShowLettersOfBoard;
        public LinkedList<string> m_CellsThatAreNotDisplayed = new LinkedList<string>();
        private int m_BoardWidth;
        private int m_BoardLength;
        private char[] m_LettersToNumberDictionary = { 'A', 'B', 'C', 'D', 'E', 'F' };
        private char[] m_NumbersDictionary = { '1', '2', '3', '4', '5', '6' };

        public Board(int i_BoardWidth, int i_BoardLength)
        {
            m_BoardWidth = i_BoardWidth;
            m_BoardLength = i_BoardLength;
            m_BoardOfGame = new char[i_BoardLength, i_BoardWidth];
            m_ShowLettersOfBoard = new bool[i_BoardLength, i_BoardWidth];
            initializeBoardGame();
            LinkedListInitializer();
        }

        public void LinkedListInitializer()
        {
            for (int row = 0; row < m_BoardLength; row++)
            {
                for (int column = 0; column < m_BoardWidth; column++)
                {
                    m_CellsThatAreNotDisplayed.AddLast(row + "" + column);
                    
                }
            }
        }

        public void RemoveLinkedListElement(string cellToRemove)
        {
            m_CellsThatAreNotDisplayed.Remove(cellToRemove);
        }

        public void initializeBoardGame()
        {
            char[] lettersForGame = GetCharsArray();
            char row = '0';
            char column = '0';
            LinkedList<string> cellsThatAreEmpty = new LinkedList<string>();
            for (int i = 0; i < m_BoardLength; i++)
            {
                for (int j = 0; j < m_BoardWidth; j++)
                {
                    cellsThatAreEmpty.AddLast(char.ToString(row) + char.ToString(column));
                    column++;
                }

                column = '0';
                row++;
            }

            string cell = "";
            for (int i = 0; i < lettersForGame.Length; i++)
            {
                int randomNumber = 0;
                int timesCharHasBeenEntered = 0;
                while (timesCharHasBeenEntered < 2)
                {
                    timesCharHasBeenEntered++;
                    LinkedList<string>.Enumerator enumerator = cellsThatAreEmpty.GetEnumerator();
                    randomNumber = RandomNumber(0, cellsThatAreEmpty.Count);
                    int place = -1;
                    while (place < randomNumber)
                    {
                        enumerator.MoveNext();
                        place++;
                    }

                    cell = enumerator.Current;
                    int.TryParse(cell[0].ToString(), out int rowNumber);
                    int.TryParse(cell[1].ToString(), out int columnNumber);
                    m_BoardOfGame[rowNumber, columnNumber] = lettersForGame[i % lettersForGame.Length];
                    cellsThatAreEmpty.Remove(cell);
                }
            }
        }

        public char[] GetCharsArray()
        {
            string englishAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int numOfLettersForBoard = (m_BoardWidth * m_BoardLength) / 2;
            char[] lettersUsedForGame = new char[numOfLettersForBoard];
            for (int i = 0; i < numOfLettersForBoard; i++)
            {
                lettersUsedForGame[i] = englishAlphabet[i];
            }

            return lettersUsedForGame;
        }

        public int RandomNumber(int min, int max)
        {
            Thread.Sleep(100);
            Random random = new Random();
            return random.Next(min, max);
        }

        public void SetBooleanValue(string i_CellToUpdate, bool i_ValToUpdate)
        {
            int.TryParse(i_CellToUpdate[0].ToString(), out int row);
            int.TryParse(i_CellToUpdate[1].ToString(), out int column);
            m_ShowLettersOfBoard[row, column] = i_ValToUpdate;
        }

        public char GetCharOfCell(string i_CellToDisplay)
        {
            int.TryParse(i_CellToDisplay[0].ToString(), out int row);
            int.TryParse(i_CellToDisplay[1].ToString(), out int column);
            return m_BoardOfGame[row, column];
        }
    }
}
