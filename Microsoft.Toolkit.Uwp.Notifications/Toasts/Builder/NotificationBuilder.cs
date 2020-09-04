// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
#if WINDOWS_UWP
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
#endif

namespace Microsoft.UI.Notifications
{
#if !WINRT
    /// <summary>
    /// Specifies the priority of a notification.
    /// </summary>
    public enum NotificationPriority
    {
        /// <summary>
        /// The notification should have default behavior in terms of delivery and display priority during connected standby mode.
        /// </summary>
        Default = 0,

        /// <summary>
        /// The notification should be treated as high priority. For desktop PCs, this means during connected standby mode the incoming notification can turn on the screen for Surface-like devices if it doesn’t have a closed lid detected.
        /// </summary>
        High = 1
    }

    /// <summary>
    /// Builder class used to create <see cref="ToastContent"/>
    /// </summary>
    public partial class NotificationBuilder
    {
        /// <summary>
        /// Gets internal instance of <see cref="ToastContent"/>.
        /// </summary>
        private ToastContent _content;

        /// <summary>
        /// Gets internal instance of <see cref="ToastContent"/>.
        /// </summary>
        public ToastContent Content => _content;

#if WINDOWS_UWP
        private string _tag;
        private string _group;
        private bool _suppressPopup = false;
        private NotificationPriority _priority;
        private NotificationData _data;
        private DateTime? _expirationTime;
        private ToastNotification _toastNotification;
        private XmlDocument _doc;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationBuilder"/> class.
        /// </summary>
        public NotificationBuilder()
        {
            _content = new ToastContent();
        }

#if WINDOWS_UWP
        /// <summary>
        /// Creates a notification builder from a ToastNotification. Note that you must call Show, any additional methods will not work.
        /// </summary>
        /// <param name="notif">The notif</param>
        /// <returns>Builder</returns>
        public static NotificationBuilder FromToastNotification(ToastNotification notif)
        {
            var answer = new NotificationBuilder();
            answer._toastNotification = notif;
            return answer;
        }

        /// <summary>
        /// Createsa  builder from XML.
        /// </summary>
        /// <param name="doc">XML</param>
        /// <returns>Builder</returns>
        public static NotificationBuilder FromXmlDocument(XmlDocument doc)
        {
            var answer = new NotificationBuilder();
            answer._doc = doc;
            return answer;
        }

        /// <summary>
        /// Gets a toast notification.
        /// </summary>
        /// <returns>Toast.</returns>
        public ToastNotification GetToastNotification()
        {
            var notif = new ToastNotification(GetXmlDocument());

            if (_tag != null)
            {
                notif.Tag = _tag;
            }

            if (_group != null)
            {
                notif.Group = _group;
            }

            if (_suppressPopup)
            {
                notif.SuppressPopup = true;
            }

            if (_priority != NotificationPriority.Default)
            {
                notif.Priority = (ToastNotificationPriority)(int)_priority;
            }

            if (_data != null)
            {
                notif.Data = _data;
            }

            if (_expirationTime != null)
            {
                notif.ExpirationTime = _expirationTime;
            }

            return notif;
        }

        /// <summary>
        /// Sets the tag on the notification.
        /// </summary>
        /// <param name="tag">A string</param>
        /// <returns>The current instance of the builder.</returns>
        public NotificationBuilder SetTag(string tag)
        {
            _tag = tag;
            return this;
        }

        /// <summary>
        /// Sets the group on the notification.
        /// </summary>
        /// <param name="group">String</param>
        /// <returns>Builder</returns>
        public NotificationBuilder SetGroup(string group)
        {
            _group = group;
            return this;
        }

        /// <summary>
        /// Sets whether the popup should be suppressed.
        /// </summary>
        /// <param name="suppressPopup">Boolean</param>
        /// <returns>Builder</returns>
        public NotificationBuilder SetSuppressPopup(bool suppressPopup)
        {
            _suppressPopup = suppressPopup;
            return this;
        }

