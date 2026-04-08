using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    public IAppWindow CreateAppWindow()
    {
        UWPAppWindow instance = new(this);
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

    public async Task ShowNewWindowAsync()
    {
        CoreApplicationView newView = CoreApplication.CreateNewView();
        int newViewId = 0;
        await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            ExtendViewIntoTitleBar();

            Window.Current.Content = new ShellView(CreateAppWindow());
            Window.Current.Activate();

            newViewId = ApplicationView.GetForCurrentView().Id;
        });
        await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
    }
}
