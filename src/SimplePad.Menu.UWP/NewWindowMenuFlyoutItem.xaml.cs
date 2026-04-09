using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class NewWindowMenuFlyoutItem : MenuFlyoutItem
{
    private readonly IAppWindowManager _appWindowManager;

    public NewWindowMenuFlyoutItem()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private async void OnClick(object sender, RoutedEventArgs e)
    {
        IAppWindow newAppWindow = await _appWindowManager.ShowNewWindowAsync();
        newAppWindow.Execute(appWindow => appWindow.TabRoot.AddBlankTab());
    }
}