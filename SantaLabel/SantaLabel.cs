// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="SantaLabel.cs" company="Zeroit Dev Technologies">
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

    #region Santa Label


    /// <summary>
    /// A class collection for rendering gradient label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    public class ZeroitSantaGradientLabel : Label
    {

        #region Implementtation Member Fields
        /// <summary>
        /// The gradient color one
        /// </summary>
        protected Color gradientColorOne = Color.White;
        /// <summary>
        /// The gradient color two
        /// </summary>
        protected Color gradientColorTwo = Color.Blue;
        /// <summary>
        /// The LGM
        /// </summary>
        protected LinearGradientMode lgm = LinearGradientMode.ForwardDiagonal;
        /// <summary>
        /// The b3dstyle
        /// </summary>
        protected Border3DStyle b3dstyle = Border3DStyle.Bump;

        
        /// <summary>
        /// The border color
        /// </summary>
        private Color borderColor = Color.Black;
        #endregion

        #region GradientColorOne Properties
        /// <summary>
        /// Gets or sets the gradient color.
        /// </summary>
        /// <value>The gradient color one.</value>
        [
        DefaultValue(typeof(Color), "White"),
        Description("The first gradient color."),
        Category("Appearance"),
        ]

        //GradientColorOne Properties
        public Color GradientColorOne
        {
            get
            {
                return gradientColorOne;
            }
            set
            {
                gradientColorOne = value;
                Invalidate();
            }
        }
        #endregion

        #region GradientColorTwo Properties
        /// <summary>
        /// Gets or sets the gradient color.
        /// </summary>
        /// <value>The gradient color two.</value>
        [
        DefaultValue(typeof(Color), "Blue"),
        Description("The second gradient color."),
        Category("Appearance"),
        ]

        //GradientColorTwo Properties
        public Color GradientColorTwo
        {
            get
            {
                return gradientColorTwo;
            }
            set
            {
                gradientColorTwo = value;
                Invalidate();
            }
        }

        #endregion

        #region LinearGradientMode Properties
        //LinearGradientMode Properties
        /// <summary>
        /// Gets or sets the linear gradient mode.
        /// </summary>
        /// <value>The gradient mode.</value>
        [
        DefaultValue(typeof(LinearGradientMode), "ForwardDiagonal"),
        Description("Gradient Mode"),
        Category("Appearance"),
        ]

        public LinearGradientMode GradientMode
        {
            get
            {
                return lgm;
            }

            set
            {
                lgm = value;
                Invalidate();
            }
        }
        #endregion

        #region Border3DStyle Properties


        /// <summary>
        /// Enum Border3DStyle
        /// </summary>
        public enum Border3DStyle
        {
            /// <summary>
            /// The default
            /// </summary>
            Default,
            /// <summary>
            /// The adjust
            /// </summary>
            Adjust,
            /// <summary>
            /// The bump
            /// </summary>
            Bump,
            /// <summary>
            /// The etched
            /// </summary>
            Etched,
            /// <summary>
            /// The flat
            /// </summary>
            Flat,
            /// <summary>
            /// The raised
            /// </summary>
            Raised,
            /// <summary>
            /// The raised inner
            /// </summary>
            RaisedInner,
            /// <summary>
            /// The raised outer
            /// </summary>
            RaisedOuter,
            /// <summary>
            /// The sunken
            /// </summary>
            Sunken,
            /// <summary>
            /// The sunken inner
            /// </summary>
            SunkenInner,
            /// <summary>
            /// The sunken outer
            /// </summary>
            SunkenOuter,
            /// <summary>
            /// The none
            /// </summary>
            None

        }


        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        public Border3DStyle BorderStyle
        {
            get { return b3dstyle; }
            set
            {
                b3dstyle = value;
                Invalidate();
            }
        }


        //Border3DStyle Properties
        /// <summary>
        /// Gets or sets the border style for the control.
        /// </summary>
        /// <value>The border style.</value>
        [
        DefaultValue(typeof(Border3DStyle), "Bump"),
        Description("BorderStyle"),
        Category("Appearance"),
        ]

        #endregion

        #region Removed Properties

        // Remove BackColor Property
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>The color of the back.</value>
        [
        Browsable(false),
        EditorBrowsable(EditorBrowsableState.Never)
        ]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return new System.Drawing.Color();
            }
            set {; }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSantaGradientLabel" /> class.
        /// </summary>
        public ZeroitSantaGradientLabel()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        //		{
        //			Graphics gfx = e.Graphics;
        //			//Border3DStyle b3dstyle = Border3DStyle.Bump;
        //			//Border3DSide b3dside = Border3DSide.All;
        //			
        //			Rectangle rect = new Rectangle (0,0,this.Width,this.Height);
        //
        //			// Dispose of brush resources after use
        //			using (LinearGradientBrush lgb = new LinearGradientBrush(rect, gradientColorOne,gradientColorTwo,lgm))
        //			gfx.FillRectangle(lgb,rect);
        //			
        //			//3d border
        //			//ControlPaint.DrawBorder3D(gfx,rect,b3dstyle,b3dside);
        //			
        //						
        //			// Call the OnPaint method of the base class
        //            base.OnPaint(e);
        //			
        //		}

        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            Graphics gfx = pevent.Graphics;

            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            // Dispose of brush resources after use
            using (LinearGradientBrush lgb = new LinearGradientBrush(rect, gradientColorOne, gradientColorTwo, lgm))
                gfx.FillRectangle(lgb, rect);

            switch (BorderStyle)
            {
                case Border3DStyle.Default:
                    gfx.DrawRectangle(new Pen(BorderColor), rect);
                    
                    break;
                case Border3DStyle.Adjust:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.Adjust);

                    break;
                case Border3DStyle.Bump:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.Bump);

                    break;
                case Border3DStyle.Etched:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.Etched);

                    break;
                case Border3DStyle.Flat:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.Flat);

                    break;
                case Border3DStyle.Raised:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.Raised);

                    break;
                case Border3DStyle.RaisedInner:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.RaisedInner);

                    break;
                case Border3DStyle.RaisedOuter:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.RaisedOuter);

                    break;
                case Border3DStyle.Sunken:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.Sunken);

                    break;
                case Border3DStyle.SunkenInner:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.SunkenInner);

                    break;
                case Border3DStyle.SunkenOuter:
                    ControlPaint.DrawBorder3D(gfx, rect, System.Windows.Forms.Border3DStyle.SunkenOuter);

                    break;
                case Border3DStyle.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

    }


    #endregion

}