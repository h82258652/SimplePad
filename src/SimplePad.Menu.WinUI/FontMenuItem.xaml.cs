using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using SimplePad.Settings;

namespace SimplePad.Menu;

public sealed partial class FontMenuItem : MenuFlyoutItem
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