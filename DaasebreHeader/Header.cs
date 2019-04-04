// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="Header.cs" company="Zeroit Dev Technologies">
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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Permissions;

namespace Zeroit.Framework.Labels.Headers
{

    #region Common

    /// <summary>
    /// ErrMsg class
    /// </summary>
    internal abstract class ErrMsg
	{
        /// <summary>
        /// Negs the value.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string NegVal()
		{
			return "Value cannot be negative.";
		}

        /// <summary>
        /// Nulls the value.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string NullVal()
		{
			return "Value cannot be null.";
		}

        /// <summary>
        /// Invs the value.
        /// </summary>
        /// <param name="sValue">The s value.</param>
        /// <returns>System.String.</returns>
        public static string InvVal(string sValue)
		{
			return string.Format("Value of \"{0}\" is invalid.", sValue);
		}

        /// <summary>
        /// Indexes the out of range.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string IndexOutOfRange()
		{
			return "Index is out of range.";
		}

        /// <summary>
        /// Sections the is already attached.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public static string SectionIsAlreadyAttached(string sText)
		{
			return "Section \"" + sText + "\" is already added to the collection.";
		}

        /// <summary>
        /// Sections the does not exist.
        /// </summary>
        /// <param name="sText">The s text.</param>
        /// <returns>System.String.</returns>
        public static string SectionDoesNotExist(string sText)
		{
			return "Section \"" + sText + "\" does not exist in the collection";
		}

        /// <summary>
        /// Faileds to insert item.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string FailedToInsertItem()
		{
			return "Failed to insert item.";
		}

        /// <summary>
        /// Faileds to remove item.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string FailedToRemoveItem()
		{
			return "Failed to remove item.";
		}

        /// <summary>
        /// Faileds to change item.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string FailedToChangeItem()
		{
			return "Failed to change item.";
		}			
	}

    #endregion Common


    #region ZeroitColumnHeader Section

    /// <summary>
    /// Types
    /// </summary>

    [Serializable]
	public enum HeaderSectionSortMarks : int
	{
        /// <summary>
        /// The non
        /// </summary>
        Non = 0,
        /// <summary>
        /// Up
        /// </summary>
        Up = NativeHeader.HDF_SORTUP,
        /// <summary>
        /// Down
        /// </summary>
        Down = NativeHeader.HDF_SORTDOWN
	}

    /// <summary>
    /// HeaderSection class.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Component" />
    /// <seealso cref="System.ICloneable" />
    [
        Description("HeaderSection component"),
		DefaultProperty("Text"),
		ToolboxItem(false),
		DesignTimeVisible(false),
		SecurityPermission(SecurityAction.LinkDemand, 
						   Flags=SecurityPermissionFlag.UnmanagedCode)
	]
	public class HeaderSection : Component, ICloneable
	{
        /// <summary>
        /// Data fields
        /// </summary>

        // Owner collection
        private HeaderSectionCollection collection = null;

        /// <summary>
        /// Gets or sets the collection.
        /// </summary>
        /// <value>The collection.</value>
        [
            Description("Collection which section is kept in."),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
			Browsable(false)
		]
		internal HeaderSectionCollection Collection
		{
			get { return this.collection; }
			set { this.collection = value; }
		}

        // Owner header control
        /// <summary>
        /// Gets the zeroit column header.
        /// </summary>
        /// <value>The zeroit column header.</value>
        [
            Description("Owner header control."),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
			Browsable(false)
		]
		public ZeroitColumnHeader ZeroitColumnHeader
		{
			get { return collection != null ? collection.ZeroitColumnHeader : null; }
		}

        // Index
        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        [
            Description("Index of the section."),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
			Browsable(false)
		]
		public int Index
		{
			get { return collection != null ? collection.IndexOf(this) : -1; }
		}

        // Width
        /// <summary>
        /// The cx width
        /// </summary>
        private int cxWidth = 100;

        /// <summary>
        /// Sets the width.
        /// </summary>
        /// <param name="cx">The cx.</param>
        /// <exception cref="ArgumentOutOfRangeException">cx</exception>
        internal void _SetWidth(int cx)
		{			
			if ( cx < 0 )
				throw new ArgumentOutOfRangeException("cx", cx, ErrMsg.NegVal());

			this.cxWidth = cx;
		}

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        [
            Category("Data"),
			Description("Specifies section width.")
		]                      
		public int Width
		{
			get { return this.cxWidth; }

			set
			{ 
				if ( value != this.cxWidth )
				{ 
					_SetWidth(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionWidthChanged(this);
					}
				}
			}
		}

        // TODO Support owner drawing HDF_OWNERDRAW

        // Format
        /// <summary>
        /// The f format
        /// </summary>
        private int fFormat = NativeHeader.HDF_LEFT;

        /// <summary>
        /// Sets the format.
        /// </summary>
        /// <param name="fFormat">The f format.</param>
        internal void _SetFormat(int fFormat)
		{
			this.fFormat = fFormat;
		}

        /// <summary>
        /// Gets the format.
        /// </summary>
        /// <value>The format.</value>
        [
            Description("Raw window styles."),
			Browsable(false)
		]
		internal int Format
		{
			get 
			{ 
				if ( this._GetActualRightToLeft() == RightToLeft.Yes )
					return this.fFormat|NativeHeader.HDF_RTLREADING;
				else
					return this.fFormat; 
			}
		}

        // Text
        /// <summary>
        /// The text
        /// </summary>
        private string text = null;

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="text">The text.</param>
        internal void _SetText(string text)
		{
			this.text = text; 
          
			if ( this.text != null )
				this.fFormat |= NativeHeader.HDF_STRING;
			else
				this.fFormat &= (~NativeHeader.HDF_STRING);
		}

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        [
            Category("Data"),
			Description("Text to be displayed."),
			DefaultValue("Section")
		]
		public string Text
		{
			get { return this.text; }

			set
			{ 
				if ( value != this.text )
				{ 
					_SetText(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionTextChanged(this);
					}
				}
			}
		}

        // ImageIndex
        /// <summary>
        /// The i image
        /// </summary>
        private int iImage = -1;

        /// <summary>
        /// Sets the index of the image.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <exception cref="ArgumentException">value</exception>
        internal void _SetImageIndex(int index)
		{
			this.iImage = index; 
          
			if ( this.iImage >= 0 )
				this.fFormat |= NativeHeader.HDF_IMAGE;
			else
			{
				if ( this.iImage != -1 )
					throw new ArgumentException(ErrMsg.InvVal(index.ToString()), "value");

				this.fFormat &= (~NativeHeader.HDF_IMAGE);
			}
		}

        /// <summary>
        /// Gets or sets the index of the image.
        /// </summary>
        /// <value>The index of the image.</value>
        [
            Category("Data"),
			Description("Index of image associated with section."),
			TypeConverter(typeof(ImageIndexConverter)),
		//      Editor(typeof(ImageIndexEditor), typeof(UITypeEditor)),
			Localizable(true),
			DefaultValue(-1)
		]
		public int ImageIndex
		{
			get { return this.iImage; }

			set
			{ 
				if ( value != this.iImage )
				{
					_SetImageIndex(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionImageIndexChanged(this);
					}
				}
			}
		}

        // Bitmap
        /// <summary>
        /// The bitmap
        /// </summary>
        private Bitmap bitmap = null;
        /// <summary>
        /// The h bitmap
        /// </summary>
        private IntPtr hBitmap = IntPtr.Zero;

        /// <summary>
        /// Gets the h bitmap.
        /// </summary>
        /// <returns>IntPtr.</returns>
        internal IntPtr _GetHBitmap()
		{
			if ( this.hBitmap == IntPtr.Zero && this.bitmap != null )
			{
				this.hBitmap = this.bitmap.GetHbitmap();
			}

			return this.hBitmap;
		}

        /// <summary>
        /// Sets the bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        internal void _SetBitmap(Bitmap bitmap)
		{
			if ( this.hBitmap != IntPtr.Zero )
			{
				NativeWindowCommon.DeleteObject(this.hBitmap);
				this.hBitmap = IntPtr.Zero;
			}

			this.bitmap = bitmap; 
          
			if ( this.bitmap != null )
				this.fFormat |= NativeHeader.HDF_BITMAP;
			else
				this.fFormat &= (~NativeHeader.HDF_BITMAP);
		}


        /// <summary>
        /// Gets or sets the bitmap.
        /// </summary>
        /// <value>The bitmap.</value>
        [
            Category("Data"),
			Description("Bitmap to be drawn on the section."),
		]
		public Bitmap Bitmap
		{
			get { return this.bitmap; }
			set
			{ 
				if ( value != this.bitmap )
				{
					_SetBitmap(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionBitmapChanged(this);
					}
				}
			}
		}

        // RightToLeft
        /// <summary>
        /// The en right to left
        /// </summary>
        private RightToLeft enRightToLeft = RightToLeft.No;

        /// <summary>
        /// Gets the actual right to left.
        /// </summary>
        /// <returns>RightToLeft.</returns>
        internal RightToLeft _GetActualRightToLeft()
		{
			ZeroitColumnHeader owner = this.ZeroitColumnHeader;

			return ( this.enRightToLeft == RightToLeft.Inherit && owner != null ) 
						? owner.RightToLeft
						: this.enRightToLeft;
		}

        /// <summary>
        /// Sets the right to left.
        /// </summary>
        /// <param name="enRightToLeft">The en right to left.</param>
        internal void _SetRightToLeft(RightToLeft enRightToLeft)
		{
			this.enRightToLeft = enRightToLeft;
		}

        /// <summary>
        /// Gets or sets the right to left.
        /// </summary>
        /// <value>The right to left.</value>
        [
            Category("Appearance"),
			Description("Right to left layout."),
		]
		public RightToLeft RightToLeft
		{
			get { return enRightToLeft; }
			set 
			{
				if ( this.enRightToLeft != value )
				{
					_SetRightToLeft(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionRightToLeftChanged(this);
					}
				}
			}
		}

