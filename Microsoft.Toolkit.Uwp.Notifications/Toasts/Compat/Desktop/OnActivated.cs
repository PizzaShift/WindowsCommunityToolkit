// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WIN32

namespace Microsoft.UI.Notifications
{
    /// <summary>
    /// Event triggered when a notification is clicked.
    /// </summary>
    /// <param name="e">Arguments that specify what action was taken and the user inputs.</param>
    internal delegate void OnActivated(ToastNotificationActivatedEventArgsCompat e);
}

#endif