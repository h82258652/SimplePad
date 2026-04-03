using SimplePad.Settings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Menu;

public sealed partial class FontMenuFlyoutItem : MenuFlyoutItem
{
    public static readonly DependencyProperty SettingsStateProperty = DependencyProperty.Register(
        nameof(SettingsState),
        typeof(SettingsState),
        typeof(FontMenuFlyoutItem),
        null);

    public FontMenuFlyoutItem()
    {
        InitializeComponent();
    }

    public SettingsState? SettingsState
    {
        get => (SettingsState?)GetValue(SettingsStateProperty);
        set => SetValue(SettingsStateProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        if (SettingsState is { } settingsState)
        {
            settingsState.IsVisible = true;
            settingsState.IsFontSettingsExpanded = true;
        }
    }
}