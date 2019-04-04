// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="CustomText.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Reflection;



namespace Zeroit.Framework.Labels
{


    #region CustomText
    /// <summary>
    /// Class CustomText.
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.INotifyExtPropertyChanged" />
    /// <seealso cref="System.ICloneable" />
    [System.ComponentModel.Browsable(true)]
    [System.ComponentModel.DesignTimeVisible(true)]
    [System.ComponentModel.DefaultEvent("CustomTextChanged")]
    public class CustomText : INotifyExtPropertyChanged, ICloneable
    {
        /// <summary>
        /// Enum ShowType
        /// </summary>
        public enum ShowType
        {
            /// <summary>
            /// The permanent
            /// </summary>
            Permanent,
            /// <summary>
            /// The on demand
            /// </summary>
            OnDemand
        };

        /// <summary>
        /// Occurs when [ext property changed].
        /// </summary>
        [System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public event ExtPropertyChangedEventHandler ExtPropertyChanged = delegate { };

        /// <summary>
        /// The name
        /// </summary>
        private string _Name;
        /// <summary>
        /// The text
        /// </summary>
        private string _Text;
        /// <summary>
        /// The image
        /// </summary>
        private Image _Image;
        /// <summary>
        /// The text design
        /// </summary>
        private string _TextDesign;
        /// <summary>
        /// The output type
        /// </summary>
        private ShowType _OutputType;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        internal string[] Data { set; get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="CustomText"/> class.
        /// </summary>
        public CustomText()
        {
            this._Name = "";
            this._Text = "";
            this._Image = null;
            this._TextDesign = "<none>";
            this._OutputType = ShowType.Permanent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomText"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        /// <param name="image">The image.</param>
        /// <param name="textFormatName">Name of the text format.</param>
        /// <param name="showType">Type of the show.</param>
        public CustomText(string name, string text, Image image, string textFormatName, ShowType showType)
        {
            this._Name = name;
            this._Text = text;
            this._Image = image;
            this._TextDesign = textFormatName;
            this._OutputType = showType;
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
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [Category("Appearance")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(System.Drawing.Design.UITypeEditor))]
        [Description("Text to display. If it is left blank and Image is null, line will not be shown.")]
        public string Text
        {
            get { return this._Text; }
            set
            {
                if (this._Text != value)
                {
                    this._Text = value;
                    this.ValueChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("Image to display. If it is null and Text is left blank, line will not be shown.")]
        public Image Image
        {
            get { return this._Image; }
            set
            {
                if (this._Image != value)
                {
                    this._Image = value;
                    this.ValueChanged("Image");
                }
            }
        }

        /// <summary>
        /// Gets or sets the text design.
        /// </summary>
        /// <value>The text design.</value>
        [Category("Appearance")]
        [Editor(typeof(TextFormatNameEditor), typeof(UITypeEditor))]
        [DefaultValue(typeof(string))]
        [Description("The Text Format to apply to the text")]
        public string TextDesign
        {
            get { return this._TextDesign; }
            set
            {
                if (this._TextDesign != value)
                {
                    this._TextDesign = value;
                    this.ValueChanged("TextDesign");
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of the output.
        /// </summary>
        /// <value>The type of the output.</value>
        [Category("Appearance")]
        [DefaultValue(ShowType.Permanent)]
        [Description("Determines if text is shown ever or only on Runtime")]
        public ShowType OutputType
        {
            get { return this._OutputType; }
            set
            {
                if (this._OutputType != value)
                {
                    this._OutputType = value;
                    this.ValueChanged("OutputType");
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
            this.OnValueChanged(this, args);
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
            this.OnValueChanged(this, args);
        }

        /// <summary>
        /// Handles the <see cref="E:ValueChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExtPropertyChangedEventArgs"/> instance containing the event data.</param>
        public virtual void OnValueChanged(object sender, ExtPropertyChangedEventArgs e)
        {
            if (ExtPropertyChanged != null)
                ExtPropertyChanged.Invoke(sender, e);
        }
    }

    #region TextFormatNameEditor
    /// <summary>
    /// Class TextFormatNameEditor. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.Drawing.Design.UITypeEditor" />
    public sealed class TextFormatNameEditor : System.Drawing.Design.UITypeEditor
    {
        /// <summary>
        /// The ed SVC
        /// </summary>
        private System.Windows.Forms.Design.IWindowsFormsEditorService edSvc = null;
        /// <summary>
        /// The text format names list
        /// </summary>
        private ListBox TextFormatNamesList;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextFormatNameEditor"/> class.
        /// </summary>
        public TextFormatNameEditor()
        {
            this.TextFormatNamesList = new ListBox();
            this.TextFormatNamesList.BorderStyle = BorderStyle.FixedSingle;
            this.TextFormatNamesList.Size = new Size(80, 150);
            this.TextFormatNamesList.ItemHeight = 5;
            this.TextFormatNamesList.SelectedIndexChanged += new System.EventHandler(this.ObjectList_SelectedIndexChanged);
        }

        /// <summary>
        /// Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
        /// <param name="provider">An <see cref="T:System.IServiceProvider" /> that this editor can use to obtain services.</param>
        /// <param name="value">The object to edit.</param>
        /// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
        public override Object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, Object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                this.edSvc = ((System.Windows.Forms.Design.IWindowsFormsEditorService)(provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService))));
                if (this.edSvc != null)
                {
                    this.TextFormatNamesList.Items.Clear();
                    if (context.Instance is CustomText)
                    {
                        CustomText customtext = (CustomText)context.Instance;
                        this.TextFormatNamesList.Items.AddRange(customtext.Data);
                        if (value != null && value.ToString() != "")
                            this.TextFormatNamesList.SelectedItem = value;
                        this.edSvc.DropDownControl(this.TextFormatNamesList);
                        value = this.TextFormatNamesList.SelectedItem;
                    }
                }
            }
            return value;
        }

        /// <summary>
        /// Gets the editor style used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
        /// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> value that indicates the style of editor used by the <see cref="M:System.Drawing.Design.UITypeEditor.EditValue(System.IServiceProvider,System.Object)" /> method. If the <see cref="T:System.Drawing.Design.UITypeEditor" /> does not support this method, then <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        /// <summary>
        /// Objects the list selected index changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ObjectList_SelectedIndexChanged(Object sender, System.EventArgs e)
        {
            if (this.TextFormatNamesList.SelectedItem == null)
                return;
            this.edSvc.CloseDropDown();
        }
    }
    #endregion

    #region CustomizedTextCollectionEditor
    /// <summary>
    /// Class CustomTextCollectionEditor.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.CollectionEditor" />
    public class CustomTextCollectionEditor : CollectionEditor
    {
        /// <summary>
        /// The mflabel
        /// </summary>
        private ZeroitMultiFormatLabel mflabel;
        /// <summary>
        /// The text format names
        /// </summary>
        private string[] TextFormatNames;
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
        /// Initializes a new instance of the <see cref="CustomTextCollectionEditor"/> class.
        /// </summary>
        /// <param name="type">The type of the collection for this editor to edit.</param>
        public CustomTextCollectionEditor(Type type) : base(type) { }

        /// <summary>
        /// Edits the value of the specified object using the specified service provider and context.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
        /// <param name="provider">A service provider object through which editing services can be obtained.</param>
        /// <param name="value">The object to edit the value of.</param>
        /// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (provider != null && context != null)
            {
                if (context.Instance is ZeroitMultiFormatLabel)
                {
                    this.mflabel = (ZeroitMultiFormatLabel)context.Instance;
                    this.TextFormatNames = mflabel.textFormatNames.ToArray();
                    if (value is List<CustomText>)
                    {
                        List<CustomText> customtexts = (List<CustomText>)value;
                        foreach (CustomText ctext in customtexts)
                        {
                            ctext.Data = this.TextFormatNames;
                            ctext.ExtPropertyChanged -= new ExtPropertyChangedEventHandler(this.mflabel.CustomTextHasChanged);
                            ctext.ExtPropertyChanged += new ExtPropertyChangedEventHandler(this.mflabel.CustomTextHasChanged);
                        }
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
        CustomText ctext = (CustomText)base.CreateInstance(itemType);
        string dText = this.GetDisplayText(ctext);
        dText = dText.Substring(dText.LastIndexOf(".") + 1);
        int counter = 0;
        do { counter++; }
        while (this.mflabel.customTextNames.Contains(dText + counter.ToString()));
        ctext.Name = ctext.Text = dText + counter.ToString();
        ctext.Data = this.TextFormatNames;
        ctext.ExtPropertyChanged += new ExtPropertyChangedEventHandler(this.mflabel.CustomTextHasChanged);
        this.mflabel.AddCustomText(ctext);
        return ctext;
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
                    if (tlpanel.Controls[0] is Button)
                    {
                        Button downButton = tlpanel.Controls[0] as Button;
                        downButton.Click += new EventHandler(this.MoveDown);
                    }
                    if (tlpanel.Controls[7] is Button)
                    {
                        Button upButton = tlpanel.Controls[7] as Button;
                        upButton.Click += new EventHandler(this.MoveUp);
                    }
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
                cForm.Load += new EventHandler(this.mflabel.CustomTextEditorLoad);
                Button Cancel = cForm.CancelButton as Button;
                Cancel.Click += new EventHandler(this.mflabel.CustomTextEditorCancel);
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
                    CustomText ctext = pInfo.GetValue(this.actual, null) as CustomText;
                    this.mflabel.RemoveCustomText(ctext);
                }
                return;
            }
            if (pInfo.GetValue(this.previous, null) is CustomText)
            {
                CustomText ctext = pInfo.GetValue(this.previous, null) as CustomText;
                this.mflabel.RemoveCustomText(ctext);
            }
        }

        /// <summary>
        /// Moves up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MoveUp(object sender, EventArgs e)
        {
            PropertyInfo p = this.actual.GetType().GetProperty("Value");
            if (p.GetValue(this.actual, null) is CustomText)
            {
                CustomText ctext = p.GetValue(this.actual, null) as CustomText;
                this.mflabel.CustomTextmoveUp(ctext);
            }
        }

        /// <summary>
        /// Moves down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MoveDown(object sender, EventArgs e)
        {
            PropertyInfo p = this.actual.GetType().GetProperty("Value");
            if (p.GetValue(this.actual, null) is CustomText)
            {
                CustomText ctext = p.GetValue(this.actual, null) as CustomText;
                this.mflabel.CustomTextmoveDown(ctext);
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