        // Content align
        /// <summary>
        /// Gets the content align.
        /// </summary>
        /// <returns>HorizontalAlignment.</returns>
        internal HorizontalAlignment _GetContentAlign()
		{
			switch ( fFormat & NativeHeader.HDF_JUSTIFYMASK )
			{
			case NativeHeader.HDF_LEFT:
				return HorizontalAlignment.Left;

			case NativeHeader.HDF_RIGHT:
				return HorizontalAlignment.Right;

			case NativeHeader.HDF_CENTER:
				return HorizontalAlignment.Center;
			}

			return HorizontalAlignment.Left;
		}

        /// <summary>
        /// Sets the content align.
        /// </summary>
        /// <param name="enValue">The en value.</param>
        /// <exception cref="NotSupportedException">null</exception>
        internal void _SetContentAlign(HorizontalAlignment enValue)
		{
			int nFlag;

			switch ( enValue )
			{
			case HorizontalAlignment.Left:
				nFlag = NativeHeader.HDF_LEFT;
				break;

			case HorizontalAlignment.Right:
				nFlag = NativeHeader.HDF_RIGHT;
				break;

			case HorizontalAlignment.Center:
				nFlag = NativeHeader.HDF_CENTER;
				break;

			default:
				throw new NotSupportedException(ErrMsg.InvVal(enValue.ToString()), null);
			}

			this.fFormat &= (~NativeHeader.HDF_JUSTIFYMASK);
			this.fFormat |= nFlag;
		}

        /// <summary>
        /// Gets or sets the content align.
        /// </summary>
        /// <value>The content align.</value>
        [
            Category("Appearance"),
			Description("Specifies content alignment."),
		]
		public HorizontalAlignment ContentAlign
		{
			get { return _GetContentAlign(); }

			set 
			{
				if ( value != _GetContentAlign() )
				{
					_SetContentAlign(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionContentAlignChanged(this);
					}
				}
			}
		}

        // Image align
        /// <summary>
        /// Gets the image align.
        /// </summary>
        /// <returns>LeftRightAlignment.</returns>
        internal LeftRightAlignment _GetImageAlign()
		{
			if ( (this.fFormat & NativeHeader.HDF_BITMAP_ON_RIGHT) != 0 )
				return LeftRightAlignment.Right;
			else
				return LeftRightAlignment.Left;
		}

        /// <summary>
        /// Sets the image align.
        /// </summary>
        /// <param name="enValue">The en value.</param>
        /// <exception cref="NotSupportedException">null</exception>
        internal void _SetImageAlign(LeftRightAlignment enValue)
		{
			int nFlag;
			const int fMask = NativeHeader.HDF_BITMAP_ON_RIGHT;

			switch ( enValue )
			{
			case LeftRightAlignment.Left:
				nFlag = 0;
				break;

			case LeftRightAlignment.Right:
				nFlag = NativeHeader.HDF_BITMAP_ON_RIGHT;
				break;
    
			default:
				throw new NotSupportedException(ErrMsg.InvVal(enValue.ToString()), null);
			}

			this.fFormat &= (~fMask);
			this.fFormat |= nFlag;
		}

        /// <summary>
        /// Gets or sets the image align.
        /// </summary>
        /// <value>The image align.</value>
        [
            Category("Appearance"),
			Description("Specifies image placement."),
		]
		public LeftRightAlignment ImageAlign
		{
			get { return _GetImageAlign(); }

			set 
			{
				if ( value != _GetImageAlign() )
				{
					_SetImageAlign(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionImageAlignChanged(this);
					}
				}
			}
		}

        // Sort mark
        /// <summary>
        /// Gets the sort mark.
        /// </summary>
        /// <returns>HeaderSectionSortMarks.</returns>
        internal HeaderSectionSortMarks _GetSortMark()
		{
			const int fSortMask = NativeHeader.HDF_SORTUP|NativeHeader.HDF_SORTDOWN;

			return (HeaderSectionSortMarks)(this.fFormat & fSortMask);
		}

        /// <summary>
        /// Sets the sort mark.
        /// </summary>
        /// <param name="enValue">The en value.</param>
        /// <exception cref="NotSupportedException">null</exception>
        internal void _SetSortMark(HeaderSectionSortMarks enValue)
		{
			const int fSortMask = NativeHeader.HDF_SORTUP|NativeHeader.HDF_SORTDOWN;
			int nFlag;

			switch ( enValue )
			{
			case HeaderSectionSortMarks.Non:
				nFlag = 0;
				break;

			case HeaderSectionSortMarks.Up:
				nFlag = NativeHeader.HDF_SORTUP;
				break;

			case HeaderSectionSortMarks.Down:
				nFlag = NativeHeader.HDF_SORTDOWN;
				break;
        
			default:
				throw new NotSupportedException(ErrMsg.InvVal(enValue.ToString()), null);
			}

			this.fFormat &= (~fSortMask);
			this.fFormat |= nFlag;
		}

        /// <summary>
        /// Gets or sets the sort mark.
        /// </summary>
        /// <value>The sort mark.</value>
        [
            Category("Appearance"),
			Description("Defines sort mark to be shown on the section."),
		]
		public HeaderSectionSortMarks SortMark
		{
			get { return _GetSortMark(); }

			set 
			{
				if ( value != _GetSortMark() )
				{
					_SetSortMark(value);

					// Notify owner header control
					ZeroitColumnHeader owner = this.ZeroitColumnHeader;
					if ( owner != null )
					{
						owner._OnSectionSortMarkChanged(this);
					}
				}
			}
		}

        // Tag
        /// <summary>
        /// Sets the tag.
        /// </summary>
        /// <param name="tag">The tag.</param>
        internal void _SetTag(object tag)
		{
			this.tag = tag;
		}

        /// <summary>
        /// The tag
        /// </summary>
        private object tag = null;

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        [
            Browsable(false)
		]
		public object Tag
		{
			get { return this.tag; }
			set 
			{ 
				if ( this.tag != value )
				{
					this.tag = value; 
				}
			}
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        public HeaderSection()
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        public HeaderSection(string text, int cxWidth)
		{
			_SetText(text);
			_SetWidth(cxWidth);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="iImage">The i image.</param>
        public HeaderSection(string text, int cxWidth, int iImage)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="tag">The tag.</param>
        public HeaderSection(string text, int cxWidth, object tag)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetTag(tag);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="iImage">The i image.</param>
        /// <param name="tag">The tag.</param>
        public HeaderSection(string text, int cxWidth, int iImage, object tag)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
			_SetTag(tag);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="bitmap">The bitmap.</param>
        public HeaderSection(string text, int cxWidth, Bitmap bitmap)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetBitmap(bitmap);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="iImage">The i image.</param>
        /// <param name="bitmap">The bitmap.</param>
        public HeaderSection(string text, int cxWidth, int iImage, Bitmap bitmap)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
			_SetBitmap(bitmap);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="iImage">The i image.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="enContentAlign">The en content align.</param>
        public HeaderSection(string text, int cxWidth, int iImage, Bitmap bitmap, 
							 HorizontalAlignment enContentAlign)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
			_SetBitmap(bitmap);
			_SetContentAlign(enContentAlign);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="iImage">The i image.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="enContentAlign">The en content align.</param>
        /// <param name="enImageAlign">The en image align.</param>
        public HeaderSection(string text, int cxWidth, int iImage, Bitmap bitmap, 
							 HorizontalAlignment enContentAlign, 
							 LeftRightAlignment enImageAlign)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
			_SetBitmap(bitmap);
			_SetContentAlign(enContentAlign);
			_SetImageAlign(enImageAlign);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="iImage">The i image.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="enContentAlign">The en content align.</param>
        /// <param name="enImageAlign">The en image align.</param>
        /// <param name="tag">The tag.</param>
        public HeaderSection(string text, int cxWidth, int iImage, Bitmap bitmap, 
							 HorizontalAlignment enContentAlign, 
							 LeftRightAlignment enImageAlign, object tag)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
			_SetBitmap(bitmap);
			_SetContentAlign(enContentAlign);
			_SetImageAlign(enImageAlign);
			_SetTag(tag);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="iImage">The i image.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="enRightToLeft">The en right to left.</param>
        /// <param name="enContentAlign">The en content align.</param>
        /// <param name="enImageAlign">The en image align.</param>
        /// <param name="enSortMark">The en sort mark.</param>
        /// <param name="tag">The tag.</param>
        public HeaderSection(string text, int cxWidth, int iImage, Bitmap bitmap, 
							 RightToLeft enRightToLeft,	HorizontalAlignment enContentAlign, 
							 LeftRightAlignment enImageAlign, 
							 HeaderSectionSortMarks enSortMark, object tag)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
			_SetBitmap(bitmap);
			_SetRightToLeft(enRightToLeft);
			_SetContentAlign(enContentAlign);
			_SetImageAlign(enImageAlign);
			_SetSortMark(enSortMark);
			_SetTag(tag);
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        /// <param name="cxWidth">Width of the cx.</param>
        /// <param name="text">The text.</param>
        /// <param name="iImage">The i image.</param>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="enRightToLeft">The en right to left.</param>
        /// <param name="fFormat">The f format.</param>
        /// <param name="tag">The tag.</param>
        protected HeaderSection(int cxWidth, string text, int iImage, Bitmap bitmap, 
								RightToLeft enRightToLeft, int fFormat, object tag)
		{
			_SetText(text);
			_SetWidth(cxWidth);
			_SetImageIndex(iImage);
			_SetBitmap(bitmap);
			_SetRightToLeft(enRightToLeft);
			_SetFormat(fFormat);
			_SetTag(tag);
		}

        /// <summary>
        /// Finalizes an instance of the <see cref="HeaderSection"/> class.
        /// </summary>
        ~HeaderSection()
		{
			Dispose(false);
		}

        /// <summary>
        /// Overrides
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
		{
			return "HeaderSection: {" + this.text + "}"; 
		}

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="bDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool bDisposing)
		{
			if ( this.hBitmap != IntPtr.Zero )
			{
				NativeWindowCommon.DeleteObject(this.hBitmap);
				this.hBitmap = IntPtr.Zero;
			}

			if ( bDisposing && this.collection != null )
			{
				this.collection.Remove(this);
			}

			base.Dispose(bDisposing);
		}

        /// <summary>
        /// ICloneable implementation
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public virtual object Clone()
		{
			return new HeaderSection(this.cxWidth, this.text, this.iImage, this.bitmap, 
									 this.enRightToLeft, this.fFormat, this.tag);
		}

        /// <summary>
        /// Operations
        /// </summary>
        /// <param name="iOrder">The i order.</param>
        /// <param name="item">The item.</param>
        internal void ComposeNativeData(int iOrder, out NativeHeader.HDITEM item)
		{
			item = new NativeHeader.HDITEM();
      
			// Width
			item.mask = NativeHeader.HDI_WIDTH;
			item.cxy = this.cxWidth;

			// Text
			if ( this.text != null )
			{
				item.mask |= NativeHeader.HDI_TEXT;
				item.lpszText = this.text;
				item.cchTextMax = 0;
			}

			// ImageIndex
			if ( this.iImage >= 0 )
			{
				item.mask |= NativeHeader.HDI_IMAGE;
				item.iImage = this.iImage;
			}

			// Bitmap
			if ( this.bitmap != null && this.bitmap.GetHbitmap() != IntPtr.Zero )
			{
				item.mask |= NativeHeader.HDI_BITMAP;
				item.hbm = this._GetHBitmap();
			}

			// Format
			item.mask |= NativeHeader.HDI_FORMAT;
			item.fmt = this.Format;

			// Order
			if ( iOrder >= 0 )
			{
				item.mask |= NativeHeader.HDI_ORDER;
				item.iOrder = iOrder;
			}

			//      item.lParam;
			//      item.type;
			//      item.pvFilter;
		}    

	}

