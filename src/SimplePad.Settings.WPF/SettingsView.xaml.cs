using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace SimplePad.Settings;

public sealed partial class SettingsView : UserControl
{
    private readonly SettingsState _settingsState;

    private Window? _window;

    public SettingsView()
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();

        InitializeComponent();
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

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _window = Window.GetWindow(this);
        if (_window is not null)
        {
            _window.SizeChanged -= OnWindowSizeChanged;
            _window.SizeChanged += OnWindowSizeChanged;
        }

        UpdateVisualState();
    }

    private void OnSettingsStateIsFontSettingsExpandedChanged(object? sender, bool e)
    {
        UpdateFontSettingsExpander();
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateVisibility();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_window is not null)
        {
            _window.SizeChanged -= OnWindowSizeChanged;
        }

        _window = null;
    }

    private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateVisualState();
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

    private void UpdateVisualState()
    {
        if (_window is null)
        {
            return;
        }

        if (_window.ActualWidth < 646)
        {
            VisualStateManager.GoToState(this, nameof(Narrow), true);
            TitleText.Style = (Style)FindResource("TitleTextBlockStyle");
        }
        else if (_window.ActualWidth < 958)
        {
            VisualStateManager.GoToState(this, nameof(Middle), true);
            TitleText.Style = (Style)FindResource("TitleLargeTextBlockStyle");
        }
        else
        {
            VisualStateManager.GoToState(this, nameof(Wide), true);
            TitleText.Style = (Style)FindResource("TitleLargeTextBlockStyle");
        }
    }
}