using MyFirstWindowsForm.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyFirstWindowsForm
{ 
    public partial class Form1 : Form
    {
        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;
        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public byte PlayCount;

        }
        enum enPlayer
        {
            Player1, Player2
        }

        enum enWinner
        {
            Player1, Player2, Draw, InProgress
        }
        public void ChangeImage(Button btn)
        {
            if (GameStatus.GameOver)
            {
                MessageBox.Show("Game Over Please Press Restart Game to Play again", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lblPlayerTurn.Text = "Player 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                        break;
                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblPlayerTurn.Text = "Player 1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (GameStatus.PlayCount == 9 && (!GameStatus.GameOver))
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }
        }

        void EndGame()
        {

            lblPlayerTurn.Text = "Game Over";
            switch (GameStatus.Winner)
            {
                case enWinner.Player1:
                    LblWinnerName.Text = "Player 1";
                    break;
                case enWinner.Player2:
                    LblWinnerName.Text = "Player 2";
                    break;
                default:
                    LblWinnerName.Text = "Draw";
                    break;

            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if ((btn1.Tag.ToString() != "?") && (btn1.Tag.ToString() == btn2.Tag.ToString()) && (btn1.Tag.ToString() == btn3.Tag.ToString()))
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }
            }

            GameStatus.GameOver = false;
            return false;
        }


        public void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button3, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;
        }
        private void RestButton(Button btn)
        {
            btn.Tag = "?";
            btn.Image = Resources.question_mark_96;
            btn.BackColor = Color.Transparent;
        }
        private void RestartGame()
        {
            RestButton(button1);
            RestButton(button2);
            RestButton(button3);
            RestButton(button4);
            RestButton(button5);
            RestButton(button6);
            RestButton(button7);
            RestButton(button8);
            RestButton(button9);

            PlayerTurn = enPlayer.Player1;
            lblPlayerTurn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.InProgress;
            LblWinnerName.Text = "In Progress";
        }
        public Form1()
        {
            InitializeComponent();

        }

        
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.FromArgb(255, 255, 255);

            Pen myPen = new Pen(White);

            myPen.Width = 10;

            myPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            myPen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(myPen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(myPen, 400, 460, 1050, 460);
            e.Graphics.DrawLine(myPen, 610, 140, 610, 620);
            e.Graphics.DrawLine(myPen, 840, 140, 840, 620);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeImage(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChangeImage(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeImage(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChangeImage(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeImage(button5);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeImage(button6);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeImage(button7);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeImage(button8);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeImage(button9);

        }

        private void btnRestarteGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }
    }