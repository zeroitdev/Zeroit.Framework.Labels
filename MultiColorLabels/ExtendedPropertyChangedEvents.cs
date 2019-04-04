// ***********************************************************************
// Assembly         : Zeroit.Framework.Labels
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ExtendedPropertyChangedEvents.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;



namespace Zeroit.Framework.Labels
{

    #region ExtPropertyChagedEvents
    /// <summary>
    /// Delegate ExtPropertyChangedEventHandler
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="ExtPropertyChangedEventArgs"/> instance containing the event data.</param>
    public delegate void ExtPropertyChangedEventHandler(object sender, ExtPropertyChangedEventArgs e);

    /// <summary>
    /// Interface INotifyExtPropertyChanged
    /// </summary>
    public interface INotifyExtPropertyChanged
    {
        /// <summary>
        /// Occurs when [ext property changed].
        /// </summary>
        event ExtPropertyChangedEventHandler ExtPropertyChanged;
    }

    /// <summary>
    /// Class ExtPropertyChangedEventArgs.
    /// </summary>
    /// <seealso cref="System.ComponentModel.PropertyChangedEventArgs" />
    public class ExtPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; private set; }
        /// <summary>
        /// Gets the old value.
        /// </summary>
        /// <value>The old value.</value>
        public object OldValue { get; private set; }
        /// <summary>
        /// Gets the new value.
        /// </summary>
        /// <value>The new value.</value>
        public object NewValue { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public ExtPropertyChangedEventArgs(string propertyName)
            : base(propertyName) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        public ExtPropertyChangedEventArgs(string propertyName, object value)
            : base(propertyName)
        {
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtPropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public ExtPropertyChangedEventArgs(string propertyName, object oldValue, object newValue)
            : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
    #endregion

}
