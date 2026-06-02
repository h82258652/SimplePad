using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;

namespace SimplePad.Settings;

public sealed class BackButton : Button
{
    private readonly SettingsState _settingsState;

    public BackButton()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        Click += OnClick;
    }

    private void OnClick(object? sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = false;
    }
}