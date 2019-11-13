using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.IO;

namespace BigBadBolts_Assign5
{

    public partial class Form1 : Form
    {
        public  Label label1;
        public Label lbScore;
        private GameArea gameArea;
        public Button btnStart;
        public Panel panel1;
        public System.Windows.Forms.Timer timer;
        public System.Diagnostics.Stopwatch gameTimer;
        public static System.Timers.Timer everySecond;
        public Label lbElimRows;

        const int WM_KEYDOWN = 0x100;

        private int score = 0;
        private int elimRows = 0;
        private Block nextBlock = null;
        private BlockArea pnlNext;

        public Form1()
        {
            InitializeComponent();
            Label.CheckForIllegalCrossThreadCalls = false;

            //It wont work!!!
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer("..\\..\\Tetris.wav");
            sp.Play();

            gameTimer = new Stopwatch();
            everySecond = new System.Timers.Timer(1000);
            everySecond.Elapsed += IncrementTime;
            everySecond.AutoReset = true;
            pnlNext = new BlockArea(4, 4);
            panel1.Controls.Add(this.pnlNext);
            pnlNext.CurrentBlock = null;
            pnlNext.Location = new System.Drawing.Point(5, 21);
            pnlNext.Name = "pnlNext";
            pnlNext.Size = new System.Drawing.Size(80, 80);
        }

 
        /**
         * This handles the function to start the game or pause it.
         */
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")//Handles the case of starting the game
            {
                lbTime.Text = "0:00";
                gameTimer.Reset();
                gameTimer.Start();
                everySecond.Enabled = true;
                lbTime.Visible = true;
                lbTimer.Visible = true;
                lbGameOver.Visible = false;
                label1.Visible = true;
                lbElimRows.Visible = true;
                lbScore.Visible = true;
                gameArea.Clear();
                pnlNext.Clear();
                score = 0;
                elimRows = 0;
                lbElimRows.Text = "Rows: 0";
                lbScore.Text = "Score: 0";
                gameArea.CurrentBlock = gameArea.NewBlock(gameArea, 3, 0);
                nextBlock = gameArea.NewBlock(pnlNext, 0, 0);
                gameArea.CurrentBlock.Show();

                nextBlock.Show();
                timer.Start();
                btnStart.Text = "Pause";
            }
            else if (btnStart.Text == "Pause")
            {
                timer.Stop();
                gameTimer.Stop();
                btnStart.Text = "Paused";
            }
            else if (btnStart.Text == "Paused")
            {
                timer.Start();
                gameTimer.Start();
                btnStart.Text = "Pause";
            }

        }
     
        /**
         * This function handles the pressing of keys and what to do for each
         */
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (btnStart.Text == "Paused" || btnStart.Text == "Start")//Dont do anything if the game isn't running
            {
                return false;
            }
            if (msg.Msg == WM_KEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.Left:
                        gameArea.MoveLeft();
                        return true;

                    case Keys.Right:
                        gameArea.MoveRight();
                        return true;

                    case Keys.Down:
                        gameArea.MoveDown();
                        return true;
                    case Keys.A:
                        gameArea.RotateBlockLeft();
                        return true;
                    case Keys.D:
                        gameArea.RotateBlockRight();
                        return true;
                }

            }
            return false;
        }

        /**
         * This updates the time label every second
         */
        public void IncrementTime(object sender, ElapsedEventArgs e)
        {
            lbTime.Text = String.Format("{0:#0}:{1:00}", gameTimer.Elapsed.Minutes, gameTimer.Elapsed.Seconds);
        }
        /**
         * This advances the game by one frame aka one tick
         */
        private void timer_Tick(object sender, EventArgs e)
        {
            gameArea.MoveDown();
        }

        /**
         * This stops the game, like a pause
         */
        private void gameArea_StopMoveEvent(object sender, EventArgs e)
        {
            timer.Stop();
        }

        /**
         * This is what happens when the game ends
         */
        private void gameArea_EndGame(object sender, EventArgs e)
        {
            timer.Stop();
            gameTimer.Stop();
            lbGameOver.Visible = true;
            label1.Visible = false;
            btnStart.Text = "Start";
        }

        /// 
        /**
         * This function creates a new block when the current block cannot do any more legal moves
         */
        private void gameArea_StartNewEvent(object sender, EventArgs e)
        {

            nextBlock.Hide();
            gameArea.CurrentBlock = nextBlock;
            gameArea.CurrentBlock.BlockArea = gameArea;
            gameArea.CurrentBlock.Location = new Point(3, 0);
            if (!gameArea.CurrentBlock.CanShow())//We cannot move the block anymore
            {
                timer.Stop();
                btnStart.Text = "Start";
                return;
            }
            gameArea.CurrentBlock.Show();

            nextBlock = gameArea.NewBlock(pnlNext, 0, 0);
            nextBlock.Show();
            timer.Start();
        }


        /**
         * This funciton handles when a row is eleiminated
         */
        private void gameArea_AddScoreEvent(object sender, AddScoreEventArgs e)
        {
            score += 5 * e.Count * e.Count + 5;
            elimRows += e.Count;
            lbElimRows.Text = "Rows: " + elimRows.ToString();
            lbScore.Text = "Score: " + score.ToString();
        }

        public void updateLabel(bool x)
        {
            label1.Visible = x;
        }

    
    }
}
