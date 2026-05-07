using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;

namespace SimplePad.Settings;

public sealed partial class SettingsView : UserControl
{
    private readonly SettingsState _settingsState;

    public SettingsView()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
        ApplicationThemeManager.Apply(this);
        InitializeVersionText();

        UpdateVisibility();
        UpdateFontSettingsExpander();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
        _settingsState.IsFontSettingsExpandedChanged += OnSettingsStateIsFontSettingsExpandedChanged;
    }

    private void InitializeVersionText()
    {
        Version version = (Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly()).GetName().Version ?? new Version(1, 0, 0, 0);
        VersionText.Text = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
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