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

        private static void success(ref Board io_Board, string i_FirstCell, string i_SecondCell, Player io_PlayingPlayer, Player i_WaitingPlayer)
        {
            io_Board.SetBooleanValue(i_FirstCell, true);
            io_Board.SetBooleanValue(i_SecondCell, true);
            io_PlayingPlayer.AddPoint();
            io_Board.RemoveLinkedListElement(i_FirstCell);
            io_Board.RemoveLinkedListElement(i_SecondCell); 
        }

        private static void failure(ref Board i_Board, string i_FirstCell, string i_SecondCell, Player i_PlayingPlayer, Player i_WaitingPlayer)
        {
            i_Board.SetBooleanValue(i_FirstCell, false);
            i_Board.SetBooleanValue(i_SecondCell, false);
        }
    }
}
