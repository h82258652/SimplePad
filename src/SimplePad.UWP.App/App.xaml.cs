using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SimplePad.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default <see cref="Application"/> class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            Suspending += OnSuspending;
        }

        /// <inheritdoc/>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active.
            if (Window.Current.Content is not ShellView shellView)
            {
                IAppWindowManager appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

                // Create a Frame to act as the navigation context and navigate to the first page
                shellView = new ShellView(appWindowManager.CreateAppWindow());

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                ExtendViewIntoTitleBar();

                // Place the frame in the current Window
                Window.Current.Content = shellView;
            }

            if (e.PrelaunchActivated == false)
            {
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        private static void ExtendViewIntoTitleBar()
        {
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
