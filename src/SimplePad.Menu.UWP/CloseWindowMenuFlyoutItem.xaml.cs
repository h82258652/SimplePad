using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class CloseWindowMenuFlyoutItem : MenuFlyoutItem
{
    private readonly IAppWindowManager _appWindowManager;

    public CloseWindowMenuFlyoutItem()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        if (_appWindowManager.CurrentWindow is { } currentWindow)
        {
            _ = await _appWindowManager.CloseAsync(currentWindow);
        }
    }
}
