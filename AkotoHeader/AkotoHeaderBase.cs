// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="AkotoHeaderBase.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Design;



namespace Zeroit.Framework.Labels.Headers
{
    /// <summary>
    /// Enum TextAlignment
    /// </summary>
    public enum TextAlignment
	{
        /// <summary>
        /// The near
        /// </summary>
        near,
        /// <summary>
        /// The center
        /// </summary>
        center
    };

    /// <summary>
    /// Enum BorderStyles
    /// </summary>
    public enum BorderStyles
	{
        /// <summary>
        /// The none
        /// </summary>
        none,
        /// <summary>
        /// The single
        /// </summary>
        single,
        /// <summary>
        /// The three dee
        /// </summary>
        threeDee
    };

    /// <summary>
    /// Summary description for XPExtendedHeaderBase.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [ToolboxItem(false)]
	public class XPExtendedHeaderBase : Control
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="XPExtendedHeaderBase"/> class.
        /// </summary>
        public XPExtendedHeaderBase()
		{
		}

        #region Properties, Variables and such

        /// <summary>
        /// Detremines if the mouse is currently in the header area.
        /// Used in the OnPaint handler.
        /// </summary>
        protected bool _isSelected = false;

        /// <summary>
        /// The br text
        /// </summary>
        protected Brush brText = new SolidBrush(Color.Black);
        /// <summary>
        /// The br text dim
        /// </summary>
        protected Brush brTextDim = new SolidBrush(Color.Gray);

        //Ear adornment for the left ear
        /// <summary>
        /// The left ear
        /// </summary>
        protected EarAdornment _leftEar = new EarAdornment(EarTypes.regular, 
			new Rectangle(0, 0, 0, 0));

        //Ear adornment for the right ear
        /// <summary>
        /// The right ear
        /// </summary>
        protected EarAdornment _rightEar = new EarAdornment(EarTypes.regular, 
			new Rectangle(0, 0, 0, 0));

