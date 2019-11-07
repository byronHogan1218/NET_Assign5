using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigBadBolts_Assign5
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Windows message number for key down event.
        /// </summary>
        const int WM_KEYDOWN = 0x100;
        /// <summary>
        /// Creates the main window instants.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            // pnlNext
            pnlNext = new BlockArea(4, 4);
            panel1.Controls.Add(this.pnlNext);
            //pnlNext.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            pnlNext.CurrentBlock = null;
            pnlNext.Location = new System.Drawing.Point(5, 21);
            pnlNext.Name = "pnlNext";
            pnlNext.Size = new System.Drawing.Size(80, 80);
            //pnlNext.TabIndex = 1;
        }
        /// <summary>
        /// Handles the start button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (btnStart.Text == "Start")
            {
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
                //this.Focus(); 
            }
            else if (btnStart.Text == "Pause")
            {
                timer.Stop();
                btnStart.Text = "Paused";
            }
            else if (btnStart.Text == "Paused")
            {
                timer.Start();
                btnStart.Text = "Pause";
            }

        }
        /// <summary>
        /// Handles the up/down/left/right key down event.
        /// </summary>
        /// <param name="msg">windows message</param>
        /// <param name="keyData">windows message data</param>
        /// <returns>true if the message is handled</returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (btnStart.Text == "Paused" || btnStart.Text == "Start")
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
                    case Keys.Up:
                        gameArea.RotateBlock();
                        return true;
                }

            }
            return false;
        }

        /// <summary>
        /// Handles the exit menu click event.
        /// </summary>
        /// <param name="sender">this form</param>
        /// <param name="e">empty event arg</param>
        private void miExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Forces the block move down at the timer tick.
        /// </summary>
        /// <param name="sender">this form</param>
        /// <param name="e">empty event arg</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            gameArea.MoveDown();
        }

        private void gameArea_StopMoveEvent(object sender, EventArgs e)
        {
            timer.Stop();
        }

        /// <summary>
        /// Creates a new block when the current block is stoped.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameArea_StartNewEvent(object sender, EventArgs e)
        {

            nextBlock.Hide();
            gameArea.CurrentBlock = nextBlock;
            gameArea.CurrentBlock.BlockArea = gameArea;
            gameArea.CurrentBlock.Location = new Point(3, 0);
            // cannot move anymore
            if (!gameArea.CurrentBlock.CanShow())
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
        /// <summary>
        /// Happens when there are rows eliminated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameArea_AddScoreEvent(object sender, AddScoreEventArgs e)
        {
            score += 5 * e.Count * e.Count + 5;
            elimRows += e.Count;
            lbElimRows.Text = "Rows: " + elimRows.ToString();
            lbScore.Text = "Score: " + score.ToString();
        }

        /// <summary>
        /// The game score
        /// </summary>
        private int score = 0;

        /// <summary>
        /// Eliminated rows count
        /// </summary>
        private int elimRows = 0;

        /// <summary>
        /// The next block to be dropped.
        /// </summary>
        private Block nextBlock = null;

        /// <summary>
        /// The display panel for the next block.
        /// </summary>
        private BlockArea pnlNext;
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
