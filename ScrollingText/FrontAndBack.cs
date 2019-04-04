// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="FrontAndBack.cs" company="Zeroit Dev Technologies">
//    This program is for creating Label controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************

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