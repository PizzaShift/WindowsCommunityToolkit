#if WINDOWS_UWP
using Windows.Foundation;

namespace Microsoft.UI.Notifications
{
    /// <summary>
    /// Allows creating and managing push channels.
    /// </summary>
    public static class PushManager
    {
        /// <summary>
        /// Creates a push channel
        /// </summary>
        /// <param name="registrationId">Your registration ID</param>
        /// <returns>A push channel</returns>
        public static IAsyncOperation<PushChannel> CreateChannelAsync(string registrationId)
        {
            return null;
        }
    }
}
#endif