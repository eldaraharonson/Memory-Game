using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMemoryGame
{
    public partial class GameForm : Form
    {
        private int m_BoardLength;
        private int m_BoardWidth;
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private Player m_CurrentPlayer;
        private Player m_WaitingPlayer;
        private string m_FirstCardCell;
        private int m_CardsPicked = 0;
        private Card[,] m_Cards; 
        private Board m_Board;
        private Label m_CurrentPlayerLabel;
        private Label m_FirstPlayerLabel;
        private Label m_SecondPlayerLabel;
        private Card m_FirstCardPicked;
        private Card m_SecondCardPicked;

        public object MessageBoxResult { get; private set; }

        public GameForm(int i_BoardLength, int i_BoardWidth, string i_FirstPlayerName, string i_SecondPlayerName)
        {
            InitializeComponent();
            m_BoardLength = i_BoardLength;
            m_BoardWidth = i_BoardWidth;
            m_FirstPlayer = new Player(i_FirstPlayerName, false, Color.LightGreen);
            if (string.Equals(i_SecondPlayerName, "-computer-"))
            {
                m_SecondPlayer = new Player(i_SecondPlayerName, true, Color.Plum);
            }
            else
            {
                m_SecondPlayer = new Player(i_SecondPlayerName, false, Color.Plum);
            }

            m_CurrentPlayer = m_FirstPlayer;
            m_WaitingPlayer = m_SecondPlayer;
            m_Cards = new Card[m_BoardLength, m_BoardWidth]; 
            m_Board = new Board(m_BoardWidth, m_BoardLength);
            SetSize();
            AddLabels();
            AddButtons();
        }

        public void SetSize()
        {
            this.Height = 210 + m_BoardLength * 60;
            this.Width = 115 + m_BoardWidth * 50;
        }

        public void AddButtons()
        {
            for (int i = 0; i < m_BoardLength; i++)
            {
                for (int j = 0; j < m_BoardWidth; j++)
                {
                    Card newCard = new Card(i, j);
                    newCard.Height = 55;
                    newCard.Width = 55;
                    newCard.Top = 10 + i * 65;
                    newCard.Left = 10 + j * 65;
                    newCard.BackColor = Color.FromArgb(200, 200, 200);
                    m_Cards[i, j] = newCard;
                    newCard.Click += NewCard_Click;
                    this.Controls.Add(newCard);
                }
            }
        }
        
        private void NewCard_Click(object sender, EventArgs e)
        {
            
            m_CardsPicked++;
            if (m_CardsPicked == 1)
            {
                m_FirstCardPicked = sender as Card;
                m_FirstCardPicked.Text = m_Board.m_BoardOfGame[m_FirstCardPicked.m_Row, m_FirstCardPicked.m_Column].ToString();
                m_FirstCardCell = m_FirstCardPicked.m_Row + "" + m_FirstCardPicked.m_Column;
                
            }
            else
            {
                m_SecondCardPicked = sender as Card;
                m_SecondCardPicked.Text = m_Board.m_BoardOfGame[m_SecondCardPicked.m_Row, m_SecondCardPicked.m_Column].ToString();
                Application.DoEvents();
                if (!string.Equals(m_SecondCardPicked.m_Row + "" + m_SecondCardPicked.m_Column, m_FirstCardCell))
                {
                    m_CardsPicked = 0;
                    HandleTurn(m_SecondCardPicked.m_Row + "" + m_SecondCardPicked.m_Column);
                }
            }
        }

        public void HandleTurn(string i_SecondCardCell)
        {
            if (Turn.checkIfCellsAreEqual(ref m_Board, m_FirstCardCell, i_SecondCardCell, m_CurrentPlayer, m_WaitingPlayer))
            {
                m_FirstPlayerLabel.Text = m_FirstPlayer.GetPlayerName() + "'s score: " + m_FirstPlayer.GetPoints();
                m_SecondPlayerLabel.Text = m_SecondPlayer.GetPlayerName() + "'s score: " + m_SecondPlayer.GetPoints();
                m_FirstCardPicked.BackColor = m_CurrentPlayer.m_BackgroundColor;
                m_SecondCardPicked.BackColor = m_CurrentPlayer.m_BackgroundColor;
                if (!m_Board.m_CellsThatAreNotDisplayed.Any())
                {
                    ShowResults();
                }
            }
            else
            {
                Player temp = m_CurrentPlayer;
                m_CurrentPlayer = m_WaitingPlayer;
                m_WaitingPlayer = temp;
                m_CurrentPlayerLabel.Text = "Current Player: " + m_CurrentPlayer.GetPlayerName();
                m_CurrentPlayerLabel.BackColor = m_CurrentPlayer.m_BackgroundColor;
                Thread.Sleep(1500);
            }

            for (int i = 0; i < m_BoardLength; i++)
            {
                for (int j = 0; j < m_BoardWidth; j++)
                {
                    if (m_Board.m_ShowLettersOfBoard[i, j])
                    {
                        m_Cards[i, j].Text = m_Board.m_BoardOfGame[i, j].ToString();
                    }
                    else if (m_Board.m_CellsThatAreNotDisplayed.Contains(i + "" + j))
                    {
                        m_Cards[i, j].Text = string.Empty;
                    }
                }
            }

            if (m_CurrentPlayer.GetIfIsComputer())
            {
                HandleComputerTurn();
            }
        }

        public void HandleComputerTurn()
        {
            m_FirstCardCell = Turn.GetRandomCell(m_Board);
            Thread.Sleep(200);
            string secondCell;
            do
            {
                secondCell = Turn.GetRandomCell(m_Board);
            }
            while (string.Equals(m_FirstCardCell, secondCell));
            
            int.TryParse(m_FirstCardCell[0].ToString(), out int firstCellRow);
            int.TryParse(m_FirstCardCell[1].ToString(), out int firstCellColumn);
            int.TryParse(secondCell[0].ToString(), out int secondCellRow);
            int.TryParse(secondCell[1].ToString(), out int secondCellColumn);
            m_FirstCardPicked = m_Cards[firstCellRow, firstCellColumn];
            m_SecondCardPicked = m_Cards[secondCellRow, secondCellColumn];
            m_Cards[firstCellRow, firstCellColumn].Text = m_Board.m_BoardOfGame[firstCellRow, firstCellColumn].ToString();
            m_Cards[secondCellRow, secondCellColumn].Text = m_Board.m_BoardOfGame[secondCellRow, secondCellColumn].ToString();
            m_CurrentPlayerLabel.Text = "Current Player: " + m_CurrentPlayer.GetPlayerName();
            m_CurrentPlayerLabel.BackColor = m_CurrentPlayer.m_BackgroundColor;
            Application.DoEvents();
            Thread.Sleep(2000);
            HandleTurn(secondCell);
        }
        
        public void AddLabels()
        {
            m_CurrentPlayerLabel = new Label();
            m_CurrentPlayerLabel.Text = "Current Player: " + m_FirstPlayer.GetPlayerName();
            m_CurrentPlayerLabel.BackColor = Color.LightGreen;
            m_CurrentPlayerLabel.Height = 20;
            m_CurrentPlayerLabel.Width = 150;
            m_CurrentPlayerLabel.Left = 15;
            m_CurrentPlayerLabel.Top = m_BoardLength * 70;
            this.Controls.Add(m_CurrentPlayerLabel);
            m_FirstPlayerLabel = new Label();
            m_FirstPlayerLabel.Text = m_FirstPlayer.GetPlayerName() + "'s score: " + m_FirstPlayer.GetPoints();
            m_FirstPlayerLabel.BackColor = Color.LightGreen;
            m_FirstPlayerLabel.Height = 20;
            m_FirstPlayerLabel.Width = 150;
            m_FirstPlayerLabel.Left = 15;
            m_FirstPlayerLabel.Top = m_CurrentPlayerLabel.Top + 40;
            this.Controls.Add(m_FirstPlayerLabel);
            m_SecondPlayerLabel = new Label();
            m_SecondPlayerLabel.Text = m_SecondPlayer.GetPlayerName() + "'s score: " + m_SecondPlayer.GetPoints();
            m_SecondPlayerLabel.BackColor = Color.Plum;
            m_SecondPlayerLabel.Height = 20;
            m_SecondPlayerLabel.Width = 150;
            m_SecondPlayerLabel.Left = 15;
            m_SecondPlayerLabel.Top = m_FirstPlayerLabel.Top + 40;
            this.Controls.Add(m_SecondPlayerLabel);
        }

        public void ShowResults()
        {
            DialogResult result = MessageBox.Show(getEndGameMessage(), "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
                GameForm gameForm = new GameForm(m_BoardLength, m_BoardWidth, m_FirstPlayer.GetPlayerName(), m_SecondPlayer.GetPlayerName());
                gameForm.ShowDialog();
            }

            if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }

        public string getEndGameMessage()
        {
            string endGameMessage;
            if (m_FirstPlayer.GetPoints() != m_SecondPlayer.GetPoints())
            {
                Player winner;
                if (m_FirstPlayer.GetPoints() > m_SecondPlayer.GetPoints())
                {
                    winner = m_FirstPlayer;
                }
                else
                {
                    winner = m_SecondPlayer;
                }

                endGameMessage = string.Format(
@"{0} has won with {1} points!
Would you like to play another round?",
                winner.GetPlayerName(), winner.GetPoints().ToString());
            }
            else
            {
                endGameMessage = string.Format(
@"We have a tie!
Would you like to play another round?");
            }

            return endGameMessage;
        }
    }
}
