// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 12-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="Paint.cs" company="Zeroit Dev Technologies">
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
        private void PathOnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            var _with1 = e.Graphics;
            _with1.TextRenderingHint = TextRendering;

            //The StringFormat used to align the Text in the Label
            StringFormat sf = new StringFormat();
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

                    
                    if (Slide)
                    {
                        //For rotation, who about rotation?
                        double angle = (rotationAngle / 180) * Math.PI;
                        _with1.TranslateTransform(
                            (ClientRectangle.Width + (float)(height * Math.Sin(angle)) - (float)(width * Math.Cos(angle))) / 2,
                            (ClientRectangle.Height - (float)(height * Math.Cos(angle)) - (float)(width * Math.Sin(angle))) / 2);
                        _with1.RotateTransform((float)rotationAngle);
                        DrawSlidingText(_with1, new SolidBrush(ForeColor), pth, new StringFormat());
                        _with1.ResetTransform();

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

                                            pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                new Rectangle(
                                                    (int)(radius * (1 - Math.Cos(
                                                                         arcAngle * i +
                                                                         rotationAngle / 180 * Math.PI))),
                                                    (int)(radius * (1 - Math.Sin(
                                                                         arcAngle * i +
                                                                         rotationAngle / 180 * Math.PI))), pathSize,
                                                    pathSize), sf);

                                            if (_ShowTextShadow)
                                            {
                                                switch (ShadowPosition)
                                                {
                                                    case ShadowArea.TopLeft:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.TopRight:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomLeft:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomRight:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin(arcAngle * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    default:
                                                        throw new ArgumentOutOfRangeException();
                                                }

                                            }

                                        }
                                    }
                                    else
                                    {
                                        for (int i = 0; i < Text.Length; i++)
                                        {

                                            pth.Reverse();
                                            pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                new Rectangle(
                                                    (int)(radius * (1 - Math.Cos(
                                                                         arcAngle * i +
                                                                         rotationAngle / 180 * Math.PI))),
                                                    (int)(radius * (1 + Math.Sin(
                                                                         arcAngle * i +
                                                                         rotationAngle / 180 * Math.PI))), pathSize,
                                                    pathSize), sf);

                                            if (_ShowTextShadow)
                                            {
                                                switch (ShadowPosition)
                                                {
                                                    case ShadowArea.TopLeft:
                                                        
                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 + Math.Sin(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);

                                                        break;
                                                    case ShadowArea.TopRight:
                                                        
                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 + Math.Sin(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomLeft:
                                                        
                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 + Math.Sin(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomRight:


                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 + Math.Sin(
                                                                                    arcAngle * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    default:
                                                        throw new ArgumentOutOfRangeException();
                                                }

                                            }

                                            //pth.Reverse();
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

                                            pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                new Rectangle(
                                                    (int)(radius * (1 - Math.Cos(
                                                                         (2 * Math.PI / Text.Length) * i +
                                                                         rotationAngle / 180 * Math.PI))),
                                                    (int)(radius * (1 - Math.Sin(
                                                                         (2 * Math.PI / Text.Length) * i +
                                                                         rotationAngle / 180 * Math.PI))), pathSize,
                                                    pathSize), sf);

                                            if (_ShowTextShadow)
                                            {
                                                switch (ShadowPosition)
                                                {
                                                    case ShadowArea.TopLeft:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.TopRight:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomLeft:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomRight:
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Cos((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 - Math.Sin((2 * Math.PI / Text.Length) * i + rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    default:
                                                        throw new ArgumentOutOfRangeException();
                                                }
                                            }

                                        }

                                        
                                    }
                                    else
                                    {
                                        for (int i = 0; i < Text.Length; i++)
                                        {
                                            pth.Reverse();
                                            pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                new Rectangle(
                                                    (int)(radius * (1 - Math.Sin(
                                                                         (2 * Math.PI / Text.Length) * i +
                                                                         rotationAngle / 180 * Math.PI))),
                                                    (int)(radius * (1 - Math.Cos(
                                                                         (2 * Math.PI / Text.Length) * i +
                                                                         rotationAngle / 180 * Math.PI))), pathSize,
                                                    pathSize), sf);
                                            

                                            if (_ShowTextShadow)
                                            {
                                                switch (ShadowPosition)
                                                {
                                                    case ShadowArea.TopLeft:
                                                        
                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Sin(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.TopRight:
                                                        
                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Sin(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomLeft:
                                                        
                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Sin(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) - _ShadowDepth,
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    case ShadowArea.BottomRight:
                                                        
                                                        pth.Reverse();
                                                        pth.AddString(Text[i].ToString(), this.Font.FontFamily,
                                                            Convert.ToInt32(this.Font.Style),
                                                            Convert.ToSingle((_with1.DpiY * this.Font.Size) / CorrectWidth),
                                                            new Rectangle(
                                                                (int)(radius * (1 - Math.Sin(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth,
                                                                (int)(radius * (1 - Math.Cos(
                                                                                    (2 * Math.PI / Text.Length) * i +
                                                                                    rotationAngle / 180 * Math.PI))) + _ShadowDepth, pathSize,
                                                                pathSize), sf);
                                                        break;
                                                    default:
                                                        throw new ArgumentOutOfRangeException();
                                                }
                                            }

                                        }

                                    }
                                    break;
                                }
                            case Orientation.Rotate:
                                {

                                    //For rotation, who about rotation?

                                    pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                                        Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                                        new Rectangle(
                                            this.Padding.Left,
                                            this.Padding.Top,
                                            (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right),
                                            (this.ClientSize.Height - 1) -
                                            (this.Padding.Top + this.Padding.Bottom)), sf);

                                    if (_ShowTextShadow)
                                    {
                                        if (_ShadowPosition == ShadowArea.TopLeft)
                                        {

                                            pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                                                new Rectangle(
                                                    this.Padding.Left,
                                                    this.Padding.Top,
                                                    (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
                                                    (this.ClientSize.Height - 1) -
                                                    (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);

                                        }
                                        else if (_ShadowPosition == ShadowArea.TopRight)
                                        {

                                            pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                                                new Rectangle(
                                                    this.Padding.Left,
                                                    this.Padding.Top,
                                                    (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) + _ShadowDepth,
                                                    (this.ClientSize.Height - 1) -
                                                    (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);

                                        }
                                        else if (_ShadowPosition == ShadowArea.BottomLeft)
                                        {

                                            pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                                                new Rectangle(
                                                    this.Padding.Left,
                                                    this.Padding.Top,
                                                    (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
                                                    (this.ClientSize.Height - 1) -
                                                    (this.Padding.Top + this.Padding.Bottom) + _ShadowDepth), sf);
                                        }
                                        else
                                        {

                                            pth.AddString(this.Text, this.Font.FontFamily, Convert.ToInt32(this.Font.Style),
                                                Convert.ToSingle((_with1.DpiY * this.Font.Size) / 72),
                                                new Rectangle(
                                                    this.Padding.Left,
                                                    this.Padding.Top,
                                                    (this.ClientSize.Width - 1) - (this.Padding.Left + this.Padding.Right) - _ShadowDepth,
                                                    (this.ClientSize.Height - 1) -
                                                    (this.Padding.Top + this.Padding.Bottom) - _ShadowDepth), sf);
                                        }
                                    }

                                    break;
                                }
                        }
                        
                        //Add the Text to the Drawing2D.GraphicsPath using the StringFormat
                        
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
                DrawLabelBorder(e.Graphics, new Rectangle(2, 2, this.Width - 4, this.Height - 4));
            }
        }

        
        #endregion
        
        
    }
}
