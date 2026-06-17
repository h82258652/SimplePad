using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Windowing;

namespace SimplePad.Menu;

public partial class CloseWindowMenuItem : MenuItem
{
    private readonly IAppWindowManager _appWindowManager;

    public CloseWindowMenuItem()
    {
        _appWindowManager = ServiceLocator.Current.GetRequiredService<IAppWindowManager>();

        InitializeComponent();
    }

    private async void OnClick(object? sender, RoutedEventArgs e)
    {
        if (_appWindowManager.CurrentWindow is { } currentWindow)
        {
            _ = await _appWindowManager.CloseAsync(currentWindow);
        }
    }
}