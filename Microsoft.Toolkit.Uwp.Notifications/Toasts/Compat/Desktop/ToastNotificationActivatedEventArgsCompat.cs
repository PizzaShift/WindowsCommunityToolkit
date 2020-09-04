// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WIN32

using Windows.Foundation.Collections;

namespace Microsoft.UI.Notifications
{
    /// <summary>
    /// Provides information about an event that occurs when the app is activated because a user tapped on the body of a toast notification or performed an action inside a toast notification.
    /// </summary>
    internal class ToastNotificationActivatedEventArgsCompat
    {
        internal ToastNotificationActivatedEventArgsCompat()
        {
        }

        public string Argument { get; internal set; }

        public ValueSet UserInput { get; internal set; }
    }
}

#endif