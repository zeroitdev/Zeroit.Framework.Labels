// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="TransparentLabel.cs" company="Zeroit Dev Technologies">
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
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{


    #region ZeroitTransparentLabel
    /// <summary>
    /// A class collection for rendering a transparent label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    public class ZeroitTransparentLabel : System.Windows.Forms.Control
    {
        /// <summary>
        /// The text align
        /// </summary>
        System.Drawing.ContentAlignment _textAlign = System.Drawing.ContentAlignment.TopLeft;
        /// <summary>
        /// The draw format
        /// </summary>
        StringFormat _drawFormat = new StringFormat();
        /// <summary>
        /// The text rect
        /// </summary>
        Rectangle _textRect = new Rectangle(0, 0, 0, 0);

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitTransparentLabel" /> class.
        /// </summary>
        public ZeroitTransparentLabel()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            SetStyle(ControlStyles.Opaque, false);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Invalidates the ex.
        /// </summary>
        protected virtual void InvalidateEx()
        {
            if (null == this.Parent) return;
            System.Drawing.Rectangle dirtyRect = new Rectangle(this.Location, this.Size);
            this.Parent.Invalidate(dirtyRect, true);
        }

        /// <summary>
        /// Resets the align.
        /// </summary>
        private void ResetAlign()
        {
            switch (_textAlign)
            {
                case System.Drawing.ContentAlignment.BottomLeft:
                case System.Drawing.ContentAlignment.MiddleLeft:
                case System.Drawing.ContentAlignment.TopLeft:
                    _drawFormat.Alignment = StringAlignment.Near;
                    break;
                case System.Drawing.ContentAlignment.BottomCenter:
                case System.Drawing.ContentAlignment.MiddleCenter:
                case System.Drawing.ContentAlignment.TopCenter:
                    _drawFormat.Alignment = StringAlignment.Center;
                    break;
                case System.Drawing.ContentAlignment.BottomRight:
                case System.Drawing.ContentAlignment.MiddleRight:
                case System.Drawing.ContentAlignment.TopRight:
                    _drawFormat.Alignment = StringAlignment.Far;
                    break;
            }
        }

        /// <summary>
        /// Resets the rect.
        /// </summary>
        private void ResetRect()
        {
            Graphics g = this.CreateGraphics();
            SizeF textSize = g.MeasureString(base.Text, this.Font);
            switch (_textAlign)
            {
                case System.Drawing.ContentAlignment.BottomLeft:
                case System.Drawing.ContentAlignment.BottomCenter:
                case System.Drawing.ContentAlignment.BottomRight:
                    _textRect = new Rectangle(0, this.ClientRectangle.Height - (int)textSize.Height, this.ClientRectangle.Width, (int)textSize.Height);
                    break;
                case System.Drawing.ContentAlignment.MiddleLeft:
                case System.Drawing.ContentAlignment.MiddleCenter:
                case System.Drawing.ContentAlignment.MiddleRight:
                    _textRect = new Rectangle(0, ((this.ClientRectangle.Height - (int)textSize.Height) / 2), this.ClientRectangle.Width, (int)textSize.Height);
                    break;
                case System.Drawing.ContentAlignment.TopLeft:
                case System.Drawing.ContentAlignment.TopCenter:
                case System.Drawing.ContentAlignment.TopRight:
                    _textRect = new Rectangle(0, 0, this.ClientRectangle.Width, (int)textSize.Height);
                    break;
            }
            g.Dispose();
        }

        #region O V E R R I D E S
        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        /// <value>The create parameters.</value>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT
                return (cp);
            }
        }

        /// <summary>
        /// Called after the control has been added to another container.
        /// </summary>
        protected override void InitLayout()
        {
            base.InitLayout();
            ResetAlign();
            ResetRect();
            // This get's removed immediatly upon the first invocation.
            this.Parent.Paint += new PaintEventHandler(Parent_Paint);
        }
        //Must be stubbed out. If you implement this you will loose your transparent background.
        /// <summary>
        /// Paints the background of the control.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint.</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //do not allow the background to be painted  
            //Debug.WriteLine( "ZeroitTransparentLabel(" + this.Name + ")::OnPaintBackground" ) ;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ResetRect();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Color fg = this.ForeColor;
            if (!Enabled) fg = System.Drawing.SystemColors.GrayText;
            SolidBrush drawBrush = new SolidBrush(fg);
            g.DrawString(base.Text, this.Font, drawBrush, _textRect, _drawFormat);
            drawBrush.Dispose();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            this.InvalidateEx();
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            ResetRect();
            this.InvalidateEx();
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            this.InvalidateEx();
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            this.InvalidateEx();
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            ResetRect();
            this.InvalidateEx();
            this.Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            this.InvalidateEx();
            this.Invalidate();
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
                _drawFormat.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // ZeroitTransparentLabel
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "ZeroitTransparentLabel";
            this.Size = new System.Drawing.Size(192, 16);

        }
        #endregion

        #region P R O P E R T I E S
        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        [Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; this.InvalidateEx(); }
        }

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        [Browsable(true),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
        DefaultValue(System.Drawing.ContentAlignment.TopLeft)]
        public System.Drawing.ContentAlignment TextAlign
        {
            get { return (_textAlign); }
            set
            {
                _textAlign = value;
                this.InvalidateEx();
                ResetAlign();
                ResetRect();
            }
        }
        #endregion

        #region E V E N T   H A N D L E R S
        /// <summary>
        /// Handles the Paint event of the Parent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void Parent_Paint(object sender, PaintEventArgs e)
        {
            this.Invalidate();
            // This get's removed immediatly upon the first invocation, 
            // becuase it is just a cludge to stop the text from disapearing when
            // you drop a TranparentLable onto a container.
            // If you don't remove it you get way-to-many paints.
            this.Parent.Paint -= new PaintEventHandler(Parent_Paint);
        }
        #endregion

    }
    
    
    #endregion

}