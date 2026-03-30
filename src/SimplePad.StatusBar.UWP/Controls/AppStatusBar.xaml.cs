using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.UWP.Extensions;
using SimplePad.StatusBar.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.StatusBar.UWP.Controls;

public sealed partial class AppStatusBar : UserControl
{
    private readonly IStatusBarSettings _statusBarSettings;

    public AppStatusBar()
    {
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();

        InitializeComponent();

        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
    }

    private void UpdateVisibility()
    {
        Visibility = _statusBarSettings.IsStatusBarVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    private async void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        await Dispatcher.SafeRunAsync(UpdateVisibility);
    }
}
