// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="OrientedText.cs" company="Zeroit Dev Technologies">
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
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{


    #region ZeroitOrientedLabel



    #region Orientation

    //Orientation of the text

    /// <summary>
    /// Setting the text Orientation <c><see cref="ZeroitOrientedLabel" /></c> label.
    /// </summary>
    public enum Orientation
    {
        /// <summary>
        /// Set the orientation to circle.
        /// </summary>
        Circle,
        /// <summary>
        /// Set the orientation to arc.
        /// </summary>
        Arc,
        /// <summary>
        /// Set the orientation to circle.
        /// </summary>
        Rotate
    }

    /// <summary>
    /// Sets the <c><see cref="Direction" /></c> of the <c><see cref="ZeroitOrientedLabel" /></c> label.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Set to Clockwise.
        /// </summary>
        Clockwise,
        /// <summary>
        /// Set to Anti-Clockwise.
        /// </summary>
        AntiClockwise
    }


    #endregion


    /// <summary>
    /// A class collection for rendering a label to any direction and angle.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    public class ZeroitOrientedLabel : System.Windows.Forms.Label
    {

        #region Variables

        /// <summary>
        /// The rotation angle
        /// </summary>
        private double rotationAngle;
        /// <summary>
        /// The text
        /// </summary>
        private string text;
        /// <summary>
        /// The text orientation
        /// </summary>
        private Orientation textOrientation;
        /// <summary>
        /// The text direction
        /// </summary>
        private Direction textDirection;



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitOrientedLabel" /> class.
        /// </summary>
        public ZeroitOrientedLabel()
        {
            //Setting the initial condition.
            rotationAngle = 0d;
            textOrientation = Orientation.Rotate;
            this.Size = new Size(105, 12);

            IncludeInConstructor();
        }

        #endregion

        #region Properties

        #region TextRenderingHint

        #region Add it to OnPaint / Graphics Rendering component

        //e.Graphics.TextRenderingHint = textrendering;
        #endregion

        /// <summary>
        /// The textrendering
        /// </summary>
        TextRenderingHint textrendering = TextRenderingHint.AntiAlias;

        /// <summary>
        /// Gets or sets the text rendering mode.
        /// </summary>
        /// <value>The text rendering.</value>
        public TextRenderingHint TextRendering
        {
            get { return textrendering; }
            set
            {
                textrendering = value;
                Invalidate();
            }
        }
        #endregion


        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        /// <value>The rotation angle.</value>
        [Description("Rotation Angle"), Category("Appearance")]
        public double RotationAngle
        {
            get
            {
                return rotationAngle;
            }
            set
            {

                rotationAngle = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text orientation.
        /// </summary>
        /// <value>The text orientation.</value>
        [Description("Kind of Text Orientation"), Category("Appearance")]
        public Orientation TextOrientation
        {
            get
            {
                return textOrientation;
            }
            set
            {
                if (value == Orientation.Circle || value == Orientation.Arc)
                {
                    AutoSize = false;
                }
                textOrientation = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text direction.
        /// </summary>
        /// <value>The text direction.</value>
        [Description("Direction of the Text"), Category("Appearance")]
        public Direction TextDirection
        {
            get
            {
                return textDirection;
            }
            set
            {

                textDirection = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [Description("Display Text"), Category("Appearance")]
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                this.Invalidate();
            }
        }

        #endregion
        
        #region Transparency


        #region Include in Paint

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion

        #region Method

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion


        #endregion

        #region Method

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            Graphics graphics = e.Graphics;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.Trimming = StringTrimming.None;
            e.Graphics.TextRenderingHint = textrendering;

            Brush textBrush = new SolidBrush(this.ForeColor);

            //Getting the width and height of the text, which we are going to write
            float width = graphics.MeasureString(text, this.Font).Width;
            float height = graphics.MeasureString(text, this.Font).Height;

            //The radius is set to 0.9 of the width or height, b'cos not to 
            //hide and part of the text at any stage
            float radius = 0f;
            if (ClientRectangle.Width < ClientRectangle.Height)
            {
                radius = ClientRectangle.Width * 0.9f / 2;
            }
            else
            {
                radius = ClientRectangle.Height * 0.9f / 2;
            }

            //Setting the text according to the selection
            switch (textOrientation)
            {
                case Orientation.Arc:
                    {
                        //Arc angle must be get from the length of the text.
                        float arcAngle = (2 * width / radius) / text.Length;
                        if (textDirection == Direction.Clockwise)
                        {
                            for (int i = 0; i < text.Length; i++)
                            {

                                graphics.TranslateTransform(
                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                graphics.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                graphics.DrawString(text[i].ToString(), this.Font, textBrush, 0, 0);
                                graphics.ResetTransform();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < text.Length; i++)
                            {

                                graphics.TranslateTransform(
                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                graphics.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                graphics.DrawString(text[i].ToString(), this.Font, textBrush, 0, 0);
                                graphics.ResetTransform();

                            }
                        }
                        break;
                    }
                case Orientation.Circle:
                    {
                        if (textDirection == Direction.Clockwise)
                        {
                            for (int i = 0; i < text.Length; i++)
                            {
                                graphics.TranslateTransform(
                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI))),
                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI))));
                                graphics.RotateTransform(-90 + (float)rotationAngle + (360 / text.Length) * i);
                                graphics.DrawString(text[i].ToString(), this.Font, textBrush, 0, 0);
                                graphics.ResetTransform();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < text.Length; i++)
                            {
                                graphics.TranslateTransform(
                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI))),
                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / text.Length) * i + rotationAngle / 180 * Math.PI))));
                                graphics.RotateTransform(-90 - (float)rotationAngle - (360 / text.Length) * i);
                                graphics.DrawString(text[i].ToString(), this.Font, textBrush, 0, 0);
                                graphics.ResetTransform();
                            }

                        }
                        break;
                    }
                case Orientation.Rotate:
                    {
                        //For rotation, who about rotation?
                        double angle = (rotationAngle / 180) * Math.PI;
                        graphics.TranslateTransform(
                            (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - (float)(width * Math.Cos(angle))) / 2,
                            (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - (float)(width * Math.Sin(angle))) / 2);
                        graphics.RotateTransform((float)rotationAngle);
                        graphics.DrawString(text, this.Font, textBrush, 0, 0);
                        graphics.ResetTransform();

                        break;
                    }
            }
        }
        #endregion

        #region Animation

        #region Include in Private Field

        private bool autoAnimate = false;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer timerDecrement = new System.Windows.Forms.Timer();
        private float speedMultiplier = 1;
        private float change = 0.1f;
        private bool reverse = true;
        private float value = 0;
        private float minimum = 0;
        private float maximum = 360;
        private bool sluggish = false;
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

        public float Change
        {
            get { return change; }
            set
            {
                change = value;
                Invalidate();
            }
        }

        public float SpeedMultiplier
        {
            get { return speedMultiplier; }
            set
            {
                speedMultiplier = value;
                Invalidate();
            }
        }

        public int TimerInterval
        {
            get { return timer.Interval; }
            set
            {
                timer.Interval = value;
                timerDecrement.Interval = value;
                Invalidate();
            }
        }


        public float Minimum
        {
            get { return minimum; }
            set
            {
                minimum = value;
                Invalidate();
            }
        }

        public float Maximum
        {
            get { return maximum; }
            set
            {
                maximum = value;
                Invalidate();
            }
        }

        public bool Sluggish
        {
            get { return sluggish; }
            set
            {
                sluggish = value;
                Invalidate();
            }
        }

        #endregion

        #region Event

        private void Timer_Tick(object sender, EventArgs e)
        {

            if (Reverse)
            {

                if (this.RotationAngle + (Change * SpeedMultiplier) > Maximum)
                {
                    timer.Stop();
                    timer.Enabled = false;
                    timerDecrement.Enabled = true;
                    timerDecrement.Start();
                    //timerDecrement.Tick += TimerDecrement_Tick;
                    Invalidate();
                }

                else
                {
                    this.RotationAngle += (Change * SpeedMultiplier);
                    Invalidate();
                }


            }
            else
            {

                if (Sluggish)
                {
                    if (this.RotationAngle + (Change * SpeedMultiplier) > Maximum)
                    {
                        timer.Stop();
                        timer.Enabled = false;
                        timerDecrement.Enabled = true;
                        timerDecrement.Start();
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        Invalidate();
                    }

                    else
                    {
                        this.RotationAngle += (Change * SpeedMultiplier);
                        Invalidate();
                    }
                }
                else
                {
                    if (this.RotationAngle + (Change * SpeedMultiplier) > Maximum)
                    {
                        timerDecrement.Enabled = false;
                        timerDecrement.Stop();
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        RotationAngle = 0;
                        Invalidate();
                    }

                    else
                    {
                        this.RotationAngle += (Change * SpeedMultiplier);
                        Invalidate();
                    }
                }

            }
        }


        private void TimerDecrement_Tick(object sender, EventArgs e)
        {
            if (this.RotationAngle < this.Minimum)
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
                this.RotationAngle -= (Change * SpeedMultiplier);
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
                    timerDecrement.Interval = 100;
                    timer.Interval = 100;
                    timer.Start();
                }
            }

            if (!DesignMode)
            {
                timer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 100;
                    timer.Interval = 100;
                    timer.Start();
                }
            }

        }

        #endregion


        #endregion


    }

    #endregion

}