        /// <summary>
        /// The adornment type to be used for the left ear.
        /// </summary>
        private EarTypes _leftEarType = EarTypes.regular;
        /// <summary>
        /// Gets or sets the type of the left ear.
        /// </summary>
        /// <value>The type of the left ear.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Ear type for top left corner of header"),
		Editor(typeof(Zeroit.Framework.Labels.Headers.EarAdornmentTypeEditor), typeof(UITypeEditor))
		]
		public EarTypes LeftEarType
		{
			get { return _leftEarType; }
			set 
			{
				_leftEarType = value;
				_leftEar.EarType = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// The adornment type to be used for the right ear.
        /// </summary>
        /// &gt;
        private EarTypes _rightEarType = EarTypes.regular;
        /// <summary>
        /// Gets or sets the type of the right ear.
        /// </summary>
        /// <value>The type of the right ear.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Ear type for top right corner of header"),
		Editor(typeof(Zeroit.Framework.Labels.Headers.EarAdornmentTypeEditor), typeof(UITypeEditor))
		]
		public EarTypes RightEarType
		{
			get { return _rightEarType; }
			set 
			{
				_rightEarType = value;
				_rightEar.EarType = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// The radius of the left ear adornment
        /// </summary>
        protected int _arcRadiusLeft = 20;
        /// <summary>
        /// Gets or sets the arc radius left.
        /// </summary>
        /// <value>The arc radius left.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Radius of top corner arcs (in pixels)"),
		DefaultValue(20)
		]
		public int ArcRadiusLeft
		{
			get	
			{
				if (_arcRadiusLeft <= 0)
					return 20;
				else
					return _arcRadiusLeft; 
			}
			set
			{
				_arcRadiusLeft = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// The radius of the left ear adornment
        /// </summary>
        protected int _arcRadiusRight = 20;
        /// <summary>
        /// Gets or sets the arc radius right.
        /// </summary>
        /// <value>The arc radius right.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Radius of top corner arcs (in pixels)"),
		DefaultValue(20)
		]
		public int ArcRadiusRight
		{
			get	
			{
				if (_arcRadiusRight <= 0)
					return 20;
				else
					return _arcRadiusRight; }
			set
			{
				_arcRadiusRight = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// First gradient color to be used for the background
        /// </summary>
        protected Color _bckgndColor1 = Color.WhiteSmoke;
        /// <summary>
        /// Gets or sets the background gradient color1.
        /// </summary>
        /// <value>The background gradient color1.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Background Gradient Color1")
		]
		public Color BackgroundGradientColor1
		{
			get { return _bckgndColor1; }
			set 
			{
				_bckgndColor1 = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// Second gradient color to be used for the background
        /// </summary>
        protected Color _bckgndColor2 = Color.LightSteelBlue;
        /// <summary>
        /// Gets or sets the background gradient color2.
        /// </summary>
        /// <value>The background gradient color2.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Background Gradient Color2")
		]
		public Color BackgroundGradientColor2
		{
			get { return _bckgndColor2; }
			set 
			{
				_bckgndColor2 = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// Gradient angle.
        /// </summary>
        protected float _gradientAngle = 65;
        /// <summary>
        /// Gets or sets the background gradient angle.
        /// </summary>
        /// <value>The background gradient angle.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Background Gradient Angle")
		]
		public float BackgroundGradientAngle
		{
			get { return _gradientAngle; }
			set
			{
				_gradientAngle = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// This is an effect that causes the linear gradient colors
        /// to appear as though they're twisting.
        /// </summary>
        protected bool _twistColors = false;
        /// <summary>
        /// Gets or sets a value indicating whether [swirl colors].
        /// </summary>
        /// <value><c>true</c> if [swirl colors]; otherwise, <c>false</c>.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Swirl/Twist colors in Background Gradient")
		]
		public bool SwirlColors
		{
			get { return _twistColors; }
			set 
			{
				_twistColors = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// The image on the left side to display.  i.e. The main image
        /// </summary>
        protected Image _headerImage = null;
        /// <summary>
        /// Gets or sets the header image.
        /// </summary>
        /// <value>The header image.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Header Image"),
		DefaultValue(null)
		]
		public Image HeaderImage
		{
			get { return _headerImage; }
			set
			{
				_headerImage = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// Text to be displayed in control area
        /// </summary>
        protected string _headerText = "Add text";
        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        /// <value>The header text.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Text to display in Header")
		]
		public string HeaderText
		{
			get { return _headerText; }
			set
			{
				_headerText = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// Alignment of the text within the display area. (Horiz.)
        /// </summary>
        protected TextAlignment _textAlignment = TextAlignment.near;
        /// <summary>
        /// Gets or sets the header text alignment.
        /// </summary>
        /// <value>The header text alignment.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Text alignment, may be near or center"),
		DefaultValue(TextAlignment.near)
		]
		public TextAlignment HeaderTextAlignment
		{
			get { return _textAlignment; }
			set
			{
				_textAlignment = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// Font to be used by text when displaying
        /// </summary>
        protected Font _headerFont = new Font("Microsoft Sans Serif", 12);
        /// <summary>
        /// Gets or sets the header font.
        /// </summary>
        /// <value>The header font.</value>
        [
        Category("XPExtendedHeaderControl"),
		Description("Font to be used by text")
		]
		public Font HeaderFont
		{
			get { return _headerFont; }
			set 
			{
				_headerFont = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// Style of border to be drawn around control
        /// </summary>
        protected BorderStyles _borderStyle = BorderStyles.none;
        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        [
        Category("XPExtendedHeaderControl"),
		DefaultValue(BorderStyles.none)
		]
		public BorderStyles BorderStyle
		{
			get { return _borderStyle; }
			set
			{
				_borderStyle = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// Preserves the original coordinates of the control associated
        /// with this header.
        /// </summary>
        protected Rectangle _workingControlOrgCoords = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// Gets or sets the working control org coords.
        /// </summary>
        /// <value>The working control org coords.</value>
        [
        Browsable(false)
		]
		public Rectangle WorkingControlOrgCoords
		{
			get { return _workingControlOrgCoords; }
			set { _workingControlOrgCoords = value; }
		}

        /// <summary>
        /// The control to be associated with this header
        /// </summary>
        protected Control _workingControl = null;
        /// <summary>
        /// Gets or sets the working control.
        /// </summary>
        /// <value>The working control.</value>
        [
        Category("XPExtendedHeaderControl"),
		DefaultValue(null)
		]
		public Control WorkingControl
		{
			get { return _workingControl; }
			set 
			{ 
				_workingControl = value; 
				if (_workingControl != null)
					WorkingControlOrgCoords = _workingControl.Bounds;
			}
		}

        /// <summary>
        /// Provides an interface to the design mode to do the docking
        /// of the control.
        /// </summary>
        private bool _dockControl = false;
        /// <summary>
        /// Gets or sets a value indicating whether [dock working control].
        /// </summary>
        /// <value><c>true</c> if [dock working control]; otherwise, <c>false</c>.</value>
        /// <exception cref="Exception">(Header) Must set a control to associate the Header with! Set WorkingControl property</exception>
        [
        Category("XPExtendedHeaderControl"),
		DefaultValue(false)
		]
		public bool DockWorkingControl
		{
			get { return _dockControl; }
			set
			{
				_dockControl = value;

				if (_workingControl == null)
				{
					_dockControl = false;
					if (DesignMode)
						throw new Exception("(Header) Must set a control to associate the Header with! Set WorkingControl property");
				}
				else
					if (_dockControl)
					DockControl();
			}
		}

        #endregion

        #region Virtual methods

        /// <summary>
        /// Docks the header to the associated control
        /// </summary>
        protected virtual void DockControl()
		{
		}

        /// <summary>
        /// Paints the ExpandCollapse image
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected virtual void DrawCollapseImage(PaintEventArgs e)
		{
		}

        #endregion

        /// <summary>
        /// Draws the Main image to the left of the control
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected void DrawImage(PaintEventArgs e)
		{
			if (_headerImage == null)
				return;

			Rectangle rct = new Rectangle(IMAGE_OFFSET_X,
				(this.Height - _headerImage.Height) / 2,
				_headerImage.Width,
				_headerImage.Height);

			e.Graphics.DrawImage(
				_headerImage, 
				rct); 
		}

        /// <summary>
        /// Draws border
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        /// <param name="gp">GraphicsPath</param>
        protected void DrawBorderStyle(PaintEventArgs e, GraphicsPath gp)
		{
			LinearGradientBrush lgb1 = new LinearGradientBrush(
				gp.GetBounds(), 
				Color.White, 
				Color.DimGray, 
				_gradientAngle, 
				true);
			Pen p = new Pen(lgb1, 5);
			Pen p1 = new Pen(Color.Black, 2);
			
			switch (_borderStyle)
			{
				case BorderStyles.single:
					e.Graphics.DrawPath(p1, gp);
					break;
				case BorderStyles.threeDee:
					e.Graphics.DrawPath(p, gp);
					break;
			}

			lgb1.Dispose();
			p.Dispose();
			p1.Dispose();
		}

        /// <summary>
        /// The image offset x
        /// </summary>
        protected const int IMAGE_OFFSET_X = 25;
        /// <summary>
        /// The text offset x
        /// </summary>
        protected const int TEXT_OFFSET_X = IMAGE_OFFSET_X + 10;
        /// <summary>
        /// Draw the text in the control area
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected void DrawText(PaintEventArgs e)
		{
			Rectangle textRct = new Rectangle(0, 0, 0, 0);

			//This is a workaround to get MeasureString to work properly
			e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
			SizeF sz = e.Graphics.MeasureString(this._headerText, _headerFont);
			Size tmp = sz.ToSize();

			//Calculate rectangle bounds for the image
			textRct = new Rectangle(TEXT_OFFSET_X, 
				(this.Height -  tmp.Height) / 2,
				this.Width,
				this.Height);

			//Move the text over to accomidate image
			if (_headerImage != null)
				textRct.X += _headerImage.Width;

			//Align text according to users wishes
			if (_textAlignment == TextAlignment.center)
				textRct.X = (this.Width - tmp.Width) / 2;

			//If the mouse is passing over the header it is selected and will be dimmed
			//	otherwise nada.
			if (_isSelected)
				e.Graphics.DrawString(_headerText,
					_headerFont,
					brTextDim,
					textRct);
			else
				e.Graphics.DrawString(_headerText,
					_headerFont,
					brText,
					textRct);
		}
	}
}
