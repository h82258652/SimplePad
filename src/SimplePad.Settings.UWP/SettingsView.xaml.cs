using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SimplePad.Settings;

public sealed partial class SettingsView : UserControl
{
    private readonly SettingsState _settingsState;

    public SettingsView()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();

        UpdateVisibility();
        UpdateXo();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
        _settingsState.IsFontSettingsExpandedChanged += _settingsState_IsFontSettingsExpandedChanged;
    }

    private void UpdateXo()
    {
        FontSettingsExpander.IsExpanded = _settingsState.IsFontSettingsExpanded;
    }

    private void _settingsState_IsFontSettingsExpandedChanged(object? sender, bool e)
    {
        UpdateXo();
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    public UIElement TitleBar => TitleBarElement;

    private void UpdateVisibility()
    {
        if (_settingsState is { IsVisible: true })
        {
            Visibility = Visibility.Visible;
        }
        else
        {
            Visibility = Visibility.Collapsed;
        }
    }

    private void FontSettingsExpander_Expanded(object sender, System.EventArgs e)
    {
        _settingsState.IsFontSettingsExpanded = true;
    }

    private void FontSettingsExpander_Collapsed(object sender, System.EventArgs e)
    {
        _settingsState.IsFontSettingsExpanded = false;
    }
}