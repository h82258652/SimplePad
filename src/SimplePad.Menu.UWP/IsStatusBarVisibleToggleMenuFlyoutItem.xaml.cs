using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Core.Extensions;
using SimplePad.StatusBar;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class IsStatusBarVisibleToggleMenuFlyoutItem : ToggleMenuFlyoutItem
{
    private readonly CoreDispatcher _dispatcher;
    private readonly IStatusBarSettings _statusBarSettings;

    public IsStatusBarVisibleToggleMenuFlyoutItem()
    {
        _dispatcher = Dispatcher;
        _statusBarSettings = ServiceLocator.Current.GetRequiredService<IStatusBarSettings>();

        InitializeComponent();

        UpdateIsChecked();

        _statusBarSettings.IsStatusBarVisibleChanged += OnStatusBarSettingsIsStatusBarVisibleChanged;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _statusBarSettings.IsStatusBarVisible = IsChecked;
    }

    private async void OnStatusBarSettingsIsStatusBarVisibleChanged(object? sender, bool e)
    {
        await _dispatcher.SafeRunAsync(UpdateIsChecked);
    }

    private void UpdateIsChecked()
    {
        IsChecked = _statusBarSettings.IsStatusBarVisible;
    }
}