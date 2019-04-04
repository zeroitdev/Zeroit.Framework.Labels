// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 04-26-2018
// ***********************************************************************
// <copyright file="EarAdornmentTypeEditor.cs" company="Zeroit Dev Technologies">
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
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace Zeroit.Framework.Labels.Headers
{
    /// <summary>
    /// Summary description for EarAdornmentTypeEditor.
    /// </summary>
    /// <seealso cref="System.Drawing.Design.UITypeEditor" />
    public class EarAdornmentTypeEditor : UITypeEditor
	{
        /// <summary>
        /// Return Editor Style (none (default), drop down or modal.
        /// DropDown and Modal are similar in functionality and
        /// invocation.
        /// </summary>
        /// <param name="context">Provides component context</param>
        /// <returns>Editor Style, in this case DropDown</returns>
        public override UITypeEditorEditStyle GetEditStyle(
			ITypeDescriptorContext context)
		{
            if (context != null)
				return UITypeEditorEditStyle.DropDown;
			return base.GetEditStyle(context);
		}

        /// <summary>
        /// Edits the specified object's value using the editor style indicated by the <see cref="M:System.Drawing.Design.UITypeEditor.GetEditStyle" /> method.
        /// </summary>
        /// <param name="context">Component context</param>
        /// <param name="provider">Provides custom support to other objects</param>
        /// <param name="value">Property value</param>
        /// <returns>New/Modified property value</returns>
        public override object EditValue(
			ITypeDescriptorContext context,
			IServiceProvider provider,
			object value)
		{
			if ((context != null) && (provider != null))
			{
				IWindowsFormsEditorService editorService = (IWindowsFormsEditorService)
					provider.GetService(typeof(IWindowsFormsEditorService));
				
				if (editorService != null)
				{
					EarUIEditor dropDownEditor = new EarUIEditor(editorService);
					//Set initial value in editor for this property
					dropDownEditor.TypeOfEar = (EarTypes) value;
					//Show our EarUIEditor
					editorService.DropDownControl(dropDownEditor);
					//Return with the new/undated property value.
					return dropDownEditor.TypeOfEar;
				}
			}
			return base.EditValue(context, provider, value);
		}
	}
}
