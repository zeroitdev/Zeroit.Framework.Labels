// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ScrollingText.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{
    
    #region Scrolling Text

    /// <summary>
    /// Sets the Scroll Direction of the <c><see cref="ZeroitMarqueeLabel" /></c> control.
    /// </summary>
    public enum ScrollDirection
    {
        /// <summary>
        /// Set direction from Right to Left
        /// </summary>
        RightToLeft,
        /// <summary>
        /// Set direction from Left to Right
        /// </summary>
        LeftToRight,
        /// <summary>
        /// Set direction boucing
        /// </summary>
        Bouncing
    }

    /// <summary>
    /// Sets the vertical text position of the <c><see cref="ZeroitMarqueeLabel" /></c> control.
    /// </summary>
    public enum VerticleTextPosition
    {
        /// <summary>
        /// Set the position to top.
        /// </summary>
        Top,
        /// <summary>
        /// Set the position to center.
        /// </summary>
        Center,
        /// <summary>
        /// Set the position to bottom.
        /// </summary>
        Botom
    }

    /// <summary>
    /// A class collection for rendering scrolling text.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [
    ToolboxBitmapAttribute(typeof(ZeroitMarqueeLabel), "ScrollingText.bmp"),
    DefaultEvent("TextClicked")
    ]
    public class ZeroitMarqueeLabel : System.Windows.Forms.Control
    {

        /// <summary>
        /// Sets the rendering mode.
        /// </summary>
        public enum drawMode
        {
            /// <summary>
            /// Set the rendering mode solid.
            /// </summary>
            Solid,
            /// <summary>
            /// Set the rendering mode gradient.
            /// </summary>
            Gradient
        }

        /// <summary>
        /// The timer
        /// </summary>
        private System.Windows.Forms.Timer timer;                            // Timer for text animation.
        //private string text = "Text";                   // Scrolling text
        /// <summary>
        /// The static text position
        /// </summary>
        private float staticTextPos = 0;                // The running x pos of the text
        /// <summary>
        /// The y position
        /// </summary>
        private float yPos = 0;                         // The running y pos of the text
        /// <summary>
        /// The scroll direction
        /// </summary>
        private ScrollDirection scrollDirection = ScrollDirection.RightToLeft;              // The direction the text will scroll
        /// <summary>
        /// The current direction
        /// </summary>
        private ScrollDirection currentDirection = ScrollDirection.LeftToRight;             // Used for text bouncing 
        /// <summary>
        /// The verticle text position
        /// </summary>
        private VerticleTextPosition verticleTextPosition = VerticleTextPosition.Center;    // Where will the text be vertically placed
        /// <summary>
        /// The scroll pixel distance
        /// </summary>
        private int scrollPixelDistance = 2;            // How far the text scrolls per timer event
        /// <summary>
        /// The show border
        /// </summary>
        private bool showBorder = true;                 // Show a border or not
        /// <summary>
        /// The stop scroll on mouse over
        /// </summary>
        private bool stopScrollOnMouseOver = false;     // Flag to stop the scroll if the user mouses over the text
        /// <summary>
        /// The scroll on
        /// </summary>
        private bool scrollOn = true;                   // Internal flag to stop / start the scrolling of the text
        /// <summary>
        /// The foreground brush
        /// </summary>
        private Brush foregroundBrush = null;           // Allow the user to set a custom Brush to the text Font
        /// <summary>
        /// The background brush
        /// </summary>
        private Brush backgroundBrush = null;           // Allow the user to set a custom Brush to the background
        /// <summary>
        /// The border color
        /// </summary>
        private Color borderColor = Color.Black;        // Allow the user to set the color of the control border
        /// <summary>
        /// The last known rect
        /// </summary>
        private RectangleF lastKnownRect;               // The last known position of the text

        /// <summary>
        /// The m color1
        /// </summary>
        private Color m_Color1 = Color.Black;  // First default color.
        /// <summary>
        /// The m color2
        /// </summary>
        private Color m_Color2 = Color.Gold;   // Second default color.

        /// <summary>
        /// The draw mode
        /// </summary>
        private drawMode _drawMode = drawMode.Solid;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMarqueeLabel" /> class.
        /// </summary>
        public ZeroitMarqueeLabel()
        {
            // Setup default properties for ScrollingText control
            InitializeComponent();

            //This turns off internal double buffering of all custom GDI+ drawing
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            //setup the timer object
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 25;    //default timer interval
            timer.Enabled = true;
            timer.Tick += new EventHandler(Tick);
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
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Make sure our brushes are cleaned up
                if (foregroundBrush != null)
                    foregroundBrush.Dispose();

                //Make sure our brushes are cleaned up
                if (backgroundBrush != null)
                    backgroundBrush.Dispose();

                //Make sure our timer is cleaned up
                if (timer != null)
                    timer.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //ScrollingText			
            this.Name = "ScrollingText";
            this.Size = new System.Drawing.Size(216, 40);
            this.Click += new System.EventHandler(this.ScrollingText_Click);
        }
        #endregion

        //Controls the animation of the text.
        /// <summary>
        /// Ticks the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Tick(object sender, EventArgs e)
        {
            //update rectangle to include where to paint for new position			
            //lastKnownRect.X -= 10;
            //lastKnownRect.Width += 20;			
            lastKnownRect.Inflate(10, 5);

            //create region based on updated rectangle
            Region updateRegion = new Region(lastKnownRect);

            //repaint the control			
            Invalidate(updateRegion);
            Update();
        }

        //Paint the ScrollingTextCtrl.
        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="pe">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            //Console.WriteLine(pe.ClipRectangle.X + ",  " + pe.ClipRectangle.Y + ",  " + pe.ClipRectangle.Width + ",  " + pe.ClipRectangle.Height);

            TransInPaint(pe.Graphics);

            //Paint the text to its new position
            DrawScrollingText(pe.Graphics);

            //pass on the graphics obj to the base Control class
            base.OnPaint(pe);
        }

        //Draw the scrolling text on the control		
        /// <summary>
        /// Draws the scrolling text.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void DrawScrollingText(Graphics canvas)
        {
            //measure the size of the string for placement calculation
            SizeF stringSize = canvas.MeasureString(this.Text, this.Font);

            // This is a fancy brush that draws graded colors.
            Brush MyBrush = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, m_Color1, m_Color2, 10);


            //Calculate the begining x position of where to paint the text
            if (scrollOn)
            {
                CalcTextPosition(stringSize);
            }

            ////Clear the control with user set BackColor
            //if (backgroundBrush != null)
            //{
            //    canvas.FillRectangle(backgroundBrush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            //}
            //else
            //    canvas.Clear(this.BackColor);

            // Draw the border
            if (showBorder)
            {
                using (Pen borderPen = new Pen(borderColor))
                    canvas.DrawRectangle(borderPen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);

            }

            switch (DrawMode)
            {
                case drawMode.Solid:
                    // Draw the text string in the bitmap in memory
                    if (foregroundBrush == null)
                    {
                        using (Brush tempForeBrush = new System.Drawing.SolidBrush(this.ForeColor))
                            canvas.DrawString(this.Text, this.Font, tempForeBrush, staticTextPos, yPos);
                    }
                    else
                    {
                        canvas.DrawString(this.Text, this.Font, foregroundBrush, staticTextPos, yPos);

                    }
                    break;
                case drawMode.Gradient:
                    // Draw the text string in the bitmap in memory
                    if (foregroundBrush == null)
                    {
                        using (Brush tempForeBrush = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, m_Color1, m_Color2, 10))
                            canvas.DrawString(this.Text, this.Font, tempForeBrush, staticTextPos, yPos);
                    }
                    else
                    {
                        canvas.DrawString(this.Text, this.Font, MyBrush, staticTextPos, yPos);

                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }



            lastKnownRect = new RectangleF(staticTextPos, yPos, stringSize.Width, stringSize.Height);
            EnableTextLink(lastKnownRect);
        }

        /// <summary>
        /// Calculates the text position.
        /// </summary>
        /// <param name="stringSize">Size of the string.</param>
        private void CalcTextPosition(SizeF stringSize)
        {
            switch (scrollDirection)
            {
                case ScrollDirection.RightToLeft:
                    if (staticTextPos < (-1 * (stringSize.Width)))
                        staticTextPos = this.ClientSize.Width - 1;
                    else
                        staticTextPos -= scrollPixelDistance;
                    break;
                case ScrollDirection.LeftToRight:
                    if (staticTextPos > this.ClientSize.Width)
                        staticTextPos = -1 * stringSize.Width;
                    else
                        staticTextPos += scrollPixelDistance;
                    break;
                case ScrollDirection.Bouncing:
                    if (currentDirection == ScrollDirection.RightToLeft)
                    {
                        if (staticTextPos < 0)
                            currentDirection = ScrollDirection.LeftToRight;
                        else
                            staticTextPos -= scrollPixelDistance;
                    }
                    else if (currentDirection == ScrollDirection.LeftToRight)
                    {
                        if (staticTextPos > this.ClientSize.Width - stringSize.Width)
                            currentDirection = ScrollDirection.RightToLeft;
                        else
                            staticTextPos += scrollPixelDistance;
                    }
                    break;
            }

            //Calculate the vertical position for the scrolling text				
            switch (verticleTextPosition)
            {
                case VerticleTextPosition.Top:
                    yPos = 2;
                    break;
                case VerticleTextPosition.Center:
                    yPos = (this.ClientSize.Height / 2) - (stringSize.Height / 2);
                    break;
                case VerticleTextPosition.Botom:
                    yPos = this.ClientSize.Height - stringSize.Height;
                    break;
            }
        }

        #region Mouse over, text link logic
        /// <summary>
        /// Enables the text link.
        /// </summary>
        /// <param name="textRect">The text rect.</param>
        private void EnableTextLink(RectangleF textRect)
        {
            Point curPt = this.PointToClient(Cursor.Position);

            //if (curPt.X > textRect.Left && curPt.X < textRect.Right
            //	&& curPt.Y > textRect.Top && curPt.Y < textRect.Bottom)			
            if (textRect.Contains(curPt))
            {
                //Stop the text of the user mouse's over the text
                if (stopScrollOnMouseOver)
                    scrollOn = false;

                this.Cursor = Cursors.Hand;
            }
            else
            {
                //Make sure the text is scrolling if user's mouse is not over the text
                scrollOn = true;

                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the ScrollingText control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ScrollingText_Click(object sender, System.EventArgs e)
        {
            //Trigger the text clicked event if the user clicks while the mouse 
            //is over the text.  This allows the text to act like a hyperlink
            if (this.Cursor == Cursors.Hand)
                OnTextClicked(this, new EventArgs());
        }

        /// <summary>
        /// Delegate TextClickEventHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void TextClickEventHandler(object sender, EventArgs args);
        /// <summary>
        /// Occurs when [text clicked].
        /// </summary>
        public event TextClickEventHandler TextClicked;

        /// <summary>
        /// Handles the <see cref="E:TextClicked" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnTextClicked(object sender, EventArgs args)
        {
            //Call the delegate
            if (TextClicked != null)
                TextClicked(sender, args);
        }
        #endregion


        #region Properties

        /// <summary>
        /// Gets or sets the rendering mode.
        /// </summary>
        /// <value>The draw mode.</value>
        [Browsable(true)]
        [CategoryAttribute("Scrolling Text")]
        [Description("This sets the Draw Mode")]
        public drawMode DrawMode
        {
            get { return _drawMode; }
            set
            {
                _drawMode = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the gradient color.
        /// </summary>
        /// <value>The gradient color 1.</value>
        [Browsable(true)]
        [CategoryAttribute("Scrolling Text")]
        [Description("This sets the gradient color")]
        public Color GradColor1
        {
            get { return m_Color1; }
            set
            {
                m_Color1 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the gradient color.
        /// </summary>
        /// <value>The gradient color 2.</value>
        [Browsable(true)]
        [CategoryAttribute("Scrolling Text")]
        [Description("This sets the gradient color")]
        public Color GradColor2
        {
            get { return m_Color2; }
            set
            {
                m_Color2 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the text scrolling speed.
        /// </summary>
        /// <value>The text scroll speed.</value>
        /// <remarks>The timer interval that determines how often the control is repainted.</remarks>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The timer interval that determines how often the control is repainted")
        ]
        public int TextScrollSpeed
        {
            set
            {
                timer.Interval = value;
                Invalidate();
            }
            get
            {
                return timer.Interval;

            }
        }

        /// <summary>
        /// Gets or sets the text scroll distance.
        /// </summary>
        /// <value>The text scroll distance.</value>
        /// <remarks>How many pixels will the text be moved per Paint.</remarks>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("How many pixels will the text be moved per Paint")
        ]
        public int TextScrollDistance
        {
            set
            {
                scrollPixelDistance = value;
                Invalidate();
            }
            get
            {
                return scrollPixelDistance;
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The text that will scroll accros the control")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        //[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [SettingsBindable(true)]
        public new string Text
        {
            set
            {
                base.Text = value;
                this.Invalidate();
                this.Update();
            }
            get
            {
                return base.Text;
            }
        }

        /// <summary>
        /// Gets or sets the scroll direction.
        /// </summary>
        /// <value>The scroll direction.</value>
        /// <remarks>What direction the text will scroll: Left to Right, Right to Left, or Bouncing.</remarks>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("What direction the text will scroll: Left to Right, Right to Left, or Bouncing")
        ]
        public ScrollDirection ScrollDirection
        {
            set
            {
                scrollDirection = value;
                Invalidate();
            }
            get
            {
                return scrollDirection;
            }
        }

        /// <summary>
        /// Gets or sets the vertical text position.
        /// </summary>
        /// <value>The vertical text position.</value>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The vertical alignment of the text")
        ]
        public VerticleTextPosition VerticleTextPosition
        {
            set
            {
                verticleTextPosition = value;
                Invalidate();
            }
            get
            {
                return verticleTextPosition;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to enable/disable border.
        /// </summary>
        /// <value><c>true</c> if show border; otherwise, <c>false</c>.</value>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("Turns the border on or off")
        ]
        public bool ShowBorder
        {
            set
            {
                showBorder = value;
                Invalidate();
            }
            get
            {
                return showBorder;
            }
        }

        /// <summary>
        /// Gets or sets the color of the border.
        /// </summary>
        /// <value>The color of the border.</value>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The color of the border")
        ]
        public Color BorderColor
        {
            set
            {
                borderColor = value;
                Invalidate();
            }
            get
            {
                return borderColor;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to stop scrolling on mouse over.
        /// </summary>
        /// <value><c>true</c> if stop scrolling on mouse over; otherwise, <c>false</c>.</value>
        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("Determines if the text will stop scrolling if the user's mouse moves over the text")
        ]
        public bool StopScrollOnMouseOver
        {
            set
            {
                stopScrollOnMouseOver = value;
                Invalidate();
            }
            get
            {
                return stopScrollOnMouseOver;
            }
        }

        /// <summary>
        /// Gets or sets the foreground brush.
        /// </summary>
        /// <value>The foreground brush.</value>
        [
        Browsable(false)
        ]
        public Brush ForegroundBrush
        {
            set
            {
                foregroundBrush = value;
                Invalidate();
            }
            get
            {
                return foregroundBrush;
            }
        }

        /// <summary>
        /// Gets or sets the background brush.
        /// </summary>
        /// <value>The background brush.</value>
        [
        ReadOnly(true)
        ]
        public Brush BackgroundBrush
        {
            set
            {
                backgroundBrush = value;
                Invalidate();
            }
            get
            {
                return backgroundBrush;
            }
        }
        #endregion
    }



    #endregion

}