    #endregion // HeaderSection


    #region ZeroitColumnHeader Sections' Collection

    /// <summary>
    /// HeaderSectionCollection class.
    /// </summary>
    /// <seealso cref="System.Collections.IList" />

    //  [Serializable]
    public class HeaderSectionCollection : IList
	{
        /// <summary>
        /// Data fields
        /// </summary>
        private ZeroitColumnHeader owner = null;
        /// <summary>
        /// Gets the zeroit column header.
        /// </summary>
        /// <value>The zeroit column header.</value>
        public ZeroitColumnHeader ZeroitColumnHeader
		{
			get { return this.owner; }
		}

        /// <summary>
        /// The al sections by order
        /// </summary>
        private ArrayList alSectionsByOrder = null;
        /// <summary>
        /// The al sections by raw index
        /// </summary>
        private ArrayList alSectionsByRawIndex = null;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The count.</value>
        public int Count 
		{ 
			get { return this.alSectionsByOrder.Count; }
		}

        /// <summary>
        /// Gets or sets the <see cref="HeaderSection"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>HeaderSection.</returns>
        /// <exception cref="ArgumentOutOfRangeException">index</exception>
        public HeaderSection this[int index] 
		{
			get { return (HeaderSection)this.alSectionsByOrder[index]; }
			set
			{
				if ( index < 0 || index >= this.alSectionsByOrder.Count )
					throw new ArgumentOutOfRangeException("index", index, 
														  ErrMsg.IndexOutOfRange());
      
				_SetSection(index, (HeaderSection)value);
			}
		}

        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="owner">The owner.</param>
        internal HeaderSectionCollection(ZeroitColumnHeader owner)
		{
			this.owner = owner;
			this.alSectionsByOrder = new ArrayList();
			this.alSectionsByRawIndex  = new ArrayList();
		}

        /// <summary>
        /// Helpers
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException">item</exception>
        /// <exception cref="ArgumentException">item</exception>
        private void BindSection(HeaderSection item)
		{
			if ( item == null )
			{
				throw new ArgumentNullException("item", ErrMsg.NullVal());
			}

			if ( item.Collection != null )
			{
				throw new ArgumentException(ErrMsg.SectionIsAlreadyAttached(item.Text), "item");
			}

			item.Collection = this;
		}

        /// <summary>
        /// Unbinds the section.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException">item</exception>
        /// <exception cref="ArgumentException">item</exception>
        private void UnbindSection(HeaderSection item)
		{
			if ( item == null )
			{
				throw new ArgumentNullException("item", ErrMsg.NullVal());
			}

			if ( item.Collection != this )
			{
				throw new ArgumentException(ErrMsg.SectionDoesNotExist(item.Text), "item");
			}

			item.Collection = null;
		}

        /// <summary>
        /// Operations
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        internal int _FindSectionRawIndex(HeaderSection item)
		{
			return this.alSectionsByRawIndex.IndexOf(item);
		}

        /// <summary>
        /// Gets the index of the section by raw.
        /// </summary>
        /// <param name="iSection">The i section.</param>
        /// <returns>HeaderSection.</returns>
        internal HeaderSection _GetSectionByRawIndex(int iSection)
		{
			return (HeaderSection)this.alSectionsByRawIndex[iSection];
		}

        /// <summary>
        /// Moves the specified i from.
        /// </summary>
        /// <param name="iFrom">The i from.</param>
        /// <param name="iTo">The i to.</param>
        internal void _Move(int iFrom, int iTo)
		{
			Debug.Assert( iFrom >= 0 || iFrom < this.alSectionsByOrder.Count );
			Debug.Assert( iTo >= 0 || iTo < this.alSectionsByOrder.Count );
			Debug.Assert( this.alSectionsByOrder.Count == this.alSectionsByRawIndex.Count );

			HeaderSection item = (HeaderSection)this.alSectionsByOrder[iFrom];
			this.alSectionsByOrder.RemoveAt(iFrom);
			this.alSectionsByOrder.Insert(iTo, item);
		}

        /// <summary>
        /// Sets the section.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        internal void _SetSection(int index, HeaderSection item)
		{
			Debug.Assert( index >= 0 || index < this.alSectionsByOrder.Count );
			Debug.Assert( this.alSectionsByOrder.Count == this.alSectionsByRawIndex.Count );			

			// Bind item to the collection
			BindSection(item);

			HeaderSection itemOld = (HeaderSection)this.alSectionsByOrder[index];
			int iSection = this.alSectionsByRawIndex.IndexOf(itemOld);

			try
			{        
				this.alSectionsByOrder[index] = item;
				this.alSectionsByRawIndex[iSection] = item;

				UnbindSection(itemOld);

				// Notify owner
				if ( this.owner != null )  
					this.owner._OnSectionChanged(iSection, item);
			}
			catch
			{
				if ( itemOld.Collection == null )
					BindSection(itemOld);

				this.alSectionsByOrder[index] = itemOld;
				this.alSectionsByRawIndex[iSection] = itemOld;

				UnbindSection(item);
				throw;
			}
		}

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentOutOfRangeException">index</exception>
        public void Insert(int index, HeaderSection item)
		{ 
			Debug.Assert( this.alSectionsByOrder.Count == this.alSectionsByRawIndex.Count );

			if ( index < 0 || index > this.alSectionsByOrder.Count )
				throw new ArgumentOutOfRangeException("index", index, ErrMsg.IndexOutOfRange());

			// Bind item to the collection
			BindSection(item);

			try
			{
				this.alSectionsByOrder.Insert(index, item);
				this.alSectionsByRawIndex.Insert(index, item);

				try
				{
					// Notify owner
					if ( this.owner != null )  
						this.owner._OnSectionInserted(index, item);
				}
				catch
				{
					this.alSectionsByOrder.Remove(item);
					this.alSectionsByRawIndex.Remove(item);
					throw;
				}
			}
			catch
			{
				if ( this.alSectionsByOrder.Count > this.alSectionsByRawIndex.Count )
					this.alSectionsByOrder.RemoveAt(index);

				UnbindSection(item);
				throw;
			}
		}

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        public int Add(HeaderSection item)
		{
			int index = this.alSectionsByOrder.Count;

			Insert(index, item);

			return index;
		}

        /// <summary>
        /// Removes the <see cref="T:System.Collections.IList" /> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">index</exception>
        public void RemoveAt(int index)
		{
			Debug.Assert( this.alSectionsByOrder.Count == this.alSectionsByRawIndex.Count );

			if ( index < 0 || index >= this.alSectionsByOrder.Count )
				throw new ArgumentOutOfRangeException("index", index, ErrMsg.IndexOutOfRange());

			HeaderSection item = (HeaderSection)this.alSectionsByOrder[index];
			int iSectionRemoved = this.alSectionsByRawIndex.IndexOf(item);
			Debug.Assert( iSectionRemoved >= 0 );

			UnbindSection(item);

			this.alSectionsByOrder.RemoveAt(index);
			this.alSectionsByRawIndex.RemoveAt(iSectionRemoved);

			if ( this.owner != null )  
				this.owner._OnSectionRemoved(iSectionRemoved, item);
		}

        /// <summary>
        /// Removes the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public virtual void Remove(HeaderSection item)
		{      
			int index = this.alSectionsByOrder.IndexOf(item);

			if ( index != -1 )
				RemoveAt(index);
		}

        /// <summary>
        /// Moves the specified i from.
        /// </summary>
        /// <param name="iFrom">The i from.</param>
        /// <param name="iTo">The i to.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// iFrom
        /// or
        /// iTo
        /// </exception>
        public void Move(int iFrom, int iTo)
		{
			if ( iFrom < 0 || iFrom >= this.alSectionsByOrder.Count )
				throw new ArgumentOutOfRangeException("iFrom", iFrom, ErrMsg.IndexOutOfRange());

			if ( iTo < 0 || iTo >= this.alSectionsByOrder.Count )
				throw new ArgumentOutOfRangeException("iTo", iTo, ErrMsg.IndexOutOfRange());

			_Move(iFrom, iTo);
		}

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.IList" />.
        /// </summary>
        public void Clear()
		{
			Debug.Assert( this.alSectionsByOrder.Count == this.alSectionsByRawIndex.Count );

			foreach( HeaderSection item in this.alSectionsByOrder )
			{
				UnbindSection(item);
			}

			this.alSectionsByOrder.Clear();
			this.alSectionsByRawIndex.Clear();

			if ( this.owner != null )  
				this.owner._OnAllSectionsRemoved();
		}

