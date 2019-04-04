// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 01-01-2019
//
// Last Modified By : ZEROIT
// Last Modified On : 01-01-2019
// ***********************************************************************
// <copyright file="ZeroitDottedMatrix.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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
    /// Class ZeroitDottedMatrix.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public class ZeroitDottedMatrix : Control
    {
        #region Private Fields
        /// <summary>
        /// The i automatic
        /// </summary>
        private bool iAuto;
        /// <summary>
        /// The d height
        /// </summary>
        private float dHeight = 5;
        /// <summary>
        /// The d width
        /// </summary>
        private float dWidth = 5;
        /// <summary>
        /// The d space
        /// </summary>
        private int dSpace = 0;
        /// <summary>
        /// The c on
        /// </summary>
        private Color cOn = Color.LightGreen;
        /// <summary>
        /// The c on shadow
        /// </summary>
        private Color cOnShadow = Color.Green;
        /// <summary>
        /// The c off
        /// </summary>
        private Color cOff = Color.Transparent;
        /// <summary>
        /// The c off shadow
        /// </summary>
        private Color cOffShadow = Color.Transparent;
        #endregion

        #region Struct

        /// <summary>
        /// Struct tMatrix
        /// </summary>
        public struct tMatrix
        {
            /// <summary>
            /// The dot
            /// </summary>
            public bool[,] Dot;
        }

        #endregion

        #region  Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitDottedMatrix"/> class.
        /// </summary>
        public ZeroitDottedMatrix()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            this.Size = new System.Drawing.Size(288, 33);

        }

        #endregion

        #region Properties

        /// <summary>
        /// This property is not relevant for this class.
        /// </summary>
        /// <value><c>true</c> if [automatic size]; otherwise, <c>false</c>.</value>
        public bool AutoSize
        {
            get
            {
                return iAuto;
            }
            set
            {
                iAuto = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the on.
        /// </summary>
        /// <value>The color of the on.</value>
        public Color OnColor
        {
            get
            {
                return cOn;
            }
            set
            {
                cOn = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the on color shadow.
        /// </summary>
        /// <value>The on color shadow.</value>
        public Color OnColorShadow
        {
            get
            {
                return cOnShadow;
            }
            set
            {
                cOnShadow = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the off.
        /// </summary>
        /// <value>The color of the off.</value>
        public Color OffColor
        {
            get
            {
                return cOff;
            }
            set
            {
                cOff = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the off color shadow.
        /// </summary>
        /// <value>The off color shadow.</value>
        public Color OffColorShadow
        {
            get
            {
                return cOffShadow;
            }
            set
            {
                cOffShadow = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the height of the dot.
        /// </summary>
        /// <value>The height of the dot.</value>
        public float DotHeight
        {
            get
            {
                return dHeight;
            }
            set
            {
                dHeight = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the dot.
        /// </summary>
        /// <value>The width of the dot.</value>
        public float DotWidth
        {
            get
            {
                return dWidth;
            }
            set
            {
                dWidth = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the dot space.
        /// </summary>
        /// <value>The dot space.</value>
        public int DotSpace
        {
            get
            {
                return dSpace;
            }
            set
            {
                dSpace = value;
                Invalidate();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Redraws the specified e graphics.
        /// </summary>
        /// <param name="eGraphics">The e graphics.</param>
        private void Redraw(Graphics eGraphics)
        {
            //declare brushes and graphic objects
            int i = 0;
            int ii = 0;
            int iii = 0;
            char[] txArray = Text.ToCharArray();
            tMatrix[] DotMatrix = new tMatrix[txArray.Length];
            Graphics g = eGraphics;
            //SolidBrush bBrush = new SolidBrush(bColor);

            //enable smoothing to make it look clean
            g.SmoothingMode = SmoothingMode.HighQuality;

            //draw background
            //g.FillRectangle(bBrush, 0, 0, this.Size.Width, this.Size.Height);
            //g.Clear(BackColor);

            //set all dots in the matrix to off color
            for (i = 0; i < DotMatrix.Length; i++)
            {
                DotMatrix[i].Dot = new bool[6, 5];
                for (ii = 0; ii <= 5; ii++)
                {
                    for (iii = 0; iii <= 4; iii++)
                    {
                        DotMatrix[i].Dot[ii, iii] = false;
                    }
                }
            }

            //loop through all characters in text
            for (i = 0; i < txArray.Length; i++)
            {
                switch (Convert.ToString(char.ToLower(txArray[i])))
                {

                    case "a":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "b":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "c":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "d":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "e":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "f":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        break;
                    case "g":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "h":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "i":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[2, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[2, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "j":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[3, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[3, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        break;
                    case "k":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[3, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[3, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "l":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "m":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[1, 1] = true;
                        DotMatrix[i].Dot[3, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "n":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[1, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[3, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "0":
                    case "o":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "p":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        break;
                    case "q":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[3, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "r":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[3, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "s":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "t":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[2, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[2, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[2, 4] = true;
                        break;
                    case "u":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "v":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[1, 3] = true;
                        DotMatrix[i].Dot[3, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[2, 4] = true;
                        break;
                    case "w":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[2, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[2, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "x":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[1, 1] = true;
                        DotMatrix[i].Dot[3, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[1, 3] = true;
                        DotMatrix[i].Dot[3, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "y":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[1, 1] = true;
                        DotMatrix[i].Dot[3, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[2, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[2, 4] = true;
                        break;
                    case "z":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[3, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[1, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "1":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[2, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[2, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "2":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[1, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        DotMatrix[i].Dot[4, 4] = true;
                        break;
                    case "3":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "4":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[3, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[3, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "5":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "6":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[0, 2] = true;
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "7":
                        //row 0
                        DotMatrix[i].Dot[0, 0] = true;
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[2, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[2, 4] = true;
                        break;
                    case "8":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[0, 3] = true;
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case "9":
                        //row 0
                        DotMatrix[i].Dot[1, 0] = true;
                        DotMatrix[i].Dot[2, 0] = true;
                        DotMatrix[i].Dot[3, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[0, 1] = true;
                        DotMatrix[i].Dot[4, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[1, 2] = true;
                        DotMatrix[i].Dot[2, 2] = true;
                        DotMatrix[i].Dot[3, 2] = true;
                        DotMatrix[i].Dot[4, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[4, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        DotMatrix[i].Dot[2, 4] = true;
                        DotMatrix[i].Dot[3, 4] = true;
                        break;
                    case ":":
                        //row 1
                        DotMatrix[i].Dot[2, 1] = true;
                        //row 3
                        DotMatrix[i].Dot[2, 3] = true;
                        break;
                    case ".":
                        //row 4
                        DotMatrix[i].Dot[2, 4] = true;
                        break;
                    case "/":
                        //row 0
                        DotMatrix[i].Dot[4, 0] = true;
                        //row 1
                        DotMatrix[i].Dot[3, 1] = true;
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[1, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[0, 4] = true;
                        break;
                    case ",":
                        //row 2
                        DotMatrix[i].Dot[2, 2] = true;
                        //row 3
                        DotMatrix[i].Dot[2, 3] = true;
                        //row 4
                        DotMatrix[i].Dot[1, 4] = true;
                        break;
                }
            }

            //draw the matrix
            int cWidth = 5;
            for (i = 0; i < txArray.Length; i++)
            {
                //defines char width (if last char in string, dont draw space)
                if (i == txArray.Length - 1)
                {
                    cWidth = 4;
                }
                else
                {
                    cWidth = 5;
                }
                for (ii = 0; ii <= cWidth; ii++)
                {
                    for (iii = 0; iii <= 4; iii++)
                    {
                        int dotX = 0;
                        int dotY = 0;
                        int offsetX = 0;
                        offsetX = i * (Convert.ToInt32(dWidth) + dSpace) * 6;
                        dotX = offsetX + (ii) * (Convert.ToInt32(dWidth) + dSpace);
                        dotY = (iii) * (Convert.ToInt32(dHeight) + dSpace);
                        Rectangle DotArea = new Rectangle(dotX, dotY, Convert.ToInt32(dWidth), Convert.ToInt32(dHeight));
                        LinearGradientBrush onBrush = new LinearGradientBrush(DotArea, cOn, cOnShadow, LinearGradientMode.ForwardDiagonal);
                        LinearGradientBrush offBrush = new LinearGradientBrush(DotArea, cOff, cOffShadow, LinearGradientMode.ForwardDiagonal);
                        if (DotMatrix[i].Dot[ii, iii] == true)
                        {
                            g.FillEllipse(onBrush, dotX, dotY, dWidth, dHeight);
                        }
                        else
                        {
                            g.FillEllipse(offBrush, dotX, dotY, dWidth, dHeight);
                        }
                    }
                }
            }

            //autosize if enabled
            if (iAuto == true)
            {
                int newWidth = 0;
                int newHeight = 0;
                newWidth = txArray.Length * Convert.ToInt32(dWidth) * 6;
                newHeight = 5 * Convert.ToInt32(dHeight);
                this.Width = newWidth;
                this.Height = newHeight;
            }
        }

        #endregion

        #region Overrides
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            Redraw(e.Graphics);

        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DotWidth = 0.0091164804469274f * Width;
            DotHeight = 0.1785714285714286f * Height;
        }

        #endregion

        #region Transparency


        #region Include in Paint

        /// <summary>
        /// Transes the in paint.
        /// </summary>
        /// <param name="g">The g.</param>
        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        /// <summary>
        /// The allow transparency
        /// </summary>
        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [allow transparency].
        /// </summary>
        /// <value><c>true</c> if [allow transparency]; otherwise, <c>false</c>.</value>
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

        /// <summary>
        /// Makes the transparent.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="g">The g.</param>
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


    }
}
