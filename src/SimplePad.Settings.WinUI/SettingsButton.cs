using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Settings;

public sealed partial class SettingsButton : Button
{
    private readonly SettingsState _settingsState;

    public SettingsButton()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = true;

        // TODO scroll to top of settings view
    }
}