        /// <summary>
        /// Sets the priority.
        /// </summary>
        /// <param name="priority">Priority</param>
        /// <returns>Builder</returns>
        public NotificationBuilder SetPriority(NotificationPriority priority)
        {
            _priority = priority;
            return this;
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns>Builder.</returns>
        public NotificationBuilder SetData(NotificationData data)
        {
            _data = data;
            return this;
        }

        /// <summary>
        /// Sets the expiration time.
        /// </summary>
        /// <param name="expirationTime">Time</param>
        /// <returns>Builder</returns>
        public NotificationBuilder SetExpirationTime(DateTime expirationTime)
        {
            _expirationTime = expirationTime;
            return this;
        }
#endif

        /// <summary>
        /// Add custom time stamp on the toast to override the time display on the toast.
        /// </summary>
        /// <param name="dateTime">Custom Time to be displayed on the toast</param>
        /// <returns>The current instance of <see cref="NotificationBuilder"/></returns>
        public NotificationBuilder SetCustomTimeStamp(DateTime dateTime)
        {
            _content.DisplayTimestamp = dateTime;

            return this;
        }

        /// <summary>
        /// Add a header to a toast.
        /// </summary>
        /// <param name="id">A developer-created identifier that uniquely identifies this header. If two notifications have the same header id, they will be displayed underneath the same header in Action Center.</param>
        /// <param name="title">A title for the header.</param>
        /// <param name="arguments">A developer-defined string of arguments that is returned to the app when the user clicks this header.</param>
        /// <returns>The current instance of <see cref="NotificationBuilder"/></returns>
        /// <remarks>More info about toast header: https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/toast-headers </remarks>
        public NotificationBuilder SetHeader(string id, string title, string arguments)
        {
            _content.Header = new ToastHeader(id, title, arguments);

            return this;
        }

        /// <summary>
        /// Add info that can be used by the application when the app was activated/launched by the toast.
        /// </summary>
        /// <param name="launchArgs">Custom app-defined launch arguments to be passed along on toast activation</param>
        /// <param name="activationType">Set the activation type that will be used when the user click on this toast</param>
        /// <returns>The current instance of <see cref="NotificationBuilder"/></returns>
        public NotificationBuilder SetLaunchArgs(string launchArgs, NotificationActivationType activationType = NotificationActivationType.Foreground)
        {
            _content.Launch = launchArgs;
            _content.ActivationType = activationType;
            return this;
        }

        /// <summary>
        /// Sets the amount of time the Toast should display. You typically should use the
        /// Scenario attribute instead, which impacts how long a Toast stays on screen.
        /// </summary>
        /// <param name="duration">Duration of the toast</param>
        /// <returns>The current instance of <see cref="NotificationBuilder"/></returns>
        public NotificationBuilder SetDuration(ToastDuration duration)
        {
            _content.Duration = duration;
            return this;
        }

        /// <summary>
        ///  Sets the scenario, to make the Toast behave like an alarm, reminder, or more.
        /// </summary>
        /// <param name="scenario">Scenario to be used for the toast's behavior</param>
        /// <returns>The current instance of <see cref="NotificationBuilder"/></returns>
        public NotificationBuilder SetScenario(ToastScenario scenario)
        {
            _content.Scenario = scenario;
            return this;
        }

        /// <summary>
        /// Set custom audio to go along with the toast.
        /// </summary>
        /// <param name="src">Source to the media that will be played when the toast is pop</param>
        /// <param name="loop">Indicating whether sound should repeat as long as the Toast is shown; false to play only once (default).</param>
        /// <param name="silent">Indicating whether sound is muted; false to allow the Toast notification sound to play (default).</param>
        /// <returns>The current instance of <see cref="NotificationBuilder"/></returns>
        public NotificationBuilder SetAudio(Uri src, bool? loop = null, bool? silent = null)
        {
            if (!src.IsFile)
            {
                throw new ArgumentException(nameof(src), "Audio Source has to be a file.");
            }

            _content.Audio = new ToastAudio();
            _content.Audio.Src = src;

            if (loop != null)
            {
                _content.Audio.Loop = loop.Value;
            }

            if (silent != null)
            {
                _content.Audio.Silent = silent.Value;
            }

            return this;
        }

        /// <summary>
        /// Set custom audio to go along with the toast.
        /// </summary>
        /// <param name="audio">The <see cref="ToastAudio"/> to set.</param>
        /// <returns>The current instance of <see cref="NotificationBuilder"/></returns>
        public NotificationBuilder SetAudio(ToastAudio audio)
        {
            _content.Audio = audio;
            return this;
        }

        /// <summary>
        /// Retrieves the notification XML content as a string, so that it can be sent with a HTTP POST in a push notification.
        /// </summary>
        /// <returns>String of XML</returns>
        public string GetXmlString()
        {
            return _content.GetContent();
        }

#if WINDOWS_UWP
        /// <summary>
        /// Retrieves the notification XML content as a WinRT XmlDocument, so that it can be used with a local Toast notification's constructor on either <see cref="ToastNotification"/> or <see cref="ScheduledToastNotification"/>.
        /// </summary>
        /// <returns>The notification XML content as a WinRT XmlDocument.</returns>
        public Windows.Data.Xml.Dom.XmlDocument GetXmlDocument()
        {
            return _content.GetXml();
        }

        /// <summary>
        /// Shows a new notification with the current content.
        /// </summary>
        public void Show()
        {
            NotificationManager.CreateToastNotifier().Show(GetToastNotification());
        }

        /// <summary>
        /// Schedules the notification.
        /// </summary>
        /// <param name="deliveryTime">The date and time that Windows should display the toast notification. This time must be in the future.</param>
        public void Schedule(DateTimeOffset deliveryTime)
        {
            var notif = new ScheduledToastNotification(GetXmlDocument(), deliveryTime);

            if (_tag != null)
            {
                notif.Tag = _tag;
            }

            if (_group != null)
            {
                notif.Group = _group;
            }

            if (_suppressPopup)
            {
                notif.SuppressPopup = true;
            }

            if (_expirationTime != null)
            {
                notif.ExpirationTime = _expirationTime;
            }

            // TODO: Data and Priority don't exist on scheduled notifications?
            NotificationManager.CreateToastNotifier().AddToSchedule(notif);
        }
#endif
    }

#endif
}
