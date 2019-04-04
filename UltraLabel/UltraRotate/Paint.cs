// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 12-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="Paint.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        
        
        #region Overrides and Private Methods
        //Use the OnPaint overrides sub to paint the control to match how all the properties settings have been set by the user
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        private void GraphicsOnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            var _with1 = e.Graphics;
            _with1.TextRenderingHint = TextRendering;

            #region RotationCode
            //Getting the width and height of the Text, which we are going to write
            float width = _with1.MeasureString(Text, this.Font).Width;
            float height = _with1.MeasureString(Text, this.Font).Height;

            //The radius is set to 0.9 of the width or height, b'cos not to 
            //hide and part of the Text at any stage
            float radius = 0f;
            if (ClientRectangle.Width < ClientRectangle.Height)
            {
                radius = ClientRectangle.Width * 0.9f / 2;
            }
            else
            {
                radius = ClientRectangle.Height * 0.9f / 2;
            }

            #endregion

            //Fill the background with the BackColor color
            _with1.FillRectangle(_BackgroundBrush, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height));

            //If the BackgroundImage property has been set to an image then draw the BackgroundImage
            if (this.BackgroundImage != null)
            {
                DrawBackImage(e.Graphics);
            }

            //If the Image property has been set to an image then draw the image on the control
            if (_Image != null)
            {
                _with1.DrawImage(_Image, AlignImage(new Rectangle(0, 0, this.Width - 1, this.Height - 1)));
            }

            //If the Text property has bet assigned any Text then draw the Text on the control
            if (!string.IsNullOrEmpty(this.Text))
            {
                //Set the smothing mode of the _with1 to make things look smother
                //_with1.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias;
                _with1.SmoothingMode = Smoothing;

                //The Drawing2D.GraphicsPath used for drawing and/or filling the Text
                using (GraphicsPath pth = new GraphicsPath())
                {

                    //If the ShowTextShadow property is set to true then draw the shadow
                    if (_ShowTextShadow)
                    {
                        switch (TextOrientation)
                        {
                            case Orientation.Arc:
                                float arcAngle = (2 * width / radius) / Text.Length;
                                for (int i = 0; i < Text.Length; i++)
                                {
                                    //Use the ShadowPosition property to set the Graphics.TranslateTransform to draw the
                                    //shadow at the correct offset position.
                                    if (_ShadowPosition == ShadowArea.TopLeft)
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }


                                    }
                                    else if (_ShadowPosition == ShadowArea.TopRight)
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }


                                    }
                                    else if (_ShadowPosition == ShadowArea.BottomLeft)
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }



                                    }
                                    else
                                    {

                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }


                                    }


                                }

                                break;
                            case Orientation.Circle:
                                for (int i = 0; i < Text.Length; i++)
                                {
                                    if (_ShadowPosition == ShadowArea.TopLeft)
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();
                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }

                                    }
                                    else if (_ShadowPosition == ShadowArea.TopRight)
                                    {

                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();
                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth);
                                            _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }

                                    }
                                    else if (_ShadowPosition == ShadowArea.BottomLeft)
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();
                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }


                                    }
                                    else
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();
                                        }
                                        else
                                        {
                                            _with1.TranslateTransform(
                                                (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth);
                                            _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                            _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ShadowColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                            _with1.ResetTransform();

                                        }


                                    }
                                }
                                break;
                            case Orientation.Rotate:

                                if (_ShadowPosition == ShadowArea.TopLeft)
                                {
                                    //For rotation, who about rotation?
                                    double angle = (rotationAngle / 180) * Math.PI;

                                    if (!Slide)
                                        _with1.DrawString(Text,
                                        new Font(this.Font.FontFamily,
                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                        new SolidBrush(ShadowColor),
                                        new RectangleF(
                                            this.Padding.Left - _ShadowDepth, 
                                            this.Padding.Top - _ShadowDepth,
                                            (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                            (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                    
                                }
                                else if (_ShadowPosition == ShadowArea.TopRight)
                                {
                                    //For rotation, who about rotation?
                                    double angle = (rotationAngle / 180) * Math.PI;

                                    if (!Slide)
                                        _with1.DrawString(Text,
                                        new Font(this.Font.FontFamily,
                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                        new SolidBrush(ShadowColor),
                                        new RectangleF(
                                            this.Padding.Left + _ShadowDepth,
                                            this.Padding.Top - _ShadowDepth,
                                            (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                            (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));


                                }
                                else if (_ShadowPosition == ShadowArea.BottomLeft)
                                {
                                    //For rotation, who about rotation?
                                    double angle = (rotationAngle / 180) * Math.PI;

                                    if (!Slide)
                                        _with1.DrawString(Text,
                                        new Font(this.Font.FontFamily,
                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                        new SolidBrush(ShadowColor),
                                        new RectangleF(
                                            this.Padding.Left - _ShadowDepth,
                                            this.Padding.Top + _ShadowDepth,
                                            (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                            (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));


                                }
                                else
                                {
                                    //For rotation, who about rotation?
                                    double angle = (rotationAngle / 180) * Math.PI;

                                    if (!Slide)
                                        _with1.DrawString(Text,
                                        new Font(this.Font.FontFamily,
                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                        new SolidBrush(ShadowColor),
                                        new RectangleF(
                                            this.Padding.Left + _ShadowDepth,
                                            this.Padding.Top + _ShadowDepth,
                                            (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                            (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));

                                }

                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }


                        if (_ShadowStyle == ShadowDrawingType.DrawShadow)
                        {
                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                _with1.DrawPath(_ShadowPen, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                _with1.DrawPath(_ShadowPen, pth);
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                _with1.DrawPath(_ShadowPen, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                _with1.DrawPath(_ShadowPen, pth);
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        //_with1.TranslateTransform(
                                        //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - (float)(width * Math.Cos(angle))) / 2,
                                        //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - (float)(width * Math.Sin(angle))) / 2);
                                        //_with1.RotateTransform((float)rotationAngle);
                                        //_with1.DrawPath(_ShadowPen, pth);
                                        //_with1.ResetTransform();

                                        break;
                                    }
                            }

                            //Draw the Drawing2D.GraphicsPath with the _ShadowPen that is set to the ShadowColor having the ShadowTransparency
                            //_with1.DrawPath(_ShadowPen, pth);
                            //Draws the shadow
                        }
                        else
                        {

                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        //_with1.TranslateTransform(
                                        //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - (float)(width * Math.Cos(angle))) / 2,
                                        //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - (float)(width * Math.Sin(angle))) / 2);
                                        //_with1.RotateTransform((float)rotationAngle);
                                        //_with1.FillPath(_ShadowBrush, pth);
                                        //_with1.ResetTransform();

                                        break;
                                    }
                            }

                            //Fill the Drawing2D.GraphicsPath with the _ShadowBrush that is set to the ShadowColor having the ShadowTransparency
                            //_with1.FillPath(_ShadowBrush, pth);
                            //Draws the shadow
                        }


                        //Now use the Graphics.TranslateTransform to shift the _with1 back in the opposite
                        //direction before Drawing and Filling the Drawing2D.GraphicsPath again with Text colors
                        if (_ShadowPosition == ShadowArea.TopLeft)
                        {

                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        //_with1.TranslateTransform(
                                        //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) + (_ShadowDepth * 2),
                                        //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) + (_ShadowDepth * 2));
                                        //_with1.RotateTransform((float)rotationAngle);
                                        //_with1.FillPath(_ShadowBrush, pth);
                                        //_with1.ResetTransform();

                                        if (!Slide)
                                            _with1.DrawString(Text,
                                            new Font(this.Font.FontFamily,
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                            new SolidBrush(ShadowColor),
                                            new RectangleF(
                                                this.Padding.Left + (_ShadowDepth * 2),
                                                this.Padding.Top + (_ShadowDepth * 2),
                                                (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                                (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));


                                        break;
                                    }
                            }

                            //_with1.TranslateTransform(+(_ShadowDepth * 2), +(_ShadowDepth * 2));
                        }
                        else if (_ShadowPosition == ShadowArea.TopRight)
                        {

                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        //_with1.TranslateTransform(
                                        //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) - (_ShadowDepth * 2),
                                        //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) + (_ShadowDepth * 2));
                                        //_with1.RotateTransform((float)rotationAngle);
                                        //_with1.FillPath(_ShadowBrush, pth);
                                        //_with1.ResetTransform();

                                        if(!Slide)
                                        _with1.DrawString(Text,
                                            new Font(this.Font.FontFamily,
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                            new SolidBrush(ShadowColor),
                                            new RectangleF(
                                                this.Padding.Left - (_ShadowDepth * 2),
                                                this.Padding.Top + (_ShadowDepth * 2),
                                                (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                                (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));



                                        break;
                                    }
                            }

                            //_with1.TranslateTransform(-(_ShadowDepth * 2), +(_ShadowDepth * 2));
                        }
                        else if (_ShadowPosition == ShadowArea.BottomLeft)
                        {
                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        //_with1.TranslateTransform(
                                        //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) + (_ShadowDepth * 2),
                                        //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) - (_ShadowDepth * 2));
                                        //_with1.RotateTransform((float)rotationAngle);
                                        //_with1.FillPath(_ShadowBrush, pth);
                                        //_with1.ResetTransform();

                                        if(!Slide)
                                        _with1.DrawString(Text,
                                            new Font(this.Font.FontFamily,
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                            new SolidBrush(ShadowColor),
                                            new RectangleF(
                                                this.Padding.Left + (_ShadowDepth * 2),
                                                this.Padding.Top - (_ShadowDepth * 2),
                                                (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                                (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));



                                        break;
                                    }
                            }
                            //_with1.TranslateTransform(+(_ShadowDepth * 2), -(_ShadowDepth * 2));
                        }
                        else
                        {
                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - (_ShadowDepth * 2));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                _with1.FillPath(_ShadowBrush, pth);
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        //_with1.TranslateTransform(
                                        //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - ((float)(width * Math.Cos(angle))) / 2) - (_ShadowDepth * 2),
                                        //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - ((float)(width * Math.Sin(angle))) / 2) - (_ShadowDepth * 2));
                                        //_with1.RotateTransform((float)rotationAngle);
                                        //_with1.FillPath(_ShadowBrush, pth);
                                        //_with1.ResetTransform();

                                        if(!Slide)
                                        _with1.DrawString(Text,
                                            new Font(this.Font.FontFamily,
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                            new SolidBrush(ShadowColor),
                                            new RectangleF(
                                                this.Padding.Left - (_ShadowDepth * 2),
                                                this.Padding.Top - (_ShadowDepth * 2),
                                                (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                                (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));



                                        break;
                                    }
                            }
                            //_with1.TranslateTransform(-(_ShadowDepth * 2), -(_ShadowDepth * 2));
                        }
                    }


                    //The StringFormat used to align the Text in the Label
                    using (StringFormat sf = new StringFormat())
                    {
                        //Use (ta) which is an integer value of the ContentAlignment integer enum to set the
                        //Alignment of the Text that will be added to the Drawing2D.GraphicsPath
                        int ta = Convert.ToInt32(_TextAlign);
                        if (ta < 8)
                        {
                            sf.LineAlignment = StringAlignment.Near;
                        }
                        else if (ta < 128)
                        {
                            sf.LineAlignment = StringAlignment.Center;
                            ta = ta / 16;
                        }
                        else
                        {
                            sf.LineAlignment = StringAlignment.Far;
                            ta = ta / 256;
                        }
                        if (ta == Convert.ToInt32(ContentAlignment.TopLeft))
                        {
                            sf.Alignment = StringAlignment.Near;
                        }
                        else if (ta == Convert.ToInt32(ContentAlignment.TopCenter))
                        {
                            sf.Alignment = StringAlignment.Center;
                        }
                        else if (ta == Convert.ToInt32(ContentAlignment.TopRight))
                        {
                            sf.Alignment = StringAlignment.Far;
                        }
                        
                        

                        if(Slide)
                        {
                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                DrawSlidingText(_with1, new SolidBrush(ForeColor), pth, new StringFormat());
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                DrawSlidingText(_with1, new SolidBrush(ForeColor), pth, new StringFormat());
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                DrawSlidingText(_with1, new SolidBrush(ForeColor), pth, new StringFormat());
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                DrawSlidingText(_with1, new SolidBrush(ForeColor), pth, new StringFormat());
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        _with1.TranslateTransform(
                                            (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - (float)(width * Math.Cos(angle))) / 2,
                                            (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - (float)(width * Math.Sin(angle))) / 2);
                                        _with1.RotateTransform((float)rotationAngle);
                                        DrawSlidingText(_with1, new SolidBrush(ForeColor), pth, new StringFormat());
                                        _with1.ResetTransform();

                                        break;
                                    }
                            }
                            
                        }
                        else
                        {
                            switch (textOrientation)
                            {
                                case Orientation.Arc:
                                    {
                                        //Arc angle must be get from the length of the Text.
                                        float arcAngle = (2 * width / radius) / Text.Length;
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 + (float)rotationAngle + 180 * arcAngle * i / (float)Math.PI));
                                                _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ForeColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {

                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform((-90 - (float)rotationAngle - 180 * arcAngle * i / (float)Math.PI));
                                                _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ForeColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                                _with1.ResetTransform();

                                            }
                                        }
                                        break;
                                    }
                                case Orientation.Circle:
                                    {
                                        if (textDirection == Direction.Clockwise)
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 + (float)rotationAngle + (360 / Text.Length) * i);
                                                _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ForeColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                                _with1.ResetTransform();
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < Text.Length; i++)
                                            {
                                                _with1.TranslateTransform(
                                                    (float)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))),
                                                    (float)(radius * (1 + Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))));
                                                _with1.RotateTransform(-90 - (float)rotationAngle - (360 / Text.Length) * i);
                                                _with1.DrawString(Text[i].ToString(), new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ForeColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                                _with1.ResetTransform();
                                            }

                                        }
                                        break;
                                    }
                                case Orientation.Rotate:
                                    {
                                        //For rotation, who about rotation?
                                        double angle = (rotationAngle / 180) * Math.PI;
                                        //_with1.TranslateTransform(
                                        //    (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - (float)(width * Math.Cos(angle))) / 2,
                                        //    (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - (float)(width * Math.Sin(angle))) / 2);
                                        //_with1.RotateTransform((float)rotationAngle);
                                        //_with1.DrawString(Text,new Font(this.Font.FontFamily, Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style), new SolidBrush(ForeColor), new RectangleF(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));
                                        //    //pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style), Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), new Rectangle(this.Padding.Left, this.Padding.Top, (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right), (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)), sf);
                                        //_with1.ResetTransform();


                                        _with1.DrawString(Text,
                                            new Font(this.Font.FontFamily,
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72), Font.Style),
                                            new SolidBrush(ForeColor),
                                            new RectangleF(
                                                this.Padding.Left,
                                                this.Padding.Top,
                                                (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                                (this.ClientSize.Height - 1) - (this.Padding.Top + this.Padding.Bottom)));



                                        break;
                                    }
                            }

                            //Add the Text to the Drawing2D.GraphicsPath using the StringFormat
                            

                        }
                    }

                    
                    //If the TextPatternImage property has been set to an image then fill the center of the Text with the image
                    //else the center will be filled with a soloid color of the ForeColor property.
                    if (_TextPatternImage != null)
                    {
                        //Use the TextPatternImageLayout property to resize and/or position the TextPatternImage
                        Rectangle br = new Rectangle();
                        RectangleF r = pth.GetBounds();
                        if (_TextPatternImageLayout == PatternLayout.Normal | _TextPatternImageLayout == PatternLayout.Tile)
                        {
                            br = new Rectangle(Convert.ToInt32(r.X) + 1, Convert.ToInt32(r.Y + 1), _TextPatternImage.Width + 1, _TextPatternImage.Height + 1);
                        }
                        else if (_TextPatternImageLayout == PatternLayout.Center)
                        {
                            int xx = Convert.ToInt32((r.X + 1) + ((r.Width / 2) - (_TextPatternImage.Width / 2)));
                            int yy = Convert.ToInt32((r.Y + 1) + ((r.Height / 2) - (_TextPatternImage.Height / 2)));
                            br = new Rectangle(xx, yy, _TextPatternImage.Width + 1, _TextPatternImage.Height + 1);
                        }
                        else if (_TextPatternImageLayout == PatternLayout.Stretch)
                        {
                            br = new Rectangle(Convert.ToInt32(r.X) + 1, Convert.ToInt32(r.Y + 1), Convert.ToInt32(r.Width) + 1, Convert.ToInt32(r.Height) + 1);
                        }
                        using (Bitmap patBmp = new Bitmap(_TextPatternImage, br.Width, br.Height))
                        {
                            //Use a TextureBrush with the TextPatternImage assigned as the texture image
                            using (TextureBrush tb = new TextureBrush(patBmp))
                            {
                                //If the TextPatternImageLayout property is not set to Tile then set the set the
                                //TextureBrush`s WrapMode to Clamp to stop it from tiling the image.
                                if (!(_TextPatternImageLayout == PatternLayout.Tile))
                                    tb.WrapMode = WrapMode.Clamp;
                                tb.TranslateTransform(br.X, br.Y);
                                //Fill the GraphicsPath with the TextureBrush.
                                _with1.FillPath(tb, pth);
                            }
                        }
                    }
                    else
                    {
                        //Fill the GraphicsPath with a soloid color of the ForeColor property.
                        _with1.FillPath(_CenterBrush, pth);
                    }
                    //Draw the GraphicsPath with the OutlineColor.
                    _with1.DrawPath(_OutLinePen, pth);
                }
            }

            //If the BorderStyle property is other than None then call the DrawBorder sub to draw the border
            if (_BorderStyle != BorderType.None)
            {
                DrawLabelBorder(e.Graphics, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            }
        }

        
        #endregion
        

    }
}
