// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="AkotoFooterControl.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel.Design;
using System.Text;

namespace Zeroit.Framework.Labels.Headers
{

    /// <summary>
    /// Summary description for ZeroitAkotoHeader.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.XPExtendedHeaderBase" />
    /// <seealso cref="Zeroit.Framework.Labels.Headers.AkotoHeader.XPExtendedHeaderBase" />
    [ToolboxItem(true)]
    public class ZeroitAkotoFooter : XPExtendedHeaderBase
    {
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
        /// Creates an instance of the XP Footer Control
        /// </summary>
        public ZeroitAkotoFooter()
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

        }

        #endregion

        /// <summary>
        /// The image that is to be displayed in the right hand
        /// image position.
        /// </summary>
        private Image _collapseExpandImage = null;
        /// <summary>
        /// Gets or sets the collapse expand image.
        /// </summary>
        /// <value>The collapse expand image.</value>
        [
                Category("ZeroitAkotoFooter"),
                DefaultValue(null),
                Description("Footers Collapse Image")
                ]
        public Image CollapseExpandImage
        {
            get { return _collapseExpandImage; }
            set
            {
                _collapseExpandImage = value;
                this.Invalidate();
            }
        }

        #region (Footer) methods and Handlers

        /// <summary>
        /// Docks this footer to the associated control
        /// </summary>
        protected override void DockControl()
        {
            Rectangle coords = WorkingControlOrgCoords;

            coords = WorkingControlOrgCoords;
            coords.Y = WorkingControlOrgCoords.Y + WorkingControlOrgCoords.Height;
            coords.Height = this.Height;

            this.Location = coords.Location;
            this.Size = coords.Size;
        }

        /// <summary>
        /// Just Invalidate client area.  Used during resizing in Design mode.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            this.Invalidate();
        }

        //Called by header control to tell this control that it needs to 
        //	recalculate location and move to it! i.e. When Expanding or 
        //	collapsing!
        /// <summary>
        /// Moves the control.
        /// </summary>
        public void MoveControl()
        {
            if (this._workingControl != null)
                this.Location = new Point(
                    _workingControl.Location.X,
                    _workingControl.Location.Y + _workingControl.Height);
        }

        /// <summary>
        /// Draw this thing
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);

            base.OnPaint(e);

            GraphicsPath gp = new GraphicsPath();
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(this.Width, 0);

            //Line across top
            gp.AddLine(pt1, pt2);

            //Side from top to arc
            pt1 = pt2;
            pt2.Y = this.Height - ArcRadiusRight;
            gp.AddLine(pt1, pt2);

            //Draw lower right arc
            _rightEar.EarBounds = new Rectangle(
                this.Width - ArcRadiusRight,
                this.Height - ArcRadiusRight,
                _arcRadiusRight,
                _arcRadiusRight);
            //TODO Need to check if radius inbounds?????
            _rightEar.DrawFooterRightEar(ref gp, _rightEar.EarBounds);


            //Line between arcs
            pt1.X = this.Width - ArcRadiusRight;
            pt1.Y = this.Height;
            pt2.X = ArcRadiusLeft;
            pt2.Y = this.Height;
            gp.AddLine(pt1, pt2);

            //Draw lower left ear
            _leftEar.EarBounds = new Rectangle(0,
                this.Height - ArcRadiusLeft,
                ArcRadiusLeft,
                ArcRadiusLeft);
            _leftEar.DrawFooterLeftEar(ref gp, _leftEar.EarBounds);

            //Line from arc to top
            pt1.X = 0;
            pt1.Y = this.Height - ArcRadiusLeft;
            pt2.X = 0;
            pt2.Y = 0;
            gp.AddLine(pt1, pt2);

            gp.CloseAllFigures();

            Region rgn = new Region(gp);
            this.Region = rgn;

            //Gradiate background
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
        /// Draws the image as required
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        protected override void DrawCollapseImage(PaintEventArgs e)
        {
            if (_collapseExpandImage == null)
                return;

            Rectangle rct = new Rectangle(this.Width - _collapseExpandImage.Width - IMAGE_OFFSET_X,
                (this.Height - _collapseExpandImage.Height) / 2,
                _collapseExpandImage.Width,
                _collapseExpandImage.Height);

            e.Graphics.DrawImage(_collapseExpandImage, rct);
        }

        #endregion
    }



    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(XPFooterControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class XPFooterControlDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class XPFooterControlDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new XPFooterControlSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Zeroit Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }
        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class XPFooterControlSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class XPFooterControlSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitAkotoFooter colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="XPFooterControlSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public XPFooterControlSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitAkotoFooter;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }



        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));
            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));
            items.Add(new DesignerActionPropertyItem("Color1_inactive",
                                 "Color1 inactive", "Appearance",
                                 "Type few characters to filter Cities."));

            items.Add(new DesignerActionPropertyItem("Color2_inactive",
                                 "Color2 inactive", "Appearance",
                                 "Type few characters to filter Cities."));

            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion

}
