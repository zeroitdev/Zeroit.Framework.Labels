
#region Imports

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    public class ZeroitFrontBack : Label
    {
        #region Scroll Text

        string movingText = "Powered By :\nZeroit Dev Technologies";
        int index = 0;
        private char symbol = char.Parse("_");

        public char Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                Invalidate();
            }
        }
        public int Index
        {
            get { return index; }
            set
            {
                index = value;

                base.Text = movingText.Substring(0, value) + Symbol;

                Invalidate();
            }
        }


        
        public new string Text
        {
            get { return movingText; }
            set
            {
                movingText = value;
                Invalidate();
            }
        }

        
        public ZeroitFrontBack()
        {
            IncludeInConstructor();
            base.Text = movingText;
        }



        #region Include in Private Field


        private bool autoAnimate = true;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerDecrement = new System.Windows.Forms.Timer();
        private bool reverse = true;
        #endregion

        #region Include in Public Properties

        public bool AutoAnimate
        {
            get { return autoAnimate; }
            set
            {
                autoAnimate = value;

                if (value == true)
                {
                    timer.Enabled = true;
                }

                else
                {
                    timer.Enabled = false;
                    timerDecrement.Enabled = false;
                }

                Invalidate();
            }
        }

        public bool Reverse
        {
            get { return reverse; }
            set
            {

                reverse = value;
                Invalidate();
            }
        }

        public int Speed
        {
            get { return timer.Interval; }
            set
            {
                timer.Interval = value;
                timerDecrement.Interval = value;
                Invalidate();
            }
        }


        #endregion

        #region Event

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (Reverse)
            {

                if (Index + 1 == movingText.Length)
                {

                    timer.Stop();
                    timer.Enabled = false;
                    timerDecrement.Enabled = true;
                    timerDecrement.Start();
                    Invalidate();
                }

                else
                {

                    Index += 1;
                    Invalidate();
                }
            }
            else
            {

                if (Index + 1 == movingText.Length)
                {

                    timerDecrement.Enabled = false;
                    timerDecrement.Stop();
                    Index = 0;
                    Invalidate();
                }

                else
                {

                    Index += 1;
                    Invalidate();
                }
            }
        }


        private void TimerDecrement_Tick(object sender, EventArgs e)
        {
            if (Index <= 0)
            {

                timerDecrement.Stop();
                timerDecrement.Enabled = false;
                timer.Enabled = true;
                timer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                Index -= 1;
                Invalidate();
            }

        }


        #endregion

        #region Constructor

        private void IncludeInConstructor()
        {

            if (DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 50;
                    timer.Interval = 50;
                    timer.Start();
                }
            }

            if (!DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 50;
                    timer.Interval = 50;
                    timer.Start();
                }
            }

        }

        #endregion




        #endregion

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            timer.Enabled = false;
            timerDecrement.Enabled = false;
            timer.Dispose();
            timerDecrement.Dispose();
        }
    }

}