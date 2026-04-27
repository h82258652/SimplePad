using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Tabs;
using SimplePad.Themes;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SimplePad.Windowing;

public sealed class UWPAppWindowManager : IAppWindowManager
{
    private readonly SemaphoreSlim _closeLock = new(1);
    private readonly List<UWPAppWindow> _instances = [];
    private readonly IServiceProvider _serviceProvider;
    private readonly TabManager _tabManager;

    public UWPAppWindowManager(IServiceProvider serviceProvider, TabManager tabManager)
    {
        _serviceProvider = serviceProvider;
        _tabManager = tabManager;
    }

    public IAppWindow? CurrentWindow
    {
        get
        {
            CoreDispatcher dispatcher = CoreApplication.GetCurrentView().Dispatcher;
            return _instances.FirstOrDefault(instance => instance.Dispatcher == dispatcher);
        }
    }

    public IReadOnlyList<IAppWindow> Instances => _instances;

    public async Task<bool> CloseAsync(IAppWindow window)
    {
        if (window is not UWPAppWindow uwpWindow)
        {
            throw new InvalidOperationException();
        }

        if (!_instances.Contains(uwpWindow))
        {
            return false;
        }

        await _closeLock.WaitAsync();
        try
        {
            TaskCompletionSource<bool> tcs = new();
            uwpWindow.Execute(async uwpWindowInstance =>
            {
                foreach (Tab tab in uwpWindowInstance.TabRoot.Tabs.ToList())
                {
                    if (!await _tabManager.CloseAsync(tab))
                    {
                        tcs.SetResult(false);
                        return;
                    }
                }

                await ApplicationView.GetForCurrentView().TryConsolidateAsync();
                tcs.SetResult(true);
            });
            bool allTabsClsoed = await tcs.Task;
            if (allTabsClsoed)
            {
                _instances.Remove(uwpWindow);
            }

            return allTabsClsoed;
        }
        finally
        {
            _closeLock.Release();
        }
    }

    public IAppWindow CreateAppWindow()
    {
        var s = _serviceProvider.CreateScope();
        ServiceLocator.SetLocatorProvider(s.ServiceProvider);
        // todo dispose ?

        var d = CoreApplication.GetCurrentView().Dispatcher;

        UWPAppWindow instance = new(this, d, s.ServiceProvider.GetRequiredService<IThemeSettings>(), s.ServiceProvider.GetRequiredService<TabManager>());
        _instances.Add(instance);
        return instance;
    }

    public async Task<IAppWindow> ShowNewWindowAsync()
    {
        CoreApplicationView newView = CoreApplication.CreateNewView();
        int newViewId = 0;
        TaskCompletionSource<IAppWindow> tcs = new();
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

    private static void ExtendViewIntoTitleBar()
    {
        CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
        coreTitleBar.ExtendViewIntoTitleBar = true;

        ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
        titleBar.ButtonBackgroundColor = Colors.Transparent;
        titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
    }
}