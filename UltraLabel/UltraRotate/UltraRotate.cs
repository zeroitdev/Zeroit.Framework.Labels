// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 12-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="UltraRotate.cs" company="Zeroit Dev Technologies">
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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Zeroit.Framework.Labels
{
    /// <summary>
    /// A class collection for rendering a label with nice features.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(UltraRotateDesigner))]
    public partial class ZeroitUltraRotate : Control
    {

        
        

        #region Constructor

        //In the constructor we set all the styles we want the LabelEx control to have when it is created.
        //We also set a few properties that we want the control to have set by default when a new instance is created.
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitUltraRotate" /> class.
        /// </summary>
        public ZeroitUltraRotate()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Font = new Font("Comic Sans MS", 18, FontStyle.Bold);
            this.Size = new Size(130, 38);
            this.ForeColor = Color.White;
            this.BackColor = Color.Transparent;

            SlideIncludeInConstructor();
            IncludeInConstructor();

            pathSize = (ClientRectangle.Width + ClientRectangle.Height) / 15;
        }

        #endregion
        
        #region Overrides and Private Methods
        
        //A private sub used to position, resize, and draw the BackgroundImage according to the BackgroundImageLayout
        /// <summary>
        /// Draws the back image.
        /// </summary>
        /// <param name="g">The g.</param>
        private void DrawBackImage(Graphics g)
        {
            if (this.BackgroundImageLayout == ImageLayout.None)
            {
                g.DrawImage(this.BackgroundImage, 0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height);
            }
            else if (this.BackgroundImageLayout == ImageLayout.Tile)
            {
                int tc = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.Width / this.BackgroundImage.Width)));
                int tr = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.Height / this.BackgroundImage.Height)));
                for (int y = 0; y <= tr; y++)
                {
                    for (int x = 0; x <= tc; x++)
                    {
                        g.DrawImage(this.BackgroundImage, (x * this.BackgroundImage.Width), (y * this.BackgroundImage.Height), this.BackgroundImage.Width, this.BackgroundImage.Height);
                    }
                }
            }
            else if (this.BackgroundImageLayout == ImageLayout.Center)
            {
                int xx = Convert.ToInt32((this.Width / 2) - (this.BackgroundImage.Width / 2));
                int yy = Convert.ToInt32((this.Height / 2) - (this.BackgroundImage.Height / 2));
                g.DrawImage(this.BackgroundImage, xx, yy, this.BackgroundImage.Width, this.BackgroundImage.Height);
            }
            else if (this.BackgroundImageLayout == ImageLayout.Stretch)
            {
                g.DrawImage(this.BackgroundImage, 0, 0, this.Width, this.Height);
            }
            else if (this.BackgroundImageLayout == ImageLayout.Zoom)
            {
                double meratio = this.Width / this.Height;
                double imgratio = this.BackgroundImage.Width / this.BackgroundImage.Height;
                Rectangle imgrect = new Rectangle(0, 0, this.Width, this.Height);
                if (imgratio > meratio)
                {
                    imgrect.Width = this.Width;
                    imgrect.Height = Convert.ToInt32(this.Width / imgratio);
                }
                else if (imgratio < meratio)
                {
                    imgrect.Height = this.Height;
                    imgrect.Width = Convert.ToInt32(this.Height * imgratio);
                }
                imgrect.X = Convert.ToInt32((this.Width / 2) - (imgrect.Width / 2));
                imgrect.Y = Convert.ToInt32((this.Height / 2) - (imgrect.Height / 2));
                g.DrawImage(this.BackgroundImage, imgrect);
            }
        }

        //A private sub used for drawing the Border part of the control
        /// <summary>
        /// Draws the label border.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rec">The record.</param>
        private void DrawLabelBorder(Graphics g, Rectangle rec)
        {
            //If the ShowTextShadow property is true and the Text property is not an empty string then because of the
            //prior calls to the Graphics.TranslateTransform used for the shadow effect the Graphics must be shifted
            //back to its center position before drawing the border.
            //if (_ShowTextShadow & !string.IsNullOrEmpty(this.Text))
            //{
            //    if (_ShadowPosition == ShadowArea.TopLeft)
            //    {
            //        g.TranslateTransform(-_ShadowDepth, -_ShadowDepth);
            //    }
            //    else if (_ShadowPosition == ShadowArea.TopRight)
            //    {
            //        g.TranslateTransform(+_ShadowDepth, -_ShadowDepth);
            //    }
            //    else if (_ShadowPosition == ShadowArea.BottomLeft)
            //    {
            //        g.TranslateTransform(-_ShadowDepth, +_ShadowDepth);
            //    }
            //    else
            //    {
            //        g.TranslateTransform(+_ShadowDepth, +_ShadowDepth);
            //    }
            //}

            //If the BorderStyle property is set to Rounded then draw the border with rounded corners
            //else just draw a Rectangle
            if (_BorderStyle == BorderType.Rounded)
            {
                g.SmoothingMode = Smoothing;
                using (GraphicsPath gp = new GraphicsPath())
                {
                    int rad = Convert.ToInt32(rec.Height / 3);
                    if (rec.Width < rec.Height)
                        rad = Convert.ToInt32(rec.Width / 3);
                    gp.AddArc(rec.X, rec.Y, rad, rad, 180, 90);
                    gp.AddArc(rec.Right - (rad), rec.Y, rad, rad, 270, 90);
                    gp.AddArc(rec.Right - (rad), rec.Bottom - (rad), rad, rad, 0, 90);
                    gp.AddArc(rec.X, rec.Bottom - (rad), rad, rad, 90, 90);
                    gp.CloseFigure();
                    g.DrawPath(_BorderPen, gp);
                }
            }
            else
            {
                g.DrawRectangle(_BorderPen, rec.X, rec.Y, rec.Width, rec.Height);
            }
        }

        //A private function used for calculating the rectagle area of the Label to draw the Image in
        /// <summary>
        /// Aligns the image.
        /// </summary>
        /// <param name="Rect">The rect.</param>
        /// <returns>Rectangle.</returns>
        private Rectangle AlignImage(Rectangle Rect)
        {
            //Use the value of the ContentAlignment assigned to the ImageAlign property to set the X and Y
            //values of the returned rectangle for the image.
            int xp = 0;
            int yp = 0;
            int ia = Convert.ToInt32(_ImageAlign);
            if (ia < 8)
            {
                yp = 0 + this.Padding.Top;
            }
            else if (ia < 128)
            {
                yp = Convert.ToInt32(Rect.Height / 2) - Convert.ToInt32(_Image.Height / 2);
                ia = ia / 16;
            }
            else
            {
                yp = Rect.Height - _Image.Height - this.Padding.Bottom;
                ia = ia / 256;
            }
            if (ia == Convert.ToInt32(ContentAlignment.TopLeft))
            {
                xp = 0 + this.Padding.Left;
            }
            else if (ia == Convert.ToInt32(ContentAlignment.TopCenter))
            {
                xp = Convert.ToInt32(Rect.Width / 2) - Convert.ToInt32(_Image.Width / 2);
            }
            else if (ia == Convert.ToInt32(ContentAlignment.TopRight))
            {
                xp = Rect.Width - _Image.Width - this.Padding.Right;
            }
            return new Rectangle(xp, yp, _Image.Width, _Image.Height);
        }

        //Need to use the OnTextChanged overrides sub to make the Label repaint itself when the Text is changed
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            SlideOnTextChanged(e);
            this.Refresh();
            base.OnTextChanged(e);
        }

        //Need to use the Dispose Overides sub to make sure all of the New brushes and pens created for the
        //property backing feilds are disposed.
        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            //SliderTimer
            SlideDispose();

            timer.Stop();
            timer.Enabled = false;
            timer.Dispose();

            rotateTimer.Stop();
            rotateTimer.Enabled = false;
            rotateTimer.Dispose();

            timerDecrement.Stop();
            timerDecrement.Enabled = false;
            timerDecrement.Dispose();

            this._BackgroundBrush.Dispose();
            this._BorderPen.Dispose();
            this._CenterBrush.Dispose();
            this._OutLinePen.Dispose();
            this._ShadowBrush.Dispose();
            this._ShadowPen.Dispose();
            base.Dispose(disposing);
        }

        
        #endregion

        #region Sliding Timer

        #region Include in Private Field

        /// <summary>
        /// The timer
        /// </summary>
        Timer timer = new Timer();
        /// <summary>
        /// The slide
        /// </summary>
        private bool slide = false;
        /// <summary>
        /// The sliding a
        /// </summary>
        int slidingA = 0;
        /// <summary>
        /// The art
        /// </summary>
        bool art = false;
        /// <summary>
        /// The sliding limit
        /// </summary>
        private int slidingLimit = 5;
        /// <summary>
        /// The correct width
        /// </summary>
        private int correctWidth = 75;

        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets the width of the correct.
        /// </summary>
        /// <value>The width of the correct.</value>
        public int CorrectWidth
        {
            get { return correctWidth; }
            set
            {
                if (value > 100)
                {
                    value = 100;
                    Invalidate();
                }

                if (value < 75)
                {
                    value = 75;
                }
                correctWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the sliding limit.
        /// </summary>
        /// <value>The sliding limit.</value>
        public int SlidingLimit
        {
            get { return slidingLimit; }
            set
            {

                slidingLimit = value;
                this.OnSlidingLimitChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitUltraRotate"/> is slide.
        /// </summary>
        /// <value><c>true</c> if slide; otherwise, <c>false</c>.</value>
        public bool Slide
        {
            get { return slide; }
            set
            {
                if (value)
                {
                    this.Width = TextRenderer.MeasureText(Text, Font).Width - SlidingLimit;
                    this.Height = TextRenderer.MeasureText(Text, Font).Height;
                    Invalidate();
                }
                slide = value;
                timer.Enabled = slide;
                if (!slide)
                {
                    slidingA = 0;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the sliding speed.
        /// </summary>
        /// <value>The sliding speed.</value>
        public int SlidingSpeed
        {
            get { return timer.Interval; }
            set
            {
                timer.Interval = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the Text associated with this control.
        /// </summary>
        /// <value>The Text.</value>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                timer.Start();
            }
        }

        /// <summary>
        /// Gets or sets the font of the Text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                timer.Start();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitUltraRotate"/> is art.
        /// </summary>
        /// <value><c>true</c> if art; otherwise, <c>false</c>.</value>
        public bool Art
        {
            get { return art; }
            set
            {
                art = value;

                Invalidate();

            }
        }

        #endregion

        #region Include in Constructor

        /// <summary>
        /// Slides the include in constructor.
        /// </summary>
        private void SlideIncludeInConstructor()
        {
            timer.Interval = 120;
            timer.Tick += Sliding_Timer_Tick;
            slide = false;
            timer.Enabled = false;
        }


        #endregion

        #region Sliding Timer

        /// <summary>
        /// Handles the Tick event of the Sliding_Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Sliding_Timer_Tick(object sender, EventArgs e)
        {
            Size tSize = TextRenderer.MeasureText(Text, Font);
            if (tSize.Width <= Width)
            {
                timer.Stop();
                slidingA = 1;
                Invalidate();
                return;
            }
            int maxFar = tSize.Width >= Width ? tSize.Width - Width : 0;
            if (slidingA >= 1)
                art = false;
            if (-slidingA >= maxFar + Font.Height)
                art = true;
            slidingA = art ? slidingA + 1 : slidingA - 1;
            Invalidate();
        }


        #endregion

        #endregion

        #region Sliding Text Method

        /// <summary>
        /// Draws the sliding Text.
        /// </summary>
        /// <param name="_with1">The with1.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="pth">The PTH.</param>
        /// <param name="sf">The sf.</param>
        private void DrawSlidingText(Graphics _with1, Brush brush, GraphicsPath pth, StringFormat sf)
        {

            Size tSize = TextRenderer.MeasureText(Text, Font);
            int y = Height / 2 - tSize.Height / 2;


            //pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style), Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), new Point(slidingA, y),sf);

            pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style), Convert.ToSingle((_with1.DpiY * this.Font.Size)  / CorrectWidth), new Rectangle(Padding.Left + slidingA + SlidingLimit, Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)), sf);

            //_with1.DrawString(Text.ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ForeColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
            
        }


        #endregion
        
        #region Animation

        #region Include in Private Field

        private bool autoAnimate = false;
        private System.Windows.Forms.Timer rotateTimer = new System.Windows.Forms.Timer();
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
                    rotateTimer.Enabled = true;
                }

                else
                {
                    rotateTimer.Enabled = false;
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
                rotateTimer.Interval = value;
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
                    rotateTimer.Stop();
                    rotateTimer.Enabled = false;
                    timerDecrement.Enabled = true;
                    timerDecrement.Start();
                    //timerDecrement.Tick += TimerDecrement_Tick;
                    Invalidate();
                }

                else
                {
                    this.RotationAngle += (Change * SpeedMultiplier);

                    if (textOrientation == Orientation.Rotate)
                    {
                        double angle = (rotationAngle / 180) * Math.PI;
                        float width = CreateGraphics().MeasureString(Text, this.Font).Width;
                        float height = CreateGraphics().MeasureString(Text, this.Font).Height;
                        this.Location = new Point(((Parent.Width) + (byte) (height * Math.Sin(angle)) -
                                                   (byte) (width * Math.Cos(angle))) / 2,
                            ((Parent.Height) - (byte) (height * Math.Cos(angle)) -
                             (byte) (width * Math.Sin(angle))) / 2);

                    }

                    Invalidate();
                }


            }
            else
            {

                if (Sluggish)
                {
                    if (this.RotationAngle + (Change * SpeedMultiplier) > Maximum)
                    {
                        rotateTimer.Stop();
                        rotateTimer.Enabled = false;
                        timerDecrement.Enabled = true;
                        timerDecrement.Start();
                        //timerDecrement.Tick += TimerDecrement_Tick;
                        Invalidate();
                    }

                    else
                    {
                        this.RotationAngle += (Change * SpeedMultiplier);

                        if (textOrientation == Orientation.Rotate)
                        {
                            double angle = (rotationAngle / 180) * Math.PI;
                            float width = CreateGraphics().MeasureString(Text, this.Font).Width;
                            float height = CreateGraphics().MeasureString(Text, this.Font).Height;
                            this.Location = new Point(((Parent.Width) + (int)(height * Math.Sin(angle)) -
                                                       (int)(width * Math.Cos(angle))) / 2,
                                ((Parent.Height) - (int)(height * Math.Cos(angle)) -
                                 (int)(width * Math.Sin(angle))) / 2);

                        }

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

                        if (textOrientation == Orientation.Rotate)
                        {
                            double angle = (rotationAngle / 180) * Math.PI;
                            float width = CreateGraphics().MeasureString(Text, this.Font).Width;
                            float height = CreateGraphics().MeasureString(Text, this.Font).Height;
                            this.Location = new Point(((this.Parent.Width) + (int)(height * Math.Sin(angle)) -
                                                       (int)(width * Math.Cos(angle))) / 2,
                                ((this.Parent.Height) - (int)(height * Math.Cos(angle)) -
                                 (int)(width * Math.Sin(angle))) / 2);

                        }

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
                rotateTimer.Enabled = true;
                rotateTimer.Start();
                //timer.Tick += Timer_Tick;
                Invalidate();
            }

            else
            {
                this.RotationAngle -= (Change * SpeedMultiplier);

                if (textOrientation == Orientation.Rotate)
                {
                    double angle = (rotationAngle / 180) * Math.PI;
                    float width = CreateGraphics().MeasureString(Text, this.Font).Width;
                    float height = CreateGraphics().MeasureString(Text, this.Font).Height;
                    this.Location = new Point(((Width) + (byte) (height * Math.Sin(angle)) -
                                               (byte) (width * Math.Cos(angle))) / 2,
                        ((Height) - (byte) (height * Math.Cos(angle)) -
                         (byte) (width * Math.Sin(angle))) / 2);

                }

                Invalidate();
            }


        }


        #endregion

        #region Constructor

        private void IncludeInConstructor()
        {

            if (DesignMode)
            {
                rotateTimer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 100;
                    rotateTimer.Interval = 100;
                    rotateTimer.Start();
                }
            }

            if (!DesignMode)
            {
                rotateTimer.Tick += Timer_Tick;
                timerDecrement.Tick += TimerDecrement_Tick;
                if (AutoAnimate)
                {
                    timerDecrement.Interval = 100;
                    rotateTimer.Interval = 100;
                    rotateTimer.Start();
                }
            }

        }

        #endregion


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


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            TransInPaint(e.Graphics);

            switch (GraphicType)
            {
                case GraphicsType.Graphics:
                    GraphicsOnPaint(e);
                    break;
                case GraphicsType.GraphicsPath:
                    PathOnPaint(e);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

    }
}
