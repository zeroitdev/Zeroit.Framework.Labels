// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="UltraTextBox.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Zeroit.Framework.Labels
{


    /// <summary>
    /// A class collection for rendering a placeholder text.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("TextChanged")]
    [Designer(typeof(UltraTextBoxDesigner))]
    public class ZeroitUltraTextBox : Control
    {

        #region  Variables

        /// <summary>
        /// The ultra tb
        /// </summary>
        TextBox UltraTB = new TextBox();
        /// <summary>
        /// The aln type
        /// </summary>
        HorizontalAlignment ALNType;

        /// <summary>
        /// The maxchars
        /// </summary>
        int maxchars = 32767;
        /// <summary>
        /// The read only
        /// </summary>
        bool readOnly;
        /// <summary>
        /// The previous read only
        /// </summary>
        bool previousReadOnly;
        /// <summary>
        /// The multiline
        /// </summary>
        bool multiline;
        /// <summary>
        /// The is password masked
        /// </summary>
        bool isPasswordMasked = false;
        /// <summary>
        /// The enable
        /// </summary>
        bool Enable = true;

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

        //Color fontColor = ColorTranslator.FromHtml("#999999");
        /// <summary>
        /// The focus color
        /// </summary>
        Color focusColor = Color.FromArgb(80, 142, 245);

        /// <summary>
        /// The enabled focused color
        /// </summary>
        Color EnabledFocusedColor;
        /// <summary>
        /// The enabled string color
        /// </summary>
        Color EnabledStringColor;

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
        /// The text background color
        /// </summary>
        private Color textBackgroundColor;

        /// <summary>
        /// The place holder label
        /// </summary>
        private Label PlaceHolderLabel = new Label();
        /// <summary>
        /// The place holdetext
        /// </summary>
        private string placeHoldetext = "PlaceHolderText Here";

        /// <summary>
        /// The placeholder
        /// </summary>
        private bool placeholder = true;
        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        public HorizontalAlignment TextAlignment
        {
            get
            {
                return ALNType;
            }
            set
            {
                ALNType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        [Category("Behavior")]
        public int MaxLength
        {
            get
            {
                return maxchars;
            }
            set
            {
                maxchars = value;
                UltraTB.MaxLength = MaxLength;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitUltraTextBox"/> is multiline.
        /// </summary>
        /// <value><c>true</c> if multiline; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool Multiline
        {
            get
            {
                return multiline;
            }
            set
            {
                multiline = value;
                if (UltraTB != null)
                {
                    UltraTB.Multiline = value;

                    if (value)
                    {
                        UltraTB.Location = new Point(-3, 1);
                        UltraTB.Width = Width + 3;
                        UltraTB.Height = Height - 6;
                    }
                    else
                    {
                        UltraTB.Location = new Point(0, 1);
                        UltraTB.Width = Width;
                        Height = 24;
                    }
                }
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [use system password character].
        /// </summary>
        /// <value><c>true</c> if [use system password character]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return isPasswordMasked;
            }
            set
            {
                UltraTB.UseSystemPasswordChar = UseSystemPasswordChar;
                isPasswordMasked = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value><c>true</c> if [read only]; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
                if (UltraTB != null)
                {
                    UltraTB.ReadOnly = value;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool IsEnabled
        {
            get { return Enable; }
            set
            {
                Enable = value;

                if (IsEnabled)
                {
                    readOnly = previousReadOnly;
                    UltraTB.ReadOnly = previousReadOnly;
                    UltraTB.ForeColor = EnabledStringColor;
                }
                else
                {
                    previousReadOnly = ReadOnly;
                    ReadOnly = true;
                    UltraTB.ForeColor = disabledStringColor;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the focused.
        /// </summary>
        /// <value>The color of the focused.</value>
        [Category("Appearance")]
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
        [Browsable(true)]
        public bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(true)]
        public Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                UltraTB.Font = value;
                PlaceHolderLabel.Font = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        [Browsable(true)]
        public Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the place holder text.
        /// </summary>
        /// <value>The color of the place holder text.</value>
        public Color PlaceHolderTextColor
        {
            get
            {
                return PlaceHolderLabel.ForeColor;
            }
            set
            {
                PlaceHolderLabel.ForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the text background.
        /// </summary>
        /// <value>The color of the text background.</value>
        public Color TextBackgroundColor
        {
            get { return textBackgroundColor; }
            set
            {
                textBackgroundColor = value;
                UltraTB.BackColor = value;
                PlaceHolderLabel.BackColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the password character.
        /// </summary>
        /// <value>The password character.</value>
        public char PasswordChar
        {
            get { return UltraTB.PasswordChar; }
            set
            {
                
                UltraTB.PasswordChar = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [word wrap].
        /// </summary>
        /// <value><c>true</c> if [word wrap]; otherwise, <c>false</c>.</value>
        public bool WordWrap
        {
            get { return UltraTB.WordWrap; }
            set
            {
                UltraTB.WordWrap = value;
                Invalidate();
            }
        }

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
        /// Gets or sets a value indicating whether this <see cref="ZeroitUltraTextBox"/> is placeholder.
        /// </summary>
        /// <value><c>true</c> if placeholder; otherwise, <c>false</c>.</value>
        public bool Placeholder
        {
            get { return placeholder; }
            set {

                if (value)
                {
                    UltraTB.Text = "";
                    PlaceHolderLabel.Visible = true;
                }
                else
                {
                    UltraTB.Text = Text;
                    PlaceHolderLabel.Visible = false;
                }

                placeholder = value;

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the place holder text.
        /// </summary>
        /// <value>The place holder text.</value>
        public string PlaceHolderText
        {
            get { return placeHoldetext; }
            set
            {
                placeHoldetext = value;
                PlaceHolderLabel.Text = value;
                Invalidate();
            }
        }

        #endregion

        #region  Events

        /// <summary>
        /// Handles the <see cref="E:KeyDown" /> event.
        /// </summary>
        /// <param name="Obj">The object.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        protected void OnKeyDown(object Obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                UltraTB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                UltraTB.Copy();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                UltraTB.Cut();
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            UltraTB.Focus();
            UltraTB.SelectionLength = 0;
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

            UltraTB.Width = Width;
            
            if (multiline)
            {
                UltraTB.Location = new Point(-3, 1);
                UltraTB.Width = Width + 3;
                UltraTB.Height = Height - 6;
            }
            else
            {
                UltraTB.Location = new Point(0, 1);
                UltraTB.Width = Width;
                Height = 24;
            }
        }

        #endregion

        /// <summary>
        /// Adds the text box.
        /// </summary>
        private void AddTextBox()
        {
            textBackgroundColor = BackColor;

            UltraTB.Location = new Point(0, 1);
            UltraTB.Size = new Size(Width, 20);
            UltraTB.Text = Text;

            UltraTB.BorderStyle = BorderStyle.None;
            UltraTB.TextAlign = HorizontalAlignment.Left;
            UltraTB.Font =  Font;
            UltraTB.UseSystemPasswordChar = UseSystemPasswordChar;
            UltraTB.Multiline = false;
            UltraTB.BackColor = BackColor;
            UltraTB.ScrollBars = ScrollBars.None;
            UltraTB.KeyDown += OnKeyDown;

            UltraTB.GotFocus += (sender, args) => Focus = true; AnimationTimer.Start();
            UltraTB.LostFocus += (sender, args) => Focus = false; AnimationTimer.Start();

            
        }

        /// <summary>
        /// Adds the place holder text.
        /// </summary>
        private void AddPlaceHolderText()
        {
            PlaceHolderLabel.BackColor = TextBackgroundColor;
            PlaceHolderLabel.Location = new Point(1, 0);
            PlaceHolderLabel.Size = new Size(Width - 2, Height-6);
            PlaceHolderLabel.ForeColor = Color.FromArgb(153, 153, 153);
            PlaceHolderLabel.Text = placeHoldetext;
            PlaceHolderLabel.Font = Font;
            PlaceHolderLabel.Click += PlaceHolderLabel_Click;
            PlaceHolderLabel.MouseEnter += PlaceHolderLabel_MouseEnter;

            if (Placeholder)
            {
                PlaceHolderLabel.Visible = false;
            }

            else
            {
                PlaceHolderLabel.Visible = true;
            }

        }

        /// <summary>
        /// Handles the MouseEnter event of the PlaceHolderLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PlaceHolderLabel_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.IBeam;
        }

        /// <summary>
        /// Handles the Click event of the PlaceHolderLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PlaceHolderLabel_Click(object sender, EventArgs e)
        {
            UltraTB.Focus();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitUltraTextBox" /> class.
        /// </summary>
        public ZeroitUltraTextBox()
        {
            Width = 300;
            DoubleBuffered = true;
            previousReadOnly = ReadOnly;

            AddTextBox();
            AddPlaceHolderText();
            Controls.Add(PlaceHolderLabel);
            Controls.Add(UltraTB);

            Font = new Font("Segoe UI", 11);
            //UltraTB.TextChanged += (sender, args) => Text = UltraTB.Text;
            UltraTB.TextChanged += UltraTB_TextChanged;
            base.TextChanged += (sender, args) => UltraTB.Text = Text;

            AnimationTimer.Tick += new EventHandler(AnimationTick);

            ForeColor = Color.FromArgb(153, 153, 153);
            
        }

        /// <summary>
        /// Handles the TextChanged event of the UltraTB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UltraTB_TextChanged(object sender, EventArgs e)
        {
            Text = UltraTB.Text;
            if (UltraTB.Text.Length <= 0)
            {
                Placeholder = true;
                //label1.Text = "Type your details here";
            }
            else
            {
                Placeholder = false;
                //label1.Text = string.Empty;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.Clear(BackColor);

            EnabledStringColor = ForeColor;
            EnabledFocusedColor = focusColor;

            UltraTB.TextAlign = TextAlignment;
            UltraTB.ForeColor = IsEnabled ? EnabledStringColor : disabledStringColor;
            UltraTB.UseSystemPasswordChar = UseSystemPasswordChar;

            G.DrawLine(new Pen(new SolidBrush(IsEnabled ? enabledUnFocusedColor : disabledUnFocusedColor)), new Point(0, Height - 2), new Point(Width, Height - 2));

            if (IsEnabled)
            {
                G.FillRectangle(new SolidBrush(EnabledFocusedColor), PointAnimation, (float)Height - 3, SizeAnimation, 2);
                
            }

            
            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);

            G.Dispose();
            B.Dispose();
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
        
    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class UltraTextBoxDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class UltraTextBoxDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new UltraTextBoxSmartTagActionList(this.Component));
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
    /// Class UltraTextBoxSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class UltraTextBoxSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitUltraTextBox colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="UltraTextBoxSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public UltraTextBoxSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitUltraTextBox;

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

        /// <summary>
        /// Gets or sets the color of the text background.
        /// </summary>
        /// <value>The color of the text background.</value>
        public Color TextBackgroundColor
        {
            get
            {
                return colUserControl.TextBackgroundColor;
            }
            set
            {
                GetPropertyByName("TextBackgroundColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the place holder text.
        /// </summary>
        /// <value>The color of the place holder text.</value>
        public Color PlaceHolderTextColor
        {
            get
            {
                return colUserControl.PlaceHolderTextColor;
            }
            set
            {
                GetPropertyByName("PlaceHolderTextColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public new string Text
        {
            get { return colUserControl.Text; }
            set
            {
                GetPropertyByName("Text").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UltraTextBoxSmartTagActionList"/> is placeholder.
        /// </summary>
        /// <value><c>true</c> if placeholder; otherwise, <c>false</c>.</value>
        public bool Placeholder
        {
            get { return colUserControl.Placeholder; }
            set
            {
                GetPropertyByName("Placeholder").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the place holder text.
        /// </summary>
        /// <value>The place holder text.</value>
        public string PlaceHolderText
        {
            get { return colUserControl.PlaceHolderText; }
            set
            {
                GetPropertyByName("PlaceHolderText").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("Placeholder",
                "Place Holder", "Appearance",
                "Set to enable placeholder."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("PlaceHolderTextColor",
                                "Place Holder Text Color", "Appearance",
                                "Sets the placeholder text color."));

            items.Add(new DesignerActionPropertyItem("TextBackgroundColor",
                                "Text Background", "Appearance",
                                "Sets the text background."));

            items.Add(new DesignerActionPropertyItem("PlaceHolderText",
                                 "Place Holder Text", "Appearance",
                                 "Sets the place holder text."));

            items.Add(new DesignerActionPropertyItem("Text",
                                "Text", "Appearance",
                                "Sets the text."));


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
