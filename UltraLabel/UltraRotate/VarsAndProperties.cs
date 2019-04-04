// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 12-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="VarsAndProperties.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.Labels
{
    /// <summary>
    /// A class collection for rendering a label with nice features.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    //[Designer(typeof(UltraRotateDesigner))]
    public partial class ZeroitUltraRotate : Control
    {
        #region Enums
        //Declare the Enums used for some of the special selections we want to use for some of the properties

        /// <summary>
        /// Enum of BorderTypes used for the Labels BorderStyle.
        /// </summary>
        public enum BorderType : int
        {
            /// <summary>
            /// Set the border type to none.
            /// </summary>
            None = 0,
            /// <summary>
            /// Set the border type to squared.
            /// </summary>
            Squared = 1,
            /// <summary>
            /// Set the border type to rounded.
            /// </summary>
            Rounded = 2
        }

        /// <summary>
        /// Enum of layout styles used for the Labels TextPaternImage.
        /// </summary>
        public enum PatternLayout : int
        {
            /// <summary>
            /// Set the pattern layout to normal.
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Set the pattern layout to center.
            /// </summary>
            Center = 1,
            /// <summary>
            /// Set the pattern layout to stretch.
            /// </summary>
            Stretch = 2,
            /// <summary>
            /// Set the pattern layout to title.
            /// </summary>
            Tile = 3
        }

        /// <summary>
        /// Enum of areas used for the Labels ShadowPosition.
        /// </summary>
        public enum ShadowArea : int
        {
            /// <summary>
            /// Set the Shadow area to Top Left.
            /// </summary>
            TopLeft = 0,
            /// <summary>
            /// Set the Shadow area to Top Right.
            /// </summary>
            TopRight = 1,
            /// <summary>
            /// Set the Shadow area to Bottom Left.
            /// </summary>
            BottomLeft = 2,
            /// <summary>
            /// Set the Shadow area to Bottom Right.
            /// </summary>
            BottomRight = 3
        }

        /// <summary>
        /// Enum of drawing types used for the Labels ShadowStyle.
        /// </summary>
        public enum ShadowDrawingType : int
        {
            /// <summary>
            /// Set the shadow drawing type to <c>DrawShadow</c>.
            /// </summary>
            DrawShadow = 0,
            /// <summary>
            /// Set the shadow drawing type to <c>FillShadow</c>.
            /// </summary>
            FillShadow = 1
        }

        /// <summary>
        /// Enum GraphicsType
        /// </summary>
        public enum GraphicsType
        {
            /// <summary>
            /// The graphics
            /// </summary>
            Graphics,
            /// <summary>
            /// The graphics path
            /// </summary>
            GraphicsPath
        }

        
        #endregion


        #region Private Variables

        #region Rotation Code

        /// <summary>
        /// The rotation angle
        /// </summary>
        private double rotationAngle = 0;
        
        /// <summary>
        /// The Text orientation
        /// </summary>
        private Orientation textOrientation = Orientation.Rotate;
        /// <summary>
        /// The Text direction
        /// </summary>
        private Direction textDirection = Direction.Clockwise;

        #endregion

        //Add all of the Property Backing Feilds for the Properties added to the LabelEx class
        /// <summary>
        /// The out line pen
        /// </summary>
        private Pen _OutLinePen = new Pen(Color.Black);
        /// <summary>
        /// The border pen
        /// </summary>
        private Pen _BorderPen = new Pen(Color.Black);
        /// <summary>
        /// The center brush
        /// </summary>
        private SolidBrush _CenterBrush = new SolidBrush(Color.White);
        /// <summary>
        /// The background brush
        /// </summary>
        private SolidBrush _BackgroundBrush = new SolidBrush(Color.Transparent);
        /// <summary>
        /// The border style
        /// </summary>
        private BorderType _BorderStyle = BorderType.None;
        /// <summary>
        /// The image
        /// </summary>
        private Bitmap _Image = null;
        /// <summary>
        /// The image align
        /// </summary>
        private ContentAlignment _ImageAlign = ContentAlignment.MiddleCenter;
        /// <summary>
        /// The Text align
        /// </summary>
        private ContentAlignment _TextAlign = ContentAlignment.MiddleCenter;
        /// <summary>
        /// The Text pattern image
        /// </summary>
        private Bitmap _TextPatternImage = null;
        /// <summary>
        /// The Text pattern image layout
        /// </summary>
        private PatternLayout _TextPatternImageLayout = PatternLayout.Stretch;
        /// <summary>
        /// The shadow brush
        /// </summary>
        private SolidBrush _ShadowBrush = new SolidBrush(Color.FromArgb(128, Color.Black));
        /// <summary>
        /// The shadow pen
        /// </summary>
        private Pen _ShadowPen = new Pen(Color.FromArgb(128, Color.Black));
        /// <summary>
        /// The shadow color
        /// </summary>
        private Color _ShadowColor = Color.Black;
        /// <summary>
        /// The show Text shadow
        /// </summary>
        private bool _ShowTextShadow = false;
        /// <summary>
        /// The shadow position
        /// </summary>
        private ShadowArea _ShadowPosition = ShadowArea.BottomRight;
        /// <summary>
        /// The shadow depth
        /// </summary>
        private int _ShadowDepth = 2;
        /// <summary>
        /// The shadow transparency
        /// </summary>
        private int _ShadowTransparency = 128;
        /// <summary>
        /// The shadow style
        /// </summary>
        private ShadowDrawingType _ShadowStyle = ShadowDrawingType.FillShadow;
        /// <summary>
        /// The fore color transparency
        /// </summary>
        private int _ForeColorTransparency = 255;
        /// <summary>
        /// The outline thickness
        /// </summary>
        private int _OutlineThickness = 1;

        /// <summary>
        /// The graphics type
        /// </summary>
        private GraphicsType graphicsType = GraphicsType.GraphicsPath;

        private int pathSize;

        #endregion
        
        #region Properties

        #region Rotation

        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        /// <value>The rotation angle.</value>
        [Description("Rotation Angle"), Category("Appearance")]
        public double RotationAngle
        {
            get
            {
                return rotationAngle;
            }
            set
            {

                rotationAngle = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text orientation.
        /// </summary>
        /// <value>The text orientation.</value>
        [Description("Kind of Text Orientation"), Category("Appearance")]
        public Orientation TextOrientation
        {
            get
            {
                return textOrientation;
            }
            set
            {
                if (value == Orientation.Circle || value == Orientation.Arc)
                {
                    AutoSize = false;
                }
                textOrientation = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text direction.
        /// </summary>
        /// <value>The text direction.</value>
        [Description("Direction of the Text"), Category("Appearance")]
        public Direction TextDirection
        {
            get
            {
                return textDirection;
            }
            set
            {

                textDirection = value;
                this.Invalidate();
            }
        }
        #endregion


        /// <summary>
        /// Gets or sets the type of the graphic.
        /// </summary>
        /// <value>The type of the graphic.</value>
        public GraphicsType GraphicType
        {
            get { return graphicsType; }
            set
            {
                graphicsType = value;
                Invalidate();
            }
        }

        //Create all of the properties we want the control to have and Override the ones it already has if they need to be used for special reasons. 

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// <value>The color of the back.</value>
        [Category("Appearance"), Description("The background color of the Label.")]
        [Browsable(true), DefaultValue(typeof(Color), "Transparent")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                _BackgroundBrush.Color = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the Text.</value>
        [Category("Appearance"), Description("The center color of the Text.")]
        [Browsable(true), DefaultValue(typeof(Color), "White")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                if (value == Color.Transparent)
                    _ForeColorTransparency = 0;
                _CenterBrush.Color = Color.FromArgb(_ForeColorTransparency, value);
            }
        }

        /// <summary>
        /// Gets or sets the fore color transparency.
        /// </summary>
        /// <value>The fore color transparency.</value>
        /// <remarks>A value between 0 and 255 that sets the transparency of the ForeColor.</remarks>
        [Category("Appearance"), Description("A value between 0 and 255 that sets the transparency of the ForeColor.")]
        [Browsable(true), DefaultValue(255)]
        public int ForeColorTransparency
        {
            get { return _ForeColorTransparency; }
            set
            {
                if (value > 255)
                    value = 255;
                if (value < 0 | this.ForeColor == Color.Transparent)
                    value = 0;
                _ForeColorTransparency = value;
                _CenterBrush.Color = Color.FromArgb(value, this.ForeColor);
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Text alignment.
        /// </summary>
        /// <value>The Text alignment.</value>
        /// <remarks>Aligns the Text to the left, right, top, or bottom of the Label.</remarks>
        [Category("Appearance"), Description("Aligns the Text to the left, right, top, or bottom of the Label.")]
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign
        {
            get { return _TextAlign; }
            set
            {
                _TextAlign = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        [Category("Appearance"), Description("The Image for the Label.")]
        [Browsable(true)]
        public Bitmap Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the image alignment.
        /// </summary>
        /// <value>The image align.</value>
        /// <remarks>Aligns the Image to the left, right, top, or bottom.</remarks>
        [Category("Appearance"), Description("Aligns the Image to the left, right, top, or bottom.")]
        [Browsable(true), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment ImageAlign
        {
            get { return _ImageAlign; }
            set
            {
                _ImageAlign = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the outline color of the Text.
        /// </summary>
        /// <value>The outline color of the Text.</value>
        [Category("Appearance"), Description("The outline color of the Text.")]
        [Browsable(true), DefaultValue(typeof(Color), "Black")]
        public Color OutlineColor
        {
            get { return _OutLinePen.Color; }
            set
            {
                _OutLinePen.Color = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the outline thickness.
        /// </summary>
        /// <value>The outline thickness.</value>
        /// <remarks>The thickness of the Text outline. Limited to a number between 1 and 10.</remarks>
        [Category("Appearance"), Description("The thickness of the Text outline. Limited to a number between 1 and 10.")]
        [Browsable(true), DefaultValue(1)]
        public int OutlineThickness
        {
            get { return _OutlineThickness; }
            set
            {
                if (value < 1)
                    value = 1;
                //Dont let the user set lower than 1
                if (value > 10)
                    value = 10;
                //Dont let the user set higher than 10
                _OutlineThickness = value;
                _OutLinePen.Width = value;
                _ShadowPen.Width = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        /// <exception cref="Exception">The border color does not support the Transparent color</exception>
        [Category("Appearance"), Description("The color of the Labels border.")]
        [Browsable(true), DefaultValue(typeof(Color), "Black")]
        public Color BorderColor
        {
            get { return _BorderPen.Color; }
            set
            {
                if (value == Color.Transparent)
                {
                    value = _BorderPen.Color;
                    //Set it back to the prior color
                    //Alert the user that Color.Transparent is not supported for this property
                    throw new Exception("The border color does not support the Transparent color");
                }
                _BorderPen.Color = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        [Category("Appearance"), Description("The style of the border around the Label.")]
        [Browsable(true), DefaultValue(typeof(BorderType), "None")]
        public BorderType BorderStyle
        {
            get { return _BorderStyle; }
            set
            {
                _BorderStyle = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Text pattern image.
        /// </summary>
        /// <value>The Text pattern image.</value>
        /// <remarks>An image used as a fill pattern for the center of the Text.</remarks>
        [Category("Appearance"), Description("An image used as a fill pattern for the center of the Text.")]
        [Browsable(true)]
        public Bitmap TextPatternImage
        {
            get { return _TextPatternImage; }
            set
            {
                _TextPatternImage = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the Text pattern image layout.
        /// </summary>
        /// <value>The Text pattern image layout.</value>
        [Category("Appearance"), Description("The layout of the pattern image inside the Text.")]
        [Browsable(true), DefaultValue(typeof(PatternLayout), "Stretch")]
        public PatternLayout TextPatternImageLayout
        {
            get { return _TextPatternImageLayout; }
            set
            {
                _TextPatternImageLayout = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show Text shadow.
        /// </summary>
        /// <value><c>true</c> if show Text shadow; otherwise, <c>false</c>.</value>
        [Category("Appearance"), Description("Show a shadow behind the Text.")]
        [Browsable(true), DefaultValue(false)]
        public bool ShowTextShadow
        {
            get { return _ShowTextShadow; }
            set
            {
                _ShowTextShadow = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the color of the shadow.
        /// </summary>
        /// <value>The color of the shadow.</value>
        /// <exception cref="Exception">The Shadow color does not support using Color.Transparent</exception>
        [Category("Appearance"), Description("The color of the shadow behind the Text.")]
        [Browsable(true), DefaultValue(typeof(Color), "Black")]
        public Color ShadowColor
        {
            get { return _ShadowColor; }
            set
            {
                if (value == Color.Transparent)
                {
                    value = _ShadowBrush.Color;
                    //Set it back to the prior color
                    //Alert the user that Color.Transparent is not supported for this property
                    throw new Exception("The Shadow color does not support using Color.Transparent");
                }
                _ShadowColor = value;
                _ShadowBrush.Color = Color.FromArgb(_ShadowTransparency, value);
                _ShadowPen.Color = Color.FromArgb(_ShadowTransparency, value);
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the shadow position.
        /// </summary>
        /// <value>The shadow position.</value>
        [Category("Appearance"), Description("The position of the shadow behind the Text.")]
        [Browsable(true), DefaultValue(typeof(ShadowArea), "BottomRight")]
        public ShadowArea ShadowPosition
        {
            get { return _ShadowPosition; }
            set
            {
                _ShadowPosition = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the shadow depth.
        /// </summary>
        /// <value>The shadow depth.</value>
        /// <remarks>A value between 1-10 that controls the depth of the shadow behind the Text.</remarks>
        [Category("Appearance"), Description("A value between 1-10 that controls the depth of the shadow behind the Text.")]
        [Browsable(true), DefaultValue(2)]
        public int ShadowDepth
        {
            get { return _ShadowDepth; }
            set
            {
                if (value < 1)
                    value = 1;
                //Dont let user set this property lower than 1
                if (value > 10)
                    value = 10;
                //Dont let user set this property higher than 10
                _ShadowDepth = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the shadow transparency.
        /// </summary>
        /// <value>The shadow transparency.</value>
        /// <remarks>A value between 0 and 255 that sets the transparency of the shadow.</remarks>
        [Category("Appearance"), Description("A value between 0 and 255 that sets the transparency of the shadow.")]
        [Browsable(true), DefaultValue(128)]
        public int ShadowTransparency
        {
            get { return _ShadowTransparency; }
            set
            {
                if (value < 0)
                    value = 0;
                //Dont let user set this property lower than 0
                if (value > 255)
                    value = 255;
                //Dont let user set this property higher than 255
                _ShadowTransparency = value;
                _ShadowBrush.Color = Color.FromArgb(value, _ShadowColor);
                _ShadowPen.Color = Color.FromArgb(value, _ShadowColor);
                this.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the shadow style.
        /// </summary>
        /// <value>The shadow style.</value>
        [Category("Appearance"), Description("The style used to draw the shadow.")]
        [Browsable(true), DefaultValue(typeof(ShadowDrawingType), "FillShadow")]
        public ShadowDrawingType ShadowStyle
        {
            get { return _ShadowStyle; }
            set
            {
                _ShadowStyle = value;
                this.Refresh();
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
        /// Gets or sets the Text rendering.
        /// </summary>
        /// <value>The Text rendering.</value>
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

        #region Smoothing Mode

        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.AntiAlias;

        /// <summary>
        /// Gets or sets the smoothing.
        /// </summary>
        /// <value>The smoothing.</value>
        public SmoothingMode Smoothing
        {
            get { return smoothing; }
            set
            {
                if (value == SmoothingMode.Invalid)
                {
                    value = SmoothingMode.AntiAlias;
                    Invalidate();
                }
                smoothing = value;
                Invalidate();
            }
        }

        #endregion


    }
}
