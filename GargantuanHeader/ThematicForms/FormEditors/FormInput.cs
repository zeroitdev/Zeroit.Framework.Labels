using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zeroit.Framework.FormThemes.UIThemes.ThematicForms.FormEditors
{
    [TypeConverter(typeof(FormInput.FormConverter))]
    [Editor(typeof(FormInputEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class FormInput
    {

        #region Private Fields


        private themes _themes = themes.Mumtz;

        private bool customTheme = true;

        private int shadowBlur = 10;
        private int shadowSpread = -5;
        private int shadowVertical = 0;
        private int shadowHorizontal = 0;
        private Color shadowColor = Color.Black;

        private bool activateShadow = false;

        #endregion
        
        #region Public Properties

        public bool CustomTheme
        {
            get { return customTheme; }
            set
            {
                customTheme = value;
            }
        }

        public themes Theme
        {
            get { return _themes; }
            set
            {
                _themes = value;

            }
        }

        public bool ActivateShadow
        {
            get { return activateShadow; }
            set
            {
                activateShadow = value;

            }
        }

        public int ShadowBlur
        {
            get { return shadowBlur; }
            set
            {
                shadowBlur = value;

            }
        }

        public int ShadowSpread
        {
            get { return shadowSpread; }
            set
            {
                shadowSpread = value;

            }
        }

        public int ShadowVertical
        {
            get { return shadowVertical; }
            set
            {
                shadowVertical = value;

            }
        }

        public int ShadowHorizontal
        {
            get { return shadowHorizontal; }
            set
            {
                shadowHorizontal = value;

            }
        }

        public Color ShadowColor
        {
            get { return shadowColor; }
            set
            {
                shadowColor = value;

            }
        }


        #endregion

        #region Constructor

        //Internal Constructor
        public FormInput(themes theme)
        {
            this._themes = theme;
        }

        /// <summary>
        /// Constructor for no input
        /// </summary>

        public FormInput() : this(themes.Spicylips)
        {
            
        }

        public FormInput(
            themes _themes, 
            bool customTheme,
            bool activateShadow,
            int shadowBlur,
            int shadowSpread,
            int shadowVertical,
            int shadowHorizontal,
            Color shadowColor
            
            ) :this(themes.Resizable)
        {
            this._themes = _themes;
            this.customTheme = customTheme;
            this.activateShadow = activateShadow;
            this.shadowBlur = shadowBlur;
            this.shadowSpread = shadowSpread;
            this.shadowVertical = shadowVertical;
            this.shadowHorizontal = shadowHorizontal;
            this.shadowColor = shadowColor;
            
        }

        #endregion

        #region Public Properties

        public FormInput Clone()
        {
            return new FormInput
            (
                _themes
            );
        }

        public static FormInput Empty()
        {
            return new FormInput();
        }

        #endregion


        #region Converter

        internal class FormConverter : TypeConverter
        {

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(InstanceDescriptor) || destinationType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertTo(context, destinationType);
            }

            // This code allows the designer to generate the Shape constructor

            public override object ConvertTo(ITypeDescriptorContext context,
                CultureInfo culture,
                object value,
                Type destinationType)
            {
                if (value is FormInput)
                {
                    if (destinationType == typeof(string))
                    {
                        // Display string in designer
                        return "(FormInput)";
                    }
                    
                    else if (destinationType == typeof(InstanceDescriptor))
                    {
                        FormInput formInput = (FormInput)value;

                        if (formInput.CustomTheme == true)
                        {
                            ConstructorInfo ctor = typeof(FormInput).GetConstructor(new Type[]
                            {
                                
                                typeof(themes),
                                typeof(bool),
                                typeof(bool),
                                typeof(int),
                                typeof(int),
                                typeof(int),
                                typeof(int),
                                typeof(Color)



                            });
                            if (ctor != null)
                            {
                                return new InstanceDescriptor(ctor, new object[] {
                                    
                                    formInput._themes,
                                    formInput.customTheme,
                                    formInput.activateShadow,
                                    formInput.shadowBlur,
                                    formInput.shadowSpread,
                                    formInput.shadowVertical,
                                    formInput.shadowHorizontal,
                                    formInput.shadowColor

                                });
                            }
                        }

                        
                        else
                        {
                            ConstructorInfo ctor = typeof(FormInput).GetConstructor(Type.EmptyTypes);
                            if (ctor != null)
                            {
                                return new InstanceDescriptor(ctor, null);
                            }
                        }
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }

        #endregion

    }
}
