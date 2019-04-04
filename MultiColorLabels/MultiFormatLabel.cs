// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="MultiFormatLabel.cs" company="Zeroit Dev Technologies">
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
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Zeroit.Framework.Labels
{
    #region ZeroitMultiFormatLabel
    /// <summary>
    /// A class collection for rendering Multi-format label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [ToolboxBitmap(typeof(ZeroitMultiFormatLabel),"ZeroitMultiFormatLabel")]
    public partial class ZeroitMultiFormatLabel : UserControl
    {
        #region private & internal variables
        /// <summary>
        /// The separator
        /// </summary>
        private string[] separator = new string[] { "\r\n" };
        /// <summary>
        /// The CRLF
        /// </summary>
        private string CRLF = "\r\n";
        /// <summary>
        /// The main text
        /// </summary>
        private string _MainText;
        /// <summary>
        /// The text lines
        /// </summary>
        private string[] TextLines;
        /// <summary>
        /// The image
        /// </summary>
        private Image _Image;
        /// <summary>
        /// The main text align
        /// </summary>
        private ContentAlignment _MainTextAlign;
        /// <summary>
        /// The image align
        /// </summary>
        private ContentAlignment _ImageAlign;
        /// <summary>
        /// The main text back color
        /// </summary>
        private Color _MainTextBackColor;
        /// <summary>
        /// The main text border style
        /// </summary>
        private BorderStyle _MainTextBorderStyle;
        /// <summary>
        /// The main text padding
        /// </summary>
        private Padding _MainTextPadding;
        /// <summary>
        /// The default format
        /// </summary>
        private TextFormat defaultFormat;
        /// <summary>
        /// The main format
        /// </summary>
        private TextFormat MainFormat;
        /// <summary>
        /// The customized text formats
        /// </summary>
        private List<TextFormat> _customizedTextFormats;
        /// <summary>
        /// The text formats cop
        /// </summary>
        private List<TextFormat> textFormatsCop;
        /// <summary>
        /// The custom lines
        /// </summary>
        private List<CustomText> _customLines;
        /// <summary>
        /// The custom lines cop
        /// </summary>
        private List<CustomText> customLinesCop;
        /// <summary>
        /// The text format names
        /// </summary>
        internal List<string> textFormatNames;
        /// <summary>
        /// The text format names cop
        /// </summary>
        internal List<string> textFormatNamesCop;
        /// <summary>
        /// The custom text names
        /// </summary>
        internal List<string> customTextNames;
        /// <summary>
        /// The custom text names cop
        /// </summary>
        internal List<string> customTextNamesCop;
        /// <summary>
        /// The automatic scroll
        /// </summary>
        private bool _automaticScroll;
        /// <summary>
        /// The main label
        /// </summary>
        private Label MainLabel;
        /// <summary>
        /// The timer
        /// </summary>
        private Timer timer = new Timer();
        /// <summary>
        /// The position
        /// </summary>
        private decimal Pos;
        /// <summary>
        /// The speed
        /// </summary>
        private decimal speed;
        /// <summary>
        /// The limit
        /// </summary>
        private decimal limit;
        /// <summary>
        /// The scroll speed
        /// </summary>
        private int _scrollSpeed;
        /// <summary>
        /// The controls cop
        /// </summary>
        private Control[] controlsCop;
        /// <summary>
        /// The name error
        /// </summary>
        private string NameError;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitMultiFormatLabel" /> class.
        /// </summary>
        public ZeroitMultiFormatLabel()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this._MainText = "";
            this.TextLines = new string[0];
            this._Image = null;
            this._ImageAlign = ContentAlignment.MiddleCenter;
            this._MainTextAlign = ContentAlignment.TopLeft;
            this._MainTextBorderStyle = BorderStyle.None;
            this._MainTextBackColor = Color.Transparent;
            this._MainTextPadding = new Padding(0);
            this._automaticScroll = false;
            this.defaultFormat = new TextFormat(SystemColors.ControlText,
                new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Color.Empty, BorderStyle.None,
                ContentAlignment.TopLeft, ContentAlignment.MiddleCenter, new Padding(0), "<none>");
            this.MainFormat = new TextFormat(SystemColors.ControlText,
                new Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Color.Empty, BorderStyle.None,
                ContentAlignment.TopLeft, ContentAlignment.MiddleCenter, new Padding(0), "MainFormat");
            this.MainFormat.ExtPropertyChanged += new ExtPropertyChangedEventHandler(this.TextFormatHasChanged);
            this.textFormatNames = new List<string>();
            this.textFormatNames.Add("<none>");
            this.textFormatNames.Add("MainFormat");
            this.customTextNames = new List<string>();
            this.timer.Tick += new EventHandler(this.LabelScroll);
            this._scrollSpeed = 25;

            InitializeComponent();

            this.MainText = this.Name;
            //Added for Localization Aid
            this.NameError = "Name Error. This Name allready exist.";
        }
        #endregion

        #region Properties
        /// <summary>
        /// The MainText to show. If it is left blank and Image is null, line will not be shown.
        /// </summary>
        /// <value>The main text.</value>
        [Category("ContentAppearance")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor", typeof(System.Drawing.Design.UITypeEditor))]
        [Description("The MainText to show. If it is left blank and Image is null, line will not be shown.")]
        public string MainText
        {
            get { return this._MainText; }
            set
            {
                bool hascontent = this._MainText.Length > 0 || this.Image != null;
                if (this._MainText != value)
                {
                    this._MainText = value;
                    this.TextLines = value.Split(this.separator, StringSplitOptions.None);
                    this.ChangeMainLabelContent(hascontent, value, this.Image);
                }
            }
        }

        /// <summary>
        /// The Image to show with the MainText. If it is null and the MainText is left blank, line will not be shown.
        /// </summary>
        /// <value>The image.</value>
        [Category("ContentAppearance")]
        [DefaultValue(null)]
        [Description("The Image to show with the MainText. If it is null and the MainText is left blank, line will not be shown.")]
        public Image Image
        {
            get { return this._Image; }
            set
            {
                bool hascontent = this.MainText.Length > 0 || this._Image != null;
                if (this._Image != value)
                {
                    this._Image = value;
                    this.ChangeMainLabelContent(hascontent, this.MainText, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the foreground color for the MainText.
        /// </summary>
        /// <value>The color of the MainText.</value>
        [Category("ContentAppearance")]
        [Description("The ForeColor for the MainText.")]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                if (base.ForeColor != value)
                {
                    base.ForeColor = value;
                    this.MainFormat.ForeColor = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the main text's alignment.
        /// </summary>
        /// <value>The main text's align.</value>
        [Category("ContentAppearance")]
        [DefaultValue(ContentAlignment.TopLeft)]
        [Description("The Text Alignment for the MainText.")]
        public ContentAlignment MainTextAlign
        {
            get { return this._MainTextAlign; }
            set
            {
                if (this._MainTextAlign != value)
                {
                    this._MainTextAlign = value;
                    this.MainFormat.TextAlign = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by in the main text.
        /// </summary>
        /// <value>The font.</value>
        [Category("ContentAppearance")]
        [Description("The Font for the MainText.")]
        public new Font Font
        {
            get { return base.Font; }
            set
            {
                if (base.Font != value)
                {
                    base.Font = value;
                    this.MainFormat.Font = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the main text's border style.
        /// </summary>
        /// <value>The main text border style.</value>
        [Category("ContentAppearance")]
        [DefaultValue(BorderStyle.None)]
        [Description("The BorderStyle for the MainText.")]
        public BorderStyle MainTextBorderStyle
        {
            get { return this._MainTextBorderStyle; }
            set
            {
                if (this._MainTextBorderStyle != value)
                {
                    this._MainTextBorderStyle = value;
                    this.MainFormat.BorderStyle = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the main text's background.
        /// </summary>
        /// <value>The color of the main text's background.</value>
        [Category("ContentAppearance")]
        [DefaultValue(typeof(Color), "Transparent")]
        [Description("The BackColor for the MainText.")]
        public Color MainTextBackColor
        {
            get { return this._MainTextBackColor; }
            set
            {
                if (this._MainTextBackColor != value)
                {
                    this._MainTextBackColor = value;
                    this.MainFormat.TextBackColor = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the image alignment.
        /// </summary>
        /// <value>The image alignment.</value>
        [Category("ContentAppearance")]
        [Description("The Image Alignment for the Image in the MainText.")]
        public ContentAlignment ImageAlign
        {
            get { return this._ImageAlign; }
            set
            {
                if (this._ImageAlign != value)
                {
                    this._ImageAlign = value;
                    this.MainFormat.ImageAlign = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the main text's padding.
        /// </summary>
        /// <value>The main text's padding.</value>
        [Category("ContentAppearance")]
        [Description("The Padding for the MainText.")]
        public Padding MainTextPadding
        {
            get { return this._MainTextPadding; }
            set
            {
                if (this._MainTextPadding != value)
                {
                    this._MainTextPadding = value;
                    this.MainFormat.Padding = value;
                }
            }
        }

        /// <summary>
        /// Gets the customized text format.
        /// </summary>
        /// <value>The customized text format.</value>
        [Category("ContentAppearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(typeof(List<TextFormat>))]
        [Editor(typeof(TextFormatCollectionEditor), typeof(UITypeEditor))]
        [Description("Managing of the TextsFormat Collection.")]
        public List<TextFormat> CustomizedTextFormats
        {
            get
            {
                if (this._customizedTextFormats == null)
                    this._customizedTextFormats = new List<TextFormat>();
                return this._customizedTextFormats;
            }
        }

        /// <summary>
        /// Gets the customized text.
        /// </summary>
        /// <value>The customized text.</value>
        [Category("ContentAppearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(typeof(List<CustomText>))]
        [Editor(typeof(CustomTextCollectionEditor), typeof(UITypeEditor))]
        [Description("Managing of the CustomText Collection.")]
        public List<CustomText> CustomizedTexts
        {
            get
            {
                if (this._customLines == null)
                    this._customLines = new List<CustomText>();
                return this._customLines;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether there should be automatic scrolling.
        /// </summary>
        /// <value><c>true</c> if automatic scrolling; otherwise, <c>false</c>.</value>
        [Category("ContentAppearance")]
        [DefaultValue(false)]
        [Description("Activates / Deactivates the AutomaticScroll function.")]
        public bool AutomaticScroll
        {
            get { return this._automaticScroll; }
            set
            {
                this._automaticScroll = value;
                this.AutoScroll = !value;
            }
        }

        /// <summary>
        /// Gets or sets the speed of automatic scrolling.
        /// </summary>
        /// <value>The automatic scroll speed.</value>
        [Category("ContentAppearance")]
        [DefaultValue(25)]
        [Description("Speed in pixels per second for the text scroll. Values must be between 10 and 500")]
        public int AutomaticScrollSpeed
        {
            get { return this._scrollSpeed; }
            set
            {
                if (value < 10) value = 10;
                if (value > 500) value = 500;
                this._scrollSpeed = value;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// Appends text with the default format
        /// </summary>
        /// <param name="text">string to display.</param>
        public void AppendContent(string text)
        {
            this.addtext(text, this.MainFormat, "", null);
        }

        /// <summary>
        /// Appends text lines with the default text format
        /// </summary>
        /// <param name="TextLines">string Array to display.</param>
        public void AppendContent(string[] TextLines)
        {
            this.addLines(TextLines, this.MainFormat, "", null);
        }

        /// <summary>
        /// Appends an image with the default format
        /// </summary>
        /// <param name="image">image to display.</param>
        public void AppendContent(Image image)
        {
            this.addtext("", this.MainFormat, "", image);
        }

        /// <summary>
        /// Appends text and image with the default format
        /// </summary>
        /// <param name="text">text to display.</param>
        /// <param name="image">image to display.</param>
        public void AppendContent(string text, Image image)
        {
            this.addtext(text, this.MainFormat, "", image);
        }

        /// <summary>
        /// Appends text lines and image with the default text format
        /// </summary>
        /// <param name="TextLines">string Array to display.</param>
        /// <param name="image">image to display.</param>
        public void AppendContent(string[] TextLines, Image image)
        {
            this.addLines(TextLines, this.MainFormat, "", image);
        }

        /// <summary>
        /// Appends text with a defined text format
        /// </summary>
        /// <param name="text">string to display.</param>
        /// <param name="TFormat">TextFormat to apply to the text.</param>
        public void AppendContent(string text, TextFormat TFormat)
        {
            this.addtext(text, TFormat, "", null);
        }

        /// <summary>
        /// Appends text lines with a defined text format
        /// </summary>
        /// <param name="TextLines">string Array to display.</param>
        /// <param name="TFormat">TextFormat to apply to the text.</param>
        public void AppendContent(string[] TextLines, TextFormat TFormat)
        {
            this.addLines(TextLines, TFormat, "", null);
        }

        /// <summary>
        /// Appends an image with a defined format
        /// </summary>
        /// <param name="image">image to display.</param>
        /// <param name="TFormat">TextFormat to apply.</param>
        public void AppendContent(Image image, TextFormat TFormat)
        {
            this.addtext("", TFormat, "", image);
        }

        /// <summary>
        /// Appends text and image with a defined format
        /// </summary>
        /// <param name="text">text to display.</param>
        /// <param name="image">image to display.</param>
        /// <param name="TFormat">TextFormat to apply.</param>
        public void AppendContent(string text, Image image, TextFormat TFormat)
        {
            this.addtext(text, TFormat, "", image);
        }

        /// <summary>
        /// Appends text lines and image with a defined text format
        /// </summary>
        /// <param name="TextLines">string Array to display.</param>
        /// <param name="image">image to display.</param>
        /// <param name="TFormat">TextFormat to apply.</param>
        public void AppendContent(string[] TextLines, Image image, TextFormat TFormat)
        {
            this.addLines(TextLines, TFormat, "", image);
        }

        /// <summary>
        /// Appends text with a defined text format
        /// </summary>
        /// <param name="text">string to display.</param>
        /// <param name="TextFormatName">A string with Name of the TextFormat from the TextFormats Collection to apply to the text.</param>
        public void AppendContent(string text, string TextFormatName)
        {
            this.addtext(text, TextFormatName, "", null);
        }

        /// <summary>
        /// Appends text lines with a defined text format
        /// </summary>
        /// <param name="TextLines">string[] to display.</param>
        /// <param name="TextFormatName">A string with Name of the TextFormat from the TextFormats Collection to apply to the text.</param>
        public void AppendContent(string[] TextLines, string TextFormatName)
        {
            this.addLines(TextLines, TextFormatName, "", null);
        }

        /// <summary>
        /// Appends an image with a defined format
        /// </summary>
        /// <param name="image">Image to display.</param>
        /// <param name="TextFormatName">A string with Name of the TextFormat from the TextFormats Collection to apply.</param>
        public void AppendContent(Image image, string TextFormatName)
        {
            this.addtext("", TextFormatName, "", image);
        }

        /// <summary>
        /// Appends text and image with a defined format
        /// </summary>
        /// <param name="text">string to display.</param>
        /// <param name="image">Image to display.</param>
        /// <param name="TextFormatName">A string with Name of the TextFormat from the TextFormats Collection to apply.</param>
        public void AppendContent(string text, Image image, string TextFormatName)
        {
            this.addtext(text, TextFormatName, "", image);
        }

        /// <summary>
        /// Appends text lines and image with a defined format
        /// </summary>
        /// <param name="TextLines">string[] to display.</param>
        /// <param name="image">Image to display.</param>
        /// <param name="TextFormatName">A string with Name of the TextFormat from the TextFormats Collection to apply.</param>
        public void AppendContent(string[] TextLines, Image image, string TextFormatName)
        {
            this.addLines(TextLines, TextFormatName, "", image);
        }

        /// <summary>
        /// Appends a customized text
        /// </summary>
        /// <param name="CText">The c text.</param>
        public void AppendCustomizedText(CustomText CText)
        {
            this.addtext(CText.Text, CText.TextDesign, CText.Name, CText.Image);
        }

        /// <summary>
        /// Appends a customized text by Name
        /// </summary>
        /// <param name="CustomTextName">A string with Name of the CustomText from the CustomizedTexts Collection to be displayed.</param>
        public void AppendCustomizedText(string CustomTextName)
        {
            CustomText CText = this.CustomizedTexts.Find(g => g.Name == CustomTextName);
            if (CText == null)
                return;
            this.addtext(CText.Text, CText.TextDesign, CText.Name, CText.Image);
        }

        /// <summary>
        /// Get a List with the preconfigured CustomTextFormat Names
        /// </summary>
        /// <value>The custom text format names.</value>
        [Browsable(false)]
        public List<string> CustomTextFormatNames
        {
            get { return this.textFormatNames; }
        }

        /// <summary>
        /// Get a List with the preconfigured CustomizedText Names
        /// </summary>
        /// <value>The customized text names.</value>
        [Browsable(false)]
        public List<string> CustomizedTextNames
        {
            get { return this.customTextNames; }
        }

        /// <summary>
        /// Returns the Texformat from the CustomTextFormats List with the given Name.
        /// </summary>
        /// <param name="Name">The Name of the TextFormat.</param>
        /// <returns>The TextFormat</returns>
        public TextFormat CustomizedTextFormat_byName(string Name)
        {
            TextFormat TFormat = null;
            if (Name == "<none>")
                TFormat = this.defaultFormat;
            else if (Name == "MainFormat")
                TFormat = this.MainFormat;
            else
                TFormat = this.CustomizedTextFormats.Find(h => h.Name == Name);
            return TFormat;
        }

        /// <summary>
        /// Customizeds the name of the text by.
        /// </summary>
        /// <param name="Name">The name.</param>
        /// <returns>CustomText.</returns>
        public CustomText CustomizedText_byName(string Name)
        {
            return this.CustomizedTexts.Find(h => h.Name == Name);
        }


        /// <summary>
        /// Resets the ZeroitMultiFormatLabel to its original status
        /// </summary>
        public void ResetContent()
        {
            this.Controls.Clear();
            if (this.MainLabel.Text != "" || this.MainLabel.Image != null)
                this.Controls.Add(this.MainLabel);
            if (this.CustomizedTexts != null && this.CustomizedTexts.Count > 0)
            {
                foreach (CustomText ctext in this.CustomizedTexts)
                {
                    if (ctext.OutputType == CustomText.ShowType.Permanent)
                        this.AppendCustomizedText(ctext);
                }
            }
        }
        #endregion

        #region TextFormats Events
        /// <summary>
        /// Texts the format editor load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        internal void TextFormatEditorLoad(object sender, EventArgs e)
        {
            this.textFormatNamesCop = this.cloneNames(this.textFormatNames);
            this.textFormatsCop = this.cloneTextFormats(this.CustomizedTextFormats);
            this.customTextNamesCop = this.cloneNames(this.customTextNames);
            this.customLinesCop = this.cloneCustomTexts(this.CustomizedTexts);
            this.controlsCop = this.cloneControls(this.Controls);
        }

        /// <summary>
        /// Adds the text format.
        /// </summary>
        /// <param name="TextFormatName">Name of the text format.</param>
        internal void AddTextFormat(string TextFormatName)
        {
            this.textFormatNames.Add(TextFormatName);
        }

        /// <summary>
        /// Removes the text format.
        /// </summary>
        /// <param name="TextFormatName">Name of the text format.</param>
        internal void RemoveTextFormat(string TextFormatName)
        {
            if (this.textFormatNames.Contains(TextFormatName) && TextFormatName != "MainFormat" && TextFormatName != "<none>")
                this.textFormatNames.Remove(TextFormatName);
            foreach (CustomText Ctext in this.CustomizedTexts)
            {
                if (Ctext.TextDesign == TextFormatName)
                    Ctext.TextDesign = "<none>";
            }
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Label)
                {
                    Label label = (Label)this.Controls[i];
                    if (label.Tag.ToString() == TextFormatName)
                    {
                        label.Tag = "<none>";
                        label = this.ReConfigureLabel(label, this.defaultFormat);
                    }
                }
            }
        }

        /// <summary>
        /// Texts the format has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExtPropertyChangedEventArgs"/> instance containing the event data.</param>
        internal void TextFormatHasChanged(object sender, ExtPropertyChangedEventArgs e)
        {
            if (sender is TextFormat)
            {
                TextFormat TextF = (TextFormat)sender;
                if (e.PropertyName == "Name")
                {
                    string newname = e.NewValue.ToString();
                    string oldname = e.OldValue.ToString();
                    if (this.textFormatNames.Contains(newname))
                    {
                        TextF.ExtPropertyChanged -= this.TextFormatHasChanged;
                        TextF.Name = oldname;
                        MessageBox.Show(this.NameError);
                        TextF.ExtPropertyChanged += new ExtPropertyChangedEventHandler(this.TextFormatHasChanged);
                    }
                    else
                    {
                        int index = this.textFormatNames.FindIndex(x => x == oldname);
                        if (index >= 0)
                            this.textFormatNames[index] = newname;

                        foreach (CustomText Ctext in this.CustomizedTexts)
                        {
                            if (Ctext.TextDesign == oldname)
                                Ctext.TextDesign = newname;
                        }

                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            if (this.Controls[i] is Label)
                            {
                                Label label = (Label)this.Controls[i];
                                if (label.Tag.ToString() == oldname)
                                    label.Tag = newname;
                            }
                        }
                    }
                }
                else
                {
                    string texfName = TextF.Name;
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i] is Label)
                        {
                            Label label = (Label)this.Controls[i];
                            if (label.Tag.ToString() == texfName)
                                label = this.ReConfigureLabel(label, TextF);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Texts the format editor cancel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        internal void TextFormatEditorCancel(object sender, EventArgs e)
        {
            this.textFormatNames = this.cloneNames(this.textFormatNamesCop);
            this._customizedTextFormats = this.cloneTextFormats(this.textFormatsCop);
            this.customTextNames = this.cloneNames(this.customTextNamesCop);
            this._customLines = this.cloneCustomTexts(this.customLinesCop);
            this.Controls.Clear();
            this.Controls.AddRange(this.controlsCop);
            this.textFormatNamesCop = null;
            this.textFormatsCop = null;
            this.customTextNamesCop = null;
            this.customLinesCop = null;
            this.controlsCop = null;
        }
        #endregion

        #region CustomTexts Events
        /// <summary>
        /// Customs the text editor load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        internal void CustomTextEditorLoad(object sender, EventArgs e)
        {
            this.customTextNamesCop = this.cloneNames(this.customTextNames);
            this.customLinesCop = this.cloneCustomTexts(this.CustomizedTexts);
            this.controlsCop = this.cloneControls(this.Controls);
        }
        /// <summary>
        /// Adds the custom text.
        /// </summary>
        /// <param name="customText">The custom text.</param>
        internal void AddCustomText(CustomText customText)
        {
            this.customTextNames.Add(customText.Name);
            if (customText.OutputType == CustomText.ShowType.Permanent)
                this.ShowCustomText(customText);
        }
        /// <summary>
        /// Removes the custom text.
        /// </summary>
        /// <param name="customText">The custom text.</param>
        internal void RemoveCustomText(CustomText customText)
        {
            string CustomTextName = customText.Name;
            if (this.customTextNames.Contains(CustomTextName))
                this.customTextNames.Remove(CustomTextName);
            if (customText.OutputType == CustomText.ShowType.Permanent)
                this.ExcludeCustomText(CustomTextName);
        }

        /// <summary>
        /// Customs the text has changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ExtPropertyChangedEventArgs"/> instance containing the event data.</param>
        internal void CustomTextHasChanged(object sender, ExtPropertyChangedEventArgs e)
        {
            if (sender is CustomText)
            {
                CustomText CText = (CustomText)sender;
                if (e.PropertyName == "Name")
                {
                    string newname = e.NewValue.ToString();
                    string oldname = e.OldValue.ToString();
                    if (this.customTextNames.Contains(newname))
                    {
                        CText.ExtPropertyChanged -= this.CustomTextHasChanged;
                        CText.Name = oldname;
                        MessageBox.Show(this.NameError);
                        CText.ExtPropertyChanged += new ExtPropertyChangedEventHandler(this.CustomTextHasChanged);
                    }
                    else
                    {
                        int index = customTextNames.FindIndex(x => x == oldname);
                        if (index >= 0)
                            this.customTextNames[index] = newname;

                        object control = this.Controls["label_" + oldname];
                        if (control is Label)
                        {
                            Label label = (Label)control;
                            label.Name = "label_" + newname;
                        }
                    }
                }
                else
                {
                    string cTextName = CText.Name;
                    if (e.PropertyName == "OutputType")
                    {
                        if (CText.OutputType == CustomText.ShowType.Permanent)
                            this.redrawControl();
                        else
                            this.ExcludeCustomText(cTextName);
                    }
                    else
                    {
                        object control = this.Controls["label_" + cTextName];
                        if (control is Label)
                        {
                            Label label = (Label)control;
                            label.Text = CText.Text;
                            label.Image = CText.Image;
                            this.ReConfigureLabel(label, CText.TextDesign);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Customs the textmove up.
        /// </summary>
        /// <param name="customText">The custom text.</param>
        internal void CustomTextmoveUp(CustomText customText)
        {
            string CustomTextName = customText.Name;
            int index1 = this.customTextNames.FindIndex(h => h == CustomTextName);
            if (index1 > 0)
            {
                CustomText ctext = CustomizedTexts.Find(h => h.Name == this.customTextNames[index1 - 1]);
                this.customTextNames.RemoveAt(index1);
                this.customTextNames.Insert(index1 - 1, CustomTextName);
                if (ctext.OutputType != CustomText.ShowType.OnDemand || customText.OutputType != CustomText.ShowType.OnDemand)
                    this.redrawControl();
            }
        }

        /// <summary>
        /// Customs the textmove down.
        /// </summary>
        /// <param name="customText">The custom text.</param>
        internal void CustomTextmoveDown(CustomText customText)
        {
            string CustomTextName = customText.Name;
            int index1 = this.customTextNames.FindIndex(h => h == CustomTextName);
            if (index1 < this.customTextNames.Count - 1)
            {
                CustomText ctext = CustomizedTexts.Find(h => h.Name == this.customTextNames[index1 + 1]);
                this.customTextNames.RemoveAt(index1);
                this.customTextNames.Insert(index1 + 1, CustomTextName);
                if (ctext.OutputType != CustomText.ShowType.OnDemand || customText.OutputType != CustomText.ShowType.OnDemand)
                    this.redrawControl();
            }
        }

        /// <summary>
        /// Customs the text editor cancel.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        internal void CustomTextEditorCancel(object sender, EventArgs e)
        {
            this._customLines = this.cloneCustomTexts(this.customLinesCop);
            this.customTextNames = this.cloneNames(this.customTextNamesCop);
            this.Controls.Clear();
            this.Controls.AddRange(this.controlsCop);
            this.customTextNamesCop = null;
            this.customLinesCop = null;
            this.controlsCop = null;
        }
        #endregion

        #region private methods
        /// <summary>
        /// Addtexts the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="TFormatName">Name of the t format.</param>
        /// <param name="CTextName">Name of the c text.</param>
        /// <param name="CTextImage">The c text image.</param>
        private void addtext(string text, string TFormatName, string CTextName, Image CTextImage)
        {
            string[] textLines = text.Split(this.separator, StringSplitOptions.None);
            this.addLines(textLines, TFormatName, CTextName, CTextImage);
        }

        /// <summary>
        /// Addtexts the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="TFormat">The t format.</param>
        /// <param name="CTextName">Name of the c text.</param>
        /// <param name="CTextImage">The c text image.</param>
        private void addtext(string text, TextFormat TFormat, string CTextName, Image CTextImage)
        {
            string[] textLines = text.Split(this.separator, StringSplitOptions.None);
            this.addLines(textLines, TFormat, CTextName, CTextImage);
        }

        /// <summary>
        /// Adds the lines.
        /// </summary>
        /// <param name="textLines">The text lines.</param>
        /// <param name="TFormatName">Name of the t format.</param>
        /// <param name="CTextName">Name of the c text.</param>
        /// <param name="CTextImage">The c text image.</param>
        private void addLines(string[] textLines, string TFormatName, string CTextName, Image CTextImage)
        {
            TextFormat TFormat = this.CustomizedTextFormat_byName(TFormatName);
            if (TFormat != null)
                this.addLines(textLines, TFormat, CTextName, CTextImage);
        }

        /// <summary>
        /// Adds the lines.
        /// </summary>
        /// <param name="textLines">The text lines.</param>
        /// <param name="tFormat">The t format.</param>
        /// <param name="CTextName">Name of the c text.</param>
        /// <param name="CTextImage">The c text image.</param>
        private void addLines(string[] textLines, TextFormat tFormat, string CTextName, Image CTextImage)
        {
            int lines = this.Controls.Count;
            int addlines = textLines.Length;
            string text = string.Join(CRLF, textLines);

            Label newlabel = this.CreateLabel("label_" + lines.ToString(), tFormat, text, addlines, CTextName, CTextImage);
            this.Controls.Add(newlabel);
            this.Controls.SetChildIndex(newlabel, 0);
            this.ScrollControlIntoView(newlabel);
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelName">Name of the label.</param>
        /// <param name="textF">The text f.</param>
        /// <param name="text">The text.</param>
        /// <param name="addlines">The addlines.</param>
        /// <param name="CTextName">Name of the c text.</param>
        /// <param name="image">The image.</param>
        /// <returns>Label.</returns>
        private Label CreateLabel(string labelName, TextFormat textF, string text, int addlines, string CTextName, Image image)
        {
            Label result = new Label();

            result.BackColor = textF.TextBackColor;
            result.Dock = DockStyle.Top;
            result.Font = textF.Font;
            result.ForeColor = textF.ForeColor;
            result.TextAlign = textF.TextAlign;
            result.ImageAlign = textF.ImageAlign;
            result.Text = text;
            result.Image = image;
            result.Tag = textF.Name;
            result.Name = labelName;
            result.BorderStyle = textF.BorderStyle;
            result.Padding = textF.Padding;

            int verticalpos = 0;
            for (int i = 0; i < this.Controls.Count; i++)
            {
                verticalpos = verticalpos + this.Controls[i].Height;
            }
            result.Location = new System.Drawing.Point(0, verticalpos);
            int imageHeight = 0;
            if (image != null)
                imageHeight = image.Height;

            result.Size = this.labelSize(result, addlines, imageHeight);
            return result;
        }

        /// <summary>
        /// Labels the size.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="addlines">The addlines.</param>
        /// <param name="imageHeight">Height of the image.</param>
        /// <returns>Size.</returns>
        private Size labelSize(Label result, int addlines, int imageHeight)
        {
            Graphics graf = result.CreateGraphics();
            Size stringSize = Size.Round(graf.MeasureString(result.Text, result.Font, this.Width - 4 - result.Padding.Left - result.Padding.Right));
            Size linesize = Size.Round(graf.MeasureString("Z", result.Font, 100));
            int numlin = (int)Math.Ceiling((decimal)stringSize.Height / (decimal)linesize.Height);
            if (numlin > addlines)
                addlines = numlin;
            stringSize.Height += 2 * addlines;
            if (stringSize.Height < imageHeight)
                stringSize.Height = imageHeight;
            return new System.Drawing.Size(this.Size.Width - 4, stringSize.Height + result.Padding.Top + result.Padding.Bottom + 4);
        }

        /// <summary>
        /// Res the configure label.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="textFName">Name of the text f.</param>
        /// <returns>Label.</returns>
        private Label ReConfigureLabel(Label result, string textFName)
        {
            TextFormat TFormat = this.CustomizedTextFormat_byName(textFName);
            if (TFormat == null)
                return null;
            return this.ReConfigureLabel(result, TFormat);
        }

        /// <summary>
        /// Res the configure label.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="textF">The text f.</param>
        /// <returns>Label.</returns>
        private Label ReConfigureLabel(Label result, TextFormat textF)
        {
            result.BackColor = textF.TextBackColor;
            result.Font = textF.Font;
            result.ForeColor = textF.ForeColor;
            result.TextAlign = textF.TextAlign;
            result.ImageAlign = textF.ImageAlign;
            result.BorderStyle = textF.BorderStyle;
            result.Padding = textF.Padding;
            result.Tag = textF.Name;

            string[] textLines = result.Text.Split(this.separator, StringSplitOptions.None);
            int addlines = textLines.Length;
            int height = result.Size.Height;
            int imageHeight = 0;
            if (result.Image != null)
                imageHeight = result.Image.Height;
            result.Size = this.labelSize(result, addlines, imageHeight);

            if (height != result.Size.Height && this.Controls.Count > 0)
            {
                int dif = height - result.Size.Height;
                int index = this.Controls.GetChildIndex(result);
                if (index > 0)
                {
                    for (int i = index - 1; i >= 0; i--)
                    {
                        this.Controls[i].Location = new Point(0, this.Controls[i].Location.Y - dif);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Changes the content of the main label.
        /// </summary>
        /// <param name="hascontent">if set to <c>true</c> [hascontent].</param>
        /// <param name="text">The text.</param>
        /// <param name="image">The image.</param>
        private void ChangeMainLabelContent(bool hascontent, string text, Image image)
        {
            if (text.Length == 0 && image == null)
            {
                if (this.Controls.ContainsKey("MainLabel"))
                {
                    int key = this.Controls.IndexOfKey("MainLabel");
                    int h = this.MainLabel.Height;
                    this.Controls.RemoveByKey("MainLabel");
                    for (int i = key - 1; i >= 0; i--)
                    {
                        this.Controls[i].Top -= h;
                    }
                }
            }
            else
            {
                if (hascontent == false)
                {
                    this.MainLabel = this.CreateLabel("MainLabel", this.MainFormat, text, this.TextLines.Length, "", image);
                    this.MainLabel.Top = 0;
                    int h = this.MainLabel.Height;
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        this.Controls[i].Top += h;
                    }
                    this.Controls.Add(this.MainLabel);
                    this.ScrollControlIntoView(this.MainLabel);
                }
                else
                {
                    this.MainLabel.Text = text;
                    this.MainLabel.Image = image;
                    this.ReConfigureLabel(this.MainLabel, this.MainFormat);
                }
            }
        }

        /// <summary>
        /// Shows the custom text.
        /// </summary>
        /// <param name="customText">The custom text.</param>
        private void ShowCustomText(CustomText customText)
        {
            string[] textLines = customText.Text.Split(this.separator, StringSplitOptions.None);
            TextFormat TFormat = this.CustomizedTextFormat_byName(customText.TextDesign);
            if (TFormat != null)
            {
                Label label = this.CreateLabel("label_" + customText.Name, TFormat, customText.Text, textLines.Length, customText.Name, customText.Image);
                this.Controls.Add(label);
                this.Controls.SetChildIndex(label, 0);
                this.ScrollControlIntoView(label);
            }
        }

        /// <summary>
        /// Redraws the control.
        /// </summary>
        private void redrawControl()
        {
            this.Controls.Clear();
            if (this.MainLabel.Text != "" || this.MainLabel.Image != null)
                this.Controls.Add(this.MainLabel);
            if (this.CustomizedTexts != null && this.CustomizedTexts.Count > 0)
            {
                foreach (string ctextName in this.customTextNames)
                {
                    CustomText ctext = this.CustomizedTexts.Find(h => h.Name == ctextName);
                    if (ctext.OutputType == CustomText.ShowType.Permanent)
                        this.ShowCustomText(ctext);
                }
            }
        }

        /// <summary>
        /// Excludes the custom text.
        /// </summary>
        /// <param name="CustomTextName">Name of the custom text.</param>
        private void ExcludeCustomText(string CustomTextName)
        {
            object control = this.Controls["label_" + CustomTextName];
            if (control is Label)
            {
                Label label = (Label)control;
                int index = this.Controls.IndexOfKey("label_" + CustomTextName);
                int h = label.Height;
                this.Controls.RemoveAt(index);
                for (int i = index - 1; i >= 0; i--)
                {
                    this.Controls[i].Top -= h;
                }
            }
        }

        #region elements Clone
        /// <summary>
        /// Clones the text formats.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List&lt;TextFormat&gt;.</returns>
        private List<TextFormat> cloneTextFormats(List<TextFormat> list)
        {
            List<TextFormat> result = new List<TextFormat>();
            foreach (TextFormat textf in list)
            {
                result.Add((TextFormat)textf.Clone());
            }
            return result;
        }

        /// <summary>
        /// Clones the names.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        private List<string> cloneNames(List<string> list)
        {
            List<string> result = new List<string>();
            foreach (string name in list)
            {
                result.Add((string)name.Clone());
            }
            return result;
        }

        /// <summary>
        /// Clones the custom texts.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List&lt;CustomText&gt;.</returns>
        private List<CustomText> cloneCustomTexts(List<CustomText> list)
        {
            List<CustomText> result = new List<CustomText>();
            foreach (CustomText ctext in list)
            {
                result.Add((CustomText)ctext.Clone());
            }
            return result;
        }

        /// <summary>
        /// Clones the controls.
        /// </summary>
        /// <param name="controls">The controls.</param>
        /// <returns>Control[].</returns>
        private Control[] cloneControls(ControlCollection controls)
        {
            Control[] copy = new Control[controls.Count];
            for (int i = 0; i < controls.Count; i++)
            {
                if (controls[i] is Label)
                {
                    Label orig = (Label)controls[i];
                    Label result = new Label();
                    result.BackColor = orig.BackColor;
                    result.Dock = orig.Dock;
                    result.Font = orig.Font;
                    result.ForeColor = orig.ForeColor;
                    result.Image = orig.Image;
                    result.ImageAlign = orig.ImageAlign;
                    result.Location = orig.Location;
                    result.Name = orig.Name;
                    result.Size = orig.Size;
                    result.Tag = orig.Tag;
                    result.Text = orig.Text;
                    result.TextAlign = orig.TextAlign;
                    result.BorderStyle = orig.BorderStyle;
                    copy[i] = result;
                }
            }
            return copy;
        }
        #endregion

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




        #region overrided events
        /// <summary>
        /// Handles the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                if (this.Controls[i] is Label)
                {
                    Label label = (Label)this.Controls[i];
                    string[] textLines = label.Text.Split(this.separator, StringSplitOptions.None);
                    int addlines = textLines.Length;
                    int imageHeight = 0;
                    if (label.Image != null)
                        imageHeight = label.Image.Height;
                    label.Size = this.labelSize(label, addlines, imageHeight);
                }
            }
            base.OnResize(e);
        }




        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            base.OnPaint(e);
        }

        //Overrided to avoid unintentional change
        /// <summary>
        /// Gets or sets a value indicating whether the container enables the user to scroll to any controls placed outside of its visible boundaries.
        /// </summary>
        /// <value><c>true</c> if [automatic scroll]; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        [DefaultValue(true)]
        public override bool AutoScroll
        {
            get { return base.AutoScroll; }
            set { base.AutoScroll = true; }
        }
        #endregion

        #region Label Scrolling Process
        #region AutomaticScroll control
        /// <summary>
        /// Shows the scroll bar.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="wBar">The w bar.</param>
        /// <param name="bShow">if set to <c>true</c> [b show].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        /// <summary>
        /// Enum ScrollBarDirection
        /// </summary>
        private enum ScrollBarDirection
        {
            /// <summary>
            /// The sb horz
            /// </summary>
            SB_HORZ = 0,
            /// <summary>
            /// The sb vert
            /// </summary>
            SB_VERT = 1,
            /// <summary>
            /// The sb control
            /// </summary>
            SB_CTL = 2,
            /// <summary>
            /// The sb both
            /// </summary>
            SB_BOTH = 3,
            /// <summary>
            /// The vm vscroll
            /// </summary>
            VM_VSCROLL = 0x115,
            /// <summary>
            /// The wm paint
            /// </summary>
            WM_PAINT = 0x0F
        }

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (this.AutomaticScroll == true)
                ShowScrollBar(this.Handle, (int)ScrollBarDirection.SB_VERT, false);

            base.WndProc(ref m);
        }
        #endregion

        /// <summary>
        /// Starts the scrolling.
        /// </summary>
        public void StartScrolling()
        {
            if (this.AutomaticScroll == false)
                return;
            this.timer.Interval = 10;
            decimal alto = (decimal)this.ClientSize.Height;
            decimal dim = (decimal)(this.Controls[0].Top + this.Controls[0].Height);
            this.limit = dim - alto;
            this.speed = (decimal)this.AutomaticScrollSpeed / 66; //66 is an empirical constant to adjust value with system clock resolution 
            this.VerticalScroll.Maximum = 10000;
            this.Pos = 0;
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// Labels the scroll.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LabelScroll(object sender, EventArgs e)
        {
            this.Pos = this.Pos + this.speed;
            this.SuspendLayout();
            this.VerticalScroll.Value = (int)this.Pos;
            this.PerformLayout();
            if (this.Pos >= this.limit)
            {
                this.timer.Stop();
                this.timer.Enabled = false;
            }
        }

        /// <summary>
        /// Stops the scrolling.
        /// </summary>
        public void StopScrolling()
        {
            this.timer.Stop();
            this.timer.Enabled = false;
            this.Pos = 0;
            this.VerticalScroll.Value = (int)this.Pos;
        }

        /// <summary>
        /// Continues the scrolling.
        /// </summary>
        public void ContinueScrolling()
        {
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// Pauses the scrolling.
        /// </summary>
        public void PauseScrolling()
        {
            this.timer.Stop();
            this.timer.Enabled = false;
        }
        #endregion

        #region ZeroitMultiFormatLabel Load
        /// <summary>
        /// Handles the Load event of the MultiFormatLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MultiFormatLabel_Load(object sender, EventArgs e)
        {
            if (this.CustomizedTextFormats.Count > 0)
            {
                foreach (TextFormat textF in this.CustomizedTextFormats)
                {
                    this.textFormatNames.Add(textF.Name);
                }
            }

            if (this.CustomizedTexts != null || this.CustomizedTexts.Count > 0)
            {
                foreach (CustomText ctext in this.CustomizedTexts)
                {
                    this.customTextNames.Add(ctext.Name);
                    if (ctext.OutputType == CustomText.ShowType.Permanent)
                        this.ShowCustomText(ctext);
                }
            }
            this.ScrollControlIntoView(this.MainLabel);
        }
        #endregion
    }
    #endregion

    
}