        /// <summary>
        /// Clears the specified b dispose items.
        /// </summary>
        /// <param name="bDisposeItems">if set to <c>true</c> [b dispose items].</param>
        internal void Clear(bool bDisposeItems)
    {
      Debug.Assert( this.alSectionsByOrder.Count == this.alSectionsByRawIndex.Count );

      foreach( HeaderSection item in this.alSectionsByOrder )
      {
        UnbindSection(item);

        if ( bDisposeItems )
          item.Dispose();
      }

      this.alSectionsByOrder.Clear();
      this.alSectionsByRawIndex.Clear();

      if ( this.owner != null )  
        this.owner._OnAllSectionsRemoved();
    }


        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        public int IndexOf(HeaderSection item)
		{
			return this.alSectionsByOrder.IndexOf(item);
		}

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        public bool Contains(HeaderSection item)
		{
			return this.alSectionsByOrder.IndexOf(item) != -1; 
		}

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="aDest">a dest.</param>
        /// <param name="index">The index.</param>
        public void CopyTo(Array aDest, int index)
		{
			this.alSectionsByOrder.CopyTo(aDest, index);
		}

        /// <summary>
        /// Implementation: IEnumerable
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
		{ 
			return this.alSectionsByOrder.GetEnumerator();
		}

        /// <summary>
        /// Implementation: ICollection
        /// </summary>
        /// <value><c>true</c> if this instance is synchronized; otherwise, <c>false</c>.</value>
        bool ICollection.IsSynchronized 
		{ 
			get { return true; }
		}

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <value>The synchronize root.</value>
        object ICollection.SyncRoot 
		{
			get { return this; }
		}


        /// <summary>
        /// Implementation: IList
        /// </summary>
        /// <value><c>true</c> if this instance is fixed size; otherwise, <c>false</c>.</value>
        bool IList.IsFixedSize 
		{
			get { return false; }
		}

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.IList" /> is read-only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        bool IList.IsReadOnly 
		{
			get { return false; }
		}

        /// <summary>
        /// Gets or sets the <see cref="System.Object"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Object.</returns>
        object IList.this[int index] 
		{
			get { return this.alSectionsByOrder[index]; } 
			set 
			{ 
				_SetSection(index, (HeaderSection)value);
			} 
		}

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.IList" /> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
        /// <param name="value">The object to insert into the <see cref="T:System.Collections.IList" />.</param>
        void IList.Insert(int index, object value)
		{
			Insert(index, (HeaderSection)value);
		}

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.IList" />.
        /// </summary>
        /// <param name="value">The object to add to the <see cref="T:System.Collections.IList" />.</param>
        /// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
        int IList.Add(object value)
		{     
			return Add((HeaderSection)value);
		}

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.IList" />.
        /// </summary>
        /// <param name="value">The object to remove from the <see cref="T:System.Collections.IList" />.</param>
        void IList.Remove(object value)
		{
			Remove((HeaderSection)value);
		}

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.IList" /> contains a specific value.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
        /// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
        bool IList.Contains(object value)
		{
			return this.alSectionsByOrder.Contains(value); 
		}

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.IList" />.
        /// </summary>
        /// <param name="value">The object to locate in the <see cref="T:System.Collections.IList" />.</param>
        /// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
        int IList.IndexOf(object value)
		{
			return this.alSectionsByOrder.IndexOf(value);
		}

	} // HeaderSectionCollection class


    #endregion // HeaderSectionCollection


    #region ZeroitColumnHeader Event Arguments' classes

    /// <summary>
    /// HeaderSectionEventArgs class
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    [Serializable]
	public class HeaderSectionEventArgs : EventArgs
	{
        // Fields
        /// <summary>
        /// The item
        /// </summary>
        private HeaderSection item = null;
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        public HeaderSection Item
		{
			get { return this.item; }
		}

        // Fields
        /// <summary>
        /// The en button
        /// </summary>
        private MouseButtons enButton = MouseButtons.None;
        /// <summary>
        /// Gets the button.
        /// </summary>
        /// <value>The button.</value>
        public MouseButtons Button
		{
			get { return this.enButton; }
		}

        // Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public HeaderSectionEventArgs(HeaderSection item)
		{
			this.item = item;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        public HeaderSectionEventArgs(HeaderSection item, MouseButtons enButton)
		{
			this.item = item;
			this.enButton = enButton;
		}

	} // HeaderSectionEventArgs

    /// <summary>
    /// Delegate HeaderSectionEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="ea">The <see cref="HeaderSectionEventArgs"/> instance containing the event data.</param>
    public delegate void HeaderSectionEventHandler(
							object sender, HeaderSectionEventArgs ea);


    /// <summary>
    /// HeaderSectionConformableEventArgs class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.HeaderSectionEventArgs" />
    [Serializable]
	public class HeaderSectionConformableEventArgs : HeaderSectionEventArgs
	{
        // Fields
        /// <summary>
        /// The b accepted
        /// </summary>
        private bool bAccepted = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HeaderSectionConformableEventArgs"/> is accepted.
        /// </summary>
        /// <value><c>true</c> if accepted; otherwise, <c>false</c>.</value>
        public bool Accepted
		{
			get { return this.bAccepted; }
			set { this.bAccepted = value; }
		}

        // Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public HeaderSectionConformableEventArgs(HeaderSection item)
			: base(item)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        public HeaderSectionConformableEventArgs(HeaderSection item, MouseButtons enButton)
			: base(item, enButton)
		{
		}

	} // HeaderSectionConformableEventArgs

    /// <summary>
    /// Delegate HeaderSectionConformableEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="ea">The <see cref="HeaderSectionConformableEventArgs"/> instance containing the event data.</param>
    public delegate void HeaderSectionConformableEventHandler(
							object sender, HeaderSectionConformableEventArgs ea);


    /// <summary>
    /// HeaderSectionWidthEventArgs class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.HeaderSectionEventArgs" />
    [Serializable]
	public class HeaderSectionWidthEventArgs : HeaderSectionEventArgs
	{
        // Fields
        /// <summary>
        /// The cx width
        /// </summary>
        private int cxWidth = 0;
        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>The width.</value>
        public int Width
		{
			get { return this.cxWidth; }
		}

        // Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionWidthEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public HeaderSectionWidthEventArgs(HeaderSection item)
			: base(item)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionWidthEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        public HeaderSectionWidthEventArgs(HeaderSection item, MouseButtons enButton)
			: base(item, enButton)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionWidthEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        public HeaderSectionWidthEventArgs(HeaderSection item, MouseButtons enButton, 
										   int cxWidth)
			: base(item, enButton)
		{
			this.cxWidth = cxWidth;
		}

	} // HeaderWidthItemEventArgs

    /// <summary>
    /// Delegate HeaderSectionWidthEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="ea">The <see cref="HeaderSectionWidthEventArgs"/> instance containing the event data.</param>
    public delegate void HeaderSectionWidthEventHandler(
							object sender, HeaderSectionWidthEventArgs ea);


    /// <summary>
    /// HeaderSectionWidthConformableEventArgs class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.HeaderSectionWidthEventArgs" />
    [Serializable]
	public class HeaderSectionWidthConformableEventArgs : HeaderSectionWidthEventArgs
	{
        // Fields
        /// <summary>
        /// The b accepted
        /// </summary>
        private bool bAccepted = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HeaderSectionWidthConformableEventArgs"/> is accepted.
        /// </summary>
        /// <value><c>true</c> if accepted; otherwise, <c>false</c>.</value>
        public bool Accepted
		{
			get { return this.bAccepted; }
			set { this.bAccepted = value; }
		}

        // Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionWidthConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public HeaderSectionWidthConformableEventArgs(HeaderSection item)
			: base(item)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionWidthConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        public HeaderSectionWidthConformableEventArgs(HeaderSection item, MouseButtons enButton)
			: base(item, enButton)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionWidthConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        /// <param name="cxWidth">Width of the cx.</param>
        public HeaderSectionWidthConformableEventArgs(HeaderSection item, MouseButtons enButton, 
													  int cxWidth)
			: base(item, enButton, cxWidth)
		{
		}

	} // HeaderSectionWidthConformableEventArgs

    /// <summary>
    /// Delegate HeaderSectionWidthConformableEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="ea">The <see cref="HeaderSectionWidthConformableEventArgs"/> instance containing the event data.</param>
    public delegate void HeaderSectionWidthConformableEventHandler(
							object sender, HeaderSectionWidthConformableEventArgs ea);


    /// <summary>
    /// HeaderSectionOrderEventArgs class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.HeaderSectionEventArgs" />
    [Serializable]
	public class HeaderSectionOrderEventArgs : HeaderSectionEventArgs
	{
        // Fields
        /// <summary>
        /// The i order
        /// </summary>
        private int iOrder = -1;
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order
		{
			get { return this.iOrder; }
		}

        // Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionOrderEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public HeaderSectionOrderEventArgs(HeaderSection item)
			: base(item)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionOrderEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        public HeaderSectionOrderEventArgs(HeaderSection item, MouseButtons enButton)
			: base(item, enButton)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionOrderEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        /// <param name="iOrder">The i order.</param>
        public HeaderSectionOrderEventArgs(HeaderSection item, MouseButtons enButton, 
										   int iOrder)
			: base(item, enButton)
		{
			this.iOrder = iOrder;
		}

	} // HeaderSectionOrderEventArgs

    /// <summary>
    /// Delegate HeaderSectionOrderEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="ea">The <see cref="HeaderSectionOrderEventArgs"/> instance containing the event data.</param>
    public delegate void HeaderSectionOrderEventHandler(
							object sender, HeaderSectionOrderEventArgs ea);


