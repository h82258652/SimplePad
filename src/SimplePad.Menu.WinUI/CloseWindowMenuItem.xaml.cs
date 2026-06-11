using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed partial class CloseWindowMenuItem : MenuFlyoutItem
{
    private readonly IAppWindowManager _appWindowManager;

    public CloseWindowMenuItem()
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