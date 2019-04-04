// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="Designers.cs" company="Zeroit Dev Technologies">
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

using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Zeroit.Framework.Labels.Headers
{
    /// <summary>
    /// HeaderDesigner class
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    public class HeaderDesigner : ControlDesigner
	{
        /// <summary>
        /// Construction
        /// </summary>
        public HeaderDesigner()
		{
		}

        /// <summary>
        /// Overrides
        /// </summary>
        /// <value>The associated components.</value>
        public override ICollection AssociatedComponents
		{
			get 
			{  
				ZeroitColumnHeader header = base.Control as ZeroitColumnHeader;
				if ( header != null )
				return header.Sections; 

				return base.AssociatedComponents; 
			}
		}

        /// <summary>
        /// Posts the filter properties.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties) 
		{
			//Properties.Remove("BackgroundImage");
			//Properties.Remove("BackColor");
			//Properties.Remove("ForeColor");
			//Properties.Remove("Cursor");
			Properties.Remove("Text");
			Properties.Remove("TabStop");
		}

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="message">The message.</param>
        protected override void WndProc(ref Message message)
		{
			if ( message.Msg == NativeHeader.WM_NOTIFY + NativeHeader.OCM__BASE )
			{
				NativeWindowCommon.NMHDR nmhdr = 
					(NativeWindowCommon.NMHDR)message.GetLParam(typeof(NativeWindowCommon.NMHDR));

				if ( nmhdr.code == NativeHeader.HDN_ENDTRACK )
				{
					IComponentChangeService service 
						= (IComponentChangeService)GetService(typeof(IComponentChangeService));

					service.OnComponentChanged(this.Component, null, null, null);					
				}
			}

			base.WndProc(ref message);
		}

        /// <summary>
        /// Indicates whether a mouse click at the specified point should be handled by the control.
        /// </summary>
        /// <param name="point">A <see cref="T:System.Drawing.Point" /> indicating the position at which the mouse was clicked, in screen coordinates.</param>
        /// <returns>true if a click at the specified point is to be handled by the control; otherwise, false.</returns>
        protected override bool GetHitTest(Point point)
		{ 
			ZeroitColumnHeader.HitTestArea fDirectEdit = ZeroitColumnHeader.HitTestArea.OnDivider|
											 ZeroitColumnHeader.HitTestArea.OnDividerOpen;

			ZeroitColumnHeader header = (ZeroitColumnHeader)this.Component;
			Point ptClient = this.Control.PointToClient(point);

			ZeroitColumnHeader.HitTestInfo hti = header.HitTest(point);

			return (hti.fArea & fDirectEdit) != 0;
		}

	}
}
