using System.Linq;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using SimplePad.Core;
using SimplePad.Settings;
using SimplePad.Tabs;

namespace SimplePad.Windowing;

public partial class ShellWindow : Window
{
    private readonly SettingsState _settingsState;
    private readonly TabManager _tabManager;
    private readonly TabRoot _tabRoot;

    public ShellWindow(IAppWindow appWindow)
    {
        _settingsState = ServiceLocator.Current.GetRequiredService<SettingsState>();
        _tabManager = ServiceLocator.Current.GetRequiredService<TabManager>();
        _tabRoot = appWindow.TabRoot;

        InitializeComponent();
        TabView.TabRoot = _tabRoot;

        UpdateContentPanelVisible();

        _settingsState.IsVisibleChanged += OnSettingsStateIsVisibleChanged;
    }

    private async void OnClosing(object? sender, WindowClosingEventArgs e)
    {
        e.Cancel = true;
        // CloseAsync will modify the Tabs collection, we cast to List first to avoid the "Collection was modified" exception.
        foreach (Tab tab in _tabRoot.Tabs.ToList())
        {
            if (!await _tabManager.CloseAsync(tab))
            {
                return;
            }
        }

        Close();
    }

    private void OnSettingsStateIsVisibleChanged(object? sender, bool e)
    {
        UpdateContentPanelVisible();
    }

    private void UpdateContentPanelVisible()
    {
        ContentPanel.IsVisible = !_settingsState.IsVisible;
    }
}