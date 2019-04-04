// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 11-28-2018
// ***********************************************************************
// <copyright file="MultiFormatLabel.Designer.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Zeroit.Framework.Labels
{
    /// <summary>
    /// Class ZeroitMultiFormatLabel.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    partial class ZeroitMultiFormatLabel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ZeroitMultiFormatLabel
            // 
            this.AutoScroll = true;
            this.DoubleBuffered = true;
            this.Name = "ZeroitMultiFormatLabel";
            this.Size = new System.Drawing.Size(200, 50);
            this.Load += new System.EventHandler(this.MultiFormatLabel_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
