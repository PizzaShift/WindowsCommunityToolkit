﻿#if WINDOWS_UWP
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Foundation.Collections;

namespace Microsoft.ApplicationModel.Activation
{
    /// <summary>
    /// Provides information about an event that occurs when the app is activated because a user tapped on the body of a toast notification or performed an action inside a toast notification.
    /// </summary>
    public class NotificationActivatedEventArgs : IActivatedEventArgs
    {
        internal NotificationActivatedEventArgs()
        {
        }

        /// <summary>
        /// Gets the arguments from the toast XML payload related to the action that was invoked on the toast.
        /// </summary>
        public string Argument { get; internal set; }

        /// <summary>
        /// Gets a set of values that you can use to obtain the user input from an interactive toast notification.
        /// </summary>
        public ValueSet UserInput { get; internal set; }
    }
}
#endif