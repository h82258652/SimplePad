using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.File;
using SimplePad.Windowing;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
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
        /// <param name="serviceProvider">TODO</param>
        public App(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            InitializeComponent();

            Suspending += OnSuspending;
        }

        private readonly IServiceProvider _serviceProvider;

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            if (args.Kind == ActivationKind.File)
            {
                var fileArgs = args as FileActivatedEventArgs;
                var f = fileArgs.Files[0] as StorageFile;

                IAppWindowManager appWindowManager = _serviceProvider.GetRequiredService<IAppWindowManager>();
                appWindowManager.CreateAppWindow().Execute(wwww =>
                {
                    wwww.TabRoot.AddTabFromFile(new UWPFile(f));
                });
            }
        }

        /// <inheritdoc/>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active.
            if (Window.Current.Content is not ShellView shellView)
            {
                IAppWindowManager appWindowManager = _serviceProvider.GetRequiredService<IAppWindowManager>();

                // Create a Frame to act as the navigation context and navigate to the first page
                var appWindow = appWindowManager.CreateAppWindow();
                appWindow.Execute(w => w.TabRoot.AddBlankTab());
                shellView = new ShellView(appWindow);

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
