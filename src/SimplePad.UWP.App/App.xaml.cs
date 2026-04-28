using Microsoft.Extensions.DependencyInjection;
using SimplePad.Editor;
using SimplePad.File;
using SimplePad.Fonts;
using SimplePad.Search;
using SimplePad.StatusBar;
using SimplePad.Tabs;
using SimplePad.Themes;
using SimplePad.Windowing;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SimplePad.App;

public sealed partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        InitializeComponent();

        Suspending += OnSuspending;
    }

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

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        if (Window.Current.Content is not ShellView shellView)
        {
            IAppWindowManager appWindowManager = _serviceProvider.GetRequiredService<IAppWindowManager>();

            IAppWindow appWindow = appWindowManager.CreateAppWindow();
            appWindow.Execute(window => window.TabRoot.AddBlankTab());
            shellView = new ShellView(appWindow);

            ExtendViewIntoTitleBar();

            Window.Current.Content = shellView;
        }

        if (e.PrelaunchActivated == false)
        {
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

    private async void OnSuspending(object sender, SuspendingEventArgs e)
    {
        SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

        try
        {
            await Task.WhenAll(
                _serviceProvider.GetRequiredService<IEditorSettings>().SaveAsync(),
                _serviceProvider.GetRequiredService<IFontSettings>().SaveAsync(),
                _serviceProvider.GetRequiredService<ISearchSettings>().SaveAsync(),
                _serviceProvider.GetRequiredService<IStatusBarSettings>().SaveAsync(),
                _serviceProvider.GetRequiredService<ITabsSettings>().SaveAsync(),
                _serviceProvider.GetRequiredService<IThemeSettings>().SaveAsync());
        }
        finally
        {
            deferral.Complete();
        }
    }
}