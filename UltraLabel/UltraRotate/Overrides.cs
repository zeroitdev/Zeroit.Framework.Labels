// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 12-23-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-23-2018
// ***********************************************************************
// <copyright file="Overrides.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
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

        

        #region Overrides


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            if(Slide)
            {
                this.Width = TextRenderer.MeasureText(Text, Font).Width - SlidingLimit;
                this.Height = TextRenderer.MeasureText(Text, Font).Height;
                timer.Enabled = true;
            }
            pathSize = (ClientRectangle.Width + ClientRectangle.Height) / 15;
            base.OnResize(e);
        }

        /// <summary>
        /// Slides the on Text changed.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SlideOnTextChanged(EventArgs e)
        {
            if(Slide)
            {
                this.Width = TextRenderer.MeasureText(Text, Font).Width - SlidingLimit;
                this.Height = TextRenderer.MeasureText(Text, Font).Height;
            }
            
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            if(Slide)
            {
                this.Width = TextRenderer.MeasureText(Text, Font).Width - SlidingLimit;
                this.Height = TextRenderer.MeasureText(Text, Font).Height;
            }
            
        }

        /// <summary>
        /// Slides the dispose.
        /// </summary>
        private void SlideDispose()
        {
            timer.Stop();
            
        }





        #region Event Creation

        /////Implement this in the Property you want to trigger the event///////////////////////////
        // 
        //  For Example this will be triggered by the Value Property
        //
        //  public int Value
        //   { 
        //      get { return _value;}
        //      set
        //         {
        //          
        //              _value = value;
        //
        //              this.OnValueChanged(EventArgs.Empty);
        //              Invalidate();
        //          }
        //    }
        //
        ////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// The sliding limit changed
        /// </summary>
        private EventHandler slidingLimitChanged;

        /// <summary>
        /// Triggered when the Value changes
        /// </summary>

        public event EventHandler SlidingLimitChanged
        {
            add
            {
                this.slidingLimitChanged = this.slidingLimitChanged + value;
            }
            remove
            {
                this.slidingLimitChanged = this.slidingLimitChanged - value;
            }
        }

        /// <summary>
        /// Handles the <see cref="E:SlidingLimitChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnSlidingLimitChanged(EventArgs e)
        {
            if (this.slidingLimitChanged == null)
                return;

            this.Width = TextRenderer.MeasureText(Text, Font).Width - SlidingLimit;
            this.Height = TextRenderer.MeasureText(Text, Font).Height;

            this.slidingLimitChanged((object)this, e);
        }

        #endregion




        #endregion
        

    }
}
