#if WINDOWS_UWP
using System;

namespace Microsoft.UI.Notifications
{
    /// <summary>
    /// Represents a push channel.
    /// </summary>
    public sealed class PushChannel
    {
        /// <summary>
        /// Gets the channel URI
        /// </summary>
        public Uri Uri { get; internal set; }
    }
}
#endif