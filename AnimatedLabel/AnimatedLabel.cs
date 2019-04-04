// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="AnimatedLabel.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region AnimatedLabel

    /// <summary>
    /// A class collection for rendering Swing Animated Label
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    /// <seealso cref="System.ComponentModel.ICustomTypeDescriptor" />
    [ToolboxBitmapAttribute(typeof(ZeroitSwingLabel), "EffectsLabel.bmp"),
    Description("Label with shadow and animation (transparency, rotation and zoom)")]
    public partial class ZeroitSwingLabel : Control, ICustomTypeDescriptor
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitSwingLabel" /> class.
        /// </summary>
        public ZeroitSwingLabel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            InitializeComponent();
            //DoubleBuffered = true;//Flickers a lot without this

            _CurrentAlpha = 255;
            _MinAlpha = 255;
            _MaxAlpha = 255;
            _CurrentZoom = 100;
            _MinZoom = 100;
            base.AutoSize = true;
        }


        #endregion
        
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
        
        #region Overriden inherited properties
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [Description("Text to display")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                RecalculateSize(_Letterwise == false);

                numberOfLines = 1;//there is allways at least one line
                foreach (Char c in Text)
                    if (c == '\n')
                        numberOfLines++;
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                RecalculateSize(_Letterwise == false);
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        public override Color ForeColor
        {
            get { return Color.FromArgb(255, base.ForeColor); }
            set { base.ForeColor = value; }
        }
        /// <summary>
        /// This property is not relevant for this class.
        /// </summary>
        /// <value><c>true</c> if [automatic size]; otherwise, <c>false</c>.</value>
        [Browsable(true), DefaultValue(true)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                RecalculateSize(_Letterwise == false);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [Description("Enables/disables animation")]
        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                if (value)//value==true
                    RecalculateTimer();
                else
                    aTimer.Stop();
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                //brushove dispozat
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region ShadowProperties
        /// <summary>
        /// The shadow offset
        /// </summary>
        private Point _ShadowOffset;


        /// <summary>
        /// Gets or sets the shadow offset.
        /// </summary>
        /// <value>The shadow offset.</value>
        [Category("Effects"), DefaultValue(typeof(Point), "0, 0"),
        Description("If differs from (0,0), creates shadow with specified offset from Left and Top")]
        public Point ShadowOffset
        {
            get { return _ShadowOffset; }
            set
            {
                _ShadowOffset = value;
                RecalculateSize(_Letterwise == false);
            }
        }

        /// <summary>
        /// Gets or sets the color of the shadow.
        /// </summary>
        /// <value>The color of the shadow.</value>
        [Category("Effects"), DefaultValue(typeof(Color), "LightGray"),
        Description("If ShadowOffset is set, this is color of the shadow")]
        public Color ShadowColor
        {
            get { return Color.FromArgb(255, BackBrush.Color); }
            set
            {
                BackBrush.Color = value;
                Refresh();
            }
        }
        #endregion

        #region Zoom
        /// <summary>
        /// The minimum zoom
        /// </summary>
        private byte _MinZoom;
        /// <summary>
        /// Gets or sets the minimum zoom.
        /// </summary>
        /// <value>The minimum zoom.</value>
        [Category("Effects"), DefaultValue((byte)100),
        Description("Animate text zoom, in range MinZoom%-100%")]
        public byte MinZoom
        {
            get { return _MinZoom; }
            set
            {
                _MinZoom = value <= 100 ? value : (byte)100;
                if (_MinZoom == 0)
                    _MinZoom = 1;
                if (_CurrentZoom < _MinZoom)
                    _CurrentZoom = _MinZoom;
                if (_MinZoom < 100)
                {
                    aTimer.Start();
                }
                else
                    RecalculateTimer();
                Refresh();
            }
        }
        #endregion

        #region AlphaProperties
        /// <summary>
        /// The maximum alpha
        /// </summary>
        private byte _MaxAlpha;
        /// <summary>
        /// Gets or sets the maximum alpha.
        /// </summary>
        /// <value>The maximum alpha.</value>
        [Category("Effects"), DefaultValue((byte)255),
        Description("Animate text transparency, in range MinAlpha-MaxAlpha")]
        public byte MaxAlpha
        {
            get { return _MaxAlpha; }
            set
            {
                _MaxAlpha = value;
                if (_CurrentAlpha > _MaxAlpha || _MaxAlpha <= _MinAlpha)
                    _CurrentAlpha = _MaxAlpha;
                if (_MinAlpha < _MaxAlpha)
                {
                    aTimer.Start();
                }
                else
                    RecalculateTimer();
                ForeColor = Color.FromArgb(_CurrentAlpha, ForeColor);
                BackBrush.Color = Color.FromArgb(_CurrentAlpha, BackBrush.Color);
                Refresh();
            }
        }

        /// <summary>
        /// The minimum alpha
        /// </summary>
        private byte _MinAlpha;
        /// <summary>
        /// Gets or sets the minimum alpha.
        /// </summary>
        /// <value>The minimum alpha.</value>
        [Category("Effects"), DefaultValue((byte)255),
        Description("Animate text transparency, in range MinAlpha-MaxAlpha")]
        public byte MinAlpha
        {
            get { return _MinAlpha; }
            set
            {
                _MinAlpha = value;
                if (_CurrentAlpha < _MinAlpha || _MaxAlpha <= _MinAlpha)
                    _CurrentAlpha = _MinAlpha;
                if (_MinAlpha < _MaxAlpha)
                {
                    aTimer.Start();
                }
                else
                    RecalculateTimer();
                ForeColor = Color.FromArgb(_CurrentAlpha, ForeColor);
                BackBrush.Color = Color.FromArgb(_CurrentAlpha, BackBrush.Color);
                Refresh();
            }
        }
        #endregion

        #region RotateProperties
        /// <summary>
        /// The maximum rotate
        /// </summary>
        private sbyte _MaxRotate;
        /// <summary>
        /// Gets or sets the maximum rotate.
        /// </summary>
        /// <value>The maximum rotate.</value>
        [Category("Effects"), DefaultValue(typeof(sbyte), "0"),
        //DefaultValue does not have overload for sbyte, so we use general type specification (otherwise, it is allways bold in VisualStudio's properties panel)
        Description("Animate text rotation, in range MinRotate°-MaxRotate°")]
        public sbyte MaxRotate
        {
            get { return _MaxRotate; }
            set
            {
                _MaxRotate = value;
                if (_CurrentRotate > _MaxRotate || _MaxRotate <= _MinRotate)
                    _CurrentRotate = _MaxRotate;
                if (_MinRotate < _MaxRotate)
                {
                    aTimer.Start();
                }
                else
                    RecalculateTimer();
                RecalculateSize(_Letterwise == false);
                Refresh();
            }
        }

        /// <summary>
        /// The minimum rotate
        /// </summary>
        private sbyte _MinRotate;
        /// <summary>
        /// Gets or sets the minimum rotate.
        /// </summary>
        /// <value>The minimum rotate.</value>
        [Category("Effects"), DefaultValue(typeof(sbyte), "0"),
        //DefaultValue does not have overload for sbyte, so we use general type specification (otherwise, it is allways bold in VisualStudio's properties panel)
        Description("Animate text rotation, in range MinRotate°-MaxRotate°")]
        public sbyte MinRotate
        {
            get { return _MinRotate; }
            set
            {
                _MinRotate = value;
                if (_CurrentRotate < _MinRotate || _MaxRotate <= _MinRotate)
                    _CurrentRotate = _MinRotate;
                if (_MinRotate < _MaxRotate)
                {
                    aTimer.Start();
                }
                else
                    RecalculateTimer();
                RecalculateSize(_Letterwise == false);
                Refresh();
            }
        }
        #endregion

        #region Letterwise
        /// <summary>
        /// The letterwise
        /// </summary>
        private bool _Letterwise = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitSwingLabel" /> is letterwise.
        /// </summary>
        /// <value><c>true</c> if letterwise; otherwise, <c>false</c>.</value>
        [Category("Effects"), DefaultValue(true),
        Description("Determines wheather animation should be letter by letter, or apply to whole string at once")]
        public bool Letterwise
        {
            get { return _Letterwise; }
            set
            {
                _Letterwise = value;
                RecalculateSize(true);
                Refresh();
            }
        }
        #endregion

        #region Inner workings
        /// <summary>
        /// The current alpha
        /// </summary>
        private byte _CurrentAlpha;
        /// <summary>
        /// The alpha step
        /// </summary>
        private sbyte _AlphaStep = 2;//change alpha by this value after each animation timer tick
        /// <summary>
        /// The current zoom
        /// </summary>
        private byte _CurrentZoom;
        /// <summary>
        /// The zoom step
        /// </summary>
        private sbyte _ZoomStep = 1;
        /// <summary>
        /// The current rotate
        /// </summary>
        private sbyte _CurrentRotate;
        /// <summary>
        /// The rotate step
        /// </summary>
        private sbyte _RotateStep = 2;
        /// <summary>
        /// The back brush
        /// </summary>
        private SolidBrush BackBrush = new SolidBrush(Color.LightGray);

        /// <summary>
        /// The character sizes
        /// </summary>
        private RectangleF[] charSizes = null;
        /// <summary>
        /// The space size
        /// </summary>
        SizeF spaceSize;
        /// <summary>
        /// The number of lines
        /// </summary>
        uint numberOfLines;
        /// <summary>
        /// The in paint
        /// </summary>
        bool InPaint = false;

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {

            TransInPaint(e.Graphics);

            lock (this)
            {
                if (InPaint)
                    return;//control is allready in paint event
                InPaint = true;
            }
            //e.Graphics.PageUnit = GraphicsUnit.Pixel;
            if (!_Letterwise || (_CurrentRotate == 0 && !aTimer.Enabled))
            //this case executes faster, so use it when rotation is disabled
            {
                //e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                SizeF tsize = e.Graphics.MeasureString(Text, Font);
                tsize = new SizeF(tsize.Width + ShadowOffset.X, tsize.Height + ShadowOffset.Y);

                if (_ShadowOffset != new Point(0, 0))
                {
                    e.Graphics.TranslateTransform((Size.Width + ShadowOffset.X) / 2, (Size.Height + ShadowOffset.Y) / 2);
                    if (_CurrentRotate != 0)
                        e.Graphics.RotateTransform(_CurrentRotate);
                    if (_CurrentZoom < 100)
                        e.Graphics.ScaleTransform(_CurrentZoom / 100.0f, _CurrentZoom / 100.0f);
                    e.Graphics.DrawString(Text, Font, BackBrush, -tsize.Width / 2, -tsize.Height / 2);
                    e.Graphics.Transform = new Matrix();//clear tranformation matrix
                }

                e.Graphics.TranslateTransform(Size.Width / 2, Size.Height / 2);
                if (_CurrentRotate != 0)
                    e.Graphics.RotateTransform(_CurrentRotate);
                if (_CurrentZoom < 100)
                    e.Graphics.ScaleTransform(_CurrentZoom / 100.0f, _CurrentZoom / 100.0f);
                e.Graphics.DrawString(Text, Font, new SolidBrush(base.ForeColor), -tsize.Width / 2, -tsize.Height / 2);
                //Our ForeColor does not return proper alpha (alpha=255), so use (correct) parent color 
            }
            else//_Letterwise=true
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //animation is not smooth without anti-aliasing
                const float _ZeroRotationAngle = 0.1f;
                //if this is 0, text shakes if zoom is applied, but no rotation is applied

                bool shadow_exists = (_ShadowOffset != new Point(0, 0));
                Matrix empty = new Matrix();

                RectangleF posRect = new RectangleF();
                posRect.Size = e.Graphics.MeasureString(Text, Font);
                posRect.Location = new PointF((Size.Width - posRect.Width - _ShadowOffset.X) / 2, (Size.Height - posRect.Height - _ShadowOffset.Y) / 2);

                Single lineHeight = posRect.Height / numberOfLines;

                RectangleF lastChar = new RectangleF(posRect.Location, new SizeF(0, 0));
                if (shadow_exists)//first draw all shadows, and later "main" text over them
                    //this ensures that shadow never draws over normal text
                    for (int i = 0; i < Text.Length; i++)
                    {
                        e.Graphics.Transform = empty;//e.Graphics.Transform.Reset(); did not work! (i don't know why)

                        if (Text[i] == '\n')//when newline character encountered, manually compute rectangle
                            lastChar = new RectangleF(posRect.X, lastChar.Y + lineHeight, 0, lineHeight);
                        else
                        {
                            if (charSizes[i].Height == 0 || charSizes[i].Width == 0)
                                //that was a non-printable character (like space, tab, and so on)
                                //replace it with a space the size of spaceSize
                                lastChar = new RectangleF(new PointF(lastChar.X + lastChar.Width, lastChar.Y), spaceSize);
                            else
                                lastChar = new RectangleF(new PointF(lastChar.X + lastChar.Width, lastChar.Y), charSizes[i].Size);
                        }

                        e.Graphics.TranslateTransform(lastChar.X + lastChar.Width / 2 + _ShadowOffset.X, lastChar.Y + lastChar.Height / 2 + _ShadowOffset.Y);
                        if (_CurrentRotate == 0)
                            e.Graphics.RotateTransform(_ZeroRotationAngle);
                        else
                            e.Graphics.RotateTransform(_CurrentRotate);
                        e.Graphics.ScaleTransform(_CurrentZoom / 100.0f, _CurrentZoom / 100.0f);
                        e.Graphics.DrawString(Text[i].ToString(), Font, BackBrush, -lastChar.Width / 2.0f, -lastChar.Height / 2.0f, StringFormat.GenericTypographic);
                    }
                lastChar = new RectangleF(posRect.Location, new SizeF(0, 0));//reinitialize lastChar
                for (int i = 0; i < Text.Length; i++)
                {
                    e.Graphics.Transform = empty;//e.Graphics.Transform.Reset(); did not work! (i don't know why)

                    if (Text[i] == '\n')//when newline character encountered, manually compute rectangle
                        lastChar = new RectangleF(posRect.X, lastChar.Y + lineHeight, 0, lineHeight);
                    else
                    {
                        if (charSizes[i].Height == 0 || charSizes[i].Width == 0)
                            //that was a non-printable character (like space, tab, and so on)
                            //replace it with a space the size of spaceSize
                            lastChar = new RectangleF(new PointF(lastChar.X + lastChar.Width, lastChar.Y), spaceSize);
                        else
                            lastChar = new RectangleF(new PointF(lastChar.X + lastChar.Width, lastChar.Y), charSizes[i].Size);
                    }

                    e.Graphics.TranslateTransform(lastChar.X + lastChar.Width / 2, lastChar.Y + lastChar.Height / 2);
                    if (_CurrentRotate == 0)
                        e.Graphics.RotateTransform(_ZeroRotationAngle);
                    else
                        e.Graphics.RotateTransform(_CurrentRotate);
                    e.Graphics.ScaleTransform(_CurrentZoom / 100.0f, _CurrentZoom / 100.0f);
                    e.Graphics.DrawString(Text[i].ToString(), Font, new SolidBrush(base.ForeColor), -lastChar.Width / 2.0f, -lastChar.Height / 2.0f, StringFormat.GenericTypographic);
                }
            }
            lock (this)
            {
                InPaint = false;
            }
        }

        /// <summary>
        /// Recalculates size of the control
        /// </summary>
        /// <param name="MaintainCenter">Wheather position of the center should be maintained, otherwise maintains UpperLeft</param>
        private void RecalculateSize(bool MaintainCenter)
        {
            Size oldSize = Size;
            Size newSize;
            Graphics g = this.CreateGraphics();
            if (!_Letterwise)//whole string is rotated
            {
                if (!base.AutoSize)
                    return;//if autosizing is not set, no work needed
                SizeF measured_size = g.MeasureString(Text, Font);
                newSize = new Size(Convert.ToInt32(Math.Ceiling(measured_size.Width + Math.Abs(ShadowOffset.X))),//Math.Abs(ShadowOffset
                    Convert.ToInt32(Math.Ceiling(measured_size.Height + Math.Abs(ShadowOffset.Y))));

                int d = Convert.ToInt32(Math.Ceiling(Math.Sqrt(newSize.Width * newSize.Width + newSize.Height * newSize.Height)));//d=√(w²+h²)
                //size (calculated below) can be vastly improved (currently, it allmost allways oversizes control)
                if (newSize.Width == 0)
                    newSize = new Size(newSize.Height, 1);//must be non-null in order to calcualte arcus tangent
                double d_angle = Math.Atan((double)newSize.Height / newSize.Width);//angle between diagonal and x-axis
                double r_angle = Math.Max(Math.Abs(-d_angle + _MinRotate * Math.PI / 180.0), Math.Abs(d_angle + _MaxRotate * Math.PI / 180.0));//maximum rotational angle
                newSize = new Size(d, Convert.ToInt32(Math.Ceiling(d * Math.Sin(Math.Min(Math.PI / 2, r_angle + d_angle)))));
            }
            else//each character is rotated independently
            {
                SizeF W = g.MeasureString("W", Font);//single character dimensions
                SizeF measured_size = g.MeasureString(Text, Font);
                newSize = new Size(Convert.ToInt32(Math.Ceiling(measured_size.Width + Math.Abs(ShadowOffset.X) + W.Height * 0.5)),
                    Convert.ToInt32(Math.Ceiling(measured_size.Height + Math.Abs(ShadowOffset.Y) + W.Height * 0.5)));

                //calculate size of each character (which is used in OnPaint)
                //do it here once, and use it many times in OnPaint
                SizeF A_A = g.MeasureString("A A", Font, new SizeF(newSize.Width, newSize.Height), StringFormat.GenericTypographic);
                SizeF AA = g.MeasureString("AA", Font, new SizeF(newSize.Width, newSize.Height), StringFormat.GenericTypographic);
                spaceSize = new SizeF(A_A.Width - AA.Width, A_A.Height);

                charSizes = new RectangleF[Text.Length];//allocate it
                CharacterRange[] cr = new CharacterRange[1];
                StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic);
                Region[] r;
                cr[0] = new CharacterRange(0, 1);

                for (int i = 0; i < Text.Length; i++)
                {
                    stringFormat.SetMeasurableCharacterRanges(cr);
                    r = g.MeasureCharacterRanges(Text[i].ToString(), Font, new RectangleF(0, 0, newSize.Width, newSize.Height), stringFormat);
                    charSizes[i] = r[0].GetBounds(g);
                    r[0].Dispose();
                }

                stringFormat.Dispose();
            }
            if (base.AutoSize)
            {
                Size = newSize;
                if (MaintainCenter)
                {
                    Left += (oldSize.Width - newSize.Width) / 2;
                    Top += (oldSize.Height - newSize.Height) / 2;
                }
            }
            g.Dispose();
            Refresh();
        }

        /// <summary>
        /// Determines whether any animation is active, and starts/stops animation timer accordingly
        /// </summary>
        private void RecalculateTimer()
        {
            if (_MinAlpha < _MaxAlpha || _MinRotate < _MaxRotate || _MinZoom < 100)
                aTimer.Start();
            else
                aTimer.Stop();
        }

        /// <summary>
        /// Handles the Tick event of the aTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void aTimer_Tick(object sender, EventArgs e)//animation timer tick
        {
            if (_MinAlpha < _MaxAlpha)//transparency animation enabled?
            {
                if (_CurrentAlpha == _MaxAlpha)
                    _AlphaStep = (sbyte)-Math.Abs(_AlphaStep);
                if (_CurrentAlpha == _MinAlpha)
                    _AlphaStep = (sbyte)Math.Abs(_AlphaStep);
                int sum = (_CurrentAlpha + _AlphaStep);
                sum = sum < _MinAlpha ? _MinAlpha : sum;
                sum = sum > _MaxAlpha ? _MaxAlpha : sum;
                _CurrentAlpha = (byte)sum;
                ForeColor = Color.FromArgb(_CurrentAlpha, ForeColor);
                BackBrush.Color = Color.FromArgb(_CurrentAlpha, BackBrush.Color);
            }
            if (_MinRotate < _MaxRotate)//roatation enabled?
            {
                if (_CurrentRotate == _MaxRotate)
                    _RotateStep = (sbyte)-Math.Abs(_RotateStep);
                if (_CurrentRotate == _MinRotate)
                    _RotateStep = (sbyte)Math.Abs(_RotateStep);
                int sum = (_CurrentRotate + _RotateStep);
                sum = sum < _MinRotate ? _MinRotate : sum;
                sum = sum > _MaxRotate ? _MaxRotate : sum;
                _CurrentRotate = (sbyte)sum;
            }
            if (_MinZoom < 100)//zoom animation enabled?
            {
                if (_CurrentZoom == 100)
                    _ZoomStep = (sbyte)-1;
                if (_CurrentZoom == _MinZoom)
                    _ZoomStep = (sbyte)1;
                _CurrentZoom = (byte)(_CurrentZoom + _ZoomStep);
            }
            Refresh();
        }
        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code
        /// <summary>
        /// a timer
        /// </summary>
        private System.Windows.Forms.Timer aTimer;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.aTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // aTimer
            // 
            this.aTimer.Interval = 20;
            this.aTimer.Tick += new System.EventHandler(this.aTimer_Tick);
            this.ResumeLayout(false);
        }
        #endregion

        #region Getting rid of senseless properties (bobpowell.net)
        /// <summary>
        /// The names to remove
        /// </summary>
        private string[] NamesToRemove =
        {
            "AccesibilityObject",
            "AccessibleDescription",
            "AccessibleName",
            "AllowDrop",
            "Capture",
            "DisplayRectangle",
            "RightToLeft",
            "Region",
            "Tag",
            "UseWaitCursor",
            "ImeMode",
            "Padding"
        };

        //Does the property filtering...
        /// <summary>
        /// Filters the properties.
        /// </summary>
        /// <param name="pdc">The PDC.</param>
        /// <returns>PropertyDescriptorCollection.</returns>
        private PropertyDescriptorCollection
        FilterProperties(PropertyDescriptorCollection pdc)
        {
            ArrayList toRemove = new ArrayList();
            foreach (string s in NamesToRemove)
                toRemove.Add(s);

            PropertyDescriptorCollection adjustedProps = new PropertyDescriptorCollection(new PropertyDescriptor[] { });
            foreach (PropertyDescriptor pd in pdc)
                if (!toRemove.Contains(pd.Name))
                    adjustedProps.Add(pd);

            return adjustedProps;
        }

        #region ICustomTypeDescriptor Members
        /// <summary>
        /// Returns a type converter for this instance of a component.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        /// <summary>
        /// Returns the events for this instance of a component using the specified attribute array as a filter.
        /// </summary>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
        /// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        /// <summary>
        /// Returns the events for this instance of a component.
        /// </summary>
        /// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
        EventDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        /// <summary>
        /// Returns the name of this instance of a component.
        /// </summary>
        /// <returns>The name of the object, or null if the object does not have a name.</returns>
        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        /// <summary>
        /// Returns an object that contains the property described by the specified property descriptor.
        /// </summary>
        /// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
        /// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        /// <summary>
        /// Returns a collection of custom attributes for this instance of a component.
        /// </summary>
        /// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.</returns>
        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }


        /// <summary>
        /// Returns the properties for this instance of a component using the attribute array as a filter.
        /// </summary>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
        /// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.</returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(this, attributes, true);
            return FilterProperties(pdc);
        }

        /// <summary>
        /// Returns the properties for this instance of a component.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.</returns>
        PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
        {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(this, true);
            return FilterProperties(pdc);
        }

        /// <summary>
        /// Returns an editor of the specified type for this instance of a component.
        /// </summary>
        /// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
        /// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        /// <summary>
        /// Returns the default property for this instance of a component.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.</returns>
        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        /// <summary>
        /// Returns the default event for this instance of a component.
        /// </summary>
        /// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.</returns>
        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        /// <summary>
        /// Returns the class name of this instance of a component.
        /// </summary>
        /// <returns>The class name of the object, or null if the class does not have a name.</returns>
        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }
        #endregion

        #endregion
    }

    #endregion

}