﻿// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 12-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="SmartTag.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
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
    /// A class collection for rendering a label with nice features.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    //[Designer(typeof(UltraRotateDesigner))]
    public partial class ZeroitUltraRotate : Control
    {

        
        #region Smart Tag Code

        #region Cut and Paste it on top of the component class

        //--------------- [Designer(typeof(UltraRotateDesigner))] --------------------//
        #endregion

        #region ControlDesigner
        /// <summary>
        /// Class UltraRotateDesigner.
        /// </summary>
        /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
        public class UltraRotateDesigner : System.Windows.Forms.Design.ControlDesigner
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
                        actionLists.Add(new ZeroitUltraRotate.ZeroitUltraRotateSmartTagActionList(this.Component));
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
        /// Class ZeroitUltraRotateSmartTagActionList.
        /// </summary>
        /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
        public class ZeroitUltraRotateSmartTagActionList : System.ComponentModel.Design.DesignerActionList
        {
            //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
            /// <summary>
            /// The col user control
            /// </summary>
            private ZeroitUltraRotate colUserControl;


            /// <summary>
            /// The designer action UI SVC
            /// </summary>
            private DesignerActionUIService designerActionUISvc = null;


            /// <summary>
            /// Initializes a new instance of the <see cref="ZeroitUltraRotate.ZeroitUltraRotateSmartTagActionList"/> class.
            /// </summary>
            /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
            public ZeroitUltraRotateSmartTagActionList(IComponent component) : base(component)
            {
                this.colUserControl = component as ZeroitUltraRotate;

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

            #region Properties
            //Create all of the properties we want the control to have and Override the ones it already has if they need to be used for special reasons. 


            /// <summary>
            /// Gets or sets a value indicating whether [show Text shadow].
            /// </summary>
            /// <value><c>true</c> if [show Text shadow]; otherwise, <c>false</c>.</value>
            public bool ShowTextShadow
            {
                get
                {
                    return colUserControl.ShowTextShadow;
                }
                set
                {
                    GetPropertyByName("ShowTextShadow").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the image.
            /// </summary>
            /// <value>The image.</value>
            public Bitmap Image
            {
                get
                {
                    return colUserControl.Image;
                }
                set
                {
                    GetPropertyByName("Image").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the Text pattern image.
            /// </summary>
            /// <value>The Text pattern image.</value>
            public Bitmap TextPatternImage
            {
                get
                {
                    return colUserControl.TextPatternImage;
                }
                set
                {
                    GetPropertyByName("TextPatternImage").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the color of the outline.
            /// </summary>
            /// <value>The color of the outline.</value>
            public Color OutlineColor
            {
                get
                {
                    return colUserControl.OutlineColor;
                }
                set
                {
                    GetPropertyByName("OutlineColor").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the color of the border.
            /// </summary>
            /// <value>The color of the border.</value>
            public Color BorderColor
            {
                get
                {
                    return colUserControl.BorderColor;
                }
                set
                {
                    GetPropertyByName("BorderColor").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the color of the shadow.
            /// </summary>
            /// <value>The color of the shadow.</value>
            public Color ShadowColor
            {
                get
                {
                    return colUserControl.ShadowColor;
                }
                set
                {
                    GetPropertyByName("ShadowColor").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the border style.
            /// </summary>
            /// <value>The border style.</value>
            public ZeroitUltraRotate.BorderType BorderStyle
            {
                get
                {
                    return colUserControl.BorderStyle;
                }
                set
                {
                    GetPropertyByName("BorderStyle").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the Text align.
            /// </summary>
            /// <value>The Text align.</value>
            public ContentAlignment TextAlign
            {
                get
                {
                    return colUserControl.TextAlign;
                }
                set
                {
                    GetPropertyByName("TextAlign").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the image align.
            /// </summary>
            /// <value>The image align.</value>
            public ContentAlignment ImageAlign
            {
                get
                {
                    return colUserControl.ImageAlign;
                }
                set
                {
                    GetPropertyByName("ImageAlign").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the Text pattern image layout.
            /// </summary>
            /// <value>The Text pattern image layout.</value>
            public ZeroitUltraRotate.PatternLayout TextPatternImageLayout
            {
                get
                {
                    return colUserControl.TextPatternImageLayout;
                }
                set
                {
                    GetPropertyByName("TextPatternImageLayout").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the shadow position.
            /// </summary>
            /// <value>The shadow position.</value>
            public ZeroitUltraRotate.ShadowArea ShadowPosition
            {
                get
                {
                    return colUserControl.ShadowPosition;
                }
                set
                {
                    GetPropertyByName("ShadowPosition").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the shadow style.
            /// </summary>
            /// <value>The shadow style.</value>
            public ZeroitUltraRotate.ShadowDrawingType ShadowStyle
            {
                get
                {
                    return colUserControl.ShadowStyle;
                }
                set
                {
                    GetPropertyByName("ShadowStyle").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the fore color transparency.
            /// </summary>
            /// <value>The fore color transparency.</value>
            public int ForeColorTransparency
            {
                get
                {
                    return colUserControl.ForeColorTransparency;
                }
                set
                {
                    GetPropertyByName("ForeColorTransparency").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the outline thickness.
            /// </summary>
            /// <value>The outline thickness.</value>
            public int OutlineThickness
            {
                get
                {
                    return colUserControl.OutlineThickness;
                }
                set
                {
                    GetPropertyByName("OutlineThickness").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the shadow depth.
            /// </summary>
            /// <value>The shadow depth.</value>
            public int ShadowDepth
            {
                get
                {
                    return colUserControl.ShadowDepth;
                }
                set
                {
                    GetPropertyByName("ShadowDepth").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the shadow transparency.
            /// </summary>
            /// <value>The shadow transparency.</value>
            public int ShadowTransparency
            {
                get
                {
                    return colUserControl.ShadowTransparency;
                }
                set
                {
                    GetPropertyByName("ShadowTransparency").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the Text.
            /// </summary>
            /// <value>The Text.</value>
            public string Text
            {
                get
                {
                    return colUserControl.Text;
                }
                set
                {
                    GetPropertyByName("Text").SetValue(colUserControl, value);
                }
            }


            /// <summary>
            /// Gets or sets the width of the correct.
            /// </summary>
            /// <value>The width of the correct.</value>
            public int CorrectWidth
            {
                get
                {
                    return colUserControl.CorrectWidth;
                }
                set
                {
                    GetPropertyByName("CorrectWidth").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the sliding limit.
            /// </summary>
            /// <value>The sliding limit.</value>
            public int SlidingLimit
            {
                get
                {
                    return colUserControl.SlidingLimit;
                }
                set
                {
                    GetPropertyByName("SlidingLimit").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="ZeroitUltraRotate.ZeroitUltraRotateSmartTagActionList"/> is slide.
            /// </summary>
            /// <value><c>true</c> if slide; otherwise, <c>false</c>.</value>
            public bool Slide
            {
                get
                {
                    return colUserControl.Slide;
                }
                set
                {
                    GetPropertyByName("Slide").SetValue(colUserControl, value);
                }
            }

            /// <summary>
            /// Gets or sets the sliding speed.
            /// </summary>
            /// <value>The sliding speed.</value>
            public int SlidingSpeed
            {
                get
                {
                    return colUserControl.SlidingSpeed;
                }
                set
                {
                    GetPropertyByName("SlidingSpeed").SetValue(colUserControl, value);
                }
            }

            public double RotationAngle
            {
                get
                {
                    return colUserControl.RotationAngle;
                }
                set
                {
                    GetPropertyByName("RotationAngle").SetValue(colUserControl, value);
                }
            }

            public Orientation TextOrientation
            {
                get
                {
                    return colUserControl.TextOrientation;
                }
                set
                {
                    GetPropertyByName("TextOrientation").SetValue(colUserControl, value);
                }
            }

            public Direction TextDirection
            {
                get
                {
                    return colUserControl.TextDirection;
                }
                set
                {
                    GetPropertyByName("TextDirection").SetValue(colUserControl, value);
                }
            }

            #endregion

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
                items.Add(new DesignerActionHeaderItem("Rotation"));


                items.Add(new DesignerActionPropertyItem("TextOrientation",
                    "Text Orientation", "Rotation",
                    "Sets the text orientation."));

                items.Add(new DesignerActionPropertyItem("TextDirection",
                    "Text Direction", "Rotation",
                    "Sets the text direction."));

                items.Add(new DesignerActionPropertyItem("RotationAngle",
                    "Rotation Angle", "Rotation",
                    "Sets the rotation angle."));


                //Define static section header entries.
                items.Add(new DesignerActionHeaderItem("Appearance"));

                items.Add(new DesignerActionPropertyItem("ShowTextShadow",
                                    "Show Text Shadow", "Appearance",
                                    "Set to show Text shadow."));

                items.Add(new DesignerActionPropertyItem("BackColor",
                                     "Back Color", "Appearance",
                                     "Sets the background color."));

                items.Add(new DesignerActionPropertyItem("ForeColor",
                                     "Fore Color", "Appearance",
                                     "Sets the foreground color."));

                items.Add(new DesignerActionPropertyItem("OutlineColor",
                                    "Outline Color", "Appearance",
                                    "Sets the outline color."));

                items.Add(new DesignerActionPropertyItem("BorderColor",
                                    "Border Color", "Appearance",
                                    "Sets the border color."));

                items.Add(new DesignerActionPropertyItem("ShadowColor",
                                    "Shadow Color", "Appearance",
                                    "Sets the shadow color."));

                items.Add(new DesignerActionPropertyItem("Image",
                                     "Image", "Appearance",
                                     "Sets the image."));

                items.Add(new DesignerActionPropertyItem("TextPatternImage",
                                    "Text Pattern Image", "Appearance",
                                    "Sets the Text pattern image."));


                items.Add(new DesignerActionPropertyItem("BorderStyle",
                                    "Border Style", "Appearance",
                                    "Sets the border style."));

                items.Add(new DesignerActionPropertyItem("TextAlign",
                                    "Text Align", "Appearance",
                                    "Sets the alignment of the Text."));

                items.Add(new DesignerActionPropertyItem("ImageAlign",
                                    "Image Align", "Appearance",
                                    "Sets the alignment of the image."));

                items.Add(new DesignerActionPropertyItem("TextPatternImageLayout",
                                    "Text Pattern Image Layout", "Appearance",
                                    "Sets the Text pattern image layout."));

                items.Add(new DesignerActionPropertyItem("ShadowPosition",
                                    "Shadow Position", "Appearance",
                                    "Sets the shadow position."));

                items.Add(new DesignerActionPropertyItem("ShadowStyle",
                                    "Shadow Style", "Appearance",
                                    "Sets the shadow style."));

                items.Add(new DesignerActionPropertyItem("ForeColorTransparency",
                                    "Fore Color Transparency", "Appearance",
                                    "Sets the forecolor transparency."));


                items.Add(new DesignerActionPropertyItem("OutlineThickness",
                                    "Outline Thickness", "Appearance",
                                    "Sets the outline thickness."));

                items.Add(new DesignerActionPropertyItem("ShadowDepth",
                                    "Shadow Depth", "Appearance",
                                    "Sets the shadow depth."));

                items.Add(new DesignerActionPropertyItem("ShadowTransparency",
                                    "Shadow Transparency", "Appearance",
                                    "Sets the shadow transparency."));

                items.Add(new DesignerActionPropertyItem("Text",
                    "Text", "Appearance",
                    "Sets the Text of the control."));

                //Define static section header entries.
                items.Add(new DesignerActionHeaderItem("Sliding"));


                items.Add(new DesignerActionPropertyItem("Slide",
                                    "Slide", "Sliding",
                                    "Sets whether the Text should have a marquee effect."));

                items.Add(new DesignerActionPropertyItem("CorrectWidth",
                                    "Correct Width", "Sliding",
                                    "Sets the shadow transparency."));

                items.Add(new DesignerActionPropertyItem("SlidingLimit",
                                    "Sliding Limit", "Sliding",
                                    "Sets the shadow transparency."));

                items.Add(new DesignerActionPropertyItem("SlidingSpeed",
                                    "Sliding Speed", "Sliding",
                                    "Sets the sliding sliding speed."));

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
}
