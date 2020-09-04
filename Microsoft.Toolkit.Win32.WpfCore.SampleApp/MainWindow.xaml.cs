// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows;
using Microsoft.UI.Notifications;
using SimpleToasts;

namespace Microsoft.Toolkit.Win32.WpfCore.SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonShowToast_Click(object sender, RoutedEventArgs e)
        {
            new NotificationBuilder()
                .AddText("Hello from WPF!")
                .Show();
        }

        private void ButtonClearToasts_Click(object sender, RoutedEventArgs e)
        {
            NotificationManager.Clear();
        }

        private void ExistingSdkInterop()
        {
            var notif = SimpleToast.CreateNotification("My old toast"); // Returns Windows.UI.Notification.ToastNotification
            NotificationBuilder.FromToastNotification(notif).Show();

            var xml = SimpleToast.CreateXml("My old toast XML");
            NotificationBuilder.FromXmlDocument(xml).Show();

            // The following ShowNotification APIs will work for UWP apps as they always have, but won't work for plain Win32 apps (they didn't work before, the SDK needs to update to Reunion). UWP apps should still be able to use these old SDKs, including using the new builders with the old SDKs...
            SimpleToast.ShowNotification("Sent through old platform APIs");
            SimpleToast.ShowNotification(new NotificationBuilder().AddText("Constructed with new, but sent through old").GetToastNotification());
            SimpleToast.ShowNotification(new NotificationBuilder().AddText("Constructed with new, but sent through old").GetXmlDocument());
        }
    }
}

#pragma warning disable SA1403 // File may only contain a single namespace
namespace SimpleToasts
#pragma warning restore SA1403 // File may only contain a single namespace
{
    /// <summary>
    /// Example old SDK that hasn't updated to Reunion yet, can still use it!
    /// </summary>
#pragma warning disable SA1402 // File may only contain a single type
    public class SimpleToast
#pragma warning restore SA1402 // File may only contain a single type
    {
        public static Windows.UI.Notifications.ToastNotification CreateNotification(string title)
        {
            return new Windows.UI.Notifications.ToastNotification(new NotificationBuilder().AddText(title).GetXmlDocument());
        }

        public static Windows.Data.Xml.Dom.XmlDocument CreateXml(string title)
        {
            return new NotificationBuilder().AddText(title).GetXmlDocument();
        }

        public static void ShowNotification(string title)
        {
            Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().Show(CreateNotification(title));
        }

        public static void ShowNotification(Windows.UI.Notifications.ToastNotification notif)
        {
            Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier().Show(notif);
        }

        public static void ShowNotification(Windows.Data.Xml.Dom.XmlDocument doc)
        {
            ShowNotification(new Windows.UI.Notifications.ToastNotification(doc));
        }
    }
}