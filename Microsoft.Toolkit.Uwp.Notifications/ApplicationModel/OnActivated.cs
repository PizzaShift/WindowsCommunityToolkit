#if WINDOWS_UWP

namespace Microsoft.ApplicationModel
{
    /// <summary>
    /// Event triggered when a notification is clicked.
    /// </summary>
    /// <param name="e">Arguments that specify what action was taken and the user inputs.</param>
    public delegate void OnActivated(Activation.IActivatedEventArgs e);
}
#endif