using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class ExitMenuFlyoutItem : MenuFlyoutItem
{
    private readonly IAppWindowManager _appWindowManager;

    public ExitMenuFlyoutItem()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        foreach (IAppWindow appWindow in _appWindowManager.Instances)
        {
            _ = _appWindowManager.CloseAsync(appWindow);
        }
    }
}