using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsMemoryGame
{
    public class Turn
    {
        /// <summary>
        /// Picks a random cell from the list of cells that are not rendered (haven't been matched)
        /// </summary>
        /// <returns>string representing the cell location of the random cell</returns>
        public static string GetRandomCell(Board i_Board)
        {
            Random random = new Random();
            int randomIndex = random.Next(1, i_Board.m_CellsThatAreNotDisplayed.Count() + 1);
            LinkedList<string>.Enumerator enumerator = i_Board.m_CellsThatAreNotDisplayed.GetEnumerator();
            int place = 0;
            string cell = "";
            while (place < randomIndex)
            {
                enumerator.MoveNext();
                cell = enumerator.Current;
                place++;
            }

            return cell;
        }

        /// <summary>
        /// Recieves two strings representing cell spot and checks if they present the same letter
        /// </summary>
        public static bool checkIfCellsAreEqual(ref Board i_Board, string i_FirstCell, string i_SecondCell, Player i_PlayingPlayer, Player i_WaitingPlayer)
        {
            if (string.Equals(i_Board.GetCharOfCell(i_FirstCell), i_Board.GetCharOfCell(i_SecondCell)))
            {
                success(ref i_Board, i_FirstCell, i_SecondCell, i_PlayingPlayer, i_WaitingPlayer);
            }
            else
            {
                failure(ref i_Board, i_FirstCell, i_SecondCell, i_PlayingPlayer, i_WaitingPlayer);
            }

            return string.Equals(i_Board.GetCharOfCell(i_FirstCell), i_Board.GetCharOfCell(i_SecondCell));
        }

        /// <summary>
        /// Is called when a pair has been matched
        /// </summary>
        private static void success(ref Board io_Board, string i_FirstCell, string i_SecondCell, Player io_PlayingPlayer, Player i_WaitingPlayer)
        {
            // the 2 cards that have been matched are rendered
            io_Board.SetBooleanValue(i_FirstCell, true);
            io_Board.SetBooleanValue(i_SecondCell, true);
            io_PlayingPlayer.AddPoint();
            io_Board.RemoveLinkedListElement(i_FirstCell);
            io_Board.RemoveLinkedListElement(i_SecondCell); 
        }

        /// <summary>
        /// Is called when 2 cards that were picked don't match
        /// </summary>
        private static void failure(ref Board i_Board, string i_FirstCell, string i_SecondCell, Player i_PlayingPlayer, Player i_WaitingPlayer)
        {
            // Letters of the cards are hidden again
            i_Board.SetBooleanValue(i_FirstCell, false);
            i_Board.SetBooleanValue(i_SecondCell, false);
        }
    }
}
