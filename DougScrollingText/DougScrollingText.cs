// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="DougScrollingText.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region ZeroitMaxScrollText

    /// <summary>
    /// A class collection for rendering a scrolling text.
    /// <para> Care should be taken as there are performance issues.</para>
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    
    public class ZeroitMaxScrollText : System.Windows.Forms.Control
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        /// <summary>
        /// The m color1
        /// </summary>
        private Color m_Color1 = Color.Black;  // First default color.
        /// <summary>
        /// The m color2
        /// </summary>
        private Color m_Color2 = Color.Gold;   // Second default color.
        /// <summary>
        /// The m my font
        /// </summary>
        private Font m_MyFont;   // For the font.
        /// <summary>
        /// The timer interval
        /// </summary>
        private int timerInterval = 250;

        /// <summary>
        /// The m timer
        /// </summary>
        protected System.Windows.Forms.Timer m_Timer = new System.Windows.Forms.Timer();             // Timer for text animation.
        /// <summary>
        /// The s scroll text
        /// </summary>
        protected string sScrollText = null;   // Text to be displayed in the control.

        /// <summary>
        /// Initializes a new instance of the <c><see cref="ZeroitMaxScrollText" /> </c> class.
        /// </summary>

        /*//////////////////////////////////////////////////////////////////////////////
        //
        // Function: public ZeroitMaxScrollText()
        //
        // By: Doug 
        //
        // Date: 2/27/03
        //
        // Description: Constructor.
        //
        /////////////////////////////////////////////////////////////////////////////*/
        //
        public ZeroitMaxScrollText()
        {

            // Set the timer speed and properties.
            m_Timer.Interval = timerInterval;
            m_Timer.Enabled = true;
            m_Timer.Tick += new EventHandler(Animate);
        }


        /// <summary>
        /// Gets or sets the timer interval.
        /// </summary>
        /// <value>The timer interval.</value>
        /// <remarks>For either increasing/decreasing the speed of the animation</remarks>
        public int TimerInterval
        {
            get { return timerInterval; }
            set
            {
                timerInterval = value;
                if(value > 0)
                {
                    m_Timer.Interval = value;
                }
                Invalidate();
            }
        }

        // Add a color property.
        /// <summary>
        /// Gets or sets the doug scrolling text color.
        /// </summary>
        /// <value>The doug scrolling text color1.</value>
        public Color DougScrollingTextColor1
        {
            get { return m_Color1; }
            set
            {
                m_Color1 = value;
                Invalidate();
            }
        }

        // Add a color property.
        /// <summary>
        /// Gets or sets the doug scrolling text color.
        /// </summary>
        /// <value>The doug scrolling text color2.</value>
        public Color DougScrollingTextColor2
        {
            get { return m_Color2; }
            set
            {
                m_Color2 = value;
                Invalidate();
            }
        }

        /*//////////////////////////////////////////////////////////////////////////////
        //
        // Function: Animate( object sender, EventArgs e )
        //
        // By: Doug 
        //
        // Date: 2/27/03
        //
        // Description: Sets up the animation of the text.
        //
        /////////////////////////////////////////////////////////////////////////////*/
        //
        /// <summary>
        /// Animates the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void Animate(object sender, EventArgs e)
        {
            // sScrollText string is from the Text property, add 4 spaces after the string so everything is not bunche together.
            if (sScrollText == null)
            {
                sScrollText = Text + "    ";
            }

            // Scroll text by triming one character at a time from the left, then adding that character to the right side of the control to make it look like scrolling text.
            sScrollText = sScrollText.Substring(1, sScrollText.Length - 1) + 
                sScrollText.Substring(0, 1);

            // Call Invalidate() to tell the windows form that our control needs to be repainted.
            Invalidate();
        }

        /*//////////////////////////////////////////////////////////////////////////////
        //
        // Function: StartStop( object sender, EventArgs e )
        //
        // By: Doug 
        //
        // Date: 2/27/03
        //
        // Description: Start and stop the timer.
        //
        /////////////////////////////////////////////////////////////////////////////*/
        //
        /// <summary>
        /// Starts the stop.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void StartStop(object sender, EventArgs e)
        {
            m_Timer.Enabled = !m_Timer.Enabled;
        }

        /*//////////////////////////////////////////////////////////////////////////////
        //
        // Function: protected override void OnTextChanged( EventArgs e )
        //
        // By: Doug 
        //
        // Date: 2/27/03
        //
        // Description: If/when the string text is changed, I need to update the sScrollText string.
        //
        /////////////////////////////////////////////////////////////////////////////*/
        //
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            sScrollText = null;

            base.OnTextChanged(e);
        }

        /*//////////////////////////////////////////////////////////////////////////////
        //
        // Function: protected override void OnClick( EventArgs e )
        //
        // By: Doug 
        //
        // Date: 2/27/03
        //
        // Description: Handle the click event of the ZeroitMaxScrollText.
        //
        /////////////////////////////////////////////////////////////////////////////*/
        //
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClick(EventArgs e)
        {
            m_Timer.Enabled = !m_Timer.Enabled;

            base.OnClick(e);
        }

        /*public override void Dispose()
        {
            // Since the m_Timer hasn't been added to a collection (because
            // we don’t have one!) we have to dispose it manually.
            m_Timer.Dispose();
            base.Dispose();
        }*/




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
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="pe">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs pe)
        {

            TransInPaint(pe.Graphics);
            // This is a fancy brush that draws graded colors.
            Brush MyBrush = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, m_Color1, m_Color2, 10);

            // Get the font and use it to draw text in the control.  Resize to the height of the control if possible.
            m_MyFont = new Font(Font.Name, (Height * 3) / 4, Font.Style, GraphicsUnit.Pixel);

            // Draw the text string in the control.
            pe.Graphics.DrawString(sScrollText, m_MyFont, MyBrush, ClientRectangle);

            base.OnPaint(pe);

            // Clean up variables..
            MyBrush.Dispose();
            m_MyFont.Dispose();
        }
    }

    #endregion

}