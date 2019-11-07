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


        /// <summary>
        /// Creates a BlockArea.
        /// </summary>
        /// <param name="rows">number of rows</param>
        /// <param name="cols">number of columns</param>
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

        /// <summary>
        /// Get the square from the sender object,
        /// define the size of a square, and create a gradient
        /// brush to fill the square with.
        /// </summary>
        public void ShowSquare(object sender, EventArgs e)
        {
            Square sq = sender as Square;
            Graphics g = CreateGraphics();
            DrawSquare(g, sq);
            g.Dispose();
        }
        /// <summary>
        /// Draw over the square with the background colour.
        /// </summary>
        public void HideSquare(object sender, EventArgs e)
        {
            Square sq = sender as Square;
            Graphics g = CreateGraphics();
            g.FillRectangle(new SolidBrush(BackColor), sq.Location.X * squareSize, sq.Location.Y * squareSize, squareSize, squareSize);
            g.Dispose();
        }

        /// <summary>
        /// Return the square at the specified location. Otherwise,
        /// throw an exception.
        /// </summary>
        public Square GetSquare(int x, int y)
        {
            if (x < 0 || x >= cols || y < 0 || y >= rows)
                return null;
            //throw new ArgumentOutOfRangeException();
            return GameArray[y, x];
        }

        /// <summary>
        /// Clean up the area, hide all squares.
        /// </summary>
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
        


        /// <summary>
        /// Repaint the visible squares when the game area needs to be repainted.
        /// </summary>
        /// <param name="e"></param>
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

        /// <summary>
        /// draw a square on a Graphics surface.
        /// </summary>
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