    /// <summary>
    /// HeaderSectionOrderConformableEventArgs class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.HeaderSectionOrderEventArgs" />
    [Serializable]
	public class HeaderSectionOrderConformableEventArgs : HeaderSectionOrderEventArgs
	{
        // Fields
        /// <summary>
        /// The b accepted
        /// </summary>
        private bool bAccepted = true;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HeaderSectionOrderConformableEventArgs"/> is accepted.
        /// </summary>
        /// <value><c>true</c> if accepted; otherwise, <c>false</c>.</value>
        public bool Accepted
		{
			get { return this.bAccepted; }
			set { this.bAccepted = value; }
		}

        // Construction
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionOrderConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public HeaderSectionOrderConformableEventArgs(HeaderSection item)
			: base(item)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionOrderConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        public HeaderSectionOrderConformableEventArgs(HeaderSection item, MouseButtons enButton)
			: base(item, enButton)
		{
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderSectionOrderConformableEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="enButton">The en button.</param>
        /// <param name="iOrder">The i order.</param>
        public HeaderSectionOrderConformableEventArgs(HeaderSection item, MouseButtons enButton, 
													  int iOrder)
			: base(item, enButton, iOrder)
		{
		}

	} // HeaderSectionOrderConformableEventArgs

    /// <summary>
    /// Delegate HeaderSectionOrderConformableEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="ea">The <see cref="HeaderSectionOrderConformableEventArgs"/> instance containing the event data.</param>
    public delegate void HeaderSectionOrderConformableEventHandler(
							object sender, HeaderSectionOrderConformableEventArgs ea);

    #endregion // HeaderEventArgs


    #region ZeroitColumnHeader control

    /// <summary>
    /// ZeroitColumnHeader class.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [
        Description("SP ZeroitColumnHeader Control"),
		DefaultProperty("Sections"),
		DefaultEvent("AfterSectionTrack"),
		Designer(typeof(HeaderDesigner)),
		SecurityPermission(SecurityAction.LinkDemand, 
						   Flags=SecurityPermissionFlag.UnmanagedCode)
	]
	public class ZeroitColumnHeader : Control
	{
        /// <summary>
        /// Types
        /// </summary>

        [Flags]
		public enum HitTestArea : int
		{

            /// <summary>
            /// The no where
            /// </summary>
            NoWhere = NativeHeader.HHT_NOWHERE,
            /// <summary>
            /// The on header
            /// </summary>
            OnHeader = NativeHeader.HHT_ONHEADER,
            /// <summary>
            /// The on divider
            /// </summary>
            OnDivider = NativeHeader.HHT_ONDIVIDER,
            /// <summary>
            /// The on divider open
            /// </summary>
            OnDividerOpen = NativeHeader.HHT_ONDIVOPEN,
            /// <summary>
            /// The on filter
            /// </summary>
            OnFilter = NativeHeader.HHT_ONFILTER,
            /// <summary>
            /// The on filter button
            /// </summary>
            OnFilterButton = NativeHeader.HHT_ONFILTERBUTTON,
            /// <summary>
            /// The above
            /// </summary>
            Above = NativeHeader.HHT_ABOVE,
            /// <summary>
            /// The below
            /// </summary>
            Below = NativeHeader.HHT_BELOW,
            /// <summary>
            /// To left
            /// </summary>
            ToLeft = NativeHeader.HHT_TOLEFT,
            /// <summary>
            /// To right
            /// </summary>
            ToRight = NativeHeader.HHT_TORIGHT
		}


        /// <summary>
        /// A struct containing HitTest information
        /// </summary>
        public struct HitTestInfo
		{
            /// <summary>
            /// The HitTest area
            /// </summary>
            public HitTestArea    fArea;
            /// <summary>
            /// The ZeroitColumnHeader section
            /// </summary>
            public HeaderSection  section;
		}

        /// <summary>
        /// Data Fields
        /// </summary>

        private int fStyle = NativeHeader.WS_CHILD;

        // Clickable
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitColumnHeader" /> is clickable.
        /// </summary>
        /// <value><c>true</c> if clickable; otherwise, <c>false</c>.</value>
        [
            Category("Behavior"),
			Description("Determines if control will generate events " +
						"when user clicks on its column titles." ),
			DefaultValue(false)
		]
		public bool Clickable
		{
			get { return (this.fStyle & NativeHeader.HDS_BUTTONS) != 0; }
			set 
			{ 
				bool bOldValue = (this.fStyle & NativeHeader.HDS_BUTTONS) != 0;

				if ( value != bOldValue )
				{
					if ( value )
						this.fStyle |= NativeHeader.HDS_BUTTONS;
					else
						this.fStyle &= (~NativeHeader.HDS_BUTTONS);

					if ( this.IsHandleCreated )
					{
						UpdateWndStyle();
					}
				}
			}
		}

        // HotTrack
        /// <summary>
        /// Gets or sets a value indicating whether hot tracking should be enabled.
        /// </summary>
        /// <value><c>true</c> if HotTrack; otherwise, <c>false</c>.</value>
        [
            Category("Behavior"),
			Description("Enables or disables hot tracking." ),
			DefaultValue(false)
		]
		public bool HotTrack 
		{
			get { return (this.fStyle & NativeHeader.HDS_HOTTRACK) != 0; }
			set 
			{ 
				bool bOldValue = (this.fStyle & NativeHeader.HDS_HOTTRACK) != 0;

				if ( value != bOldValue )
				{
					if ( value )
						this.fStyle |= NativeHeader.HDS_HOTTRACK;
					else
						this.fStyle &= (~NativeHeader.HDS_HOTTRACK);

					if ( this.IsHandleCreated )
					{
						UpdateWndStyle();
					}
				}
			}
		}

        // Flat
        /// <summary>
        /// Causes the header control to be drawn flat.
        /// </summary>
        /// <value><c>true</c> if flat; otherwise, <c>false</c>.</value>
        [
            Category("Appearance"),
			Description("Causes the header control to be drawn flat when " + 
						"Microsoft� Windows� XP is running in classic mode." ),
			DefaultValue(false)
		]
		public bool Flat 
		{
			get { return (this.fStyle & NativeHeader.HDS_FLAT) != 0; }
			set 
			{ 
				bool bOldValue = (this.fStyle & NativeHeader.HDS_FLAT) != 0;

				if ( value != bOldValue )
				{
					if ( value )
						this.fStyle |= NativeHeader.HDS_FLAT;
					else
						this.fStyle &= (~NativeHeader.HDS_FLAT);

					if ( this.IsHandleCreated )
					{
						UpdateWndStyle();
					}
				}
			}
		}

        // AllowDragSections
        /// <summary>
        /// Determines if user will be able to drag header column on another position.
        /// </summary>
        /// <value><c>true</c> if allow drag; otherwise, <c>false</c>.</value>
        [
            Category("Behavior"),
			Description("Determines if user will be able to drag header column " + 
						"on another position." ),
			DefaultValue(false)
		]
		public bool AllowDragSections
		{
			get { return (this.fStyle & NativeHeader.HDS_DRAGDROP) != 0; }
			set 
			{ 
				bool bOldValue = (this.fStyle & NativeHeader.HDS_DRAGDROP) != 0;

				if ( value != bOldValue )
				{
					if ( value )
						this.fStyle |= NativeHeader.HDS_DRAGDROP;
					else
						this.fStyle &= (~NativeHeader.HDS_DRAGDROP);

					if ( this.IsHandleCreated )
					{
						UpdateWndStyle();
					}
				}
			}
		}

        // FullDragSections
        /// <summary>
        /// Causes the header control to display column contents even while the user resizes a column.
        /// </summary>
        /// <value><c>true</c> if full drag sections; otherwise, <c>false</c>.</value>
        [
            Category("Behavior"),
			Description("Causes the header control to display column contents " + 
						"even while the user resizes a column." ),
			DefaultValue(false)
		]
		public bool FullDragSections
		{
			get { return (this.fStyle & NativeHeader.HDS_FULLDRAG) != 0; }
			set 
			{ 
				bool bOldValue = (this.fStyle & NativeHeader.HDS_FULLDRAG) != 0;

				if ( value != bOldValue )
				{
					if ( value )
						this.fStyle |= NativeHeader.HDS_FULLDRAG;
					else
						this.fStyle &= (~NativeHeader.HDS_FULLDRAG);

					if ( this.IsHandleCreated )
					{
						UpdateWndStyle();
					}
				}
			}
		}

        // Sections
        /// <summary>
        /// The col sections
        /// </summary>
        private HeaderSectionCollection colSections = null;

        /// <summary>
        /// Sections of the header.
        /// </summary>
        /// <value>The sections.</value>
        [
            Category("Data"),
			Description("Sections of the header." ),
			MergableProperty(false),
			DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
			Localizable(true)
		]
		public HeaderSectionCollection Sections
		{
			get { return this.colSections; }
		}

        /// <summary>
        /// The image list
        /// </summary>
        private ImageList imageList = null;
        /// <summary>
        /// Images for header's sections.
        /// </summary>
        /// <value>The image list.</value>
        [
            Category("Data"),
			Description("Images for header's sections." ),
			DefaultValue(null)
		]
		public ImageList ImageList
		{
			get { return this.imageList; }
			set 
			{ 
				if ( this.imageList != value )
				{
					EventHandler ehRecreateHandle = 
						new EventHandler(this.OnImageListRecreateHandle);

					EventHandler ehDetachImageList = 
						new EventHandler(this.OnDetachImageList);

					if ( this.imageList != null )
					{
						this.imageList.RecreateHandle -= ehRecreateHandle;
						this.imageList.Disposed -= ehDetachImageList;
					}

					this.imageList = value; 

					if ( this.imageList != null )
					{
						this.imageList.RecreateHandle += ehRecreateHandle;
						this.imageList.Disposed += ehDetachImageList;
					}         

					if ( IsHandleCreated )
					{
						HandleRef hrThis = new HandleRef(this, this.Handle);

						UpdateWndImageList(ref hrThis, this.imageList);            
					}
				}
			}
		}

