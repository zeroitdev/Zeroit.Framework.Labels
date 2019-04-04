// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="AkotoHeaderControl.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace Zeroit.Framework.Labels.Headers
{
    /// <summary>
    /// Summary description for ZeroitAkotoHeader.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.XPExtendedHeaderBase" />
    [DefaultEvent("OnExpandCollapseClick") ]
	[ToolboxItem(true)]
    public class ZeroitAkotoHeader : XPExtendedHeaderBase
	{
        /// <summary>
        /// Creates an instance of the Header Control
        /// </summary>
		public ZeroitAkotoHeader()
		{
			InitializeComponent();
		}

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			this.tmrCollapse = new System.Timers.Timer();
			this.tmrExpand = new System.Timers.Timer();
			((System.ComponentModel.ISupportInitialize)(this.tmrCollapse)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.tmrExpand)).BeginInit();
			// 
			// tmrCollapse
			// 
			this.tmrCollapse.Interval = 30;
			this.tmrCollapse.SynchronizingObject = this;
			this.tmrCollapse.Elapsed += new System.Timers.ElapsedEventHandler(this.tmrCollapse_Elapsed);
			// 
			// tmrExpand
			// 
			this.tmrExpand.Interval = 30;
			this.tmrExpand.SynchronizingObject = this;
			this.tmrExpand.Elapsed += new System.Timers.ElapsedEventHandler(this.tmrExpand_Elapsed);
			((System.ComponentModel.ISupportInitialize)(this.tmrCollapse)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.tmrExpand)).EndInit();

		}

        #endregion

        #region (Header) Properties, Variables and such

        //Current image being displayed
        /// <summary>
        /// The display image
        /// </summary>
        private Image _displayImage = null;

        /// <summary>
        /// The image displayed when the control is expanded
        /// </summary>
        private Image _expandImage = null;
        /// <summary>
        /// Gets or sets the expanded image.
        /// </summary>
        /// <value>The expanded image.</value>
        [
        Category("ZeroitAkotoHeader"),
		DefaultValue(null)
		]
		public Image ExpandedImage
		{
			get { return _expandImage; }
			set
			{
				_expandImage = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// The image displayed when the control is collapsed
        /// </summary>
        private Image _collapseImage = null;
        /// <summary>
        /// Gets or sets the collapsed image.
        /// </summary>
        /// <value>The collapsed image.</value>
        [
        Category("ZeroitAkotoHeader"),
		DefaultValue(null)
		]
		public Image CollapsedImage
		{
			get { return _collapseImage; }
			set
			{
				_collapseImage = value;
				this.Invalidate();
			}
		}

        /// <summary>
        /// The footer control associated with this control
        /// </summary>
        private ZeroitAkotoFooter _footerControl = null;
        /// <summary>
        /// Gets or sets the footer control.
        /// </summary>
        /// <value>The footer control.</value>
        [
        Category("ZeroitAkotoHeader"),
		DefaultValue(null)
		]
		public ZeroitAkotoFooter FooterControl
		{
			get { return _footerControl; }
			set { _footerControl = value; }
		}

        /// <summary>
        /// Does the user wish to use animation when expanding/collapsing
        /// the control.
        /// </summary>
        private bool _animate = true;
        /// <summary>
        /// Gets or sets a value indicating whether [animate header].
        /// </summary>
        /// <value><c>true</c> if [animate header]; otherwise, <c>false</c>.</value>
        [
        Category("ZeroitAkotoHeader")
		]
		public bool AnimateHeader
		{ 
			get { return _animate; }
			set { _animate = value; }
		}

        #endregion

        #region (Animation) Properties, Variables and such

        /// <summary>
        /// The TMR collapse
        /// </summary>
        private System.Timers.Timer tmrCollapse;
        /// <summary>
        /// The TMR expand
        /// </summary>
        private System.Timers.Timer tmrExpand;

        //Increment defines the number of pixels to span at each tick
        /// <summary>
        /// The default increment
        /// </summary>
        private const int DEFAULT_INCREMENT = 40;
        //Frequency is the time between ticks in milliseconds
        /// <summary>
        /// The default frequency
        /// </summary>
        private const int DEFAULT_FREQUENCY = 30;

        /// <summary>
        /// The count
        /// </summary>
        private int _cnt = 0;
        /// <summary>
        /// The maximum count
        /// </summary>
        private int _maxCnt = 0;
        /// <summary>
        /// The is animated
        /// </summary>
        private bool isAnimated = false;

        /// <summary>
        /// The increment
        /// </summary>
        private int _increment = DEFAULT_INCREMENT;

        #endregion

        #region (Header) Delegates, Events and such

        /// <summary>
        /// Invoked when the header is clicked
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public delegate void ExpandCollapseClickEventHandler(object sender, EventArgs e);
        /// <summary>
        /// Occurs when [on expand collapse click].
        /// </summary>
        [
        Category("XPControl"),
		Description("Fired when Header clicked")
		]
		public event ExpandCollapseClickEventHandler OnExpandCollapseClick;

        #endregion

        #region (Header) methods and Handlers

        /// <summary>
        /// Docks this header to a windows control taking on its
        /// width in the process.
        /// </summary>
        protected override void DockControl()
		{
			Rectangle coords = WorkingControlOrgCoords;

			coords.X = _workingControl.Location.X;
			coords.Y = _workingControl.Location.Y - this.Height;
			coords.Width = _workingControl.Width;
			coords.Height = this.Height;

			this.Location = coords.Location;
			this.Size = coords.Size;
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.Invalidate();
		}

        /// <summary>
        /// Draw this thang
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

			base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

			GraphicsPath gp = new GraphicsPath();
			Point pt1 = new Point(ArcRadiusLeft, 0);
			Point pt2 = new Point(0, 0);

			//Top line between ears
			pt2.X = this.Width - _arcRadiusRight;
			gp.AddLine(pt1, pt2);

			//Draw upper right arc
			_rightEar.EarBounds = new Rectangle(this.Width - ArcRadiusRight,
				0,
				ArcRadiusRight, 
				ArcRadiusRight);
			_rightEar.DrawRightEar(ref gp, _rightEar.EarBounds);

			//Right side from arc to bottom
			pt1.X = this.Width;
			pt1.Y = ArcRadiusRight;
			pt2.X = this.Width;
			pt2.Y = this.Height;
			gp.AddLine(pt1, pt2);

			//Across bottom
			pt1 = pt2;
			pt2.X = 0;
			gp.AddLine(pt1, pt2);

			//Up left side to arc
			pt1 = pt2;
			pt2.Y = ArcRadiusLeft;
			gp.AddLine(pt1, pt2);

			//Draw upper left arc
			_leftEar.EarBounds = new Rectangle(0, 0,ArcRadiusLeft, ArcRadiusLeft);
			_leftEar.DrawLeftEar(ref gp, _leftEar.EarBounds);

			gp.CloseAllFigures();

			Region rgn = new Region(gp);
			this.Region = rgn;

			LinearGradientBrush lgb = new LinearGradientBrush(
				this.DisplayRectangle, 
				this._bckgndColor1,
				this._bckgndColor2,
				_gradientAngle,
				true);

			if (_twistColors)
				lgb.SetBlendTriangularShape(.5f, .75f);

			e.Graphics.FillRegion(lgb, rgn);

			rgn.Dispose();
			lgb.Dispose();

			DrawBorderStyle(e, gp);
			DrawImage(e);
			DrawCollapseImage(e);
			DrawText(e);
		}

        /// <summary>
        /// The collapse expand offset x
        /// </summary>
        private const int COLLAPSE_EXPAND_OFFSET_X = 22;
        /// <summary>
        /// The collapse expand offset y
        /// </summary>
        private const int COLLAPSE_EXPAND_OFFSET_Y = 5;
        /// <summary>
        /// Draw the expand/collapse image
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void DrawCollapseImage(PaintEventArgs e)
		{
			//I did this here because in constructor the values for _expandImage &
			//	_collapse image are not yet defined!
			if (isExpanded)
				_displayImage = _collapseImage;
			else
				_displayImage = _expandImage;

			//Finally I check to make sure that images are defined else get out!
			if (_displayImage == null)
				return;

			Rectangle rct = new Rectangle(
				this.Width - this._displayImage.Width - COLLAPSE_EXPAND_OFFSET_X,
				this.Height - _displayImage.Height - COLLAPSE_EXPAND_OFFSET_Y, 
				this._displayImage.Width,
				this._displayImage.Height);

			//This color matrix is set up to set the transparency of the image to 50%.
			float [][] pts =
			{
				new float [] {1, 0, 0, 0, 0},
				new float [] {0, 1, 0, 0, 0},
				new float [] {0, 0, 1, 0, 0},
				new float [] {0, 0, 0, .5f, 0},	//The ,5f here means 50%
				new float [] {0, 0, 0, 0, 1}
			};
			ColorMatrix cm = new ColorMatrix(pts);
			ImageAttributes attr = new ImageAttributes();
			//This effectively dims the image by setting the transparency level of the image to 50%.
			attr.SetColorMatrix(cm, ColorMatrixFlag.Default);

			//If selected this applies, or not, the color matrix to the image
			if (_isSelected)
				e.Graphics.DrawImage(_displayImage, rct, 0, 0, _displayImage.Width, _displayImage.Height, GraphicsUnit.Pixel, attr);
			else
				e.Graphics.DrawImage(_displayImage, rct);
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			//Set flag indicating that we are over the header, selected!
			_isSelected = true;

			this.Invalidate();
		}

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			//Set flag indicating that we are over the header, selected!
			_isSelected = false;
		
			this.Invalidate();
		}

        /// <summary>
        /// The is expanded
        /// </summary>
        private bool isExpanded = true;
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			//First see if we have a control associated with this control
			if (_workingControl != null)
			{
				//If the control is expanded them we change to display the proper image
				//	then we collapse it.
				//else
				//	we expand it
				if (isExpanded)
				{
					_displayImage = _expandImage;
					CollapseControl();
				}
				else
				{
					ExpandControl();
					_displayImage = _collapseImage;
				}
				//isAnimated is used to tell whether we are in the process of
				//	expansion/collapsion. (um new word?)
				isAnimated = true;
			}

			//Let the parent know that a click has taken place
			if (OnExpandCollapseClick != null)
				OnExpandCollapseClick(this, new EventArgs());
		}

        #endregion

        #region (Animation) Methods and Handlers

        /// <summary>
        /// Expands the control.
        /// </summary>
        public void ExpandControl()
		{
			//If we are to animate
			//	Start animation else just pull it up all at once
			if (_animate)
			{
				//If we're not animating yet set the count to zero
				if (!isAnimated)
					_cnt = 0;
				
				_maxCnt = this.WorkingControlOrgCoords.Height;

				tmrCollapse.Stop();
				tmrExpand.Start();
			}
			else
			{
				//Get the associated controls height here to preserve for later.
				_workingControl.Height = WorkingControlOrgCoords.Height;

				//If we have a footer associated with this control then tell it to Move
				if (_footerControl != null)
					_footerControl.MoveControl();
			}
			isExpanded = true;
		}

        /// <summary>
        /// Collapses the control
        /// </summary>
        public void CollapseControl()
		{
			if (_animate)
			{
				if (!isAnimated)
					_cnt =  WorkingControlOrgCoords.Height;

				tmrExpand.Stop();
				tmrCollapse.Start();
			}
			else
			{
				_workingControl.Height = 0;
				if (_footerControl != null)
					_footerControl.MoveControl();
			}
			isExpanded = false;
		}

        /// <summary>
        /// Timer tick elapsed handler for Expanding
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void tmrExpand_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			//If no associated control just ignore.  Shouldn't get here if no control
			//	but better safe than sorry!
			if (_workingControl == null)
				return;

			_cnt += _increment;
			if (_cnt > _maxCnt)
			{
				tmrExpand.Stop();
				isAnimated = false;
				_workingControl.Height = WorkingControlOrgCoords.Height;
			}
			else
				_workingControl.Height = _cnt;

			//Tells the footer that it needs to move
			if (_footerControl != null)
				_footerControl.MoveControl();
		}

        /// <summary>
        /// Timer tick elapsed handler for Collapsing
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private void tmrCollapse_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (_workingControl == null)
				return;

			_cnt -= _increment;
			if (_cnt < 0)
			{
				tmrCollapse.Stop();
				isAnimated = false;
				_workingControl.Height = 0;
			}
			else
				_workingControl.Height = _cnt;
	
			//Tells the footer that it needs to move
			if (_footerControl != null)
				_footerControl.MoveControl();
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




    }
}
