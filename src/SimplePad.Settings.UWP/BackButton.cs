using System;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

internal sealed partial class BackButton : Button
{
    private readonly SettingsState _settingsState;

    internal BackButton()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        DefaultStyleKey = typeof(BackButton);
        DefaultStyleResourceUri = new Uri("ms-appx:///SimplePad.Settings.UWP/BackButton.xaml");

        Click += OnClick;
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisible = false;
    }
}
