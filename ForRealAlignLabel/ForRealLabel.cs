// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ForRealLabel.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region ZeroitRealLabel

    /// <summary>
    /// A class collection for rendering an aligned label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
	[ToolboxBitmap(typeof(ZeroitRealLabel), "images.TextAlign.bmp"),
    System.ComponentModel.DefaultPropertyAttribute("Angle")]
    public class ZeroitRealLabel : System.Windows.Forms.Control
    {
        #region Events
        /// <summary>
        /// Delegate OnAngleChangedHandler
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="Angle">The angle.</param>
        public delegate void OnAngleChangedHandler(object sender, int Angle);

        /// <summary>
        /// Occurs when [on angle changed event].
        /// </summary>
        [Category("Action")]
        [Description("Fired when angle has changed")]
        public event OnAngleChangedHandler OnAngleChangedEvent;
        #endregion

        #region Variable Declarations
        /// <summary>
        /// The track mouse
        /// </summary>
        private bool _trackMouse = false;
        /// <summary>
        /// The show helpers
        /// </summary>
        private bool showHelpers = false;
        /// <summary>
        /// The show border
        /// </summary>
        private bool showBorder = false;
        /// <summary>
        /// The angle
        /// </summary>
        private int _Angle = 0;
        /// <summary>
        /// The peg size
        /// </summary>
        private const int PegSize = 3;
        /// <summary>
        /// The offset
        /// </summary>
        private const int _Offset = 10;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or Sets Angle for control
        /// </summary>
        /// <value>The angle.</value>
        [Category("TextAlign")]
        [Description("Gets/Sets angle of text")]
        public int Angle
        {
            get
            {
                return _Angle;
            }

            set
            {
                if (!Enabled)
                    return;

                if (value < -90 || value > 90)
                    return;

                _Angle = value;
                Invalidate();

                if (OnAngleChangedEvent != null)
                    OnAngleChangedEvent(this, _Angle);

            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        [Browsable(true)]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show helpers.
        /// </summary>
        /// <value><c>true</c> if show helpers; otherwise, <c>false</c>.</value>
        public bool ShowHelpers
        {
            get { return showHelpers; }
            set
            {
                showHelpers = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether show border.
        /// </summary>
        /// <value><c>true</c> if show border; otherwise, <c>false</c>.</value>
        public bool ShowBorder
        {
            get { return showBorder; }
            set
            {
                showBorder = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Called when [angle changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="angle">The angle.</param>
        protected virtual void OnAngleChanged(object sender, int angle)
        {
            if (OnAngleChangedEvent != null)
                OnAngleChangedEvent(this, _Angle);
        }

        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitRealLabel"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public ZeroitRealLabel(System.ComponentModel.IContainer container)
        {
            _Init();
            container.Add(this);
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitRealLabel"/> class.
        /// </summary>
        public ZeroitRealLabel()
        {
            _Init();
            InitializeComponent();
        }

        /// <summary>
        /// Common Contructor Code
        /// </summary>
        private void _Init()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();
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



        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            base.OnPaint(e);
        }

        /// <summary>
        /// Processes the dialog key.
        /// </summary>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <returns>true if the key was processed by the control; otherwise, false.</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (!Enabled)
                return base.ProcessDialogKey(keyData);

            int delta = Angle;

            switch (keyData)
            {
                case Keys.Home:
                    delta = 0;
                    break;

                case (Keys.Down | Keys.Control):
                    delta -= 15;
                    delta = ((delta / 15) * 15);
                    break;

                case Keys.Down:
                    delta -= 1;
                    break;

                case (Keys.Up | Keys.Control):
                    delta += 15;
                    delta = ((delta / 15) * 15);
                    break;

                case Keys.Up:
                    delta += 1;
                    break;

                default:
                    return base.ProcessDialogKey(keyData);

            }

            Angle = delta;
            return false;

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


        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // TextAlign
            // 
            //this.BackColor = System.Drawing.SystemColors.Window;
            this.Name = "TextAlign";
            this.Size = new System.Drawing.Size(120, 176);
            this.SizeChanged += new System.EventHandler(this.TextAlign_SizeChanged);
            this.Enter += new System.EventHandler(this.TextAlign_Enter);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TextAlign_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TextAlign_Paint);
            this.Leave += new System.EventHandler(this.TextAlign_Leave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextAlign_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextAlign_MouseDown);

        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Renders the specified g.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="rcClient">The rc client.</param>
        private void _Render(Graphics g, Rectangle rcClient)
        {
            //g.Clear(BackColor);
            g.SmoothingMode = SmoothingMode.HighQuality;
            // Render Background
            //g.FillRectangle(Enabled ? new SolidBrush(this.BackColor) : SystemBrushes.Control, rcClient);

            if (ShowBorder)
            {
                // Render Frame
                g.DrawRectangle(Focused ? SystemPens.Highlight : SystemPens.ControlDark, rcClient.X, rcClient.Y, rcClient.Width - 1, rcClient.Height - 1);

            }

            Point ptCenter = new Point(_Offset, rcClient.Height / 2);

            StringFormat format = new StringFormat(StringFormat.GenericDefault);
            format.HotkeyPrefix = HotkeyPrefix.None;

            SizeF sz = g.MeasureString(Text, Font, Point.Empty, format);

            Rectangle rcText = rcClient;
            rcText.X += 2; // Indent from Left

            // Rotate text on given Angle
            g.TranslateTransform(ptCenter.X, ptCenter.Y);
            g.RotateTransform(-_Angle);
            g.DrawString(Text, Font, new SolidBrush(ForeColor), new Point(0, (int)-(sz.Height / 2)), format);
            g.ResetTransform();


            // Render Dial
            Rectangle rc;

            // Render tick @ 15 degree intervals
            for (int tick = -90; tick <= 90; tick += 15)
            {
                Point ptSelectStart = _AngleToPoint(ptCenter, (double)_Angle, ptCenter.X + sz.Width - 8);
                Point ptSelectEnd = _AngleToPoint(ptCenter, (double)_Angle, rcClient.Width - 24);

                if (ShowHelpers)
                {
                    g.DrawLine(Pens.Black, ptSelectStart, ptSelectEnd);

                }

                Point pt = _AngleToPoint(ptCenter, tick, rcClient.Width - 18);

                rc = new Rectangle(pt, Size.Empty);
                rc.Inflate(1, 1);

                if (ShowHelpers)
                {
                    g.FillRectangle(Brushes.Black, rc);
                }


                if ((tick % 45) == 0)
                {
                    using (GraphicsPath myPath = new GraphicsPath())
                    {
                        myPath.AddLine(pt.X, pt.Y - PegSize, pt.X + PegSize, pt.Y);
                        myPath.AddLine(pt.X + PegSize, pt.Y, pt.X, pt.Y + PegSize);
                        myPath.AddLine(pt.X, pt.Y + PegSize, pt.X - PegSize, pt.Y);
                        myPath.CloseFigure();

                        if (ShowHelpers)
                        {
                            g.FillPath(tick == _Angle ? Brushes.Red : Brushes.Black, myPath);

                        }

                        myPath.AddLine(pt.X, pt.Y - PegSize, pt.X + PegSize, pt.Y);
                        myPath.AddLine(pt.X + PegSize, pt.Y, pt.X, pt.Y + PegSize);
                        myPath.AddLine(pt.X, pt.Y + PegSize, pt.X - PegSize, pt.Y);
                        myPath.CloseFigure();

                        if (ShowHelpers)
                        {
                            g.DrawPath(Pens.Black, myPath);
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Convert Angle to point
        /// </summary>
        /// <param name="ptOffset">The pt offset.</param>
        /// <param name="angle">The angle.</param>
        /// <param name="radius">The radius.</param>
        /// <returns>Point.</returns>
        private Point _AngleToPoint(Point ptOffset, double angle, double radius)
        {
            double radians = angle / (180.0 / Math.PI);
            int x = ptOffset.X + (int)((double)radius * Math.Cos(radians));
            int y = ptOffset.Y - (int)((double)radius * Math.Sin(radians));
            return new Point(x, y);
        }

        /// <summary>
        /// Arcs the tangent.
        /// </summary>
        /// <param name="ratio">The ratio.</param>
        /// <returns>System.Double.</returns>
        private double _ArcTangent(double ratio)
        {
            double angle = Math.Atan(ratio);

            // convert radians to degrees 
            return angle * (180.0 / Math.PI); ;
        }

        /// <summary>
        /// Arcs the tangent.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>System.Double.</returns>
        private double _ArcTangent(double x, double y)
        {
            double angle = 0.0;

            // both x and y are 0
            if ((x == 0.0) && (y == 0.0))
            {
                // Set tthe angle to Zero degrees 
                angle = 0.0;
            }
            else if (x == 0.0)
            {
                // If we're on the y axis line 
                angle = (y < 0.0) ? -90 : 90.0;
            }
            else
            {
                // else neither x or y is zero
                // Find the arc-tangent of y / x in degrees
                angle = _ArcTangent((y / x));

                // if x is negative 
                if (x < 0.0)
                {
                    angle += (y > 0.0) ? 180.0 : -180.0;
                }

                if (angle < -90)
                    angle = -90;

                if (angle > 90)
                    angle = 90;

            }

            return angle;
        }

        /// <summary>
        /// Converts a point to angle
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="center">The center.</param>
        /// <returns>System.Double.</returns>
        private double PointToAngle(Point point, Point center)
        {
            // Calculate the position user click relative to the center
            int x = point.X - center.X;
            int y = center.Y - point.Y;

            //Convert xy position to an angle.
            double angle = _ArcTangent((double)x, (double)y);

            return angle;
        }

        /// <summary>
        /// Render Control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void TextAlign_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            _Render(e.Graphics, this.ClientRectangle);
        }

        /// <summary>
        /// Update Angle in relation to mouse
        /// </summary>
        /// <param name="point">The point.</param>
        private void _UpdateAngle(Point point)
        {
            Rectangle rcClient = this.ClientRectangle;
            Point ptCenter = new Point(_Offset, rcClient.Height / 2);

            if (_trackMouse)
            {
                //Convert xy position to an angle.
                Angle = (int)(PointToAngle(point, ptCenter));
            }

        }

        /// <summary>
        /// Track mouse as user moves mouse over control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TextAlign_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!Enabled)
                return;

            _UpdateAngle(new Point(e.X, e.Y));
            Invalidate();
        }

        /// <summary>
        /// End tracking mouse
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TextAlign_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!Enabled)
                return;

            _UpdateAngle(new Point(e.X, e.Y));
            _trackMouse = false;
            Capture = false;
            Invalidate();

        }

        /// <summary>
        /// Begin Tracking mouse
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void TextAlign_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!Enabled)
                return;

            _trackMouse = true;
            this.Capture = true;
            Focus();
            _UpdateAngle(new Point(e.X, e.Y));
            Invalidate();
        }

        /// <summary>
        /// Control size changed, refresh control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextAlign_SizeChanged(object sender, System.EventArgs e)
        {
            Refresh();
        }

        #endregion

        /// <summary>
        /// Handles the Enter event of the TextAlign control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextAlign_Enter(object sender, System.EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Handles the Leave event of the TextAlign control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void TextAlign_Leave(object sender, System.EventArgs e)
        {
            Invalidate();
        }


        
    }


    #endregion

}