// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="EarUIEditor.cs" company="Zeroit Dev Technologies">
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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing.Drawing2D;


namespace Zeroit.Framework.Labels.Headers
{
    /// <summary>
    /// Summary description for EarUIEditor.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />

    [ToolboxItem(false)]
	public class EarUIEditor : System.Windows.Forms.UserControl
	{
        /// <summary>
        /// The label1
        /// </summary>
        private System.Windows.Forms.Label label1;
        /// <summary>
        /// The BTN regular
        /// </summary>
        private System.Windows.Forms.Button btnRegular;
        /// <summary>
        /// The BTN ogee
        /// </summary>
        private System.Windows.Forms.Button btnOgee;
        /// <summary>
        /// The BTN concave
        /// </summary>
        private System.Windows.Forms.Button btnConcave;
        /// <summary>
        /// The BTN line
        /// </summary>
        private System.Windows.Forms.Button btnLine;
        /// <summary>
        /// The BTN rounded
        /// </summary>
        private System.Windows.Forms.Button btnRounded;
        /// <summary>
        /// The BTN stair
        /// </summary>
        private System.Windows.Forms.Button btnStair;
        /// <summary>
        /// The BTN slant
        /// </summary>
        private System.Windows.Forms.Button btnSlant;
        /// <summary>
        /// The BTN square
        /// </summary>
        private System.Windows.Forms.Button btnSquare;
        /// <summary>
        /// The image list1
        /// </summary>
        private System.Windows.Forms.ImageList imageList1;
        /// <summary>
        /// The components
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Initializes a new instance of the <see cref="EarUIEditor"/> class.
        /// </summary>
        public EarUIEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

