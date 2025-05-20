using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class frmMainTicTacToe : Form
    {
        public frmMainTicTacToe()
        {
            InitializeComponent();
        }

        private void frmMainTicTacToe_Paint(object sender, PaintEventArgs e)
        {
            Color color = Color.White;
            Pen pen = new Pen(color);
            pen.Width = 7;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            // Vertical Lines 
            e.Graphics.DrawLine(pen, 560, 55, 560, 380);
            e.Graphics.DrawLine(pen, 710, 55, 710, 380);

            // Horizantail Lines
            e.Graphics.DrawLine(pen, 425, 158, 850, 158);
            e.Graphics.DrawLine(pen, 425, 275, 850, 275);
        }

        enum enPlayerTurn
        {
            Player1 = 1,
            Player2 = 2
        }

        enum enTheWinnerOfTheGame
        {
            Player1 = 1,
            Player2 = 2,
            Draw = 3,
            InProgress = 4
        }

        struct StGameStatus
        {
            public enTheWinnerOfTheGame AWinner;
            public bool IsGameOver;
            public byte PlayCounter;
        }

        struct StGameRounds
        {
            public byte Player1Counter;
            public byte Player2Counter;

            public StGameRounds(byte player1Counter=0, byte player2Counter=0)
            {
                Player1Counter = player1Counter;
                Player2Counter = player2Counter;
            }
        }

        StGameRounds GameRounds;

        enPlayerTurn PlayerTurn = enPlayerTurn.Player1;
        StGameStatus GameStatus;

        private bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag == btn2.Tag && btn1.Tag == btn3.Tag)
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.AWinner = enTheWinnerOfTheGame.Player1;
                    GameStatus.IsGameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.AWinner = enTheWinnerOfTheGame.Player2;
                    GameStatus.IsGameOver = true;
                    EndGame();
                    return true;
                }
            }

            GameStatus.IsGameOver = false;
            return false;

        }

        private void DisableButton()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;

            lbTurn.Enabled = false;
            lbWinner.Enabled = false;
        }

        private void EnableButton()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;

            lbTurn.Enabled = true;
            lbWinner.Enabled = true;
        }

        private void EndGame()
        {
            lbTurn.Text = "Game Over";
            switch(GameStatus.AWinner)
            {
                case enTheWinnerOfTheGame.Player1:
                    lbWinner.Text = "Player1";
                    GameRounds.Player1Counter++;
                    lbPlayer1Counter.Text = GameRounds.Player1Counter.ToString();
                    lbPlayer2Counter.Text = GameRounds.Player2Counter.ToString();
                    DisableButton();
                    break;
                case enTheWinnerOfTheGame.Player2:
                    lbWinner.Text = "Player2";
                    GameRounds.Player2Counter++;
                    lbPlayer1Counter.Text = GameRounds.Player1Counter.ToString();
                    lbPlayer2Counter.Text = GameRounds.Player2Counter.ToString();
                    DisableButton();
                    break;
                default:
                    lbWinner.Text = "Draw";
                    lbPlayer1Counter.Text = GameRounds.Player1Counter.ToString();
                    lbPlayer2Counter.Text = GameRounds.Player2Counter.ToString();
                    DisableButton();
                    break;
            }
            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CheckWinner()
        {
            // Check Rows
            CheckValues(button1, button2, button3);
            CheckValues(button4, button5, button6);
            CheckValues(button7, button8, button9);

            // Check Coulums
            CheckValues(button1, button4, button7);
            CheckValues(button2, button5, button8);
            CheckValues(button3, button6, button9);

            // Check Dailoge
            CheckValues(button1, button5, button9);
            CheckValues(button3, button5, button7);
        }

        private void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?") 
            {
                switch(PlayerTurn)
                {
                    case enPlayerTurn.Player1:
                        btn.BackgroundImage = Resources.X;
                        PlayerTurn = enPlayerTurn.Player2;
                        lbTurn.Text = "Player2";
                        btn.Tag = "X";
                        GameStatus.PlayCounter++;
                        CheckWinner();
                        break;
                    case enPlayerTurn.Player2:
                        btn.BackgroundImage = Resources.O;
                        PlayerTurn = enPlayerTurn.Player1;
                        lbTurn.Text = "Player1";
                        btn.Tag = "O";
                        GameStatus.PlayCounter++;
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("This is choiced already", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(GameStatus.PlayCounter == 9)
            {
                GameStatus.IsGameOver = true;
                GameStatus.AWinner = enTheWinnerOfTheGame.Draw;
                EndGame();
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        private void frmMainTicTacToe_Load(object sender, EventArgs e)
        {

        }

        private void ResetButton(Button btn)
        {
            btn.BackgroundImage = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
            EnableButton();
        }

        private void ResetGame()
        {
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            PlayerTurn = enPlayerTurn.Player1;
            lbTurn.Text = "Player1";
            GameStatus.PlayCounter = 0;
            GameStatus.IsGameOver = false;
            GameStatus.AWinner = enTheWinnerOfTheGame.InProgress;
            lbWinner.Text = "In Progress";
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            lbPlayer1Counter.Text= "0";
            lbPlayer2Counter.Text= "0";
        }
    }
}