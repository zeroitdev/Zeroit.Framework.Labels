// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="TextFormat.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;


namespace Zeroit.Framework.Labels
{

    #region TextFormat
    /// <summary>
    /// Class TextFormat.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.INotifyExtPropertyChanged" />
    /// <seealso cref="System.ICloneable" />
    [System.ComponentModel.Browsable(true)]
[System.ComponentModel.DesignTimeVisible(true)]
public class TextFormat : INotifyExtPropertyChanged, ICloneable
{
        /// <summary>
        /// The name
        /// </summary>
        private string _Name;
        /// <summary>
        /// The border style
        /// </summary>
        private BorderStyle _BorderStyle;
        /// <summary>
        /// The back color
        /// </summary>
        private Color _BackColor;
        /// <summary>
        /// The fore color
        /// </summary>
        private Color _ForeColor;
        /// <summary>
        /// The text align
        /// </summary>
        private ContentAlignment _TextAlign;
        /// <summary>
        /// The image align
        /// </summary>
        private ContentAlignment _ImageAlign;
        /// <summary>
        /// The text font
        /// </summary>
        private Font _TextFont;
        /// <summary>
        /// The padding
        /// </summary>
        private Padding _Padding;

        /// <summary>
        /// Occurs when [ext property changed].
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public event ExtPropertyChangedEventHandler ExtPropertyChanged = delegate { };

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFormat"/> class.
        /// </summary>
        public TextFormat()
        {
            this.ForeColor = Color.Black;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Padding = new Padding(0);
            this.Name = "";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFormat"/> class.
        /// </summary>
        /// <param name="Fcolor">The fcolor.</param>
        /// <param name="font">The font.</param>
        /// <param name="Bcolor">The bcolor.</param>
        /// <param name="borderStyle">The border style.</param>
        /// <param name="textAlign">The text align.</param>
        /// <param name="imageAlign">The image align.</param>
        /// <param name="padding">The padding.</param>
        /// <param name="name">The name.</param>
        public TextFormat(Color Fcolor, Font font, Color Bcolor, BorderStyle borderStyle, ContentAlignment textAlign, ContentAlignment imageAlign, Padding padding, string name)
        {
            this.ForeColor = Fcolor;
            this.Font = font;
            this.TextBackColor = Bcolor;
            this.BorderStyle = borderStyle;
            this.TextAlign = textAlign;
            this.ImageAlign = imageAlign;
            this.Padding = padding;
            this.Name = name;
        }

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Category("Appearance")]
        public string Name
        {
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    string temp = this._Name;
                    this._Name = value;
                    this.ValueChanged("Name", temp, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the border style.
        /// </summary>
        /// <value>The border style.</value>
        [Category("Appearance")]
        [DefaultValue(BorderStyle.None)]
        [Description("The BorderStyle for the CustomText")]
        public BorderStyle BorderStyle
        {
            get { return this._BorderStyle; }
            set
            {
                if (this._BorderStyle != value)
                {
                    this._BorderStyle = value;
                    this.ValueChanged("BorderStyle");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the text back.
        /// </summary>
        /// <value>The color of the text back.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Transparent")]
        [Description("The BackColor for the CustomText")]
        public Color TextBackColor
        {
            get { return this._BackColor; }
            set
            {
                if (this._BackColor != value)
                {
                    this._BackColor = value;
                    this.ValueChanged("TextBackColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        [Category("Appearance")]
        [DefaultValue(typeof(Color), "Black")]
        public Color ForeColor
        {
            get { return this._ForeColor; }
            set
            {
                if (this._ForeColor != value)
                {
                    this._ForeColor = value;
                    this.ValueChanged("ForeColor");
                }
            }
        }

        /// <summary>
        /// Gets or sets the text align.
        /// </summary>
        /// <value>The text align.</value>
        [Category("Appearance")]
        [DefaultValue(ContentAlignment.TopLeft)]
        public ContentAlignment TextAlign
        {
            get { return this._TextAlign; }
            set
            {
                if (this._TextAlign != value)
                {
                    this._TextAlign = value;
                    this.ValueChanged("TextAlign");
                }
            }
        }

        /// <summary>
        /// Gets or sets the image align.
        /// </summary>
        /// <value>The image align.</value>
        [Category("Appearance")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment ImageAlign
        {
            get { return this._ImageAlign; }
            set
            {
                if (this._ImageAlign != value)
                {
                    this._ImageAlign = value;
                    this.ValueChanged("ImageAlign");
                }
            }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        [Category("Appearance")]
        public Font Font
        {
            get { return this._TextFont; }
            set
            {
                if (this._TextFont != value)
                {
                    this._TextFont = value;
                    this.ValueChanged("Font");
                }
            }
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        [Category("Appearance")]
        [Description("The Padding for the CustomText")]
        public Padding Padding
        {
            get { return this._Padding; }
            set
            {
                if (this._Padding != value)
                {
                    this._Padding = value;
                    this.ValueChanged("Padding");
                }
            }
        }
        #endregion

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Values the changed.
        /// </summary>
        /// <param name="PropName">Name of the property.</param>
        private void ValueChanged(string PropName)
        {
            ExtPropertyChangedEventArgs args = new ExtPropertyChangedEventArgs(PropName);
            this.OnValueChanged(args);
        }

        /// <summary>
        /// Values the changed.
        /// </summary>
        /// <param name="PropName">Name of the property.</param>
        /// <param name="OldValue">The old value.</param>
        /// <param name="NewValue">The new value.</param>
        private void ValueChanged(string PropName, string OldValue, string NewValue)
        {
            ExtPropertyChangedEventArgs args = new ExtPropertyChangedEventArgs(PropName, OldValue, NewValue);
            this.OnValueChanged(args);
        }

        /// <summary>
        /// Handles the <see cref="E:ValueChanged" /> event.
        /// </summary>
        /// <param name="args">The <see cref="ExtPropertyChangedEventArgs"/> instance containing the event data.</param>
        protected void OnValueChanged(ExtPropertyChangedEventArgs args)
        {
            if (ExtPropertyChanged != null)
                ExtPropertyChanged.Invoke(this, args);
        }

    }

    #region TextFormatCollectionEditor
    /// <summary>
    /// Class TextFormatCollectionEditor. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.CollectionEditor" />
    public sealed class TextFormatCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// The mflabel
        /// </summary>
        private ZeroitMultiFormatLabel mflabel;
        /// <summary>
        /// The list box
        /// </summary>
        private ListBox listBox;
        /// <summary>
        /// The previous
        /// </summary>
        private object previous = null;
        /// <summary>
        /// The actual
        /// </summary>
        private object actual = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFormatCollectionEditor"/> class.
        /// </summary>
        /// <param name="type">The type of the collection for this editor to edit.</param>
        public TextFormatCollectionEditor(Type type) : base(type) { }

        /// <summary>
        /// Edits the value of the specified object using the specified service provider and context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
        /// <param name="provider">A service provider object through which editing services can be obtained.</param>
        /// <param name="value">The object to edit the value of.</param>
        /// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
        public override Object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, Object value)
        {
            if (provider != null && context != null)
            {
                if (context.Instance is ZeroitMultiFormatLabel)
                    this.mflabel = (ZeroitMultiFormatLabel)context.Instance;
                if (value is List<TextFormat>)
                {
                    List<TextFormat> textformats = (List<TextFormat>)value;
                    foreach (TextFormat textF in textformats)
                    {
                        textF.ExtPropertyChanged -= new ExtPropertyChangedEventHandler(this.mflabel.TextFormatHasChanged);
                        textF.ExtPropertyChanged += new ExtPropertyChangedEventHandler(this.mflabel.TextFormatHasChanged);
                    }
                }
            }
            return base.EditValue(context, provider, value);
        }

        /// <summary>
        /// Creates a new instance of the specified collection item type.
        /// </summary>
        /// <param name="itemType">The type of item to create.</param>
        /// <returns>A new instance of the specified object.</returns>
        protected override object CreateInstance(Type itemType)
        {
            TextFormat textF = (TextFormat)base.CreateInstance(itemType);
            string dText = this.GetDisplayText(textF);
            dText = dText.Substring(dText.LastIndexOf(".") + 1);
            int counter = 0;
            do { counter++; }
            while (this.mflabel.textFormatNames.Contains(dText + counter.ToString()));
            textF.Name = dText + counter.ToString();
            textF.ExtPropertyChanged += new ExtPropertyChangedEventHandler(this.mflabel.TextFormatHasChanged);
            this.mflabel.AddTextFormat(textF.Name);
            return textF;
        }

        /// <summary>
        /// Creates a new form to display and edit the current collection.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.CollectionEditor.CollectionForm" /> to provide as the user interface for editing the collection.</returns>
        protected override CollectionForm CreateCollectionForm()
        {
            CollectionForm collectionForm = base.CreateCollectionForm();
            Form cForm = collectionForm as Form;
            if (cForm != null)
            {
                if (cForm.Controls[0] is TableLayoutPanel)
                {
                    TableLayoutPanel tlpanel = cForm.Controls[0] as TableLayoutPanel;
                    if (tlpanel.Controls[1] is TableLayoutPanel)
                    {
                        TableLayoutPanel tlpanel1 = tlpanel.Controls[1] as TableLayoutPanel;
                        if (tlpanel1.Controls[1] is Button)
                            (tlpanel1.Controls[1] as Button).Click += new EventHandler(this.deleteItem);
                    }
                    if (tlpanel.Controls[4] is ListBox)
                    {
                        this.listBox = tlpanel.Controls[4] as ListBox;
                        listBox.SelectedIndexChanged += listBox_SelectedIndexChanged;
                    }
                    if (tlpanel.Controls[5] is PropertyGrid)
                    {
                        PropertyGrid pGrid = tlpanel.Controls[5] as PropertyGrid;
                        pGrid.HelpVisible = true;
                    }
                }
                cForm.Load += new EventHandler(this.mflabel.TextFormatEditorLoad);
                Button Cancel = cForm.CancelButton as Button;
                Cancel.Click += new EventHandler(this.mflabel.TextFormatEditorCancel);
            }
            return collectionForm; 
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void deleteItem(object sender, EventArgs e)
        {
            PropertyInfo pInfo = this.previous.GetType().GetProperty("Value");
            if (pInfo == null || this.listBox.SelectedItem == null)
            {
                pInfo = this.actual.GetType().GetProperty("Value");
                if (pInfo != null)
                {
                    TextFormat textF = pInfo.GetValue(this.actual, null) as TextFormat;
                    this.mflabel.RemoveTextFormat(textF.Name);
                }
                return;
            }
            if (pInfo.GetValue(this.previous, null) is TextFormat)
            {
                TextFormat textF = pInfo.GetValue(this.previous, null) as TextFormat;
                this.mflabel.RemoveTextFormat(textF.Name);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the listBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBox.SelectedItem != null)
            {
                this.previous = this.actual;
                this.actual = this.listBox.SelectedItem;
            }
        }

        /// <summary>
        /// Indicates whether multiple collection items can be selected at once.
        /// </summary>
        /// <returns>true if it multiple collection members can be selected at the same time; otherwise, false. By default, this returns true.</returns>
        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }
    }
    #endregion
    #endregion

}
