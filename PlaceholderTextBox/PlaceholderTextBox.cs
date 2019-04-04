// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="PlaceholderTextBox.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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

    #region PlaceHolder TextBox

    /// <summary>
    /// A class collection for rendering placeholder text.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.TextBox" />
    public class ZeroitPlaceHolderTextBox : TextBox
    {
        /// <summary>
        /// The old font
        /// </summary>
        private Font oldFont = null;
        /// <summary>
        /// The water mark text enabled
        /// </summary>
        private Boolean waterMarkTextEnabled = false;

        #region Attributes
        /// <summary>
        /// The water mark color
        /// </summary>
        private Color _waterMarkColor = Color.Gray;
        /// <summary>
        /// Gets or sets the color of the place holder.
        /// </summary>
        /// <value>The color of the place holder.</value>
        public Color PlaceHolderColor
        {
            get { return _waterMarkColor; }
            set { _waterMarkColor = value; Invalidate();/*thanks to Bernhard Elbl for Invalidate()*/ }
        }

        /// <summary>
        /// The water mark text
        /// </summary>
        private string _waterMarkText = "Placeholder Text Here";
        /// <summary>
        /// Gets or sets the place holder text.
        /// </summary>
        /// <value>The place holder text.</value>
        public string PlaceHolderText
        {
            get { return _waterMarkText; }
            set { _waterMarkText = value; Invalidate(); }
        }
        #endregion

        //Default constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitPlaceHolderTextBox" /> class.
        /// </summary>
        public ZeroitPlaceHolderTextBox()
        {
            JoinEvents(true);


            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            
            GetEvents();

            BackColor = Color.Transparent;
        }

        //Override OnCreateControl ... thanks to  "lpgray .. codeproject guy"
        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            WaterMark_Toggel(null, null);
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




        //Override OnPaint
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            //g.Clear(BackColor);

            if (BorderStyle == BorderStyle.FixedSingle)
            {
                g.DrawRectangle(new Pen(Color.FromArgb(100, 100, 100)), new Rectangle(0,0, Width-1, Height-1));
            }
            if (BorderStyle == BorderStyle.None)
            {
                g.DrawRectangle(new Pen(Parent.BackColor), new Rectangle(0, 0, Width - 1, Height - 1));
            }
            // Use the same font that was defined in base class
            //System.Drawing.Font drawFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);
            
            //Create new brush with gray color or 
            SolidBrush drawBrush = new SolidBrush(PlaceHolderColor);//use Water mark color
            //Draw Text or WaterMark

            switch (BorderStyle)
            {
                case BorderStyle.None:
                    e.Graphics.DrawString((waterMarkTextEnabled ? PlaceHolderText : Text), Font, drawBrush, new PointF(0.0F, 0.0F));

                    break;
                case BorderStyle.FixedSingle:
                    e.Graphics.DrawString((waterMarkTextEnabled ? PlaceHolderText : Text), Font, drawBrush, new PointF(0.0F, 2.0F));

                    break;
                case BorderStyle.Fixed3D:
                    e.Graphics.DrawString((waterMarkTextEnabled ? PlaceHolderText : Text), Font, drawBrush, new PointF(0.0F, 2.0F));

                    break;
                
            }
            
            PaintUnderLine(e);

            base.OnPaint(e);
        }


        #region DrawLine

        /// <summary>
        /// The animation timer
        /// </summary>
        Timer AnimationTimer = new Timer { Interval = 1 };

        /// <summary>
        /// The focus
        /// </summary>
        bool Focus = false;

        /// <summary>
        /// The size animation
        /// </summary>
        float SizeAnimation = 0;
        /// <summary>
        /// The size inc decimal
        /// </summary>
        float SizeInc_Dec;

        /// <summary>
        /// The point animation
        /// </summary>
        float PointAnimation;
        /// <summary>
        /// The point inc decimal
        /// </summary>
        float PointInc_Dec;
        /// <summary>
        /// The focus color
        /// </summary>
        Color focusColor = Color.FromArgb(80, 142, 245);


        /// <summary>
        /// The enabled un focused color
        /// </summary>
        Color enabledUnFocusedColor = Color.FromArgb(219, 219, 219);

        /// <summary>
        /// The disabled un focused color
        /// </summary>
        Color disabledUnFocusedColor = Color.FromArgb(233, 236, 238);
        /// <summary>
        /// The disabled string color
        /// </summary>
        Color disabledStringColor = Color.FromArgb(186, 187, 189);

        /// <summary>
        /// Gets or sets the color of the enabled un focused.
        /// </summary>
        /// <value>The color of the enabled un focused.</value>
        public Color EnabledUnFocusedColor
        {
            get { return enabledUnFocusedColor; }
            set { enabledUnFocusedColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the disabled un focused.
        /// </summary>
        /// <value>The color of the disabled un focused.</value>
        public Color DisabledUnFocusedColor
        {
            get { return disabledUnFocusedColor; }
            set { disabledUnFocusedColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the disabled string.
        /// </summary>
        /// <value>The color of the disabled string.</value>
        public Color DisabledStringColor
        {
            get { return disabledStringColor; }
            set { disabledStringColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the focused.
        /// </summary>
        /// <value>The color of the focused.</value>
        public Color FocusedColor
        {
            get { return focusColor; }
            set
            {
                focusColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public new bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;

                //if (base.Enabled)
                //{
                //    //readOnly = previousReadOnly;
                //    //this.ReadOnly = previousReadOnly;
                //    //this.ForeColor = EnabledStringColor;
                //}
                //else
                //{
                //    previousReadOnly = ReadOnly;
                //    ReadOnly = true;
                //    this.ForeColor = disabledStringColor;
                //}

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border type of the text box control.
        /// </summary>
        /// <value>The border style.</value>
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set
            {
                base.BorderStyle = value;

                if (value == BorderStyle.None)
                {
                    Height += 3;
                }
                
                Invalidate();
            }
        }

        /// <summary>
        /// Paints the under line.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void PaintUnderLine(PaintEventArgs e)
        {
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;

            if (BorderStyle == BorderStyle.Fixed3D)
            {
                G.DrawLine(new Pen(new SolidBrush(Enabled ? enabledUnFocusedColor : disabledUnFocusedColor)), new Point(0, Height - 2), new Point(Width, Height - 2));

            }
            else if (BorderStyle == BorderStyle.Fixed3D)
            {
                G.DrawLine(new Pen(new SolidBrush(Enabled ? enabledUnFocusedColor : disabledUnFocusedColor)), new Point(0, Height - 2), new Point(Width, Height - 2));

            }

            else
            {
                G.DrawLine(new Pen(new SolidBrush(Enabled ? enabledUnFocusedColor : disabledUnFocusedColor)), new Point(0, Height - 1), new Point(Width, Height - 1));

            }
            
            if (Enabled)
            {
                G.FillRectangle(new SolidBrush(focusColor), PointAnimation, (float)Height - 3, SizeAnimation, 2);

            }

        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            PointAnimation = Width / 2;
            SizeInc_Dec = Width / 18;
            PointInc_Dec = Width / 36;

            
            //LollipopTB.Width = Width;


            //if (multiline)
            //{
            //    this.Location = new Point(-3, 1);
            //    this.Width = Width + 3;
            //    this.Height = Height - 6;
            //}
            //else
            //{
            //    this.Location = new Point(0, 1);
            //    this.Width = Width;
            //    Height = 24;
            //}
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            this.Focus();
            SelectionLength = 0;

            Focus = true;
            AnimationTimer.Start();
        }

        /// <summary>
        /// Gets the events.
        /// </summary>
        private void GetEvents()
        {
            this.GotFocus += (sender, args) => Focus = true; AnimationTimer.Start();
            this.LostFocus += (sender, args) => Focus = false; AnimationTimer.Start();

            AnimationTimer.Tick += new EventHandler(AnimationTick);

        }

        /// <summary>
        /// Animations the tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void AnimationTick(object sender, EventArgs e)
        {
            if (Focus)
            {
                if (SizeAnimation < Width)
                {
                    SizeAnimation += SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation > 0)
                {
                    PointAnimation -= PointInc_Dec;
                    this.Invalidate();
                }
            }
            else
            {
                if (SizeAnimation > 0)
                {
                    SizeAnimation -= SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation < Width / 2)
                {
                    PointAnimation += PointInc_Dec;
                    this.Invalidate();
                }
            }


        }

        /// <summary>
        /// Handles the <see cref="E:TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Handles the <see cref="E:KeyDown" /> event.
        /// </summary>
        /// <param name="Obj">The object.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        protected void OnKeyDown(object Obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                this.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                this.Copy();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                this.Cut();
                e.SuppressKeyPress = true;
            }
        }
        #endregion


        /// <summary>
        /// Joins the events.
        /// </summary>
        /// <param name="join">The join.</param>
        private void JoinEvents(Boolean join)
        {
            if (join)
            {
                this.TextChanged += new System.EventHandler(this.WaterMark_Toggel);
                this.LostFocus += new System.EventHandler(this.WaterMark_Toggel);
                this.FontChanged += new System.EventHandler(this.WaterMark_FontChanged);
                //No one of the above events will start immeddiatlly 
                //TextBox control still in constructing, so,
                //Font object (for example) couldn't be catched from within WaterMark_Toggle
                //So, call WaterMark_Toggel through OnCreateControl after TextBox is totally created
                //No doupt, it will be only one time call

                //Old solution uses Timer.Tick event to check Create property
                
            }
        }



        /// <summary>
        /// Handles the Toggel event of the WaterMark control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WaterMark_Toggel(object sender, EventArgs args)
        {
            if (this.Text.Length <= 0)
                EnableWaterMark();
            else
                DisableWaterMark();
        }

        /// <summary>
        /// Enables the water mark.
        /// </summary>
        private void EnableWaterMark()
        {
            //Save current font until returning the UserPaint style to false (NOTE: It is a try and error advice)
            oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);
            //Enable OnPaint event handler
            this.SetStyle(ControlStyles.UserPaint, true);
            this.waterMarkTextEnabled = true;
            //Triger OnPaint immediatly
            Refresh();
        }

        /// <summary>
        /// Disables the water mark.
        /// </summary>
        private void DisableWaterMark()
        {
            //Disbale OnPaint event handler
            this.waterMarkTextEnabled = false;
            this.SetStyle(ControlStyles.UserPaint, false);
            //Return back oldFont if existed
            if (oldFont != null)
                this.Font = new System.Drawing.Font(oldFont.FontFamily, oldFont.Size, oldFont.Style, oldFont.Unit);
        }

        /// <summary>
        /// Handles the FontChanged event of the WaterMark control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void WaterMark_FontChanged(object sender, EventArgs args)
        {
            if (waterMarkTextEnabled)
            {
                oldFont = new System.Drawing.Font(Font.FontFamily, Font.Size, Font.Style, Font.Unit);
                Refresh();
            }
        }
        
    }

    #endregion

}
