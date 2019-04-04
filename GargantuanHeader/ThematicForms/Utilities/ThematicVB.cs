using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using Zeroit.Framework.FormThemes.UIThemes;

namespace Zeroit.Framework.Form.UIThemes
{
    public class ThematicVB : Theme
    {

        #region Private Fields

        public enum themes 
        {
            BetaBlue
        }

        private themes _themes = themes.BetaBlue;

        #endregion

        #region Public Properties

        public themes Themes
        {
            get { return _themes; }
            set
            {
                _themes = value;
                Invalidate();
            }
        }

        #endregion

        public override void PaintHook()
        {
            switch (_themes)
            {
                case themes.BetaBlue:
                    G.Clear(Color.FromKnownColor(KnownColor.Control));
                    // Clear the form first
                    //DrawGradient(Color.FromArgb(0, 105, 246), Color.FromArgb(0, 81, 181), 0, 0, Width, Height, 90S)   ' Form Gradient
                    G.Clear(Color.FromArgb(0, 95, 218));
                    DrawGradient(Color.FromArgb(0, 95, 218), Color.FromArgb(0, 55, 202), 0, 0, Width, 25, 90);
                    // Form Top Bar

                    DrawCorners(Color.Fuchsia, ClientRectangle);
                    // Then draw some clean corners
                    DrawBorders(Pens.DarkBlue, Pens.DodgerBlue, ClientRectangle);
                    // Then we draw our form borders

                    G.DrawLine(Pens.Black, 0, 25, Width, 25);
                    // Top Line
                    //G.DrawLine(Pens.Black, 0, Height - 25, Width, Height - 25)   ' Bottom Line

                    DrawText(HorizontalAlignment.Left, Color.White, 8, 2);
                    // Finally, we draw our text
                    break;
            }
            
        }
    }
}