        /// <summary>
        /// The cx bitmap margin
        /// </summary>
        private int cxBitmapMargin = SystemInformation.Border3DSize.Width;
        /// <summary>
        /// Width of the margin that surrounds a bitmap within an existing header control.
        /// </summary>
        /// <value>The bitmap margin.</value>
        /// <exception cref="ArgumentOutOfRangeException">value</exception>
        [
            Category("Appearance"),
			Description("Width of the margin that surrounds a bitmap " +
						"within an existing header control." ),
			DefaultValue(2)
		]
		public int BitmapMargin
		{
			get { return this.cxBitmapMargin; }
			set 
			{ 
				if ( this.cxBitmapMargin != value )
				{
					if ( value < 0 )
						throw new ArgumentOutOfRangeException("value", value, ErrMsg.NegVal());

					this.cxBitmapMargin = value; 

					if ( IsHandleCreated )
					{
						HandleRef hrThis = new HandleRef(this, this.Handle);

						UpdateWndBitmapMargin(ref hrThis, this.cxBitmapMargin);
					}
				}
			}
		}

        /// <summary>
        /// Construction and finalization
        /// </summary>

        public ZeroitColumnHeader()
			: base()
		{
			this.SetStyle(ControlStyles.UserPaint, false);
			//this.SetStyle(ControlStyles.UserMouse, false); // ???
			this.SetStyle(ControlStyles.StandardClick, false);
			this.SetStyle(ControlStyles.StandardDoubleClick, false);
			this.SetStyle(ControlStyles.Opaque, true);
			this.SetStyle(ControlStyles.ResizeRedraw, this.DesignMode);
			this.SetStyle(ControlStyles.Selectable, false); 
			//this.SetStyle(ControlStyles.AllPaintingInWmPaint, false); // ???

			this.colSections = new HeaderSectionCollection(this);
		}

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>

        protected override void Dispose(bool disposing)
		{
			if ( disposing )
			{
				if ( this.imageList != null )
				{
					this.imageList.RecreateHandle -= 
						new EventHandler(this.OnImageListRecreateHandle);

					this.imageList.Disposed -= 
						new EventHandler(this.OnDetachImageList);

					this.imageList = null;
				}

				this.colSections.Clear(true);
			}

			base.Dispose( disposing );
		}

        /// <summary>
        /// Helpers
        /// </summary>
        /// <param name="nmh">The NMH.</param>
        /// <returns>System.Int32.</returns>

        private int ExtractIndexFromNMHEADER(ref NativeHeader.NMHEADER nmh)
		{
			return nmh.iItem;
		}

        /// <summary>
        /// Extracts the mouse button from nmheader.
        /// </summary>
        /// <param name="nmh">The NMH.</param>
        /// <returns>MouseButtons.</returns>
        private MouseButtons ExtractMouseButtonFromNMHEADER(ref NativeHeader.NMHEADER nmh)
		{
			switch ( nmh.iButton )
			{
			case 0:
				return MouseButtons.Left;

			case 1:
				return MouseButtons.Right;

			case 2:
				return MouseButtons.Middle;         
			}

			return MouseButtons.None;
		}

        /// <summary>
        /// Extracts the section data from nmheader.
        /// </summary>
        /// <param name="nmh">The NMH.</param>
        /// <param name="item">The item.</param>
        private void ExtractSectionDataFromNMHEADER(ref NativeHeader.NMHEADER nmh,
													ref NativeHeader.HDITEM2 item)
		{
			if ( nmh.pitem != IntPtr.Zero )
			{
				item = (NativeHeader.HDITEM2)Marshal.PtrToStructure(
												nmh.pitem, typeof(NativeHeader.HDITEM2));
			}
		}

        /// <summary>
        /// Updates the WND image list.
        /// </summary>
        /// <param name="hrThis">The hr this.</param>
        /// <param name="imageList">The image list.</param>
        private static void UpdateWndImageList(ref HandleRef hrThis, ImageList imageList)
		{
			Debug.Assert( hrThis.Handle != IntPtr.Zero );

			IntPtr hIL = (imageList != null) ? imageList.Handle : IntPtr.Zero;

			NativeHeader.SetImageList(hrThis.Handle, new HandleRef(imageList, hIL).Handle);        
		}

        /// <summary>
        /// Updates the WND bitmap margin.
        /// </summary>
        /// <param name="hrThis">The hr this.</param>
        /// <param name="cxMargin">The cx margin.</param>
        private static void UpdateWndBitmapMargin(ref HandleRef hrThis, int cxMargin)
		{
			Debug.Assert( hrThis.Handle != IntPtr.Zero );

			NativeHeader.SetBitmapMargin(hrThis.Handle, cxMargin);
		}

        /// <summary>
        /// Updates the WND style.
        /// </summary>
        /// <param name="hrThis">The hr this.</param>
        /// <param name="fNewStyle">The f new style.</param>
        private static void UpdateWndStyle(ref HandleRef hrThis, int fNewStyle)
		{
			Debug.Assert( hrThis.Handle != IntPtr.Zero );

			const int fOptions = NativeHeader.SWP_NOSIZE|
								 NativeHeader.SWP_NOMOVE|
								 NativeHeader.SWP_NOZORDER|
								 NativeHeader.SWP_NOACTIVATE|
								 NativeHeader.SWP_FRAMECHANGED;

			int fStyle = NativeHeader.GetWindowLong(hrThis.Handle, NativeHeader.GWL_STYLE);
			fStyle &= ~(NativeHeader.HDS_BUTTONS|
						NativeHeader.HDS_HOTTRACK|
						NativeHeader.HDS_FLAT|
						NativeHeader.HDS_DRAGDROP|
						NativeHeader.HDS_FULLDRAG);

			fStyle |= fNewStyle;

			NativeHeader.SetWindowLong(hrThis.Handle, NativeHeader.GWL_STYLE, fStyle);

			NativeHeader.SetWindowPos(hrThis.Handle, IntPtr.Zero, 0, 0, 0, 0, fOptions);
		}

        /// <summary>
        /// Updates the WND style.
        /// </summary>
        private void UpdateWndStyle()
		{
			HandleRef hrThis = new HandleRef(this, this.Handle);

			UpdateWndStyle(ref hrThis, this.fStyle);
		}

        /// <summary>
        /// Internal notifications
        /// </summary>
        /// <param name="ea">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>

        protected override void OnHandleCreated(EventArgs ea)
		{
			HandleRef hrThis = new HandleRef(this, this.Handle);

			// Set Window Style
			UpdateWndStyle(ref hrThis, this.fStyle);

			// Set Bitmap Margin
			UpdateWndBitmapMargin(ref hrThis, this.cxBitmapMargin);

			// Set ImageList
			UpdateWndImageList(ref hrThis, this.imageList);

			// Add items
			for ( int i = 0; i < this.colSections.Count; i++ )
			{
				HeaderSection item = colSections[i];

				NativeHeader.HDITEM hdi;
				item.ComposeNativeData(i, out hdi);

				int nResult = NativeHeader.InsertItem(this.Handle, i, ref hdi);
				Debug.Assert( nResult >= 0 );
				if ( nResult < 0 )
					throw new InvalidOperationException(ErrMsg.FailedToInsertItem(), 
														new Win32Exception());
			}

			base.OnHandleCreated(ea);
		}

        /// <summary>
        /// Handles the <see cref="E:HandleDestroyed" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleDestroyed(EventArgs ea)
		{
			// Collect item parameters from native window

			base.OnHandleDestroyed(ea); 
		}

        /// <summary>
        /// Handles the <see cref="E:EnabledChanged" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnEnabledChanged(EventArgs ea)
		{
			base.OnEnabledChanged(ea);
		}

        /// <summary>
        /// Handles the <see cref="E:FontChanged" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnFontChanged(EventArgs ea)
		{
			base.OnFontChanged(ea);
		}

        /// <summary>
        /// Ons the section inserted.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionInserted(int index, HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				NativeHeader.HDITEM hdi;
				hdi.lpszText = null;

				item.ComposeNativeData(index, out hdi);        

				int iResult = NativeHeader.InsertItem(new HandleRef(this, this.Handle).Handle, 
													  index, ref hdi);
				Debug.Assert( iResult == index );
				if ( iResult < 0 )
					throw new InvalidOperationException(ErrMsg.FailedToInsertItem(), 
														new Win32Exception());
			}
		}

        /// <summary>
        /// Ons the section removed.
        /// </summary>
        /// <param name="iRawIndex">Index of the i raw.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionRemoved(int iRawIndex, HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.DeleteItem(hrThis.Handle, iRawIndex);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToRemoveItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons all sections removed.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnAllSectionsRemoved()
		{
			if ( this.IsHandleCreated )
			{
				BeginUpdate();

				try
				{
					HandleRef hrThis = new HandleRef(this, this.Handle);

					while ( NativeHeader.GetItemCount(this.Handle) != 0  )
					{
						bool bResult = NativeHeader.DeleteItem(hrThis.Handle, 0);
						Debug.Assert( bResult );
						if ( !bResult )
						{
							throw new InvalidOperationException(ErrMsg.FailedToRemoveItem(), 
																new Win32Exception());
						}

					}
				}
				finally
				{
					EndUpdate();
				}
			}
		}

        /// <summary>
        /// Ons the section changed.
        /// </summary>
        /// <param name="iRawIndex">Index of the i raw.</param>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionChanged(int iRawIndex, HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				NativeHeader.HDITEM hdi;
				item.ComposeNativeData(-1, out hdi);

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iRawIndex, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section width changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionWidthChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_WIDTH;
				hdi.cxy = item.Width;

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section text changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionTextChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_FORMAT|NativeHeader.HDI_TEXT;
				hdi.fmt = item.Format;
				hdi.lpszText = item.Text;

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section image index changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionImageIndexChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_FORMAT|NativeHeader.HDI_IMAGE;
				hdi.fmt = item.Format;
				hdi.iImage = item.ImageIndex;

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section bitmap changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionBitmapChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_FORMAT|NativeHeader.HDI_BITMAP;
				hdi.fmt = item.Format;
				hdi.hbm = item._GetHBitmap();

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section right to left changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionRightToLeftChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_FORMAT;
				hdi.fmt = item.Format;

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section content align changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionContentAlignChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_FORMAT;
				hdi.fmt = item.Format;

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section image align changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionImageAlignChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_FORMAT;
				hdi.fmt = item.Format;

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Ons the section sort mark changed.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        internal void _OnSectionSortMarkChanged(HeaderSection item)
		{
			if ( this.IsHandleCreated )
			{
				int iSection = this.colSections._FindSectionRawIndex(item);
				Debug.Assert( iSection >= 0 );

				NativeHeader.HDITEM hdi = new NativeHeader.HDITEM();
				hdi.mask = NativeHeader.HDI_FORMAT;
				hdi.fmt = item.Format;

				HandleRef hrThis = new HandleRef(this, this.Handle);

				bool bResult = NativeHeader.SetItem(hrThis.Handle, iSection, ref hdi);
				Debug.Assert( bResult );
				if ( !bResult )
				{
					throw new InvalidOperationException(ErrMsg.FailedToChangeItem(), 
														new Win32Exception());
				}
			}
		}

