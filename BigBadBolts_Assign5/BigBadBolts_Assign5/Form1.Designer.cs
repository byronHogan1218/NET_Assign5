namespace BigBadBolts_Assign5
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lbScore = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbElimRows = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.gameArea = new BigBadBolts_Assign5.GameArea();
            this.lbGameOver = new System.Windows.Forms.Label();
            this.lbTimer = new System.Windows.Forms.Label();
            this.lbTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gameArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Location = new System.Drawing.Point(11, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Next Shape";
            this.label1.Visible = false;
            // 
            // lbScore
            // 
            this.lbScore.AutoSize = true;
            this.lbScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbScore.Location = new System.Drawing.Point(12, 159);
            this.lbScore.Name = "lbScore";
            this.lbScore.Size = new System.Drawing.Size(64, 18);
            this.lbScore.TabIndex = 4;
            this.lbScore.Text = "Score: 0";
            this.lbScore.Visible = false;
            // 
            // btnStart
            // 
            this.btnStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnStart.Location = new System.Drawing.Point(15, 254);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.TabStop = false;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbTime);
            this.panel1.Controls.Add(this.lbTimer);
            this.panel1.Controls.Add(this.lbElimRows);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.lbScore);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(211, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 410);
            this.panel1.TabIndex = 11;
            // 
            // lbElimRows
            // 
            this.lbElimRows.AutoSize = true;
            this.lbElimRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbElimRows.Location = new System.Drawing.Point(13, 141);
            this.lbElimRows.Name = "lbElimRows";
            this.lbElimRows.Size = new System.Drawing.Size(63, 18);
            this.lbElimRows.TabIndex = 11;
            this.lbElimRows.Text = "Rows: 0";
            this.lbElimRows.Visible = false;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // gameArea
            // 
            this.gameArea.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.gameArea.Controls.Add(this.lbGameOver);
            this.gameArea.CurrentBlock = null;
            this.gameArea.Location = new System.Drawing.Point(0, 0);
            this.gameArea.Name = "gameArea";
            this.gameArea.Size = new System.Drawing.Size(200, 400);
            this.gameArea.TabIndex = 9;
            this.gameArea.StopMoveEvent += new System.EventHandler(this.gameArea_StopMoveEvent);
            this.gameArea.StartNewEvent += new System.EventHandler(this.gameArea_StartNewEvent);
            this.gameArea.EndGame += new System.EventHandler(this.gameArea_EndGame);
            this.gameArea.AddScoreEvent += new BigBadBolts_Assign5.GameArea.AddScoreEventHandler(this.gameArea_AddScoreEvent);
            // 
            // lbGameOver
            // 
            this.lbGameOver.AutoSize = true;
            this.lbGameOver.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGameOver.ForeColor = System.Drawing.Color.Red;
            this.lbGameOver.Location = new System.Drawing.Point(7, 193);
            this.lbGameOver.Name = "lbGameOver";
            this.lbGameOver.Size = new System.Drawing.Size(190, 37);
            this.lbGameOver.TabIndex = 0;
            this.lbGameOver.Text = "Game Over";
            this.lbGameOver.Visible = false;
            // 
            // lbTimer
            // 
            this.lbTimer.AutoSize = true;
            this.lbTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTimer.Location = new System.Drawing.Point(3, 193);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(49, 18);
            this.lbTimer.TabIndex = 12;
            this.lbTimer.Text = "Time: ";
            this.lbTimer.Visible = false;
            // 
            // lbTime
            // 
            this.lbTime.AutoSize = true;
            this.lbTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTime.Location = new System.Drawing.Point(44, 215);
            this.lbTime.Name = "lbTime";
            this.lbTime.Size = new System.Drawing.Size(36, 18);
            this.lbTime.TabIndex = 13;
            this.lbTime.Text = "time";
            this.lbTime.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(334, 410);
            this.Controls.Add(this.gameArea);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Tetris";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gameArea.ResumeLayout(false);
            this.gameArea.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lbGameOver;
        private System.Windows.Forms.Label lbTime;
        private System.Windows.Forms.Label lbTimer;



        //public System.Windows.Forms.Label label1;
        //public System.Windows.Forms.Label lbScore;
        //private GameArea gameArea;
        //public System.Windows.Forms.Button btnStart;
        //public System.Windows.Forms.Panel panel1;
        //public System.Windows.Forms.Timer timer;
        //public System.Windows.Forms.Label lbElimRows;

    }
}

