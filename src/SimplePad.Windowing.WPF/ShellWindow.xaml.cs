using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public partial class ShellWindow : ThemeWindow
{
    private readonly SettingsState _settingsState;
    private readonly IThemeSettings _themeSettings;

    public ShellWindow(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();
        _themeSettings = ServiceLocator.Current.GetRequiredService<IThemeSettings>();
        AppWindow = appWindow;

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

        UpdateContentGridVisibility();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    internal IAppWindow AppWindow { get; }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        // If WindowChrome is used, the window background will be black. Re apply the theme to fix this.
        await Task.Yield();
        ThemeMode = ThemeMode.System;
        ThemeMode = ThemeMode.Light;
        ThemeMode = ThemeMode.Dark;
        ThemeMode = _themeSettings.AppTheme.GetThemeMode();
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateContentGridVisibility();
    }

    private void OnTitleBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        DragMove();
    }

    private void UpdateContentGridVisibility()
    {
        if (_settingsState.IsVisible)
        {
            ContentGrid.Visibility = Visibility.Collapsed;
        }
        else
        {
            ContentGrid.Visibility = Visibility.Visible;
        }
    }
}