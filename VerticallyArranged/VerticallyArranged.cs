// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="VerticallyArranged.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
#region Imports

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

#endregion

namespace Zeroit.Framework.Labels
{

    #region Vertically arranged text

    /// <summary>
    /// A class collection for rendering vertical string.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    /// <remarks>The ZeroitVerticalString class enables text to be written vertically, while keeping
    /// each character in the string upright, as opposed to just rotating the string
    /// and characters as a whole.</remarks>

    public class ZeroitVerticalString : Label
    {

        /// <summary>
        /// The text to use
        /// </summary>
        private string textToUse = "Vertical String";

        /// <summary>
        /// Gets or sets the text to use.
        /// </summary>
        /// <value>The text to use.</value>
        public string TextToUse
        {
            get { return textToUse; }
            set
            {
                textToUse = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Enum for Extra Space Allowance.
        /// </summary>
        [Description("esa (Extra Space Allowance) enumerator")]
        public enum esaType
        {
            /// <summary>
            /// The pre
            /// </summary>
            Pre,
            /// <summary>
            /// The post
            /// </summary>
            Post,
            /// <summary>
            /// The either
            /// </summary>
            Either
        }

        /// <summary>
        /// The text spread
        /// </summary>
        [Description("TextSpread parameter - controls vertical spacing")]
        private double textSpread;

        /// <summary>
        /// Gets or sets the text spread.
        /// </summary>
        /// <value>The text spread.</value>
        public double TextSpread
        {
            get
            {
                return textSpread;
            }

            set
            {
                textSpread = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitVerticalString" /> class.
        /// </summary>
        [Description("ZeroitVerticalString Constructor")]
        public ZeroitVerticalString()
        {
            textSpread = .75F;
            //AutoSize = false;
        }

        /// <summary>
        /// Draws the specified g.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="stringRect">The string rect.</param>
        [Description("Draw Method Overload 1 - draw string in top left of rectangle." +
                     "Calls Overload 3")]
        public void Draw(Graphics g, string text, Font font, Brush brush, Rectangle stringRect)
        {
            this.Draw(g, text, font, brush,
                      stringRect.X,
                      stringRect.Y);
        }

        /// <summary>
        /// Draws the specified g.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="stringRect">The string rect.</param>
        /// <param name="stringStrFmt">The string string FMT.</param>
        [Description("Draw Method Overload 2 - draw string in rectangle according to Alignment " +
                     "and LineAlignment - Calls Overload 3")]
        public void Draw(Graphics g, string text, Font font, Brush brush,
                         Rectangle stringRect, StringFormat stringStrFmt)
        {
            int horOffset;
            int vertOffset;

            // Set horizontal offset
            switch (stringStrFmt.Alignment)
            {
                case StringAlignment.Center:
                    horOffset = (stringRect.Width / 2) - (int)(font.Size / 2) - 2;
                    break;
                case StringAlignment.Far:
                    horOffset = (stringRect.Width - (int)font.Size - 2);
                    break;
                default:
                    horOffset = 0;
                    break;
            }

            // Set vertical offset

            double textSize = this.Length(text, font);

            switch (stringStrFmt.LineAlignment)
            {
                case StringAlignment.Center:
                    vertOffset = (stringRect.Height / 2) - (int)(textSize / 2);
                    break;
                case StringAlignment.Far:
                    vertOffset = stringRect.Height - (int)textSize - 2;
                    break;
                default:
                    vertOffset = 0;
                    break;
            }

            // Draw the string using the offsets
            this.Draw(g, text, font, brush,
                      stringRect.X + horOffset,
                      stringRect.Y + vertOffset);
        }

        /// <summary>
        /// Draws the specified g.
        /// </summary>
        /// <param name="g">The g.</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        [Description("Draw Method Overload 3 - draw string at coordinates x, y")]
        public void Draw(Graphics g, string text, Font font, Brush brush, int x, int y)
        {
            char[] textChars = text.ToCharArray();              // Put the string into array of chars
            StringFormat charStrFmt = new StringFormat();       // Used to align each char centrally
            charStrFmt.Alignment = StringAlignment.Center;

            // Create a small rectangle for each individual character.
            // This will then be offset according to the values calculated by 
            // the ExtraSpaceAllowance code.
            Rectangle charRect = new Rectangle(x, y, (int)(font.Size * 1.5), font.Height);

            // Loop through each character in the string, draw it in the charRect rectangle,
            // moving charRect down the screen as appropriate.
            for (int i = 0; i < text.Length; i++)
            {
                // Move down by character's height allowance BEFORE writing
                charRect.Offset(0, ExtraSpaceAllowance(esaType.Pre, textChars[i], font));

                // Write the character
                g.DrawString(textChars[i].ToString(), font, brush, charRect, charStrFmt);

                // Move down by standard font height
                charRect.Offset(0, (int)(font.Height * textSpread));

                // Move down by character's "dangle" allowance AFTER writing
                charRect.Offset(0, ExtraSpaceAllowance(esaType.Post, textChars[i], font));
            }
        }

        /// <summary>
        /// Lengthes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <returns>System.Int32.</returns>
        [Description("Length Method - returns vertical length of string")]
        public int Length(string text, Font font)
        {
            char[] textChars = text.ToCharArray();      // Put the string into array of chars
            int len = new int();

            for (int i = 0; i < text.Length; i++)
            {
                len += (int)(font.Height * textSpread);     // Add height of font, times spread factor.
                len += ExtraSpaceAllowance(esaType.Either, textChars[i], font); // Add allowance
            }
            len += 1;                                   // Breathing space...			
            return len;
        }


        /// <summary>
        /// Extras the space allowance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="ch">The ch.</param>
        /// <param name="font">The font.</param>
        /// <returns>System.Int32.</returns>
        private int ExtraSpaceAllowance(esaType type, char ch, Font font)
        {

            if (textSpread >= 1) return 0;              // No action if textSpread 1 or more

            int offset = 0;

            // Do we need to pad BEFORE the next char?
            if (type == esaType.Pre | type == esaType.Either)
            {
                // Does our character appear in the "pre" list? (ie taller than average)
                if (" bdfhijkltABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".IndexOf(ch) > 0)
                {
                    offset += (int)(font.Height * .2);
                }
            }

            // Do we need to pad AFTER the next char?
            if (type == esaType.Post | type == esaType.Either)
            {
                // Does our character appear in the "post" list? 
                // (ie dangles over the bottom of the line)
                if (" gjpqyQ".IndexOf(ch) > 0)
                {
                    offset += (int)(font.Height * .2);
                }
            }

            return offset;
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
            AutoSize = false;
            Text = "";

            Draw(e.Graphics, textToUse, Font, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height));
        }


        #region TextRenderingHint

        #region Add it to OnPaint / Graphics Rendering component

        //e.Graphics.TextRenderingHint = textrendering;
        #endregion

        /// <summary>
        /// The textrendering
        /// </summary>
        TextRenderingHint textrendering = TextRenderingHint.AntiAlias;

        /// <summary>
        /// Gets or sets the text rendering.
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


        #region Smoothing Mode

        /// <summary>
        /// The smoothing
        /// </summary>
        private SmoothingMode smoothing = SmoothingMode.HighQuality;

        /// <summary>
        /// Gets or sets the smoothing.
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



    }

    #endregion

}