using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

///*******************************************************************
//*                                                                  *
//*  CSCI 473-1/504-1       Assignment 5                Fall   2019  *
//*                                                                  *
//*                                                                  *
//*  Program Name:  Tetris                                           *
//*                                                                  *
//*  Programmer:    Byron Hogan,  z1825194                           *
//*                 Margaret Higginbotham, z1793581                  *
//*                                                                  *
//*******************************************************************/

namespace BigBadBolts_Assign5
{
    //These are the types of Blocks
    public enum BlockType { J, L, O, T, Z, S, I }

    // This is a block in the game. it handles everything a block can potential do
    public abstract class Block
    {
        private int patternId = 0;
        private BlockArea blockArea;
        private Color foreColor;
        private Color centerColor;
        private Square[] squares = null;
        private Point location;
        private List<Point[]> patterns;

        /**
         * This the constructor for a new block. It must be passed in the coordinates of the blcok
         * Parameters:  blockArea - the area that the block will take up
         *              x         - the starting x location of the block
         *              y         - the starting y location of the block
         */
        public Block(BlockArea blockArea, int x, int y)
        {
            this.blockArea = blockArea;
            location.X = x;
            location.Y = y;
            patterns = new List<Point[]>();
            InitPatterns();
            squares = GetSquares(location.X, location.Y, 0);
        }


        /**
         * This function gets the locaton of a block
         */
        public Point Location
        {
            get { return location; }
            set
            {
                location = value;
                squares = GetSquares(location.X, location.Y, patternId);
            }
        }

        public bool CanShow()
        {
            return CanMove(squares);
        }

        /**
         * This gets the foreground color
         */
        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }
     
        /**
         * This gets the centercolor of each block
         */
        public Color CenterColor
        {
            get { return centerColor; }
            set { centerColor = value; }
        }
 
        /**
         * This gets the squares that make up the blocks location
         */
        public Square[] Squares
        {
            get { return squares; }
        }

        /** 
         * This is used to tell what blockarea the block belongs to
         */
        public BlockArea BlockArea
        {
            get { return blockArea; }
            set
            {
                blockArea = value;
                squares = GetSquares(location.X, location.Y, 0);
            }
        }
  
        
        /**
         * This function is used to make sure there are no collisions 
         * in the next spot to move the block to
         * Parameters: dest - the is space that the block wants to move to
         * Returns:    true - if the move is legal
         *             false- if we cannot move the block
         */
        public bool CanMove(Square[] dest)
        {
            for (int i = 0; i < dest.Length; i++)
            {
                if (dest[i] == null || dest[i].Visible)//checks to see if the block is good to move there
                    return false;
            }
            return true;
        }


        /**
         * This function is used to move a block to the left
         */
        public void Left()
        {
            Square[] dest = GetSquares(location.X - 1, location.Y, patternId);
            Hide();
            if (CanMove(dest))
            {
                location.X -= 1;
                squares = dest;
            }
            Show();
        }

        /**
         * This function is used to move a block to the right
         */
        public void Right()
        {
            Square[] dest = GetSquares(location.X + 1, location.Y, patternId);
            Hide();
            if (CanMove(dest))
            {
                location.X += 1;
                squares = dest;
            }
            Show();
        }

         /**
         * This function is used to move a block down
         */
        public void Down()
        {
            Square[] dest = GetSquares(location.X, location.Y + 1, patternId);
            Hide();

            if (CanMove(dest))
            {
                location.Y += 1;
                squares = dest;
            }
            Show();
        }

 
        /**
         * Makes sure the block can move down a line
         */
        public bool CanMoveDown()
        {
            Hide();
            Square[] dest = GetSquares(location.X, location.Y + 1, patternId);
            bool down = CanMove(dest);
            Show();
            return down;
        }


        /**
         * This is usd to rotate the block Left
         */
        public void RotateLeft()
        {
            if (patterns.Count == 0)
                return;
            int pid = 0;
            if (patternId - 1 < 0 )
            {
                pid = patterns.Count-1;
            }
            else
            {
                pid = (patternId - 1);
            }
            Hide();
            Square[] dest = GetSquares(location.X, location.Y, pid);
            if (CanMove(dest))
            {
                patternId = pid;
                squares = dest;
            }
            Show();
        }

         /**
         * This is used to rotate the block right
         */
        public void RotateRight()
        {
            if (patterns.Count == 0)
                return;
            int pid = (patternId + 1) % patterns.Count;
            Hide();
            Square[] dest = GetSquares(location.X, location.Y, pid);
            if (CanMove(dest))
            {
                patternId = pid;
                squares = dest;
            }
            Show();
        }


        /**
         * This function shows the block
         */
        public void Show()
        {
            foreach (Square sq in squares)
                if (sq != null)
                    sq.Show(foreColor, centerColor);
        }

