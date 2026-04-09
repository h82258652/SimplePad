using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class FontMenuFlyoutItem : MenuFlyoutItem
{
    private readonly SettingsState _settingsState;

    public FontMenuFlyoutItem()
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