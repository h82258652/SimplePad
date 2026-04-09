using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SimplePad.Windowing;

public sealed class UWPAppWindowManager : IAppWindowManager
{
    private readonly List<IAppWindow> _instances = [];

    public IReadOnlyList<IAppWindow> Instances => _instances;

    private readonly IServiceProvider _serviceProvider;

    public UWPAppWindowManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAppWindow CreateAppWindow()
    {
        var s = _serviceProvider.CreateScope();
        ServiceLocator.SetLocatorProvider(s.ServiceProvider);
        // todo dispose ?

        var d = CoreApplication.GetCurrentView().Dispatcher;

        UWPAppWindow instance = new(this, d);
        _instances.Add(instance);
        return instance;
    }

    private static void ExtendViewIntoTitleBar()
    {
        CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        coreTitleBar.ExtendViewIntoTitleBar = true;

        ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
    }

    public async Task<IAppWindow> ShowNewWindowAsync()
    {
        CoreApplicationView newView = CoreApplication.CreateNewView();
        int newViewId = 0;
        TaskCompletionSource<IAppWindow> tcs = new TaskCompletionSource<IAppWindow>();
        await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            ExtendViewIntoTitleBar();

            IAppWindow newAppWindow = CreateAppWindow();
            tcs.SetResult(newAppWindow);

            Window.Current.Content = new ShellView(newAppWindow);
            Window.Current.Activate();

            newViewId = ApplicationView.GetForCurrentView().Id;
        });
        await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);

        return await tcs.Task;
    }
}
