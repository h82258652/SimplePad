using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public sealed partial class NewWindowMenuItem : MenuFlyoutItem
{
    private readonly IAppWindowManager _appWindowManager;

    public NewWindowMenuItem()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private async void OnClick(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        IAppWindow newAppWindow = await _appWindowManager.ShowNewWindowAsync();
        newAppWindow.Execute(appWindow => appWindow.TabRoot.AddBlankTab());
    }
}
