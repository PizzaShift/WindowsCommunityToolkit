#if WINDOWS_UWP
using System.Collections.Generic;
using Microsoft.ApplicationModel.Activation;
using Microsoft.UI.Notifications;

namespace Microsoft.ApplicationModel
{
    /// <summary>
    /// Class for managing app lifecycle events
    /// </summary>
    public static class AppLifecycle
    {
#if WIN32

        private static bool _registeredOnActivated;
        private static List<OnActivated> _onActivated = new List<OnActivated>();

        /// <summary>
        /// Event that is triggered when a notification or notification button is clicked. Subscribe to this event in your app's initial startup code.
        /// </summary>
        public static event OnActivated OnActivated
        {
            add
            {
                lock (_onActivated)
                {
                    if (!_registeredOnActivated)
                    {
                        NotificationManager.OnActivated += NotificationManager_OnActivated;
                        _registeredOnActivated = true;
                    }

                    _onActivated.Add(value);
                }
            }

            remove
            {
                lock (_onActivated)
                {
                    _onActivated.Remove(value);
                }
            }
        }

        private static void NotificationManager_OnActivated(ToastNotificationActivatedEventArgsCompat e)
        {
            var args = new NotificationActivatedEventArgs()
            {
                Argument = e.Argument,
                UserInput = e.UserInput
            };

            OnActivated[] listeners;
            lock (_onActivated)
            {
                listeners = _onActivated.ToArray();
            }

            foreach (var listener in listeners)
            {
                listener(args);
            }
        }

        /// <summary>
        /// Gets whether the current process was activated with a modern activation. If so, the OnActivated event will be triggered soon after process launch.
        /// </summary>
        /// <returns>True if the current process was activated due to a modern activation, otherwise false.</returns>
        public static bool WasCurrentProcessActivated()
        {
            return NotificationManager.WasCurrentProcessToastActivated();
        }

        /// <summary>
        /// If you're not using MSIX, call this when your app is being uninstalled to properly clean up all notifications and notification-related resources. Note that this must be called from your app's main EXE (the one that you used notifications for) and not a separate uninstall EXE. If called from a MSIX app, this method no-ops.
        /// </summary>
        public static void Uninstall()
        {
            NotificationManager.Uninstall();
        }
#endif
    }
}
#endif