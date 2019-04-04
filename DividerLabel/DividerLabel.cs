// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="DividerLabel.cs" company="Zeroit Dev Technologies">
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

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region DividerLabel

    /// <summary>
    /// A class collection for rendering Label with Divider.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ZeroitDividerLabel))]
    public class ZeroitDividerLabel : System.Windows.Forms.Label
    {

        /// <summary>
        /// Gets or sets the gap (in pixels) between label and line.
        /// </summary>
        /// <value>The gap.</value>
        [Category("Appearance")]
        [Description("Gap between text and divider line.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), DefaultValue(0)]
        public int Gap
        {
            get { return m_gap; }
            set
            {
                m_gap = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The dashstyle
        /// </summary>
        private DashStyle dashstyle = DashStyle.Solid;
        /// <summary>
        /// Gets or sets the dash style.
        /// </summary>
        /// <value>The dash style.</value>
        public DashStyle DashStyle
        {
            get { return dashstyle; }
            set
            {
                dashstyle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The start cap
        /// </summary>
        private LineCap startCap = LineCap.Flat;
        /// <summary>
        /// Gets or sets the start cap.
        /// </summary>
        /// <value>The start cap.</value>
        public LineCap StartCap
        {
            get { return startCap; }
            set
            {
                startCap = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The end cap
        /// </summary>
        private LineCap endCap = LineCap.Flat;
        /// <summary>
        /// Gets or sets the end cap.
        /// </summary>
        /// <value>The end cap.</value>
        public LineCap EndCap
        {
            get { return endCap; }
            set
            {
                endCap = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The deep color
        /// </summary>
        private Color deepColor = Color.Gray;
        /// <summary>
        /// The light color
        /// </summary>
        private Color lightColor = Color.LightGray;

        /// <summary>
        /// Gets or sets the color for deepening.
        /// </summary>
        /// <value>The color of the deep.</value>
        public Color DeepColor
        {
            get { return deepColor; }
            set
            {
                deepColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color for lightening.
        /// </summary>
        /// <value>The color of the light.</value>
        public Color LightColor
        {
            get { return lightColor; }
            set
            {
                lightColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// The line width
        /// </summary>
        private int lineWidth = 1;

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        public int LineWidth
        {
            get { return lineWidth; }
            set
            {
                lineWidth = value;
                Invalidate();
            }
        }





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
        

        /// <summary>
        /// Overrides <c>Label.OnPaint</c> method.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            PlaceLine(e.Graphics);
            base.OnPaint(e);
        }

        /// <summary>
        /// Calculates points for 3D horizontal divider and places it.
        /// </summary>
        /// <param name="g"><c>Graphics</c> object.</param>
        protected void PlaceLine(Graphics g)
        {
            // evaluates text size
            SizeF textSize = g.MeasureString(this.Text, this.Font);
            int x0 = 0;           // first point x-coordinate
            int x1 = this.Width;  // second point x-coordinate
            // for different horizontal alignments recalculates x-coordinates
            switch (GetHorizontalAlignment())
            {
                case HorizontalAlignment.Left:
                    x0 = (int)textSize.Width + m_gap;
                    break;
                case HorizontalAlignment.Right:
                    x1 = this.Width - (int)textSize.Width - m_gap;
                    break;
                case HorizontalAlignment.Center:
                    x1 = (this.Width - (int)textSize.Width) / 2 - m_gap;
                    break;
            }
            int y = (int)textSize.Height / 2;
            // for different vertical alignments recalculates y-coordinate           
            if (TextAlign == System.Drawing.ContentAlignment.MiddleLeft
                || TextAlign == System.Drawing.ContentAlignment.MiddleCenter
                || TextAlign == System.Drawing.ContentAlignment.MiddleRight)
                y = this.Height / 2;
            else if (TextAlign == System.Drawing.ContentAlignment.BottomLeft
                || TextAlign == System.Drawing.ContentAlignment.BottomCenter
                || TextAlign == System.Drawing.ContentAlignment.BottomRight)
                y = this.Height - (int)(textSize.Height / 2) - 2;

            Draw3DLine(g, x0, y, x1, y);
            // for centered text, two line sections have to be drawn
            if (TextAlign == System.Drawing.ContentAlignment.TopCenter
                || TextAlign == System.Drawing.ContentAlignment.MiddleCenter
                || TextAlign == System.Drawing.ContentAlignment.BottomCenter)
            {
                x0 = (this.Width + (int)textSize.Width) / 2 + m_gap;
                x1 = this.Width;
                Draw3DLine(g, x0, y, x1, y);
            }
        }

        /// <summary>
        /// Evaluates horizontal alignment depending on <c>TextAlign</c> and
        /// <c>RightToLeft</c> settings.
        /// </summary>
        /// <returns>One of the <c>HorizontalAlignment</c> values.</returns>
        protected HorizontalAlignment GetHorizontalAlignment()
        {
            if (TextAlign == System.Drawing.ContentAlignment.TopLeft
                || TextAlign == System.Drawing.ContentAlignment.MiddleLeft
                || TextAlign == System.Drawing.ContentAlignment.BottomLeft)
            {
                if (RightToLeft == RightToLeft.Yes)
                    return HorizontalAlignment.Right;
                else
                    return HorizontalAlignment.Left;
            }
            if (TextAlign == System.Drawing.ContentAlignment.TopRight
                || TextAlign == System.Drawing.ContentAlignment.MiddleRight
                || TextAlign == System.Drawing.ContentAlignment.BottomRight)
            {
                if (RightToLeft == RightToLeft.Yes)
                    return HorizontalAlignment.Left;
                else
                    return HorizontalAlignment.Right;
            }
            return HorizontalAlignment.Center;
        }

        /// <summary>
        /// Draws 3D horizontal divider line
        /// </summary>
        /// <param name="g"><c>Graphics</c> object.</param>
        /// <param name="x1">x-coordinate of the first point.</param>
        /// <param name="y1">y-coordinate of the first point.</param>
        /// <param name="x2">x-coordinate of the second point.</param>
        /// <param name="y2">y-coordinate of the second point.</param>
        protected void Draw3DLine(Graphics g, int x1, int y1, int x2, int y2)
        {
            Pen line1 = new Pen(deepColor, lineWidth);
            Pen line2 = new Pen(lightColor, lineWidth);

            line1.DashStyle = dashstyle;
            line2.DashStyle = dashstyle;

            line1.StartCap = startCap;
            line2.StartCap = startCap;

            line1.EndCap = endCap;
            line2.EndCap = endCap;

            
            g.DrawLine(line1 /*SystemPens.ControlDark*/, x1, y1, x2, y2);
            g.DrawLine(line2 /*SystemPens.ControlLightLight*/, x1, y1 + 1, x2, y2 + 1);
        }

        /// <summary>
        /// The m gap
        /// </summary>
        private int m_gap;
    }

    #endregion

}