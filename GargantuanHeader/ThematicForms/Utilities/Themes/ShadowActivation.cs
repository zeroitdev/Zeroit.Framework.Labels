using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zeroit.Framework.Form;
using Zeroit.Framework.FormThemes.Helper;

namespace Zeroit.Framework.FormThemes.UIThemes
{
    public partial class Thematic150WithEditor
    {


        private void OnParentLoad()
        {
            //if (!DesignMode)
            //    Parent.FindForm().Load += ParentForm_Load;

            
        }

        //private int shadowBlur = 10;
        //private int shadowSpread = -5;
        //private int shadowVertical = 0;
        //private int shadowHorizontal = 0;
        //private Color shadowColor = Color.Black;

        private bool activateShadow = false;

        public bool ActivateShadow
        {
            get { return formInput.ActivateShadow; }
            set
            {
                formInput.ActivateShadow = value;
                Invalidate();
            }
        }

        [Category("Shadow Properties")]
        public int ShadowBlur
        {
            get { return formInput.ShadowBlur; }
            set
            {
                formInput.ShadowBlur = value;
                Invalidate();
            }
        }

        [Category("Shadow Properties")]
        public int ShadowSpread
        {
            get { return formInput.ShadowSpread; }
            set
            {
                formInput.ShadowSpread = value;
                Invalidate();
            }
        }

        [Category("Shadow Properties")]
        public int ShadowVertical
        {
            get { return formInput.ShadowVertical; }
            set
            {
                formInput.ShadowVertical = value;
                Invalidate();
            }
        }

        [Category("Shadow Properties")]
        public int ShadowHorizontal
        {
            get { return formInput.ShadowHorizontal; }
            set
            {
                formInput.ShadowHorizontal = value;
                Invalidate();
            }
        }

        [Category("Shadow Properties")]
        public Color ShadowColor
        {
            get { return formInput.ShadowColor; }
            set
            {
                formInput.ShadowColor = value;
                Invalidate();
            }
        }


        //ZeroitDropshadow AddShadow = null;
        public void Shadow(Control control)
        {

            ZeroitDropshadow AddShadow = new ZeroitDropshadow((System.Windows.Forms.Form)control);
            AddShadow.ShadowBlur = ShadowBlur;
            AddShadow.ShadowSpread = ShadowSpread;
            AddShadow.ShadowV = ShadowVertical;
            AddShadow.ShadowH = ShadowHorizontal;
            AddShadow.ShadowColor = ShadowColor;

            if (ActivateShadow)
            {
                AddShadow.ActivateShadow();
            }
            
        }

        
        
    }
}
