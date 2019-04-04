// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 04-26-2018
// ***********************************************************************
// <copyright file="NativeWrappers.cs" company="Zeroit Dev Technologies">
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
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Zeroit.Framework.Labels.Headers
{
    /// <summary>
    /// InitCommonControlsHelper class
    /// </summary>
    internal class InitCommonControlsHelper
	{
        /// <summary>
        /// Constants: Platform
        /// </summary>
        const int ICC_LISTVIEW_CLASSES   = 0x00000001;
        /// <summary>
        /// The icc treeview classes
        /// </summary>
        const int ICC_TREEVIEW_CLASSES   = 0x00000002;
        /// <summary>
        /// The icc bar classes
        /// </summary>
        const int ICC_BAR_CLASSES        = 0x00000004;
        /// <summary>
        /// The icc tab classes
        /// </summary>
        const int ICC_TAB_CLASSES        = 0x00000008;
        /// <summary>
        /// The icc updown class
        /// </summary>
        const int ICC_UPDOWN_CLASS       = 0x00000010;
        /// <summary>
        /// The icc progress class
        /// </summary>
        const int ICC_PROGRESS_CLASS     = 0x00000020;
        /// <summary>
        /// The icc hotkey class
        /// </summary>
        const int ICC_HOTKEY_CLASS       = 0x00000040;
        /// <summary>
        /// The icc animate class
        /// </summary>
        const int ICC_ANIMATE_CLASS      = 0x00000080;
        /// <summary>
        /// The icc wi N95 classes
        /// </summary>
        const int ICC_WIN95_CLASSES      = 0x000000FF;
        /// <summary>
        /// The icc date classes
        /// </summary>
        const int ICC_DATE_CLASSES       = 0x00000100;
        /// <summary>
        /// The icc userex classes
        /// </summary>
        const int ICC_USEREX_CLASSES     = 0x00000200;
        /// <summary>
        /// The icc cool classes
        /// </summary>
        const int ICC_COOL_CLASSES       = 0x00000400;
        // IE 4.0
        /// <summary>
        /// The icc internet classes
        /// </summary>
        const int ICC_INTERNET_CLASSES   = 0x00000800;
        /// <summary>
        /// The icc pagescroller class
        /// </summary>
        const int ICC_PAGESCROLLER_CLASS = 0x00001000;
        /// <summary>
        /// The icc nativefntctl class
        /// </summary>
        const int ICC_NATIVEFNTCTL_CLASS = 0x00002000;
        // WIN XP
        /// <summary>
        /// The icc standard classes
        /// </summary>
        const int ICC_STANDARD_CLASSES   = 0x00004000;
        /// <summary>
        /// The icc link class
        /// </summary>
        const int ICC_LINK_CLASS         = 0x00008000;

        /// <summary>
        /// Types
        /// </summary>
        [Flags]
			public enum Classes : int
		{
            /// <summary>
            /// The ListView
            /// </summary>
            ListView = ICC_LISTVIEW_CLASSES,
            /// <summary>
            /// The TreeView
            /// </summary>
            TreeView = ICC_TREEVIEW_CLASSES,
            /// <summary>
            /// The header
            /// </summary>
            Header = ICC_LISTVIEW_CLASSES,
            /// <summary>
            /// The tool bar
            /// </summary>
            ToolBar = ICC_BAR_CLASSES,
            /// <summary>
            /// The status bar
            /// </summary>
            StatusBar = ICC_BAR_CLASSES,
            /// <summary>
            /// The track bar
            /// </summary>
            TrackBar = ICC_BAR_CLASSES,
            /// <summary>
            /// The tool tips
            /// </summary>
            ToolTips = ICC_BAR_CLASSES,
            /// <summary>
            /// The tab control
            /// </summary>
            TabControl = ICC_TAB_CLASSES,
            /// <summary>
            /// Up down
            /// </summary>
            UpDown = ICC_UPDOWN_CLASS,
            /// <summary>
            /// The progress
            /// </summary>
            Progress = ICC_PROGRESS_CLASS,
            /// <summary>
            /// The hot key
            /// </summary>
            HotKey = ICC_HOTKEY_CLASS,
            /// <summary>
            /// The animate
            /// </summary>
            Animate = ICC_ANIMATE_CLASS,
            /// <summary>
            /// The win95
            /// </summary>
            Win95 = ICC_WIN95_CLASSES,
            /// <summary>
            /// The date time picker
            /// </summary>
            DateTimePicker = ICC_DATE_CLASSES,
            /// <summary>
            /// The ComboBox ex
            /// </summary>
            ComboBoxEx = ICC_USEREX_CLASSES,
            /// <summary>
            /// The rebar
            /// </summary>
            Rebar = ICC_COOL_CLASSES,
            /// <summary>
            /// The internet
            /// </summary>
            Internet = ICC_INTERNET_CLASSES,
            /// <summary>
            /// The page scroller
            /// </summary>
            PageScroller = ICC_PAGESCROLLER_CLASS,
            /// <summary>
            /// The native font
            /// </summary>
            NativeFont = ICC_NATIVEFNTCTL_CLASS,
            /// <summary>
            /// The standard
            /// </summary>
            Standard = ICC_STANDARD_CLASSES,
            /// <summary>
            /// The link
            /// </summary>
            Link = ICC_LINK_CLASS
		};

        /// <summary>
        /// Types: Platform
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
			private struct INITCOMMONCONTROLSEX 
		{
            /// <summary>
            /// The cb size
            /// </summary>
            public int cbSize;
            /// <summary>
            /// The n flags
            /// </summary>
            public int nFlags;

            /// <summary>
            /// Initializes a new instance of the <see cref="INITCOMMONCONTROLSEX"/> struct.
            /// </summary>
            /// <param name="cbSize">Size of the cb.</param>
            /// <param name="nFlags">The n flags.</param>
            public INITCOMMONCONTROLSEX(int cbSize, int nFlags)
			{
				this.cbSize = cbSize;
				this.nFlags = nFlags;
			}
		}

        /// <summary>
        /// Initializes the common controls ex.
        /// </summary>
        /// <param name="icc">The icc.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("comctl32.dll")]
		private static extern bool InitCommonControlsEx(ref INITCOMMONCONTROLSEX icc);

        /// <summary>
        /// Operations
        /// </summary>
        /// <param name="fClasses">Bit flags defining classes to be initialized</param>
        /// <exception cref="SystemException">Failture initializing common controls.</exception>
        static public void Init(Classes fClasses)
		{
			INITCOMMONCONTROLSEX icc = 
				new INITCOMMONCONTROLSEX(Marshal.SizeOf(typeof(INITCOMMONCONTROLSEX)), 
										 (int)fClasses);

			bool bResult = InitCommonControlsEx(ref icc);
			Debug.Assert( bResult );
			if ( !bResult )
			{
				throw new SystemException("Failture initializing common controls.");
			}
		}

	} // InitCommonControlsHelper class


    /// <summary>
    /// NativeWindowCommon class
    /// </summary>
    internal class NativeWindowCommon
	{
        /// <summary>
        /// Constants: Window Styles
        /// </summary>
        public const int WS_OVERLAPPED       = 0x00000000;

        /// <summary>
        /// The ws popup
        /// </summary>
        public const int WS_POPUP            = unchecked((int)0x80000000);
        /// <summary>
        /// The ws child
        /// </summary>
        public const int WS_CHILD            = 0x40000000;
        /// <summary>
        /// The ws minimize
        /// </summary>
        public const int WS_MINIMIZE         = 0x20000000;
        /// <summary>
        /// The ws visible
        /// </summary>
        public const int WS_VISIBLE          = 0x10000000;
        /// <summary>
        /// The ws disabled
        /// </summary>
        public const int WS_DISABLED         = 0x08000000;
        /// <summary>
        /// The ws clipsiblings
        /// </summary>
        public const int WS_CLIPSIBLINGS     = 0x04000000;
        /// <summary>
        /// The ws clipchildren
        /// </summary>
        public const int WS_CLIPCHILDREN     = 0x02000000;
        /// <summary>
        /// The ws maximize
        /// </summary>
        public const int WS_MAXIMIZE         = 0x01000000;
        /// <summary>
        /// The ws caption
        /// </summary>
        public const int WS_CAPTION          = 0x00C00000;  // WS_BORDER|WS_DLGFRAME
                                                            /// <summary>
                                                            /// The ws border
                                                            /// </summary>
        public const int WS_BORDER           = 0x00800000;
        /// <summary>
        /// The ws dlgframe
        /// </summary>
        public const int WS_DLGFRAME         = 0x00400000;
        /// <summary>
        /// The ws vscroll
        /// </summary>
        public const int WS_VSCROLL          = 0x00200000;
        /// <summary>
        /// The ws hscroll
        /// </summary>
        public const int WS_HSCROLL          = 0x00100000;
        /// <summary>
        /// The ws sysmenu
        /// </summary>
        public const int WS_SYSMENU          = 0x00080000;
        /// <summary>
        /// The ws thickframe
        /// </summary>
        public const int WS_THICKFRAME       = 0x00040000;
        /// <summary>
        /// The ws group
        /// </summary>
        public const int WS_GROUP            = 0x00020000;
        /// <summary>
        /// The ws tabstop
        /// </summary>
        public const int WS_TABSTOP          = 0x00010000;
        /// <summary>
        /// The ws minimizebox
        /// </summary>
        public const int WS_MINIMIZEBOX      = 0x00020000;
        /// <summary>
        /// The ws maximizebox
        /// </summary>
        public const int WS_MAXIMIZEBOX      = 0x00010000;

        /// <summary>
        /// The ws tiled
        /// </summary>
        public const int WS_TILED            = WS_OVERLAPPED;
        /// <summary>
        /// The ws iconic
        /// </summary>
        public const int WS_ICONIC           = WS_MINIMIZE;
        /// <summary>
        /// The ws sizebox
        /// </summary>
        public const int WS_SIZEBOX          = WS_THICKFRAME;
        /// <summary>
        /// The ws tiledwindow
        /// </summary>
        public const int WS_TILEDWINDOW      = WS_OVERLAPPEDWINDOW;

        /// <summary>
        /// The ws overlappedwindow
        /// </summary>
        public const int WS_OVERLAPPEDWINDOW = WS_OVERLAPPED|WS_CAPTION|
											   WS_SYSMENU|WS_THICKFRAME|
											   WS_MINIMIZEBOX|WS_MAXIMIZEBOX;

        /// <summary>
        /// The ws popupwindow
        /// </summary>
        public const int WS_POPUPWINDOW      = WS_POPUP|WS_BORDER|WS_SYSMENU;
        /// <summary>
        /// The ws childwindow
        /// </summary>
        public const int WS_CHILDWINDOW      = WS_CHILD;

        /// <summary>
        /// Constants: Extended Window Styles
        /// </summary>
        public const int WS_EX_DLGMODALFRAME     = 0x00000001;
        /// <summary>
        /// The ws ex noparentnotify
        /// </summary>
        public const int WS_EX_NOPARENTNOTIFY    = 0x00000004;
        /// <summary>
        /// The ws ex topmost
        /// </summary>
        public const int WS_EX_TOPMOST           = 0x00000008;
        /// <summary>
        /// The ws ex acceptfiles
        /// </summary>
        public const int WS_EX_ACCEPTFILES       = 0x00000010;
        /// <summary>
        /// The ws ex transparent
        /// </summary>
        public const int WS_EX_TRANSPARENT       = 0x00000020;

        /// <summary>
        /// The ws ex mdichild
        /// </summary>
        public const int WS_EX_MDICHILD          = 0x00000040;
        /// <summary>
        /// The ws ex toolwindow
        /// </summary>
        public const int WS_EX_TOOLWINDOW        = 0x00000080;
        /// <summary>
        /// The ws ex windowedge
        /// </summary>
        public const int WS_EX_WINDOWEDGE        = 0x00000100;
        /// <summary>
        /// The ws ex clientedge
        /// </summary>
        public const int WS_EX_CLIENTEDGE        = 0x00000200;
        /// <summary>
        /// The ws ex contexthelp
        /// </summary>
        public const int WS_EX_CONTEXTHELP       = 0x00000400;

        /// <summary>
        /// The ws ex right
        /// </summary>
        public const int WS_EX_RIGHT             = 0x00001000;
        /// <summary>
        /// The ws ex left
        /// </summary>
        public const int WS_EX_LEFT              = 0x00000000;
        /// <summary>
        /// The ws ex rtlreading
        /// </summary>
        public const int WS_EX_RTLREADING        = 0x00002000;
        /// <summary>
        /// The ws ex ltrreading
        /// </summary>
        public const int WS_EX_LTRREADING        = 0x00000000;
        /// <summary>
        /// The ws ex leftscrollbar
        /// </summary>
        public const int WS_EX_LEFTSCROLLBAR     = 0x00004000;
        /// <summary>
        /// The ws ex rightscrollbar
        /// </summary>
        public const int WS_EX_RIGHTSCROLLBAR    = 0x00000000;

        /// <summary>
        /// The ws ex controlparent
        /// </summary>
        public const int WS_EX_CONTROLPARENT     = 0x00010000;
        /// <summary>
        /// The ws ex staticedge
        /// </summary>
        public const int WS_EX_STATICEDGE        = 0x00020000;
        /// <summary>
        /// The ws ex appwindow
        /// </summary>
        public const int WS_EX_APPWINDOW         = 0x00040000;

        /// <summary>
        /// The ws ex overlappedwindow
        /// </summary>
        public const int WS_EX_OVERLAPPEDWINDOW  = WS_EX_WINDOWEDGE|WS_EX_CLIENTEDGE;
        /// <summary>
        /// The ws ex palettewindow
        /// </summary>
        public const int WS_EX_PALETTEWINDOW     = WS_EX_WINDOWEDGE|WS_EX_TOOLWINDOW|
			WS_EX_TOPMOST;

        /// <summary>
        /// The ws ex layered
        /// </summary>
        public const int WS_EX_LAYERED           = 0x00080000;
        /// <summary>
        /// The ws ex noinheritlayout
        /// </summary>
        public const int WS_EX_NOINHERITLAYOUT   = 0x00100000;
        /// <summary>
        /// The ws ex layoutrtl
        /// </summary>
        public const int WS_EX_LAYOUTRTL         = 0x00400000;

        /// <summary>
        /// The ws ex composited
        /// </summary>
        public const int WS_EX_COMPOSITED        = 0x02000000;
        /// <summary>
        /// The ws ex noactivate
        /// </summary>
        public const int WS_EX_NOACTIVATE        = 0x08000000;

        // Common control shared messages
        /// <summary>
        /// The CCM first
        /// </summary>
        public const int CCM_FIRST               = 0x00002000;
        /// <summary>
        /// The CCM last
        /// </summary>
        public const int CCM_LAST                = CCM_FIRST + 0x200;
        /// <summary>
        /// The CCM setbkcolor
        /// </summary>
        public const int CCM_SETBKCOLOR          = CCM_FIRST + 1;
        /// <summary>
        /// The CCM setcolorscheme
        /// </summary>
        public const int CCM_SETCOLORSCHEME      = CCM_FIRST + 2;
        /// <summary>
        /// The CCM getcolorscheme
        /// </summary>
        public const int CCM_GETCOLORSCHEME      = CCM_FIRST + 3;
        /// <summary>
        /// The CCM getdroptarget
        /// </summary>
        public const int CCM_GETDROPTARGET       = CCM_FIRST + 4;
        /// <summary>
        /// The CCM setunicodeformat
        /// </summary>
        public const int CCM_SETUNICODEFORMAT    = CCM_FIRST + 5;
        /// <summary>
        /// The CCM getunicodeformat
        /// </summary>
        public const int CCM_GETUNICODEFORMAT    = CCM_FIRST + 6;


        // Common messages
        /// <summary>
        /// The wm setredraw
        /// </summary>
        public const int WM_SETREDRAW           = 0x000B;
        /// <summary>
        /// The wm cancelmode
        /// </summary>
        public const int WM_CANCELMODE          = 0x001F;

        /// <summary>
        /// The wm keydown
        /// </summary>
        public const int WM_KEYDOWN             = 0x100;
        /// <summary>
        /// The wm keyup
        /// </summary>
        public const int WM_KEYUP               = 0x101;
        /// <summary>
        /// The wm character
        /// </summary>
        public const int WM_CHAR                = 0x0102;
        /// <summary>
        /// The wm syskeydown
        /// </summary>
        public const int WM_SYSKEYDOWN          = 0x104;
        /// <summary>
        /// The wm syskeyup
        /// </summary>
        public const int WM_SYSKEYUP            = 0x105;

        /// <summary>
        /// The wm mouselast
        /// </summary>
        public const int WM_MOUSELAST           = 0x20a;
        /// <summary>
        /// The wm mousemove
        /// </summary>
        public const int WM_MOUSEMOVE           = 0x200;
        /// <summary>
        /// The wm lbuttondown
        /// </summary>
        public const int WM_LBUTTONDOWN         = 0x201;

        /// <summary>
        /// The wm menuchar
        /// </summary>
        public const int WM_MENUCHAR            = 0x120;

        /// <summary>
        /// The wm nchittest
        /// </summary>
        public const int WM_NCHITTEST           = 0x0084;

        /// <summary>
        /// The wm setcursor
        /// </summary>
        public const int WM_SETCURSOR           = 0x0020;

        /// <summary>
        /// The wm notify
        /// </summary>
        public const int WM_NOTIFY              = 0x4e;
        /// <summary>
        /// The wm command
        /// </summary>
        public const int WM_COMMAND             = 0x111;

        /// <summary>
        /// The wm user
        /// </summary>
        public const int WM_USER                = 0x0400;
        /// <summary>
        /// The ocm base
        /// </summary>
        public const int OCM__BASE              = WM_USER + 0x1c00;


        /// <summary>
        /// The hterror
        /// </summary>
        public const int HTERROR			= -2;
        /// <summary>
        /// The httransparent
        /// </summary>
        public const int HTTRANSPARENT		= -1;
        /// <summary>
        /// The htnowhere
        /// </summary>
        public const int HTNOWHERE			= 0;
        /// <summary>
        /// The htclient
        /// </summary>
        public const int HTCLIENT			= 1;
        /// <summary>
        /// The htcaption
        /// </summary>
        public const int HTCAPTION			= 2;
        /// <summary>
        /// The htsysmenu
        /// </summary>
        public const int HTSYSMENU			= 3;
        /// <summary>
        /// The htgrowbox
        /// </summary>
        public const int HTGROWBOX			= 4;
        /// <summary>
        /// The htsize
        /// </summary>
        public const int HTSIZE				= HTGROWBOX;
        /// <summary>
        /// The htmenu
        /// </summary>
        public const int HTMENU				= 5;
        /// <summary>
        /// The hthscroll
        /// </summary>
        public const int HTHSCROLL			= 6;
        /// <summary>
        /// The htvscroll
        /// </summary>
        public const int HTVSCROLL			= 7;
        /// <summary>
        /// The htminbutton
        /// </summary>
        public const int HTMINBUTTON		= 8;
        /// <summary>
        /// The htmaxbutton
        /// </summary>
        public const int HTMAXBUTTON		= 9;
        /// <summary>
        /// The htleft
        /// </summary>
        public const int HTLEFT				= 10;
        /// <summary>
        /// The htright
        /// </summary>
        public const int HTRIGHT			= 11;
        /// <summary>
        /// The httop
        /// </summary>
        public const int HTTOP				= 12;
        /// <summary>
        /// The httopleft
        /// </summary>
        public const int HTTOPLEFT			= 13;
        /// <summary>
        /// The httopright
        /// </summary>
        public const int HTTOPRIGHT			= 14;
        /// <summary>
        /// The htbottom
        /// </summary>
        public const int HTBOTTOM			= 15;
        /// <summary>
        /// The htbottomleft
        /// </summary>
        public const int HTBOTTOMLEFT		= 16;
        /// <summary>
        /// The htbottomright
        /// </summary>
        public const int HTBOTTOMRIGHT		= 17;
        /// <summary>
        /// The htborder
        /// </summary>
        public const int HTBORDER			= 18;
        /// <summary>
        /// The htreduce
        /// </summary>
        public const int HTREDUCE			= HTMINBUTTON;
        /// <summary>
        /// The htzoom
        /// </summary>
        public const int HTZOOM				= HTMAXBUTTON;
        /// <summary>
        /// The htsizefirst
        /// </summary>
        public const int HTSIZEFIRST		= HTLEFT;
        /// <summary>
        /// The htsizelast
        /// </summary>
        public const int HTSIZELAST			= HTBOTTOMRIGHT;
        /// <summary>
        /// The htobject
        /// </summary>
        public const int HTOBJECT			= 19;
        /// <summary>
        /// The htclose
        /// </summary>
        public const int HTCLOSE			= 20;
        /// <summary>
        /// The hthelp
        /// </summary>
        public const int HTHELP				= 21;

        /// <summary>
        /// Constants for SetWindowPos
        /// </summary>
        public const int SWP_NOSIZE         = 0x0001;
        /// <summary>
        /// The SWP nomove
        /// </summary>
        public const int SWP_NOMOVE         = 0x0002;
        /// <summary>
        /// The SWP nozorder
        /// </summary>
        public const int SWP_NOZORDER       = 0x0004;
        /// <summary>
        /// The SWP noredraw
        /// </summary>
        public const int SWP_NOREDRAW       = 0x0008;
        /// <summary>
        /// The SWP noactivate
        /// </summary>
        public const int SWP_NOACTIVATE     = 0x0010;
        /// <summary>
        /// The SWP framechanged
        /// </summary>
        public const int SWP_FRAMECHANGED   = 0x0020;
        /// <summary>
        /// The SWP showwindow
        /// </summary>
        public const int SWP_SHOWWINDOW     = 0x0040;
        /// <summary>
        /// The SWP hidewindow
        /// </summary>
        public const int SWP_HIDEWINDOW     = 0x0080;
        /// <summary>
        /// The SWP nocopybits
        /// </summary>
        public const int SWP_NOCOPYBITS     = 0x0100;
        /// <summary>
        /// The SWP noownerzorder
        /// </summary>
        public const int SWP_NOOWNERZORDER  = 0x0200;
        /// <summary>
        /// The SWP nosendchanging
        /// </summary>
        public const int SWP_NOSENDCHANGING = 0x0400;

        /// <summary>
        /// The SWP drawframe
        /// </summary>
        public const int SWP_DRAWFRAME      = SWP_FRAMECHANGED;
        /// <summary>
        /// The SWP noreposition
        /// </summary>
        public const int SWP_NOREPOSITION   = SWP_NOOWNERZORDER;
        /// <summary>
        /// The SWP defererase
        /// </summary>
        public const int SWP_DEFERERASE     = 0x2000;
        /// <summary>
        /// The SWP asyncwindowpos
        /// </summary>
        public const int SWP_ASYNCWINDOWPOS = 0x4000;

        /// <summary>
        /// The HWND top
        /// </summary>
        public static readonly IntPtr HWND_TOP;
        /// <summary>
        /// The HWND bottom
        /// </summary>
        public static readonly IntPtr HWND_BOTTOM;
        /// <summary>
        /// The HWND topmost
        /// </summary>
        public static readonly IntPtr HWND_TOPMOST;
        /// <summary>
        /// The HWND notopmost
        /// </summary>
        public static readonly IntPtr HWND_NOTOPMOST;

        /// <summary>
        /// Constants for GetWindowLong
        /// </summary>
        public const int GWL_WNDPROC          = -4;
        /// <summary>
        /// The GWL hinstance
        /// </summary>
        public const int GWL_HINSTANCE        = -6;
        /// <summary>
        /// The GWL hwndparent
        /// </summary>
        public const int GWL_HWNDPARENT       = -8;
        /// <summary>
        /// The GWL style
        /// </summary>
        public const int GWL_STYLE            = -16;
        /// <summary>
        /// The GWL exstyle
        /// </summary>
        public const int GWL_EXSTYLE          = -20;
        /// <summary>
        /// The GWL userdata
        /// </summary>
        public const int GWL_USERDATA         = -21;
        /// <summary>
        /// The GWL identifier
        /// </summary>
        public const int GWL_ID               = -12;

        /// <summary>
        /// Types
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
            /// <summary>
            /// The x
            /// </summary>
            public int x;
            /// <summary>
            /// The y
            /// </summary>
            public int y;
		}

        /// <summary>
        /// Struct RECT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
            /// <summary>
            /// The left
            /// </summary>
            public int left;
            /// <summary>
            /// The top
            /// </summary>
            public int top;
            /// <summary>
            /// The right
            /// </summary>
            public int right;
            /// <summary>
            /// The bottom
            /// </summary>
            public int bottom;
		}

        /// <summary>
        /// Struct WINDOWPOS
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
		public struct WINDOWPOS 
		{
            /// <summary>
            /// The HWND
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// The HWND insert after
            /// </summary>
            public IntPtr hwndInsertAfter;
            /// <summary>
            /// The x
            /// </summary>
            public int    x;
            /// <summary>
            /// The y
            /// </summary>
            public int    y;
            /// <summary>
            /// The cx
            /// </summary>
            public int    cx;
            /// <summary>
            /// The cy
            /// </summary>
            public int    cy;
            /// <summary>
            /// The flags
            /// </summary>
            public int    flags;
		}

        /// <summary>
        /// Struct NMHDR
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
		public struct NMHDR
		{
            /// <summary>
            /// The HWND from
            /// </summary>
            public IntPtr hwndFrom;
            /// <summary>
            /// The identifier from
            /// </summary>
            public int    idFrom;
            /// <summary>
            /// The code
            /// </summary>
            public int    code;
		}

        /// <summary>
        /// Static constuctor
        /// </summary>
        static NativeWindowCommon()
		{
			HWND_TOP = (IntPtr)0;
			HWND_BOTTOM = (IntPtr)1;
			HWND_TOPMOST = (IntPtr)(-1);
			HWND_NOTOPMOST = (IntPtr)(-2);
		}

        /// <summary>
        /// Helpers
        /// </summary>
        /// <returns><c>true</c> if [is system character set ANSI]; otherwise, <c>false</c>.</returns>
        protected static bool IsSysCharSetAnsi()
		{
			return Marshal.SystemDefaultCharSize == 1;
		}

        /// <summary>
        /// Operations
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        /// <summary>
        /// Sets the window position.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hWndInsertAfter">The h WND insert after.</param>
        /// <param name="X">The x.</param>
        /// <param name="Y">The y.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="uFlags">The u flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, 
			int X, int Y, int cx, int cy, int uFlags);

        /// <summary>
        /// Gets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        /// <summary>
        /// Sets the window long.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nIndex">Index of the n.</param>
        /// <param name="dwNewLong">The dw new long.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="hObject">The h object.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("gdi32.dll", CharSet=CharSet.Auto)]
		public static extern bool DeleteObject(IntPtr hObject);

	} // NativeWindowCommon


    /// <summary>
    /// NativeHeader class
    /// </summary>
    /// <seealso cref="Zeroit.Framework.Labels.Headers.NativeWindowCommon" />
    internal sealed class NativeHeader : NativeWindowCommon
	{
        /// <summary>
        /// Constants: Window class name
        /// </summary>
        public const string WC_HEADER = "SysHeader32";

        /// <summary>
        /// Constants: Control styles
        /// </summary>
        public const int HDS_HORZ       = 0x00000000;
        /// <summary>
        /// The HDS buttons
        /// </summary>
        public const int HDS_BUTTONS    = 0x00000002;
        /// <summary>
        /// The HDS hottrack
        /// </summary>
        public const int HDS_HOTTRACK   = 0x00000004;
        /// <summary>
        /// The HDS hidden
        /// </summary>
        public const int HDS_HIDDEN     = 0x00000008;
        /// <summary>
        /// The HDS dragdrop
        /// </summary>
        public const int HDS_DRAGDROP   = 0x00000040;
        /// <summary>
        /// The HDS fulldrag
        /// </summary>
        public const int HDS_FULLDRAG   = 0x00000080;
        /// <summary>
        /// The HDS filterbar
        /// </summary>
        public const int HDS_FILTERBAR  = 0x00000100;
        /// <summary>
        /// The HDS flat
        /// </summary>
        public const int HDS_FLAT       = 0x00000200;

        /// <summary>
        /// Constants: Control specific messages
        /// </summary>
        public const int HDM_FIRST                  = 0x00001200;
        /// <summary>
        /// The HDM getitemcount
        /// </summary>
        public const int HDM_GETITEMCOUNT           = HDM_FIRST + 0;
        /// <summary>
        /// The HDM insertitem
        /// </summary>
        public static readonly int HDM_INSERTITEM;
        /// <summary>
        /// The HDM deleteitem
        /// </summary>
        public const int HDM_DELETEITEM             = HDM_FIRST + 2;
        /// <summary>
        /// The HDM getitem
        /// </summary>
        public static readonly int HDM_GETITEM;
        /// <summary>
        /// The HDM setitem
        /// </summary>
        public static readonly int HDM_SETITEM;
        /// <summary>
        /// The HDM layout
        /// </summary>
        public const int HDM_LAYOUT                 = HDM_FIRST + 5;
        /// <summary>
        /// The HDM hittest
        /// </summary>
        public const int HDM_HITTEST                = HDM_FIRST + 6;
        /// <summary>
        /// The HDM getitemrect
        /// </summary>
        public const int HDM_GETITEMRECT            = HDM_FIRST + 7;
        /// <summary>
        /// The HDM setimagelist
        /// </summary>
        public const int HDM_SETIMAGELIST           = HDM_FIRST + 8;
        /// <summary>
        /// The HDM getimagelist
        /// </summary>
        public const int HDM_GETIMAGELIST           = HDM_FIRST + 9;
        /// <summary>
        /// The HDM ordertoindex
        /// </summary>
        public const int HDM_ORDERTOINDEX           = HDM_FIRST + 15;
        /// <summary>
        /// The HDM createdragimage
        /// </summary>
        public const int HDM_CREATEDRAGIMAGE        = HDM_FIRST + 16;
        /// <summary>
        /// The HDM getorderarray
        /// </summary>
        public const int HDM_GETORDERARRAY          = HDM_FIRST + 17;
        /// <summary>
        /// The HDM setorderarray
        /// </summary>
        public const int HDM_SETORDERARRAY          = HDM_FIRST + 18;
        /// <summary>
        /// The HDM sethotdivider
        /// </summary>
        public const int HDM_SETHOTDIVIDER          = HDM_FIRST + 19;
        /// <summary>
        /// The HDM setbitmapmargin
        /// </summary>
        public const int HDM_SETBITMAPMARGIN        = HDM_FIRST + 20;
        /// <summary>
        /// The HDM getbitmapmargin
        /// </summary>
        public const int HDM_GETBITMAPMARGIN        = HDM_FIRST + 21;
        /// <summary>
        /// The HDM setunicodeformat
        /// </summary>
        public const int HDM_SETUNICODEFORMAT       = CCM_SETUNICODEFORMAT;
        /// <summary>
        /// The HDM getunicodeformat
        /// </summary>
        public const int HDM_GETUNICODEFORMAT       = CCM_GETUNICODEFORMAT;
        /// <summary>
        /// The HDM setfilterchangetimeout
        /// </summary>
        public const int HDM_SETFILTERCHANGETIMEOUT = HDM_FIRST + 22;
        /// <summary>
        /// The HDM editfilter
        /// </summary>
        public const int HDM_EDITFILTER             = HDM_FIRST + 23;
        /// <summary>
        /// The HDM clearfilter
        /// </summary>
        public const int HDM_CLEARFILTER            = HDM_FIRST + 24;

        /// <summary>
        /// Constants: Control specific notifications
        /// </summary>
        public const int HDN_FIRST            = 0 - 300;
        /// <summary>
        /// The HDN last
        /// </summary>
        public const int HDN_LAST             = 0 - 399;

        /// <summary>
        /// The HDN itemchanging
        /// </summary>
        public static readonly int HDN_ITEMCHANGING;
        /// <summary>
        /// The HDN itemchanged
        /// </summary>
        public static readonly int HDN_ITEMCHANGED;
        /// <summary>
        /// The HDN itemclick
        /// </summary>
        public static readonly int HDN_ITEMCLICK;
        /// <summary>
        /// The HDN itemdblclick
        /// </summary>
        public static readonly int HDN_ITEMDBLCLICK;
        /// <summary>
        /// The HDN dividerdblclick
        /// </summary>
        public static readonly int HDN_DIVIDERDBLCLICK;
        /// <summary>
        /// The HDN begintrack
        /// </summary>
        public static readonly int HDN_BEGINTRACK;
        /// <summary>
        /// The HDN endtrack
        /// </summary>
        public static readonly int HDN_ENDTRACK;
        /// <summary>
        /// The HDN track
        /// </summary>
        public static readonly int HDN_TRACK;
        /// <summary>
        /// The HDN getdispinfo
        /// </summary>
        public static readonly int HDN_GETDISPINFO;

        /// <summary>
        /// The HDN begindrag
        /// </summary>
        public const int HDN_BEGINDRAG        = HDN_FIRST - 10;
        /// <summary>
        /// The HDN enddrag
        /// </summary>
        public const int HDN_ENDDRAG          = HDN_FIRST - 11;
        /// <summary>
        /// The HDN filterchange
        /// </summary>
        public const int HDN_FILTERCHANGE     = HDN_FIRST - 12;
        /// <summary>
        /// The HDN filterbtnclick
        /// </summary>
        public const int HDN_FILTERBTNCLICK   = HDN_FIRST - 13;

        /// <summary>
        /// Constants: HDITEM mask
        /// </summary>
        public const int HDI_WIDTH            = 0x00000001;
        /// <summary>
        /// The hdi height
        /// </summary>
        public const int HDI_HEIGHT           = HDI_WIDTH;
        /// <summary>
        /// The hdi text
        /// </summary>
        public const int HDI_TEXT             = 0x00000002;
        /// <summary>
        /// The hdi format
        /// </summary>
        public const int HDI_FORMAT           = 0x00000004;
        /// <summary>
        /// The hdi lparam
        /// </summary>
        public const int HDI_LPARAM           = 0x00000008;
        /// <summary>
        /// The hdi bitmap
        /// </summary>
        public const int HDI_BITMAP           = 0x00000010;
        /// <summary>
        /// The hdi image
        /// </summary>
        public const int HDI_IMAGE            = 0x00000020;
        /// <summary>
        /// The hdi di setitem
        /// </summary>
        public const int HDI_DI_SETITEM       = 0x00000040;
        /// <summary>
        /// The hdi order
        /// </summary>
        public const int HDI_ORDER            = 0x00000080;
        /// <summary>
        /// The hdi filter
        /// </summary>
        public const int HDI_FILTER           = 0x00000100;

        /// <summary>
        /// Constants: HDITEM fmt
        /// </summary>
        public const int HDF_LEFT             = 0x00000000;
        /// <summary>
        /// The HDF right
        /// </summary>
        public const int HDF_RIGHT            = 0x00000001;
        /// <summary>
        /// The HDF center
        /// </summary>
        public const int HDF_CENTER           = 0x00000002;
        /// <summary>
        /// The HDF justifymask
        /// </summary>
        public const int HDF_JUSTIFYMASK      = 0x00000003;
        /// <summary>
        /// The HDF rtlreading
        /// </summary>
        public const int HDF_RTLREADING       = 0x00000004;
        /// <summary>
        /// The HDF ownerdraw
        /// </summary>
        public const int HDF_OWNERDRAW        = 0x00008000;
        /// <summary>
        /// The HDF string
        /// </summary>
        public const int HDF_STRING           = 0x00004000;
        /// <summary>
        /// The HDF bitmap
        /// </summary>
        public const int HDF_BITMAP           = 0x00002000;
        /// <summary>
        /// The HDF bitmap on right
        /// </summary>
        public const int HDF_BITMAP_ON_RIGHT  = 0x00001000;
        /// <summary>
        /// The HDF image
        /// </summary>
        public const int HDF_IMAGE            = 0x00000800;
        /// <summary>
        /// The HDF sortup
        /// </summary>
        public const int HDF_SORTUP           = 0x00000400;
        /// <summary>
        /// The HDF sortdown
        /// </summary>
        public const int HDF_SORTDOWN         = 0x00000200;

        /// <summary>
        /// The HHT nowhere
        /// </summary>
        public const int HHT_NOWHERE          = 0x00000001;
        /// <summary>
        /// The HHT onheader
        /// </summary>
        public const int HHT_ONHEADER         = 0x00000002;
        /// <summary>
        /// The HHT ondivider
        /// </summary>
        public const int HHT_ONDIVIDER        = 0x00000004;
        /// <summary>
        /// The HHT ondivopen
        /// </summary>
        public const int HHT_ONDIVOPEN        = 0x00000008;
        /// <summary>
        /// The HHT onfilter
        /// </summary>
        public const int HHT_ONFILTER         = 0x00000010;
        /// <summary>
        /// The HHT onfilterbutton
        /// </summary>
        public const int HHT_ONFILTERBUTTON   = 0x00000020;
        /// <summary>
        /// The HHT above
        /// </summary>
        public const int HHT_ABOVE            = 0x00000100;
        /// <summary>
        /// The HHT below
        /// </summary>
        public const int HHT_BELOW            = 0x00000200;
        /// <summary>
        /// The HHT toright
        /// </summary>
        public const int HHT_TORIGHT          = 0x00000400;
        /// <summary>
        /// The HHT toleft
        /// </summary>
        public const int HHT_TOLEFT           = 0x00000800;

        /// <summary>
        /// Types
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
		public struct HDHITTESTINFO
		{
            /// <summary>
            /// The pt
            /// </summary>
            public POINT pt;
            /// <summary>
            /// The flags
            /// </summary>
            public int   flags;
            /// <summary>
            /// The i item
            /// </summary>
            public int   iItem;
		}

        /// <summary>
        /// Struct HDITEM
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct HDITEM
		{
            /// <summary>
            /// The mask
            /// </summary>
            public int    mask;
            /// <summary>
            /// The cxy
            /// </summary>
            public int    cxy;
            /// <summary>
            /// The LPSZ text
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
			public string lpszText;
            /// <summary>
            /// The HBM
            /// </summary>
            public IntPtr hbm;
            /// <summary>
            /// The CCH text maximum
            /// </summary>
            public int    cchTextMax;
            /// <summary>
            /// The FMT
            /// </summary>
            public int    fmt;
            /// <summary>
            /// The l parameter
            /// </summary>
            public int    lParam;
            /// <summary>
            /// The i image
            /// </summary>
            public int    iImage;
            /// <summary>
            /// The i order
            /// </summary>
            public int    iOrder;
            /// <summary>
            /// The type
            /// </summary>
            public int    type;
            /// <summary>
            /// The pv filter
            /// </summary>
            public IntPtr pvFilter;      
		}

        /// <summary>
        /// Struct HDITEM2
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct HDITEM2
		{
            /// <summary>
            /// The mask
            /// </summary>
            public int    mask;
            /// <summary>
            /// The cxy
            /// </summary>
            public int    cxy;
            /// <summary>
            /// The LPSZ text
            /// </summary>
            public IntPtr lpszText;
            /// <summary>
            /// The HBM
            /// </summary>
            public IntPtr hbm;
            /// <summary>
            /// The CCH text maximum
            /// </summary>
            public int    cchTextMax;
            /// <summary>
            /// The FMT
            /// </summary>
            public int    fmt;
            /// <summary>
            /// The l parameter
            /// </summary>
            public int    lParam;
            /// <summary>
            /// The i image
            /// </summary>
            public int    iImage;
            /// <summary>
            /// The i order
            /// </summary>
            public int    iOrder;
            /// <summary>
            /// The type
            /// </summary>
            public int    type;
            /// <summary>
            /// The pv filter
            /// </summary>
            public IntPtr pvFilter;      
		}

        /// <summary>
        /// Struct HDLAYOUT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
		public struct HDLAYOUT
		{
            /// <summary>
            /// The PRC
            /// </summary>
            public IntPtr prc;   // RECT*
                                 /// <summary>
                                 /// The pwpos
                                 /// </summary>
            public IntPtr pwpos; // WINDOWPOS*
		}

        /// <summary>
        /// Struct HDTEXTFILTER
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct HDTEXTFILTER
		{
            /// <summary>
            /// The LPSZ text
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
			public string lpszText;
            /// <summary>
            /// The CCH text maximum
            /// </summary>
            public int cchTextMax;
		}

        /// <summary>
        /// Struct NMHDDISPINFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct NMHDDISPINFO
		{
            /// <summary>
            /// The HDR
            /// </summary>
            public NMHDR hdr;
            /// <summary>
            /// The i item
            /// </summary>
            public int iItem;
            /// <summary>
            /// The mask
            /// </summary>
            public int mask;
            /// <summary>
            /// The LPSZ text
            /// </summary>
            [MarshalAs(UnmanagedType.LPTStr)]
			public string lpszText;
            /// <summary>
            /// The CCH text maximum
            /// </summary>
            public int cchTextMax;
            /// <summary>
            /// The i image
            /// </summary>
            public int iImage;
            /// <summary>
            /// The l parameter
            /// </summary>
            public int lParam;
		}

        /// <summary>
        /// Struct NMHDFILTERBTNCLICK
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct NMHDFILTERBTNCLICK 
		{
            /// <summary>
            /// The HDR
            /// </summary>
            public NMHDR hdr;
            /// <summary>
            /// The i item
            /// </summary>
            public int iItem;
            /// <summary>
            /// The rc
            /// </summary>
            public RECT rc;
		}

        /// <summary>
        /// Struct NMHEADER
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Auto)]
		public struct NMHEADER
		{
            /// <summary>
            /// The HDR
            /// </summary>
            public NMHDR hdr;
            /// <summary>
            /// The i item
            /// </summary>
            public int iItem;
            /// <summary>
            /// The i button
            /// </summary>
            public int iButton;
            /// <summary>
            /// The pitem
            /// </summary>
            public IntPtr pitem;
		}

        /// <summary>
        /// Static constructor
        /// </summary>
        static NativeHeader()
		{
			if ( IsSysCharSetAnsi() )
			{
				HDM_INSERTITEM      = HDM_FIRST + 1;
				HDM_GETITEM         = HDM_FIRST + 3;
				HDM_SETITEM         = HDM_FIRST + 4;

				HDN_ITEMCHANGING    = HDN_FIRST - 0;
				HDN_ITEMCHANGED     = HDN_FIRST - 1;
				HDN_ITEMCLICK       = HDN_FIRST - 2;
				HDN_ITEMDBLCLICK    = HDN_FIRST - 3;
				HDN_DIVIDERDBLCLICK = HDN_FIRST - 5;
				HDN_BEGINTRACK      = HDN_FIRST - 6;
				HDN_ENDTRACK        = HDN_FIRST - 7;
				HDN_TRACK           = HDN_FIRST - 8;
				HDN_GETDISPINFO     = HDN_FIRST - 9;
			}
			else
			{
				HDM_INSERTITEM      = HDM_FIRST + 10;
				HDM_GETITEM         = HDM_FIRST + 11;
				HDM_SETITEM         = HDM_FIRST + 12;

				HDN_ITEMCHANGING    = HDN_FIRST - 20;
				HDN_ITEMCHANGED     = HDN_FIRST - 21; 
				HDN_ITEMCLICK       = HDN_FIRST - 22;
				HDN_ITEMDBLCLICK    = HDN_FIRST - 23;
				HDN_DIVIDERDBLCLICK = HDN_FIRST - 25;
				HDN_BEGINTRACK      = HDN_FIRST - 26;
				HDN_ENDTRACK        = HDN_FIRST - 27;
				HDN_TRACK           = HDN_FIRST - 28;
				HDN_GETDISPINFO     = HDN_FIRST - 29;
			}
		}

        /// <summary>
        /// Helpers
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">if set to <c>true</c> [w parameter].</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int msg, bool wParam, 
											  int lParam);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="hdi">The hdi.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, 
											  ref HDITEM hdi);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="rc">The rc.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, 
											  ref RECT rc);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, 
												 IntPtr lParam);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, 
											  ref HDLAYOUT lParam);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, 
											  ref HDHITTESTINFO lParam);

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="cItems">The c items.</param>
        /// <param name="aOrders">a orders.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int msg, int cItems, 
											  int[] aOrders);

        /// <summary>
        /// Operations
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <returns>System.Int32.</returns>
        public static int GetItemCount(IntPtr hwnd)
		{
			Debug.Assert( hwnd != IntPtr.Zero );

			return SendMessage(hwnd, HDM_GETITEMCOUNT, 0, 0);
		}

        /// <summary>
        /// Inserts the item.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <param name="hdi">The hdi.</param>
        /// <returns>System.Int32.</returns>
        public static int InsertItem(IntPtr hWnd, int index, ref HDITEM hdi)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_INSERTITEM, index, ref hdi);
		}

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool DeleteItem(IntPtr hWnd, int index)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_DELETEITEM, index, 0) != 0;
		}

        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <param name="hdi">The hdi.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetItem(IntPtr hWnd, int index, ref HDITEM hdi)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_GETITEM, index, ref hdi) != 0;
		}

        /// <summary>
        /// Sets the item.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <param name="hdi">The hdi.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SetItem(IntPtr hWnd, int index, ref HDITEM hdi)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_SETITEM, index, ref hdi) != 0;
		}

        /// <summary>
        /// Gets the item rect.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <param name="rect">The rect.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetItemRect(IntPtr hWnd, int index, out RECT rect)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			rect = new RECT();

			return SendMessage(hWnd, HDM_GETITEMRECT, index, ref rect) != 0;
		}

        /// <summary>
        /// Gets the image list.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr GetImageList(IntPtr hWnd)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_GETIMAGELIST, 0, IntPtr.Zero);
		}

        /// <summary>
        /// Sets the image list.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="himl">The himl.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr SetImageList(IntPtr hWnd, IntPtr himl)
		{ 
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_SETIMAGELIST, 0, himl);
		}

        /// <summary>
        /// Creates the drag image.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <returns>IntPtr.</returns>
        public static IntPtr CreateDragImage(IntPtr hWnd, int index)
		{ 
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_CREATEDRAGIMAGE, index, IntPtr.Zero);
		}

        /// <summary>
        /// Layouts the specified h WND.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="layout">The layout.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool Layout(IntPtr hWnd, ref HDLAYOUT layout)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_LAYOUT, 0, ref layout) != 0; 
		}

        /// <summary>
        /// Hits the test.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hdhti">The hdhti.</param>
        /// <returns>System.Int32.</returns>
        public static int HitTest(IntPtr hWnd, ref HDHITTESTINFO hdhti)
		{
			Debug.Assert( hWnd != IntPtr.Zero );
    
			return SendMessage(hWnd, HDM_HITTEST, 0, ref hdhti);
		}

        /// <summary>
        /// Gets the bitmap margin.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>System.Int32.</returns>
        public static int GetBitmapMargin(IntPtr hWnd)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_GETBITMAPMARGIN, 0, 0);
		}

        /// <summary>
        /// Sets the bitmap margin.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="iWidth">Width of the i.</param>
        /// <returns>System.Int32.</returns>
        public static int SetBitmapMargin(IntPtr hWnd, int iWidth)
		{
			Debug.Assert( hWnd != IntPtr.Zero && iWidth >= 0 );

			return SendMessage(hWnd, HDM_SETBITMAPMARGIN, iWidth, 0);
		}

        /// <summary>
        /// Sets the hot divider.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="flag">if set to <c>true</c> [flag].</param>
        /// <param name="dwInputValue">The dw input value.</param>
        /// <returns>System.Int32.</returns>
        public static int SetHotDivider(IntPtr hWnd, bool flag, int dwInputValue)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			return SendMessage(hWnd, HDM_SETHOTDIVIDER, flag, dwInputValue);
		}

        /// <summary>
        /// Orders to index.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="iOrder">The i order.</param>
        /// <returns>System.Int32.</returns>
        public static int OrderToIndex(IntPtr hWnd, int iOrder)
		{
			Debug.Assert( hWnd != IntPtr.Zero );
       
			return SendMessage(hWnd, HDM_ORDERTOINDEX, iOrder, 0);      
		}

        /// <summary>
        /// Gets the order array.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="aOrders">a orders.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetOrderArray(IntPtr hWnd, out int[] aOrders)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			int cItems = GetItemCount(hWnd);
			aOrders = new int[cItems];
      
			return SendMessage(hWnd, HDM_GETORDERARRAY, cItems, aOrders) != 0;      
		}

        /// <summary>
        /// Sets the order array.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="aOrders">a orders.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SetOrderArray(IntPtr hWnd, int[] aOrders)
		{
			Debug.Assert( hWnd != IntPtr.Zero );
      
			return SendMessage(hWnd, HDM_GETORDERARRAY, aOrders.Length, aOrders) != 0;      
		}

        /// <summary>
        /// Gets the unicode format.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool GetUnicodeFormat(IntPtr hWnd)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			// ???

			return false;
		}

        /// <summary>
        /// Sets the unicode format.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="fUnicode">if set to <c>true</c> [f unicode].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SetUnicodeFormat(IntPtr hWnd, bool fUnicode)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			// ???

			return false;
		}

        /// <summary>
        /// Clears all filters.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <returns>System.Int32.</returns>
        public static int ClearAllFilters(IntPtr hWnd)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			// ???

			return 0;
		}

        /// <summary>
        /// Clears the filter.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="index">The index.</param>
        /// <returns>System.Int32.</returns>
        public static int ClearFilter(IntPtr hWnd, int index)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			// ???

			return 0;
		}

        /// <summary>
        /// Edits the filter.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="i">The i.</param>
        /// <param name="fDiscardChanges">if set to <c>true</c> [f discard changes].</param>
        /// <returns>System.Int32.</returns>
        public static int EditFilter(IntPtr hWnd, int i, bool fDiscardChanges)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			// ???

			return 0;
		}

        /// <summary>
        /// Sets the filter change timeout.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="i">The i.</param>
        /// <returns>System.Int32.</returns>
        public static int SetFilterChangeTimeout(IntPtr hWnd, int i)
		{
			Debug.Assert( hWnd != IntPtr.Zero );

			// ???

			return 0;
		}

	} // NativeHeader class
}
