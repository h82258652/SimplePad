using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SimplePad.Settings;

public sealed partial class SettingsView : UserControl
{
    private readonly SettingsState _settingsState;

    public SettingsView()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
        // TODO init Version text

        UpdateVisibility();
        UpdateFontSettingsExpander();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
        _settingsState.IsFontSettingsExpandedChanged += OnSettingsStateIsFontSettingsExpandedChanged;
    }

    private void OnSettingsStateIsFontSettingsExpandedChanged(object? sender, bool e)
    {
        throw new NotImplementedException();
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        throw new NotImplementedException();
    }

    private void UpdateFontSettingsExpander()
    {
        throw new NotImplementedException();
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