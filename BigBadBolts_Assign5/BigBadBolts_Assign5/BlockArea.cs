using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace BigBadBolts_Assign5
{
    public class BlockArea : Panel
    {

        private Square[,] gameArray;
        private int rows = 20;
        private int cols = 10;
        private int squareSize = 20;
        private Block currentBlock;
        

        public int Cols
        {
            get { return cols; }
        }

        public int Rows
        {
            get { return Rows; }
        }

        public Square[,] GameArray
        {
            get { return gameArray; }
        }
        public Block CurrentBlock
        {
            get { return currentBlock; }
            set { currentBlock = value; }
        }

 
        /**
         * This creates a block in a certain area
         * parameters: rows - Number of rows
         *             cols - Number of columns
         */
        public BlockArea(int rows, int cols) : base()
        {
            gameArray = new Square[rows, cols];
            this.rows = rows;
            this.cols = cols;
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    gameArray[i, j] = new Square(j, i);
                    gameArray[i, j].ShowEvent += new EventHandler(ShowSquare);
                    gameArray[i, j].HideEvent += new EventHandler(HideSquare);
                }
        }

        /**
         * This function is what draws the square on the screen
         */
        public void ShowSquare(object sender, EventArgs e)
        {
            Square sq = sender as Square;
            Graphics g = CreateGraphics();
            DrawSquare(g, sq);
            g.Dispose();
        }
    
        
        /**
         * This fills the square with a color
         */
        public void HideSquare(object sender, EventArgs e)
        {
            Square sq = sender as Square;
            Graphics g = CreateGraphics();
            g.FillRectangle(new SolidBrush(BackColor), sq.Location.X * squareSize, sq.Location.Y * squareSize, squareSize, squareSize);
            g.Dispose();
        }

   
        /**
         * This will get a square at a location
         * Returns null if nothing is found
         * Returns the square if it found one
         */
        public Square GetSquare(int x, int y)
        {
            if (x < 0 || x >= cols || y < 0 || y >= rows)
                return null;
            return GameArray[y, x];
        }

        /**
         * Gets rid of all the squares drawn on screen
         */
        public void Clear()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    gameArray[i, j].Hide();
                }
            }
        }
        


     
        /**
         * This funciton redraws the squares on the screen if neccesary
         */
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    Square sq = gameArray[i, j];
                    if (sq.Visible)
                        DrawSquare(e.Graphics, sq);
                }
        }

   
        /**
         * This function draws a square
         */
        private void DrawSquare(Graphics g, Square sq)
        {
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(sq.Location.X * squareSize, sq.Location.Y * squareSize, squareSize, squareSize);
            path.AddRectangle(rect);
            PathGradientBrush pthGrBrush = new PathGradientBrush(path);
            pthGrBrush.CenterColor = sq.BackColor;
            Color[] halo = { sq.ForeColor };
            pthGrBrush.SurroundColors = halo;
            g.FillRectangle(pthGrBrush, rect);
        }


    }
}
