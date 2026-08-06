using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SimplePad.Core;
using System;
using Windows.ApplicationModel;

namespace SimplePad.Settings;

public sealed partial class SettingsView : UserControl
{
    private readonly SettingsState _settingsState;

    public SettingsView()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
        InitializeVersionText();

        UpdateVisibility();
        UpdateFontSettingsExpander();
    }

    internal void ScrollToTop()
    {
        ContentScrollViewer.ChangeView(null, 0, null);
    }

    private void InitializeVersionText()
    {
        PackageVersion version = Package.Current.Id.Version;
        VersionText.Text = $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }

    private void OnFontSettingsExpanderCollapsed(object sender, EventArgs e)
    {
        _settingsState.IsFontSettingsExpanded = false;
    }

    private void OnFontSettingsExpanderExpanded(object sender, EventArgs e)
    {
        _settingsState.IsFontSettingsExpanded = true;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
        _settingsState.IsFontSettingsExpandedChanged += OnSettingsStateIsFontSettingsExpandedChanged;

        UpdateVisibility();
        UpdateFontSettingsExpander();
    }

    private void OnSettingsStateIsFontSettingsExpandedChanged(object? sender, bool e)
    {
        _settingsState.IsFontSettingsExpanded = false;
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        _settingsState.IsVisibleChanged -= OnSettingsStateIsVisibleChanged;
        _settingsState.IsFontSettingsExpandedChanged -= OnSettingsStateIsFontSettingsExpandedChanged;
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