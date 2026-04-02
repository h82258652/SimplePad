using System;
using System.Threading.Tasks;
using SimplePad.Windowing.Services;
using SimplePad.Windowing.UWP.Views;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace SimplePad.Windowing.UWP.Services;

public sealed class UWPAppWindowManager : IAppWindowManager
{
    public async Task ShowNewWindowAsync()
    {
        CoreApplicationView newView = CoreApplication.CreateNewView();
        int newViewId = 0;
        await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        {
            // TODO title bar

            Window.Current.Content = new ShellView(new AppWindowViewModel());
            Window.Current.Activate();

            newViewId = ApplicationView.GetForCurrentView().Id;
        });
        await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
    }
}
