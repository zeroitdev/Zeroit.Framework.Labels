using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zeroit.Framework.Form;
using Zeroit.Framework.FormThemes.Helper;

namespace Zeroit.Framework.FormThemes.UIThemes.ThematicForms.FormEditors
{
    public partial class FormEditorsDialog : System.Windows.Forms.Form
    {
        #region Constructor

        /// <summary>
        ///		Initializes a new instance of <c>FillerEditorDialog</c> using an empty <c>Filler</c>
        /// 	at the default window position.
        /// </summary>
        public FormEditorsDialog() : this(FormInput.Empty())
        {
        }

        /// <summary>
        ///		Initializes a new instance of <c>FillerEditorDialog</c> using an empty <c>Filler</c>
        ///		and positioned beneath the specified control.
        /// </summary>
        /// <param name="c">Control beneath which the dialog should be placed.</param>
        public FormEditorsDialog(Control c) : this(FormInput.Empty(), c)
        {
        }

        /// <summary>
        ///		Initializes a new instance of <c>FillerEditorDialog</c> using an existing <c>Filler</c>
        ///		and positioned beneath the specified control.
        /// </summary>
        /// <param name="filler">Existing <c>Filler</c> object.</param>
        /// <param name="c">Control beneath which the dialog should be placed.</param>
        /// <exception cref="System.ArgumentNullException">
        ///		Thrown if <paramref name="filler" /> is null.
        ///	</exception>
        public FormEditorsDialog(FormInput formInput, Control c) : this(formInput)
        {
            Zeroit.Framework.FormThemes.UIThemes.ThematicForms.FormEditors.Utilities.Draw.SetStartPositionBelowControl(this, c);
        }

        /// <summary>
        /// 	Initializes a new instance of <c>FillerEditorDialog</c> using an existing <c>Filler</c>
        /// 	at the default window position.
        /// </summary>
        /// <param name="peaceInput">Existing <c>Filler</c> object.</param>
        /// <exception cref="System.ArgumentNullException">
        ///		Thrown if <paramref name="peaceInput" /> is null.
        ///	</exception>
        public FormEditorsDialog(FormInput formInput)
        {
            if (formInput == null)
            {
                throw new ArgumentNullException("formInput");
            }


            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            Set_Initial_Values(formInput);

            //AdjustDialogSize();
            //SetInitial_Values(pizaroAnimatorInput);

        }


        #endregion

        #region Public Properties

        private FormInput formInput;

        public FormInput FormInput
        {
            get { return formInput; }
        }


        #endregion


        #region Private Events

        private void Set_Initial_Values(FormInput formInput)
        {


            #region Add Enum to ComboBox
            // get a list of member names from EasingFunctionTypes enum,
            // figure out the numeric value, and display
            foreach (string volume in Enum.GetNames(typeof(themes)))
            {
                main_formType_ComboBox.Items.Add(volume);

            }

            for (int i = 0; i < main_formType_ComboBox.Items.Count; i++)
            {
                if (formInput.Theme == (themes)Enum.Parse(typeof(themes), main_formType_ComboBox.Items[i].ToString()))
                {
                    main_formType_ComboBox.SelectedIndex = i;

                }

            }
            #endregion

            if (formInput.CustomTheme == true)
            {
                main_Custom_Yes_RadioBtn.Checked = true;
                main_Custom_No_RadioBtn.Checked = false;

            }
            else
            {
                main_Custom_Yes_RadioBtn.Checked = false;
                main_Custom_No_RadioBtn.Checked = true;
            }

            if (formInput.ActivateShadow)
            {

                Helper.ZeroitDropshadow AddShadow = new Helper.ZeroitDropshadow(this);
                AddShadow.ShadowBlur = formInput.ShadowBlur;
                AddShadow.ShadowSpread = formInput.ShadowSpread;
                AddShadow.ShadowV = formInput.ShadowVertical;
                AddShadow.ShadowH = formInput.ShadowHorizontal;
                AddShadow.ShadowColor = formInput.ShadowColor;
                AddShadow.ActivateShadow();

                main_Shadow_Yes_RadioBtn.Checked = true;

                
            }

            blur_Numeric.Value = formInput.ShadowBlur;
            spread_Numeric.Value = formInput.ShadowSpread;
            vertical_Numeric.Value = formInput.ShadowVertical;
            horizontal_Numeric.Value = formInput.ShadowHorizontal;
            colorSelector_Btn.BackColor = formInput.ShadowColor;

        }

        private void Set_Retrieved_Values(FormInput formInput)
        {
            formInput.Theme = 
                (themes)Enum.Parse(
                typeof(themes),
                main_formType_ComboBox.SelectedItem.ToString());

            formInput.ShadowBlur = (int)blur_Numeric.Value;
            formInput.ShadowSpread = (int)spread_Numeric.Value;
            formInput.ShadowVertical = (int)vertical_Numeric.Value;
            formInput.ShadowHorizontal = (int)horizontal_Numeric.Value;
            formInput.ShadowColor = colorSelector_Btn.BackColor;

            if (main_Shadow_Yes_RadioBtn.Checked)
            {
                formInput.ActivateShadow = true;
            }

            else
            {
                formInput.ActivateShadow = false;
            }


        }

        private void FormChanged(object sender, EventArgs e)
        {
            formPreviewer.Theme = (themes)main_formType_ComboBox.SelectedIndex;

        }



        #endregion

        private void shadowColor_Btn_Click(object sender, EventArgs e)
        {
            if (colorSelector.ShowDialog() == DialogResult.OK)
            {
                shadowColor_Btn.BackColor = colorSelector.Color;
            }
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (main_Custom_Yes_RadioBtn.Checked)
            {
                formInput = new FormInput(themes.Spicylips, true,false, 10, -5, 0, 0, Color.Black);

                Set_Retrieved_Values(formInput);
            }
            else
            {
                formInput = new FormInput(themes.Spicylips, true, false, 10, -5, 0, 0, Color.Black);

                Set_Retrieved_Values(formInput);
            }

            DialogResult = DialogResult.OK;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void main_Shadow_Yes_RadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (main_Shadow_Yes_RadioBtn.Checked)
            {
                shadow_GroupBox.Visible = true;
            }
        }

        private void colorSelector_Btn_Click(object sender, EventArgs e)
        {
            if (colorSelector.ShowDialog() == DialogResult.OK)
            {
                colorSelector_Btn.BackColor = colorSelector.Color;
            }
        }

        private void main_Shadow_No_RadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (main_Shadow_No_RadioBtn.Checked)
            {
                shadow_GroupBox.Visible = false;
            }
            else
            {
                shadow_GroupBox.Visible = true;
            }
        }
    }
}
