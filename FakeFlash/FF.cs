// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="FF.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Text;



namespace Zeroit.Framework.Labels.Headers
{


    /// <summary>
    /// A class collection for rendering flashes.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public class ZeroitFlashLabel : System.Windows.Forms.Control 
	{
        /// <summary>
        /// The components
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// The start color
        /// </summary>
        private Color startColor = Color.AliceBlue;
        /// <summary>
        /// The end color
        /// </summary>
        private Color endColor = Color.CornflowerBlue;
        /// <summary>
        /// The timer1
        /// </summary>
        private System.Windows.Forms.Timer timer1;
        /// <summary>
        /// The bars
        /// </summary>
        private int bars = 4;
        /// <summary>
        /// The moving
        /// </summary>
        private bool moving = false;
        /// <summary>
        /// The intxstr
        /// </summary>
        private int intxstr = 20;
        /// <summary>
        /// The intystr
        /// </summary>
        private int intystr = 10;
        /// <summary>
        /// The XSTR
        /// </summary>
        private int xstr = 20;
        /// <summary>
        /// The ystr
        /// </summary>
        private int ystr = 10;
        /// <summary>
        /// The direction
        /// </summary>
        private int direction = 0;
        /// <summary>
        /// The mdir
        /// </summary>
        private bool mdir = false;


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitFlashLabel" /> class.
        /// </summary>
        public ZeroitFlashLabel() 
		{
			//
			// Required for Windows Form designer support.
			//
			InitializeComponent();

			//
			// Add any constructor code after the InitializeComponent call.
			//
			SetStyle(ControlStyles.Opaque, true);
			SetStyle(ControlStyles.ResizeRedraw, true);
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint,	true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			UpdateStyles();
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
        /// <summary>
        /// Required method for designer support. Do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        void InitializeComponent () 
		{
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			// 
			// timer1
			// 
			this.timer1.Enabled = false;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			this.timer1.Interval = 100;
			// 
			// Title
			// 
			this.BackColor = System.Drawing.Color.White;
			this.ForeColor = System.Drawing.Color.Black;
			this.Font = new Font("Verdana",24);
			this.Size = new System.Drawing.Size(120, 24);
			this.Text = "FFTitle";
		}


        #region Smoothing Mode

        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.HighQuality;

        /// <summary>
        /// Gets or sets the smooth rendering mode.
        /// </summary>
        /// <value>The smoothing.</value>
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
        /// Gets or sets the speed of the animation.
        /// </summary>
        /// <value>The speed.</value>
        [Category("Look"), DefaultValue(100)]
		public int Speed
		{
			get 
			{
				return this.timer1.Interval;
			}
			set 
			{
				this.timer1.Interval = value;
                Invalidate();
            }
		}
        /// <summary>
        /// Gets or sets the bars.
        /// </summary>
        /// <value>The bars.</value>
        [Category("Look"), DefaultValue(4)]
		public int Bars 
		{
			get 
			{
				return bars;
			}
			set 
			{
				bars = value;
                Invalidate();
            }
		}
        /// <summary>
        /// Gets or sets the start color.
        /// </summary>
        /// <value>The start color.</value>
        [Category("Look")	]
		public Color StartColor 
		{
			get 
			{
				return startColor;
			}
			set 
			{
				startColor = value;
                Invalidate();
            }
		}
        /// <summary>
        /// Gets or sets the end color.
        /// </summary>
        /// <value>The end color.</value>
        [Category("Look")	]
		public Color EndColor 
		{
			get 
			{
				return endColor;
			}
			set 
			{
				endColor = value;
                Invalidate();
            }
		}
        /// <summary>
        /// Gets or sets a value indicating whether this control's animation is enabled.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        [Category("Look")	]
		public bool Active 
		{
			get 
			{
				return this.timer1.Enabled;
			}
			set 
			{
				this.timer1.Enabled = value;
                Invalidate();
            }
		}
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>The direction.</value>
        [Category("Look")	]
		public int Direction 
		{
			get 
			{
				return this.direction;
			}
			set 
			{
				this.direction = value;
                Invalidate();
            }
		}

        /// <summary>
        /// Marquees the on.
        /// </summary>
        public void MarqueeOn()
		{
			this.moving = true;
			this.intxstr = 20;
			this.intystr = 10;
		}
        /// <summary>
        /// Marquees the off.
        /// </summary>
        public void MarqueeOff()
		{
			this.moving = false;
			this.xstr = this.intxstr;
			this.ystr = this.intystr;
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
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

			base.OnPaint(e);

            e.Graphics.TextRenderingHint = textrendering;
            e.Graphics.SmoothingMode = smoothing;

            Rectangle sqb = new System.Drawing.Rectangle(0,0,this.Width,this.Height);
			LinearGradientBrush lgbb = new System.Drawing.Drawing2D.LinearGradientBrush(sqb, this.startColor, this.endColor, 0, true);
			lgbb.GammaCorrection = true;
			Rectangle sqs = new System.Drawing.Rectangle(0,0,10,this.Height);
			LinearGradientBrush lgbs = new System.Drawing.Drawing2D.LinearGradientBrush(sqb, this.BackColor, Color.Transparent, 90, true);
			lgbs.GammaCorrection = true;
			e.Graphics.FillRectangle(lgbb, 0, 0, this.Width, this.Height);

			Random rnd = new Random(unchecked((int)DateTime.Now.Ticks)); 
			for (int i=0; i < this.bars; i++)
			{
				int rnm1 = rnd.Next(this.Width-20);
				int rnm1a = rnd.Next(10)+10;
				e.Graphics.FillRectangle(lgbs, rnm1, 0, rnm1a, this.Height);
			}
			//e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			if(this.moving) 
			{
				switch(this.direction)
				{
					case 0:
						MoveVertical();
						break;
					case 1:
						MoveHorizontal();
						break;
					default:
						MoveHorizontal();
						break;
				}
			}
			e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(Color.Gray), (this.xstr+1), (this.ystr+1));
			e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), this.xstr, this.ystr);
		}

        /// <summary>
        /// Moves the vertical.
        /// </summary>
        private void MoveVertical()
		{
			if(this.ystr >= 0)
			{
				if (this.ystr >= this.Height-50 || this.mdir)
				{
					this.ystr = this.ystr-2;
				} 
				else 
				{
					this.ystr = this.ystr+2;
					if (this.ystr >= this.Height-50) { this.mdir = true; }
				}
			}
			else 
			{
				this.ystr = this.ystr+2;
			}
		}
        /// <summary>
        /// Moves the horizontal.
        /// </summary>
        private void MoveHorizontal()
		{
			if(this.xstr >= 0)
			{
				if (this.xstr >= this.Width-70 || this.mdir)
				{
					this.xstr = this.xstr-2;
				} 
				else 
				{
					this.xstr = this.xstr+2;
					if (this.xstr >= this.Width-70) { this.mdir = true; }
				}
			}
			else 
			{
				this.xstr = this.xstr-2;
				this.mdir = false;
			}
		}
        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.Update();
			this.Invalidate();
		}

	}
}
