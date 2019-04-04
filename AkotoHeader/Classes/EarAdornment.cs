// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="EarAdornment.cs" company="Zeroit Dev Technologies">
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
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Design;

namespace Zeroit.Framework.Labels.Headers
{
    /// <summary>
    /// Enum EarTypes
    /// </summary>
    public enum EarTypes
	{
        /// <summary>
        /// The regular
        /// </summary>
        regular,
        /// <summary>
        /// The ogee
        /// </summary>
        ogee,
        /// <summary>
        /// The concave
        /// </summary>
        concave,
        /// <summary>
        /// The line
        /// </summary>
        line,
        /// <summary>
        /// The rounded
        /// </summary>
        rounded,
        /// <summary>
        /// The stair
        /// </summary>
        stair,
        /// <summary>
        /// The slant
        /// </summary>
        slant,
        /// <summary>
        /// The square
        /// </summary>
        square
    };

    /// <summary>
    /// Summary description for EarAdornment.
    /// </summary>

    public class EarAdornment : Object
	{
        /// <summary>
        /// The service
        /// </summary>
        private IWindowsFormsEditorService service = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EarAdornment"/> class.
        /// </summary>
        /// <param name="et">The et.</param>
        /// <param name="bnds">The BNDS.</param>
        public EarAdornment(EarTypes et, Rectangle bnds)
		{
			_earType = et;
			_earBounds = bnds;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="EarAdornment"/> class.
        /// </summary>
        /// <param name="srv">The SRV.</param>
        public EarAdornment(IWindowsFormsEditorService srv)
		{
			service = srv;
		}

        /// <summary>
        /// The ear type
        /// </summary>
        private EarTypes _earType = EarTypes.regular;
        /// <summary>
        /// Gets or sets the type of the ear.
        /// </summary>
        /// <value>The type of the ear.</value>
        public EarTypes EarType
		{
			get { return _earType; }
			set { _earType = value; }
		}

        /// <summary>
        /// The ear bounds
        /// </summary>
        private Rectangle _earBounds = new Rectangle(0, 0, 0, 0);
        /// <summary>
        /// Gets or sets the ear bounds.
        /// </summary>
        /// <value>The ear bounds.</value>
        public Rectangle EarBounds
		{
			get { return _earBounds; }
			set { _earBounds = value; }
		}

        /// <summary>
        /// Draws the left ear.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="bnds">The BNDS.</param>
        public void DrawLeftEar(ref GraphicsPath gp, Rectangle bnds)
		{
			Point pt1 = new Point(0, 0);
			Point pt2 = new Point(0, 0);

			Rectangle rct = new Rectangle(0, 0, 0, 0);
			rct.Width = bnds.Width / 2;
			rct.Height = bnds.Height / 2;
			
			switch (_earType)
			{
				case EarTypes.regular:
					rct.Width = bnds.Width;
					rct.Height = bnds.Height;
					gp.AddArc(rct, 180, 90);
					break;
				case EarTypes.ogee:
					rct.X = 0;
					rct.Y = rct.Height;
					gp.AddArc(rct, 180, 90);
					rct.X = 0;
					rct.Y = 0;
					gp.AddArc(rct, 0, 90);
					rct.X = rct.Width;
					rct.Y = 0;
					gp.AddArc(rct, 180, 90);
					break;
				case EarTypes.concave:
					gp.AddArc(bnds, 0, 90);
					break;
				case EarTypes.rounded:
					rct.X = 0;
					rct.Y = rct.Height;
					gp.AddArc(rct, 180, 90);
					pt1.X = rct.Width;
					pt1.Y = rct.Height;
					pt2 = pt1;
					pt2.Y = rct.Height;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.X = rct.Width;
					gp.AddLine(pt1, pt2);
					rct.X = rct.Width;
					rct.Y = 0;
					gp.AddArc(rct, 180, 90);
					break;
				case EarTypes.line:
					break;
				case EarTypes.stair:
					pt1.Y = bnds.Height;
					pt2.X = bnds.Width;
					pt2.Y = pt1.Y;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.Y = 0;
					gp.AddLine(pt1, pt2);
					break;
				case EarTypes.slant:
					pt1.Y = bnds.Height;
					pt2.X = bnds.Width;
					pt2.Y = rct.Height;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.Y = 0;
					gp.AddLine(pt1, pt2);
					break;
				case EarTypes.square:
					pt1.Y = rct.Height;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.X = rct.X + rct.Width;
					gp.AddLine(pt1, pt2);
					break;
			}
		}

        /// <summary>
        /// Draws the right ear.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="bnds">The BNDS.</param>
        public void DrawRightEar(ref GraphicsPath gp, Rectangle bnds)
		{
			Rectangle rct = bnds;
			Point pt1 = new Point(0, 0);
			Point pt2 = new Point(0, 0);

			rct.Width = bnds.Width / 2;
			rct.Height = bnds.Height / 2;
			
			switch (_earType)
			{
				case EarTypes.regular:
					rct.Width = bnds.Width;
					rct.Height = bnds.Height;
					gp.AddArc(rct, 270, 90);
					break;
				case EarTypes.ogee:
					gp.AddArc(rct, 270, 90);
					rct.X = bnds.X + rct.Width;
					rct.Y = bnds.Y;
					gp.AddArc(rct, 90, 90);
					rct.X = bnds.X + rct.Width;
					rct.Y = bnds.Y + rct.Height;
					gp.AddArc(rct, 270, 90);
					break;
				case EarTypes.rounded:
					gp.AddArc(rct, 270, 90);
					pt1.X = bnds.X + rct.Width ;
					pt1.Y = bnds.Y + rct.Height;
					pt2 = pt1;
					pt2.Y = bnds.Y + rct.Height;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.X = bnds.X + rct.Width;
					gp.AddLine(pt1, pt2);
					rct.X = bnds.X + rct.Width;
					rct.Y = bnds.Y + rct.Height;
					gp.AddArc(rct, 270, 90);
					break;
				case EarTypes.line:
					break;
				case EarTypes.stair:
					pt1.X = bnds.X;
					pt1.Y = bnds.Y;
					pt2.X = bnds.X;
					pt2.Y = bnds.Y + bnds.Height;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.X = bnds.X + bnds.Width;
					gp.AddLine(pt1, pt2);
					break;
				case EarTypes.slant:
					pt1.X = bnds.X;
					pt2.X = bnds.X;
					pt2.Y = bnds.Y + rct.Height;
					gp.AddLine(pt1, pt2);
					break;
				case EarTypes.concave:
					gp.AddArc(bnds, 90, 90);
					break;
				case EarTypes.square:
					pt1.X = bnds.Width;
					pt2.X = bnds.X + bnds.Width;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.Y = bnds.Y;
					gp.AddLine(pt1, pt2);
					break;
			}
		}

        /// <summary>
        /// Draws the footer left ear.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="bnds">The BNDS.</param>
        public void DrawFooterLeftEar(ref GraphicsPath gp, Rectangle bnds)
		{
			Point pt1 = (Point) bnds.Size;
			Point pt2 = bnds.Location;

			switch (_earType)
			{
				case EarTypes.regular:
					gp.AddArc(bnds, 90, 90);
					break;
				case EarTypes.square:
					pt2.Y = bnds.Y + bnds.Height;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					gp.AddLine(pt1, pt2);
					break;
				case EarTypes.line:
					break;
				default:
					gp.AddArc(bnds, 90, 90);
					break;
			}
		}

        /// <summary>
        /// Draws the footer right ear.
        /// </summary>
        /// <param name="gp">The gp.</param>
        /// <param name="bnds">The BNDS.</param>
        public void DrawFooterRightEar(ref GraphicsPath gp, Rectangle bnds)
		{
			Point pt1 = (Point) bnds.Size;
			Point pt2 = bnds.Location;

			switch (_earType)
			{
				case EarTypes.regular:
					gp.AddArc(bnds, 0, 90);
					break;
				case EarTypes.square:
					pt1.X = bnds.X;
					pt2.X = bnds.X + bnds.Width;
					gp.AddLine(pt1, pt2);
					pt1 = pt2;
					pt2.Y = bnds.Height;
					gp.AddLine(pt1, pt2);
					break;
				case EarTypes.line:
					break;
				default:
					gp.AddArc(bnds, 0, 90);
					break;
			}
		}	
	}
}
