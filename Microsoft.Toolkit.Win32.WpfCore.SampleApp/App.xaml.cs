// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows;
using Microsoft.ApplicationModel;
using Microsoft.ApplicationModel.Activation;
using Microsoft.UI.Notifications;

namespace Microsoft.Toolkit.Win32.WpfCore.SampleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Listen to activations
            AppLifecycle.OnActivated += this.AppLifecycle_OnActivated;

            if (!AppLifecycle.WasCurrentProcessActivated())
            {
                new MainWindow().Show();
            }
        }

        private void AppLifecycle_OnActivated(IActivatedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                OpenWindowIfNeeded();

                if (e is NotificationActivatedEventArgs notifArgs)
                {
                    var args = notifArgs.Argument;
                    var userInputCount = notifArgs.UserInput.Count;
                    MessageBox.Show("Activated from notification! Args: " + args);
                }
            });
        }

        private void OpenWindowIfNeeded()
        {
            // Make sure we have a window open (in case user clicked toast while app closed)
            if (App.Current.Windows.Count == 0)
            {
                new MainWindow().Show();
            }

            // Activate the window, bringing it to focus
            App.Current.Windows[0].Activate();

            // And make sure to maximize the window too, in case it was currently minimized
            App.Current.Windows[0].WindowState = WindowState.Normal;
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // If your app has an installer, you should call this when your app is uninstalled. Otherwise, if your app is a "portable app" and you no longer need notifications while the app is closed, you can call this upon exit.
            AppLifecycle.Uninstall();
        }
    }
}