        /**
         * This function hides the block
         */
        public void Hide()
        {
            foreach (Square sq in squares)
                if (sq != null)
                    sq.Hide();
        }

  
        /**
         * This function determines the bottom index of the square
         * Returns: The lowest index in the square
         */
        public int BottomIndex()
        {
            int result = squares[0].Location.Y;
            for (int i = 1; i < 4; i++)
                if (squares[i].Location.Y > result)
                    result = squares[i].Location.Y;
            return result;
        }


        /**
         * Initialize the patterns of the blcok
         */
        protected abstract void InitPatterns();

    
        /**
         * This function figures out the current pattern of the squares
         */
        protected virtual Square[] GetSquares(int x, int y, int patternId)
        {
            if (patterns.Count == 0)
                return null;
            Square[] result = new Square[4];
            for (int i = 0; i < 4; i++)
                result[i] = blockArea.GetSquare(x + patterns[patternId][i].X, y + patterns[patternId][i].Y);
            return result;
        }

    
        protected List<Point[]> Patterns
        {
            get { return patterns; }
        }

     

    }

    ////////////////////////////////////////////////////////////////////////
    ///                                                                  ///
    ///       The following is the definiton of the differnt blocks      ///
    ///                                                                  ///
    ////////////////////////////////////////////////////////////////////////

    /**
     * This is a T block
     */
    public class TBlock : Block
    {
        public TBlock(BlockArea blockArea, int x, int y)
            : base(blockArea, x, y)
        {
            ForeColor = Color.Purple;
            CenterColor = Color.Purple;
        }

        protected override void InitPatterns()
        {
            Patterns.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(1, 2) });
            Patterns.Add(new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(1, 2) });
            Patterns.Add(new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
            Patterns.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) });
        }
    }
    /**
    * This is a I block
    */
    public class IBlock : Block
    {
        public IBlock(BlockArea blockArea, int x, int y)
            : base(blockArea, x, y)
        {
            ForeColor = Color.Cyan;
            CenterColor = Color.Cyan;
        }
        protected override void InitPatterns()
        {
            Patterns.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(3, 1) });
            Patterns.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(1, 3) });
        }
    }
    /**
     * This is a J block
    */
    public class JBlock : Block
    {
        public JBlock(BlockArea blockArea, int x, int y)
            : base(blockArea, x, y)
        {
            ForeColor = Color.Blue;
            CenterColor = Color.Blue;
        }

        protected override void InitPatterns()
        {
            Patterns.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(0, 2) });
            Patterns.Add(new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
            Patterns.Add(new Point[] { new Point(1, 0), new Point(2, 0), new Point(1, 1), new Point(1, 2) });
            Patterns.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(2, 2) });
        }

    }
    /**
      * This is a L block
      */
    public class LBlock : Block
    {
        public LBlock(BlockArea blockArea, int x, int y)
            : base(blockArea, x, y)
        {
            ForeColor = Color.DarkOrange;
            CenterColor = Color.DarkOrange;
        }
        protected override void InitPatterns()
        {
            Patterns.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(1, 2), new Point(2, 2) });
            Patterns.Add(new Point[] { new Point(0, 1), new Point(1, 1), new Point(2, 1), new Point(0, 2) });
            Patterns.Add(new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(1, 2) });
            Patterns.Add(new Point[] { new Point(2, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) });
        }
    }
    /**
      * This is a S block
      */
    public class SBlock : Block
    {
        public SBlock(BlockArea blockArea, int x, int y)
            : base(blockArea, x, y)
        {
            ForeColor = Color.Green;
            CenterColor = Color.Green;
        }
        protected override void InitPatterns()
        {
            Patterns.Add(new Point[] { new Point(1, 0), new Point(2, 0), new Point(0, 1), new Point(1, 1) });
            Patterns.Add(new Point[] { new Point(1, 0), new Point(1, 1), new Point(2, 1), new Point(2, 2) });
        }
    }
    /**
      * This is a Z block
      */
    public class ZBlock : Block
    {
        public ZBlock(BlockArea blockArea, int x, int y)
            : base(blockArea, x, y)
        {
            ForeColor = Color.Crimson;
            CenterColor = Color.Crimson;
        }
        protected override void InitPatterns()
        {
            Patterns.Add(new Point[] { new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(2, 1) });
            Patterns.Add(new Point[] { new Point(2, 0), new Point(1, 1), new Point(2, 1), new Point(1, 2) });
        }
    }
    /**
      * This is a O block
      */
    public class OBlock : Block
    {
        public OBlock(BlockArea blockArea, int x, int y)
            : base(blockArea, x, y)
        {
            ForeColor = Color.Yellow;
            CenterColor = Color.Yellow;
        }
        protected override void InitPatterns()
        {
            Patterns.Add(new Point[] { new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(1, 1) });
        }
    }
}

