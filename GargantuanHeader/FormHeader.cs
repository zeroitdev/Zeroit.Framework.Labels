// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="FormHeader.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Zeroit.Framework.Labels.Headers
{
    /*******************************************************************************************************************************

	*******************************************************************************************************************************/
    /// <summary>
    /// A class collection for rendering Form Header.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public class ZeroitFormHeader : System.Windows.Forms.Control
	{
        /***************************************************************
			static properties
		***************************************************************/
        /// <summary>
        /// The default message font style
        /// </summary>
        public static readonly System.Drawing.FontStyle DefaultMessageFontStyle = System.Drawing.FontStyle.Regular;
        /// <summary>
        /// The default title font style
        /// </summary>
        public static readonly System.Drawing.FontStyle DefaultTitleFontStyle = System.Drawing.FontStyle.Bold;
        /// <summary>
        /// The default boundry size
        /// </summary>
        public static readonly int DefaultBoundrySize = 15;



        /// <summary>
        /// The string title
        /// </summary>
        private String _strTitle = String.Empty;
        /// <summary>
        /// The string message
        /// </summary>
        private String _strMessage = String.Empty;
        /// <summary>
        /// The message font
        /// </summary>
        private Font _messageFont;
        /// <summary>
        /// The title font
        /// </summary>
        private Font _titleFont;
        /// <summary>
        /// The icon
        /// </summary>
        private Icon _icon = null;
        /// <summary>
        /// The image
        /// </summary>
        private Image _image = null	;
        /// <summary>
        /// The i boundry size
        /// </summary>
        private int _iBoundrySize = DefaultBoundrySize;
        /// <summary>
        /// The title font style
        /// </summary>
        private System.Drawing.FontStyle _titleFontStyle = DefaultTitleFontStyle;
        /// <summary>
        /// The message font style
        /// </summary>
        private System.Drawing.FontStyle _messageFontStyle = DefaultMessageFontStyle;
        /// <summary>
        /// The draw text workaround append string
        /// </summary>
        private string _drawTextWorkaroundAppendString = new string( ' ', 10000) + ".";
        /// <summary>
        /// The text start point
        /// </summary>
        private Point _textStartPoint = new Point( DefaultBoundrySize, DefaultBoundrySize );

	    /// <summary>
	    /// The border style
	    /// </summary>
	    private Border3DStyle borderStyle = Border3DStyle.Default;

	    /// <summary>
	    /// The border color
	    /// </summary>
	    private Color borderColor = Color.Black;

	    /// <summary>
	    /// The shifts
	    /// </summary>
	    Shifts shifts = new Shifts();


        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitFormHeader" /> class.
        /// </summary>
        public ZeroitFormHeader()
		{
			this.Size = new Size(10,70); //header height of 70 does not look bad
			this.Dock = DockStyle.Top;
			this.CreateTitleFont();
			this.CreateMessageFont();


            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            
        }


        /***************************************************************
			public properties
		***************************************************************/
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public String Message
		{
			get	{	return _strMessage;	}
			set	
			{	
				_strMessage = value;
				Invalidate();
			}
		}
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public String Title
		{
			get	{	return _strTitle;	}
			set	
			{	
				_strTitle = value;
				Invalidate();
			}
		}

        /// <summary>
        /// Gets or sets the title font.
        /// </summary>
        /// <value>The title font.</value>
        public Font TitleFont
	    {
            get { return _titleFont; }
	        set
	        {
                _titleFont = value;
	            Invalidate();

	        }
	    }
        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public Icon Icon
		{
			get	{	return this._icon;	}
			set	
			{
				this._icon = value;
				Invalidate();			
			}
		}
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
		{
			get	{	return this._image;	}
			set	
			{
				this._image = value;
				Invalidate();
			}
		}
        /// <summary>
        /// Gets or sets the title font style.
        /// </summary>
        /// <value>The title font style.</value>
        public System.Drawing.FontStyle TitleFontStyle
		{
			get	{	return this._titleFontStyle;	}
			set	
			{
				this._titleFontStyle = value;
				this.CreateTitleFont();
				Invalidate();			
			}
		}

        /// <summary>
        /// Gets or sets the message font style.
        /// </summary>
        /// <value>The message font style.</value>
        public System.Drawing.FontStyle MessageFontStyle
		{
			get	{	return this._messageFontStyle;	}
			set	
			{
				this._messageFontStyle = value;
				this.CreateMessageFont();
				Invalidate();			
			}
		}
        /// <summary>
        /// Gets or sets the size of the boundry.
        /// </summary>
        /// <value>The size of the boundry.</value>
        public int BoundrySize
		{
			get	{	return _iBoundrySize;	}
			set	
			{
				_iBoundrySize = value;
				Invalidate();
			}
		}
        /// <summary>
        /// Gets or sets the text start position.
        /// </summary>
        /// <value>The text start position.</value>
        public Point TextStartPosition
		{
			get	{	return _textStartPoint;	}
			set	
			{
				_textStartPoint = value;
				Invalidate();
			}
		}




        /***************************************************************
			newly implemented/overridden public properties
		***************************************************************/
        /// <summary>
        /// Gets or sets the background image displayed in the control.
        /// </summary>
        /// <value>The background image.</value>
        new public Image BackgroundImage
		{
		    get
		    {
		        /*return null;*/
		        return base.BackgroundImage;
		    }
		    set
		    {
                base.BackgroundImage = value;
		        Invalidate();
		    }
		}
        /// <summary>
        /// Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent.
        /// </summary>
        /// <value>The anchor.</value>
        new public AnchorStyles Anchor
		{
		    get
		    {
		        //return  System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
		        return base.Anchor;
		    }
		    set
		    {
                base.Anchor = value;
		        Invalidate();
		    }
		}
        //only allow black foregound and white background
        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        new public Color ForeColor
		{
			get {	return  base.ForeColor;	}
		    set
		    {
                base.ForeColor = value;
		        Invalidate();
		    }
		}
        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>The color of the back.</value>
        new public Color BackColor
		{
			get {	return  base.BackColor;	}
		    set
		    {
		        base.BackColor = value;
		        Invalidate();
		    }
		}

        /// <summary>
        /// Gets or sets the color of the message.
        /// </summary>
        /// <value>The color of the message.</value>
        public Color MessageColor
	    {
	        get { return messageColor; }
	        set { messageColor = value;
	            Invalidate();
	        }
	    }

        /// <summary>
        /// Gets or sets the message font.
        /// </summary>
        /// <value>The message font.</value>
        public Font MessageFont
	    {
	        get { return _messageFont; }
	        set { _messageFont = value;
	            Invalidate();
	        }
	    }

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
            get { return borderStyle; }
            set
            {
                borderStyle = value;
                Invalidate();
            }
        }

        
        /// <summary>
        /// Gets or sets the shift.
        /// </summary>
        /// <value>The shift.</value>
        [TypeConverter(typeof(ExpandableObjectConverter))]
	    public Shifts Shift
	    {
	        get { return shifts; }
	        set
	        {
                shifts = value;
	            Invalidate();
	        }
	    }

        /***************************************************************
			drawing stuff
		***************************************************************/
        /// <summary>
        /// Creates the title font.
        /// </summary>
        protected void CreateTitleFont()
		{
			this._titleFont = new Font( this.Font.FontFamily, this.Font.Size,  this._titleFontStyle);			
		}
        /// <summary>
        /// Creates the message font.
        /// </summary>
        protected void CreateMessageFont()
		{			
			this._messageFont = new Font( this.Font.FontFamily, this.Font.Size,  this._messageFontStyle);
		}

        /// <summary>
        /// Draw3ds the line.
        /// </summary>
        /// <param name="g">The g.</param>
        protected void Draw3dLine( System.Drawing.Graphics g )
        {
            
		    switch (BorderStyle)
		    {
		        case Border3DStyle.Default:
		            g.DrawRectangle(new Pen(BorderColor), new Rectangle(Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y)));
                    break;
		        case Border3DStyle.Adjust:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.Adjust);

                    break;
		        case Border3DStyle.Bump:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.Bump);

                    break;
		        case Border3DStyle.Etched:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.Etched);

                    break;
		        case Border3DStyle.Flat:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.Flat);

                    break;
		        case Border3DStyle.Raised:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.Raised);

                    break;
		        case Border3DStyle.RaisedInner:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.RaisedInner);

                    break;
		        case Border3DStyle.RaisedOuter:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.RaisedOuter);

                    break;
		        case Border3DStyle.Sunken:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.Sunken);

                    break;
		        case Border3DStyle.SunkenInner:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.SunkenInner);

                    break;
		        case Border3DStyle.SunkenOuter:
		            ControlPaint.DrawBorder3D(g, Shift.X, Shift.Y, Width - (2 * Shift.X), Height - (2 * Shift.Y), System.Windows.Forms.Border3DStyle.SunkenOuter);

                    break;
		        case Border3DStyle.None:
		            
                    break;
		        default:
		            throw new ArgumentOutOfRangeException();
		    }
			
		}

        /// <summary>
        /// Draws the title.
        /// </summary>
        /// <param name="g">The g.</param>
        protected void DrawTitle( System.Drawing.Graphics g )
		{
			// Normally the next line should work fine
			// but the spacing of the characters at the end of the string is smaller than at the beginning
			// therefore we add _drawTextWorkaroundAppendString to the string to be drawn
			// this works fine
			//
			// i reported this behaviour to microsoft. they confirmed this is a bug in GDI+.
			//
			//			g.DrawString( this._strTitle, this._titleFont, new SolidBrush(Color.Black), BoundrySize, BoundrySize); //BoundrySize is used as the x & y coords
			g.DrawString( this._strTitle + _drawTextWorkaroundAppendString, this._titleFont, new SolidBrush(ForeColor), this.TextStartPosition );
		}

        /// <summary>
        /// The message color
        /// </summary>
        private Color messageColor = Color.Black;

        /// <summary>
        /// Draws the message.
        /// </summary>
        /// <param name="g">The g.</param>
        protected void DrawMessage( System.Drawing.Graphics g )
		{
			//calculate the new startpoint
			int iNewPosY = this.TextStartPosition.Y + this.Font.Height * 3 / 2;
			int iNewPosX = this.TextStartPosition.X + this.Font.Height * 3 / 2;
			int iTextBoxWidth = this.Width -iNewPosX;
			int iTextBoxHeight = this.Height-iNewPosY;

			if (this._icon != null)
				iTextBoxWidth -=  (BoundrySize + _icon.Width); // subtract the width of the icon and the boundry size again
			else if (this._image != null)
				iTextBoxWidth -=  (BoundrySize + _image.Width); // subtract the width of the icon and the boundry size again

			Rectangle rect = new Rectangle(iNewPosX, iNewPosY, iTextBoxWidth, iTextBoxHeight );
			g.DrawString( this._strMessage , this._messageFont, new SolidBrush(MessageColor), rect);
		}

        /// <summary>
        /// Draws the image.
        /// </summary>
        /// <param name="g">The g.</param>
        protected void DrawImage(System.Drawing.Graphics g )
		{
			if (this._image == null)
				return;
			g.DrawImage( this._image, this.Width-this._image.Width-BoundrySize , (this.Height-this._image.Height)/2);
		}
        /// <summary>
        /// Draws the icon.
        /// </summary>
        /// <param name="g">The g.</param>
        protected void DrawIcon( System.Drawing.Graphics g )
		{
			if (this._icon == null)
				return;
			g.DrawIcon( this._icon, this.Width-this._icon.Width-BoundrySize , (this.Height-this._icon.Height)/2);
		}




        #region Transparency


        #region Include in Paint

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
            else
            {
                g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, this.Width, this.Height);

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
        /// Draws the background.
        /// </summary>
        /// <param name="g">The g.</param>
        protected virtual void DrawBackground( System.Drawing.Graphics g )
        {
            TransInPaint(g);
			//g.FillRectangle( new SolidBrush( this.BackColor), 0,0, this.Width, this.Height );

        }


        /***************************************************************
			overridden methods
		***************************************************************/
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnFontChanged(EventArgs e)
		{
			this.CreateTitleFont();
			base.OnFontChanged (e);
		}


        //      /// <summary>
        //      /// Paints the background of the control.
        //      /// </summary>
        //      /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
        //      protected override void OnPaintBackground(PaintEventArgs pevent)
        //{
        //}

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
		{
			this.DrawBackground(e.Graphics);
			this.Draw3dLine( e.Graphics );
			this.DrawTitle(e.Graphics);
			this.DrawMessage(e.Graphics);
			if (this._icon != null)
				this.DrawIcon(e.Graphics);
			else if (this._image != null)
				this.DrawImage(e.Graphics);
		}


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
		{
			Invalidate();
			base.OnSizeChanged (e);
		}
	}

    [Serializable]
    public class Shifts
    {

        public int X { get; set; } = 2;
        public int Y { get; set; } = 2;
    }
    /*******************************************************************************************************************************
		ColorSlideFormHeader is an extended version of the FormHeader class
		It also provides the functionality of a color slide of the background image
	*******************************************************************************************************************************/
    /// <summary>
    /// A class collection for rendering Header.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.ZeroitFormHeader" />
    /// <seealso cref="ZeroitFormHeader" />
    /// <remarks>This is an extended version of the <c><see cref="ZeroitFormHeader" /></c> class.
    /// It also provides the functionality of a color slide of the background image.</remarks>
    public class ZeroitFormSlideHeader : ZeroitFormHeader
    {
        /// <summary>
        /// The default color1
        /// </summary>
        public static readonly Color DefaultColor1 = Color.White;
        /// <summary>
        /// The default color2
        /// </summary>
        public static readonly Color DefaultColor2 = Color.White;

        /// <summary>
        /// The color1
        /// </summary>
        private Color _color1 = DefaultColor1;
        /// <summary>
        /// The color2
        /// </summary>
        private Color _color2 = DefaultColor2;
        /// <summary>
        /// The image
        /// </summary>
        private Image _image = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitFormSlideHeader" /> class.
        /// </summary>
        public ZeroitFormSlideHeader()
		{
			CreateBackgroundPicture();
		}


        /// <summary>
        /// Creates the background picture.
        /// </summary>
        protected virtual void CreateBackgroundPicture()
		{
			try 
			{
				_image = new Bitmap( this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb );
			}
			catch
			{
				return;
			}

			Graphics gfx = Graphics.FromImage( _image );

			if (this._color1.Equals(this._color2) ) //check if we need to calc the color slide
			{
				gfx.FillRectangle( new SolidBrush(this._color1), 0,0, this.Width, this.Height);
			}
			else
			{
				for (int i=0 ; i<_image.Width; i++)
				{
					//
					// calculate the new color to use (linear color mix)
					//
					int colorR = ( (int)(this.Color2.R - this.Color1.R ) ) * i / _image.Width;
					int colorG = ( (int)(this.Color2.G - this.Color1.G ) ) * i / _image.Width;
					int colorB = ( (int)(this.Color2.B - this.Color1.B ) ) * i / _image.Width;
					Color color = Color.FromArgb( this.Color1.R+colorR, this.Color1.G+colorG, this.Color1.B+colorB );

					gfx.DrawLine( new Pen( new SolidBrush( color )), i, 0, i, this.Height );
				}
			}
		}


        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color1.</value>
        public Color Color1
		{
			get{	return this._color1;	}
			set{	this._color1 = value;
					CreateBackgroundPicture();
					Invalidate();
			}
		}


        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color2.</value>
        public Color Color2
		{
			get{	return this._color2;	}
			set{	this._color2 = value;
					CreateBackgroundPicture();
					Invalidate();
			}
		}


        /// <summary>
        /// Draws the background.
        /// </summary>
        /// <param name="g">The g.</param>
        protected override void DrawBackground( Graphics g)
		{
			g.DrawImage( this._image, 0,0 );
		}


        /// <summary>
        /// Handles the <see cref="E:SizeChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
		{
			CreateBackgroundPicture();
			base.OnSizeChanged (e);
			Invalidate();
		}
	}


    /// <summary>
    /// A class collection for rendering image headers.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.ZeroitFormHeader" />
    /// <seealso cref="ZeroitFormHeader" />
    /// <remarks>This is an extended version of the <see cref="ZeroitFormHeader" /> class.</remarks>
    public class ZeroitImageFormHeader : ZeroitFormHeader
    {

        /// <summary>
        /// The background image
        /// </summary>
        private Image _backgroundImage;


        /// <summary>
        /// Gets or sets the background image displayed in the control.
        /// </summary>
        /// <value>The background image.</value>
        new public Image BackgroundImage
		{
			get	{	return this._backgroundImage;	}
			set	
			{
				this._backgroundImage = value;
				Invalidate();			
			}
		}

        /// <summary>
        /// Draws the background image.
        /// </summary>
        /// <param name="g">The g.</param>
        protected void DrawBackgroundImage( System.Drawing.Graphics g)
		{
			if (this._backgroundImage == null)
				return;
			g.DrawImage( this._backgroundImage, 0,0);
		}
        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
		{
			this.DrawBackground(e.Graphics);

			this.DrawBackgroundImage(e.Graphics);
			this.Draw3dLine( e.Graphics );
			this.DrawTitle(e.Graphics);
			this.DrawMessage(e.Graphics);
			if (this.Icon!= null)
				this.DrawIcon(e.Graphics);
			else if (this.Image!= null)
				this.DrawImage(e.Graphics);
		}
	}
}
