using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Settings;

internal sealed partial class BackButton : Button
{
    private readonly SettingsState _settingsState;

    internal BackButton()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = false;
    }
}
