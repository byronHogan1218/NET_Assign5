using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BigBadBolts_Assign5
{
    /*
    * This is where all the blocks will be displayed and manipulated.
    * The game area does not include the title screen, the score,
    * or the Next Block section.
    */
    public class GameArea : BlockArea
    {
        // This is the number of rows in the game area.
        public const int DEF_ROWS = 20;
        // This is the number of columns in the game area.
        public const int DEF_COLS = 10;


        /**
         * Create a two-dimensional array of pre-defined size.
         * add an event handler to show and hide the squares
         * in each block.
         */
        public GameArea() : base(DEF_ROWS, DEF_COLS)
        {
        }

        //Check to see if the block has reached the bottom and needs to stop moving
        public event EventHandler StopMoveEvent;
 
        //Check to see when a line is eliminated
        public event EventHandler StartNewEvent;

        //End the game
        public event EventHandler EndGame;
        
        //Handles the score, including adding the score
        public delegate void AddScoreEventHandler(object sender, AddScoreEventArgs e);
   
        //Detects a line being eliminated and needing to add that to the score
        public event AddScoreEventHandler AddScoreEvent;


      
        /**
         * This function is used to create a new random block to be placed into
         * the game space.
         * Paramters: area - The block area being displayed
         *            x    - The initial x value of the block
         *            y    - The initial y value of the black
         */
        public Block NewBlock(BlockArea area, int x, int y)
        {
            Block newBlk = null;
            BlockType type = (BlockType)rnd.Next(7);
            switch (type)
            {
                case BlockType.I:
                    newBlk = new IBlock(area, x, y);
                    break;
                case BlockType.J:
                    newBlk = new JBlock(area, x, y);
                    break;
                case BlockType.L:
                    newBlk = new LBlock(area, x, y);
                    break;
                case BlockType.S:
                    newBlk = new SBlock(area, x, y);
                    break;
                case BlockType.Z:
                    newBlk = new ZBlock(area, x, y);
                    break;
                case BlockType.O:
                    newBlk = new OBlock(area, x, y);
                    break;
                case BlockType.T:
                    newBlk = new TBlock(area, x, y);
                    break;
            }
            return newBlk;
        }
  
        /**
         * This function will move a block left one space
         */
        public void MoveLeft()
        {
            if (CurrentBlock != null)
                CurrentBlock.Left();
        }

        /**
        * This function will move a block right one space
        */
        public void MoveRight()
        {
            if (CurrentBlock != null)
                CurrentBlock.Right();
        }


        /**
         * This function moves a block down. It checks if it reaches the bottom
         * and if it does, then it fires the StopMoveEvent. It also checks to 
         * see if any lines need to be eliminated
         */
        public void MoveDown()
        {
            if (CurrentBlock == null)
            {
                return;
            }
            if (CurrentBlock.CanMoveDown())
            {
                CurrentBlock.Down();
            }
            else
            {
                if (StopMoveEvent != null)
                {
                    StopMoveEvent(this, null);
                }
                EliminateLines(CurrentBlock.BottomIndex());
                CurrentBlock = null;
                if (StartNewEvent != null)
                {
                    StartNewEvent(this, null);
                }
                if(CurrentBlock.CanMoveDown() == false)//This fires if the game is over
                {
                    EndGame(this,null);
                }
            }

        }

        /**
         * This function is used to rotate the block
         */
        public void RotateBlock()
        {
            if (CurrentBlock != null)
                CurrentBlock.Rotate();
        }

      
        /**
         * This is where the lines are eliminated form the game area
         * Parameters: row - where to start checking to delete a row
         */
        public void EliminateLines(int row)
        {
            int upper = Math.Max(row - 3, 0);
            int elimCount = 0;
            for (int i = row; i >= upper; i--)
            {
                bool elim = true;
                for (int j = 0; j < Cols; j++)
                {
                    if (!GameArray[i, j].Visible)
                    {
                        elim = false;
                        break;
                    }
                }
                if (!elim)
                {
                    continue;
                }
                elimCount++;
                for (int j = 0; j < Cols; j++)
                    GameArray[i, j].ClearEvents(); // unregister events, prevent memory leak.
                for (int k = i; k > 0; k--)
                    for (int j = 0; j < Cols; j++)
                    {
                        GameArray[k, j] = GameArray[k - 1, j];
                        GameArray[k, j].Location = new Point(j, k);
                    }
                for (int j = 0; j < Cols; j++)
                {
                    GameArray[0, j] = new Square(j, 0);
                    GameArray[0, j].ShowEvent += new EventHandler(ShowSquare);
                    GameArray[0, j].HideEvent += new EventHandler(HideSquare);
                }
                i++; // one line is eliminated, so recheck this line.
                upper++; // the upper bound is moved down
                Refresh();
            }
            if (elimCount != 0 && AddScoreEvent != null)
            {
                AddScoreEvent(this, new AddScoreEventArgs(elimCount));
            }

        }

        /** 
         * This function is used to generate a rondom number
         */
        private Random rnd = new Random((int)DateTime.Now.Ticks);

    }

    /**
     * This is used to eliminate lines on the game board
     */
    public class AddScoreEventArgs : EventArgs
    {
        //count is how many lines to be eliminated
        public int Count { get; set; }

        //public accesser
        public AddScoreEventArgs(int count)
        {
            Count = count;
        }
    }
}

