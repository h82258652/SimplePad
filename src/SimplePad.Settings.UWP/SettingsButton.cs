using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

public sealed partial class SettingsButton : Button
{
    private readonly SettingsState _settingsState;

    public SettingsButton()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        DefaultStyleKey = typeof(SettingsButton);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Settings.UWP/SettingsButton.xaml"
        );

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = true;

        // TODO Scroll to the top of the settings page
    }
}