using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;

namespace SimplePad.Menu;

public partial class FontMenuItem : MenuItem
{
    private readonly SettingsState _settingsState;

    public FontMenuItem()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = true;
        _settingsState.IsFontSettingsExpanded = true;
    }
}