        /// <summary>
        /// This is a required constructor that is th econtext for the
        /// invoking control.
        /// </summary>
        /// <param name="ed">Provides an interface to display Windows Forms
        /// dialog boxes or forms, and drop down list controls.</param>
        public EarUIEditor(IWindowsFormsEditorService ed)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			editorService = ed;
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EarUIEditor));
			this.btnRegular = new System.Windows.Forms.Button();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnOgee = new System.Windows.Forms.Button();
			this.btnConcave = new System.Windows.Forms.Button();
			this.btnLine = new System.Windows.Forms.Button();
			this.btnRounded = new System.Windows.Forms.Button();
			this.btnStair = new System.Windows.Forms.Button();
			this.btnSlant = new System.Windows.Forms.Button();
			this.btnSquare = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnRegular
			// 
			this.btnRegular.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnRegular.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnRegular.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegular.Image = Properties.Resources.btnRegular_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnRegular.Image")));*/
			this.btnRegular.ImageIndex = 0;
			this.btnRegular.ImageList = this.imageList1;
			this.btnRegular.Location = new System.Drawing.Point(2, 24);
			this.btnRegular.Name = "btnRegular";
			this.btnRegular.Size = new System.Drawing.Size(48, 48);
			this.btnRegular.TabIndex = 0;
			this.btnRegular.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
			//this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.Add("0", Properties.Resources.btnConcave_Image);
            this.imageList1.Images.Add("1", Properties.Resources.btnLine_Image);
            this.imageList1.Images.Add("2", Properties.Resources.btnOgee_Image);
            this.imageList1.Images.Add("3", Properties.Resources.btnRegular_Image);
            this.imageList1.Images.Add("4", Properties.Resources.btnRounded_Image);
            this.imageList1.Images.Add("5", Properties.Resources.btnSlant_Image);
            this.imageList1.Images.Add("6", Properties.Resources.btnStair_Image);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnOgee
			// 
			this.btnOgee.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnOgee.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnOgee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOgee.Image = Properties.Resources.btnOgee_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnOgee.Image")));*/
			this.btnOgee.ImageIndex = 1;
			this.btnOgee.ImageList = this.imageList1;
			this.btnOgee.Location = new System.Drawing.Point(50, 24);
			this.btnOgee.Name = "btnOgee";
			this.btnOgee.Size = new System.Drawing.Size(48, 48);
			this.btnOgee.TabIndex = 1;
			this.btnOgee.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// btnConcave
			// 
			this.btnConcave.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnConcave.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnConcave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConcave.Image = Properties.Resources.btnConcave_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnConcave.Image")));*/
			this.btnConcave.ImageIndex = 2;
			this.btnConcave.ImageList = this.imageList1;
			this.btnConcave.Location = new System.Drawing.Point(98, 24);
			this.btnConcave.Name = "btnConcave";
			this.btnConcave.Size = new System.Drawing.Size(48, 48);
			this.btnConcave.TabIndex = 2;
			this.btnConcave.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// btnLine
			// 
			this.btnLine.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnLine.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLine.Image = Properties.Resources.btnLine_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnLine.Image")));*/
			this.btnLine.ImageIndex = 3;
			this.btnLine.ImageList = this.imageList1;
			this.btnLine.Location = new System.Drawing.Point(2, 72);
			this.btnLine.Name = "btnLine";
			this.btnLine.Size = new System.Drawing.Size(48, 48);
			this.btnLine.TabIndex = 3;
			this.btnLine.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// btnRounded
			// 
			this.btnRounded.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnRounded.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnRounded.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRounded.Image = Properties.Resources.btnRounded_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnRounded.Image")));*/
			this.btnRounded.ImageIndex = 4;
			this.btnRounded.ImageList = this.imageList1;
			this.btnRounded.Location = new System.Drawing.Point(50, 72);
			this.btnRounded.Name = "btnRounded";
			this.btnRounded.Size = new System.Drawing.Size(48, 48);
			this.btnRounded.TabIndex = 4;
			this.btnRounded.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// btnStair
			// 
			this.btnStair.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnStair.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnStair.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStair.Image = Properties.Resources.btnStair_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnStair.Image")));*/
			this.btnStair.ImageIndex = 5;
			this.btnStair.ImageList = this.imageList1;
			this.btnStair.Location = new System.Drawing.Point(98, 72);
			this.btnStair.Name = "btnStair";
			this.btnStair.Size = new System.Drawing.Size(48, 48);
			this.btnStair.TabIndex = 5;
			this.btnStair.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// btnSlant
			// 
			this.btnSlant.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnSlant.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSlant.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSlant.Image = Properties.Resources.btnSlant_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnSlant.Image")));*/
			this.btnSlant.ImageIndex = 6;
			this.btnSlant.ImageList = this.imageList1;
			this.btnSlant.Location = new System.Drawing.Point(2, 120);
			this.btnSlant.Name = "btnSlant";
			this.btnSlant.Size = new System.Drawing.Size(48, 48);
			this.btnSlant.TabIndex = 6;
			this.btnSlant.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// btnSquare
			// 
			this.btnSquare.BackColor = System.Drawing.Color.LightSlateGray;
			this.btnSquare.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnSquare.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSquare.Image = Properties.Resources.btnSquare_Image;/* ((System.Drawing.Bitmap)(resources.GetObject("btnSquare.Image")));*/
			this.btnSquare.ImageIndex = 7;
			this.btnSquare.ImageList = this.imageList1;
			this.btnSquare.Location = new System.Drawing.Point(50, 120);
			this.btnSquare.Name = "btnSquare";
			this.btnSquare.Size = new System.Drawing.Size(48, 48);
			this.btnSquare.TabIndex = 7;
			this.btnSquare.Click += new System.EventHandler(this.genericButton_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(2, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 22);
			this.label1.TabIndex = 9;
			this.label1.Text = "Adornments";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label1.SizeChanged += new System.EventHandler(this.label1_SizeChanged);
			this.label1.Paint += new System.Windows.Forms.PaintEventHandler(this.header_Paint);
			// 
			// EarUIEditor
			// 
			this.BackColor = System.Drawing.Color.LightSlateGray;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.label1,
																		  this.btnSquare,
																		  this.btnSlant,
																		  this.btnStair,
																		  this.btnRounded,
																		  this.btnLine,
																		  this.btnConcave,
																		  this.btnOgee,
																		  this.btnRegular});
			this.Name = "EarUIEditor";
			this.Size = new System.Drawing.Size(146, 170);
			this.ResumeLayout(false);

		}
        #endregion

        /// <summary>
        /// The invoking editor service
        /// </summary>
        IWindowsFormsEditorService editorService = null;

        /// <summary>
        /// This is the property used to xfer property values.
        /// </summary>
        EarTypes earTypes = EarTypes.regular;
        /// <summary>
        /// Gets or sets the type of ear.
        /// </summary>
        /// <value>The type of ear.</value>
        public EarTypes TypeOfEar 
		{
			get { return earTypes; }
			set { earTypes = value; }
		}

        /// <summary>
        /// Handles the click events for the buttons.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks>This handler set the TypeOfEar property so we have
        /// it for the invoking editor and close this control. (Required
        /// functionality for dismissing DropDown</remarks>
        private void genericButton_Click(object sender, System.EventArgs e)
		{
			Button btn = (Button) sender;

			switch (btn.Name)
			{
				case "btnRegular":
					TypeOfEar = EarTypes.regular;
					break;
				case "btnOgee":
					TypeOfEar = EarTypes.ogee;
					break;
				case "btnConcave":
					TypeOfEar = EarTypes.concave;
					break;
				case "btnLine":
					TypeOfEar = EarTypes.line;
					break;
				case "btnRounded":
					TypeOfEar = EarTypes.rounded;
					break;
				case "btnStair":
					TypeOfEar = EarTypes.stair;
					break;
				case "btnSlant":
					TypeOfEar = EarTypes.slant;
					break;
				case "btnSquare":
					TypeOfEar = EarTypes.square;
					break;
			}

			editorService.CloseDropDown();
		}

        /// <summary>
        /// Paints the 3D border around the control
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			//Draw 3D border
			Pen pen = new Pen(Color.MidnightBlue, 2f);
			Pen pen1 = new Pen(Color.Silver, 2f);
			Pen pen2 = new Pen(Color.DarkSlateGray, 2f);
			Rectangle tmp = this.Bounds;

			tmp = DeflateRectangle(tmp, 2, 2);
			e.Graphics.DrawRectangle(pen2, tmp);

			tmp.Size = new Size(tmp.Width + 2, tmp.Height + 2);
			e.Graphics.DrawRectangle(pen1, tmp);
			e.Graphics.DrawRectangle(pen, this.Bounds);

			pen.Dispose();
		}

        /// <summary>
        /// Deflates the given rectangle by the given amount
        /// </summary>
        /// <param name="rct">The rectangle to convert</param>
        /// <param name="w">By what amount horiz.</param>
        /// <param name="h">By what amount vert.</param>
        /// <returns>New modified rectangle</returns>
        private Rectangle DeflateRectangle(Rectangle rct, int w, int h)
		{
			Rectangle retRct = new Rectangle(
				rct.X + w,
				rct.Y + h,
				rct.Width - (w * 2),
				rct.Height - (h * 2));
			return retRct;
		}

        /// <summary>
        /// Draw the Header portion of this controlProvides an interface to display Windows Forms dialog boxes or forms, and drop down list controls.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        private void header_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			string str = "Adorments";
			Font fnt = new Font("Microsoft Sans Serif", 10, FontStyle.Bold | FontStyle.Italic);
			SolidBrush br = new SolidBrush(Color.Black);

			LinearGradientBrush lgb = new LinearGradientBrush(label1.ClientRectangle, Color.Sienna, Color.Wheat, 65f, true);
			lgb.SetSigmaBellShape(.5f, 1f);

			e.Graphics.FillRectangle(lgb, label1.ClientRectangle);
			e.Graphics.DrawString(str, fnt, br, DeflateRectangle(label1.ClientRectangle, 2, 2));

			br.Dispose();
			lgb.Dispose();
			fnt.Dispose();
		}

        /// <summary>
        /// Handles the SizeChanged event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void label1_SizeChanged(object sender, System.EventArgs e)
		{
			label1.Width = this.Width;
		}
	}
}