        /// <summary>
        /// Handles the <see cref="E:ImageListRecreateHandle" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="ea">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnImageListRecreateHandle(object sender, EventArgs ea)
		{
			if ( IsHandleCreated )
			{
				HandleRef hrThis = new HandleRef(this, this.Handle);

				UpdateWndImageList(ref hrThis, this.imageList);
			}
		}

        /// <summary>
        /// Handles the <see cref="E:DetachImageList" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="ea">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDetachImageList(object sender, EventArgs ea)
		{
			if ( sender == this.imageList )
			{
				this.imageList = null;

				if ( IsHandleCreated )
				{
					HandleRef hrThis = new HandleRef(this, this.Handle);

					UpdateWndImageList(ref hrThis, this.imageList);
				}
			}
		}

        /// <summary>
        /// Events
        /// </summary>

        [
            Description("Occurs when user clicks on the section.")
		]
		public event HeaderSectionEventHandler SectionClick;
        /// <summary>
        /// Handles the <see cref="E:SectionClick" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSectionClick(HeaderSectionEventArgs ea)
		{
			if ( this.SectionClick != null )
				this.SectionClick(this, ea);
		}

        /// <summary>
        /// Occurs when [section double click].
        /// </summary>
        [
            Description("Occurs when user performs double clicks on the section.")
		]
		public event HeaderSectionEventHandler SectionDblClick;

        /// <summary>
        /// Handles the <see cref="E:SectionDblClick" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnSectionDblClick(HeaderSectionEventArgs ea)
		{
			if ( this.SectionDblClick != null )
				this.SectionDblClick(this, ea);
		}

        /// <summary>
        /// Occurs when [divider double click].
        /// </summary>
        [
            Description("Occurs when user performs double click on section's divider.")
		]
		public event HeaderSectionEventHandler DividerDblClick;

        /// <summary>
        /// Handles the <see cref="E:DividerDblClick" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionEventArgs"/> instance containing the event data.</param>
        protected virtual void OnDividerDblClick(HeaderSectionEventArgs ea)
		{
			if ( this.DividerDblClick != null )
				this.DividerDblClick(this, ea);
		}

        /// <summary>
        /// Occurs when [before section track].
        /// </summary>
        [
            Description("Occurs when user is about to start resizing of the section.")
		]
		public event HeaderSectionWidthConformableEventHandler BeforeSectionTrack;
        /// <summary>
        /// Handles the <see cref="E:BeforeSectionTrack" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionWidthConformableEventArgs"/> instance containing the event data.</param>
        protected void OnBeforeSectionTrack(HeaderSectionWidthConformableEventArgs ea)
		{
			if ( this.BeforeSectionTrack != null )
			{
				Delegate[] aHandlers = this.BeforeSectionTrack.GetInvocationList();
        
				foreach( HeaderSectionWidthConformableEventHandler handler in aHandlers )
				{
					try
					{
						handler(this, ea);
					}
					catch ( Exception )
					{
						ea.Accepted = false;
					}
       
					if ( !ea.Accepted )
						break;
				}
			}
		}

        /// <summary>
        /// Occurs when [section tracking].
        /// </summary>
        [
            Description("Occurs when user is resizing the section.")
		]
		public event HeaderSectionWidthConformableEventHandler SectionTracking;
        /// <summary>
        /// Handles the <see cref="E:SectionTracking" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionWidthConformableEventArgs"/> instance containing the event data.</param>
        protected void OnSectionTracking(HeaderSectionWidthConformableEventArgs ea)
		{
			if ( this.SectionTracking != null )
			{
				Delegate[] aHandlers = this.SectionTracking.GetInvocationList();
        
				foreach( HeaderSectionWidthConformableEventHandler handler in aHandlers )
				{
					try
					{
						handler(this, ea);
					}
					catch ( Exception )
					{
						ea.Accepted = false;
					}
       
					if ( !ea.Accepted )
						break;
				}
			}
		}

        /// <summary>
        /// Occurs when [after section track].
        /// </summary>
        [
            Description("Occurs when user has section resized.")
		]
		public event HeaderSectionWidthEventHandler AfterSectionTrack;

        /// <summary>
        /// Handles the <see cref="E:AfterSectionTrack" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionWidthEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterSectionTrack(HeaderSectionWidthEventArgs ea)
		{
			if ( this.AfterSectionTrack != null )
				this.AfterSectionTrack(this, ea);
		}

        /// <summary>
        /// Occurs when [before section drag].
        /// </summary>
        [
            Description("Occurs when user is about to start dragging of the " + 
						      "section to another position.")
		]
		public event HeaderSectionOrderConformableEventHandler BeforeSectionDrag;
        /// <summary>
        /// Handles the <see cref="E:BeforeSectionDrag" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionOrderConformableEventArgs"/> instance containing the event data.</param>
        protected void OnBeforeSectionDrag(HeaderSectionOrderConformableEventArgs ea)
		{
			if ( this.BeforeSectionDrag != null )
			{
				Delegate[] aHandlers = this.BeforeSectionDrag.GetInvocationList();
        
				foreach( HeaderSectionOrderConformableEventHandler handler in aHandlers )
				{
					try
					{
						handler(this, ea);
					}
					catch ( Exception )
					{
						ea.Accepted = false;
					}
       
					if ( !ea.Accepted )
						break;
				}
			}
		}

        /// <summary>
        /// Occurs when [after section drag].
        /// </summary>
        [
            Description("Occurs when user has drugged the section to another position")
		]
		public event HeaderSectionOrderConformableEventHandler AfterSectionDrag;
        /// <summary>
        /// Handles the <see cref="E:AfterSectionDrag" /> event.
        /// </summary>
        /// <param name="ea">The <see cref="HeaderSectionOrderConformableEventArgs"/> instance containing the event data.</param>
        protected virtual void OnAfterSectionDrag(HeaderSectionOrderConformableEventArgs ea)
		{
			if ( this.AfterSectionDrag != null )
			{
				Delegate[] aHandlers = this.AfterSectionDrag.GetInvocationList();
        
				foreach( HeaderSectionOrderConformableEventHandler handler in aHandlers )
				{
					try
					{
						handler(this, ea);
					}
					catch ( Exception )
					{
						ea.Accepted = false;
					}
       
					if ( !ea.Accepted )
						break;
				}
			}
		}

        /// <summary>
        /// Operations
        /// </summary>
        /// <value>The default size.</value>
        protected override Size DefaultSize
		{
			get { return new Size(168, 24); }
		}

        /// <summary>
        /// Creates a handle for the control.
        /// </summary>
        protected override void CreateHandle()
		{
			if ( !this.RecreatingHandle )
			{
				InitCommonControlsHelper.Init(InitCommonControlsHelper.Classes.Header);
			}

			base.CreateHandle();
		}

        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        /// <value>The create parameters.</value>
        protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;

				createParams.ClassName = NativeHeader.WC_HEADER;
				createParams.Style &= ~(NativeHeader.HDS_BUTTONS|
										NativeHeader.HDS_HOTTRACK|
										NativeHeader.HDS_FLAT|
										NativeHeader.HDS_DRAGDROP|
										NativeHeader.HDS_FULLDRAG);

				createParams.Style |= this.fStyle;
         
				return createParams;
			}
		}

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        protected override void WndProc(ref Message msg)
		{            
			switch ( msg.Msg )
			{
			// Handle notifications
			case (NativeHeader.WM_NOTIFY + NativeHeader.OCM__BASE):
				{
					NativeWindowCommon.NMHDR nmhdr = 
						(NativeWindowCommon.NMHDR)msg.GetLParam(typeof(NativeWindowCommon.NMHDR));

					if ( nmhdr.code == NativeHeader.HDN_ITEMCHANGING )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						int cxWidth = 0;

						NativeHeader.HDITEM2 hdi = new NativeHeader.HDITEM2();
						hdi.mask = 0;

						ExtractSectionDataFromNMHEADER(ref nmh, ref hdi);
						if ( (hdi.mask & NativeHeader.HDI_WIDTH) != 0 && this.FullDragSections )
						{
							cxWidth = hdi.cxy;

							HeaderSectionWidthConformableEventArgs ea = 
								new HeaderSectionWidthConformableEventArgs(item, enButton, cxWidth);

							OnSectionTracking(ea);

							msg.Result = ea.Accepted ? (IntPtr)0 : (IntPtr)1;
							return;
						}
					}
					else if ( nmhdr.code == NativeHeader.HDN_ITEMCHANGED )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						int cxWidth = 0;

						NativeHeader.HDITEM2 hdi = new NativeHeader.HDITEM2();
						hdi.mask = 0;

						ExtractSectionDataFromNMHEADER(ref nmh, ref hdi);
						if ( (hdi.mask & NativeHeader.HDI_WIDTH) != 0 && this.FullDragSections )
						{
							cxWidth = hdi.cxy;

							HeaderSectionWidthEventArgs ea = 
								new HeaderSectionWidthEventArgs(item, enButton, cxWidth);

							item._SetWidth(cxWidth);

							OnAfterSectionTrack(ea);
						}				
					}
					else if ( nmhdr.code == NativeHeader.HDN_ITEMCLICK )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);

