using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsMemoryGame
{
    public partial class SettingsForm : Form
    {
        private readonly string[] m_BoardSizes = {"4x4", "4x5", "4x6", "5x4", "5x6", "6x4", "6x5", "6x6"};
        private int m_CounterForBoardSizes = 0;

        public SettingsForm()
        {
            InitializeComponent();
        }

        public void BoardSizeButton_Click(object sender, EventArgs e)
        {
            if (m_CounterForBoardSizes == 7)
            {
                m_CounterForBoardSizes = 0;
            }
            else
            {
                m_CounterForBoardSizes++;
            }

            (sender as Button).Text = m_BoardSizes[m_CounterForBoardSizes];
        }

        private void againstAFriendButton_Click(object sender, EventArgs e)
        {
            if (string.Equals((sender as Button).Text, "Against a Friend"))
            {
                (sender as Button).Text = "Against Computer";
                secondPlayerNameTextBox.Text = string.Empty;
                secondPlayerNameTextBox.Enabled = true;
            }
            else
            {
                (sender as Button).Text = "Against a Friend";
                secondPlayerNameTextBox.Text = "-computer-";
                secondPlayerNameTextBox.Enabled = false;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            int.TryParse(BoardSizeButton.Text[0].ToString(), out int boardLength);
            int.TryParse(BoardSizeButton.Text[2].ToString(), out int boardWidth);
            GameForm gameForm = new GameForm(boardLength, boardWidth, this.firstPlayerNameTextBox.Text, this.secondPlayerNameTextBox.Text);
            this.Hide();
            gameForm.ShowDialog();
        }
    }
}
