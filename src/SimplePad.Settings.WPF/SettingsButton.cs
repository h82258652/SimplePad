using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System.Windows;
using System.Windows.Controls;

namespace SimplePad.Settings;

public sealed class SettingsButton : Button
{
    private readonly SettingsState _settingsState;

    static SettingsButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(SettingsButton), new FrameworkPropertyMetadata(typeof(SettingsButton)));
    }

    public SettingsButton()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = true;
    }
}
