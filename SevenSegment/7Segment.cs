// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="7Segment.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region SevenSegment Led Control

    #region SevenSegment

    /// <summary>
    /// A class collection for rendering seven segment led label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public class ZeroitShortSegLed : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitShortSegLed" /> class.
        /// </summary>
        public ZeroitShortSegLed()
        {
            this.SuspendLayout();
            this.Name = "SevenSegment";
            this.Size = new System.Drawing.Size(32, 64);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SevenSegment_Paint);
            this.Resize += new System.EventHandler(this.SevenSegment_Resize);
            this.ResumeLayout(false);

            this.TabStop = false;
            this.Padding = new Padding(4, 4, 4, 4);
            this.DoubleBuffered = true;

            segPoints = new Point[7][];
            for (int i = 0; i < 7; i++) segPoints[i] = new Point[6];

            RecalculatePoints();
        }

        /// <summary>
        /// Recalculate the points that represent the polygons of the
        /// seven segments, whether we're just initializing or
        /// changing the segment width.
        /// </summary>
        private void RecalculatePoints()
        {
            int halfHeight = gridHeight / 2, halfWidth = elementWidth / 2;

            int p = 0;
            segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = 0;
            segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = 0;
            segPoints[p][2].X = gridWidth - halfWidth - 1; segPoints[p][2].Y = halfWidth;
            segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = elementWidth;
            segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = elementWidth;
            segPoints[p][5].X = halfWidth + 1; segPoints[p][5].Y = halfWidth;

            p++;
            segPoints[p][0].X = 0; segPoints[p][0].Y = elementWidth + 1;
            segPoints[p][1].X = halfWidth; segPoints[p][1].Y = halfWidth + 1;
            segPoints[p][2].X = elementWidth; segPoints[p][2].Y = elementWidth + 1;
            segPoints[p][3].X = elementWidth; segPoints[p][3].Y = halfHeight - halfWidth - 1;
            segPoints[p][4].X = 4; segPoints[p][4].Y = halfHeight - 1;
            segPoints[p][5].X = 0; segPoints[p][5].Y = halfHeight - 1;

            p++;
            segPoints[p][0].X = gridWidth - elementWidth; segPoints[p][0].Y = elementWidth + 1;
            segPoints[p][1].X = gridWidth - halfWidth; segPoints[p][1].Y = halfWidth + 1;
            segPoints[p][2].X = gridWidth; segPoints[p][2].Y = elementWidth + 1;
            segPoints[p][3].X = gridWidth; segPoints[p][3].Y = halfHeight - 1;
            segPoints[p][4].X = gridWidth - 4; segPoints[p][4].Y = halfHeight - 1;
            segPoints[p][5].X = gridWidth - elementWidth; segPoints[p][5].Y = halfHeight - halfWidth - 1;

            p++;
            segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = halfHeight - halfWidth;
            segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = halfHeight - halfWidth;
            segPoints[p][2].X = gridWidth - 5; segPoints[p][2].Y = halfHeight;
            segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = halfHeight + halfWidth;
            segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = halfHeight + halfWidth;
            segPoints[p][5].X = 5; segPoints[p][5].Y = halfHeight;

            p++;
            segPoints[p][0].X = 0; segPoints[p][0].Y = halfHeight + 1;
            segPoints[p][1].X = 4; segPoints[p][1].Y = halfHeight + 1;
            segPoints[p][2].X = elementWidth; segPoints[p][2].Y = halfHeight + halfWidth + 1;
            segPoints[p][3].X = elementWidth; segPoints[p][3].Y = gridHeight - elementWidth - 1;
            segPoints[p][4].X = halfWidth; segPoints[p][4].Y = gridHeight - halfWidth - 1;
            segPoints[p][5].X = 0; segPoints[p][5].Y = gridHeight - elementWidth - 1;

            p++;
            segPoints[p][0].X = gridWidth - elementWidth; segPoints[p][0].Y = halfHeight + halfWidth + 1;
            segPoints[p][1].X = gridWidth - 4; segPoints[p][1].Y = halfHeight + 1;
            segPoints[p][2].X = gridWidth; segPoints[p][2].Y = halfHeight + 1;
            segPoints[p][3].X = gridWidth; segPoints[p][3].Y = gridHeight - elementWidth - 1;
            segPoints[p][4].X = gridWidth - halfWidth; segPoints[p][4].Y = gridHeight - halfWidth - 1;
            segPoints[p][5].X = gridWidth - elementWidth; segPoints[p][5].Y = gridHeight - elementWidth - 1;

            p++;
            segPoints[p][0].X = elementWidth + 1; segPoints[p][0].Y = gridHeight - elementWidth;
            segPoints[p][1].X = gridWidth - elementWidth - 1; segPoints[p][1].Y = gridHeight - elementWidth;
            segPoints[p][2].X = gridWidth - halfWidth - 1; segPoints[p][2].Y = gridHeight - halfWidth;
            segPoints[p][3].X = gridWidth - elementWidth - 1; segPoints[p][3].Y = gridHeight;
            segPoints[p][4].X = elementWidth + 1; segPoints[p][4].Y = gridHeight;
            segPoints[p][5].X = halfWidth + 1; segPoints[p][5].Y = gridHeight - halfWidth;
        }

        /// <summary>
        /// The seg points
        /// </summary>
        private Point[][] segPoints;

        /// <summary>
        /// The grid height
        /// </summary>
        private int gridHeight = 80;
        /// <summary>
        /// The grid width
        /// </summary>
        private int gridWidth = 48;
        /// <summary>
        /// The element width
        /// </summary>
        private int elementWidth = 10;
        /// <summary>
        /// The italic factor
        /// </summary>
        private float italicFactor = 0.0F;
        /// <summary>
        /// The color background
        /// </summary>
        private Color colorBackground = Color.DarkGray;
        /// <summary>
        /// The color dark
        /// </summary>
        private Color colorDark = Color.DimGray;
        /// <summary>
        /// The color light
        /// </summary>
        private Color colorLight = Color.Red;


        /// <summary>
        /// Background color of the 7-segment display.
        /// </summary>
        /// <value>The color background.</value>
        public Color ColorBackground { get { return colorBackground; } set { colorBackground = value; Invalidate(); } }
        /// <summary>
        /// Color of inactive LED segments.
        /// </summary>
        /// <value>The color dark.</value>
        public Color ColorDark { get { return colorDark; } set { colorDark = value; Invalidate(); } }
        /// <summary>
        /// Color of active LED segments.
        /// </summary>
        /// <value>The color light.</value>
        public Color ColorLight { get { return colorLight; } set { colorLight = value; Invalidate(); } }

        /// <summary>
        /// Width of LED segments.
        /// </summary>
        /// <value>The width of the element.</value>
        public int ElementWidth { get { return elementWidth; } set { elementWidth = value; RecalculatePoints(); Invalidate(); } }
        /// <summary>
        /// Shear coefficient for italicizing the displays. Try a value like -0.1.
        /// </summary>
        /// <value>The italic factor.</value>
        public float ItalicFactor { get { return italicFactor; } set { italicFactor = value; Invalidate(); } }

        /// <summary>
        /// Handles the Resize event of the SevenSegment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SevenSegment_Resize(object sender, EventArgs e) { this.Invalidate(); }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnPaddingChanged(EventArgs e) { base.OnPaddingChanged(e); this.Invalidate(); }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
            e.Graphics.Clear(colorBackground);
        }

        /// <summary>
        /// These are the various bit patterns that represent the characters
        /// that can be displayed in the seven segments. Bits 0 through 6
        /// correspond to each of the LEDs, from top to bottom!
        /// </summary>
        public enum ValuePattern
        {
            /// <summary>
            /// Set the value pattern to none.
            /// </summary>
            None = 0x0,
            /// <summary>
            /// Set the value pattern to zero.
            /// </summary>
            Zero = 0x77,
            /// <summary>
            /// Set the value pattern to one.
            /// </summary>
            One = 0x24,
            /// <summary>
            /// Set the value pattern to two.
            /// </summary>
            Two = 0x5D,
            /// <summary>
            /// Set the value pattern to three.
            /// </summary>
            Three = 0x6D,
            /// <summary>
            /// Set the value pattern to four.
            /// </summary>
            Four = 0x2E,
            /// <summary>
            /// Set the value pattern to five.
            /// </summary>
            Five = 0x6B,
            /// <summary>
            /// Set the value pattern to six.
            /// </summary>
            Six = 0x7B,
            /// <summary>
            /// Set the value pattern to seven.
            /// </summary>
            Seven = 0x25,
            /// <summary>
            /// Set the value pattern to eight.
            /// </summary>
            Eight = 0x7F,
            /// <summary>
            /// Set the value pattern to nine.
            /// </summary>
            Nine = 0x6F,
            /// <summary>
            /// Set the value pattern to A.
            /// </summary>
            A = 0x3F,
            /// <summary>
            /// Set the value pattern to B.
            /// </summary>
            B = 0x7A,
            /// <summary>
            /// Set the value pattern to C.
            /// </summary>
            C = 0x53,
            /// <summary>
            /// Set the value pattern to D.
            /// </summary>
            D = 0x7C,
            /// <summary>
            /// Set the value pattern to E.
            /// </summary>
            E = 0x5B,
            /// <summary>
            /// Set the value pattern to F.
            /// </summary>
            F = 0x1B,
            /// <summary>
            /// Set the value pattern to G.
            /// </summary>
            G = 0x73,
            /// <summary>
            /// Set the value pattern to H.
            /// </summary>
            H = 0x3E,
            /// <summary>
            /// Set the value pattern to J.
            /// </summary>
            J = 0x74,
            /// <summary>
            /// Set the value pattern to L.
            /// </summary>
            L = 0x52,
            /// <summary>
            /// Set the value pattern to N.
            /// </summary>
            N = 0x38,
            /// <summary>
            /// Set the value pattern to O.
            /// </summary>
            O = 0x78,
            /// <summary>
            /// Set the value pattern to P.
            /// </summary>
            P = 0x1F,
            /// <summary>
            /// Set the value pattern to Q.
            /// </summary>
            Q = 0x2F,
            /// <summary>
            /// Set the value pattern to R.
            /// </summary>
            R = 0x18,
            /// <summary>
            /// Set the value pattern to T.
            /// </summary>
            T = 0x5A,
            /// <summary>
            /// Set the value pattern to U.
            /// </summary>
            U = 0x76,
            /// <summary>
            /// Set the value pattern to Y.
            /// </summary>
            Y = 0x6E,
            /// <summary>
            /// Set the value pattern to Dash.
            /// </summary>
            Dash = 0x8,
            /// <summary>
            /// Set the value pattern to Equals.
            /// </summary>
            Equals = 0x48
        }

        /// <summary>
        /// The value
        /// </summary>
        private string theValue = null;

        /// <summary>
        /// Character to be displayed on the seven segments. Supported characters
        /// are digits and most letters.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return theValue; }
            set
            {
                customPattern = 0;
                if (value != null)
                {
                    //is it an integer?
                    bool success = false;
                    try
                    {
                        int tempValue = Convert.ToInt32(value);
                        if (tempValue > 9) tempValue = 9; if (tempValue < 0) tempValue = 0;
                        switch (tempValue)
                        {
                            case 0: customPattern = (int)ValuePattern.Zero; break;
                            case 1: customPattern = (int)ValuePattern.One; break;
                            case 2: customPattern = (int)ValuePattern.Two; break;
                            case 3: customPattern = (int)ValuePattern.Three; break;
                            case 4: customPattern = (int)ValuePattern.Four; break;
                            case 5: customPattern = (int)ValuePattern.Five; break;
                            case 6: customPattern = (int)ValuePattern.Six; break;
                            case 7: customPattern = (int)ValuePattern.Seven; break;
                            case 8: customPattern = (int)ValuePattern.Eight; break;
                            case 9: customPattern = (int)ValuePattern.Nine; break;
                        }
                        success = true;
                    }
                    catch { }
                    if (!success)
                    {
                        try
                        {
                            //is it a letter?
                            string tempString = Convert.ToString(value);
                            switch (tempString.ToLower()[0])
                            {
                                case 'a': customPattern = (int)ValuePattern.A; break;
                                case 'b': customPattern = (int)ValuePattern.B; break;
                                case 'c': customPattern = (int)ValuePattern.C; break;
                                case 'd': customPattern = (int)ValuePattern.D; break;
                                case 'e': customPattern = (int)ValuePattern.E; break;
                                case 'f': customPattern = (int)ValuePattern.F; break;
                                case 'g': customPattern = (int)ValuePattern.G; break;
                                case 'h': customPattern = (int)ValuePattern.H; break;
                                case 'j': customPattern = (int)ValuePattern.J; break;
                                case 'l': customPattern = (int)ValuePattern.L; break;
                                case 'n': customPattern = (int)ValuePattern.N; break;
                                case 'o': customPattern = (int)ValuePattern.O; break;
                                case 'p': customPattern = (int)ValuePattern.P; break;
                                case 'q': customPattern = (int)ValuePattern.Q; break;
                                case 'r': customPattern = (int)ValuePattern.R; break;
                                case 't': customPattern = (int)ValuePattern.T; break;
                                case 'u': customPattern = (int)ValuePattern.U; break;
                                case 'y': customPattern = (int)ValuePattern.Y; break;
                                case '-': customPattern = (int)ValuePattern.Dash; break;
                                case '=': customPattern = (int)ValuePattern.Equals; break;
                            }
                        }
                        catch { }
                    }
                }
                theValue = value; Invalidate();
            }
        }

        /// <summary>
        /// The custom pattern
        /// </summary>
        private int customPattern = 0;
        /// <summary>
        /// Set a custom bit pattern to be displayed on the seven segments. This is an
        /// integer value where bits 0 through 6 correspond to each respective LED
        /// segment.
        /// </summary>
        /// <value>The custom pattern.</value>
        public int CustomPattern { get { return customPattern; } set { customPattern = value; Invalidate(); } }

        /// <summary>
        /// The show dot
        /// </summary>
        private bool showDot = true, dotOn = false;
        /// <summary>
        /// Specifies if the decimal point LED is displayed.
        /// </summary>
        /// <value><c>true</c> if [decimal show]; otherwise, <c>false</c>.</value>
        public bool DecimalShow { get { return showDot; } set { showDot = value; Invalidate(); } }
        /// <summary>
        /// Specifies if the decimal point LED is active.
        /// </summary>
        /// <value><c>true</c> if [decimal on]; otherwise, <c>false</c>.</value>
        public bool DecimalOn { get { return dotOn; } set { dotOn = value; Invalidate(); } }


        /// <summary>
        /// Handles the Paint event of the SevenSegment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void SevenSegment_Paint(object sender, PaintEventArgs e)
        {
            int useValue = customPattern;

            Brush brushLight = new SolidBrush(colorLight);
            Brush brushDark = new SolidBrush(colorDark);

            // Define transformation for our container...
            RectangleF srcRect = new RectangleF(0.0F, 0.0F, gridWidth, gridHeight);
            RectangleF destRect = new RectangleF(Padding.Left, Padding.Top, this.Width - Padding.Left - Padding.Right, this.Height - Padding.Top - Padding.Bottom);

            // Begin graphics container that remaps coordinates for our convenience
            GraphicsContainer containerState = e.Graphics.BeginContainer(destRect, srcRect, GraphicsUnit.Pixel);

            Matrix trans = new Matrix();
            trans.Shear(italicFactor, 0.0F);
            e.Graphics.Transform = trans;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Default;

            // Draw elements based on whether the corresponding bit is high
            e.Graphics.FillPolygon((useValue & 0x1) == 0x1 ? brushLight : brushDark, segPoints[0]);
            e.Graphics.FillPolygon((useValue & 0x2) == 0x2 ? brushLight : brushDark, segPoints[1]);
            e.Graphics.FillPolygon((useValue & 0x4) == 0x4 ? brushLight : brushDark, segPoints[2]);
            e.Graphics.FillPolygon((useValue & 0x8) == 0x8 ? brushLight : brushDark, segPoints[3]);
            e.Graphics.FillPolygon((useValue & 0x10) == 0x10 ? brushLight : brushDark, segPoints[4]);
            e.Graphics.FillPolygon((useValue & 0x20) == 0x20 ? brushLight : brushDark, segPoints[5]);
            e.Graphics.FillPolygon((useValue & 0x40) == 0x40 ? brushLight : brushDark, segPoints[6]);

            if (showDot)
                e.Graphics.FillEllipse(dotOn ? brushLight : brushDark, gridWidth - 1, gridHeight - elementWidth + 1, elementWidth, elementWidth);

            e.Graphics.EndContainer(containerState);
        }


    }

    #endregion

    #region SevenSegmentArray

    /// <summary>
    /// A class collection for rendering long seven segment led label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public class ZeroitLongSegLed : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLongSegLed" /> class.
        /// </summary>
        public ZeroitLongSegLed()
        {
            this.SuspendLayout();
            this.Name = "SevenSegmentArray";
            this.Size = new System.Drawing.Size(100, 25);
            this.Resize += new System.EventHandler(this.SevenSegmentArray_Resize);
            this.ResumeLayout(false);

            this.TabStop = false;
            elementPadding = new Padding(4, 4, 4, 4);
            RecreateSegments(4);
        }


        /// <summary>
        /// Array of segment controls that are currently children of this control.
        /// </summary>
        private ZeroitShortSegLed[] segments = null;

        /// <summary>
        /// Change the number of elements in our LED array. This destroys
        /// the previous elements, and creates new ones in their place, applying
        /// all the current options to the new ones.
        /// </summary>
        /// <param name="count">Number of elements to create.</param>
        private void RecreateSegments(int count)
        {
            if (segments != null)
                for (int i = 0; i < segments.Length; i++) { segments[i].Parent = null; segments[i].Dispose(); }

            if (count <= 0) return;
            segments = new ZeroitShortSegLed[count];

            for (int i = 0; i < count; i++)
            {
                segments[i] = new ZeroitShortSegLed();
                segments[i].Parent = this;
                segments[i].Top = 0;
                segments[i].Height = this.Height;
                segments[i].Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
                segments[i].Visible = true;
            }

            ResizeSegments();
            UpdateSegments();
            this.Value = theValue;
        }

        /// <summary>
        /// Align the elements of the array to fit neatly within the
        /// width of the parent control.
        /// </summary>
        private void ResizeSegments()
        {
            int segWidth = this.Width / segments.Length;
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i].Left = this.Width * (segments.Length - 1 - i) / segments.Length;
                segments[i].Width = segWidth;
            }
        }

        /// <summary>
        /// Update the properties of each element with the properties
        /// we have stored.
        /// </summary>
        private void UpdateSegments()
        {
            for (int i = 0; i < segments.Length; i++)
            {
                segments[i].ColorBackground = colorBackground;
                segments[i].ColorDark = colorDark;
                segments[i].ColorLight = colorLight;
                segments[i].ElementWidth = elementWidth;
                segments[i].ItalicFactor = italicFactor;
                segments[i].DecimalShow = showDot;
                segments[i].Padding = elementPadding;
            }
        }

        /// <summary>
        /// Handles the Resize event of the SevenSegmentArray control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SevenSegmentArray_Resize(object sender, EventArgs e) { ResizeSegments(); }

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaintBackground(PaintEventArgs e) { e.Graphics.Clear(colorBackground); }


        /// <summary>
        /// The element width
        /// </summary>
        private int elementWidth = 10;
        /// <summary>
        /// The italic factor
        /// </summary>
        private float italicFactor = 0.0F;
        /// <summary>
        /// The color background
        /// </summary>
        private Color colorBackground = Color.DarkGray;
        /// <summary>
        /// The color dark
        /// </summary>
        private Color colorDark = Color.DimGray;
        /// <summary>
        /// The color light
        /// </summary>
        private Color colorLight = Color.Red;
        /// <summary>
        /// The show dot
        /// </summary>
        private bool showDot = true;
        /// <summary>
        /// The element padding
        /// </summary>
        private Padding elementPadding;

        /// <summary>
        /// Background color of the LED array.
        /// </summary>
        /// <value>The color background.</value>
        public Color ColorBackground { get { return colorBackground; } set { colorBackground = value; UpdateSegments(); } }
        /// <summary>
        /// Color of inactive LED segments.
        /// </summary>
        /// <value>The color dark.</value>
        public Color ColorDark { get { return colorDark; } set { colorDark = value; UpdateSegments(); } }
        /// <summary>
        /// Color of active LED segments.
        /// </summary>
        /// <value>The color light.</value>
        public Color ColorLight { get { return colorLight; } set { colorLight = value; UpdateSegments(); } }

        /// <summary>
        /// Width of LED segments.
        /// </summary>
        /// <value>The width of the element.</value>
        public int ElementWidth { get { return elementWidth; } set { elementWidth = value; UpdateSegments(); } }
        /// <summary>
        /// Shear coefficient for italicizing the displays. Try a value like -0.1.
        /// </summary>
        /// <value>The italic factor.</value>
        public float ItalicFactor { get { return italicFactor; } set { italicFactor = value; UpdateSegments(); } }
        /// <summary>
        /// Specifies if the decimal point LED is displayed.
        /// </summary>
        /// <value><c>true</c> if [decimal show]; otherwise, <c>false</c>.</value>
        public bool DecimalShow { get { return showDot; } set { showDot = value; UpdateSegments(); } }

        /// <summary>
        /// Number of seven-segment elements in this array.
        /// </summary>
        /// <value>The array count.</value>
        public int ArrayCount { get { return segments.Length; } set { if ((value > 0) && (value <= 100)) RecreateSegments(value); } }
        /// <summary>
        /// Padding that applies to each seven-segment element in the array.
        /// Tweak these numbers to get the perfect appearance for the array of your size.
        /// </summary>
        /// <value>The element padding.</value>
        public Padding ElementPadding { get { return elementPadding; } set { elementPadding = value; UpdateSegments(); } }


        /// <summary>
        /// The value
        /// </summary>
        private string theValue = null;
        /// <summary>
        /// The value to be displayed on the LED array. This can contain numbers,
        /// certain letters, and decimal points.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return theValue; }
            set
            {
                theValue = value;
                for (int i = 0; i < segments.Length; i++) { segments[i].CustomPattern = 0; segments[i].DecimalOn = false; }
                if (theValue != null)
                {
                    int segmentIndex = 0;
                    for (int i = theValue.Length - 1; i >= 0; i--)
                    {
                        if (segmentIndex >= segments.Length) break;
                        if (theValue[i] == '.') segments[segmentIndex].DecimalOn = true;
                        else segments[segmentIndex++].Value = theValue[i].ToString();
                    }
                }
            }
        }

    }

    #endregion

    #endregion

}