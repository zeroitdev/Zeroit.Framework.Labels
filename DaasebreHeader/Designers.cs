// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="Designers.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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
