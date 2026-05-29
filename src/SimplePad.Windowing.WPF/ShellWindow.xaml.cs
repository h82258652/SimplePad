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

    public ShellWindow(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();
        AppWindow = appWindow;

        InitializeComponent();
        TabView.TabRoot = appWindow.TabRoot;

        UpdateContentGridVisibility();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    internal IAppWindow AppWindow { get; }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateContentGridVisibility();
    }

    private void OnTitleBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
        else
        {
            DragMove();
        }
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