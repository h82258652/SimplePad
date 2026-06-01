using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.Tabs;
using SimplePad.Themes;

namespace SimplePad.Windowing;

public partial class ShellWindow : ThemeWindow
{
    private readonly SettingsState _settingsState;
    private readonly TabManager _tabManager;
    private readonly TabRoot _tabRoot;
    private bool _closeByProgramming;

    public ShellWindow(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();
        _tabRoot = appWindow.TabRoot;
        AppWindow = appWindow;

        InitializeComponent();
        TabView.TabRoot = _tabRoot;

        UpdateContentGridVisibility();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    internal IAppWindow AppWindow { get; }

    private async void OnClosing(object sender, CancelEventArgs e)
    {
        if (_closeByProgramming)
        {
            return;
        }

        e.Cancel = true;
        // CloseAsync will modify the Tabs collection, we cast to List first to avoid the "Collection was modified" exception.
        foreach (Tab tab in _tabRoot.Tabs.ToList())
        {
            if (!await _tabManager.CloseAsync(tab))
            {
                return;
            }
        }

        _closeByProgramming = true;
        Close();
    }

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