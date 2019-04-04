// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ShadowLabel.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region ZeroitShadowLabel

    /// <summary>
    /// A class collection for rendering a label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    [ToolboxItem(true)]
    public class ZeroitShadowLabel : Label
    {

        #region Private Fields

        /// <summary>
        /// The draw gradient
        /// </summary>
        private bool _drawGradient = true;
        /// <summary>
        /// The start color
        /// </summary>
        private Color _startColor = Color.White;
        /// <summary>
        /// The end color
        /// </summary>
        private Color _endColor = Color.LightSkyBlue;
        /// <summary>
        /// The angle
        /// </summary>
        private float _angle = 0;

        /// <summary>
        /// The draw shadow
        /// </summary>
        private bool _drawShadow = true;
        /// <summary>
        /// The y offset
        /// </summary>
        private float _yOffset = 1;
        /// <summary>
        /// The x offset
        /// </summary>
        private float _xOffset = 1;
        /// <summary>
        /// The shadow color
        /// </summary>
        private Color _shadowColor = Color.Black;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitShadowLabel" /> class.
        /// </summary>
        public ZeroitShadowLabel()
        {
            
            InitializeComponent();

            
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = smoothing;
            e.Graphics.TextRenderingHint = textrendering;
            if (_drawGradient == true)
            {
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, this.Height), _startColor, _endColor, _angle, true);
                e.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
            }

            if (_drawShadow == true)
                e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(_shadowColor), _xOffset, _yOffset, StringFormat.GenericDefault);

            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), 0, 0, StringFormat.GenericDefault);
        }
        #endregion

        #region Public Properties


        #region Smoothing Mode

        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.HighQuality;

        /// <summary>
        /// Gets or sets the smoothing mode.
        /// </summary>
        /// <value>The smoothing mode.</value>
        public SmoothingMode Smoothing
        {
            get { return smoothing; }
            set
            {
                smoothing = value;
                Invalidate();
            }
        }

        #endregion



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
        /// <value>The text rendering mode.</value>
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
        /// Gets or sets a value indicating whether to draw gradient background.
        /// </summary>
        /// <value><c>true</c> if draw gradient; otherwise, <c>false</c>.</value>
        [Category("Gradient"),
        Description("Set to true to draw the gradient background"),
        DefaultValue(true)]
        public bool DrawGradient
        {
            get { return this._drawGradient; }
            set { this._drawGradient = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the start color of the gradient.
        /// </summary>
        /// <value>The start color.</value>
        [Category("Gradient"),
        Description("The start color of the gradient"),
        DefaultValue(typeof(Color), "Color.White")]
        public Color StartColor
        {
            get { return this._startColor; }
            set { this._startColor = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the end color of the gradient.
        /// </summary>
        /// <value>The end color.</value>
        [Category("Gradient"),
        Description("The end color of the gradient"),
        DefaultValue(typeof(Color), "Color.LightSkyBlue")]
        public Color EndColor
        {
            get { return this._endColor; }
            set { this._endColor = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the angle of the gradient.
        /// </summary>
        /// <value>The angle.</value>
        [Category("Gradient"),
        Description("The angle of the gradient"),
        DefaultValue(0)]
        public float Angle
        {
            get { return this._angle; }
            set { this._angle = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to draw the drop shadow.
        /// </summary>
        /// <value><c>true</c> if draw shadow; otherwise, <c>false</c>.</value>
        [Category("Drop Shadow"),
        Description("Set to true to draw the Drop Shadow"),
        DefaultValue(true)]
        public bool DrawShadow
        {
            get { return this._drawShadow; }
            set { this._drawShadow = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the x offset used to draw the shadow.
        /// </summary>
        /// <value>The x offset.</value>
        [Category("Drop Shadow"),
        Description("The X Offset used to draw the shadow"),
        DefaultValue(1)]
        public float XOffset
        {
            get { return this._xOffset; }
            set { this._xOffset = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the y offset used to draw the shadow.
        /// </summary>
        /// <value>The y offset.</value>
        [Category("Drop Shadow"),
        Description("The Y Offset used to draw the shadow"),
        DefaultValue(1)]
        public float YOffset
        {
            get { return this._yOffset; }
            set { this._yOffset = value; this.Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the shadow.
        /// </summary>
        /// <value>The color of the shadow.</value>
        [Category("Drop Shadow"),
        Description("The color used to draw the shadow"),
        DefaultValue(typeof(System.Drawing.Color), "Color.Black")]
        public Color ShadowColor
        {
            get { return this._shadowColor; }
            set { this._shadowColor = value; this.Invalidate(); }
        }

        #endregion

        #region Component Designer generated code


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ForeColor = Color.LightSkyBlue;
        }


        /// <summary>
        /// Required designer variable.
        /// </summary>

        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitShadowLabel"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ZeroitShadowLabel(System.ComponentModel.IContainer container)
        {
            ///
            /// Required for Windows.Forms Class Composition Designer support
            ///
            container.Add(this);
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion
    }

    #endregion

}