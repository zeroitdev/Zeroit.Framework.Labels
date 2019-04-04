// ***********************************************************************
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


        private void Workaround(PaintEventArgs e)
        {
            ////If the ShowTextShadow property is set to true then draw the shadow
            //if (_ShowTextShadow)
            //{
            //    switch (TextOrientation)
            //    {
            //        case Orientation.Arc:
            //            float arcAngle = (2 * width / radius) / Text.Length;
            //            for (int i = 0; i < Text.Length; i++)
            //            {
            //                //Use the ShadowPosition property to set the Graphics.TranslateTransform to draw the
            //                //shadow at the correct offset position.
            //                if (_ShadowPosition == ShadowArea.TopLeft)
            //                {
            //                    if (textDirection == Direction.Clockwise)
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);

            //                    }
            //                    else
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                        //pth.Reverse();
            //                    }


            //                }
            //                else if (_ShadowPosition == ShadowArea.TopRight)
            //                {
            //                    if (textDirection == Direction.Clockwise)
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);

            //                    }
            //                    else
            //                    {


            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                        //pth.Reverse();
            //                    }


            //                }
            //                else if (_ShadowPosition == ShadowArea.BottomLeft)
            //                {
            //                    if (textDirection == Direction.Clockwise)
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                pathSize), sf);

            //                    }
            //                    else
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                        //pth.Reverse();

            //                    }



            //                }
            //                else
            //                {

            //                    if (textDirection == Direction.Clockwise)
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                    }
            //                    else
            //                    {
            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                pathSize), sf);

            //                    }


            //                }


            //            }

            //            break;
            //        case Orientation.Circle:
            //            for (int i = 0; i < Text.Length; i++)
            //            {
            //                if (_ShadowPosition == ShadowArea.TopLeft)
            //                {
            //                    if (textDirection == Direction.Clockwise)
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                    }
            //                    else
            //                    {
            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                        //pth.Reverse();

            //                    }

            //                }
            //                else if (_ShadowPosition == ShadowArea.TopRight)
            //                {

            //                    if (textDirection == Direction.Clockwise)
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                    }
            //                    else
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                        //pth.Reverse();
            //                    }

            //                }
            //                else if (_ShadowPosition == ShadowArea.BottomLeft)
            //                {
            //                    if (textDirection == Direction.Clockwise)
            //                    {

            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                    }
            //                    else
            //                    {
            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
            //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                        //pth.Reverse();

            //                    }


            //                }
            //                else
            //                {
            //                    if (textDirection == Direction.Clockwise)
            //                    {
            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                             Convert.ToInt32(this.Font.Style),
            //                             Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                             new Rectangle(
            //                                 (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                 (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                 pathSize), sf);
            //                    }
            //                    else
            //                    {
            //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
            //                            Convert.ToInt32(this.Font.Style),
            //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
            //                            new Rectangle(
            //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
            //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
            //                                pathSize), sf);
            //                        //pth.Reverse();

            //                    }


            //                }
            //            }
            //            break;
            //        case Orientation.Rotate:

            //            if (_ShadowPosition == ShadowArea.TopLeft)
            //            {

            //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
            //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
            //                    new Rectangle(
            //                        this.Padding.Left,
            //                        this.Padding.Top,
            //                        (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
            //                        (this.ClientSize.Height - 1) -
            //                        (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);

            //            }
            //            else if (_ShadowPosition == ShadowArea.TopRight)
            //            {

            //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
            //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
            //                    new Rectangle(
            //                        this.Padding.Left,
            //                        this.Padding.Top,
            //                        (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) + _ShadowDepth,
            //                        (this.ClientSize.Height - 1) -
            //                        (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);

            //            }
            //            else if (_ShadowPosition == ShadowArea.BottomLeft)
            //            {

            //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
            //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
            //                    new Rectangle(
            //                        this.Padding.Left,
            //                        this.Padding.Top,
            //                        (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
            //                        (this.ClientSize.Height - 1) -
            //                        (this.Padding.Top + this.Padding.Bottom) + _ShadowDepth), sf);
            //            }
            //            else
            //            {

            //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
            //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
            //                    new Rectangle(
            //                        this.Padding.Left,
            //                        this.Padding.Top,
            //                        (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
            //                        (this.ClientSize.Height - 1) -
            //                        (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);
            //            }

            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }

                //if (_ShadowStyle == ShadowDrawingType.DrawShadow)
                //{
                //    switch (textOrientation)
                //    {
                //        case Orientation.Arc:
                //            {
                //                //Arc angle must be get from the length of the Text.
                //                float arcAngle = (2 * width / radius) / Text.Length;
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();

                //                    }
                //                }
                //                break;
                //            }
                //        case Orientation.Circle:
                //            {
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {
                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();
                //                    }

                //                }
                //                break;
                //            }
                //        case Orientation.Rotate:
                //            {

                //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                //                    new Rectangle(
                //                        this.Padding.Left,
                //                        this.Padding.Top,
                //                        (ClientRectangle.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
                //                        (ClientRectangle.Height - 1) - (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);

                //                break;
                //            }
                //    }

                //    //Draw the Drawing2D.GraphicsPath with the _ShadowPen that is set to the ShadowColor having the ShadowTransparency
                //    //_with1.DrawPath(_ShadowPen, pth);
                //    //Draws the shadow
                //}
                //else
                //{

                //    switch (textOrientation)
                //    {
                //        case Orientation.Arc:
                //            {
                //                //Arc angle must be get from the length of the Text.
                //                float arcAngle = (2 * width / radius) / Text.Length;
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);

                //                    }
                //                }
                //                break;
                //            }
                //        case Orientation.Circle:
                //            {
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);

                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                //                                (int)(float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))), pathSize,
                //                                pathSize), sf);
                //                    }

                //                }
                //                break;
                //            }
                //        case Orientation.Rotate:
                //            {
                //                //For rotation, who about rotation?

                //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                //                    new Rectangle(
                //                        this.Padding.Left,
                //                        this.Padding.Top,
                //                        (ClientRectangle.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
                //                        (ClientRectangle.Height - 1) -
                //                        (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);

                //                break;
                //            }
                //    }

                //    //Fill the Drawing2D.GraphicsPath with the _ShadowBrush that is set to the ShadowColor having the ShadowTransparency
                //    //_with1.FillPath(_ShadowBrush, pth);
                //    //Draws the shadow
                //}


                //Now use the Graphics.TranslateTransform to shift the _with1 back in the opposite
                //direction before Drawing and Filling the Drawing2D.GraphicsPath again with Text colors
                //if (_ShadowPosition == ShadowArea.TopLeft)
                //{

                //    switch (textOrientation)
                //    {
                //        case Orientation.Arc:
                //            {
                //                //Arc angle must be get from the length of the Text.
                //                float arcAngle = (2 * width / radius) / Text.Length;
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {



                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();
                //                    }
                //                }
                //                break;
                //            }
                //        case Orientation.Circle:
                //            {
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();
                //                    }

                //                }
                //                break;
                //            }
                //        case Orientation.Rotate:
                //            {
                //                //For rotation, who about rotation?
                //                double angle = (rotationAngle / 180) * Math.PI;
                //                //_with1.TranslateTransform(
                //                //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) + (_ShadowDepth * 2),
                //                //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) + (_ShadowDepth * 2));
                //                //_with1.RotateTransform((float)rotationAngle);
                //                //_with1.FillPath(_ShadowBrush, pth);
                //                //_with1.ResetTransform();

                //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                //                    new Rectangle(
                //                        this.Padding.Left,
                //                        this.Padding.Top,
                //                        (ClientRectangle.Width - 1) - (this.Padding.Left + this.Padding.Right) + (_ShadowDepth * 2),
                //                        (ClientRectangle.Height - 1) - (this.Padding.Top + this.Padding.Bottom) + (_ShadowDepth * 2)), sf);


                //                break;
                //            }
                //    }

                //    //_with1.TranslateTransform(+(_ShadowDepth * 2), +(_ShadowDepth * 2));
                //}
                //else if (_ShadowPosition == ShadowArea.TopRight)
                //{

                //    switch (textOrientation)
                //    {
                //        case Orientation.Arc:
                //            {
                //                //Arc angle must be get from the length of the Text.
                //                float arcAngle = (2 * width / radius) / Text.Length;
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);

                //                    }
                //                }
                //                break;
                //            }
                //        case Orientation.Circle:
                //            {
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);

                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();
                //                    }

                //                }
                //                break;
                //            }
                //        case Orientation.Rotate:
                //            {
                //                //For rotation, who about rotation?
                //                double angle = (rotationAngle / 180) * Math.PI;
                //                //_with1.TranslateTransform(
                //                //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) - (_ShadowDepth * 2),
                //                //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) + (_ShadowDepth * 2));
                //                //_with1.RotateTransform((float)rotationAngle);
                //                //_with1.FillPath(_ShadowBrush, pth);
                //                //_with1.ResetTransform();

                //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                //                    new Rectangle(
                //                        this.Padding.Left,
                //                        this.Padding.Top,
                //                        (ClientRectangle.Width - 1) - (this.Padding.Left + this.Padding.Right) - (_ShadowDepth * 2),
                //                        (ClientRectangle.Height - 1) - (this.Padding.Top + this.Padding.Bottom) + (_ShadowDepth * 2)), sf);


                //                break;
                //            }
                //    }

                //    //_with1.TranslateTransform(-(_ShadowDepth * 2), +(_ShadowDepth * 2));
                //}
                //else if (_ShadowPosition == ShadowArea.BottomLeft)
                //{
                //    switch (textOrientation)
                //    {
                //        case Orientation.Arc:
                //            {
                //                //Arc angle must be get from the length of the Text.
                //                float arcAngle = (2 * width / radius) / Text.Length;
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();
                //                    }
                //                }
                //                break;
                //            }
                //        case Orientation.Circle:
                //            {
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 + Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();
                //                    }

                //                }
                //                break;
                //            }
                //        case Orientation.Rotate:
                //            {
                //                //For rotation, who about rotation?
                //                double angle = (rotationAngle / 180) * Math.PI;
                //                //_with1.TranslateTransform(
                //                //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) + (_ShadowDepth * 2),
                //                //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) - (_ShadowDepth * 2));
                //                //_with1.RotateTransform((float)rotationAngle);
                //                //_with1.FillPath(_ShadowBrush, pth);
                //                //_with1.ResetTransform();

                //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                //                    new Rectangle(
                //                        this.Padding.Left,
                //                        this.Padding.Top,
                //                        (ClientRectangle.Width - 1) - (this.Padding.Left + this.Padding.Right) + (_ShadowDepth * 2),
                //                        (ClientRectangle.Height - 1) - (this.Padding.Top + this.Padding.Bottom) - (_ShadowDepth * 2)), sf);


                //                break;
                //            }
                //    }
                //    //_with1.TranslateTransform(+(_ShadowDepth * 2), -(_ShadowDepth * 2));
                //}
                //else
                //{
                //    switch (textOrientation)
                //    {
                //        case Orientation.Arc:
                //            {
                //                //Arc angle must be get from the length of the Text.
                //                float arcAngle = (2 * width / radius) / Text.Length;
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {


                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();
                //                    }
                //                }
                //                break;
                //            }
                //        case Orientation.Circle:
                //            {
                //                if (textDirection == Direction.Clockwise)
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);

                //                    }
                //                }
                //                else
                //                {
                //                    for (int i = 0; i < Text.Length; i++)
                //                    {

                //                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                //                            Convert.ToInt32(this.Font.Style),
                //                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                //                            new Rectangle(
                //                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                //                                (int)(float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2), pathSize,
                //                                pathSize), sf);
                //                        //pth.Reverse();

                //                    }

                //                }
                //                break;
                //            }
                //        case Orientation.Rotate:
                //            {
                //                //For rotation, who about rotation?
                //                double angle = (rotationAngle / 180) * Math.PI;
                //                //_with1.TranslateTransform(
                //                //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) - (_ShadowDepth * 2),
                //                //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) - (_ShadowDepth * 2));
                //                //_with1.RotateTransform((float)rotationAngle);
                //                //_with1.FillPath(_ShadowBrush, pth);
                //                //_with1.ResetTransform();

                //                pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                //                    Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                //                    new Rectangle(
                //                        this.Padding.Left,
                //                        this.Padding.Top,
                //                        (ClientRectangle.Width - 1) - (this.Padding.Left + this.Padding.Right) - (_ShadowDepth * 2),
                //                        (ClientRectangle.Height - 1) - (this.Padding.Top + this.Padding.Bottom) - (_ShadowDepth * 2)), sf);


                //                break;
                //            }
                //    }
                //    //_with1.TranslateTransform(-(_ShadowDepth * 2), -(_ShadowDepth * 2));
                //}
            //}



        }


    }
}
