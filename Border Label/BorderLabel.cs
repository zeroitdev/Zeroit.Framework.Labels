// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="Tarzan.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region Tarzan Label

    /// <summary>
    /// A class collection for rendering a label with a border.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    public partial class ZeroitBorderLabel : Label
    {
        /// <summary>
        /// The border size
        /// </summary>
        private float borderSize;
        /// <summary>
        /// The border color
        /// </summary>
        private Color borderColor;

        /// <summary>
        /// The point
        /// </summary>
        private PointF point;
        /// <summary>
        /// The draw size
        /// </summary>
        private SizeF drawSize;
        /// <summary>
        /// The draw pen
        /// </summary>
        private Pen drawPen;
        /// <summary>
        /// The draw path
        /// </summary>
        private GraphicsPath drawPath;
        /// <summary>
        /// The forecolor brush
        /// </summary>
        private SolidBrush forecolorBrush;




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




        // Constructor
        //-----------------------------------------------------

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitBorderLabel" /> class.
        /// </summary>
        public ZeroitBorderLabel()
        {
            this.borderSize = 1f;
            this.borderColor = Color.White;
            this.drawPath = new GraphicsPath();
            this.drawPen = new Pen(new SolidBrush(this.borderColor), borderSize);
            this.forecolorBrush = new SolidBrush(this.ForeColor);

            this.Invalidate();
        }
        #endregion



        // Public Properties
        //-----------------------------------------------------

        #region Public Properties

        /// <summary>
        /// The border's thickness
        /// </summary>
        /// <value>The size of the border.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("The border's thickness")]
        [DefaultValue(1f)]
        public float BorderSize
        {
            get { return this.borderSize; }
            set
            {
                this.borderSize = value;
                if (value == 0)
                {
                    //If border size equals zero, disable the
                    // border by setting it as transparent
                    this.drawPen.Color = Color.Transparent;
                }
                else
                {
                    this.drawPen.Color = this.BorderColor;
                    this.drawPen.Width = value;
                }

                this.OnTextChanged(EventArgs.Empty);
            }
        }


        /// <summary>
        /// The border color of this component
        /// </summary>
        /// <value>The color of the border.</value>
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "White")]
        [Description("The border color of this component")]
        public Color BorderColor
        {
            get { return this.borderColor; }
            set
            {
                this.borderColor = value;

                if (this.BorderSize != 0)
                    this.drawPen.Color = value;

                this.Invalidate();
            }
        }

        #endregion



        // Public Methods
        //-----------------------------------------------------

        #region Public Methods
        /// <summary>
        /// Releases all resources used by this control
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                if (this.forecolorBrush != null)
                    this.forecolorBrush.Dispose();

                if (this.drawPath != null)
                    this.drawPath.Dispose();

                if (this.drawPen != null)
                    this.drawPen.Dispose();

            }
            base.Dispose(disposing);
        }

        #endregion



        // Event Handling
        //-----------------------------------------------------

        #region Event Handling
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Label.TextAlignChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnForeColorChanged(EventArgs e)
        {
            this.forecolorBrush.Color = base.ForeColor;
            base.OnForeColorChanged(e);
            this.Invalidate();
        }
        #endregion



        // Drawning Events
        //-----------------------------------------------------

        #region Drawning
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            // First lets check if we indeed have text to draw.
            //  if we have no text, then we have nothing to do.
            if (this.Text.Length == 0)
                return;


            // Secondly, lets begin setting the smoothing mode to AntiAlias, to
            // reduce image sharpening and compositing quality to HighQuality,
            // to improve our drawnings and produce a better looking image.

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;



            // Next, we measure how much space our drawning will use on the control.
            //  this is important so we can determine the correct position for our text.
            this.drawSize = e.Graphics.MeasureString(this.Text, this.Font, new PointF(), StringFormat.GenericTypographic);



            // Now, we can determine how we should align our text in the control
            //  area, both horizontally and vertically. If the control is set to auto
            //  size itselft, then it should be automatically drawn to the standard position.

            if (this.AutoSize)
            {
                this.point.X = this.Padding.Left;
                this.point.Y = this.Padding.Top;
            }
            else
            {
                // Text is Left-Aligned:
                if (this.TextAlign == System.Drawing.ContentAlignment.TopLeft ||
                    this.TextAlign == System.Drawing.ContentAlignment.MiddleLeft ||
                    this.TextAlign == System.Drawing.ContentAlignment.BottomLeft)
                    this.point.X = this.Padding.Left;

                // Text is Center-Aligned
                else if (this.TextAlign == System.Drawing.ContentAlignment.TopCenter ||
                         this.TextAlign == System.Drawing.ContentAlignment.MiddleCenter ||
                         this.TextAlign == System.Drawing.ContentAlignment.BottomCenter)
                    point.X = (this.Width - this.drawSize.Width) / 2;

                // Text is Right-Aligned
                else point.X = this.Width - (this.Padding.Right + this.drawSize.Width);


                // Text is Top-Aligned
                if (this.TextAlign == System.Drawing.ContentAlignment.TopLeft ||
                    this.TextAlign == System.Drawing.ContentAlignment.TopCenter ||
                    this.TextAlign == System.Drawing.ContentAlignment.TopRight)
                    point.Y = this.Padding.Top;

                // Text is Middle-Aligned
                else if (this.TextAlign == System.Drawing.ContentAlignment.MiddleLeft ||
                         this.TextAlign == System.Drawing.ContentAlignment.MiddleCenter ||
                         this.TextAlign == System.Drawing.ContentAlignment.MiddleRight)
                    point.Y = (this.Height - this.drawSize.Height) / 2;

                // Text is Bottom-Aligned
                else point.Y = this.Height - (this.Padding.Bottom + this.drawSize.Height);
            }



            // Now we can draw our text to a graphics path.
            //  
            //   PS: this is a tricky part: AddString() expects float emSize in pixel, but Font.Size
            //   measures it as points. So, we need to convert between points and pixels, which in
            //   turn requires detailed knowledge of the DPI of the device we are drawing on. 
            //
            //   The solution was to get the last value returned by the Graphics.DpiY property and
            //   divide by 72, since point is 1/72 of an inch, no matter on what device we draw.
            //
            //   The source of this solution can be seen on CodeProject's article
            //   'OSD window with animation effect' - http://www.codeproject.com/csharp/OSDwindow.asp 

            float fontSize = e.Graphics.DpiY * this.Font.SizeInPoints / 72;

            this.drawPath.Reset();
            this.drawPath.AddString(this.Text, this.Font.FontFamily, (int)this.Font.Style, fontSize,
                point, StringFormat.GenericTypographic);


            // And finally, using our pen, all we have to do now
            //  is draw our graphics path to the screen. Voilla!
            e.Graphics.FillPath(this.forecolorBrush, this.drawPath);
            e.Graphics.DrawPath(this.drawPen, this.drawPath);

        }
        #endregion


    }

    #endregion
    
}