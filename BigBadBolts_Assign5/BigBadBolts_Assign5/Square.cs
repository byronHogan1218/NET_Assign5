using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace BigBadBolts_Assign5
{
    public class Square
    {
        //Need to show a square
        public event EventHandler ShowEvent;
        //Need to hide a square
        public event EventHandler HideEvent;

        private Point location;
        private bool visible;
        private Color foreColor;
        private Color centerColor;


        /**
         * This is a constructor for a square
         * Parameters: x - The width of the square
         *             y - the height of the square
         */
        public Square(int x, int y)
        {
            location.X = x;
            location.Y = y;
        }

        /**
         * This function makes the square visible
         */
        public void Show(Color foreColor, Color centerColor)
        {
            this.foreColor = foreColor;
            this.centerColor = centerColor;

            visible = true;
            if (ShowEvent != null)
                ShowEvent(this, null);
        }

        /**
         * This function makes the square hidden
         */
        public void Hide()
        {
            visible = false;
            if (HideEvent != null)
                HideEvent(this, null);
        }
        /**
         * This is used to reset the game
         */
        public void ClearEvents()
        {
            ShowEvent = null;
            HideEvent = null;
        }
 
        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public bool Visible
        {
            get { return visible; }
        }

  
        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }


        public Color BackColor
        {
            get { return centerColor; }
            set { centerColor = value; }
        }
    }
}