						HeaderSectionEventArgs ea = new HeaderSectionEventArgs(item, enButton);

						OnSectionClick(ea);
					}
					else if ( nmhdr.code == NativeHeader.HDN_ITEMDBLCLICK )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);

						HeaderSectionEventArgs ea = new HeaderSectionEventArgs(item, enButton);

						OnSectionDblClick(ea);
					}
					else if ( nmhdr.code == NativeHeader.HDN_DIVIDERDBLCLICK )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);

						HeaderSectionEventArgs ea = new HeaderSectionEventArgs(item, enButton);

						OnDividerDblClick(ea);
					}
					else if ( nmhdr.code == NativeHeader.HDN_BEGINTRACK )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						int cxWidth = 0;

						NativeHeader.HDITEM2 hdi = new NativeHeader.HDITEM2();
						hdi.mask = 0;

						ExtractSectionDataFromNMHEADER(ref nmh, ref hdi);
						if ( (hdi.mask & NativeHeader.HDI_WIDTH) != 0 )
						{
							cxWidth = hdi.cxy;
						}

						HeaderSectionWidthConformableEventArgs ea = 
							new HeaderSectionWidthConformableEventArgs(item, enButton, cxWidth);

						OnBeforeSectionTrack(ea);

						msg.Result = ea.Accepted ? (IntPtr)0 : (IntPtr)1;
						return;
					}
					else if ( nmhdr.code == NativeHeader.HDN_TRACK )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						int cxWidth = 0;

						NativeHeader.HDITEM2 hdi = new NativeHeader.HDITEM2();
						hdi.mask = 0;

						ExtractSectionDataFromNMHEADER(ref nmh, ref hdi);
						if ( (hdi.mask & NativeHeader.HDI_WIDTH) != 0 )
						{
							cxWidth = hdi.cxy;
						}

						HeaderSectionWidthConformableEventArgs ea = 
							new HeaderSectionWidthConformableEventArgs(item, enButton, cxWidth);

						OnSectionTracking(ea);

						msg.Result = ea.Accepted ? (IntPtr)0 : (IntPtr)1;
						return;
					}
					else if ( nmhdr.code == NativeHeader.HDN_ENDTRACK )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);
						int cxWidth = 0;

						NativeHeader.HDITEM2 hdi = new NativeHeader.HDITEM2();
						hdi.mask = 0;

						ExtractSectionDataFromNMHEADER(ref nmh, ref hdi);
						if ( (hdi.mask & NativeHeader.HDI_WIDTH) != 0 )
						{
							cxWidth = hdi.cxy;
						}

						HeaderSectionWidthEventArgs ea = 
							new HeaderSectionWidthEventArgs(item, enButton, cxWidth);

						item._SetWidth(cxWidth);

						OnAfterSectionTrack(ea);
					}
					else if ( nmhdr.code == NativeHeader.HDN_BEGINDRAG )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);
						MouseButtons enButton = MouseButtons.Left; // Microsoft bugfix
						// MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);

						int iOrder = this.colSections.IndexOf(item);

						HeaderSectionOrderConformableEventArgs ea = 
							new HeaderSectionOrderConformableEventArgs(item, enButton, iOrder);

						OnBeforeSectionDrag(ea);

						msg.Result = ea.Accepted ? (IntPtr)0 : (IntPtr)1;
						return;
					}
					else if ( nmhdr.code == NativeHeader.HDN_ENDDRAG )
					{
						NativeHeader.NMHEADER nmh = 
							(NativeHeader.NMHEADER)msg.GetLParam(typeof(NativeHeader.NMHEADER));

						int iSection = ExtractIndexFromNMHEADER(ref nmh);
						HeaderSection item = this.colSections._GetSectionByRawIndex(iSection);
						MouseButtons enButton = ExtractMouseButtonFromNMHEADER(ref nmh);

						NativeHeader.HDITEM2 hdi = new NativeHeader.HDITEM2();
						hdi.mask = 0;

						ExtractSectionDataFromNMHEADER(ref nmh, ref hdi);
						Debug.Assert( (hdi.mask & NativeHeader.HDI_ORDER) != 0 );
						int iNewOrder = hdi.iOrder;

						HeaderSectionOrderConformableEventArgs ea = 
							new HeaderSectionOrderConformableEventArgs(item, enButton, iNewOrder);

						OnAfterSectionDrag(ea);

						// Update orders
						if ( ea.Accepted )
						{
							int iOldOrder = this.colSections.IndexOf(item);

							this.colSections._Move(iOldOrder, iNewOrder);
						}

						msg.Result = ea.Accepted ? (IntPtr)0 : (IntPtr)1;
						return;
					}
					          
					//		  else if ( nmhdr.code == NativeHeader.HDN_GETDISPINFO )
					//		  {
					//		  }
					//		  else if ( nmhdr.code == NativeHeader.HDN_FILTERCHANGE )
					//		  {
					//		  }
					//		  else if ( nmhdr.code == NativeHeader.HDN_FILTERBTNCLICK )
					//		  {
					//		  }
				}
				break;

			case NativeHeader.WM_SETCURSOR:
				DefWndProc(ref msg);
				return;
			}

			base.WndProc(ref msg);
		}

        /// <summary>
        /// Operations
        /// </summary>

        public void BeginUpdate()
		{
			if ( this.IsHandleCreated )
			{
				HandleRef hrThis = new HandleRef(this, this.Handle);

				NativeWindowCommon.SendMessage(hrThis.Handle, NativeWindowCommon.WM_SETREDRAW, 
											   0, 0);
			}
		}

        /// <summary>
        /// Ends the update.
        /// </summary>
        public void EndUpdate()
		{
			if ( this.IsHandleCreated )
			{
				HandleRef hrThis = new HandleRef(this, this.Handle);

				NativeWindowCommon.SendMessage(hrThis.Handle, NativeWindowCommon.WM_SETREDRAW, 
											   1, 0);
			}
		}

        /// <summary>
        /// Gets the section rect.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Rectangle.</returns>
        /// <exception cref="Win32Exception"></exception>
        public Rectangle GetSectionRect(HeaderSection item)
		{
			int iSection = this.colSections._FindSectionRawIndex(item);
			Debug.Assert( iSection >= 0 );

			HandleRef hrThis = new HandleRef(this, this.Handle);

			NativeHeader.RECT rc;
			bool bResult = NativeHeader.GetItemRect(hrThis.Handle, iSection, out rc);
			Debug.Assert( bResult );
			if ( !bResult )
				throw new Win32Exception();

			return Rectangle.FromLTRB(rc.left, rc.top, rc.right, rc.bottom);
		}

        /// <summary>
        /// Calculates the layout.
        /// </summary>
        /// <param name="rectArea">The rect area.</param>
        /// <param name="rectPosition">The rect position.</param>
        /// <exception cref="Win32Exception"></exception>
        public void CalculateLayout(Rectangle rectArea, out Rectangle rectPosition)
		{    
			NativeHeader.HDLAYOUT hdl = new NativeHeader.HDLAYOUT();
			hdl.prc = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeHeader.RECT)));
			hdl.pwpos = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeHeader.WINDOWPOS)));

			try
			{
				HandleRef hrThis = new HandleRef(this, this.Handle);

				NativeHeader.RECT rc = new NativeHeader.RECT();     
				rc.left = rectArea.Left;
				rc.top = rectArea.Top;
				rc.right = rectArea.Right;
				rc.bottom = rectArea.Bottom;   

				Marshal.StructureToPtr(rc, hdl.prc, false);

				bool bResult = NativeHeader.Layout(hrThis.Handle, ref hdl);
				Debug.Assert( bResult );
				if ( !bResult )
					throw new Win32Exception();

				NativeHeader.WINDOWPOS wp = 
					(NativeHeader.WINDOWPOS)Marshal.PtrToStructure(hdl.pwpos, 
														typeof(NativeHeader.WINDOWPOS));

				rectPosition = new Rectangle(wp.x, wp.y, wp.cx, wp.cy);
			}
			finally
			{
				if ( hdl.prc != IntPtr.Zero )
					Marshal.FreeHGlobal(hdl.prc);

				if ( hdl.pwpos != IntPtr.Zero )
					Marshal.FreeHGlobal(hdl.pwpos);
			}
		}

        /// <summary>
        /// Sets the hot divider.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>System.Int32.</returns>
        public int SetHotDivider(int x, int y)
		{
			HandleRef hrThis = new HandleRef(this, this.Handle);

			return NativeHeader.SetHotDivider(hrThis.Handle, true, (y << 16) | x);
		}

        /// <summary>
        /// Sets the hot divider.
        /// </summary>
        /// <param name="iDevider">The i devider.</param>
        /// <returns>System.Int32.</returns>
        public int SetHotDivider(int iDevider)
		{
			HandleRef hrThis = new HandleRef(this, this.Handle);

			return NativeHeader.SetHotDivider(hrThis.Handle, false, iDevider);
		}

        /// <summary>
        /// Hits the test.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>HitTestInfo.</returns>
        public HitTestInfo HitTest(int x, int y)
		{
			return HitTest(new Point(x, y));
		}

        /// <summary>
        /// Hits the test.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>HitTestInfo.</returns>
        public HitTestInfo HitTest(Point point)
		{
			HandleRef hrThis = new HandleRef(this, this.Handle);

			Point pointClient = PointToClient(point);

			NativeHeader.HDHITTESTINFO htiRaw = new NativeHeader.HDHITTESTINFO();
			htiRaw.pt.x = pointClient.X;
			htiRaw.pt.y = pointClient.Y;
			htiRaw.iItem = -1;
			htiRaw.flags = 0;
    
			NativeHeader.HitTest(hrThis.Handle, ref htiRaw);
     
			HitTestInfo hti = new HitTestInfo();
			hti.fArea = (HitTestArea)htiRaw.flags;

			if ( htiRaw.iItem >= 0 )
			{
				hti.section = this.colSections._GetSectionByRawIndex(htiRaw.iItem);
			}

			return hti;
		}

	}

#endregion // ZeroitColumnHeader control

}
