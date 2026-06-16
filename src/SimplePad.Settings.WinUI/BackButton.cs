using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;

namespace SimplePad.Settings;

public sealed partial class BackButton : Button
{
    private readonly SettingsState _settingsState;

    public BackButton()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = false;
    }
}
