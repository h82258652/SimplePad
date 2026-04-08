using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

public sealed partial class SettingsButton : Button
{
    public static readonly DependencyProperty SettingsStateProperty = DependencyProperty.Register(
        nameof(SettingsState),
        typeof(SettingsState),
        typeof(SettingsButton),
        null);

    public SettingsButton()
    {
        DefaultStyleKey = typeof(SettingsButton);
        DefaultStyleResourceUri = new Uri(
            "ms-appx:///SimplePad.Settings.UWP/SettingsButton.xaml"
        );

        Click += OnClick;
    }

    public SettingsState? SettingsState
    {
        get => (SettingsState?)GetValue(SettingsStateProperty);
        set => SetValue(SettingsStateProperty, value);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        SettingsState?.IsVisible = true;
    }
}
