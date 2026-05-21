using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public partial class NewWindowMenuItem : MenuItem
{
    private readonly IAppWindowManager _appWindowManager;

    public NewWindowMenuItem()
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