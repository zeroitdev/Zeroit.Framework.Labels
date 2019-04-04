// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="TrayBalloonFrm.cs" company="Zeroit Dev Technologies">
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
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Zeroit.Framework.Labels.Notification
{
    /// <summary>
    /// A class collection for rendering a tray balloon.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    internal partial class TrayBalloonFrm : Form
    {
        /// <summary>
        /// The title
        /// </summary>
        public string Title;
        /// <summary>
        /// The message
        /// </summary>
        public string Message;

        /// <summary>
        /// The starting offset index
        /// </summary>
        public volatile int StartingOffsetIndex;

        /// <summary>
        /// The opacity step
        /// </summary>
        private float OpacityStep;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrayBalloonFrm" /> class.
        /// </summary>
        public TrayBalloonFrm()
        {
            InitializeComponent();

            Title = null;
            StartingOffsetIndex = 0;

            SetStyle(ControlStyles.Selectable, false);

            MessageLabel.MouseDown += new MouseEventHandler(TrayBalloonFrm_MouseDown);
        }

        /// <summary>
        /// The sound location
        /// </summary>
        public string SoundLocation;
        /// <summary>
        /// The background location
        /// </summary>
        public string BackgroundLocation;

        /// <summary>
        /// The light weight
        /// </summary>
        private bool _LightWeight;
        /// <summary>
        /// Gets or sets a value indicating whether to enable/disable light weight.
        /// </summary>
        /// <value><c>true</c> if light weight; otherwise, <c>false</c>.</value>
        public bool LightWeight
        {
            get
            {
                return _LightWeight;
            }
            set
            {
                _LightWeight = value;
            }
        }

        /// <summary>
        /// The background image
        /// </summary>
        private Image _BackgroundImage;

        /// <summary>
        /// Gets or sets the background image displayed in the control.
        /// </summary>
        /// <value>The background image.</value>
        public override Image BackgroundImage
        {
            get
            {
                if (LightWeight)
                    return null;

                if (_BackgroundImage != null)
                    return _BackgroundImage;

                if (!string.IsNullOrEmpty(BackgroundLocation))
                {
                    try
                    {
                        _BackgroundImage = Bitmap.FromFile(BackgroundLocation);
                    }
                    catch (System.IO.IOException)
                    { }
                    catch (ArgumentException)
                    { }
                    catch (UnauthorizedAccessException)
                    { }
                    catch (OutOfMemoryException)
                    { }
                }

                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Closed" /> event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            CloseTimer.Stop();
            MoveTimer.Stop();
        }

        /// <summary>
        /// The opacity value
        /// </summary>
        private double _OpacityValue;
        /// <summary>
        /// Gets or sets the opacity value.
        /// </summary>
        /// <value>The opacity value.</value>
        public double OpacityValue
        {
            get
            {
                if (UseOpacity)
                {
                    return Opacity;
                }
                else
                {
                    if (_OpacityValue < 0)
                        return 0;
                    else if (_OpacityValue > 1)
                        return 1;

                    return _OpacityValue;
                }
            }
            set
            {
                _OpacityValue = value;
                if (UseOpacity)
                    Opacity = value;
            }
        }

        /// <summary>
        /// The use opacity
        /// </summary>
        public bool UseOpacity = true;

        /// <summary>
        /// Handles the Tick event of the MoveTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void MoveTimer_Tick(object sender, EventArgs e)
        {
            OpacityValue += OpacityStep;
            if (Location.Y > Screen.PrimaryScreen.WorkingArea.Height - Height)
            {
                Location = new Point(Location.X, Location.Y - 2);
            }
            else
            {
                if (OpacityValue == 1.0)
                {
                    MoveTimer.Stop();
                    CloseTimer.Start();
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the CloseTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            if (Bounds.Contains(Cursor.Position))
                return;

            CloseTimer.Interval = MoveTimer.Interval / 2;

            if (OpacityValue == 0)
                Close();
            else
                OpacityValue -= OpacityStep;

            MessageLabel.Refresh();
        }

        /// <summary>
        /// a
        /// </summary>
        private static readonly System.Text.RegularExpressions.Regex A = new System.Text.RegularExpressions.Regex(
            "\\<a\\W+href=\"(?<href>[^\"]*)\"\\W*\\>(?<text>[^\\<]*)\\</a\\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
        /// <summary>
        /// Setups the text.
        /// </summary>
        private void SetupText()
        {
            TitleLabel.Text = Title;
            string msg = Message ?? string.Empty;

            var matches = A.Matches(msg);
            if (matches == null || matches.Count == 0)
            {
                MessageLabel.Text = msg;
                MessageLabel.LinkArea = new LinkArea(msg.Length, 0);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                int last_index = 0;
                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    var href = match.Groups["href"].Value;
                    var text = match.Groups["text"].Value;

                    sb.Append(msg, last_index, match.Index - last_index);
                    MessageLabel.Links.Add(new LinkLabel.Link(sb.Length, text.Length) { LinkData = href });
                    sb.Append(text);
                    last_index = match.Index + match.Length;
                }
                if (last_index < msg.Length)
                    sb.Append(msg.Substring(last_index));

                MessageLabel.Text = sb.ToString();
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!UseOpacity)
                Opacity = 1;

            SetupText();
       
            Width = 200;
            Height = 180;
            Location = new Point(
                Screen.PrimaryScreen.Bounds.Width - Width,
                Screen.PrimaryScreen.Bounds.Height - Height * (1 + StartingOffsetIndex) + ((StartingOffsetIndex == 0) ? 0 : (Screen.PrimaryScreen.WorkingArea.Height - Screen.PrimaryScreen.Bounds.Height)));

            OpacityStep = (float)(((float)SystemInformation.WorkingArea.Height / (float)SystemInformation.VirtualScreen.Height) / 10.0);

            MoveTimer.Start();

            Play(SoundLocation);
        }

        /// <summary>
        /// Plays the specified sound location.
        /// </summary>
        /// <param name="SoundLocation">The sound location.</param>
        private static void Play(string SoundLocation)
        {
            if (string.IsNullOrEmpty(SoundLocation))
                return;

            try
            {
                System.Media.SoundPlayer Player = new System.Media.SoundPlayer();
                Player.SoundLocation = SoundLocation;
                Player.Play();
            }
            catch (System.IO.IOException)
            { }
            catch (UnauthorizedAccessException)
            { }
            catch (InvalidOperationException)
            { }
            catch (System.ServiceProcess.TimeoutException)
            { }
        }

        /// <summary>
        /// Handles the MouseDown event of the TrayBalloonFrm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void TrayBalloonFrm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                Close();
        }

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (LightWeight)
            {
                LinearGradientBrush lgb = new LinearGradientBrush(
                    ClientRectangle, SystemColors.MenuHighlight, Color.LightBlue, 90);
                e.Graphics.FillRectangle(lgb, ClientRectangle);
                e.Graphics.DrawRectangle(new Pen(Color.White, 3), ClientRectangle);
                e.Graphics.DrawRectangle(new Pen(SystemColors.MenuHighlight, 1), ClientRectangle);
            }
        }

        /// <summary>
        /// The wm nccalcsize
        /// </summary>
        public const int WM_NCCALCSIZE = 0x83;
        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCCALCSIZE)
            { return; }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Handles the LinkClicked event of the MessageLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void MessageLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link == null)
                return;

            var Link = e.Link.LinkData as string;

            if (string.IsNullOrEmpty(Link))
                return;

            try
            {
                var psi = new System.Diagnostics.ProcessStartInfo();
                psi.UseShellExecute = true;
                psi.FileName = Link;
                System.Diagnostics.Process.Start(Link);
            }
            catch (System.IO.IOException)
            { }
            catch (InvalidOperationException)
            { }
            catch (ArgumentException)
            { }
            catch (System.ComponentModel.Win32Exception)
            { }
        }
    }
}