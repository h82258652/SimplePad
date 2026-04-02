using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu.UWP.Controls;

public sealed partial class AppMenuBar : MenuBar
{
    private readonly IAppWindowManager _appWindowManager;

    public AppMenuBar()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private async void OnNewWindowClick(object sender, RoutedEventArgs e)
    {
        await _appWindowManager.ShowNewWindowAsync();
    }

    private void OnFontClick(object sender, RoutedEventArgs e)
    {

    }
}
