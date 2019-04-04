// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 07-14-2017
// ***********************************************************************
// <copyright file="TrayBalloon.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Runtime.InteropServices;

namespace Zeroit.Framework.Labels.Notification
{
    /// <summary>
    /// Class TrayBalloon.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class TrayBalloon : IDisposable
	{
        /// <summary>
        /// The FRM
        /// </summary>
        private readonly TrayBalloonFrm Frm;

        /// <summary>
        /// Delegate RunDialogHandler
        /// </summary>
        private delegate void RunDialogHandler();

        /// <summary>
        /// The currently visible
        /// </summary>
        private readonly static System.Collections.Queue CurrentlyVisible;

        /// <summary>
        /// Initializes static members of the <see cref="TrayBalloon"/> class.
        /// </summary>
        static TrayBalloon()
        {
            CurrentlyVisible = 
                System.Collections.Queue.Synchronized(new System.Collections.Queue());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrayBalloon"/> class.
        /// </summary>
        public TrayBalloon()
		{
			Frm = new TrayBalloonFrm();
		}

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
		{
			get
			{
                return Frm.Message;
			}

			set
			{
                Frm.Message = value;
			}
		}

        /// <summary>
        /// Gets or sets a value indicating whether [use opacity].
        /// </summary>
        /// <value><c>true</c> if [use opacity]; otherwise, <c>false</c>.</value>
        public bool UseOpacity
        {
            get
            {
                return Frm.UseOpacity;
            }
            set
            {
                Frm.UseOpacity = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [top most].
        /// </summary>
        /// <value><c>true</c> if [top most]; otherwise, <c>false</c>.</value>
        public bool TopMost
        {
            get
            {
                return Frm.TopMost;
            }
            set
            {
                Frm.TopMost = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [light weight].
        /// </summary>
        /// <value><c>true</c> if [light weight]; otherwise, <c>false</c>.</value>
        public bool LightWeight
        {
            get
            {
                return Frm.LightWeight;
            }
            set
            {
                Frm.LightWeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
		{
			get
			{
                return Frm.Title;
			}

			set
			{
                Frm.Title = value;
			}
		}

        /// <summary>
        /// Gets or sets the sound location.
        /// </summary>
        /// <value>The sound location.</value>
        public string SoundLocation
        {
            get
            {
                return Frm.SoundLocation;
            }
            set
            {
                Frm.SoundLocation = value;
            }
        }

        /// <summary>
        /// Gets or sets the background location.
        /// </summary>
        /// <value>The background location.</value>
        public string BackgroundLocation
        {
            get
            {
                return Frm.BackgroundLocation;
            }
            set
            {
                Frm.BackgroundLocation = value;
            }
        }

        /// <summary>
        /// Unregisters this instance.
        /// </summary>
        private static void Unregister()
		{
            lock (CurrentlyVisible)
            {
                CurrentlyVisible.Dequeue();
            }
		}

        /// <summary>
        /// Gets the free count.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int GetFreeCount()
        {
            int Full = System.Windows.Forms.SystemInformation.WorkingArea.Height;
            return Full / Frm.Height;
        }

        /// <summary>
        /// Registers the index of the and starting offset.
        /// </summary>
        /// <returns>System.Int32.</returns>
        private int RegisterAndStartingOffsetIndex()
		{
            lock (CurrentlyVisible)
			{
				CurrentlyVisible.Enqueue(Frm);

				if (CurrentlyVisible.Count <= 1)
					return 0;

                bool[] Poss = new bool[GetFreeCount()];
                for (int Idx = 0; Idx < Poss.Length; Idx++)
					Poss[Idx] = true;

				foreach (TrayBalloonFrm XFrm in CurrentlyVisible.ToArray())
				{
					if (!(XFrm == Frm))
                        if (XFrm.StartingOffsetIndex < Poss.Length)
						    Poss[XFrm.StartingOffsetIndex]  = false;
				}

                for (int Idx = 0; Idx < Poss.Length; Idx++)
					if (Poss[Idx] == true)
						return Idx;

				return Poss.Length - 1;
			}
		}

        /// <summary>
        /// Sets the window position.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="hWndInsertAfter">The h WND insert after.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="cx">The cx.</param>
        /// <param name="cy">The cy.</param>
        /// <param name="flags">The flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int flags);

        /// <summary>
        /// Runs the dialog.
        /// </summary>
        private void RunDialog()
		{
			Frm.StartingOffsetIndex = RegisterAndStartingOffsetIndex();
            
            SetWindowPos(Frm.Handle, (IntPtr)(-1), 0, 0, 0, 0, 0x50);

            Frm.ShowDialog();
			Unregister();
		}

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
		{
			RunDialogHandler RDH = new RunDialogHandler(RunDialog);
			RDH.BeginInvoke(null, null);
		}

        /// <summary>
        /// Runs the specified message.
        /// </summary>
        /// <param name="Message">The message.</param>
        public void Run(string Message)
		{
			this.Message = Message;
			Run();
		}

        /// <summary>
        /// Runs the specified title.
        /// </summary>
        /// <param name="Title">The title.</param>
        /// <param name="Message">The message.</param>
        public void Run(string Title, string Message)
		{
            this.Title = Title;
			this.Message = Message;
			Run();
		}


        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Frm.Dispose();
        }

        #endregion
    }
}
