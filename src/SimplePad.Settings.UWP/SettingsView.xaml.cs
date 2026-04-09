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
        UpdateFontSettingsExpander();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
        _settingsState.IsFontSettingsExpandedChanged += OnSettingsStateIsFontSettingsExpandedChanged;
    }

    public UIElement TitleBar => TitleBarElement;

    private void OnFontSettingsExpanderCollapsed(object sender, EventArgs e)
    {
        _settingsState.IsFontSettingsExpanded = false;
    }

    private void OnFontSettingsExpanderExpanded(object sender, EventArgs e)
    {
        _settingsState.IsFontSettingsExpanded = true;
    }

    private void OnSettingsStateIsFontSettingsExpandedChanged(object? sender, bool e)
    {
        UpdateFontSettingsExpander();
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void UpdateFontSettingsExpander()
    {
        FontSettingsExpander.IsExpanded = _settingsState.IsFontSettingsExpanded;
    }

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
}